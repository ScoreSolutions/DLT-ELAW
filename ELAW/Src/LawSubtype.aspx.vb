﻿Imports System.Data
Imports System.Data.OleDb
Partial Class Src_LawSubtype
    Inherits System.Web.UI.Page
    Dim cn As OleDbConnection
    Dim cmd As OleDbCommand
    Dim MD As New MainData
    Dim MC As New MainClass
    Dim DV As DataView
    Public iRow As Integer
    Dim DS As DataSet
    Dim iRec As Integer
    Function GetRefNo() As String
        'หาค่า Ref_No จาก Law_Type
        Dim str As String
        Dim oDs As DataSet
        str = "select * from Law_Type where type_id='" & ddlLawType.SelectedValue & "'"
        oDs = MD.GetDataset(str)
        If oDs.Tables(0).Rows.Count > 0 Then
            Return oDs.Tables(0).Rows(0).Item("ref_no").ToString
        End If
        Return "00"
    End Function

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
            ViewState("sortfield") = "subtype_name"
            ViewState("sortdirection") = "asc"
            Me.DataLawType()
            Me.gData() 'เรียกใช้ฟังก์ชั่นการแสดงข้อมูล
            Me.MyGridBind()
        Else
            If Session("law_subtype") Is Nothing Then
                Me.gData()
            Else
                DV = Session("law_subtype")
            End If
        End If
    End Sub
    Private Sub Auto() 'ฟังก์ชั่นการกำหนดรหัสของข้อมูลโดยเพิ่มขึ้นจากเดิมทีละ 1  
        Dim sqlTmp As String = ""
        Dim comTmp As OleDbCommand = New OleDbCommand
        Dim drTmp As OleDbDataReader
        Dim tmpMemberID As Integer = 0
        Dim tmpMemberID2 As String = ""

        sqlTmp = "SELECT TOP 1 subtype_id FROM law_subtype "
        sqlTmp &= " ORDER BY subtype_id DESC"

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

                tmpMemberID2 = drTmp.Item("subtype_id")

                tmpMemberID = CInt(tmpMemberID2) + 1
                lblId.Text = tmpMemberID.ToString

            End With
        Catch
            lblId.Text = "1"
        End Try
        cn.Close()

    End Sub
    Public Sub DataLawType()

        Dim strsql As String
        strsql = "select type_id,type_name from law_type order by type_id  "

        Dim DTS As DataTable = MD.GetDataTable(strsql)
        Dim dr As DataRow = DTS.NewRow
        dr!type_id = 0
        dr!type_name = "---------โปรดเลือก---------"
        DTS.Rows.InsertAt(dr, 0)
        ddlLawType.DataTextField = "type_name"
        ddlLawType.DataValueField = "type_id"
        ddlLawType.DataSource = DTS
        ddlLawType.DataBind()

    End Sub

    Private Sub gData() 'ฟังก์ชั่นการเรียกข้อมูลมาแสดง

        Dim strsql As String
        strsql = "select l.type_id,l.subtype_id,l.subtype_name,e.firstname+' '+e.lastname creation_by,l.created_date, "
        strsql &= " e1.firstname+' '+e1.lastname updated_by,l.updated_date,l1.type_name,l.ref_no  "
        strsql &= " from law_subtype l inner join employee e  "
        strsql &= " on l.creation_by=e.empid "
        strsql &= " inner join employee e1 "
        strsql &= " on l.updated_by=e1.empid "
        strsql &= " inner join law_type l1 "
        strsql &= " on l.type_id = l1.type_id "

        If ddlLawType.SelectedValue <> "0" Then
            strsql &= " and l.type_id='" & ddlLawType.SelectedValue & "' "
        End If

        If txtStatus.Text <> "" Then
            strsql &= " and l.subtype_name like '%" & txtStatus.Text & "%'"
        End If

        Dim DT As DataTable = MD.GetDataTable(strsql)
        DV = DT.DefaultView
        DV.Sort = "[" & ViewState("sortfield") & "] " & ViewState("sortdirection")
        Session("law_subtype") = DV
    End Sub
    Private Sub MyGridBind() 'ฟังก์ชั่นการแสดงข้อมูลในตาราง
        GridView1.DataSource = DV
        Dim X1() As String = {"subtype_id"}
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
        Dim Y As Integer = MD.Execute("delete from law_subtype where subtype_id ='" & K1(0) & "'")
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
        ddlLawType.SelectedValue = ldept.Text
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
        lblLawType.Text = ""
        lblLawSubtype.Text = ""
     
    End Sub
    Protected Sub bSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bSave.Click
        'การบันทึกข้อมูล
        Dim strsql As String = ""
        Dim sEmpNo As String = Session("EmpNo")
        If ddlLawType.SelectedValue = "0" Then
            Me.ClearAlert()
            lblLawType.Text = "กรุณาเลือกประเภทกฎหมาย"
            Exit Sub
        End If

        If txtStatus.Text.Trim = "" Then
            Me.ClearAlert()
            lblLawSubtype.Text = "กรุณากรอกประเภทย่อยกฎหมาย"
            txtStatus.Focus()
            Exit Sub
        End If

        Dim refno As String = Me.GetRefNo

        If lblStatus.Text = "Edit" Then
            'การบันทึกข้อมูลเมื่อมีฟังก์ชั่นการแก้ไข
            Try
                strsql = "update law_subtype set "
                strsql &= "type_id=?,subtype_name=?,ref_no=?,updated_by=?,updated_date=? "
                strsql &= "where subtype_id=?"

                cn = New OleDbConnection(MD.Strcon)
                cmd = New OleDbCommand(strsql, cn)
                MD.CreateParam(cmd, "TTTTDT")

                cmd.Parameters("@P1").Value = ddlLawType.SelectedValue
                cmd.Parameters("@P2").Value = txtStatus.Text
                cmd.Parameters("@P3").Value = refno
                cmd.Parameters("@P4").Value = sEmpNo
                cmd.Parameters("@P5").Value = DateTime.Parse(Date.Now)
                cmd.Parameters("@P6").Value = lblId.Text

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
                strsql = "insert into law_subtype (subtype_id,type_id,subtype_name,ref_no,creation_by,created_date,updated_by,updated_date)"
                strsql &= " Values (?,?,?,?,?,?,?,?)"

                cn = New OleDbConnection(MD.Strcon)
                cmd = New OleDbCommand(strsql, cn)
                MD.CreateParam(cmd, "TTTTTDTD")

                cmd.Parameters("@P1").Value = lblId.Text
                cmd.Parameters("@P2").Value = ddlLawType.SelectedValue
                cmd.Parameters("@P3").Value = txtStatus.Text
                cmd.Parameters("@P4").Value = refno
                cmd.Parameters("@P5").Value = sEmpNo
                cmd.Parameters("@P6").Value = DateTime.Parse(Date.Now)
                cmd.Parameters("@P7").Value = sEmpNo
                cmd.Parameters("@P8").Value = DateTime.Parse(Date.Now)

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
        ddlLawType.SelectedIndex = 0
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



