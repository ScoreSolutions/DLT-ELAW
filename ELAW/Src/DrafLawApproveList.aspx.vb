Imports System.Data
Partial Class Src_DrafLawApproveList
    Inherits System.Web.UI.Page
    Dim DVLst As DataView
    Dim iRow As Integer
    Dim MD As New MainData
    Const menuID As Integer = Constant.MenuID.DrafLawApproveList
    Private Function chkAssignPosID(ByVal sEmpNo As String, ByVal chkPosID As Long) As Boolean
        Dim ret As Boolean = False
        Dim sql As String = ""
        sql += " select a.assign_from"
        sql += " from authorize a"
        sql += " where a.assign_to = '" & sEmpNo & "' "
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
    Private Sub gData()
        Dim sEmpNo As String = Page.User.Identity.Name
        'ผู้ใช้ที่ Login มีตำแหน่งเป็นผู้อำนวยการหรือป่าว
        Dim uData As New LoginUser
        uData.GetUserData(sEmpNo)

        Dim sql As String = ""
        sql += "select ld.law_id, ld.title+'('+lld.name+')' title,lt.type_name, ls.subtype_name, s.status_name,l.level_name" & vbNewLine
        sql += " from law_data ld" & vbNewLine
        sql += " inner join law_subtype ls on ls.subtype_id=ld.subtype_id" & vbNewLine
        sql += " inner join law_type lt on lt.type_id=ls.type_id" & vbNewLine
        sql += " inner join law_status s on s.status_id=ld.status_id" & vbNewLine
        sql += " inner join law_level l on l.level_id=ld.level_id" & vbNewLine
        sql += " inner join (select l.law_id,l.ref_id,f.*" & vbNewLine
        sql += " from LAW_DATA l" & vbNewLine
        sql += " inner join FULLNAME f on f.empid=l.creation_by" & vbNewLine
        sql += " where  l.law_id=l.ref_id and l.active='0') lld on lld.law_id=ld.ref_id" & vbNewLine
        sql += " left join AUTHORIZE a " & vbNewLine
        sql += " on ld.status_id =a.status_id and a.menu_id =80 " & vbNewLine
        sql += " where (ld.active = 1 " & vbNewLine
        sql += " and ld.status_id in (select lawer_status   from LAW_PROCESS_STATUS union select leader_status from LAW_PROCESS_STATUS ) " & vbNewLine
        sql += " and case when ld.status_id in (select lawer_status   from LAW_PROCESS_STATUS)  then ld.sendto  else ld.sendto_director  end='" & sEmpNo & "' " & vbNewLine
        sql += " or (a.assign_from = case when ld.status_id in (select lawer_status   from LAW_PROCESS_STATUS) then ld.sendto  else ld.sendto_director  end and a.assign_to ='" & sEmpNo & "' and ld.active =1 " & vbNewLine
        sql += " and convert(nvarchar(10),getdate(),120) between convert(nvarchar(10),a.date_from,120) and convert(nvarchar(10),a.date_to,120))) " & vbNewLine



        'sql += " left join AUTHORIZE a " & vbNewLine
        'sql += " on ld.status_id =a.status_id and a.menu_id = 80 " & vbNewLine
        'sql += " where ld.active='1' " & vbNewLine
        'If uData.PosID = Constant.DirectorPosID Or chkAssignPosID(sEmpNo, Constant.DirectorPosID) = True Then
        '    sql += " and ((ld.sendto = '" & sEmpNo & "' or (ld.sendto=a.assign_from and a.assign_to='" & sEmpNo & "' " & vbNewLine
        '    sql += "      and convert(nvarchar(10),getdate(),120) between convert(nvarchar(10),a.date_from,120) and convert(nvarchar(10),a.date_to,120)))" & vbNewLine
        '    sql += " or ((ld.sendto_director='" & sEmpNo & "' or (ld.sendto_director=a.assign_from and a.assign_to='" & sEmpNo & "' " & vbNewLine
        '    sql += "      and convert(nvarchar(10),getdate(),120) between convert(nvarchar(10),a.date_from,120) and convert(nvarchar(10),a.date_to,120))) " & vbNewLine
        '    sql += " and ld.leader_approve='1')) " & vbNewLine
        '    sql += " and ld.status_id in (select lp.leader_status from law_process_status lp where lp.type_id=ls.type_id)" & vbNewLine
        'Else
        '    sql += " and (ld.sendto='" & sEmpNo & "' " & vbNewLine
        '    sql += " or (ld.sendto=a.assign_from and a.assign_to='" & sEmpNo & "' " & vbNewLine
        '    sql += "     and convert(nvarchar(10),getdate(),120) between convert(nvarchar(10),a.date_from,120) and convert(nvarchar(10),a.date_to,120)))" & vbNewLine
        '    sql += " and ld.status_id in (select lp.lawer_status from law_process_status lp where lp.type_id=ls.type_id)" & vbNewLine
        'End If

        If ddlLawType.SelectedValue <> "0" Then
            sql += " and lt.type_id='" & ddlLawType.SelectedValue & "'" & vbNewLine
        End If
        If ddlSubType.SelectedValue <> "0" Then
            sql += " and ld.subtype_id='" & ddlSubType.SelectedValue & "'" & vbNewLine
        End If
        If ddlStatus.SelectedValue <> "0" Then
            sql += " and ld.status_id='" & ddlStatus.SelectedValue & "'" & vbNewLine
        End If
        If ddlLevel.SelectedValue <> "0" Then
            sql += " and ld.level_id='" & ddlLevel.SelectedValue & "'" & vbNewLine
        End If
        If txtTitle.Text.Trim() <> "" Then
            sql += " and ld.title like '%" & txtTitle.Text.Trim() & "%'" & vbNewLine
        End If
        If txtKeyword.Text.Trim() <> "" Then
            sql += " and ld.keyword like '%" & txtKeyword.Text.Trim() & "%'" & vbNewLine
        End If
        If txtLawerName.Text.Trim <> "" Then
            sql += " and lld.name like '%" & txtLawerName.Text.Trim() & "%' "
        End If

        sql += " group by ld.law_id, ld.title+'('+lld.name+')',lt.type_name, ls.subtype_name, s.status_name,l.level_name,ld.sendto ,ld.leader_approve " & vbNewLine

        Dim DT As DataTable = MD.GetDataTable(sql)
        DVLst = DT.DefaultView
        DVLst.Sort = "[" & ViewState("sortfield") & "] " & ViewState("sortdirection")
        Session("CaseEditList") = DVLst

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
        dt.Columns.Add("txtLawerName")

        Dim data As DataRow = dt.NewRow
        data("menuID") = menuID
        data("ddlLawType") = ddlLawType.SelectedValue
        data("ddlSubType") = ddlSubType.SelectedValue
        data("ddlStatus") = ddlStatus.SelectedValue
        data("ddlLevel") = ddlLevel.SelectedValue
        data("txtTitle") = txtTitle.Text
        data("txtKeyWord") = txtKeyword.Text
        data("txtLawerName") = txtLawerName.Text
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
                txtLawerName.Text = data("txtLawerName")
            Else
                Session.Remove(Constant.searchSession)
            End If
        End If
    End Sub

    Protected Sub bSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bSearch.Click
        gData()
        MyGridBind()
        SetSearchSession()
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
            End If
        End If

    End Sub
    Protected Sub GridView1_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles GridView1.RowEditing
        Dim K1 As DataKey = GridView1.DataKeys(e.NewEditIndex)
        SetSearchSession()
        Session("txtPage") = iRow / 10 - 1
        Response.Redirect("../Src/DrafLawApprove.aspx?id=" & K1(0))
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

    Protected Sub linkHome_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles linkHome.Click
        Response.Redirect("" & MD.pHome & "")
    End Sub
End Class
