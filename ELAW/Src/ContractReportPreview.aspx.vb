Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine
Imports System.Data.OleDb
Imports System.IO
Imports System.Data.DataSet
Imports System.Data
Partial Class Src_ContractReportPreview
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
        Dim emp As String = Request.QueryString("empid")
        Dim DSreport As New DataSet()
        Dim SqlReader As SqlClient.SqlDataReader
        Dim Conn As New SqlClient.SqlConnection()

        Dim sql As String

        sql = " select c.contract_id,c.contract_no,c.subtype_id ,c.contract_name ,c.dates_recieve ,c.creation_by,"
        sql &= " case when '" & type & "'='0' then 'ทั้งหมด'else s.subtype_name end type, "
        sql &= " c.witness1_date, "
        sql &= " case when '" & emp & "'='0' then 'ทั้งหมด'else f.name end name1 "
        sql &= " from "
        sql &= " (select contract_id,contract_name,subtype_id,creation_by,witness1_date,contract_no,dates_recieve from CONTRACT_DATA "
        sql &= " where active =1)c "
        sql &= " inner join CONTRACT_SUBTYPE s "
        sql &= " on c.subtype_id =s.subtype_id "
        sql &= " inner join fullname f "
        sql &= " on c.creation_by=f.empid "

        If t1 <> "" And t2 <> "" Then
            sql &= " and convert(nvarchar(10),c.dates_recieve,120) between convert(nvarchar(10),'" & t1 & "',120) and convert(nvarchar(10),'" & t2 & "',120) "
        End If
        If type <> "0" Then
            sql &= " and c.subtype_id='" & type & "' "
        End If
        If emp <> "0" Then
            sql &= " and c.creation_by='" & emp & "' "
        End If

        Conn.ConnectionString = MD.strConnIMP
        Conn.Open()

        Dim DT As New DataTable("tempReport")
        DT.Columns.Add("contract_no", GetType(String))
        DT.Columns.Add("contract_name", GetType(String))

        DT.Columns.Add("dates_recieve", GetType(Date))

        DT.Columns.Add("t1", GetType(String))
        DT.Columns.Add("t2", GetType(String))
        DT.Columns.Add("type", GetType(String))
        DT.Columns.Add("name1", GetType(String))

        DSreport.Tables.Add(DT)

        Dim SqlAdapter As New SqlClient.SqlDataAdapter(sql, Conn)
        SqlReader = SqlAdapter.SelectCommand.ExecuteReader

        If SqlReader.HasRows Then

            While SqlReader.Read
                Dim DR As DataRow = DSreport.Tables("tempReport").NewRow

                ' Add ข้อมูลที่อ่านจาก SQL Base ใส่เข้าไปแต่ละ Rows ของ Temp Table

                DR.Item("contract_no") = SqlReader.Item("contract_no")
                DR.Item("contract_name") = SqlReader.Item("contract_name")

                DR.Item("dates_recieve") = SqlReader.Item("dates_recieve")

                If t1 <> "" And t2 <> "" Then
                    DR.Item("t1") = DateTime.Parse(t1).ToString("dd/MM/yyyy")
                    DR.Item("t2") = DateTime.Parse(t2).ToString("dd/MM/yyyy")
                Else
                    DR.Item("t1") = "ไม่ระบุ"
                    DR.Item("t2") = "ไม่ระบุ"
                End If

                DR.Item("type") = SqlReader.Item("type")
                DR.Item("name1") = SqlReader.Item("name1")

                DSreport.Tables("tempReport").Rows.Add(DR) 'Add Row เข้าไปใน Temp Table

            End While

            SqlReader.Close()
            Conn.Close()

            crReportDocument.ResourceName = "../Report/ReportContract.rpt" 'Set Report Name
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