Imports System.Data
Imports System.Data.OleDb
Partial Class ClientQuetions
    Inherits System.Web.UI.Page
    Dim Conn As OleDbConnection
    Dim Com As OleDbCommand
    Dim TR As OleDbTransaction
    Dim MD As New MainData
    Dim MC As New MainClass

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            clear()
        End If
    End Sub

    Private Sub BindLAW_TYPE()
        Dim SQL As String
        Dim DT As New DataTable
        SQL = " SELECT ' ' + type_name as type_name  FROM LAW_TYPE WHERE type_name <> 'อื่นๆ'"
        DT = MD.GetDataTable(SQL)
        If DT.Rows.Count > 0 Then
            With Me.cblLAW_TYPE
                .DataSource = DT.DefaultView
                .DataTextField = "type_name"
                .DataValueField = "type_name"
                .DataBind()
            End With
        End If
    End Sub

    Private Sub BindCONTRACT_TYPE()
        Dim SQL As String
        Dim DT As New DataTable
        SQL = " SELECT ' ' + type_name as type_name FROM CONTRACT_TYPE WHERE type_name <> 'อื่นๆ'"

        DT = MD.GetDataTable(SQL)
        If DT.Rows.Count > 0 Then
            With Me.cblCONTRACT_TYPE
                .DataSource = DT.DefaultView
                .DataTextField = "type_name"
                .DataValueField = "type_name"
                .DataBind()
            End With
        End If
    End Sub

    Private Sub BindCASE_TYPE()
        Dim SQL As String
        Dim DT As New DataTable
        SQL = " SELECT ' ' + type_name as type_name FROM CASE_TYPE WHERE type_name <> 'อื่นๆ'"
        DT = MD.GetDataTable(SQL)
        If DT.Rows.Count > 0 Then
            With Me.cblCASE_TYPE
                .DataSource = DT.DefaultView
                .DataTextField = "type_name"
                .DataValueField = "type_name"
                .DataBind()
            End With
        End If
    End Sub

    Private Sub BindMaxIDQuestion()
        Dim SQL As String
        Dim DT As New DataTable
        SQL = " SELECT IDQuestion FROM WQUESTION ORDER BY IDQuestion DESC"
        DT = MD.GetDataTable(SQL)
        If DT.Rows.Count > 0 Then
            lblIDQuestion.Text = CInt(DT(0)(0)) + 1
        Else
            lblIDQuestion.Text = 1
        End If
    End Sub

    Protected Sub btnSent_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSent.Click

        Dim Total As Integer = 0
        For i = 0 To cblLAW_TYPE.Items.Count - 1
            If cblLAW_TYPE.Items(i).Selected Then
                Total += 1
            End If
        Next
        For i = 0 To cblCONTRACT_TYPE.Items.Count - 1
            If cblCONTRACT_TYPE.Items(i).Selected Then
                Total += 1
            End If
        Next
        For i = 0 To cblCASE_TYPE.Items.Count - 1
            If cblCASE_TYPE.Items(i).Selected Then
                Total += 1
            End If
        Next
        If Total = 0 And txtOther.Text = "" Then
            MC.MessageBox(Me, "กรุณาเลือกหัวข้อที่สนใจอย่างน้อย 1 หัวข้อ หรือ ระบุในหัวข้ออื่นๆ")
            Exit Sub
        End If

        If txtQuestion.Text = "" Then
            MC.MessageBox(Me, "กรุณาป้อนคำถาม")
            Exit Sub
        End If
        If txtName.Text = "" Then
            MC.MessageBox(Me, "กรุณาป้อนชื่อผู้ฝากคำถาม")
            Exit Sub
        End If
        If txtEmail.Text = "" Then
            MC.MessageBox(Me, "กรุณาป้อนอีเมล์")
            Exit Sub
        End If
       
        BindMaxIDQuestion()
        Dim sql As String = ""
        Try
            sql = " INSERT WQUESTION ( IDQuestion,Question,Email,creation_by,created_date)"
            sql &= " VALUES ( " & lblIDQuestion.Text & ",'" & txtQuestion.Text & "','" & txtEmail.Text & "','" & txtName.Text & "','" & Now.Year & "-" & Now.Month & "-" & Now.Day & "' )"

            Conn = New OleDbConnection(MD.Strcon)
            Com = New OleDbCommand(sql, Conn)
            Conn.Open()
            TR = Conn.BeginTransaction

            Com.Transaction = TR
            Com.ExecuteNonQuery()

            For i = 0 To cblLAW_TYPE.Items.Count - 1
                If cblLAW_TYPE.Items(i).Selected Then
                    sql = " INSERT WSTAT_INTEREST ( Law_Type,LawGroup,creation_by,created_date,IDQuestion)"
                    sql &= " VALUES ( '" & cblLAW_TYPE.Items(i).Text & "','กฏหมาย','" & txtName.Text & "','" & Now.Year & "-" & Now.Month & "-" & Now.Day & "' ," & lblIDQuestion.Text & ")"

                    With Com
                        .CommandText = sql
                        .CommandType = CommandType.Text
                        .Connection = Conn
                        .Transaction = TR
                        .ExecuteNonQuery()
                    End With
                End If
            Next
            For i = 0 To cblCONTRACT_TYPE.Items.Count - 1
                If cblCONTRACT_TYPE.Items(i).Selected Then
                    sql = " INSERT WSTAT_INTEREST ( Law_Type,LawGroup,creation_by,created_date,IDQuestion)"
                    sql &= " VALUES ( '" & cblCONTRACT_TYPE.Items(i).Text & "','สัญญา','" & txtName.Text & "','" & Now.Year & "-" & Now.Month & "-" & Now.Day & "' ," & lblIDQuestion.Text & ")"

                    With Com
                        .CommandText = sql
                        .CommandType = CommandType.Text
                        .Connection = Conn
                        .Transaction = TR
                        .ExecuteNonQuery()
                    End With
                End If
            Next
            For i = 0 To cblCASE_TYPE.Items.Count - 1
                If cblCASE_TYPE.Items(i).Selected Then
                    sql = " INSERT WSTAT_INTEREST ( Law_Type,LawGroup,creation_by,created_date,IDQuestion)"
                    sql &= " VALUES ( '" & cblCASE_TYPE.Items(i).Text & "','คดี','" & txtName.Text & "','" & Now.Year & "-" & Now.Month & "-" & Now.Day & "' ," & lblIDQuestion.Text & ")"

                    With Com
                        .CommandText = sql
                        .CommandType = CommandType.Text
                        .Connection = Conn
                        .Transaction = TR
                        .ExecuteNonQuery()
                    End With
                End If
            Next
            If txtOther.Text <> "" Then
                sql = " INSERT WSTAT_INTEREST ( Law_Type,LawGroup,creation_by,created_date,IDQuestion)"
                sql &= " VALUES ( '" & txtOther.Text & "','อื่นๆ','" & txtName.Text & "','" & Now.Year & "-" & Now.Month & "-" & Now.Day & "' ," & lblIDQuestion.Text & ")"

                With Com
                    .CommandText = sql
                    .CommandType = CommandType.Text
                    .Connection = Conn
                    .Transaction = TR
                    .ExecuteNonQuery()
                End With
            End If
            TR.Commit()
            clear()
            MC.MessageBox(Me, "ส่งคำถามเรียบร้อยแล้ว")
        Catch ex As Exception
            TR.Rollback()
            MC.MessageBox(Me, ex.ToString)
        Finally
            Conn.Close()
        End Try
    End Sub

    Sub clear()
        BindLAW_TYPE()
        BindCONTRACT_TYPE()
        BindCASE_TYPE()
        txtOther.Text = ""
        txtName.Text = ""
        txtQuestion.Text = ""
        txtEmail.Text = ""
    End Sub
End Class
