Imports System.Data
Imports System.Data.OleDb
Partial Class Src_CaseReport
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

        End If
    End Sub
    Public Sub DataContractType()

        Dim strsql As String
        strsql = "select type_id,type_name from case_type order by type_name  "

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
        MC.OpenWindow(Me, "../Src/CaseReportPreview.aspx?type=" & ddlContract.SelectedValue & "&t1=" & d1 & "&t2=" & d2 & "")
    End Sub

End Class
