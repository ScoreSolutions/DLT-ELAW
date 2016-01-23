Imports System.Data
Imports System.Data.OleDb
Partial Class Src_Division
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

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Me.ChkPermis()
        If Not Page.IsPostBack Then
            ViewState("sortfield") = "div_name"
            ViewState("sortdirection") = "asc"

            Me.DataDepartment()
            Me.gData()
            Me.MyGridBind()


        Else
            If Session("division") Is Nothing Then
                Me.gData()
            Else
                DV = Session("division")
            End If
        End If
    End Sub
    Private Sub Auto()

        Dim sqlTmp As String = ""
        Dim comTmp As OleDbCommand = New OleDbCommand
        Dim drTmp As OleDbDataReader
        Dim tmpMemberID As Integer = 0
        Dim tmpMemberID2 As String = ""


        sqlTmp = "SELECT TOP 1 div_id FROM division "
        sqlTmp &= " ORDER BY div_id DESC"

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

                tmpMemberID2 = drTmp.Item("div_id")

                tmpMemberID = CInt(tmpMemberID2) + 1
                lblId.Text = tmpMemberID.ToString

            End With
        Catch
            lblId.Text = "1"
        End Try
        cn.Close()

    End Sub
    Public Sub DataDepartment()

        Dim strsql As String
        strsql = "select dept_id,dept_name from department order by dept_name  "

        Dim DTS As DataTable = MD.GetDataTable(strsql)
        Dim dr As DataRow = DTS.NewRow
        dr!dept_id = 0
        dr!dept_name = "---------โปรดเลือก---------"
        DTS.Rows.InsertAt(dr, 0)
        ddlDept.DataTextField = "dept_name"
        ddlDept.DataValueField = "dept_id"
        ddlDept.DataSource = DTS
        ddlDept.DataBind()

    End Sub

    Private Sub gData()

        Dim strsql As String
        strsql = "select d.dept_id,d.div_id,d.div_name,e.firstname+' '+e.lastname creation_by,d.created_date,  "
        strsql &= " e1.firstname+' '+e1.lastname updated_by,d.updated_date,d1.dept_name  "
        strsql &= " from division d inner join employee e  "
        strsql &= " on d.creation_by=e.empid "
        strsql &= " inner join employee e1 "
        strsql &= " on d.updated_by=e1.empid "
        strsql &= " inner join department d1 "
        strsql &= " on d.dept_id = d1.dept_id "

        If ddlDept.SelectedValue <> "0" Then
            strsql &= "and d.dept_id='" & ddlDept.SelectedValue & "' "
        End If


        If txtStatus.Text <> "" Then
            strsql &= " and d.div_name like '%" & txtStatus.Text & "%'"
        End If


        Dim DT As DataTable = MD.GetDataTable(strsql)
        DV = DT.DefaultView
        DV.Sort = "[" & ViewState("sortfield") & "] " & ViewState("sortdirection")
        Session("division") = DV
    End Sub
    Private Sub MyGridBind()
        GridView1.DataSource = DV
        Dim X1() As String = {"div_id"}
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
                Dim L1 As ImageButton = e.Row.Cells(7).Controls(1)
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
        Dim Y As Integer = MD.Execute("delete from division where div_id ='" & K1(0) & "'")
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
        Dim ldept As Label = GridView1.Rows(e.NewEditIndex).Cells(0).Controls(1)
        Dim lname As Label = GridView1.Rows(e.NewEditIndex).Cells(2).Controls(1)

        lblId.Text = K1(0).ToString
        ddlDept.SelectedValue = ldept.Text
        txtStatus.Text = lname.Text

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
        lblDep.Text = ""
        lblDiv.Text = ""
       
    End Sub
    Protected Sub bSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bSave.Click
        Dim strsql As String = ""
        Dim sEmpNo As String = Session("EmpNo")

        If ddlDept.SelectedValue = "0" Then
            Me.ClearAlert()
            lblDiv.Text = "***กรุณาเลือกหน่วยงาน"
            Exit Sub
        End If

        If txtStatus.Text.Trim = "" Then
            Me.ClearAlert()
            lblDiv.Text = "***กรุณากรอกชื่อส่วนงาน"
            Exit Sub
        End If

        If lblStatus.Text = "Edit" Then

            'Edit Data
            Try
                strsql = "update division set "
                strsql &= "dept_id=?,div_name=?,updated_by=?,updated_date=? "
                strsql &= "where div_id=?"


                cn = New OleDbConnection(MD.Strcon)
                cmd = New OleDbCommand(strsql, cn)
                MD.CreateParam(cmd, "TTTDT")

                cmd.Parameters("@P1").Value = ddlDept.SelectedValue
                cmd.Parameters("@P2").Value = txtStatus.Text
                cmd.Parameters("@P3").Value = sEmpNo
                cmd.Parameters("@P4").Value = DateTime.Parse(Date.Now)
                cmd.Parameters("@P5").Value = lblId.Text

                cn.Open()
                cmd.ExecuteNonQuery()

                MC.MessageBox(Me, "บันทึกลงฐานข้อมูลเรียบร้อยแล้ว")
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
                strsql = "insert into division (div_id,dept_id,div_name,creation_by,created_date,updated_by,updated_date)"
                strsql &= " Values (?,?,?,?,?,?,?)"


                cn = New OleDbConnection(MD.Strcon)
                cmd = New OleDbCommand(strsql, cn)
                MD.CreateParam(cmd, "TTTTDTD")

                cmd.Parameters("@P1").Value = lblId.Text
                cmd.Parameters("@P2").Value = ddlDept.SelectedValue
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
        lblId.Text = ""
        ddlDept.SelectedIndex = 0
        txtStatus.Text = ""
        lblStatus.Text = ""

    End Sub
    Protected Sub bCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bCancel.Click
        txtStatus.Text = ""
        lblStatus.Text = ""

    End Sub

End Class
