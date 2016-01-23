
Partial Class Src_DrafLawPreview
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim LawID As String = Request.QueryString("LawID")
        Dim Sql As String = ""
        Sql += " select message from law_data where law_id='" & LawID & "'"
        Dim MD As New MainData

        lblPreview.Text = MD.GetDataTable(Sql).Rows(0)("message")
    End Sub
End Class
