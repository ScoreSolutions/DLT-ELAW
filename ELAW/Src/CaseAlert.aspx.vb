Imports System.Data
Imports System.Data.OleDb
Partial Class Src_CaseAlert
    Inherits System.Web.UI.Page
    Dim MD As New MainData
    Dim MC As New MainClass
    Dim DVLst As DataView
    Public iRow As Integer
    Dim DS As DataSet
    Dim iRec As Integer
    Const menuID As Integer = Constant.MenuID.DrafLawList
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            ViewState("sortfield") = "case_id"
            ViewState("sortdirection") = "desc"
            Me.DataType()
            Me.DataStatus()

            'FillCondition()

            Me.gData("0")
            Me.MyGridBind()
        Else
            If Session("CaseAlert") Is Nothing Then
                Me.gData()
            Else
                DVLst = Session("CaseAlert")
            End If
        End If
    End Sub
    Private Sub SetSearchSession()

        Dim dt As New DataTable
        dt.Columns.Add("menuID")
        dt.Columns.Add("ddlCase")
        dt.Columns.Add("ddlStatus")
        dt.Columns.Add("txtblack")
        dt.Columns.Add("txtRed")

        Dim data As DataRow = dt.NewRow
        data("menuID") = menuID
        data("ddlCase") = ddlCase.SelectedValue
        data("ddlStatus") = ddlStatus.SelectedValue
        data("txtBlack") = txtBlackNo.Text
        data("txtRed") = txtRedNo.Text
        Session(Constant.searchSession) = data
    End Sub
    Private Sub FillCondition()
        If Session(Constant.searchSession) IsNot Nothing Then
            Dim data As DataRow = Session(Constant.searchSession)
            If data("menuID") = menuID Then
                ddlCase.SelectedValue = data("ddlCase")
                If ddlCase.SelectedValue <> "0" Then
                    DataStatus()
                End If

                ddlCase.SelectedValue = data("ddlCase")
                ddlStatus.SelectedValue = data("ddlStatus")
                txtBlackNo.Text = data("txtBlack")
                txtRedNo.Text = data("txtRed")
            Else
                Session.Remove(Constant.searchSession)
            End If
        End If
    End Sub
    Private Sub gData(Optional ByVal Type As String = "")
        'ดึงข้อมูลมาแสดงใน Gridview
        Dim sEmpNo As String = Session("EmpNo")
        Dim strsql As String

        strsql = " select c.case_id,c.black_no,c.red_no,a.case_no, "
        strsql &= "c.status_id ,s.status_name,c.type_id ,t.type_name, "
        strsql &= "e.firstname+' '+e.lastname fullname,c.defendant,c.prosecutor "
        strsql &= "from "
        strsql &= "(select c.case_no "
        strsql &= "from court_date c  "
        strsql &= "where  convert(nvarchar(10),c.dates,120) <=  "
        strsql &= "convert(nvarchar(10),getdate()-(select chk_date from check_alert where chk_id=1),120) "
        strsql &= "and c.alert=1 "
        strsql &= "union "
        strsql &= "select e.case_no  "
        strsql &= "from explain_date e "
        strsql &= "where  convert(nvarchar(10),e.dates,120) <= "
        strsql &= "convert(nvarchar(10),getdate()-(select chk_date from check_alert where chk_id=2),120)  "
        strsql &= "and e.alert=1 "
        strsql &= "group by case_no ) a "
        strsql &= "inner join case_data c "
        strsql &= "on a.case_no=c.case_no inner join case_status s "
        strsql &= "on c.status_id =s.status_id inner join case_type t "
        strsql &= "on c.type_id =t.type_id inner join employee e  "
        strsql &= "on c.creation_by=e.empid  "
        strsql &= "where c.active=1 "


        If ddlCase.SelectedValue <> "0" Then
            strsql &= "and c.type_id='" & ddlCase.SelectedValue & "' "
        End If

        If ddlStatus.SelectedValue <> "0" Then
            strsql &= "and c.status_id='" & ddlStatus.SelectedValue & "' "
        End If

        If txtBlackNo.Text <> "" Then
            strsql &= "and c.black_no like '%" & txtBlackNo.Text & "%' "
        End If

        If txtRedNo.Text <> "" Then
            strsql &= "and c.red_no like '%" & txtRedNo.Text & "%' "
        End If

        If txtProsecutor.Text <> "" Then
            strsql &= "and c.prosecutor like '%" & txtProsecutor.Text & "%' "
        End If

        If txtDefendant.Text <> "" Then
            strsql &= "and c.defendant like '%" & txtDefendant.Text & "%' "
        End If

        If txtKeyword.Text <> "" Then
            strsql &= "and c.keyword like '%" & txtKeyword.Text & "%' "
        End If

        Dim DT As DataTable = MD.GetDataTable(strsql)
        DVLst = DT.DefaultView
        DVLst.Sort = "[" & ViewState("sortfield") & "] " & ViewState("sortdirection")
        Session("CaseAlert") = DVLst

    End Sub
    Private Sub MyGridBind()
        GridView1.DataSource = DVLst
        Dim X1() As String = {"case_id"}
        GridView1.DataKeyNames = X1
        GridView1.DataBind()
    End Sub
    Public Sub DataType()
        'ประเภทคดี
        Dim strsql As String
        strsql = "select type_id,type_name from case_type order by type_name  "

        Dim DTS As DataTable = MD.GetDataTable(strsql)
        Dim dr As DataRow = DTS.NewRow
        dr!type_id = 0
        dr!type_name = "---------โปรดเลือก---------"
        DTS.Rows.InsertAt(dr, 0)
        ddlCase.DataTextField = "type_name"
        ddlCase.DataValueField = "type_id"
        ddlCase.DataSource = DTS
        ddlCase.DataBind()

    End Sub
    Public Sub DataStatus()
        'สถานะคดี
        Dim strsql As String
        strsql = "select status_id,status_name from case_status where type_id='" & ddlCase.SelectedValue & "' order by status_name  "

        Dim DTS As DataTable = MD.GetDataTable(strsql)
        Dim dr As DataRow = DTS.NewRow
        dr!status_id = 0
        dr!status_name = "---------โปรดเลือก---------"
        DTS.Rows.InsertAt(dr, 0)
        ddlStatus.DataTextField = "status_name"
        ddlStatus.DataValueField = "status_id"
        ddlStatus.DataSource = DTS
        ddlStatus.DataBind()

    End Sub
    Protected Sub bSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bSearch.Click
        Me.gData()
        Me.MyGridBind()
        'SetSearchSession()
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
            End If
        End If
    End Sub
    Protected Sub ddlCase_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCase.SelectedIndexChanged
        Me.DataStatus()
    End Sub

    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lName As Label = e.Row.Cells(0).Controls(1)
            e.Row.Cells(8).Text = "<a href=""javascript:openwindow('" + "ShowAlert" + "','" + lName.Text + "','" + "');"">" + "เลือก" + "</a>"
        End If
    End Sub
End Class
