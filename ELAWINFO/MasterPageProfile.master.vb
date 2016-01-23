Imports System.Data
Imports System.Data.OleDb
Partial Class Profile_MasterPageProfile
    Inherits System.Web.UI.MasterPage

    Dim Conn As OleDbConnection
    Dim Com As OleDbCommand
    Dim TR As OleDbTransaction
    Dim MD As New MainData
    Dim MC As New MainClass

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            UpdatePageView()
        End If
        CountPageView()
    End Sub

    Sub CountPageView()
        Dim Sql As String = ""
        Sql = "SELECT Count "
        Sql &= " FROM VIEWCOUNT"

        Dim DT As New DataTable
        DT = MD.GetDataTable(Sql)

        If DT.Rows.Count <> 0 Then
            lblCount.Text = DT.Rows(0)(0)
        End If
    End Sub

    Sub UpdatePageView()
        Try
            Dim i As Integer
            Dim Sql As String = ""
            Sql = "SELECT Count FROM VIEWCOUNT"

            Dim DT As New DataTable
            DT = MD.GetDataTable(Sql)
            i = DT.Rows(0)(0) + 1

            Conn = New OleDbConnection(MD.Strcon)
            Com = New OleDbCommand()
            Conn.Open()

            Sql = "UPDATE VIEWCOUNT SET Count=" & i
            With Com
                .CommandText = Sql
                .CommandType = CommandType.Text
                .Connection = Conn
                .ExecuteNonQuery()
            End With
        Catch ex As Exception
            MC.MessageBox(Me, ex.ToString)
        Finally
            Conn.Close()
        End Try
    End Sub
End Class

