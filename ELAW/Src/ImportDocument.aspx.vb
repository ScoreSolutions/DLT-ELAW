Imports System.IO
Imports System.Web
Imports System.Web.UI
Imports System.Data
Imports System.Data.OleDb
Partial Class Src_ImportDocument
    Inherits System.Web.UI.Page
    Dim MD As New MainData
    Dim MC As New MainClass
    Dim DV As DataView
    Dim oDate As New clsDate
    Public iRow As Integer
    Dim DS As DataSet
    Dim iRec As Integer
    Dim DVLst As DataView
    Dim DVListBox As DataView
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
    Private Sub Name()
        Dim sEmpNo As String = Session("EmpNo")
        Dim strsql As String
        Dim oDs As New DataSet

        strsql = "select firstname+' '+lastname as fullname from employee where empid='" & sEmpNo & "'"


        oDs = MD.GetDataset(strsql)
        If oDs.Tables(0).Rows.Count > 0 Then
            lblIdName1.Text = sEmpNo
            txtName1.Text = oDs.Tables(0).Rows(0).Item("fullname").ToString

            Session("TextName1") = txtName1.Text
            Session("TextId1") = lblIdName1.Text
        End If
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim X As String = Request.QueryString("id")
        Me.SetJava()
        'Me.ChkPermis()
        Me.Name()


        txtReceiveDate.Text = Date.Today


        If Not Page.IsPostBack Then
            ViewState("sortfield") = "doc_id"
            ViewState("sortdirection") = "desc"

            If DDType.SelectedValue = 1 Then
                Me.LawType()
                DDLawType.Enabled = True
            ElseIf DDType.SelectedValue = 2 Then
                Me.CaseType()
                DDLawType.Enabled = True
            ElseIf DDType.SelectedValue = 3 Then
                Me.ContractType()
                DDLawType.Enabled = True
            Else
                DDLawType.SelectedItem.Text = ""
                DDLawType.Enabled = False
            End If

            Me.DataLawType()

            If X <> "" Then

                Dim sql As String

                sql = "select d.doc_id,d.doc_type,d.doc_subtype,d.doc_name,d.dates_recieve,d.secret, "
                sql &= " d.doc_page,d.name_imp,e.firstname+' '+e.lastname name1 "
                sql &= " from import_document d inner join employee e "
                sql &= " on d.name_imp=e.empid "
                sql &= " where d.doc_id='" & X & "'"

                DS = MD.GetDataset(sql)
                Session("imp_doc") = DS
                iRec = 0
                ViewState("iRec") = iRec

                Me.MyDataBind()
                Me.FindRow()

                txtDocId.Text = X
                lblStatus.Text = "Edit"

            Else

                Dim sql As String

                sql = "select * from import_document "

                DS = MD.GetDataset(sql)
                Session("imp_doc") = DS
                iRec = 0
                ViewState("iRec") = iRec

                lblStatus.Text = "Add"

            End If

            Me.gData()
            Me.MyGridBind()

        Else
            Me.RefreshPage()

            DS = Session("imp_doc")
            iRec = ViewState("iRec")

            If Session("DocumentImport") Is Nothing Then
                Me.gData()
            Else
                DVLst = Session("DocumentImport")
            End If

            If Session("ListBox") Is Nothing Then
                Me.BindListView1()
            Else
                DVListBox = Session("ListBox")
            End If

        End If

        bDel.OnClientClick = "return confirm('คุณต้องการลบข้อมูลใช่หรือไม่?');"
        txtReceiveDate.Attributes.Add("onkeypress", "return blockText(this, event,-1,0);")

        Dim UpdateVal As String = ((("getlstvalue('" + ListBox2.ClientID & "', '") + ListBox2.ClientID & "_zLstSelect');getlstvalue('") + ListBox1.ClientID & "', '") + ListBox1.ClientID & "_zLstNoSelect');"
        ListBox1.Attributes.Add("OnDblClick", ("lstmove('" + ListBox1.ClientID & "','") + ListBox2.ClientID & "');" & UpdateVal & "return false;")
        ListBox2.Attributes.Add("OnDblClick", ("lstmove('" + ListBox2.ClientID & "','") + ListBox1.ClientID & "');" & UpdateVal & "return false;")

    End Sub
    Private Sub LawType()

        Dim strsql As String

        strsql = " select l.subtype_id,l.subtype_name  "
        strsql &= "from law_subtype l order by l.subtype_name "

        Dim DTS As DataTable = MD.GetDataTable(strsql)

        DDLawType.DataTextField = "subtype_name"
        DDLawType.DataValueField = "subtype_id"
        DDLawType.DataSource = DTS
        DDLawType.DataBind()

    End Sub
    Private Sub CaseType()

        Dim strsql As String

        strsql = " select c.type_id,c.type_name  "
        strsql &= "from case_type c order by c.type_name "

        Dim DTS As DataTable = MD.GetDataTable(strsql)

        DDLawType.DataTextField = "type_name"
        DDLawType.DataValueField = "type_id"
        DDLawType.DataSource = DTS
        DDLawType.DataBind()

    End Sub
    Public Sub DataLawType()

        Dim strsql As String

        strsql = " select s.subtype_id,s.subtype_name  "
        strsql &= "from  law_subtype s order by s.subtype_name "

        Dim DTS As DataTable = MD.GetDataTable(strsql)
        Dim dr As DataRow = DTS.NewRow
        dr!subtype_id = 0
        dr!subtype_name = "---------โปรดเลือก---------"
        DTS.Rows.InsertAt(dr, 0)
        ddlLawType.DataTextField = "subtype_name"
        ddlLawType.DataValueField = "subtype_id"
        ddlLawType.DataSource = DTS
        ddlLawType.DataBind()

    End Sub
    Private Sub ContractType()

        Dim strsql As String

        strsql = " select c.subtype_id,c.subtype_name  "
        strsql &= "from  contract_subtype c order by c.subtype_name "

        Dim DTS As DataTable = MD.GetDataTable(strsql)

        DDLawType.DataTextField = "subtype_name"
        DDLawType.DataValueField = "subtype_id"
        DDLawType.DataSource = DTS
        DDLawType.DataBind()

    End Sub
    Protected Sub DDType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DDType.SelectedIndexChanged
        If DDType.SelectedValue = 1 Then
            Me.LawType()
            DDLawType.Enabled = True
        ElseIf DDType.SelectedValue = 2 Then
            Me.CaseType()
            DDLawType.Enabled = True
        ElseIf DDType.SelectedValue = 3 Then
            Me.ContractType()
            DDLawType.Enabled = True
        Else
            DDLawType.SelectedItem.Text = ""
            DDLawType.Enabled = False
        End If
    End Sub
    Public Function BindField(ByVal FieldName As String) As String
        Dim DT As DataTable = DS.Tables(0)
        Select Case FieldName
            Case "cost"
                If IsDBNull(DT.Rows(iRec)("cost")) Then
                    Return "0.00"

                Else
                    Dim P1 As Double = DT.Rows(iRec)("cost")
                    Return P1.ToString("#,##0.00")
                End If
            Case "dates_receive"
                If IsDBNull(DT.Rows(iRec)("dates_receive")) Then
                    Return "-"

                Else
                    Dim P1 As Date = DT.Rows(iRec)("dates_receive")
                    Return P1.ToString("dd/MM/yyyy")
                End If

            Case Else
                Return DT.Rows(iRec)(FieldName) & ""
        End Select
    End Function
    Public Sub MyDataBind()
        txtDocId.DataBind()
        txtDocName.DataBind()
        txtReceiveDate.DataBind()
        lblIdName1.DataBind()
        txtName1.DataBind()
        txtDocPage.DataBind()

        Session("TextName1") = txtName1.Text
        Session("TextId1") = lblIdName1.Text

        If DS.Tables(0).Rows(0).Item("secret").ToString = "1" Then
            chkSecret.Checked = True
        Else
            chkSecret.Checked = False
        End If

    End Sub
    Private Sub FindRow()

        Dim X1 As String
        X1 = DS.Tables(0).Rows(iRec)("doc_type") & ""
        DDType.SelectedIndex = FindTypeRow(X1)

        Dim X2 As String
        X2 = DS.Tables(0).Rows(iRec)("doc_subtype") & ""
        DDLawType.SelectedIndex = FindSubTypeRow(X2)

    End Sub
    Public Function FindTypeRow(ByVal X As String) As Integer
        Dim i As Integer = 0
        For i = 0 To DDType.Items.Count - 1
            If X = DDType.Items(i).Value Then
                Return i
            End If
        Next

        Return 0
    End Function
    Public Function FindSubTypeRow(ByVal X As String) As Integer
        If DDType.SelectedValue = 1 Then
            Me.LawType()

        ElseIf DDType.SelectedValue = 2 Then
            Me.CaseType()

        ElseIf DDType.SelectedValue = 3 Then
            Me.ContractType()

        Else
            DDLawType.SelectedItem.Text = ""
            DDLawType.Enabled = False
        End If

        Dim i As Integer = 0
        For i = 0 To DDLawType.Items.Count - 1
            If X = DDLawType.Items(i).Value Then
                Return i
            End If
        Next

    End Function
    Private Sub SetJava()
        txtDocPage.Attributes.Add("onkeypress", "return blockNonNumbers(this, event,-1,0);")
        bSelect1.Attributes.Add("onclick", "popupwindown('SearchEmp.aspx?id=TextName1&name=TextId1');")
    End Sub
    Private Sub RefreshPage()
        txtName1.Text = Session("TextName1")
        lblIdName1.Text = Session("TextId1")
    End Sub
    Private Sub Auto()

        Dim sqlTmp As String = ""
        Dim comTmp As OleDbCommand = New OleDbCommand
        Dim drTmp As OleDbDataReader
        Dim tmpMemberID As Integer = 0
        Dim tmpMemberID2 As String = ""
        Dim txt As String

        If DDType.SelectedValue = 1 Then
            txt = "LW"
        ElseIf DDType.SelectedValue = 2 Then
            txt = "CS"
        ElseIf DDType.SelectedValue = 3 Then
            txt = "CT"
        ElseIf DDType.SelectedValue = 4 Then
            txt = "QA"
        Else
            txt = "OT"
        End If

        Dim sYear As String
        sYear = txt + Right(Date.Now.Year, 2)

        Dim sAuto As String = sYear

        sqlTmp = "SELECT TOP 1 doc_id FROM import_document "
        sqlTmp &= " WHERE left(doc_id,4) ='" & sAuto & "'"
        sqlTmp &= " ORDER BY doc_id DESC"

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

                tmpMemberID2 = Right(drTmp.Item("doc_id"), 4)

                tmpMemberID = CInt(tmpMemberID2) + 1
                txtDocId.Text = sAuto + tmpMemberID.ToString("-00000")

            End With
        Catch
            txtDocId.Text = sAuto + "-00001"
        End Try
        cn.Close()

    End Sub
    Function AutoDoc() As String

        Dim sqlTmp As String = ""
        Dim comTmp As OleDbCommand = New OleDbCommand
        Dim drTmp As OleDbDataReader
        Dim tmpMemberID As Integer = 0
        Dim tmpMemberID2 As String = ""
        Dim txt As String



        sqlTmp = "SELECT TOP 1 keyword_id FROM import_keywords "
        sqlTmp &= " ORDER BY keyword_id DESC"

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

                tmpMemberID2 = drTmp.Item("keyword_id")

                tmpMemberID = CInt(tmpMemberID2) + 1
                txt = tmpMemberID.ToString

            End With
        Catch
            txt = "1"
        End Try
        cn.Close()

        Return txt
    End Function
    Private Sub gData(Optional ByVal Type As String = "")

        Dim strsql As String

        strsql = " select d.keyword_id,d.doc_id,d.keyword "
        strsql &= "from import_keywords d "
        strsql &= "where d.doc_id='" & txtDocId.Text & "'"

        If txtSearch.Text <> "" Then
            strsql &= "and d.keyword like '%" & txtSearch.Text & "%'"
        End If

        Dim DT As DataTable = MD.GetDataTable(strsql)
        DVLst = DT.DefaultView
        DVLst.Sort = "[" & ViewState("sortfield") & "] " & ViewState("sortdirection")
        Session("DocumentImport") = DVLst

    End Sub
    Private Sub MyGridBind()
        GridView1.DataSource = DVLst
        Dim X1() As String = {"keyword_id"}
        GridView1.DataKeyNames = X1
        GridView1.DataBind()
    End Sub
    Private Sub AddNew()
        Dim dr As DataRow = DS.Tables(0).NewRow
        DS.Tables(0).Rows.Add(dr)
        iRec = DS.Tables(0).Rows.Count - 1
        ViewState("iRec") = iRec
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
                Dim L1 As ImageButton = e.Row.Cells(2).Controls(1)
                L1.OnClientClick = "return confirm('คุณต้องการลบข้อมูลใช่หรือไม่?');"
            End If
        End If


    End Sub
    Protected Sub GridView1_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GridView1.RowDeleting
        Dim K1 As DataKey = GridView1.DataKeys(e.RowIndex)
        Dim strsql As String
        strsql = "delete from import_keywords where keyword_id ='" & K1(0) & "'"
        Dim Y As Integer = MD.Execute(strsql)
        If Y > 0 Then
            Me.gData()
            Me.MyGridBind()
            MC.MessageBox(Me, "ลบข้อมูลเรียบร้อยแล้ว")
        Else
            MC.MessageBox(Me, "ไม่สามารถลบข้อมูลได้")
        End If
    End Sub
    Protected Sub GridView1_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles GridView1.RowEditing
       
    End Sub
    Protected Sub bCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bCancel.Click
        Me.ClearData()
        Me.ClearKeyText()

        Me.gData()
        Me.MyGridBind()
    End Sub

    Protected Sub bSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bSave.Click
        Dim sEmpNo As String = Session("EMPNO")
        Dim strPath As String = "Document_Import\" 'Server.MapPath("..\Document_Import\")

        If lblStatus.Text = "Add" Then
            Me.AddNew()
            Me.Auto()
        End If

        Dim DT As DataTable = DS.Tables(0)
        Dim dr As DataRow = DT.Rows(iRec)
        Dim txtLawType As String

        If DDType.SelectedValue = 4 Then
            txtLawType = ""
        Else
            txtLawType = DDLawType.SelectedValue
        End If

        If txtDocName.Text = "" Then
            MC.MessageBox(Me, "กรุณาชื่อเอกสาร")
            txtDocName.Focus()
            Exit Sub
        End If
        If txtReceiveDate.Text.Trim = "" Then
            MC.MessageBox(Me, "กรุณากรอกวันที่รับเรื่อง")
            Exit Sub
        End If
        If lblIdName1.Text = "" Then
            MC.MessageBox(Me, "กรุณากรอกเลือกผู้นำเข้าเอกสาร")
            Exit Sub
        End If
        If txtDocPage.Text = "" Then
            MC.MessageBox(Me, "กรุณาจำนวนหน้า")
            txtDocPage.Focus()
            Exit Sub
        End If

        Dim Drecieve As String = MC.Date2DB(txtReceiveDate.Text)
        Dim txtSecret As String
        If chkSecret.Checked = True Then
            txtSecret = "1"
        Else
            txtSecret = "0"
        End If
        If lblStatus.Text = "Add" Then

            '--------------Insert-----------------
            If FileUpload1.PostedFile Is Nothing OrElse String.IsNullOrEmpty(FileUpload1.PostedFile.FileName) OrElse FileUpload1.PostedFile.InputStream Is Nothing Then
                MC.MessageBox(Me, "กรุณาเลือกไฟล์ที่ต้องการอัพโหลด")
                Exit Sub
            End If

            Try
                Dim Strsql As String
                Strsql = "insert into import_document (doc_id,doc_type,doc_subtype,dates_recieve,secret "

                If FileUpload1.HasFile Then
                    Strsql &= ",file_path,mime_type "
                End If

                Strsql &= ",doc_name,doc_page,name_imp"
                Strsql &= ",creation_by,created_date,updated_by,updated_date)"
                Strsql &= " values  "
                Strsql &= " ('" & txtDocId.Text & "','" & DDType.SelectedValue & "','" & txtLawType & "','" & Drecieve & "','" & txtSecret & "'"

                If FileUpload1.HasFile Then
                    Dim extension As String = System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName).ToLower()
                    Dim MIMEType As String = Nothing
                    Select Case extension
                        Case ".jpg", ".jpeg", ".jpe"
                            MIMEType = ".jpg"
                            Strsql &= ",'" & strPath & "" & txtDocId.Text & "" & MIMEType & "','" & MIMEType & "'"
                        Case ".csv"
                            MIMEType = ".csv"
                            Strsql &= ",'" & strPath & "" & txtDocId.Text & "" & MIMEType & "','" & MIMEType & "'"
                        Case ".xls"
                            MIMEType = ".xls"
                            Strsql &= ",'" & strPath & "" & txtDocId.Text & "" & MIMEType & "','" & MIMEType & "'"
                        Case ".xlsx"
                            MIMEType = ".xlsx"
                            Strsql &= ",'" & strPath & "" & txtDocId.Text & "" & MIMEType & "','" & MIMEType & "'"
                        Case ".pdf"
                            MIMEType = ".pdf"
                            Strsql &= ",'" & strPath & "" & txtDocId.Text & "" & MIMEType & "','" & MIMEType & "'"
                        Case ".doc"
                            MIMEType = ".doc"
                            Strsql &= ",'" & strPath & "" & txtDocId.Text & "" & MIMEType & "','" & MIMEType & "'"
                        Case ".docx"
                            MIMEType = ".docx"
                            Strsql &= ",'" & strPath & "" & txtDocId.Text & "" & MIMEType & "','" & MIMEType & "'"
                        Case ".txt"
                            MIMEType = ".txt"
                            Strsql &= ",'" & strPath & "" & txtDocId.Text & "" & MIMEType & "','" & MIMEType & "'"
                        Case ".htm", ".html"
                            MIMEType = ".html"
                            Strsql &= ",'" & strPath & "" & txtDocId.Text & "" & MIMEType & "','" & MIMEType & "'"
                        Case Else
                            MC.MessageBox(Me, "Not a valid file format")
                            Exit Sub
                    End Select
                End If

                Strsql &= " ,'" & txtDocName.Text & "','" & txtDocPage.Text & "','" & lblIdName1.Text & "'"
                Strsql &= " ,'" & sEmpNo & "',getdate(),"
                Strsql &= " '" & sEmpNo & "',getdate())"


                Dim Y As Integer = MD.Execute(Strsql)
                If Y > 0 Then
                    Me.UploadFile()
                    'Me.ClearData()
                    MC.MessageBox(Me, "บันทึกลงฐานข้อมูลเรียบร้อยแล้ว")

                Else
                    MC.MessageBox(Me, "ไม่สามารถบันทึกลงฐานข้อมูลได้")

                End If
            Catch ex As Exception
                MC.MessageBox(Me, ex.ToString)
            End Try

        Else
            '--------------Update-------------
            Try
                Dim Strsql As String
                Strsql = "update import_document set doc_type='" & DDType.SelectedValue & "',doc_subtype='" & txtLawType & "', "
                Strsql &= "dates_recieve='" & Drecieve & "',secret='" & txtSecret & "' "

                If FileUpload1.HasFile Then
                    Strsql &= ",file_path= "
                End If

                If FileUpload1.HasFile Then
                    Dim extension As String = System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName).ToLower()
                    Dim MIMEType As String = Nothing
                    Select Case extension
                        Case ".jpg", ".jpeg", ".jpe"
                            MIMEType = ".jpg"
                            Strsql &= "'" & strPath & "" & txtDocId.Text & "" & MIMEType & "',mime_type='" & MIMEType & "'"
                        Case ".csv"
                            MIMEType = ".csv"
                            Strsql &= "'" & strPath & "" & txtDocId.Text & "" & MIMEType & "',mime_type='" & MIMEType & "'"
                        Case ".xls"
                            MIMEType = ".xls"
                            Strsql &= "'" & strPath & "" & txtDocId.Text & "" & MIMEType & "',mime_type='" & MIMEType & "'"
                        Case ".xlsx"
                            MIMEType = ".xlsx"
                            Strsql &= "'" & strPath & "" & txtDocId.Text & "" & MIMEType & "',mime_type='" & MIMEType & "'"
                        Case ".pdf"
                            MIMEType = ".pdf"
                            Strsql &= "'" & strPath & "" & txtDocId.Text & "" & MIMEType & "',mime_type='" & MIMEType & "'"
                        Case ".doc"
                            MIMEType = ".doc"
                            Strsql &= "'" & strPath & "" & txtDocId.Text & "" & MIMEType & "',mime_type='" & MIMEType & "'"
                        Case ".docx"
                            MIMEType = ".docx"
                            Strsql &= "'" & strPath & "" & txtDocId.Text & "" & MIMEType & "',mime_type='" & MIMEType & "'"
                        Case ".txt"
                            MIMEType = ".txt"
                            Strsql &= ",'" & strPath & "" & txtDocId.Text & "" & MIMEType & "',mime_type='" & MIMEType & "'"
                        Case ".htm", ".html"
                            MIMEType = ".html"
                            Strsql &= "'" & strPath & "" & txtDocId.Text & "" & MIMEType & "',mime_type='" & MIMEType & "'"
                        Case Else
                            MC.MessageBox(Me, "Not a valid file format")
                            Exit Sub
                    End Select
                End If

                Strsql &= ",doc_name='" & txtDocName.Text & "',doc_page='" & txtDocPage.Text & "',name_imp='" & lblIdName1.Text & "'"
                Strsql &= ",creation_by='" & sEmpNo & "',created_date=getdate(),updated_by='" & sEmpNo & "',updated_date=getdate()"
                Strsql &= "where doc_id='" & txtDocId.Text & "'"


                Dim Y As Integer = MD.Execute(Strsql)
                If Y > 0 Then
                    Me.UploadFile()
                    'Me.ClearData()
                    MC.MessageBox(Me, "บันทึกลงฐานข้อมูลเรียบร้อยแล้ว")
                Else
                    MC.MessageBox(Me, "ไม่สามารถบันทึกลงฐานข้อมูลได้")

                End If
            Catch ex As Exception
                MC.MessageBox(Me, ex.ToString)
            End Try

        End If
    End Sub
    Private Sub ClearData()
        txtDocId.Text = ""
        txtDocName.Text = ""
        txtReceiveDate.Text = ""
        txtName1.Text = ""
        lblIdName1.Text = ""
        txtDocPage.Text = ""

        lblStatus.Text = "Add"
    End Sub
    Private Sub UploadFile()
        If FileUpload1.HasFile Then
            Dim extension As String = System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName).ToLower()
            Dim MIMEType As String = Nothing

            Try

                Select Case extension

                    Case ".jpg", ".jpeg", ".jpe"
                        MIMEType = ".jpg"
                        Dim X As String = Path.GetFileName("" & txtDocId.Text & "" & MIMEType & "")
                        X = Server.MapPath("..\Document_Import\" & X)
                        FileUpload1.PostedFile.SaveAs(X)
                    Case ".csv"
                        MIMEType = ".csv"
                        Dim X As String = Path.GetFileName("" & txtDocId.Text & "" & MIMEType & "")
                        X = Server.MapPath("..\Document_Import\" & X)
                        FileUpload1.PostedFile.SaveAs(X)
                    Case ".xls"
                        MIMEType = ".xls"
                        Dim X As String = Path.GetFileName("" & txtDocId.Text & "" & MIMEType & "")
                        X = Server.MapPath("..\Document_Import\" & X)
                        FileUpload1.PostedFile.SaveAs(X)
                    Case ".xlsx"
                        MIMEType = ".xlsx"
                        Dim X As String = Path.GetFileName("" & txtDocId.Text & "" & MIMEType & "")
                        X = Server.MapPath("..\Document_Import\" & X)
                        FileUpload1.PostedFile.SaveAs(X)
                    Case ".pdf"
                        MIMEType = ".pdf"
                        Dim X As String = Path.GetFileName("" & txtDocId.Text & "" & MIMEType & "")
                        X = Server.MapPath("..\Document_Import\" & X)
                        FileUpload1.PostedFile.SaveAs(X)
                    Case ".doc"
                        MIMEType = ".doc"
                        Dim X As String = Path.GetFileName("" & txtDocId.Text & "" & MIMEType & "")
                        X = Server.MapPath("..\Document_Import\" & X)
                        FileUpload1.PostedFile.SaveAs(X)
                    Case ".docx"
                        MIMEType = ".docx"
                        Dim X As String = Path.GetFileName("" & txtDocId.Text & "" & MIMEType & "")
                        X = Server.MapPath("..\Document_Import\" & X)
                        FileUpload1.PostedFile.SaveAs(X)
                    Case ".txt"
                        MIMEType = ".txt"
                        Dim X As String = Path.GetFileName("" & txtDocId.Text & "" & MIMEType & "")
                        X = Server.MapPath("..\Document_Import\" & X)
                        FileUpload1.PostedFile.SaveAs(X)
                    Case ".htm", ".html"
                        MIMEType = ".html"
                        Dim X As String = Path.GetFileName("" & txtDocId.Text & "" & MIMEType & "")
                        X = Server.MapPath("..\Document_Import\" & X)
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
    Protected Sub bAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bAdd.Click
        Dim im As Integer = 0
        Try

            If txtKey1.Text.Trim = "" And txtKey2.Text.Trim = "" And txtKey3.Text.Trim = "" And txtKey4.Text.Trim = "" And txtKey5.Text.Trim = "" Then
                MC.MessageBox(Me, "กรุณากรอกคำค้นหา")
                txtKey1.Focus()
                Exit Sub
            End If

            If txtKey1.Text <> "" Then
                Me.SaveKeyword(txtKey1.Text)
                im += 1
            End If

            If txtKey2.Text <> "" Then
                Me.SaveKeyword(txtKey2.Text)
                im += 1
            End If

            If txtKey3.Text <> "" Then
                Me.SaveKeyword(txtKey3.Text)
                im += 1
            End If

            If txtKey4.Text <> "" Then
                Me.SaveKeyword(txtKey4.Text)
                im += 1
            End If

            If txtKey5.Text <> "" Then
                Me.SaveKeyword(txtKey5.Text)
                im += 1
            End If


        Catch ex As Exception

        End Try

        If im > 0 Then
            Me.gData()
            Me.MyGridBind()
            'MC.MessageBox(Me, "บันทึกลงฐานข้อมูลเรียบร้อยแล้ว")
            Me.ClearKeyText()
        End If

    End Sub
    Private Sub ClearKeyText()
        txtKey1.Text = ""
        txtKey2.Text = ""
        txtKey3.Text = ""
        txtKey4.Text = ""
        txtKey5.Text = ""
    End Sub

    Private Sub SaveKeyword(ByVal str As String)
        Dim sEmpNo As String = Session("EMPNO")
        Dim i As Integer = 0

        If txtDocId.Text = "" Then
            MC.MessageBox(Me, "กรุณาบันทึกข้อมูลพื้นฐานก่อน")
            Exit Sub
        End If

        Dim kid As String = AutoDoc()

        Dim sql As String
        sql = "insert into import_keywords (keyword_id,doc_id,keyword,creation_by,created_date,updated_by,updated_date)"
        sql &= " values "
        sql &= " ('" & kid & "','" & txtDocId.Text & "','" & str & "'"
        sql &= " ,'" & sEmpNo & "',getdate(),"
        sql &= " '" & sEmpNo & "',getdate())"

        Dim Y As Integer = MD.Execute(sql)

        If Y > 0 Then
            'Me.ClearKeyText()
            'MC.MessageBox(Me, "บันทึกลงฐานข้อมูลเรียบร้อยแล้ว")
        Else
            MC.MessageBox(Me, "ไม่สามารถบันทึกลงฐานข้อมูลได้")
        End If

    End Sub
    Protected Sub bCancelAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bCancelAdd.Click
        Me.ClearKeyText()
    End Sub
    Public Sub cb1_Checked(ByVal sender As Object, ByVal e As EventArgs)
        Dim cb1 As CheckBox = sender
        Dim dgi As GridViewRow
        Dim cb2 As CheckBox
        For Each dgi In GridView1.Rows
            cb2 = dgi.Cells(1).FindControl("cb1")
            cb2.Checked = cb1.Checked
        Next
    End Sub

    Protected Sub bDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bDel.Click
        Dim S1 As New System.Text.StringBuilder("")
        Dim sEmpNo As String = Session("EmpNo")
        Dim MD As New MainData
        Dim strSql As String = ""
        Dim oDs As New Data.DataSet
        Dim sData As String = ""

        For Each dgi As GridViewRow In GridView1.Rows
            Dim cb As CheckBox = dgi.Cells(1).FindControl("cb1")
            If cb.Checked = True Then

                Dim K1 As DataKey = GridView1.DataKeys(dgi.RowIndex)
                Dim index As Integer = Convert.ToInt32(dgi.RowIndex)
                Dim row As GridViewRow = GridView1.Rows(index)
                Dim item As New ListItem()

                item.Text = Server.HtmlDecode(row.Cells(1).Text) 'item

                S1.Append(K1(0))
                Dim Vkey As String = K1.Value
                If S1.Length > 0 Then 'Then S1.Append(",'")

                    strSql = "delete from import_keywords where keyword_id ='" & K1(0) & "'"

                    MD.Execute(strSql)
                    Me.gData()
                    Me.MyGridBind()
                End If

            End If

        Next

    End Sub
    Protected Sub bSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bSearchLaw.Click
        Me.gData()
        Me.MyGridBind()
    End Sub
    Private Sub BindListView1()
        Dim strsql As String

        strsql = " select d.doc_id,d.doc_name  "
        strsql &= "from import_document d  "
        strsql &= "where d.doc_type=1 "
        'strsql &= "and d.doc_subtype='" & DDLawType.SelectedValue & "'"
        strsql &= "order by d.doc_name"

        Dim DTS As DataTable = MD.GetDataTable(strsql)

        ListBox1.DataTextField = "doc_name"
        ListBox1.DataValueField = "doc_id"
        ListBox1.DataSource = DTS
        ListBox1.DataBind()

        DVListBox = DTS.DefaultView
        Session("ListBox") = DVListBox

    End Sub
    Protected Sub bSearchLaw_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bSearchLaw.Click
        Me.BindListView1()
    End Sub
    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        MC.MessageBox(Me, ListBox2.Items.Count.ToString())
    End Sub
End Class
