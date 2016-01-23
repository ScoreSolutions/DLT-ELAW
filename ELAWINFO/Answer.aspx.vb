Imports System.Data
Imports System.Data.OleDb
Partial Class Profile_Answer
    Inherits System.Web.UI.Page

    Dim Conn As OleDbConnection
    Dim Com As OleDbCommand
    Dim TR As OleDbTransaction
    Dim MD As New MainData
    Dim MC As New MainClass

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            BindSubjMaxDate()
            RptAnswer()
        End If
    End Sub
    Protected Function Choose(ByVal value As String) As String
        Return "คำถาม " & value & " :"
    End Function
    Protected Function ChooseAnswer(ByVal value As String) As String
        Return "คำตอบ " & value & " :"
    End Function

    Sub BindSubjMaxDate()
        Dim SQL As String
        Dim DT As New DataTable
        SQL = " SELECT * FROM SUBJ ORDER BY created_date DESC"
        DT = MD.GetDataTable(SQL)
        If DT.Rows.Count > 0 Then
            ddlSubj.DataSource = DT
            ddlSubj.DataTextField = "subject_desc"
            ddlSubj.DataValueField = "subject_id"
            ddlSubj.DataBind()
        Else
            ddlSubj.Items.Insert(0, (New ListItem("ไม่มีหัวข้อ", "null")))
        End If
    End Sub

    Sub RptAnswer()

        Dim SQL As String
        SQL = " SELECT ISNULL(MAX(subject_day),0) AS subject_day  , ISNULL(MAX(created_date),0) AS created_date FROM SUBJ "

        Dim DTSubj As New DataTable
        DTSubj = MD.GetDataTable(SQL)

        SQL = "SELECT question_id,subj_question,created_date"
        SQL &= " FROM SUBJ_QUESTION WHERE  subject_id=" & ddlSubj.SelectedValue

        Dim DT As New DataTable
        DT = MD.GetDataTable(SQL)

        If DT.Rows.Count <> 0 Then

            Dim StartDate As Integer = Math.Floor(DT.Rows(0)("created_date").ToOADate)
            Dim EndDate As Integer = Math.Floor(Now.ToOADate)
            Dim Diff As Integer
            Diff = DateDiff(DateInterval.Day, DateTime.FromOADate(StartDate), DateTime.FromOADate(EndDate))

            If Val(DTSubj.Rows(0)("subject_day")) >= Diff Then
                RepeaterAnswer.DataSource = DT
                RepeaterAnswer.DataBind()
                btnCreateAnswer.Visible = True
            End If
        Else
            btnCreateAnswer.Visible = False
        End If

        

        Dim strsql2 As String = ""
        strsql2 = "select * from SUBJ_FileUpload Where Subj_ID='" & ddlSubj.SelectedValue & "'"

        DT = New DataTable
        DT = MD.GetDataTable(strsql2)
        If dt.Rows.Count > 0 Then
            lbtnDownload.Visible = True
        Else
            lbtnDownload.Visible = False
        End If

        DT.Dispose()
        DTSubj.Dispose()
    End Sub

    Protected Sub btnCreateAnswer_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCreateAnswer.Click

        For Each Item In RepeaterAnswer.Items
            If Item.ItemType = ListItemType.Item Or Item.ItemType = ListItemType.AlternatingItem Then
                Dim txtAnswer As TextBox = Item.FindControl("txtAnswer")
                If txtAnswer.Text = "" Then
                    MC.MessageBox(Me, "กรุณาป้อนข้อมูลให้ครบ")
                    Exit Sub
                End If
            End If
        Next

        Try
            Dim SQL As String
            Conn = New OleDbConnection(MD.Strcon)
            Com = New OleDbCommand()
            Conn.Open()

            TR = Conn.BeginTransaction
            For Each Item In RepeaterAnswer.Items
                If Item.ItemType = ListItemType.Item Or Item.ItemType = ListItemType.AlternatingItem Then
                    Dim HiddenField As HiddenField = Item.FindControl("HiddenField")
                    Dim txtAnswer As TextBox = Item.FindControl("txtAnswer")
                    If txtAnswer.Text <> "" Then

                        SQL = " INSERT SUBJ_ANSWER (question_id,answer,created_date,subject_id)"
                        SQL &= " VALUES ( '" & HiddenField.Value & "','" & txtAnswer.Text & "','" & Now.Year & "-" & Now.Month & "-" & Now.Day & "'," & ddlSubj.SelectedValue & " )"

                        With Com
                            .CommandText = SQL
                            .CommandType = CommandType.Text
                            .Connection = Conn
                            .Transaction = TR
                            .ExecuteNonQuery()
                        End With
                    End If
                End If
            Next

            MC.MessageBox(Me, "บันทึกข้อมูลเรียบร้อยแล้ว")
            TR.Commit()
            ClearDate()
            RepeaterAnswer.DataSource = Nothing

        Catch ex As Exception
            TR.Rollback()
            MC.MessageBox(Me, ex.ToString)
        Finally
            Conn.Close()
            Response.Redirect("Answer.aspx")
        End Try
    End Sub

    Sub ClearDate()
        For Each Item In RepeaterAnswer.Items
            If Item.ItemType = ListItemType.Item Or Item.ItemType = ListItemType.AlternatingItem Then
                Dim txtAnswer As TextBox = Item.FindControl("txtAnswer")
                If txtAnswer.Text <> "" Then
                    txtAnswer.Text = ""
                End If
            End If
        Next
    End Sub

    Protected Sub ddlSubj_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSubj.SelectedIndexChanged
        RptAnswer()
    End Sub
    Protected Sub lbtnDownload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbtnDownload.Click
        Dim strsql2 As String = ""
        strsql2 = "select * from SUBJ_FileUpload Where Subj_ID='" & ddlSubj.SelectedValue & "'"

        Dim dt As DataTable
        dt = MD.GetDataTable(strsql2)
        If dt.Rows.Count > 0 Then

            If dt(0)("mime_type").ToString() = ".pdf" Then

                Dim strUrl As String = "http://" & Constant.BaseURL(Request) & dt(0)("file_path").ToString() & ""
                lbtnDownload.Text = "<a href='" & strUrl & "?time=" & Date.Now.ToString("HH:mm:ss") & "' Target='_Blank'>" & "ดาวน์โหลดเอกสาร" & "</a>&nbsp;&nbsp;"

            ElseIf dt(0)("mime_type").ToString() = ".doc" Or dt(0)("mime_type").ToString() = ".docx" Then

                Dim strUrl As String = "http://" & Constant.BaseURL(Request) & dt(0)("file_path").ToString()
                lbtnDownload.Text = "<a href='" & strUrl & "?time=" & Date.Now.ToString("HH:mm:ss") & "' Target='_Blank'>" & "ดาวน์โหลดเอกสาร" & "</a>&nbsp;&nbsp;"

            ElseIf dt(0)("mime_type").ToString() = ".xls" Or dt(0)("mime_type").ToString() = ".xlsx" Then

                Dim strUrl As String = "http://" & Constant.BaseURL(Request) & dt(0)("file_path").ToString()
                lbtnDownload.Text = "<a href='" & strUrl & "?time=" & Date.Now.ToString("HH:mm:ss") & "' Target='_Blank'>" & "ดาวน์โหลดเอกสาร" & "</a>&nbsp;&nbsp;"

            ElseIf dt(0)("mime_type").ToString() = ".txt" Then

                Dim strUrl As String = "http://" & Constant.BaseURL(Request) & dt(0)("file_path").ToString()
                lbtnDownload.Text = "<a href='" & strUrl & "?time=" & Date.Now.ToString("HH:mm:ss") & "' Target='_Blank'>" & "ดาวน์โหลดเอกสาร" & "</a>&nbsp;&nbsp;"

            End If

        End If
        lbtnDownload.Visible = True
    End Sub

End Class
