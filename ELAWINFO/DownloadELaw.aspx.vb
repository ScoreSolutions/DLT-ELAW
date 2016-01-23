Imports System.Data
Imports System.Data.OleDb
Partial Class DownloadELaw
    Inherits System.Web.UI.Page
    Dim Conn As OleDbConnection
    Dim Com As OleDbCommand
    Dim TR As OleDbTransaction
    Dim MD As New MainData
    Dim MC As New MainClass

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            BindData()
        End If
    End Sub

    Sub BindData()

        Dim SQL As String
        Dim DT As New DataTable

        SQL = "SELECT *"
        SQL &= " FROM File_Upload order by file_id "
        DT = MD.GetDataTable(SQL)

        If DT.Rows.Count <> 0 Then
            GridView1.DataSource = DT
            GridView1.DataBind()
        End If
        DT.Dispose()
    End Sub

    Protected Sub GridView1_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridView1.PageIndexChanging
        GridView1.PageIndex = e.NewPageIndex
        BindData()
    End Sub
    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        'Download File Document
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim strsql2 As String = ""


            strsql2 = "select * from File_Upload"
            'strsql2 &= " where file_id='" & lblID.Text & "'"


            Dim dt As DataTable
            dt = MD.GetDataTable(strsql2)

            Dim lblLink As Label = e.Row.Cells(1).FindControl("lblLink")

            For Each dr As DataRow In dt.Rows


                If dr("mime_type").ToString() = ".pdf" Or dr("mime_type").ToString() = ".txt" Then
                    Dim strUrl As String = "http://" & Constant.BaseURL(Request) & "" & dr("file_path").ToString() & ""

                    lblLink.Text = "<a href='" & strUrl & "' Target='_Blank'>ดูเอกสาร</a>&nbsp;&nbsp;"

                Else

                    Dim strUrl As String = "http://" & Constant.BaseURL(Request) & dr("file_path").ToString()
                    lblLink.Text = "<a href='" & strUrl & "' Target='_Blank'>ดูเอกสาร</a>&nbsp;&nbsp;"

                End If

            Next

        End If

    End Sub
End Class
