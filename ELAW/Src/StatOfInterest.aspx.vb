Imports System.Data
Imports System.Data.OleDb
Partial Class StatOfInterest
    Inherits System.Web.UI.Page

    Dim Conn As OleDbConnection
    Dim Com As OleDbCommand
    Dim TR As OleDbTransaction
    Dim MD As New MainData
    Dim MC As New MainClass

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            GetStatLaw()
            GetStatCont()
            GetStatCase()
            GetStatOther()
            GetStatQuestion()
        End If
    End Sub

    Sub GetStatLaw()
        Dim SQL As String
        SQL = " SELECT LawGroup,Law_Type,COUNT(law_type) as stat FROM WSTAT_INTEREST"
        SQL &= " WHERE LawGroup = 'กฏหมาย'"
        SQL &= " GROUP BY LawGroup,law_type ORDER BY stat DESC"

        Dim DT As New DataTable
        DT = MD.GetDataTable(SQL)
        If DT.Rows.Count > 0 Then
            gdvStatLaw.DataSource = DT
            gdvStatLaw.DataBind()
            lblLaw.Visible = False
        Else
            lblLaw.Visible = True
        End If
    End Sub
    Sub GetStatCont()
        Dim SQL As String
        SQL = " SELECT LawGroup,Law_Type,COUNT(law_type) as stat FROM WSTAT_INTEREST"
        SQL &= " WHERE LawGroup = 'สัญญา'"
        SQL &= " GROUP BY LawGroup,law_type ORDER BY stat DESC"

        Dim DT As New DataTable
        DT = MD.GetDataTable(SQL)
        If DT.Rows.Count > 0 Then
            gdvStatContact.DataSource = DT
            gdvStatContact.DataBind()
            lblCont.Visible = False
        Else
            lblCont.Visible = True
        End If
    End Sub
    Sub GetStatCase()
        Dim SQL As String
        SQL = " SELECT LawGroup,Law_Type,COUNT(law_type) as stat FROM WSTAT_INTEREST"
        SQL &= " WHERE LawGroup = 'คดี'"
        SQL &= " GROUP BY LawGroup,law_type ORDER BY stat DESC"

        Dim DT As New DataTable
        DT = MD.GetDataTable(SQL)
        If DT.Rows.Count > 0 Then
            gdvStatCase.DataSource = DT
            gdvStatCase.DataBind()
            lblCase.Visible = False
        Else
            lblCase.Visible = True
        End If
    End Sub
    Sub GetStatOther()
        Dim SQL As String
        SQL = " SELECT LawGroup,Law_Type,COUNT(law_type) as stat FROM WSTAT_INTEREST"
        SQL &= " WHERE LawGroup = 'อื่นๆ'"
        SQL &= " GROUP BY LawGroup,law_type ORDER BY stat DESC"

        Dim DT As New DataTable
        DT = MD.GetDataTable(SQL)
        If DT.Rows.Count > 0 Then
            gdvOther.DataSource = DT
            gdvOther.DataBind()
            lblOther.Visible = False
        Else
            lblOther.Visible = True
        End If
    End Sub

    Sub GetStatQuestion()
        Dim SQL As String
        SQL = " SELECT * FROM WQUESTION"
        SQL &= " ORDER BY created_date Desc"

        Dim DT As New DataTable
        DT = MD.GetDataTable(SQL)
        If DT.Rows.Count > 0 Then
            gdvWQuestion.DataSource = DT
            gdvWQuestion.DataBind()
            lblQuestion.Visible = False
        Else
            lblQuestion.Visible = True
        End If
    End Sub
End Class
