
Partial Class UserControl_TextDate
    Inherits System.Web.UI.UserControl
    Public Delegate Sub SelectedDateEvent(ByVal sender As Object, ByVal e As System.EventArgs)
    Public Event SelectedDateChanged As SelectedDateEvent

    Dim _IsNotNull As Boolean = False
    Public Property AutoPosBack() As Boolean
        Get
            Return TextBox1.AutoPostBack
        End Get
        Set(ByVal value As Boolean)
            TextBox1.AutoPostBack = value
        End Set
    End Property

    Public Property TxtBox() As TextBox
        Get
            Return TextBox1
        End Get
        Set(ByVal value As TextBox)
            TextBox1 = value
        End Set
    End Property
    Public ReadOnly Property CalendarClientID() As String
        Get
            Return TextBox1.ClientID
        End Get
    End Property

    Public ReadOnly Property ImageClientID() As String
        Get
            Return ImageButton1.ClientID
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
            Return ImageButton1.Visible
        End Get
        Set(ByVal value As Boolean)
            ImageButton1.Visible = value
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
            Return Label1.Text
        End Get
        Set(ByVal value As String)
            Label1.Text = value
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
        If TextBox1.Text.Trim() <> "" Then
            Return Date.ParseExact(TextBox1.Text, "dd/MM/yyyy", Nothing)
        Else
            Return New Date()
        End If
    End Function
    Private Sub SetDate(ByVal DateValue As DateTime)
        If DateValue.Year = 1 Then
            TextBox1.Text = ""
        Else
            TextBox1.Text = DateValue.ToString("dd/MM/yyyy")
        End If

    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            TextBox1.Attributes.Add("OnClick", "this.select();")
            TextBox1.Attributes.Add("OnKeyPress", "window.event.keyCode = 0")
            TextBox1.Attributes.Add("onKeyDown", "CheckKeyBackSpace();")

            If _IsNotNull = False Then
                Label1.Visible = False
            Else
                Label1.Visible = True
            End If

        End If
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        RaiseEvent SelectedDateChanged(sender, e)
    End Sub
End Class