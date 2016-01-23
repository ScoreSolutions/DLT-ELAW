Imports System.IO
Imports System.Data
Imports System.Data.OleDb
Partial Class Src_CaseAttDecision
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

            'Show Gridview Document
            Me.gDataDoc()
            Me.MyGridBind()

        Else

            If Session("DocumentCaseDecision") Is Nothing Then
                Me.gDataDoc()
            Else
                DVLst = Session("DocumentCaseDecision")
            End If

        End If

        txtDocPage.Attributes.Add("onkeypress", "return blockNonNumbers(this, event,-1,0);")

    End Sub
    Private Sub gDataDoc(Optional ByVal Type As String = "")
        'Data in Gridview (Table CASE_DOCUMENT)
        Dim X As String = Request.QueryString("id")
        Dim strsql As String

        strsql = " select d.document_id,d.title,d.page  "
        strsql &= "from case_document d "
        strsql &= "where d.case_id='" & X & "' and d.decision='T' "

        Dim DT As DataTable = MD.GetDataTable(strsql)
        DVLst = DT.DefaultView
        Session("DocumentCaseDecision") = DVLst
    End Sub
    Private Sub MyGridBind()
        GridView1.DataSource = DVLst
        Dim X1() As String = {"document_id"}
        GridView1.DataKeyNames = X1
        GridView1.DataBind()
    End Sub
    Protected Sub bSaveFile_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bSaveFile.Click
        Dim X As String = Request.QueryString("id")
        Dim No As String = Request.QueryString("no")
        Dim sEmpNo As String = Session("EmpNo")
        Dim strPath As String = "Document\Case\"

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
                        Strsql &= ",file_path='" & strPath & "" & X & "-" & lblDocId.Text & "" & MIMEType & "',mime_type='" & MIMEType & "'"
                    Case ".csv"
                        MIMEType = ".csv"
                        Strsql &= ",file_path='" & strPath & "" & X & "-" & lblDocId.Text & "" & MIMEType & "',mime_type='" & MIMEType & "'"
                    Case ".xls"
                        MIMEType = ".xls"
                        Strsql &= ",file_path='" & strPath & "" & X & "-" & lblDocId.Text & "" & MIMEType & "',mime_type='" & MIMEType & "'"
                    Case ".xlsx"
                        MIMEType = ".xlsx"
                        Strsql &= ",file_path='" & strPath & "" & X & "-" & lblDocId.Text & "" & MIMEType & "',mime_type='" & MIMEType & "'"
                    Case ".pdf"
                        MIMEType = ".pdf"
                        Strsql &= ",file_path='" & strPath & "" & X & "-" & lblDocId.Text & "" & MIMEType & "',mime_type='" & MIMEType & "'"
                    Case ".doc"
                        MIMEType = ".doc"
                        Strsql &= ",file_path='" & strPath & "" & X & "-" & lblDocId.Text & "" & MIMEType & "',mime_type='" & MIMEType & "'"
                    Case ".docx"
                        MIMEType = ".docx"
                        Strsql &= ",file_path='" & strPath & "" & X & "-" & lblDocId.Text & "" & MIMEType & "',mime_type='" & MIMEType & "'"
                    Case ".txt"
                        MIMEType = ".txt"
                        Strsql &= ",file_path='" & strPath & "" & X & "-" & lblDocId.Text & "" & MIMEType & "',mime_type='" & MIMEType & "'"
                    Case ".htm", ".html"
                        MIMEType = ".html"
                        Strsql &= ",file_path='" & strPath & "" & X & "-" & lblDocId.Text & "" & MIMEType & "',mime_type='" & MIMEType & "'"
                    Case Else
                        MC.MessageBox(Me, "Not a valid file format")
                        Exit Sub
                End Select

                Dim Xid As String = Request.QueryString("id")
                Dim fname As String = Xid + "-" + lblDocId.Text
                Func.UploadFile(sEmpNo, FileUpload1, fname & MIMEType, strPath)

            End If


            Strsql &= ",decision='T',creation_by='" & sEmpNo & "',created_date=getdate(),updated_by='" & sEmpNo & "',updated_date=getdate() "
            Strsql &= " where case_id='" & X & "' and document_id ='" & lblDocId.Text & "'"


            Dim Y As Integer = MD.Execute(Strsql)
            If Y > 0 Then
                'If FileUpload1.HasFile Then
                '    'Me.UploadFile()
                '    Dim Xid As String = Request.QueryString("id")
                '    Dim extension As String = System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName).ToLower()
                '    Dim MIMEType As String = Nothing
                '    Dim fname As String = Xid + "-" + lblDocId.Text
                '    Func.UploadFile(sEmpNo, FileUpload1, fname & MIMEType, strPath)
                'End If

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

                Strsql &= ",title,page,decision,creation_by,created_date,updated_by,updated_date)values  "
                Strsql &= "('" & X & "','" & No & "','" & lblDocId.Text & "' "

                If FileUpload1.HasFile Then
                    Dim extension As String = System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName).ToLower()
                    Dim MIMEType As String = Nothing

                    Select Case extension
                        Case ".jpg", ".jpeg", ".jpe"
                            MIMEType = ".jpg"
                            Strsql &= ",'" & strPath & "" & X & "-" & lblDocId.Text & "" & MIMEType & "','" & MIMEType & "'"
                        Case ".csv"
                            MIMEType = ".csv"
                            Strsql &= ",'" & strPath & "" & X & "-" & lblDocId.Text & "" & MIMEType & "','" & MIMEType & "'"
                        Case ".xls"
                            MIMEType = ".xls"
                            Strsql &= ",'" & strPath & "" & X & "-" & lblDocId.Text & "" & MIMEType & "','" & MIMEType & "'"
                        Case ".xlsx"
                            MIMEType = ".xlsx"
                            Strsql &= ",'" & strPath & "" & X & "-" & lblDocId.Text & "" & MIMEType & "','" & MIMEType & "'"
                        Case ".pdf"
                            MIMEType = ".pdf"
                            Strsql &= ",'" & strPath & "" & X & "-" & lblDocId.Text & "" & MIMEType & "','" & MIMEType & "'"
                        Case ".doc"
                            MIMEType = ".doc"
                            Strsql &= ",'" & strPath & "" & X & "-" & lblDocId.Text & "" & MIMEType & "','" & MIMEType & "'"
                        Case ".docx"
                            MIMEType = ".docx"
                            Strsql &= ",'" & strPath & "" & X & "-" & lblDocId.Text & "" & MIMEType & "','" & MIMEType & "'"
                        Case ".txt"
                            MIMEType = ".txt"
                            Strsql &= ",'" & strPath & "" & X & "-" & lblDocId.Text & "" & MIMEType & "','" & MIMEType & "'"
                        Case ".htm", ".html"
                            MIMEType = ".html"
                            Strsql &= ",'" & strPath & "" & X & "-" & lblDocId.Text & "" & MIMEType & "','" & MIMEType & "'"
                        Case Else
                            MC.MessageBox(Me, "Not a valid file format")
                            Exit Sub
                    End Select
                    Dim Xid As String = Request.QueryString("id")
                    Dim fname As String = Xid + "-" + lblDocId.Text
                    Func.UploadFile(sEmpNo, FileUpload1, fname & MIMEType, strPath)
                End If

                Strsql &= ",'" & txtDocDetail.Text & "','" & txtDocPage.Text & "','T','" & sEmpNo & "',getdate(),"
                Strsql &= "'" & sEmpNo & "',getdate())"


                Dim Y As Integer = MD.Execute(Strsql)
                If Y > 0 Then
                    'Me.UploadFile()

                    'If FileUpload1.HasFile Then
                    '    'Me.UploadFile()
                    '    Dim Xid As String = Request.QueryString("id")
                    '    Dim extension As String = System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName).ToLower()
                    '    Dim MIMEType As String = Nothing
                    '    Dim fname As String = Xid + "-" + lblDocId.Text

                    '    Func.UploadFile(sEmpNo, FileUpload1, fname & MIMEType, strPath)
                    'End If

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
    Private Sub AutoFile()
        'Genarate Document Id
        Dim X As String = Request.QueryString("id")
        Dim sqlTmp As String = ""
        Dim comTmp As OleDbCommand = New OleDbCommand
        Dim drTmp As OleDbDataReader
        Dim tmpMemberID As Integer = 0
        Dim tmpMemberID2 As String = ""

        sqlTmp = "SELECT TOP 1 document_id FROM case_document "
        sqlTmp &= " WHERE case_id ='" & X & "'"
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
    Private Sub UploadFile()
        'Upload and save file at directory
        Dim Xid As String = Request.QueryString("id")
        If FileUpload1.HasFile Then
            Dim extension As String = System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName).ToLower()
            Dim MIMEType As String = Nothing
            Dim fname As String = Xid + "-" + lblDocId.Text
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
                Dim L1 As ImageButton = e.Row.Cells(3).Controls(1)
                L1.OnClientClick = "return confirm('คุณต้องการลบข้อมูลใช่หรือไม่?');"
            End If
        End If


    End Sub
    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        'Download File Document
        Dim Xid As String = Request.QueryString("id")
        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim K2 As String
            K2 = Convert.ToString(GridView1.DataKeys(e.Row.RowIndex).Values(0).ToString())
            Dim strsql2 As String = ""


            strsql2 = "select d.case_id,d.document_id, "
            strsql2 &= "d.file_path,d.mime_type "
            strsql2 &= "from case_document d "
            strsql2 &= "where case_id='" & Xid & "' and d.document_id='" & K2(0) & "'"


            Dim dt As DataTable
            dt = MD.GetDataTable(strsql2)

            Dim lblLink As Label = e.Row.Cells(2).FindControl("lblLink")

            For Each dr As DataRow In dt.Rows

                If dr("mime_type").ToString() = ".pdf" Or dr("mime_type").ToString() = ".txt" Then
                    Dim strUrl As String = "http://" & Constant.BaseURL(Request) & dr("file_path").ToString()
                    lblLink.Text = "<a href='" & strUrl & "' Target='_Blank'>ดาวน์โหลด</a>&nbsp;&nbsp;"

                Else
                    Dim strUrl As String = "http://" & Constant.BaseURL(Request) & dr("file_path").ToString()
                    lblLink.Text = "<a href=" & strUrl & ">ดาวน์โหลด</a>&nbsp;&nbsp;"

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
    Protected Sub GridView1_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GridView1.RowDeleting
        'Delete Document
        Dim Xid As String = Request.QueryString("id")
        Dim strPath As String = Constant.BaseURL(Request) & ("Document\Case\")
        Dim K1 As DataKey = GridView1.DataKeys(e.RowIndex)
        Dim strsql As String
        Dim chk As String

        chk = "select * from case_document where case_id='" & Xid & "' and document_id ='" & K1(0) & "'"
        Dim DS As DataSet
        Dim mtype As String = ""
        Dim fname As String = ""
        DS = MD.GetDataset(chk)

        If DS.Tables(0).Rows.Count > 0 Then
            mtype = DS.Tables(0).Rows(0).Item("mime_type").ToString
            fname = DS.Tables(0).Rows(0).Item("case_id").ToString + "-" + DS.Tables(0).Rows(0).Item("document_id").ToString
            fname = Func.getServerPath() & DS.Tables(0).Rows(0).Item("file_path").ToString

        End If

        strsql = "delete from case_document where case_id='" & Xid & "' and document_id ='" & K1(0) & "'"
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
    Protected Sub link2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles link2.Click
        Response.Redirect("../Src/CaseDecisionList.aspx", True)
    End Sub
End Class
