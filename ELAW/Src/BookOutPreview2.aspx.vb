Imports System.IO
Imports System.Data
Imports System.Data.OleDb

Partial Class Src_BookOutPreview2
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
    Private Sub ChkPermis()
        'ตรวจสอบสิทธิ์การใช้งาน
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

        'Me.ChkPermis()

        If Not Page.IsPostBack Then

            If X <> "" Then
                'Preview, Approve, Edit
                Dim sql As String

                sql = " select b.bookout_id,b.system_no,b.bookout_no,b.bookkind_id,k.bookkind_name,b.present,  "
                sql &= "b.message,b.postscript,b.postname,b.post_pos,b.comment,b.contact,  "
                sql &= "b.status_id,s.status_name,b.topic,b.keyword,b.dates,e.firstname+' '+e.lastname createname, "
                sql &= "b.user_sign,e1.firstname+' '+e1.lastname signname,b.priority_id,p.priority_name,b.sendto,f.short_name sendname, "
                sql &= "b.sendto1,b.sendto_app,b.sendto_comment,b.sendto1_app,b.sendto_comment1,t.type_name,b.ref_title,b.ref_id,b.ref_type,b.booktype_id,right(b.runno,4) runno "
                sql &= "from bookout_data b inner join book_kind k "
                sql &= "on b.bookkind_id=k.bookkind_id inner join book_status s "
                sql &= "on b.status_id=s.status_id inner join employee e "
                sql &= "on b.creation_by=e.empid left join employee e1 "
                sql &= "on b.user_sign=e1.empid inner join book_priority p "
                sql &= "on b.priority_id=p.priority_id left join fullname f "
                sql &= "on b.sendto=f.empid inner join bookout_type t "
                sql &= "on b.booktype_id=t.type_id "
                sql &= "where b.active=1 "
                sql &= "and b.bookout_id='" & X & "'"

                DS = MD.GetDataset(sql)
                Session("BookOutPreviewData") = DS
                iRec = 0
                ViewState("iRec") = iRec

                lblId.Text = X

                Me.MyDataBind()

                Me.gDataDoc()
                Me.MyGridBind()

            Else
                'Add New
                Dim sql As String

                sql = "select * from bookout_data "

                DS = MD.GetDataset(sql)
                Session("BookOutPreviewData") = DS
                iRec = 0
                ViewState("iRec") = iRec


                lblId.Text = ""
                lblMainStatus.Text = "Add"

            End If

        Else

            DS = Session("BookOutPreviewData")
            iRec = ViewState("iRec")

        End If

        If DS.Tables(0).Rows(0).Item("ref_type").ToString() = "1" Then
            LinkDetail.Text = "<a href=""javascript:openwindow('" + "PrintLaw" + "','" + DS.Tables(0).Rows(0).Item("ref_id").ToString + "','" + "');"">" + "พิมพ์รายละเอียด" + "</a>"
        ElseIf DS.Tables(0).Rows(0).Item("ref_type").ToString() = "2" Then
            LinkDetail.Text = "<a href=""javascript:openwindow('" + "PrintCase" + "','" + DS.Tables(0).Rows(0).Item("ref_id").ToString + "','" + "');"">" + "พิมพ์รายละเอียด" + "</a>"
        ElseIf DS.Tables(0).Rows(0).Item("ref_type").ToString() = "3" Then
            LinkDetail.Text = "<a href=""javascript:openwindow('" + "PrintContract" + "','" + DS.Tables(0).Rows(0).Item("ref_id").ToString + "','" + "');"">" + "ดูรายละเอียด" + "</a>"
        Else
            LinkDetail.Visible = False
        End If

        If DS.Tables(0).Rows(0).Item("booktype_id").ToString() = "1" Then
            lblPrint.Text = "<a href=""javascript:openwindow('" + "BookOutPreviewIn" + "','" + X + "','" + "');"">" + "พิมพ์หนังสือนำส่ง" + "</a>" 'BookOutPreviewIn
        Else
            lblPrint.Text = "<a href=""javascript:openwindow('" + "BookOutPreview" + "','" + X + "','" + "');"">" + "พิมพ์หนังสือนำส่ง" + "</a>"
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

            Case Else
                Return DT.Rows(iRec)(FieldName) & ""
        End Select
    End Function
    Public Sub MyDataBind()

        lblName.DataBind()
        lblStatus.DataBind()
        lblStatusId.DataBind()
        lblDate.DataBind()
        lblCreateName.DataBind()
        lblType.DataBind()
        lblPriority.DataBind()
        lblTopic.DataBind()
        lblPresent.DataBind()
        lblMessage.DataBind()
        lblPostScript.DataBind()
        lblPostName.DataBind()
        lblPostPosition.DataBind()
        lblContact.DataBind()
        lblComment.DataBind()
        lblKeyword.DataBind()
        lblApp1Comment.DataBind()
        lblSendto.DataBind()

        If DS.Tables(0).Rows(0).Item("sendto_app").ToString = "T" Then
            lblApp1.Text = "ผ่าน"
        ElseIf DS.Tables(0).Rows(0).Item("sendto_app").ToString = "F" Then
            lblApp1.Text = "แก้ไข"
        ElseIf DS.Tables(0).Rows(0).Item("sendto_app").ToString = "N" Then
            lblApp1.Text = "ส่งต่อ"
        Else
            lblApp1.Text = ""
        End If

        If DS.Tables(0).Rows(0).Item("sendto1_app").ToString = "T" Then
            lblApp2.Text = "ผ่าน"
        ElseIf DS.Tables(0).Rows(0).Item("sendto1_app").ToString = "F" Then
            lblApp2.Text = "แก้ไข"
        ElseIf DS.Tables(0).Rows(0).Item("sendto_app").ToString = "N" Then
            lblApp1.Text = "ส่งต่อ"
        Else
            lblApp2.Text = ""
        End If


        lblBookType.DataBind()
        lblRefTitle.DataBind()
        lblRefId.DataBind()
        lblRefType.DataBind()

        lblApp2Comment.DataBind()
        lblRno.DataBind()
    End Sub
    Protected Sub linkHome_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles linkHome.Click
        Response.Redirect(MD.pHome, True)
    End Sub
    Protected Sub link2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles link2.Click
        Response.Redirect("../Src/BookOutDataList.aspx", True)
    End Sub
    Private Sub gDataDoc(Optional ByVal Type As String = "")
        'Data in Gridview (Table BOOKOUT_DOCUMENT)
        Dim X As String = Request.QueryString("id")
        Dim strsql As String

        strsql = " select b.document_id,b.title,b.page  "
        strsql &= "from bookout_document b "
        strsql &= "where b.system_no='" & DS.Tables(0).Rows(0).Item("system_no").ToString & "'"

        Dim DT As DataTable = MD.GetDataTable(strsql)
        DVLst = DT.DefaultView
        Session("DocumentBookOut") = DVLst

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
                'Dim L1 As ImageButton = e.Row.Cells(3).Controls(0)
                'L1.OnClientClick = "return confirm('คุณต้องการลบข้อมูลใช่หรือไม่?');"
            End If
        End If
    End Sub
    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        'Download File Document
        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim K2 As String
            K2 = Convert.ToString(GridView1.DataKeys(e.Row.RowIndex).Values(0).ToString())
            Dim strsql2 As String = ""

            strsql2 = "select d.bookout_id,d.document_id, "
            strsql2 &= "d.file_path,d.mime_type "
            strsql2 &= "from bookout_document d "
            strsql2 &= "where d.system_no='" & DS.Tables(0).Rows(0).Item("system_no").ToString & "' AND d.document_id='" & K2 & "'"


            Dim dt As DataTable
            dt = MD.GetDataTable(strsql2)

            Dim lblLink As Label = e.Row.Cells(2).FindControl("lblLink")

            For Each dr As DataRow In dt.Rows

                If dr("mime_type").ToString() = ".pdf" Or dr("mime_type").ToString() = ".txt" Then
                    Dim strUrl As String = "http://" & Constant.BaseURL(Request) & "" & dr("file_path").ToString() & ""

                    lblLink.Text = "<a href='" & strUrl & "?time=" & Date.Now.ToString("HH:mm:ss") & "' Target='_Blank'>ดาวน์โหลด</a>&nbsp;&nbsp;"

                Else

                    Dim strUrl As String = "http://" & Constant.BaseURL(Request) & dr("file_path").ToString()
                    lblLink.Text = "<a href='" & strUrl & "?time=" & Date.Now.ToString("HH:mm:ss") & "' Target='_Blank'>ดาวน์โหลด</a>&nbsp;&nbsp;"

                End If

            Next

        End If

    End Sub
    Protected Sub lnkPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkPrint.Click
        Dim X As String = Request.QueryString("id")

        If DS.Tables(0).Rows(0).Item("booktype_id").ToString() = "1" Then
            MC.OpenWindow(Me, "../Src/BookInCrystal.aspx?id=" & X & "")
        Else
            MC.OpenWindow(Me, "../Src/BookInCrystal.aspx?id='" & X & "'")
        End If

    End Sub
    
End Class

