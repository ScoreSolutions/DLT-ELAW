Imports System.IO
Imports System.Data
Imports System.Data.OleDb
Partial Class Src_LawDetail2
    Inherits System.Web.UI.Page
    Dim cn As OleDbConnection
    Dim cmd As OleDbCommand
    Dim MD As New MainData
    Dim MC As New MainClass
    Dim DV As DataView
    Dim oDate As New clsDate
    Public iRow As Integer
    Dim DS As DataSet
    Dim iRec As Integer
    Dim DVLst As DataView
    Dim DVLst2 As DataView
    Dim DVLstSelect As DataView
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            'ViewState("sortfield") = "subtype_name"
            'ViewState("sortdirection") = "desc"

            Me.gData()
            Me.MyGridBind()

        Else
            If Session("DocDetail2") Is Nothing Then
                Me.gData()
            Else
                DVLst = Session("DocDetail2")
            End If
        End If

        'Me.LoadLaw()

    End Sub
    Private Sub LoadLaw()
        Dim X As String = Request.QueryString("id")
        Dim strsql As String = ""

        strsql = "select d.doc_id,d.doc_name,case when d.cancel=1 then 'ยกเลิกเอกสาร('+d.cancel_comment+')' else '' end cancel, "
        strsql &= "d.file_path,d.mime_type "
        strsql &= "from import_document d "
        strsql &= "where d.doc_id='" & X & "' and d.active=1 "

        Dim dt As DataTable
        dt = MD.GetDataTable(strsql)

        For Each dr As DataRow In dt.Rows


            If dr("mime_type").ToString() = ".pdf" Then
                Dim strUrlFile As String = "../" & "" & dr("file_path").ToString() & ""
            Else
                Dim strUrl As String = "http://" & Constant.BaseURL(Request) & dr("file_path").ToString()
            End If

        Next
    End Sub
    Private Sub gData()
        Dim X As String = Request.QueryString("id")
        Dim strsql As String = ""

       
        strsql = " select im.doc_id,v.name subtype_name,"
        strsql &= " case when im.cancel=1 then im.doc_name+' (ยกเลิก : '+im.cancel_comment+')' else im.doc_name end doc_name"
        strsql &= " from import_document im"
        strsql &= " inner join DOCUMENT_TYPE t"
        strsql &= " on im.doc_type=t.doc_id "
        strsql &= " inner join import_document_subtype v"
        strsql &= " on im.doc_subtype=v.id and v.tbl=t.ref_table"

        strsql &= " where im.doc_id in"
        strsql &= " (select doc_id"
        strsql &= " from link_detail "
        strsql &= " where link_id in"
        strsql &= " (select link_id from LINKLAW_DATA where title  ='" & X & "') union select '" & X & "' )"

        strsql &= " and im.secret = 0 And im.active = 1"


        'strsql &= " and d.link_id =(select link_id from LINKLAW_DATA  where title ='" & X & "')"


        Dim DT As DataTable = MD.GetDataTable(strsql)
        DVLst = DT.DefaultView
        'DVLst.Sort = "[" & ViewState("sortfield") & "] " & ViewState("sortdirection")
        Session("DocDetail2") = DVLst

    End Sub
    Private Sub MyGridBind()
        GridView1.DataSource = DVLst
        Dim X1() As String = {"doc_id"}
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
                'Dim L1 As ImageButton = e.Row.Cells(2).Controls(1)
                'L1.OnClientClick = "return confirm('คุณต้องการลบข้อมูลใช่หรือไม่?');"
            End If
        End If


    End Sub
    Protected Sub GridView1_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles GridView1.RowEditing
        Dim K1 As DataKey = GridView1.DataKeys(e.NewEditIndex)
        MC.OpenWindow(Me, "../Src/LawDetail2.aspx?id=" & K1(0) & "&status=preview&menu=1")
    End Sub

    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim K2 As String
            K2 = Convert.ToString(GridView1.DataKeys(e.Row.RowIndex).Values(0).ToString())
            Dim X As String = Request.QueryString("id")
            Dim strsql As String = ""

            strsql = "select d.doc_id, "
            strsql &= "case when d.cancel=1 then d.doc_name+' (ยกเลิก : '+d.cancel_comment+')' else d.doc_name end doc_name,"
            strsql &= "d.file_path,d.mime_type "
            strsql &= "from import_document d "
            strsql &= "where d.doc_id='" & K2 & "' and d.active=1 "

            Dim dt As DataTable
            dt = MD.GetDataTable(strsql)

            Dim lblLinkFile As Label = e.Row.Cells(1).FindControl("lblLinkFile")
            Dim lblLinkDownload As Label = e.Row.Cells(2).FindControl("lblLinkDownload")

            ' MC.OpenWindow(Me, "../Src/LawDetail.aspx?id=" & K1(0) & "&status=preview&menu=1")
            For Each dr As DataRow In dt.Rows


                If dr("mime_type").ToString() = ".pdf" Then

                    Dim strUrlFile As String = "http://" & Constant.BaseURL(Request) & dr("file_path").ToString() & ""
                    lblLinkFile.Text = "<a href='" & strUrlFile & "?time=" & Date.Now.ToString("HH:mm:ss") & "' Target='_Blank'>" & dr("doc_name").ToString() & " </a>&nbsp;&nbsp;"
                    lblLinkDownload.Text = "<a href='" & strUrlFile & "?time=" & Date.Now.ToString("HH:mm:ss") & "' Target='_Blank'><img src='../Images/PDFIcon.png' border=0></a>&nbsp;&nbsp;"

                ElseIf dr("mime_type").ToString() = ".doc" Or dr("mime_type").ToString() = ".docx" Then

                    Dim strUrl As String = "http://" & Constant.BaseURL(Request) & dr("file_path").ToString()
                    lblLinkFile.Text = "<a href='" & strUrl & "?time=" & Date.Now.ToString("HH:mm:ss") & "' Target='_Blank'>" & dr("doc_name").ToString() & "</a>&nbsp;&nbsp;"
                    lblLinkDownload.Text = "<a href='" & strUrl & "?time=" & Date.Now.ToString("HH:mm:ss") & "' Target='_Blank'><img src='../Images/WORDicon.gif' border=0></a>&nbsp;&nbsp;"

                ElseIf dr("mime_type").ToString() = ".xls" Or dr("mime_type").ToString() = ".xlsx" Then

                    Dim strUrl As String = "http://" & Constant.BaseURL(Request) & dr("file_path").ToString()
                    lblLinkFile.Text = "<a href='" & strUrl & "?time=" & Date.Now.ToString("HH:mm:ss") & "' Target='_Blank'>" & dr("doc_name").ToString() & "</a>&nbsp;&nbsp;"
                    lblLinkDownload.Text = "<a href='" & strUrl & "?time=" & Date.Now.ToString("HH:mm:ss") & "' Target='_Blank'><img src='../Images/ExcelIcon.gif' border=0></a>&nbsp;&nbsp;"

                ElseIf dr("mime_type").ToString() = ".txt" Then

                    Dim strUrl As String = "http://" & Constant.BaseURL(Request) & dr("file_path").ToString()
                    lblLinkFile.Text = "<a href='" & strUrl & "?time=" & Date.Now.ToString("HH:mm:ss") & "' Target='_Blank'>" & dr("doc_name").ToString() & "</a>&nbsp;&nbsp;"
                    lblLinkDownload.Text = "<a href='" & strUrl & "?time=" & Date.Now.ToString("HH:mm:ss") & "' Target='_Blank'><img src='../Images/txt-icon.gif' border=0></a>&nbsp;&nbsp;"

                End If

            Next

        End If
    End Sub

End Class
