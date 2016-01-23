Imports System.Data

Partial Class UserControl_ComboBox
    Inherits System.Web.UI.UserControl
    Public Event SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
    Dim _isDefaultValue As Boolean = True
    Dim _DefaultValue As String = "0"
    Dim _DefaultDisplay As String = "---------โปรดเลือก----------"

    Public Property IsDefaultValue() As Boolean
        Get
            Return _isDefaultValue
        End Get
        Set(ByVal value As Boolean)
            _isDefaultValue = value
        End Set
    End Property
    Public Property DefaultValue() As String
        Get
            Return _DefaultValue
        End Get
        Set(ByVal value As String)
            _DefaultValue = value
        End Set
    End Property
    Public Property DefaultDisplay() As String
        Get
            Return _DefaultDisplay
        End Get
        Set(ByVal value As String)
            _DefaultDisplay = value
        End Set
    End Property
    Public Property IsNotNull() As Boolean
        Get
            Return lblStar.Visible
        End Get
        Set(ByVal value As Boolean)
            lblStar.Visible = value
        End Set
    End Property

    Public Property SelectedValue() As String
        Get
            Return cmbCombo.SelectedValue
        End Get
        Set(ByVal value As String)
            cmbCombo.SelectedValue = value
        End Set
    End Property
    Public ReadOnly Property SelectedText() As String
        Get
            Return cmbCombo.SelectedItem.Text
        End Get
    End Property
    Public Property AutoPosBack() As Boolean
        Get
            Return cmbCombo.AutoPostBack
        End Get
        Set(ByVal value As Boolean)
            cmbCombo.AutoPostBack = value
        End Set
    End Property
    Public Property Width() As Double
        Get
            Return Convert.ToDouble(cmbCombo.Width)
        End Get
        Set(ByVal value As Double)
            cmbCombo.Width = value
        End Set
    End Property
    Public Property Enabled() As Boolean
        Get
            Return cmbCombo.Enabled
        End Get
        Set(ByVal value As Boolean)
            cmbCombo.Enabled = value
        End Set
    End Property
    Public WriteOnly Property CssClass() As String
        Set(ByVal value As String)
            cmbCombo.CssClass = value
        End Set
    End Property
    Public WriteOnly Property ValidMsg() As String
        Set(ByVal value As String)
            lblStar.Text = value
        End Set
    End Property

    Public Sub SetItemList(ByVal itemText As String, ByVal itemValue As String)
        Dim lst As New ListItem(itemText, itemValue)
        cmbCombo.Items.Add(lst)
    End Sub
    Public Sub SetItemList(ByVal dt As DataTable, ByVal fldText As String, ByVal fldValue As String)
        ClearCombo()

        If _isDefaultValue = True Then
            Dim dr As DataRow = dt.NewRow
            dr(fldText) = _DefaultDisplay
            dr(fldValue) = _DefaultValue

            dt.Rows.InsertAt(dr, 0)
        End If

        'If dt.Rows.Count > 0 Then
        cmbCombo.DataSource = dt
        cmbCombo.DataValueField = fldValue
        cmbCombo.DataTextField = fldText
        cmbCombo.DataBind()
        'End If
    End Sub
    Public Sub ClearCombo()
        cmbCombo.Items.Clear()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If _isDefaultValue = True And cmbCombo.Items.Count = 0 Then
            'SetItemList(_DefaultDisplay, _DefaultValue)
            Dim lst As New ListItem(_DefaultDisplay, _DefaultValue)
            cmbCombo.Items.Insert(0, lst)
        End If
    End Sub

    Private Sub cmbCombo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbCombo.SelectedIndexChanged
        RaiseEvent SelectedIndexChanged(sender, e)
    End Sub
End Class