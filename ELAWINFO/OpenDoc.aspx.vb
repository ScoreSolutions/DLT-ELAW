
Partial Class OpenDoc
    Inherits System.Web.UI.Page

    Protected Sub form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles form1.Load
        Counter.DocCounter(Request.QueryString("type"), Request.QueryString("subtype"))
        Response.Redirect(Request.QueryString("url") & "?time=" & Date.Now.ToString("HH:mm:ss"))
    End Sub
End Class
