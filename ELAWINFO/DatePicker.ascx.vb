
Partial Class UserControl_DatePicker
    Inherits System.Web.UI.UserControl

    Public Delegate Sub SelectedDateEvent(ByVal sender As Object, ByVal e As System.EventArgs)
    Public Event SelectedDateChanged As SelectedDateEvent

    Dim _IsNotNull As Boolean = False
    Public Property AutoPosBack() As Boolean
        Get
            Return txtDate.AutoPostBack
        End Get
        Set(ByVal value As Boolean)
            txtDate.AutoPostBack = value
        End Set
    End Property

    Public Property TxtBox() As TextBox
        Get
            Return txtDate
        End Get
        Set(ByVal value As TextBox)
            txtDate = value
        End Set
    End Property
    Public ReadOnly Property CalendarClientID() As String
        Get
            Return txtDate.ClientID
        End Get
    End Property

    Public ReadOnly Property ImageClientID() As String
        Get
            Return imgButton.ClientID
        End Get
    End Property

    Public Property Text() As DateTime
        Get
            Return GetDate()
        End Get
        Set(ByVal value As DateTime)
            SetDate(value)
        End Set
    End Property

    Public Property VisibleImg() As Boolean
        Get
            Return imgButton.Visible
        End Get
        Set(ByVal value As Boolean)
            imgButton.Visible = value
        End Set
    End Property
    Public Property IsNotNull() As Boolean
        Get
            Return _IsNotNull
        End Get
        Set(ByVal value As Boolean)
            _IsNotNull = value
        End Set
    End Property
    Public Property ErrMsg() As String
        Get
            Return lblChkDate1.Text
        End Get
        Set(ByVal value As String)
            lblChkDate1.Text = value
        End Set
    End Property
    Public ReadOnly Property SaveDate() As String
        Get
            Dim DateStr As Date = GetDate()
            Return DateStr.Year.ToString("0000") & DateStr.ToString("-MM-dd")
        End Get
    End Property

    Public Overrides Sub DataBind()
        MyBase.DataBind()
    End Sub

    Private Function GetDate() As Date
        If txtDate.Text.Trim() <> "" Then
            Return Date.ParseExact(txtDate.Text, "dd/MM/yyyy", Nothing)
        Else
            Return New Date()
        End If
    End Function
    Private Sub SetDate(ByVal DateValue As DateTime)
        If DateValue.Year = 1 Then
            txtDate.Text = ""
        Else
            txtDate.Text = DateValue.ToString("dd/MM/yyyy")
        End If

    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            txtDate.Attributes.Add("OnClick", "this.select();")
            txtDate.Attributes.Add("OnKeyPress", "window.event.keyCode = 0")
            txtDate.Attributes.Add("onKeyDown", "CheckKeyBackSpace();")

            If _IsNotNull = False Then
                lblChkDate1.Visible = False
            Else
                lblChkDate1.Visible = True
            End If

        End If
    End Sub

    Private Sub txtDate_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDate.TextChanged
        RaiseEvent SelectedDateChanged(sender, e)
    End Sub
End Class
