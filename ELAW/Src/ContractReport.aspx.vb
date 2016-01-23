Imports System.Data
Imports System.Data.OleDb
Partial Class Src_ContractReport
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
            Me.DataFullName()
           
        End If
    End Sub
    Public Sub DataContractType()

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
    Public Sub DataFullName()

        Dim strsql As String
        strsql = "select f.empid,f.name from fullname f inner join employee e  "
        strsql &= "on f.empid=e.empid where e.dept_id=1 order by f.name "

        Dim DTS As DataTable = MD.GetDataTable(strsql)
        Dim dr As DataRow = DTS.NewRow
        dr!empid = 0
        dr!name = "---------โปรดเลือก---------"
        DTS.Rows.InsertAt(dr, 0)
        ddlFullName.DataTextField = "name"
        ddlFullName.DataValueField = "empid"
        ddlFullName.DataSource = DTS
        ddlFullName.DataBind()

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
        MC.OpenWindow(Me, "../Src/ContractReportPreview.aspx?type=" & ddlContract.SelectedValue & "&t1=" & d1 & "&t2=" & d2 & "&empid=" & ddlFullName.SelectedValue & "")
    End Sub

End Class
