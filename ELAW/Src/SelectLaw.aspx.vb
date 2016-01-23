Imports System.IO
Imports System.Data
Imports System.Data.OleDb
Partial Class Src_SelectLaw
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
    Dim DVLstSelect As DataView
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim X As String = Request.QueryString("id")

        If Not Page.IsPostBack Then
            ViewState("sortfield") = "doc_id"
            ViewState("sortdirection") = "desc"

            Me.DataType()
            Me.gData()
            Me.MyGridBind()

        Else
            If Session("SelectDoc") Is Nothing Then
                Me.gData()
            Else
                DVLst = Session("SelectDoc")
            End If
        End If

    End Sub
    Public Sub DataType()

        Dim strsql As String
        strsql = "select subtype_id,subtype_name from law_subtype  order by subtype_name  "

        Dim DTS As DataTable = MD.GetDataTable(strsql)
        Dim dr As DataRow = DTS.NewRow
        dr!subtype_id = 0
        dr!subtype_name = "---------โปรดเลือก---------"
        DTS.Rows.InsertAt(dr, 0)
        ddlType.DataTextField = "subtype_name"
        ddlType.DataValueField = "subtype_id"
        ddlType.DataSource = DTS
        ddlType.DataBind()

    End Sub
    Private Sub gData()


        Dim strsql As String = ""
        If txtKeyword.Text <> "" Then
            strsql = " select d.doc_id,d.doc_name,s.subtype_name "
            strsql &= "from import_document d inner join law_subtype s "
            strsql &= "on d.doc_subtype=s.subtype_id inner join import_keywords k "
            strsql &= "on d.doc_id=k.doc_id "
            strsql &= "where d.doc_type=1 "
            strsql &= "and k.keyword like '%" & txtKeyword.Text & "%' "

            If ddlType.SelectedValue <> "0" Then
                strsql &= "and d.doc_subtype='" & ddlType.SelectedValue & "' "
            End If
            If txtTitle.Text <> "" Then
                strsql &= "and d.doc_name like '%" & txtTitle.Text & "%' "
            End If

            strsql &= " group by d.doc_id,d.doc_name,s.subtype_name   "
        Else
            strsql = " select d.doc_id,d.doc_name,s.subtype_name "
            strsql &= "from import_document d inner join law_subtype s "
            strsql &= "on d.doc_subtype=s.subtype_id "
            strsql &= "where d.doc_type=1 "

            If ddlType.SelectedValue <> "0" Then
                strsql &= "and d.doc_subtype='" & ddlType.SelectedValue & "' "
            End If
            If txtTitle.Text <> "" Then
                strsql &= "and d.doc_name like '%" & txtTitle.Text & "%' "
            End If
        End If



        Dim DT As DataTable = MD.GetDataTable(strsql)
        DVLst = DT.DefaultView
        DVLst.Sort = "[" & ViewState("sortfield") & "] " & ViewState("sortdirection")
        Session("SelectDoc") = DVLst

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

    Public Sub cb1_Checked(ByVal sender As Object, ByVal e As EventArgs)
        Dim cb1 As CheckBox = sender
        Dim dgi As GridViewRow
        Dim cb2 As CheckBox
        For Each dgi In GridView1.Rows
            cb2 = dgi.Cells(1).FindControl("cb1")
            cb2.Checked = cb1.Checked
        Next
    End Sub

    Protected Sub bSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bSave.Click
        Me.gData()
        Me.MyGridBind()
    End Sub

    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim K1 As String
            K1 = Convert.ToString(GridView1.DataKeys(e.Row.RowIndex).Values(0).ToString())
            Dim strsql2 As String = ""


            strsql2 = "select d.doc_id,d.doc_name, "
            strsql2 &= "k.keyword,d.file_path,d.mime_type "
            strsql2 &= "from import_document d left join import_keywords k "
            strsql2 &= "on d.doc_id=k.doc_id  "
            strsql2 &= "where d.doc_id='" & K1 & "' "

            If txtKeyword.Text <> "" Then
                strsql2 &= "and k.keyword like '%" & txtKeyword.Text & "%' "
            End If


            If txtKeyword.Text <> "" Then

                Dim dt As DataTable
                dt = MD.GetDataTable(strsql2)

                Dim lblLinkDoc As Label = e.Row.Cells(2).FindControl("lblLinkDoc")
                Dim lblLink As Label = e.Row.Cells(3).FindControl("lblLink")

                For Each dr As DataRow In dt.Rows


                    If dr("mime_type").ToString() = ".pdf" Then
                        Dim strUrl As String = "../" & "" & dr("file_path").ToString() & "" & "?#search=" & "" & dr("keyword").ToString() & ""
                        Dim strUrlFile As String = "../" & "" & dr("file_path").ToString() & ""

                        lblLink.Text += "<a href='" & strUrl & "' Target='_Blank'>" & dr("keyword").ToString() & "</a>&nbsp;&nbsp;"
                        lblLinkDoc.Text = "<a href='" & strUrlFile & "' Target='_Blank'>" & dr("doc_name").ToString() & "</a>&nbsp;&nbsp;"

                    Else

                        Dim strUrl As String = "../" & dr("file_path").ToString()
                        lblLink.Text += "<a href=" & strUrl & ">" & dr("keyword").ToString() & "</a>&nbsp;&nbsp;"
                        lblLinkDoc.Text = "<a href=" & strUrl & ">" & dr("doc_name").ToString() & "</a>&nbsp;&nbsp;"

                    End If

                Next

            Else

                Dim dt As DataTable
                dt = MD.GetDataTable(strsql2)

                Dim lblLinkDoc As Label = e.Row.Cells(2).FindControl("lblLinkDoc")
                Dim lblLink As Label = e.Row.Cells(3).FindControl("lblLink")

                For Each dr As DataRow In dt.Rows


                    If dr("mime_type").ToString() = ".pdf" Then
                        Dim strUrlFile As String = "../" & "" & dr("file_path").ToString() & ""
                        lblLinkDoc.Text = "<a href='" & strUrlFile & "' Target='_Blank'>" & dr("doc_name").ToString() & "</a>&nbsp;&nbsp;"

                    Else

                        Dim strUrl As String = "../" & dr("file_path").ToString()
                        lblLinkDoc.Text = "<a href=" & strUrl & ">" & dr("doc_name").ToString() & "</a>&nbsp;&nbsp;"

                    End If

                Next

            End If
        End If

    End Sub
    Dim aspsrv As New clsSystemConfig
    Protected Sub bSelect_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bSelect.Click
        Dim X As String = Request.QueryString("id")
        Dim sEmpNo As String = Session("EmpNo")
        Dim S1 As New System.Text.StringBuilder("")
        Dim MD As New MainData
        Dim strSql As String = ""
        Dim oDs As New Data.DataSet
        Dim sData As String = ""

        Try
            For Each dgi As GridViewRow In GridView1.Rows
                Dim cb As CheckBox = dgi.Cells(1).FindControl("cb1")
                If cb.Checked = True Then

                    Dim K1 As DataKey = GridView1.DataKeys(dgi.RowIndex)
                    Dim index As Integer = Convert.ToInt32(dgi.RowIndex)
                    Dim row As GridViewRow = GridView1.Rows(index)
                    Dim item As New ListItem()

                    item.Text = Server.HtmlDecode(row.Cells(1).Text) 'item

                    S1.Append(K1(0))
                    Dim Vkey As String = K1.Value
                    If S1.Length > 0 Then 'Then S1.Append(",'")
                        Try
                            strSql = "insert into link_detail (link_id,doc_id, "
                            strSql &= " creation_by,created_date,updated_by,updated_date )"
                            strSql &= " values (?,?,?,?,?,?) "

                            cn = New OleDbConnection(MD.Strcon)
                            cmd = New OleDbCommand(strSql, cn)
                            MD.CreateParam(cmd, "TTTDTD")

                            cmd.Parameters("@P1").Value = X
                            cmd.Parameters("@P2").Value = K1(0).ToString
                            cmd.Parameters("@P3").Value = sEmpNo
                            cmd.Parameters("@P4").Value = DateTime.Parse(Date.Now)
                            cmd.Parameters("@P5").Value = sEmpNo
                            cmd.Parameters("@P6").Value = DateTime.Parse(Date.Now)

                            cn.Open()
                            cmd.ExecuteNonQuery()
                        Catch ex As Exception

                        End Try



                    End If

                End If

            Next

            Session("ChkGrd") = "1"

            Response.Write(aspsrv.RefreshPage("aspnetForm"))
            Response.Write("<script language=JavaScript>window.close()</script>")

        Catch ex As Exception
            Dim ses As String = System.Web.HttpContext.Current.Session.SessionID
            Dim url As String = HttpContext.Current.Request.FilePath
            Dim browser As String = " Type:" + Request.Browser.Type + " Version:" + Request.Browser.Version + " Browser:" + Request.Browser.Browser

            MC.MessageBox(Me, ex.ToString)
            MC.ErrLog(url, ses, ex.ToString, sEmpNo, browser)

        Finally
            cn.Close()
        End Try


    End Sub


End Class
