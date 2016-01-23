Option Compare Text
Imports System.Data
Imports System.Data.OleDb
Imports System.Web
Imports System.Web.Security
Imports System.Xml.Serialization
Imports System.IO

Partial Class Src_Login
    Inherits System.Web.UI.Page
    Dim MC As New MainClass
    Dim oMsg As New MainClass   'MessageBox
    Dim SerialUserData As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'HttpContext.Current.Response.Cookies.Clear()
        'FormsAuthentication.SignOut()

        'If Not Page.IsPostBack Then
        '    If Not IsNothing(Request.Cookies("HOME_USERNAME")) Then
        '        txtUsername.Text = Request.Cookies("HOME_USERNAME").Value
        '        ChkRememberMe.Checked = Request.Cookies("HOME_USERNAME").Value <> ""
        '    End If
        'End If

        txtUsername.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" + txtPassword.UniqueID + "').focus();return false;}} else {return true}; ")
        'txtPassword.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" + lnkLogin.UniqueID + "').focus();return false;}} else {return true}; ")

        txtPassword.Attributes.Add("onkeypress", "return clickButton(event,'" + lnkLogin.ClientID + "')")


    End Sub

    Protected Sub lnkLogin_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkLogin.Click
        Dim UserId As String = MC.ChkPassword(txtUsername.Text, txtPassword.Text)
        If UserId <> "" Then
            'oMsg.MessageBox(Me, "รหัสถูกต้อง")
            Session("EmpNo") = UserId

            Dim uData As New LoginUser
            uData.GetUserData(UserId)

            SerialUserData = GetSerialUserData(uData)

            HttpContext.Current.Response.Cookies.Clear()
            'Dim fat As New FormsAuthenticationTicket(1, txtUsername.Text, DateTime.Now, DateTime.Now.AddDays(1), True, SerialUserData)
            Dim fat As New FormsAuthenticationTicket(1, txtUsername.Text.ToUpper, DateTime.Now, DateTime.Now.AddDays(1), True, SerialUserData)
            Dim ck As New HttpCookie(".ELAWSingSingOn")
            ck.Value = FormsAuthentication.Encrypt(fat)
            ck.Expires = fat.Expiration
            HttpContext.Current.Response.Cookies.Add(ck)

            'If MC.ChkFirstLogin(UserId) = True Then
            If uData.ForceChangePwd = "Y" Then
                'กรณี Login ครั้งแรก บังคับให้เปลี่ยนรหัสผ่านก่อน
                Dim popupScript = "<script language='javascript'> " & _
                                    " alert('ท่านเข้าสู่ระบบเป็นครั้งแรก กรุณาเปลี่ยนรหัสผ่าน'); " & _
                                    " window.open('../Src/ChangePassword.aspx', '_self','');" & _
                                    " </script>"
                Me.RegisterStartupScript("Google", popupScript)
            ElseIf uData.PwdExpireDate.Date < Today.Date Then
                'กรณีรหัสผ่านหมดอายุบังคับให้เปลี่ยนรหัสผ่านคือกัน
                Dim popupScript = "<script language='javascript'> " & _
                                    " alert('รหัสผ่านของท่านหมดอายุ กรุณาเปลี่ยนรหัสผ่าน'); " & _
                                    " window.open('../Src/ChangePassword.aspx', '_self','');" & _
                                    " </script>"
                Me.RegisterStartupScript("Google", popupScript)
            Else
                Response.Redirect("../Src/SearchLaw.aspx")
                GetCookies()
                'Response.Redirect(FormsAuthentication.DefaultUrl)


                'Dim popupScript = "<script language='javascript'> " & _
                '                    " window.open('../Src/ContractData.aspx', '_self','');" & _
                '                    " </script>"
                'Me.RegisterStartupScript("FirstPage", popupScript)

            End If
        Else
            oMsg.MessageBox(Me, "รหัสไม่ถูกต้อง")
        End If
    End Sub

    Sub GetCookies()
        'Input Cookies
        If ChkRememberMe.Checked Then
            Response.Cookies("HOME_USERNAME").Value = txtUsername.Text
        ElseIf Not IsNothing(Request.Cookies("HOME_USERNAME")) Then
            Response.Cookies("HOME_USERNAME").Value = ""
            Response.Cookies.Remove("HOME_USERNAME")
            ChkRememberMe.Checked = False
            Response.Cookies.Clear()
            Request.Cookies.Clear()
        End If
    End Sub

    Private Function GetSerialUserData(ByVal uData As LoginUser) As String
        'Dim uData As New LoginUser
        'uData.GetUserData(UserID)
        Dim sr As New XmlSerializer(GetType(LoginUser))
        Dim st As New MemoryStream()
        sr.Serialize(st, uData)
        Dim b() As Byte = st.GetBuffer()

        Return Convert.ToBase64String(b)
    End Function
End Class
