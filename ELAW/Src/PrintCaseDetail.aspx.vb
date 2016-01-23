Imports System.Data
Imports System.Data.OleDb
Partial Class Src_PrintCaseDetail
    Inherits System.Web.UI.Page
    Dim cn As OleDbConnection
    Dim cmd As OleDbCommand
    Dim MD As New MainData
    Dim MC As New MainClass
    Dim DV As DataView
    Dim DVLst As DataView
    Public iRow As Integer
    Dim DS As DataSet
    Dim iRec As Integer
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim X As String = Request.QueryString("id")
        Dim status As String = Request.QueryString("status")

        If Not Page.IsPostBack Then

            If X <> "" Then
                'Preview, Approve, Edit
                Dim sql As String

                sql = " select * from case_data  "
                sql &= "where case_id='" & X & "'"

                DS = MD.GetDataset(sql)
                Session("CaseDataPrint") = DS
                iRec = 0
                ViewState("iRec") = iRec


                Me.MyDataBind()

            Else
                'Add New
                Dim sql As String

                sql = "select * from case_data "

                DS = MD.GetDataset(sql)
                Session("CaseDataPrint") = DS
                iRec = 0
                ViewState("iRec") = iRec

            End If

        Else

            DS = Session("CaseDataPrint")
            iRec = ViewState("iRec")

        End If

    End Sub
    Public Function BindField(ByVal FieldName As String) As String
        'BindField when Preview,Approve,Edit
        Dim DT As DataTable = DS.Tables(0)
        Select Case FieldName
            Case "cost"
                If IsDBNull(DT.Rows(iRec)("cost")) Then
                    Return "0.00"
                Else
                    Dim P1 As Double = DT.Rows(iRec)("cost")
                    Return P1.ToString("#,##0.00")
                End If
            Case "dates"
                If IsDBNull(DT.Rows(iRec)("dates")) Then
                    Return ""
                Else
                    Dim P1 As Date = DT.Rows(iRec)("dates")
                    Return P1.ToString("dd/MM/yyyy")
                End If
            Case "sendto_date"
                If IsDBNull(DT.Rows(iRec)("sendto_date")) Then
                    Return ""
                Else
                    Dim P1 As Date = DT.Rows(iRec)("sendto_date")
                    Return P1.ToString("dd MMM yyyy")
                End If
            Case "sendto1_date"
                If IsDBNull(DT.Rows(iRec)("sendto1_date")) Then
                    Return ""
                Else
                    Dim P1 As Date = DT.Rows(iRec)("sendto1_date")
                    Return P1.ToString("dd MMM yyyy")
                End If

            Case Else
                Return DT.Rows(iRec)(FieldName) & ""
        End Select
    End Function
    Public Sub MyDataBind()

        lblDetail.DataBind()
      
    End Sub
    Protected Sub ImageButton1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
        'Print Document
        ImageButton1.Visible = False
        Response.Write("<script language=javascript>")
        Response.Write("{print();}")
        Response.Write("</script>")
    End Sub
    Function MonthThai(ByVal MonthThaiExIndex As String) As String
        Dim strMonthThai As String = ""
        Select Case MonthThaiExIndex
            Case "01"
                strMonthThai = "มกราคม"
            Case "02"
                strMonthThai = "กุมภาพันธ์"
            Case "03"
                strMonthThai = "มีนาคม"
            Case "04"
                strMonthThai = "เมษายน"
            Case "05"
                strMonthThai = "พฤษภาคม"
            Case "06"
                strMonthThai = "มิถุนายน"
            Case "07"
                strMonthThai = "กรกฎาคม"
            Case "08"
                strMonthThai = "สิงหาคม"
            Case "09"
                strMonthThai = "กันยายน"
            Case "10"
                strMonthThai = "ตุลาคม"
            Case "11"
                strMonthThai = "พฤศจิกายน"
            Case "12"
                strMonthThai = "ธันวาคม"
        End Select
        Return strMonthThai

    End Function
End Class
