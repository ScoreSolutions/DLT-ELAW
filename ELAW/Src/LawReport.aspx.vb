Imports System.Data
Imports System.Data.OleDb
Partial Class Src_LawReport
    Inherits System.Web.UI.Page
    Dim MD As New MainData
    Dim MC As New MainClass
    Dim DVLst As DataView
    Public iRow As Integer
    Dim DS As DataSet
    Dim iRec As Integer
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then

            Me.DataDiv()
            Me.DataName()

        End If
    End Sub
    Public Sub DataDiv()

        Dim strsql As String
        strsql = "select div_id,div_name from division where div_id in (3,4) order by div_name  "

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
    Public Sub DataName()

        Dim strsql As String
        strsql = "select empid,name from fullname where div_id='" & ddlDiv.SelectedValue & "' order by name  "

        Dim DTS As DataTable = MD.GetDataTable(strsql)
        Dim dr As DataRow = DTS.NewRow
        dr!empid = 0
        dr!name = "---------โปรดเลือก---------"
        DTS.Rows.InsertAt(dr, 0)
        ddlName.DataTextField = "name"
        ddlName.DataValueField = "empid"
        ddlName.DataSource = DTS
        ddlName.DataBind()

    End Sub
    Protected Sub bPreview_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bPreview.Click
        Dim d1, d2 As String

        If DatePicker1.Text.Year = "1" Then
            d1 = ""
        Else
            d1 = DatePicker1.SaveDate
        End If
        If DatePicker2.Text.Year = "1" Then
            d2 = ""
        Else
            d2 = DatePicker2.SaveDate
        End If
        MC.OpenWindow(Me, "../Src/LawReportPreview.aspx?name=" & ddlName.SelectedValue & "&t1=" & d1 & "&t2=" & d2 & "&div=" & ddlDiv.SelectedValue & "")
    End Sub
    Protected Sub ddlDiv_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlDiv.SelectedIndexChanged
        Me.DataName()
    End Sub

    Protected Sub linkHome_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles linkHome.Click
        Response.Redirect(MD.pHome, True)
    End Sub
End Class
