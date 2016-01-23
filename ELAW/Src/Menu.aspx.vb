Imports System.Data
Imports System.Data.OleDb
Partial Class Src_Menu
    Inherits System.Web.UI.Page
    Dim cn As OleDbConnection
    Dim cmd As OleDbCommand
    Dim MD As New MainData
    Dim MC As New MainClass
    Dim DV As DataView
    Public iRow As Integer
    Dim DS As DataSet
    Dim iRec As Integer

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
    Public Sub DataModule()

        Dim strsql As String
        strsql = "select module_id,module_name from module order by module_name  "

        Dim DTS As DataTable = MD.GetDataTable(strsql)
        Dim dr As DataRow = DTS.NewRow
        dr!module_id = 0
        dr!module_name = "---------โปรดเลือก---------"
        DTS.Rows.InsertAt(dr, 0)
        ddlModule.DataTextField = "module_name"
        ddlModule.DataValueField = "module_id"
        ddlModule.DataSource = DTS
        ddlModule.DataBind()

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Me.ChkPermis()


        If Not Page.IsPostBack Then
            ViewState("sortfield") = "module_name"
            ViewState("sortdirection") = "asc"
            Me.DataModule()
            Me.gData()
            Me.MyGridBind()
        
        Else
            If Session("module") Is Nothing Then
                Me.gData()
            Else
                DV = Session("module")
            End If
        End If
    End Sub
    Private Sub Auto()

        Dim sqlTmp As String = ""
        Dim comTmp As OleDbCommand = New OleDbCommand
        Dim drTmp As OleDbDataReader
        Dim tmpMemberID As Integer = 0
        Dim tmpMemberID2 As String = ""


        sqlTmp = "SELECT TOP 1 menu_id FROM menu "
        sqlTmp &= " ORDER BY menu_id DESC"

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

                tmpMemberID2 = drTmp.Item("menu_id")

                tmpMemberID = CInt(tmpMemberID2) + 1
                lblId.Text = tmpMemberID.ToString

            End With
        Catch
            lblId.Text = "1"
        End Try
        cn.Close()

    End Sub
    Private Sub gData()

        Dim strsql As String
        strsql = "select m.module_id,m.menu_id,m.menu_name,e.firstname+' '+e.lastname creation_by,m.created_date,m.menu_no,  "
        strsql &= " e1.firstname+' '+e1.lastname updated_by,m.updated_date,m.menu_url,m1.module_name,m1.module_id "
        strsql &= " from menu m inner join employee e "
        strsql &= " on m.creation_by=e.empid "
        strsql &= " inner join employee e1 "
        strsql &= " on m.updated_by=e1.empid "
        strsql &= " inner join module m1 "
        strsql &= " on m.module_id=m1.module_id "

        If ddlModule.SelectedValue <> "0" Then
            strsql &= " and m.module_id='" & ddlModule.SelectedValue & "' "
        End If

        If txtStatus.Text <> "" Then
            strsql &= " and m.menu_name like '%" & txtStatus.Text & "%'"
        End If

        If txtStatus0.Text <> "" Then
            strsql &= " and m.menu_no like '%" & txtStatus0.Text & "%'"
        End If

        If txtStatus1.Text <> "" Then
            strsql &= " and m.menu_url like '%" & txtStatus1.Text & "%'"
        End If

        Dim DT As DataTable = MD.GetDataTable(strsql)
        DV = DT.DefaultView
        DV.Sort = "[" & ViewState("sortfield") & "] " & ViewState("sortdirection")
        Session("menu") = DV
    End Sub
    Private Sub MyGridBind()
        GridView1.DataSource = DV
        Dim X1() As String = {"menu_id"}
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
                Dim L1 As ImageButton = e.Row.Cells(9).Controls(1)
                L1.OnClientClick = "return confirm('คุณต้องการลบข้อมูลใช่หรือไม่?');"
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

        Dim Y As Integer = MD.Execute("delete from menu where menu_id ='" & K1(0) & "'")
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
        Dim K1 As DataKey = GridView1.DataKeys(e.NewEditIndex)
        Dim lModule As Label = GridView1.Rows(e.NewEditIndex).Cells(0).Controls(1)
        Dim lName As Label = GridView1.Rows(e.NewEditIndex).Cells(2).Controls(1)
        Dim lno As Label = GridView1.Rows(e.NewEditIndex).Cells(3).Controls(1)
        Dim lurl As Label = GridView1.Rows(e.NewEditIndex).Cells(4).Controls(1)

        lblId.Text = K1(0).ToString

        ddlModule.SelectedValue = lModule.Text
        txtStatus.Text = lName.Text
        txtStatus0.Text = lno.Text
        txtStatus1.Text = lurl.Text

        lblStatus.Text = "Edit"
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
    Private Sub ClearAlert()
        ddlModule.SelectedValue = "0"
        lblIDmenu.Text = ""
        lblMenu.Text = ""
        lblURL.Text = ""

    End Sub
    Protected Sub bSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bSave.Click
        Dim strsql As String = ""
        Dim sEmpNo As String = Session("EmpNo")

        If ddlModule.SelectedValue = "0" Then
            Me.ClearAlert()
            lblModule.Text = "***กรุณาเลือกโมดูล"
            Exit Sub
        End If

        If txtStatus.Text.Trim = "" Then
            Me.ClearAlert()
            lblMenu.Text = "***กรุณากรอกเมนู"
            Exit Sub
        End If

        If txtStatus0.Text.Trim = "" Then
            Me.ClearAlert()
            lblIDmenu.Text = "***กรุณากรอกรหัสเมนู"
            Exit Sub
        End If

        If txtStatus1.Text.Trim = "" Then
            Me.ClearAlert()
            lblURL.Text = "***กรุณากรอกURL"
            Exit Sub
        End If



        If lblStatus.Text = "Edit" Then

            'Edit Data
            Try
                strsql = "update menu set "
                strsql &= "module_id=?,menu_name=?,menu_no=?,menu_url=?,updated_by=?,updated_date=? "
                strsql &= "where menu_id=?"


                cn = New OleDbConnection(MD.Strcon)
                cmd = New OleDbCommand(strsql, cn)
                MD.CreateParam(cmd, "TTTTTDT")

                cmd.Parameters("@P1").Value = ddlModule.SelectedValue
                cmd.Parameters("@P2").Value = txtStatus.Text
                cmd.Parameters("@P3").Value = txtStatus0.Text
                cmd.Parameters("@P4").Value = txtStatus1.Text
                cmd.Parameters("@P5").Value = sEmpNo
                cmd.Parameters("@P6").Value = DateTime.Parse(Date.Now)
                cmd.Parameters("@P7").Value = lblId.Text

                cn.Open()
                cmd.ExecuteNonQuery()

                MC.MessageBox(Me, "บันทึกลงฐานข้อมูลเรียบร้อยแล้ว")

                ddlModule.SelectedValue = "0"
                lblId.Text = ""
                txtStatus.Text = ""
                txtStatus0.Text = ""
                txtStatus1.Text = ""
                lblStatus.Text = ""

                Me.gData()
                Me.MyGridBind()

                'Save To Error_Log
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

            'Insert data

            Try
                strsql = "insert into menu (module_id,menu_name,menu_id,menu_no,menu_url,creation_by,created_date,updated_by,updated_date)"
                strsql &= " Values (?,?,?,?,?,?,?,?,?)"


                cn = New OleDbConnection(MD.Strcon)
                cmd = New OleDbCommand(strsql, cn)
                MD.CreateParam(cmd, "TTTTTTDTD")

                cmd.Parameters("@P1").Value = ddlModule.SelectedValue
                cmd.Parameters("@P2").Value = txtStatus.Text
                cmd.Parameters("@P3").Value = lblId.Text
                cmd.Parameters("@P4").Value = txtStatus0.Text
                cmd.Parameters("@P5").Value = txtStatus1.Text
                cmd.Parameters("@P6").Value = sEmpNo
                cmd.Parameters("@P7").Value = DateTime.Parse(Date.Now)
                cmd.Parameters("@P8").Value = sEmpNo
                cmd.Parameters("@P9").Value = DateTime.Parse(Date.Now)

                cn.Open()
                cmd.ExecuteNonQuery()

                MC.MessageBox(Me, "บันทึกลงฐานข้อมูลเรียบร้อยแล้ว")

                ddlModule.SelectedValue = "0"
                lblId.Text = ""
                txtStatus.Text = ""
                txtStatus0.Text = ""
                txtStatus1.Text = ""
                lblStatus.Text = ""

                Me.gData()
                Me.MyGridBind()

                'Save To Error_Log

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
      
    End Sub
    Protected Sub bCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bCancel.Click
        ddlModule.SelectedValue = "0"
        txtStatus.Text = ""
        txtStatus0.Text = ""
        txtStatus1.Text = ""
        lblStatus.Text = ""

    End Sub

 
    Protected Sub bSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bSearch.Click
        Me.gData()
        Me.MyGridBind()
    End Sub
End Class
