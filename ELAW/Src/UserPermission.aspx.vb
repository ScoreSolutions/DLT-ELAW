Imports System.Data
Imports System.Data.OleDb
Partial Class Src_UserPermission
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
            ViewState("sortfield") = "firstname"
            ViewState("sortdirection") = "asc"

            Me.ChkName()

            Me.gData()
            Me.MyGridBind()


        Else
            If Session("employee") Is Nothing Then
                Me.gData()
            Else
                DV = Session("employee")
            End If
        End If
    End Sub
    
    Private Sub gData()

        Dim X As String = Request.QueryString("id")
        Dim strsql As String
        strsql = "select e.empid,p.pos_id,p.pos_name,d1.div_name,firstname,lastname,  "
        strsql &= " d.dept_id,d.dept_name,d.dept_name,"
        strsql &= " d1.div_id,d1.div_name,p.pos_name, "
        strsql &= " case when u.chk=1 then 'OK' else 'NO' end chk "
        strsql &= " from employee e inner join department d  "
        strsql &= " on e.dept_id =  d.dept_id "
        strsql &= " inner join division d1 "
        strsql &= " on e.div_id = d1.div_id "
        strsql &= " inner join position p "
        strsql &= " on e.pos_id = p.pos_id left join "
        strsql &= " (select empid,'1' chk from usergroup where group_id='" & X & "') u "
        strsql &= " on e.empid = u.empid "


        Dim DT As DataTable = MD.GetDataTable(strsql)
        DV = DT.DefaultView
        DV.Sort = "[" & ViewState("sortfield") & "] " & ViewState("sortdirection")
        Session("employee") = DV
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

    Protected Sub GridView1_SelectedIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSelectEventArgs) Handles GridView1.SelectedIndexChanging
        Dim X As String = Request.QueryString("id")
        Dim K1 As DataKey = GridView1.DataKeys(e.NewSelectedIndex)
        Dim chk As String
        Dim oDs As DataSet
        Dim txtActive As String = ""
        Dim txtAlert As String = ""

        chk = "select * from usergroup where group_id='" & X & "' and empid ='" & K1(0) & "' "

        oDs = MD.GetDataset(chk)

        If oDs.Tables(0).Rows.Count > 0 Then
            Me.DeleteData(oDs.Tables(0).Rows(0).Item("empid").ToString)
            Me.gData()
            Me.MyGridBind()
        Else

            Me.SaveData(K1(0).ToString)
            Me.gData()
            Me.MyGridBind()
        End If

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
    Private Sub DeleteData(ByVal X As String)
        'Insert data
        Dim Xid As String = Request.QueryString("id")
        Dim del As String
        del = "delete from usergroup where group_id='" & Xid & "' and empid ='" & X & "'"
        MD.Execute(del)
    End Sub
    
    
    Private Sub SaveData(ByVal empid As String)
        'Insert data
        Dim X As String = Request.QueryString("id")

        Dim sEmpNo As String = Session("EmpNo")
        Dim strsql As String = ""


        Try
            strsql = "insert into usergroup (group_id,empid)"
            strsql &= " Values (?,?)"


            cn = New OleDbConnection(MD.Strcon)
            cmd = New OleDbCommand(strsql, cn)
            MD.CreateParam(cmd, "TT")

            cmd.Parameters("@P1").Value = X
            cmd.Parameters("@P2").Value = empid


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
   
End Class

