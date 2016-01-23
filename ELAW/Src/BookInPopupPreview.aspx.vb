Imports System.Data
Imports System.Data.OleDb
Partial Class Src_BookInPopupPreview
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

                sql = " select b.bookin_id,b.system_no,b.bookin_no,b.bookkind_id,k.bookkind_name,  "
                sql &= "b.status_id,s.status_name,b.topic,b.keyword,b.recieve_date,b.stamp_date,b.from_name, "
                sql &= "b.sendto,b.present,b.priority_id,f.name creation_name,f1.name send_name ,f1.short_name pos, "
                sql &= "p.priority_name,b.sendto_comment1,b.sendto_date1,b.sendto_comment2,b.sendto_date2, "
                sql &= "f2.name send_name2,f2.short_name pos2,f3.name send_name1 ,f3.short_name pos1 "
                sql &= "from bookin_data b inner join book_kind k "
                sql &= "on b.bookkind_id=k.bookkind_id inner join book_status s "
                sql &= "on b.status_id=s.status_id left join fullname f "
                sql &= "on b.creation_by=f.empid left join fullname f1  "
                sql &= "on b.sendto=f1.empid inner join book_priority p "
                sql &= "on b.priority_id=p.priority_id left join fullname f2 "
                sql &= "on b.sendto2=f2.empid left join fullname f3 "
                sql &= " on b.sendto1=f3.empid "
                sql &= " where b.bookin_id='" & X & "'"


                DS = MD.GetDataset(sql)
                Session("BookInPreviewPopup") = DS
                iRec = 0
                ViewState("iRec") = iRec


                Me.MyDataBind()

            Else
                'Add New
                Dim sql As String

                sql = " select b.bookin_id,b.system_no,b.bookin_no,b.bookkind_id,k.bookkind_name,  "
                sql &= "b.status_id,s.status_name,b.topic,b.keyword,b.recieve_date,b.stamp_date,b.from_name, "
                sql &= "b.sendto,b.present,b.priority_id,e.firstname+' '+e.lastname creation_name,e1.firstname+' '+e1.lastname send_name,"
                sql &= "p.priority_name "
                sql &= "from bookin_data b inner join book_kind k "
                sql &= "on b.bookkind_id=k.bookkind_id inner join book_status s "
                sql &= "on b.status_id=s.status_id inner join employee e "
                sql &= "on b.creation_by=e.empid inner join employee e1  "
                sql &= "on b.sendto=e1.empid inner join book_priority p "
                sql &= "on b.priority_id=p.priority_id "

                DS = MD.GetDataset(sql)
                Session("BookInPreviewPopup") = DS
                iRec = 0
                ViewState("iRec") = iRec




            End If

            Me.gDataDoc()
            Me.MyGridBind()

        Else


            DS = Session("BookInPreviewPopup")
            iRec = ViewState("iRec")

            If Session("DocumentBookIn") Is Nothing Then
                Me.gDataDoc()
            Else
                DVLst = Session("DocumentBookIn")
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
            Case "sendto_date1"
                If IsDBNull(DT.Rows(iRec)("sendto_date1")) Then
                    Return ""
                Else
                    Dim P1 As Date = DT.Rows(iRec)("sendto_date1")
                    Return P1.ToString("dd MMM yyyy")
                End If
            Case "sendto_date2"
                If IsDBNull(DT.Rows(iRec)("sendto_date2")) Then
                    Return ""
                Else
                    Dim P1 As Date = DT.Rows(iRec)("sendto_date2")
                    Return P1.ToString("dd MMM yyyy")
                End If
            Case Else
                Return DT.Rows(iRec)(FieldName) & ""
        End Select
    End Function
    Public Sub MyDataBind()
        lblSysNo.DataBind()
        lblBookNo.DataBind()
        lblBookType.DataBind()
        lblCreate.DataBind()
        lblFrom.DataBind()
        lblKeyword.DataBind()
        lblPresent.DataBind()
        lblPriority.DataBind()
        lblRecieveDate.DataBind()
        lblSendTo.DataBind()
        lblStampDate.DataBind()
        lblStatus.DataBind()
        lblTopic.DataBind()

        Dim mnt As String = MonthThai(Right(Left(lblStampDate.Text, 5), 2))
        lblStampDateThai.Text = Left(lblStampDate.Text, 2) + " " + mnt + " " + Right(lblStampDate.Text, 4)

        Dim mnt2 As String = MonthThai(Right(Left(lblRecieveDate.Text, 5), 2))
        lblRecieveDateThai.Text = Left(lblRecieveDate.Text, 2) + " " + mnt + " " + Right(lblRecieveDate.Text, 10)

        lblComment1.DataBind()
        lblSendTo1.DataBind()
        lblSendToDate1.DataBind()

        lblComment2.DataBind()
        lblSendTo2.DataBind()
        lblSendToDate2.DataBind()
        lblSendToLeader.DataBind()
        lblPos.DataBind()
        lblPos1.DataBind()
        lblSendPosition.DataBind()
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
    Private Sub gDataDoc(Optional ByVal Type As String = "")
        'Data in Gridview (Table BOOKIN_DOCUMENT)
        Dim X As String = Request.QueryString("id")
        Dim strsql As String

        strsql = " select d.document_id,d.title,d.page  "
        strsql &= "from bookin_document d "
        strsql &= "where d.system_no='" & lblSysNo.Text & "'"

        Dim DT As DataTable = MD.GetDataTable(strsql)
        DVLst = DT.DefaultView
        'DVLst.Sort = "[" & ViewState("sortfield") & "] " & ViewState("sortdirection")
        Session("DocumentBookIn") = DVLst

    End Sub
    Private Sub MyGridBind()
        GridView1.DataSource = DVLst
        Dim X1() As String = {"document_id"}
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
                'Dim L1 As ImageButton = e.Row.Cells(3).Controls(1)
                'L1.OnClientClick = "return confirm('คุณต้องการลบข้อมูลใช่หรือไม่?');"
            End If
        End If


    End Sub

    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        'Download File Document
        Dim X As String = Request.QueryString("id")
        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim K2 As String
            K2 = Convert.ToString(GridView1.DataKeys(e.Row.RowIndex).Values(0).ToString())
            Dim strsql2 As String = ""


            strsql2 = "select d.bookin_id,d.document_id, "
            strsql2 &= "d.file_path,d.mime_type "
            strsql2 &= "from bookin_document d "
            strsql2 &= "where system_no='" & lblSysNo.Text & "' and d.document_id='" & K2(0) & "'"


            Dim dt As DataTable
            dt = MD.GetDataTable(strsql2)

            Dim lblLink As Label = e.Row.Cells(2).FindControl("lblLink")

            For Each dr As DataRow In dt.Rows


                If dr("mime_type").ToString() = ".pdf" Or dr("mime_type").ToString() = ".txt" Then
                    Dim strUrl As String = "http://" & Constant.BaseURL(Request) & dr("file_path").ToString() & ""

                    lblLink.Text = "<a href='" & strUrl & "?time=" & Date.Now.ToString("HH:mm:ss") & "' Target='_Blank'>ดาวน์โหลด</a>&nbsp;&nbsp;"

                Else

                    Dim strUrl As String = "http://" & Constant.BaseURL(Request) & dr("file_path").ToString()
                    lblLink.Text = "<a href='" & strUrl & "?time=" & Date.Now.ToString("HH:mm:ss") & "' Target='_Blank'>ดาวน์โหลด</a>&nbsp;&nbsp;"

                End If

            Next

        End If

    End Sub
End Class
