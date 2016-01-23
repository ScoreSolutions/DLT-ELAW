Imports System.Data
Imports System.Data.OleDb
Partial Class Profile_Subject
    Inherits System.Web.UI.Page

    Dim Conn As OleDbConnection
    Dim Com As OleDbCommand
    Dim TR As OleDbTransaction
    Dim MD As New MainData
    Dim MC As New MainClass
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            BindSubjMaxDate()
        End If
    End Sub

    Sub BindSubjMaxDate()
        Dim SQL As String
        Dim DT As New DataTable
        SQL = " SELECT * FROM SUBJ ORDER BY subject_desc"
        DT = MD.GetDataTable(SQL)
        If DT.Rows.Count > 0 Then
            ddlSubj.DataSource = DT
            ddlSubj.DataTextField = "subject_desc"
            ddlSubj.DataValueField = "subject_id"
            ddlSubj.DataBind()
            ddlSubj.Items.Insert(0, (New ListItem("กรุณาเลือกหัวข้อ", "null")))
        Else
            ddlSubj.Items.Insert(0, (New ListItem("ไม่มีหัวข้อ", "null")))
        End If
    End Sub

    Protected Sub btnFind_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFind.Click
        If ddlSubj.SelectedValue <> "null" Then
            BindCategory()
        End If
    End Sub
    'Private Sub BindCategory(ByVal _Date As Date)
    Private Sub BindCategory()
        Dim SQL As String
        ' SQL = "SELECT QUEST.question_id,QUEST.subj_question FROM SUBJ_QUESTION AS QUEST WHERE CONVERT(VARCHAR(10),created_date, 126) = '" & _Date.Year & "-" & _Date.Month & "-" & _Date.Day & "'"
        SQL = "SELECT subject_id,subject_desc FROM SUBJ AS SUBJ "
        SQL &= " WHERE subject_id=" & ddlSubj.SelectedValue & ""

        Dim DT As New DataTable
        DT = MD.GetDataTable(SQL)

        rptSubject.DataSource = DT
        rptSubject.DataBind()
    End Sub

    Protected Sub rptSubject_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptSubject.ItemDataBound
        If e.Item.ItemType <> ListItemType.Item And e.Item.ItemType <> ListItemType.AlternatingItem Then Exit Sub
        Dim lblsubject_desc As Label = e.Item.FindControl("lblsubject_desc")

        lblsubject_desc.Text = e.Item.DataItem("subject_desc")

        Dim FnSubject_id As Integer = 0
        If Not IsDBNull(e.Item.DataItem("subject_id")) Then FnSubject_id = e.Item.DataItem("subject_id")
        lblsubject_desc.Attributes.Add("subject_id", FnSubject_id)

        Dim rptQuestion As Repeater = e.Item.FindControl("rptQuestion")

        Dim SQL As String
        SQL = "SELECT question_id,subj_question FROM SUBJ_QUESTION WHERE subject_id = '" & FnSubject_id & "'"

        Dim DT As New DataTable
        DT = MD.GetDataTable(SQL)

        AddHandler rptQuestion.ItemDataBound, AddressOf rptQuestion_ItemDataBound
        rptQuestion.DataSource = DT
        rptQuestion.DataBind()
    End Sub

    Protected Sub rptQuestion_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs)

        If e.Item.ItemType <> ListItemType.Item And e.Item.ItemType <> ListItemType.AlternatingItem Then Exit Sub

        Dim lblQuestion As Label = e.Item.FindControl("lblQuestion")
        lblQuestion.Text = e.Item.DataItem("subj_question")

        Dim FnGroupID As Integer = 0
        If Not IsDBNull(e.Item.DataItem("question_id")) Then FnGroupID = e.Item.DataItem("question_id")
        lblQuestion.Attributes.Add("question_id", FnGroupID)

        Dim SQL As String
        SQL = "SELECT answer_id,answer FROM SUBJ_ANSWER  WHERE question_id = '" & FnGroupID & "' AND subject_id = " & ddlSubj.SelectedValue

        Dim DT As New DataTable
        DT = MD.GetDataTable(SQL)

        Dim rptAnswer As Repeater = e.Item.FindControl("rptAnswer")
        AddHandler rptAnswer.ItemDataBound, AddressOf rptAnswer_ItemDataBound

        rptAnswer.DataSource = DT
        rptAnswer.DataBind()
    End Sub

    Protected Sub rptAnswer_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs)
        If e.Item.ItemType <> ListItemType.Item And e.Item.ItemType <> ListItemType.AlternatingItem Then Exit Sub

        Dim lblAnswer As Label = e.Item.FindControl("lblAnswer")
        lblAnswer.Text = e.Item.DataItem("answer")
    End Sub

  
    Protected Sub ddlSubj_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSubj.SelectedIndexChanged

    End Sub
End Class
