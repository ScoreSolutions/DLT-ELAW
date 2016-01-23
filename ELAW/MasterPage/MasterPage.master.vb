Imports System.Data
Imports System.Data.OleDb
Imports System.Xml.Serialization
Imports System.IO
Imports System.Web.Security
Imports System.Web

Partial Class MasterPage_MasterPage
    Inherits System.Web.UI.MasterPage
    Dim MD As New MainData

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            Try
                If Page.User.Identity.Name = "" Then
                    FormsAuthentication.RedirectToLoginPage()
                Else
                    Try
                        Dim id As FormsIdentity = CType(HttpContext.Current.User.Identity, FormsIdentity)
                        Dim tik As FormsAuthenticationTicket = id.Ticket
                        Dim sr As New XmlSerializer(GetType(LoginUser))
                        Dim st As New MemoryStream(Convert.FromBase64String(tik.UserData))

                        Dim uData As New LoginUser
                        uData = CType(sr.Deserialize(st), LoginUser)
                        uData.GetUserData(HttpContext.Current.User.Identity.Name)
                        Name(uData)
                    Catch

                    End Try

                End If
            Catch ex As Exception
                ex.ToString()
                FormsAuthentication.SignOut()
                FormsAuthentication.RedirectToLoginPage()
            End Try
        End If
    End Sub
    Private Sub Name()
        Dim sEmpNo As String = Session("EmpNo")
        Dim strsql As String
        Dim oDs As New DataSet

        strsql = "select firstname+' '+lastname as fullname from employee where empid='" & sEmpNo & "'"


        oDs = MD.GetDataset(strsql)
        If oDs.Tables(0).Rows.Count > 0 Then
            'lblUserName.Text = oDs.Tables(0).Rows(0).Item("fullname").ToString
        End If
    End Sub
    Private Sub Name(ByVal uData As LoginUser)
        lblUser.Text = uData.PrefixName & uData.FirstName & " " & uData.LastName
    End Sub
End Class

