Imports System.Data
Imports System.Data.OleDb
Imports System.Security.Cryptography
Imports System.Data.SqlClient
Partial Class Users_ShowUsers
    Inherits System.Web.UI.Page
    Dim MC As New MainClass
    Dim MD As New MainData
    Dim DV As DataView
    Public iRow As Integer
    Dim oMsg As New MainClass
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'Me.CheckAdmin()
        If Not Page.IsPostBack Then
            ViewState("sortfield") = "empid"
            ViewState("sortdirection") = "asc"
            Me.gData()
            Me.MyGridBind()
            'Me.DataDept()
            'Me.DataPre()


        Else
            If Session("data_users") Is Nothing Then
                Me.gData()
            Else
                DV = Session("data_users")
            End If
        End If
    End Sub
    Private Sub CheckAdmin()
        Dim sEmpNo As String = Session("EmpNo")
        Dim url As String = HttpContext.Current.Request.FilePath
        Dim X As Boolean = MC.ChkPermission(sEmpNo, url)
        If X = True Then

        Else
            Response.Redirect("../Menu/NoAuth.aspx")
        End If

    End Sub
    Public Sub DataPre()
        Dim strsql As String
        strsql = "select pn_id,pn_name from Pre_Name order by pn_id"

        Dim cn As New OleDbConnection(MD.Strcon)
        Dim cmd As New OleDbCommand(strsql, cn)
        cn.Open()
        Dim dr As OleDbDataReader = cmd.ExecuteReader
        DDPreName.DataTextField = "pn_name"
        DDPreName.DataValueField = "pn_id"
        DDPreName.DataSource = dr
        DDPreName.DataBind()
        cn.Close()
    End Sub
    Public Sub DataDept()
        Dim strsql As String
        strsql = "select dp_id,dp_name from department order by dp_name"

        Dim cn As New OleDbConnection(MD.Strcon)
        Dim cmd As New OleDbCommand(strsql, cn)
        cn.Open()
        Dim dr As OleDbDataReader = cmd.ExecuteReader
        DDDept.DataTextField = "dp_name"
        DDDept.DataValueField = "dp_id"
        DDDept.DataSource = dr
        DDDept.DataBind()
        cn.Close()
    End Sub
    Private Sub gData(Optional ByVal X As String = "", Optional ByVal xType As String = "S")

        Dim strsql As String
        strsql = "select * from employee "
      

        If X <> "" And xType = "0" Then
            strsql &= "and u.us_id like '%" & X & "%'"
        ElseIf X <> "" And xType = "1" Then
            strsql &= "and u.us_fname like '%" & X & "%'"
        ElseIf X <> "" And xType = "2" Then
            strsql &= "and u.us_lname like '%" & X & "%'"
        End If

        Dim DT As DataTable = MD.GetDataTable(strsql)
        DV = DT.DefaultView
        DV.Sort = "[" & ViewState("sortfield") & "] " & ViewState("sortdirection")
        Session("data_users") = DV
    End Sub
    Private Sub MyGridBind()
        GridView1.DataSource = DV
        Dim X1() As String = {"empid"}
        GridView1.DataKeyNames = X1
        GridView1.DataBind()
    End Sub
    Protected Sub GridView1_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridView1.PageIndexChanging
        GridView1.PageIndex = e.NewPageIndex
        Me.MyGridBind()
    End Sub

    Protected Sub GridView1_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowCreated
        If e.Row.RowType = DataControlRowType.Pager Then
            Dim td As TableCell = e.Row.Cells(0)
            Dim Y As Boolean = False
            If GridView1.PageIndex > 0 Then
                Dim L1 As New LinkButton
                L1.Text = "First"
                td.Controls.Add(L1)
                AddHandler L1.Click, AddressOf FirstClick

                Y = True

                Dim L2 As New Literal
                L2.Text = " &nbsp;&nbsp;"
                td.Controls.Add(L2)

                L1 = New LinkButton
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
                L1.Text = "Next"
                AddHandler L1.Click, AddressOf NextClick
                td.Controls.Add(L1)

                L2 = New Literal
                L2.Text = " &nbsp;&nbsp;"
                td.Controls.Add(L2)


                L1 = New LinkButton
                L1.Text = "Last"
                AddHandler L1.Click, AddressOf LastClick
                td.Controls.Add(L1)
            End If
        End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            iRow = e.Row.DataItemIndex + 1
            If Not e.Row.RowState And DataControlRowState.Edit Then
                'Dim L1 As ImageButton = e.Row.Cells(11).Controls(1)
                'L1.OnClientClick = "return confirm('คุณต้องการลบข้อมูลใช่หรือไม่?');"
            End If
        End If
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
    Protected Sub GridView1_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GridView1.RowDeleting
        Dim K1 As DataKey = GridView1.DataKeys(e.RowIndex)
        Dim Y As Integer = MD.Execute("delete from users where us_id ='" & K1(0) & "'")
        If Y = 1 Then
            Dim drv As DataRowView = DV((GridView1.PageIndex * GridView1.PageSize) + e.RowIndex)
            drv.Delete()
            drv.Row.AcceptChanges()
            Me.MyGridBind()
            oMsg.MessageBox(Me, "ลบข้อมูลเรียบร้อยแล้ว")
        Else
            oMsg.MessageBox(Me, "ไม่สามารถลบข้อมูลได้")
        End If
    End Sub

    Protected Sub GridView1_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles GridView1.RowEditing

        Dim lId As Label = GridView1.Rows(e.NewEditIndex).Cells(1).Controls(1)
        'Dim lPwd As Label = GridView1.Rows(e.NewEditIndex).Cells(2).Controls(1)
        Dim lPre As Label = GridView1.Rows(e.NewEditIndex).Cells(3).Controls(1)
        Dim lFname As Label = GridView1.Rows(e.NewEditIndex).Cells(5).Controls(1)
        Dim lLname As Label = GridView1.Rows(e.NewEditIndex).Cells(6).Controls(1)
        Dim lDept As Label = GridView1.Rows(e.NewEditIndex).Cells(7).Controls(1)

        txtId.Text = lId.Text
        'txtPwd.Text = lPwd.Text
        DDPreName.SelectedValue = lPre.Text
        txtFname.Text = lFname.Text
        txtLname.Text = lLname.Text
        DDDept.SelectedValue = lDept.Text

        txtId.Enabled = False
        lblState.Text = "Edit"
        lblTstate.Text = "แก้ไขข้อมูลผู้ใช้งาน"
    End Sub
    Protected Sub GridView1_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles GridView1.Sorting
        If ViewState("sortfield") = e.SortExpression Then
            If ViewState("sortdirection") = "asc" Then ViewState("sortdirection") = "desc" Else ViewState("sortdirection") = "asc"
        Else
            ViewState("sortfield") = e.SortExpression
            ViewState("sortdirection") = "asc"
        End If
        DV.Sort = "[" & ViewState("sortfield") & "] " & ViewState("sortdirection")
        Me.MyGridBind()
    End Sub
    Protected Sub bSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bSave.Click
        Dim strsql As String = ""
        Dim sEmpNo As String = Session("EmpNo")
        If txtId.Text.Trim = "" Then
            oMsg.MessageBox(Me, "กรุณากรอกชื่อเข้าระบบ")
            txtId.Focus()
            Exit Sub
        End If
        If txtFname.Text.Trim = "" Then
            oMsg.MessageBox(Me, "กรุณากรอกชื่อจริง")
            txtFname.Focus()
            Exit Sub
        End If

        Dim md5Hasher As New MD5CryptoServiceProvider()

        Dim hashedBytes As Byte()
        Dim encoder As New UTF8Encoding()

        hashedBytes = md5Hasher.ComputeHash(encoder.GetBytes(txtPwd.Text))


        If lblState.Text = "Edit" Then

            strsql = "update Users set pn_id=?,us_fname=?,us_lname=?,dp_id=?,us_user_update=?,us_date_update=? "
            strsql &= " where us_id=? "


            Dim cmd As OleDb.OleDbCommand = MD.CreateCommand(strsql)
            MD.CreateParam(cmd, "TTTTTDT")

            cmd.Parameters(0).Value = DDPreName.SelectedValue
            cmd.Parameters(1).Value = txtFname.Text
            cmd.Parameters(2).Value = txtLname.Text
            cmd.Parameters(3).Value = DDDept.SelectedValue
            cmd.Parameters(4).Value = sEmpNo
            cmd.Parameters(5).Value = Date.Now
            cmd.Parameters(6).Value = txtId.Text




            Dim Y1 As Integer
            Y1 = MD.ExecuteGrid(cmd)
            If Y1 = 1 Then
                DV.Table.AcceptChanges()
                GridView1.EditIndex = -1
                oMsg.MessageBox(Me, "บันทึกลงฐานข้อมูลเรียบร้อยแล้ว")
                Me.gData()
                Me.MyGridBind()

            Else
                oMsg.MessageBox(Me, "ไม่สามารถบันทึกลงฐานข้อมูลได้")
            End If
        Else

            strsql = "insert into users (loginname,password,empid) "
            strsql &= " values (?,?,?)"

            Dim cmd As OleDb.OleDbCommand = MD.CreateCommand(strsql)
            MD.CreateParam(cmd, "TPT")

            cmd.Parameters(0).Value = txtId.Text
            cmd.Parameters(1).Value = hashedBytes 'txtPwd.Text
            cmd.Parameters(2).Value = txtFname.Text
            



            Dim Y1 As Integer
            Y1 = MD.ExecuteGrid(cmd)
            If Y1 = 1 Then
                DV.Table.AcceptChanges()
                GridView1.EditIndex = -1
                oMsg.MessageBox(Me, "บันทึกลงฐานข้อมูลเรียบร้อยแล้ว")
                Me.gData()
                Me.MyGridBind()

            Else
                oMsg.MessageBox(Me, "ไม่สามารถบันทึกลงฐานข้อมูลได้")
            End If

        End If
        lblId.Text = ""
        txtId.Text = ""
        txtPwd.Text = ""
        txtFname.Text = ""
        txtLname.Text = ""
        lblState.Text = ""
        lblTstate.Text = "เพิ่มผู้ใช้งาน"
    End Sub
    Protected Sub bCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        txtId.Text = ""
        txtPwd.Text = ""
        txtFname.Text = ""
        txtLname.Text = ""
        lblState.Text = ""
        lblTstate.Text = "เพิ่มผู้ใช้งาน"
        txtId.Enabled = True
    End Sub

    Protected Sub bSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Me.gData(txtSearch.Text, DDSearch.SelectedValue)
        Me.MyGridBind()
    End Sub

    Protected Sub GridView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)

    End Sub
    Protected Sub bChange_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bChange.Click
        Dim strsql As String = ""
        Dim sEmpNo As String = Session("EmpNo")
        If txtId.Text.Trim = "" Then
            oMsg.MessageBox(Me, "กรุณากรอกชื่อเข้าระบบ")
            txtId.Focus()
            Exit Sub
        End If
        If txtFname.Text.Trim = "" Then
            oMsg.MessageBox(Me, "กรุณากรอกชื่อจริง")
            txtFname.Focus()
            Exit Sub
        End If

        Dim md5Hasher As New MD5CryptoServiceProvider()

        Dim hashedBytes As Byte()
        Dim encoder As New UTF8Encoding()

        hashedBytes = md5Hasher.ComputeHash(encoder.GetBytes(txtPwd.Text))


        If lblState.Text = "Edit" Then

            strsql = "update Users set us_pwd=?,pn_id=?,us_fname=?,us_lname=?,dp_id=?,us_user_update=?,us_date_update=? "
            strsql &= " where us_id=? "


            Dim cmd As OleDb.OleDbCommand = MD.CreateCommand(strsql)
            MD.CreateParam(cmd, "PTTTTTDT")

            cmd.Parameters(0).Value = hashedBytes 'txtPwd.Text
            cmd.Parameters(1).Value = DDPreName.SelectedValue
            cmd.Parameters(2).Value = txtFname.Text
            cmd.Parameters(3).Value = txtLname.Text
            cmd.Parameters(4).Value = DDDept.SelectedValue
            cmd.Parameters(5).Value = sEmpNo
            cmd.Parameters(6).Value = Date.Now
            cmd.Parameters(7).Value = txtId.Text




            Dim Y1 As Integer
            Y1 = MD.ExecuteGrid(cmd)
            If Y1 = 1 Then
                DV.Table.AcceptChanges()
                GridView1.EditIndex = -1
                oMsg.MessageBox(Me, "บันทึกลงฐานข้อมูลเรียบร้อยแล้ว")
                Me.gData()
                Me.MyGridBind()

            Else
                oMsg.MessageBox(Me, "ไม่สามารถบันทึกลงฐานข้อมูลได้")
            End If
        Else

            strsql = "insert into Users (us_id,us_pwd,pn_id,us_fname,us_lname,dp_id,us_user_update,us_date_update) "
            strsql &= " values (?,?,?,?,?,?,?,?)"

            Dim cmd As OleDb.OleDbCommand = MD.CreateCommand(strsql)
            MD.CreateParam(cmd, "TPTTTTTD")

            cmd.Parameters(0).Value = txtId.Text
            cmd.Parameters(1).Value = hashedBytes 'txtPwd.Text
            cmd.Parameters(2).Value = DDPreName.SelectedValue
            cmd.Parameters(3).Value = txtFname.Text
            cmd.Parameters(4).Value = txtLname.Text
            cmd.Parameters(5).Value = DDDept.SelectedValue
            cmd.Parameters(6).Value = sEmpNo
            cmd.Parameters(7).Value = Date.Now



            Dim Y1 As Integer
            Y1 = MD.ExecuteGrid(cmd)
            If Y1 = 1 Then
                DV.Table.AcceptChanges()
                GridView1.EditIndex = -1
                oMsg.MessageBox(Me, "บันทึกลงฐานข้อมูลเรียบร้อยแล้ว")
                Me.gData()
                Me.MyGridBind()

            Else
                oMsg.MessageBox(Me, "ไม่สามารถบันทึกลงฐานข้อมูลได้")
            End If

        End If
        lblId.Text = ""
        txtId.Text = ""
        txtPwd.Text = ""
        txtFname.Text = ""
        txtLname.Text = ""
        lblState.Text = ""
        lblTstate.Text = "เพิ่มผู้ใช้งาน"
    End Sub
End Class
