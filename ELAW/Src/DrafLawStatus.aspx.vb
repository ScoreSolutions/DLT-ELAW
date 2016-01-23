Imports System.IO
Imports System.Data
Imports System.Data.OleDb
Imports System.Web.Configuration
Imports System.Data.SqlClient
Partial Class Src_DrafLawStatus
    Inherits System.Web.UI.Page
    Dim cn As SqlConnection
    Dim trans As SqlTransaction
    Dim cmd As SqlCommand
    Dim MD As New MainData
    Dim MC As New MainClass
    Const ListCellDownload As Integer = 2
    Const ListCellDelete As Integer = 3
    Const ListCellEdit As Integer = 4
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim X As String = Request.QueryString("id")

        If Page.IsPostBack = False Then
            SetDropdownList()
            If X <> "" Then
                txtLawId.Text = X
                BindDrafLawData(txtLawId.Text)
                GetDocList()
            End If

        End If

    End Sub
    Private Sub SetDropdownList()
        Me.DataLawType()
        Me.DataLawSubType()
        Me.DataLawStatus()
        Me.DataLawLevel()
    End Sub
    Private Sub DataLawType()
        'ประเภทกฎหมาย
        Dim strsql As String
        strsql = "select type_id,type_name    "
        strsql &= "from law_type order by type_name "

        ddlLawType.SetItemList(MD.GetDataTable(strsql), "type_name", "type_id")
    End Sub
    Private Sub DataLawSubType()
        'ประเภทย่อยกฎหมาย
        Dim strsql As String
        strsql = "select subtype_id,subtype_name    "
        strsql &= "from law_subtype where type_id='" & ddlLawType.SelectedValue & "' order by subtype_name "
        ddlLawSubType.SetItemList(MD.GetDataTable(strsql), "subtype_name", "subtype_id")

    End Sub
    Private Sub DataLawStatus()
        'สถานะการร่างกฎหมาย
        Dim sqlS As String = ""
        sqlS += " select begin_status from law_process_status where type_id='" & ddlLawType.SelectedValue & "' union "
        sqlS += " select lawer_status from law_process_status where type_id='" & ddlLawType.SelectedValue & "' union "
        sqlS += " select leader_status from law_process_status where type_id='" & ddlLawType.SelectedValue & "' "

        Dim strsql As String
        strsql = "select status_id,status_name    "
        strsql &= "from law_status where type_id='" & ddlLawType.SelectedValue & "' "
        strsql += " and status_id not in (" & sqlS & ")"
        strsql += " order by status_id "

        ddlLawStatus.SetItemList(MD.GetDataTable(strsql), "status_name", "status_id")

    End Sub
    Private Sub DataLawLevel()
        'ระดับความสำคัญ
        Dim sql As String = ""
        sql += " select level_id, level_name "
        sql += " from law_level "
        sql += " order by level_id"

        ddlLawLevel.SetItemList(MD.GetDataTable(sql), "level_name", "level_id")
    End Sub
    Protected Sub bSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bSave.Click
        cn = New SqlConnection(MD.strConnIMP)
        cn.Open()
        trans = cn.BeginTransaction(IsolationLevel.ReadCommitted)

        If SaveData(trans) = True Then
            If ddlLawStatus.SelectedValue <> lblCurrStatusID.Text Then
                If SaveNewStatus(trans) = False Then
                    trans.Rollback()
                    Exit Sub
                End If
            End If
            trans.Commit()
            If ddlLawStatus.SelectedValue = GetLastStatus(ddlLawType.SelectedValue) Then
                Response.Redirect("../Src/ImportDocument2.aspx?LawID=" & txtLawId.Text)
            Else
                BindDrafLawData(txtLawId.Text)
                MC.MessageBox(Me, "บันทึกลงฐานข้อมูลเรียบร้อยแล้ว")
            End If

        End If
    End Sub
    Private Function GetLastStatus(ByVal TypeID As Integer) As Integer
        'สถานะสุดท้ายของการร่างกฎหมาย
        Dim sql As String = " select last_status from law_process_status where type_id=" & TypeID
        Return Convert.ToInt64(MD.GetDataTable(sql).Rows(0)("last_status"))

    End Function
    Protected Sub ddlLawType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlLawType.SelectedIndexChanged
        Me.DataLawSubType()
        Me.DataLawStatus()
    End Sub
    Private Sub Auto()
        'Genarate law_id
        Dim sqlTmp As String = ""
        Dim comTmp As OleDbCommand = New OleDbCommand
        Dim drTmp As OleDbDataReader
        Dim tmpMemberID As Integer = 0
        Dim tmpMemberID2 As String = ""

        Dim sYear As String
        sYear = Date.Now.Year

        Dim sAuto As String = sYear

        sqlTmp = "SELECT TOP 1 law_id FROM law_data "
        sqlTmp &= " WHERE left(law_id,4) ='" & sAuto & "'"
        sqlTmp &= " ORDER BY law_id DESC"

        Dim cn As New OleDbConnection(MD.Strcon)
        Dim cmd As New OleDbCommand(sqlTmp, cn)
        cn.Open()

        Try
            With comTmp
                .CommandType = CommandType.Text
                .CommandText = sqlTmp
                .Connection = cn
                drTmp = .ExecuteReader()

                drTmp.Read()

                tmpMemberID2 = Right(drTmp.Item("law_id"), 4)

                tmpMemberID = CInt(tmpMemberID2) + 1
                txtLawId.Text = sAuto + tmpMemberID.ToString("-0000")

            End With
        Catch
            txtLawId.Text = sAuto + "-0001"
        End Try
        cn.Close()

    End Sub

    Private Function GenerateLawID(ByVal trans As SqlTransaction) As String
        'Genarate Law_id
        Dim ret As String = ""
        Dim sqlTmp As String = ""
        Dim comTmp As SqlCommand = New SqlCommand
        Dim drTmp As SqlDataReader
        Dim tmpMemberID As Integer = 0
        Dim tmpMemberID2 As String = ""


        Dim sYear As String
        sYear = Date.Now.Year

        Dim sAuto As String = sYear

        sqlTmp = "SELECT TOP 1 law_id FROM law_data "
        sqlTmp &= " WHERE left(law_id,4) ='" & sAuto & "'"
        sqlTmp &= " ORDER BY law_id DESC"

        Try
            With comTmp
                .CommandType = CommandType.Text
                .CommandText = sqlTmp
                .Connection = trans.Connection
                .Transaction = trans
                drTmp = .ExecuteReader()

                drTmp.Read()

                tmpMemberID2 = Right(drTmp.Item("law_id"), 4)

                tmpMemberID = CInt(tmpMemberID2) + 1
                ret = sAuto + tmpMemberID.ToString("-0000")

            End With
        Catch
            ret = sAuto + "-0001"

        End Try
        'cn.Close()
        drTmp.Close()

        Return ret

    End Function
    Private Function SaveNewStatus(ByVal trans As SqlTransaction) As Boolean
        Dim ret As Boolean = True
        Dim sEmpNo As String = Page.User.Identity.Name
        Dim sql As String = ""
        sql += " update law_data "
        sql += " set active='0' , updated_by ='" & sEmpNo & "', updated_date = getdate()"
        sql += " where law_id='" & txtLawId.Text & "'"

        Try
            Dim cmd As New SqlCommand(sql, trans.Connection, trans)
            cmd.ExecuteNonQuery()

            'Create New LawID
            Dim newLawID As String = GenerateLawID(trans)

            sql = "insert into LAW_DATA (law_id, subtype_id,status_id, "
            sql += " dates, level_id, Title, keyword, message, "
            sql += " leader_approve, note, sendto,"
            sql += " ref_id, active, sendto_director, creation_by,created_date,ref_bookin, lawer_id,other_status)"
            sql += " select '" & newLawID & "', subtype_id, '" & ddlLawStatus.SelectedValue & "', "
            sql += " dates, level_id, title, keyword, message, "
            sql += " leader_approve, note,sendto,  "
            sql += " ref_id, '1', sendto_director, '" & sEmpNo & "', GETDATE(),ref_bookin,lawer_id, other_status"
            sql += " from law_data "
            sql += " where law_id='" & txtLawId.Text & "' "

            Dim cmd2 As New SqlCommand(sql, trans.Connection, trans)
            cmd2.ExecuteNonQuery()
            txtLawId.Text = newLawID
        Catch ex As SqlException
            ret = False
            MC.MessageBox(Me, ex.Message)
        Catch ex As Exception
            ret = False
            MC.MessageBox(Me, ex.Message)
        End Try
        Return ret
    End Function
    Private Function SaveData(ByVal trans As SqlTransaction) As Boolean
        Dim ret As Boolean = True

        If ValidateData() = True Then
            Dim sEmpNo As String = Page.User.Identity.Name

            Dim vSubTypeID As Integer = ddlLawSubType.SelectedValue
            Dim vStatusID As Integer = lblCurrStatusID.Text
            Dim vOtherStatus As String = Replace(txtOtherStatus.Text, "'", "''")
            Dim vDate As String = dpDate.SaveDate
            Dim vTitle As String = Replace(txtTitle.Text.Trim(), "'", "''")
            Dim vKeyWord As String = Replace(txtKeyword.Text.Trim(), "'", "''")
            Dim vMessage As String = txtDirectorMessage.Value.Trim()
            Dim vLevelID As Integer = ddlLawLevel.SelectedValue
            Dim vNote As String = Replace(txtNote.Text.Trim(), "'", "''")

            Try
                Dim strsql As String = ""
                If txtLawId.Text.Trim() = "" Then

                Else
                    strsql = " update law_data "
                    strsql &= " set subtype_id='" & vSubTypeID & "',"
                    strsql &= " status_id = '" & vStatusID & "', "
                    strsql &= " dates = '" & vDate & "', "
                    strsql &= " title = '" & vTitle & "', "
                    strsql &= " keyword = '" & vKeyWord & "', "
                    strsql &= " message = '" & vMessage & "', "
                    strsql &= " level_id = '" & vLevelID & "', "
                    strsql &= " note = '" & vNote & "',"
                    strsql &= " updated_by = '" & sEmpNo & "', "
                    strsql &= " updated_date = getdate(), "
                    strsql &= " ref_bookin='" & lblTitle.Text & "', "
                    strsql &= " other_status = '" & vOtherStatus & "'"
                    strsql &= " where law_id = '" & txtLawId.Text & "'"
                End If

                cmd = New SqlCommand(strsql, cn, trans)
                cmd.CommandType = CommandType.Text
                If cmd.ExecuteNonQuery() > 0 Then
                    ClearMsg()
                    ret = True
                Else
                    ret = False
                End If

            Catch ex As Exception
                ret = False
                Dim ses As String = System.Web.HttpContext.Current.Session.SessionID
                Dim url As String = HttpContext.Current.Request.FilePath
                Dim browser As String = " Type:" + Request.Browser.Type + " Version:" + Request.Browser.Version + " Browser:" + Request.Browser.Browser

                MC.MessageBox(Me, ex.ToString)
                MC.ErrLog(url, ses, ex.ToString, sEmpNo, browser)
            End Try
        Else
            ret = False
        End If

        Return ret
    End Function
    Private Sub BindDrafLawData(ByVal lawID As String)
        Dim sql As String = ""
        sql += " select ld.law_id, ls.type_id, ld.subtype_id, ld.status_id,ld.level_id,ld.ref_id,"
        sql += " ld.dates, ld.title, ld.keyword, ld.message, ld.ref_id, ld.sendto_director, ld.leader_approve, ld.note, "
        sql += " ld.ref_bookin,b.topic, ld.other_status "
        sql += " from law_data ld"
        sql += " inner join law_subtype ls on ls.subtype_id=ld.subtype_id"
        sql += " left join bookin_data b on ld.ref_bookin=b.bookin_id "
        sql += " where ld.law_id= '" & lawID & "'"

        Dim dt As DataTable = MD.GetDataTable(sql)
        If dt.Rows.Count > 0 Then
            txtLawId.Text = dt.Rows(0)("law_id").ToString()
            If Convert.IsDBNull(dt.Rows(0)("ref_id")) = False Then lblRefID.Text = dt.Rows(0)("ref_id").ToString()
            ddlLawType.SelectedValue = dt.Rows(0)("type_id").ToString()
            DataLawSubType()
            DataLawStatus()
            ddlLawSubType.SelectedValue = dt.Rows(0)("subtype_id").ToString()
            ddlLawStatus.SelectedValue = dt.Rows(0)("status_id").ToString()
            txtOtherStatus.Enabled = (Convert.ToInt64(ddlLawStatus.SelectedValue) = getOtherStatus(ddlLawType.SelectedValue))
            txtOtherStatus.Text = dt.Rows(0)("other_status").ToString()
            lblCurrStatusID.Text = dt.Rows(0)("status_id").ToString()
            ddlLawLevel.SelectedValue = dt.Rows(0)("level_id").ToString()
            dpDate.Text = Convert.ToDateTime(dt.Rows(0)("dates"))
            txtTitle.Text = dt.Rows(0)("title").ToString()
            txtKeyword.Text = dt.Rows(0)("keyword").ToString()
            txtDirectorMessage.Value = GetDirectorMessage(lblRefID.Text)
            lblDirectorMessage.Text = txtDirectorMessage.Value
            txtTitleBookIn.Text = dt.Rows(0)("topic").ToString
            lblTitle.Text = dt.Rows(0)("ref_bookin").ToString


            If Convert.IsDBNull(dt.Rows(0)("note")) = False Then txtNote.Text = dt.Rows(0)("note").ToString()

            'ผู้ใช้ที่ Login มีตำแหน่งเป็นผู้อำนวยการหรือป่าว
            Dim uData As New LoginUser
            uData.GetUserData(Page.User.Identity.Name)

            If ddlLawStatus.SelectedValue <> GetLawerStatus(ddlLawType.SelectedValue) Then
                If uData.PosID = Constant.DirectorPosID Then
                    lblIsDirector.Text = "Y"
                End If
            End If

            If dt.Rows(0)("ref_bookin").ToString <> "" Then
                LinkDetail.Text = "<a href=""javascript:openwindow('" + "BookInPopupPreview" + "','" + dt.Rows(0)("ref_bookin").ToString + "','" + "');"">" + "ดูรายละเอียด" + "</a>"
            Else
                LinkDetail.Visible = False
            End If
            ctlPrintLaw1.LawID = txtLawId.Text
            ctlPrintLaw1.Visible = True

        End If
    End Sub
    Private Function GetLawerStatus(ByVal TypeID As String) As String
        Dim sql As String = ""
        sql += " select lawer_status "
        sql += " from law_process_status "
        sql += " where type_id = '" & TypeID & "'"

        Return MD.GetDataTable(sql).Rows(0)("lawer_status").ToString()
    End Function
    Private Function GetDirectorMessage(ByVal RefID As String) As String
        Dim ret As String = ""

        'ค้นหาสถานะที่หัวหน้ากลุ่มพิจารณา รอการอนุมัติ
        Dim Sql As String = ""
        Sql = " select leader_status "
        Sql += " from law_process_status "
        Sql += " where type_id='" & ddlLawType.SelectedValue & "'"
        Dim dt As DataTable = MD.GetDataTable(Sql)
        If dt.Rows.Count > 0 Then

            Sql = " select top 1 message, law_id "
            Sql += " from law_data "
            Sql += " where ref_id='" & lblRefID.Text & "' "
            Sql += " and status_id='" & dt.Rows(0)("leader_status").ToString() & "' "
            Sql += " and leader_approve = '1' "
            Sql += " order by law_id desc"

            dt = MD.GetDataTable(Sql)
            If dt.Rows.Count > 0 Then
                ret = dt.Rows(0)("message").ToString()
            End If
        End If

        Return ret
    End Function
    Public Function ValidateData() As Boolean
        Dim ret As Boolean = True
        ClearMsg()
        If ddlLawType.SelectedValue = "" Then
            ret = False
            ddlLawType.ValidMsg = "***กรุณาเลือกประเภทกฎหมาย"
        ElseIf ddlLawSubType.SelectedValue = "" Then
            ret = False
            ddlLawSubType.ValidMsg = "***กรุณาเลือกประเภทย่อย"
        ElseIf ddlLawLevel.SelectedValue = "" Then
            ret = False
            ddlLawLevel.ValidMsg = "***กรุณาเลือกความเร่งด่วน"
        ElseIf dpDate.Text.Year = 1 Then
            ret = False
            dpDate.ErrMsg = "***กรุณาเลือกวันที่"
        ElseIf txtTitle.Text.Trim() = "" Then
            ret = False
            lblChkTitle.Text = "***กรุณาระบุชื่อเรื่อง"
        ElseIf txtKeyword.Text.Trim() = "" Then
            ret = False
            lblChkKeyword.Text = "***กรุณาระบุคำค้นหา"
        ElseIf Convert.ToInt64(ddlLawStatus.SelectedValue) = getOtherStatus(ddlLawType.SelectedValue) Then
            If txtOtherStatus.Text = "" Then
                ret = False
                txtOtherStatus.ChkNullMsg = "***กรุณาระบุสถานะอื่นๆ"
            End If
        End If

        Return ret
    End Function
    Private Function ValidSaveFile() As Boolean
        Dim ret As Boolean = True
        If FileUpload1.PostedFile Is Nothing OrElse String.IsNullOrEmpty(FileUpload1.PostedFile.FileName) OrElse FileUpload1.PostedFile.InputStream Is Nothing Then
            If lblFileName.Text.Trim = "" Then
                lblAFile.Text = "***กรุณาเลือกไฟล์ที่ต้องการอัพโหลด"
                lblADocDeatil.Text = ""
                ret = False
            End If
        ElseIf txtDocDetail.Text.Trim = "" Then
            lblADocDeatil.Text = "***กรุณาระรายละเอียดของไฟล์"
            ret = False
        ElseIf txtDocPage.Text.Trim = "" Or txtDocPage.Text.Trim = "0" Then
            txtDocPage.ChkNullMsg = "***กรุณาระบุจำนวนหน้าของเอกสาร"
            ret = False
        End If

        Return ret
    End Function
    Private Sub ClearMsg()
        ddlLawType.ValidMsg = "*"
        ddlLawStatus.ValidMsg = "*"
        ddlLawStatus.ValidMsg = "*"
        ddlLawLevel.ValidMsg = "*"
        dpDate.ErrMsg = "*"
        lblChkTitle.Text = "*"
        lblChkKeyword.Text = "*"
        lblValidMessage.Visible = False
        lblADocDeatil.Text = "*"
        txtDocPage.ChkNullMsg = "*"
    End Sub
    Private Sub GetDocList()
        Dim sql As String = ""
        sql += " select document_id, title, page, run_id, file_path"
        sql += " from law_document "
        sql += " where law_id = '" & txtLawId.Text & "'"
        sql += " order by run_id"

        Dim dt As DataTable = MD.GetDataTable(sql)
        If dt.Rows.Count > 0 Then
            gvDocList.DataSource = dt
            gvDocList.DataBind()
        Else
            sql = " select ld.document_id, ld.title, ld.page, ld.run_id, ld.file_path"
            sql += " from law_document ld "
            sql += " inner join law_data l on l.ref_id=ld.law_id"
            sql += " where l.ref_id = '" & lblRefID.Text & "' and l.law_id='" & txtLawId.Text & "'"
            sql += " order by ld.run_id"

            Dim dt2 As DataTable = MD.GetDataTable(sql)
            If dt2.Rows.Count > 0 Then
                gvDocList.DataSource = dt2
                gvDocList.DataBind()
            Else
                gvDocList.DataSource = Nothing
                gvDocList.DataBind()
            End If
        End If
    End Sub
    Private Function GetDocData(ByVal DocID As Integer) As DataTable
        'Dim ret As DataTable
        Dim sql As String = ""
        sql += " select document_id, law_id,title, page, run_id, file_path,"
        sql += " creation_by, created_date, updated_by,updated_date"
        sql += " from law_document "
        sql += " where document_id = " & DocID

        Return MD.GetDataTable(sql)
    End Function
    Private Sub BindDocData(ByVal DocID As Integer)
        Dim dt As DataTable = GetDocData(DocID)
        If dt.Rows.Count > 0 Then
            If Convert.IsDBNull(dt.Rows(0)("file_path")) = False Then
                lblFileName.Visible = True
                lblFileName.Text = dt.Rows(0)("file_path").ToString()
            End If
            If Convert.IsDBNull(dt.Rows(0)("document_id")) = False Then lblDocId.Text = dt.Rows(0)("document_id").ToString()
            If Convert.IsDBNull(dt.Rows(0)("title")) = False Then txtDocDetail.Text = dt.Rows(0)("title").ToString()
            If Convert.IsDBNull(dt.Rows(0)("page")) = False Then txtDocPage.Text = dt.Rows(0)("page").ToString()
        Else
            ClearFileForm()
        End If
    End Sub
    Private Sub ClearFileForm()
        lblAFile.Text = "*"
        lblFileName.Text = ""
        lblStatus.Text = ""
        lblDocId.Text = ""
        txtDocDetail.Text = ""
        txtDocPage.Text = ""
        lblADocDeatil.Text = "*"
        txtDocPage.ChkNullMsg = "*"
    End Sub
    Protected Sub bSaveFile_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bSaveFile.Click
        If ValidSaveFile() = True Then
            Dim sEmpNo As String = Page.User.Identity.Name
            Dim fldName As String = "Document\LAW\"
            Dim fileName As String = ""
            Dim oldFile As String = ""

            Dim sql As String = ""
            If lblDocId.Text.Trim = "" Then
                lblDocId.Text = getDocNextID()
                Dim vRunID As Integer = getRunID(txtLawId.Text)
                Dim MIMEType As String = Func.getMIMEType(FileUpload1.PostedFile.FileName)
                Dim FilePath As String = fldName & "" & txtLawId.Text & "-" & lblDocId.Text & "" & MIMEType
                fileName = txtLawId.Text & "-" & lblDocId.Text & MIMEType

                sql += "INSERT INTO [LAW_DOCUMENT] ([document_id],[law_id],[run_id],"
                sql += "[Title],[file_path],[page],[creation_by],[created_date])"
                sql += "VALUES('" & lblDocId.Text & "','" & lblRefID.Text & "'," & vRunID & ","
                sql += " '" & Replace(txtDocDetail.Text, "'", "''") & "', '" & FilePath & "','" & txtDocPage.Text & "',"
                sql += " '" & sEmpNo & "',getdate())"

            Else
                Dim MIMEType As String = ""
                If FileUpload1.HasFile = True Then
                    MIMEType = Func.getMIMEType(FileUpload1.PostedFile.FileName)
                Else
                    MIMEType = Func.getMIMEType(lblFileName.Text)
                End If

                Dim FilePath As String = fldName & "" & txtLawId.Text & "-" & lblDocId.Text & "" & MIMEType
                fileName = txtLawId.Text & "-" & lblDocId.Text & MIMEType
                oldFile = Func.getServerPath() & GetDocData(lblDocId.Text).Rows(0)("file_path").ToString()

                sql += " update law_document "
                sql += " set title = '" & Replace(txtDocDetail.Text, "'", "''") & "', "
                sql += " file_path = '" & FilePath & "', "
                sql += " page = '" & txtDocPage.Text & "', "
                sql += " updated_by ='" & sEmpNo & "', updated_date = getdate() "
                sql += " where document_id = '" & lblDocId.Text & "' "
            End If

            If MD.Execute(sql) > 0 Then
                If FileUpload1.HasFile = True Then
                    Func.DeleteFile(oldFile)   'ลบไฟล์เก่าก่อนจะ Upload File ใหม่
                    If Func.UploadFile(sEmpNo, FileUpload1, fileName, fldName) = True Then
                        ClearFileForm()
                        GetDocList()
                        MC.MessageBox(Me, "บันทึกลงฐานข้อมูลเรียบร้อยแล้ว")
                    Else
                        MC.MessageBox(Me, "ไม่สามารถอัพโหลดไฟล์ได้")
                    End If
                Else
                    ClearFileForm()
                    GetDocList()
                    MC.MessageBox(Me, "บันทึกลงฐานข้อมูลเรียบร้อยแล้ว")
                End If
            Else
                MC.MessageBox(Me, "ไม่สามารถบันทึกลงฐานข้อมูลได้")
            End If
        End If

    End Sub
    Private Function getRunID(ByVal LawID As String) As Integer
        Dim ret As String = ""
        Dim sql As String = ""
        sql += "select top 1 run_id from law_document where law_id='" & LawID & "' order by run_id desc"
        Dim dt As DataTable = MD.GetDataTable(sql)
        If dt.Rows.Count > 0 Then
            If Convert.IsDBNull(dt.Rows(0)("run_id")) = False Then
                ret = Convert.ToInt64(dt.Rows(0)("run_id")) + 1
            Else
                ret = 1
            End If
        Else
            ret = 1
        End If

        Return ret
    End Function
    Private Function getDocNextID() As Integer
        Dim ret As String = ""
        Dim sql As String = ""
        sql += "select top 1 document_id from law_document order by document_id desc"
        Dim dt As DataTable = MD.GetDataTable(sql)
        If dt.Rows.Count > 0 Then
            If Convert.IsDBNull(dt.Rows(0)("document_id")) = False Then
                ret = Convert.ToInt64(dt.Rows(0)("document_id")) + 1
            Else
                ret = 1
            End If
        Else
            ret = 1
        End If

        Return ret
    End Function
    Private Function getFileName(ByVal vFileName As String, ByVal strPath As String, ByVal lblID As String, ByVal lblDocID As String) As String
        Dim ret As String = ""
        Dim extension As String = System.IO.Path.GetExtension(vFileName).ToLower()
        Dim MIMEType As String = ""

        Select Case extension
            Case ".jpg", ".jpeg", ".jpe"
                MIMEType = ".jpg"
                ret = strPath & "" & lblID & "-" & lblDocID & "" & MIMEType
            Case ".csv", ".xls", ".xlsx", ".pdf", ".doc", ".docx", ".txt"
                MIMEType = extension
                ret = strPath & "" & lblID & "-" & lblDocID & "" & MIMEType
            Case ".htm", ".html"
                MIMEType = ".html"
                ret = strPath & "" & lblID & "-" & lblDocID & "" & MIMEType
            Case Else
                ret = ""
        End Select

        Return ret
    End Function
    Protected Sub gvDocList_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvDocList.RowCommand
        If e.CommandName = "Edit" Then
            BindDocData(e.CommandArgument)
        ElseIf e.CommandName = "Delete" Then
            Dim sql As String = "delete from law_document where document_id = '" & e.CommandArgument & "'"
            MD.Execute(sql)
            GetDocList()
        End If
    End Sub
    Protected Sub gvDocList_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvDocList.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lblPath As Label = e.Row.Cells(ListCellDownload).FindControl("lblPath")
            Dim LblLink As Label = e.Row.Cells(ListCellDownload).FindControl("lblLink")
            lblPath.Text = "<a href='http://" & Constant.BaseURL(Request) & LblLink.Text & "?time=" & Today.Now.ToString("HH:mm:ss") & "' target='_blank' >ดาวน์โหลด</a>"
        End If
    End Sub
    Protected Sub gvDocList_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gvDocList.RowDeleting

    End Sub
    Protected Sub gvDocList_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles gvDocList.RowEditing

    End Sub
    Private Function GetPrevStatus(ByVal TypeID As String) As Integer
        'สถานะก่อนการพิจารณาอนุมัติ
        Dim ret As Integer
        Dim sql As String = ""
        If lblIsDirector.Text = "Y" Then
            sql += "select top 1 s.lawer_status AS status_id"
        Else
            sql += "select top 1 s.begin_status AS status_id "
        End If
        sql += " from law_process_status s"
        sql += " where s.type_id='" & TypeID & "' "

        Dim dt As DataTable = MD.GetDataTable(sql)
        If dt.Rows.Count > 0 Then
            ret = Convert.ToInt64(dt.Rows(0)("status_id"))
        End If
        Return ret
    End Function
    Private Function GetNextStatus(ByVal TypeID As String) As Integer
        'สถานะหลังจากอนุมัติ
        Dim ret As Integer
        Dim sql As String = ""
        If lblIsDirector.Text = "Y" Then
            sql += "select top 1 s.director_status AS status_id"
        Else
            sql += "select top 1 s.leader_status AS status_id "
        End If
        sql += " from law_process_status s"
        sql += " where s.type_id='" & TypeID & "' "

        Dim dt As DataTable = MD.GetDataTable(sql)
        If dt.Rows.Count > 0 Then
            ret = Convert.ToInt64(dt.Rows(0)("status_id"))
        End If
        Return ret
    End Function
    Protected Sub bCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bCancel.Click
        BindDrafLawData(txtLawId.Text)

    End Sub
    Protected Sub bCancelFile_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bCancelFile.Click
        BindDocData(Val(lblDocId.Text) + 0)
    End Sub
    Protected Sub TabContainer1_ActiveTabChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabContainer1.ActiveTabChanged
        cn = New SqlConnection(MD.strConnIMP)
        cn.Open()
        trans = cn.BeginTransaction(IsolationLevel.ReadCommitted)

        If SaveData(trans) = False Then
            trans.Rollback()
            If lblIsDirector.Text = "N" Then
                TabContainer1.ActiveTabIndex = 0
            Else
                TabContainer1.ActiveTabIndex = 1
            End If
        Else
            trans.Commit()
            BindDrafLawData(txtLawId.Text)
        End If
        cn.Close()
        cn = Nothing
    End Sub

    Protected Sub bDelTitle_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bDelTitle.Click
        txtTitleBookIn.Text = ""
        lblTitle.Text = ""
    End Sub

    Protected Sub linkHome_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles linkHome.Click
        Response.Redirect(MD.pHome, True)
    End Sub

    Protected Sub link2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles link2.Click
        Response.Redirect("../Src/DrafLawStatusList.aspx", True)
    End Sub

    Protected Sub ddlLawStatus_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlLawStatus.SelectedIndexChanged
        If Convert.ToInt64(ddlLawStatus.SelectedValue) = getOtherStatus(ddlLawType.SelectedValue) Then
            txtOtherStatus.Enabled = True

        Else
            txtOtherStatus.Enabled = False
            txtOtherStatus.Text = ""
        End If
    End Sub
    Private Function getOtherStatus(ByVal TypeID As String) As Integer
        Dim sql As String = "select other_status from law_process_status where type_id= '" & TypeID & "' "
        Return Convert.ToInt64(MD.GetDataTable(sql).Rows(0)("other_status"))
    End Function
End Class
