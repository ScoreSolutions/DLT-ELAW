Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine
Imports System.Data.OleDb
Imports System.IO
Imports System.Data.DataSet
Imports System.Data
Partial Class Src_BookOutInReportPreview
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


        sql = "select b.bookout_id ,b.bookout_no,b.run_date,b.topic ,t.type_name, "
        sql &= " 'สำนักกฎหมาย' from_div,b.present,  "
        sql &= " case when '" & id & "'='0'  then '(ทั้งหมด)' else '( '+f.name+' )' end fullname "
        sql &= " from BOOKOUT_DATA b inner join BOOKOUT_TYPE t "
        sql &= " on b.booktype_id =t.type_id inner join fullname f  "
        sql &= " on b.creation_by=f.empid "
        sql &= " where b.active =1 "
        sql &= " and b.booktype_id =1 "
        sql &= " and b.status_id = 10 "

     


        If t1 <> "" And t2 <> "" Then
            sql &= " and convert(nvarchar(10),b.run_date,120) between convert(nvarchar(10),'" & t1 & "',120) and convert(nvarchar(10),'" & t2 & "',120) "
        End If
        If id <> "0" Then
            sql &= " and b.creation_by='" & id & "' "
        End If

        Conn.ConnectionString = MD.strConnIMP
        Conn.Open()

        Dim DT As New DataTable("tempReport")
        DT.Columns.Add("bookout_no", GetType(String))
        DT.Columns.Add("run_date", GetType(Date))
        DT.Columns.Add("topic", GetType(String))
        DT.Columns.Add("type_name", GetType(String))
        DT.Columns.Add("from_div", GetType(String))
        DT.Columns.Add("present", GetType(String))
        DT.Columns.Add("t1", GetType(String))
        DT.Columns.Add("t2", GetType(String))
        DT.Columns.Add("fullname", GetType(String))

        DSreport.Tables.Add(DT)

        Dim SqlAdapter As New SqlClient.SqlDataAdapter(sql, Conn)
        SqlReader = SqlAdapter.SelectCommand.ExecuteReader

        If SqlReader.HasRows Then

            While SqlReader.Read
                Dim DR As DataRow = DSreport.Tables("tempReport").NewRow

                ' Add ข้อมูลที่อ่านจาก SQL Base ใส่เข้าไปแต่ละ Rows ของ Temp Table

                DR.Item("bookout_no") = SqlReader.Item("bookout_no")
                DR.Item("run_date") = SqlReader.Item("run_date")
                DR.Item("topic") = SqlReader.Item("topic")
                DR.Item("type_name") = SqlReader.Item("type_name")
                DR.Item("from_div") = SqlReader.Item("from_div")
                DR.Item("present") = SqlReader.Item("present")

                If t1 <> "" And t2 <> "" Then
                    DR.Item("t1") = DateTime.Parse(t1).ToString("dd/MM/yyyy")
                    DR.Item("t2") = DateTime.Parse(t2).ToString("dd/MM/yyyy")
                Else
                    DR.Item("t1") = "ไม่ระบุ"
                    DR.Item("t2") = "ไม่ระบุ"
                End If

                DR.Item("fullname") = SqlReader.Item("fullname")

                DSreport.Tables("tempReport").Rows.Add(DR) 'Add Row เข้าไปใน Temp Table

            End While

            SqlReader.Close()
            Conn.Close()

            crReportDocument.ResourceName = "../Report/ReportBookOut_In.rpt" 'Set Report Name
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