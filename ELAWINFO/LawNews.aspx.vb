Imports System.Data
Imports System.Data.OleDb
Partial Class LawNews
    Inherits System.Web.UI.Page
    Dim MD As New MainData
    Dim MC As New MainClass

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            'ดึงข้อมูลแสดงใน Repeater
            If Request.QueryString("set") = "All" OrElse Request.QueryString("set") = "" Then
                BindLawNews()
            Else
                BindLawNewsByWhere(Request.QueryString("set"))
            End If
        End If
    End Sub

    Private Sub BindLawNews()
        Try
            Dim sql As String = " SELECT [news_id], [news_title], [news_detail], [creation_by], [created_date], [updated_by], [updated_date] "
            sql &= " FROM [NEWS] ORDER BY [created_date] Desc "
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
                lblLawSubject.Text = DT(0)("news_title")
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
End Class
