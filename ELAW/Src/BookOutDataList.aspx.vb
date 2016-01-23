Imports System.Data
Imports System.Data.OleDb
Partial Class Src_BookOutDataList
    Inherits System.Web.UI.Page
    Dim MD As New MainData
    Dim MC As New MainClass
    Dim DVLst As DataView
    Public iRow As Integer
    Dim DS As DataSet
    Dim iRec As Integer
    Const menuID As Integer = Constant.MenuID.BookOutDataList
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            ViewState("sortfield") = "bookout_id"
            ViewState("sortdirection") = "desc"
            Me.DataType()
            Me.DataStatus()
            Me.DataDivision()
            FillCondition()
            Me.gData()
            Me.MyGridBind()

            Try
                Dim data As DataRow = Session(Constant.searchSession)
                If Session("txtPage") IsNot Nothing And data("menuID") = menuID Then
                    GridView1.PageIndex = Session("txtPage")
                End If
                Me.MyGridBind()
            Catch ex As Exception

            End Try

        Else
            If Session("BookOutDataList") Is Nothing Then
                Me.gData()
            Else
                DVLst = Session("BookOutDataList")
            End If

        End If
    End Sub
    Private Sub SetSearchSession()

        Dim dt As New DataTable
        dt.Columns.Add("menuID")
        dt.Columns.Add("ddlType")
        dt.Columns.Add("ddlStatus")
        dt.Columns.Add("ddlDiv")

        dt.Columns.Add("txtBookNo")
        dt.Columns.Add("txtTopic")
        dt.Columns.Add("txtKeyword")

        Dim data As DataRow = dt.NewRow
        data("menuID") = menuID
        data("ddlType") = ddlType.SelectedValue
        data("ddlStatus") = ddlStatus.SelectedValue
        data("ddlDiv") = ddlDiv.SelectedValue

        data("txtBookNo") = txtBookNo.Text
        data("txtTopic") = txtTopic.Text
        data("txtKeyword") = txtKeyword.Text

        Session(Constant.searchSession) = data
    End Sub
    Private Sub FillCondition()
        If Session(Constant.searchSession) IsNot Nothing Then
            Dim data As DataRow = Session(Constant.searchSession)
            If data("menuID") = menuID Then
                ddlType.SelectedValue = data("ddlType")
                ddlStatus.SelectedValue = data("ddlStatus")
                ddlDiv.SelectedValue = data("ddlDiv")
                txtBookNo.Text = data("txtBookNo")
                txtTopic.Text = data("txtTopic")
                txtKeyword.Text = data("txtKeyword")
            Else
                Session.Remove(Constant.searchSession)
            End If
        End If
    End Sub
    Private Sub gData(Optional ByVal Type As String = "")
        'ดึงข้อมูลมาแสดงใน Gridview
        Dim sEmpNo As String = Session("EmpNo")
        Dim strsql As String

        strsql = " select b.bookout_id,b.system_no,b.bookout_no,b.bookkind_id,k.bookkind_name,  "
        strsql &= "b.status_id,b.topic,b.keyword,b.dates,e.firstname+' '+e.lastname createname, "
        strsql &= "case when b.cancel=1 then 'ยกเลิก' else s.status_name end status_name,right(b.runno,4) runno "
        strsql &= "from bookout_data b inner join book_kind k "
        strsql &= "on b.bookkind_id=k.bookkind_id inner join book_status s "
        strsql &= "on b.status_id=s.status_id inner join employee e "
        strsql &= "on b.creation_by=e.empid "
        strsql &= "where b.active=1 "

        If ddlType.SelectedValue <> "0" Then
            strsql &= "and b.bookkind_id='" & ddlType.SelectedValue & "' "
        End If


        If ddlStatus.SelectedValue <> "0" Then
            strsql &= "and b.status_id='" & ddlStatus.SelectedValue & "' "
        End If

        If ddlDiv.SelectedValue <> "0" Then
            strsql &= "and e.div_id='" & ddlDiv.SelectedValue & "' "
        End If

        If txtBookNo.Text <> "" Then
            strsql &= "and b.bookout_no like '%" & txtBookNo.Text & "%' "
        End If

        If txtTopic.Text <> "" Then
            strsql &= "and b.topic like '%" & txtTopic.Text & "%' "
        End If

        If txtKeyword.Text <> "" Then
            strsql &= "and (b.keyword like '%" & txtKeyword.Text & "%' "
            strsql &= "or e.firstname +' '+e.lastname like '%" & txtKeyword.Text & "%')  "

        End If

        Dim DT As DataTable = MD.GetDataTable(strsql)
        DVLst = DT.DefaultView
        DVLst.Sort = "[" & ViewState("sortfield") & "] " & ViewState("sortdirection")
        Session("BookOutDataList") = DVLst

    End Sub
    Private Sub MyGridBind()
        GridView1.DataSource = DVLst
        Dim X1() As String = {"bookout_id"}
        GridView1.DataKeyNames = X1
        GridView1.DataBind()
    End Sub
    Public Sub DataType()
        'ประเภทหนังสือ
        Dim strsql As String
        strsql = "select bookkind_id,bookkind_name from book_kind where booktype_id=2 order by bookkind_name  "

        Dim DTS As DataTable = MD.GetDataTable(strsql)
        Dim dr As DataRow = DTS.NewRow
        dr!bookkind_id = 0
        dr!bookkind_name = "---------โปรดเลือก---------"
        DTS.Rows.InsertAt(dr, 0)
        ddlType.DataTextField = "bookkind_name"
        ddlType.DataValueField = "bookkind_id"
        ddlType.DataSource = DTS
        ddlType.DataBind()

    End Sub
    Public Sub DataStatus()
        'สถานะหนังสือ
        Dim strsql As String
        strsql = "select status_id,status_name from book_status where booktype_id=2 order by status_name  "

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
    Public Sub DataDivision()
        'สถานะหนังสือ
        Dim strsql As String
        strsql = "select * from division where dept_id=1  "

        Dim DTS As DataTable = MD.GetDataTable(strsql)
        Dim dr As DataRow = DTS.NewRow
        dr!div_id = 0
        dr!div_name = "---------โปรดเลือก---------"
        DTS.Rows.InsertAt(dr, 0)
        ddlDiv.DataTextField = "div_name"
        ddlDiv.DataValueField = "div_id"
        ddlDiv.DataSource = DTS
        ddlDiv.DataBind()

    End Sub
    Protected Sub bSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bSearch.Click
        Me.gData()
        Me.MyGridBind()
        SetSearchSession()
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
    Protected Sub GridView1_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles GridView1.RowEditing
    End Sub
    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        'If e.Row.RowType = DataControlRowType.DataRow Then
        '    Dim lName As Label = e.Row.Cells(0).Controls(1)
        '    e.Row.Cells(7).Text = "<a href=""javascript:openwindow('" + "BookOutPreview2" + "','" + lName.Text + "','" + "');"">" + "เลือก" + "</a>"
        'End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            If DataBinder.Eval(e.Row.DataItem, "status_name").ToString = "ยกเลิก" Then
                Dim i As Integer = 1
                While i < 8
                    e.Row.Cells(i).ForeColor = Drawing.Color.Red
                    i = i + 1
                End While

            End If

            If DataBinder.Eval(e.Row.DataItem, "status_name").ToString = "รอออกเลขหนังสือ" Then
                Dim i As Integer = 1
                While i < 8
                    e.Row.Cells(i).ForeColor = Drawing.Color.Blue
                    i = i + 1
                End While

            End If
        End If
    End Sub
    Protected Sub GridView1_SelectedIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSelectEventArgs) Handles GridView1.SelectedIndexChanging
        Dim K1 As DataKey = GridView1.DataKeys(e.NewSelectedIndex)
        SetSearchSession()
        Session("txtPage") = iRow / 10 - 1
        Response.Redirect("../Src/BookOutPreview2.aspx?id=" & K1(0) & "", True)
    End Sub
End Class
