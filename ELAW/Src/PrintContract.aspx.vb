Imports System.Data
Imports System.Data.OleDb
Partial Class Src_PrintContract
    Inherits System.Web.UI.Page
    Dim cn As OleDbConnection
    Dim cmd As OleDbCommand
    Dim MD As New MainData
    Dim MC As New MainClass
    Dim DV As DataView
    Public iRow As Integer
    Dim DS As DataSet
    Dim iRec As Integer
    Dim DVLst As DataView
    Private Sub ChkPermis()
        Dim sEmpNo As String = Session("EmpNo")
        Dim url As String = HttpContext.Current.Request.FilePath
        If sEmpNo = "" Then
            Response.Redirect(MD.pLogin, True)
        Else
            Dim chk As Boolean = MC.ChkPermission(sEmpNo, url)
            If chk = False Then
                Response.Redirect(MD.pNoAut, True)
            End If
        End If
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim X As String = Request.QueryString("id")
        Dim status As String = Request.QueryString("status")


        If Not Page.IsPostBack Then

            If X <> "" Then
                'Preview, Approve, Edit
                Dim sql As String

                sql = "select d.contract_id,d.contract_no,s.type_id,d.subtype_id, "
                sql &= " d.process_id,d.status_id,d.dates_recieve,d.dates_contract,d.dates_comesign,d.dates_sign, "
                sql &= " d.material,d.user_sale,d.tax_id,d.keyword,d.witness1,d.message,e1.firstname+' '+e1.lastname name1,d.witness1_comment, "
                sql &= " d.witness2,e2.firstname+' '+e2.lastname name2,d.witness2_comment, "
                sql &= " d.user_buy,e3.firstname+' '+e3.lastname name3, "
                sql &= " s1.subtype_name,p.process_name,s2.status_name, "
                sql &= " d.witness1_app,d.witness2_app,d.guarantee_id,g.guarantee_name,d.guarantee_no,d.dates_start,d.dates_finish ,d.money"
                sql &= " from contract_data d inner join contract_subtype s"
                sql &= " on d.subtype_id=s.subtype_id left join employee e1 "
                sql &= " on d.witness1=e1.empid left join employee e2 "
                sql &= " on d.witness2=e2.empid left join employee e3 "
                sql &= " on d.user_buy=e3.empid inner join contract_subtype s1 "
                sql &= " on d.subtype_id=s1.subtype_id inner join contract_process p "
                sql &= " on d.process_id=p.process_id inner join contract_status s2 "
                sql &= " on d.status_id=s2.status_id left join guarantee g "
                sql &= " on d.guarantee_id=g.guarantee_id "
                sql &= " where d.contract_id='" & X & "'"


                DS = MD.GetDataset(sql)
                Session("PrintContract") = DS
                iRec = 0
                ViewState("iRec") = iRec


                Me.MyDataBind()

            Else
                'Add New
                Dim sql As String

                sql = "select d.contract_id,d.contract_no,s.type_id,d.subtype_id, "
                sql &= " d.process_id,d.status_id,d.dates_recieve,d.dates_contract,d.dates_comesign,d.dates_sign, "
                sql &= " d.material,d.user_sale,d.tax_id,d.keyword,d.witness1,d.message,e1.firstname+' '+e1.lastname name1,d.witness1_comment, "
                sql &= " d.witness2,e2.firstname+' '+e2.lastname name2,d.witness2_comment, "
                sql &= " d.user_buy,e3.firstname+' '+e3.lastname name3, "
                sql &= " s1.subtype_name,p.process_name,s2.status_name, "
                sql &= " d.witness1_app,d.witness2_app,d.guarantee_id,g.guarantee_name,d.guarantee_no,d.dates_start,d.dates_finish ,d.money"
                sql &= " from contract_data d inner join contract_subtype s"
                sql &= " on d.subtype_id=s.subtype_id left join employee e1 "
                sql &= " on d.witness1=e1.empid left join employee e2 "
                sql &= " on d.witness2=e2.empid left join employee e3 "
                sql &= " on d.user_buy=e3.empid inner join contract_subtype s1 "
                sql &= " on d.subtype_id=s1.subtype_id inner join contract_process p "
                sql &= " on d.process_id=p.process_id inner join contract_status s2 "
                sql &= " on d.status_id=s2.status_id left join guarantee g "
                sql &= " on d.guarantee_id=g.guarantee_id "


                DS = MD.GetDataset(sql)
                Session("PrintContract") = DS
                iRec = 0
                ViewState("iRec") = iRec




            End If


        Else


            DS = Session("PrintContract")
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
            Case "recieve_date"
                If IsDBNull(DT.Rows(iRec)("recieve_date")) Then
                    Return ""
                Else
                    Dim P1 As Date = DT.Rows(iRec)("recieve_date")
                    Return P1.ToString("dd/MM/yyyy HH:mm")
                End If
            Case "stamp_date"
                If IsDBNull(DT.Rows(iRec)("stamp_date")) Then
                    Return ""
                Else
                    Dim P1 As Date = DT.Rows(iRec)("stamp_date")
                    Return P1.ToString("dd/MM/yyyy")
                End If
            Case Else
                Return DT.Rows(iRec)(FieldName) & ""
        End Select
    End Function
    Public Sub MyDataBind()
        lblNo.DataBind()
        lblSubType.DataBind()
        lblDetail.DataBind()


        'Dim mnt As String = MonthThai(Right(Left(lblStampDate.Text, 5), 2))
        'lblStampDateThai.Text = Left(lblStampDate.Text, 2) + " " + mnt + " " + Right(lblStampDate.Text, 4)

        'Dim mnt2 As String = MonthThai(Right(Left(lblRecieveDate.Text, 5), 2))
        'lblRecieveDateThai.Text = Left(lblRecieveDate.Text, 2) + " " + mnt + " " + Right(lblRecieveDate.Text, 10)


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
    Protected Sub ImageButton1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
        ImageButton1.Visible = False
        Response.Write("<script language=javascript>")
        Response.Write("{print();}")
        Response.Write("</script>")
    End Sub

    
End Class
