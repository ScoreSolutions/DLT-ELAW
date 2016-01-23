Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine
Imports System.Data.OleDb
Imports System.IO
Imports System.Data.DataSet
Imports System.Data
Partial Class Src_LawReportPreview
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

        Dim name As String = Request.QueryString("name")
        Dim div As String = Request.QueryString("div")
        Dim t1 As String = Request.QueryString("t1")
        Dim t2 As String = Request.QueryString("t2")
        Dim DSreport As New DataSet()
        Dim SqlReader As SqlClient.SqlDataReader
        Dim Conn As New SqlClient.SqlConnection()

        Dim sql As String


        sql = "select j.recieve_date,l.title,l.ref_id,r.creation_by,   "
        sql &= " case when '" & name & "' <> '0' then f.name else '(ทั้งหมด)' end name,"
        sql &= " case when r.div<>'0'  then d.div_name else '(ทั้งหมด)' end div "
        sql &= " from law_data l"
        sql &= " left join "
        sql &= " (select j.ref_bookin,b.recieve_date ,j.id "
        sql &= " from JOB_BOOKIN j inner join BOOKIN_DATA b "
        sql &= " on j.ref_bookin =b.bookin_id  "
        sql &= " and j.type_id =1)j "
        sql &= " on l.law_id =j.id inner join  "
        sql &= " (select l.ref_id,l.creation_by,'" & div & "' div "
        sql &= " from law_data l "
        sql &= " where l.created_date in"
        sql &= " (select r.dates from"
        sql &= " (select ref_id,MIN(created_date) dates  "
        sql &= " from LAW_DATA "
        sql &= " group by ref_id)r))r "
        sql &= " on l.ref_id=r.ref_id "
        sql &= " inner join FULLNAME f "
        sql &= " on r.creation_by=f.empid left join division d "
        sql &= " on r.div=d.div_id "
        sql &= " where l.active = 1 "



        If t1 <> "" And t2 <> "" Then
            sql &= " and convert(nvarchar(10),j.recieve_date,120) between convert(nvarchar(10),'" & t1 & "',120) and convert(nvarchar(10),'" & t2 & "',120) "
        End If
        If div <> "0" Then
            sql &= " and f.div_id ='" & div & "' "
        End If
        If name <> "0" Then
            sql &= " and r.creation_by ='" & name & "' "
        End If

        Conn.ConnectionString = MD.strConnIMP
        Conn.Open()

        Dim DT As New DataTable("tempReport")
        DT.Columns.Add("title", GetType(String))
        DT.Columns.Add("recieve_date", GetType(Date))
        DT.Columns.Add("name", GetType(String))
        DT.Columns.Add("div", GetType(String))
        DT.Columns.Add("t1", GetType(String))
        DT.Columns.Add("t2", GetType(String))

        DSreport.Tables.Add(DT)

        Dim SqlAdapter As New SqlClient.SqlDataAdapter(sql, Conn)
        SqlReader = SqlAdapter.SelectCommand.ExecuteReader

        If SqlReader.HasRows Then

            While SqlReader.Read
                Dim DR As DataRow = DSreport.Tables("tempReport").NewRow

                ' Add ข้อมูลที่อ่านจาก SQL Base ใส่เข้าไปแต่ละ Rows ของ Temp Table

                DR.Item("title") = SqlReader.Item("title")
                DR.Item("recieve_date") = SqlReader.Item("recieve_date")
                DR.Item("name") = SqlReader.Item("name")
                DR.Item("div") = SqlReader.Item("div")


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

            crReportDocument.ResourceName = "../Report/ReportLaw.rpt" 'Set Report Name
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