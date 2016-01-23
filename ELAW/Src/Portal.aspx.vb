Imports System.Data
Imports System.Data.SqlClient
Partial Class Src_Portal
    Inherits System.Web.UI.Page
    Dim MC As New MainClass
    Dim MD As New MainData
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim sEmpNo As String = Page.User.Identity.Name
        Dim MD As New MainData


        Dim assTo As String = ""    'กรณีมีการมอบหมายงาน
        assTo += " or (a.assign_to='" & sEmpNo & "' " & vbNewLine
        assTo += "     and convert(nvarchar(10),getdate(),120) between convert(nvarchar(10),a.date_from,120) and convert(nvarchar(10),a.date_to,120))" & vbNewLine

        'เมนูย่อย
        Dim sqlMu As String = ""
        sqlMu += " select distinct m.module_id, md.module_name, m.menu_id, m.menu_name, m.menu_url, m.sort_by" & vbNewLine
        sqlMu += " from  menu m " & vbNewLine
        sqlMu += " inner join module md on md.module_id=m.module_id" & vbNewLine
        sqlMu += " inner join PERMISSION p on m.menu_id=p.menu_id" & vbNewLine
        sqlMu += " inner join USERGROUP ug on ug.group_id=p.group_id " & vbNewLine
        sqlMu += " inner join USERS u on u.empid=ug.empid " & vbNewLine
        sqlMu += " left join AUTHORIZE a on m.menu_id=a.menu_id " & vbNewLine
        sqlMu += " where p.isview = 'T' and m.active='1' " & vbNewLine
        sqlMu += " and u.loginname='" & sEmpNo & "' " & vbNewLine
        sqlMu += assTo
        sqlMu += " order by m.sort_by" & vbNewLine
        Dim dtM As DataTable = MD.GetDataTable(sqlMu)

        Dim dt As New DataTable
        dt = dtM.Clone

        For i As Integer = 0 To dtM.DefaultView.Count - 1
            Dim rd As DataRow = dtM.Rows.Item(i)
            Dim MenuID As Double = Convert.ToDouble(rd("menu_id"))
            Dim MenuName As String = rd("menu_name").ToString()
            Dim MenuUrl As String = rd("menu_url").ToString()

            Dim ImgNew As String = ""     'แสดงรูปภาพ  new หน้าเมนู
            Dim sql As String = ""
            sql += " select id, table_name, conditions "
            sql += " from menu_condition "
            sql += " where menu_id = " & MenuID
            Dim dtI As DataTable = MD.GetDataTable(sql)

            If dtI.Rows.Count = 1 Then
                Dim strSql As String = ""
                strSql += " select count(*) "
                strSql += " from " & dtI.Rows(0)("table_name")
                strSql += " " & dtI.Rows(0)("conditions")
                Dim conn As New SqlConnection(MD.strConnIMP)
                conn.Open()
                Dim cmd As New SqlCommand(strSql, conn)
                Dim parm As New SqlParameter("@sEmpNo", SqlDbType.VarChar, 25)
                parm.Value = Page.User.Identity.Name
                cmd.Parameters.Add(parm)

                If cmd.ExecuteScalar > 0 Then
                    Dim dr As DataRow = dt.NewRow
                    dr("module_id") = dtM.Rows(i)("module_id")
                    dr("module_name") = dtM.Rows(i)("module_name")
                    dr("menu_id") = dtM.Rows(i)("menu_id")
                    dr("menu_name") = dtM.Rows(i)("menu_name")
                    dr("menu_url") = dtM.Rows(i)("menu_url")
                    dr("sort_by") = dtM.Rows(i)("sort_by")
                    dr("menu_name") = dtM.Rows(i)("module_name") & "(" & dr("menu_name") & ")"
                    dt.Rows.Add(dr)
                End If
                conn.Close()
            End If
        Next

        GridView1.DataSource = dt


        Dim X1() As String = {"menu_id"}
        GridView1.DataKeyNames = X1

        GridView1.DataBind()
    End Sub

    Protected Sub GridView1_SelectedIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSelectEventArgs) Handles GridView1.SelectedIndexChanging
        Dim K1 As DataKey = GridView1.DataKeys(e.NewSelectedIndex)
        Dim oDs As New DataSet
        Dim strsql As String

        strsql = "select menu_url from menu where menu_id='" & K1(0) & "'"

        oDs = MD.GetDataset(strsql)
        If oDs.Tables(0).Rows.Count > 0 Then
            Dim mMenu As String = Trim(oDs.Tables(0).Rows(0).Item("menu_url").ToString)
            Response.Redirect("" & mMenu & "")
        End If

        MC.MessageBox(Me, K1(0).ToSring)
    End Sub
End Class
