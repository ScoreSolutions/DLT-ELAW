﻿Imports System.Data
Imports System.Data.OleDb
Partial Class Src_AssignMenu
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
    Private Sub ChkName()
        Dim id As String = Request.QueryString("id")
        Dim X As String = Session("EmpNo")

        Dim str As String
        str = "select firstname+' '+lastname name1 from employee where empid='" & X & "' "
        Dim oDs As DataSet
        oDs = MD.GetDataset(str)
        If oDs.Tables(0).Rows.Count > 0 Then
            lblFrom.Text = oDs.Tables(0).Rows(0).Item("name1").ToString
        End If

        Dim str1 As String
        str1 = "select firstname+' '+lastname name1 from employee where empid='" & id & "' "
        Dim oDs1 As DataSet
        oDs1 = MD.GetDataset(str1)
        If oDs1.Tables(0).Rows.Count > 0 Then
            lblTo.Text = oDs1.Tables(0).Rows(0).Item("name1").ToString
        End If
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'Me.ChkPermis()

        If Not Page.IsPostBack Then
            ViewState("sortfield") = "menu_name"
            ViewState("sortdirection") = "asc"
            Me.ChkName()
            Me.gData()
            Me.MyGridBind()
        Else
            If Session("assign_menu") Is Nothing Then
                Me.gData()
            Else
                DV = Session("assign_menu")
            End If
        End If
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
    Private Sub gData()
        Dim id As String = Request.QueryString("id")
        Dim X As String = Session("EmpNo") 'Request.QueryString("id")
        Dim strsql As String
        Dim chk As String
        chk = "select * from employee where empid='" & X & "' "

        Dim oDs As DataSet
        oDs = MD.GetDataset(chk)
        If oDs.Tables(0).Rows.Count > 0 Then
            Select Case oDs.Tables(0).Rows(0).Item("pos_id").ToString
                Case Constant.DirectorPosID
                    strsql = "  select distinct(m.menu_id),m.menu_name,a.date_from ,a.date_to,"
                    strsql &= " case when a.chk=1 then 'OK' else 'NO' end chk "
                    strsql &= " from menu_assign m left join "
                    strsql &= " (select distinct(menu_id),'1' chk,convert(nvarchar(10),date_from,120)date_from ,convert(nvarchar(10),date_to,120) date_to "
                    strsql &= " from AUTHORIZE  where assign_from ='" & X & "' and assign_to ='" & id & "'  "
                    strsql &= " and convert(nvarchar(10),getdate(),120) between convert(nvarchar(10),date_from,120) and convert(nvarchar(10),date_to,120) "
                    strsql &= " and status_id in (select status_id from menu_assign where level_id=2))a"
                    strsql &= " on m.menu_id =a.menu_id "
                    strsql &= " where m.level_id = 2 "

                Case 2, 3, 5, 6
                    strsql = "  select distinct(m.menu_id),m.menu_name,a.date_from ,a.date_to,"
                    strsql &= " case when a.chk=1 then 'OK' else 'NO' end chk  "
                    strsql &= " from menu_assign m left join "
                    strsql &= " (select distinct(menu_id),'1' chk,convert(nvarchar(10),date_from,120)date_from ,convert(nvarchar(10),date_to,120) date_to "
                    strsql &= " from AUTHORIZE  where assign_from ='" & X & "' and assign_to ='" & id & "'  "
                    strsql &= " and convert(nvarchar(10),getdate(),120) between convert(nvarchar(10),date_from,120) and convert(nvarchar(10),date_to,120) "
                    strsql &= " and status_id in (select status_id from menu_assign where level_id=1))a"
                    strsql &= " on m.menu_id =a.menu_id "
                    strsql &= " where m.level_id = 1 "
                Case Else
                    strsql = "  select distinct(m.menu_id),m.menu_name,a.date_from ,a.date_to,"
                    strsql &= " case when a.chk=1 then 'OK' else 'NO' end chk "
                    strsql &= " from menu_assign m left join "
                    strsql &= " (select distinct(menu_id),'1' chk,convert(nvarchar(10),date_from,120)date_from ,convert(nvarchar(10),date_to,120) date_to "
                    strsql &= " from AUTHORIZE  where assign_from ='" & X & "' and assign_to ='" & id & "'  "
                    strsql &= " and convert(nvarchar(10),getdate(),120) between convert(nvarchar(10),date_from,120) and convert(nvarchar(10),date_to,120) "
                    strsql &= " and status_id in (select status_id from menu_assign where level_id=0))a"
                    strsql &= " on m.menu_id =a.menu_id "
                    strsql &= " where m.level_id = 0 "
            End Select
        End If

        Dim DT As DataTable = MD.GetDataTable(strsql)
        DV = DT.DefaultView
        DV.Sort = "[" & ViewState("sortfield") & "] " & ViewState("sortdirection")
        Session("assign_menu") = DV

    End Sub
    Private Sub MyGridBind()
        GridView1.DataSource = DV
        Dim X1() As String = {"menu_id"}
        GridView1.DataKeyNames = X1
        GridView1.DataBind()
    End Sub
    Private Sub DeleteData(ByVal X As String)
        Dim del As String
        del = "delete from permission where permiss_id ='" & X & "'"
        MD.Execute(del)
    End Sub
    Protected Sub GridView1_SelectedIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSelectEventArgs) Handles GridView1.SelectedIndexChanging
        Dim K1 As DataKey = GridView1.DataKeys(e.NewSelectedIndex)

        Dim id As String = Request.QueryString("id")
        Dim X As String = Session("EmpNo")
        Dim strsql As String
        Dim status As String
        Dim process As String
        Dim chk As String
        Dim oDs As DataSet

        process = "select * from authorize where menu_id='" & K1(0) & "' and assign_from ='" & X & "'  "

        oDs = MD.GetDataset(process)

        If oDs.Tables(0).Rows.Count > 0 Then
            process = "select * from authorize where menu_id='" & K1(0) & "' and assign_from ='" & X & "' and assign_to='" & id & "' "

            oDs = MD.GetDataset(process)

            If oDs.Tables(0).Rows.Count > 0 Then
                chk = "select * from employee where empid='" & X & "' "


                oDs = MD.GetDataset(chk)
                If oDs.Tables(0).Rows.Count > 0 Then
                    Select Case oDs.Tables(0).Rows(0).Item("pos_id").ToString
                        Case 4
                            status = "select status_id from menu_assign where menu_id='" & K1(0) & "' and level_id=2 "
                            Dim a1 As New OleDbDataAdapter(status, MD.Strcon)
                            Dim s1 As New DataSet()
                            a1.Fill(s1)

                            For Each dr As DataRow In s1.Tables(0).Rows
                                strsql = "delete from authorize   "
                                strsql &= "where menu_id='" & K1(0) & "' and status_id='" & dr(0).ToString() & "' and assign_from='" & X & "' and assign_to='" & id & "'  "
                                MD.Execute(strsql)
                            Next
                        Case 2, 3, 5, 6
                            status = "select status_id from menu_assign where menu_id='" & K1(0) & "' and level_id=1 "
                            Dim a1 As New OleDbDataAdapter(status, MD.Strcon)
                            Dim s1 As New DataSet()
                            a1.Fill(s1)

                            For Each dr As DataRow In s1.Tables(0).Rows
                                strsql = "delete from authorize   "
                                strsql &= "where menu_id='" & K1(0) & "' and status_id='" & dr(0).ToString() & "' and assign_from='" & X & "' and assign_to='" & id & "'  "
                                MD.Execute(strsql)
                            Next

                    End Select
                End If
            Else
                '    chk = "select * from employee where empid='" & X & "' "


                '    oDs = MD.GetDataset(chk)
                '    If oDs.Tables(0).Rows.Count > 0 Then
                '        Select Case oDs.Tables(0).Rows(0).Item("pos_id").ToString
                '            Case 4
                '                status = "select status_id from menu_assign where menu_id='" & K1(0) & "' and level_id=2 "
                '                Dim a1 As New OleDbDataAdapter(status, MD.Strcon)
                '                Dim s1 As New DataSet()
                '                a1.Fill(s1)

                '                For Each dr As DataRow In s1.Tables(0).Rows
                '                    strsql = "insert into authorize (menu_id,status_id,assign_from,assign_to,date_from,date_to)  "
                '                    strsql &= "values ('" & K1(0) & "','" & dr(0).ToString() & "','" & X & "','" & id & "',getdate(),getdate()+2)  "
                '                    MD.Execute(strsql)
                '                Next
                '            Case 2, 3, 5, 6
                '                status = "select status_id from menu_assign where menu_id='" & K1(0) & "' and level_id=1 "
                '                Dim a1 As New OleDbDataAdapter(status, MD.Strcon)
                '                Dim s1 As New DataSet()
                '                a1.Fill(s1)

                '                For Each dr As DataRow In s1.Tables(0).Rows
                '                    strsql = "insert into authorize (menu_id,status_id,assign_from,assign_to,date_from,date_to)  "
                '                    strsql &= "values ('" & K1(0) & "','" & dr(0).ToString() & "','" & X & "','" & id & "',getdate(),getdate()+2)  "
                '                    MD.Execute(strsql)
                '                Next

                '        End Select
                '    End If

                MC.MessageBox(Me, "มีการมอบหมายงานนี้ให้ผู้อื่นทำแทนแล้วไม่สามารถมอบหมายซ้ำได้")
                Exit Sub

            End If

        Else

            If txtDateFrom.Text.Year = 1 Or txtDateTo.Text.Year = 1 Then
                MC.MessageBox(Me, "กรุณาระบุวันที่ที่ต้องการมอบหมายงาน")
                Exit Sub
            End If

            If Convert.ToDateTime(txtDateFrom.Text) > Convert.ToDateTime(txtDateTo.Text) Then
                MC.MessageBox(Me, "กรุณาระบุช่วงวันที่ให้ถูกต้อง")
                Exit Sub
            End If

            Dim vDateFrom As String = txtDateFrom.SaveDate
            Dim vDateTo As String = txtDateTo.SaveDate

            chk = "select * from employee where empid='" & X & "' "

            oDs = MD.GetDataset(chk)
            If oDs.Tables(0).Rows.Count > 0 Then
                Select Case oDs.Tables(0).Rows(0).Item("pos_id").ToString
                    Case 4
                        status = "select status_id from menu_assign where menu_id='" & K1(0) & "' and level_id=2 "
                        Dim a1 As New OleDbDataAdapter(status, MD.Strcon)
                        Dim s1 As New DataSet()
                        a1.Fill(s1)

                        For Each dr As DataRow In s1.Tables(0).Rows
                            'strsql = "insert into authorize (menu_id,status_id,assign_from,assign_to,date_from,date_to)  "
                            'strsql &= "values ('" & K1(0) & "','" & dr(0).ToString() & "','" & X & "','" & id & "',getdate(),getdate()+" & ddlDay.Text & ")  "
                            strsql = "insert into authorize (menu_id,status_id,assign_from,assign_to,date_from,date_to)  "
                            strsql &= "values ('" & K1(0) & "','" & dr(0).ToString() & "','" & X & "','" & id & "','" & vDateFrom & "','" & vDateTo & "')  "

                            MD.Execute(strsql)
                        Next
                    Case 2, 3, 5, 6
                        status = "select status_id from menu_assign where menu_id='" & K1(0) & "' and level_id=1 "
                        Dim a1 As New OleDbDataAdapter(status, MD.Strcon)
                        Dim s1 As New DataSet()
                        a1.Fill(s1)

                        For Each dr As DataRow In s1.Tables(0).Rows
                            'strsql = "insert into authorize (menu_id,status_id,assign_from,assign_to,date_from,date_to)  "
                            'strsql &= "values ('" & K1(0) & "','" & dr(0).ToString() & "','" & X & "','" & id & "',getdate(),getdate()+" & ddlDay.Text & ")  "
                            strsql = "insert into authorize (menu_id,status_id,assign_from,assign_to,date_from,date_to)  "
                            strsql &= "values ('" & K1(0) & "','" & dr(0).ToString() & "','" & X & "','" & id & "','" & vDateFrom & "','" & vDateTo & "')  "


                            MD.Execute(strsql)
                        Next

                End Select
            End If
            'MC.MessageBox(Me, "มีการมอบหมายงานนี้ให้ผู้อื่นทำแทนแล้วไม่สามารถมอบหมายซ้ำได้")
            'Exit Sub

        End If
        Me.gData()
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