Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine
Imports System.Data.OleDb
Imports System.IO
Imports System.Data.DataSet
Imports System.Data
Partial Class Src_CaseReportPreview
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

        Dim type As String = Request.QueryString("type")
        Dim t1 As String = Request.QueryString("t1")
        Dim t2 As String = Request.QueryString("t2")
        Dim DSreport As New DataSet()
        Dim SqlReader As SqlClient.SqlDataReader
        Dim Conn As New SqlClient.SqlConnection()

        Dim sql As String


        sql = "select d.case_id ,d.case_no,d.red_no ,d.black_no ,d.defendant ,d.defendant1,"
        sql &= " d.prosecutor ,d.prosecutor1 ,e.firstname +' '+e.lastname createname,d.recieve_date ,"
        sql &= " s.status_name,"
        sql &= " case when " & type & "='0'  then 'ทั้งหมด'else t.type_name end type_name "
        sql &= " from CASE_DATA d inner join CASE_STATUS s"
        sql &= " on d.status_id =s.status_id inner join CASE_TYPE t"
        sql &= " on d.type_id =t.type_id inner join EMPLOYEE e"
        sql &= " on d.creation_by=e.empid "
        sql &= " and d.active =1"


        If t1 <> "" And t2 <> "" Then
            sql &= " and convert(nvarchar(10),d.recieve_date,120) between convert(nvarchar(10),'" & t1 & "',120) and convert(nvarchar(10),'" & t2 & "',120) "
        End If
        If type <> 0 Then
            sql &= " and d.type_id='" & type & "' "
        End If

        Conn.ConnectionString = MD.strConnIMP
        Conn.Open()

        Dim DT As New DataTable("tempReport")
        DT.Columns.Add("black_no", GetType(String))
        DT.Columns.Add("red_no", GetType(String))
        DT.Columns.Add("defendant", GetType(String))
        DT.Columns.Add("defendant1", GetType(String))
        DT.Columns.Add("prosecutor", GetType(String))
        DT.Columns.Add("prosecutor1", GetType(String))
        DT.Columns.Add("type_name", GetType(String))
        DT.Columns.Add("status_name", GetType(String))
        DT.Columns.Add("recieve_date", GetType(Date))
        DT.Columns.Add("createname", GetType(String))
        DT.Columns.Add("t1", GetType(String))
        DT.Columns.Add("t2", GetType(String))

        DSreport.Tables.Add(DT)

        Dim SqlAdapter As New SqlClient.SqlDataAdapter(sql, Conn)
        SqlReader = SqlAdapter.SelectCommand.ExecuteReader

        If SqlReader.HasRows Then

            While SqlReader.Read
                Dim DR As DataRow = DSreport.Tables("tempReport").NewRow

                ' Add ข้อมูลที่อ่านจาก SQL Base ใส่เข้าไปแต่ละ Rows ของ Temp Table

                DR.Item("black_no") = SqlReader.Item("black_no")
                DR.Item("red_no") = SqlReader.Item("red_no")
                DR.Item("defendant") = SqlReader.Item("defendant")
                DR.Item("defendant1") = SqlReader.Item("defendant1")
                DR.Item("prosecutor") = SqlReader.Item("prosecutor")
                DR.Item("prosecutor1") = SqlReader.Item("prosecutor1")
                DR.Item("type_name") = SqlReader.Item("type_name")
                DR.Item("status_name") = SqlReader.Item("status_name")
                DR.Item("recieve_date") = SqlReader.Item("recieve_date")
                DR.Item("createname") = SqlReader.Item("createname")

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

            crReportDocument.ResourceName = "../Report/ReportCase.rpt" 'Set Report Name
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