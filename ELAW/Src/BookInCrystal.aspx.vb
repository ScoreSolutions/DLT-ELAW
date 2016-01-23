Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine
Partial Class Src_BookInCrystal
    Inherits System.Web.UI.Page
    Dim crReportDocument As Report = New Report
#Region " Web Form Designer Generated Code "
    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init

        Dim Main As New MainData

        Dim id As String = Request.QueryString("id")

        InitializeComponent()

        Dim crReportDocument As Report = New Report '//Set Report Name
        crReportDocument.ResourceName = "../Report/BookInCrystal.rpt" '//Set Report Name

        'crReportDocument.OpenSubreport("SubSum")
        'crReportDocument.OpenSubreport("SubReportOT2.rpt")

        crReportDocument.SetDatabaseLogon(Main.RId, Main.RPas, Main.RServer, Main.RDb)

        For Each tab As Table In crReportDocument.Database.Tables

            Dim logonInfo As TableLogOnInfo = tab.LogOnInfo

            logonInfo.ConnectionInfo.UserID = Main.RId

            logonInfo.ConnectionInfo.Password = Main.RPas

            logonInfo.ConnectionInfo.ServerName = Main.RServer

            logonInfo.ConnectionInfo.DatabaseName = Main.RDb

            tab.ApplyLogOnInfo(logonInfo)

        Next

        CrystalReportViewer1.DisplayGroupTree = False

        CrystalReportViewer1.ReportSource = crReportDocument


        crReportDocument.SetParameterValue("id", id)


        Dim RangePram1 As New ParameterRangeValue


    End Sub
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
        crReportDocument.Close()
        crReportDocument.Dispose()
    End Sub
End Class
