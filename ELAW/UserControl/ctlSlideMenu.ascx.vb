Imports System.Data.SqlClient
Imports System.Data

Partial Class UserControl_ctlSlideMenu
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If IsPostBack = False Then
        Dim url As String = Request.Url.AbsolutePath
        url = url.Substring(url.LastIndexOf("/"))

        Dim ExpandModuleID As Double = 0
        Dim ExpandMenuID As Double = 0
        Dim sql As String = ""
        sql += "select m.menu_id, md.module_id "
        sql += " from menu m"
        sql += " inner join module md on md.module_id=m.module_id"
        sql += " where rtrim(m.menu_url) like '%" & url & "'"

        Dim MD As New MainData
        Dim conn As New SqlConnection(MD.strConnIMP)
        conn.Open()
        Dim cmd As New SqlCommand(sql, conn)

        Dim rd As SqlDataReader = cmd.ExecuteReader
        If rd.Read Then
            ExpandModuleID = Convert.ToDouble(rd("module_id"))
            ExpandMenuID = Convert.ToDouble(rd("menu_id"))
        End If
        conn.Close()

        SetMenu(ExpandModuleID, ExpandMenuID)

        'End If
    End Sub

    Private Sub SetMenu(ByVal ExpandModuleID As Double, ByVal ExpandMenuID As Double)
        Dim sEmpNo As String = Page.User.Identity.Name
        Dim itemFocusHighlighColor As String = "#FFFF00"   'สีของข้อความในเมนูที่กำลังทำงานอยู่
        Dim itemBG As String = "#015367"    'สีของพื้นหลังของเมนูย่อย
        Dim itemOverHighligh As String = "#015367"  'สีของเมนูย่อยตอนที่เอาเมาร์ไปชี้
        Dim itemOverColor As String = "#ffffff" 'สีของข้อความของเมนูย่อยเมื่อเอาเมาร์ไปชี้ #4D4D4D

        Dim ret As String = ""
        ret += "<script type='text/javascript'> " & vbNewLine
        ret += " stBM(0,'tree6ed7',[0,'','','../Images/Menu/blank.gif',0,'left','default','hand',1,0,-1,-1,-1,'none',0,'#0066CC','" & itemBG & "','','no-repeat',1,'../Images/Menu/button_01.gif','../Images/Menu/button_02.gif',10,10,0,'line_def0.gif','line_def1.gif','line_def2.gif','line_def3.gif',1,0,5,3,'left',0,0]);" & vbNewLine
        ret += " stBS('p0',[0,0]);" & vbNewLine

        Dim assTo As String = ""    'กรณีมีการมอบหมายงาน
        assTo += " or (a.assign_to='" & sEmpNo & "' " & vbNewLine
        assTo += "     and convert(nvarchar(10),getdate(),120) between convert(nvarchar(10),a.date_from,120) and convert(nvarchar(10),a.date_to,120))" & vbNewLine

        Dim l1 As Integer = 0
        Dim MD As New MainData
        'เมนูหลัก
        Dim sqlMo As String = ""
        sqlMo = "select distinct mo.module_id, mo.module_name" & vbNewLine
        sqlMo += " from MODULE mo " & vbNewLine
        sqlMo += " inner join menu m on m.module_id=mo.module_id" & vbNewLine
        sqlMo += " inner join PERMISSION p on m.menu_id=p.menu_id" & vbNewLine
        sqlMo += " inner join USERGROUP ug on ug.group_id=p.group_id " & vbNewLine
        sqlMo += " inner join USERS u on u.empid=ug.empid " & vbNewLine
        sqlMo += " left join AUTHORIZE a on m.menu_id=a.menu_id " & vbNewLine
        sqlMo += " where u.loginname='" & sEmpNo & "' and p.isview = 'T' and m.active='1'" & vbNewLine
        sqlMo += assTo
        Dim dt As DataTable = MD.GetDataTable(sqlMo)

        'เมนูย่อย
        Dim sqlMu As String = ""
        sqlMu += " select distinct m.module_id, m.menu_id, m.menu_name, m.menu_url, m.sort_by" & vbNewLine
        sqlMu += " from  menu m " & vbNewLine
        sqlMu += " inner join PERMISSION p on m.menu_id=p.menu_id" & vbNewLine
        sqlMu += " inner join USERGROUP ug on ug.group_id=p.group_id " & vbNewLine
        sqlMu += " inner join USERS u on u.empid=ug.empid " & vbNewLine
        sqlMu += " left join AUTHORIZE a on m.menu_id=a.menu_id " & vbNewLine
        sqlMu += " where p.isview = 'T' and m.active='1' " & vbNewLine
        sqlMu += " and u.loginname='" & sEmpNo & "' " & vbNewLine
        sqlMu += assTo
        sqlMu += " order by m.sort_by" & vbNewLine
        Dim dtM As DataTable = MD.GetDataTable(sqlMu)

        For Each r As DataRow In dt.Rows
            Dim ModuleId As Double = Convert.ToDouble(r("module_id"))
            Dim ModuleName As String = r("module_name").ToString()

            'เมนูหลัก
            ret += " stIT('p0i" & l1 & "',['" & ModuleName & "','#','_self','','','','',0,0," & Chr(34) & "bold 9pt 'Tahoma'" & Chr(34) & ",'#FFFFFF','none','transparent','../Images/Menu/bg_01.gif','repeat-x'," & Chr(34) & "bold 9pt 'Tahoma'" & Chr(34) & ",'#000000','none','transparent','../Images/Menu/bg_01.gif','repeat-x'," & Chr(34) & "bold 9pt 'Tahoma'" & Chr(34) & ",'#FFFFFF','none','transparent','../Images/Menu/bg_01.gif','repeat-x'," & Chr(34) & "bold 9pt 'Tahoma'" & Chr(34) & ",'#000000','none','transparent','../Images/Menu/bg_01.gif','repeat-x',1,0,'left','middle',120,10]);" & vbNewLine
            ret += " stBS('p" & l1 & "',[0," & IIf(ModuleId = ExpandModuleID, "1", "0") & "],'p0');" & vbNewLine

            'เมนูย่อย
            Dim li As Integer = 0
            dtM.DefaultView.RowFilter = "module_id = " & ModuleId
            For i As Integer = 0 To dtM.DefaultView.Count - 1
                Dim rd As DataRowView = dtM.DefaultView.Item(i)
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
                        ImgNew = "../Images/new.gif"
                    End If
                    conn.Close()
                End If

                'ret += " stIT('p1i0',['" & MenuName & "','" & MenuUrl & "',,,,,,,,,'#FFFFFF',,,'','no-repeat',,'#FFFFFF','underline',,'','no-repeat',,,,,'','no-repeat',,'#FFFFFF','underline',,'','no-repeat',,,,,,0],'p0i" & l1 & "');" & vbNewLine
                ret += " stIT('p1i0',['" & MenuName & "','" & MenuUrl & "',,,,,,,,,'" & IIf(ExpandMenuID = MenuID, itemFocusHighlighColor, "#FFFFFF") & "',,,'" & ImgNew & "','no-repeat',,'" & itemOverColor & "','underline','" & itemOverHighligh & "','" & ImgNew & "','no-repeat',,,,,'" & ImgNew & "','no-repeat',,'" & itemOverColor & "','underline','" & itemOverHighligh & "','" & ImgNew & "','no-repeat',,,,,,0],'p0i" & l1 & "');" & vbNewLine
                'ret += " stIT('p1i0',['" & MenuName & "','" & MenuUrl & "',,,,,,,," & Chr(34) & "9pt 'Tahoma'" & Chr(34) & "," + IIf(itemHighligh <> "", "'#FFFF00'", "'#FFFFFF'") + ",,'#1F2C3A',,," & Chr(34) & "9pt 'Tahoma'" & Chr(34) & ",,,'#1F2C3A',,," & Chr(34) & "9pt 'Tahoma'" & Chr(34) & ",,,'#1F2C3A',,," & Chr(34) & "9pt 'Tahoma'" & Chr(34) & ",,,'#1F2C3A',,,,,,,,15],'p0i" & l1 & "');"
                li += 1
            Next
            ret += "stES();" & vbNewLine
            l1 += 1
        Next

        ret += " stIT('p0i" & l1 & "',['มอบหมายงาน','../Src/Assign.aspx',,,,,,,,,'#FFFFFF',,,,,,,,,,,,,,,,,,,,,,,,,,,0],'p0i0');" & vbNewLine
        ret += " stIT('p0i" & l1 & "',['เปลี่ยนรหัสผ่าน','../Src/ChangePassword.aspx',,,,,,,,,'#FFFFFF',,,,,,,,,,,,,,,,,,,,,,,,,,,0],'p0i0');" & vbNewLine

        ret += " stIT('p0i" & l1 & "',['ออกจากระบบ','../Src/Login.aspx',,,,,,,,,'#FF5555',,,,,,,,,,,,,,,,,,,,,,,,,,,0],'p0i0');" & vbNewLine
        ret += " stES();" & vbNewLine
        ret += " stEM();" & vbNewLine
        ret += "</script>"

        lblMenu.Text = ret
    End Sub
End Class
