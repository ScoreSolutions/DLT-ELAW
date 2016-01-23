Imports System.Data
Imports System.Data.OleDb
Imports System.Security.Cryptography
Imports System.Data.SqlClient
Partial Class Src_Employee
    Inherits System.Web.UI.Page
    Dim cn As OleDbConnection
    Dim cmd As OleDbCommand
    Dim MD As New MainData
    Dim MC As New MainClass
    Dim DV As DataView
    Public iRow As Integer
    Dim DS As DataSet
    Dim iRec As Integer

    Private Sub ChkPermis() 'ฟังก์ชั่นการตรวจสอบสิทธิ์การเข้าใช้
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
        Me.ChkPermis() 'เรียกใช้ฟังก์ชั่นการตรวจสอบสิทธิ์การเข้าใช้ 
        Me.SetJava()
        If Not Page.IsPostBack Then
            ViewState("sortfield") = "empid"
            ViewState("sortdirection") = "asc"

            If X <> "" Then

                Dim strsql As String
                strsql = "select e.empid,p.pos_id,p.pos_name,e.firstname+' '+e.lastname creation_by,e.created_date,e.email,d1.div_name,  "
                strsql &= " e.firstname+' '+e.lastname updated_by,e.updated_date,d.dept_id,d.dept_name,e.sex,e.prefix,d.dept_name,"
                strsql &= " d1.div_id,d1.div_name,e.firstname,e.lastname,e.phonehome,e.phonemobile,e.phonework,e.birthday,p.pos_name,e.status "
                strsql &= " from employee e inner join department d  "
                strsql &= " on e.dept_id =  d.dept_id "
                strsql &= " inner join division d1 "
                strsql &= " on e.div_id = d1.div_id "
                strsql &= " inner join position p "
                strsql &= " on e.pos_id = p.pos_id "
                strsql &= " where e.empid='" & X & "'"

                DS = MD.GetDataset(strsql)
                Session("emp_data") = DS
                iRec = 0
                ViewState("iRec") = iRec

                Me.DataPrefix()
                Me.DataDepartment()
                Me.DataDivision()
                Me.DataPosition()
                Me.MyDataBind()
                Me.FindRow()

                lblStatus.Text = "Edit"
                txtEmpID.ReadOnly = True
                bResetPassword.Visible = True
            Else
                'บันทึกข้อมูลที่เข้ามาใหม่
                txtEmpID.ReadOnly = False
                Dim sql As String

                sql = "select * from employee "

                DS = MD.GetDataset(sql)
                Session("emp_data") = DS
                iRec = 0
                ViewState("iRec") = iRec

                Me.DataPrefix()
                Me.DataDepartment()
                Me.DataDivision()
                Me.DataPosition()

                lblStatus.Text = "Add"
                bResetPassword.Visible = False
            End If
        Else

            DS = Session("emp_data")
            iRec = ViewState("iRec")

        End If

    End Sub

    Public Function BindField(ByVal FieldName As String) As String
        'ฟังก์ชั่นการแสดงข้อมูลสำหรับแก้ไข
        Dim DT As DataTable = DS.Tables(0)
        Select Case FieldName
            Case "money"
                If IsDBNull(DT.Rows(iRec)("money")) Then
                    Return "0.00"
                Else
                    Dim P1 As Double = DT.Rows(iRec)("money")
                    Return P1.ToString("#,##0.00")
                End If
            Case "birthday"
                If IsDBNull(DT.Rows(iRec)("birthday")) Then
                    Return ""
                Else
                    Dim P1 As Date = DT.Rows(iRec)("birthday")
                    Return P1.ToString("dd/MM/yyyy")
                End If
            Case "dates_contract"
                If IsDBNull(DT.Rows(iRec)("dates_contract")) Then
                    Return ""
                Else
                    Dim P1 As Date = DT.Rows(iRec)("dates_contract")
                    Return P1.ToString("dd/MM/yyyy")
                End If
            Case Else
                Return DT.Rows(iRec)(FieldName) & ""
        End Select
    End Function
    Public Sub MyDataBind()
        txtEmpID.DataBind()
        txtFirstName.DataBind()
        txtLastname.DataBind()
        txtPhoneHome.DataBind()
        txtPhoneMobile.DataBind()
        txtPhoneOffice.DataBind()
        txtbirthday.DataBind()

        If DS.Tables(0).Rows(0).Item("status").ToString = "0" Then
            ChkStatus.Checked = True
        Else
            ChkStatus.Checked = False
        End If

    End Sub

    Private Sub FindRow()
        'การแสดงข้อมูลใน drop down list
        Dim X3 As String
        X3 = DS.Tables(0).Rows(iRec)("prefix") & ""
        ddlPrefix.SelectedIndex = FindPrefixRow(X3)

        Dim X4 As String
        X4 = DS.Tables(0).Rows(iRec)("dept_id") & ""
        ddlDept.SelectedIndex = FindDeptRow(X4)

        Dim X5 As String
        X5 = DS.Tables(0).Rows(iRec)("div_id") & ""
        ddlDiv.SelectedIndex = FindDivRow(X5)

        Dim X6 As String
        X6 = DS.Tables(0).Rows(iRec)("pos_id") & ""
        ddlPos.SelectedIndex = FindPosRow(X6)



        If DS.Tables(0).Rows(0).Item("sex").ToString <> "0" Then
            ddlSex.SelectedValue = DS.Tables(0).Rows(0).Item("sex").ToString
        End If
    End Sub
    Public Function FindSexRow(ByVal X As String) As Integer
        Dim i As Integer = 0
        For i = 0 To ddlSex.Items.Count - 1
            If X = ddlSex.Items(i).Value Then
                Return i
            End If
        Next
        Return 0
    End Function
    Public Function FindPrefixRow(ByVal X As String) As Integer
        Dim i As Integer = 0

        For i = 0 To ddlPrefix.Items.Count - 1
            If X = ddlPrefix.Items(i).Value Then
                Return i
            End If
        Next
        Return 0
    End Function
    Public Function FindDeptRow(ByVal X As String) As Integer
        Dim i As Integer = 0

        For i = 0 To ddlDept.Items.Count - 1
            If X = ddlDept.Items(i).Value Then
                Return i
            End If
        Next
        Return 0
    End Function
    Public Function FindDivRow(ByVal X As String) As Integer
        Me.DataDivision()
        Dim i As Integer = 0

        For i = 0 To ddlDiv.Items.Count - 1
            If X = ddlDiv.Items(i).Value Then
                Return i
            End If
        Next
        Return 0
    End Function
    Public Function FindPosRow(ByVal X As String) As Integer
        Me.DataPosition()
        Dim i As Integer = 0

        For i = 0 To ddlPos.Items.Count - 1
            If X = ddlPos.Items(i).Value Then
                Return i
            End If
        Next
        Return 0
    End Function
    Private Sub SetJava()
        'การจำกัดประเภทข้อมูลที่บันทึก
        txtPhoneHome.Attributes.Add("onkeypress", "return blockNonNumbers(this, event,-1,0);")
        txtPhoneOffice.Attributes.Add("onkeypress", "return blockNonNumbers(this, event,-1,0);")
        txtPhoneMobile.Attributes.Add("onkeypress", "return blockNonNumbers(this, event,-1,0);")

    End Sub
    Public Sub DataPrefix()
        Dim strsql As String
        strsql = "select prefix_id,prefix_name from prefix order by prefix_name  "
        Dim DTS As DataTable = MD.GetDataTable(strsql)
        Dim dr As DataRow = DTS.NewRow
        dr!prefix_id = 0
        dr!prefix_name = "---โปรดเลือก---"
        DTS.Rows.InsertAt(dr, 0)
        ddlPrefix.DataTextField = "prefix_name"
        ddlPrefix.DataValueField = "prefix_id"
        ddlPrefix.DataSource = DTS
        ddlPrefix.DataBind()

    End Sub
    Public Sub DataDepartment()
        Dim strsql As String
        strsql = "select dept_id,dept_name from department order by dept_name  "
        Dim DTS As DataTable = MD.GetDataTable(strsql)
        Dim dr As DataRow = DTS.NewRow
        dr!dept_id = 0
        dr!dept_name = "---------โปรดเลือก---------"
        DTS.Rows.InsertAt(dr, 0)
        ddlDept.DataTextField = "dept_name"
        ddlDept.DataValueField = "dept_id"
        ddlDept.DataSource = DTS
        ddlDept.DataBind()
    End Sub
    Public Sub DataDivision()
        Dim strsql As String
        strsql = "select div_id,div_name from division where dept_id='" & ddlDept.SelectedValue & "'  order by div_name  "
        Dim DTS As DataTable = MD.GetDataTable(strsql)
        Dim dr As DataRow = DTS.NewRow
        dr!div_id = 0
        dr!div_name = "---------โปรดเลือก---------"
        DTS.Rows.InsertAt(dr, 0)
        ddlDiv.DataTextField = "div_name"
        ddlDiv.DataValueField = "div_id"
        ddlDiv.DataSource = DTS
        ddlDiv.DataBind()
    End Sub

    Public Sub DataPosition()
        Dim strsql As String
        strsql = "select pos_id,pos_name from position where dept_id='" & ddlDept.SelectedValue & "' and div_id='" & ddlDiv.SelectedValue & "' order by pos_name  "
        Dim DTS As DataTable = MD.GetDataTable(strsql)
        Dim dr As DataRow = DTS.NewRow
        dr!pos_id = 0
        dr!pos_name = "---------โปรดเลือก---------"
        DTS.Rows.InsertAt(dr, 0)
        ddlPos.DataTextField = "pos_name"
        ddlPos.DataValueField = "pos_id"
        ddlPos.DataSource = DTS
        ddlPos.DataBind()
    End Sub
    Private Sub ClearAlert()
        'การลบการแจ้งเตือน
        lblID.Text = ""
        lblName.Text = ""
        lblLastname.Text = ""
        lblDept.Text = ""
        lblDiv.Text = ""
        lblPos.Text = ""
        lblSex.Text = ""
        lblLogin.Text = ""
        lblPass.Text = ""
        lblPreName.Text = ""
    End Sub
    Protected Sub bSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bSave.Click
        'การบันทึกข้อมูล
        Dim strsql As String = ""
        Dim sEmpNo As String = Session("EmpNo")

        If txtEmpID.Text.Trim = "" Then
            Me.ClearAlert()
            lblID.Text = "กรุณากรอกรหัส"
            Exit Sub
        End If
        If ddlSex.SelectedValue = "0" Then
            Me.ClearAlert()
            lblSex.Text = "กรุณาเลือกเพศ"
            Exit Sub
        End If
        If ddlPrefix.SelectedValue = "0" Then
            Me.ClearAlert()
            lblPreName.Text = "กรุณาเลือกคำนำหน้าชื่อ"
            Exit Sub
        End If
        If txtFirstName.Text.Trim = "" Then
            Me.ClearAlert()
            lblName.Text = "กรุณากรอกชื่อ"
            Exit Sub
        End If

        If txtLastname.Text.Trim = "" Then
            Me.ClearAlert()
            lblLastname.Text = "กรุณากรอกนามสกุล"
            Exit Sub
        End If

        'If txtLoginName.Text.Trim = "" Then
        '    Me.ClearAlert()
        '    lblLogin.Text = "กรุณากรอกชื่อเข้าระบบ"
        '    Exit Sub
        'End If

        'If txtPassword.Text.Trim = "" Then
        '    Me.ClearAlert()
        '    lblPass.Text = "กรุณากรอกรหัสผ่าน"
        '    Exit Sub
        'End If

        If ddlDept.SelectedValue = "0" Then
            Me.ClearAlert()
            lblDept.Text = "กรุณาเลือกหน่วยงาน"
            Exit Sub
        End If

        If ddlDiv.SelectedValue = "0" Then
            Me.ClearAlert()
            lblDiv.Text = "กรุณาเลือกส่วนงาน"
            Exit Sub
        End If

        If ddlPos.SelectedValue = "0" Then
            Me.ClearAlert()
            lblPos.Text = "กรุณาเลือกตำแหน่ง"
            Exit Sub
        End If

        Dim status As String
        If ChkStatus.Checked = True Then
            status = 0
        Else
            status = 1
        End If
        If lblStatus.Text = "Edit" Then
            'การบันทึกข้อมูลเมื่อมีฟังก์ชั่นการแก้ไข
            Try
                strsql = "update employee set "
                strsql &= "sex=?,prefix=?,firstname=?,lastname=?,birthday=?,phonehome=?,phonework=?,phonemobile=?, "
                strsql &= "email=?,dept_id=?,div_id=?,pos_id=?,updated_by=?,updated_date=?,status=? "
                strsql &= "where empid=?"

                cn = New OleDbConnection(MD.Strcon)
                cmd = New OleDbCommand(strsql, cn)
                MD.CreateParam(cmd, "TTTTTTTTTTTTTDTT")

                cmd.Parameters("@P1").Value = ddlSex.SelectedValue
                cmd.Parameters("@P2").Value = ddlPrefix.SelectedValue
                cmd.Parameters("@P3").Value = txtFirstName.Text
                cmd.Parameters("@P4").Value = txtLastname.Text

                If txtbirthday.Text.Trim <> "" Then
                    cmd.Parameters("@P5").Value = DateTime.Parse(txtbirthday.Text)
                Else
                    cmd.Parameters("@P5").Value = DBNull.Value
                End If

                cmd.Parameters("@P6").Value = txtPhoneHome.Text
                cmd.Parameters("@P7").Value = txtPhoneOffice.Text
                cmd.Parameters("@P8").Value = txtPhoneMobile.Text
                cmd.Parameters("@P9").Value = txtEmail.Text
                cmd.Parameters("@P10").Value = ddlDept.SelectedValue
                cmd.Parameters("@P11").Value = ddlDiv.SelectedValue
                cmd.Parameters("@P12").Value = ddlPos.SelectedValue
                cmd.Parameters("@P13").Value = sEmpNo
                cmd.Parameters("@P14").Value = DateTime.Parse(Date.Now)
                cmd.Parameters("@P15").Value = status
                cmd.Parameters("@P16").Value = txtEmpID.Text

                cn.Open()
                cmd.ExecuteNonQuery()

                MC.MessageBox(Me, "บันทึกลงฐานข้อมูลเรียบร้อยแล้ว")
                Me.ClearAlert()

                'บันทึก Error_Log
            Catch ex As Exception
                Dim ses As String = System.Web.HttpContext.Current.Session.SessionID
                Dim url As String = HttpContext.Current.Request.FilePath
                Dim browser As String = " Type:" + Request.Browser.Type + " Version:" + Request.Browser.Version + " Browser:" + Request.Browser.Browser

                MC.MessageBox(Me, ex.ToString)
                MC.ErrLog(url, ses, ex.ToString, sEmpNo, browser)

            Finally
                cn.Close()
            End Try
        Else
            'บันทึก Error_Log
            If txtLoginName.Text.Trim = "" Then
                Me.ClearAlert()
                lblLogin.Text = "กรุณากรอกชื่อเข้าระบบ"
                Exit Sub
            End If

            If txtPassword.Text.Trim = "" Then
                Me.ClearAlert()
                lblPass.Text = "กรุณากรอกรหัสผ่าน"
                Exit Sub
            End If

            Try
                strsql = "insert into employee (empid,sex,prefix,firstname,lastname,birthday,"
                strsql += " phonehome, phonework, phonemobile, "
                strsql &= " email,dept_id,div_id,pos_id,creation_by,created_date,"
                strsql += " updated_by,updated_date,status)"
                strsql &= " Values ('" & txtEmpID.Text & "','" & ddlSex.SelectedValue & "','" & ddlPrefix.SelectedValue & "','" & txtFirstName.Text & "','" & txtLastname.Text & "', ?,"
                strsql += " '" & txtPhoneHome.Text & "','" & txtPhoneOffice.Text & "','" & txtPhoneMobile.Text & "', "
                strsql += " '" & txtEmail.Text & "','" & ddlDept.SelectedValue & "','" & ddlDiv.SelectedValue & "','" & ddlPos.SelectedValue & "','" & sEmpNo & "',getdate(),"
                strsql += " '" & sEmpNo & "',getdate(),'" & status & "')"

                cn = New OleDbConnection(MD.Strcon)
                cmd = New OleDbCommand(strsql, cn)
                MD.CreateParam(cmd, "T")
                cmd.Parameters("@P1").Value = ddlSex.SelectedValue
                If (txtbirthday.Text.Trim <> "") Then
                    cmd.Parameters("@P1").Value = DateTime.Parse(txtbirthday.Text)
                Else
                    cmd.Parameters("@P1").Value = DBNull.Value
                End If
                cn.Open()
                cmd.ExecuteNonQuery()

                MC.MessageBox(Me, "บันทึกลงฐานข้อมูลเรียบร้อยแล้ว")
                Me.AddUser()
                lblStatus.Text = "Edit"
                Me.ClearAlert()
            Catch ex As Exception

                Dim ses As String = System.Web.HttpContext.Current.Session.SessionID
                Dim url As String = HttpContext.Current.Request.FilePath
                Dim browser As String = " Type:" + Request.Browser.Type + " Version:" + Request.Browser.Version + " Browser:" + Request.Browser.Browser

                MC.MessageBox(Me, ex.ToString)
                MC.ErrLog(url, ses, ex.ToString, sEmpNo, browser)

            Finally
                cn.Close()
            End Try

        End If
    End Sub
    Private Sub AddUser() 'ฟังก์ชั่นการเพิ่ม ID และ Password ของผู้ใช้แต่ละคน
        Dim sEmpNo As String = Session("EmpNo")
        Dim strsql As String
        Dim md5Hasher As New MD5CryptoServiceProvider()

        Dim hashedBytes As Byte()
        Dim encoder As New UTF8Encoding()

 

        hashedBytes = md5Hasher.ComputeHash(encoder.GetBytes(txtPassword.Text))


        strsql = "insert into users (loginname,password,empid,creation_by,created_date,updated_by,updated_date) "
        strsql &= " values (?,?,?,?,?,?,?)"

        Dim cmd As OleDb.OleDbCommand = MD.CreateCommand(strsql)
        MD.CreateParam(cmd, "TPTTDTD")

        cmd.Parameters(0).Value = txtLoginName.Text
        cmd.Parameters(1).Value = hashedBytes
        cmd.Parameters(2).Value = txtEmpID.Text
        cmd.Parameters(3).Value = sEmpNo
        cmd.Parameters(4).Value = DateTime.Parse(Date.Now)
        cmd.Parameters(5).Value = sEmpNo
        cmd.Parameters(6).Value = DateTime.Parse(Date.Now)

        MD.ExecuteGrid(cmd)
    End Sub
    Protected Sub bCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bCancel.Click
        'ยกเลิกการบันทึกข้อความ
        txtFirstName.Text = ""
        txtEmpID.Text = ""
        txtLastname.Text = ""
        txtEmail.Text = ""
        txtPhoneHome.Text = ""
        txtPhoneMobile.Text = ""
        txtPhoneOffice.Text = ""
        ddlDept.SelectedIndex = 0
        ddlDiv.SelectedIndex = 0
        ddlPos.SelectedIndex = 0
        ddlSex.SelectedIndex = 0
        ddlPrefix.SelectedIndex = 0
        Me.ClearAlert()
    End Sub
    Protected Sub ddlDept_SelectedIndexChanged1(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlDept.SelectedIndexChanged
        Me.DataDivision()
        Me.DataPosition()
    End Sub

    Protected Sub ddlDiv_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlDiv.SelectedIndexChanged
        Me.DataPosition()
    End Sub

    Protected Sub bResetPassword_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bResetPassword.Click
        Dim sEmpNo As String = Session("EmpNo")
        Dim strsql As String
        Dim md5Hasher As New MD5CryptoServiceProvider()

        Dim hashedBytes As Byte()
        Dim encoder As New UTF8Encoding()

        If txtLoginName.Text.Trim = "" Then
            Me.ClearAlert()
            lblLogin.Text = "กรุณากรอกชื่อเข้าระบบ"
            Exit Sub
        End If

        If txtPassword.Text.Trim = "" Then
            Me.ClearAlert()
            lblPass.Text = "กรุณากรอกรหัสผ่าน"
            Exit Sub
        End If

        Try


            hashedBytes = md5Hasher.ComputeHash(encoder.GetBytes(txtPassword.Text))

            strsql = "update users set loginname=?,password=?,updated_by=?,updated_date=? "
            strsql &= " where empid=? "

            Dim cmd As OleDb.OleDbCommand = MD.CreateCommand(strsql)
            MD.CreateParam(cmd, "TPTDT")

            cmd.Parameters(0).Value = txtLoginName.Text
            cmd.Parameters(1).Value = hashedBytes
            cmd.Parameters(2).Value = sEmpNo
            cmd.Parameters(3).Value = DateTime.Parse(Date.Now)

            cmd.Parameters(4).Value = txtEmpID.Text

            MD.ExecuteGrid(cmd)

            MC.MessageBox(Me, "บันทึกลงฐานข้อมูลเรียบร้อยแล้ว")
            Me.ClearAlert()

        Catch ex As Exception
            Dim ses As String = System.Web.HttpContext.Current.Session.SessionID
            Dim url As String = HttpContext.Current.Request.FilePath
            'Request.UserHostName
            Dim browser As String = " Type:" + Request.Browser.Type + " Version:" + Request.Browser.Version + " Browser:" + Request.Browser.Browser

            MC.MessageBox(Me, ex.ToString)
            MC.ErrLog(url, ses, ex.ToString, sEmpNo, browser)
        End Try
    End Sub
End Class


