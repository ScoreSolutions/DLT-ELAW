Imports System.Data
Imports System.Data.OleDb
Partial Class Src_ShowAlert
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
                sql &= " where c.case_no ='" & X & "'"
                sql &= " and c.active=1 "


                DS = MD.GetDataset(sql)
                Session("AlertPopup") = DS
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
                sql &= " and c.active=1 "

                DS = MD.GetDataset(sql)
                Session("AlertPopup") = DS
                iRec = 0
                ViewState("iRec") = iRec

            End If

            Me.gAlert()
            Me.MyGridBind()

        Else


            DS = Session("AlertPopup")
            iRec = ViewState("iRec")

            If Session("ShowAlert") Is Nothing Then
                Me.gAlert()
            Else
                DVLst = Session("ShowAlert")
            End If

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
        lblBlackNo.DataBind()
        lblRedNo.DataBind()
        lblCaseType.DataBind()
        lblStatus.DataBind()
        lblAttornney.DataBind()
        lblDefandent.DataBind()
        lblDefandent1.DataBind()
        lblProsecutor.DataBind()
        lblProsecutor1.DataBind()
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
        'Print Document
        ImageButton1.Visible = False
        Response.Write("<script language=javascript>")
        Response.Write("{print();}")
        Response.Write("</script>")
    End Sub
    Private Sub gAlert(Optional ByVal Type As String = "")
        'Data in Gridview (Table BOOKIN_DOCUMENT)
        Dim X As String = Request.QueryString("id")
        Dim strsql As String

        strsql = "select c.case_id,c.case_no,c.dates,c.title   "
        strsql &= "from court_date c  "
        strsql &= "where  convert(nvarchar(10),c.dates,120) <=  "
        strsql &= "convert(nvarchar(10),getdate()-(select chk_date from check_alert where chk_id=1),120) "
        strsql &= "and c.alert=1 "
        strsql &= "and case_no='" & X & "'"
        strsql &= "union "
        strsql &= "select e.case_id ,e.case_no,e.dates,e.title  "
        strsql &= "from explain_date e "
        strsql &= "where  convert(nvarchar(10),e.dates,120) <= "
        strsql &= "convert(nvarchar(10),getdate()-(select chk_date from check_alert where chk_id=2),120) "
        strsql &= "and e.alert=1 "
        strsql &= "and case_no='" & X & "'"
        strsql &= "order by dates "

        Dim DT As DataTable = MD.GetDataTable(strsql)
        DVLst = DT.DefaultView
        Session("ShowAlert") = DVLst

    End Sub
    Private Sub MyGridBind()
        GridView1.DataSource = DVLst
        Dim X1() As String = {"case_id"}
        GridView1.DataKeyNames = X1
        GridView1.DataBind()
    End Sub
    Protected Sub GridView1_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridView1.PageIndexChanging
        GridView1.PageIndex = e.NewPageIndex
        Me.MyGridBind()
    End Sub
    Private Sub GoPage(ByVal xPage As Integer)
        GridView1.PageIndex = xPage
        Me.MyGridBind()
    End Sub
    Private Sub FirstClick(ByVal sender As Object, ByVal e As EventArgs)
        Me.GoPage(0)
    End Sub
    Private Sub PrevClick(ByVal sender As Object, ByVal e As EventArgs)
        Me.GoPage(GridView1.PageIndex - 1)
    End Sub
    Private Sub NextClick(ByVal sender As Object, ByVal e As EventArgs)
        Me.GoPage(GridView1.PageIndex + 1)
    End Sub
    Private Sub LastClick(ByVal sender As Object, ByVal e As EventArgs)
        Me.GoPage(GridView1.PageCount - 1)
    End Sub
    Protected Sub GridView1_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles GridView1.Sorting
        If ViewState("sortfield") = e.SortExpression Then
            If ViewState("sortdirection") = "asc" Then ViewState("sortdirection") = "desc" Else ViewState("sortdirection") = "asc"
        Else
            ViewState("sortfield") = e.SortExpression
            ViewState("sortdirection") = "asc"
        End If
        DVLst.Sort = "[" & ViewState("sortfield") & "] " & ViewState("sortdirection")
        Me.MyGridBind()
    End Sub
    Protected Sub GridView1_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowCreated
        'Create Page Gridview
        If e.Row.RowType = DataControlRowType.Pager Then
            Dim td As TableCell = e.Row.Cells(0)
            Dim Y As Boolean = False
            If GridView1.PageIndex > 0 Then
                Dim L1 As New LinkButton
                L1.ForeColor = Drawing.Color.Black
                L1.Text = "First"
                td.Controls.Add(L1)
                AddHandler L1.Click, AddressOf FirstClick

                Y = True

                Dim L2 As New Literal
                L2.Text = " &nbsp;&nbsp;"
                td.Controls.Add(L2)
                td.HorizontalAlign = HorizontalAlign.Left

                L1 = New LinkButton
                L1.ForeColor = Drawing.Color.Black
                L1.Text = "Prev"
                AddHandler L1.Click, AddressOf PrevClick
                td.Controls.Add(L1)
                td.HorizontalAlign = HorizontalAlign.Left

            End If
            If GridView1.PageIndex < GridView1.PageCount - 1 Then
                Dim L2 As Literal

                If Y = True Then
                    L2 = New Literal
                    L2.Text = " &nbsp;&nbsp;"
                    td.Controls.Add(L2)
                End If

                Dim L1 As New LinkButton
                L1.ForeColor = Drawing.Color.Black
                L1.Text = "Next"
                AddHandler L1.Click, AddressOf NextClick
                td.Controls.Add(L1)

                L2 = New Literal
                L2.Text = " &nbsp;&nbsp;"
                td.Controls.Add(L2)
                td.HorizontalAlign = HorizontalAlign.Left

                L1 = New LinkButton
                L1.ForeColor = Drawing.Color.Black
                L1.Text = "Last"
                AddHandler L1.Click, AddressOf LastClick
                td.Controls.Add(L1)
                td.HorizontalAlign = HorizontalAlign.Left
            End If
        End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            iRow = e.Row.DataItemIndex + 1
            If Not e.Row.RowState And DataControlRowState.Edit Then
            End If
        End If


    End Sub
    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound

    End Sub
End Class

