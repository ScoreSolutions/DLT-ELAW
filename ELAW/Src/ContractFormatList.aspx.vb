﻿Imports System.Data
Imports System.Data.OleDb
Partial Class Src_ContractFormatList
    Inherits System.Web.UI.Page
    Dim MD As New MainData
    Dim MC As New MainClass
    Dim DVLst As DataView
    Public iRow As Integer
    Dim DS As DataSet
    Dim iRec As Integer
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Me.DataContractType()
            Me.gData("0")
            Me.MyGridBind()
        Else

            If Session("ContractFormat") Is Nothing Then
                Me.gData()
            Else
                DVLst = Session("ContractFormat")
            End If

        End If
    End Sub
    Public Sub DataContractType()
        'ประเภทสัญญา
        Dim strsql As String
        strsql = "select type_id,type_name from contract_type order by type_name  "

        Dim DTS As DataTable = MD.GetDataTable(strsql)
        Dim dr As DataRow = DTS.NewRow
        dr!type_id = 0
        dr!type_name = "---------โปรดเลือก---------"
        DTS.Rows.InsertAt(dr, 0)
        ddlContract.DataTextField = "type_name"
        ddlContract.DataValueField = "type_id"
        ddlContract.DataSource = DTS
        ddlContract.DataBind()

    End Sub
    Private Sub gData(Optional ByVal Type As String = "")
        'ดึงข้อมูลมาแสดงใน Gridview
        Dim strsql As String

        strsql = " select s.subtype_id,s.subtype_name,t.type_name  "
        strsql &= "from contract_subtype s inner join contract_type t "
        strsql &= "on s.type_id=t.type_id "

        If Type <> "0" Then
            strsql &= "where t.type_id='" & Type & "' "
        End If

        Dim DT As DataTable = MD.GetDataTable(strsql)
        DVLst = DT.DefaultView
        Session("ContractFormat") = DVLst
    End Sub
    Private Sub MyGridBind()
        GridView1.DataSource = DVLst
        Dim X1() As String = {"subtype_id"}
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
        Response.Redirect("../Src/ContractFormat.aspx?id=" & K1(0) & "&menu=4")
    End Sub
    Protected Sub ddlContract_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlContract.SelectedIndexChanged
        Me.gData(ddlContract.SelectedValue)
        Me.MyGridBind()
    End Sub
    Protected Sub linkHome_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles linkHome.Click
        Response.Redirect(MD.pHome, True)
    End Sub
End Class
