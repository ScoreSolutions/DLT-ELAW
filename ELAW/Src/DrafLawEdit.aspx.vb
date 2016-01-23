Imports System.IO
Imports System.Data
Imports System.Data.OleDb
Imports System.Web.Configuration
Imports System.Data.SqlClient

Partial Class Src_DrafLawEdit
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
            dpDate.Text = DateTime.Now
            SetDropdownList()
            If X <> "" Then
                txtLawId.Text = X
                BindDrafLawData(txtLawId.Text)
            End If

            If Request.QueryString("type") = "View" Then
                bSave.Visible = False
                bSaveSend.Visible = False
                bCancel.Visible = False
            End If
        Else
            Me.RefreshPage()
        End If

        bSelectTitle.Attributes.Add("onclick", "popupwindown('ShowBookIn.aspx?id=TextTitle&name=TextIdTitle');")


    End Sub

    Private Sub SetLawData(ByVal LawID As String)

    End Sub


    Private Sub SetDropdownList()
        Me.DataLawType()
        Me.DataLawSubType()
        Me.DataLawStatus()
        Me.DataLawLevel()
        Me.DataEmployee()
    End Sub

    Private Sub DataLawType()
        Dim strsql As String
        strsql = "select type_id,type_name    "
        strsql &= "from law_type order by type_name "

        ddlLawType.SetItemList(MD.GetDataTable(strsql), "type_name", "type_id")
    End Sub
    Private Sub DataLawSubType()

        Dim strsql As String
        strsql = "select subtype_id,subtype_name    "
        strsql &= "from law_subtype where type_id='" & ddlLawType.SelectedValue & "' order by subtype_name "
        ddlLawSubType.SetItemList(MD.GetDataTable(strsql), "subtype_name", "subtype_id")

    End Sub
    Private Sub DataLawStatus()

        Dim strsql As String
        strsql = "select status_id,status_name "
        strsql &= "from law_status where type_id='" & ddlLawType.SelectedValue & "' order by status_id "

        ddlLawStatus.SetItemList(MD.GetDataTable(strsql), "status_name", "status_id")

    End Sub

    Private Sub DataLawLevel()
        Dim sql As String = ""
        sql += " select level_id, level_name "
        sql += " from law_level "
        sql += " order by level_id"

        ddlLawLevel.SetItemList(MD.GetDataTable(sql), "level_name", "level_id")
    End Sub
    Private Sub DataEmployee()
        'แสดงเฉพาะรายชื่อหัวหน้ากลุ่มงานที่อยู่แผนกเดียวกันกับผู้ที่ Login
        Dim uData As New LoginUser
        uData.GetUserData(Page.User.Identity.Name)

        Dim sql As String = ""
        sql += " select e.empid, p.pos_name fullname"
        sql += " from  EMPLOYEE e "
        sql += " left join PREFIX pe on pe.prefix_id=e.prefix"
        sql += " inner join POSITION p on p.pos_id=e.pos_id"
        sql += " where e.pos_id = " & GetLeaderPosID(uData.DivID) & " and e.div_id=" & uData.DivID
        sql += " and e.status='1' "
        ddlSendTo.SetItemList(MD.GetDataTable(sql), "fullname", "empid")

    End Sub
    Private Function GetLeaderPosID(ByVal DivID As Integer) As Integer
        Dim ret As Integer = 0
        Dim sql As String = ""
        sql += " select pos_id from division where div_id = " & DivID
        Return Convert.ToInt64(MD.GetDataTable(sql).Rows(0)("pos_id"))
    End Function
    Protected Sub bSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bSave.Click
        cn = New SqlConnection(MD.strConnIMP)
        cn.Open()
        trans = cn.BeginTransaction(IsolationLevel.ReadCommitted)

        If SaveData(trans) = True Then
            trans.Commit()
            BindDrafLawData(txtLawId.Text)
            MC.MessageBox(Me, "บันทึกลงฐานข้อมูลเรียบร้อยแล้ว")
        Else
            trans.Rollback()
        End If
        cn.Close()
        cn = Nothing
    End Sub

    Protected Sub ddlLawType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlLawType.SelectedIndexChanged
        Me.DataLawSubType()
        Me.DataLawStatus()
    End Sub
    Private Sub Auto()
        'Genarate Case_id
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
    Private Function SaveData(ByVal trans As SqlTransaction) As Boolean
        Dim ret As Boolean = True

        If ValidateData() = True Then
            Dim sEmpNo As String = Page.User.Identity.Name

            Dim vSubTypeID As Integer = ddlLawSubType.SelectedValue
            Dim vStatusID As Integer = ddlLawStatus.SelectedValue
            'Dim vDate As String = dpDate.Text.Year.ToString("0000") & dpDate.Text.ToString("-MM-dd")
            Dim vDate As String = dpDate.SaveDate
            Dim vTitle As String = Replace(txtTitle.Text.Trim(), "'", "''")
            Dim vKeyWord As String = Replace(txtKeyword.Text.Trim(), "'", "''")
            Dim vMessage As String = txtMessage.Value.Trim()
            Dim vLevelID As Integer = ddlLawLevel.SelectedValue
            Dim vSendTo As String = ddlSendTo.SelectedValue
            Dim vNote As String = Replace(txtNote.Text.Trim(), "'", "''")

            Try
                Dim strsql As String
                If txtLawId.Text.Trim() = "" Then
                    txtLawId.Text = GenerateLawID(trans)
                    strsql = "insert into law_data (law_id,subtype_id,status_id,"
                    strsql &= " dates, Title, keyword, note, "
                    strsql &= " creation_by,created_date, message, level_id, sendto, ref_id,ref_bookin,lawer_id) "
                    strsql &= " values  ('" & txtLawId.Text & "','" & vSubTypeID & "', '" & vStatusID & "', "
                    strsql &= " '" & vDate & "', '" & vTitle & "', '" & vKeyWord & "', '" & vNote & "',"
                    strsql &= " '" & sEmpNo & "',getdate(), '" & vMessage & "','" & vLevelID & "','" & vSendTo & "','" & txtLawId.Text & "','" & lblTitle.Text & "','" & sEmpNo & "')"
                Else
                    strsql = " update law_data "
                    strsql &= " set subtype_id='" & vSubTypeID & "',"
                    strsql &= " status_id = '" & vStatusID & "', "
                    strsql &= " dates = '" & vDate & "', "
                    strsql &= " title = '" & vTitle & "', "
                    strsql &= " keyword = '" & vKeyWord & "', "
                    strsql &= " message = '" & vMessage & "', "
                    strsql &= " level_id = '" & vLevelID & "', "
                    strsql &= " note = '" & vNote & "', "
                    strsql &= " sendto = '" & vSendTo & "', "
                    strsql &= " updated_by = '" & sEmpNo & "', "
                    strsql &= " updated_date = getdate() ,"
                    strsql &= " ref_id = '" & lblRefID.Text & "',"
                    strsql &= " ref_bookin = '" & lblTitle.Text & "'"
                    strsql &= " where law_id = '" & txtLawId.Text & "'"
                End If

                cmd = New SqlCommand(strsql, trans.Connection, trans)
                cmd.CommandType = CommandType.Text

                If cmd.ExecuteNonQuery() > 0 Then
                    ClearMsg()
                    ret = True
                Else
                    ret = False
                End If

            Catch ex As Exception
                ret = False
            End Try
        Else
            ret = False
        End If

        Return ret
    End Function



    Protected Sub bSaveSend_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bSaveSend.Click
        cn = New SqlConnection(MD.strConnIMP)
        cn.Open()
        trans = cn.BeginTransaction(IsolationLevel.ReadCommitted)

        If SaveData(trans) = True Then
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
                Dim nextStatus As String = GetNextStatus(ddlLawType.SelectedValue)
                sql = "insert into LAW_DATA (law_id, subtype_id,status_id, "
                sql += " dates, level_id, Title, keyword, message, note,"
                sql += " ref_id, sendto, active,  creation_by,created_date, leader_approve,ref_bookin, lawer_id)"
                sql += " select '" & newLawID & "', subtype_id, '" & nextStatus & "', "
                sql += " dates, level_id, title, keyword, message, note,"
                sql += " ref_id, sendto, '1',  '" & sEmpNo & "', GETDATE(), '1',ref_bookin, lawer_id "
                sql += " from law_data "
                sql += " where law_id='" & txtLawId.Text & "' "

                Dim cmd2 As New SqlCommand(sql, trans.Connection, trans)
                ret = (cmd2.ExecuteNonQuery() > 0)
                If ret = True Then
                    trans.Commit()
                    Response.Redirect("../Src/DrafLawEditList.aspx")
                Else
                    trans.Rollback()
                End If
            Catch ex As SqlException
                ret = False
                trans.Rollback()
                Func.SaveErrLog(ex.Message, sEmpNo)
                MC.MessageBox(Me, ex.Message)
            End Try
        End If
        cn.Close()
        cn = Nothing
    End Sub

    Private Sub BindDrafLawData(ByVal lawID As String)
        Dim sql As String = ""
        sql += " select ld.law_id, ls.type_id, ld.subtype_id, ld.status_id,ld.level_id,"
        sql += " ld.dates, ld.title, ld.keyword, ld.message, ld.sendto, ld.active, ld.ref_id, note,"
        sql += " ld.ref_bookin,b.topic "
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
            ddlLawLevel.SelectedValue = dt.Rows(0)("level_id").ToString()
            dpDate.Text = Convert.ToDateTime(dt.Rows(0)("dates"))
            txtTitle.Text = dt.Rows(0)("title").ToString()
            txtKeyword.Text = dt.Rows(0)("keyword").ToString()
            txtMessage.Value = dt.Rows(0)("message").ToString()
            txtLeaderMessage.Value = GetLeaderMessage(lblRefID.Text)
            lblLeaderMessage.Text = txtLeaderMessage.Value
            lblActive.Text = dt.Rows(0)("active").ToString()
            txtTitleBookIn.Text = dt.Rows(0)("topic").ToString
            lblTitle.Text = dt.Rows(0)("ref_bookin").ToString

            'DataEmployee()
            If Convert.IsDBNull(dt.Rows(0)("note")) = False Then txtNote.Text = dt.Rows(0)("note").ToString()
            If Convert.IsDBNull(dt.Rows(0)("sendto")) = False Then ddlSendTo.SelectedValue = dt.Rows(0)("sendto").ToString()

            ctlPrintLaw1.LawID = txtLawId.Text
            ctlPrintLaw1.Visible = True
            ctlPrintLaw2.Visible = True
        End If
    End Sub
    Private Sub RefreshPage()

        If Session("TextIdTitle") <> "" Then
            txtTitleBookIn.Text = Session("TextTitle")
            lblTitle.Text = Session("TextIdTitle")
        End If
    End Sub

    Private Function GetLeaderMessage(ByVal LawID As String) As String
        Dim ret As String = ""
        'ค้นหาสถานะที่หัวหน้ากลุ่มพิจารณา รอการอนุมัติ
        Dim Sql As String = ""
        Sql = " select lawer_status "
        Sql += " from law_process_status "
        Sql += " where type_id='" & ddlLawType.SelectedValue & "'"
        Dim dt As DataTable = MD.GetDataTable(Sql)
        If dt.Rows.Count > 0 Then

            Sql = " select top 1 message, law_id "
            Sql += " from law_data "
            Sql += " where ref_id='" & lblRefID.Text & "' "
            Sql += " and status_id='" & dt.Rows(0)("lawer_status").ToString() & "' "
            Sql += " order by law_id desc"

            dt = MD.GetDataTable(Sql)
            If dt.Rows.Count > 0 Then
                ctlPrintLaw2.LawID = dt.Rows(0)("law_id").ToString()
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
        ElseIf txtMessage.Value.Trim() = "" Then
            ret = False
            lblChkMessage.Visible = True
            lblChkMessage.Text = "***กรุณาระบุรายละเอียด"
        ElseIf ddlSendTo.SelectedValue = "" Then
            ret = False
            ddlSendTo.ValidMsg = "***กรุณาเลือกส่งต่อเพื่อตรวจ"
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
        lblChkMessage.Text = "*"
        ddlSendTo.ValidMsg = "*"

        lblADocDeatil.Text = "*"
        txtDocPage.ChkNullMsg = "*"
    End Sub
    Protected Sub TabContainer1_ActiveTabChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabContainer1.ActiveTabChanged
        cn = New SqlConnection(MD.strConnIMP)
        cn.Open()
        trans = cn.BeginTransaction(IsolationLevel.ReadCommitted)
        If SaveData(trans) = False Then
            trans.Rollback()
            TabContainer1.ActiveTabIndex = 0  'ถ้ายังไม่บันทึก หรือบันทึกไม่ได้ให้กลับมาอยู่ที่แท็บแรก
        Else
            trans.Commit()
            BindDrafLawData(txtLawId.Text)
            If TabContainer1.ActiveTabIndex = 1 Then 'แท็บเอกสารประกอบ
                GetDocList()
            End If
        End If
        'การทำงานของ TabContainer จะมีปัญหาถ้า Tab อยู่ภายใต้ UpdatePanel

        cn.Close()
        cn = Nothing
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
            'Dim X As String = lblId.Text
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
                    'If UploadFile(sEmpNo, FileUpload1, lblId.Text, lblDocId.Text, fldName) = True Then
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
            Dim oldFile = Func.getServerPath() & GetDocData(e.CommandArgument).Rows(0)("file_path").ToString()
            If MD.Execute(sql) > 0 Then
                Func.DeleteFile(oldFile)
                GetDocList()
            End If
        End If
    End Sub


    Protected Sub gvDocList_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvDocList.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lblPath As Label = e.Row.Cells(ListCellDownload).FindControl("lblPath")
            Dim LblLink As Label = e.Row.Cells(ListCellDownload).FindControl("lblLink")
            lblPath.Text = "<a href='http://" & Constant.BaseURL(Request) & LblLink.Text & "?time=" & Today.Now.ToString("HH:mm:ss") & "' target='_blank' >ดาวน์โหลด</a>"
            'e.Row.Cells(ListCellDownload).Text = "<a href=""javascript:openwindow('" + "BookInPopupPreview" + "','" + lName.Text + "','" + "');"">" + "เลือก" + "</a>"
        End If
    End Sub

    Protected Sub gvDocList_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gvDocList.RowDeleting

    End Sub

    Protected Sub gvDocList_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles gvDocList.RowEditing

    End Sub



    Private Function GetNextStatus(ByVal TypeID As String) As Integer
        'สถานะเมื่อนิติกรบันทึกพร้อมส่ง
        Dim ret As Integer
        Dim sql As String = ""
        sql += "select top 1 s.lawer_status "
        sql += " from law_process_status s"
        sql += " where s.type_id='" & TypeID & "' "

        Dim dt As DataTable = MD.GetDataTable(sql)
        If dt.Rows.Count > 0 Then
            ret = Convert.ToInt64(dt.Rows(0)("lawer_status"))
        End If
        Return ret
    End Function

    Protected Sub bCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bCancel.Click
        BindDrafLawData(txtLawId.Text)

    End Sub

    Protected Sub bCancelFile_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bCancelFile.Click
        BindDocData(Val(lblDocId.Text) + 0)
    End Sub

    Protected Sub bDelTitle_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bDelTitle.Click
        txtTitleBookIn.Text = ""
        lblTitle.Text = ""
    End Sub

    Protected Sub bPreview_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bPreview.Click
        cn = New SqlConnection(MD.strConnIMP)
        cn.Open()
        trans = cn.BeginTransaction(IsolationLevel.ReadCommitted)

        If SaveData(trans) = True Then
            trans.Commit()
            BindDrafLawData(txtLawId.Text)

            Dim popupScript As String = " window.open('../Src/DrafLawPreview.aspx?LawId=" & txtLawId.Text & "', '_blank','resizable=no,scrollbars=no,status=no,location=no,top=10,left=10,width=1015,height=700');"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Preview", popupScript, True)
        Else
            trans.Rollback()
        End If
        cn.Close()
        cn = Nothing
    End Sub

    Protected Sub linkHome_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles linkHome.Click
        Response.Redirect(MD.pHome, True)
    End Sub

    Protected Sub link2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles link2.Click
        Response.Redirect("../Src/DrafLawEditList.aspx", True)
    End Sub

End Class
