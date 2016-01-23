Imports System.IO
Imports System.Data
Imports System.Data.OleDb
Partial Class Src_ContractPreview
    Inherits System.Web.UI.Page
    Dim MD As New MainData
    Dim MC As New MainClass
    Dim DV As DataView
    Dim oDate As New clsDate
    Public iRow As Integer
    Dim DS As DataSet
    Dim iRec As Integer
    Dim DVLst As DataView
    Dim DVLink As DataView
    Dim DVContract As DataView
    Private Sub ChkPermis()
        'ตรวจสอบสิทธิ์การใช้งาน
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
        Dim X As String = Request.QueryString("id")
        Dim status As String = Request.QueryString("status")
        Dim sEmpNo As String = Session("EmpNo")

        Me.ChkPermis()

        If Not Page.IsPostBack Then

            If X <> "" Then
                'Preview, Approve, Edit
                Dim sql As String

                sql = "select d.contract_id,d.contract_no,s.type_id,d.subtype_id, "
                sql &= " d.process_id,d.status_id,d.dates_recieve,d.dates_comesign,d.dates_sign, "
                sql &= " d.material,d.user_sale,d.tax_id,d.keyword,d.witness1,d.message,e1.firstname+' '+e1.lastname name1,d.witness1_comment, "
                sql &= " d.witness2,e2.firstname+' '+e2.lastname name2,d.witness2_comment,d.contract_name, "
                sql &= " d.user_buy,e3.firstname+' '+e3.lastname name3, "
                sql &= " s1.subtype_name,p.process_name,s2.status_name, "
                sql &= " d.witness1_app,d.witness2_app,d.guarantee_id,g.guarantee_name,d.guarantee_no,d.dates_start,d.dates_finish ,d.money,"
                sql &= " case when  d.witness1_msg is not null then d.witness1_msg else d.message end witness1_msg, "
                sql &= " case when  d.witness2_msg is not null then d.witness2_msg else d.message end witness2_msg, "
                sql &= " d.ref_bookin,'เลขที่หนังสือ : '+b.bookin_no+' เรื่อง : '+b.topic topic,d.cancel_comment "
                sql &= " from contract_data d inner join contract_subtype s"
                sql &= " on d.subtype_id=s.subtype_id left join employee e1 "
                sql &= " on d.witness1=e1.empid left join employee e2 "
                sql &= " on d.witness2=e2.empid left join employee e3 "
                sql &= " on d.user_buy=e3.empid inner join contract_subtype s1 "
                sql &= " on d.subtype_id=s1.subtype_id inner join contract_process p "
                sql &= " on d.process_id=p.process_id inner join contract_status s2 "
                sql &= " on d.status_id=s2.status_id left join guarantee g "
                sql &= " on d.guarantee_id=g.guarantee_id left join bookin_data b "
                sql &= " on d.ref_bookin=b.bookin_id "
                sql &= " where d.contract_id='" & X & "'"

                DS = MD.GetDataset(sql)
                Session("contract_data") = DS
                iRec = 0
                ViewState("iRec") = iRec

                Me.MyDataBind()

            Else
                'Add New
                Dim sql As String

                sql = "select * from contract_data "

                DS = MD.GetDataset(sql)
                Session("contract_data") = DS
                iRec = 0
                ViewState("iRec") = iRec



            End If

            Me.gData()
            Me.MyGridBind()

            Me.gDataLink()
            Me.MyGridBindLink()

            Me.gDataLinkContract()
            Me.MyGridBindLinkContract()

            If status = "cancel" Then

                Me.DataCancel()
                Title = "ยกเลิกสัญญา"
                Me.EnableControl()
                TabPanel7.Visible = False
                TabContainer1.ActiveTabIndex = 5
                link2.Text = "ยกเลิกการร่างสัญญา"
            ElseIf status = "app" Then
                If DS.Tables(0).Rows(0).Item("status_id").ToString = "2" Then
                    lblAppText.Visible = False
                    txtAppComment1.ReadOnly = True
                    bApp1.Visible = False
                    bAppAndSend1.Visible = False
                    bAppCancel1.Visible = False

                    TabPanel6.Visible = False
                    TabPanel7.Visible = False
                    TabContainer1.ActiveTabIndex = 3
                ElseIf DS.Tables(0).Rows(0).Item("status_id").ToString = "3" Then
                    txtAppComent.ReadOnly = True
                    bApp.Visible = False
                    bAppAndSend.Visible = False
                    bAppCancel.Visible = False

                    TabPanel6.Visible = False
                    TabPanel7.Visible = False
                    TabContainer1.ActiveTabIndex = 4
                End If
                Title = "ลงนามสัญญา"
                link2.Text = "สัญญารอลงนาม"

            ElseIf status = "chkstate" Then

                Me.DataStatus()
                Title = "ปรับปรุงสถานะ"
                Me.EnableControl()
                TabPanel6.Visible = False
                TabContainer1.ActiveTabIndex = 5
                link2.Text = "ปรับปรุงสถานะ"

            ElseIf status = "preview" Then
                Title = "ดูรายละเอียด"
                Me.EnableControl()
                TabPanel6.Visible = False
                TabPanel7.Visible = False

            End If


        Else

            DS = Session("contract_data")
            iRec = ViewState("iRec")

            If Session("DocumentContract") Is Nothing Then
                Me.gData()
            Else
                DVLst = Session("DocumentContract")
            End If

        End If

        lblPrint.Text = "<a href=""javascript:openwindow('" + "PrintContract" + "','" + lblId.Text + "','" + "');"">" + "พิมพ์สัญญา" + "</a>"
        bAppAndSend.OnClientClick = "return confirm('ยืนยันการส่งข้อมูล');"
        bAppAndSend1.OnClientClick = "return confirm('ยืนยันการส่งข้อมูล');"
        bUpdateStatus.OnClientClick = "return confirm('ยืนยันการส่งข้อมูล');"

        If DS.Tables(0).Rows(0).Item("ref_bookin").ToString() <> "" Then
            LinkDetail.Text = "<a href=""javascript:openwindow('" + "BookInPopupPreview" + "','" + DS.Tables(0).Rows(0).Item("ref_bookin").ToString + "','" + "');"">" + "ดูรายละเอียด" + "</a>"
        Else
            LinkDetail.Visible = False
        End If
    End Sub
    Function CheckApprove(ByVal app As String, ByVal status As String) As String
        'ตรวจสอบการมอบหมายงาน
        Dim X As String = Request.QueryString("id")
        Dim sEmpNo As String = Session("EmpNo")
        Dim chk As String
        Dim oDs As DataSet

        chk = "select * from contract_data "
        chk &= "where contract_id='" & X & "' and status_id  =" & status & " and active=1 "
        chk &= "and " & app & "='" & sEmpNo & "'"

        oDs = MD.GetDataset(chk)

        If oDs.Tables(0).Rows.Count < 1 Then
            Dim aut As String
            aut = "select * from authorize "
            aut &= "where assign_from='" & DS.Tables(0).Rows(0).Item("" & app & "").ToString & "' "
            aut &= "and menu_id=35 "
            aut &= "and status_id =" & status & " "
            aut &= "and assign_to='" & sEmpNo & "' "
            aut &= "and convert(nvarchar(10),getdate(),120) between convert(nvarchar(10),date_from,120) and convert(nvarchar(10),date_to,120) "
            Return "1"
        Else
            Return "0"
        End If

    End Function
    Private Sub gData(Optional ByVal Type As String = "")
        'Data Gridview
        Dim X As String = Request.QueryString("id")
        Dim strsql As String

        strsql = " select d.document_id,d.contract_no,d.title,d.page  "
        strsql &= "from contract_document d "
        strsql &= "where d.contract_no='" & lblNo.Text & "'"

        Dim DT As DataTable = MD.GetDataTable(strsql)
        DVLst = DT.DefaultView
        'DVLst.Sort = "[" & ViewState("sortfield") & "] " & ViewState("sortdirection")
        Session("DocumentContract") = DVLst

    End Sub
    Private Sub MyGridBind()
        GridView1.DataSource = DVLst
        Dim X1() As String = {"document_id"}
        GridView1.DataKeyNames = X1
        GridView1.DataBind()
    End Sub
    Public Sub DataCancel()
        'สาเหตุการยกเลิกสัญญา
        Dim strsql As String
        strsql = "select c.cancel_id,c.cancel_name   "
        strsql &= "from contract_cancel c "
        strsql &= "order by c.cancel_name "

        Dim DTS As DataTable = MD.GetDataTable(strsql)

        DDCancel.DataTextField = "cancel_name"
        DDCancel.DataValueField = "cancel_id"
        DDCancel.DataSource = DTS
        DDCancel.DataBind()

    End Sub
    Public Sub DataStatus()
        'สถานะสัญญา
        Dim strsql As String
        strsql = "select status_id,status_name  "
        strsql &= "from contract_status where status_id not in (1,2,3,8) "
        strsql &= "order by status_name "

        Dim DTS As DataTable = MD.GetDataTable(strsql)
  
        DDStatus.DataTextField = "status_name"
        DDStatus.DataValueField = "status_id"
        DDStatus.DataSource = DTS
        DDStatus.DataBind()

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
            Case "dates_recieve"
                If IsDBNull(DT.Rows(iRec)("dates_recieve")) Then
                    Return ""

                Else
                    Dim P1 As Date = DT.Rows(iRec)("dates_recieve")
                    Return P1.ToString("dd/MM/yyyy")
                End If
            Case "dates_comesign"
                If IsDBNull(DT.Rows(iRec)("dates_comesign")) Then
                    Return ""

                Else
                    Dim P1 As Date = DT.Rows(iRec)("dates_comesign")
                    Return P1.ToString("dd/MM/yyyy")
                End If
            Case "dates_sign"
                If IsDBNull(DT.Rows(iRec)("dates_sign")) Then
                    Return ""

                Else
                    Dim P1 As Date = DT.Rows(iRec)("dates_sign")
                    Return P1.ToString("dd/MM/yyyy")
                End If
            Case "dates_contract"
                If IsDBNull(DT.Rows(iRec)("dates_contract")) Then
                    Return ""

                Else
                    Dim P1 As Date = DT.Rows(iRec)("dates_contract")
                    Return P1.ToString("dd/MM/yyyy")
                End If
            Case "dates_start"
                If IsDBNull(DT.Rows(iRec)("dates_start")) Then
                    Return ""

                Else
                    Dim P1 As Date = DT.Rows(iRec)("dates_start")
                    Return P1.ToString("dd/MM/yyyy")
                End If
            Case "dates_finish"
                If IsDBNull(DT.Rows(iRec)("dates_finish")) Then
                    Return ""

                Else
                    Dim P1 As Date = DT.Rows(iRec)("dates_finish")
                    Return P1.ToString("dd/MM/yyyy")
                End If
            Case Else
                Return DT.Rows(iRec)(FieldName) & ""
        End Select
    End Function
    Private Sub EnableControl()

        txtAppComent.ReadOnly = True
        bApp.Visible = False
        bAppAndSend.Visible = False
        bAppCancel.Visible = False
        txtAppComment1.ReadOnly = True
        bApp1.Visible = False
        bAppAndSend1.Visible = False
        bAppCancel1.Visible = False

    End Sub
    Public Sub MyDataBind()
        'Databind at Control
        lblNo.DataBind()
        lblId.DataBind()
        lblDateRecieve.DataBind()
        lblDateComesign.DataBind()
        lblDateSing.DataBind()
        lblStatus.DataBind()
        lblProcess.DataBind()
        lblSubType.DataBind()
        lblUserSale.DataBind()
        lblTax_id.DataBind()
        lblKeyword.DataBind()
        lblMaterial.DataBind()
        FCKeditor1.DataBind()
        FCKeditor2.DataBind()
        FCKeditor3.DataBind()

        lblIdName1.DataBind()
        lblIdName2.DataBind()
        lblIdName3.DataBind()

        If DS.Tables(0).Rows(0).Item("witness1_app").ToString = "T" Or DS.Tables(0).Rows(0).Item("witness1_app").ToString = "F" Then
            rdoApp.SelectedValue = DS.Tables(0).Rows(0).Item("witness1_app").ToString
        End If
        txtAppComent.DataBind()
        lblAppName.DataBind()

        If DS.Tables(0).Rows(0).Item("witness2_app").ToString = "T" Or DS.Tables(0).Rows(0).Item("witness2_app").ToString = "F" Then
            rdoApp1.SelectedValue = DS.Tables(0).Rows(0).Item("witness2_app").ToString
        End If

        txtAppComment1.DataBind()
        lblAppName1.DataBind()

        lblDateStart.DataBind()
        lblDateFinish.DataBind()
        lblGuarantee.DataBind()
        lblGuaranteeNo.DataBind()
        lblMoney.DataBind()
        lblContractName.DataBind()
        lblTopic.DataBind()
        txtCancelComment.DataBind()
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
        'Create Page Gridview
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
            End If
        End If
    End Sub
    Protected Sub bCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bCancel.Click
        'ยกเลิกสัญญา
        Dim X As String = Request.QueryString("id")
        Dim sEmpNo As String = Session("EmpNo")
        Dim strsql As String


        Me.UpdateActive()
        Me.Auto()

        strsql = "insert into contract_data (contract_id,contract_no,dates_recieve,dates_contract,dates_comesign, "
        Strsql &= " dates_sign,status_id,subtype_id,process_id,material,tax_id,user_sale,keyword,message,witness1,witness2,user_buy,"
        Strsql &= " dates_start,dates_finish,guarantee_id,guarantee_no,"
        strsql &= " witness1_app,witness1_comment,witness1_date,"
        strsql &= " witness2_app,witness2_comment,witness2_date,"
        strsql &= " cancel_id,cancel_user,cancel_date,witness1_msg,witness2_msg,contract_name,money,"
        strsql &= " creation_by,created_date,updated_by,updated_date,ref_bookin,cancel_comment,ref_contract)"
        Strsql &= " select '" & lblIdNew.Text & "',contract_no,dates_recieve,dates_contract,dates_comesign, "
        strsql &= " dates_sign,status_id,subtype_id,process_id,material,tax_id,user_sale,keyword,message,witness1,witness2,user_buy,"
        Strsql &= " dates_start,dates_finish,guarantee_id,guarantee_no,"
        strsql &= " witness1_app,witness1_comment,witness1_date,"
        strsql &= " witness2_app,witness2_comment,witness2_date,"
        strsql &= " '" & DDCancel.SelectedValue & "','" & sEmpNo & "',getdate(),witness1_msg,witness2_msg,contract_name,money,"
        strsql &= " creation_by,created_date,'" & sEmpNo & "',getdate(),ref_bookin,'" & txtCancelComment.Text & "',ref_contract "
        Strsql &= " from contract_data "
        strsql &= " where contract_id='" & lblId.Text & "'"

        Dim Y As Integer = MD.Execute(strsql)
        If Y > 0 Then
            MC.MessageBox(Me, "บันทึกลงฐานข้อมูลเรียบร้อยแล้ว")
        Else
            MC.MessageBox(Me, "ไม่สามารถบันทึกลงฐานข้อมูลได้")
        End If

    End Sub
    Protected Sub bApp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bApp.Click
        'บันทึกข้อมูลแต่ยังไม่ส่งข้อมูล
        Dim X As String = Request.QueryString("id")
        Dim sEmpNo As String = Session("EmpNo")
        Dim strsql As String = ""
        If rdoApp.SelectedValue <> "T" And rdoApp.SelectedValue <> "F" Then
            lblAApp1.Text = "***กรุณาเลือกความคิดเห็น"
            lblAAppComment.Text = ""
            Exit Sub
        End If

        Dim app As String = Me.CheckApprove("witness1", "2")
        If app = "1" Then
            strsql = "update contract_data set witness1_app='" & rdoApp.SelectedValue & "',witness1_comment='" & txtAppComent.Text & "',witness1_date=getdate(), "
            strsql &= "witness1_msg='" & FCKeditor2.Value & "',witness1_ass='" & sEmpNo & "' "
            strsql &= "where contract_id='" & X & "'"
        Else
            strsql = "update contract_data set witness1_app='" & rdoApp.SelectedValue & "',witness1_comment='" & txtAppComent.Text & "',witness1_date=getdate(), "
            strsql &= "witness1_msg='" & FCKeditor2.Value & "' "
            strsql &= "where contract_id='" & X & "'"

        End If

        Dim Y As Integer = MD.Execute(strsql)

        If Y > 0 Then
            MC.MessageBox(Me, "บันทึกลงฐานข้อมูลเรียบร้อยแล้ว")
            lblAApp1.Text = ""
            lblAAppComment.Text = ""

        Else
            MC.MessageBox(Me, "ไม่สามารถบันทึกลงฐานข้อมูลได้")
        End If
    End Sub
    Protected Sub bApp1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bApp1.Click
        'บันทึกข้อมูลแต่ยังไม่ส่งข้อมูล
        Dim X As String = Request.QueryString("id")
        Dim sEmpNo As String = Session("EmpNo")
        Dim strsql As String = ""
        If rdoApp1.SelectedValue <> "T" And rdoApp1.SelectedValue <> "F" Then
            lblAApp2.Text = "***กรุณาเลือกความคิดเห็น"
            lblAAppComment2.Text = ""
            Exit Sub
        End If

        Dim app As String = Me.CheckApprove("witness2", "3")
        If app = "1" Then
            strsql = "update contract_data set witness2_app='" & rdoApp1.SelectedValue & "',witness2_comment='" & txtAppComment1.Text & "',witness2_date=getdate(), "
            strsql &= "witness2_msg='" & FCKeditor3.Value & "',witness2_ass='" & sEmpNo & "' "
            strsql &= "where contract_id='" & X & "'"

        Else
            strsql = "update contract_data set witness2_app='" & rdoApp1.SelectedValue & "',witness2_comment='" & txtAppComment1.Text & "',witness2_date=getdate(), "
            strsql &= "witness2_msg='" & FCKeditor3.Value & "' "
            strsql &= "where contract_id='" & X & "'"

        End If

        Dim Y As Integer = MD.Execute(strsql)
        If Y > 0 Then
            MC.MessageBox(Me, "บันทึกลงฐานข้อมูลเรียบร้อยแล้ว")
            lblAApp2.Text = ""
            lblAAppComment2.Text = ""
        Else
            MC.MessageBox(Me, "ไม่สามารถบันทึกลงฐานข้อมูลได้")
        End If
    End Sub
    Protected Sub bAppAndSend_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bAppAndSend.Click
        'บันทึกพร้อมเปลี่ยนสถานะเพื่อส่งให้หัวหน้าตรวจสอบ
        Dim X As String = Request.QueryString("id")
        Dim sEmpNo As String = Session("EmpNo")
        Dim txtStatus As String = ""

        If rdoApp.SelectedValue <> "T" And rdoApp.SelectedValue <> "F" Then
            lblAApp1.Text = "***กรุณาเลือกความคิดเห็น"
            lblAAppComment.Text = ""
            Exit Sub
        End If

        If txtAppComent.Text.Trim = "" Then
            lblAAppComment.Text = "***กรุณากรอกความคิดห็น"
            lblAApp1.Text = ""
            txtAppComent.Focus()
            Exit Sub
        End If

        If rdoApp.SelectedValue = "T" Then
            txtStatus = "3"
        ElseIf rdoApp.SelectedValue = "F" Then
            txtStatus = "1"
        End If

        Dim app As String = Me.CheckApprove("witness1", "2")

        Me.UpdateActive()
        Me.Auto()
        Try
            Dim Strsql As String = ""
            If app = "1" Then

                Strsql = "insert into contract_data (contract_id,contract_no,dates_recieve,dates_contract,dates_comesign, "
                Strsql &= " dates_sign,status_id,subtype_id,process_id,material,tax_id,user_sale,keyword,message,witness1,witness2,user_buy,"
                Strsql &= " dates_start,dates_finish,guarantee_id,guarantee_no,"
                Strsql &= " witness1_app,witness1_comment,witness1_date,witness1_msg,contract_name,money,witness1_ass,"
                Strsql &= " creation_by,created_date,updated_by,updated_date,ref_bookin,ref_contract "
                Strsql &= " select '" & lblIdNew.Text & "',contract_no,dates_recieve,dates_contract,dates_comesign, "
                Strsql &= " dates_sign,'" & txtStatus & "',subtype_id,process_id,material,tax_id,user_sale,keyword,message,witness1,witness2,user_buy,"
                Strsql &= " dates_start,dates_finish,guarantee_id,guarantee_no,'" & rdoApp.SelectedValue & "','" & txtAppComent.Text & "',getdate(),'" & FCKeditor2.Value & "', "
                Strsql &= " contract_name,money,'" & sEmpNo & "',"
                Strsql &= " creation_by,created_date,'" & sEmpNo & "',getdate(),ref_bookin,ref_contract "
                Strsql &= " from contract_data "
                Strsql &= " where contract_id='" & lblId.Text & "'"

            Else

                Strsql = "insert into contract_data (contract_id,contract_no,dates_recieve,dates_contract,dates_comesign, "
                Strsql &= " dates_sign,status_id,subtype_id,process_id,material,tax_id,user_sale,keyword,message,witness1,witness2,user_buy,"
                Strsql &= " dates_start,dates_finish,guarantee_id,guarantee_no,"
                Strsql &= " witness1_app,witness1_comment,witness1_date,witness1_msg,contract_name,money,"
                Strsql &= " creation_by,created_date,updated_by,updated_date,ref_bookin,ref_contract)"
                Strsql &= " select '" & lblIdNew.Text & "',contract_no,dates_recieve,dates_contract,dates_comesign, "
                Strsql &= " dates_sign,'" & txtStatus & "',subtype_id,process_id,material,tax_id,user_sale,keyword,message,witness1,witness2,user_buy,"
                Strsql &= " dates_start,dates_finish,guarantee_id,guarantee_no,'" & rdoApp.SelectedValue & "','" & txtAppComent.Text & "',getdate(),'" & FCKeditor2.Value & "', "
                Strsql &= " contract_name,money,"
                Strsql &= " creation_by,created_date,'" & sEmpNo & "',getdate(),ref_bookin,ref_contract "
                Strsql &= " from contract_data "
                Strsql &= " where contract_id='" & lblId.Text & "'"
            End If


            Dim Y As Integer = MD.Execute(Strsql)
            If Y > 0 Then
                MC.MessageBox(Me, "บันทึกลงฐานข้อมูลเรียบร้อยแล้ว")
                'Response.Redirect("../Src/ContractApproveList.aspx", True)
                txtAppComent.Text = ""
                lblAApp1.Text = ""
                lblAAppComment.Text = ""
                Response.Redirect("../Src/ContractApproveList.aspx", True)
            Else
                MC.MessageBox(Me, "ไม่สามารถบันทึกลงฐานข้อมูลได้")
            End If
        Catch ex As Exception
            Dim ses As String = System.Web.HttpContext.Current.Session.SessionID
            Dim url As String = HttpContext.Current.Request.FilePath
            Dim browser As String = " Type:" + Request.Browser.Type + " Version:" + Request.Browser.Version + " Browser:" + Request.Browser.Browser

            MC.MessageBox(Me, ex.ToString)
            MC.ErrLog(url, ses, ex.ToString, sEmpNo, browser)
        End Try

    End Sub
    Private Sub UpdateActive()
        'Change Active เปลี่ยน Active เป็น 0 เมื่อมีการเปลี่ยนสถานะสัญญา
        Dim sEmpNo As String = Session("EMPNO")

        Try
            Dim Strsql As String
            Strsql = "update contract_data set  "
            Strsql &= " active=0 "
            Strsql &= " where contract_id='" & lblId.Text & "'"

            Dim Y As Integer = MD.Execute(Strsql)
           
        Catch ex As Exception
            Dim ses As String = System.Web.HttpContext.Current.Session.SessionID
            Dim url As String = HttpContext.Current.Request.FilePath
            Dim browser As String = " Type:" + Request.Browser.Type + " Version:" + Request.Browser.Version + " Browser:" + Request.Browser.Browser

            MC.MessageBox(Me, ex.ToString)
            MC.ErrLog(url, ses, ex.ToString, sEmpNo, browser)
        End Try

    End Sub
    Private Sub Auto()
        'Genarate Contract_id
        Dim sqlTmp As String = ""
        Dim comTmp As OleDbCommand = New OleDbCommand
        Dim drTmp As OleDbDataReader
        Dim tmpMemberID As Integer = 0
        Dim tmpMemberID2 As String = ""


        Dim sYear As String
        sYear = Date.Now.Year

        Dim sAuto As String = sYear

        sqlTmp = "SELECT TOP 1 contract_id FROM contract_data "
        sqlTmp &= " WHERE left(contract_id,4) ='" & sAuto & "'"
        sqlTmp &= " ORDER BY contract_id DESC"

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

                tmpMemberID2 = Right(drTmp.Item("contract_id"), 4)

                tmpMemberID = CInt(tmpMemberID2) + 1
                lblIdNew.Text = sAuto + tmpMemberID.ToString("-0000")

            End With
        Catch
            lblIdNew.Text = sAuto + "-0001"
        End Try
        cn.Close()

    End Sub
    Protected Sub bAppAndSend1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bAppAndSend1.Click
        'บันทึกพร้อมเปลี่ยนสถานะเพื่อส่งให้ผู้อำนวยการตรวจสอบ
        Dim X As String = Request.QueryString("id")
        Dim sEmpNo As String = Session("EmpNo")
        Dim txtStatus As String = ""

        If rdoApp1.SelectedValue <> "T" And rdoApp1.SelectedValue <> "F" Then
            lblAApp2.Text = "***กรุณาเลือกความคิดเห็น"
            lblAAppComment2.Text = ""
            Exit Sub
        End If

        If txtAppComment1.Text.Trim = "" Then
            lblAAppComment2.Text = "***กรุณากรอกความคิดห็น"
            lblAApp2.Text = ""
            txtAppComment1.Focus()
            Exit Sub
        End If


        If rdoApp1.SelectedValue = "T" Then
            txtStatus = "4"
        ElseIf rdoApp1.SelectedValue = "F" Then
            txtStatus = "2"
        End If

        Dim app As String = Me.CheckApprove("witness2", "3")

        Me.UpdateActive()
        Me.Auto()
        Try
            Dim Strsql As String = ""
            If app = "1" Then
                Strsql = "insert into contract_data (contract_id,contract_no,dates_recieve,dates_contract,dates_comesign, "
                Strsql &= " dates_sign,status_id,subtype_id,process_id,material,tax_id,user_sale,keyword,message,witness1,witness2,user_buy,"
                Strsql &= " dates_start,dates_finish,guarantee_id,guarantee_no,"
                Strsql &= " witness1_app,witness1_comment,witness1_date,"
                Strsql &= " witness2_app,witness2_comment,witness2_date,witness1_msg,witness2_msg,contract_name,money,witness1_ass,witness2_ass,"
                Strsql &= " creation_by,created_date,updated_by,updated_date,ref_bookin,ref_contract)"
                Strsql &= " select '" & lblIdNew.Text & "',contract_no,dates_recieve,dates_contract,dates_comesign, "
                Strsql &= " dates_sign,'" & txtStatus & "',subtype_id,process_id,material,tax_id,user_sale,keyword,message,witness1,witness2,user_buy,"
                Strsql &= " dates_start,dates_finish,guarantee_id,guarantee_no,"
                Strsql &= " witness1_app,witness1_comment,witness1_date,"
                Strsql &= " '" & rdoApp1.SelectedValue & "','" & txtAppComment1.Text & "',getdate(),witness1_msg,'" & FCKeditor3.Value & "',contract_name,money,witness1_ass,'" & sEmpNo & "',"
                Strsql &= " creation_by,created_date,'" & sEmpNo & "',getdate(),ref_bookin,ref_contract "
                Strsql &= " from contract_data "
                Strsql &= " where contract_id='" & lblId.Text & "'"

            Else

                Strsql = "insert into contract_data (contract_id,contract_no,dates_recieve,dates_contract,dates_comesign, "
                Strsql &= " dates_sign,status_id,subtype_id,process_id,material,tax_id,user_sale,keyword,message,witness1,witness2,user_buy,"
                Strsql &= " dates_start,dates_finish,guarantee_id,guarantee_no,"
                Strsql &= " witness1_app,witness1_comment,witness1_date,"
                Strsql &= " witness2_app,witness2_comment,witness2_date,witness1_msg,witness2_msg,contract_name,money,"
                Strsql &= " creation_by,created_date,updated_by,updated_date,ref_bookin,ref_contract)"
                Strsql &= " select '" & lblIdNew.Text & "',contract_no,dates_recieve,dates_contract,dates_comesign, "
                Strsql &= " dates_sign,'" & txtStatus & "',subtype_id,process_id,material,tax_id,user_sale,keyword,message,witness1,witness2,user_buy,"
                Strsql &= " dates_start,dates_finish,guarantee_id,guarantee_no,"
                Strsql &= " witness1_app,witness1_comment,witness1_date,"
                Strsql &= " '" & rdoApp1.SelectedValue & "','" & txtAppComment1.Text & "',getdate(),witness1_msg,'" & FCKeditor3.Value & "',contract_name,money,"
                Strsql &= " creation_by,created_date,'" & sEmpNo & "',getdate(),ref_bookin,ref_contract "
                Strsql &= " from contract_data "
                Strsql &= " where contract_id='" & lblId.Text & "'"

            End If

            Dim Y As Integer = MD.Execute(Strsql)
            If Y > 0 Then
                MC.MessageBox(Me, "บันทึกลงฐานข้อมูลเรียบร้อยแล้ว")
                txtAppComment1.Text = ""
                lblAAppComment2.Text = ""
                lblAApp2.Text = ""
                Response.Redirect("../Src/ContractApproveList.aspx", True)
            Else
                MC.MessageBox(Me, "ไม่สามารถบันทึกลงฐานข้อมูลได้")
            End If
        Catch ex As Exception
            Dim ses As String = System.Web.HttpContext.Current.Session.SessionID
            Dim url As String = HttpContext.Current.Request.FilePath
            Dim browser As String = " Type:" + Request.Browser.Type + " Version:" + Request.Browser.Version + " Browser:" + Request.Browser.Browser

            MC.MessageBox(Me, ex.ToString)
            MC.ErrLog(url, ses, ex.ToString, sEmpNo, browser)
        End Try

    End Sub
    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        'Download File Contract
        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim K2 As String
            K2 = Convert.ToString(GridView1.DataKeys(e.Row.RowIndex).Values(0).ToString())

            Dim strsql2 As String = ""

            strsql2 = "select d.contract_id,d.document_id, "
            strsql2 &= "d.file_path,d.mime_type "
            strsql2 &= "from contract_document d "
            strsql2 &= "where d.contract_id='" & lblId.Text & "' and d.document_id='" & K2(0) & "'"


            Dim dt As DataTable
            dt = MD.GetDataTable(strsql2)

            Dim lblLink As Label = e.Row.Cells(2).FindControl("lblLink")

            For Each dr As DataRow In dt.Rows


                If dr("mime_type").ToString() = ".pdf" Or dr("mime_type").ToString() = ".txt" Then
                    Dim strUrl As String = "http://" & Constant.BaseURL(Request) & dr("file_path").ToString() & ""

                    lblLink.Text = "<a href='" & strUrl & "?time=" & Date.Now.ToString("HH:mm:ss") & "' Target='_Blank'>ดาวน์โหลด</a>&nbsp;&nbsp;"

                Else

                    Dim strUrl As String = "http://" & Constant.BaseURL(Request) & dr("file_path").ToString()
                    lblLink.Text = "<a href='" & strUrl & "?time=" & Date.Now.ToString("HH:mm:ss") & "' Target='_Blank'>ดาวน์โหลด</a>&nbsp;&nbsp;"

                End If

            Next

        End If
    End Sub
    Protected Sub bUpdateStatus_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bUpdateStatus.Click
        'ปรับปรุงสถานะสัญญา
        Dim X As String = Request.QueryString("id")
        Dim sEmpNo As String = Session("EmpNo")
        Dim strsql As String


        Me.UpdateActive()
        Me.Auto()

        strsql = "insert into contract_data (contract_id,contract_no,dates_recieve,dates_contract,dates_comesign, "
        strsql &= " dates_sign,status_id,subtype_id,process_id,material,tax_id,user_sale,keyword,message,witness1,witness2,user_buy,"
        strsql &= " dates_start,dates_finish,guarantee_id,guarantee_no,"
        strsql &= " witness1_app,witness1_comment,witness1_date,"
        strsql &= " witness2_app,witness2_comment,witness2_date,comment,witness1_msg,witness2_msg,contract_name,money,"
        strsql &= " creation_by,created_date,updated_by,updated_date,ref_bookin,ref_contract)"

        strsql &= " select '" & lblIdNew.Text & "',contract_no,dates_recieve,dates_contract,dates_comesign, "
        strsql &= " dates_sign,'" & DDStatus.SelectedValue & "',subtype_id,process_id,material,tax_id,user_sale,keyword,message,witness1,witness2,user_buy,"
        strsql &= " dates_start,dates_finish,guarantee_id,guarantee_no,"
        strsql &= " witness1_app,witness1_comment,witness1_date,"
        strsql &= " witness2_app,witness2_comment,witness2_date,'" & txtComment.Text & "',witness1_msg,witness2_msg,contract_name,money,"
        strsql &= " creation_by,created_date,'" & sEmpNo & "',getdate(),ref_bookin,ref_contract "
        strsql &= " from contract_data "
        strsql &= " where contract_id='" & lblId.Text & "'"


        Dim Y As Integer = MD.Execute(strsql)
        If Y > 0 Then
            MC.MessageBox(Me, "บันทึกลงฐานข้อมูลเรียบร้อยแล้ว")
            Response.Redirect("../Src/ContractPreviewList.aspx", True)
        Else
            MC.MessageBox(Me, "ไม่สามารถบันทึกลงฐานข้อมูลได้")
        End If
    End Sub
    Protected Sub bAppCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bAppCancel.Click
        txtAppComent.Text = ""
        lblAApp1.Text = ""
        lblAAppComment.Text = ""
    End Sub
    Protected Sub bAppCancel1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bAppCancel1.Click
        txtAppComment1.Text = ""
        lblAAppComment2.Text = ""
        lblAApp2.Text = ""
    End Sub
    Protected Sub link2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles link2.Click
        Dim status As String = Request.QueryString("status")

        If status = "cancel" Then
            Response.Redirect("../Src/ContractCancelList.aspx", True)
        ElseIf status = "app" Then
            Response.Redirect("../Src/ContractApproveList.aspx", True)
        ElseIf status = "chkstate" Then
            Response.Redirect("../Src/ContractWarningList.aspx", True)
        Else
            Response.Redirect("../Src/ContractPreviewList.aspx", True)
        End If

    End Sub
    Protected Sub linkHome_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles linkHome.Click
        Response.Redirect(MD.pHome, True)
    End Sub
    Private Sub gDataLink()
        'ดึงข้อมูลมาแสดงใน Gridview 
        Dim X As String = Request.QueryString("id")
        Dim sEmpNo As String = Session("EmpNo")
        Dim strsql As String

        strsql = " select *  "
        strsql &= "from bookout_data "
        strsql &= "where ref_type=3 and ref_id='" & X & "'"

        Dim DT As DataTable = MD.GetDataTable(strsql)
        DVLink = DT.DefaultView
        'DVLink.Sort = "[" & ViewState("sortfield") & "] " & ViewState("sortdirection")
        Session("LinkBookOut") = DVLink

    End Sub
    Private Sub MyGridBindLink()
        GridView4.DataSource = DVLink
        Dim X1() As String = {"bookout_id"}
        GridView4.DataKeyNames = X1
        GridView4.DataBind()
    End Sub
    Protected Sub GridView4_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridView4.RowCommand

    End Sub
    Protected Sub GridView4_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView4.RowCreated
        If e.Row.RowType = DataControlRowType.Pager Then
            Dim td As TableCell = e.Row.Cells(0)
            Dim Y As Boolean = False
            If GridView4.PageIndex > 0 Then
                Dim L1 As New LinkButton
                L1.ForeColor = Drawing.Color.White
                L1.Text = "First"
                td.Controls.Add(L1)
                AddHandler L1.Click, AddressOf FirstClickLink

                Y = True

                Dim L2 As New Literal
                L2.Text = " &nbsp;&nbsp;"
                td.Controls.Add(L2)

                L1 = New LinkButton
                L1.ForeColor = Drawing.Color.White
                L1.Text = "Prev"
                AddHandler L1.Click, AddressOf PrevClickLink
                td.Controls.Add(L1)
            End If
            If GridView4.PageIndex < GridView4.PageCount - 1 Then
                Dim L2 As Literal
                If Y = True Then
                    L2 = New Literal
                    L2.Text = " &nbsp;&nbsp;"
                    td.Controls.Add(L2)
                End If

                Dim L1 As New LinkButton
                L1.ForeColor = Drawing.Color.White
                L1.Text = "Next"
                AddHandler L1.Click, AddressOf NextClickLink
                td.Controls.Add(L1)

                L2 = New Literal
                L2.Text = " &nbsp;&nbsp;"
                td.Controls.Add(L2)


                L1 = New LinkButton
                L1.ForeColor = Drawing.Color.White
                L1.Text = "Last"
                AddHandler L1.Click, AddressOf LastClickLink
                td.Controls.Add(L1)
            End If
        End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            iRow = e.Row.DataItemIndex + 1
            'If Not e.Row.RowState And DataControlRowState.Edit Then
            '    Dim L1 As ImageButton = e.Row.Cells(16).Controls(1)
            '    L1.OnClientClick = "return confirm('คุณต้องการลบข้อมูลใช่หรือไม่?');"
            'End If
        End If


    End Sub
    Private Sub GoPageLink(ByVal xPage As Integer)
        GridView4.PageIndex = xPage
        Me.MyGridBindLink()
    End Sub
    Private Sub FirstClickLink(ByVal sender As Object, ByVal e As EventArgs)
        Me.GoPageLink(0)
    End Sub
    Private Sub PrevClickLink(ByVal sender As Object, ByVal e As EventArgs)
        Me.GoPageLink(GridView4.PageIndex - 1)
    End Sub
    Private Sub NextClickLink(ByVal sender As Object, ByVal e As EventArgs)
        Me.GoPageLink(GridView4.PageIndex + 1)
    End Sub
    Private Sub LastClickLink(ByVal sender As Object, ByVal e As EventArgs)
        Me.GoPageLink(GridView4.PageCount - 1)
    End Sub
    Protected Sub GridView4_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView4.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim K2 As String
            K2 = Convert.ToString(GridView4.DataKeys(e.Row.RowIndex).Values(0).ToString())
            Dim strsql2 As String = ""


            strsql2 = " select *  "
            strsql2 &= "from bookout_data "
            strsql2 &= "where bookout_id='" & K2 & "'"

            Dim dt As DataTable
            dt = MD.GetDataTable(strsql2)

            Dim lblLink As LinkButton = e.Row.Cells(3).FindControl("LinkName")

            For Each dr As DataRow In dt.Rows

                lblLink.Text = "<a href=""javascript:openwindow('" + "BookOutPreview" + "','" + dr("bookout_id").ToString() + "','" + "');"">" + dr("topic").ToString() + "</a>"

            Next


        End If

    End Sub
    Protected Sub GridView4_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles GridView4.Sorting
        If ViewState("sortfield") = e.SortExpression Then
            If ViewState("sortdirection") = "asc" Then ViewState("sortdirection") = "desc" Else ViewState("sortdirection") = "asc"
        Else
            ViewState("sortfield") = e.SortExpression
            ViewState("sortdirection") = "asc"
        End If
        DV.Sort = "[" & ViewState("sortfield") & "] " & ViewState("sortdirection")
        Me.MyGridBindLink()
    End Sub
    Private Sub gDataLinkContract()
        'ดึงข้อมูลมาแสดงใน Gridview 
        Dim X As String = Request.QueryString("id")
        Dim sEmpNo As String = Session("EmpNo")
        Dim strsql As String

        strsql = "select a.ref_contract,d.contract_no,d.contract_name"
        strsql &= " from"
        strsql &= " (select d.ref_contract "
        strsql &= " from CONTRACT_DATA d"
        strsql &= " where d.contract_id='" & X & "'"
        strsql &= " union"
        strsql &= " Select contract_id"
        strsql &= " from CONTRACT_DATA where ref_contract in"
        strsql &= " (select d.ref_contract "
        strsql &= " from CONTRACT_DATA d"
        strsql &= " where d.contract_id='" & X & "')"
        strsql &= " and active =1 and contract_id <> '" & X & "')a"
        strsql &= " inner join contract_data d"
        strsql &= " on a.ref_contract=d.contract_id"



        Dim DT As DataTable = MD.GetDataTable(strsql)
        DVContract = DT.DefaultView
        'DVContract.Sort = "[" & ViewState("sortfield") & "] " & ViewState("sortdirection")
        Session("LinkContract") = DVContract

    End Sub
    Private Sub MyGridBindLinkContract()
        GridView5.DataSource = DVContract
        Dim X1() As String = {"ref_contract"}
        GridView5.DataKeyNames = X1
        GridView5.DataBind()
    End Sub
    Protected Sub GridView5_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView5.RowCreated
        If e.Row.RowType = DataControlRowType.DataRow Then
            iRow = e.Row.DataItemIndex + 1
            'If Not e.Row.RowState And DataControlRowState.Edit Then
            '    Dim L1 As ImageButton = e.Row.Cells(16).Controls(1)
            '    L1.OnClientClick = "return confirm('คุณต้องการลบข้อมูลใช่หรือไม่?');"
            'End If
        End If

    End Sub

    Protected Sub GridView5_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView5.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim K2 As String
            K2 = Convert.ToString(GridView5.DataKeys(e.Row.RowIndex).Values(0).ToString())
            Dim strsql2 As String = ""


            strsql2 = " select *  "
            strsql2 &= "from contract_data "
            strsql2 &= "where contract_id='" & K2 & "'"

            Dim dt As DataTable
            dt = MD.GetDataTable(strsql2)

            Dim lblLink As LinkButton = e.Row.Cells(2).FindControl("LinkContractName")

            For Each dr As DataRow In dt.Rows

                lblLink.Text = "<a href=""javascript:openwindow('" + "PrintContract" + "','" + dr("contract_id").ToString() + "','" + "');"">" + dr("contract_name").ToString() + "</a>"

            Next


        End If
    End Sub
End Class
