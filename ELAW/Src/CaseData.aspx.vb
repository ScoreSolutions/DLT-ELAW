Imports System.IO
Imports System.Data
Imports System.Data.OleDb
Partial Class Src_CaseData
    Inherits System.Web.UI.Page
    Dim cn As OleDbConnection
    Dim cmd As OleDbCommand
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

                sql = " select c.case_id,c.black_no,c.red_no,c.status_id,s.status_name,c.type_id,t.type_name, "
                sql &= "e.firstname+' '+e.lastname fullname,c.defendant,c.prosecutor,c.court_id,ct.court_name, "
                sql &= "c.attorney_id,a.attorney_name,c.prosecutor,c.prosecutor1,c.defendant,c.defendant1,c.keyword,c.detail, "
                sql &= "c.recieve_type,c.recieve_date,c.app1,c.app2,c.case_no,c.app1_app,c.app1_comment,c.app1_date, "
                sql &= "e1.firstname+' '+e1.lastname appname,c.ref_bookin,b.topic,c.ref_case,r.case2,c.court_name cname "
                sql &= "from case_data c inner join case_status s "
                sql &= "on c.status_id=s.status_id inner join case_type t "
                sql &= "on c.type_id=t.type_id inner join employee e "
                sql &= "on c.creation_by=e.empid left join court ct "
                sql &= "on c.court_id=ct.court_id left join attorney a "
                sql &= "on c.attorney_id=a.attorney_id inner join employee e1 "
                sql &= "on c.app1=e1.empid left join bookin_data b "
                sql &= "on c.ref_bookin=b.bookin_id left join "

                sql &= " (select c.case_id,c.ref_case,'คดีดำ : '+c2.black_no+' คดีแดง : '+c2.red_no case2 "
                sql &= " from case_data c"
                sql &= " inner join case_data c2"
                sql &= " on c.ref_case=c2.case_id "
                sql &= " where c.active=1 and c.ref_case is not null)r "
                sql &= " on c.case_id=r.case_id "

                sql &= " where c.case_id='" & X & "'"

                DS = MD.GetDataset(sql)
                Session("case_data") = DS
                iRec = 0
                ViewState("iRec") = iRec

                lblId.Text = X

                Me.DataCaseType()
                Me.DataCaseStatus()
                Me.DataCaseAttorney()
                Me.DataCaseCourt()


                Me.DataApp1()
                Me.DataApp2()


                Me.MyDataBind()
                Me.FindRow()

                ddlStatus.Enabled = False

                lblMainStatus.Text = "Edit"

                link2.Text = "แก้ไขข้อมูลคดี"

            Else
                'Add New
                Dim sql As String

                sql = "select * from case_data "

                DS = MD.GetDataset(sql)
                Session("case_data") = DS
                iRec = 0
                ViewState("iRec") = iRec

                Me.DataCaseType()
                Me.DataCaseStatus()
                Me.DataCaseCourt()
                Me.DataCaseAttorney()
                Me.DataApp1()
                Me.DataApp2()


                ddlStatus.Enabled = False

                lblMainStatus.Text = "Add"


                link2.Text = "บันทึกข้อมูลคดี"

                Session("TextIdTitle") = ""
                Session("TextIdCase") = ""

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

        Else
            Me.RefreshPage()

            DS = Session("case_data")
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

        txtDocPage.Attributes.Add("onkeypress", "return blockNonNumbers(this, event,-1,0);")
        txtReceiveDate.Attributes.Add("onkeypress", "return blockText(this, event,-1,0);")
        txtDate1.Attributes.Add("onkeypress", "return blockText(this, event,-1,0);")
        txtDate2.Attributes.Add("onkeypress", "return blockText(this, event,-1,0);")
        bSelectTitle.Attributes.Add("onclick", "popupwindown('ShowBookIn.aspx?id=TextTitle&name=TextIdTitle');")

        bSelectCase.Attributes.Add("onclick", "popupwindown('ShowCase.aspx?id=TextIdCase&name=TextCase');")


        bSaveAndSend.OnClientClick = "return confirm('ยืนยันการส่งข้อมูล');"
    End Sub
    Private Sub RefreshPage()

        If Session("TextIdTitle") <> "" Then
            txtTitle.Text = Session("TextTitle")
            lblTitle.Text = Session("TextIdTitle")
        End If

        If Session("TextIdCase") <> "" Then
            txtNameCase.Text = Session("TextCase")
            lblIdCase.Text = Session("TextIdCase")
        End If

    End Sub
    Private Sub AddNew()
        Dim dr As DataRow = DS.Tables(0).NewRow
        DS.Tables(0).Rows.Add(dr)
        iRec = DS.Tables(0).Rows.Count - 1
        ViewState("iRec") = iRec
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
            Case "app1_date"
                If IsDBNull(DT.Rows(iRec)("app1_date")) Then
                    Return ""
                Else
                    Dim P1 As Date = DT.Rows(iRec)("app1_date")
                    Return P1.ToString("dd/MM/yyyy HH:mm")
                End If
            Case Else
                Return DT.Rows(iRec)(FieldName) & ""
        End Select
    End Function
    Public Sub MyDataBind()
        'Databind at Control
        txtBlackNo.DataBind()
        txtRedNo.DataBind()
        txtDefendant.DataBind()
        txtDefendant1.DataBind()
        txtProsecutor.DataBind()
        txtProsecutor1.DataBind()
        txtKeyword.DataBind()
        FCKeditor1.DataBind()
        txtReceiveDate.DataBind()
        lblCaseNo.Text = DS.Tables(0).Rows(0).Item("case_no").ToString()
        'lblCaseNo.DataBind()
        txtAppComent.DataBind()
        lblApp1Date.DataBind()
        lblApp1_App.DataBind()
        lblAppName.DataBind()

        If DS.Tables(0).Rows(0).Item("recieve_type").ToString = "0" Or DS.Tables(0).Rows(0).Item("recieve_type").ToString = "1" Or DS.Tables(0).Rows(0).Item("recieve_type").ToString = "2" Then
            rdbcloserecieve.SelectedValue = DS.Tables(0).Rows(0).Item("recieve_type").ToString
        End If
        If DS.Tables(0).Rows(0).Item("app1_app").ToString = "F" Then
            lblApp1_App.Text = "แก้ไข"
        Else
            lblApp1_App.Text = "ผ่าน"
        End If

        txtTitle.DataBind()
        lblTitle.DataBind()

        txtNameCase.DataBind()
        lblIdCase.DataBind()
        txtCourtName.DataBind()

    End Sub
    Private Sub FindRow()
        'BindData Dropdownlist

        Dim X1 As String
        X1 = DS.Tables(0).Rows(iRec)("type_id") & ""
        ddlCaseType.SelectedIndex = FindTypeRow(X1)

        Dim X2 As String
        X2 = DS.Tables(0).Rows(iRec)("court_id") & ""
        ddlCourt.SelectedIndex = FindCourtRow(X2)

        Dim X3 As String
        X3 = DS.Tables(0).Rows(iRec)("attorney_id") & ""
        ddlAttorney.SelectedIndex = FindAttorneyRow(X3)

        Dim X4 As String
        X4 = DS.Tables(0).Rows(iRec)("status_id") & ""
        ddlStatus.SelectedIndex = FindStatusRow(X4)

        Dim X5 As String
        X5 = DS.Tables(0).Rows(iRec)("app1") & ""
        ddlApp1.SelectedIndex = FindApp1Row(X5)

        Dim X6 As String
        X6 = DS.Tables(0).Rows(iRec)("app2") & ""
        ddlApp2.SelectedIndex = FindApp2Row(X6)

    End Sub
    Public Function FindTypeRow(ByVal X As String) As Integer
        Dim i As Integer = 0
        For i = 0 To ddlCaseType.Items.Count - 1
            If X = ddlCaseType.Items(i).Value Then
                Return i
            End If
        Next
        Return 0
    End Function
    Public Function FindCourtRow(ByVal X As String) As Integer
        Dim i As Integer = 0
        For i = 0 To ddlCourt.Items.Count - 1
            If X = ddlCourt.Items(i).Value Then
                Return i
            End If
        Next
        Return 0
    End Function
    Public Function FindAttorneyRow(ByVal X As String) As Integer
        Me.DataCaseAttorney()
        Dim i As Integer = 0
        For i = 0 To ddlAttorney.Items.Count - 1
            If X = ddlAttorney.Items(i).Value Then
                Return i
            End If
        Next
        Return 0
    End Function
    Public Function FindStatusRow(ByVal X As String) As Integer
        Me.DataCaseStatus()
        Dim i As Integer = 0
        For i = 0 To ddlStatus.Items.Count - 1
            If X = ddlStatus.Items(i).Value Then
                Return i
            End If
        Next
        Return 0
    End Function
    Public Function FindApp1Row(ByVal X As String) As Integer
        Dim i As Integer = 0

        For i = 0 To ddlApp1.Items.Count - 1
            If X = ddlApp1.Items(i).Value Then
                Return i
            End If
        Next
        Return 0
    End Function
    Public Function FindApp2Row(ByVal X As String) As Integer
        Dim i As Integer = 0

        For i = 0 To ddlApp2.Items.Count - 1
            If X = ddlApp2.Items(i).Value Then
                Return i
            End If
        Next
        Return 0
    End Function
    Public Sub DataCaseType()
        'ประเภทคดี
        Dim strsql As String
        strsql = "select type_id,type_name    "
        strsql &= "from case_type order by type_name "

        Dim DTS As DataTable = MD.GetDataTable(strsql)

        ddlCaseType.DataTextField = "type_name"
        ddlCaseType.DataValueField = "type_id"
        ddlCaseType.DataSource = DTS
        ddlCaseType.DataBind()

    End Sub
    Public Sub DataCaseStatus()
        'สถานะคดี
        Dim strsql As String
        strsql = "select status_id,status_name    "
        strsql &= "from case_status where type_id='" & ddlCaseType.SelectedValue & "' order by status_id "

        Dim DTS As DataTable = MD.GetDataTable(strsql)

        ddlStatus.DataTextField = "status_name"
        ddlStatus.DataValueField = "status_id"
        ddlStatus.DataSource = DTS
        ddlStatus.DataBind()

    End Sub
    Public Sub DataCaseCourt()
        'ชื่อศาล
        Dim strsql As String
        strsql = "select court_id,court_name    "
        strsql &= "from court order by court_name "

        Dim DTS As DataTable = MD.GetDataTable(strsql)
        Dim dr As DataRow = DTS.NewRow
        dr!court_id = 0
        dr!court_name = "---------โปรดเลือก---------"
        DTS.Rows.InsertAt(dr, 0)

        ddlCourt.DataTextField = "court_name"
        ddlCourt.DataValueField = "court_id"
        ddlCourt.DataSource = DTS
        ddlCourt.DataBind()

    End Sub
    Public Sub DataCaseAttorney()
        'ชื่ออัยการ
        Dim strsql As String
        strsql = "select attorney_id id,attorney_name+' โทร. '+tel name    "
        strsql &= "from attorney where court_id='" & ddlCourt.SelectedValue & "' order by attorney_name "

        Dim DTS As DataTable = MD.GetDataTable(strsql)
        Dim dr As DataRow = DTS.NewRow
        dr!id = 0
        dr!name = "---------โปรดเลือก---------"
        DTS.Rows.InsertAt(dr, 0)

        ddlAttorney.DataTextField = "name"
        ddlAttorney.DataValueField = "id"
        ddlAttorney.DataSource = DTS
        ddlAttorney.DataBind()

    End Sub
    Public Sub DataApp1()
        'ชื่อหัวหน้าผู้ตรวจสอบ
        Dim strsql As String
        strsql = "select e.empid,e.firstname+' '+e.lastname nameapp1   "
        strsql &= "from employee e inner join division d "
        strsql &= "on e.div_id = d.div_id and e.dept_id=1 and d.div_id=5 and e.pos_id=  "
        strsql &= "(select pos_id from division where div_id=5 )"
        strsql &= "order by e.firstname+' '+e.lastname  "

        Dim DTS As DataTable = MD.GetDataTable(strsql)
      
        ddlApp1.DataTextField = "nameapp1"
        ddlApp1.DataValueField = "empid"
        ddlApp1.DataSource = DTS
        ddlApp1.DataBind()

    End Sub
    Public Sub DataApp2()
        'ชื่อ ผอ ผู้ตรวจสอบ
        Dim strsql As String
        strsql = "select e.empid,e.firstname+' '+e.lastname nameapp2   "
        strsql &= "from employee e inner join division d "
        strsql &= "on e.div_id = d.div_id and e.dept_id=1 and d.div_id=7 and e.pos_id=  "
        strsql &= "(select pos_id from division where div_id=7 )"
        strsql &= "order by e.firstname+' '+e.lastname  "

        Dim DTS As DataTable = MD.GetDataTable(strsql)
    
        ddlApp2.DataTextField = "nameapp2"
        ddlApp2.DataValueField = "empid"
        ddlApp2.DataSource = DTS
        ddlApp2.DataBind()

    End Sub
    Private Sub gDataDoc(Optional ByVal Type As String = "")
        'Data in Gridview (Table CASE_DOCUMENT)
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
    Public Function ImagesGet(ByVal X As String) As String
        Dim X1 As String = Replace(X, " ", "_")
        Dim X2 As String = "..\Images\" & X1 & ".jpg"
        Dim xFile As String = Server.MapPath(X2)

        If IO.File.Exists(xFile) Then
            Return "<img src='" & Replace(X2, "\", "/") & "' align='absmiddle'>"
        Else
            Return ""
        End If
    End Function
    Private Sub gDataCourt(Optional ByVal Type As String = "")
        'Data in Gridview2 (Table COURT_DATE)
        Dim X As String = Request.QueryString("id")
        Dim strsql As String

        strsql = " select court_date_id,title,dates,alert alert_id,  "
        strsql &= "case when alert=1 then 'OK' else 'NO' end alert "
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

        strsql = " select explain_date_id,title,dates,alert alert_id, "
        strsql &= "case when alert=1 then 'OK' else 'NO' end alert "
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
    Protected Sub ddlCourt_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCourt.SelectedIndexChanged
        Me.DataCaseAttorney()
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
                lblId.Text = sAuto + tmpMemberID.ToString("-0000")

            End With
        Catch
            lblId.Text = sAuto + "-0001"
        End Try
        cn.Close()

    End Sub
    Private Sub AutoFile()
        'Genarate Document Id
        Dim sqlTmp As String = ""
        Dim comTmp As OleDbCommand = New OleDbCommand
        Dim drTmp As OleDbDataReader
        Dim tmpMemberID As Integer = 0
        Dim tmpMemberID2 As String = ""

        sqlTmp = "SELECT TOP 1 document_id FROM case_document "
        sqlTmp &= " WHERE case_id ='" & lblId.Text & "'"
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
    Private Sub GenCaseNo()
        'Genarate Case No.
        Dim sqlTmp As String = ""
        Dim comTmp As OleDbCommand = New OleDbCommand
        Dim drTmp As OleDbDataReader
        Dim tmpMemberID As Integer = 0
        Dim tmpMemberID2 As String = ""


        Dim sYear As String
        sYear = Date.Now.Year

        Dim sAuto As String = "CASE" + Right(sYear, 2)

        sqlTmp = "SELECT TOP 1 right(case_no,3) case_no FROM case_data "
        sqlTmp &= " WHERE left(case_no,6) ='" & sAuto & "'"
        sqlTmp &= " ORDER BY case_no DESC"

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

                tmpMemberID2 = Right(drTmp.Item("case_no"), 3)

                tmpMemberID = CInt(tmpMemberID2) + 1
                lblCaseNo.Text = sAuto + tmpMemberID.ToString("-000")

            End With
        Catch
            lblCaseNo.Text = sAuto + "-001"
        End Try
        cn.Close()

    End Sub
    Function GenKey(ByVal field As String, ByVal table As String) As String
        'Genarate Id
        Dim sqlTmp As String = ""
        Dim comTmp As OleDbCommand = New OleDbCommand
        Dim drTmp As OleDbDataReader
        Dim tmpMemberID As Integer = 0
        Dim tmpMemberID2 As String = ""

        sqlTmp = "SELECT TOP 1 " & field & " FROM " & table & " "
        sqlTmp &= " WHERE case_id ='" & lblId.Text & "'"
        sqlTmp &= " ORDER BY " & field & " DESC"

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

                tmpMemberID2 = drTmp.Item("" & field & "")

                tmpMemberID = CInt(tmpMemberID2) + 1
                Return tmpMemberID.ToString

            End With
        Catch
            Return "1"
        End Try
        cn.Close()

    End Function
    Private Sub ClearAlert()
        lblARecieve.Text = ""
        lblARecieveDate.Text = ""
        lblAProsecutor.Text = ""
        lblADefedant.Text = ""
        lblAKeyword.Text = ""
    End Sub
    Protected Sub bSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bSave.Click
        Dim X As String = Request.QueryString("id")
        Dim sEmpNo As String = Session("EmpNo")

        If rdbcloserecieve.SelectedValue <> "0" And rdbcloserecieve.SelectedValue <> "1" And rdbcloserecieve.SelectedValue <> "2" Then
            Me.ClearAlert()
            lblARecieve.Text = "กรุณาเลือกปิดหมาย/รับหมาย/ส่งฟ้อง"
            Exit Sub
        End If
        If txtReceiveDate.Text.Year = 1 Then
            Me.ClearAlert()
            lblARecieveDate.Text = "กรุณาเลือกวันที่"
            Exit Sub
        End If
        If txtDefendant.Text.Trim = "" Then
            Me.ClearAlert()
            lblADefedant.Text = "กรุณากรอกชื่อโจทก์"
            txtDefendant.Focus()
            Exit Sub
        End If

        If txtProsecutor.Text.Trim = "" Then
            Me.ClearAlert()
            lblAProsecutor.Text = "กรุณากรอกชื่อจำเลย"
            txtProsecutor.Focus()
            Exit Sub
        End If


        If txtKeyword.Text.Trim = "" Then
            Me.ClearAlert()
            lblAKeyword.Text = "กรุณากรอกคำค้นหา"
            txtKeyword.Focus()
            Exit Sub
        End If
        If FCKeditor1.Value = "" Then
            MC.MessageBox(Me, "กรุณากรอกรายละเอียด")
            Exit Sub
        End If

        If lblMainStatus.Text = "Add" Then
            Me.Auto()
            Me.GenCaseNo()

            Dim str As String
            Dim oDs As DataSet
            str = "select * from case_process where type_id='" & ddlCaseType.SelectedValue & "' "
            oDs = MD.GetDataset(str)
            If oDs.Tables(0).Rows.Count > 0 Then
                Me.SaveData(oDs.Tables(0).Rows(0).Item("step1").ToString)
            End If
        Else
            Dim str As String
            Dim oDs As DataSet
            str = "select * from case_process where type_id='" & ddlCaseType.SelectedValue & "' "
            oDs = MD.GetDataset(str)
            If oDs.Tables(0).Rows.Count > 0 Then
                Me.EditData(oDs.Tables(0).Rows(0).Item("step1").ToString)
            End If
        End If

    End Sub
    Private Sub SaveData(ByVal status As String)
        Dim sEmpNo As String = Session("EmpNo")
        Dim strsql As String

        Try

            strsql = "insert into case_data (case_id,case_no,type_id,status_id,court_id,attorney_id,red_no,black_no, "
            strsql &= " defendant,defendant1,prosecutor,prosecutor1,detail,keyword,recieve_type,recieve_date, "
            strsql &= " app1,app2,creation_by,created_date,updated_by,updated_date,ref_bookin,ref_case,court_name) "
            strsql &= " values  "
            strsql &= " (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)"


            cn = New OleDbConnection(MD.Strcon)
            cmd = New OleDbCommand(strsql, cn)
            MD.CreateParam(cmd, "TTTTTTTTTTTTTTTDTTTDTDTTT")

            cmd.Parameters("@P1").Value = lblId.Text
            cmd.Parameters("@P2").Value = lblCaseNo.Text
            cmd.Parameters("@P3").Value = ddlCaseType.SelectedValue
            cmd.Parameters("@P4").Value = status
            cmd.Parameters("@P5").Value = ddlCourt.SelectedValue
            cmd.Parameters("@P6").Value = ddlAttorney.SelectedValue

            If (txtRedNo.Text.Trim = "") Then
                cmd.Parameters("@P7").Value = DBNull.Value
            Else
                cmd.Parameters("@P7").Value = txtRedNo.Text
            End If

            If (txtBlackNo.Text.Trim = "") Then
                cmd.Parameters("@P8").Value = DBNull.Value
            Else
                cmd.Parameters("@P8").Value = txtBlackNo.Text
            End If

            cmd.Parameters("@P9").Value = txtDefendant.Text
            cmd.Parameters("@P10").Value = txtDefendant1.Text
            cmd.Parameters("@P11").Value = txtProsecutor.Text
            cmd.Parameters("@P12").Value = txtProsecutor1.Text
            cmd.Parameters("@P13").Value = FCKeditor1.Value
            cmd.Parameters("@P14").Value = txtKeyword.Text
            cmd.Parameters("@P15").Value = rdbcloserecieve.SelectedValue

            If (txtReceiveDate.Text.Year = 1) Then
                cmd.Parameters("@P16").Value = DBNull.Value
            Else
                cmd.Parameters("@P16").Value = DateTime.Parse(txtReceiveDate.Text)
            End If

            cmd.Parameters("@P17").Value = ddlApp1.SelectedValue
            cmd.Parameters("@P18").Value = ddlApp2.SelectedValue
            cmd.Parameters("@P19").Value = sEmpNo
            cmd.Parameters("@P20").Value = DateTime.Parse(Date.Now)
            cmd.Parameters("@P21").Value = sEmpNo
            cmd.Parameters("@P22").Value = DateTime.Parse(Date.Now)
            cmd.Parameters("@P23").Value = lblTitle.Text
            cmd.Parameters("@P24").Value = lblIdCase.Text
            cmd.Parameters("@P25").Value = txtCourtName.Text

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

            strsql = "update case_data set type_id=?,status_id=?,court_id=?,attorney_id=?,red_no=?,black_no=?, "
            strsql &= " defendant=?,defendant1=?,prosecutor=?,prosecutor1=?,detail=?,keyword=?,recieve_type=?,recieve_date=?, "
            strsql &= " app1=?,app2=?,creation_by=?,created_date=?,updated_by=?,updated_date=?,ref_bookin=?,ref_case=?,court_name=?  "
            strsql &= " where case_id=? "

            cn = New OleDbConnection(MD.Strcon)
            cmd = New OleDbCommand(strsql, cn)
            MD.CreateParam(cmd, "TTTTTTTTTTTTTDTTTDTDTTTT")


            cmd.Parameters("@P1").Value = ddlCaseType.SelectedValue
            cmd.Parameters("@P2").Value = status
            cmd.Parameters("@P3").Value = ddlCourt.SelectedValue
            cmd.Parameters("@P4").Value = ddlAttorney.SelectedValue

            If (txtRedNo.Text.Trim = "") Then
                cmd.Parameters("@P5").Value = DBNull.Value
            Else
                cmd.Parameters("@P5").Value = txtRedNo.Text
            End If

            If (txtBlackNo.Text.Trim = "") Then
                cmd.Parameters("@P6").Value = DBNull.Value
            Else
                cmd.Parameters("@P6").Value = txtBlackNo.Text
            End If

            cmd.Parameters("@P7").Value = txtDefendant.Text
            cmd.Parameters("@P8").Value = txtDefendant1.Text
            cmd.Parameters("@P9").Value = txtProsecutor.Text
            cmd.Parameters("@P10").Value = txtProsecutor1.Text
            cmd.Parameters("@P11").Value = FCKeditor1.Value
            cmd.Parameters("@P12").Value = txtKeyword.Text
            cmd.Parameters("@P13").Value = rdbcloserecieve.SelectedValue

            If (txtReceiveDate.Text.Year = 1) Then
                cmd.Parameters("@P14").Value = DBNull.Value
            Else
                cmd.Parameters("@P14").Value = DateTime.Parse(txtReceiveDate.Text)
            End If

            cmd.Parameters("@P15").Value = ddlApp1.SelectedValue
            cmd.Parameters("@P16").Value = ddlApp2.SelectedValue
            cmd.Parameters("@P17").Value = sEmpNo
            cmd.Parameters("@P18").Value = DateTime.Parse(Date.Now)
            cmd.Parameters("@P19").Value = sEmpNo
            cmd.Parameters("@P20").Value = DateTime.Parse(Date.Now)
            cmd.Parameters("@P21").Value = lblTitle.Text
            cmd.Parameters("@P22").Value = lblIdCase.Text
            cmd.Parameters("@P23").Value = txtCourtName.Text
            cmd.Parameters("@P24").Value = lblId.Text

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
    Protected Sub bSaveFile_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bSaveFile.Click
        Dim X As String = Request.QueryString("id")
        Dim sEmpNo As String = Session("EmpNo")
        Dim strPath As String = "Document\Case\"

        If lblId.Text = "" Then
            Me.bSave_Click(sender, e)
        End If

        If lblCaseNo.Text = "" Then
            MC.MessageBox(Me, "กรุณาบันทึกข้อมูลหลักก่อน")
            Exit Sub
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
            Strsql = "update case_document set title='" & txtDocDetail.Text & "',page='" & txtDocPage.Text & "' "
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
            Strsql &= " where case_id='" & lblId.Text & "' and document_id ='" & lblDocId.Text & "'"

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
                Strsql = "insert into case_document (case_id,case_no,document_id "

                If FileUpload1.HasFile Then
                    Strsql &= ",file_path,mime_type "
                End If


                Strsql &= ",title,page,creation_by,created_date,updated_by,updated_date)values  "
                Strsql &= "('" & lblId.Text & "','" & lblCaseNo.Text & "','" & lblDocId.Text & "' "

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
                        X = Server.MapPath("..\Document\Case\" & X)
                        FileUpload1.PostedFile.SaveAs(X)
                    Case ".csv"
                        MIMEType = ".csv"
                        Dim X As String = Path.GetFileName("" & fname & "" & MIMEType & "")
                        X = Server.MapPath("..\Document\Case\" & X)
                        FileUpload1.PostedFile.SaveAs(X)
                    Case ".xls"
                        MIMEType = ".xls"
                        Dim X As String = Path.GetFileName("" & fname & "" & MIMEType & "")
                        X = Server.MapPath("..\Document\Case\" & X)
                        FileUpload1.PostedFile.SaveAs(X)
                    Case ".xlsx"
                        MIMEType = ".xlsx"
                        Dim X As String = Path.GetFileName("" & fname & "" & MIMEType & "")
                        X = Server.MapPath("..\Document\Case\" & X)
                        FileUpload1.PostedFile.SaveAs(X)
                    Case ".pdf"
                        MIMEType = ".pdf"
                        Dim X As String = Path.GetFileName("" & fname & "" & MIMEType & "")
                        X = Server.MapPath("..\Document\Case\" & X)
                        FileUpload1.PostedFile.SaveAs(X)
                    Case ".doc"
                        MIMEType = ".doc"
                        Dim X As String = Path.GetFileName("" & fname & "" & MIMEType & "")
                        X = Server.MapPath("..\Document\Case\" & X)
                        FileUpload1.PostedFile.SaveAs(X)
                    Case ".docx"
                        MIMEType = ".docx"
                        Dim X As String = Path.GetFileName("" & fname & "" & MIMEType & "")
                        X = Server.MapPath("..\Document\Case\" & X)
                        FileUpload1.PostedFile.SaveAs(X)
                    Case ".txt"
                        MIMEType = ".txt"
                        Dim X As String = Path.GetFileName("" & fname & "" & MIMEType & "")
                        X = Server.MapPath("..\Document\Case\" & X)
                        FileUpload1.PostedFile.SaveAs(X)
                    Case ".htm", ".html"
                        MIMEType = ".html"
                        Dim X As String = Path.GetFileName("" & fname & "" & MIMEType & "")
                        X = Server.MapPath("..\Document\Case\" & X)
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
    Protected Sub GridView1_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GridView1.RowDeleting
        'Delete Document
        Dim strPath As String = Constant.BaseURL(Request) & ("Document\Case\")
        Dim K1 As DataKey = GridView1.DataKeys(e.RowIndex)
        Dim strsql As String
        Dim chk As String

        chk = "select * from case_document where case_id='" & lblId.Text & "' and document_id ='" & K1(0) & "'"
        Dim DS As DataSet
        Dim mtype As String = ""
        Dim fname As String = ""
        DS = MD.GetDataset(chk)

        If DS.Tables(0).Rows.Count > 0 Then
            mtype = DS.Tables(0).Rows(0).Item("mime_type").ToString
            fname = DS.Tables(0).Rows(0).Item("case_id").ToString + "-" + DS.Tables(0).Rows(0).Item("document_id").ToString
            fname = Func.getServerPath() & DS.Tables(0).Rows(0).Item("file_path").ToString

        End If

        strsql = "delete from case_document where case_id='" & lblId.Text & "' and document_id ='" & K1(0) & "'"
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
            strsql2 &= "where case_id='" & lblId.Text & "' and d.document_id='" & K2(0) & "'"


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
    Protected Sub GridView1_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles GridView1.RowEditing
        'Edit Document
        Dim K1 As DataKey = GridView1.DataKeys(e.NewEditIndex)
        Dim lName As Label = GridView1.Rows(e.NewEditIndex).Cells(0).Controls(0)
        Dim lPage As Label = GridView1.Rows(e.NewEditIndex).Cells(1).Controls(0)

        lblDocId.Text = K1(0).ToString
        txtDocDetail.Text = lName.Text
        txtDocPage.Text = lPage.Text

        lblDocStatus.Text = "Edit"
    End Sub
    Protected Sub bCancelFile_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bCancelFile.Click
        txtDocDetail.Text = ""
        txtDocPage.Text = ""
        lblDocStatus.Text = ""
        lblAFile.Text = ""
        lblADetail1.Text = ""
        lblAPage.Text = ""
    End Sub
    Protected Sub bSaveCourt_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bSaveCourt.Click
        Dim X As String = Request.QueryString("id")
        Dim sEmpNo As String = Session("EmpNo")
        Dim DCourt As String = MC.Date2DB(txtDate1.Text)
        Dim alert As String

        If chkAlert1.Checked = True Then
            alert = 1
        Else
            alert = 0
        End If

        If lblId.Text = "" Then
            Me.bSave_Click(sender, e)
        End If

        If txtCourtDetail.Text.Trim = "" Then
            lblADetail.Text = "กรุณากรอกรายละเอียดข้อมูลศาลนัด"
            lblADate1.Text = ""
            txtCourtDetail.Focus()
            Exit Sub
        End If

        If txtDate1.Text.Trim = "" Then
            lblADate1.Text = "กรุณาเลือกวันที่"
            lblADetail.Text = ""
            Exit Sub
        End If

        If lblCourtStatus.Text = "Edit" Then
            Dim Strsql As String
            Strsql = "update court_date set title='" & txtCourtDetail.Text & "',dates='" & DCourt & "',alert='" & alert & "', "
            Strsql &= "updated_by='" & sEmpNo & "',updated_date=getdate() "
            Strsql &= "where case_id='" & lblId.Text & "' and court_date_id='" & lblCourtDate.Text & "'"


            Dim Y As Integer = MD.Execute(Strsql)
            If Y > 0 Then

                Me.gDataCourt()
                Me.MyGridBindCourt()
                MC.MessageBox(Me, "บันทึกลงฐานข้อมูลเรียบร้อยแล้ว")

                lblCourtDate.Text = ""
                lblCourtStatus.Text = ""

                txtCourtDetail.Text = ""
                txtDate1.Text = ""
                chkAlert1.Checked = False

                lblADate1.Text = ""
                lblADetail.Text = ""

            Else
                MC.MessageBox(Me, "ไม่สามารถบันทึกลงฐานข้อมูลได้")
            End If

        Else

            Try
                Dim key As String = Me.GenKey("court_date_id", "court_date")
                Dim Strsql As String

                Strsql = "insert into court_date (case_id,case_no,court_date_id "
                Strsql &= ",title,dates,alert,creation_by,created_date,updated_by,updated_date)values  "
                Strsql &= "('" & lblId.Text & "','" & lblCaseNo.Text & "','" & key & "','" & txtCourtDetail.Text & "','" & DCourt & "','" & alert & "','" & sEmpNo & "', "
                Strsql &= "getdate(),'" & sEmpNo & "',getdate()) "

                Dim Y As Integer = MD.Execute(Strsql)
                If Y > 0 Then



                    lblCourtDate.Text = ""
                    lblCourtStatus.Text = ""

                    txtCourtDetail.Text = ""
                    txtDate1.Text = ""
                    chkAlert1.Checked = False

                    lblADate1.Text = ""
                    lblADetail.Text = ""

                    Me.gDataCourt()
                    Me.MyGridBindCourt()

                    MC.MessageBox(Me, "บันทึกลงฐานข้อมูลเรียบร้อยแล้ว")
                Else
                    MC.MessageBox(Me, "ไม่สามารถบันทึกลงฐานข้อมูลได้")
                End If
            Catch ex As Exception
                MC.MessageBox(Me, ex.ToString)
            End Try
        End If
    End Sub
    Protected Sub bCourtCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bCourtCancel.Click
        txtCourtDetail.Text = ""
        txtDate1.Text = ""
        chkAlert1.Checked = False

        lblADate1.Text = ""
        lblADetail.Text = ""
    End Sub
    Protected Sub GridView2_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridView2.PageIndexChanging
        GridView2.PageIndex = e.NewPageIndex
        Me.MyGridBindCourt()
    End Sub
    Protected Sub GridView2_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridView2.RowCommand

    End Sub
    Protected Sub GridView2_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView2.RowCreated
        If e.Row.RowType = DataControlRowType.DataRow Then
            iRow = e.Row.DataItemIndex + 1
            If Not e.Row.RowState And DataControlRowState.Edit Then
                Dim Llert As LinkButton = e.Row.Cells(3).Controls(0)
                Llert.OnClientClick = "return confirm('คุณต้องการแก้ไขการเตือนใช่หรือไม่?');"

                Dim L1 As ImageButton = e.Row.Cells(4).Controls(0)
                L1.OnClientClick = "return confirm('คุณต้องการลบข้อมูลใช่หรือไม่?');"
            End If
        End If
    End Sub
    Protected Sub GridView2_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView2.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            If DataBinder.Eval(e.Row.DataItem, "alert").ToString = "เปิด" Then
                e.Row.Cells(3).ForeColor = Drawing.Color.Blue
            Else
                e.Row.Cells(3).ForeColor = Drawing.Color.Red
            End If
        End If
    End Sub
    Protected Sub GridView2_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GridView2.RowDeleting
        'Delete Court Date
        Dim K1 As DataKey = GridView2.DataKeys(e.RowIndex)
        Dim strsql As String

        strsql = "delete from court_date where case_id='" & lblId.Text & "' and court_date_id ='" & K1(0) & "'"

        Dim Y As Integer = MD.Execute(strsql)
        If Y > 0 Then
            Me.gDataCourt()
            Me.MyGridBindCourt()
            MC.MessageBox(Me, "ลบข้อมูลเรียบร้อยแล้ว")
        Else
            MC.MessageBox(Me, "ไม่สามารถลบข้อมูลได้")
        End If
    End Sub
    Protected Sub GridView2_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles GridView2.RowEditing
        'Edit Court Date
        Dim K1 As DataKey = GridView2.DataKeys(e.NewEditIndex)
        Dim lCourtDetail As Label = GridView2.Rows(e.NewEditIndex).Cells(0).Controls(0)
        Dim lCourtDate As Label = GridView2.Rows(e.NewEditIndex).Cells(1).Controls(0)
        Dim chk As Label = GridView2.Rows(e.NewEditIndex).Cells(2).Controls(0)

        lblCourtDate.Text = K1(0).ToString
        txtCourtDetail.Text = lCourtDetail.Text
        txtDate1.Text = lCourtDate.Text

        If chk.Text = "1" Then
            chkAlert1.Checked = True
        Else
            chkAlert1.Checked = False
        End If

        lblCourtStatus.Text = "Edit"
    End Sub
    Protected Sub bExplainSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bExplainSave.Click
        Dim X As String = Request.QueryString("id")
        Dim sEmpNo As String = Session("EmpNo")
        Dim DExplain As String = MC.Date2DB(txtDate2.Text)
        Dim alert As String

        If chkAlert2.Checked = True Then
            alert = 1
        Else
            alert = 0
        End If

        If lblId.Text = "" Then
            Me.bSave_Click(sender, e)
        End If

        If txtExplainDetail.Text.Trim = "" Then
            lblAExplain.Text = "กรุณากรอกรายละเอียดข้อมูลขยายเวลายื่นคำชี้แจง"
            lblADate2.Text = ""
            txtExplainDetail.Focus()
            Exit Sub
        End If

        If txtDate2.Text.Trim = "" Then
            lblADate2.Text = "กรุณาเลือกวันที่"
            lblAExplain.Text = ""
            Exit Sub
        End If

        If lblExplainStatus.Text = "Edit" Then
            Dim Strsql As String
            Strsql = "update explain_date set title='" & txtExplainDetail.Text & "',dates='" & DExplain & "',alert='" & alert & "', "
            Strsql &= "updated_by='" & sEmpNo & "',updated_date=getdate() "
            Strsql &= "where case_id='" & lblId.Text & "' and explain_date_id='" & lblExplainDate.Text & "'"


            Dim Y As Integer = MD.Execute(Strsql)
            If Y > 0 Then

                Me.gDataExplain()
                Me.MyGridBindExplain()
                MC.MessageBox(Me, "บันทึกลงฐานข้อมูลเรียบร้อยแล้ว")

                lblExplainDate.Text = ""
                lblExplainStatus.Text = ""
                txtExplainDetail.Text = ""
                txtDate2.Text = ""
                chkAlert2.Checked = False

                lblAExplain.Text = ""
                lblADate2.Text = ""

            Else
                MC.MessageBox(Me, "ไม่สามารถบันทึกลงฐานข้อมูลได้")
            End If

        Else

            Try
                Dim key As String = Me.GenKey("explain_date_id", "explain_date")
                Dim Strsql As String

                Strsql = "insert into explain_date (case_id,case_no,explain_date_id "
                Strsql &= ",title,dates,alert,creation_by,created_date,updated_by,updated_date)values  "
                Strsql &= "('" & lblId.Text & "','" & lblCaseNo.Text & "','" & key & "','" & txtExplainDetail.Text & "','" & DExplain & "','" & alert & "','" & sEmpNo & "', "
                Strsql &= "getdate(),'" & sEmpNo & "',getdate()) "


                Dim Y As Integer = MD.Execute(Strsql)
                If Y > 0 Then

                    Me.gDataExplain()
                    Me.MyGridBindExplain()
                    MC.MessageBox(Me, "บันทึกลงฐานข้อมูลเรียบร้อยแล้ว")

                    lblExplainStatus.Text = ""
                    lblExplainDate.Text = ""
                    txtExplainDetail.Text = ""
                    txtDate2.Text = ""
                    chkAlert2.Checked = False

                    lblAExplain.Text = ""
                    lblADate2.Text = ""
                Else
                    MC.MessageBox(Me, "ไม่สามารถบันทึกลงฐานข้อมูลได้")
                End If
            Catch ex As Exception
                MC.MessageBox(Me, ex.ToString)
            End Try
        End If
    End Sub
    Protected Sub GridView3_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridView3.PageIndexChanging
        GridView3.PageIndex = e.NewPageIndex
        Me.MyGridBindExplain()
    End Sub
    Protected Sub GridView3_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView3.RowCreated
        If e.Row.RowType = DataControlRowType.DataRow Then
            iRow = e.Row.DataItemIndex + 1
            If Not e.Row.RowState And DataControlRowState.Edit Then

                Dim Llert As ImageButton = e.Row.Cells(5).Controls(1)
                Llert.OnClientClick = "return confirm('คุณต้องการแก้ไขการเตือนใช่หรือไม่?');"

                Dim L1 As ImageButton = e.Row.Cells(4).Controls(1)
                L1.OnClientClick = "return confirm('คุณต้องการลบข้อมูลใช่หรือไม่?');"
            End If
        End If
    End Sub
    Protected Sub GridView3_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView3.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            If DataBinder.Eval(e.Row.DataItem, "alert").ToString = "เปิด" Then
                e.Row.Cells(3).ForeColor = Drawing.Color.Blue
            Else
                e.Row.Cells(3).ForeColor = Drawing.Color.Red
            End If
        End If
    End Sub
    Protected Sub GridView3_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GridView3.RowDeleting
        Dim K1 As DataKey = GridView3.DataKeys(e.RowIndex)
        Dim strsql As String

        strsql = "delete from explain_date where case_id='" & lblId.Text & "' and explain_date_id ='" & K1(0) & "'"

        Dim Y As Integer = MD.Execute(strsql)
        If Y > 0 Then
            Me.gDataExplain()
            Me.MyGridBindExplain()
            MC.MessageBox(Me, "ลบข้อมูลเรียบร้อยแล้ว")
        Else
            MC.MessageBox(Me, "ไม่สามารถลบข้อมูลได้")
        End If
    End Sub
    Protected Sub GridView3_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles GridView3.RowEditing
        'Edit Court Date
        Dim K1 As DataKey = GridView3.DataKeys(e.NewEditIndex)
        Dim lExplainDetail As Label = GridView3.Rows(e.NewEditIndex).Cells(0).Controls(1)
        Dim lExplainDate As Label = GridView3.Rows(e.NewEditIndex).Cells(1).Controls(1)
        Dim chk As Label = GridView3.Rows(e.NewEditIndex).Cells(2).Controls(1)

        lblExplainDate.Text = K1(0).ToString
        txtExplainDetail.Text = lExplainDetail.Text
        txtDate2.Text = lExplainDate.Text

        If chk.Text = "1" Then
            chkAlert2.Checked = True
        Else
            chkAlert2.Checked = False
        End If

        lblExplainStatus.Text = "Edit"

    End Sub
    Protected Sub bCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bCancel.Click
        Dim X As String = Request.QueryString("id")
        If X <> "" Then
            Me.MyDataBind()
        Else
            txtBlackNo.Text = ""
            txtRedNo.Text = ""
            txtReceiveDate.Text = "0:00:00"
            txtProsecutor.Text = ""
            txtProsecutor1.Text = ""
            txtDefendant.Text = ""
            txtDefendant1.Text = ""
            txtKeyword.Text = ""
        End If
        Me.ClearAlert()

    End Sub
    Protected Sub ddlCaseType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCaseType.SelectedIndexChanged
        Me.DataCaseStatus()
    End Sub
    Private Sub UpdateActive()

        Dim sEmpNo As String = Session("EmpNo")

        Try
            Dim Strsql As String
            Strsql = "update case_data set  "
            Strsql &= " active = 0 "
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
    Protected Sub bSaveAndSend_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bSaveAndSend.Click
        Dim X As String = Request.QueryString("id")
        Dim sEmpNo As String = Session("EmpNo")

        If rdbcloserecieve.SelectedValue <> "0" And rdbcloserecieve.SelectedValue <> "1" And rdbcloserecieve.SelectedValue <> "2" Then
            Me.ClearAlert()
            lblARecieve.Text = "กรุณาเลือกปิดหมาย/รับหมาย/ส่งฟ้อง"
            Exit Sub
        End If
        If txtReceiveDate.Text.Year = 1 Then
            Me.ClearAlert()
            lblARecieveDate.Text = "กรุณาเลือกวันที่"
            Exit Sub
        End If

        If txtDefendant.Text.Trim = "" Then
            Me.ClearAlert()
            lblADefedant.Text = "กรุณากรอกชื่อโจทก์"
            txtDefendant.Focus()
            Exit Sub
        End If

        If txtProsecutor.Text.Trim = "" Then
            Me.ClearAlert()
            lblAProsecutor.Text = "กรุณากรอกชื่อจำเลย"
            txtProsecutor.Focus()
            Exit Sub
        End If


        If txtKeyword.Text.Trim = "" Then
            Me.ClearAlert()
            lblAKeyword.Text = "กรุณากรอกคำค้นหา"
            txtKeyword.Focus()
            Exit Sub
        End If
        If FCKeditor1.Value = "" Then
            MC.MessageBox(Me, "กรุณากรอกรายละเอียด")
            Exit Sub
        End If

        Me.UpdateActive()
        Me.Auto()

        If lblCaseNo.Text = "" Then
            Me.GenCaseNo()
        End If

        'Select Case ddlCaseType.SelectedValue
        '    Case 1
        '        Me.SaveData(2)
        '    Case 2
        '        Me.SaveData(12)
        '    Case 3
        '        Me.SaveData(22)
        'End Select

        Dim str As String
        Dim oDs As DataSet
        str = "select * from case_process where type_id='" & ddlCaseType.SelectedValue & "' "
        oDs = MD.GetDataset(str)
        If oDs.Tables(0).Rows.Count > 0 Then
            Me.SaveData(oDs.Tables(0).Rows(0).Item("step2").ToString)
        End If


        Me.ClearData()
        lblMainStatus.Text = "Add"

        'Show Gridview Document
        Me.gDataDoc()
        Me.MyGridBind()
        'Show Gridview CourtDate
        Me.gDataCourt()
        Me.MyGridBindCourt()
        'Show Gridveiw ExplainDate
        Me.gDataExplain()
        Me.MyGridBindExplain()

        Response.Redirect("../Src/CaseDataList.aspx", True)

    End Sub
    Private Sub ClearData()
        txtBlackNo.Text = ""
        txtRedNo.Text = ""
        txtReceiveDate.Text = "0:00:00"
        txtDefendant.Text = ""
        txtDefendant1.Text = ""
        txtProsecutor.Text = ""
        txtProsecutor1.Text = ""
        txtKeyword.Text = ""
        FCKeditor1.Value = ""
        txtDocDetail.Text = ""
        txtDocPage.Text = "'"

        'Show Gridview Document
        Me.gDataDoc()
        Me.MyGridBind()
        'Show Gridview CourtDate
        Me.gDataCourt()
        Me.MyGridBindCourt()
        'Show Gridveiw ExplainDate
        Me.gDataExplain()
        Me.MyGridBindExplain()
    End Sub
    Protected Sub bExplainCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bExplainCancel.Click
        txtExplainDetail.Text = ""
        txtDate2.Text = ""
        chkAlert2.Checked = False

        lblAExplain.Text = ""
        lblADate2.Text = ""
    End Sub
    Protected Sub GridView2_SelectedIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSelectEventArgs) Handles GridView2.SelectedIndexChanging
        'Update Court Date
        Dim K1 As DataKey = GridView2.DataKeys(e.NewSelectedIndex)
        Dim strsql As String
        Dim chk As String
        Dim oDs As DataSet
        Dim txtActive As String = ""
        Dim txtAlert As String = ""

        chk = "select alert from court_date where case_id='" & lblId.Text & "' and court_date_id ='" & K1(0) & "' "

        oDs = MD.GetDataset(chk)
        If oDs.Tables(0).Rows.Count > 0 Then
            If oDs.Tables(0).Rows(0).Item("alert").ToString = 0 Then
                txtActive = 1
                txtAlert = "ตั้งเตือน"
            Else
                txtActive = 0
                txtAlert = "ปิดการตั้งเตือน"
            End If
        End If

        strsql = "update court_date set alert='" & txtActive & "' where case_id='" & lblId.Text & "' and court_date_id ='" & K1(0) & "'"

        Dim Y As Integer = MD.Execute(strsql)
        If Y > 0 Then
            Me.gDataCourt()
            Me.MyGridBindCourt()
            MC.MessageBox(Me, txtAlert & "เรียบร้อยแล้ว")
        Else
            MC.MessageBox(Me, "ไม่สามารถ" & txtAlert & "ได้")
        End If
    End Sub
    Protected Sub GridView3_SelectedIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSelectEventArgs) Handles GridView3.SelectedIndexChanging
        'Update Explain Date
        Dim K1 As DataKey = GridView2.DataKeys(e.NewSelectedIndex)
        Dim strsql As String
        Dim chk As String
        Dim oDs As DataSet
        Dim txtActive As String = ""
        Dim txtAlert As String = ""

        chk = "select alert from explain_date where case_id='" & lblId.Text & "' and explain_date_id ='" & K1(0) & "' "

        oDs = MD.GetDataset(chk)
        If oDs.Tables(0).Rows.Count > 0 Then
            If oDs.Tables(0).Rows(0).Item("alert").ToString = 0 Then
                txtActive = 1
                txtAlert = "ตั้งเตือน"
            Else
                txtActive = 0
                txtAlert = "ปิดการตั้งเตือน"
            End If
        End If

        strsql = "update explain_date set alert='" & txtActive & "' where case_id='" & lblId.Text & "' and explain_date_id ='" & K1(0) & "'"

        Dim Y As Integer = MD.Execute(strsql)
        If Y > 0 Then
            Me.gDataExplain()
            Me.MyGridBindExplain()
            MC.MessageBox(Me, txtAlert & "เรียบร้อยแล้ว")
        Else
            MC.MessageBox(Me, "ไม่สามารถ" & txtAlert & "ได้")
        End If

    End Sub
    Protected Sub bSaveFCK_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bSaveFCK.Click
        Dim sEmpNo As String = Session("EmpNo")
        Dim strsql As String

        Call bSave_Click(sender, e)

        If lblId.Text.Trim = "" Then
            MC.MessageBox(Me, "กรุณาบันทึกข้อมูลพื้นฐานก่อน")
            Exit Sub
        End If

        Try

            strsql = "update case_data set detail=?, "
            strsql &= " updated_by=?,updated_date=? "
            strsql &= " where case_id=? "

            cn = New OleDbConnection(MD.Strcon)
            cmd = New OleDbCommand(strsql, cn)
            MD.CreateParam(cmd, "TTDT")


            cmd.Parameters("@P1").Value = FCKeditor1.Value
            cmd.Parameters("@P2").Value = sEmpNo
            cmd.Parameters("@P3").Value = DateTime.Parse(Date.Now)
            cmd.Parameters("@P4").Value = lblId.Text

            cn.Open()
            cmd.ExecuteNonQuery()
            'MC.MessageBox(Me, "บันทึกลงฐานข้อมูลเรียบร้อยแล้ว")


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
    Protected Sub linkHome_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles linkHome.Click
        Response.Redirect(MD.pHome, True)
    End Sub
    Protected Sub link2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles link2.Click
        Dim X As String = Request.QueryString("id")
        If X <> "" Then
            Response.Redirect("../Src/CaseEditList.aspx", True)
        End If
    End Sub
    Protected Sub bDelTitle_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bDelTitle.Click
        txtTitle.Text = ""
        lblTitle.Text = ""
    End Sub
    Protected Sub bDelCase_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bDelCase.Click
        txtNameCase.Text = ""
        lblIdCase.Text = ""
    End Sub
End Class
