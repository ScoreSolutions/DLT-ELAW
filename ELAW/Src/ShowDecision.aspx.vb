﻿Imports System.Data
Imports System.Data.OleDb
Partial Class Src_ShowDecision
    Inherits System.Web.UI.Page
    Dim MD As New MainData
    Dim MC As New MainClass
    Dim DVLst As DataView
    Dim DVLst1 As DataView
    Public iRow As Integer
    Dim DS As DataSet
    Dim iRec As Integer
    Const menuID As Integer = Constant.MenuID.ShowDicision
    Private Sub ChkPermis()
        'ตรวจสอบสิทธิ์การใช้งาน
        Dim sEmpNo As String = Session("EmpNo")
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
        dt.Columns.Add("ddlCase")

        dt.Columns.Add("txtblack")
        dt.Columns.Add("txtRed")
        dt.Columns.Add("txtProsecutor")
        dt.Columns.Add("txtDefendant")
        dt.Columns.Add("txtKeyword")

        Dim data As DataRow = dt.NewRow
        data("menuID") = menuID
        data("ddlCase") = ddlCase.SelectedValue

        data("txtBlack") = txtBlackNo.Text
        data("txtRed") = txtRedNo.Text
        data("txtProsecutor") = txtProsecutor.Text
        data("txtDefendant") = txtDefendant.Text
        data("txtKeyword") = txtKeyword.Text
        Session(Constant.searchSession) = data
    End Sub
    Private Sub FillCondition()
        If Session(Constant.searchSession) IsNot Nothing Then
            Dim data As DataRow = Session(Constant.searchSession)
            If data("menuID") = menuID Then
                ddlCase.SelectedValue = data("ddlCase")


                ddlCase.SelectedValue = data("ddlCase")
                txtBlackNo.Text = data("txtBlack")
                txtRedNo.Text = data("txtRed")
                txtProsecutor.Text = data("txtProsecutor")
                txtDefendant.Text = data("txtDefendant")
                txtKeyword.Text = data("txtKeyword")
            Else
                Session.Remove(Constant.searchSession)
            End If
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Me.ChkPermis()
        If Not Page.IsPostBack Then
            ViewState("sortfield") = "case_id"
            ViewState("sortdirection") = "desc"

            Me.DataType()
            'FillCondition()
            Me.gData("0")
            Me.MyGridBind()
        Else

            If Session("ShowDecisionList") Is Nothing Then
                Me.gData()
            Else
                DVLst = Session("ShowDecisionList")
            End If

        End If

    End Sub
    Private Sub gData(Optional ByVal Type As String = "")
        'ดึงข้อมูลมาแสดงใน Gridview
        Dim sEmpNo As String = Session("EmpNo")
        Dim strsql As String

        strsql = " select c.case_id,c.case_no,c.black_no,c.red_no,c.status_id,s.status_name,c.type_id,t.type_name, "
        strsql &= "e.firstname+' '+e.lastname fullname,c.defendant,c.prosecutor "
        strsql &= "from case_data c inner join case_status s "
        strsql &= "on c.status_id=s.status_id inner join case_type t "
        strsql &= "on c.type_id=t.type_id inner join employee e "
        strsql &= "on c.creation_by=e.empid "
        strsql &= "where c.active=1 "
        strsql &= "and c.status_id in (select step_last from case_process) "

        If ddlCase.SelectedValue <> "0" Then
            strsql &= "and c.type_id='" & ddlCase.SelectedValue & "' "
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
        Session("ShowDecisionList") = DVLst

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
    Private Sub gDataDoc(Optional ByVal id As String = "")
        'ข้อมูลเอกสารประกอบ
        Dim strsql As String
        strsql = " select d.document_id,d.title,d.page  "
        strsql &= "from case_document d "
        strsql &= "where d.case_id='" & id & "' and d.decision='T' "

        Dim DT As DataTable = MD.GetDataTable(strsql)
        DVLst1 = DT.DefaultView
        Session("DocumentCaseDecision") = DVLst1
    End Sub
    Private Sub MyGridBindDoc()
        GridView2.DataSource = DVLst1
        Dim X1() As String = {"document_id"}
        GridView2.DataKeyNames = X1
        GridView2.DataBind()
    End Sub
    Protected Sub GridView2_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView2.RowDataBound
        'Download File Document
        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim K2 As String
            K2 = Convert.ToString(GridView2.DataKeys(e.Row.RowIndex).Values(0).ToString())

            Dim strsql2 As String = ""

            strsql2 = "select d.case_id,d.document_id, "
            strsql2 &= "d.file_path,d.mime_type "
            strsql2 &= "from case_document d "
            strsql2 &= "where case_id='" & lblCaseId.Text & "' and d.document_id='" & K2(0) & "'"

            Dim dt As DataTable
            dt = MD.GetDataTable(strsql2)

            Dim lblLink As Label = e.Row.Cells(1).FindControl("lblLink")

            For Each dr As DataRow In dt.Rows

                If dr("mime_type").ToString() = ".pdf" Or dr("mime_type").ToString() = ".txt" Then
                    Dim strUrl As String = "../" & "" & dr("file_path").ToString() & ""
                    lblLink.Text = "<a href='" & strUrl & "' Target='_Blank'>ดาวน์โหลด</a>&nbsp;&nbsp;"

                Else
                    Dim strUrl As String = "../" & dr("file_path").ToString()
                    lblLink.Text = "<a href=" & strUrl & ">ดาวน์โหลด</a>&nbsp;&nbsp;"

                End If

            Next

        End If
    End Sub
    Protected Sub GridView1_SelectedIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSelectEventArgs) Handles GridView1.SelectedIndexChanging
        Dim K1 As DataKey = GridView1.DataKeys(e.NewSelectedIndex)

        'lblCaseId.Text = K1(0).ToString

        'Me.gDataDoc(K1(0).ToString)
        'Me.MyGridBindDoc()

        'lblHeader.Text = "เอกสารประกอบคำพิพากษา"

        MC.OpenWindow(Me, "../Src/ShowdicisionDetail.aspx?id=" & K1(0) & "")
    End Sub
End Class

