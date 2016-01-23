Imports System.Data
Imports System.Data.OleDb
Partial Class Src_SearchLaw
    Inherits System.Web.UI.Page
    Dim MD As New MainData
    Dim MC As New MainClass
    Dim DVLst As DataView
    Dim DVLstSearch As DataView
    Dim DVLstContract As DataView
    Dim DVLstCase As DataView
    Public iRow As Integer
    Dim DS As DataSet
    Dim iRec As Integer
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            ViewState("sortfield") = "doc_id"
            ViewState("sortdirection") = "desc"

            Me.gDataSearch()
            Me.MyGridBindSearch()

            Me.gDataContract()
            Me.MyGridBindContract()

            Me.gDataCase()
            Me.MyGridBindCase()

        Else
            Dim t1 As String = Me.ChkLaw()
            Dim t2 As String = Me.ChkContract()
            Dim t3 As String = Me.ChkCase()

            Me.gData(t1, t2, t3)
            Me.MyGridBind()
            If Session("SearchLaw") Is Nothing Then
                Me.gData(t1, t2, t3)
            Else
                DVLst = Session("SearchLaw")
            End If

        End If
    End Sub
    Private Sub gDataSearch()
        'ดึงข้อมูลประเภทกฎหมาย แสดงใน Gridview
        Dim sEmpNo As String = Session("EmpNo")
        Dim strsql As String

        strsql = " select type_id,type_name  "
        strsql &= "from law_type  "


        Dim DT As DataTable = MD.GetDataTable(strsql)
        DVLstSearch = DT.DefaultView
        Session("law_type") = DVLstSearch

    End Sub
    Private Sub MyGridBindSearch()
        GridView2.DataSource = DVLstSearch
        Dim X1() As String = {"type_id"}
        GridView2.DataKeyNames = X1
        GridView2.DataBind()
    End Sub
    Public Sub cb1_Checked(ByVal sender As Object, ByVal e As EventArgs)
        'CheckBox หน้า Gridveiw ประเภทเอกสาร
        Dim cb1 As CheckBox = sender
        Dim dgi As GridViewRow
        Dim cb2 As CheckBox
        For Each dgi In GridView2.Rows
            cb2 = dgi.Cells(1).FindControl("cb1")
            cb2.Checked = cb1.Checked
        Next
    End Sub
    Private Sub gDataContract()
        'ดึงข้อมูลประเภทกฎหมาย แสดงใน Gridview Contract
        Dim sEmpNo As String = Session("EmpNo")
        Dim strsql As String

        strsql = " select type_id,type_name  "
        strsql &= "from contract_type  "


        Dim DT As DataTable = MD.GetDataTable(strsql)
        DVLstContract = DT.DefaultView
        Session("contract_type") = DVLstContract

    End Sub
    Private Sub MyGridBindCase()
        gdvCase.DataSource = DVLstCase
        Dim X1() As String = {"type_id"}
        gdvCase.DataKeyNames = X1
        gdvCase.DataBind()
    End Sub
    Public Sub cb2_Checked(ByVal sender As Object, ByVal e As EventArgs)
        'CheckBox หน้า Gridveiw ประเภทเอกสาร
        Dim cb1 As CheckBox = sender
        Dim dgi As GridViewRow
        Dim cb2 As CheckBox
        For Each dgi In gdvContract.Rows
            cb2 = dgi.Cells(1).FindControl("cb2")
            cb2.Checked = cb1.Checked
        Next
    End Sub
    Private Sub gDataCase()
        'ดึงข้อมูลประเภทกฎหมาย แสดงใน Gridview Case
        Dim sEmpNo As String = Session("EmpNo")
        Dim strsql As String

        strsql = " select type_id,type_name  "
        strsql &= "from case_type  "


        Dim DT As DataTable = MD.GetDataTable(strsql)
        DVLstCase = DT.DefaultView
        Session("case_type") = DVLstCase

    End Sub
    Private Sub MyGridBindContract()
        gdvContract.DataSource = DVLstContract
        Dim X1() As String = {"type_id"}
        gdvContract.DataKeyNames = X1
        gdvContract.DataBind()
    End Sub
    Public Sub cb3_Checked(ByVal sender As Object, ByVal e As EventArgs)
        'CheckBox หน้า Gridveiw ประเภทเอกสาร
        Dim cb1 As CheckBox = sender
        Dim dgi As GridViewRow
        Dim cb2 As CheckBox
        For Each dgi In gdvCase.Rows
            cb2 = dgi.Cells(1).FindControl("cb3")
            cb2.Checked = cb1.Checked
        Next
    End Sub
    Private Sub gData(Optional ByVal Type As String = "", Optional ByVal TypeContract As String = "", Optional ByVal TypeCase As String = "")
        'ดึงข้อมูล แสดงใน Gridview
        Dim strsql As String = ""
        If txtKeyword.Text.Trim <> "" Then

            strsql = " select d.doc_id,case when d.cancel=1 then d.doc_name+' (ยกเลิก : '+d.cancel_comment+')' else d.doc_name end doc_name,"
            strsql &= " v.name subtype_name,d.doc_type ,d.doc_subtype "
            strsql &= " from import_document d  "
            strsql &= " inner join import_keywords k"
            strsql &= " on d.doc_id=k.doc_id inner join DOCUMENT_TYPE t"
            strsql &= " on d.doc_type=t.doc_id  "
            strsql &= " inner join import_document_subtype v "
            strsql &= " on d.doc_subtype=v.id and v.tbl=t.ref_table "
            strsql &= " where d.active = 1 "

            If Type <> "" Then
                strsql &= " and t.doc_id=1 and v.type_id  in (" & Type & ") and  d.active = 1  "
                If txtTitle.Text <> "" Then
                    strsql &= "and d.doc_name like '%" & txtTitle.Text & "%' "
                End If
                strsql &= setKeyWord(txtKeyword.Text)
            End If

            If TypeContract <> "" Then
                If Type <> "" Then
                    strsql &= " or "
                Else
                    strsql &= " and "
                End If
                strsql &= " t.doc_id=3 and v.type_id  in (" & TypeContract & ")  And d.active = 1 "
                If txtTitle.Text <> "" Then
                    strsql &= "and d.doc_name like '%" & txtTitle.Text & "%' "
                End If
                strsql &= setKeyWord(txtKeyword.Text)
            End If

            If TypeCase <> "" Then
                If Type <> "" Then
                    strsql &= " or "
                Else
                    If TypeContract <> "" Then
                        strsql &= " or "
                    Else
                        strsql &= " and "
                    End If
                End If
                strsql &= "t.doc_id=2 and v.type_id  in (" & TypeCase & ") And d.active = 1 "
                If txtTitle.Text <> "" Then
                    strsql &= "and d.doc_name like '%" & txtTitle.Text & "%' "
                End If
                strsql &= setKeyWord(txtKeyword.Text)
            End If
            If txtTitle.Text <> "" Then
                strsql &= "and d.doc_name like '%" & txtTitle.Text & "%' "
            End If

            'strsql &= setKeyWord(txtKeyword.Text)
            strsql &= "and k.keyword like '%" & txtKeyword.Text & "%'"

            strsql &= " group by d.doc_id,case when d.cancel=1 then d.doc_name+' (ยกเลิก : '+d.cancel_comment+')' else d.doc_name end,v.name,d.doc_type ,d.doc_subtype    "
            strsql &= " order by d.doc_type ,d.doc_subtype "

            Dim DT As DataTable = MD.GetDataTable(strsql)
            DVLst = DT.DefaultView
            Session("SearchLaw") = DVLst
            If DVLst.Table.Rows.Count < 1 Then
                strsql = " select d.doc_id,case when d.cancel=1 then d.doc_name+' (ยกเลิก : '+d.cancel_comment+')' else d.doc_name end doc_name,"
                strsql &= " v.name subtype_name,d.doc_type ,d.doc_subtype "
                strsql &= " from import_document d  "
                strsql &= " inner join import_keywords k"
                strsql &= " on d.doc_id=k.doc_id inner join DOCUMENT_TYPE t"
                strsql &= " on d.doc_type=t.doc_id  "
                strsql &= " inner join import_document_subtype v "
                strsql &= " on d.doc_subtype=v.id and v.tbl=t.ref_table "
                strsql &= " where d.active = 1 "

                If Type <> "" Then
                    strsql &= " and t.doc_id=1 and v.type_id  in (" & Type & ") and  d.active = 1  "
                    If txtTitle.Text <> "" Then
                        strsql &= "and d.doc_name like '%" & txtTitle.Text & "%' "
                    End If
                    strsql &= setKeyWord(txtKeyword.Text)
                End If

                If TypeContract <> "" Then
                    If Type <> "" Then
                        strsql &= " or "
                    Else
                        strsql &= " and "
                    End If
                    strsql &= " t.doc_id=3 and v.type_id  in (" & TypeContract & ")  And d.active = 1 "
                    If txtTitle.Text <> "" Then
                        strsql &= "and d.doc_name like '%" & txtTitle.Text & "%' "
                    End If
                    strsql &= setKeyWord(txtKeyword.Text)
                End If

                If TypeCase <> "" Then
                    If Type <> "" Then
                        strsql &= " or "
                    Else
                        If TypeContract <> "" Then
                            strsql &= " or "
                        Else
                            strsql &= " and "
                        End If
                    End If
                    strsql &= "t.doc_id=2 and v.type_id  in (" & TypeCase & ") And d.active = 1 "
                    If txtTitle.Text <> "" Then
                        strsql &= "and d.doc_name like '%" & txtTitle.Text & "%' "
                    End If
                    strsql &= setKeyWord(txtKeyword.Text)
                End If
                If txtTitle.Text <> "" Then
                    strsql &= "and d.doc_name like '%" & txtTitle.Text & "%' "
                End If
                strsql &= setKeyWord(txtKeyword.Text)

                strsql &= " group by d.doc_id,case when d.cancel=1 then d.doc_name+' (ยกเลิก : '+d.cancel_comment+')' else d.doc_name end,v.name,d.doc_type ,d.doc_subtype    "
                strsql &= " order by d.doc_type ,d.doc_subtype "

                DT = MD.GetDataTable(strsql)
                DVLst = DT.DefaultView
                Session("SearchLaw") = DVLst
            End If

        Else

            strsql = " select d.doc_id,case when d.cancel=1 then d.doc_name+' (ยกเลิก : '+d.cancel_comment+')' else d.doc_name end doc_name,"
            strsql &= " v.name subtype_name "
            strsql &= " from import_document d  "
            strsql &= " inner join DOCUMENT_TYPE t"
            strsql &= " on d.doc_type=t.doc_id  "
            strsql &= " inner join import_document_subtype v "
            strsql &= " on d.doc_subtype=v.id and v.tbl=t.ref_table "
            strsql &= " where d.active = 1 "

            If Type <> "" Then
                strsql &= " and t.doc_id=1 and v.type_id  in (" & Type & ") And d.active = 1  "
                If txtTitle.Text <> "" Then
                    strsql &= "and d.doc_name like '%" & txtTitle.Text & "%' "
                End If
            End If

            If TypeContract <> "" Then
                If Type <> "" Then
                    strsql &= " or "
                Else
                    strsql &= " and "
                End If
                strsql &= " t.doc_id=3 and v.type_id  in (" & TypeContract & ") and d.active = 1 "
                If txtTitle.Text <> "" Then
                    strsql &= "and d.doc_name like '%" & txtTitle.Text & "%' "
                End If
            End If

            If TypeCase <> "" Then
                If Type <> "" Then
                    strsql &= " or "
                Else
                    If TypeContract <> "" Then
                        strsql &= " or "
                    Else
                        strsql &= " and "
                    End If
                End If
                strsql &= "t.doc_id=2 and v.type_id  in (" & TypeCase & ") And d.active = 1 "
                If txtTitle.Text <> "" Then
                    strsql &= "and d.doc_name like '%" & txtTitle.Text & "%' "
                End If
            End If

            If txtTitle.Text <> "" Then
                strsql &= "and d.doc_name like '%" & txtTitle.Text & "%' "
            End If
            strsql &= " order by d.doc_type ,d.doc_subtype "


            Dim DT As DataTable = MD.GetDataTable(strsql)
            DVLst = DT.DefaultView
            Session("SearchLaw") = DVLst


        End If


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
            ret = " and (" & ret & ")"
        End If

        Return ret
    End Function
    Private Sub MyGridBind()
        GridView1.DataSource = DVLst
        Dim X1() As String = {"doc_id"}
        GridView1.DataKeyNames = X1
        GridView1.DataBind()
    End Sub
    Protected Sub bSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bSearch.Click
        'ค้นหาเอกสาร
        Dim t1 As String = Me.ChkLaw()
        Dim t2 As String = Me.ChkContract()
        Dim t3 As String = Me.ChkCase()

        Me.gData(t1, t2, t3)
        Me.MyGridBind()
    End Sub
    Function ChkLaw() As String
        'ค้นหาเอกสาร'
        Dim S1 As New System.Text.StringBuilder("")
        Dim MD As New MainData
        Dim strSql As String = ""
        Dim txtType As String = ""
        Dim oDs As New Data.DataSet
        Dim sData As String = ""

        If txtKeyword.Text.Trim() <> "" Then
            SaveKeyword(txtKeyword.Text.Trim())
        End If

        For Each dgi As GridViewRow In GridView2.Rows
            Dim cb As CheckBox = dgi.Cells(1).FindControl("cb1")
            If cb.Checked = True Then

                Dim K1 As DataKey = GridView2.DataKeys(dgi.RowIndex)
                Dim index As Integer = Convert.ToInt32(dgi.RowIndex)
                Dim row As GridViewRow = GridView2.Rows(index)
                Dim item As New ListItem()

                item.Text = Server.HtmlDecode(row.Cells(1).Text) 'item

                Dim Vkey As String = K1.Value
                If S1.Length > 0 Then S1.Append("','")

                S1.Append(K1(0).ToString)

            End If

        Next

        If S1.ToString.Length > 0 Then
            Return "'" & S1.ToString & "'"
        Else
            Return ""
        End If
    End Function
    Function ChkContract() As String
        'ค้นหาเอกสาร
        Dim S1 As New System.Text.StringBuilder("")
        Dim MD As New MainData
        Dim strSql As String = ""
        Dim txtType As String = ""
        Dim oDs As New Data.DataSet
        Dim sData As String = ""

        If txtKeyword.Text.Trim() <> "" Then
            SaveKeyword(txtKeyword.Text.Trim())
        End If

        For Each dgi As GridViewRow In gdvContract.Rows
            Dim cb As CheckBox = dgi.Cells(1).FindControl("cb2")
            If cb.Checked = True Then

                Dim K1 As DataKey = gdvContract.DataKeys(dgi.RowIndex)
                Dim index As Integer = Convert.ToInt32(dgi.RowIndex)
                Dim row As GridViewRow = gdvContract.Rows(index)
                Dim item As New ListItem()

                item.Text = Server.HtmlDecode(row.Cells(1).Text) 'item

                Dim Vkey As String = K1.Value
                If S1.Length > 0 Then S1.Append("','")

                S1.Append(K1(0).ToString)

            End If

        Next

        If S1.ToString.Length > 0 Then
            Return "'" & S1.ToString & "'"
        Else
            Return ""
        End If
    End Function
    Function ChkCase() As String
        'ค้นหาเอกสาร
        Dim S1 As New System.Text.StringBuilder("")
        Dim MD As New MainData
        Dim strSql As String = ""
        Dim txtType As String = ""
        Dim oDs As New Data.DataSet
        Dim sData As String = ""

        If txtKeyword.Text.Trim() <> "" Then
            SaveKeyword(txtKeyword.Text.Trim())
        End If

        For Each dgi As GridViewRow In gdvCase.Rows
            Dim cb As CheckBox = dgi.Cells(1).FindControl("cb3")
            If cb.Checked = True Then

                Dim K1 As DataKey = gdvCase.DataKeys(dgi.RowIndex)
                Dim index As Integer = Convert.ToInt32(dgi.RowIndex)
                Dim row As GridViewRow = gdvCase.Rows(index)
                Dim item As New ListItem()

                item.Text = Server.HtmlDecode(row.Cells(1).Text) 'item

                Dim Vkey As String = K1.Value
                If S1.Length > 0 Then S1.Append("','")

                S1.Append(K1(0).ToString)

            End If

        Next

        If S1.ToString.Length > 0 Then
            Return "'" & S1.ToString & "'"
        Else
            Return ""
        End If
    End Function
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
    Protected Sub GridView1_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles GridView1.RowEditing
        'คลิกเลือกเพื่อทำการเปิดหน้าดาวน์โหลดเอกสาร
        Dim K1 As DataKey = GridView1.DataKeys(e.NewEditIndex)
        MC.OpenWindow(Me, "../Src/LawDetail.aspx?id=" & K1(0) & "&status=preview&menu=1")
    End Sub
    Protected Sub bClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bClear.Click
        txtKeyword.Text = ""
        txtTitle.Text = ""

        Me.gDataSearch()
        Me.MyGridBindSearch()
        Me.gDataContract()
        Me.MyGridBindContract()
        Me.gDataCase()
        Me.MyGridBindCase()

        Dim t1 As String = Me.ChkLaw()
        Dim t2 As String = Me.ChkContract()
        Dim t3 As String = Me.ChkCase()

        Me.gData(t1, t2, t3)
        Me.MyGridBind()
    End Sub
    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        'คลิกเลือกเพื่อทำการเปิดหน้าดาวน์โหลดเอกสาร
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lName As Label = e.Row.Cells(0).Controls(1)
            e.Row.Cells(3).Text = "<a href=""javascript:openwindow('" + "LawDetail" + "','" + lName.Text + "','" + "');"">" + "เลือก" + "</a>"
        End If
    End Sub
    Protected Sub linkHome_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles linkHome.Click
        Response.Redirect(MD.pHome, True)
    End Sub
End Class