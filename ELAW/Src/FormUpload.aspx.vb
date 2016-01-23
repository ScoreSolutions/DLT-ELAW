Imports System.Data
Imports System.Data.OleDb
Imports System.IO
Partial Class Src_FormUpload
    Inherits System.Web.UI.Page
    Dim MD As New MainData
    Dim MC As New MainClass
    Dim DV As DataView
    Public iRow As Integer
    Dim DS As DataSet
    Dim iRec As Integer
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

        Me.ChkPermis()

        If Not Page.IsPostBack Then
            ViewState("sortfield") = "form_name"
            ViewState("sortdirection") = "asc"
            Me.gData()
            Me.MyGridBind()


        Else
            If Session("form_download ") Is Nothing Then
                Me.gData()
            Else
                DV = Session("form_download")
            End If
        End If
    End Sub
    Private Sub Auto()

        Dim sqlTmp As String = ""
        Dim comTmp As OleDbCommand = New OleDbCommand
        Dim drTmp As OleDbDataReader
        Dim tmpMemberID As Integer = 0
        Dim tmpMemberID2 As String = ""


        sqlTmp = "SELECT TOP 1 form_id FROM form_download "
        sqlTmp &= " ORDER BY form_id DESC"

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

                tmpMemberID2 = drTmp.Item("form_id")

                tmpMemberID = CInt(tmpMemberID2) + 1
                lblId.Text = tmpMemberID.ToString

            End With
        Catch
            lblId.Text = "1"
        End Try
        cn.Close()

    End Sub
    Private Sub gData()

        Dim strsql As String
        strsql = "select f.form_id,f.form_name,e.firstname+' '+e.lastname creation_by,f.created_date,f.detail,  "
        strsql &= " e1.firstname+' '+e1.lastname updated_by,f.updated_date,f.file_path,f.mime_type "
        strsql &= " from form_download f inner join employee e "
        strsql &= " on f.creation_by=e.empid "
        strsql &= " inner join employee e1 "
        strsql &= " on f.updated_by=e1.empid "

       

        If txtStatus.Text <> "" Then
            strsql &= "and f.form_name like '%" & txtStatus.Text & "%' "
        End If

        If txtStatus0.Text <> "" Then
            strsql &= "and f.detail like '%" & txtStatus0.Text & "%' "
        End If



        Dim DT As DataTable = MD.GetDataTable(strsql)
        DV = DT.DefaultView
        DV.Sort = "[" & ViewState("sortfield") & "] " & ViewState("sortdirection")
        Session("form_download") = DV
    End Sub
    Private Sub MyGridBind()
        GridView1.DataSource = DV
        Dim X1() As String = {"form_id"}
        GridView1.DataKeyNames = X1
        GridView1.DataBind()
    End Sub
    Protected Sub GridView1_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridView1.PageIndexChanging
        GridView1.PageIndex = e.NewPageIndex
        Me.MyGridBind()

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
            If Not e.Row.RowState And DataControlRowState.Edit Then
                Dim L1 As ImageButton = e.Row.Cells(8).Controls(1)
                L1.OnClientClick = "return confirm('คุณต้องการลบข้อมูลใช่หรือไม่?');"
            End If
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
    Protected Sub GridView1_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GridView1.RowDeleting

        Dim K1 As DataKey = GridView1.DataKeys(e.RowIndex)
        Dim strPath As String = Server.MapPath("..\Document\Form\")
        Dim strsql As String
        Dim chk As String

        chk = "select * from form_download where form_id='" & K1(0) & "'"
        Dim DS As DataSet
        Dim mtype As String = ""
        DS = MD.GetDataset(chk)

        If DS.Tables(0).Rows.Count > 0 Then
            mtype = DS.Tables(0).Rows(0).Item("mime_type").ToString
        End If

        strsql = "delete from form_download where form_id ='" & K1(0) & "'"
        Dim Y As Integer = MD.Execute(strsql)
        If Y > 0 Then
            File.Delete(strPath & K1(0).ToString & mtype)

            Me.gData()
            Me.MyGridBind()
            MC.MessageBox(Me, "ลบข้อมูลเรียบร้อยแล้ว")
        Else
            MC.MessageBox(Me, "ไม่สามารถลบข้อมูลได้")
        End If
    End Sub
    Protected Sub GridView1_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles GridView1.RowEditing
        Dim K1 As DataKey = GridView1.DataKeys(e.NewEditIndex)
        Dim lName As Label = GridView1.Rows(e.NewEditIndex).Cells(0).Controls(1)
        Dim lDetail As Label = GridView1.Rows(e.NewEditIndex).Cells(1).Controls(1)

        lblId.Text = K1(0).ToString
        txtStatus.Text = lName.Text
        txtStatus0.Text = lDetail.Text
        lblStatus.Text = "Edit"
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
    Protected Sub bSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bSave.Click
        Dim strsql As String = ""
        Dim sEmpNo As String = Session("EmpNo")
        Dim strPath As String = "Document\Form\"

        If txtStatus.Text.Trim = "" Then
            MC.MessageBox(Me, "กรุณากรอกข้อมูล")
            txtStatus.Focus()
            Exit Sub
        End If

        

        If lblStatus.Text = "Edit" Then

            strsql = "update form_download set "
            strsql &= "form_name='" & txtStatus.Text & "',detail='" & txtStatus0.Text & "' "

            If FileUpload1.HasFile Then
                Dim extension As String = System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName).ToLower()
                Dim MIMEType As String = Nothing
                Select Case extension
                    Case ".jpg", ".jpeg", ".jpe"
                        MIMEType = ".jpg"
                        strsql &= ",file_path='" & strPath & "" & lblId.Text & "" & MIMEType & "',mime_type='" & MIMEType & "'"
                    Case ".csv"
                        MIMEType = ".csv"
                        strsql &= ",file_path='" & strPath & "" & lblId.Text & "" & MIMEType & "',mime_type='" & MIMEType & "'"
                    Case ".xls"
                        MIMEType = ".xls"
                        strsql &= ",file_path='" & strPath & "" & lblId.Text & "" & MIMEType & "',mime_type='" & MIMEType & "'"
                    Case ".xlsx"
                        MIMEType = ".xlsx"
                        strsql &= ",file_path='" & strPath & "" & lblId.Text & "" & MIMEType & "',mime_type='" & MIMEType & "'"
                    Case ".pdf"
                        MIMEType = ".pdf"
                        strsql &= ",file_path='" & strPath & "" & lblId.Text & "" & MIMEType & "',mime_type='" & MIMEType & "'"
                    Case ".doc"
                        MIMEType = ".doc"
                        strsql &= ",file_path='" & strPath & "" & lblId.Text & "" & MIMEType & "',mime_type='" & MIMEType & "'"
                    Case ".docx"
                        MIMEType = ".docx"
                        strsql &= ",file_path='" & strPath & "" & lblId.Text & "" & MIMEType & "',mime_type='" & MIMEType & "'"
                    Case ".txt"
                        MIMEType = ".txt"
                        strsql &= ",file_path='" & strPath & "" & lblId.Text & "" & MIMEType & "',mime_type='" & MIMEType & "'"
                    Case ".htm", ".html"
                        MIMEType = ".html"
                        strsql &= ",file_path='" & strPath & "" & lblId.Text & "" & MIMEType & "',mime_type='" & MIMEType & "'"
                    Case Else
                        MC.MessageBox(Me, "Not a valid file format")
                        Exit Sub
                End Select
            End If

            strsql &= ",updated_by='" & sEmpNo & "',updated_date=getdate() "
            strsql &= "where form_id='" & lblId.Text & "'"

            Dim Y As Integer = MD.Execute(strsql)
            If Y > 0 Then
                Me.UploadFile()
                MC.MessageBox(Me, "บันทึกลงฐานข้อมูลเรียบร้อยแล้ว")
                Me.gData()
                Me.MyGridBind()
            Else
                MC.MessageBox(Me, "ไม่สามารถบันทึกลงฐานข้อมูลได้")
            End If



        Else

            If FileUpload1.PostedFile Is Nothing OrElse String.IsNullOrEmpty(FileUpload1.PostedFile.FileName) OrElse FileUpload1.PostedFile.InputStream Is Nothing Then
                MC.MessageBox(Me, "กรุณาเลือกไฟล์ที่ต้องการอัพโหลด")
                Exit Sub
            End If

            Me.Auto()

            strsql = "insert into form_download (form_id,form_name,detail,file_path,mime_type,creation_by,created_date,updated_by,updated_date) "
            strsql &= " values ('" & lblId.Text & "','" & txtStatus.Text & "','" & txtStatus0.Text & "' "

            If FileUpload1.HasFile Then
                Dim extension As String = System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName).ToLower()
                Dim MIMEType As String = Nothing
                Select Case extension
                    Case ".jpg", ".jpeg", ".jpe"
                        MIMEType = ".jpg"
                        strsql &= ",'" & strPath & "" & lblId.Text & "" & MIMEType & "','" & MIMEType & "'"
                    Case ".csv"
                        MIMEType = ".csv"
                        strsql &= ",'" & strPath & "" & lblId.Text & "" & MIMEType & "','" & MIMEType & "'"
                    Case ".xls"
                        MIMEType = ".xls"
                        strsql &= ",'" & strPath & "" & lblId.Text & "" & MIMEType & "','" & MIMEType & "'"
                    Case ".xlsx"
                        MIMEType = ".xlsx"
                        strsql &= ",'" & strPath & "" & lblId.Text & "" & MIMEType & "','" & MIMEType & "'"
                    Case ".pdf"
                        MIMEType = ".pdf"
                        strsql &= ",'" & strPath & "" & lblId.Text & "" & MIMEType & "','" & MIMEType & "'"
                    Case ".doc"
                        MIMEType = ".doc"
                        strsql &= ",'" & strPath & "" & lblId.Text & "" & MIMEType & "','" & MIMEType & "'"
                    Case ".docx"
                        MIMEType = ".docx"
                        strsql &= ",'" & strPath & "" & lblId.Text & "" & MIMEType & "','" & MIMEType & "'"
                    Case ".txt"
                        MIMEType = ".txt"
                        strsql &= ",'" & strPath & "" & lblId.Text & "" & MIMEType & "','" & MIMEType & "'"
                    Case ".htm", ".html"
                        MIMEType = ".html"
                        strsql &= ",'" & strPath & "" & lblId.Text & "" & MIMEType & "','" & MIMEType & "'"
                    Case Else
                        MC.MessageBox(Me, "Not a valid file format")
                        Exit Sub
                End Select
            End If

            strsql &= ",'" & sEmpNo & "',getdate(),'" & sEmpNo & "',getdate())"

            Dim Y As Integer = MD.Execute(strsql)
            If Y > 0 Then
                Me.UploadFile()
                MC.MessageBox(Me, "บันทึกลงฐานข้อมูลเรียบร้อยแล้ว")
                Me.gData()
                Me.MyGridBind()
            Else
                MC.MessageBox(Me, "ไม่สามารถบันทึกลงฐานข้อมูลได้")
            End If

        End If
        lblId.Text = ""
        txtStatus.Text = ""
        txtStatus0.Text = ""
        lblStatus.Text = ""
    End Sub
    Private Sub UploadFile()
        If FileUpload1.HasFile Then
            Dim extension As String = System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName).ToLower()
            Dim MIMEType As String = Nothing

            Try

                Select Case extension

                    Case ".jpg", ".jpeg", ".jpe"
                        MIMEType = ".jpg"
                        Dim X As String = Path.GetFileName("" & lblId.Text & "" & MIMEType & "")
                        X = Server.MapPath("..\Document\Form\" & X)
                        FileUpload1.PostedFile.SaveAs(X)
                    Case ".csv"
                        MIMEType = ".csv"
                        Dim X As String = Path.GetFileName("" & lblId.Text & "" & MIMEType & "")
                        X = Server.MapPath("..\Document\Form\" & X)
                        FileUpload1.PostedFile.SaveAs(X)
                    Case ".xls"
                        MIMEType = ".xls"
                        Dim X As String = Path.GetFileName("" & lblId.Text & "" & MIMEType & "")
                        X = Server.MapPath("..\Document\Form\" & X)
                        FileUpload1.PostedFile.SaveAs(X)
                    Case ".xlsx"
                        MIMEType = ".xlsx"
                        Dim X As String = Path.GetFileName("" & lblId.Text & "" & MIMEType & "")
                        X = Server.MapPath("..\Document\Form\" & X)
                        FileUpload1.PostedFile.SaveAs(X)
                    Case ".pdf"
                        MIMEType = ".pdf"
                        Dim X As String = Path.GetFileName("" & lblId.Text & "" & MIMEType & "")
                        X = Server.MapPath("..\Document\Form\" & X)
                        FileUpload1.PostedFile.SaveAs(X)
                    Case ".doc"
                        MIMEType = ".doc"
                        Dim X As String = Path.GetFileName("" & lblId.Text & "" & MIMEType & "")
                        X = Server.MapPath("..\Document\Form\" & X)
                        FileUpload1.PostedFile.SaveAs(X)
                    Case ".docx"
                        MIMEType = ".docx"
                        Dim X As String = Path.GetFileName("" & lblId.Text & "" & MIMEType & "")
                        X = Server.MapPath("..\Document\Form\" & X)
                        FileUpload1.PostedFile.SaveAs(X)
                    Case ".txt"
                        MIMEType = ".txt"
                        Dim X As String = Path.GetFileName("" & lblId.Text & "" & MIMEType & "")
                        X = Server.MapPath("..\Document\Form\" & X)
                        FileUpload1.PostedFile.SaveAs(X)
                    Case ".htm", ".html"
                        MIMEType = ".html"
                        Dim X As String = Path.GetFileName("" & lblId.Text & "" & MIMEType & "")
                        X = Server.MapPath("..\Document\Form\" & X)
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
    Protected Sub bCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bCancel.Click
        txtStatus.Text = ""
        txtStatus0.Text = ""
        lblStatus.Text = ""

    End Sub


    Protected Sub bSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bSearch.Click
        Me.gData()
        Me.MyGridBind()
    End Sub
End Class
