Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.Security
Imports System.Security.Cryptography

Partial Class Src_ChangePassword
    Inherits System.Web.UI.Page

    Protected Sub lnkLogin_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkLogin.Click
        Dim msg As String = ""
        If txtPassword.Text.Trim() = "" Or txtConfirmPassword.Text.Trim() = "" Then
            msg = "กรุณากรอกข้อมูลให้ครบถ้วน"

        ElseIf txtPassword.Text.Trim() <> txtConfirmPassword.Text.Trim() Then
            msg = "การยืนยันรหัสผ่านไม่ถูกต้อง"
        End If

        If msg <> "" Then
            Dim popupScript = "<script language='javascript'> "
            popupScript += " alert('" & msg & "'); "
            popupScript += " document.getElementById('" & txtPassword.ClientID & "').focus();"
            popupScript += " </script>"
            Me.RegisterStartupScript("Google", popupScript)
        Else
            Dim MC As New MainClass
            Dim expDate As String = MC.Date2DB(FormatDateTime(DateAdd(DateInterval.Day, Constant.pwdExpDay, Today.Date), DateFormat.ShortDate))
            Dim sql As String = ""
            sql += "update users "
            sql += " set password=@Password, "
            sql += " forcechangepwd = 'N', "
            sql += " pwdexpiredate='" & expDate & "'"
            sql += " where loginname='" & Page.User.Identity.Name & "' "

            Dim MD As New MainData
            Dim conn As New SqlConnection(MD.strConnIMP)
            conn.Open()
            Dim trans As SqlTransaction = conn.BeginTransaction(IsolationLevel.ReadCommitted)
            Dim cmd As New SqlCommand(sql, conn, trans)

            'Encrypt the password
            Dim md5Hasher As New MD5CryptoServiceProvider()

            Dim hashedDataBytes As Byte()
            Dim encoder As New UTF8Encoding()

            hashedDataBytes = md5Hasher.ComputeHash(encoder.GetBytes(txtPassword.Text.Trim()))

            Dim paramPwd As SqlParameter
            paramPwd = New SqlParameter("@Password", SqlDbType.Binary, 16)
            paramPwd.Value = hashedDataBytes
            cmd.Parameters.Add(paramPwd)

            If cmd.ExecuteNonQuery() > 0 Then
                trans.Commit()
                FormsAuthentication.SignOut()
                Dim popupScript = "<script language='javascript'> "
                popupScript += " alert('เปลี่ยนรหัสผ่านเรียบร้อย กรุณาเข้าสู่ระบบอีกครั้งด้วยรหัสผ่านใหม่'); "
                popupScript += " window.open('../Src/Login.aspx', '_self','');"
                popupScript += " </script>"
                Me.RegisterStartupScript("Google", popupScript)
                'Response.Redirect(".." & FormsAuthentication.LoginUrl)
            Else
                trans.Rollback()
            End If
        End If

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            Dim uData As New LoginUser
            uData.GetUserData(Page.User.Identity.Name)
            lblUserName.Text = uData.PrefixName & uData.FirstName & " " & uData.LastName
        End If
    End Sub
End Class
