Imports System.Data
Imports System.Data.OleDb
Partial Class LawDraft
    Inherits System.Web.UI.Page
    Dim Conn As OleDbConnection
    Dim Com As OleDbCommand
    Dim TR As OleDbTransaction
    Dim MD As New MainData
    Dim MC As New MainClass

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If Request.QueryString("set") = "All" OrElse Request.QueryString("set") = "" Then
                BindAllLawDraft()
            Else
                BindLawDraftByWhere(Request.QueryString("set"))
            End If
        End If
    End Sub

    Protected Sub rptLawDraft_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.RepeaterCommandEventArgs) Handles rptLawDraft.ItemCommand
        If e.CommandName = "Comment" Then
            BindLawDraftByWhere(e.CommandArgument)
            BindListQuestion(e.CommandArgument)
        End If
    End Sub

    Private Sub BindAllLawDraft()
        Try
            Dim sql As String = " SELECT  [subject_id], [subject_desc], [subject_day], [created_user], [created_date]"
            sql &= " FROM [SUBJ] "
            sql += " WHERE active='1'"
            sql += " ORDER BY [created_date] Desc "
            Dim DT As DataTable = MD.GetDataTable(sql)
            rptLawDraft.DataSource = DT
            rptLawDraft.DataBind()
            pnlListData.Visible = True
            pnlEnterData.Visible = False
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alert", "<script>document.write('" & ex.Message & "');</script>", False)
        End Try
    End Sub

    Private Sub BindLawDraftByWhere(ByVal id As Integer)
        Try
            Dim sql As String = " SELECT  [subject_id], [subject_desc], [subject_day], [created_user], [created_date]"
            sql &= " FROM [SUBJ] "
            sql &= " WHERE subject_id=" & id
            Dim DT As DataTable = MD.GetDataTable(sql)
            If DT.Rows.Count > 0 Then
                lblLawSubject.Text = DT(0)("subject_desc")
                BindListQuestion(id)
                pnlEnterData.Visible = True
                pnlListData.Visible = False
                BindFileUpload(Request.QueryString("set"))
            Else
                BindAllLawDraft()
            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alert", "<script>document.write('" & ex.Message & "');</script>", False)
        End Try
    End Sub

    Sub BindListQuestion(ByVal id As Integer)
        Try
            Dim sql As String = " SELECT question_id,subj_question,created_date"
            sql &= " FROM SUBJ_QUESTION WHERE  subject_id=" & id

            Dim DT As New DataTable
            DT = MD.GetDataTable(sql)

            If DT.Rows.Count <> 0 Then
                rptListQuestion.DataSource = DT
                rptListQuestion.DataBind()
            End If
           
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alert", "<script>document.write('" & ex.Message & "');</script>", False)
        End Try
    End Sub

    Protected Sub btnEnter_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEnter.Click
        For Each Item In rptListQuestion.Items
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
            For Each Item In rptListQuestion.Items
                If Item.ItemType = ListItemType.Item Or Item.ItemType = ListItemType.AlternatingItem Then
                    Dim HiddenField As HiddenField = Item.FindControl("HiddenField")
                    Dim txtAnswer As TextBox = Item.FindControl("txtAnswer")
                    If txtAnswer.Text <> "" Then

                        SQL = " INSERT SUBJ_ANSWER (question_id,answer,created_date,subject_id)"
                        SQL &= " VALUES ( '" & HiddenField.Value & "','" & txtAnswer.Text & "',"
                        SQL &= " '" & Now.Year & "-" & Now.Month & "-" & Now.Day & "'," & Request.QueryString("set") & ")"
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
            BindAllLawDraft()
        Catch ex As Exception
            TR.Rollback()
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alert", "<script>document.write('" & ex.Message & "');</script>", False)
        Finally
            Conn.Close()
            BindAllLawDraft()
        End Try
    End Sub

    Sub ClearDate()
        For Each Item In rptListQuestion.Items
            If Item.ItemType = ListItemType.Item Or Item.ItemType = ListItemType.AlternatingItem Then
                Dim txtAnswer As TextBox = Item.FindControl("txtAnswer")
                If txtAnswer.Text <> "" Then
                    txtAnswer.Text = ""
                End If
            End If
        Next
    End Sub

    Sub BindFileUpload(ByVal id As Integer)
        Try
            Dim sql As String = " select * from SUBJ_FileUpload Where Subj_ID=" & id

            Dim dt As DataTable
            dt = MD.GetDataTable(sql)

            If dt.Rows.Count > 0 Then
                If dt(0)("mime_type").ToString() = ".pdf" Then
                    Dim strUrl As String = "http://" & Constant.BaseURL(Request) & dt(0)("file_path").ToString() & ""
                    lblDownload.Text = "<a href='" & strUrl & "?time=" & Date.Now.ToString("HH:mm:ss") & "' Target='_Blank'>" & "ดาวน์โหลดเอกสาร" & "</a>&nbsp;&nbsp;"
                ElseIf dt(0)("mime_type").ToString() = ".doc" Or dt(0)("mime_type").ToString() = ".docx" Then
                    Dim strUrl As String = "http://" & Constant.BaseURL(Request) & dt(0)("file_path").ToString()
                    lblDownload.Text = "<a href='" & strUrl & "?time=" & Date.Now.ToString("HH:mm:ss") & "' Target='_Blank'>" & "ดาวน์โหลดเอกสาร" & "</a>&nbsp;&nbsp;"
                ElseIf dt(0)("mime_type").ToString() = ".xls" Or dt(0)("mime_type").ToString() = ".xlsx" Then
                    Dim strUrl As String = "http://" & Constant.BaseURL(Request) & dt(0)("file_path").ToString()
                    lblDownload.Text = "<a href='" & strUrl & "?time=" & Date.Now.ToString("HH:mm:ss") & "' Target='_Blank'>" & "ดาวน์โหลดเอกสาร" & "</a>&nbsp;&nbsp;"
                ElseIf dt(0)("mime_type").ToString() = ".txt" Then
                    Dim strUrl As String = "http://" & Constant.BaseURL(Request) & dt(0)("file_path").ToString()
                    lblDownload.Text = "<a href='" & strUrl & "?time=" & Date.Now.ToString("HH:mm:ss") & "' Target='_Blank'>" & "ดาวน์โหลดเอกสาร" & "</a>&nbsp;&nbsp;"
                End If
                lblDownload.Visible = True
            Else
                lblDownload.Visible = False
            End If

        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alert", "<script>document.write('" & ex.Message & "');</script>", False)
        End Try
    End Sub

    Protected Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
        BindAllLawDraft()
    End Sub
End Class
