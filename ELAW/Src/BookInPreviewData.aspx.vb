Imports System.Data
Imports System.Data.OleDb
Partial Class Src_BookInPreviewData
    Inherits System.Web.UI.Page
    Dim cn As OleDbConnection
    Dim cmd As OleDbCommand
    Dim MD As New MainData
    Dim MC As New MainClass
    Dim DV As DataView
    Dim DVDIV As DataView
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
    Private Sub gDataDIV()
        'ดึงข้อมูลประเภทกฎหมาย แสดงใน Gridview Case
        Dim sEmpNo As String = Session("EmpNo")
        Dim strsql As String
        Dim chkdiv As String

        chkdiv = "select div_id from bookin_send where bookin_no='" & DS.Tables(0).Rows(0).Item("system_no").ToString & "'"


        strsql = " select div_id,div_name  "
        strsql &= "from division where dept_id=1 and div_id in (" & chkdiv & ") "


        Dim DT As DataTable = MD.GetDataTable(strsql)
        DVDIV = DT.DefaultView
        Session("datadiv") = DVDIV

    End Sub
    Private Sub MyGridBindDIV()
        gdvDiv.DataSource = DVDIV
        Dim X1() As String = {"div_id"}
        gdvDiv.DataKeyNames = X1
        gdvDiv.DataBind()
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim X As String = Request.QueryString("id")
        Dim status As String = Request.QueryString("status")

        Me.ChkPermis()

        If Not Page.IsPostBack Then

            If X <> "" Then
                'Preview, Approve, Edit
                Dim sql As String

                sql = " select b.bookin_id,b.system_no,b.bookin_no,b.bookkind_id,k.bookkind_name,  "
                sql &= "b.status_id,s.status_name,b.topic,b.keyword,b.recieve_date,b.stamp_date,b.from_name, "
                sql &= "b.present,b.priority_id,e.firstname+' '+e.lastname creation_name,e1.firstname+' '+e1.lastname send_name,"
                sql &= "p.priority_name,b.sendto_comment,b.sendto_comment1, "
                sql &= "e2.firstname+' '+e2.lastname sendto1,e3.firstname+' '+e3.lastname sendto2,"
                sql &= "b.sendto_date1,b.sendto_date2,"
                sql &= "case when b.status_id=3 then b.sendto1 else b.sendto2 end sendto,"
                sql &= "case when b.status_id=3 then b.sendto_comment1 else b.sendto_comment2 end sendto_comment, "
                sql &= "right(b.runno,4) runno "
                sql &= "from bookin_data b inner join book_kind k "
                sql &= "on b.bookkind_id=k.bookkind_id inner join book_status s "
                sql &= "on b.status_id=s.status_id inner join employee e "
                sql &= "on b.creation_by=e.empid inner join employee e1  "
                sql &= "on b.sendto=e1.empid inner join book_priority p "
                sql &= "on b.priority_id=p.priority_id left join employee e2 "
                sql &= "on b.sendto1=e2.empid left join employee e3 "
                sql &= "on b.sendto2=e3.empid "
                sql &= " where b.bookin_id='" & X & "'"


                DS = MD.GetDataset(sql)
                Session("BookInPreview") = DS
                iRec = 0
                ViewState("iRec") = iRec

                Me.MyDataBind()

                Me.gDataDIV()
                Me.MyGridBindDIV()

                Me.gData()
                Me.MyGridBind()
            Else
                'Add New
                Dim sql As String

                sql = " select * from bookin_data  "

                DS = MD.GetDataset(sql)
                Session("BookInPreview") = DS
                iRec = 0
                ViewState("iRec") = iRec

            End If

        Else


            DS = Session("BookInPreview")
            iRec = ViewState("iRec")


        End If

        lblPrint.Text = "<a href=""javascript:openwindow('" + "BookInPopupPreview" + "','" + X + "','" + "');"">" + "ดูรายละเอียด" + "</a>"
        lblPrintComment.Text = "<a href=""javascript:openwindow('" + "BookInPopupPreview" + "','" + X + "','" + "');"">" + "ดูความคิดเห็น" + "</a>"

    End Sub
    Private Sub gData()
        'ดึงข้อมูลมาแสดงใน Gridview 
        Dim X As String = Request.QueryString("id")
        Dim sEmpNo As String = Session("EmpNo")
        Dim strsql As String

        strsql = " select *  "
        strsql &= "from job_bookin "
        strsql &= "where ref_bookin='" & X & "'"

        Dim DT As DataTable = MD.GetDataTable(strsql)
        DVLst = DT.DefaultView
        'DVLst.Sort = "[" & ViewState("sortfield") & "] " & ViewState("sortdirection")
        Session("LinkBookIn") = DVLst

    End Sub
    Private Sub MyGridBind()
        GridView1.DataSource = DVLst
        Dim X1() As String = {"id"}
        GridView1.DataKeyNames = X1
        GridView1.DataBind()
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
            Case "recieve_date"
                If IsDBNull(DT.Rows(iRec)("recieve_date")) Then
                    Return ""
                Else
                    Dim P1 As Date = DT.Rows(iRec)("recieve_date")
                    Return P1.ToString("dd/MM/yyyy HH:mm")
                End If
            Case "stamp_date"
                If IsDBNull(DT.Rows(iRec)("stamp_date")) Then
                    Return ""
                Else
                    Dim P1 As Date = DT.Rows(iRec)("stamp_date")
                    Return P1.ToString("dd/MM/yyyy")
                End If
            Case "sendto_date1"
                If IsDBNull(DT.Rows(iRec)("sendto_date1")) Then
                    Return ""
                Else
                    Dim P1 As Date = DT.Rows(iRec)("sendto_date1")
                    Return P1.ToString("dd/MM/yyyy HH:mm")
                End If
            Case "sendto_date2"
                If IsDBNull(DT.Rows(iRec)("sendto_date2")) Then
                    Return ""
                Else
                    Dim P1 As Date = DT.Rows(iRec)("sendto_date2")
                    Return P1.ToString("dd/MM/yyyy HH:mm")
                End If
            Case Else
                Return DT.Rows(iRec)(FieldName) & ""
        End Select
    End Function
    Public Sub MyDataBind()
        lblBookNo.DataBind()
        lblBookType.DataBind()
        lblCreate.DataBind()
        lblFrom.DataBind()
        lblKeyword.DataBind()
        lblPresent.DataBind()
        lblPriority.DataBind()
        lblRecieveDate.DataBind()
        lblSendTo.DataBind()
        lblStampDate.DataBind()
        lblStatus.DataBind()
        lblTopic.DataBind()
        lblSendto1.DataBind()
        lblSendto_comment.DataBind()
        lblSendto_date1.DataBind()
        lblSendto2.DataBind()
        lblSendto_comment1.DataBind()
        lblSendto_date2.DataBind()
        lblRunNo.DataBind()
    End Sub
    Private Sub Auto()
        'Genarate Bookin_id
        Dim sqlTmp As String = ""
        Dim comTmp As OleDbCommand = New OleDbCommand
        Dim drTmp As OleDbDataReader
        Dim tmpMemberID As Integer = 0
        Dim tmpMemberID2 As String = ""


        Dim sYear As String
        sYear = Date.Now.Year

        Dim sAuto As String = sYear

        sqlTmp = "SELECT TOP 1 bookin_id FROM bookin_data "
        sqlTmp &= " WHERE left(bookin_id,4) ='" & sAuto & "'"
        sqlTmp &= " ORDER BY bookin_id DESC"

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

                tmpMemberID2 = Right(drTmp.Item("bookin_id"), 4)

                tmpMemberID = CInt(tmpMemberID2) + 1
                lblIdNew.Text = sAuto + tmpMemberID.ToString("-0000")

            End With
        Catch
            lblIdNew.Text = sAuto + "-0001"
        End Try
        cn.Close()

    End Sub
    Private Sub UpdateActive()
        Dim X As String = Request.QueryString("id")
        Dim sEmpNo As String = Session("EmpNo")

        Try
            Dim Strsql As String
            Strsql = "update bookin_data set  "
            Strsql &= " active = 0 "
            Strsql &= " where bookin_id='" & X & "'"

            Dim Y As Integer = MD.Execute(Strsql)

        Catch ex As Exception
            Dim ses As String = System.Web.HttpContext.Current.Session.SessionID
            Dim url As String = HttpContext.Current.Request.FilePath
            Dim browser As String = " Type:" + Request.Browser.Type + " Version:" + Request.Browser.Version + " Browser:" + Request.Browser.Browser

            MC.MessageBox(Me, ex.ToString)
            MC.ErrLog(url, ses, ex.ToString, sEmpNo, browser)
        End Try

    End Sub
    Protected Sub linkHome_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles linkHome.Click
        Response.Redirect(MD.pHome, True)
    End Sub
    Protected Sub link2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles link2.Click
        Response.Redirect("../Src/BookInDataList.aspx", True)
    End Sub
    Protected Sub GridView1_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridView1.PageIndexChanging
        GridView1.PageIndex = e.NewPageIndex
        Me.MyGridBind()
    End Sub
    Protected Sub GridView1_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridView1.RowCommand

        Dim pValue As String
        Dim row As GridViewRow = DirectCast(DirectCast(e.CommandSource, Control).NamingContainer, GridViewRow)
        Dim pName As String = Convert.ToString(GridView1.DataKeys(row.RowIndex).Value)

        Dim index As Integer = Convert.ToInt32(row.RowIndex)
        Dim row1 As GridViewRow = GridView1.Rows(index)
        Dim item As New ListItem()
        item.Text = Server.HtmlDecode(row1.Cells(2).Text)

        Select Case e.CommandName

            Case "cName"
                pValue = e.CommandArgument.ToString()
                If pValue = "1" Then
                    Response.Redirect("../Src/DrafLaw.aspx?id=" & pName & "&type=View", True)
                ElseIf pValue = "2" Then
                    Response.Redirect("../Src/CasePreview.aspx?id=" & pName & "&status=preview", True)
                ElseIf pValue = "3" Then
                    Response.Redirect("../Src/ContractPreview.aspx?id=" & pName & "&status=preview", True)
                End If

        End Select

    End Sub
    Protected Sub GridView1_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowCreated
        If e.Row.RowType = DataControlRowType.Pager Then
            Dim td As TableCell = e.Row.Cells(0)
            Dim Y As Boolean = False
            If GridView1.PageIndex > 0 Then
                Dim L1 As New LinkButton
                L1.ForeColor = Drawing.Color.White
                L1.Text = "First"
                td.Controls.Add(L1)
                AddHandler L1.Click, AddressOf FirstClick

                Y = True

                Dim L2 As New Literal
                L2.Text = " &nbsp;&nbsp;"
                td.Controls.Add(L2)

                L1 = New LinkButton
                L1.ForeColor = Drawing.Color.White
                L1.Text = "Prev"
                AddHandler L1.Click, AddressOf PrevClick
                td.Controls.Add(L1)
            End If
            If GridView1.PageIndex < GridView1.PageCount - 1 Then
                Dim L2 As Literal
                If Y = True Then
                    L2 = New Literal
                    L2.Text = " &nbsp;&nbsp;"
                    td.Controls.Add(L2)
                End If

                Dim L1 As New LinkButton
                L1.ForeColor = Drawing.Color.White
                L1.Text = "Next"
                AddHandler L1.Click, AddressOf NextClick
                td.Controls.Add(L1)

                L2 = New Literal
                L2.Text = " &nbsp;&nbsp;"
                td.Controls.Add(L2)


                L1 = New LinkButton
                L1.ForeColor = Drawing.Color.White
                L1.Text = "Last"
                AddHandler L1.Click, AddressOf LastClick
                td.Controls.Add(L1)
            End If
        End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            iRow = e.Row.DataItemIndex + 1
            'If Not e.Row.RowState And DataControlRowState.Edit Then
            '    Dim L1 As ImageButton = e.Row.Cells(16).Controls(1)
            '    L1.OnClientClick = "return confirm('คุณต้องการลบข้อมูลใช่หรือไม่?');"
            'End If
        End If


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
    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then

            DirectCast(e.Row.Cells(4).Controls(1), LinkButton).CommandArgument = DirectCast(e.Row.Cells(2).Controls(1), Label).Text

        End If


    End Sub
    Protected Sub GridView1_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles GridView1.Sorting
        If ViewState("sortfield") = e.SortExpression Then
            If ViewState("sortdirection") = "asc" Then ViewState("sortdirection") = "desc" Else ViewState("sortdirection") = "asc"
        Else
            ViewState("sortfield") = e.SortExpression
            ViewState("sortdirection") = "asc"
        End If
        DV.Sort = "[" & ViewState("sortfield") & "] " & ViewState("sortdirection")
        Me.MyGridBind()
    End Sub
End Class
