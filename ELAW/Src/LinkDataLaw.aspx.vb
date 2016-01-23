Imports System.IO
Imports System.Data
Imports System.Data.OleDb
Partial Class Src_LinkDataLaw
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

            If X <> "" Then
                'Edit

                Dim sql As String
                sql = "select link_id,title,detail,secret "
                sql &= " from linklaw_data "
                sql &= " where link_id='" & X & "' "

                DS = MD.GetDataset(sql)
                Session("link_data") = DS
                iRec = 0
                ViewState("iRec") = iRec

                Me.MyDataBind()
            Else

                Dim sql As String
                sql = "select * "
                sql &= " from linklaw_data "

                DS = MD.GetDataset(sql)
                Session("link_data") = DS
                iRec = 0
                ViewState("iRec") = iRec

                lblMainStatus.Text = "Add"

            End If

            Me.gData()
            Me.MyGridBind()

        Else

            If Session("ChkGrd") = "1" Then
                Me.RefreshPage()
                Session("ChkGrd") = "0"
            End If


            DS = Session("link_data")
            iRec = ViewState("iRec")

            If Session("LinkDoc") Is Nothing Then
                Me.gData()
            Else
                DVLst = Session("LinkDoc")
            End If


            If txtId.Text = "" Then
                lblBstate.Text = "***กรุณาบันทึกข้อมูลก่อน"
                Exit Sub
            Else
                bSelect.Attributes.Add("onclick", "popupwindown('SelectLaw.aspx?id=" & txtId.Text & "');")
                lblBstate.Text = ""
            End If


        End If

    End Sub
    Private Sub RefreshPage()
        Me.gData()
        Me.MyGridBind()
    End Sub
    Private Sub gData(Optional ByVal Type As String = "")
        Dim X As String = Request.QueryString("id")
        Dim strsql As String

        strsql = " select d.link_id,d.doc_id,i.doc_name,l.subtype_name "
        strsql &= "from link_detail d inner join import_document i "
        strsql &= "on d.doc_id=i.doc_id inner join law_subtype l "
        strsql &= "on i.doc_subtype=l.subtype_id "
        strsql &= "where d.link_id='" & txtId.Text & "'"


        Dim DT As DataTable = MD.GetDataTable(strsql)
        DVLst = DT.DefaultView
        DVLst.Sort = "[" & ViewState("sortfield") & "] " & ViewState("sortdirection")
        Session("LinkDoc") = DVLst

    End Sub
    Private Sub MyGridBind()
        GridView1.DataSource = DVLst
        Dim X1() As String = {"doc_id"}
        GridView1.DataKeyNames = X1
        GridView1.DataBind()
    End Sub
    Public Function BindField(ByVal FieldName As String) As String
        'BindField when Preview,Approve,Edit
        Dim DT As DataTable = DS.Tables(0)
        Select Case FieldName
            Case "money"
                If IsDBNull(DT.Rows(iRec)("money")) Then
                    Return "0.00"
                Else
                    Dim P1 As Double = DT.Rows(iRec)("money")
                    Return P1.ToString("#,##0.00")
                End If
            Case "dates_receive"
                If IsDBNull(DT.Rows(iRec)("dates_receive")) Then
                    Return ""
                Else
                    Dim P1 As Date = DT.Rows(iRec)("dates_receive")
                    Return P1.ToString("dd/MM/yyyy")
                End If
            Case "dates_contract"
                If IsDBNull(DT.Rows(iRec)("dates_contract")) Then
                    Return ""
                Else
                    Dim P1 As Date = DT.Rows(iRec)("dates_contract")
                    Return P1.ToString("dd/MM/yyyy")
                End If
            Case Else
                Return DT.Rows(iRec)(FieldName) & ""
        End Select
    End Function
    Public Sub MyDataBind()
        txtId.DataBind()
        txtTitle.DataBind()
        txtDetail.DataBind()

        If DS.Tables(0).Rows(0).Item("secret").ToString = 1 Then
            chkSecret.Checked = True
        Else
            chkSecret.Checked = False
        End If
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
    Private Sub GenAuto()

        Dim sqlTmp As String = ""
        Dim comTmp As OleDbCommand = New OleDbCommand
        Dim drTmp As OleDbDataReader
        Dim tmpMemberID As Integer = 0
        Dim tmpMemberID2 As String = ""

        Dim sAuto As String = "LNK" + Right(Now.Year, 2)

        sqlTmp = "SELECT TOP 1 right(link_id,4) link_id FROM linklaw_data "
        sqlTmp &= "where left(link_id,5)='" & sAuto & "' "
        sqlTmp &= " ORDER BY link_id DESC"

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

                tmpMemberID2 = Right(drTmp.Item("link_id"), 4)

                tmpMemberID = CInt(tmpMemberID2) + 1
                txtId.Text = sAuto + tmpMemberID.ToString("-0000")

            End With
        Catch
            txtId.Text = sAuto + "-0001"
        End Try
        cn.Close()

    End Sub
    Protected Sub bSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bSave.Click
        If txtTitle.Text = "" Then
            lblAtitle.Text = "***กรุณากรอกชื่อเรื่อง"
            lblADetail.Text = ""
            txtTitle.Focus()
            Exit Sub
        End If
        If txtDetail.Text = "" Then
            lblADetail.Text = "***กรุณากรอกรายละเอียด"
            lblAtitle.Text = ""
            txtDetail.Focus()
            Exit Sub
        End If

        If lblMainStatus.Text = "Add" Then
            Me.GenAuto()
            Me.SaveData()
        Else
            Me.EditData()
        End If

    End Sub
    Private Sub SaveData()
        Dim X As String = Request.QueryString("id")
        Dim sEmpNo As String = Session("EmpNo")
        Dim strsql As String

        Try

            strsql = "insert into linklaw_data (link_id,title,detail, "
            strsql &= " creation_by,created_date,updated_by,updated_date,secret )"
            strsql &= " values (?,?,?,?,?,?,?,?) "

            cn = New OleDbConnection(MD.Strcon)
            cmd = New OleDbCommand(strsql, cn)
            MD.CreateParam(cmd, "TTTTDTDT")

            cmd.Parameters("@P1").Value = txtId.Text
            cmd.Parameters("@P2").Value = txtTitle.Text
            cmd.Parameters("@P3").Value = txtDetail.Text
            cmd.Parameters("@P4").Value = sEmpNo
            cmd.Parameters("@P5").Value = DateTime.Parse(Date.Now)
            cmd.Parameters("@P6").Value = sEmpNo
            cmd.Parameters("@P7").Value = DateTime.Parse(Date.Now)

            If chkSecret.Checked = True Then
                cmd.Parameters("@P8").Value = "1"
            Else
                cmd.Parameters("@P8").Value = "0"
            End If


            cn.Open()
            cmd.ExecuteNonQuery()
            MC.MessageBox(Me, "บันทึกลงฐานข้อมูลเรียบร้อยแล้ว")
            lblMainStatus.Text = "Edit"



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
    Private Sub EditData()
        Dim X As String = Request.QueryString("id")
        Dim sEmpNo As String = Session("EmpNo")
        Dim strsql As String

        Try

            strsql = "update linklaw_data set title=?,detail=?, "
            strsql &= " updated_by=?,updated_date=?,secret=? "
            strsql &= " where link_id=? "

            cn = New OleDbConnection(MD.Strcon)
            cmd = New OleDbCommand(strsql, cn)
            MD.CreateParam(cmd, "TTTDTT")


            cmd.Parameters("@P1").Value = txtTitle.Text
            cmd.Parameters("@P2").Value = txtDetail.Text
            cmd.Parameters("@P3").Value = sEmpNo
            cmd.Parameters("@P4").Value = DateTime.Parse(Date.Now)

            If chkSecret.Checked = True Then
                cmd.Parameters("@P5").Value = "1"
            Else
                cmd.Parameters("@P5").Value = "0"
            End If

            cmd.Parameters("@P6").Value = txtId.Text

            cn.Open()
            cmd.ExecuteNonQuery()
            MC.MessageBox(Me, "บันทึกลงฐานข้อมูลเรียบร้อยแล้ว")
            lblMainStatus.Text = "Add"


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
