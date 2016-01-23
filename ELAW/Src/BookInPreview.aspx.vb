Imports System.Data
Imports System.Data.OleDb
Partial Class Src_BookInPreview
    Inherits System.Web.UI.Page
    Dim cn As OleDbConnection
    Dim cmd As OleDbCommand
    Dim MD As New MainData
    Dim MC As New MainClass
    Dim DV As DataView
    Dim DVDIV As DataView
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
                sql &= "b.present,b.priority_id,b.creation_by,e.firstname+' '+e.lastname creation_name,e1.firstname+' '+e1.lastname send_name,"
                sql &= "p.priority_name,b.sendto,b.sendto1,b.sendto2, "
                sql &= "case when b.status_id=3 then b.sendto1 else b.sendto2 end send,"
                sql &= "case when b.status_id=3 then b.sendto_comment1 else b.sendto_comment2 end sendto_comment,b.send_type "
                sql &= "from bookin_data b inner join book_kind k "
                sql &= "on b.bookkind_id=k.bookkind_id inner join book_status s "
                sql &= "on b.status_id=s.status_id inner join employee e "
                sql &= "on b.creation_by=e.empid inner join employee e1  "
                sql &= "on b.sendto=e1.empid inner join book_priority p "
                sql &= "on b.priority_id=p.priority_id "
                sql &= " where b.bookin_id='" & X & "'"


                DS = MD.GetDataset(sql)
                Session("BookInPreview") = DS
                iRec = 0
                ViewState("iRec") = iRec


                Me.MyDataBind()
                Me.FindRow()

                Me.gDataDIV()
                Me.MyGridBindDIV()

                If txtComment.Text = "" Then
                    txtComment.Text = "เพื่อดำเนินการต่อไป"
                End If
            Else
                'Add New
                Dim sql As String

                sql = " select * from bookin_data  "

                DS = MD.GetDataset(sql)
                Session("BookInPreview") = DS
                iRec = 0
                ViewState("iRec") = iRec


            End If

            ''Check status Enable and Visible Control 

            If status = "wait" Then
                Me.DataSendTo()
                MultiViewApp.ActiveViewIndex = 0
                Title = "ดูรายละเอียด"

            ElseIf status = "chkstate" Then

                Title = "ดูรายละเอียด"
            ElseIf status = "preview" Then

                Title = "ดูรายละเอียด"

                bApp.Visible = False
                bAppAndSend.Visible = False
                bAppCancel.Visible = False
            End If


        Else


            DS = Session("BookInPreview")
            iRec = ViewState("iRec")

        End If

        MultiViewMaster.ActiveViewIndex = 0
        bAppAndSend.OnClientClick = "return confirm('ยืนยันการส่งข้อมูล');"
        lblPrint.Text = "<a href=""javascript:openwindow('" + "BookInPopupPreview" + "','" + X + "','" + "');"">" + "ดูรายละเอียด" + "</a>"

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
        txtComment.DataBind()

        If DS.Tables(0).Rows(0).Item("send_type").ToString = "0" Or DS.Tables(0).Rows(0).Item("send_type").ToString = "1" Or DS.Tables(0).Rows(0).Item("send_type").ToString = "2" Then
            rdbSend.SelectedValue = DS.Tables(0).Rows(0).Item("send_type").ToString
        End If

        If rdbSend.SelectedValue = 2 Then
            ddlSendName.Enabled = False

        Else
            ddlSendName.Enabled = True
        End If

        If DS.Tables(0).Rows(0).Item("send_type").ToString = "2" Then
            ChkBack.Checked = True

        Else
            ChkBack.Checked = False
        End If
    End Sub
    Private Sub FindRow()
        'BindData Dropdownlist
        Dim X As String = Request.QueryString("id")
        Dim oDs As DataSet
        Dim strsql As String = ""
        Dim chk As String

        chk = "select status_id from bookin_data where bookin_id='" & X & "'"

        oDs = MD.GetDataset(chk)

        If oDs.Tables(0).Rows.Count > 0 Then

            Select oDs.Tables(0).Rows(0).Item("status_id").ToString
                Case 3

                    Dim X1 As String
                    X1 = DS.Tables(0).Rows(iRec)("sendto1") & ""
                    ddlSendName.SelectedIndex = FindBookSendRow(X1)
                Case 2

                    Dim X1 As String
                    X1 = DS.Tables(0).Rows(iRec)("sendto2") & ""
                    ddlSendName.SelectedIndex = FindBookSendRow(X1)
            End Select
        End If
    End Sub
    Public Function FindBookSendRow(ByVal X As String) As Integer
        Me.DataSendTo()
        Dim i As Integer = 0
        For i = 0 To ddlSendName.Items.Count - 1
            If X = ddlSendName.Items(i).Value Then
                Return i
            End If
        Next
        Return 0
    End Function
    Public Sub DataSendTo()
        Dim X As String = Request.QueryString("id")
        Dim sEmpNo As String = Session("EmpNo")
        Dim oDs As DataSet
        Dim strsql As String = ""
        Dim chk As String

        chk = "select status_id from bookin_data where bookin_id='" & X & "'"

        oDs = MD.GetDataset(chk)

        If oDs.Tables(0).Rows.Count > 0 Then

            Select Case oDs.Tables(0).Rows(0).Item("status_id").ToString
                Case 3
                    rdbSend.Visible = True
                    ChkBack.Visible = False
                    Dim chkdiv As String
                    If rdbSend.SelectedValue = "0" Then
                        chkdiv = "select pos_id from division "
                        chkdiv &= "where div_id in (select div_id from bookin_send where bookin_no='" & DS.Tables(0).Rows(0).Item("system_no").ToString & "')"
                        'strsql = "select empid,firstname+' '+lastname nameapp1   "
                        'strsql &= "from employee where dept_id=1 and pos_id in (" & chkdiv & ") "
                        ''strsql &= "or empid='" & DS.Tables(0).Rows(0).Item("creation_by").ToString & "'  "
                        'strsql &= "order by firstname+' '+lastname  "

                        strsql = "select d.div_name,p.pos_name nameapp1,e.empid "
                        strsql &= "from DIVISION d inner join POSITION p "
                        strsql &= "on d.pos_id =p.pos_id inner join EMPLOYEE e "
                        strsql &= "on d.empid =e.empid "
                        strsql &= "where d.pos_id in (" & chkdiv & ")"
                    Else
                        strsql = "select f.empid,f.name nameapp1 from fullname f inner join employee e   "
                        strsql &= "on f.empid=e.empid where e.dept_id=1 "
                    End If

                Case 2

                    rdbSend.Visible = False
                    ChkBack.Visible = True

                    Dim chkdiv As String
                    If rdbSend.SelectedValue = "0" Then
                        chkdiv = "select e.div_id from bookin_data b inner join employee e "
                        chkdiv &= "on b.sendto1=e.empid "
                        chkdiv &= "where b.bookin_id='" & X & "'"

                        strsql = "select empid,firstname+' '+lastname nameapp1   "
                        strsql &= "from employee where dept_id=1 and div_id in (" & chkdiv & ")  "
                        'strsql &= "or empid='" & DS.Tables(0).Rows(0).Item("creation_by").ToString & "'  "
                        strsql &= "order by firstname+' '+lastname  "
                    Else
                        strsql = "select f.empid,f.name nameapp1 from fullname f inner join employee e   "
                        strsql &= "on f.empid=e.empid where e.dept_id=1 "
                    End If

                Case 1
                    rdbSend.Visible = True
                    ChkBack.Visible = False
                    If rdbSend.SelectedValue = "0" Then
                        strsql = "select empid,firstname+' '+lastname nameapp1   "
                        strsql &= "from employee where dept_id=1 and pos_id = 4  "
                        strsql &= "order by firstname+' '+lastname "
                    Else
                        strsql = "select f.empid,f.name nameapp1 from fullname f inner join employee e   "
                        strsql &= "on f.empid=e.empid where e.dept_id=1 "
                    End If
            End Select

        End If

        Dim DTS As DataTable = MD.GetDataTable(strsql)
        Dim dr As DataRow = DTS.NewRow
        dr!empid = 0
        dr!nameapp1 = "---------โปรดเลือก---------"
        DTS.Rows.InsertAt(dr, 0)
        ddlSendName.DataTextField = "nameapp1"
        ddlSendName.DataValueField = "empid"
        ddlSendName.DataSource = DTS
        ddlSendName.DataBind()

    End Sub
    Function CheckApprove(ByVal app As String, ByVal status As String) As String
        Dim X As String = Request.QueryString("id")
        Dim sEmpNo As String = Session("EmpNo")
        Dim chk As String
        Dim oDs As DataSet

        chk = "select * from bookin_data "
        chk &= "where bookin_id='" & X & "' and status_id  =" & status & " and active=1 "
        chk &= "and " & app & "='" & sEmpNo & "'"

        oDs = MD.GetDataset(chk)

        If oDs.Tables(0).Rows.Count < 1 Then
            Dim aut As String
            aut = "select * from authorize "
            aut &= "where assign_from='" & DS.Tables(0).Rows(0).Item("" & app & "").ToString & "' "
            aut &= "and menu_id=64 "
            aut &= "and status_id =" & status & " "
            aut &= "and assign_to='" & sEmpNo & "' "
            aut &= "and convert(nvarchar(10),getdate(),120) between convert(nvarchar(10),date_from,120) and convert(nvarchar(10),date_to,120) "
            Return "1"
        Else
            Return "0"
        End If

    End Function
    Protected Sub ChkStatus()
        'บันทึกข้อมูลแต่ยังไม่ส่งข้อมูล
        Dim X As String = Request.QueryString("id")
        Dim sEmpNo As String = Session("EmpNo")
        Dim oDs As DataSet
        Dim strsql As String = ""
        Dim chk As String

        chk = "select status_id from bookin_data where bookin_id='" & X & "'"

        oDs = MD.GetDataset(chk)

        If oDs.Tables(0).Rows.Count > 0 Then

            Select Case oDs.Tables(0).Rows(0).Item("status_id").ToString
                Case 3 'ผู้อำนวยการจ่ายงาน
                    ChkBack.Visible = False
                Case 2 'หัวหน้าจ่ายงาน
                    ChkBack.Visible = True
            End Select

        End If

    End Sub
    Protected Sub bApp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bApp.Click
        'บันทึกข้อมูลแต่ยังไม่ส่งข้อมูล
        Dim X As String = Request.QueryString("id")
        Dim sEmpNo As String = Session("EmpNo")
        Dim oDs As DataSet
        Dim strsql As String = ""
        Dim chk As String
        Dim sendname As String
        Dim sendtype As String
  
        chk = "select status_id from bookin_data where bookin_id='" & X & "'"

        oDs = MD.GetDataset(chk)

        If oDs.Tables(0).Rows.Count > 0 Then

            Select Case oDs.Tables(0).Rows(0).Item("status_id").ToString
                Case 3 'ผู้อำนวยการจ่ายงาน

                    If rdbSend.SelectedValue <> 2 Then
                        If ddlSendName.SelectedValue = "0" Then
                            lblAApp.Text = "กรุณาเลือกชื่อเพื่อส่งต่อ"
                            lblAComment.Text = ""
                            Exit Sub
                        End If
                        sendname = ddlSendName.SelectedValue
                    Else
                        sendname = DS.Tables(0).Rows(0).Item("creation_by").ToString.ToUpper
                    End If

                    Dim app As String = Me.CheckApprove("sendto", "3")
                    If app = "1" Then
                        strsql = "update bookin_data set sendto_comment1='" & txtComment.Text & "',sendto1='" & sendname & "',sendto_date1=getdate(),sendto_ass1='" & sEmpNo & "',send_type='" & rdbSend.SelectedValue & "' "
                        strsql &= "where bookin_id='" & X & "'"
                    Else
                        strsql = "update bookin_data set sendto_comment1='" & txtComment.Text & "',sendto1='" & sendname & "',sendto_date1=getdate(),send_type='" & rdbSend.SelectedValue & "' "
                        strsql &= "where bookin_id='" & X & "'"
                    End If
                Case 2 'หัวหน้าจ่ายงาน

                    If ChkBack.Checked = False Then
                        If ddlSendName.SelectedValue = "0" Then
                            lblAApp.Text = "กรุณาเลือกชื่อเพื่อส่งต่อ"
                            lblAComment.Text = ""
                            Exit Sub
                        End If
                        sendname = ddlSendName.SelectedValue
                        sendtype = 1
                    Else
                        sendname = ""
                        sendtype = 2
                    End If

                    Dim app As String = Me.CheckApprove("sendto1", "2")
                    If app = "1" Then
                        strsql = "update bookin_data set sendto_comment2='" & txtComment.Text & "',sendto2='" & sendname & "',sendto_date2=getdate(),sendto_ass2='" & sEmpNo & "',send_type='" & sendtype & "' "
                        strsql &= "where bookin_id='" & X & "'"
                    Else
                        strsql = "update bookin_data set sendto_comment2='" & txtComment.Text & "',sendto2='" & sendname & "',sendto_date2=getdate(),send_type='" & sendtype & "' "
                        strsql &= "where bookin_id='" & X & "'"
                    End If
            End Select

        End If


        Dim Y As Integer = MD.Execute(strsql)

        If Y > 0 Then
            MC.MessageBox(Me, "บันทึกลงฐานข้อมูลเรียบร้อยแล้ว")
            lblAComment.Text = ""
            lblAApp.Text = ""
        Else
            MC.MessageBox(Me, "ไม่สามารถบันทึกลงฐานข้อมูลได้")
        End If

    End Sub
    Protected Sub bAppAndSend_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bAppAndSend.Click
        'บันทึกข้อมูลพร้อมส่ง
        If rdbSend.SelectedValue <> 2 Then
            If ddlSendName.SelectedValue = "0" Then
                lblAApp.Text = "กรุณาเลือกชื่อเพื่อส่งต่อ"
                lblAComment.Text = ""
                Exit Sub
            End If
        End If

        If ChkBack.Checked = False Then
            If ddlSendName.SelectedValue = "0" Then
                lblAApp.Text = "กรุณาเลือกชื่อเพื่อส่งต่อ"
                lblAComment.Text = ""
                Exit Sub
            End If
        End If

        If txtComment.Text = "" Then
            lblAComment.Text = "กรุณากรอกบันทึก"
            lblAApp.Text = ""
            Exit Sub
        End If

        Me.UpdateActive()
        Me.Auto()

        Me.SaveData()

    End Sub
    Private Sub SaveData()
        Dim X As String = Request.QueryString("id")
        Dim sEmpNo As String = Session("EmpNo")
        Dim strsql As String = ""
        Dim oDs As DataSet
        Dim chk As String

        chk = "select status_id from bookin_data where bookin_id='" & X & "'"

        oDs = MD.GetDataset(chk)

        If oDs.Tables(0).Rows.Count > 0 Then

            Select Case oDs.Tables(0).Rows(0).Item("status_id").ToString
                Case 3 'ผู้อำนวยการจ่ายงาน
                    If rdbSend.SelectedValue = "0" Then 'ส่งให้หัวหน้าจ่ายงานต่อ
                        Dim app As String = Me.CheckApprove("sendto", "3")
                        If app = "1" Then
                        
                            strsql = "insert into bookin_data (bookin_id,system_no,bookin_no,from_name,bookkind_id,status_id,priority_id, "
                            strsql &= "keyword,topic,recieve_date,stamp_date,present,sendto,sendto_comment,sendto1,sendto_comment1,sendto_date1,  "
                            strsql &= "sendto2,sendto_comment2,sendto_ass1,"
                            strsql &= "creation_by,created_date,updated_by,updated_date,send_type,runno)"

                            strsql &= "select '" & lblIdNew.Text & "',system_no,bookin_no,from_name,bookkind_id,2,priority_id, "
                            strsql &= "keyword,topic,recieve_date,stamp_date,present,sendto,sendto_comment,'" & ddlSendName.SelectedValue & "','" & txtComment.Text & "',getdate(),  "
                            strsql &= "sendto2,sendto_comment2,'" & sEmpNo & "',"
                            strsql &= "creation_by,created_date,'" & sEmpNo & "',getdate()," & rdbSend.SelectedValue & ",runno  "
                            strsql &= "from bookin_data where bookin_id='" & X & "'"

                        Else

                            strsql = "insert into bookin_data (bookin_id,system_no,bookin_no,from_name,bookkind_id,status_id,priority_id, "
                            strsql &= "keyword,topic,recieve_date,stamp_date,present,sendto,sendto_comment,sendto1,sendto_comment1,sendto_date1,  "
                            strsql &= "sendto2,sendto_comment2,"
                            strsql &= "creation_by,created_date,updated_by,updated_date,send_type,runno )"

                            strsql &= "select '" & lblIdNew.Text & "',system_no,bookin_no,from_name,bookkind_id,2,priority_id, "
                            strsql &= "keyword,topic,recieve_date,stamp_date,present,sendto,sendto_comment,'" & ddlSendName.SelectedValue & "','" & txtComment.Text & "',getdate(),  "
                            strsql &= "sendto2,sendto_comment2,"
                            strsql &= "creation_by,created_date,'" & sEmpNo & "',getdate()," & rdbSend.SelectedValue & ",runno "
                            strsql &= "from bookin_data where bookin_id='" & X & "'"


                        End If
                    ElseIf rdbSend.SelectedValue = "1" Then 'ส่งตรงนิติกรเจ้าของเรื่อง 
                        Dim app As String = Me.CheckApprove("sendto", "3")
                        If app = "1" Then

                            strsql = "insert into bookin_data (bookin_id,system_no,bookin_no,from_name,bookkind_id,status_id,priority_id, "
                            strsql &= "keyword,topic,recieve_date,stamp_date,present,sendto,sendto_comment,sendto1,sendto_comment1,sendto_date1,  "
                            strsql &= "sendto2,sendto_comment2,sendto_ass1,"
                            strsql &= "creation_by,created_date,updated_by,updated_date,send_type,runno )"

                            strsql &= "select '" & lblIdNew.Text & "',system_no,bookin_no,from_name,bookkind_id,4,priority_id, "
                            strsql &= "keyword,topic,recieve_date,stamp_date,present,sendto,sendto_comment,null,'" & txtComment.Text & "',getdate(),  "
                            strsql &= "'" & ddlSendName.SelectedValue & "',sendto_comment2,'" & sEmpNo & "',"
                            strsql &= "creation_by,created_date,'" & sEmpNo & "',getdate()," & rdbSend.SelectedValue & ",runno "
                            strsql &= "from bookin_data where bookin_id='" & X & "'"

                        Else
                            strsql = "insert into bookin_data (bookin_id,system_no,bookin_no,from_name,bookkind_id,status_id,priority_id, "
                            strsql &= "keyword,topic,recieve_date,stamp_date,present,sendto,sendto_comment,sendto1,sendto_comment1,sendto_date1,  "
                            strsql &= "sendto2,sendto_comment2,"
                            strsql &= "creation_by,created_date,updated_by,updated_date,send_type,runno )"

                            strsql &= "select '" & lblIdNew.Text & "',system_no,bookin_no,from_name,bookkind_id,4,priority_id, "
                            strsql &= "keyword,topic,recieve_date,stamp_date,present,sendto,sendto_comment,null,'" & txtComment.Text & "',getdate(),  "
                            strsql &= "'" & ddlSendName.SelectedValue & "',sendto_comment2,"
                            strsql &= "creation_by,created_date,'" & sEmpNo & "',getdate()," & rdbSend.SelectedValue & ",runno "
                            strsql &= "from bookin_data where bookin_id='" & X & "'"

                        End If

                    ElseIf rdbSend.SelectedValue = "2" Then 'ส่งกลับธุรการ
                        Dim app As String = Me.CheckApprove("sendto", "3")
                        If app = "1" Then
                            strsql = "insert into bookin_data (bookin_id,system_no,bookin_no,from_name,bookkind_id,status_id,priority_id, "
                            strsql &= "keyword,topic,recieve_date,stamp_date,present,sendto,sendto_comment,sendto1,sendto_comment1,  "
                            strsql &= "sendto2,sendto_comment2,sendto_date1,sendto_date2,sendto_ass1,sendto_ass2,"
                            strsql &= "creation_by,created_date,updated_by,updated_date,send_type,runno )"

                            strsql &= "select '" & lblIdNew.Text & "',system_no,bookin_no,from_name,bookkind_id,1,priority_id, "
                            strsql &= "keyword,topic,recieve_date,stamp_date,present,sendto,sendto_comment,sendto1,sendto_comment1,  "
                            strsql &= "null,'" & txtComment.Text & "',sendto_date1,getdate(),sendto_ass1,'" & sEmpNo & "',"
                            strsql &= "creation_by,created_date,'" & sEmpNo & "',getdate()," & rdbSend.SelectedValue & ",runno "
                            strsql &= "from bookin_data where bookin_id='" & X & "'"

                        Else
                            strsql = "insert into bookin_data (bookin_id,system_no,bookin_no,from_name,bookkind_id,status_id,priority_id, "
                            strsql &= "keyword,topic,recieve_date,stamp_date,present,sendto,sendto_comment,sendto1,sendto_comment1,  "
                            strsql &= "sendto2,sendto_comment2,sendto_date1,sendto_date2,sendto_ass1,sendto_ass2,"
                            strsql &= "creation_by,created_date,updated_by,updated_date,send_type,runno )"

                            strsql &= "select '" & lblIdNew.Text & "',system_no,bookin_no,from_name,bookkind_id,1,priority_id, "
                            strsql &= "keyword,topic,recieve_date,stamp_date,present,sendto,sendto_comment,sendto1,sendto_comment1,  "
                            strsql &= "null,'" & txtComment.Text & "',sendto_date1,getdate(),sendto_ass1,sendto_ass2,"
                            strsql &= "creation_by,created_date,'" & sEmpNo & "',getdate()," & rdbSend.SelectedValue & ",runno "
                            strsql &= "from bookin_data where bookin_id='" & X & "'"

                        End If

                    End If


                Case 2 'หัวหน้าจ่ายงาน
                    Dim app As String = Me.CheckApprove("sendto1", "2")
                    If ChkBack.Checked = True Then 'ส่งกลับให้ธุรการ

                        If app = "1" Then
                            strsql = "insert into bookin_data (bookin_id,system_no,bookin_no,from_name,bookkind_id,status_id,priority_id, "
                            strsql &= "keyword,topic,recieve_date,stamp_date,present,sendto,sendto_comment,sendto1,sendto_comment1,  "
                            strsql &= "sendto2,sendto_comment2,sendto_date1,sendto_date2,sendto_ass1,sendto_ass2,send_type,runno,"
                            strsql &= "creation_by,created_date,updated_by,updated_date )"

                            strsql &= "select '" & lblIdNew.Text & "',system_no,bookin_no,from_name,bookkind_id,1,priority_id, "
                            strsql &= "keyword,topic,recieve_date,stamp_date,present,sendto,sendto_comment,sendto1,sendto_comment1,  "
                            strsql &= "'" & ddlSendName.SelectedValue & "','" & txtComment.Text & "',sendto_date1,getdate(),sendto_ass1,'" & sEmpNo & "',2,runno,"
                            strsql &= "creation_by,created_date,'" & sEmpNo & "',getdate() "
                            strsql &= "from bookin_data where bookin_id='" & X & "'"

                        Else
                            strsql = "insert into bookin_data (bookin_id,system_no,bookin_no,from_name,bookkind_id,status_id,priority_id, "
                            strsql &= "keyword,topic,recieve_date,stamp_date,present,sendto,sendto_comment,sendto1,sendto_comment1,  "
                            strsql &= "sendto2,sendto_comment2,sendto_date1,sendto_date2,sendto_ass1,sendto_ass2,send_type,runno,"
                            strsql &= "creation_by,created_date,updated_by,updated_date )"

                            strsql &= "select '" & lblIdNew.Text & "',system_no,bookin_no,from_name,bookkind_id,1,priority_id, "
                            strsql &= "keyword,topic,recieve_date,stamp_date,present,sendto,sendto_comment,sendto1,sendto_comment1,  "
                            strsql &= "'" & ddlSendName.SelectedValue & "','" & txtComment.Text & "',sendto_date1,getdate(),sendto_ass1,sendto_ass2,2,runno,"
                            strsql &= "creation_by,created_date,'" & sEmpNo & "',getdate() "
                            strsql &= "from bookin_data where bookin_id='" & X & "'"

                        End If
                    Else 'ส่งให้นิติกร

                        If app = "1" Then
                            strsql = "insert into bookin_data (bookin_id,system_no,bookin_no,from_name,bookkind_id,status_id,priority_id, "
                            strsql &= "keyword,topic,recieve_date,stamp_date,present,sendto,sendto_comment,sendto1,sendto_comment1,  "
                            strsql &= "sendto2,sendto_comment2,sendto_date1,sendto_date2,sendto_ass1,sendto_ass2,send_type,runno,"
                            strsql &= "creation_by,created_date,updated_by,updated_date )"

                            strsql &= "select '" & lblIdNew.Text & "',system_no,bookin_no,from_name,bookkind_id,4,priority_id, "
                            strsql &= "keyword,topic,recieve_date,stamp_date,present,sendto,sendto_comment,sendto1,sendto_comment1,  "
                            strsql &= "'" & ddlSendName.SelectedValue & "','" & txtComment.Text & "',sendto_date1,getdate(),sendto_ass1,'" & sEmpNo & "',1,runno,"
                            strsql &= "creation_by,created_date,'" & sEmpNo & "',getdate() "
                            strsql &= "from bookin_data where bookin_id='" & X & "'"

                        Else
                            strsql = "insert into bookin_data (bookin_id,system_no,bookin_no,from_name,bookkind_id,status_id,priority_id, "
                            strsql &= "keyword,topic,recieve_date,stamp_date,present,sendto,sendto_comment,sendto1,sendto_comment1,  "
                            strsql &= "sendto2,sendto_comment2,sendto_date1,sendto_date2,sendto_ass1,sendto_ass2,send_type,runno,"
                            strsql &= "creation_by,created_date,updated_by,updated_date )"

                            strsql &= "select '" & lblIdNew.Text & "',system_no,bookin_no,from_name,bookkind_id,4,priority_id, "
                            strsql &= "keyword,topic,recieve_date,stamp_date,present,sendto,sendto_comment,sendto1,sendto_comment1,  "
                            strsql &= "'" & ddlSendName.SelectedValue & "','" & txtComment.Text & "',sendto_date1,getdate(),sendto_ass1,sendto_ass2,1,runno,"
                            strsql &= "creation_by,created_date,'" & sEmpNo & "',getdate() "
                            strsql &= "from bookin_data where bookin_id='" & X & "'"

                        End If
                    End If


            End Select

        End If

        Dim Y As Integer = MD.Execute(strsql)
        If Y > 0 Then
            MC.MessageBox(Me, "บันทึกลงฐานข้อมูลเรียบร้อยแล้ว")

            lblAApp.Text = ""
            lblAComment.Text = ""
            Response.Redirect("../Src/BookInWaitList.aspx", True)
        Else
            MC.MessageBox(Me, "ไม่สามารถบันทึกลงฐานข้อมูลได้")
        End If

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
        'ก่อน Update Status ให้ Set Active = 0
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
    Protected Sub bAppCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bAppCancel.Click
        txtComment.Text = ""
        lblAApp.Text = ""
        lblAComment.Text = ""
    End Sub
    Protected Sub linkHome_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles linkHome.Click
        Response.Redirect(MD.pHome, True)
    End Sub
    Protected Sub link2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles link2.Click
        Response.Redirect("../Src/BookInWaitList.aspx", True)
    End Sub
    Protected Sub rdbSend_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdbSend.SelectedIndexChanged
        If rdbSend.SelectedValue = 2 Then
            Me.DataSendTo()
            ddlSendName.Enabled = False
        Else
            Me.DataSendTo()
            ddlSendName.Enabled = True
        End If

    End Sub
    Protected Sub ChkBack_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkBack.CheckedChanged
        If ChkBack.Checked = True Then
            Me.DataSendTo()
            ddlSendName.Enabled = False
        Else
            Me.DataSendTo()
            ddlSendName.Enabled = True
        End If
    End Sub
   
End Class
