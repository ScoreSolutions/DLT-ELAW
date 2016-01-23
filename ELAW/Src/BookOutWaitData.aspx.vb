Imports System.IO
Imports System.Data
Imports System.Data.OleDb
Partial Class Src_BookOutWaitData
    Inherits System.Web.UI.Page
    Dim cn As OleDbConnection
    Dim cmd As OleDbCommand
    Dim MD As New MainData
    Dim MC As New MainClass
    Dim DV As DataView
    Dim DVLst As DataView
    Public iRow As Integer
    Dim DS As DataSet
    Dim iRec As Integer
    Private Sub ChkPermis()
        'ตรวจสอบสิทธิ์การใช้งาน
        Dim sEmpNo As String = Session("EmpNo")
        Dim url As String = HttpContext.Current.Request.FilePath
        If sEmpNo = "" Then
            Response.Redirect(MD.pLogin, True)
        Else
            Dim chk As Boolean = MC.ChkPermission(sEmpNo, url)
            If chk = False Then
                Response.Redirect(MD.pNoAut, True)
            End If
        End If
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim X As String = Request.QueryString("id")
        Dim status As String = Request.QueryString("status")

        Me.ChkPermis()

        If Not Page.IsPostBack Then

            If X <> "" Then
                'Preview, Approve, Edit
                Dim sql As String

                sql = " select b.bookout_id,b.system_no,b.bookout_no,b.bookkind_id,k.bookkind_name,b.present,  "
                sql &= "b.message,b.postscript,b.postname,b.post_pos,b.comment,b.contact,  "
                sql &= "b.status_id,s.status_name,b.topic,b.keyword,b.dates,e.firstname+' '+e.lastname createname, "
                sql &= "b.user_sign,e1.firstname+' '+e1.lastname signname,b.priority_id,p.priority_name,b.sendto,e2.firstname+' '+e2.lastname sendname, "
                sql &= "b.sendto_app,b.sendto_comment,t.type_name,b.ref_title,b.ref_id,b.ref_type,b.creation_by "
                sql &= "from bookout_data b inner join book_kind k "
                sql &= "on b.bookkind_id=k.bookkind_id inner join book_status s "
                sql &= "on b.status_id=s.status_id inner join employee e "
                sql &= "on b.creation_by=e.empid inner join employee e1 "
                sql &= "on b.user_sign=e1.empid inner join book_priority p "
                sql &= "on b.priority_id=p.priority_id inner join employee e2 "
                sql &= "on b.sendto=e2.empid inner join bookout_type t "
                sql &= "on b.booktype_id=t.type_id "
                sql &= "where b.active=1 "
                sql &= "and b.bookout_id='" & X & "'"

                DS = MD.GetDataset(sql)
                Session("BookOutWaitData") = DS
                iRec = 0
                ViewState("iRec") = iRec

                lblId.Text = X

                Me.DataSentTo()
                Me.MyDataBind()


                If DS.Tables(0).Rows(0).Item("ref_title").ToString = "" Then
                    LinkDetail.Visible = False
                Else
                    LinkDetail.Visible = True
                End If

                Me.gDataDoc()
                Me.MyGridBind()

            Else
                'Add New
                Dim sql As String

                sql = "select * from bookout_data "

                DS = MD.GetDataset(sql)
                Session("BookOutWaitData") = DS
                iRec = 0
                ViewState("iRec") = iRec

                lblId.Text = ""
                lblMainStatus.Text = "Add"

            End If
        Else
            DS = Session("BookOutWaitData")
            iRec = ViewState("iRec")
        End If
        bSaveAndSend.OnClientClick = "return confirm('ยืนยันการส่งข้อมูล');"

        If DS.Tables(0).Rows(0).Item("ref_type").ToString() = "1" Then
            LinkDetail.Text = "<a href=""javascript:openwindow('" + "PrintLaw" + "','" + DS.Tables(0).Rows(0).Item("ref_id").ToString + "','" + "');"">" + "พิมพ์รายละเอียด" + "</a>"
        ElseIf DS.Tables(0).Rows(0).Item("ref_type").ToString() = "2" Then
            LinkDetail.Text = "<a href=""javascript:openwindow('" + "PrintCase" + "','" + DS.Tables(0).Rows(0).Item("ref_id").ToString + "','" + "');"">" + "พิมพ์รายละเอียด" + "</a>"
        ElseIf DS.Tables(0).Rows(0).Item("ref_type").ToString() = "3" Then
            LinkDetail.Text = "<a href=""javascript:openwindow('" + "PrintContract" + "','" + DS.Tables(0).Rows(0).Item("ref_id").ToString + "','" + "');"">" + "ดูรายละเอียด" + "</a>"
        Else
            LinkDetail.Visible = False
        End If

    End Sub
    Public Sub DataSentTo()
        'รายชื่อหัวหน้ากลุ่ม
        Dim sEmpNo As String = Session("EmpNo")
        Dim strsql As String
        If rdoApp.SelectedValue = "T" Then
            strsql = "select e.empid,e.firstname+' '+e.lastname nameapp1   "
            strsql &= "from employee e inner join division d "
            strsql &= "on e.div_id = d.div_id and e.dept_id=1 and d.div_id = 7 and e.pos_id=  "
            strsql &= "(select pos_id from division where div_id=7 )"
            strsql &= "order by e.firstname+' '+e.lastname  "
        Else
            strsql = "select e.empid,e.firstname+' '+e.lastname nameapp1   "
            strsql &= "from employee e where e.dept_id=1 "
        End If

        Dim DTS As DataTable = MD.GetDataTable(strsql)
        Dim dr As DataRow = DTS.NewRow
        dr!empid = 0
        dr!nameapp1 = "---------โปรดเลือก---------"
        DTS.Rows.InsertAt(dr, 0)
        ddlSentTo.DataTextField = "nameapp1"
        ddlSentTo.DataValueField = "empid"
        ddlSentTo.DataSource = DTS
        ddlSentTo.DataBind()

    End Sub
    Public Function BindField(ByVal FieldName As String) As String
        'BindField when Preview,Approve,Edit
        Dim DT As DataTable = DS.Tables(0)
        Select Case FieldName
            Case "cost"
                If IsDBNull(DT.Rows(iRec)("cost")) Then
                    Return "0.00"
                Else
                    Dim P1 As Double = DT.Rows(iRec)("cost")
                    Return P1.ToString("#,##0.00")
                End If
            Case "dates"
                If IsDBNull(DT.Rows(iRec)("dates")) Then
                    Return ""
                Else
                    Dim P1 As Date = DT.Rows(iRec)("dates")
                    Return P1.ToString("dd/MM/yyyy")
                End If

            Case Else
                Return DT.Rows(iRec)(FieldName) & ""
        End Select
    End Function
    Public Sub MyDataBind()

        lblName.DataBind()
        lblStatus.DataBind()
        lblStatusId.DataBind()
        lblDate.DataBind()
        lblCreateName.DataBind()
        lblType.DataBind()
        lblPriority.DataBind()
        lblTopic.DataBind()
        lblPresent.DataBind()
        lblMessage.DataBind()
        lblPostScript.DataBind()
        lblPostName.DataBind()
        lblPostPosition.DataBind()
        lblContact.DataBind()
        lblComment.DataBind()
        lblKeyword.DataBind()
        txtComment.DataBind()
        lblSendto.DataBind()

        'If DS.Tables(0).Rows(0).Item("sendto_app").ToString <> "" Then
        rdoApp.SelectedValue = DS.Tables(0).Rows(0).Item("sendto_app").ToString
        'End If

        lblBookType.DataBind()
        lblRefTitle.DataBind()
        lblRefId.DataBind()
        lblRefType.DataBind()

        If rdoApp.SelectedValue = "F" Then
            Me.DataSentTo()
            ddlSentTo.Enabled = False
        Else
            Me.DataSentTo()
            ddlSentTo.Enabled = True
        End If

    End Sub
    Function CheckApprove(ByVal app As String, ByVal status As String) As String
        'ตรวจสอบการมอบหมายงาน
        Dim X As String = Request.QueryString("id")
        Dim sEmpNo As String = Session("EmpNo")
        Dim chk As String
        Dim oDs As DataSet

        chk = "select * from bookout_data "
        chk &= "where bookout_id='" & X & "' and status_id  =" & status & " and active=1 "
        chk &= "and " & app & "='" & sEmpNo & "'"

        oDs = MD.GetDataset(chk)

        If oDs.Tables(0).Rows.Count < 1 Then
            Dim aut As String
            aut = "select * from authorize "
            aut &= "where assign_from='" & DS.Tables(0).Rows(0).Item("" & app & "").ToString & "' "
            aut &= "and menu_id=58 "
            aut &= "and status_id =" & status & " "
            aut &= "and assign_to='" & sEmpNo & "' "
            aut &= "and convert(nvarchar(10),getdate(),120) between convert(nvarchar(10),date_from,120) and convert(nvarchar(10),date_to,120) "
            Return "1"
        Else
            Return "0"
        End If

    End Function
    Protected Sub bSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bSave.Click
        Dim sEmpNo As String = Session("EmpNo")
        Dim strsql As String
        Dim txtSent As String = ""

        If rdoApp.SelectedValue <> "T" And rdoApp.SelectedValue <> "F" And rdoApp.SelectedValue <> "N" Then
            lblChkComment.Text = ""
            lblChkApp.Text = "กรุณาเลือกความเห็น"
            Exit Sub
        End If

        If txtComment.Text = "" Then
            lblChkApp.Text = ""
            lblChkComment.Text = "กรุณาบันทึกความเห็น"
            txtComment.Focus()
            Exit Sub
        End If


        Try
            Dim app As String = Me.CheckApprove("sendto", "7")

            strsql = "update bookout_data set sendto_app=?,sendto_comment=?,sendto1=?, "
            strsql &= "updated_by=?,updated_date=?,sendto_ass1=?,sendto_date=? ,creation_by=? "
            strsql &= "where bookout_id=? "

            cn = New OleDbConnection(MD.Strcon)
            cmd = New OleDbCommand(strsql, cn)
            MD.CreateParam(cmd, "TTTTDTDTT")

            cmd.Parameters("@P1").Value = rdoApp.SelectedValue
            cmd.Parameters("@P2").Value = txtComment.Text

            If rdoApp.SelectedValue = "T" Then
                cmd.Parameters("@P3").Value = ddlSentTo.SelectedValue
            Else
                cmd.Parameters("@P3").Value = DBNull.Value
            End If

            cmd.Parameters("@P4").Value = sEmpNo
            cmd.Parameters("@P5").Value = DateTime.Parse(Date.Now)

            If app = "1" Then
                cmd.Parameters("@P6").Value = sEmpNo
            Else
                cmd.Parameters("@P6").Value = System.DBNull.Value
            End If
            cmd.Parameters("@P7").Value = Date.Now


            If rdoApp.SelectedValue = "N" Then
                cmd.Parameters("@P8").Value = ddlSentTo.SelectedValue
            Else
                cmd.Parameters("@P8").Value = DS.Tables(0).Rows(0).Item("creation_by").ToString
            End If


            cmd.Parameters("@P9").Value = lblId.Text

            cn.Open()
            cmd.ExecuteNonQuery()
            MC.MessageBox(Me, "บันทึกลงฐานข้อมูลเรียบร้อยแล้ว")
            lblChkApp.Text = ""
            lblChkComment.Text = ""

        Catch ex As Exception
            Dim ses As String = System.Web.HttpContext.Current.Session.SessionID
            Dim url As String = HttpContext.Current.Request.FilePath
            Dim browser As String = " Type:" + Request.Browser.Type + " Version:" + Request.Browser.Version + " Browser:" + Request.Browser.Browser

            MC.MessageBox(Me, ex.ToString)
            MC.ErrLog(url, ses, ex.ToString, sEmpNo, browser)

        Finally
            cn.Close()
        End Try
    End Sub
    Private Sub Auto()
        'Genarate Bookout_id
        Dim sqlTmp As String = ""
        Dim comTmp As OleDbCommand = New OleDbCommand
        Dim drTmp As OleDbDataReader
        Dim tmpMemberID As Integer = 0
        Dim tmpMemberID2 As String = ""


        Dim sYear As String
        sYear = Date.Now.Year

        Dim sAuto As String = sYear

        sqlTmp = "SELECT TOP 1 bookout_id FROM bookout_data "
        sqlTmp &= " WHERE left(bookout_id,4) ='" & sAuto & "'"
        sqlTmp &= " ORDER BY bookout_id DESC"

        Dim cn As New OleDbConnection(MD.Strcon)
        Dim cmd As New OleDbCommand(sqlTmp, cn)
        cn.Open()

        Try
            With comTmp
                .CommandType = CommandType.Text
                .CommandText = sqlTmp
                .Connection = cn
                drTmp = .ExecuteReader()

                drTmp.Read()

                tmpMemberID2 = Right(drTmp.Item("bookout_id"), 4)

                tmpMemberID = CInt(tmpMemberID2) + 1
                lblIdNew.Text = sAuto + tmpMemberID.ToString("-0000")

            End With
        Catch
            lblIdNew.Text = sAuto + "-0001"
        End Try
        cn.Close()

    End Sub
    Private Sub UpdateActive()
        'ก่อนเปลียนสถานะ ให้ update active=0
        Dim sEmpNo As String = Session("EmpNo")

        Try
            Dim Strsql As String
            Strsql = "update bookout_data set  "
            Strsql &= " active = 0 "
            Strsql &= " where bookout_id='" & lblId.Text & "'"

            Dim Y As Integer = MD.Execute(Strsql)

        Catch ex As Exception
            Dim ses As String = System.Web.HttpContext.Current.Session.SessionID
            Dim url As String = HttpContext.Current.Request.FilePath
            Dim browser As String = " Type:" + Request.Browser.Type + " Version:" + Request.Browser.Version + " Browser:" + Request.Browser.Browser

            MC.MessageBox(Me, ex.ToString)
            MC.ErrLog(url, ses, ex.ToString, sEmpNo, browser)
        End Try
    End Sub
    Protected Sub bSaveAndSend_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bSaveAndSend.Click
        Dim sEmpNo As String = Session("EmpNo")
        Dim strsql As String
        Dim chkState As String = ""

        If rdoApp.SelectedValue <> "T" And rdoApp.SelectedValue <> "F" And rdoApp.SelectedValue <> "N" Then
            lblChkComment.Text = ""
            lblChkSend.Text = ""
            lblChkApp.Text = "กรุณาเลือกความเห็น"
            Exit Sub
        End If

        If rdoApp.SelectedValue <> "F" Then
            If ddlSentTo.SelectedValue = "0" Then
                lblChkComment.Text = ""
                lblChkApp.Text = ""
                lblChkSend.Text = "กรุณาเลือกชื่อเพื่อส่งต่อ"
                Exit Sub
            End If
        End If


        If txtComment.Text = "" Then
            lblChkApp.Text = ""
            lblChkSend.Text = ""
            lblChkComment.Text = "กรุณาบันทึกความเห็น"
            txtComment.Focus()
            Exit Sub
        End If

        Dim Cname As String = ""
        Dim Pname As String = ""

        If rdoApp.SelectedValue = "T" Then
            chkState = "8"
            Pname = ddlSentTo.SelectedValue
            Cname = DS.Tables(0).Rows(0).Item("creation_by").ToString
        ElseIf rdoApp.SelectedValue = "F" Then
            chkState = "6"
            Pname = String.Empty
            Cname = DS.Tables(0).Rows(0).Item("creation_by").ToString
        ElseIf rdoApp.SelectedValue = "N" Then
            chkState = "6"
            Pname = String.Empty
            Cname = ddlSentTo.SelectedValue
        End If

        Me.UpdateActive()
        Me.Auto()

        Dim app As String = Me.CheckApprove("sendto", "7")

        If app = "1" Then
            strsql = "insert into bookout_data (bookout_id,system_no,dates,bookkind_id, "
            strsql &= "user_request,user_sign,status_id,priority_id,keyword,topic,present, "
            strsql &= "message,postscript,postname,post_pos,contact,comment,sendto,sendto_app,sendto_comment,sendto1,sendto_ass1, "
            strsql &= "creation_by,created_date,updated_by,updated_date,ref_type,ref_id,ref_title,booktype_id,sendto_date,runno ) "
            strsql &= "select '" & lblIdNew.Text & "',system_no,dates,bookkind_id,   "
            strsql &= "user_request,user_sign,'" & chkState & "',priority_id,keyword,topic,present, "
            strsql &= "message,postscript,postname,post_pos,contact,comment,sendto,'" & rdoApp.SelectedValue & "','" & txtComment.Text & "','" & ddlSentTo.SelectedValue & "','" & sEmpNo & "', "
            strsql &= "'" & Cname & "',created_date,'" & sEmpNo & "',getdate(),ref_type,ref_id,ref_title,booktype_id,getdate(),runno  "
            strsql &= "from bookout_data where bookout_id='" & lblId.Text & "'"

        Else
            strsql = "insert into bookout_data (bookout_id,system_no,dates,bookkind_id, "
            strsql &= "user_request,user_sign,status_id,priority_id,keyword,topic,present, "
            strsql &= "message,postscript,postname,post_pos,contact,comment,sendto,sendto_app,sendto_comment,sendto1, "
            strsql &= "creation_by,created_date,updated_by,updated_date,ref_type,ref_id,ref_title,booktype_id,sendto_date,runno ) "
            strsql &= "select '" & lblIdNew.Text & "',system_no,dates,bookkind_id,   "
            strsql &= "user_request,user_sign,'" & chkState & "',priority_id,keyword,topic,present, "
            strsql &= "message,postscript,postname,post_pos,contact,comment,sendto,'" & rdoApp.SelectedValue & "','" & txtComment.Text & "','" & ddlSentTo.SelectedValue & "', "
            strsql &= "'" & Cname & "',created_date,'" & sEmpNo & "',getdate(),ref_type,ref_id,ref_title,booktype_id,getdate(),runno  "
            strsql &= "from bookout_data where bookout_id='" & lblId.Text & "'"

        End If

        MD.Execute(strsql)

        lblChkApp.Text = ""
        lblChkComment.Text = ""

        Response.Redirect("../Src/BookOutWait.aspx", True)
    End Sub
    Protected Sub rdoApp_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdoApp.SelectedIndexChanged

        If rdoApp.SelectedValue = "F" Then
            Me.DataSentTo()
            ddlSentTo.Enabled = False
        Else
            Me.DataSentTo()
            ddlSentTo.Enabled = True
        End If

    End Sub

    Protected Sub link2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles link2.Click
        Response.Redirect("../Src/BookOutWait.aspx", True)
    End Sub

    Protected Sub linkHome_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles linkHome.Click
        Response.Redirect(MD.pHome, True)
    End Sub
    Private Sub gDataDoc(Optional ByVal Type As String = "")
        'Data in Gridview (Table BOOKOUT_DOCUMENT)
        Dim X As String = Request.QueryString("id")
        Dim strsql As String

        strsql = " select b.document_id,b.title,b.page  "
        strsql &= "from bookout_document b "
        strsql &= "where b.system_no='" & DS.Tables(0).Rows(0).Item("system_no").ToString & "'"

        Dim DT As DataTable = MD.GetDataTable(strsql)
        DVLst = DT.DefaultView
        Session("DocumentBookOut") = DVLst

    End Sub
    Private Sub MyGridBind()
        GridView1.DataSource = DVLst
        Dim X1() As String = {"document_id"}
        GridView1.DataKeyNames = X1
        GridView1.DataBind()
    End Sub
    Protected Sub GridView1_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridView1.PageIndexChanging
        GridView1.PageIndex = e.NewPageIndex
        Me.MyGridBind()
    End Sub
    Private Sub GoPage(ByVal xPage As Integer)
        GridView1.PageIndex = xPage
        Me.MyGridBind()
    End Sub
    Private Sub FirstClick(ByVal sender As Object, ByVal e As EventArgs)
        Me.GoPage(0)
    End Sub
    Private Sub PrevClick(ByVal sender As Object, ByVal e As EventArgs)
        Me.GoPage(GridView1.PageIndex - 1)
    End Sub
    Private Sub NextClick(ByVal sender As Object, ByVal e As EventArgs)
        Me.GoPage(GridView1.PageIndex + 1)
    End Sub
    Private Sub LastClick(ByVal sender As Object, ByVal e As EventArgs)
        Me.GoPage(GridView1.PageCount - 1)
    End Sub
    Protected Sub GridView1_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles GridView1.Sorting
        If ViewState("sortfield") = e.SortExpression Then
            If ViewState("sortdirection") = "asc" Then ViewState("sortdirection") = "desc" Else ViewState("sortdirection") = "asc"
        Else
            ViewState("sortfield") = e.SortExpression
            ViewState("sortdirection") = "asc"
        End If
        DVLst.Sort = "[" & ViewState("sortfield") & "] " & ViewState("sortdirection")
        Me.MyGridBind()
    End Sub
    Protected Sub GridView1_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowCreated
        'Create Page Gridview
        If e.Row.RowType = DataControlRowType.Pager Then
            Dim td As TableCell = e.Row.Cells(0)
            Dim Y As Boolean = False
            If GridView1.PageIndex > 0 Then
                Dim L1 As New LinkButton
                L1.ForeColor = Drawing.Color.Black
                L1.Text = "First"
                td.Controls.Add(L1)
                AddHandler L1.Click, AddressOf FirstClick

                Y = True

                Dim L2 As New Literal
                L2.Text = " &nbsp;&nbsp;"
                td.Controls.Add(L2)
                td.HorizontalAlign = HorizontalAlign.Left

                L1 = New LinkButton
                L1.ForeColor = Drawing.Color.Black
                L1.Text = "Prev"
                AddHandler L1.Click, AddressOf PrevClick
                td.Controls.Add(L1)
                td.HorizontalAlign = HorizontalAlign.Left

            End If
            If GridView1.PageIndex < GridView1.PageCount - 1 Then
                Dim L2 As Literal

                If Y = True Then
                    L2 = New Literal
                    L2.Text = " &nbsp;&nbsp;"
                    td.Controls.Add(L2)
                End If

                Dim L1 As New LinkButton
                L1.ForeColor = Drawing.Color.Black
                L1.Text = "Next"
                AddHandler L1.Click, AddressOf NextClick
                td.Controls.Add(L1)

                L2 = New Literal
                L2.Text = " &nbsp;&nbsp;"
                td.Controls.Add(L2)
                td.HorizontalAlign = HorizontalAlign.Left

                L1 = New LinkButton
                L1.ForeColor = Drawing.Color.Black
                L1.Text = "Last"
                AddHandler L1.Click, AddressOf LastClick
                td.Controls.Add(L1)
                td.HorizontalAlign = HorizontalAlign.Left
            End If
        End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            iRow = e.Row.DataItemIndex + 1
            If Not e.Row.RowState And DataControlRowState.Edit Then
                'Dim L1 As ImageButton = e.Row.Cells(3).Controls(0)
                'L1.OnClientClick = "return confirm('คุณต้องการลบข้อมูลใช่หรือไม่?');"
            End If
        End If
    End Sub
    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        'Download File Document
        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim K2 As String
            K2 = Convert.ToString(GridView1.DataKeys(e.Row.RowIndex).Values(0).ToString())
            Dim strsql2 As String = ""

            strsql2 = "select d.bookout_id,d.document_id, "
            strsql2 &= "d.file_path,d.mime_type "
            strsql2 &= "from bookout_document d "
            strsql2 &= "where bookout_id='" & lblId.Text & "' and d.document_id='" & K2(0) & "'"


            Dim dt As DataTable
            dt = MD.GetDataTable(strsql2)

            Dim lblLink As Label = e.Row.Cells(2).FindControl("lblLink")

            For Each dr As DataRow In dt.Rows

                If dr("mime_type").ToString() = ".pdf" Or dr("mime_type").ToString() = ".txt" Then
                    Dim strUrl As String = "http://" & Constant.BaseURL(Request) & "" & dr("file_path").ToString() & ""

                    lblLink.Text = "<a href='" & strUrl & "?time=" & Date.Now.ToString("HH:mm:ss") & "' Target='_Blank'>ดาวน์โหลด</a>&nbsp;&nbsp;"

                Else

                    Dim strUrl As String = "http://" & Constant.BaseURL(Request) & dr("file_path").ToString()
                    lblLink.Text = "<a href='" & strUrl & "?time=" & Date.Now.ToString("HH:mm:ss") & "' Target='_Blank'>ดาวน์โหลด</a>&nbsp;&nbsp;"

                End If

            Next

        End If

    End Sub
End Class

