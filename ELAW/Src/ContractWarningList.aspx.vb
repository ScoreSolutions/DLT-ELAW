Imports System.Data
Imports System.Data.OleDb
Partial Class Src_ContractWarningList
    Inherits System.Web.UI.Page
    Dim MD As New MainData
    Dim MC As New MainClass
    Dim DVLst As DataView
    Public iRow As Integer
    Dim DS As DataSet
    Dim iRec As Integer
    Const menuID As Integer = Constant.MenuID.ContractWarningList
    Private Sub ChkPermis()
        'กำหนดสิทธิ์การใช้งาน
        Dim sEmpNo As String = Page.User.Identity.Name
        Dim url As String = HttpContext.Current.Request.FilePath
        If sEmpNo = "" Then
            Response.Redirect(MD.pLogin, True)
        Else
            Dim chk As Boolean = MC.ChkPermission(sEmpNo, url)
            If chk = False Then
                Response.Redirect(MD.pNoAut, True)
            End If
        End If
    End Sub
    Private Sub SetSearchSession()

        Dim dt As New DataTable
        dt.Columns.Add("menuID")
        dt.Columns.Add("ddlContract")


        dt.Columns.Add("txtNo")
        dt.Columns.Add("txtMatetial")
        dt.Columns.Add("txtTaxId")

        dt.Columns.Add("txtSaleName")
        dt.Columns.Add("txtKeyword")

        Dim data As DataRow = dt.NewRow
        data("menuID") = menuID
        data("ddlContract") = ddlContract.SelectedValue


        data("txtNo") = txtNo.Text
        data("txtMatetial") = txtMatetial.Text
        data("txtTaxId") = txtTaxId.Text

        data("txtSaleName") = txtSaleName.Text
        data("txtKeyword") = txtKeyword.Text

        Session(Constant.searchSession) = data
    End Sub
    Private Sub FillCondition()
        If Session(Constant.searchSession) IsNot Nothing Then
            Dim data As DataRow = Session(Constant.searchSession)
            If data("menuID") = menuID Then
                ddlContract.SelectedValue = data("ddlContract")


                txtNo.Text = data("txtNo")
                txtMatetial.Text = data("txtMatetial")
                txtTaxId.Text = data("txtTaxId")

                txtSaleName.Text = data("txtSaleName")
                txtKeyword.Text = data("txtKeyword")

            Else
                Session.Remove(Constant.searchSession)
            End If
        End If
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.ChkPermis()
        If Not Page.IsPostBack Then
            ViewState("sortfield") = "contract_id"
            ViewState("sortdirection") = "asc"

            Me.DataStatus()
            Me.DataContractType()
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

            If Session("ContractWarnlst") Is Nothing Then
                Me.gData()
            Else
                DVLst = Session("ContractWarnlst")
            End If

        End If

    End Sub
    Public Sub DataContractType()
        'ประเภทสัญญา
        Dim strsql As String
        strsql = "select subtype_id,subtype_name from contract_subtype order by subtype_name  "

        Dim DTS As DataTable = MD.GetDataTable(strsql)
        Dim dr As DataRow = DTS.NewRow
        dr!subtype_id = 0
        dr!subtype_name = "---------โปรดเลือก---------"
        DTS.Rows.InsertAt(dr, 0)
        ddlContract.DataTextField = "subtype_name"
        ddlContract.DataValueField = "subtype_id"
        ddlContract.DataSource = DTS
        ddlContract.DataBind()
    End Sub
    Public Sub DataStatus()
        'สถานะสัญญา
        Dim strsql As String
        strsql = "select status_id,status_name from contract_status where status_id not in (1,2,3,8) order by status_name  "

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
    Public Function ImagesGet(ByVal X As String) As String
        Dim X1 As String = Replace(X, " ", "_")
        Dim X2 As String = "..\Images\" & X1 & ".png"
        Dim xFile As String = Server.MapPath(X2)

        If IO.File.Exists(xFile) Then
            Return "<img src='" & Replace(X2, "\", "/") & "' align='absmiddle'>"
        Else
            Return ""
        End If
    End Function
    Private Sub gData(Optional ByVal Type As String = "")
        'ดึงข้อมูลมาแสดงใน Gridview
        Dim sEmpNo As String = Session("EmpNo")
        Dim strsql As String

        strsql = "select d.contract_id,d.contract_no,d.dates_recieve,d.dates_contract,d.dates_comesign,d.dates_sign, "
        strsql &= "d.material,d.user_sale,d.tax_id,d.subtype_id,d.user_buy,s.subtype_name, "
        strsql &= "case when d.cancel_id is not null then 'ยกเลิก' else s1.status_name end status_name,"
        strsql &= "e.firstname+' '+e.lastname fullname, "
        strsql &= "e1.firstname +' '+e1.lastname creation_name, "
        strsql &= "case when convert(nvarchar(10),d.dates_recieve,120) <= convert(nvarchar(10),getdate()-4,120)and d.cancel_id is null and d.status_id <> '" & MD.sFinishContract & "'"
        strsql &= "then 'SHOW' else 'NO' end warning "
        strsql &= "from contract_data d  "
        strsql &= "inner join contract_subtype s "
        strsql &= "on d.subtype_id=s.subtype_id "
        strsql &= "inner join employee e "
        strsql &= "on d.user_buy =e.empid "
        strsql &= "inner join employee e1 "
        strsql &= "on d.creation_by =e1.empid "
        strsql &= "inner join contract_status s1 "
        strsql &= "on d.status_id=s1.status_id "
        strsql &= "where d.status_id not in (1,2,3) "
        strsql &= "and d.active=1"
        strsql &= "and d.creation_by='" & sEmpNo & "' "

        If ddlContract.SelectedValue <> "0" Then
            strsql &= "and d.subtype_id='" & ddlContract.SelectedValue & "' "
        End If

        If txtNo.Text <> "" Then
            strsql &= "and d.contract_no like '%" & txtNo.Text & "%' "
        End If

        If txtMatetial.Text <> "" Then
            strsql &= "and d.material like '%" & txtMatetial.Text & "%' "
        End If

        If txtTaxId.Text <> "" Then
            strsql &= "and d.tax_id like '%" & txtTaxId.Text & "%' "
        End If

        If ddlStatus.SelectedValue <> "0" Then
            strsql &= "and d.status_id='" & ddlStatus.SelectedValue & "' "
        End If

        If txtSaleName.Text <> "" Then
            strsql &= "and d.user_sale like '%" & txtSaleName.Text & "%' "
        End If

        If txtKeyword.Text <> "" Then
            strsql &= "and d.keyword like '%" & txtKeyword.Text & "%' "
        End If

        Dim DT As DataTable = MD.GetDataTable(strsql)
        DVLst = DT.DefaultView
        DVLst.Sort = "[" & ViewState("sortfield") & "] " & ViewState("sortdirection")
        Session("ContractWarnlst") = DVLst

    End Sub
    Private Sub MyGridBind()
        GridView1.DataSource = DVLst
        Dim X1() As String = {"contract_id"}
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
            End If
        End If
    End Sub
    Protected Sub GridView1_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles GridView1.RowEditing
        Dim K1 As DataKey = GridView1.DataKeys(e.NewEditIndex)
        SetSearchSession()
        Session("txtPage") = iRow / 10 - 1
        Response.Redirect("../Src/ContractPreview.aspx?id=" & K1(0) & "&status=chkstate&menu=4")
    End Sub
    Protected Sub ddlContract_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlContract.SelectedIndexChanged
        Me.gData(ddlContract.SelectedValue)
        Me.MyGridBind()
    End Sub
    Protected Sub bSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bSearch.Click
        Me.gData()
        Me.MyGridBind()
        SetSearchSession()
    End Sub
    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            If DataBinder.Eval(e.Row.DataItem, "status_name").ToString = "ยกเลิก" Then
                Dim i As Integer = 1
                While i < 9
                    e.Row.Cells(i).ForeColor = Drawing.Color.Red
                    i = i + 1
                End While

            End If
        End If
    End Sub
    Protected Sub linkHome_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles linkHome.Click
        Response.Redirect(MD.pHome, True)
    End Sub
End Class
