
Partial Class Src_SearchResult
    Inherits System.Web.UI.Page
    Dim MC As New MainClass
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim file As String = Request.QueryString("file")
        Dim page As String = Request.QueryString("page")
        Dim tfind As String = Request.QueryString("tfind")

        'MC.MessageBox(Me, tfind)
        Response.Redirect("..\Document_Import\" & file & ".pdf?#page=" & page & "&search=" & Session("aa") & "", True)

        'Response.Redirect("..\Document_Import\LW10-00002.pdf?#page=2&search=เพิ่มเติม", True)

    End Sub

    'Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
    '    'Response.Charset = "windows-874"

    '    Dim file As String = Request.QueryString("file")
    '    Dim page As String = Request.QueryString("page")
    '    Dim tfind As String = Request.QueryString("tfind")

    '    'Response.Redirect("..\Document_Import\" & file & ".pdf?#page=" & page & "&search=" & tfind & "", True)
    '    'Response.Redirect("..\Document_Import\LW10-00002.pdf?#page=2&search=เพิ่มเติม", True)


    '    MC.OpenWindow(Me, "..\Document_Import\" & file & ".pdf?#page=" & page & "&search=" & tfind & "")
    '    Response.Redirect("..\Document_Import\" & file & ".pdf?#page=" & page & "&search=" & tfind & "", True)

    'End Sub
End Class
