Imports System.IO
Imports System.Data
Imports System.Data.OleDb
Partial Class Src_BookOutdata
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
    Private Sub Name()
        Dim sEmpNo As String = Session("EmpNo")
        Dim strsql As String
        Dim oDs As New DataSet

        strsql = "select firstname+' '+lastname as fullname from employee where empid='" & sEmpNo & "'"

        oDs = MD.GetDataset(strsql)
        If oDs.Tables(0).Rows.Count > 0 Then
            txtCreateName.Text = oDs.Tables(0).Rows(0).Item("fullname").ToString
            txtPostName.Text = "(" + oDs.Tables(0).Rows(0).Item("fullname").ToString + ")"
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim X As String = Request.QueryString("id")
        Dim status As String = Request.QueryString("status")

        Me.ChkPermis()

        If Not Page.IsPostBack Then
            txtDate.Text = Date.Today
            If X <> "" Then
                'Preview, Approve, Edit
                Dim sql As String

                sql = " select b.bookout_id,b.system_no,b.bookout_no,b.bookkind_id,k.bookkind_name,b.present,  "
                sql &= "b.message,b.postscript,b.postname,b.post_pos,b.comment,b.contact,  "
                sql &= "b.status_id,s.status_name,b.topic,b.keyword,b.dates,e.firstname+' '+e.lastname createname, "
                sql &= "b.user_sign,e1.firstname+' '+e1.lastname signname,b.priority_id,p.priority_name,b.sendto,b.ref_type,b.ref_id,b.ref_title,b.booktype_id, "
                sql &= "right(b.runno,4) runno,b.runno runbook,b.send_type "
                sql &= "from bookout_data b inner join book_kind k "
                sql &= "on b.bookkind_id=k.bookkind_id inner join book_status s "
                sql &= "on b.status_id=s.status_id inner join employee e "
                sql &= "on b.creation_by=e.empid inner join employee e1 "
                sql &= "on b.user_sign=e1.empid inner join book_priority p "
                sql &= "on b.priority_id=p.priority_id "
                sql &= "where b.active=1 "
                sql &= "and b.bookout_id='" & X & "'"

                DS = MD.GetDataset(sql)
                Session("bookout_data") = DS
                iRec = 0
                ViewState("iRec") = iRec

                Me.DataBookPriority()
                Me.DataBookStatus()
                Me.DataBookType()
                Me.DataSentTo()
                Me.DataBookInOut()

                lblId.Text = X
                lblMainStatus.Text = "Edit"

                Me.MyDataBind()
                Me.FindRow()

                txtCreateName.Text = DS.Tables(0).Rows(0).Item("createname").ToString
                txtPostName.Text = DS.Tables(0).Rows(0).Item("postname").ToString

                link2.Text = "แก้ไขหนังสือออก"

            Else
                'Add New
                Dim sql As String

                sql = "select * from bookout_data "

                DS = MD.GetDataset(sql)
                Session("bookout_data") = DS
                iRec = 0
                ViewState("iRec") = iRec

                Me.DataBookPriority()
                Me.DataBookStatus()
                Me.DataBookType()
                Me.DataSentTo()
                Me.DataBookInOut()

                lblId.Text = ""
                lblMainStatus.Text = "Add"
                Me.Name()

                Link2.Text = "บันทึกหนังสือออก"

                Session("TextId3") = ""
                Session("TextIdTitle") = ""
            End If

            Me.gDataDoc()
            Me.MyGridBind()
        Else
            Me.RefreshPage()

            DS = Session("bookout_data")
            iRec = ViewState("iRec")

            If Session("DocumentBookOut") Is Nothing Then
                Me.gDataDoc()
            Else
                DVLst = Session("DocumentBookOut")
            End If


        End If

        TabPanel3.Visible = False
        If status = "cancel" Then
            TabPanel3.Visible = True
            TabContainer1.ActiveTabIndex = 2

            bSave.Visible = False
            bSaveAndSend.Visible = False
            bCancel.Visible = False

            bSaveFile.Visible = False
            bCancelFile.Visible = False

            GridView1.Columns(3).Visible = False
            GridView1.Columns(4).Visible = False
        End If

        txtDate.Attributes.Add("onkeypress", "return blockText(this, event,-1,0);")
        bSelectTitle.Attributes.Add("onclick", "popupwindown('ShowTitle.aspx?id=TextTitle&name=TextIdTitle&type=TextIdType');")
        bSelect1.Attributes.Add("onclick", "popupwindown('SearchEmp.aspx?id=TextName3&name=TextId3');")
        bSaveAndSend.OnClientClick = "return confirm('ยืนยันการส่งข้อมูล');"
        bCancelDoc.OnClientClick = "return confirm('ยืนยันการยกเลิกข้อมูล');"

        'bPreview.Attributes.Add("onclick", "popupwindown('BookOutPreview.aspx?id=" & lblId.Text & "');")

        Try
            If DS.Tables(0).Rows(0).Item("booktype_id").ToString() = "1" Then
                'bPreview.Text = "<a href=""javascript:openwindow('" + "BookOutPreviewIn" + "','" + X + "','" + "');"">" + "พิมพ์หนังสือนำส่ง" + "</a>"
                bPreview.Attributes.Add("onclick", "popupwindown('BookOutPreviewIn.aspx?id=" & lblId.Text & "');")
            Else
                'bPreview.Text = "<a href=""javascript:openwindow('" + "BookOutPreview" + "','" + X + "','" + "');"">" + "พิมพ์หนังสือนำส่ง" + "</a>"
                bPreview.Attributes.Add("onclick", "popupwindown('BookOutPreview.aspx?id=" & lblId.Text & "');")
            End If
        Catch ex As Exception

        End Try

    End Sub
    Private Sub RefreshPage()

        If Session("TextId3") <> "" Then
            txtName1.Text = Session("TextName3")
            lblName.Text = Session("TextId3")
        End If

        If Session("TextIdTitle") <> "" Then
            txtTitle.Text = Session("TextTitle")
            lblTitle.Text = Session("TextIdTitle")
            lblType.Text = Session("TextIdType")
        End If

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
        lblBookNo.DataBind()
        txtDate.DataBind()
        txtTopic.DataBind()
        FCKeditor2.DataBind()
        txtPresent.DataBind()
        txtPostscript.DataBind()
        txtPostName.DataBind()
        txtPostPosition.DataBind()
        txtContact.DataBind()
        txtComment.DataBind()
        txtKeyword.DataBind()
        txtName1.DataBind()

        lblName.Text = DS.Tables(0).Rows(0).Item("user_sign").ToString() 'lblName.DataBind()

        txtTitle.DataBind()
        lblTitle.DataBind()
        lblType.DataBind()
        lblRunNo.DataBind()
        lblRunNoShow.DataBind()

        If DS.Tables(0).Rows(0).Item("send_type").ToString = "0" Or DS.Tables(0).Rows(0).Item("send_type").ToString = "1" Then
            rdbSend.SelectedValue = DS.Tables(0).Rows(0).Item("send_type").ToString
        End If

    End Sub
    Private Sub FindRow()
        'BindData Dropdownlist

        Dim X1 As String
        X1 = DS.Tables(0).Rows(iRec)("status_id") & ""
        ddlBookStatus.SelectedIndex = FindBookStatusRow(X1)

        Dim X2 As String
        X2 = DS.Tables(0).Rows(iRec)("bookkind_id") & ""
        ddlKindId.SelectedIndex = FindBookKindRow(X2)

        Dim X3 As String
        X3 = DS.Tables(0).Rows(iRec)("priority_id") & ""
        ddlPriority.SelectedIndex = FindPriorityRow(X3)

        Dim X4 As String
        X4 = DS.Tables(0).Rows(iRec)("sendto") & ""
        ddlSentTo.SelectedIndex = FindSendToRow(X4)

        Dim X5 As String
        X5 = DS.Tables(0).Rows(iRec)("booktype_id") & ""
        ddlBookType.SelectedIndex = FindBookTypeRow(X5)

    End Sub
    Public Function FindBookTypeRow(ByVal X As String) As Integer
        Dim i As Integer = 0
        For i = 0 To ddlBookType.Items.Count - 1
            If X = ddlBookType.Items(i).Value Then
                Return i
            End If
        Next
        Return 0
    End Function
    Public Function FindBookStatusRow(ByVal X As String) As Integer
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
        For i = 0 To ddlKindId.Items.Count - 1
            If X = ddlKindId.Items(i).Value Then
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
    Public Function FindSendToRow(ByVal X As String) As Integer
        Me.DataSentTo()

        Dim i As Integer = 0
        For i = 0 To ddlSentTo.Items.Count - 1
            If X = ddlSentTo.Items(i).Value Then
                Return i
            End If
        Next
        Return 0
    End Function
    Public Sub DataBookPriority()
        'ระดับความสำคัญ
        Dim strsql As String
        strsql = "select priority_id,priority_name from book_priority order by priority_name  "

        Dim DTS As DataTable = MD.GetDataTable(strsql)

        ddlPriority.DataTextField = "priority_name"
        ddlPriority.DataValueField = "priority_id"
        ddlPriority.DataSource = DTS
        ddlPriority.DataBind()
    End Sub
    Public Sub DataSentTo()
        'รายชื่อหัวหน้ากลุ่ม
        Dim sEmpNo As String = Session("EmpNo")
        Dim strsql As String
        If rdbSend.SelectedValue = 1 Then
            strsql = "select e.empid,e.firstname+' '+e.lastname nameapp1   "
            strsql &= "from employee e inner join division d "
            strsql &= "on e.div_id = d.div_id and e.dept_id=1 and d.div_id = (select div_id from employee where empid='" & sEmpNo & "') and e.pos_id=  "
            strsql &= "(select pos_id from division where div_id=(select div_id from employee where empid='" & sEmpNo & "') )"
            strsql &= "order by e.firstname+' '+e.lastname  "
        Else
            strsql = "select e.empid,e.firstname+' '+e.lastname nameapp1  "
            strsql &= "from employee e where e.dept_id=1 and e.pos_id=4 "
        End If

        Dim DTS As DataTable = MD.GetDataTable(strsql)

        ddlSentTo.DataTextField = "nameapp1"
        ddlSentTo.DataValueField = "empid"
        ddlSentTo.DataSource = DTS
        ddlSentTo.DataBind()

    End Sub
    Public Sub DataBookStatus()
        'สถานะหนังสือ
        Dim strsql As String
        strsql = "select status_id,status_name from book_status where booktype_id=2 order by status_id  "

        Dim DTS As DataTable = MD.GetDataTable(strsql)
  
        ddlBookStatus.DataTextField = "status_name"
        ddlBookStatus.DataValueField = "status_id"
        ddlBookStatus.DataSource = DTS
        ddlBookStatus.DataBind()

    End Sub
    Public Sub DataBookType()
        'ชนิดหนังสือ
        Dim strsql As String
        strsql = "select bookkind_id,bookkind_name from book_kind where booktype_id=2 order by bookkind_id  "

        Dim DTS As DataTable = MD.GetDataTable(strsql)
    
        ddlKindId.DataTextField = "bookkind_name"
        ddlKindId.DataValueField = "bookkind_id"
        ddlKindId.DataSource = DTS
        ddlKindId.DataBind()

    End Sub
    Public Sub DataBookInOut()
        'ประเภทหนังสือ
        Dim strsql As String
        strsql = "select type_id,type_name from bookout_type order by type_name  "

        Dim DTS As DataTable = MD.GetDataTable(strsql)

        ddlBookType.DataTextField = "type_name"
        ddlBookType.DataValueField = "type_id"
        ddlBookType.DataSource = DTS
        ddlBookType.DataBind()

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

        sqlTmp = "SELECT TOP 1 runno FROM bookout_data "
        sqlTmp &= " WHERE left(runno,4) ='" & sAuto & "'"
        sqlTmp &= " AND active=1 "
        sqlTmp &= " ORDER BY runno DESC"

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
        'Genarate Bookout No.
        Dim sqlTmp As String = ""
        Dim comTmp As OleDbCommand = New OleDbCommand
        Dim drTmp As OleDbDataReader
        Dim tmpMemberID As Integer = 0
        Dim tmpMemberID2 As String = ""


        Dim sYear As String
        sYear = Date.Now.Year

        Dim sAuto As String = "BKOT" + Right(sYear, 2)

        sqlTmp = "SELECT TOP 1 right(system_no,3) system_no FROM bookout_data "
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
    Private Sub ClearAlert()

        lblChkDates.Text = ""
        lblChkName.Text = ""
        lblChkTopic.Text = ""
        lblPresent.Text = ""
        lblChkMessage.Text = ""
        lblChkPostScript.Text = ""
        lblChkPostName.Text = ""
        lblChkPostPos.Text = ""
        lblChkKeyword.Text = ""

    End Sub
    Protected Sub bSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bSave.Click
        If txtDate.Text.Year = 1 Then
            Me.ClearAlert()
            lblChkDates.Text = "กรุณาเลือกวันที่"
            Exit Sub
        End If
        If lblName.Text = "" Then
            Me.ClearAlert()
            lblChkName.Text = "กรุณาเลือกผู้ลงนาม"
            Exit Sub
        End If
        If txtTopic.Text = "" Then
            Me.ClearAlert()
            lblChkTopic.Text = "กรุณากรอกเรื่อง"
            Exit Sub
        End If
        If txtPresent.Text = "" Then
            Me.ClearAlert()
            lblPresent.Text = "กรุณากรอกข้อมูล"
            Exit Sub
        End If
        If FCKeditor2.Value = "" Then
            Me.ClearAlert()
            lblChkMessage.Text = "กรุณากรอกเนื้อความ"
            Exit Sub
        End If
        'ไม่เชค
        ''If txtPostscript.Text = "" Then
        ''    Me.ClearAlert()
        ''    lblChkPostScript.Text = "กรุณากรอกคำลงท้าย"
        ''    txtPostscript.Focus()
        ''    Exit Sub
        ''End If
        ''If txtPostName.Text = "" Then
        ''    Me.ClearAlert()
        ''    lblChkPostName.Text = "คำลงท้ายชื่อ"
        ''    txtPostName.Focus()
        ''    Exit Sub
        ''End If
        ''If txtPostPosition.Text = "" Then
        ''    Me.ClearAlert()
        ''    lblChkPostPos.Text = "กรุณากรอกคำลงท้ายตำแหน่ง"
        ''    txtPostPosition.Focus()
        ''    Exit Sub
        ''End If
        ''If txtKeyword.Text = "" Then
        ''    Me.ClearAlert()
        ''    lblChkKeyword.Text = "กรุณากรอกคำค้นหา"
        ''    txtKeyword.Focus()
        ''    Exit Sub
        ''End If

        If lblMainStatus.Text = "Add" Then
            Me.Auto()
            Me.AutoRunNo()
            Me.GenBookNo()
            Me.SaveData("6")
        Else
            Me.EditData("6")
        End If

    End Sub
    Private Sub SaveData(ByVal status As String)
        Dim sEmpNo As String = Session("EmpNo")
        Dim strsql As String

        Try

            strsql = "insert into bookout_data (bookout_id,system_no,dates,bookkind_id, "
            strsql &= "user_request,user_sign,status_id,priority_id,keyword,topic,present, "
            strsql &= "message,postscript,postname,post_pos,contact,comment, "

            If rdbSend.SelectedValue = 1 Then
                strsql &= "sendto, "
            Else
                strsql &= "sendto1, "
            End If
            strsql &= "creation_by,created_date,updated_by,updated_date,ref_type,ref_id,ref_title,booktype_id,runno,send_type "
            strsql &= " ) "
            strsql &= " values  "
            strsql &= " (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)"


            cn = New OleDbConnection(MD.Strcon)
            cmd = New OleDbCommand(strsql, cn)
            MD.CreateParam(cmd, "TTDTTTTTTTTTTTTTTTTDTDTTTTTT")

            cmd.Parameters("@P1").Value = lblId.Text
            cmd.Parameters("@P2").Value = lblBookNo.Text
            If txtDate.Text.Year = 1 Then
                cmd.Parameters("@P3").Value = DBNull.Value
            Else
                cmd.Parameters("@P3").Value = DateTime.Parse(txtDate.Text)
            End If
            cmd.Parameters("@P4").Value = ddlKindId.SelectedValue
            cmd.Parameters("@P5").Value = sEmpNo


            cmd.Parameters("@P6").Value = lblName.Text


            cmd.Parameters("@P7").Value = status
            cmd.Parameters("@P8").Value = ddlPriority.SelectedValue
            cmd.Parameters("@P9").Value = txtKeyword.Text
            cmd.Parameters("@P10").Value = txtTopic.Text
            cmd.Parameters("@P11").Value = txtPresent.Text
            cmd.Parameters("@P12").Value = FCKeditor2.Value
            cmd.Parameters("@P13").Value = txtPostscript.Text
            cmd.Parameters("@P14").Value = txtPostName.Text
            cmd.Parameters("@P15").Value = txtPostPosition.Text
            cmd.Parameters("@P16").Value = txtContact.Text
            cmd.Parameters("@P17").Value = txtComment.Text
            cmd.Parameters("@P18").Value = ddlSentTo.Text
            cmd.Parameters("@P19").Value = sEmpNo
            cmd.Parameters("@P20").Value = DateTime.Parse(Date.Now)
            cmd.Parameters("@P21").Value = sEmpNo
            cmd.Parameters("@P22").Value = DateTime.Parse(Date.Now)

            cmd.Parameters("@P23").Value = lblType.Text
            cmd.Parameters("@P24").Value = lblTitle.Text
            cmd.Parameters("@P25").Value = txtTitle.Text
            cmd.Parameters("@P26").Value = ddlBookType.SelectedValue
            cmd.Parameters("@P27").Value = lblRunNo.Text
            cmd.Parameters("@P28").Value = rdbSend.SelectedValue

            cn.Open()
            cmd.ExecuteNonQuery()
            MC.MessageBox(Me, "บันทึกลงฐานข้อมูลเรียบร้อยแล้ว")
            lblMainStatus.Text = "Edit"

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
    End Sub
    Private Sub EditData(ByVal status As String)
        Dim sEmpNo As String = Session("EmpNo")
        Dim strsql As String
  
        Try

            strsql = "update bookout_data set system_no=?,dates=?,bookkind_id=?, "
            strsql &= "user_request=?,user_sign=?,status_id=?,priority_id=?,keyword=?,topic=?,present=?, "
            strsql &= "message=?,postscript=?,postname=?,post_pos=?,contact=?,comment=?,sendto=?, "
            strsql &= "updated_by=?,updated_date=?,ref_type=?,ref_id=?,ref_title=?,booktype_id=?,runno=?,send_type=?,sendto1=? "
            strsql &= "where bookout_id=? "


            cn = New OleDbConnection(MD.Strcon)
            cmd = New OleDbCommand(strsql, cn)
            MD.CreateParam(cmd, "TDTTTTTTTTTTTTTTTTDTTTTTTTT")


            cmd.Parameters("@P1").Value = lblBookNo.Text

            If txtDate.Text.Year = 1 Then
                cmd.Parameters("@P2").Value = DBNull.Value
            Else
                cmd.Parameters("@P2").Value = DateTime.Parse(txtDate.Text)
            End If
            cmd.Parameters("@P3").Value = ddlKindId.SelectedValue
            cmd.Parameters("@P4").Value = sEmpNo
            cmd.Parameters("@P5").Value = lblName.Text 'DS.Tables(0).Rows(0).Item("user_sign").ToString 
            cmd.Parameters("@P6").Value = status
            cmd.Parameters("@P7").Value = ddlPriority.SelectedValue
            cmd.Parameters("@P8").Value = txtKeyword.Text
            cmd.Parameters("@P9").Value = txtTopic.Text
            cmd.Parameters("@P10").Value = txtPresent.Text
            cmd.Parameters("@P11").Value = FCKeditor2.Value
            cmd.Parameters("@P12").Value = txtPostscript.Text
            cmd.Parameters("@P13").Value = txtPostName.Text
            cmd.Parameters("@P14").Value = txtPostPosition.Text
            cmd.Parameters("@P15").Value = txtContact.Text
            cmd.Parameters("@P16").Value = txtComment.Text

            If rdbSend.SelectedValue = 1 Then
                cmd.Parameters("@P17").Value = ddlSentTo.SelectedValue
            Else
                cmd.Parameters("@P17").Value = String.Empty
            End If

            cmd.Parameters("@P18").Value = sEmpNo
            cmd.Parameters("@P19").Value = DateTime.Parse(Date.Now)

            cmd.Parameters("@P20").Value = lblType.Text 'DS.Tables(0).Rows(0).Item("ref_type").ToString 
            cmd.Parameters("@P21").Value = lblTitle.Text 'DS.Tables(0).Rows(0).Item("ref_id").ToString 
            cmd.Parameters("@P22").Value = txtTitle.Text
            cmd.Parameters("@P23").Value = ddlBookType.SelectedValue
            cmd.Parameters("@P24").Value = lblRunNo.Text
            cmd.Parameters("@P25").Value = rdbSend.SelectedValue

            If rdbSend.SelectedValue = 0 Then
                cmd.Parameters("@P26").Value = ddlSentTo.SelectedValue
            Else
                cmd.Parameters("@P26").Value = String.Empty
            End If

            cmd.Parameters("@P27").Value = lblId.Text

            cn.Open()
            cmd.ExecuteNonQuery()
            MC.MessageBox(Me, "บันทึกลงฐานข้อมูลเรียบร้อยแล้ว")
            lblMainStatus.Text = "Edit"

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
    End Sub
    Private Sub UpdateActive()
        'ก่อนเปลียนสถานะให้ update active=0
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
    Private Sub ClearData()
        txtDate.Text = "0:00:00"
        txtName1.Text = ""
        lblName.Text = ""
        txtCreateName.Text = ""
        txtTopic.Text = ""
        txtPresent.Text = ""
        FCKeditor2.Value = ""
        txtPostscript.Text = ""
        txtPostName.Text = ""
        txtPostPosition.Text = ""
        txtContact.Text = ""
        txtComment.Text = ""
        txtKeyword.Text = ""
    End Sub
    Protected Sub bSaveAndSend_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bSaveAndSend.Click
        If txtDate.Text.Year = 1 Then
            Me.ClearAlert()
            lblChkDates.Text = "กรุณาเลือกวันที่"
            Exit Sub
        End If
        If lblName.Text = "" Then
            Me.ClearAlert()
            lblChkName.Text = "กรุณาเลือกผู้ลงนาม"
            Exit Sub
        End If
        If txtTopic.Text = "" Then
            Me.ClearAlert()
            lblChkTopic.Text = "กรุณากรอกเรื่อง"
            Exit Sub
        End If
        If txtPresent.Text = "" Then
            Me.ClearAlert()
            lblPresent.Text = "กรุณากรอกข้อมูล"
            Exit Sub
        End If
        If FCKeditor2.Value = "" Then
            Me.ClearAlert()
            lblChkMessage.Text = "กรุณากรอกเนื้อความ"
            Exit Sub
        End If
        If txtPostscript.Text = "" Then
            Me.ClearAlert()
            lblChkPostScript.Text = "กรุณากรอกคำลงท้าย"
            txtPostscript.Focus()
            Exit Sub
        End If
        If txtPostName.Text = "" Then
            Me.ClearAlert()
            lblChkPostName.Text = "คำลงท้ายชื่อ"
            txtPostName.Focus()
            Exit Sub
        End If
        If txtPostPosition.Text = "" Then
            Me.ClearAlert()
            lblChkPostPos.Text = "กรุณากรอกคำลงท้ายตำแหน่ง"
            txtPostPosition.Focus()
            Exit Sub
        End If
        If txtKeyword.Text = "" Then
            Me.ClearAlert()
            lblChkKeyword.Text = "กรุณากรอกคำค้นหา"
            txtKeyword.Focus()
            Exit Sub
        End If

        Me.AutoRunNo()
        Me.UpdateActive()
        Me.Auto()

        If lblBookNo.Text = "" Then
            Me.GenBookNo()
        End If


        If rdbSend.SelectedValue = 1 Then
            Me.SaveData("7")
        Else
            Me.SaveData("8")
        End If

        Me.ClearData()

        Response.Redirect("../Src/BookOutDataList.aspx", True)
    End Sub
    Protected Sub linkHome_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkHome.Click
        Response.Redirect(MD.pHome, True)
    End Sub
    Protected Sub link2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Link2.Click
        Dim X As String = Request.QueryString("id")

        If X <> "" Then
            Response.Redirect("../Src/BookOutEditList.aspx", True)
        End If

    End Sub
    Private Sub AutoFile()
        'Genarate Document Id
        Dim sqlTmp As String = ""
        Dim comTmp As OleDbCommand = New OleDbCommand
        Dim drTmp As OleDbDataReader
        Dim tmpMemberID As Integer = 0
        Dim tmpMemberID2 As String = ""

        sqlTmp = "SELECT TOP 1 document_id FROM bookout_document "
        sqlTmp &= " WHERE bookout_id ='" & lblId.Text & "'"
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
    Private Sub gDataDoc(Optional ByVal Type As String = "")
        'Data in Gridview (Table BOOKOUT_DOCUMENT)
        Dim X As String = Request.QueryString("id")
        Dim strsql As String

        strsql = " select b.document_id,b.title,b.page  "
        strsql &= "from bookout_document b "
        strsql &= "where b.system_no='" & lblBookNo.Text & "'"

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
    Protected Sub bSaveFile_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bSaveFile.Click
        Dim X As String = Request.QueryString("id")
        Dim sEmpNo As String = Session("EmpNo")
        Dim strPath As String = "Document\BookOut\"

        If lblId.Text = "" Then
            Me.bSave_Click(sender, e)
        End If

        If lblDocStatus.Text <> "Edit" Then
            If FileUpload1.PostedFile Is Nothing OrElse String.IsNullOrEmpty(FileUpload1.PostedFile.FileName) OrElse FileUpload1.PostedFile.InputStream Is Nothing Then
                lblAFile.Text = "กรุณาเลือกไฟล์ที่ต้องการอัพโหลด"
                lblADetail1.Text = ""
                lblAPage.Text = ""
                Exit Sub
            End If
        End If

        If txtDocDetail.Text.Trim = "" Then
            lblADetail1.Text = "กรุณากรอกชื่อเอกสาร"
            txtDocDetail.Focus()
            lblAFile.Text = ""
            lblAPage.Text = ""
            Exit Sub
        End If

        If txtDocPage.Text.Trim = "" Then
            lblAPage.Text = "กรุณากรอกจำนวนหน้า"
            txtDocPage.Focus()
            lblADetail1.Text = ""
            lblAFile.Text = ""
            Exit Sub
        End If

        If lblDocStatus.Text = "Edit" Then
            Dim Strsql As String
            Strsql = "update bookout_document set title='" & txtDocDetail.Text & "',page='" & txtDocPage.Text & "' "
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
            Strsql &= " where bookout_id='" & lblId.Text & "' and document_id ='" & lblDocId.Text & "'"

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

                lblAPage.Text = ""
                lblADetail1.Text = ""
                lblAFile.Text = ""

            Else
                MC.MessageBox(Me, "ไม่สามารถบันทึกลงฐานข้อมูลได้")
            End If

        Else

            Try
                Me.AutoFile()
                Dim Strsql As String
                Strsql = "insert into bookout_document (bookout_id,system_no,document_id "

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

                    lblAFile.Text = ""
                    lblADetail1.Text = ""
                    lblAPage.Text = ""
                Else
                    MC.MessageBox(Me, "ไม่สามารถบันทึกลงฐานข้อมูลได้")
                End If
            Catch ex As Exception
                MC.MessageBox(Me, ex.ToString)
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
                Dim L1 As ImageButton = e.Row.Cells(3).Controls(0)
                L1.OnClientClick = "return confirm('คุณต้องการลบข้อมูลใช่หรือไม่?');"
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
    Protected Sub GridView1_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GridView1.RowDeleting
        'Delete Document
        Dim strPath As String = "http://" & Constant.BaseURL(Request) & "Document/BookOut/"
        Dim K1 As DataKey = GridView1.DataKeys(e.RowIndex)
        Dim strsql As String
        Dim chk As String

        chk = "select * from bookout_document where bookout_id='" & lblId.Text & "' and document_id ='" & K1(0) & "'"
        Dim DS As DataSet
        Dim mtype As String = ""
        Dim fname As String = ""
        DS = MD.GetDataset(chk)

        If DS.Tables(0).Rows.Count > 0 Then
            mtype = DS.Tables(0).Rows(0).Item("mime_type").ToString
            fname = Func.getServerPath() & DS.Tables(0).Rows(0).Item("file_path").ToString
        End If

        strsql = "delete from bookout_document where bookout_id='" & lblId.Text & "' and document_id ='" & K1(0) & "'"
        Dim Y As Integer = MD.Execute(strsql)
        If Y > 0 Then
            Me.gDataDoc()
            Me.MyGridBind()
            Func.DeleteFile(fname)
            MC.MessageBox(Me, "ลบข้อมูลเรียบร้อยแล้ว")
        Else
            MC.MessageBox(Me, "ไม่สามารถลบข้อมูลได้")
        End If
    End Sub
    Protected Sub GridView1_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles GridView1.RowEditing
        Dim K1 As DataKey = GridView1.DataKeys(e.NewEditIndex)
        Dim lName As Label = GridView1.Rows(e.NewEditIndex).Cells(0).Controls(0)
        Dim lPage As Label = GridView1.Rows(e.NewEditIndex).Cells(1).Controls(0)

        lblDocId.Text = K1(0).ToString
        txtDocDetail.Text = lName.Text
        txtDocPage.Text = lPage.Text

        lblDocStatus.Text = "Edit"
    End Sub
    Protected Sub bCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bCancel.Click
        Me.ClearAlert()
        txtName1.Text = ""
        lblName.Text = ""
        txtTopic.Text = ""
        txtPresent.Text = ""
        FCKeditor2.Value = ""
        txtPostName.Text = ""
        txtPostPosition.Text = ""
        txtPostscript.Text = ""
        txtContact.Text = ""
        txtComment.Text = ""
        txtKeyword.Text = ""
    End Sub
    Protected Sub bCancelDoc_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bCancelDoc.Click
        Dim sEmpNo As String = Session("EmpNo")
        Dim strsql As String
        If txtCancelComment.Text = "" Then
            lblChkCancelComment.Text = "กรุณากรอกสาเหตุ"
            txtCancelComment.Focus()
            Exit Sub
        End If

        strsql = "update bookout_data set cancel=1,cancel_by='" & sEmpNo & "',cancel_date=getdate(),cancel_comment='" & txtCancelComment.Text & "' where bookout_id='" & lblId.Text & "'"

        Dim Y As Integer = MD.Execute(strsql)
        If Y > 0 Then

            MC.MessageBox(Me, "ยกเลิกรายการเรียบร้อยแล้ว")
            Response.Redirect("../Src/BookOutDataList.aspx", True)

        End If

    End Sub
    Protected Sub bDelTitle_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bDelTitle.Click
        txtTitle.Text = ""
        lblTitle.Text = ""
        lblType.Text = ""
    End Sub
    Protected Sub bPreview_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bPreview.Click

    End Sub
    Protected Sub rdbSend_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdbSend.SelectedIndexChanged
        Me.DataSentTo()
    End Sub
End Class