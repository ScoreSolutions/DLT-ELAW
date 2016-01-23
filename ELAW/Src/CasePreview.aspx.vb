Imports System.IO
Imports System.Data
Imports System.Data.OleDb
Partial Class Src_CasePreview
    Inherits System.Web.UI.Page
    Dim MD As New MainData
    Dim MC As New MainClass
    Dim DV As DataView
    Dim oDate As New clsDate
    Public iRow As Integer
    Dim DS As DataSet
    Dim iRec As Integer
    Dim DVLst As DataView
    Dim DVCourt As DataView
    Dim DVExplain As DataView
    Dim DVLink As DataView
    Dim DVCase As DataView
    Private Sub ChkPermis()
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
        '
        Dim X As String = Request.QueryString("id")
        Dim status As String = Request.QueryString("status")
        Dim sEmpNo As String = Session("EmpNo")

        Me.ChkPermis()

        If Not Page.IsPostBack Then

            If X <> "" Then
                'Preview, Approve, Edit
                Dim sql As String

                sql = "select c.case_id,c.black_no,c.red_no,c.recieve_date,c.defendant,c.defendant1,c.prosecutor,c.prosecutor1,c.keyword,c.detail, "
                sql &= " c.status_id,s.status_name,c.type_id,t.type_name,c.court_id,co.court_name,a.attorney_id,a.attorney_name,a.tel  , "
                sql &= " c.app1,f1.name appname1,c.app2,f2.name appname2,c.recieve_type,c.case_no, "
                sql &= " c.app1_app,c.app1_comment,c.app2_app,c.app2_comment,c.ref_bookin,'เลขที่หนังสือ : '+b.bookin_no+' เรื่อง : '+b.topic topic,c.court_name cname,  "
                sql &= " f.name createname, "
                sql &= " case when c.detail_app1 is null then c.detail else c.detail_app1 end detail_app1, "
                sql &= " case when c.detail_app2 is null then c.detail else c.detail_app2 end detail_app2 "
                sql &= " from case_data c inner join case_status s "
                sql &= " on c.status_id=s.status_id inner join case_type t "
                sql &= " on c.type_id=t.type_id left join court co "
                sql &= " on c.court_id=co.court_id left join attorney a "
                sql &= " on c.attorney_id=a.attorney_id left join fullname f1 "
                sql &= " on c.app1=f1.empid left join fullname f2 "
                sql &= " on c.app2=f2.empid left join bookin_data b "
                sql &= " on c.ref_bookin=b.bookin_id inner join fullname f "
                sql &= " on c.creation_by=f.empid "
                sql &= " where c.case_id ='" & X & "'"


                DS = MD.GetDataset(sql)
                Session("pcase_data") = DS
                iRec = 0
                ViewState("iRec") = iRec

                Me.MyDataBind()

            Else
                'Add New
                Dim sql As String

                sql = "select * from case_data "


                DS = MD.GetDataset(sql)
                Session("pcase_data") = DS
                iRec = 0
                ViewState("iRec") = iRec

            End If

            'Show Gridview Document
            Me.gDataDoc()
            Me.MyGridBind()
            'Show Gridview CourtDate
            Me.gDataCourt()
            Me.MyGridBindCourt()
            'Show Gridveiw ExplainDate
            Me.gDataExplain()
            Me.MyGridBindExplain()
            'อ้างอิงหนังสือนำส่ง
            Me.gData()
            Me.MyGridBindLink()

            Me.gDataLinkCase()
            Me.MyGridBindLinkCase()

            ''Check status Enable and Visible Control 

            If status = "approve" Then

                Dim chk As String
                Dim oDs As DataSet

                chk = "select * from case_data "
                chk &= "where case_id='" & X & "' and status_id in (select step2 from case_process) and active=1 "

                oDs = MD.GetDataset(chk)

                If oDs.Tables(0).Rows.Count > 0 Then

                    TabPanel8.Visible = False
                    txtAppComment1.ReadOnly = True
                    bApp1.Visible = False
                    bAppAndSend1.Visible = False
                    bAppCancel1.Visible = False
                    TabContainer1.ActiveTabIndex = 5
                Else
                    chk = "select * from case_data "
                    chk &= "where case_id='" & X & "' and status_id in (select step3 from case_process) and active=1 "

                    oDs = MD.GetDataset(chk)

                    If oDs.Tables(0).Rows.Count > 0 Then

                        TabPanel8.Visible = False
                        txtAppComent.ReadOnly = True
                        bApp.Visible = False
                        bAppAndSend.Visible = False
                        bAppCancel.Visible = False
                        TabPanel8.Visible = False
                        TabContainer1.ActiveTabIndex = 6

                    End If


                End If

                Title = "พิจารณางานคดี"
                link2.Text = "รอพิจารณา"

            ElseIf status = "chkstate" Then

                Me.DataStatus()
                Title = "ปรับปรุงสถานะ"
                Me.EnableControl()
                TabContainer1.ActiveTabIndex = 7
                link2.Text = "ปรับปรุงสถานะคดี"

            ElseIf status = "preview" Then
                Title = "ดูรายละเอียด"
                Me.EnableControl()
                TabPanel8.Visible = False
            End If

        Else

            DS = Session("pcase_data")
            iRec = ViewState("iRec")

            If Session("DocumentCase") Is Nothing Then
                Me.gDataDoc()
            Else
                DVLst = Session("DocumentCase")
            End If

            If Session("DateCase") Is Nothing Then
                Me.gDataCourt()
            Else
                DVCourt = Session("DateCase")
            End If

            If Session("DateExplain") Is Nothing Then
                Me.gDataExplain()
            Else
                DVExplain = Session("DateExplain")
            End If

        End If

        lblPrint.Text = "<a href=""javascript:openwindow('" + "PrintCase" + "','" + X + "','" + "');"">" + "พิมพ์รายละเอียด" + "</a>"
        LPrint1.Text = "<a href=""javascript:openwindow('" + "PrintCaseDetail" + "','" + X + "','" + "');"">" + "พิมพ์รายละเอียด" + "</a>"

        bAppAndSend.OnClientClick = "return confirm('ยืนยันการส่งข้อมูล');"
        bAppAndSend1.OnClientClick = "return confirm('ยืนยันการส่งข้อมูล');"
        bUpdateStatus.OnClientClick = "return confirm('ยืนยันการส่งข้อมูล');"

        If DS.Tables(0).Rows(0).Item("ref_bookin").ToString() <> "" Then
            LinkDetail.Text = "<a href=""javascript:openwindow('" + "BookInPopupPreview" + "','" + DS.Tables(0).Rows(0).Item("ref_bookin").ToString + "','" + "');"">" + "ดูรายละเอียด" + "</a>"
        Else
            LinkDetail.Visible = False
        End If
    End Sub
    Function CheckApprove(ByVal app As String, ByVal status As String) As String
        Dim X As String = Request.QueryString("id")
        Dim sEmpNo As String = Session("EmpNo")
        Dim chk As String
        Dim oDs As DataSet

        chk = "select * from case_data "
        chk &= "where case_id='" & X & "' and status_id in (" & status & ") and active=1 "
        chk &= "and " & app & "='" & sEmpNo & "' "


        oDs = MD.GetDataset(chk)

        If oDs.Tables(0).Rows.Count < 1 Then
            Dim aut As String
            aut = "select * from authorize "
            aut &= "where assign_from='" & DS.Tables(0).Rows(0).Item("" & app & "").ToString & "' "
            aut &= "and menu_id=30 "
            aut &= "and status_id in (" & status & ") "
            aut &= "and assign_to='" & sEmpNo & "' "
            aut &= "and convert(nvarchar(10),getdate(),120) between convert(nvarchar(10),date_from,120) and convert(nvarchar(10),date_to,120) "
            Return "1"
        Else
            Return "0"
        End If

    End Function
    Private Sub EnableControl()

        txtAppComent.ReadOnly = True
        bApp.Visible = False
        bAppAndSend.Visible = False
        bAppCancel.Visible = False
        txtAppComment1.ReadOnly = True
        bApp1.Visible = False
        bAppAndSend1.Visible = False
        bAppCancel1.Visible = False

    End Sub
    Public Function BindField(ByVal FieldName As String) As String
        'BindField when Preview,Approve,Edit
        Dim DT As DataTable = DS.Tables(0)
        Select Case FieldName
            Case "money"
                If IsDBNull(DT.Rows(iRec)("money")) Then
                    Return "0.00"

                Else
                    Dim P1 As Double = DT.Rows(iRec)("money")
                    Return P1.ToString("#,##0.00")
                End If
            Case "recieve_date"
                If IsDBNull(DT.Rows(iRec)("recieve_date")) Then
                    Return ""

                Else
                    Dim P1 As Date = DT.Rows(iRec)("recieve_date")
                    Return P1.ToString("dd/MM/yyyy")
                End If

            Case Else
                Return DT.Rows(iRec)(FieldName) & ""
        End Select
    End Function
    Public Sub MyDataBind()
        'Databind at Control
        lblId.DataBind()
        lblBlackNo.DataBind()
        lblRecieveDate.DataBind()
        lblRedNo.DataBind()
        lblDefandent.DataBind()
        lblDefandent1.DataBind()
        lblProsecutor.DataBind()
        lblProsecutor1.DataBind()
        lblKeyword.DataBind()
        FCKeditor2.DataBind()
        lblApp1.DataBind()
        lblApp2.DataBind()
        lblAttornney.DataBind()
        lblCourt.DataBind()
        lblStatus.DataBind()
        lblCaseNo.DataBind()
        lblStatusId.DataBind()
        lblCaseType.DataBind()
        lblTel.DataBind()

        If DS.Tables(0).Rows(0).Item("recieve_type").ToString = 0 Then
            lblCloseRecieve.Text = "ปิดหมาย"
        ElseIf DS.Tables(0).Rows(0).Item("recieve_type").ToString = 1 Then
            lblCloseRecieve.Text = "รับหมาย"
        ElseIf DS.Tables(0).Rows(0).Item("recieve_type").ToString = 2 Then
            lblCloseRecieve.Text = "ส่งฟ้อง"
        End If

        If DS.Tables(0).Rows(0).Item("app1_app").ToString = "T" Or DS.Tables(0).Rows(0).Item("app1_app").ToString = "F" Then
            rdoApp.SelectedValue = DS.Tables(0).Rows(0).Item("app1_app").ToString
        End If

        If DS.Tables(0).Rows(0).Item("app2_app").ToString = "T" Or DS.Tables(0).Rows(0).Item("app2_app").ToString = "F" Then
            rdoApp1.SelectedValue = DS.Tables(0).Rows(0).Item("app2_app").ToString
        End If

        txtAppComent.DataBind()
        lblAppName.DataBind()

        txtAppComment1.DataBind()
        txtAppComment1.DataBind()
        lblTopic.DataBind()
        lblCourtName.DataBind()
        lblCreateName.DataBind()
        FCKeditorApp1.DataBind()
        FCKeditorApp2.DataBind()
    End Sub
    Private Sub gDataDoc(Optional ByVal Type As String = "")
        'Data in Gridview 
        Dim X As String = Request.QueryString("id")
        Dim strsql As String

        strsql = " select d.document_id,d.title,d.page  "
        strsql &= "from case_document d "
        strsql &= "where d.case_no='" & lblCaseNo.Text & "'"

        Dim DT As DataTable = MD.GetDataTable(strsql)
        DVLst = DT.DefaultView
        Session("DocumentCase") = DVLst

    End Sub
    Private Sub MyGridBind()
        GridView1.DataSource = DVLst
        Dim X1() As String = {"document_id"}
        GridView1.DataKeyNames = X1
        GridView1.DataBind()
    End Sub
    Private Sub gDataCourt(Optional ByVal Type As String = "")
        'Data in Gridview2 (Table COURT_DATE)
        Dim X As String = Request.QueryString("id")
        Dim strsql As String

        strsql = " select court_date_id,title,dates  "
        strsql &= "from court_date "
        strsql &= "where case_no='" & lblCaseNo.Text & "'"

        Dim DT As DataTable = MD.GetDataTable(strsql)
        DVCourt = DT.DefaultView
        Session("DateCase") = DVCourt

    End Sub
    Private Sub MyGridBindCourt()
        GridView2.DataSource = DVCourt
        Dim X1() As String = {"court_date_id"}
        GridView2.DataKeyNames = X1
        GridView2.DataBind()
    End Sub
    Private Sub gDataExplain(Optional ByVal Type As String = "")
        'Data in Gridview3 (Table EXPLAIN_DATE)
        Dim X As String = Request.QueryString("id")
        Dim strsql As String

        strsql = " select explain_date_id,title,dates  "
        strsql &= "from explain_date "
        strsql &= "where case_no='" & lblCaseNo.Text & "'"

        Dim DT As DataTable = MD.GetDataTable(strsql)
        DVExplain = DT.DefaultView
        Session("DateExplain") = DVExplain

    End Sub
    Private Sub MyGridBindExplain()
        GridView3.DataSource = DVExplain
        Dim X1() As String = {"explain_date_id"}
        GridView3.DataKeyNames = X1
        GridView3.DataBind()
    End Sub
    Protected Sub GridView1_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowCreated

    End Sub
    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        'Download File Document
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim X As String = Request.QueryString("id")
            Dim K2 As String
            K2 = Convert.ToString(GridView1.DataKeys(e.Row.RowIndex).Values(0).ToString())
            Dim strsql2 As String = ""


            strsql2 = "select d.case_id,d.document_id, "
            strsql2 &= "d.file_path,d.mime_type "
            strsql2 &= "from case_document d "
            strsql2 &= "where case_no='" & lblCaseNo.Text & "'"
            'strsql2 &= "where case_id='" & lblId.Text & "'"

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
    Private Sub UpdateActive()
        'Change Active เปลี่ยน Active เป็น 0 เมื่อมีการเปลี่ยนสถานะสัญญา
        Dim sEmpNo As String = Session("EmpNo")

        Try
            Dim Strsql As String
            Strsql = "update case_data set  "
            Strsql &= " active=0 "
            Strsql &= " where case_id='" & lblId.Text & "'"

            Dim Y As Integer = MD.Execute(Strsql)

        Catch ex As Exception
            Dim ses As String = System.Web.HttpContext.Current.Session.SessionID
            Dim url As String = HttpContext.Current.Request.FilePath
            Dim browser As String = " Type:" + Request.Browser.Type + " Version:" + Request.Browser.Version + " Browser:" + Request.Browser.Browser

            MC.MessageBox(Me, ex.ToString)
            MC.ErrLog(url, ses, ex.ToString, sEmpNo, browser)
        End Try

    End Sub
    Private Sub Auto()
        'Genarate Case_id
        Dim sqlTmp As String = ""
        Dim comTmp As OleDbCommand = New OleDbCommand
        Dim drTmp As OleDbDataReader
        Dim tmpMemberID As Integer = 0
        Dim tmpMemberID2 As String = ""


        Dim sYear As String
        sYear = Date.Now.Year

        Dim sAuto As String = sYear

        sqlTmp = "SELECT TOP 1 case_id FROM case_data "
        sqlTmp &= " WHERE left(case_id,4) ='" & sAuto & "'"
        sqlTmp &= " ORDER BY case_id DESC"

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

                tmpMemberID2 = Right(drTmp.Item("case_id"), 4)

                tmpMemberID = CInt(tmpMemberID2) + 1
                lblIdNew.Text = sAuto + tmpMemberID.ToString("-0000")

            End With
        Catch
            lblIdNew.Text = sAuto + "-0001"
        End Try
        cn.Close()

    End Sub
    Protected Sub bApp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bApp.Click
        'บันทึกข้อมูลแต่ยังไม่ส่งข้อมูล
        Dim X As String = Request.QueryString("id")
        Dim sEmpNo As String = Session("EmpNo")
        Dim strsql As String = ""
        If rdoApp.SelectedValue <> "T" And rdoApp.SelectedValue <> "F" Then
            lblAApp1.Text = "กรุณาเลือกความคิดเห็น"
            lblAComment1.Text = ""
            Exit Sub
        End If

        Dim app As String = Me.CheckApprove("app1", "select step2 from case_process")
        If app = "1" Then
            strsql = "update case_data set app1_app='" & rdoApp.SelectedValue & "',app1_comment='" & txtAppComent.Text & "',app1_date=getdate(),detail_app1='" & FCKeditorApp1.Value & "',app1_ass='" & sEmpNo & "' "
            strsql &= "where case_id='" & X & "'"
        Else
            strsql = "update case_data set app1_app='" & rdoApp.SelectedValue & "',app1_comment='" & txtAppComent.Text & "',app1_date=getdate(),detail_app1='" & FCKeditorApp1.Value & "' "
            strsql &= "where case_id='" & X & "'"
        End If
        Dim Y As Integer = MD.Execute(strsql)

        If Y > 0 Then
            MC.MessageBox(Me, "บันทึกลงฐานข้อมูลเรียบร้อยแล้ว")
            lblAApp1.Text = ""
            lblAComment1.Text = ""
        Else
            MC.MessageBox(Me, "ไม่สามารถบันทึกลงฐานข้อมูลได้")
        End If
    End Sub
    Protected Sub bAppAndSend_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bAppAndSend.Click
        'บันทึกพร้อมเปลี่ยนสถานะเพื่อส่งให้หัวหน้าตรวจสอบ
        Dim X As String = Request.QueryString("id")
        Dim sEmpNo As String = Session("EmpNo")
        Dim txtStatus As String = ""

        If rdoApp.SelectedValue <> "T" And rdoApp.SelectedValue <> "F" Then
            lblAApp1.Text = "กรุณาเลือกความคิดเห็น"
            lblAComment1.Text = ""
            Exit Sub
        End If

        If txtAppComent.Text.Trim = "" Then
            lblAComment1.Text = "กรุณากรอกความคิดเห็น"
            lblAApp1.Text = ""
            txtAppComent.Focus()
            Exit Sub
        End If

        If rdoApp.SelectedValue = "T" Then


            Dim str As String
            Dim oDs As DataSet
            str = "select * from case_process where step2='" & lblStatusId.Text & "' "
            oDs = MD.GetDataset(str)
            If oDs.Tables(0).Rows.Count > 0 Then
                txtStatus = oDs.Tables(0).Rows(0).Item("step3").ToString
            End If

        ElseIf rdoApp.SelectedValue = "F" Then

            Dim str As String
            Dim oDs As DataSet
            str = "select * from case_process where step2='" & lblStatusId.Text & "' "
            oDs = MD.GetDataset(str)
            If oDs.Tables(0).Rows.Count > 0 Then
                txtStatus = oDs.Tables(0).Rows(0).Item("step1").ToString
            End If
        End If


        Dim app As String = Me.CheckApprove("app1", "select step2 from case_process")
        Me.UpdateActive()
        Me.Auto()

        Try
            Dim Strsql As String = ""


            If app = "1" Then
                Strsql = "insert into case_data (case_id,type_id,case_no,status_id,court_id,attorney_id,black_no,red_no, "
                Strsql &= " recieve_type,recieve_date,defendant,defendant1,prosecutor,prosecutor1, "
                Strsql &= " app1,app1_app,app1_comment,app1_date,"
                Strsql &= " app2,detail,keyword,app1_ass, "
                Strsql &= " creation_by,created_date,updated_by,updated_date,ref_bookin,court_name,detail_app1,detail_app2)"

                Strsql &= " select '" & lblIdNew.Text & "',type_id,case_no,'" & txtStatus & "',court_id,attorney_id,black_no,red_no, "
                Strsql &= " recieve_type,recieve_date,defendant,defendant1,prosecutor,prosecutor1, "
                Strsql &= " app1,'" & rdoApp.SelectedValue & "','" & txtAppComent.Text & "',getdate(), "
                Strsql &= " app2,detail,keyword,'" & sEmpNo & "', "
                Strsql &= " creation_by,created_date,'" & sEmpNo & "',getdate(),ref_bookin,,court_name,'" & FCKeditorApp1.Value & "',detail_app2 "
                Strsql &= " from case_data "
                Strsql &= " where case_id='" & lblId.Text & "'"
            Else
                Strsql = "insert into case_data (case_id,type_id,case_no,status_id,court_id,attorney_id,black_no,red_no, "
                Strsql &= " recieve_type,recieve_date,defendant,defendant1,prosecutor,prosecutor1, "
                Strsql &= " app1,app1_app,app1_comment,app1_date,"
                Strsql &= " app2,detail,keyword, "
                Strsql &= " creation_by,created_date,updated_by,updated_date,ref_bookin,court_name,detail_app1,detail_app2)"

                Strsql &= " select '" & lblIdNew.Text & "',type_id,case_no,'" & txtStatus & "',court_id,attorney_id,black_no,red_no, "
                Strsql &= " recieve_type,recieve_date,defendant,defendant1,prosecutor,prosecutor1, "
                Strsql &= " app1,'" & rdoApp.SelectedValue & "','" & txtAppComent.Text & "',getdate(), "
                Strsql &= " app2,detail,keyword, "
                Strsql &= " creation_by,created_date,'" & sEmpNo & "',getdate(),ref_bookin,court_name,'" & FCKeditorApp1.Value & "',detail_app2 "
                Strsql &= " from case_data "
                Strsql &= " where case_id='" & lblId.Text & "'"
            End If

            Dim Y As Integer = MD.Execute(Strsql)
            If Y > 0 Then
              
                Response.Redirect("../Src/CaseApproveList.aspx", True)
            Else
                MC.MessageBox(Me, "ไม่สามารถบันทึกลงฐานข้อมูลได้")
            End If
        Catch ex As Exception
            Dim ses As String = System.Web.HttpContext.Current.Session.SessionID
            Dim url As String = HttpContext.Current.Request.FilePath
            Dim browser As String = " Type:" + Request.Browser.Type + " Version:" + Request.Browser.Version + " Browser:" + Request.Browser.Browser

            MC.MessageBox(Me, ex.ToString)
            MC.ErrLog(url, ses, ex.ToString, sEmpNo, browser)
        End Try
    End Sub
    Protected Sub bAppCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bAppCancel.Click
        txtAppComent.Text = ""
        lblAApp1.Text = ""
        lblAComment1.Text = ""
    End Sub
    Protected Sub bAppCancel1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bAppCancel1.Click
        txtAppComment1.Text = ""
        lblAApp2.Text = ""
        lblAComment2.Text = ""
    End Sub
    Protected Sub bApp1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bApp1.Click
        'บันทึกข้อมูลแต่ยังไม่ส่งข้อมูล
        Dim X As String = Request.QueryString("id")
        Dim sEmpNo As String = Session("EmpNo")
        Dim strsql As String = ""
        If rdoApp1.SelectedValue <> "T" And rdoApp1.SelectedValue <> "F" Then
            lblAApp2.Text = "กรุณาเลือกความคิดเห็น"
            lblAComment2.Text = ""
            Exit Sub
        End If

        Dim app As String = Me.CheckApprove("app2", "select step3 from case_process")
        If app = "1" Then
            strsql = "update case_data set app2_app='" & rdoApp1.SelectedValue & "',app2_comment='" & txtAppComment1.Text & "',app2_date=getdate(),detail_app2='" & FCKeditorApp2.Value & "',app2_ass='" & sEmpNo & "' "
            strsql &= "where case_id='" & X & "'"

        Else
            strsql = "update case_data set app2_app='" & rdoApp1.SelectedValue & "',app2_comment='" & txtAppComment1.Text & "',app2_date=getdate(),detail_app2='" & FCKeditorApp2.Value & "' "
            strsql &= "where case_id='" & X & "'"

        End If

        Dim Y As Integer = MD.Execute(strsql)

        If Y > 0 Then
            MC.MessageBox(Me, "บันทึกลงฐานข้อมูลเรียบร้อยแล้ว")
            lblAApp2.Text = ""
            lblAComment2.Text = ""
        Else
            MC.MessageBox(Me, "ไม่สามารถบันทึกลงฐานข้อมูลได้")
        End If
    End Sub
    Protected Sub bAppAndSend1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bAppAndSend1.Click
        'บันทึกพร้อมเปลี่ยนสถานะเพื่อส่งให้หัวหน้าตรวจสอบ
        Dim X As String = Request.QueryString("id")
        Dim sEmpNo As String = Session("EmpNo")
        Dim txtStatus As String = ""

        If rdoApp1.SelectedValue <> "T" And rdoApp1.SelectedValue <> "F" Then
            lblAApp2.Text = "กรุณาเลือกความคิดเห็น"
            lblAComment2.Text = ""
            Exit Sub
        End If

        If txtAppComment1.Text.Trim = "" Then
            lblAComment2.Text = "กรุณากรอกความคิดเห็น"
            lblAApp2.Text = ""
            txtAppComment1.Focus()
            Exit Sub
        End If


        If rdoApp1.SelectedValue = "T" Then



            Dim str As String
            Dim oDs As DataSet
            str = "select * from case_process where step3='" & lblStatusId.Text & "' "
            oDs = MD.GetDataset(str)
            If oDs.Tables(0).Rows.Count > 0 Then
                txtStatus = oDs.Tables(0).Rows(0).Item("step4").ToString
            End If

        ElseIf rdoApp1.SelectedValue = "F" Then

            Dim str As String
            Dim oDs As DataSet
            str = "select * from case_process where step3='" & lblStatusId.Text & "' "
            oDs = MD.GetDataset(str)
            If oDs.Tables(0).Rows.Count > 0 Then
                txtStatus = oDs.Tables(0).Rows(0).Item("step2").ToString
            End If
        End If


        Dim app As String = Me.CheckApprove("app2", "select step3 from case_process")
        Me.UpdateActive()
        Me.Auto()

        Try
            Dim Strsql As String = ""
            If app = "1" Then
                Strsql = "insert into case_data (case_id,type_id,case_no,status_id,court_id,attorney_id,black_no,red_no, "
                Strsql &= " recieve_type,recieve_date,defendant,defendant1,prosecutor,prosecutor1, "
                Strsql &= " app1,app1_app,app1_comment,app1_date,"
                Strsql &= " app2,app2_app,app2_comment,app2_date,detail,keyword,app1_ass,app2_ass, "
                Strsql &= " creation_by,created_date,updated_by,updated_date,ref_bookin,court_name,detail_app1,detail_app2)"

                Strsql &= " select '" & lblIdNew.Text & "',type_id,case_no,'" & txtStatus & "',court_id,attorney_id,black_no,red_no, "
                Strsql &= " recieve_type,recieve_date,defendant,defendant1,prosecutor,prosecutor1, "
                Strsql &= " app1,app1_app,app1_comment,app1_date,"
                Strsql &= " app2,'" & rdoApp1.SelectedValue & "','" & txtAppComment1.Text & "',getdate(),detail,keyword,app1_ass,'" & sEmpNo & "' "
                Strsql &= " creation_by,created_date,'" & sEmpNo & "',getdate(),ref_bookin,court_name,detail_app1,'" & FCKeditorApp2.Value & "' "
                Strsql &= " from case_data "
                Strsql &= " where case_id='" & lblId.Text & "'"
            Else
                Strsql = "insert into case_data (case_id,type_id,case_no,status_id,court_id,attorney_id,black_no,red_no, "
                Strsql &= " recieve_type,recieve_date,defendant,defendant1,prosecutor,prosecutor1, "
                Strsql &= " app1,app1_app,app1_comment,app1_date,"
                Strsql &= " app2,app2_app,app2_comment,app2_date,detail,keyword, "
                Strsql &= " creation_by,created_date,updated_by,updated_date,ref_bookin,court_name,detail_app1,detail_app2)"

                Strsql &= " select '" & lblIdNew.Text & "',type_id,case_no,'" & txtStatus & "',court_id,attorney_id,black_no,red_no, "
                Strsql &= " recieve_type,recieve_date,defendant,defendant1,prosecutor,prosecutor1, "
                Strsql &= " app1,app1_app,app1_comment,app1_date,"
                Strsql &= " app2,'" & rdoApp1.SelectedValue & "','" & txtAppComment1.Text & "',getdate(),detail,keyword, "
                Strsql &= " creation_by,created_date,'" & sEmpNo & "',getdate(),ref_bookin,court_name,detail_app1,'" & FCKeditorApp2.Value & "' "
                Strsql &= " from case_data "
                Strsql &= " where case_id='" & lblId.Text & "'"

            End If


            Dim Y As Integer = MD.Execute(Strsql)
            If Y > 0 Then
                Response.Redirect("../Src/CaseApproveList.aspx", True)
            Else
                MC.MessageBox(Me, "ไม่สามารถบันทึกลงฐานข้อมูลได้")
            End If
        Catch ex As Exception
            Dim ses As String = System.Web.HttpContext.Current.Session.SessionID
            Dim url As String = HttpContext.Current.Request.FilePath
            Dim browser As String = " Type:" + Request.Browser.Type + " Version:" + Request.Browser.Version + " Browser:" + Request.Browser.Browser

            MC.MessageBox(Me, ex.ToString)
            MC.ErrLog(url, ses, ex.ToString, sEmpNo, browser)
        End Try
    End Sub
    Public Sub DataStatus()

        Dim strsql As String
        strsql = "select status_id,status_name  "
        strsql &= "from case_status where type_id = '" & DS.Tables(0).Rows(0).Item("type_id").ToString & "' and status_id not in "
        strsql &= "(select step1 from case_process union "
        strsql &= "select step2 from case_process union "
        strsql &= "select step3 from case_process) "
        strsql &= "order by status_name "

        Dim DTS As DataTable = MD.GetDataTable(strsql)

        DDStatus.DataTextField = "status_name"
        DDStatus.DataValueField = "status_id"
        DDStatus.DataSource = DTS
        DDStatus.DataBind()

    End Sub
    Protected Sub bUpdateStatus_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bUpdateStatus.Click
        Dim X As String = Request.QueryString("id")
        Dim sEmpNo As String = Session("EmpNo")

        Me.UpdateActive()
        Me.Auto()

        Try
            Dim Strsql As String

            Strsql = "insert into case_data (case_id,type_id,case_no,status_id,court_id,attorney_id,black_no,red_no, "
            Strsql &= " recieve_type,recieve_date,defendant,defendant1,prosecutor,prosecutor1, "
            Strsql &= " app1,app1_app,app1_comment,app1_date,"
            Strsql &= " app2,app2_app,app2_comment,app2_date,detail,keyword,comment, "
            Strsql &= " creation_by,created_date,updated_by,updated_date,ref_bookin)"

            Strsql &= " select '" & lblIdNew.Text & "',type_id,case_no,'" & DDStatus.SelectedValue & "',court_id,attorney_id,black_no,red_no, "
            Strsql &= " recieve_type,recieve_date,defendant,defendant1,prosecutor,prosecutor1, "
            Strsql &= " app1,app1_app,app1_comment,app1_date,"
            Strsql &= " app2,'" & rdoApp1.SelectedValue & "','" & txtAppComment1.Text & "',getdate(),detail,keyword,'" & txtComment.Text & "', "
            Strsql &= " creation_by,created_date,'" & sEmpNo & "',getdate(),ref_bookin "
            Strsql &= " from case_data "
            Strsql &= " where case_id='" & lblId.Text & "'"

            Dim Y As Integer = MD.Execute(Strsql)
            If Y > 0 Then
                MC.MessageBox(Me, "บันทึกลงฐานข้อมูลเรียบร้อยแล้ว")
                Response.Redirect("../Src/CaseDataList.aspx", True)
            Else
                MC.MessageBox(Me, "ไม่สามารถบันทึกลงฐานข้อมูลได้")
            End If
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
        Dim status As String = Request.QueryString("status")
        If status = "approve" Then
            Response.Redirect("../Src/CaseApproveList.aspx", True)
        ElseIf status = "chkstate" Then
            Response.Redirect("../Src/CaseStatusList.aspx", True)
        Else
            Response.Redirect("../Src/CaseDataList.aspx", True)
        End If
    End Sub
    Private Sub gData()
        'ดึงข้อมูลมาแสดงใน Gridview 
        Dim X As String = Request.QueryString("id")
        Dim sEmpNo As String = Session("EmpNo")
        Dim strsql As String

        strsql = " select *  "
        strsql &= "from bookout_data "
        strsql &= "where ref_type=2 and ref_id='" & X & "'"

        Dim DT As DataTable = MD.GetDataTable(strsql)
        DVLink = DT.DefaultView
        'DVLink.Sort = "[" & ViewState("sortfield") & "] " & ViewState("sortdirection")
        Session("LinkBookOut") = DVLink

    End Sub
    Private Sub MyGridBindLink()
        GridView4.DataSource = DVLink
        Dim X1() As String = {"bookout_id"}
        GridView4.DataKeyNames = X1
        GridView4.DataBind()
    End Sub
    Protected Sub GridView4_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridView4.RowCommand


    End Sub
    Protected Sub GridView4_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView4.RowCreated
        If e.Row.RowType = DataControlRowType.Pager Then
            Dim td As TableCell = e.Row.Cells(0)
            Dim Y As Boolean = False
            If GridView4.PageIndex > 0 Then
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
            If GridView4.PageIndex < GridView4.PageCount - 1 Then
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
        GridView4.PageIndex = xPage
        Me.MyGridBindLink()
    End Sub
    Private Sub FirstClick(ByVal sender As Object, ByVal e As EventArgs)
        Me.GoPage(0)
    End Sub
    Private Sub PrevClick(ByVal sender As Object, ByVal e As EventArgs)
        Me.GoPage(GridView4.PageIndex - 1)
    End Sub
    Private Sub NextClick(ByVal sender As Object, ByVal e As EventArgs)
        Me.GoPage(GridView4.PageIndex + 1)
    End Sub
    Private Sub LastClick(ByVal sender As Object, ByVal e As EventArgs)
        Me.GoPage(GridView4.PageCount - 1)
    End Sub
    Protected Sub GridView4_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView4.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim K2 As String
            K2 = Convert.ToString(GridView4.DataKeys(e.Row.RowIndex).Values(0).ToString())
            Dim strsql2 As String = ""


            strsql2 = " select *  "
            strsql2 &= "from bookout_data "
            strsql2 &= "where bookout_id='" & K2 & "'"

            Dim dt As DataTable
            dt = MD.GetDataTable(strsql2)

            Dim lblLink As LinkButton = e.Row.Cells(3).FindControl("LinkName")

            For Each dr As DataRow In dt.Rows

                lblLink.Text = "<a href=""javascript:openwindow('" + "BookOutPreview" + "','" + dr("bookout_id").ToString() + "','" + "');"">" + dr("topic").ToString() + "</a>"

            Next


        End If

    End Sub
    Protected Sub GridView4_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles GridView4.Sorting
        If ViewState("sortfield") = e.SortExpression Then
            If ViewState("sortdirection") = "asc" Then ViewState("sortdirection") = "desc" Else ViewState("sortdirection") = "asc"
        Else
            ViewState("sortfield") = e.SortExpression
            ViewState("sortdirection") = "asc"
        End If
        DV.Sort = "[" & ViewState("sortfield") & "] " & ViewState("sortdirection")
        Me.MyGridBindLink()
    End Sub
    Private Sub gDataLinkCase()
        'ดึงข้อมูลมาแสดงใน Gridview 
        Dim X As String = Request.QueryString("id")
        Dim sEmpNo As String = Session("EmpNo")
        Dim strsql As String

        strsql = "select a.ref_case,c.case_id,c.case_no,'คดีดำ : '+c.black_no+' คดีแดง : '+c.red_no casename "
        strsql &= " from"
        strsql &= " (select c.ref_case "
        strsql &= " from CASE_DATA c"
        strsql &= " where c.case_id='" & X & "'"
        strsql &= " union"
        strsql &= " Select case_id"
        strsql &= " from CASE_DATA where ref_case in"
        strsql &= " (select c.ref_case "
        strsql &= " from CASE_DATA c"
        strsql &= " where c.case_id='" & X & "')"
        strsql &= " and active =1 and case_id <> '" & X & "')a"
        strsql &= " inner join case_data c"
        strsql &= " on a.ref_case=c.case_id"



        Dim DT As DataTable = MD.GetDataTable(strsql)
        DVCase = DT.DefaultView
        'DVContract.Sort = "[" & ViewState("sortfield") & "] " & ViewState("sortdirection")
        Session("LinkContract") = DVCase

    End Sub
    Private Sub MyGridBindLinkCase()
        GridView5.DataSource = DVCase
        Dim X1() As String = {"ref_case"}
        GridView5.DataKeyNames = X1
        GridView5.DataBind()
    End Sub
    Protected Sub GridView5_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView5.RowCreated
        If e.Row.RowType = DataControlRowType.DataRow Then
            iRow = e.Row.DataItemIndex + 1
            'If Not e.Row.RowState And DataControlRowState.Edit Then
            '    Dim L1 As ImageButton = e.Row.Cells(16).Controls(1)
            '    L1.OnClientClick = "return confirm('คุณต้องการลบข้อมูลใช่หรือไม่?');"
            'End If
        End If

    End Sub
    Protected Sub GridView5_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView5.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim K2 As String
            K2 = Convert.ToString(GridView5.DataKeys(e.Row.RowIndex).Values(0).ToString())
            Dim strsql2 As String = ""

            strsql2 = " select c.case_id,c.ref_case,'คดีดำ : '+case when c.black_no is null then '' else c.black_no end+' คดีแดง : '+case when c.red_no is null then '' else c.red_no end casename  "
            strsql2 &= "from case_data c "
            strsql2 &= "where c.case_id='" & K2 & "'"

            Dim dt As DataTable
            dt = MD.GetDataTable(strsql2)

            Dim lblLink As LinkButton = e.Row.Cells(2).FindControl("LinkCaseName")

            For Each dr As DataRow In dt.Rows

                lblLink.Text = "<a href=""javascript:openwindow('" + "PrintCase" + "','" + dr("case_id").ToString() + "','" + "');"">" + dr("casename").ToString() + "</a>"

            Next

        End If
    End Sub
End Class