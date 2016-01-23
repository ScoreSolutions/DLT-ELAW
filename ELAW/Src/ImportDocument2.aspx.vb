Imports System.IO
Imports System.Web
Imports System.Web.UI
Imports System.Data
Imports System.Data.OleDb
Partial Class Src_ImportDocument2
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
    Dim DVList1 As DataView
    Dim DVList2 As DataView
    Private Sub ChkPermis()
        'ตรวจสอบสิทธิ์การใช้งาน
        Dim sEmpNo As String = Page.User.Identity.Name
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
        Dim LawID As String = Request.QueryString("LawID")


        Me.ChkPermis()
        Me.SetJava()

        If Not Page.IsPostBack Then
            ViewState("sortfield") = "doc_id"
            ViewState("sortdirection") = "desc"

            txtReceiveDate.Text = Date.Today

            Me.DocType()
            Me.DocSubType()
            'ค้นหาเอกสาร
            Me.DocTypeSearch()
            Me.DocSubTypeSearch()


            If X <> "" Then
                'wutt Add 
                'เพิ่มเอกสารเก่าให้สามาถดาวน์โหลดมาดูได้
                lbtnDownload.Visible = True

                'ดึงข้อมูลมาแสดงใน fields กรณีแก้ไขข้อมูล
                Dim sql As String

                sql = "select d.doc_id,d.doc_type,d.doc_subtype,d.doc_name,d.dates_recieve,d.secret, "
                sql &= " d.doc_page,d.name_imp,e.firstname+' '+e.lastname name1,l.link_id,d.cancel,d.cancel_comment "
                sql &= " from import_document d inner join employee e "
                sql &= " on d.name_imp=e.empid inner join linklaw_data l "
                sql &= " on d.doc_id=l.title "
                sql &= " where d.doc_id='" & X & "' and d.active=1 "

                DS = MD.GetDataset(sql)
                Session("imp_doc") = DS
                iRec = 0
                ViewState("iRec") = iRec

                Me.MyDataBind()
                Me.FindRow()

                txtDocId.Text = X
                lblStatus.Text = "Edit"

                'ต้องการให้แก้ไขได้ 120710
                'DDType.Enabled = False
                'DDLawType.Enabled = False
            Else
                Dim sql As String
                If LawID <> "" Then
                    sql = " select ld.law_id, '1' doc_type, ld.subtype_id doc_subtype, ld.title doc_name, GETDATE() dates_recieve, "
                    sql += "'' secret,1 doc_page,e.empid name_imp, e.firstname+' '+e.lastname name1, '' link_id, '' cancel, '' cancel_comment "
                    sql += " from LAW_DATA ld "
                    sql += " inner join employee e on e.empid=ld.creation_by "
                    sql += " where law_id='" & LawID & "'"

                    txtLawID.Text = LawID
                Else
                    sql = "select d.doc_id,d.doc_type,d.doc_subtype,d.doc_name,d.dates_recieve,d.secret, "
                    sql &= " d.doc_page,d.name_imp,e.firstname+' '+e.lastname name1,l.link_id,d.cancel,d.cancel_comment "
                    sql &= " from import_document d inner join employee e "
                    sql &= " on d.name_imp=e.empid inner join linklaw_data l "
                    sql &= " on d.doc_id=l.title and d.active=1 "

                End If

                DS = MD.GetDataset(sql)
                Session("imp_doc") = DS
                iRec = 0
                ViewState("iRec") = iRec

                lblStatus.Text = "Add"

            End If

            Me.gData()
            Me.MyGridBind()

            'Me.gDataFull(DDTypeSearch.SelectedValue, ddlLawType.SelectedValue)
            'Me.MyGridBindFull()

            'Me.gDataSelect()
            'Me.MyGridBindSelect()

        Else
            'wutt Add 
            'เพิ่มเอกสารเก่าให้สามาถดาวน์โหลดมาดูได้
            ''lbtnDownload.Visible = False

            DS = Session("imp_doc")
            iRec = ViewState("iRec")

            If Session("DocumentImport") Is Nothing Then
                Me.gData()
            Else
                DVLst = Session("DocumentImport")
            End If

            If Session("List1") Is Nothing Then
                Me.gDataFull(DDTypeSearch.SelectedValue, ddlLawType.SelectedValue)
            Else
                DVList1 = Session("List1")
            End If


            If Session("List2") Is Nothing Then
                Me.gDataSelect()
            Else
                DVList2 = Session("List2")
            End If

        End If

        txtReceiveDate.Attributes.Add("onkeypress", "return blockText(this, event,-1,0);")

    End Sub
    Private Sub DocType()
        'ประเภทเอกสาร
        Dim strsql As String

        strsql = " select * from document_type  "
        strsql &= "order by doc_name "

        Dim DTS As DataTable = MD.GetDataTable(strsql)

        DDType.DataTextField = "doc_name"
        DDType.DataValueField = "doc_id"
        DDType.DataSource = DTS
        DDType.DataBind()

    End Sub
    Private Sub DocSubType()
        'ชนิดเอกสาร
        Dim strsql As String
        Dim oDs As DataSet
        strsql = " select * from document_type where doc_id='" & DDType.SelectedValue & "' "
        oDs = MD.GetDataset(strsql)

        If oDs.Tables(0).Rows.Count > 0 Then
            Dim DTS As DataTable = MD.GetDataTable(oDs.Tables(0).Rows(0).Item("strsql").ToString)

            DDLawType.DataTextField = "name"
            DDLawType.DataValueField = "id"
            DDLawType.DataSource = DTS
            DDLawType.DataBind()
        End If


    End Sub
    Protected Sub DDType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DDType.SelectedIndexChanged
        Me.DocSubType()
        If txtDocId.Text <> "" Then
            bRename.Visible = True
        End If
    End Sub
    Public Function BindField(ByVal FieldName As String) As String
        'BindField
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
            Case Else
                Return DT.Rows(iRec)(FieldName) & ""
        End Select
    End Function
    Public Sub MyDataBind()
        'แสดงข้อมูลแต่ละ fields
        txtDocId.DataBind()
        txtDocName.DataBind()
        txtReceiveDate.DataBind()
        txtDocPage.DataBind()

        If DS.Tables(0).Rows(0).Item("secret").ToString = "1" Then
            chkSecret.Checked = True
        Else
            chkSecret.Checked = False
        End If

        If DS.Tables(0).Rows(0).Item("cancel").ToString = "1" Then
            chkCancel.Checked = True
        Else
            chkCancel.Checked = False
        End If

        lblId.DataBind()
        txtCancel_Comment.DataBind()

    End Sub
    Private Sub FindRow()
        'แสดงข้อมูลใน Datalist
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
        Me.DocSubType()

        Dim i As Integer = 0
        For i = 0 To DDLawType.Items.Count - 1
            If X = DDLawType.Items(i).Value Then
                Return i
            End If
        Next

    End Function
    Private Sub SetJava()
        'Key ได้เฉพาะตัวเลข
        txtDocPage.Attributes.Add("onkeypress", "return blockNonNumbers(this, event,-1,0);")
    End Sub
    Function genNoDiv() As String
        'ชนิดเอกสาร
        Dim sEmpNo As String = Page.User.Identity.Name
        Dim strsql As String
        Dim oDs As DataSet
        strsql = " select d.ref_no   "
        strsql &= " from employee e inner join division d "
        strsql &= " on e.div_id=d.div_id"
        strsql &= " where e.empid = '" & sEmpNo & "'"


        oDs = MD.GetDataset(strsql)

        If oDs.Tables(0).Rows.Count > 0 Then
            Return oDs.Tables(0).Rows(0).Item("ref_no").ToString
        End If
        Return "0"
    End Function
    Function genNoType() As String
        'ชนิดเอกสาร
        Dim strsql As String
        Dim oDs As DataSet
        strsql = " select * from document_type where doc_id='" & DDType.SelectedValue & "' "
        oDs = MD.GetDataset(strsql)

        If oDs.Tables(0).Rows.Count > 0 Then
            Dim DTS As DataTable = MD.GetDataTable("select ref_no from " & oDs.Tables(0).Rows(0).Item("ref_table").ToString & " where " & oDs.Tables(0).Rows(0).Item("ref_field").ToString & " ='" & DDLawType.SelectedValue & "'")
            Return DTS.Rows(0)("ref_no").ToString
        End If
        Return "XX"
    End Function

    Private Sub Auto()
        'Run No เอกสาร
        Dim sqlTmp As String = ""
        Dim comTmp As OleDbCommand = New OleDbCommand
        Dim drTmp As OleDbDataReader
        Dim tmpMemberID As Integer = 0
        Dim tmpMemberID2 As String = ""
        Dim div As String = genNoDiv()
        Dim txt As String = genNoType()


        Dim sYear As String
        sYear = Right(Date.Now.Year, 2)

        Dim sAuto As String = "LW" + div + "-" + txt + "-" + sYear

        sqlTmp = "SELECT TOP 1 doc_id FROM import_document "
        sqlTmp &= " WHERE left(doc_id,9) ='" & sAuto & "'"
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
        'Run No เอกสารแนบ
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
        'ดึงข้อมูลคำค้นหา ใส่ใน Gridview
        Dim strsql As String

        strsql = " select d.keyword_id,d.doc_id,d.keyword,d.pages "
        strsql &= "from import_keywords d "
        strsql &= "where d.doc_id='" & txtDocId.Text & "'  "

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
                Dim L1 As ImageButton = e.Row.Cells(2).Controls(1)
                'ยืนยันการลบข้อมูล
                L1.OnClientClick = "return confirm('คุณต้องการลบข้อมูลใช่หรือไม่?');"
            End If
        End If


    End Sub
    Protected Sub GridView1_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GridView1.RowDeleting
        'ลบคำค้นหา
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
    Protected Sub bCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bCancel.Click
        Me.ClearData()
        Me.ClearKeyText()

        Me.gData()
        Me.MyGridBind()
    End Sub
    Private Sub ClearAlert()
        lblAName.Text = ""
        lblADate.Text = ""
        lblAPage.Text = ""
        lblAFile.Text = ""
    End Sub
    Protected Sub bSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bSave.Click
        'บันทึกข้อมูล
        Dim sEmpNo As String = Page.User.Identity.Name
        Dim strPath As String = "Document_Import\"

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
            Me.ClearAlert()
            lblAName.Text = "กรุณากรอกชื่อเอกสาร"
            txtDocName.Focus()
            Exit Sub
        End If
        If txtReceiveDate.Text.Year = 1 Then
            Me.ClearAlert()
            lblADate.Text = "กรุณากรอกวันที่รับเรื่อง"
            Exit Sub
        End If

        If txtDocPage.Text = "" Then
            Me.ClearAlert()
            lblAPage.Text = "กรุณากรอกจำนวนหน้า"
            txtDocPage.Focus()
            Exit Sub
        End If

        Dim txtSecret As String
        Dim txtCancel As String

        If chkSecret.Checked = True Then
            txtSecret = "1"
        Else
            txtSecret = "0"
        End If

        If chkCancel.Checked = True Then
            txtCancel = "1"
        Else
            txtCancel = "0"
        End If

        If lblStatus.Text = "Add" Then

            '--------------Insert-----------------
            If FileUpload1.PostedFile Is Nothing OrElse String.IsNullOrEmpty(FileUpload1.PostedFile.FileName) OrElse FileUpload1.PostedFile.InputStream Is Nothing Then
                Me.ClearAlert()
                lblAFile.Text = "กรุณาเลือกไฟล์ที่ต้องการอัพโหลด"
                Exit Sub
            End If

            Try
                Dim Strsql As String
                Strsql = "insert into import_document (doc_id,doc_type,doc_subtype,dates_recieve,secret,cancel,cancel_comment "

                If FileUpload1.HasFile Then
                    Strsql &= ",file_path,mime_type "
                End If

                Strsql &= ",doc_name,doc_page,name_imp"
                Strsql &= ",creation_by,created_date,updated_by,updated_date, ref_law_id)"
                Strsql &= " values  "
                Strsql &= " ('" & txtDocId.Text & "','" & DDType.SelectedValue & "','" & txtLawType & "', '" & txtReceiveDate.SaveDate & "','" & txtSecret & "',"
                Strsql &= "'" & txtCancel & "','" & txtCancel_Comment.Text & "' "

                Dim MIMEType As String = Nothing
                If FileUpload1.HasFile Then
                    Dim extension As String = System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName).ToLower()

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

                Strsql &= " ,'" & txtDocName.Text & "','" & txtDocPage.Text & "','" & sEmpNo & "'"
                Strsql &= " ,'" & sEmpNo & "',getdate(),"
                Strsql &= " '" & sEmpNo & "',getdate(),'" & txtLawID.Text & "')"


                Dim Y As Integer = MD.Execute(Strsql)
                If Y > 0 Then
                    Me.SaveData()

                    Func.UploadFile(sEmpNo, FileUpload1, txtDocId.Text & MIMEType, strPath)


                    lblStatus.Text = "Edit"
                    MC.MessageBox(Me, "บันทึกลงฐานข้อมูลเรียบร้อยแล้ว")
                    Me.ClearAlert()
                Else
                    MC.MessageBox(Me, "ไม่สามารถบันทึกลงฐานข้อมูลได้")
                    txtDocId.Text = ""
                End If
            Catch ex As Exception
                MC.MessageBox(Me, ex.ToString)
            End Try

        Else
            '--------------Update-------------
            Try
                Dim Strsql As String
                Strsql = "update import_document set doc_type='" & DDType.SelectedValue & "',doc_subtype='" & txtLawType & "', "
                Strsql &= "dates_recieve='" & txtReceiveDate.SaveDate & "',secret='" & txtSecret & "',cancel='" & txtCancel & "',cancel_comment='" & txtCancel_Comment.Text & "' "

                If FileUpload1.HasFile Then
                    Strsql &= ",file_path= "
                End If

                Dim MIMEType As String = Nothing
                If FileUpload1.HasFile Then
                    Dim extension As String = System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName).ToLower()

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
                            Strsql &= "'" & strPath & "" & txtDocId.Text & "" & MIMEType & "',mime_type='" & MIMEType & "'"
                        Case ".htm", ".html"
                            MIMEType = ".html"
                            Strsql &= "'" & strPath & "" & txtDocId.Text & "" & MIMEType & "',mime_type='" & MIMEType & "'"
                        Case Else
                            MC.MessageBox(Me, "Not a valid file format")
                            Exit Sub
                    End Select
                End If

                Strsql &= ",doc_name='" & txtDocName.Text & "',doc_page='" & txtDocPage.Text & "',name_imp='" & sEmpNo & "'"
                Strsql &= ",creation_by='" & sEmpNo & "',created_date=getdate(),updated_by='" & sEmpNo & "',updated_date=getdate(), ref_law_id='" & txtLawID.Text & "'"
                Strsql &= "where doc_id='" & txtDocId.Text & "'"


                Dim Y As Integer = MD.Execute(Strsql)
                If Y > 0 Then
                    Func.UploadFile(sEmpNo, FileUpload1, txtDocId.Text & MIMEType, strPath)

                    lblStatus.Text = "Edit"
                    MC.MessageBox(Me, "บันทึกลงฐานข้อมูลเรียบร้อยแล้ว")
                    Me.ClearAlert()
                Else
                    MC.MessageBox(Me, "ไม่สามารถบันทึกลงฐานข้อมูลได้")

                End If
            Catch ex As Exception
                MC.MessageBox(Me, ex.ToString)
            End Try

        End If
        Me.gDataFull(DDTypeSearch.SelectedValue, ddlLawType.SelectedValue)
        Me.MyGridBindFull()
    End Sub
    Private Sub ClearData()
        txtDocId.Text = ""
        txtDocName.Text = ""
        txtDocPage.Text = ""

        lblStatus.Text = "Add"
    End Sub
    Protected Sub bAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bAdd.Click
        Try
            If txtPage1.Text.Trim <> "" And txtKey1.Text <> "" Then
                Me.SaveKeyword(txtKey1.Text, txtPage1.Text)
            End If

        Catch ex As Exception

        End Try

    End Sub
    Private Sub ClearKeyText()
        txtKey1.Text = ""
    End Sub
    Private Sub SaveKeyword(ByVal txtKey As String, ByVal txtPage As String)
        Dim sEmpNo As String = Session("EMPNO")
        Dim aryItem() As String
        Dim i As Integer = 0


        If txtDocId.Text = "" Then
            MC.MessageBox(Me, "กรุณาบันทึกข้อมูลพื้นฐานก่อน")
            Exit Sub
        End If
        'ตัดคำด้วย (;) กรณีมีหลายหน้า
        aryItem = txtPage.Split(";")

        While i < aryItem.Length
            If aryItem(i).ToString <> "" Then

                Dim kid As String = AutoDoc()
                Dim sql As String
                sql = "insert into import_keywords (keyword_id,doc_id,pages,keyword,creation_by,created_date,updated_by,updated_date)"
                sql &= " values "
                sql &= " ('" & kid & "','" & txtDocId.Text & "','" & aryItem(i).ToString & "','" & txtKey & "'"
                sql &= " ,'" & sEmpNo & "',getdate(),"
                sql &= " '" & sEmpNo & "',getdate())"

                Dim Y As Integer = MD.Execute(sql)

            End If
            i += 1
        End While

        If i > 0 Then

            Me.gData()
            Me.MyGridBind()

            Me.gDataFull(DDTypeSearch.SelectedValue, ddlLawType.SelectedValue)
            Me.MyGridBindFull()
        End If
        txtKey1.Text = ""
        txtPage1.Text = ""
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
    Private Sub gDataFull(Optional ByVal type As String = "0", Optional ByVal subtype As String = "0")
        'ดึงข้อมูลเอกสารทั้งหมด มาแสดงใน Gridview
        Dim strsql As String

        strsql = " select d.doc_id,d.doc_name,v.name subtype_name,d.doc_type,d.doc_subtype"
        strsql &= " from import_document d  "
        strsql &= " inner join import_keywords k"
        strsql &= " on d.doc_id=k.doc_id inner join DOCUMENT_TYPE t"
        strsql &= " on d.doc_type=t.doc_id  "
        strsql &= " inner join import_document_subtype v"
        strsql &= " on d.doc_subtype=v.id and v.tbl=t.ref_table "
        strsql &= " where d.doc_id not in "
        strsql &= " (select doc_id from link_detail where link_id='" & lblId.Text & "') and d.doc_id<>'" & txtDocId.Text & "' and d.active=1 "

        If txtKeyword.Text <> "" Then
            strsql &= "and k.keyword like '%" & txtKeyword.Text & "%'"
        End If

        If type <> "0" And type <> "" Then
            strsql &= "and d.doc_type = '" & DDTypeSearch.SelectedValue & "' "
        End If

        If subtype <> "0" And subtype <> "" Then
            strsql &= "and d.doc_subtype = '" & ddlLawType.SelectedValue & "' "
        End If

        strsql &= "group by d.doc_id,d.doc_name,v.name, d.doc_type,d.doc_subtype "
        strsql &= "order by d.doc_type,d.doc_subtype,d.doc_name"

        Dim DT As DataTable = MD.GetDataTable(strsql)
        DVList1 = DT.DefaultView
        Session("List1") = DVList1

    End Sub
    Private Sub MyGridBindFull()
        GridView2.DataSource = DVList1
        Dim X1() As String = {"doc_id"}
        GridView2.DataKeyNames = X1
        GridView2.DataBind()
    End Sub
    Private Sub gDataSelect()
        'ดึงข้อมูลเอกสารที่เลือก มาแสดงใน Gridview
        Dim strsql As String

        strsql = " select d.doc_id,d.doc_name,v.name subtype_name"
        strsql &= " from import_document d  "
        strsql &= " inner join DOCUMENT_TYPE t"
        strsql &= " on d.doc_type=t.doc_id  "
        strsql &= " inner join import_document_subtype v"
        strsql &= " on d.doc_subtype=v.id and v.tbl=t.ref_table "
        strsql &= " inner join link_detail l on d.doc_id=l.doc_id  "
        strsql &= "where l.link_id='" & lblId.Text & "' and d.doc_id<>'" & txtDocId.Text & "' and d.active=1 "
        strsql &= "order by d.doc_name"

        Dim DT As DataTable = MD.GetDataTable(strsql)
        DVList2 = DT.DefaultView
        Session("List2") = DVList2

    End Sub
    Private Sub MyGridBindSelect()
        GridView3.DataSource = DVList2
        Dim X1() As String = {"doc_id"}
        GridView3.DataKeyNames = X1
        GridView3.DataBind()
    End Sub
    Private Sub GoPage2(ByVal xPage As Integer)
        GridView2.PageIndex = xPage
        Me.MyGridBindFull()
    End Sub
    Private Sub FirstClick2(ByVal sender As Object, ByVal e As EventArgs)
        Me.GoPage2(0)
    End Sub
    Private Sub PrevClick2(ByVal sender As Object, ByVal e As EventArgs)
        Me.GoPage2(GridView2.PageIndex - 1)
    End Sub
    Private Sub NextClick2(ByVal sender As Object, ByVal e As EventArgs)
        Me.GoPage2(GridView2.PageIndex + 1)
    End Sub
    Private Sub LastClick2(ByVal sender As Object, ByVal e As EventArgs)
        Me.GoPage2(GridView2.PageCount - 1)
    End Sub
    Protected Sub GridView2_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridView2.PageIndexChanging
        GridView2.PageIndex = e.NewPageIndex
        Me.MyGridBindFull()
    End Sub
    Protected Sub GridView2_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView2.RowCreated
        'Create Page Gridview
        If e.Row.RowType = DataControlRowType.Pager Then
            Dim td As TableCell = e.Row.Cells(0)
            Dim Y As Boolean = False
            If GridView2.PageIndex > 0 Then
                Dim L1 As New LinkButton
                L1.ForeColor = Drawing.Color.Black
                L1.Text = "First"
                td.Controls.Add(L1)
                AddHandler L1.Click, AddressOf FirstClick2

                Y = True

                Dim L2 As New Literal
                L2.Text = " &nbsp;&nbsp;"
                td.Controls.Add(L2)
                td.HorizontalAlign = HorizontalAlign.Left

                L1 = New LinkButton
                L1.ForeColor = Drawing.Color.Black
                L1.Text = "Prev"
                AddHandler L1.Click, AddressOf PrevClick2
                td.Controls.Add(L1)
                td.HorizontalAlign = HorizontalAlign.Left

            End If
            If GridView2.PageIndex < GridView2.PageCount - 1 Then
                Dim L2 As Literal

                If Y = True Then
                    L2 = New Literal
                    L2.Text = " &nbsp;&nbsp;"
                    td.Controls.Add(L2)
                End If

                Dim L1 As New LinkButton
                L1.ForeColor = Drawing.Color.Black
                L1.Text = "Next"
                AddHandler L1.Click, AddressOf NextClick2
                td.Controls.Add(L1)

                L2 = New Literal
                L2.Text = " &nbsp;&nbsp;"
                td.Controls.Add(L2)
                td.HorizontalAlign = HorizontalAlign.Left

                L1 = New LinkButton
                L1.ForeColor = Drawing.Color.Black
                L1.Text = "Last"
                AddHandler L1.Click, AddressOf LastClick2
                td.Controls.Add(L1)
                td.HorizontalAlign = HorizontalAlign.Left
            End If
        End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            iRow = e.Row.DataItemIndex + 1
            If Not e.Row.RowState And DataControlRowState.Edit Then
                'Dim L1 As ImageButton = e.Row.Cells(4).Controls(1)
                ''ยืนยันการลบข้อมูล
                'L1.OnClientClick = "return confirm('คุณต้องการลบข้อมูลใช่หรือไม่?');"
            End If
        End If
    End Sub
    Protected Sub GridView2_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView2.RowDataBound
        'ดาวน์โหลดเอกสาร
  
        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim K2 As String
            K2 = Convert.ToString(GridView2.DataKeys(e.Row.RowIndex).Values(0).ToString())
            Dim X As String = Request.QueryString("id")
            Dim strsql As String = ""

            strsql = "select d.doc_id,d.doc_name, "
            strsql &= "d.file_path,d.mime_type "
            strsql &= "from import_document d "
            strsql &= "where d.doc_id='" & K2 & "' and d.active=1 "

            Dim dt As DataTable
            dt = MD.GetDataTable(strsql)

            Dim lblLinkFile As Label = e.Row.Cells(2).FindControl("lblLinkFile")


            For Each dr As DataRow In dt.Rows

                If dr("mime_type").ToString() = ".pdf" Then

                    Dim strUrlFile As String = "http://" & Constant.BaseURL(Request) & "" & dr("file_path").ToString() & ""
                    lblLinkFile.Text = "<a href='" & strUrlFile & "?time=" & Date.Now.ToString("HH:mm:ss") & "' Target='_Blank'>" & dr("doc_name").ToString() & "</a>&nbsp;&nbsp;"

                ElseIf dr("mime_type").ToString() = ".doc" Or dr("mime_type").ToString() = ".docx" Then

                    Dim strUrl As String = "http://" & Constant.BaseURL(Request) & dr("file_path").ToString()
                    lblLinkFile.Text = "<a href='" & strUrl & "?time=" & Date.Now.ToString("HH:mm:ss") & "' Target='_Blank'>" & dr("doc_name").ToString() & "</a>&nbsp;&nbsp;"

                ElseIf dr("mime_type").ToString() = ".xls" Or dr("mime_type").ToString() = ".xlsx" Then

                    Dim strUrl As String = "http://" & Constant.BaseURL(Request) & dr("file_path").ToString()
                    lblLinkFile.Text = "<a href='" & strUrl & "?time=" & Date.Now.ToString("HH:mm:ss") & "' Target='_Blank'>" & dr("doc_name").ToString() & "</a>&nbsp;&nbsp;"

                ElseIf dr("mime_type").ToString() = ".txt" Then

                    Dim strUrl As String = "http://" & Constant.BaseURL(Request) & dr("file_path").ToString()
                    lblLinkFile.Text = "<a href='" & strUrl & "?time=" & Date.Now.ToString("HH:mm:ss") & "' Target='_Blank'>" & dr("doc_name").ToString() & "</a>&nbsp;&nbsp;"

                End If

            Next

        End If
    End Sub
    Protected Sub GridView2_SelectedIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSelectEventArgs) Handles GridView2.SelectedIndexChanging
        'เลือกเอกสารที่ต้องการเชื่อมโยง
        Dim K1 As DataKey = GridView2.DataKeys(e.NewSelectedIndex)
        Dim X As String = Request.QueryString("id")
        Dim sEmpNo As String = Session("EmpNo")
        Dim S1 As New System.Text.StringBuilder("")
        Dim MD As New MainData
        Dim strSql As String = ""
        Dim oDs As New Data.DataSet
        Dim sData As String = ""


        If lblId.Text = "" Then
            Exit Sub
        End If

        Try
            strSql = "insert into link_detail (link_id,doc_id, "
            strSql &= " creation_by,created_date,updated_by,updated_date )"
            strSql &= " values (?,?,?,?,?,?) "

            cn = New OleDbConnection(MD.Strcon)
            cmd = New OleDbCommand(strSql, cn)
            MD.CreateParam(cmd, "TTTDTD")

            cmd.Parameters("@P1").Value = lblId.Text
            cmd.Parameters("@P2").Value = K1(0).ToString
            cmd.Parameters("@P3").Value = sEmpNo
            cmd.Parameters("@P4").Value = DateTime.Parse(Date.Now)
            cmd.Parameters("@P5").Value = sEmpNo
            cmd.Parameters("@P6").Value = DateTime.Parse(Date.Now)

            cn.Open()
            cmd.ExecuteNonQuery()

            Me.gDataFull(DDTypeSearch.SelectedValue, ddlLawType.SelectedValue)
            Me.MyGridBindFull()

            Me.gDataSelect()
            Me.MyGridBindSelect()

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
    Private Sub GenAuto()
        'Run รหัส
        Dim sqlTmp As String = ""
        Dim comTmp As OleDbCommand = New OleDbCommand
        Dim drTmp As OleDbDataReader
        Dim tmpMemberID As Integer = 0
        Dim tmpMemberID2 As String = ""

        Dim sAuto As String = "LNK" + Right(Now.Year, 2)

        sqlTmp = "SELECT TOP 1 right(link_id,4) link_id FROM linklaw_data "
        sqlTmp &= "where left(link_id,5)='" & sAuto & "' "
        sqlTmp &= " ORDER BY link_id DESC"

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

                tmpMemberID2 = Right(drTmp.Item("link_id"), 4)

                tmpMemberID = CInt(tmpMemberID2) + 1
                lblId.Text = sAuto + tmpMemberID.ToString("-0000")

            End With
        Catch
            lblId.Text = sAuto + "-0001"
        End Try
        cn.Close()

    End Sub
    Private Sub SaveData()
        Dim X As String = Request.QueryString("id")
        Dim sEmpNo As String = Page.User.Identity.Name
        Dim strsql As String
        Me.GenAuto()
        Try

            strsql = "insert into linklaw_data (link_id,title, "
            strsql &= " creation_by,created_date,updated_by,updated_date )"
            strsql &= " values (?,?,?,?,?,?) "

            cn = New OleDbConnection(MD.Strcon)
            cmd = New OleDbCommand(strsql, cn)
            MD.CreateParam(cmd, "TTTDTD")

            cmd.Parameters("@P1").Value = lblId.Text
            cmd.Parameters("@P2").Value = txtDocId.Text
            cmd.Parameters("@P3").Value = sEmpNo
            cmd.Parameters("@P4").Value = DateTime.Parse(Date.Now)
            cmd.Parameters("@P5").Value = sEmpNo
            cmd.Parameters("@P6").Value = DateTime.Parse(Date.Now)


            cn.Open()
            cmd.ExecuteNonQuery()

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
    Private Sub SaveLink()
        Dim X As String = Request.QueryString("id")
        Dim sEmpNo As String = Page.User.Identity.Name
        Dim strsql As String
        Me.GenAuto()
        Try

            strsql = "insert into link_detail (link_id,doc_id, "
            strsql &= " creation_by,created_date,updated_by,updated_date )"
            strsql &= " values (?,?,?,?,?,?) "

            cn = New OleDbConnection(MD.Strcon)
            cmd = New OleDbCommand(strsql, cn)
            MD.CreateParam(cmd, "TTTDTD")

            cmd.Parameters("@P1").Value = lblId.Text
            cmd.Parameters("@P2").Value = txtDocId.Text
            cmd.Parameters("@P3").Value = sEmpNo
            cmd.Parameters("@P4").Value = DateTime.Parse(Date.Now)
            cmd.Parameters("@P5").Value = sEmpNo
            cmd.Parameters("@P6").Value = DateTime.Parse(Date.Now)


            cn.Open()
            cmd.ExecuteNonQuery()

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
    Private Sub GoPage3(ByVal xPage As Integer)
        GridView3.PageIndex = xPage
        Me.MyGridBindSelect()
    End Sub
    Private Sub FirstClick3(ByVal sender As Object, ByVal e As EventArgs)
        Me.GoPage3(0)
    End Sub
    Private Sub PrevClick3(ByVal sender As Object, ByVal e As EventArgs)
        Me.GoPage3(GridView3.PageIndex - 1)
    End Sub
    Private Sub NextClick3(ByVal sender As Object, ByVal e As EventArgs)
        Me.GoPage3(GridView3.PageIndex + 1)
    End Sub
    Private Sub LastClick3(ByVal sender As Object, ByVal e As EventArgs)
        Me.GoPage3(GridView3.PageCount - 1)
    End Sub
    Protected Sub GridView3_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridView3.PageIndexChanging
        GridView3.PageIndex = e.NewPageIndex
        Me.MyGridBindSelect()
    End Sub
    Protected Sub GridView3_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView3.RowCreated
        'Create Page Gridview
        If e.Row.RowType = DataControlRowType.Pager Then
            Dim td As TableCell = e.Row.Cells(0)
            Dim Y As Boolean = False
            If GridView3.PageIndex > 0 Then
                Dim L1 As New LinkButton
                L1.ForeColor = Drawing.Color.Black
                L1.Text = "First"
                td.Controls.Add(L1)
                AddHandler L1.Click, AddressOf FirstClick3

                Y = True

                Dim L2 As New Literal
                L2.Text = " &nbsp;&nbsp;"
                td.Controls.Add(L2)
                td.HorizontalAlign = HorizontalAlign.Left

                L1 = New LinkButton
                L1.ForeColor = Drawing.Color.Black
                L1.Text = "Prev"
                AddHandler L1.Click, AddressOf PrevClick3
                td.Controls.Add(L1)
                td.HorizontalAlign = HorizontalAlign.Left

            End If
            If GridView3.PageIndex < GridView3.PageCount - 1 Then
                Dim L2 As Literal

                If Y = True Then
                    L2 = New Literal
                    L2.Text = " &nbsp;&nbsp;"
                    td.Controls.Add(L2)
                End If

                Dim L1 As New LinkButton
                L1.ForeColor = Drawing.Color.Black
                L1.Text = "Next"
                AddHandler L1.Click, AddressOf NextClick3
                td.Controls.Add(L1)

                L2 = New Literal
                L2.Text = " &nbsp;&nbsp;"
                td.Controls.Add(L2)
                td.HorizontalAlign = HorizontalAlign.Left

                L1 = New LinkButton
                L1.ForeColor = Drawing.Color.Black
                L1.Text = "Last"
                AddHandler L1.Click, AddressOf LastClick3
                td.Controls.Add(L1)
                td.HorizontalAlign = HorizontalAlign.Left
            End If
        End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            iRow = e.Row.DataItemIndex + 1
            If Not e.Row.RowState And DataControlRowState.Edit Then
                'Dim L1 As ImageButton = e.Row.Cells(4).Controls(1)
                ''ยืนยันการลบข้อมูล
                'L1.OnClientClick = "return confirm('คุณต้องการลบข้อมูลใช่หรือไม่?');"
            End If
        End If
    End Sub
    Protected Sub GridView3_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView3.RowDataBound
        'ดาวน์โหลดเอกสาร

        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim K2 As String
            K2 = Convert.ToString(GridView3.DataKeys(e.Row.RowIndex).Values(0).ToString())
            Dim X As String = Request.QueryString("id")
            Dim strsql As String = ""

            strsql = "select d.doc_id,d.doc_name, "
            strsql &= "d.file_path,d.mime_type "
            strsql &= "from import_document d "
            strsql &= "where d.doc_id='" & K2 & "' and d.active=1 "

            Dim dt As DataTable
            dt = MD.GetDataTable(strsql)

            Dim lblLinkFile As Label = e.Row.Cells(2).FindControl("lblLinkFile")


            For Each dr As DataRow In dt.Rows


                If dr("mime_type").ToString() = ".pdf" Then

                    Dim strUrlFile As String = "http://" & Constant.BaseURL(Request) & "" & dr("file_path").ToString() & ""
                    lblLinkFile.Text = "<a href='" & strUrlFile & "?time=" & Date.Now.ToString("HH:mm:ss") & "' Target='_Blank'>" & dr("doc_name").ToString() & "</a>&nbsp;&nbsp;"

                ElseIf dr("mime_type").ToString() = ".doc" Or dr("mime_type").ToString() = ".docx" Then

                    Dim strUrl As String = "http://" & Constant.BaseURL(Request) & dr("file_path").ToString()
                    lblLinkFile.Text = "<a href='" & strUrl & "?time=" & Date.Now.ToString("HH:mm:ss") & "' Target='_Blank'>" & dr("doc_name").ToString() & "</a>&nbsp;&nbsp;"

                ElseIf dr("mime_type").ToString() = ".xls" Or dr("mime_type").ToString() = ".xlsx" Then

                    Dim strUrl As String = "http://" & Constant.BaseURL(Request) & dr("file_path").ToString()
                    lblLinkFile.Text = "<a href='" & strUrl & "?time=" & Date.Now.ToString("HH:mm:ss") & "' Target='_Blank'>" & dr("doc_name").ToString() & "</a>&nbsp;&nbsp;"

                ElseIf dr("mime_type").ToString() = ".txt" Then

                    Dim strUrl As String = "http://" & Constant.BaseURL(Request) & dr("file_path").ToString()
                    lblLinkFile.Text = "<a href='" & strUrl & "?time=" & Date.Now.ToString("HH:mm:ss") & "' Target='_Blank'>" & dr("doc_name").ToString() & "</a>&nbsp;&nbsp;"

                End If

            Next

        End If
    End Sub
    Protected Sub GridView3_SelectedIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSelectEventArgs) Handles GridView3.SelectedIndexChanging
        'เลือกเอกสารเพื่อทำการเชื่อมโยง
        Dim K2 As DataKey = GridView3.DataKeys(e.NewSelectedIndex)
        Dim X As String = Request.QueryString("id")
        Dim sEmpNo As String = Session("EmpNo")
        Dim strSql As String = ""

        strSql = "delete from link_detail where link_id='" & lblId.Text & "' and doc_id='" & K2(0).ToString & "'"

        MD.Execute(strSql)

        Me.gDataFull(DDTypeSearch.SelectedValue, ddlLawType.SelectedValue)
        Me.MyGridBindFull()

        Me.gDataSelect()
        Me.MyGridBindSelect()


    End Sub
    Protected Sub bSearchLaw_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bSearchLaw.Click
        Me.gDataFull(DDTypeSearch.SelectedValue, ddlLawType.SelectedValue)
        Me.MyGridBindFull()

        Me.gDataSelect()
        Me.MyGridBindSelect()
    End Sub
    Protected Sub bAddNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bAddNew.Click
        Me.ClearData()
        lblStatus.Text = "Add"

        txtKey1.Text = ""
        txtPage1.Text = ""

        DDType.Enabled = True
        DDLawType.Enabled = True
        bRename.Visible = False

        Me.gData()
        Me.MyGridBind()

        Me.gDataFull()
        Me.MyGridBindFull()

        Me.gDataSelect()
        Me.MyGridBindSelect()
    End Sub
    Protected Sub link1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles link2.Click
        Response.Redirect("../Src/ImportList.aspx", True)
    End Sub
    Protected Sub linkHome_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles linkHome.Click
        Response.Redirect(MD.pHome, True)
    End Sub
    Private Sub DocTypeSearch()
        'ประเภทเอกสาร
        Dim strsql As String

        strsql = " select * from document_type  "
        strsql &= "order by doc_name "

        Dim DTS As DataTable = MD.GetDataTable(strsql)
        Dim dr As DataRow = DTS.NewRow
        dr!doc_id = 0
        dr!doc_name = "---โปรดเลือก---"
        DTS.Rows.InsertAt(dr, 0)
        DDTypeSearch.DataTextField = "doc_name"
        DDTypeSearch.DataValueField = "doc_id"
        DDTypeSearch.DataSource = DTS
        DDTypeSearch.DataBind()

    End Sub
    Private Sub DocSubTypeSearch()
        'ชนิดเอกสาร
        Dim strsql As String
        Dim oDs As DataSet
        strsql = " select * from document_type where doc_id='" & DDTypeSearch.SelectedValue & "' "
        oDs = MD.GetDataset(strsql)

        If oDs.Tables(0).Rows.Count > 0 Then
            Dim DTS As DataTable = MD.GetDataTable(oDs.Tables(0).Rows(0).Item("strsql").ToString)
            Dim dr As DataRow = DTS.NewRow
            dr!id = 0
            dr!name = "---โปรดเลือก---"
            DTS.Rows.InsertAt(dr, 0)
            ddlLawType.DataTextField = "name"
            ddlLawType.DataValueField = "id"
            ddlLawType.DataSource = DTS
            ddlLawType.DataBind()
        Else
            ddlLawType.Items.Clear()
            ddlLawType.Items.Add(New ListItem("---โปรดเลือก---", 0))
        End If


    End Sub
    Protected Sub DDTypeSearch_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DDTypeSearch.SelectedIndexChanged
        Me.DocSubTypeSearch()
    End Sub
    Protected Sub lbtnDownload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbtnDownload.Click
        Try
            'wutt Add 
            'เพิ่มเอกสารเก่าให้สามาถดาวน์โหลดมาดูได้
            Dim strsql2 As String = ""
            strsql2 = "select * from import_document Where doc_id='" & txtDocId.Text & "'"

            Dim dt As New DataTable
            dt = MD.GetDataTable(strsql2)
            If dt.Rows.Count > 0 Then

                If dt.Rows(0)("Mime_type") = ".pdf" Or dt.Rows(0)("Mime_type") = ".txt" Then
                    MC.OpenWindow(Me, "http://" & Constant.BaseURL(Request) & "" & dt.Rows(0)("file_path") & "")
                Else
                    MC.OpenWindow(Me, "http://" & Constant.BaseURL(Request) & dt.Rows(0)("file_path") & "")
                End If
            End If
            lbtnDownload.Visible = True
        Catch ex As Exception
            MC.MessageBox(Me, ex.ToString)
        End Try

    End Sub
    Protected Sub DDLawType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DDLawType.SelectedIndexChanged
        If txtDocId.Text <> "" Then
            bRename.Visible = True
        End If
    End Sub
    Protected Sub bRename_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bRename.Click
        Try
            lblOldId.Text = txtDocId.Text
            lblOldLink.Text = lblId.Text

            DeleteFile()
            SaveNewFile()
            SaveNewKeyword()
            SaveNewLinkDetail()
            RenameFile()


            Me.gData()
            Me.MyGridBind()

            Me.gDataFull()
            Me.MyGridBindFull()

            Me.gDataSelect()
            Me.MyGridBindSelect()

            lblOldId.Text = ""
            lblOldLink.Text = ""
        Catch ex As Exception

        End Try


    End Sub
    Private Sub SaveNewFile()
        Me.Auto()

        Dim sEmpNo As String = Page.User.Identity.Name
        Dim strPath As String = "Document_Import\"

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
            Me.ClearAlert()
            lblAName.Text = "กรุณากรอกชื่อเอกสาร"
            txtDocName.Focus()
            Exit Sub
        End If
        If txtReceiveDate.Text.Year = 1 Then
            Me.ClearAlert()
            lblADate.Text = "กรุณากรอกวันที่รับเรื่อง"
            Exit Sub
        End If

        If txtDocPage.Text = "" Then
            Me.ClearAlert()
            lblAPage.Text = "กรุณากรอกจำนวนหน้า"
            txtDocPage.Focus()
            Exit Sub
        End If

        Dim txtSecret As String
        Dim txtCancel As String

        If chkSecret.Checked = True Then
            txtSecret = "1"
        Else
            txtSecret = "0"
        End If

        If chkCancel.Checked = True Then
            txtCancel = "1"
        Else
            txtCancel = "0"
        End If


        Try
            Dim Strsql As String
            Strsql = "insert into import_document (doc_id,doc_type,doc_subtype,dates_recieve,secret,cancel,cancel_comment "
            Strsql &= ",file_path,mime_type "
            Strsql &= ",doc_name,doc_page,name_imp"
            Strsql &= ",creation_by,created_date,updated_by,updated_date, ref_law_id)"
            Strsql &= " values  "
            Strsql &= " ('" & txtDocId.Text & "','" & DDType.SelectedValue & "','" & txtLawType & "', '" & txtReceiveDate.SaveDate & "','" & txtSecret & "',"
            Strsql &= "'" & txtCancel & "','" & txtCancel_Comment.Text & "' "

            'ดึงค่าไฟล์เดิม
            Dim chk As String
            Dim DS As DataSet
            Dim mtype As String = ""
            Dim fname As String = ""
            Dim fname2 As String = ""

            chk = "select * from import_document where doc_id ='" & lblOldId.Text & "'"

            DS = MD.GetDataset(chk)

            If DS.Tables(0).Rows.Count > 0 Then
                Strsql &= " ,'" & strPath & "" & txtDocId.Text & "" & DS.Tables(0).Rows(0).Item("mime_type").ToString & "' "
                Strsql &= " ,'" & DS.Tables(0).Rows(0).Item("mime_type").ToString & "' "
            End If

            'ดึงค่าไฟล์เดิม


            Strsql &= " ,'" & txtDocName.Text & "','" & txtDocPage.Text & "','" & sEmpNo & "'"
            Strsql &= " ,'" & sEmpNo & "',getdate(),"
            Strsql &= " '" & sEmpNo & "',getdate(),'" & txtLawID.Text & "')"


            Dim Y As Integer = MD.Execute(Strsql)
            If Y > 0 Then
                Me.SaveData()

                lblStatus.Text = "Edit"
                MC.MessageBox(Me, "บันทึกลงฐานข้อมูลเรียบร้อยแล้ว")
                Me.ClearAlert()
            Else
                MC.MessageBox(Me, "ไม่สามารถบันทึกลงฐานข้อมูลได้")
                txtDocId.Text = ""
            End If
        Catch ex As Exception
            MC.MessageBox(Me, ex.ToString)
        End Try


    End Sub
    Private Sub SaveNewKeyword()
        Dim sEmpNo As String = Page.User.Identity.Name
        Dim sql As String
        Dim chk As String

        chk = "select keyword_id,keyword from import_keywords where doc_id='" & lblOldId.Text & "'"

        Dim a1 As New OleDbDataAdapter(chk, MD.Strcon)
        Dim s1 As New DataSet()
        a1.Fill(s1)

        For Each dr As DataRow In s1.Tables(0).Rows
            Dim kid As String = AutoDoc()
            sql = "insert into import_keywords (keyword_id,doc_id,pages,keyword,creation_by,created_date,updated_by,updated_date)"
            sql &= "(select '" & kid & "','" & txtDocId.Text & "',pages,keyword,'" & sEmpNo & "',getdate(),'" & sEmpNo & "',getdate() "
            sql &= "from import_keywords where keyword_id='" & dr(0).ToString() & "')"

            MD.Execute(sql)
        Next

    End Sub
    Private Sub SaveNewLinkDetail()
        Dim sEmpNo As String = Page.User.Identity.Name
        Dim sql As String
        Dim chk As String

        chk = "select link_id,doc_id from link_detail where link_id='" & lblOldLink.Text & "'"

        Dim a1 As New OleDbDataAdapter(chk, MD.Strcon)
        Dim s1 As New DataSet()
        a1.Fill(s1)


        For Each dr As DataRow In s1.Tables(0).Rows
            Dim kid As String = AutoDoc()
            sql = "insert into link_detail (link_id,doc_id,creation_by,created_date,updated_by,updated_date )"
            sql &= "(select '" & lblId.Text & "',doc_id,'" & sEmpNo & "',getdate(),'" & sEmpNo & "',getdate() "
            sql &= "from link_detail where link_id='" & lblOldLink.Text & "' and doc_id='" & dr(1).ToString() & "')"

            MD.Execute(sql)
        Next

    End Sub
    Private Sub DeleteFile()
        Dim sEmpNo As String = Session("EmpNo")
        Dim strsql As String
        'กรณีลบข้อมูลให้ Update active=0
        strsql = "update import_document set active=0,updated_by='" & sEmpNo & "',updated_date=getdate() where doc_id='" & lblOldId.Text & "'"
        Dim Y As Integer = MD.Execute(strsql)
        If Y > 0 Then
            'MC.MessageBox(Me, "ลบข้อมูลเรียบร้อยแล้ว")
        Else
            MC.MessageBox(Me, "ไม่สามารถลบข้อมูลได้")
        End If
    End Sub
    Private Sub RenameFile()
        Dim strPath As String = "Document_Import\"
        Dim chk As String

        chk = "select * from import_document where doc_id ='" & lblOldId.Text & "'"

        Dim DS As DataSet
        Dim mtype As String = ""
        Dim fname As String = ""
        Dim fname2 As String = ""

        DS = MD.GetDataset(chk)

        If DS.Tables(0).Rows.Count > 0 Then
            mtype = DS.Tables(0).Rows(0).Item("mime_type").ToString
            fname = Func.getServerPath() & DS.Tables(0).Rows(0).Item("file_path").ToString
        End If

        fname2 = Func.getServerPath() & strPath & txtDocId.Text & DS.Tables(0).Rows(0).Item("mime_type").ToString


        Dim fi As New FileInfo(fname)
        fi.CopyTo(fname2)
    End Sub
  
    Protected Sub TabContainer1_ActiveTabChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabContainer1.ActiveTabChanged
        Me.gDataFull(DDTypeSearch.SelectedValue, ddlLawType.SelectedValue)
        Me.MyGridBindFull()

        Me.gDataSelect()
        Me.MyGridBindSelect()
    End Sub
End Class