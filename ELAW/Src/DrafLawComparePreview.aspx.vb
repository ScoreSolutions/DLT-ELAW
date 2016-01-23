Imports System.Data
Partial Class Src_DrafLawComparePreview
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            SetLeftData(Request.QueryString("LawIdLeft"))
            SetRightData(Request.QueryString("LawIdRight"))
        End If
    End Sub

    Private Sub SetLeftData(ByVal LawIdLeft As String)
        Dim dt As DataTable = GetLawData(LawIdLeft)
        If dt.Rows.Count > 0 Then
            Dim dr As DataRow = dt.Rows(0)
            lblLeftLawID.Text = LawIdLeft
            lblLeftLawType.Text = dr("type_name").ToString()
            lblLeftLawSubType.Text = dr("subtype_name").ToString()
            lblLeftStatus.Text = dr("status_name").ToString()
            lblLeftMessage.Text = dr("message").ToString()
            lblLeftCreateDate.Text = CDate(dr("created_date")).ToString("dd/MM/yyyy")
            lblLeftUpdateDate.Text = CDate(dr("update_date")).ToString("dd/MM/yyyy")
            CtlLeftPrintLaw.LawID = LawIdLeft
        End If

    End Sub
    Private Sub SetRightData(ByVal LawIdRight As String)
        Dim dt As DataTable = GetLawData(LawIdRight)
        If dt.Rows.Count > 0 Then
            Dim dr As DataRow = dt.Rows(0)
            lblRightLawID.Text = LawIdRight
            lblRightLawType.Text = dr("type_name").ToString()
            lblRightLawSubType.Text = dr("subtype_name").ToString()
            lblRightStatus.Text = dr("status_name").ToString()
            lblRightMessage.Text = dr("message").ToString()
            lblRightCreateDate.Text = CDate(dr("created_date")).ToString("dd/MM/yyyy")
            lblRightUpdateDate.Text = CDate(dr("update_date")).ToString("dd/MM/yyyy")
            ctlRightPrintLaw.LawID = LawIdRight
        End If
    End Sub

    Private Function GetLawData(ByVal LawID As String) As DataTable
        Dim sql As String = ""
        sql += " select ld.law_id, lt.type_name, ls.subtype_name, s.status_name, ld.message, "
        sql += " lld.created_date ,ISNULL(ld.updated_date,ld.created_date) update_date"
        sql += " from law_data ld "
        sql += " inner join law_subtype ls on ls.subtype_id=ld.subtype_id"
        sql += " inner join law_type lt on lt.type_id=ls.type_id"
        sql += " inner join law_status s on s.status_id=ld.status_id"
        sql += " inner join (select l.law_id,l.ref_id,l.created_date" & vbNewLine
        sql += " 	        from LAW_DATA l" & vbNewLine
        sql += " 	        where  l.law_id=l.ref_id ) lld on lld.law_id=ld.ref_id" & vbNewLine
        sql += " where ld.law_id = '" & LawID & "'"

        Dim MD As New MainData
        Return MD.GetDataTable(sql)
    End Function
End Class
