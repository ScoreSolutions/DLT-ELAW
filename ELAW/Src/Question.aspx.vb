Imports System.Data
Imports System.Data.OleDb
Partial Class Profile_Question
    Inherits System.Web.UI.Page

    Dim Conn As OleDbConnection
    Dim Com As OleDbCommand
    Dim TR As OleDbTransaction
    Dim MD As New MainData
    Dim MC As New MainClass

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Panel.Visible = False
            btnCreateELAW.Enabled = False

            txtNumberDay.Attributes.Add("onkeypress", "return blockNonNumbers(this, event,-1,0);")
            txtNumberELAW.Attributes.Add("onkeypress", "return blockNonNumbers(this, event,-1,0);")
        End If
    End Sub

    Protected Sub btnCreateELAW_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCreateELAW.Click
        Dim sEmpNo As String = Session("EmpNo")
        If txtELAW.Text = "" Then
            MC.MessageBox(Me, "กรุณาป้อนข้อมูลเรื่อง")
            Exit Sub
        End If
        If Not IsNumeric(txtNumberDay.Text) Then
            MC.MessageBox(Me, "กรุณาป้อนจำนวนวันที่ตั้งหัวข้อ ")
            Exit Sub
        End If
        If Val(txtNumberELAW.Text) >= 30 Then
            MC.MessageBox(Me, "กรุณาป้อนข้อมูลไม่เกิน 30 ข้อ")
            Exit Sub
        End If
        If txtNumberDay.Text = "" Then
            MC.MessageBox(Me, "กรุณาป้อนจำนวนวันที่ตั้งหัวข้อ")
            Exit Sub
        End If
        'If txtCreateUser.Text = "" Then
        '    MC.MessageBox(Me, "ชื่อผู้สร้าง")
        '    Exit Sub
        'End If
        If Not IsNumeric(txtNumberELAW.Text) Then
            MC.MessageBox(Me, "กรุณาป้อนข้อมูลเป็นตัวเลข")
            Exit Sub
        End If
        If txtNumberELAW.Text = "" Then
            MC.MessageBox(Me, "กรุณาป้อนจำนวนหัวข้อ")
            Exit Sub
        End If

        If FileUpload.PostedFile Is Nothing OrElse String.IsNullOrEmpty(FileUpload.PostedFile.FileName) OrElse FileUpload.PostedFile.InputStream Is Nothing Then
            MC.MessageBox(Me, "กรุณาเลือกไฟล์ที่ต้องการอัพโหลด")
            Exit Sub
        End If

        Dim tmpAlert As String = False
        For Each Item As RepeaterItem In rptText.Items
            If Item.ItemType = ListItemType.Item Or Item.ItemType = ListItemType.AlternatingItem Then
                Dim txt As TextBox = Item.FindControl("txt")
                If txt.Text.Trim = "" Then
                    tmpAlert = True
                    Exit For
                End If
            End If
        Next
        If tmpAlert = True Then
            MC.MessageBox(Me, "กรุณาระบุหัวข้อคำถามให้ครบถ้วน")
            Exit Sub
        End If



        Dim SQL As String
        Dim SubjectID As Integer
        SQL = "SELECT  ISNULL(MAX(subject_id),0)+ 1 AS subject_id FROM SUBJ "

        Dim DT As New DataTable
        DT = MD.GetDataTable(SQL)
        SubjectID = CInt(DT.Rows(0)("subject_id"))
        DT = Nothing

        SQL = " INSERT SUBJ ( subject_id,subject_desc,subject_day,created_user,created_date)"
        SQL &= " VALUES ( " & SubjectID & ",'" & txtELAW.Text & "'," & txtNumberDay.Text & ",'" & sEmpNo & "','" & Now.Year & "-" & Now.Month & "-" & Now.Day & "' )"
        Conn = New OleDbConnection(MD.Strcon)
        Com = New OleDbCommand(SQL, Conn)
        Conn.Open()
        TR = Conn.BeginTransaction

        Com.Transaction = TR
        Com.ExecuteNonQuery()
        Dim question_id As Integer = 0
        
        For Each Item As RepeaterItem In rptText.Items
            If Item.ItemType = ListItemType.Item Or Item.ItemType = ListItemType.AlternatingItem Then
                Dim txt As TextBox = Item.FindControl("txt")
                If txt.Text <> "" Then
                    question_id += 1

                    SQL = " INSERT SUBJ_QUESTION ( subject_id,question_id,subj_question,created_date)"
                    SQL &= " VALUES ( '" & SubjectID & "'," & question_id & ",'" & txt.Text & "','" & Now.Year & "-" & Now.Month & "-" & Now.Day & "' )"
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
        Panel.Visible = False
        btnCreateELAW.Enabled = False
        TR.Commit()






        Dim strPath As String = "FileSubj\"
        AutoIDFile()
        Try
            Dim Strsql As String
            Strsql = "insert into SUBJ_FileUpload (File_ID,"
            If FileUpload.HasFile Then
                Strsql &= "file_path,mime_type "
            End If
            Strsql &= ",created_date,creation_by,Subj_ID )"
            Strsql &= " values  "
            Strsql &= " ('" & lblID.Text & "'"
            Dim MIMEType As String = Nothing
            If FileUpload.HasFile Then
                Dim extension As String = System.IO.Path.GetExtension(FileUpload.PostedFile.FileName).ToLower()
                Select Case extension
                    Case ".jpg", ".jpeg", ".jpe"
                        MIMEType = ".jpg"
                        Strsql &= ",'" & strPath & "" & lblID.Text & "" & MIMEType & "','" & MIMEType & "'"
                    Case ".csv"
                        MIMEType = ".csv"
                        Strsql &= ",'" & strPath & "" & lblID.Text & "" & MIMEType & "','" & MIMEType & "'"
                    Case ".xls"
                        MIMEType = ".xls"
                        Strsql &= ",'" & strPath & "" & lblID.Text & "" & MIMEType & "','" & MIMEType & "'"
                    Case ".xlsx"
                        MIMEType = ".xlsx"
                        Strsql &= ",'" & strPath & "" & lblID.Text & "" & MIMEType & "','" & MIMEType & "'"
                    Case ".pdf"
                        MIMEType = ".pdf"
                        Strsql &= ",'" & strPath & "" & lblID.Text & "" & MIMEType & "','" & MIMEType & "'"
                    Case ".doc"
                        MIMEType = ".doc"
                        Strsql &= ",'" & strPath & "" & lblID.Text & "" & MIMEType & "','" & MIMEType & "'"
                    Case ".docx"
                        MIMEType = ".docx"
                        Strsql &= ",'" & strPath & "" & lblID.Text & "" & MIMEType & "','" & MIMEType & "'"
                    Case ".txt"
                        MIMEType = ".txt"
                        Strsql &= ",'" & strPath & "" & lblID.Text & "" & MIMEType & "','" & MIMEType & "'"
                    Case ".htm", ".html"
                        MIMEType = ".html"
                        Strsql &= ",'" & strPath & "" & lblID.Text & "" & MIMEType & "','" & MIMEType & "'"
                    Case Else
                        MC.MessageBox(Me, "Not a valid file format")
                        Exit Sub
                End Select
            End If
            Strsql &= " ,getdate(),99," & SubjectID & ")"

            Dim Y As Integer = MD.Execute(Strsql)
            If Y > 0 Then
                Func.UploadFile("", FileUpload, lblID.Text & MIMEType, strPath)
                'MC.MessageBox(Me, "บันทึกลงฐานข้อมูลเรียบร้อยแล้ว")
            End If
            ClearData()
            MC.MessageBox(Me, "บันทึกข้อมูลเรียบร้อยแล้ว")
        Catch ex As Exception
            TR.Rollback()
            MC.MessageBox(Me, ex.ToString)
        Finally
            Conn.Close()
        End Try
    End Sub

    Sub AutoIDFile()
        Dim sqlTmp As String = ""
        sqlTmp = "SELECT top 1 * From SUBJ_FileUpload order by file_id Desc "

        Dim DT As New DataTable
        DT = MD.GetDataTable(sqlTmp)

        If DT.Rows.Count > 0 Then
            lblID.Text = CInt(DT.Rows(0)("file_id")) + 1
        Else
            lblID.Text = 1
        End If
        DT.Dispose()
        DT = Nothing
    End Sub

    Sub ClearData()
        txtELAW.Text = ""
        txtNumberDay.Text = ""
        txtCreateUser.Text = ""

        txtNumberELAW.Text = ""
        rptText.DataSource = Nothing

        For Each Item As RepeaterItem In rptText.Items
            If Item.ItemType = ListItemType.Item Or Item.ItemType = ListItemType.AlternatingItem Then
                Dim txt As TextBox = Item.FindControl("txt")
                txt.Text = ""
            End If
        Next
    End Sub

    Protected Sub btnAddELAW_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddELAW.Click
        If Not IsNumeric(txtNumberELAW.Text) Then
            MC.MessageBox(Me, "กรุณาป้อนข้อมูลเป็นตัวเลข")
            Exit Sub
        End If
        If IsNumeric(txtNumberELAW.Text) <> 0 Then
            Panel.Visible = True
            btnCreateELAW.Enabled = True

            Dim DT As New DataTable
            DT.Columns.Add("lbl")
            DT.Columns.Add("txt")

            For i As Integer = 0 To Val(txtNumberELAW.Text) - 1
                Dim DR As DataRow = DT.NewRow
                DR("lbl") = "หัวข้อ " & i + 1 & " :"
                DR("txt") = ""
                DT.Rows.Add(DR)
            Next
            rptText.DataSource = DT
            rptText.DataBind()
        Else
            Panel.Visible = False
            btnCreateELAW.Enabled = False
            MC.MessageBox(Me, "กรุณาใส่จำนวนหัวข้อให้ถูกต้อง")
        End If
    End Sub

    Protected Sub lbtnCreate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbtnCreate.Click
        Response.Redirect("Question.aspx")
    End Sub

    Protected Sub lbtnEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbtnEdit.Click
        pnlEdit.Visible = True
        PanelCreate.Visible = False
        GetSubj()
    End Sub

    Sub GetSubj()
        Dim SQL As String
        SQL = "SELECT * FROM SUBJ WHERE ACTIVE=1 "

        Dim DT As New DataTable
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

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        If ddlSubj.SelectedValue <> "null" Then
            HFSubj_id.Value = ddlSubj.SelectedValue

            Dim SQL As String
            SQL = "SELECT * FROM SUBJ_QUESTION WHERE subject_id=" & HFSubj_id.Value

            Dim DT As New DataTable
            DT = MD.GetDataTable(SQL)
            If DT.Rows.Count > 0 Then
                RepeaterQuestions.DataSource = DT
                RepeaterQuestions.DataBind()
                btnUpdate.Visible = True
                bDel.Visible = True
                Dim _SQL As String
                _SQL = "SELECT * FROM SUBJ_FileUpload WHERE Subj_ID=" & HFSubj_id.Value

                Dim _DT As New DataTable
                _DT = MD.GetDataTable(_SQL)
                If _DT.Rows.Count > 0 Then
                    lblFileID.Text = _DT.Rows(0)(0)
                End If
            End If
            trow.Visible = True
        Else
            RepeaterQuestions.DataBind()
            btnUpdate.Visible = False
            bDel.Visible = False
            trow.Visible = False
        End If
    End Sub

    Protected Sub btnUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        Try
            If FileUpload1.PostedFile Is Nothing OrElse String.IsNullOrEmpty(FileUpload1.PostedFile.FileName) OrElse FileUpload1.PostedFile.InputStream Is Nothing Then
                MC.MessageBox(Me, "กรุณาเลือกไฟล์ที่ต้องการอัพโหลด")
                Exit Sub
            End If

            Dim strPath As String = "FileSubj\"
            Dim Strsql As String
            Strsql = "UPDATE SUBJ_FileUpload SET File_ID='" & lblFileID.Text & "', file_path="

            Dim MIMEType As String = Nothing
            If FileUpload1.HasFile Then
                Dim extension As String = System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName).ToLower()

                Select Case extension
                    Case ".jpg", ".jpeg", ".jpe"
                        MIMEType = ".jpg"
                        Strsql &= "'" & strPath & "" & lblFileID.Text & "" & MIMEType & "',"
                    Case ".csv"
                        MIMEType = ".csv"
                        Strsql &= "'" & strPath & "" & lblFileID.Text & "" & MIMEType & "',"
                    Case ".xls"
                        MIMEType = ".xls"
                        Strsql &= "'" & strPath & "" & lblFileID.Text & "" & MIMEType & "',"
                    Case ".xlsx"
                        MIMEType = ".xlsx"
                        Strsql &= "'" & strPath & "" & lblFileID.Text & "" & MIMEType & "',"
                    Case ".pdf"
                        MIMEType = ".pdf"
                        Strsql &= "'" & strPath & "" & lblFileID.Text & "" & MIMEType & "',"
                    Case ".doc"
                        MIMEType = ".doc"
                        Strsql &= "'" & strPath & "" & lblFileID.Text & "" & MIMEType & "',"
                    Case ".docx"
                        MIMEType = ".docx"
                        Strsql &= "'" & strPath & "" & lblFileID.Text & "" & MIMEType & "',"
                    Case ".txt"
                        MIMEType = ".txt"
                        Strsql &= "'" & strPath & "" & lblFileID.Text & "" & MIMEType & "',"
                    Case ".htm", ".html"
                        MIMEType = ".html"
                        Strsql &= "'" & strPath & "" & lblFileID.Text & "" & MIMEType & "',"
                    Case Else
                        MC.MessageBox(Me, "Not a valid file format")
                        Exit Sub
                End Select
            End If

            Strsql &= "mime_type='" & MIMEType & "',created_date= getdate() "

            Strsql &= " WHERE subj_id = " & HFSubj_id.Value & " "
           
            Dim Y As Integer = MD.Execute(Strsql)
            If Y > 0 Then
                Func.UploadFile("", FileUpload1, lblFileID.Text & MIMEType, strPath)
                'MC.MessageBox(Me, "บันทึกลงฐานข้อมูลเรียบร้อยแล้ว")
            End If

            Dim sql As String = ""
            Conn = New OleDbConnection(MD.Strcon)
            Com = New OleDbCommand()
            Conn.Open()

            TR = Conn.BeginTransaction

            For Each Item As RepeaterItem In RepeaterQuestions.Items
                If Item.ItemType = ListItemType.Item Or Item.ItemType = ListItemType.AlternatingItem Then
                    Dim txt As TextBox = Item.FindControl("txt")
                    Dim HF As HiddenField = Item.FindControl("HF")
                    sql = " UPDATE SUBJ_QUESTION SET subj_question = '" & txt.Text & "',"
                    sql &= "  created_date = '" & Now.Year & "-" & Now.Month & "-" & Now.Day & "'"
                    sql &= " WHERE subject_id = " & HFSubj_id.Value & " AND question_id= " & HF.Value & ""
                    With Com
                        .CommandText = sql
                        .CommandType = CommandType.Text
                        .Connection = Conn
                        .Transaction = TR
                        .ExecuteNonQuery()
                    End With
                End If
            Next
            MC.MessageBox(Me, "ปรับปรุงข้อมูลเรียบร้อยแล้ว")
            TR.Commit()
        Catch ex As Exception
            TR.Rollback()
            MC.MessageBox(Me, ex.ToString)
        Finally
            Conn.Close()
        End Try
    End Sub
    Protected Sub bDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bDel.Click
        Dim sql As String

        sql = "update SUBJ set active=0 WHERE subject_id = " & HFSubj_id.Value & ""

        MD.Execute(sql)

        Response.Redirect("Question.aspx")

    End Sub
End Class
