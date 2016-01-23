Imports System.IO
Imports System.Data
Imports System.Data.OleDb
Partial Class Src_BookInData
    Inherits System.Web.UI.Page
    Dim cn As OleDbConnection
    Dim cmd As OleDbCommand
    Dim MD As New MainData
    Dim MC As New MainClass
    Dim DV As DataView
    Dim DVLst As DataView
    Dim DVDIV As DataView
    Public iRow As Integer
    Dim DS As DataSet
    Dim iRec As Integer
    Private Sub ChkPermis()
        'กำหนดสิทธิ์การใช้งาน
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

        strsql = " select div_id,div_name  "
        strsql &= "from division where dept_id=1 and div_id <> 7   "


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
    Public Sub cb1_Checked(ByVal sender As Object, ByVal e As EventArgs)
        'CheckBox หน้า Gridveiw ประเภทเอกสาร
        Dim cb1 As CheckBox = sender
        Dim dgi As GridViewRow
        Dim cb2 As CheckBox
        For Each dgi In gdvDIV.Rows
            cb2 = dgi.Cells(1).FindControl("cb1")
            cb2.Checked = cb1.Checked
        Next
    End Sub
    Private Sub Name()
        Dim sEmpNo As String = Session("EmpNo")
        Dim strsql As String
        Dim oDs As New DataSet

        strsql = "select firstname+' '+lastname as fullname from employee where empid='" & sEmpNo & "'"

        oDs = MD.GetDataset(strsql)
        If oDs.Tables(0).Rows.Count > 0 Then
            txtCreate.Text = oDs.Tables(0).Rows(0).Item("fullname").ToString
        End If
    End Sub
    Private Sub BookNo()
        Dim strsql As String
        Dim oDs As New DataSet

        strsql = "select book_no from book_type where booktype_id=1 "

        oDs = MD.GetDataset(strsql)
        If oDs.Tables(0).Rows.Count > 0 Then
            txtBookNo.Text = oDs.Tables(0).Rows(0).Item("book_no").ToString
        End If
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim X As String = Request.QueryString("id")
        Dim status As String = Request.QueryString("status")

        Me.ChkPermis()
        Me.Name()

        If Not Page.IsPostBack Then
            txtDate.Text = Date.Today
            txtInDate.Text = Date.Today

            If X <> "" Then
                'Preview, Approve, Edit

                Dim sql As String

                sql = " select b.bookin_id,b.system_no,b.bookin_no,b.bookkind_id,k.bookkind_name,  "
                sql &= "b.status_id,s.status_name,b.topic,b.keyword,b.recieve_date,b.stamp_date,b.from_name, "
                sql &= "b.sendto,b.present,b.priority_id,b.div_id,right(b.runno,4) runno,b.runno runbook "
                sql &= "from bookin_data b inner join book_kind k "
                sql &= "on b.bookkind_id=k.bookkind_id inner join book_status s "
                sql &= "on b.status_id=s.status_id "
                sql &= "where b.bookin_id='" & X & "' "
                sql &= "and b.active=1 "

                DS = MD.GetDataset(sql)
                Session("bookin_data") = DS
                iRec = 0
                ViewState("iRec") = iRec

                Me.DataBookPriority()
                Me.DataBookType()
                Me.DataBookStatus()
                Me.DataSendTo()

                lblId.Text = X
                lblMainStatus.Text = "Edit"

                Me.MyDataBind()
                Me.FindRow()

                Me.gDataDIV()
                Me.MyGridBindDIV()
            Else
                'Add New
                'Me.BookNo()
                Dim sql As String

                sql = " select * from bookin_data "


                DS = MD.GetDataset(sql)
                Session("bookin_data") = DS
                iRec = 0
                ViewState("iRec") = iRec

                Me.DataBookPriority()
                Me.DataBookType()
                Me.DataBookStatus()
                Me.DataSendTo()

                Me.gDataDIV()
                Me.MyGridBindDIV()

                lblId.Text = ""
                lblMainStatus.Text = "Add"

            End If

            ''Check status Enable and Visible Control 

            If status = "cancel" Then

            ElseIf status = "wait" Then

                Title = "ดูรายละเอียด"

            ElseIf status = "chkstate" Then

                Title = "ดูรายละเอียด"
            ElseIf status = "preview" Then

                Title = "ดูรายละเอียด"

            ElseIf status = "edit" Then

                Title = "แก้ไข"
                MultiViewDoc.ActiveViewIndex = 0
                link2.Text = "แก้ไขหนังสือรับ"

            End If

            Me.gDataDoc()
            Me.MyGridBind()

        Else

            DS = Session("bookin_data")
            iRec = ViewState("iRec")

            If Session("DocumentBookIn") Is Nothing Then
                Me.gDataDoc()
            Else
                DVLst = Session("DocumentBookIn")
            End If


            If Session("datadiv") Is Nothing Then
                Me.gDataDIV()
            Else
                DVDIV = Session("datadiv")
            End If


        End If

        MultiViewMaster.ActiveViewIndex = 0
        MultiViewDoc.ActiveViewIndex = 0

        txtDate.Attributes.Add("onkeypress", "return blockText(this, event,-1,0);")
        txtInDate.Attributes.Add("onkeypress", "return blockText(this, event,-1,0);")
        txtDocPage.Attributes.Add("onkeypress", "return blockNonNumbers(this, event,-1,0);")
        bSaveSend.OnClientClick = "return confirm('ยืนยันการส่งข้อมูล');"

    End Sub
    Private Sub gDataDoc(Optional ByVal Type As String = "")
        'Data in Gridview (Table BOOKIN_DOCUMENT)
        Dim X As String = Request.QueryString("id")
        Dim strsql As String

        strsql = " select d.document_id,d.title,d.page  "
        strsql &= "from bookin_document d "
        strsql &= "where d.system_no='" & lblBookNo.Text & "'"

        Dim DT As DataTable = MD.GetDataTable(strsql)
        DVLst = DT.DefaultView
        Session("DocumentBookIn") = DVLst

    End Sub
    Private Sub MyGridBind()
        GridView1.DataSource = DVLst
        Dim X1() As String = {"document_id"}
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
                    Return P1.ToString("dd/MM/yyyy")
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
        txtBookNo.DataBind()
        txtDate.DataBind()
        txtInDate.DataBind()
        txtKeyword.DataBind()
        txtCreate.DataBind()
        txtFrom.DataBind()
        txtTo.DataBind()
        txtTopic.DataBind()

        Dim DT As DataTable = DS.Tables(0)
        Dim P1 As Date = DT.Rows(iRec)("recieve_date")
        ddlTimeHr.SelectedValue = P1.ToString("HH")
        ddlTimeMin.SelectedValue = P1.ToString("mm")
        lblRunNoShow.DataBind()
        lblRunNo.DataBind()

    End Sub
    Private Sub FindRow()
        'BindData Dropdownlist
        Dim X1 As String
        X1 = DS.Tables(0).Rows(iRec)("status_id") & ""
        ddlBookStatus.SelectedIndex = FindStatusRow(X1)

        Dim X2 As String
        X2 = DS.Tables(0).Rows(iRec)("bookkind_id") & ""
        ddlBookType.SelectedIndex = FindBookKindRow(X2)

        Dim X3 As String
        X3 = DS.Tables(0).Rows(iRec)("sendto") & ""
        ddlToName.SelectedIndex = FindToNameRow(X3)

        Dim X4 As String
        X4 = DS.Tables(0).Rows(iRec)("priority_id") & ""
        ddlPriority.SelectedIndex = FindPriorityRow(X4)

    End Sub
    Public Function FindStatusRow(ByVal X As String) As Integer
        Dim i As Integer = 0
        For i = 0 To ddlBookStatus.Items.Count - 1
            If X = ddlBookStatus.Items(i).Value Then
                Return i
            End If
        Next
        Return 0
    End Function
    Public Function FindBookKindRow(ByVal X As String) As Integer
        Dim i As Integer = 0
        For i = 0 To ddlBookType.Items.Count - 1
            If X = ddlBookType.Items(i).Value Then
                Return i
            End If
        Next
        Return 0
    End Function
    Public Function FindToNameRow(ByVal X As String) As Integer
        Dim i As Integer = 0
        For i = 0 To ddlToName.Items.Count - 1
            If X = ddlToName.Items(i).Value Then
                Return i
            End If
        Next
        Return 0
    End Function
    Public Function FindPriorityRow(ByVal X As String) As Integer
        Dim i As Integer = 0
        For i = 0 To ddlPriority.Items.Count - 1
            If X = ddlPriority.Items(i).Value Then
                Return i
            End If
        Next
        Return 0
    End Function
    Public Sub DataBookPriority()
        'ระดับความเร่งด่วน
        Dim strsql As String
        strsql = "select priority_id,priority_name from book_priority order by priority_id  "

        Dim DTS As DataTable = MD.GetDataTable(strsql)
        ddlPriority.DataTextField = "priority_name"
        ddlPriority.DataValueField = "priority_id"
        ddlPriority.DataSource = DTS
        ddlPriority.DataBind()

    End Sub
    Public Sub DataBookType()
        'ประเภทหนังสือ
        Dim strsql As String
        strsql = "select bookkind_id,bookkind_name from book_kind where booktype_id=1 order by bookkind_id  "

        Dim DTS As DataTable = MD.GetDataTable(strsql)
        ddlBookType.DataTextField = "bookkind_name"
        ddlBookType.DataValueField = "bookkind_id"
        ddlBookType.DataSource = DTS
        ddlBookType.DataBind()

    End Sub
    Public Sub DataBookStatus()
        'สถานะหนังสือ
        Dim strsql As String
        strsql = "select status_id,status_name from book_status where booktype_id=1 order by status_id  "

        Dim DTS As DataTable = MD.GetDataTable(strsql)
        ddlBookStatus.DataTextField = "status_name"
        ddlBookStatus.DataValueField = "status_id"
        ddlBookStatus.DataSource = DTS
        ddlBookStatus.DataBind()

    End Sub
    Public Sub DataSendTo()
        'รายชื่อจ้าหน้าที่
        Dim strsql As String
        strsql = "select empid,firstname+' '+lastname name_to   "
        strsql &= "from employee where dept_id=1 and pos_id in (2,3,4,5,6)  "
        strsql &= "order by firstname+' '+lastname  "

        Dim DTS As DataTable = MD.GetDataTable(strsql)
        Dim dr As DataRow = DTS.NewRow
        dr!empid = 0
        dr!name_to = "---------โปรดเลือก---------"
        DTS.Rows.InsertAt(dr, 0)
        ddlToName.DataTextField = "name_to"
        ddlToName.DataValueField = "empid"
        ddlToName.DataSource = DTS
        ddlToName.DataBind()

    End Sub
    Private Sub ClearAlert()
        lblChkBookNo.Text = ""
        lblChkDate1.Text = ""
        lblChkDate2.Text = ""
        lblChkTime.Text = ""
        lblChkFrom.Text = ""
        lblChkKeyword.Text = ""
        lblChkTopic.Text = ""
        lblChkTo.Text = ""
        lblChkDiv.Text = ""
        lblChkSendTo.Text = ""
    End Sub
    Protected Sub bSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bSave.Click
        Dim X As String = Request.QueryString("id")
        Dim sEmpNo As String = Session("EmpNo")

        If txtBookNo.Text = "" Then
            Me.ClearAlert()
            lblChkBookNo.Text = "กรุณากรอกเลขที่หนังสือ"
            txtBookNo.Focus()
            Exit Sub
        End If

        If txtDate.Text.Year = 1 Then
            Me.ClearAlert()
            lblChkDate1.Text = "กรุณาเลือกวันที่รับเรื่อง"
            Exit Sub
        End If

        If ddlTimeHr.SelectedIndex = 0 Then
            Me.ClearAlert()
            lblChkTime.Text = "กรุณาเลือกเวลาที่รับเรื่อง"
            Exit Sub
        End If

        If ddlTimeMin.SelectedIndex = 0 Then
            Me.ClearAlert()
            lblChkTime.Text = "กรุณาเลือกเวลาที่รับเรื่อง"
            Exit Sub
        End If

        If txtInDate.Text.Year = 1 Then
            Me.ClearAlert()
            lblChkDate2.Text = "กรุณาเลือกวันที่"
            Exit Sub
        End If

        If txtFrom.Text = "" Then
            Me.ClearAlert()
            lblChkFrom.Text = "กรุณากรอกที่มา"
            Exit Sub
        End If

        If txtKeyword.Text = "" Then
            Me.ClearAlert()
            lblChkKeyword.Text = "กรุณากรอกคำค้นหา"
            Exit Sub
        End If

        If txtTopic.Text = "" Then
            Me.ClearAlert()
            lblChkTopic.Text = "กรุณากรอกเรื่อง"
            Exit Sub
        End If

        If txtTo.Text = "" Then
            Me.ClearAlert()
            lblChkTo.Text = "กรุณากรอกชื่อผู้ที่จะเสนอ"
            Exit Sub
        End If

        Dim chk As String = ChkDIV()
        If chk = "" Then
            Me.ClearAlert()
            lblChkDiv.Text = "กรุณาเลือกหน่วยงาน"
            Exit Sub
        End If

        If ddlToName.SelectedValue = "0" Then
            Me.ClearAlert()
            lblChkSendTo.Text = "กรุณาเลือกส่งถึง"
            Exit Sub
        End If

       

        If lblMainStatus.Text = "Add" Then

            Me.Auto()
            Me.AutoRunNo()
            Me.GenBookNo()

            Me.SaveData("1")
        Else
            Me.EditData("1")
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
                lblId.Text = sAuto + tmpMemberID.ToString("-0000")

            End With
        Catch
            lblId.Text = sAuto + "-0001"
        End Try
        cn.Close()

    End Sub
    Private Sub AutoRunNo()
        'Genarate RunNo
        Dim sqlTmp As String = ""
        Dim comTmp As OleDbCommand = New OleDbCommand
        Dim drTmp As OleDbDataReader
        Dim tmpMemberID As Integer = 0
        Dim tmpMemberID2 As String = ""


        Dim sYear As String
        sYear = Date.Now.Year

        Dim sAuto As String = sYear

        sqlTmp = "SELECT TOP 1 runno FROM bookin_data "
        sqlTmp &= " WHERE left(runno,4) ='" & sAuto & "'"
        sqlTmp &= " and active =1 "
        sqlTmp &= " ORDER BY runno DESC "

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

                tmpMemberID2 = Right(drTmp.Item("runno"), 4)

                tmpMemberID = CInt(tmpMemberID2) + 1
                lblRunNo.Text = sAuto + tmpMemberID.ToString("-0000")

            End With
        Catch
            lblRunNo.Text = sAuto + "-0001"
        End Try
        cn.Close()

    End Sub
    Private Sub GenBookNo()
        'Genarate Bookin No.
        Dim sqlTmp As String = ""
        Dim comTmp As OleDbCommand = New OleDbCommand
        Dim drTmp As OleDbDataReader
        Dim tmpMemberID As Integer = 0
        Dim tmpMemberID2 As String = ""


        Dim sYear As String
        sYear = Date.Now.Year

        Dim sAuto As String = "BKIN" + Right(sYear, 2)

        sqlTmp = "SELECT TOP 1 right(system_no,3) system_no FROM bookin_data "
        sqlTmp &= " WHERE left(system_no,6) ='" & sAuto & "'"
        sqlTmp &= " ORDER BY system_no DESC"

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

                tmpMemberID2 = Right(drTmp.Item("system_no"), 3)

                tmpMemberID = CInt(tmpMemberID2) + 1
                lblBookNo.Text = sAuto + tmpMemberID.ToString("-000")

            End With
        Catch
            lblBookNo.Text = sAuto + "-001"
        End Try
        cn.Close()

    End Sub
    Private Sub SaveData(ByVal status As String)
        Dim sEmpNo As String = Session("EmpNo")
        Dim strsql As String

        Try

            strsql = "insert into bookin_data (bookin_id,system_no,bookin_no,status_id,from_name,recieve_date,stamp_date, "
            strsql &= "priority_id,keyword,topic,bookkind_id,present,sendto,"
            strsql &= "creation_by,created_date,updated_by,updated_date,runno ) "
            strsql &= " values  "
            strsql &= " (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)"


            cn = New OleDbConnection(MD.Strcon)
            cmd = New OleDbCommand(strsql, cn)
            MD.CreateParam(cmd, "TTTTTDDTTTTTTTDTDT")

            cmd.Parameters("@P1").Value = lblId.Text
            cmd.Parameters("@P2").Value = lblBookNo.Text
            cmd.Parameters("@P3").Value = txtBookNo.Text
            cmd.Parameters("@P4").Value = status
            cmd.Parameters("@P5").Value = txtFrom.Text

            If txtDate.Text.Year = 1 Then
                cmd.Parameters("@P6").Value = DBNull.Value
            Else
                cmd.Parameters("@P6").Value = DateTime.Parse(txtDate.Text + " " + ddlTimeHr.Text + ":" + ddlTimeMin.Text)
            End If

            If txtInDate.Text.Year = 1 Then
                cmd.Parameters("@P7").Value = DBNull.Value
            Else
                cmd.Parameters("@P7").Value = DateTime.Parse(txtInDate.Text)
            End If

            cmd.Parameters("@P8").Value = ddlPriority.SelectedValue
            cmd.Parameters("@P9").Value = txtKeyword.Text
            cmd.Parameters("@P10").Value = txtTopic.Text
            cmd.Parameters("@P11").Value = ddlBookType.SelectedValue
            cmd.Parameters("@P12").Value = txtTo.Text
            cmd.Parameters("@P13").Value = ddlToName.SelectedValue
            cmd.Parameters("@P14").Value = sEmpNo
            cmd.Parameters("@P15").Value = DateTime.Parse(Date.Now)
            cmd.Parameters("@P16").Value = sEmpNo
            cmd.Parameters("@P17").Value = DateTime.Parse(Date.Now)
            cmd.Parameters("@P18").Value = lblRunNo.Text

            cn.Open()
            cmd.ExecuteNonQuery()
            MC.MessageBox(Me, "บันทึกลงฐานข้อมูลเรียบร้อยแล้ว")
            lblMainStatus.Text = "Edit"
            Me.ClearAlert()

            Me.DeleteDIV()
            Me.SaveDIV()

            lblRunNoShow.Text = Right(lblRunNo.Text, 4)

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
    Private Sub EditData(ByVal status As String)
        Dim sEmpNo As String = Session("EmpNo")
        Dim strsql As String

        Try

            strsql = "update bookin_data set bookin_no=?,status_id=?,from_name=?,recieve_date=?,stamp_date=?, "
            strsql &= "priority_id=?,keyword=?,topic=?,bookkind_id=?,present=?,sendto=?,"
            strsql &= "updated_by=?,updated_date=?,runno=? "
            strsql &= "where bookin_id=?  "

            cn = New OleDbConnection(MD.Strcon)
            cmd = New OleDbCommand(strsql, cn)
            MD.CreateParam(cmd, "TTTDDTTTTTTTDTT")


            cmd.Parameters("@P1").Value = txtBookNo.Text
            cmd.Parameters("@P2").Value = status
            cmd.Parameters("@P3").Value = txtFrom.Text

            If txtDate.Text.Year = 1 Then
                cmd.Parameters("@P4").Value = DBNull.Value
            Else
                cmd.Parameters("@P4").Value = DateTime.Parse(txtDate.Text + " " + ddlTimeHr.Text + ":" + ddlTimeMin.Text)
            End If

            If txtInDate.Text.Year = 1 Then
                cmd.Parameters("@P5").Value = DBNull.Value
            Else
                cmd.Parameters("@P5").Value = DateTime.Parse(txtInDate.Text)
            End If

            cmd.Parameters("@P6").Value = ddlPriority.SelectedValue
            cmd.Parameters("@P7").Value = txtKeyword.Text
            cmd.Parameters("@P8").Value = txtTopic.Text
            cmd.Parameters("@P9").Value = ddlBookType.SelectedValue
            cmd.Parameters("@P10").Value = txtTo.Text
            cmd.Parameters("@P11").Value = ddlToName.SelectedValue
            cmd.Parameters("@P12").Value = sEmpNo
            cmd.Parameters("@P13").Value = DateTime.Parse(Date.Now)
            cmd.Parameters("@P14").Value = lblRunNo.Text
            cmd.Parameters("@P15").Value = lblId.Text

            cn.Open()
            cmd.ExecuteNonQuery()
            MC.MessageBox(Me, "บันทึกลงฐานข้อมูลเรียบร้อยแล้ว")
            lblMainStatus.Text = "Edit"
            Me.ClearAlert()

            Me.DeleteDIV()
            Me.SaveDIV()

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
    Protected Sub bSaveSend_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bSaveSend.Click
        Dim X As String = Request.QueryString("id")
        Dim sEmpNo As String = Session("EmpNo")

        If txtBookNo.Text = "" Then
            Me.ClearAlert()
            lblChkBookNo.Text = "กรุณากรอกเลขที่หนังสือ"
            txtBookNo.Focus()
            Exit Sub
        End If

        If txtDate.Text.Year = 1 Then
            Me.ClearAlert()
            lblChkDate1.Text = "กรุณาเลือกวันที่รับเรื่อง"
            Exit Sub
        End If

        If ddlTimeHr.SelectedIndex = 0 Then
            Me.ClearAlert()
            lblChkTime.Text = "กรุณาเลือกเวลาที่รับเรื่อง"
            Exit Sub
        End If

        If ddlTimeMin.SelectedIndex = 0 Then
            Me.ClearAlert()
            lblChkTime.Text = "กรุณาเลือกเวลาที่รับเรื่อง"
            Exit Sub
        End If

        If txtInDate.Text.Year = 1 Then
            Me.ClearAlert()
            lblChkDate2.Text = "กรุณาเลือกวันที่"
            Exit Sub
        End If

        If txtFrom.Text = "" Then
            Me.ClearAlert()
            lblChkFrom.Text = "กรุณากรอกที่มา"
            Exit Sub
        End If

        If txtKeyword.Text = "" Then
            Me.ClearAlert()
            lblChkKeyword.Text = "กรุณากรอกคำค้นหา"
            Exit Sub
        End If

        If txtTopic.Text = "" Then
            Me.ClearAlert()
            lblChkTopic.Text = "กรุณากรอกเรื่อง"
            Exit Sub
        End If

        If txtTo.Text = "" Then
            Me.ClearAlert()
            lblChkTo.Text = "กรุณากรอกชื่อผู้ที่จะเสนอ"
            Exit Sub
        End If

        Dim chk As String = ChkDIV()
        If chk = "" Then
            Me.ClearAlert()
            lblChkDiv.Text = "กรุณาเลือกหน่วยงาน"
            Exit Sub
        End If

        If ddlToName.SelectedValue = "0" Then
            Me.ClearAlert()
            lblChkSendTo.Text = "กรุณาเลือกส่งถึง"
            Exit Sub
        End If


        Me.UpdateActive()
        Me.Auto()

        If lblBookNo.Text = "" Then
            Me.GenBookNo()
        End If

        Me.SaveData("3")
        Me.ClearData()

        Response.Redirect("../Src/BookInDataList.aspx", True)

    End Sub
    Private Sub UpdateActive()

        Dim sEmpNo As String = Session("EmpNo")

        Try
            Dim Strsql As String
            Strsql = "update bookin_data set  "
            Strsql &= " active = 0 "
            Strsql &= " where bookin_id='" & lblId.Text & "'"

            Dim Y As Integer = MD.Execute(Strsql)

        Catch ex As Exception
            Dim ses As String = System.Web.HttpContext.Current.Session.SessionID
            Dim url As String = HttpContext.Current.Request.FilePath
            Dim browser As String = " Type:" + Request.Browser.Type + " Version:" + Request.Browser.Version + " Browser:" + Request.Browser.Browser

            MC.MessageBox(Me, ex.ToString)
            MC.ErrLog(url, ses, ex.ToString, sEmpNo, browser)
        End Try

    End Sub
    Public Sub ClearData()

        txtBookNo.Text = ""
        txtDate.Text = "0:00:00"
        txtInDate.Text = "0:00:00"
        txtKeyword.Text = ""
        txtCreate.Text = ""
        txtFrom.Text = ""
        txtTo.Text = ""
        txtTopic.Text = ""

        lblId.Text = ""
        lblMainStatus.Text = "Add"
        lblBookNo.Text = ""

    End Sub
    Private Sub AutoFile()
        'Genarate Document Id
        Dim sqlTmp As String = ""
        Dim comTmp As OleDbCommand = New OleDbCommand
        Dim drTmp As OleDbDataReader
        Dim tmpMemberID As Integer = 0
        Dim tmpMemberID2 As String = ""

        sqlTmp = "SELECT TOP 1 document_id FROM bookin_document "
        sqlTmp &= " WHERE bookin_id ='" & lblId.Text & "'"
        sqlTmp &= " ORDER BY document_id DESC"

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

                tmpMemberID2 = drTmp.Item("document_id")

                tmpMemberID = CInt(tmpMemberID2) + 1
                lblDocId.Text = tmpMemberID.ToString

            End With
        Catch
            lblDocId.Text = "1"
        End Try
        cn.Close()

    End Sub
    Protected Sub bSaveFile_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bSaveFile.Click

        Dim X As String = Request.QueryString("id")
        Dim sEmpNo As String = Session("EmpNo")
        Dim strPath As String = "Document\BookIn\"

        If lblId.Text = "" Then
            MC.MessageBox(Me, "กรุณาบันทึกข้อมูลหนังสือก่อน")
            Exit Sub
        End If

        If lblDocStatus.Text <> "Edit" Then
            If FileUpload1.PostedFile Is Nothing OrElse String.IsNullOrEmpty(FileUpload1.PostedFile.FileName) OrElse FileUpload1.PostedFile.InputStream Is Nothing Then
                lblChkFile.Text = "กรุณาเลือกไฟล์ที่ต้องการอัพโหลด"
                lblChkDetail.Text = ""
                lblChkPage.Text = ""
                Exit Sub
            End If
        End If

        If txtDocDetail.Text.Trim = "" Then
            lblChkDetail.Text = "กรุณากรอกชื่อเอกสาร"
            txtDocDetail.Focus()
            lblChkFile.Text = ""
            lblChkPage.Text = ""
            Exit Sub
        End If

        If txtDocPage.Text.Trim = "" Then
            lblChkPage.Text = "กรุณากรอกจำนวนหน้า"
            txtDocPage.Focus()
            lblChkFile.Text = ""
            lblChkDetail.Text = ""
            Exit Sub
        End If

        If lblDocStatus.Text = "Edit" Then
            Dim Strsql As String
            Strsql = "update bookin_document set title='" & txtDocDetail.Text & "',page='" & txtDocPage.Text & "' "
            Dim MIMEType As String = Nothing
            If FileUpload1.HasFile Then
                Dim extension As String = System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName).ToLower()


                Select Case extension
                    Case ".jpg", ".jpeg", ".jpe"
                        MIMEType = ".jpg"
                        Strsql &= ",file_path='" & strPath & "" & lblId.Text & "-" & lblDocId.Text & "" & MIMEType & "',mime_type='" & MIMEType & "'"
                    Case ".csv"
                        MIMEType = ".csv"
                        Strsql &= ",file_path='" & strPath & "" & lblId.Text & "-" & lblDocId.Text & "" & MIMEType & "',mime_type='" & MIMEType & "'"
                    Case ".xls"
                        MIMEType = ".xls"
                        Strsql &= ",file_path='" & strPath & "" & lblId.Text & "-" & lblDocId.Text & "" & MIMEType & "',mime_type='" & MIMEType & "'"
                    Case ".xlsx"
                        MIMEType = ".xlsx"
                        Strsql &= ",file_path='" & strPath & "" & lblId.Text & "-" & lblDocId.Text & "" & MIMEType & "',mime_type='" & MIMEType & "'"
                    Case ".pdf"
                        MIMEType = ".pdf"
                        Strsql &= ",file_path='" & strPath & "" & lblId.Text & "-" & lblDocId.Text & "" & MIMEType & "',mime_type='" & MIMEType & "'"
                    Case ".doc"
                        MIMEType = ".doc"
                        Strsql &= ",file_path='" & strPath & "" & lblId.Text & "-" & lblDocId.Text & "" & MIMEType & "',mime_type='" & MIMEType & "'"
                    Case ".docx"
                        MIMEType = ".docx"
                        Strsql &= ",file_path='" & strPath & "" & lblId.Text & "-" & lblDocId.Text & "" & MIMEType & "',mime_type='" & MIMEType & "'"
                    Case ".txt"
                        MIMEType = ".txt"
                        Strsql &= ",file_path='" & strPath & "" & lblId.Text & "-" & lblDocId.Text & "" & MIMEType & "',mime_type='" & MIMEType & "'"
                    Case ".htm", ".html"
                        MIMEType = ".html"
                        Strsql &= ",file_path='" & strPath & "" & lblId.Text & "-" & lblDocId.Text & "" & MIMEType & "',mime_type='" & MIMEType & "'"
                    Case Else
                        MC.MessageBox(Me, "Not a valid file format")
                        Exit Sub
                End Select

            End If

            Strsql &= ",creation_by='" & sEmpNo & "',created_date=getdate(),updated_by='" & sEmpNo & "',updated_date=getdate() "
            Strsql &= " where bookin_id='" & lblId.Text & "' and document_id ='" & lblDocId.Text & "'"

            Dim Y As Integer = MD.Execute(Strsql)
            If Y > 0 Then
                If FileUpload1.HasFile Then
                    Func.UploadFile(sEmpNo, FileUpload1, lblId.Text & "-" & lblDocId.Text & MIMEType, strPath)
                End If

                Me.gDataDoc()
                Me.MyGridBind()
                MC.MessageBox(Me, "บันทึกลงฐานข้อมูลเรียบร้อยแล้ว")

                lblDocStatus.Text = ""
                lblDocId.Text = ""

                txtDocDetail.Text = ""
                txtDocPage.Text = ""

                lblChkFile.Text = ""
                lblChkDetail.Text = ""
                lblChkPage.Text = ""

            Else
                MC.MessageBox(Me, "ไม่สามารถบันทึกลงฐานข้อมูลได้")
            End If

        Else

            Try
                Me.AutoFile()
                Dim Strsql As String
                Strsql = "insert into bookin_document (bookin_id,system_no,document_id "

                If FileUpload1.HasFile Then
                    Strsql &= ",file_path,mime_type "
                End If


                Strsql &= ",title,page,creation_by,created_date,updated_by,updated_date)values  "
                Strsql &= "('" & lblId.Text & "','" & lblBookNo.Text & "','" & lblDocId.Text & "' "

                Dim MIMEType As String = Nothing
                If FileUpload1.HasFile Then
                    Dim extension As String = System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName).ToLower()


                    Select Case extension
                        Case ".jpg", ".jpeg", ".jpe"
                            MIMEType = ".jpg"
                            Strsql &= ",'" & strPath & "" & lblId.Text & "-" & lblDocId.Text & "" & MIMEType & "','" & MIMEType & "'"
                        Case ".csv"
                            MIMEType = ".csv"
                            Strsql &= ",'" & strPath & "" & lblId.Text & "-" & lblDocId.Text & "" & MIMEType & "','" & MIMEType & "'"
                        Case ".xls"
                            MIMEType = ".xls"
                            Strsql &= ",'" & strPath & "" & lblId.Text & "-" & lblDocId.Text & "" & MIMEType & "','" & MIMEType & "'"
                        Case ".xlsx"
                            MIMEType = ".xlsx"
                            Strsql &= ",'" & strPath & "" & lblId.Text & "-" & lblDocId.Text & "" & MIMEType & "','" & MIMEType & "'"
                        Case ".pdf"
                            MIMEType = ".pdf"
                            Strsql &= ",'" & strPath & "" & lblId.Text & "-" & lblDocId.Text & "" & MIMEType & "','" & MIMEType & "'"
                        Case ".doc"
                            MIMEType = ".doc"
                            Strsql &= ",'" & strPath & "" & lblId.Text & "-" & lblDocId.Text & "" & MIMEType & "','" & MIMEType & "'"
                        Case ".docx"
                            MIMEType = ".docx"
                            Strsql &= ",'" & strPath & "" & lblId.Text & "-" & lblDocId.Text & "" & MIMEType & "','" & MIMEType & "'"
                        Case ".txt"
                            MIMEType = ".txt"
                            Strsql &= ",'" & strPath & "" & lblId.Text & "-" & lblDocId.Text & "" & MIMEType & "','" & MIMEType & "'"
                        Case ".htm", ".html"
                            MIMEType = ".html"
                            Strsql &= ",'" & strPath & "" & lblId.Text & "-" & lblDocId.Text & "" & MIMEType & "','" & MIMEType & "'"
                        Case Else
                            MC.MessageBox(Me, "Not a valid file format")
                            Exit Sub
                    End Select

                End If

                Strsql &= ",'" & txtDocDetail.Text & "','" & txtDocPage.Text & "','" & sEmpNo & "',getdate(),"
                Strsql &= "'" & sEmpNo & "',getdate())"


                Dim Y As Integer = MD.Execute(Strsql)
                If Y > 0 Then
                    Func.UploadFile(sEmpNo, FileUpload1, lblId.Text & "-" & lblDocId.Text & MIMEType, strPath)
                    Me.gDataDoc()
                    Me.MyGridBind()
                    MC.MessageBox(Me, "บันทึกลงฐานข้อมูลเรียบร้อยแล้ว")

                    lblDocStatus.Text = ""
                    lblDocId.Text = ""

                    txtDocDetail.Text = ""
                    txtDocPage.Text = ""
                Else
                    MC.MessageBox(Me, "ไม่สามารถบันทึกลงฐานข้อมูลได้")
                End If
            Catch ex As Exception
                MC.MessageBox(Me, ex.ToString)
            End Try
        End If
    End Sub
    Private Sub UploadFile()
        'Upload and save file at directory
        If FileUpload1.HasFile Then
            Dim extension As String = System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName).ToLower()
            Dim MIMEType As String = Nothing
            Dim fname As String = lblId.Text + "-" + lblDocId.Text
            Try

                Select Case extension

                    Case ".jpg", ".jpeg", ".jpe"
                        MIMEType = ".jpg"
                        Dim X As String = Path.GetFileName("" & fname & "" & MIMEType & "")
                        X = Server.MapPath("..\Document\BookIn\" & X)
                        FileUpload1.PostedFile.SaveAs(X)
                    Case ".csv"
                        MIMEType = ".csv"
                        Dim X As String = Path.GetFileName("" & fname & "" & MIMEType & "")
                        X = Server.MapPath("..\Document\BookIn\" & X)
                        FileUpload1.PostedFile.SaveAs(X)
                    Case ".xls"
                        MIMEType = ".xls"
                        Dim X As String = Path.GetFileName("" & fname & "" & MIMEType & "")
                        X = Server.MapPath("..\Document\BookIn\" & X)
                        FileUpload1.PostedFile.SaveAs(X)
                    Case ".xlsx"
                        MIMEType = ".xlsx"
                        Dim X As String = Path.GetFileName("" & fname & "" & MIMEType & "")
                        X = Server.MapPath("..\Document\BookIn\" & X)
                        FileUpload1.PostedFile.SaveAs(X)
                    Case ".pdf"
                        MIMEType = ".pdf"
                        Dim X As String = Path.GetFileName("" & fname & "" & MIMEType & "")
                        X = Server.MapPath("..\Document\BookIn\" & X)
                        FileUpload1.PostedFile.SaveAs(X)
                    Case ".doc"
                        MIMEType = ".doc"
                        Dim X As String = Path.GetFileName("" & fname & "" & MIMEType & "")
                        X = Server.MapPath("..\Document\BookIn\" & X)
                        FileUpload1.PostedFile.SaveAs(X)
                    Case ".docx"
                        MIMEType = ".docx"
                        Dim X As String = Path.GetFileName("" & fname & "" & MIMEType & "")
                        X = Server.MapPath("..\Document\BookIn\" & X)
                        FileUpload1.PostedFile.SaveAs(X)
                    Case ".txt"
                        MIMEType = ".txt"
                        Dim X As String = Path.GetFileName("" & fname & "" & MIMEType & "")
                        X = Server.MapPath("..\Document\BookIn\" & X)
                        FileUpload1.PostedFile.SaveAs(X)
                    Case ".htm", ".html"
                        MIMEType = ".html"
                        Dim X As String = Path.GetFileName("" & fname & "" & MIMEType & "")
                        X = Server.MapPath("..\Document\BookIn\" & X)
                        FileUpload1.PostedFile.SaveAs(X)
                    Case Else
                        MC.MessageBox(Me, "Not a valid file format")
                        Exit Sub
                End Select
            Catch ex As Exception
                MC.MessageBox(Me, "Can not upload file!")
            End Try
        End If

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
                Dim L1 As ImageButton = e.Row.Cells(3).Controls(1)
                L1.OnClientClick = "return confirm('คุณต้องการลบข้อมูลใช่หรือไม่?');"
            End If
        End If


    End Sub
    Protected Sub GridView1_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GridView1.RowDeleting
        'Delete Document
        Dim strPath As String = Constant.BaseURL(Request) & ("Document\BookIn\") ' Server.MapPath("..\Document\BookIn\")
        Dim K1 As DataKey = GridView1.DataKeys(e.RowIndex)
        Dim strsql As String
        Dim chk As String

        chk = "select * from bookin_document where bookin_id='" & lblId.Text & "' and document_id ='" & K1(0) & "'"
        Dim DS As DataSet
        Dim mtype As String = ""
        Dim fname As String = ""
        DS = MD.GetDataset(chk)

        If DS.Tables(0).Rows.Count > 0 Then
            mtype = DS.Tables(0).Rows(0).Item("mime_type").ToString
            fname = DS.Tables(0).Rows(0).Item("bookin_id").ToString + "-" + DS.Tables(0).Rows(0).Item("document_id").ToString
            fname = Func.getServerPath() & DS.Tables(0).Rows(0).Item("file_path").ToString
        End If

        strsql = "delete from bookin_document where bookin_id='" & lblId.Text & "' and document_id ='" & K1(0) & "'"
        Dim Y As Integer = MD.Execute(strsql)
        If Y > 0 Then
            Me.gDataDoc()
            Me.MyGridBind()
            Func.DeleteFile(fname)
            'File.Delete(strPath & fname & mtype)
            MC.MessageBox(Me, "ลบข้อมูลเรียบร้อยแล้ว")
        Else
            MC.MessageBox(Me, "ไม่สามารถลบข้อมูลได้")
        End If
    End Sub
    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        'Download File Document
        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim K2 As String
            K2 = Convert.ToString(GridView1.DataKeys(e.Row.RowIndex).Values(0).ToString())
            Dim strsql2 As String = ""


            strsql2 = "select d.bookin_id,d.document_id, "
            strsql2 &= "d.file_path,d.mime_type "
            strsql2 &= "from bookin_document d "
            strsql2 &= "where bookin_id='" & lblId.Text & "' and d.document_id='" & K2(0) & "'"


            Dim dt As DataTable
            dt = MD.GetDataTable(strsql2)

            Dim lblLink As Label = e.Row.Cells(2).FindControl("lblLink")

            For Each dr As DataRow In dt.Rows


                If dr("mime_type").ToString() = ".pdf" Or dr("mime_type").ToString() = ".txt" Then
                    Dim strUrl As String = "http://" & Constant.BaseURL(Request) & dr("file_path").ToString() & ""

                    lblLink.Text = "<a href='" & strUrl & "?time=" & Date.Now.ToString("HH:mm:ss") & "' Target='_Blank'>ดาวน์โหลด</a>&nbsp;&nbsp;"

                Else

                    Dim strUrl As String = "http://" & Constant.BaseURL(Request) & dr("file_path").ToString()
                    lblLink.Text = "<a href='" & strUrl & "?time=" & Date.Now.ToString("HH:mm:ss") & "' Target='_Blank'>ดาวน์โหลด</a>&nbsp;&nbsp;"

                End If

            Next

        End If

    End Sub
    Protected Sub GridView1_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles GridView1.RowEditing
        'Edit Document
        Dim K1 As DataKey = GridView1.DataKeys(e.NewEditIndex)
        Dim lName As Label = GridView1.Rows(e.NewEditIndex).Cells(0).Controls(1)
        Dim lPage As Label = GridView1.Rows(e.NewEditIndex).Cells(1).Controls(1)

        lblDocId.Text = K1(0).ToString
        txtDocDetail.Text = lName.Text
        txtDocPage.Text = lPage.Text

        lblDocStatus.Text = "Edit"
    End Sub
    Protected Sub bCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bCancel.Click
 
        txtDate.Text = "0:00:00"
        txtInDate.Text = "0:00:00"
        ddlTimeHr.SelectedIndex = 0
        ddlTimeMin.SelectedIndex = 0
        txtFrom.Text = ""
        txtKeyword.Text = ""
        txtTo.Text = ""
        txtTopic.Text = ""
        ddlToName.SelectedValue = "0"

        Me.ClearAlert()
    End Sub
    Protected Sub bCancelFile_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bCancelFile.Click
        txtDocDetail.Text = ""
        txtDocPage.Text = ""
        lblChkFile.Text = ""
        lblChkDetail.Text = ""
        lblChkPage.Text = ""
    End Sub
    Protected Sub link2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles link2.Click
        Dim status As String = Request.QueryString("status")
        If status = "edit" Then
            Response.Redirect("../Src/BookInEditList.aspx", True)
        End If
    End Sub
    Protected Sub linkHome_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles linkHome.Click
        Response.Redirect(MD.pHome, True)
    End Sub
    Protected Sub gdvDIV_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gdvDIV.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim K2 As String
            K2 = Convert.ToString(gdvDIV.DataKeys(e.Row.RowIndex).Values(0).ToString())
            Dim strsql As String = ""

            strsql = "select bookin_no,div_id "
            strsql &= "from bookin_send "
            strsql &= "where bookin_no='" & lblBookNo.Text & "' and div_id='" & K2 & "' "

            Dim dt As DataTable
            dt = MD.GetDataTable(strsql)

            Dim lblLinkFile As CheckBox = e.Row.Cells(0).FindControl("cb1")


            For Each dr As DataRow In dt.Rows

                If dt.Rows.Count > 0 Then
                    lblLinkFile.Checked = True
                End If


            Next

        End If
    End Sub
    Function ChkDIV() As String
        'ตรวจสอบว่าเลือกหน่วยงานที่จะส่งหรือไม่'
        Dim S1 As New System.Text.StringBuilder("")
        Dim MD As New MainData
        Dim strSql As String = ""
        Dim txtType As String = ""
        Dim oDs As New Data.DataSet
        Dim sData As String = ""


        For Each dgi As GridViewRow In gdvDIV.Rows
            Dim cb As CheckBox = dgi.Cells(1).FindControl("cb1")
            If cb.Checked = True Then

                Dim K1 As DataKey = gdvDIV.DataKeys(dgi.RowIndex)
                Dim index As Integer = Convert.ToInt32(dgi.RowIndex)
                Dim row As GridViewRow = gdvDIV.Rows(index)
                Dim item As New ListItem()

                item.Text = Server.HtmlDecode(row.Cells(1).Text) 'item

                Dim Vkey As String = K1.Value
                If S1.Length > 0 Then S1.Append("','")

                S1.Append(K1(0).ToString)

            End If

        Next

        If S1.ToString.Length > 0 Then
            Return "'" & S1.ToString & "'"
        Else
            Return ""
        End If

    End Function
    Private Sub DeleteDIV()
        Dim del As String
        del = "delete from bookin_send where bookin_no='" & lblBookNo.Text & "'"
        MD.Execute(del)
    End Sub
    Private Sub SaveDIV()
        'เลือกหน่วยงานที่จะส่ง'
        Dim S1 As New System.Text.StringBuilder("")
        Dim MD As New MainData
        Dim strSql As String = ""
        Dim txtType As String = ""
        Dim oDs As New Data.DataSet
        Dim sData As String = ""


        For Each dgi As GridViewRow In gdvDIV.Rows
            Dim cb As CheckBox = dgi.Cells(0).FindControl("cb1")
            If cb.Checked = True Then

                Dim K1 As DataKey = gdvDIV.DataKeys(dgi.RowIndex)
                Dim index As Integer = Convert.ToInt32(dgi.RowIndex)
                Dim row As GridViewRow = gdvDIV.Rows(index)
                Dim item As New ListItem()

                item.Text = Server.HtmlDecode(row.Cells(1).Text) 'item

                S1.Append(K1(0))
                Dim Vkey As String = K1.Value

                If S1.Length > 0 Then

                    Dim str As String
                    str = "insert into bookin_send (bookin_no,div_id) values "
                    str &= "('" & lblBookNo.Text & "','" & K1(0).ToString & "')"
                    MD.Execute(str)
                End If
            End If
        Next

    End Sub
End Class