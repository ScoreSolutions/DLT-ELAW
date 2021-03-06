﻿Imports System.Data
Imports System.Data.OleDb
Partial Class Src_BookKind
    Inherits System.Web.UI.Page
    Dim cn As OleDbConnection
    Dim cmd As OleDbCommand
    Dim MD As New MainData
    Dim MC As New MainClass
    Dim DV As DataView
    Public iRow As Integer
    Dim DS As DataSet
    Dim iRec As Integer

    Private Sub ChkPermis() 'ฟังก์ชั่นการตรวจสอบสิทธิ์การเข้าใช้
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
        Me.ChkPermis() 'เรียกใช้ฟังก์ชั่นการตรวจสอบสิทธิ์การเข้าใช้ 
        If Not Page.IsPostBack Then
            ViewState("sortfield") = "bookkind_name"
            ViewState("sortdirection") = "asc"

            Me.DataBookType()
            Me.gData() 'เรียกใช้ฟังก์ชั่นการแสดงข้อมูล
            Me.MyGridBind()

        Else
            If Session("division") Is Nothing Then
                Me.gData()
            Else
                DV = Session("division")
            End If
        End If
    End Sub
    Private Sub Auto() 'ฟังก์ชั่นการกำหนดรหัสของข้อมูลโดยเพิ่มขึ้นจากเดิมทีละ 1 

        Dim sqlTmp As String = ""
        Dim comTmp As OleDbCommand = New OleDbCommand
        Dim drTmp As OleDbDataReader
        Dim tmpMemberID As Integer = 0
        Dim tmpMemberID2 As String = ""

        sqlTmp = "SELECT TOP 1 bookkind_id FROM book_kind "
        sqlTmp &= " ORDER BY bookkind_id DESC"

        Dim cn As New OleDbConnection(MD.Strcon)
        Dim cmd As New OleDbCommand(sqlTmp, cn)
        cn.Open()

        Try
            With comTmp
                .CommandType = CommandType.Text
                .CommandText = sqlTmp
                .Connection = cn
                drTmp = .ExecuteReader()

                drTmp.Read()

                tmpMemberID2 = drTmp.Item("bookkind_id")

                tmpMemberID = CInt(tmpMemberID2) + 1
                lblId.Text = tmpMemberID.ToString

            End With
        Catch
            lblId.Text = "1"
        End Try
        cn.Close()

    End Sub
    Public Sub DataBookType()

        Dim strsql As String
        strsql = "select booktype_id,booktype_name from book_type order by booktype_name  "

        Dim DTS As DataTable = MD.GetDataTable(strsql)
        Dim dr As DataRow = DTS.NewRow
        dr!booktype_id = 0
        dr!booktype_name = "---------โปรดเลือก---------"
        DTS.Rows.InsertAt(dr, 0)
        ddlBooktype.DataTextField = "booktype_name"
        ddlBooktype.DataValueField = "booktype_id"
        ddlBooktype.DataSource = DTS
        ddlBooktype.DataBind()

    End Sub

    Private Sub gData() 'ฟังก์ชั่นการเรียกข้อมูลมาแสดง

        Dim strsql As String
        strsql = "select b.booktype_id,b.bookkind_id,b.bookkind_name,e.firstname+' '+e.lastname creation_by,b.created_date,  "
        strsql &= " e1.firstname+' '+e1.lastname updated_by,b.updated_date,b1.booktype_name  "
        strsql &= " from book_kind b inner join employee e  "
        strsql &= " on b.creation_by=e.empid "
        strsql &= " inner join employee e1 "
        strsql &= " on b.updated_by=e1.empid "
        strsql &= " inner join book_type b1 "
        strsql &= " on b.booktype_id = b1.booktype_id "

        If ddlBooktype.SelectedValue <> "0" Then
            strsql &= " and b1.booktype_id='" & ddlBooktype.SelectedValue & "' "
        End If

        If txtStatus.Text <> "" Then
            strsql &= " and bookkind_name like '%" & txtStatus.Text & "%'"
        End If

        Dim DT As DataTable = MD.GetDataTable(strsql)
        DV = DT.DefaultView
        DV.Sort = "[" & ViewState("sortfield") & "] " & ViewState("sortdirection")
        Session("book_kind") = DV
    End Sub
    Private Sub MyGridBind() 'ฟังก์ชั่นการแสดงข้อมูลในตาราง
        GridView1.DataSource = DV
        Dim X1() As String = {"bookkind_id"}
        GridView1.DataKeyNames = X1
        GridView1.DataBind()
    End Sub
    Protected Sub GridView1_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridView1.PageIndexChanging
        'การเลือกเปลี่ยนหน้าของข้อมูล
        GridView1.PageIndex = e.NewPageIndex
        Me.MyGridBind()

    End Sub
    Protected Sub GridView1_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowCreated
        'การสร้างลิ้งเปลี่ยนหน้าข้อมูล
        If e.Row.RowType = DataControlRowType.Pager Then
            Dim td As TableCell = e.Row.Cells(0)
            Dim Y As Boolean = False
            If GridView1.PageIndex > 0 Then
                Dim L1 As New LinkButton
                L1.ForeColor = Drawing.Color.White
                L1.Text = "First"
                td.Controls.Add(L1)
                AddHandler L1.Click, AddressOf FirstClick

                Y = True

                Dim L2 As New Literal
                L2.Text = " &nbsp;&nbsp;"
                td.Controls.Add(L2)

                L1 = New LinkButton
                L1.ForeColor = Drawing.Color.White
                L1.Text = "Prev"
                AddHandler L1.Click, AddressOf PrevClick
                td.Controls.Add(L1)
            End If
            If GridView1.PageIndex < GridView1.PageCount - 1 Then
                Dim L2 As Literal
                If Y = True Then
                    L2 = New Literal
                    L2.Text = " &nbsp;&nbsp;"
                    td.Controls.Add(L2)
                End If

                Dim L1 As New LinkButton
                L1.ForeColor = Drawing.Color.White
                L1.Text = "Next"
                AddHandler L1.Click, AddressOf NextClick
                td.Controls.Add(L1)

                L2 = New Literal
                L2.Text = " &nbsp;&nbsp;"
                td.Controls.Add(L2)


                L1 = New LinkButton
                L1.ForeColor = Drawing.Color.White
                L1.Text = "Last"
                AddHandler L1.Click, AddressOf LastClick
                td.Controls.Add(L1)
            End If
        End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            iRow = e.Row.DataItemIndex + 1
            If Not e.Row.RowState And DataControlRowState.Edit Then
                Dim L1 As ImageButton = e.Row.Cells(7).Controls(1)
                L1.OnClientClick = "return confirm('คุณต้องการลบข้อมูลใช่หรือไม่?');"
            End If
        End If
    End Sub
    Private Sub GoPage(ByVal xPage As Integer)
        'การเปลี่ยนหน้าของข้อมูล
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
    Protected Sub GridView1_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GridView1.RowDeleting
        'การลบข้อมูล
        Dim K1 As DataKey = GridView1.DataKeys(e.RowIndex)
        Dim Y As Integer = MD.Execute("delete from book_kind where bookkind_id ='" & K1(0) & "'")
        If Y = 1 Then
            Dim drv As DataRowView = DV((GridView1.PageIndex * GridView1.PageSize) + e.RowIndex)
            drv.Delete()
            drv.Row.AcceptChanges()
            Me.MyGridBind()
            MC.MessageBox(Me, "ลบข้อมูลเรียบร้อยแล้ว")
        Else
            MC.MessageBox(Me, "ไม่สามารถลบข้อมูลได้")
        End If
    End Sub
    Protected Sub GridView1_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles GridView1.RowEditing
        'การแก้ไขข้อมูล
        Dim K1 As DataKey = GridView1.DataKeys(e.NewEditIndex)
        Dim ldept As Label = GridView1.Rows(e.NewEditIndex).Cells(0).Controls(1)
        Dim lname As Label = GridView1.Rows(e.NewEditIndex).Cells(2).Controls(1)

        lblId.Text = K1(0).ToString
        ddlBooktype.SelectedValue = ldept.Text
        txtStatus.Text = lname.Text

        lblStatus.Text = "Edit"
    End Sub
    Protected Sub GridView1_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles GridView1.Sorting
        'การเรียงข้อมูล
        If ViewState("sortfield") = e.SortExpression Then
            If ViewState("sortdirection") = "asc" Then ViewState("sortdirection") = "desc" Else ViewState("sortdirection") = "asc"
        Else
            ViewState("sortfield") = e.SortExpression
            ViewState("sortdirection") = "asc"
        End If
        DV.Sort = "[" & ViewState("sortfield") & "] " & ViewState("sortdirection")
        Me.MyGridBind()
    End Sub
    Private Sub ClearAlert()
        'การลบการแจ้งเตือน
        lblBookKind.Text = ""
        lblBookType.Text = ""
       
    End Sub
    Protected Sub bSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bSave.Click
        'การบันทึกข้อมูล
        Dim strsql As String = ""
        Dim sEmpNo As String = Session("EmpNo")

        If ddlBooktype.SelectedValue = "0" Then
            Me.ClearAlert()
            lblBookType.Text = "กรุณาเลือกประเภทหนังสือ"
            Exit Sub
        End If

        If txtStatus.Text.Trim = "" Then
            Me.ClearAlert()
            lblBookKind.Text = "กรุณากรอกชนิดหนังสือ"
            txtStatus.Focus()
            Exit Sub
        End If

        If lblStatus.Text = "Edit" Then
            'การบันทึกข้อมูลเมื่อมีฟังก์ชั่นการแก้ไข
            Try
                strsql = "update book_kind set "
                strsql &= "booktype_id=?,bookkind_name=?,updated_by=?,updated_date=? "
                strsql &= "where bookkind_id=?"

                cn = New OleDbConnection(MD.Strcon)
                cmd = New OleDbCommand(strsql, cn)
                MD.CreateParam(cmd, "TTTDT")

                cmd.Parameters("@P1").Value = ddlBooktype.SelectedValue
                cmd.Parameters("@P2").Value = txtStatus.Text
                cmd.Parameters("@P3").Value = sEmpNo
                cmd.Parameters("@P4").Value = DateTime.Parse(Date.Now)
                cmd.Parameters("@P5").Value = lblId.Text

                cn.Open()
                cmd.ExecuteNonQuery()

                MC.MessageBox(Me, "บันทึกลงฐานข้อมูลเรียบร้อยแล้ว")
                Me.gData()
                Me.MyGridBind()
                Me.ClearAlert()
                'บันทึก Error_Log
            Catch ex As Exception
                Dim ses As String = System.Web.HttpContext.Current.Session.SessionID
                Dim url As String = HttpContext.Current.Request.FilePath
                Dim browser As String = " Type:" + Request.Browser.Type + " Version:" + Request.Browser.Version + " Browser:" + Request.Browser.Browser

                MC.MessageBox(Me, ex.ToString)
                MC.ErrLog(url, ses, ex.ToString, sEmpNo, browser)

            Finally
                cn.Close()
            End Try

        Else
            Me.Auto()
            'บันทึกข้อมูลที่เข้ามาใหม่
            Try
                strsql = "insert into book_kind (bookkind_id,booktype_id,bookkind_name,creation_by,created_date,updated_by,updated_date)"
                strsql &= " Values (?,?,?,?,?,?,?)"

                cn = New OleDbConnection(MD.Strcon)
                cmd = New OleDbCommand(strsql, cn)
                MD.CreateParam(cmd, "TTTTDTD")

                cmd.Parameters("@P1").Value = lblId.Text
                cmd.Parameters("@P2").Value = ddlBooktype.SelectedValue
                cmd.Parameters("@P3").Value = txtStatus.Text
                cmd.Parameters("@P4").Value = sEmpNo
                cmd.Parameters("@P5").Value = DateTime.Parse(Date.Now)
                cmd.Parameters("@P6").Value = sEmpNo
                cmd.Parameters("@P7").Value = DateTime.Parse(Date.Now)

                cn.Open()
                cmd.ExecuteNonQuery()

                MC.MessageBox(Me, "บันทึกลงฐานข้อมูลเรียบร้อยแล้ว")
                Me.gData()
                Me.MyGridBind()
                Me.ClearAlert()
            Catch ex As Exception
                Dim ses As String = System.Web.HttpContext.Current.Session.SessionID
                Dim url As String = HttpContext.Current.Request.FilePath
                Dim browser As String = " Type:" + Request.Browser.Type + " Version:" + Request.Browser.Version + " Browser:" + Request.Browser.Browser

                MC.MessageBox(Me, ex.ToString)
                MC.ErrLog(url, ses, ex.ToString, sEmpNo, browser)

            Finally
                cn.Close()
            End Try

        End If
        lblId.Text = ""
        ddlBooktype.SelectedIndex = 0
        txtStatus.Text = ""
        lblStatus.Text = ""

    End Sub
    Protected Sub bCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bCancel.Click
        'ยกเลิกการบันทึกข้อความ
        txtStatus.Text = ""
        lblStatus.Text = ""
        Me.ClearAlert()

    End Sub

    Protected Sub bSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bSearch.Click
        'การค้นหาข้อมูล
        Me.gData()
        Me.MyGridBind()
        Me.ClearAlert()
    End Sub
End Class

