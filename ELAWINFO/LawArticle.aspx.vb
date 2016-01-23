﻿Imports System.Data
Imports System.Data.OleDb
Partial Class LawArticle
    Inherits System.Web.UI.Page
    Dim MD As New MainData
    Dim MC As New MainClass

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            'ดึงข้อมูลแสดงใน Repeater
            BindLawArticle()
        End If
    End Sub

    Private Sub BindLawArticle()
        Try
            Dim sql As String = " SELECT [file_id], [name], [file_path], [creation_by], [created_date], [updated_by], [updated_date] "
            sql &= " FROM [FILE_UPLOAD] ORDER BY [created_date] Desc "
            Dim DT As DataTable = MD.GetDataTable(sql)
            rptLawArticle.DataSource = DT
            rptLawArticle.DataBind()
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alert", "<script>document.write('" & ex.Message & "');</script>", False)
        End Try
    End Sub

    Protected Sub rptLawArticle_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.RepeaterCommandEventArgs) Handles rptLawArticle.ItemCommand
        Dim path As String = "FileELaw\"
        If e.CommandName = "Download" Then
            'Download Document
            'ScriptManager.RegisterStartupScript(Me, Me.GetType, "Check", "window.open('" & e.CommandArgument & "');", True)
            'Dim strUrl As String = "http://" & Constant.BaseURL(Request) & "" & path & "" & "" & e.CommandArgument & ""
            MC.OpenWindow(Me, "http://" & Constant.BaseURL(Request) & "" & path & "" & "" & e.CommandArgument & "" & ".pdf" & "?time=" & Date.Now.ToString("HH:mm:ss"))

        End If
        If e.CommandName = "Readmore" Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "Check", "window.location.href='LawArticle.aspx?set=All';", True)
        End If
    End Sub
End Class
