Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine
Imports System.Data.OleDb
Imports System.IO
Imports System.Data.DataSet
Imports System.Data
Partial Class Src_BookInReportPreview
    Inherits System.Web.UI.Page
    Dim crReportDocument As Report = New Report
    Dim MD As New MainData
    Dim Main As New MainData
    Dim DVpic As Data.DataView
#Region " Web Form Designer Generated Code "
    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init

        Dim id As String = Request.QueryString("id")
        Dim t1 As String = Request.QueryString("t1")
        Dim t2 As String = Request.QueryString("t2")
        Dim DSreport As New DataSet()
        Dim SqlReader As SqlClient.SqlDataReader
        Dim Conn As New SqlClient.SqlConnection()

        Dim sql As String



        sql = "select b.bookin_no,b.stamp_date ,b.topic ,b.from_name ,' ' div_regis,' ' book_regis,' ' no_regis,' ' date_regis , "
        sql &= " ' ' div_send,' ' send_date,f.name name1,b.recieve_date,  "
        sql &= " case when '" & id & "'='0'  then '(ทั้งหมด)' else ' ('+f1.name+')' end name2 "
        sql &= " from BOOKIN_DATA b inner join FULLNAME f "
        sql &= " on b.creation_by=f.empid inner join FULLNAME f1 "
        sql &= " on b.sendto2=f1.empid  "
        sql &= " where b.active =1 "
        sql &= " and b.sendto2 is not null"


        If t1 <> "" And t2 <> "" Then
            sql &= " and convert(nvarchar(10),b.recieve_date,120) between convert(nvarchar(10),'" & t1 & "',120) and convert(nvarchar(10),'" & t2 & "',120) "
        End If
        If id <> "0" Then
            sql &= " and b.sendto2='" & id & "' "
        End If

        Conn.ConnectionString = MD.strConnIMP
        Conn.Open()

        Dim DT As New DataTable("tempReport")
        DT.Columns.Add("bookin_no", GetType(String))
        DT.Columns.Add("stamp_date", GetType(Date))
        DT.Columns.Add("topic", GetType(String))
        DT.Columns.Add("from_name", GetType(String))
        DT.Columns.Add("div_regis", GetType(String))
        DT.Columns.Add("book_regis", GetType(String))

        DT.Columns.Add("no_regis", GetType(String))
        DT.Columns.Add("date_regis", GetType(String))
        DT.Columns.Add("div_send", GetType(String))
        DT.Columns.Add("send_date", GetType(String))
        DT.Columns.Add("name1", GetType(String))
        DT.Columns.Add("recieve_date", GetType(Date))
        DT.Columns.Add("name2", GetType(String))

        DT.Columns.Add("t1", GetType(String))
        DT.Columns.Add("t2", GetType(String))


        DSreport.Tables.Add(DT)

        Dim SqlAdapter As New SqlClient.SqlDataAdapter(sql, Conn)
        SqlReader = SqlAdapter.SelectCommand.ExecuteReader

        If SqlReader.HasRows Then

            While SqlReader.Read
                Dim DR As DataRow = DSreport.Tables("tempReport").NewRow

                ' Add ข้อมูลที่อ่านจาก SQL Base ใส่เข้าไปแต่ละ Rows ของ Temp Table

                DR.Item("bookin_no") = SqlReader.Item("bookin_no")
                DR.Item("stamp_date") = SqlReader.Item("stamp_date")
                DR.Item("topic") = SqlReader.Item("topic")
                DR.Item("from_name") = SqlReader.Item("from_name")
                DR.Item("div_regis") = SqlReader.Item("div_regis")
                DR.Item("book_regis") = SqlReader.Item("book_regis")

                DR.Item("no_regis") = SqlReader.Item("no_regis")
                DR.Item("date_regis") = SqlReader.Item("date_regis")
                DR.Item("div_send") = SqlReader.Item("div_send")
                DR.Item("send_date") = SqlReader.Item("send_date")
                DR.Item("name1") = SqlReader.Item("name1")
                DR.Item("recieve_date") = SqlReader.Item("recieve_date")
                DR.Item("name2") = SqlReader.Item("name2")



                If t1 <> "" And t2 <> "" Then
                    DR.Item("t1") = DateTime.Parse(t1).ToString("dd/MM/yyyy")
                    DR.Item("t2") = DateTime.Parse(t2).ToString("dd/MM/yyyy")
                Else
                    DR.Item("t1") = "ไม่ระบุ"
                    DR.Item("t2") = "ไม่ระบุ"
                End If



                DSreport.Tables("tempReport").Rows.Add(DR) 'Add Row เข้าไปใน Temp Table

            End While

            SqlReader.Close()
            Conn.Close()

            crReportDocument.ResourceName = "../Report/ReportBookIn.rpt" 'Set Report Name
            Dim DTpic As Data.DataTable = DSreport.Tables("tempReport")
            DVpic = DTpic.DefaultView

            crReportDocument.SetDataSource(DT)
            CrystalReportViewer1.ReportSource = crReportDocument
            CrystalReportViewer1.RefreshReport()

        End If
    End Sub
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub
    Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
        crReportDocument.Close()
        crReportDocument.Dispose()
    End Sub
End Class