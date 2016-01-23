Imports System.Data
Imports System.Data.OleDb
Partial Class Src_Permission2
    Inherits System.Web.UI.Page
    Dim cn As OleDbConnection
    Dim cmd As OleDbCommand
    Dim MD As New MainData
    Dim MC As New MainClass
    Dim DV As DataView
    Public iRow As Integer
    Dim DS As DataSet
    Dim iRec As Integer
    Private Sub ChkName()
        Dim id As String = Request.QueryString("id")
        Dim X As String = Session("EmpNo")

        Dim str As String
        str = "select * from groupbase where group_id='" & id & "' "
        Dim oDs As DataSet
        oDs = MD.GetDataset(str)
        If oDs.Tables(0).Rows.Count > 0 Then
            lblTxt.Text = " ( " + oDs.Tables(0).Rows(0).Item("group_name").ToString + " ) "
        End If

      
    End Sub
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

        'Me.ChkPermis()

        If Not Page.IsPostBack Then
            ViewState("sortfield") = "module_name"
            ViewState("sortdirection") = "asc"
            Me.DataList()

            Me.ChkName()
            Me.gData("0")
            Me.MyGridBind()
        Else
            If Session("menu") Is Nothing Then
                Me.gData("0")
            Else
                DV = Session("menu")
            End If
        End If
    End Sub
    Private Sub DataList()
        Dim strsql As String
        strsql = "select module_id,module_name from module order by module_name "

        Dim DTS As DataTable = MD.GetDataTable(strsql)
        Dim dr As DataRow = DTS.NewRow
        dr("module_id") = 0
        dr("module_name") = "All"
        DTS.Rows.InsertAt(dr, 0)
        DataListModule.DataSource = DTS
        DataListModule.DataKeyField = "module_id"
        DataListModule.DataBind()
    End Sub
    Public Function ImagesGet(ByVal X As String) As String
        Dim X1 As String = Replace(X, " ", "_")
        Dim X2 As String = "..\Images\" & X1 & ".jpg"
        Dim xFile As String = Server.MapPath(X2)

        If IO.File.Exists(xFile) Then
            Return "<img src='" & Replace(X2, "\", "/") & "' align='absmiddle'>"
        Else
            Return ""
        End If
    End Function
    Private Sub gData(ByVal module_id As String)

        Dim X As String = Request.QueryString("id")
        Dim strsql As String

        strsql = "select m.menu_id,m.menu_name,md.module_name,  "
        strsql &= " case when s.chk=1 then 'OK' else 'NO' end chk "
        strsql &= " from menu m inner join module md "
        strsql &= " on m.module_id=md.module_id "
        strsql &= " inner join module m1 "
        strsql &= " on m.module_id=m1.module_id left join "
        strsql &= " (select menu_id,'1' chk from permission where group_id='" & X & "') s "
        strsql &= " on m.menu_id = s.menu_id "


        If module_id <> "0" Then
            strsql &= "where m.module_id= '" & module_id & "' "
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
    Private Sub Auto()

        Dim sqlTmp As String = ""
        Dim comTmp As OleDbCommand = New OleDbCommand
        Dim drTmp As OleDbDataReader
        Dim tmpMemberID As Integer = 0
        Dim tmpMemberID2 As String = ""


        sqlTmp = "SELECT TOP 1 permiss_id FROM permission "
        sqlTmp &= " ORDER BY permiss_id DESC"

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

                tmpMemberID2 = drTmp.Item("permiss_id")

                tmpMemberID = CInt(tmpMemberID2) + 1
                lblId.Text = tmpMemberID.ToString
            End With
        Catch
            lblId.Text = "1"
        End Try
        cn.Close()

    End Sub
    Private Sub SaveData(ByVal menu_id As String)
        'Insert data
        Dim X As String = Request.QueryString("id")
        Dim sEmpNo As String = Session("EmpNo")
        Dim strsql As String = ""
        Me.Auto()



        Try
            strsql = "insert into permission (permiss_id,group_id,menu_id,isview,isupdate)"
            strsql &= " Values (?,?,?,?,?)"


            cn = New OleDbConnection(MD.Strcon)
            cmd = New OleDbCommand(strsql, cn)
            MD.CreateParam(cmd, "TTTTT")

            cmd.Parameters("@P1").Value = lblId.Text
            cmd.Parameters("@P2").Value = X
            cmd.Parameters("@P3").Value = menu_id
            cmd.Parameters("@P4").Value = "T"
            cmd.Parameters("@P5").Value = "T"

            cn.Open()
            cmd.ExecuteNonQuery()
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
    Private Sub DeleteData(ByVal X As String)
        Dim del As String
        del = "delete from permission where permiss_id ='" & X & "'"
        MD.Execute(del)
    End Sub
    Protected Sub DataListStatus_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataListModule.SelectedIndexChanged
        Dim X As String = DataListModule.DataKeys(DataListModule.SelectedIndex)

        Me.gData(X)
        GridView1.PageIndex = 0
        Me.MyGridBind()
    End Sub
    Protected Sub GridView1_SelectedIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSelectEventArgs) Handles GridView1.SelectedIndexChanging

        Dim DDL As String = DataListModule.SelectedValue
        Dim X As String = Request.QueryString("id")
        Dim K1 As DataKey = GridView1.DataKeys(e.NewSelectedIndex)
        Dim chk As String
        Dim oDs As DataSet
        Dim txtActive As String = ""
        Dim txtAlert As String = ""

        chk = "select * from permission where group_id='" & X & "' and menu_id ='" & K1(0) & "' "

        oDs = MD.GetDataset(chk)

        If oDs.Tables(0).Rows.Count > 0 Then
            Me.DeleteData(oDs.Tables(0).Rows(0).Item("permiss_id").ToString)
       
        Else
            Me.SaveData(K1(0).ToString)

        End If
        Me.gData(DDL)
        Me.MyGridBind()
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
        DV.Sort = "[" & ViewState("sortfield") & "] " & ViewState("sortdirection")
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

                'Dim L1 As LinkButton = e.Row.Cells(13).Controls(0)
                'L1.OnClientClick = "return confirm('คุณต้องการลบข้อมูลใช่หรือไม่?');"


            End If

        End If

    End Sub

End Class


