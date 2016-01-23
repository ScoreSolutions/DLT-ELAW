Imports System.Data
Imports System.Data.OleDb
Partial Class _Default
    Inherits System.Web.UI.Page
    Dim MD As New MainData
    Dim MC As New MainClass
    Dim DVLst1 As DataView
    Dim DVLst2 As DataView
    Dim DVLst3 As DataView
    Dim DVLst4 As DataView
    Public iRow As Integer
    Dim DS As DataSet
    Dim iRec As Integer
    Public Function ImagesGet(ByVal X As String) As String
        Dim X1 As String = Replace(X, " ", "_")
        Dim X2 As String = "Images\" & X1 & ".gif"
        Dim xFile As String = Server.MapPath(X2)

        If IO.File.Exists(xFile) Then
            Return "<img src='" & Replace(X2, "\", "/") & "' align='absmiddle'>"
        Else
            Return ""
        End If
    End Function
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            'ดึงข้อมูลแสดงใน Repeater
            BindLawDraft()
            BindLawArticle()
            BindLaw1()
            BindLaw2()
            BindLaw3()
            BindLaw4()
            BindLawCase()
            BindLawNews()
        End If
    End Sub
    Private Sub BindLawEnforcement()
        'ยังไม่ได้ข้อมูล
    End Sub
    Private Sub BindLawDraft()
        Try
            Dim sql As String = " SELECT TOP 6 [subject_id], [subject_desc], [subject_day], [created_user], [created_date]"
            sql &= " FROM [SUBJ] "
            sql &= " where convert(nvarchar(10),created_date+subject_day,120) > convert(nvarchar(10),getdate(),120) "
            sql &= " and active=1 "
            sql &= " ORDER BY [created_date] Desc "

            Dim DT As DataTable = MD.GetDataTable(sql)
            If DT.Rows.Count > 0 Then
                rptLawDraft.DataSource = DT
                rptLawDraft.DataBind()
            End If

        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alert", "<script>document.write('" & ex.Message & "');</script>", False)
        End Try
    End Sub
    Private Sub BindLawArticle()
        Try
            Dim sql As String = " SELECT TOP 6 [file_id], [name], [file_path], [creation_by], [created_date], [updated_by], [updated_date] "
            sql &= " FROM [FILE_UPLOAD] ORDER BY [created_date] Desc "
            Dim DT As DataTable = MD.GetDataTable(sql)

            If DT.Rows.Count > 0 Then
                rptLawArticle.DataSource = DT
                rptLawArticle.DataBind()
            End If

        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alert", "<script>document.write('" & ex.Message & "');</script>", False)
        End Try
    End Sub
    Private Sub BindLaw1()
        Try
            Dim sql As String = " SELECT TOP 3 [file_id], [name]+'('+[detail]+')' name, [file_path], "
            sql &= " [creation_by], [created_date], [updated_by], [updated_date], "
            sql &= " case when CONVERT(nvarchar(10),[created_date]+14,120) < CONVERT(nvarchar(10),GETDATE(),120) then '' else  'new' end chk "
            sql &= " FROM [LAW_UPLOAD] WHERE [law_type]=1 ORDER BY [created_date] Desc "
            Dim DT As DataTable = MD.GetDataTable(sql)

            If DT.Rows.Count > 0 Then
                RepeaterLaw1.DataSource = DT
                RepeaterLaw1.DataBind()
            Else
                Label1.Visible = False
            End If

        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alert", "<script>document.write('" & ex.Message & "');</script>", False)
        End Try
    End Sub
    Private Sub BindLaw2()
        Try
            Dim sql As String = " SELECT TOP 3 [file_id], [name]+'('+[detail]+')' name, [file_path], "
            sql &= " [creation_by], [created_date], [updated_by], [updated_date], "
            sql &= " case when CONVERT(nvarchar(10),[created_date]+14,120) < CONVERT(nvarchar(10),GETDATE(),120) then '' else  'new' end chk "
            sql &= " FROM [LAW_UPLOAD] WHERE [law_type]=2 ORDER BY [created_date] Desc "
            Dim DT As DataTable = MD.GetDataTable(sql)

            If DT.Rows.Count > 0 Then
                RepeaterLaw2.DataSource = DT
                RepeaterLaw2.DataBind()
            Else
                Label2.Visible = False

            End If

        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alert", "<script>document.write('" & ex.Message & "');</script>", False)
        End Try
    End Sub
    Private Sub BindLaw3()
        Try
            Dim sql As String = " SELECT TOP 3 [file_id], [name]+'('+[detail]+')' name, [file_path], "
            sql &= " [creation_by], [created_date], [updated_by], [updated_date], "
            sql &= " case when CONVERT(nvarchar(10),[created_date]+14,120) < CONVERT(nvarchar(10),GETDATE(),120) then '' else  'new' end chk "
            sql &= " FROM [LAW_UPLOAD] WHERE [law_type]=3 ORDER BY [created_date] Desc "
            Dim DT As DataTable = MD.GetDataTable(sql)

            If DT.Rows.Count > 0 Then
                RepeaterLaw3.DataSource = DT
                RepeaterLaw3.DataBind()
            Else
                Label3.Visible = False

            End If

        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alert", "<script>document.write('" & ex.Message & "');</script>", False)
        End Try
    End Sub
    Private Sub BindLaw4()
        Try
            Dim sql As String = " SELECT TOP 3 [file_id], [name]+'('+[detail]+')' name, [file_path], "
            sql &= " [creation_by], [created_date], [updated_by], [updated_date], "
            sql &= " case when CONVERT(nvarchar(10),[created_date]+14,120) < CONVERT(nvarchar(10),GETDATE(),120) then '' else  'new' end chk "
            sql &= " FROM [LAW_UPLOAD] WHERE [law_type]=4 ORDER BY [created_date] Desc "
            Dim DT As DataTable = MD.GetDataTable(sql)

            If DT.Rows.Count > 0 Then
                RepeaterLaw4.DataSource = DT
                RepeaterLaw4.DataBind()
            Else
                Label4.Visible = False

            End If

        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alert", "<script>document.write('" & ex.Message & "');</script>", False)
        End Try
    End Sub
    Private Sub BindLawCase()
        Try
            Dim sql As String = " SELECT TOP 6 [file_id], [name]+'('+[detail]+')' name, [file_path], [creation_by], [created_date], [updated_by],                     [updated_date], "
            sql &= " case when CONVERT(nvarchar(10),[created_date]+14,120) < CONVERT(nvarchar(10),GETDATE(),120) then '' else  'new' end chk "

            sql &= " FROM [CASE_UPLOAD] ORDER BY [created_date] Desc "
            Dim DT As DataTable = MD.GetDataTable(sql)

            If DT.Rows.Count > 0 Then
                RepeaterCase.DataSource = DT
                RepeaterCase.DataBind()
            End If

        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alert", "<script>document.write('" & ex.Message & "');</script>", False)
        End Try
    End Sub
    Private Sub BindLawNews()
        Try
            Dim sql As String = " SELECT TOP 6 [news_id], [news_title], [news_detail], [creation_by], [created_date], [updated_by], [updated_date] "
            sql &= " FROM [NEWS] ORDER BY [created_date] Desc "
            Dim DT As DataTable = MD.GetDataTable(sql)

            If DT.Rows.Count > 0 Then
                rptLawNews.DataSource = DT
                rptLawNews.DataBind()
            End If

        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alert", "<script>document.write('" & ex.Message & "');</script>", False)
        End Try
    End Sub
    Protected Sub rptLawDraft_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.RepeaterCommandEventArgs) Handles rptLawDraft.ItemCommand
        If e.CommandName = "Comment" Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "Check", "window.location.href='LawDraft.aspx?set=" & e.CommandArgument & "';", True)
        End If
        If e.CommandName = "Readmore" Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "Check", "window.location.href='LawDraft.aspx?set=All';", True)
        End If
    End Sub
    Protected Sub rptLawArticle_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.RepeaterCommandEventArgs) Handles rptLawArticle.ItemCommand
        Dim path As String = "FileELaw\"

        If e.CommandName = "Download" Then
            'Download Document
            'ScriptManager.RegisterStartupScript(Me, Me.GetType, "Check", "window.open('Article/" & e.CommandArgument & "');", True)
            MC.OpenWindow(Me, "http://" & Constant.BaseURL(Request) & "" & path & "" & "" & e.CommandArgument & "" & ".pdf" & "?time=" & Date.Now.ToString("HH:mm:ss"))

        End If
        If e.CommandName = "Readmore" Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "Check", "window.location.href='LawArticle.aspx?set=All';", True)
        End If
    End Sub
    Protected Sub rptLawNews_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.RepeaterCommandEventArgs) Handles rptLawNews.ItemCommand

        If e.CommandName = "Detail" Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "Check", "window.location.href='LawNews.aspx?set=" & e.CommandArgument & "';", True)
        End If
        If e.CommandName = "Readmore" Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "Check", "window.location.href='LawNews.aspx?set=All';", True)
        End If
    End Sub
    Protected Sub RepeaterLaw1_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.RepeaterCommandEventArgs) Handles RepeaterLaw1.ItemCommand
        Dim path As String = "LAWUPLOAD\"

        If e.CommandName = "Download" Then
            'Download Document
            'ScriptManager.RegisterStartupScript(Me, Me.GetType, "Check", "window.open('Article/" & e.CommandArgument & "');", True)
            MC.OpenWindow(Me, "http://" & Constant.BaseURL(Request) & "" & path & "" & "" & e.CommandArgument & "" & ".pdf" & "?time=" & Date.Now.ToString("HH:mm:ss"))

        End If
        If e.CommandName = "Readmore" Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "Check", "window.location.href='NewLaw.aspx?set=1';", True)
        End If
    End Sub

    Protected Sub RepeaterLaw2_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.RepeaterCommandEventArgs) Handles RepeaterLaw2.ItemCommand
        Dim path As String = "LAWUPLOAD\"

        If e.CommandName = "Download" Then
            MC.OpenWindow(Me, "http://" & Constant.BaseURL(Request) & "" & path & "" & "" & e.CommandArgument & "" & ".pdf" & "?time=" & Date.Now.ToString("HH:mm:ss"))

        End If
        If e.CommandName = "Readmore" Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "Check", "window.location.href='NewLaw.aspx?set=2';", True)
        End If
    End Sub

    Protected Sub RepeaterLaw3_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.RepeaterCommandEventArgs) Handles RepeaterLaw3.ItemCommand
        Dim path As String = "LAWUPLOAD\"

        If e.CommandName = "Download" Then
            MC.OpenWindow(Me, "http://" & Constant.BaseURL(Request) & "" & path & "" & "" & e.CommandArgument & "" & ".pdf" & "?time=" & Date.Now.ToString("HH:mm:ss"))

        End If
        If e.CommandName = "Readmore" Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "Check", "window.location.href='NewLaw.aspx?set=3';", True)
        End If
    End Sub

    Protected Sub RepeaterLaw4_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.RepeaterCommandEventArgs) Handles RepeaterLaw4.ItemCommand
        Dim path As String = "LAWUPLOAD\"

        If e.CommandName = "Download" Then
            MC.OpenWindow(Me, "http://" & Constant.BaseURL(Request) & "" & path & "" & "" & e.CommandArgument & "" & ".pdf" & "?time=" & Date.Now.ToString("HH:mm:ss"))

        End If
        If e.CommandName = "Readmore" Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "Check", "window.location.href='NewLaw.aspx?set=4';", True)
        End If
    End Sub

    Protected Sub RepeaterCase_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.RepeaterCommandEventArgs) Handles RepeaterCase.ItemCommand
        Dim path As String = "CASEUPLOAD\"

        If e.CommandName = "Download" Then
            MC.OpenWindow(Me, "http://" & Constant.BaseURL(Request) & "" & path & "" & "" & e.CommandArgument & "" & ".pdf" & "?time=" & Date.Now.ToString("HH:mm:ss"))

        End If
        If e.CommandName = "Readmore" Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "Check", "window.location.href='CaseWeb.aspx?set=All';", True)
        End If
    End Sub
End Class