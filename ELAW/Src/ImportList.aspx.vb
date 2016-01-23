Imports System.IO
Imports System.Data
Imports System.Data.OleDb
Partial Class Src_ImportList
    Inherits System.Web.UI.Page
    Dim MD As New MainData
    Dim MC As New MainClass
    Dim DV As DataView
    Dim oDate As New clsDate
    Public iRow As Integer
    Dim DS As DataSet
    Dim iRec As Integer
    Dim DVLst As DataView
    Const menuID As Integer = Constant.MenuID.ImportList

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then

            Me.DocType()
            Me.DocSubType()
            FillCondition()
            Me.gData()
            Me.MyGridBind()

            Try
                Dim data As DataRow = Session(Constant.searchSession)
                If Session("txtPage") IsNot Nothing And data("menuID") = menuID Then
                    GridView1.PageIndex = Session("txtPage")
                End If
                Me.MyGridBind()
            Catch ex As Exception

            End Try

        Else
            If Session("ImportList") Is Nothing Then
                Me.gData()
            Else
                DVLst = Session("ImportList")
            End If
        End If

    End Sub
    Private Sub DocType()
        'ประเภทเอกสาร
        Dim strsql As String

        strsql = " select * from document_type  "
        strsql &= "order by doc_name "

        Dim DTS As DataTable = MD.GetDataTable(strsql)
        Dim dr As DataRow = DTS.NewRow
        dr!doc_id = 0
        dr!doc_name = "---โปรดเลือก---"
        DTS.Rows.InsertAt(dr, 0)
        DDType.DataTextField = "doc_name"
        DDType.DataValueField = "doc_id"
        DDType.DataSource = DTS
        DDType.DataBind()

    End Sub
    Private Sub DocSubType()
        'ชนิดเอกสาร
        Dim strsql As String
        Dim oDs As DataSet
        strsql = " select * from document_type where doc_id='" & DDType.SelectedValue & "' "
        oDs = MD.GetDataset(strsql)

        If oDs.Tables(0).Rows.Count > 0 Then
            Dim DTS As DataTable = MD.GetDataTable(oDs.Tables(0).Rows(0).Item("strsql").ToString)
            Dim dr As DataRow = DTS.NewRow
            dr!id = 0
            dr!name = "---โปรดเลือก---"
            DTS.Rows.InsertAt(dr, 0)
            DDLawType.DataTextField = "name"
            DDLawType.DataValueField = "id"
            DDLawType.DataSource = DTS
            DDLawType.DataBind()
        Else
            DDLawType.Items.Clear()
            DDLawType.Items.Add(New ListItem("---โปรดเลือก---", 0))
        End If


    End Sub
    Private Sub gData(Optional ByVal Type As String = "")
        'ดึงข้อมูลมาแสดงใน Gridview
        Dim strsql As String

        strsql = " select d.doc_id,d.doc_name,t.doc_name doc_type,v.name doc_typename,d.doc_type,v.id"
        strsql &= " from import_document d  "
        strsql &= " inner join DOCUMENT_TYPE t"
        strsql &= " on d.doc_type=t.doc_id  "

        strsql &= " left join import_keywords k"
        strsql &= " on d.doc_id=k.doc_id"

        strsql &= " inner join import_document_subtype v "
        strsql &= " on d.doc_subtype=v.id and v.tbl=t.ref_table "
        strsql &= " where d.active=1 "

        If DDType.SelectedIndex <> "0" Then
            strsql &= "and d.doc_type='" & DDType.SelectedValue & "' "
            If txtDocId.Text <> "" Then
                strsql &= "and d.doc_id like '%" & txtDocId.Text & "%' "

            End If
            If txtDocName.Text <> "" Then
                strsql &= "and (d.doc_name like '%" & txtDocName.Text & "%' "
                strsql &= setKeyWord(txtDocName.Text) & ")"
            End If
            If DDLawType.SelectedIndex <> "0" Then
                strsql &= "and d.doc_subtype='" & DDLawType.SelectedValue & "' "
                If txtDocId.Text <> "" Then
                    strsql &= "and d.doc_id like '%" & txtDocId.Text & "%' "

                End If

                If txtDocName.Text <> "" Then
                    strsql &= "and (d.doc_name like '%" & txtDocName.Text & "%' "
                    strsql &= setKeyWord(txtDocName.Text) & ")"
                End If
            End If

        End If

        If txtDocId.Text <> "" Then
            strsql &= "and d.doc_id like '%" & txtDocId.Text & "%' "
        End If

        If txtDocName.Text <> "" Then
            strsql &= "and (d.doc_name like '%" & txtDocName.Text & "%' "
            strsql &= setKeyWord(txtDocName.Text) & ")"
        End If



        strsql &= " group by d.doc_id,d.doc_name,t.doc_name,v.name,d.doc_type,v.id"
        strsql &= " order by d.doc_type,v.id,t.doc_name"

        Dim DT As DataTable = MD.GetDataTable(strsql)
        DVLst = DT.DefaultView
        Session("ImportList") = DVLst

    End Sub
    Public Function setKeyWord(ByVal txtKeyWord As String) As String
        'กำหนดคำค้นหา
        Dim ret As String = ""
        Dim sql As String = "select keyword from keyword_data order by keyword"
        Dim dt As DataTable = MD.GetDataTable(sql)

        Dim txtKey() As String = Split(txtKeyWord, " ")
        For i As Integer = 0 To txtKey.Length() - 1
            For Each dr As DataRow In dt.Rows
                Dim nWord As Integer = InStr(dr("keyword").ToString(), txtKey(i), CompareMethod.Text)
                If nWord > 0 Then
                    Dim whText As String = " k.keyword like '%" & txtKey(i) & "%' "
                    If ret = "" Then
                        ret = whText
                    Else
                        ret += " or " & whText
                    End If
                End If
            Next
        Next
        If ret <> "" Then
            ret = " or (" & ret & ")"
        End If

        Return ret
    End Function
    Private Sub MyGridBind()
        GridView1.DataSource = DVLst
        Dim X1() As String = {"doc_id"}
        GridView1.DataKeyNames = X1
        GridView1.DataBind()
    End Sub
    Private Sub AddNew()
        Dim dr As DataRow = DS.Tables(0).NewRow
        DS.Tables(0).Rows.Add(dr)
        iRec = DS.Tables(0).Rows.Count - 1
        ViewState("iRec") = iRec
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
                Dim L1 As ImageButton = e.Row.Cells(4).Controls(1)
                'ยืนยันการลบข้อมูล
                L1.OnClientClick = "return confirm('คุณต้องการลบข้อมูลใช่หรือไม่?');"
            End If
        End If
    End Sub
    Protected Sub GridView1_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GridView1.RowDeleting
        Dim sEmpNo As String = Session("EmpNo")
        Dim K1 As DataKey = GridView1.DataKeys(e.RowIndex)
        Dim strsql As String
        'กรณีลบข้อมูลให้ Update active=0
        strsql = "update import_document set active=0,updated_by='" & sEmpNo & "',updated_date=getdate() where doc_id='" & K1(0).ToString & "'"
        Dim Y As Integer = MD.Execute(strsql)
        If Y > 0 Then
            Me.gData()
            Me.MyGridBind()
            MC.MessageBox(Me, "ลบข้อมูลเรียบร้อยแล้ว")
        Else
            MC.MessageBox(Me, "ไม่สามารถลบข้อมูลได้")
        End If
    End Sub
    Protected Sub GridView1_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles GridView1.RowEditing
        'แก้ไขข้อมูล
        Dim K1 As DataKey = GridView1.DataKeys(e.NewEditIndex)
        SetSearchSession()
        Session("txtPage") = iRow / 10 - 1

        Response.Redirect("../Src/ImportDocument2.aspx?id=" & K1(0).ToString & "&menu=1", True)
    End Sub
    Protected Sub bSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bSearch.Click
        'ค้นหาข้อมูล
        If txtDocName.Text.Trim() <> "" Then
            SaveKeyword(txtDocName.Text.Trim())
        End If
        Me.gData()
        Me.MyGridBind()
        SetSearchSession()

        Response.Redirect("../Src/ImportList.aspx")
    End Sub
    Private Sub SaveKeyword(ByVal txtWord As String)
        'บันทึกคำค้นหาใน Temp
        Dim txtKey() As String = Split(txtWord, " ")
        For Each txt As String In txtKey
            Dim sql As String = ""
            sql += " select keyword "
            sql += " from keyword_data "
            sql += " where keyword = '" & txt & "'"
            Dim dt As DataTable = MD.GetDataTable(sql)

            If dt.Rows.Count = 0 Then
                Dim strsql As String
                strsql = "insert into keyword_data (keyword) values ('" & txt & "')"
                MD.Execute(strsql)
            End If
        Next
    End Sub
    Private Sub SetSearchSession()

        Dim dt As New DataTable
        dt.Columns.Add("menuID")
        dt.Columns.Add("DDType")
        dt.Columns.Add("DDLawType")
        dt.Columns.Add("txtDocId")
        dt.Columns.Add("txtDocName")



        Dim data As DataRow = dt.NewRow
        data("menuID") = menuID
        data("DDType") = DDType.SelectedValue
        data("DDLawType") = DDLawType.SelectedValue
        data("txtDocId") = txtDocId.Text
        data("txtDocName") = txtDocName.Text

        'Session("txtPage") = iRow / 10 - 1

    
        Session(Constant.searchSession) = data
    End Sub

    Private Sub FillCondition()
        If Session(Constant.searchSession) IsNot Nothing Then
            Dim data As DataRow = Session(Constant.searchSession)
            If data("menuID") = menuID Then
                DDType.SelectedValue = data("DDType")
                If DDType.SelectedValue <> "0" Then
                    Me.DocSubType()
                End If

                DDLawType.SelectedValue = data("DDLawType")
                txtDocId.Text = data("txtDocId")
                txtDocName.Text = data("txtDocName")
            Else
                Session.Remove(Constant.searchSession)
            End If
        End If
    End Sub

    Protected Sub linkHome_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles linkHome.Click
        Response.Redirect(MD.pHome, True)
    End Sub

    Protected Sub DDType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DDType.SelectedIndexChanged
        Me.DocSubType()
    End Sub
End Class
