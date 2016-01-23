Imports System.Data
Imports System.Data.OleDb
Partial Class Src_ShowDicisionDetail
    Inherits System.Web.UI.Page
    Dim MD As New MainData
    Dim MC As New MainClass
    Dim DVLst As DataView
    Dim DVLst1 As DataView
    Public iRow As Integer
    Dim DS As DataSet
    Dim iRec As Integer
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim X As String = Request.QueryString("id")
        If Not Page.IsPostBack Then

            Me.gDataDoc(X)
            Me.MyGridBindDoc()

        Else

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
        Dim X As String = Request.QueryString("id")
        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim K2 As String
            K2 = Convert.ToString(GridView2.DataKeys(e.Row.RowIndex).Values(0).ToString())

            Dim strsql2 As String = ""

            strsql2 = "select d.case_id,d.document_id, "
            strsql2 &= "d.file_path,d.mime_type "
            strsql2 &= "from case_document d "
            strsql2 &= "where case_id='" & X & "' and d.document_id='" & K2(0) & "'"

            Dim dt As DataTable
            dt = MD.GetDataTable(strsql2)

            Dim lblLink As Label = e.Row.Cells(1).FindControl("lblLink")

            For Each dr As DataRow In dt.Rows

                If dr("mime_type").ToString() = ".pdf" Or dr("mime_type").ToString() = ".txt" Then
                    Dim strUrl As String = "http://" & Constant.BaseURL(Request) & dr("file_path").ToString()
                    lblLink.Text = "<a href='" & strUrl & "' Target='_Blank'>ดาวน์โหลด</a>&nbsp;&nbsp;"

                Else
                    Dim strUrl As String = "http://" & Constant.BaseURL(Request) & dr("file_path").ToString()
                    lblLink.Text = "<a href=" & strUrl & ">ดาวน์โหลด</a>&nbsp;&nbsp;"

                End If

            Next

        End If
    End Sub
End Class
