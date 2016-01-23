Imports System.Data
Imports System.Data.OleDb
Partial Class Profile_UserControlPoll
    Inherits System.Web.UI.UserControl

    Dim Conn As OleDbConnection
    Dim Com As OleDbCommand
    Dim TR As OleDbTransaction
    Dim MD As New MainData
    Dim MC As New MainClass

    Protected Sub btnVote_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnVote.Click
        Try
            Dim SQL As String
            SQL = " INSERT INTO VOTE "
            SQL &= "( vote,created_date )"
            SQL &= " VALUES"
            SQL &= "( "
            If RadioButton5.Checked Then
                SQL &= " 5"
            ElseIf RadioButton4.Checked Then
                SQL &= " 4"
            ElseIf RadioButton3.Checked Then
                SQL &= " 3"
            ElseIf RadioButton2.Checked Then
                SQL &= " 2"
            ElseIf RadioButton1.Checked Then
                SQL &= " 1"
            End If
            SQL &= ", GETDATE() )"

            Conn = New OleDbConnection(MD.Strcon)
            Com = New OleDbCommand(SQL, Conn)
            Conn.Open()
            TR = Conn.BeginTransaction

            Com.Transaction = TR
            Com.ExecuteNonQuery()

            'MC.MessageBox(Me, "ได้ทำการ vote เรียบร้อยแล้ว")
            TR.Commit()
        Catch ex As Exception
            TR.Rollback()
            MC.MessageBox(Me, ex.ToString)
        Finally
            Conn.Close()
        End Try
        RadioButton5.Checked = True
        CountUser()
        CountPercent()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CountUser()
        CountPercent()
    End Sub

    Sub CountUser()
        Dim Sql As String = ""
        Sql = "SELECT COUNT(*) "
        Sql &= " FROM VOTE"

        Dim DT As New DataTable
        DT = MD.GetDataTable(Sql)

        If DT.Rows.Count <> 0 Then
            lblUser.Text = DT.Rows(0)(0)
        End If
    End Sub

    Sub CountPercent()
        Dim DT As New DataTable
        Dim Sql As String = ""
        For i = 1 To 5
            Sql = "SELECT COUNT(*) FROM VOTE "
            Select Case i
                Case 1
                    Sql &= " WHERE VOTE=1"
                    DT = MD.GetDataTable(Sql)
                    lbl1.Text = String.Format("{0:#,##0.##}", DT.Rows(0)(0) / lblUser.Text * 100)
                Case 2
                    Sql &= " WHERE VOTE=2"
                    DT = MD.GetDataTable(Sql)
                    lbl2.Text = String.Format("{0:#,##0.##}", DT.Rows(0)(0) / lblUser.Text * 100)
                Case 3
                    Sql &= " WHERE VOTE=3"
                    DT = MD.GetDataTable(Sql)
                    lbl3.Text = String.Format("{0:#,##0.##}", DT.Rows(0)(0) / lblUser.Text * 100)
                Case 4
                    Sql &= " WHERE VOTE=4"
                    DT = MD.GetDataTable(Sql)
                    lbl4.Text = String.Format("{0:#,##0.##}", DT.Rows(0)(0) / lblUser.Text * 100)
                Case 5
                    Sql &= " WHERE VOTE=5"
                    DT = MD.GetDataTable(Sql)
                    lbl5.Text = String.Format("{0:#,##0.##}", DT.Rows(0)(0) / lblUser.Text * 100)
            End Select
        Next
    End Sub
End Class
