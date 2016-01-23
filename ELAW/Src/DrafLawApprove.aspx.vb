Imports System.IO
Imports System.Data
Imports System.Data.OleDb
Imports System.Web.Configuration
Imports System.Data.SqlClient
Partial Class Src_DrafLawApprove
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
            If lblIsDirector.Text = "Y" Then
                'pnlSendTo.Visible = False
                pnlLeaderMessage.Visible = True
                txtLeaderMessage.Visible = False
                pnlDirectorMessage.Visible = False
                txtDirectorMessage.Visible = True
                lblLeaderNote.Visible = True
                TabContainer1.ActiveTabIndex = "1"
            Else
                pnlLeaderMessage.Visible = False
                txtLeaderMessage.Visible = True
                pnlDirectorMessage.Visible = True
                txtDirectorMessage.Visible = False
                lblLeaderNote.Visible = False
                TabContainer1.ActiveTabIndex = "0"
            End If
        Else
            Me.RefreshPage()
        End If

        bSelectTitle.Attributes.Add("onclick", "popupwindown('ShowBookIn.aspx?id=TextTitle&name=TextIdTitle');")

    End Sub
    Private Sub SetDropdownList()
        Me.DataLawType()
        Me.DataLawSubType()
        Me.DataLawStatus()
        Me.DataLawLevel()
        Me.DataEmployee()
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
        'สถานะกฎหมาย
        Dim strsql As String
        strsql = "select status_id,status_name    "
        strsql &= "from law_status where type_id='" & ddlLawType.SelectedValue & "' order by status_name "

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
    Private Sub DataEmployee()
        'แสดงรายชื่อผู้ที่มีตำแหน่งเป็น ผอ หรือ หัวหน้ากลุ่มงาน
        Dim sql As String = ""
        If lblIsDirector.Text = "Y" Then
            'ถ้าเป็นการอนุมัติของ ผอ ให้แสดงชื่อหัวหน้ากลุ่มงาน (กรณีส่งกลับ)
            sql += " select e.empid, pe.prefix_name + '' + e.firstname + ' ' + e.lastname fullname"
            sql += " from DIVISION d"
            sql += " inner join POSITION p on p.pos_id=d.pos_id"
            sql += " inner join EMPLOYEE e on e.pos_id=p.pos_id"
            sql += " left join PREFIX pe on pe.prefix_id=e.prefix"
            sql += " where e.status='1'"
        Else
            'ถ้าเป็นการอนุมัติของ ผอ ให้แสดงตำแหน่ง ผอ
            sql += " select e.empid, ps.pos_name fullname"
            sql += " from  EMPLOYEE e "
            sql += " left join PREFIX pe on pe.prefix_id=e.prefix"
            sql += " inner join position ps on ps.pos_id=e.pos_id"
            sql += " where e.pos_id=" & Constant.DirectorPosID
            sql += " and e.status <> '0' "
        End If
        ddlSendTo.SetItemList(MD.GetDataTable(sql), "fullname", "empid")

        'SetddlLawer()
    End Sub
    Protected Sub bSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bSave.Click
        cn = New SqlConnection(MD.strConnIMP)
        cn.Open()
        trans = cn.BeginTransaction(IsolationLevel.ReadCommitted)

        Dim sEmpNo As String = Page.User.Identity.Name
        Try
            If SaveData(trans) = True Then
                trans.Commit()
                BindDrafLawData(txtLawId.Text)
                MC.MessageBox(Me, "บันทึกลงฐานข้อมูลเรียบร้อยแล้ว")
            Else
                trans.Rollback()
            End If
        Catch ex As SqlException
            trans.Rollback()
            Func.SaveErrLog(ex.Message, sEmpNo)
        End Try

        cn.Close()
        cn = Nothing
    End Sub
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
    Private Function SaveData(ByVal trans As SqlTransaction) As Boolean
        Dim ret As Boolean = True
        If ValidateData() = True Then
            Dim sEmpNo As String = Page.User.Identity.Name
            Dim uData As New LoginUser
            uData.GetUserData(sEmpNo)

            Dim vSubTypeID As Integer = ddlLawSubType.SelectedValue
            Dim vStatusID As Integer = ddlLawStatus.SelectedValue
            Dim vDate As String = dpDate.SaveDate
            Dim vTitle As String = Replace(txtTitle.Text.Trim(), "'", "''")
            Dim vKeyWord As String = Replace(txtKeyword.Text.Trim(), "'", "''")
            Dim vMessage As String
            Dim vSendToDir As String
            Dim vLawerID As String
            Dim vSendTo As String
            Dim vDirSentBack As String
            If lblIsDirector.Text = "N" Then
                'หัวหน้า
                vMessage = txtLeaderMessage.Value.Trim()
                vSendTo = "sendto"
                vDirSentBack = "null"
                If rdiApprove.SelectedValue = "0" Then 'แก้ไข
                    vLawerID = "'" & ddlSendBackLawer.SelectedValue & "'"
                    vSendToDir = "sendto_director"
                Else  'อนุมัติ
                    vLawerID = "lawer_id"
                    vSendToDir = "'" & ddlSendTo.SelectedValue & "'"
                End If
            Else
                'ผอ
                vMessage = txtDirectorMessage.Value.Trim()

                vSendToDir = "sendto_director"
                If rdiApprove.SelectedValue = "0" Then
                    vDirSentBack = "'" & rdiSendBack.SelectedValue & "'"
                    If rdiSendBack.SelectedValue = "1" Then
                        vSendTo = "'" & ddlSendTo.SelectedValue & "'"
                        vLawerID = "lawer_id"
                    Else
                        vSendTo = "sendto"
                        vLawerID = "'" & ddlSendBackLawer.SelectedValue & "'"
                    End If
                Else
                    vSendTo = "sendto"
                    vDirSentBack = "null"
                    vLawerID = "lawer_id"
                End If

            End If
            Dim vLevelID As Integer = ddlLawLevel.SelectedValue

            Dim vNote As String = Replace(txtNote.Text.Trim(), "'", "''")
            Dim dr As DataRow = GetProcessStatus(ddlLawType.SelectedValue)
            Dim vSendToDirectorAss As String = "null"
            Dim vSendToAss As String = "null"

            If uData.PosID = Constant.DirectorPosID Or chkAssignPosID(sEmpNo, Constant.DirectorPosID) = True Then
                vSendToDirectorAss = IIf(chkAssign(sEmpNo) = True, "'" & sEmpNo & "'", "null")
            Else
                vSendToAss = IIf(chkAssign(sEmpNo) = True, "'" & sEmpNo & "'", "null")
            End If

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
                    strsql &= " leader_approve = '" & rdiApprove.SelectedValue & "',"
                    strsql &= " sendto_director = " & vSendToDir & ", "
                    strsql &= " sendto = " & vsendto & ", "
                    strsql &= " note = '" & vNote & "',"
                    strsql &= " updated_by = '" & sEmpNo & "', "
                    strsql &= " updated_date = getdate(), "
                    strsql &= " sendto_ass =" & vSendToAss & ", "
                    strsql &= " sendto_director_ass= " & vSendToDirectorAss & ","
                    strsql &= " ref_bookin = '" & lblTitle.Text & "', "
                    strsql &= " lawer_id = " & vLawerID & ", "
                    strsql &= " dir_sendback = " & vDirSentBack
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
    Private Function chkAssign(ByVal sEmpNo As String) As Boolean
        Dim ret As Boolean = False
        Dim uData As New LoginUser
        uData.GetUserData(sEmpNo)

        Dim sql As String = ""
        sql += " select law_id"
        sql += " from law_data "
        sql += " where law_id='" & txtLawId.Text & "' "
        If uData.PosID = Constant.DirectorPosID Or chkAssignPosID(sEmpNo, Constant.DirectorPosID) = True Then
            'กรณีผู้ที่ Login เป็น ผอ หรือได้รับการมอบหมายงานให้เป็น ผอ
            sql += " and sendto_director = '" & sEmpNo & "'"
        Else
            sql += " and sendto = '" & sEmpNo & "'"
        End If

        Dim dt As DataTable = MD.GetDataTable(sql)
        If dt.Rows.Count > 0 Then
            ret = False
        Else
            ret = True
        End If

        Return ret
    End Function
    Private Function chkAssignPosID(ByVal sEmpNo As String, ByVal chkPosID As Long) As Boolean
        Dim ret As Boolean = False
        Dim sql As String = ""
        sql += " select a.assign_from"
        sql += " from authorize a"
        sql += " where a.assign_to = '" & sEmpNo & "' and a.menu_id=80"
        sql += " and convert(nvarchar(10),getdate(),120) between convert(nvarchar(10),a.date_from,120) and convert(nvarchar(10),a.date_to,120)"
        Dim dt As DataTable = MD.GetDataTable(sql)

        For Each dr As DataRow In dt.Rows
            Dim uData As New LoginUser
            uData.GetUserData(dr("assign_from").ToString())
            ret = (uData.PosID = chkPosID)
            If ret = True Then
                Exit For
            End If
        Next

        Return ret
    End Function
    Protected Sub bSaveSend_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bSaveSend.Click

        If lblIsDirector.Text = "Y" Then
            If rdiApprove.SelectedValue = "0" Then 'แก้ไข
                If rdiSendBack.SelectedValue = "0" Then 'นิติกร
                    If ddlSendBackLawer.SelectedValue = "" Then
                        ddlSendBackLawer.ValidMsg = "***กรุณาเลือกนิติกรที่ต้องการส่งกลับให้แก้ไข"
                        Exit Sub
                    End If
                Else
                    If ddlSendTo.SelectedValue = "" Then
                        ddlSendTo.ValidMsg = "***กรุณาเลือกหัวหน้างานที่ต้องการส่งกลับไปให้แก้ไข"
                        Exit Sub
                    End If
                End If
            End If
        Else
            If rdiApprove.SelectedValue = "0" Then
                If ddlSendBackLawer.SelectedValue = "" Then
                    ddlSendBackLawer.ValidMsg = "***กรุณาเลือกนิติกรที่ต้องการส่งกลับให้แก้ไข"
                    Exit Sub
                End If
            End If
        End If

        cn = New SqlConnection(MD.strConnIMP)
        cn.Open()
        trans = cn.BeginTransaction(IsolationLevel.ReadCommitted)

        If SaveData(trans) = True Then
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
                Dim nextStatus As String
                Dim vNote As String
                Dim vLawerId As String
                Dim vSendTo As String
                Dim vDirSendback As String

                If rdiApprove.SelectedValue = "1" Then
                    nextStatus = GetNextStatus(ddlLawType.SelectedValue)
                Else
                    nextStatus = GetPrevStatus(ddlLawType.SelectedValue)
                End If
                If lblIsDirector.Text = "Y" Then
                    'กรณี ผอ พิจารณา
                    vNote = "note"
                    If rdiApprove.SelectedValue = "0" Then 'แก้ไข 
                        vDirSendback = "'" & rdiSendBack.SelectedValue & "'"
                        If rdiSendBack.SelectedValue = "1" Then 'ส่งกลับให้หัวหน้างาน
                            vSendTo = "'" & ddlSendTo.SelectedValue & "'"
                            vLawerId = "lawer_id"
                        Else 'ส่งกลับให้นิติกร
                            vSendTo = "sendto"
                            vLawerId = "'" & ddlSendBackLawer.SelectedValue & "'"
                        End If
                    Else  'ผอ อนุมัติ
                        vSendTo = "sendto"
                        vLawerId = "lawer_id"
                        vDirSendback = "dir_sendback"
                    End If
                Else
                    'กรณีหัวหน้าพิจารณา
                    If rdiApprove.SelectedValue = "1" Then
                        vNote = "null"
                        vLawerId = "lawer_id"
                        vDirSendback = "dir_sendback"
                    Else
                        vNote = "note"
                        vLawerId = "'" & ddlSendBackLawer.SelectedValue & "'"
                        vDirSendback = "dir_sendback"
                    End If
                End If

                sql = "insert into LAW_DATA (law_id, subtype_id,status_id, "
                sql += " dates, level_id, Title, keyword, message, "
                sql += " leader_approve,  sendto, note,"
                sql += " ref_id, active, sendto_director, creation_by,created_date, sendto_ass, sendto_director_ass,ref_bookin, lawer_id, dir_sendback)"
                sql += " select '" & newLawID & "', subtype_id, '" & nextStatus & "', "
                sql += " dates, level_id, title, keyword, message, "
                sql += " '" & rdiApprove.SelectedValue & "', sendto, " & vNote & ", "
                sql += " ref_id, '1', '" & ddlSendTo.SelectedValue & "', '" & sEmpNo & "', GETDATE(), sendto_ass, sendto_director_ass,ref_bookin, " & vLawerId & ", " & vDirSendback & " "
                sql += " from law_data "
                sql += " where law_id='" & txtLawId.Text & "' "

                Dim cmd2 As New SqlCommand(sql, trans.Connection, trans)
                If cmd2.ExecuteNonQuery() > 0 Then
                    trans.Commit()
                    Response.Redirect("../Src/DrafLawApproveList.aspx")
                Else
                    trans.Rollback()
                End If

            Catch ex As SqlException
                trans.Rollback()
                MC.MessageBox(Me, ex.Message)
            End Try
        End If
        cn.Close()
        cn = Nothing
    End Sub
    Private Sub BindDrafLawData(ByVal lawID As String)
        Dim sql As String = ""
        sql += " select ld.law_id, ls.type_id, ld.subtype_id, ld.status_id,ld.level_id,ld.ref_id,"
        sql += " ld.dates, ld.title, ld.keyword, ld.message, ld.ref_id, ld.sendto_director, ld.leader_approve, ld.note, "
        sql += " ld.ref_bookin,b.topic, ld.lawer_id, ld.dir_sendback "
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
            txtTitleBookIn.Text = dt.Rows(0)("topic").ToString()
            lblTitle.Text = dt.Rows(0)("ref_bookin").ToString()
            txtLeaderMessage.Value = GetLeaderMessage(lblRefID.Text)
            lblLeaderMessage.Text = txtLeaderMessage.Value
            lblLeaderNote.Text = "บันทึกเพิ่มเติม :<br>" & GetLeaderNote(lblRefID.Text)
            lblLawerMessage.Text = GetLawerMessage(lblRefID.Text)
            txtDirectorMessage.Value = GetDirectorMessage(lblRefID.Text)
            lblDirectorMessage.Text = GetDirectorMessage(lblRefID.Text)
            If Convert.IsDBNull(dt.Rows(0)("sendto_director")) = False Then ddlSendTo.SelectedValue = dt.Rows(0)("sendto_director").ToString()
            If Convert.IsDBNull(dt.Rows(0)("leader_approve")) = False Then
                rdiApprove.SelectedValue = dt.Rows(0)("leader_approve").ToString()
                If rdiApprove.SelectedValue = "0" Then
                    'ddlSendTo.SelectedValue = ""
                    'pnlSendTo.Visible = False
                    'pnlSendToLawer.Visible = True
                    'ddlSendBackLawer.SelectedValue = dt.Rows(0)("lawer_id").ToString()
                    rdiSendBack.SelectedValue = IIf(Convert.IsDBNull(dt.Rows(0)("dir_sendback")) = False, dt.Rows(0)("dir_sendback").ToString(), "1")
                End If
            End If

            If Convert.IsDBNull(dt.Rows(0)("note")) = False Then txtNote.Text = dt.Rows(0)("note").ToString()

            'ผู้ใช้ที่ Login มีตำแหน่งเป็นผู้อำนวยการหรือป่าว
            Dim uData As New LoginUser
            uData.GetUserData(Page.User.Identity.Name)

            If ddlLawStatus.SelectedValue <> GetLawerStatus(ddlLawType.SelectedValue) Then
                If uData.PosID = Constant.DirectorPosID Or chkAssignPosID(Page.User.Identity.Name, Constant.DirectorPosID) = True Then
                    lblIsDirector.Text = "Y"
                End If
            End If

            If dt.Rows(0)("ref_bookin").ToString <> "" Then
                LinkDetail.Text = "<a href=""javascript:openwindow('" + "BookInPopupPreview" + "','" + dt.Rows(0)("ref_bookin").ToString + "','" + "');"">" + "ดูรายละเอียด" + "</a>"
            Else
                LinkDetail.Visible = False
            End If
            SetContent()
            ctlPrintLaw1.Visible = True
            ctlPrintLawer.Visible = True
            ctlPrintDraftDir.Visible = True


        End If
    End Sub
    Private Sub RefreshPage()

        If Session("TextIdTitle") <> "" Then
            txtTitleBookIn.Text = Session("TextTitle")
            lblTitle.Text = Session("TextIdTitle")
        End If
    End Sub
    Private Function GetLawerStatus(ByVal TypeID As String) As String
        Dim sql As String = ""
        sql += " select lawer_status "
        sql += " from law_process_status "
        sql += " where type_id = '" & TypeID & "'"

        Return MD.GetDataTable(sql).Rows(0)("lawer_status").ToString()
    End Function


    Private Function GetProcessStatus(ByVal LawTypeID As String) As DataRow
        Dim ret As DataRow
        Dim sql As String = ""
        sql += "select begin_status, lawer_status,leader_status,director_status,last_status "
        sql += " from law_process_status "
        sql += " where type_id = '" & LawTypeID & "'"

        Return MD.GetDataTable(sql).Rows(0)
    End Function

    Private Function GetLawerMessage(ByVal RefID As String) As String
        Dim ret As String = ""
        'ค้นหาสถานะเริ่มต้นเมื่อนิติกรส่งให้กับหัวหน้ากลุ่ม
        Dim dr As DataRow = GetProcessStatus(ddlLawType.SelectedValue)
        If Convert.IsDBNull(dr("begin_status")) = False Then
            Dim sql As String = ""
            sql = " select top 1 message, law_id "
            sql += " from law_data "
            sql += " where ref_id='" & lblRefID.Text & "' "
            sql += " and status_id='" & dr("begin_status").ToString() & "' "
            sql += " and active='0'"
            sql += " order by law_id desc"

            Dim dt As DataTable = MD.GetDataTable(sql)
            If dt.Rows.Count > 0 Then
                ctlPrintLawer.LawID = dt.Rows(0)("law_id").ToString()
                ret = dt.Rows(0)("message").ToString()
            End If
        End If

        Return ret
    End Function
    Private Function GetLeaderNote(ByVal LawID As String) As String
        Dim ret As String = ""
        Dim dr As DataRow = GetProcessStatus(ddlLawType.SelectedValue)
        Dim Sql As String = ""
        Sql = " select top 1 note, law_id "
        Sql += " from law_data "
        Sql += " where ref_id='" & lblRefID.Text & "' "
        Sql += " and status_id='" & dr("lawer_status").ToString() & "' "
        'Sql += " and leader_approve = '1' "
        Sql += " order by law_id desc"

        Dim dt As DataTable = MD.GetDataTable(Sql)
        If dt.Rows.Count > 0 Then
            ret = dt.Rows(0)("note").ToString()
        End If

        Return ret
    End Function
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
            'Sql += " and leader_approve = '1' "
            Sql += " order by law_id desc"

            dt = MD.GetDataTable(Sql)
            If dt.Rows.Count > 0 Then
                ctlPrintLaw1.LawID = dt.Rows(0)("law_id").ToString()
                ret = dt.Rows(0)("message").ToString()
            End If
        End If

        Return ret
    End Function
    Private Function GetDirectorMessage(ByVal RefID As String) As String
        Dim ret As String = ""

        'ค้นหาสถานะที่หัวหน้ากลุ่มพิจารณา รอการอนุมัติ

        Dim dr As DataRow = GetProcessStatus(ddlLawType.SelectedValue)

        If Convert.IsDBNull(dr("leader_status")) = False Then
            Dim uData As New LoginUser
            uData.GetUserData(Page.User.Identity.Name)

            Dim Sql As String = ""
            Sql = " select top 1 message, law_id "
            Sql += " from law_data "
            Sql += " where ref_id='" & lblRefID.Text & "' "
            Sql += " and status_id='" & dr("leader_status").ToString() & "' "
            If uData.PosID = Constant.DirectorPosID Then
                Sql += " and leader_approve = '1' "
            Else
                Sql += " and active='0'"
            End If
            Sql += " order by law_id desc"

            Dim dt As DataTable = MD.GetDataTable(Sql)
            If dt.Rows.Count > 0 Then
                ctlPrintDraftDir.LawID = dt.Rows(0)("law_id").ToString()
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
        ElseIf txtLeaderMessage.Value.Trim() = "" Then
            ret = False
            lblChkLeaderMessage.Visible = True
            lblChkLeaderMessage.Text = "***กรุณาระบุรายละเอียด"
        ElseIf rdiApprove.SelectedValue = "1" Then
            If ddlSendTo.SelectedValue = "" And lblIsDirector.Text = "N" Then
                ret = False
                ddlSendTo.ValidMsg = "***กรุณาเลือกชื่อผู้ส่งต่อเพื่อตรวจ"
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
        lblChkLeaderMessage.Visible = False
        lblChkDirectorMessage.Visible = False
        ddlSendTo.ValidMsg = ""
        lblADocDeatil.Text = "*"
        txtDocPage.ChkNullMsg = "*"
        ddlSendBackLawer.ValidMsg = "*"
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
            MD.Execute(sql)
            GetDocList()
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


    Private Function GetPrevStatus(ByVal TypeID As String) As Integer
        'สถานะก่อนการพิจารณาอนุมัติ
        Dim ret As Integer
        Dim sql As String = ""
        If lblIsDirector.Text = "Y" Then
            If rdiSendBack.SelectedValue = "1" Then  ' ผอ ส่งให้หัวหน้างาน
                sql += "select top 1 s.lawer_status AS status_id"
            Else ' ผอ ส่งให้นิติกร
                sql += "select top 1 s.begin_status AS status_id "
            End If
        Else  ' หัวหน้างาน ส่งให้นิติกร
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

    Protected Sub rdiApprove_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdiApprove.SelectedIndexChanged
        SetContent()
    End Sub
    Private Sub SetddlLawer()
        'แสดงรายชื่อนิติกร กรณีส่งกลับให้นิติกร
        Dim lsql As String = ""
        lsql += " select e.empid,pe.prefix_name + '' + e.firstname + ' ' + e.lastname fullname"
        lsql += " from  EMPLOYEE e "
        lsql += " left join PREFIX pe on pe.prefix_id=e.prefix"
        lsql += " inner join position ps on ps.pos_id=e.pos_id"
        lsql += " where e.div_id in (3,4,5) and e.status <> '0'" 'ส่งให้นิติกรได้ทุกคน
        'lsql += " where e.pos_id in (1,8,9) and e.status <> '0'" 'นิติกร
        lsql += " order by e.firstname, e.lastname"
        ddlSendBackLawer.SetItemList(MD.GetDataTable(lsql), "fullname", "empid")
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
    Protected Sub link2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles link2.Click
        Response.Redirect("../Src/DrafLawApproveList.aspx", True)
    End Sub

    Protected Sub rdiSendBack_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdiSendBack.SelectedIndexChanged
        SetContent()
    End Sub

    Private Sub SetContent()
        If lblIsDirector.Text = "Y" Then
            'ผอ
            If rdiApprove.SelectedValue = "0" Then 'แก้ไข
                pnlDirOption.Visible = True
                If rdiSendBack.SelectedValue = "1" Then 'หัวหน้างาน
                    pnlSendTo.Visible = True
                    lblSendTo.Visible = False
                    'ddlSendTo.Visible = True
                    pnlSendToLawer.Visible = False
                    DataEmployee()
                    Dim sql As String = " select sendto from law_data where law_id='" & txtLawId.Text & "'"
                    ddlSendTo.SelectedValue = MD.GetDataTable(sql).Rows(0)("sendto").ToString().ToUpper
                Else 'นิติกร
                    pnlSendTo.Visible = False
                    pnlSendToLawer.Visible = True
                    lblSendToLawer.Visible = False
                    SetddlLawer()
                    Dim sql As String = "select lawer_id from law_data where law_id='" & txtLawId.Text & "'"
                    ddlSendBackLawer.SelectedValue = MD.GetDataTable(sql).Rows(0)("lawer_id").ToString().ToUpper
                End If
            Else
                pnlDirOption.Visible = False
                pnlSendToLawer.Visible = False
                pnlSendTo.Visible = False
            End If
        Else
            If rdiApprove.SelectedValue = "0" Then
                ddlSendTo.SelectedValue = ddlSendTo.DefaultValue
                ddlSendTo.Enabled = False
                pnlSendTo.Visible = False
                pnlSendToLawer.Visible = True

                SetddlLawer()
                Dim sql As String = "select lawer_id from law_data where law_id='" & txtLawId.Text & "'"
                ddlSendBackLawer.SelectedValue = MD.GetDataTable(sql).Rows(0)("lawer_id").ToString().ToUpper
            Else
                ddlSendTo.Enabled = True
                pnlSendTo.Visible = True
                ddlSendBackLawer.SelectedValue = ddlSendBackLawer.DefaultValue
                pnlSendToLawer.Visible = False
            End If
        End If




            'If lblIsDirector.Text = "Y" Then
            '    'กรณี ผอ อนุมัติ
            '    
            'Else

            'End If

    End Sub
End Class
