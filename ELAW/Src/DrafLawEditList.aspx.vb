Imports System.Data

Partial Class Src_DrafLawEditList
    Inherits System.Web.UI.Page
    Dim DVLst As DataView
    Dim iRow As Integer
    Dim MD As New MainData
    Dim MC As New MainClass
    Const menuID As Integer = Constant.MenuID.DrafLawEditList
    Private Sub gData()
        Dim sEmpNo As String = Page.User.Identity.Name
        Dim uData As New LoginUser()
        uData.GetUserData(sEmpNo)

        Dim sql As String = ""
        sql += "select ld.law_id, ld.title,lt.type_name, ls.subtype_name, s.status_name,l.level_name"
        sql += " from law_data ld"
        sql += " inner join law_subtype ls on ls.subtype_id=ld.subtype_id"
        sql += " inner join law_type lt on lt.type_id=ls.type_id"
        sql += " inner join law_status s on s.status_id=ld.status_id"
        sql += " inner join law_level l on l.level_id=ld.level_id"
        sql += " where ld.active = '1'"
        If uData.PosID = GetLeaderPosID(uData.DivID) Then
            'กรณีหัวหน้ากลุ่ม ให้แสดงเฉพาะรายการที่ ผอ ส่งกลับมาให้แก้ไข
            sql += " and ld.status_id in (" & GetLawerStatus() & ") and ld.sendto='" & sEmpNo & "'  and ld.leader_approve='0' "
            'Poom 03122010
            sql += " or (ld.lawer_id='" & sEmpNo & "' and ld.status_id  in (" & GetBeginStatus() & ") and ld.active='1')"

        Else
            'กรณีนิติกร ให้แสดงรายการร่างกฎหมายที่ยังไม่ส่งให้กับหัวหน้ากลุ่ม และรายการที่หัวหน้ากลุ่มส่งกลับมาให้แก้ไข
            'sql += " and (ld.creation_by in (" & GetDrafLawCreateBy(sEmpNo) & ") or (ld.creation_by='" & sEmpNo & "' and ld.status_id  in (" & GetBeginStatus() & "))) "
            sql += " and ld.lawer_id='" & sEmpNo & "' and ld.status_id  in (" & GetBeginStatus() & ")"
        End If

        If ddlLawType.SelectedValue <> "0" Then
            sql += " and lt.type_id='" & ddlLawType.SelectedValue & "'"
        End If
        If ddlSubType.SelectedValue <> "0" Then
            sql += " and ld.subtype_id='" & ddlSubType.SelectedValue & "'"
        End If
        If ddlStatus.SelectedValue <> "0" Then
            sql += " and ld.status_id='" & ddlStatus.SelectedValue & "'"
        End If
        If ddlLevel.SelectedValue <> "0" Then
            sql += " and ld.level_id='" & ddlLevel.SelectedValue & "'"
        End If
        If txtTitle.Text.Trim() <> "" Then
            sql += " and ld.title like '%" & txtTitle.Text.Trim() & "%'"
        End If
        If txtKeyword.Text.Trim() <> "" Then
            sql += " and ld.keyword like '%" & txtKeyword.Text.Trim() & "%'"
        End If

        Dim DT As DataTable = MD.GetDataTable(sql)
        DVLst = DT.DefaultView
        DVLst.Sort = "[" & ViewState("sortfield") & "] " & ViewState("sortdirection")
        Session("CaseEditList") = DVLst
    End Sub
    Private Function GetLawerStatus() As String
        'ขั้นตอนการร่างกฎหมาย
        Dim sql As String = ""
        sql += "select s.lawer_status "
        sql += " from law_process_status s"
        Return sql
    End Function

    Private Function GetProcessStatus(ByVal LawTypeID As String) As DataRow
        Dim ret As DataRow
        Dim sql As String = ""
        sql += "select begin_status, lawer_status,leader_status,director_status,last_status "
        sql += " from law_process_status "
        sql += " where type_id = '" & LawTypeID & "'"

        Return MD.GetDataTable(sql).Rows(0)
    End Function

    Private Function GetBeginStatus() As String
        'สถานะเริ่มต้น
        Dim sql As String = ""
        sql += "select s.begin_status "
        sql += " from law_process_status s"
        Return sql
    End Function
    Private Function GetLeaderPosID(ByVal DivID As Integer) As Integer
        'ตำแหน่งการอนุมัติ
        Dim sql As String = ""
        sql += " select pos_id from division where div_id = " & DivID
        Return Convert.ToInt64(MD.GetDataTable(sql).Rows(0)("pos_id"))
    End Function
    Private Function GetDrafLawCreateBy(ByVal sEmpNo As String) As String
        'ผู้ร่างกฏหมาย
        Dim ret As String = ""
        ret += " select sendto from law_data where law_id=ref_id and creation_by = '" & sEmpNo & "'"
        Return ret
    End Function
    Protected Sub bSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bSearch.Click
        gData()
        MyGridBind()
        SetSearchSession()
    End Sub

    Private Sub SetSearchSession()

        Dim dt As New DataTable
        dt.Columns.Add("menuID")
        dt.Columns.Add("ddlLawType")
        dt.Columns.Add("ddlSubType")
        dt.Columns.Add("ddlStatus")
        dt.Columns.Add("ddlLevel")
        dt.Columns.Add("txtTitle")
        dt.Columns.Add("txtKeyWord")

        Dim data As DataRow = dt.NewRow
        data("menuID") = menuID
        data("ddlLawType") = ddlLawType.SelectedValue
        data("ddlSubType") = ddlSubType.SelectedValue
        data("ddlStatus") = ddlStatus.SelectedValue
        data("ddlLevel") = ddlLevel.SelectedValue
        data("txtTitle") = txtTitle.Text
        data("txtKeyWord") = txtKeyword.Text
        Session(Constant.searchSession) = data
    End Sub

    Private Sub FillCondition()
        If Session(Constant.searchSession) IsNot Nothing Then
            Dim data As DataRow = Session(Constant.searchSession)
            If data("menuID") = menuID Then
                ddlLawType.SelectedValue = data("ddlLawType")
                If ddlLawType.SelectedValue <> "0" Then
                    DataLawSubType()
                    DataLawStatus()
                End If

                ddlSubType.SelectedValue = data("ddlSubType")
                ddlStatus.SelectedValue = data("ddlStatus")
                ddlLevel.SelectedValue = data("ddlLevel")
                txtTitle.Text = data("txtTitle")
                txtKeyword.Text = data("txtKeyWord")
            Else
                Session.Remove(Constant.searchSession)
            End If
        End If
    End Sub

    Private Sub MyGridBind()
        GridView1.DataSource = DVLst
        Dim X1() As String = {"law_id"}
        GridView1.DataKeyNames = X1
        GridView1.DataBind()
    End Sub
    Protected Sub GridView1_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridView1.PageIndexChanging
        GridView1.PageIndex = e.NewPageIndex
        Me.MyGridBind()
    End Sub
    Private Sub GoPage(ByVal xPage As Integer)
        GridView1.PageIndex = xPage
        Me.MyGridBind()
    End Sub
    Private Sub FirstClick(ByVal sender As Object, ByVal e As EventArgs)
        Me.GoPage(0)
    End Sub
    Private Sub PrevClick(ByVal sender As Object, ByVal e As EventArgs)
        Me.GoPage(GridView1.PageIndex - 1)
    End Sub
    Private Sub NextClick(ByVal sender As Object, ByVal e As EventArgs)
        Me.GoPage(GridView1.PageIndex + 1)
    End Sub
    Private Sub LastClick(ByVal sender As Object, ByVal e As EventArgs)
        Me.GoPage(GridView1.PageCount - 1)
    End Sub
    Protected Sub GridView1_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles GridView1.Sorting
        If ViewState("sortfield") = e.SortExpression Then
            If ViewState("sortdirection") = "asc" Then ViewState("sortdirection") = "desc" Else ViewState("sortdirection") = "asc"
        Else
            ViewState("sortfield") = e.SortExpression
            ViewState("sortdirection") = "asc"
        End If
        DVLst.Sort = "[" & ViewState("sortfield") & "] " & ViewState("sortdirection")
        Me.MyGridBind()
    End Sub
    Protected Sub GridView1_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowCreated
        'Create Page Gridview
        If e.Row.RowType = DataControlRowType.Pager Then
            Dim td As TableCell = e.Row.Cells(0)
            Dim Y As Boolean = False
            If GridView1.PageIndex > 0 Then
                Dim L1 As New LinkButton
                L1.ForeColor = Drawing.Color.Black
                L1.Text = "First"
                td.Controls.Add(L1)
                AddHandler L1.Click, AddressOf FirstClick

                Y = True

                Dim L2 As New Literal
                L2.Text = " &nbsp;&nbsp;"
                td.Controls.Add(L2)
                td.HorizontalAlign = HorizontalAlign.Left

                L1 = New LinkButton
                L1.ForeColor = Drawing.Color.Black
                L1.Text = "Prev"
                AddHandler L1.Click, AddressOf PrevClick
                td.Controls.Add(L1)
                td.HorizontalAlign = HorizontalAlign.Left

            End If
            If GridView1.PageIndex < GridView1.PageCount - 1 Then
                Dim L2 As Literal

                If Y = True Then
                    L2 = New Literal
                    L2.Text = " &nbsp;&nbsp;"
                    td.Controls.Add(L2)
                End If

                Dim L1 As New LinkButton
                L1.ForeColor = Drawing.Color.Black
                L1.Text = "Next"
                AddHandler L1.Click, AddressOf NextClick
                td.Controls.Add(L1)

                L2 = New Literal
                L2.Text = " &nbsp;&nbsp;"
                td.Controls.Add(L2)
                td.HorizontalAlign = HorizontalAlign.Left

                L1 = New LinkButton
                L1.ForeColor = Drawing.Color.Black
                L1.Text = "Last"
                AddHandler L1.Click, AddressOf LastClick
                td.Controls.Add(L1)
                td.HorizontalAlign = HorizontalAlign.Left
            End If
        End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            iRow = e.Row.DataItemIndex + 1
            If Not e.Row.RowState And DataControlRowState.Edit Then
                Dim L1 As LinkButton = e.Row.Cells(7).Controls(1)
                L1.OnClientClick = "return confirm('คุณต้องการลบข้อมูลใช่หรือไม่?');"
            End If

        End If
   
    End Sub
    Private Function getDraftLawData(ByVal lawID As String) As DataTable
        Dim sql As String = "select ld.law_id,ld.subtype_id,ls.type_id ,ld.status_id,ld.active"
        sql += " from law_data ld"
        sql += " inner join LAW_SUBTYPE ls on ls.subtype_id=ld.subtype_id"
        sql += " where ld.law_id='" & lawID & "'"

        Return MD.GetDataTable(sql)

    End Function
    Protected Sub GridView1_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles GridView1.RowEditing
        Dim K1 As DataKey = GridView1.DataKeys(e.NewEditIndex)
        Dim sEmpNo As String = Page.User.Identity.Name
        Dim uData As New LoginUser()
        uData.GetUserData(sEmpNo)
        Dim dtData As New DataTable
        dtData = getDraftLawData(K1(0))

        SetSearchSession()
        Session("txtPage") = iRow / 10 - 1

        If uData.PosID = GetLeaderPosID(uData.DivID) Then
            If GetProcessStatus(dtData.Rows(0)("type_id"))("begin_status") = dtData.Rows(0)("status_id") Then
                'กรณีหัวหน้าใช้งาน แล้วเป็นรายการที่มีสถานะเป็นนิติกร (คือ หัวหน้าทำหน้าที่ร่างกฎหมายแทนนิติกร แล้วส่งงานให้ตัวเอง)
                Response.Redirect("../Src/DrafLawEdit.aspx?id=" & K1(0))
            Else
                Response.Redirect("../Src/DrafLawApprove.aspx?id=" & K1(0))
            End If
        Else
            Response.Redirect("../Src/DrafLawEdit.aspx?id=" & K1(0))
        End If
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            ViewState("sortfield") = "law_id"
            ViewState("sortdirection") = "desc"

            SetDropdownList()
            FillCondition()
            gData()
            MyGridBind()

            Try
                Dim data As DataRow = Session(Constant.searchSession)
                If Session("txtPage") IsNot Nothing And data("menuID") = menuID Then
                    GridView1.PageIndex = Session("txtPage")
                End If
                Me.MyGridBind()
            Catch ex As Exception

            End Try
        Else
            If Session("CaseEditList") Is Nothing Then
                gData()
            Else
                DVLst = Session("CaseEditList")
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
        ddlSubType.SetItemList(MD.GetDataTable(strsql), "subtype_name", "subtype_id")
    End Sub
    Private Sub DataLawStatus()
        'สถานะกฎหมาย
        Dim strsql As String
        strsql = "select status_id,status_name    "
        strsql &= "from law_status where type_id='" & ddlLawType.SelectedValue & "' order by status_id "

        ddlStatus.SetItemList(MD.GetDataTable(strsql), "status_name", "status_id")
    End Sub
    Private Sub DataLawLevel()
        'ระดับความสำคัญ
        Dim sql As String = ""
        sql += " select level_id, level_name "
        sql += " from law_level "
        sql += " order by level_id"

        ddlLevel.SetItemList(MD.GetDataTable(sql), "level_name", "level_id")
    End Sub
    Protected Sub ddlLawType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlLawType.SelectedIndexChanged
        If ddlLawType.SelectedValue <> "0" Then
            DataLawStatus()
            DataLawSubType()
        End If
    End Sub
    Protected Sub GridView1_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GridView1.RowDeleting
        Dim K1 As DataKey = GridView1.DataKeys(e.RowIndex)
        Dim sEmpNo As String = Page.User.Identity.Name
        Dim sql As String

        sql = "update law_data set active=0,updated_by='" & sEmpNo & "',updated_date=getdate() where law_id='" & K1(0).ToString & "'"
        Dim i As Integer = MD.Execute(sql)
        If i > 0 Then
            MC.MessageBox(Me, "ลบข้อมูลเรียบร้อยแล้ว")
            Me.gData()
            Me.MyGridBind()
        Else
            MC.MessageBox(Me, "ไม่สามารถลบข้อมูลได้")
        End If
    End Sub

    Protected Sub linkHome_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles linkHome.Click
        Response.Redirect("" & MD.pHome & "")
    End Sub
End Class
