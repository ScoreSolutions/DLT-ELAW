Imports System.Data
Imports System.Data.OleDb
Partial Class Src_UploadLaw
    Inherits System.Web.UI.Page
    Dim Conn As OleDbConnection
    Dim Com As OleDbCommand
    Dim TR As OleDbTransaction
    Dim MD As New MainData
    Dim MC As New MainClass

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            DataType()
            AutoIDFile()
            BindData()
        End If
    End Sub

    Sub AutoIDFile()
        Dim sqlTmp As String = ""
        sqlTmp = "SELECT top 1 file_id From Law_Upload order by file_id Desc "

        Dim DT As New DataTable
        DT = MD.GetDataTable(sqlTmp)

        If DT.Rows.Count > 0 Then
            lblID.Text = CInt(DT.Rows(0)("file_id")) + 1
        Else
            lblID.Text = 1
        End If
        DT.Dispose()
    End Sub

    Sub BindData()

        Dim SQL As String
        Dim DT As New DataTable

        SQL = "SELECT l.file_id,l.name,l.detail,w.name wname "
        SQL &= " FROM Law_Upload l inner join law_type_web w on l.law_type=w.id "
        SQL &= " order by l.created_date desc "
        DT = MD.GetDataTable(SQL)

        If DT.Rows.Count <> 0 Then
            GridView1.DataSource = DT
            GridView1.DataBind()

            Dim X1() As String = {"file_id"}
            GridView1.DataKeyNames = X1


        End If
        DT.Dispose()
    End Sub
    Public Sub DataType()

        Dim strsql As String
        strsql = "select id,name from law_type_web order by id  "

        Dim DTS As DataTable = MD.GetDataTable(strsql)
        Dim dr As DataRow = DTS.NewRow
        dr!id = 0
        dr!name = "---------โปรดเลือก---------"
        DTS.Rows.InsertAt(dr, 0)
        ddlLawType.DataTextField = "name"
        ddlLawType.DataValueField = "id"
        ddlLawType.DataSource = DTS
        ddlLawType.DataBind()

    End Sub
    Protected Sub btnCreateELAW_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCreateELAW.Click
        Dim sEmpNo As String = Session("EmpNo")

        If ddlLawType.SelectedValue = "0" Then
            MC.MessageBox(Me, "กรุณาเลือกประเภท")
            Exit Sub
        End If

        If txtELAW.Text = "" Then
            MC.MessageBox(Me, "กรุณาป้อนข้อมูลเรื่อง")
            Exit Sub
        End If
        If txtDetail.Text = "" Then
            MC.MessageBox(Me, "กรุณาป้อนข้อมูลรายละเอียด")
            Exit Sub
        End If

        Dim strPath As String = "LAWUPLOAD\"

        Try

            If FileUpload.PostedFile Is Nothing OrElse String.IsNullOrEmpty(FileUpload.PostedFile.FileName) OrElse FileUpload.PostedFile.InputStream Is Nothing Then
                MC.MessageBox(Me, "กรุณาเลือกไฟล์ที่ต้องการอัพโหลด")
                Exit Sub
            End If

            AutoIDFile()

            Dim Strsql As String
            Strsql = "insert into Law_Upload (file_id,law_type,name,Detail,"

            If FileUpload.HasFile Then
                Strsql &= "file_path,mime_type "
            End If

            Strsql &= ",created_date,creation_by )"
            Strsql &= " values  "
            Strsql &= " ('" & lblID.Text & "','" & ddlLawType.SelectedValue & "','" & txtELAW.Text & "', '" & txtDetail.Text & "'"

            Dim MIMEType As String = Nothing
            If FileUpload.HasFile Then
                Dim extension As String = System.IO.Path.GetExtension(FileUpload.PostedFile.FileName).ToLower()

                Select Case extension
                    Case ".jpg", ".jpeg", ".jpe"
                        MIMEType = ".jpg"
                        Strsql &= ",'" & strPath & "" & lblID.Text & "" & MIMEType & "','" & MIMEType & "'"
                    Case ".csv"
                        MIMEType = ".csv"
                        Strsql &= ",'" & strPath & "" & lblID.Text & "" & MIMEType & "','" & MIMEType & "'"
                    Case ".xls"
                        MIMEType = ".xls"
                        Strsql &= ",'" & strPath & "" & lblID.Text & "" & MIMEType & "','" & MIMEType & "'"
                    Case ".xlsx"
                        MIMEType = ".xlsx"
                        Strsql &= ",'" & strPath & "" & lblID.Text & "" & MIMEType & "','" & MIMEType & "'"
                    Case ".pdf"
                        MIMEType = ".pdf"
                        Strsql &= ",'" & strPath & "" & lblID.Text & "" & MIMEType & "','" & MIMEType & "'"
                    Case ".doc"
                        MIMEType = ".doc"
                        Strsql &= ",'" & strPath & "" & lblID.Text & "" & MIMEType & "','" & MIMEType & "'"
                    Case ".docx"
                        MIMEType = ".docx"
                        Strsql &= ",'" & strPath & "" & lblID.Text & "" & MIMEType & "','" & MIMEType & "'"
                    Case ".txt"
                        MIMEType = ".txt"
                        Strsql &= ",'" & strPath & "" & lblID.Text & "" & MIMEType & "','" & MIMEType & "'"
                    Case ".htm", ".html"
                        MIMEType = ".html"
                        Strsql &= ",'" & strPath & "" & lblID.Text & "" & MIMEType & "','" & MIMEType & "'"
                    Case Else
                        MC.MessageBox(Me, "Not a valid file format")
                        Exit Sub
                End Select
            End If

            Strsql &= " ,getdate(),99)"


            Dim Y As Integer = MD.Execute(Strsql)
            If Y > 0 Then
                Func.UploadFile("", FileUpload, lblID.Text & MIMEType, strPath)
                MC.MessageBox(Me, "บันทึกลงฐานข้อมูลเรียบร้อยแล้ว")
            End If

            Response.Redirect("UploadLaw.aspx")

        Catch ex As Exception
            MC.MessageBox(Me, ex.ToString)
        End Try

    End Sub
    Protected Sub GridView1_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridView1.PageIndexChanging
        GridView1.PageIndex = e.NewPageIndex
        BindData()
    End Sub
    Protected Sub GridView1_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GridView1.RowDeleting
        'Delete Document
        Dim K As DataKey = GridView1.DataKeys(e.RowIndex)
        Dim strPath As String = Constant.BaseURL(Request) & ("LAWUPLOAD\")
        Dim strsql As String
        Dim chk As String

        chk = "select * from Law_Upload where file_id=" & K(0).ToString
        Dim DS As DataSet
        Dim mtype As String = ""
        Dim fname As String = ""
        DS = MD.GetDataset(chk)

        If DS.Tables(0).Rows.Count > 0 Then
            mtype = DS.Tables(0).Rows(0).Item("mime_type").ToString
            fname = DS.Tables(0).Rows(0).Item("file_id").ToString
            fname = Func.getServerPath() & DS.Tables(0).Rows(0).Item("file_path").ToString

        End If

        strsql = "delete from Law_Upload where file_id=" & K(0).ToString
        Dim Y As Integer = MD.Execute(strsql)
        If Y > 0 Then
            BindData()
            Func.DeleteFile(fname)

            MC.MessageBox(Me, "ลบข้อมูลเรียบร้อยแล้ว")
            Response.Redirect("UploadLaw.aspx")
        Else
            MC.MessageBox(Me, "ไม่สามารถลบข้อมูลได้")
        End If
    End Sub
    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        'Download File Document
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim strsql2 As String = ""
            Dim K2 As String
            K2 = Convert.ToString(GridView1.DataKeys(e.Row.RowIndex).Values(0).ToString())

            strsql2 = "select * from Law_Upload"
            strsql2 &= " where file_id='" & K2 & "'"


            Dim dt As DataTable
            dt = MD.GetDataTable(strsql2)

            Dim lblLink As Label = e.Row.Cells(2).FindControl("lblLink")

            For Each dr As DataRow In dt.Rows


                If dr("mime_type").ToString() = ".pdf" Or dr("mime_type").ToString() = ".txt" Then
                    Dim strUrl As String = "http://" & Constant.BaseURL(Request) & "" & dr("file_path").ToString() & "?time=" & Date.Now.ToString("HH:mm:ss")

                    lblLink.Text = "<a href='" & strUrl & "' Target='_Blank'>ดาวน์โหลด</a>&nbsp;&nbsp;"

                Else

                    Dim strUrl As String = "http://" & Constant.BaseURL(Request) & dr("file_path").ToString() & "?time=" & Date.Now.ToString("HH:mm:ss")
                    lblLink.Text = "<a href='" & strUrl & "' Target='_Blank'>ดาวน์โหลด</a>&nbsp;&nbsp;"

                End If

            Next

        End If

    End Sub
    Protected Sub GridView1_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowCreated
        'Create Page Gridview

        If e.Row.RowType = DataControlRowType.DataRow Then

            If Not e.Row.RowState And DataControlRowState.Edit Then
                Dim L1 As ImageButton = e.Row.Cells(3).Controls(1)
                L1.OnClientClick = "return confirm('คุณต้องการลบข้อมูลใช่หรือไม่?');"
            End If
        End If
    End Sub
End Class
