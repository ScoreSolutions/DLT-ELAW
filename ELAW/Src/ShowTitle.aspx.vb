Imports System.Data
Imports System.Data.OleDb
Partial Class Src_ShowTitle
    Inherits System.Web.UI.Page
    Dim MD As New MainData
    Dim MC As New MainClass
    Dim DV As DataView
    Public iRow As Integer
    Dim DS As DataSet
    Dim iRec As Integer
    Dim TextBoxID As String
    Dim TextBoxName As String
    Dim TextBoxType As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        TextBoxID = Request.QueryString("id")
        TextBoxName = Request.QueryString("name")
        TextBoxType = Request.QueryString("type")
        If Not Page.IsPostBack Then

            ViewState("sortfield") = "type_id"
            ViewState("sortdirection") = "asc"

            Me.DataBookType()
            Me.gData()
            Me.MyGridBind()


        Else
            If Session("searchTitle") Is Nothing Then
                Me.gData()
            Else
                DV = Session("searchTitle")
            End If
        End If
    End Sub
    Public Sub DataBookType()
        'ประเภทหนังสือ
        Dim strsql As String
        strsql = "select link_id,link_type from book_link order by link_type  "

        Dim DTS As DataTable = MD.GetDataTable(strsql)
        Dim dr As DataRow = DTS.NewRow
        dr!link_id = 0
        dr!link_type = "---------โปรดเลือก---------"
        DTS.Rows.InsertAt(dr, 0)

        ddlBookType.DataTextField = "link_type"
        ddlBookType.DataValueField = "link_id"
        ddlBookType.DataSource = DTS
        ddlBookType.DataBind()

    End Sub
    Private Sub gData(Optional ByVal X As String = "", Optional ByVal xType As String = "S")
        Dim sEmpNo As String = Session("EmpNo")
        Dim strsql As String = ""
        

        strsql = "select * from job_title where create_name='" & sEmpNo & "' "

        If ddlBookType.SelectedValue <> "0" Then
            strsql &= " and type_id='" & ddlBookType.SelectedValue & "'"
        End If

        If txtName.Text <> "" Then
            strsql &= " and name like '%" & txtName.Text & "%'"
        End If


        Dim DT As DataTable = MD.GetDataTable(strsql)
        DV = DT.DefaultView
        DV.Sort = "[" & ViewState("sortfield") & "] " & ViewState("sortdirection")
        Session("searchTitle") = DV
    End Sub
    Private Sub MyGridBind()
        GridView1.DataSource = DV
        Dim X1() As String = {"id"}
        GridView1.DataKeyNames = X1
        GridView1.DataBind()
    End Sub
    Protected Sub GridView1_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridView1.PageIndexChanging
        GridView1.PageIndex = e.NewPageIndex
        Me.MyGridBind()
    End Sub

    Protected Sub GridView1_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowCreated
        If e.Row.RowType = DataControlRowType.Pager Then
            Dim td As TableCell = e.Row.Cells(0)
            Dim Y As Boolean = False
            If GridView1.PageIndex > 0 Then
                Dim L1 As New LinkButton
                L1.ForeColor = Drawing.Color.White
                L1.Text = "First"
                td.Controls.Add(L1)
                AddHandler L1.Click, AddressOf FirstClick

                Y = True

                Dim L2 As New Literal
                L2.Text = " &nbsp;&nbsp;"
                td.Controls.Add(L2)

                L1 = New LinkButton
                L1.ForeColor = Drawing.Color.White
                L1.Text = "Prev"
                AddHandler L1.Click, AddressOf PrevClick
                td.Controls.Add(L1)
            End If
            If GridView1.PageIndex < GridView1.PageCount - 1 Then
                Dim L2 As Literal
                If Y = True Then
                    L2 = New Literal
                    L2.Text = " &nbsp;&nbsp;"
                    td.Controls.Add(L2)
                End If

                Dim L1 As New LinkButton
                L1.ForeColor = Drawing.Color.White
                L1.Text = "Next"
                AddHandler L1.Click, AddressOf NextClick
                td.Controls.Add(L1)

                L2 = New Literal
                L2.Text = " &nbsp;&nbsp;"
                td.Controls.Add(L2)


                L1 = New LinkButton
                L1.ForeColor = Drawing.Color.White
                L1.Text = "Last"
                AddHandler L1.Click, AddressOf LastClick
                td.Controls.Add(L1)
            End If
        End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            iRow = e.Row.DataItemIndex + 1
            If Not e.Row.RowState And DataControlRowState.Edit Then
                'Dim L1 As ImageButton = e.Row.Cells(1).Controls(1)
                'L1.OnClientClick = "return confirm('คุณต้องการลบข้อมูลใช่หรือไม่?');"
            End If
        End If
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

    Protected Sub GridView1_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GridView1.RowDeleting

    End Sub

    Protected Sub GridView1_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles GridView1.RowEditing

    End Sub
    Protected Sub GridView1_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles GridView1.Sorting
        If ViewState("sortfield") = e.SortExpression Then
            If ViewState("sortdirection") = "asc" Then ViewState("sortdirection") = "desc" Else ViewState("sortdirection") = "asc"
        Else
            ViewState("sortfield") = e.SortExpression
            ViewState("sortdirection") = "asc"
        End If
        DV.Sort = "[" & ViewState("sortfield") & "] " & ViewState("sortdirection")
        Me.MyGridBind()
    End Sub
    Dim aspsrv As New clsSystemConfig
    Protected Sub GridView1_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridView1.RowCommand
        Dim pValue As String
        Dim row As GridViewRow = DirectCast(DirectCast(e.CommandSource, Control).NamingContainer, GridViewRow)
        Dim pName As String = Convert.ToString(GridView1.DataKeys(row.RowIndex).Value)

        Dim index As Integer = Convert.ToInt32(row.RowIndex)
        Dim row1 As GridViewRow = GridView1.Rows(index)
        Dim item As New ListItem()
        item.Text = Server.HtmlDecode(row1.Cells(2).Text)
        Try


            If e.CommandName = "SelectTitle" Then

                pValue = e.CommandArgument.ToString()

                Session(TextBoxID) = item.Text
                Session(TextBoxName) = pName
                Session(TextBoxType) = pValue

                Response.Write(aspsrv.RefreshPage("aspnetForm"))
                Response.Write("<script language=JavaScript>window.close()</script>")
            End If
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub bCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bCancel.Click
        Response.Write("<script language=JavaScript>window.close()</script>")
    End Sub
    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then

            DirectCast(e.Row.Cells(3).Controls(1), LinkButton).CommandArgument = DirectCast(e.Row.Cells(0).Controls(1), Label).Text

        End If
    End Sub
    Protected Sub bSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bSearch.Click
        Me.gData()
        Me.MyGridBind()
    End Sub
End Class

