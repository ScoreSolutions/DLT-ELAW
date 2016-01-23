
Partial Class UserControl_ctlPrintLaw
    Inherits System.Web.UI.UserControl

    Public WriteOnly Property LawID() As String
        Set(ByVal value As String)
            hidLawID.Value = value
        End Set
    End Property

    Protected Sub btnPrint_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnPrint.Click
        Dim popupScript As String = " window.open('../Src/PrintLaw.aspx?id=" & hidLawID.Value & "&type=" & rdiType.SelectedValue & "', '_new','resizable=no,scrollbars=no,status=no,location=no,top=10,left=10,width=750,height=700');"
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Preview", popupScript, True)
    End Sub
End Class
