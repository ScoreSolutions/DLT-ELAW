Partial Class SearchLawData
    Inherits System.Web.UI.Page
    Dim WS As New ELAWSERVICE.Service
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then

            DataLaw()
            DataContract()
            DataCase()

            'Dim ct As Boolean = WS.setPublicCounter()
            'lblCounter.Text = WS.getPublicCounter

        End If
    End Sub
    Private Sub DataLaw()

        Dim DT As System.Data.DataTable = WS.DataLaw
        Dim DVLstSearch As System.Data.DataView = DT.DefaultView
        Session("law_type") = DVLstSearch

        grdLaw.DataSource = DVLstSearch
        Dim X1() As String = {"type_id"}
        grdLaw.DataKeyNames = X1
        grdLaw.DataBind()

    End Sub
    Private Sub DataContract()

        Dim DT As System.Data.DataTable = WS.DataContract
        Dim DVLstContract As System.Data.DataView = DT.DefaultView
        Session("contract_type") = DVLstContract

        grdContract.DataSource = DVLstContract
        Dim X1() As String = {"type_id"}
        grdContract.DataKeyNames = X1
        grdContract.DataBind()

    End Sub
    Private Sub DataCase()

        Dim DT As System.Data.DataTable = WS.DataCase
        Dim DVLstCase As System.Data.DataView = DT.DefaultView
        Session("case_type") = DVLstCase

        grdCase.DataSource = DVLstCase
        Dim X1() As String = {"type_id"}
        grdCase.DataKeyNames = X1
        grdCase.DataBind()

    End Sub
    Protected Sub bClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bClear.Click
        Response.Redirect("SearchLawData.aspx")
        'txtKeyword.Text = ""

        'Me.DataLaw()

        'Me.DataContract()

        'Me.DataCase()


        'Dim t1 As String = Me.ChkLaw()
        'Dim t2 As String = Me.ChkContract()
        'Dim t3 As String = Me.ChkCase()

        'Dim DT As System.Data.DataTable = WS.SearchKeyWord(txtKeyword.Text, t1, t2, t3)

        'Dim DVLst As System.Data.DataView = DT.DefaultView

        'GridView1.DataSource = DVLst
        'Dim X1() As String = {"doc_id"}
        'GridView1.DataKeyNames = X1
        'GridView1.DataBind()
    End Sub
    Protected Sub bSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bSearch.Click
        'ค้นหาเอกสาร

        If txtKeyword.Text.Trim() <> "" Then
            WS.SaveKeyword(txtKeyword.Text.Trim())
        End If

        Dim t1 As String = Me.ChkLaw()
        Dim t2 As String = Me.ChkContract()
        Dim t3 As String = Me.ChkCase()


        Dim DT As System.Data.DataTable = WS.SearchKeyWord(txtKeyword.Text, t1, t2, t3)

        Dim DVLst As System.Data.DataView = DT.DefaultView

        GridView1.DataSource = DVLst
        Dim X1() As String = {"doc_id"}
        GridView1.DataKeyNames = X1
        GridView1.DataBind()

    End Sub
    Function ChkLaw() As String
        'ค้นหาเอกสาร'
        Dim S1 As New System.Text.StringBuilder("")
        Dim strSql As String = ""
        Dim txtType As String = ""
        Dim oDs As New Data.DataSet
        Dim sData As String = ""

        'If txtKeyword.Text.Trim() <> "" Then
        '    WS.SaveKeyword(txtKeyword.Text.Trim())
        'End If

        For Each dgi As GridViewRow In grdLaw.Rows
            Dim cb As CheckBox = dgi.Cells(1).FindControl("cb1")
            If cb.Checked = True Then

                Dim K1 As DataKey = grdLaw.DataKeys(dgi.RowIndex)
                Dim index As Integer = Convert.ToInt32(dgi.RowIndex)
                Dim row As GridViewRow = grdLaw.Rows(index)
                Dim item As New ListItem()

                item.Text = Server.HtmlDecode(row.Cells(1).Text) 'item

                Dim Vkey As String = K1.Value
                If S1.Length > 0 Then S1.Append("','")

                S1.Append(K1(0).ToString)

            End If

        Next

        If S1.ToString.Length > 0 Then
            Return "'" & S1.ToString & "'"
        Else
            Return ""
        End If
    End Function
    Function ChkContract() As String
        'ค้นหาเอกสาร
        Dim S1 As New System.Text.StringBuilder("")
        Dim strSql As String = ""
        Dim txtType As String = ""
        Dim oDs As New Data.DataSet
        Dim sData As String = ""

        For Each dgi As GridViewRow In grdContract.Rows
            Dim cb As CheckBox = dgi.Cells(1).FindControl("cb2")
            If cb.Checked = True Then

                Dim K1 As DataKey = grdContract.DataKeys(dgi.RowIndex)
                Dim index As Integer = Convert.ToInt32(dgi.RowIndex)
                Dim row As GridViewRow = grdContract.Rows(index)
                Dim item As New ListItem()

                item.Text = Server.HtmlDecode(row.Cells(1).Text) 'item

                Dim Vkey As String = K1.Value
                If S1.Length > 0 Then S1.Append("','")

                S1.Append(K1(0).ToString)

            End If

        Next

        If S1.ToString.Length > 0 Then
            Return "'" & S1.ToString & "'"
        Else
            Return ""
        End If
    End Function
    Function ChkCase() As String
        'ค้นหาเอกสาร
        Dim S1 As New System.Text.StringBuilder("")
        Dim strSql As String = ""
        Dim txtType As String = ""
        Dim oDs As New Data.DataSet
        Dim sData As String = ""

        For Each dgi As GridViewRow In grdCase.Rows
            Dim cb As CheckBox = dgi.Cells(1).FindControl("cb3")
            If cb.Checked = True Then

                Dim K1 As DataKey = grdCase.DataKeys(dgi.RowIndex)
                Dim index As Integer = Convert.ToInt32(dgi.RowIndex)
                Dim row As GridViewRow = grdCase.Rows(index)
                Dim item As New ListItem()

                item.Text = Server.HtmlDecode(row.Cells(1).Text) 'item

                Dim Vkey As String = K1.Value
                If S1.Length > 0 Then S1.Append("','")

                S1.Append(K1(0).ToString)

            End If

        Next

        If S1.ToString.Length > 0 Then
            Return "'" & S1.ToString & "'"
        Else
            Return ""
        End If
    End Function
    Protected Sub GridView1_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridView1.PageIndexChanging
        GridView1.PageIndex = e.NewPageIndex

        Dim t1 As String = Me.ChkLaw()
        Dim t2 As String = Me.ChkContract()
        Dim t3 As String = Me.ChkCase()


        Dim DT As System.Data.DataTable = WS.SearchKeyWord(txtKeyword.Text, t1, t2, t3)

        Dim DVLst As System.Data.DataView = DT.DefaultView

        GridView1.DataSource = DVLst
        Dim X1() As String = {"doc_id"}
        GridView1.DataKeyNames = X1
        GridView1.DataBind()
    End Sub
    Private Sub GoPage(ByVal xPage As Integer)
        GridView1.PageIndex = xPage


        Dim t1 As String = Me.ChkLaw()
        Dim t2 As String = Me.ChkContract()
        Dim t3 As String = Me.ChkCase()


        Dim DT As System.Data.DataTable = WS.SearchKeyWord(txtKeyword.Text, t1, t2, t3)

        Dim DVLst As System.Data.DataView = DT.DefaultView

        GridView1.DataSource = DVLst
        Dim X1() As String = {"doc_id"}
        GridView1.DataKeyNames = X1
        GridView1.DataBind()
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


    End Sub
    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            e.Row.Attributes.Add("onmousemove", "javascript:ChangeRowColor('" & e.Row.ClientID & "')")
        End If

        'คลิกเลือกเพื่อทำการเปิดหน้าดาวน์โหลดเอกสาร
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lName As Label = e.Row.Cells(0).Controls(1)
            e.Row.Cells(3).Text = "<a href=""javascript:openwindow('" + "LawDetail" + "','" + lName.Text + "','" + "');"">" + "เลือก" + "</a>"
        End If
    End Sub


End Class
