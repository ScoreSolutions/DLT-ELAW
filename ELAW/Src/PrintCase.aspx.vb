Imports System.IO
Imports System.Data
Imports System.Data.OleDb
Partial Class Src_PrintCase
    Inherits System.Web.UI.Page
    Dim MD As New MainData
    Dim MC As New MainClass
    Dim DV As DataView
    Dim oDate As New clsDate
    Public iRow As Integer
    Dim DS As DataSet
    Dim iRec As Integer
    Dim DVLst As DataView
    Dim DVCourt As DataView
    Dim DVExplain As DataView
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim X As String = Request.QueryString("id")
        Dim status As String = Request.QueryString("status")
        Dim sEmpNo As String = Session("EmpNo")

        If Not Page.IsPostBack Then

            If X <> "" Then
                'Preview, Approve, Edit
                Dim sql As String

                sql = "select c.case_id,c.black_no,c.red_no,c.recieve_date,c.defendant,c.defendant1,c.prosecutor,c.prosecutor1,c.keyword,c.detail, "
                sql &= " c.status_id,s.status_name,c.type_id,t.type_name,c.court_id,co.court_name,a.attorney_id,a.attorney_name,a.tel  , "
                sql &= " c.app1,e.firstname+' '+e.lastname appname1,c.app2,e1.firstname+' '+e1.lastname appname2,c.recieve_type,c.case_no, "
                sql &= " c.app1_app,c.app1_comment,c.app2_app,c.app2_comment "
                sql &= " from case_data c inner join case_status s "
                sql &= " on c.status_id=s.status_id inner join case_type t "
                sql &= " on c.type_id=t.type_id inner join court co "
                sql &= " on c.court_id=co.court_id inner join attorney a "
                sql &= " on c.attorney_id=a.attorney_id left join employee e "
                sql &= " on c.app1=e.empid left join employee e1 "
                sql &= " on c.app2=e1.empid "
                sql &= " where c.case_id ='" & X & "'"


                DS = MD.GetDataset(sql)
                Session("pcase_data") = DS
                iRec = 0
                ViewState("iRec") = iRec

                Me.MyDataBind()

            Else
                'Add New
                Dim sql As String

                sql = "select c.case_id,c.black_no,c.red_no,c.recieve_date,c.defendant,c.defendant1,c.prosecutor,c.prosecutor1,c.keyword,c.detail, "
                sql &= " c.status_id,s.status_name,c.type_id,t.type_name,c.court_id,co.court_name,a.attorney_id,a.attorney_name+' : '+a.tel attorney_name , "
                sql &= " c.app1,e.firstname+' '+e.lastname appname1,c.app2,e1.firstname+' '+e1.lastname appname2,c.recieve_type,c.case_no, "
                sql &= " c.app1_app,c.app1_comment,c.app2_app,c.app2_comment "
                sql &= " from case_data c inner join case_status s "
                sql &= " on c.status_id=s.status_id inner join case_type t "
                sql &= " on c.type_id=t.type_id inner join court co "
                sql &= " on c.court_id=co.court_id inner join attorney a "
                sql &= " on c.attorney_id=a.attorney_id left join employee e "
                sql &= " on c.app1=e.empid left join employee e1 "
                sql &= " on c.app2=e1.empid "

                DS = MD.GetDataset(sql)
                Session("pcase_data") = DS
                iRec = 0
                ViewState("iRec") = iRec



            End If

        Else

            DS = Session("pcase_data")
            iRec = ViewState("iRec")

        End If



    End Sub
    Public Function BindField(ByVal FieldName As String) As String
        'BindField when Preview,Approve,Edit
        Dim DT As DataTable = DS.Tables(0)
        Select Case FieldName
            Case "money"
                If IsDBNull(DT.Rows(iRec)("money")) Then
                    Return "0.00"

                Else
                    Dim P1 As Double = DT.Rows(iRec)("money")
                    Return P1.ToString("#,##0.00")
                End If
            Case "recieve_date"
                If IsDBNull(DT.Rows(iRec)("recieve_date")) Then
                    Return ""

                Else
                    Dim P1 As Date = DT.Rows(iRec)("recieve_date")
                    Return P1.ToString("dd/MM/yyyy")
                End If

            Case Else
                Return DT.Rows(iRec)(FieldName) & ""
        End Select
    End Function
    Public Sub MyDataBind()
        'Databind at Control
        lblId.DataBind()
        lblBlackNo.DataBind()
        lblRecieveDate.DataBind()
        lblRedNo.DataBind()
        lblDefandent.DataBind()
        lblDefandent1.DataBind()
        lblProsecutor.DataBind()
        lblProsecutor1.DataBind()
        lblKeyword.DataBind()

        lblApp1.DataBind()
        lblApp2.DataBind()
        lblAttornney.DataBind()
        lblCourt.DataBind()
        lblStatus.DataBind()
        lblCaseNo.DataBind()
        lblStatusId.DataBind()
        lblCaseType.DataBind()
        lblTel.DataBind()

        If DS.Tables(0).Rows(0).Item("recieve_type").ToString = 0 Then
            lblCloseRecieve.Text = "ปิดหมาย"
        ElseIf DS.Tables(0).Rows(0).Item("recieve_type").ToString = 1 Then
            lblCloseRecieve.Text = "รับหมาย"
        End If

        lblDetail.DataBind()

    End Sub

    Protected Sub ImageButton1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click

        ImageButton1.Visible = False
        Response.Write("<script language=javascript>")
        Response.Write("{print();}")
        Response.Write("</script>")

    End Sub
End Class
