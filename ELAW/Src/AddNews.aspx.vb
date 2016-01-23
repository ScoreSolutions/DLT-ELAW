Imports System.Data
Imports System.Data.OleDb
Partial Class Src_AddNews
    Inherits System.Web.UI.Page
    Dim MD As New MainData
    Dim MC As New MainClass
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
        'ChkPermis()
        If Not Page.IsPostBack Then
            BindLawNews()
        End If
    End Sub
    Private Sub BindLawNews()
        Try
            Dim sql As String = " SELECT [news_id], [news_title], [news_detail], [creation_by], [created_date], [updated_by], [updated_date] "
            sql &= " FROM [NEWS] ORDER BY [updated_date] Desc "
            Dim DT As DataTable = MD.GetDataTable(sql)
            rptLawNews.DataSource = DT
            rptLawNews.DataBind()
            pnlEnterData.Visible = False
            pnlListData.Visible = True
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alert", "<script>document.write('" & ex.Message & "');</script>", False)
        End Try
    End Sub
    Protected Sub rptLawNews_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.RepeaterCommandEventArgs) Handles rptLawNews.ItemCommand
        If e.CommandName = "Detail" Then
            BindLawNewsByWhere(e.CommandArgument)
        End If

        If e.CommandName = "Delete" Then
            Deletenews(e.CommandArgument)
        End If
    End Sub
    Private Sub Deletenews(ByVal id As Integer)
        Try
            Dim sql As String = " DELETE FROM [NEWS]  "
            sql &= " Where [news_id] = " & id
            Dim DT As DataTable = MD.GetDataTable(sql)
            BindLawNews()
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alert", "<script>document.write('" & ex.Message & "');</script>", False)
        End Try
    End Sub
    Protected Sub rptListNewsDetail_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.RepeaterCommandEventArgs) Handles rptListNewsDetail.ItemCommand
        If e.CommandName = "Back" Then
            BindLawNews()
        End If

        If e.CommandName = "Load" Then
            Dim strsql2 As String = ""
          

            strsql2 = "select * from NEWS "
            strsql2 &= " where news_id='" & e.CommandArgument.ToString & "'"


            Dim dt As DataTable
            dt = MD.GetDataTable(strsql2)

            Dim lblLink As LinkButton = e.Item.FindControl("LinkButtonLoad")

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
    Private Sub BindLawNewsByWhere(ByVal id As Integer)
        Try
            Dim sql As String = " SELECT [news_id], [news_title], [news_detail], [creation_by], [created_date], [updated_by], [updated_date]"
            sql &= " FROM [NEWS] "
            sql &= " WHERE news_id=" & id
            Dim DT As DataTable = MD.GetDataTable(sql)
            If DT.Rows.Count > 0 Then
                lblLawSubject.Text = DT.Rows(0)("news_title")
                rptListNewsDetail.DataSource = DT
                rptListNewsDetail.DataBind()
                pnlEnterData.Visible = True
                pnlListData.Visible = False
            Else
                BindLawNews()
            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alert", "<script>document.write('" & ex.Message & "');</script>", False)
        End Try
    End Sub
    Sub AutoIDFile()
        Dim sqlTmp As String = ""
        sqlTmp = "SELECT top 1 news_id From News order by news_id Desc "

        Dim DT As New DataTable
        DT = MD.GetDataTable(sqlTmp)

        If DT.Rows.Count > 0 Then
            lblID.Text = CInt(DT.Rows(0)("news_id")) + 1
        Else
            lblID.Text = 1
        End If
        DT.Dispose()
    End Sub
    Protected Sub btnCreateELAW_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCreateELAW.Click
        Dim sEmpNo As String = Session("EmpNo")
        Dim strPath As String = "LAWNEWS\"
        If txtELAW.Text = "" Then
            MC.MessageBox(Me, "กรุณาป้อนข้อมูลหัวข้อข่าว")
            Exit Sub
        End If
        If txtDetail.Text = "" Then
            MC.MessageBox(Me, "กรุณาป้อนข้อมูลรายละเอียดข่าว")
            Exit Sub
        End If
        AutoIDFile()
        Try

            Dim Strsql As String
            Strsql = "insert into [NEWS] ([news_id],[news_title], [news_detail], [creation_by], [created_date], [updated_by], [updated_date] "

            If FileUpload.HasFile Then
                Strsql &= ",[file_path],[mime_type] "
            End If

            Strsql &= " ) "
            Strsql &= " values  "
            Strsql &= " ('" & lblID.Text & "','" & txtELAW.Text & "', '" & txtDetail.Text & "','" & sEmpNo & "',getdate(),'" & sEmpNo & "',getdate() "

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

            Strsql &= " ) "


            Dim Y As Integer = MD.Execute(Strsql)
            If Y > 0 Then
                Func.UploadFile("", FileUpload, lblID.Text & MIMEType, strPath)
                MC.MessageBox(Me, "บันทึกลงฐานข้อมูลเรียบร้อยแล้ว")
            End If

            BindLawNews()

        Catch ex As Exception

            MC.MessageBox(Me, ex.ToString)
        End Try

    End Sub
End Class

