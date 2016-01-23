Option Compare Text
Imports System.Data
Imports System.Data.OleDb
Partial Class frmLogin
    Inherits System.Web.UI.Page
    Dim MC As New MainClass
    Dim oMsg As New MainClass   'MessageBox
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim sEmpNo As String = Session("EmpNo")

        'txtPassword.Attributes.Add("onkeypress", "setNextControlFocus('" + cmdLogin.ClientID + "');")
        'txtUsername.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" + txtPassword.UniqueID + "').focus();return false;}} else {return true}; ")
        'txtPassword.Attributes.Add("value", txtPassword.Text) 'Remember Password (Not Clear Password)

    End Sub
    Protected Sub cmdLogin_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdLogin.Click
        Dim UserId As String = MC.ChkPassword(txtUsername.Text, txtPassword.Text)
        If UserId <> "" Then
            'oMsg.MessageBox(Me, "รหัสถูกต้อง")
            Session("EmpNo") = UserId
            Response.Redirect("../Src/ContractData.aspx")
        Else
            oMsg.MessageBox(Me, "รหัสไม่ถูกต้อง")
        End If
    End Sub

End Class
