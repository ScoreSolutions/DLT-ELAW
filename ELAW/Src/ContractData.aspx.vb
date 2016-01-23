Imports System.IO
Imports System.Data
Imports System.Data.OleDb
Partial Class Src_ContractData
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
    Private Sub ChkPermis()
        'กำหนดสิทธิ์การใช้งาน
        Dim sEmpNo As String = Page.User.Identity.Name
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
        '       Me.ChkPermis()
        Me.SetJava()
        If Not Page.IsPostBack Then

            If X <> "" Then
                'Edit
                Dim sql As String

                sql = "select d.contract_id,d.contract_no,s.type_id,d.subtype_id, "
                sql &= " d.process_id,d.status_id,d.dates_recieve,d.dates_comesign,d.dates_sign, "
                sql &= " d.material,d.user_sale,d.tax_id,d.keyword,d.witness1,d.message,e1.firstname+' '+e1.lastname name1,d.witness1_comment, "
                sql &= " case when d.witness1_app='T' then 'ลงนาม' when d.witness1_app='F' then 'แก้ไข' else '' end witness1_app, "
                sql &= " d.witness1_app,d.witness1_comment,d.witness1_date,d.witness1_msg,d.witness2_msg,"
                sql &= " d.witness2,e2.firstname+' '+e2.lastname name2,d.witness2_comment, "
                sql &= " case when d.witness2_app='T' then 'ลงนาม' when d.witness2_app='F' then 'แก้ไข' else '' end witness2_app, "
                sql &= " d.user_buy,e3.firstname+' '+e3.lastname name3,d.dates_start,d.dates_finish,d.guarantee_id,d.guarantee_no,d.money,d.contract_name, "
                sql &= " d.ref_bookin,b.topic,d.ref_contract,r.contract_name contract2,d.money_guarantee "
                sql &= " from contract_data d inner join contract_subtype s"
                sql &= " on d.subtype_id=s.subtype_id left join employee e1 "
                sql &= " on d.witness1=e1.empid left join employee e2 "
                sql &= " on d.witness2=e2.empid left join employee e3 "
                sql &= " on d.user_buy=e3.empid left join bookin_data b "
                sql &= " on d.ref_bookin=b.bookin_id left join  "
                sql &= " (select d.contract_id,d.ref_contract,d2.contract_name "
                sql &= " from contract_data d"
                sql &= " inner join contract_data d2"
                sql &= " on d.ref_contract=d2.contract_id "
                sql &= " where d.active=1 and d.ref_contract is not null)r "
                sql &= " on d.contract_id=r.contract_id "
                sql &= " where d.contract_id='" & X & "'"

                DS = MD.GetDataset(sql)
                Session("contract_data") = DS
                iRec = 0
                ViewState("iRec") = iRec

                lblStatusId.Text = DS.Tables(0).Rows(0).Item("status_id").ToString

                If DS.Tables(0).Rows(0).Item("status_id").ToString = "1" Then
               
                Else
                    Me.EnableControl()
                End If

                Me.DataApp1()
                Me.DataApp2()
                Me.DataGuarantee()
                Me.DataContractSubType()
                Me.DataStatus()
                Me.DataProcess()

                Me.MyDataBind()
                Me.FindRow()

                lblId.Text = X
                lblMainStatus.Text = "Edit"
                ddlStatus.Enabled = False

            Else
                'Add New
                Dim sql As String

                sql = "select * from contract_data "

                DS = MD.GetDataset(sql)
                Session("contract_data") = DS
                iRec = 0
                ViewState("iRec") = iRec


                Me.DataApp1()
                Me.DataApp2()
                Me.DataGuarantee()
                Me.DataContractSubType()

                'First status
                Me.DataStatus()
                ddlStatus.SelectedIndex = 0
                ddlStatus.Enabled = False

                Me.DataProcess()

                lblMainStatus.Text = "Add"
                link2.Text = "บันทึกข้อมูลสัญญา"

                Session("TextId3") = ""
                Session("TextIdTitle") = ""
                Session("TextIdContract") = ""

            End If

            Me.gData()
            Me.MyGridBind()

        Else

            Me.RefreshPage()

            DS = Session("contract_data")
            iRec = ViewState("iRec")

            If Session("DocumentContract") Is Nothing Then
                Me.gData()
            Else
                DVLst = Session("DocumentContract")
            End If

        End If

        bSelectTitle.Attributes.Add("onclick", "popupwindown('ShowBookIn.aspx?id=TextTitle&name=TextIdTitle');")

        bSaveAndSend.OnClientClick = "return confirm('ยืนยันการส่งข้อมูล');"

        bSelectContract.Attributes.Add("onclick", "popupwindown('ShowContract.aspx?id=TextContract&name=TextIdContract');")

        bAddGuarantee.Attributes.Add("onclick", "popupwindown('AddGuarantee.aspx?id=TextGuarantee');")

        lblPrint.Text = "<a href=""javascript:openwindow('" + "PrintContract" + "','" + lblId.Text + "','" + "');"">" + "พิมพ์สัญญา" + "</a>"


    End Sub

    Private Sub gData(Optional ByVal Type As String = "")
        'Data in Gridview
        Dim X As String = Request.QueryString("id")
        Dim strsql As String

        strsql = " select d.document_id,d.contract_no,d.title,d.page  "
        strsql &= "from contract_document d "
        strsql &= "where d.contract_no='" & txtNo.Text & "'"

        Dim DT As DataTable = MD.GetDataTable(strsql)
        DVLst = DT.DefaultView
        Session("DocumentContract") = DVLst

    End Sub
    Private Sub MyGridBind()
        GridView1.DataSource = DVLst
        Dim X1() As String = {"document_id"}
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
            Case "money_guarantee"
                If IsDBNull(DT.Rows(iRec)("money_guarantee")) Then
                    Return "0.00"
                Else
                    Dim P1 As Double = DT.Rows(iRec)("money_guarantee")
                    Return P1.ToString("#,##0.00")
                End If
            Case "dates_recieve"
                If IsDBNull(DT.Rows(iRec)("dates_recieve")) Then
                    Return ""
                Else
                    Dim P1 As Date = DT.Rows(iRec)("dates_recieve")
                    Return P1.ToString("dd/MM/yyyy")
                End If
            Case "dates_contract"
                If IsDBNull(DT.Rows(iRec)("dates_contract")) Then
                    Return ""
                Else
                    Dim P1 As Date = DT.Rows(iRec)("dates_contract")
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
    Private Sub BindDate()
        Dim DT As DataTable = DS.Tables(0)
      
        If IsDBNull(DT.Rows(iRec)("dates_recieve")) Then
            txtReceiveDate.Text = "0:00:00"
        Else
            txtReceiveDate.DataBind()

        End If

        If IsDBNull(DT.Rows(iRec)("dates_comesign")) Then
            txtComeSignDate.Text = "0:00:00"
        Else
            txtComeSignDate.DataBind()

        End If

        If IsDBNull(DT.Rows(iRec)("dates_sign")) Then
            txtSignDate.Text = "0:00:00"
        Else
            txtSignDate.DataBind()

        End If

        If IsDBNull(DT.Rows(iRec)("dates_start")) Then
            txtDateStart.Text = "0:00:00"
        Else
            txtDateStart.DataBind()

        End If

        If IsDBNull(DT.Rows(iRec)("dates_finish")) Then
            txtDateEnd.Text = "0:00:00"
        Else
            txtDateEnd.DataBind()
        End If
    End Sub
    Public Sub MyDataBind()
        'Databind at Control
        lblId.DataBind()
        txtNo.DataBind()

        Me.BindDate()

        txtSaleName.DataBind()
        txtTaxId.DataBind()
        txtMatetial.DataBind()
        txtKeyword.DataBind()
        FCKeditor1.DataBind()
        FCKeditor2.DataBind()
        FCKeditor3.DataBind()
        txtName3.DataBind()
        lblIdName3.DataBind()
        txtGuaranteeId.DataBind()

        lblApp1.DataBind()
        lblAppComment1.DataBind()
        lblAppName1.DataBind()

        lblApp2.DataBind()
        lblAppComment2.DataBind()
        lblAppName2.DataBind()
        txtMoney.DataBind()
        txtContractName.DataBind()

        txtTitle.DataBind()
        lblTitle.DataBind()

        txtNameContract.DataBind()
        lblIdContract.DataBind()
        txtgMoney.DataBind()
    End Sub
    Private Sub FindRow()
        'BindData Dropdownlist
        Dim X2 As String
        X2 = DS.Tables(0).Rows(iRec)("subtype_id") & ""
        ddlSubContract.SelectedIndex = FindSubTypeRow(X2)

        Dim X3 As String
        X3 = DS.Tables(0).Rows(iRec)("process_id") & ""
        ddlProcess.SelectedIndex = FindProcessRow(X3)

        Dim X4 As String
        X4 = DS.Tables(0).Rows(iRec)("status_id") & ""
        ddlStatus.SelectedIndex = FindStatusRow(X4)

        Dim X5 As String
        X5 = DS.Tables(0).Rows(iRec)("witness1") & ""
        ddlApp1.SelectedIndex = FindApp1Row(X5)

        Dim X6 As String
        X6 = DS.Tables(0).Rows(iRec)("witness2") & ""
        ddlApp2.SelectedIndex = FindApp2Row(X6)

        Dim X7 As String
        X7 = DS.Tables(0).Rows(iRec)("guarantee_id") & ""
        ddlGuarantee.SelectedIndex = FindGuaranteeRow(X7)

    End Sub
    Public Function FindSubTypeRow(ByVal X As String) As Integer
        Dim i As Integer = 0

        For i = 0 To ddlSubContract.Items.Count - 1
            If X = ddlSubContract.Items(i).Value Then
                Return i
            End If
        Next
        Return 0
    End Function
    Public Function FindProcessRow(ByVal X As String) As Integer
        Dim i As Integer = 0
        Me.DataContractSubType()
        For i = 0 To ddlProcess.Items.Count - 1
            If X = ddlProcess.Items(i).Value Then
                Return i
            End If
        Next
        Return 0
    End Function
    Public Function FindStatusRow(ByVal X As String) As Integer
        Dim i As Integer = 0

        For i = 0 To ddlStatus.Items.Count - 1
            If X = ddlStatus.Items(i).Value Then
                Return i
            End If
        Next
        Return 0
    End Function
    Public Function FindApp1Row(ByVal X As String) As Integer
        Dim i As Integer = 0

        For i = 0 To ddlApp1.Items.Count - 1
            If X = ddlApp1.Items(i).Value Then
                Return i
            End If
        Next
        Return 0
    End Function
    Public Function FindApp2Row(ByVal X As String) As Integer
        Dim i As Integer = 0

        For i = 0 To ddlApp2.Items.Count - 1
            If X = ddlApp2.Items(i).Value Then
                Return i
            End If
        Next
        Return 0
    End Function
    Public Function FindGuaranteeRow(ByVal X As String) As Integer
        Dim i As Integer = 0

        For i = 0 To ddlGuarantee.Items.Count - 1
            If X = ddlGuarantee.Items(i).Value Then
                Return i
            End If
        Next
        Return 0
    End Function
    Private Sub AddNew()
        Dim dr As DataRow = DS.Tables(0).NewRow
        DS.Tables(0).Rows.Add(dr)
        iRec = DS.Tables(0).Rows.Count - 1
        ViewState("iRec") = iRec

        Me.Auto()
    End Sub
    Public Sub EnableControl()
        'Enable Control

        txtNo.ReadOnly = True
        txtSaleName.ReadOnly = True
        txtTaxId.ReadOnly = True
        txtMatetial.ReadOnly = True
        txtKeyword.ReadOnly = True
        txtName3.ReadOnly = True
        lblIdName3.Enabled = False
        bGenerate.Visible = False
        bSelect3.Visible = False
        bSave.Visible = False
        bSaveAndSend.Visible = False
        bCancel.Visible = False

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
                Dim L1 As ImageButton = e.Row.Cells(3).Controls(1)
                L1.OnClientClick = "return confirm('คุณต้องการลบข้อมูลใช่หรือไม่?');"
            End If
        End If
    End Sub
    Protected Sub GridView1_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GridView1.RowDeleting
        'Delete Document
        Dim strPath As String = "http://" & Constant.BaseURL(Request) & "Document/BookOut/"
        Dim K1 As DataKey = GridView1.DataKeys(e.RowIndex)
        Dim strsql As String
        Dim chk As String

        chk = "select * from contract_document where contract_id='" & lblId.Text & "' and document_id ='" & K1(0) & "'"
        Dim DS As DataSet
        Dim mtype As String = ""
        Dim fname As String = ""
        DS = MD.GetDataset(chk)

        If DS.Tables(0).Rows.Count > 0 Then
            mtype = DS.Tables(0).Rows(0).Item("mime_type").ToString
            fname = Func.getServerPath() & DS.Tables(0).Rows(0).Item("file_path").ToString

        End If

        strsql = "delete from contract_document where contract_id='" & lblId.Text & "' and document_id ='" & K1(0) & "'"
        Dim Y As Integer = MD.Execute(strsql)
        If Y > 0 Then
            Me.gData()
            Me.MyGridBind()
            Func.DeleteFile(fname)
            MC.MessageBox(Me, "ลบข้อมูลเรียบร้อยแล้ว")
        Else
            MC.MessageBox(Me, "ไม่สามารถลบข้อมูลได้")
        End If
    End Sub
    Protected Sub GridView1_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles GridView1.RowEditing
        'Edit Document
        Dim K1 As DataKey = GridView1.DataKeys(e.NewEditIndex)
        Dim lName As Label = GridView1.Rows(e.NewEditIndex).Cells(0).Controls(1)
        Dim lPage As Label = GridView1.Rows(e.NewEditIndex).Cells(1).Controls(1)

        lblDocId.Text = K1(0).ToString
        txtDocDetail.Text = lName.Text
        txtDocPage.Text = lPage.Text

        lblStatus.Text = "Edit"
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
                lblId.Text = sAuto + tmpMemberID.ToString("-0000")

            End With
        Catch
            lblId.Text = sAuto + "-0001"
        End Try
        cn.Close()

    End Sub
    Private Sub AutoFile()
        'Genarate Document_id
        Dim sqlTmp As String = ""
        Dim comTmp As OleDbCommand = New OleDbCommand
        Dim drTmp As OleDbDataReader
        Dim tmpMemberID As Integer = 0
        Dim tmpMemberID2 As String = ""

        sqlTmp = "SELECT TOP 1 document_id FROM contract_document "
        sqlTmp &= " WHERE contract_id ='" & lblId.Text & "'"
        sqlTmp &= " ORDER BY document_id DESC"

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

                tmpMemberID2 = drTmp.Item("document_id")

                tmpMemberID = CInt(tmpMemberID2) + 1
                lblDocId.Text = tmpMemberID.ToString

            End With
        Catch
            lblDocId.Text = "1"
        End Try
        cn.Close()

    End Sub
    Private Sub GenContractNo()
        'Genarate Contract No
        Dim sqlTmp As String = ""
        Dim comTmp As OleDbCommand = New OleDbCommand
        Dim drTmp As OleDbDataReader
        Dim tmpMemberID As Integer = 0
        Dim tmpMemberID2 As String = ""
        Dim chk As String
        Dim oDs As New DataSet

        chk = "select n.pre,n.years from contract_no n inner join contract_subtype s "
        chk &= "on n.type_id=s.type_id where s.subtype_id='" & ddlSubContract.SelectedValue & "'"

        oDs = MD.GetDataset(chk)

        If oDs.Tables(0).Rows.Count > 0 Then
            sqlTmp = "SELECT TOP 1 right(left(contract_no,12),3) contract_no  FROM contract_data "
            sqlTmp &= " WHERE left(contract_no,9) = '" & oDs.Tables(0).Rows(0).Item("pre").ToString & "' and  "
            sqlTmp &= " right(contract_no,4)='" & oDs.Tables(0).Rows(0).Item("years").ToString & "' "
            sqlTmp &= " ORDER BY contract_no DESC"

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

                    tmpMemberID2 = drTmp.Item("contract_no")

                    tmpMemberID = CInt(tmpMemberID2) + 1
                    txtNo.Text = oDs.Tables(0).Rows(0).Item("pre").ToString + tmpMemberID.ToString("000") + "/" + oDs.Tables(0).Rows(0).Item("years").ToString

                End With
            Catch
                txtNo.Text = oDs.Tables(0).Rows(0).Item("pre").ToString + "001" + "/" + oDs.Tables(0).Rows(0).Item("years").ToString
            End Try
            cn.Close()

        End If

    End Sub
    Private Sub SetJava()
        'When click select users
        txtDocPage.Attributes.Add("onkeypress", "return blockNonNumbers(this, event,-1,0);")
        bSelect3.Attributes.Add("onclick", "popupwindown('SearchEmp.aspx?id=TextName3&name=TextId3');")
        txtTaxId.Attributes.Add("onkeypress", "return blockNonNumbers(this, event,-1,0);")
        'txtGuaranteeId.Attributes.Add("onkeypress", "return blockNonNumbers(this, event,-1,0);")

        txtReceiveDate.Attributes.Add("onkeypress", "return blockText(this, event,-1,0);")
        txtComeSignDate.Attributes.Add("onkeypress", "return blockText(this, event,-1,0);")
        txtSignDate.Attributes.Add("onkeypress", "return blockText(this, event,-1,0);")
        txtDateStart.Attributes.Add("onkeypress", "return blockText(this, event,-1,0);")
        txtDateEnd.Attributes.Add("onkeypress", "return blockText(this, event,-1,0);")
        txtMoney.Attributes.Add("onkeypress", "return blockNonNumbers(this, event,-1,0);")
        txtgMoney.Attributes.Add("onkeypress", "return blockNonNumbers(this, event,-1,0);")

    End Sub
    Private Sub RefreshPage()

        If Session("TextId3") <> "" Then
            txtName3.Text = Session("TextName3")
            lblIdName3.Text = Session("TextId3")
        End If

        If Session("TextIdTitle") <> "" Then
            txtTitle.Text = Session("TextTitle")
            lblTitle.Text = Session("TextIdTitle")
        End If

        If Session("TextIdContract") <> "" Then
            txtNameContract.Text = Session("TextContract")
            lblIdContract.Text = Session("TextIdContract")
        End If

        If Session("txtGuarantee") = "1" Then
            Me.DataGuarantee()
            Session("txtGuarantee") = ""
        End If

    End Sub
    Public Sub DataContractSubType()
        'ประเภทสัญญา
        Dim strsql As String
        strsql = "select s.subtype_id,s.subtype_name    "
        strsql &= "from contract_subtype s inner join contract_type t "
        strsql &= "on s.type_id=t.type_id "

        Dim DTS As DataTable = MD.GetDataTable(strsql)

        ddlSubContract.DataTextField = "subtype_name"
        ddlSubContract.DataValueField = "subtype_id"
        ddlSubContract.DataSource = DTS
        ddlSubContract.DataBind()

    End Sub
    Public Sub DataStatus()
        'สถานะสัญญา
        Dim strsql As String
        strsql = "select s.status_id,s.status_name   "
        strsql &= "from contract_status s "
        strsql &= "order by s.status_id "

        Dim DTS As DataTable = MD.GetDataTable(strsql)

        ddlStatus.DataTextField = "status_name"
        ddlStatus.DataValueField = "status_id"
        ddlStatus.DataSource = DTS
        ddlStatus.DataBind()

    End Sub
    Public Sub DataProcess()
        'วิธีจัดจ้าง
        Dim strsql As String
        strsql = "select p.process_id,p.process_name   "
        strsql &= "from contract_process p "
        strsql &= "order by p.process_name "

        Dim DTS As DataTable = MD.GetDataTable(strsql)
        Dim dr As DataRow = DTS.NewRow
        dr!process_id = 0
        dr!process_name = "---------โปรดเลือก---------"
        DTS.Rows.InsertAt(dr, 0)
        ddlProcess.DataTextField = "process_name"
        ddlProcess.DataValueField = "process_id"
        ddlProcess.DataSource = DTS
        ddlProcess.DataBind()

    End Sub
    Public Sub DataApp1()
        'หัวหน้าลงนาม
        Dim strsql As String
        strsql = "select e.empid,e.firstname+' '+e.lastname nameapp1   "
        strsql &= "from employee e inner join division d "
        strsql &= "on e.div_id = d.div_id and e.dept_id=1 and d.div_id=5 and e.pos_id=  "
        strsql &= "(select pos_id from division where div_id=5 )"
        strsql &= "order by e.firstname+' '+e.lastname  "

        Dim DTS As DataTable = MD.GetDataTable(strsql)

        ddlApp1.DataTextField = "nameapp1"
        ddlApp1.DataValueField = "empid"
        ddlApp1.DataSource = DTS
        ddlApp1.DataBind()

    End Sub
    Public Sub DataApp2()
        'ผู้อำนวยการลงนาม
        Dim strsql As String
        strsql = "select e.empid,e.firstname+' '+e.lastname nameapp2   "
        strsql &= "from employee e inner join division d "
        strsql &= "on e.div_id = d.div_id and e.dept_id=1 and d.div_id=7 and e.pos_id=  "
        strsql &= "(select pos_id from division where div_id=7 )"
        strsql &= "order by e.firstname+' '+e.lastname  "

        Dim DTS As DataTable = MD.GetDataTable(strsql)

        ddlApp2.DataTextField = "nameapp2"
        ddlApp2.DataValueField = "empid"
        ddlApp2.DataSource = DTS
        ddlApp2.DataBind()

    End Sub
    Public Sub DataGuarantee()
        'หลักประกัน
        Dim strsql As String
        strsql = "select guarantee_id,guarantee_name   "
        strsql &= "from guarantee  "
        strsql &= "order by guarantee_name "

        Dim DTS As DataTable = MD.GetDataTable(strsql)
        Dim dr As DataRow = DTS.NewRow
        dr!guarantee_id = 0
        dr!guarantee_name = "---------โปรดเลือก---------"
        DTS.Rows.InsertAt(dr, 0)
        ddlGuarantee.DataTextField = "guarantee_name"
        ddlGuarantee.DataValueField = "guarantee_id"
        ddlGuarantee.DataSource = DTS
        ddlGuarantee.DataBind()

    End Sub
    Protected Sub bGenerate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bGenerate.Click
        'Genarate Data Contract
        Dim sql As String

        Try
            sql = "select * from contract_subtype s "
            sql &= " where s.subtype_id = '" & ddlSubContract.SelectedValue & "'"

            Dim oDs As New DataTable
            oDs = MD.GetDataTable(sql)

            If oDs.Columns.Count > 0 Then
                'FCKeditor1.Value = ""
                FCKeditor1.Value = oDs.Rows(0).Item("format").ToString
            End If

        Catch ex As Exception

        End Try

    End Sub
    Protected Sub ddlSubContract_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSubContract.SelectedIndexChanged

    End Sub
    Private Sub ClearAlert()

        lblAdate1.Text = ""
        lblADate3.Text = ""
        lblADate4.Text = ""
        lblMaterail.Text = ""
        lblTaxId.Text = ""
        lblSaleName.Text = ""
        lblKeyword.Text = ""
        lblAApp1.Text = ""
        lblAApp2.Text = ""
        lblGaranteeId.Text = ""
        lblGarantee.Text = ""
        lblProcess.Text = ""
        lblBuyName.Text = ""
        lblMoney.Text = ""
    End Sub
    Protected Sub bSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bSave.Click
        Dim X As String = Request.QueryString("id")
        Dim sEmpNo As String = Session("EmpNo")

        If txtContractName.Text.Trim = "" Then
            Me.ClearAlert()
            lblContractName.Text = "กรุณากรอกชื่อสัญญา"
            txtContractName.Focus()
            Exit Sub
        End If
        If txtReceiveDate.Text.Year = 1 Then
            Me.ClearAlert()
            lblAdate1.Text = "กรุณากรอกวันที่รับเรื่อง"
            Exit Sub
        End If

        If ddlProcess.SelectedValue = "0" Then
            Me.ClearAlert()
            lblProcess.Text = "กรุณาเลือกวิธีจัดจ้าง"
            Exit Sub
        End If
        If txtMatetial.Text.Trim = "" Then
            Me.ClearAlert()
            lblMaterail.Text = "กรุณากรอกสิ่งที่จะซื้อ/จ้าง"
            txtMatetial.Focus()
            Exit Sub
        End If
        If txtTaxId.Text.Trim = "" Then
            Me.ClearAlert()
            lblTaxId.Text = "กรุณากรอกเลขที่ประจำตัวผู้เสียภาษีของคู่สัญญา"
            txtTaxId.Focus()
            Exit Sub
        End If
        If txtSaleName.Text.Trim = "" Then
            Me.ClearAlert()
            lblSaleName.Text = "กรุณากรอกชื่อผู้ขาย/ผู้รับจ้าง"
            txtSaleName.Focus()
            Exit Sub
        End If
        If txtKeyword.Text.Trim = "" Then
            Me.ClearAlert()
            lblKeyword.Text = "กรุณากรอกคำค้นหา"
            txtKeyword.Focus()
            Exit Sub
        End If

        If ddlApp1.SelectedValue = "0" Then
            Me.ClearAlert()
            lblAApp1.Text = "กรุณาเลือกชื่อหัวหน้านิติกรรมและคดี"
            Exit Sub
        End If
        If ddlApp2.SelectedValue = "0" Then
            Me.ClearAlert()
            lblAApp2.Text = "กรุณาเลือกชื่อผู้อำนวยการ"
            Exit Sub
        End If

        If lblMainStatus.Text = "Add" Then
            '--------------Insert-----------------
            Me.Auto()
            Me.GenContractNo()
            Me.SaveData(1)
        Else
            '--------------Update-----------------
            'Check Status Before Send
            Me.EditData(1)
        End If

    End Sub
    Private Sub SaveData(ByVal status As String)
        Dim sEmpNo As String = Session("EmpNo")
        Dim strsql As String

        Try

            strsql = "insert into contract_data (contract_id,contract_no,dates_recieve,dates_comesign, "
            strsql &= " dates_sign,status_id,subtype_id,process_id,material,tax_id,user_sale,keyword,message,witness1,witness2,user_buy,"
            strsql &= " dates_start,dates_finish,guarantee_id,guarantee_no,money,contract_name,"
            strsql &= " creation_by,created_date,updated_by,updated_date,ref_bookin,ref_contract,money_guarantee)"
            strsql &= " values (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?) "

            cn = New OleDbConnection(MD.Strcon)
            cmd = New OleDbCommand(strsql, cn)
            MD.CreateParam(cmd, "TTDDDTTTTTTTTTTTDDTTTTTDTDTTS")

            cmd.Parameters("@P1").Value = lblId.Text
            cmd.Parameters("@P2").Value = txtNo.Text

            If txtReceiveDate.Text.Year = 1 Then
                cmd.Parameters("@P3").Value = DBNull.Value
            Else
                cmd.Parameters("@P3").Value = DateTime.Parse(txtReceiveDate.Text) 'txtReceiveDate.SaveDate '
            End If

            If txtComeSignDate.Text.Year = 1 Then
                cmd.Parameters("@P4").Value = DBNull.Value
            Else
                cmd.Parameters("@P4").Value = DateTime.Parse(txtComeSignDate.Text) 'txtComeSignDate.SaveDate '
            End If

            If txtSignDate.Text.Year = 1 Then
                cmd.Parameters("@P5").Value = DBNull.Value
            Else
                cmd.Parameters("@P5").Value = DateTime.Parse(txtSignDate.Text) 'txtSignDate.SaveDate '
            End If

            cmd.Parameters("@P6").Value = status
            cmd.Parameters("@P7").Value = ddlSubContract.SelectedValue
            cmd.Parameters("@P8").Value = ddlProcess.SelectedValue
            cmd.Parameters("@P9").Value = txtMatetial.Text
            cmd.Parameters("@P10").Value = txtTaxId.Text
            cmd.Parameters("@P11").Value = txtSaleName.Text
            cmd.Parameters("@P12").Value = txtKeyword.Text
            cmd.Parameters("@P13").Value = FCKeditor1.Value
            cmd.Parameters("@P14").Value = ddlApp1.SelectedValue
            cmd.Parameters("@P15").Value = ddlApp2.SelectedValue
            cmd.Parameters("@P16").Value = lblIdName3.Text

            If txtDateStart.Text.Year = 1 Then
                cmd.Parameters("@P17").Value = DBNull.Value
            Else
                cmd.Parameters("@P17").Value = DateTime.Parse(txtDateStart.Text) ' txtDateStart.SaveDate '
            End If

            If txtDateEnd.Text.Year = 1 Then
                cmd.Parameters("@P18").Value = DBNull.Value
            Else
                cmd.Parameters("@P18").Value = DateTime.Parse(txtDateEnd.Text) 'txtDateEnd.SaveDate '
            End If

            cmd.Parameters("@P19").Value = ddlGuarantee.SelectedValue
            cmd.Parameters("@P20").Value = txtGuaranteeId.Text

            If (txtMoney.Text.Trim = "") Then
                cmd.Parameters("@P21").Value = "0"
            Else
                cmd.Parameters("@P21").Value = Convert.ToDouble(txtMoney.Text)
            End If

            cmd.Parameters("@P22").Value = txtContractName.Text
            cmd.Parameters("@P23").Value = sEmpNo
            cmd.Parameters("@P24").Value = DateTime.Parse(Date.Now)
            cmd.Parameters("@P25").Value = sEmpNo
            cmd.Parameters("@P26").Value = DateTime.Parse(Date.Now)
            cmd.Parameters("@P27").Value = lblTitle.Text

            If lblIdContract.Text = "" Then
                cmd.Parameters("@P28").Value = DBNull.Value
            Else
                cmd.Parameters("@P28").Value = lblIdContract.Text
            End If

            cmd.Parameters("@P29").Value = txtgMoney.Text

            cn.Open()
            cmd.ExecuteNonQuery()
            MC.MessageBox(Me, "บันทึกลงฐานข้อมูลเรียบร้อยแล้ว")
            lblMainStatus.Text = "Edit"

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

    End Sub
    Private Sub EditData(ByVal status As String)
        Dim sEmpNo As String = Session("EmpNo")
        Dim strsql As String

        Try

            strsql = "update contract_data set contract_no=?,dates_recieve=?,dates_comesign=?, "
            strsql &= " dates_sign=?,status_id=?,subtype_id=?,process_id=?,material=?,tax_id=?,user_sale=?,keyword=?,message=?,witness1=?,witness2=?,user_buy=?,"
            strsql &= " dates_start=?,dates_finish=?,guarantee_id=?,guarantee_no=?,money=?,contract_name=?,"
            strsql &= " updated_by=?,updated_date=?,ref_bookin=?,ref_contract=?,money_guarantee=? "
            strsql &= " where contract_id=? "

            cn = New OleDbConnection(MD.Strcon)
            cmd = New OleDbCommand(strsql, cn)
            MD.CreateParam(cmd, "TDDDTTTTTTTTTTTDDTTTTTDTTST")

            cmd.Parameters("@P1").Value = txtNo.Text

            If txtReceiveDate.Text.Year = 1 Then
                cmd.Parameters("@P2").Value = DBNull.Value
            Else
                cmd.Parameters("@P2").Value = DateTime.Parse(txtReceiveDate.Text)
            End If

            If txtComeSignDate.Text.Year = 1 Then
                cmd.Parameters("@P3").Value = DBNull.Value
            Else
                cmd.Parameters("@P3").Value = DateTime.Parse(txtComeSignDate.Text)
            End If

            If txtSignDate.Text.Year = 1 Then
                cmd.Parameters("@P4").Value = DBNull.Value
            Else
                cmd.Parameters("@P4").Value = DateTime.Parse(txtSignDate.Text)
            End If

            cmd.Parameters("@P5").Value = status
            cmd.Parameters("@P6").Value = ddlSubContract.SelectedValue
            cmd.Parameters("@P7").Value = ddlProcess.SelectedValue
            cmd.Parameters("@P8").Value = txtMatetial.Text
            cmd.Parameters("@P9").Value = txtTaxId.Text
            cmd.Parameters("@P10").Value = txtSaleName.Text
            cmd.Parameters("@P11").Value = txtKeyword.Text
            cmd.Parameters("@P12").Value = FCKeditor1.Value
            cmd.Parameters("@P13").Value = ddlApp1.SelectedValue
            cmd.Parameters("@P14").Value = ddlApp2.SelectedValue
            cmd.Parameters("@P15").Value = lblIdName3.Text

            If txtDateStart.Text.Year = 1 Then
                cmd.Parameters("@P16").Value = DBNull.Value
            Else
                cmd.Parameters("@P16").Value = DateTime.Parse(txtDateStart.Text)
            End If

            If txtDateEnd.Text.Year = 1 Then
                cmd.Parameters("@P17").Value = DBNull.Value
            Else
                cmd.Parameters("@P17").Value = DateTime.Parse(txtDateEnd.Text)
            End If

            cmd.Parameters("@P18").Value = ddlGuarantee.SelectedValue
            cmd.Parameters("@P19").Value = txtGuaranteeId.Text

            If (txtMoney.Text.Trim = "") Then
                cmd.Parameters("@P20").Value = "0"
            Else
                cmd.Parameters("@P20").Value = Convert.ToDouble(txtMoney.Text)
            End If

            cmd.Parameters("@P21").Value = txtContractName.Text
            cmd.Parameters("@P22").Value = sEmpNo
            cmd.Parameters("@P23").Value = DateTime.Parse(Date.Now)
            cmd.Parameters("@P24").Value = lblTitle.Text

            If lblIdContract.Text = "" Then
                cmd.Parameters("@P25").Value = DBNull.Value
            Else
                cmd.Parameters("@P25").Value = lblIdContract.Text
            End If


            cmd.Parameters("@P26").Value = txtgMoney.Text
            cmd.Parameters("@P27").Value = lblId.Text

            cn.Open()
            cmd.ExecuteNonQuery()
            MC.MessageBox(Me, "บันทึกลงฐานข้อมูลเรียบร้อยแล้ว")
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
    End Sub
    Private Sub UpdateActive()
        Dim sEmpNo As String = Session("EmpNo")

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
    Protected Sub bCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bCancel.Click

        Dim X As String = Request.QueryString("id")
        If X <> "" Then
            Me.MyDataBind()
        Else
            txtNo.Text = ""

            txtReceiveDate.Text = "0:00:00"
            txtComeSignDate.Text = "0:00:00"
            txtSignDate.Text = "0:00:00"
            txtDateStart.Text = "0:00:00"
            txtDateEnd.Text = "0:00:00"

            txtMatetial.Text = ""
            txtTaxId.Text = ""
            txtGuaranteeId.Text = ""
            txtKeyword.Text = ""
            txtSaleName.Text = ""
            txtName3.Text = ""
            lblIdName3.Text = ""
            txtMoney.Text = ""
            txtContractName.Text = ""
        End If
        Me.ClearAlert()
    End Sub
    Private Sub ClearData()
        txtNo.Text = ""
        lblId.Text = ""

        txtReceiveDate.Text = "0:00:00"
        txtComeSignDate.Text = "0:00:00"
        txtSignDate.Text = "0:00:00"
        txtDateStart.Text = "0:00:00"
        txtDateEnd.Text = "0:00:00"

        lblStatusId.Text = ""
        txtMatetial.Text = ""
        txtTaxId.Text = ""
        txtSaleName.Text = ""
        txtKeyword.Text = ""
        FCKeditor1.Value = ""

        txtGuaranteeId.Text = ""

        ddlProcess.SelectedIndex = 0
        ddlGuarantee.SelectedIndex = 0
        ddlApp1.SelectedIndex = 0
        ddlApp2.SelectedIndex = 0

        txtName3.Text = ""
        lblIdName3.Text = ""

        txtDocDetail.Text = ""
        txtDocPage.Text = ""
        lblMoney.Text = ""

        txtContractName.Text = ""

        Me.gData()
        Me.MyGridBind()
    End Sub
    Protected Sub bSaveFile_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bSaveFile.Click
        Dim X As String = Request.QueryString("id")
        Dim sEmpNo As String = Session("EmpNo")
        Dim strPath As String = "Document\Contract\"

        If lblId.Text = "" Then
            Me.bSave_Click(sender, e)
        End If

        If txtNo.Text = "" Then
            MC.MessageBox(Me, "กรุณาบันทึกข้อมูลหลักก่อน")
            Exit Sub
        End If

        If lblStatus.Text <> "Edit" Then
            If FileUpload1.PostedFile Is Nothing OrElse String.IsNullOrEmpty(FileUpload1.PostedFile.FileName) OrElse FileUpload1.PostedFile.InputStream Is Nothing Then
                lblAFile.Text = "กรุณาเลือกไฟล์ที่ต้องการอัพโหลด"
                lblADocDeatil.Text = ""
                lblADocPage.Text = ""
                Exit Sub
            End If
        End If

        If txtDocDetail.Text.Trim = "" Then
            lblADocDeatil.Text = "กรุณากรอกชื่อเอกสาร"
            lblAFile.Text = ""
            lblADocPage.Text = ""
            txtDocDetail.Focus()
            Exit Sub
        End If

        If txtDocPage.Text.Trim = "" Then
            lblADocPage.Text = "กรุณากรอกจำนวนหน้า"
            lblAFile.Text = ""
            lblADocDeatil.Text = ""
            Exit Sub
        End If

        If lblStatus.Text = "Edit" Then
            Dim Strsql As String
            Strsql = "update contract_document set contract_no='" & txtNo.Text & "',title='" & txtDocDetail.Text & "',page='" & txtDocPage.Text & "' "

            Dim MIMEType As String = Nothing
            If FileUpload1.HasFile Then
                Dim extension As String = System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName).ToLower()


                Select Case extension
                    Case ".jpg", ".jpeg", ".jpe"
                        MIMEType = ".jpg"
                        Strsql &= ",file_path='" & strPath & "" & lblId.Text & "-" & lblDocId.Text & "" & MIMEType & "',mime_type='" & MIMEType & "'"
                    Case ".csv"
                        MIMEType = ".csv"
                        Strsql &= ",file_path='" & strPath & "" & lblId.Text & "-" & lblDocId.Text & "" & MIMEType & "',mime_type='" & MIMEType & "'"
                    Case ".xls"
                        MIMEType = ".xls"
                        Strsql &= ",file_path='" & strPath & "" & lblId.Text & "-" & lblDocId.Text & "" & MIMEType & "',mime_type='" & MIMEType & "'"
                    Case ".xlsx"
                        MIMEType = ".xlsx"
                        Strsql &= ",file_path='" & strPath & "" & lblId.Text & "-" & lblDocId.Text & "" & MIMEType & "',mime_type='" & MIMEType & "'"
                    Case ".pdf"
                        MIMEType = ".pdf"
                        Strsql &= ",file_path='" & strPath & "" & lblId.Text & "-" & lblDocId.Text & "" & MIMEType & "',mime_type='" & MIMEType & "'"
                    Case ".doc"
                        MIMEType = ".doc"
                        Strsql &= ",file_path='" & strPath & "" & lblId.Text & "-" & lblDocId.Text & "" & MIMEType & "',mime_type='" & MIMEType & "'"
                    Case ".docx"
                        MIMEType = ".docx"
                        Strsql &= ",file_path='" & strPath & "" & lblId.Text & "-" & lblDocId.Text & "" & MIMEType & "',mime_type='" & MIMEType & "'"
                    Case ".txt"
                        MIMEType = ".txt"
                        Strsql &= ",file_path='" & strPath & "" & lblId.Text & "-" & lblDocId.Text & "" & MIMEType & "',mime_type='" & MIMEType & "'"
                    Case ".htm", ".html"
                        MIMEType = ".html"
                        Strsql &= ",file_path='" & strPath & "" & lblId.Text & "-" & lblDocId.Text & "" & MIMEType & "',mime_type='" & MIMEType & "'"
                    Case Else
                        MC.MessageBox(Me, "Not a valid file format")
                        Exit Sub
                End Select



            End If

            Strsql &= ",creation_by='" & sEmpNo & "',created_date=getdate(),updated_by='" & sEmpNo & "',updated_date=getdate() "
            Strsql &= " where contract_id='" & lblId.Text & "' and document_id ='" & lblDocId.Text & "'"

            Dim Y As Integer = MD.Execute(Strsql)
            If Y > 0 Then
                If FileUpload1.HasFile Then
                    Func.UploadFile(sEmpNo, FileUpload1, lblId.Text & "-" & lblDocId.Text & MIMEType, strPath)
                End If

                Me.gData()
                Me.MyGridBind()
                MC.MessageBox(Me, "บันทึกลงฐานข้อมูลเรียบร้อยแล้ว")

                lblStatus.Text = ""
                lblDocId.Text = ""

                lblADocPage.Text = ""
                lblAFile.Text = ""
                lblADocDeatil.Text = ""

            Else
                MC.MessageBox(Me, "ไม่สามารถบันทึกลงฐานข้อมูลได้")
            End If

        Else

            Try
                Me.AutoFile()
                Dim Strsql As String
                Strsql = "insert into contract_document (contract_id,contract_no,document_id "

                If FileUpload1.HasFile Then
                    Strsql &= ",file_path,mime_type "
                End If


                Strsql &= ",title,page,creation_by,created_date,updated_by,updated_date)values  "
                Strsql &= "('" & lblId.Text & "','" & txtNo.Text & "','" & lblDocId.Text & "' "

                Dim MIMEType As String = Nothing
                If FileUpload1.HasFile Then
                    Dim extension As String = System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName).ToLower()


                    Select Case extension
                        Case ".jpg", ".jpeg", ".jpe"
                            MIMEType = ".jpg"
                            Strsql &= ",'" & strPath & "" & lblId.Text & "-" & lblDocId.Text & "" & MIMEType & "','" & MIMEType & "'"
                        Case ".csv"
                            MIMEType = ".csv"
                            Strsql &= ",'" & strPath & "" & lblId.Text & "-" & lblDocId.Text & "" & MIMEType & "','" & MIMEType & "'"
                        Case ".xls"
                            MIMEType = ".xls"
                            Strsql &= ",'" & strPath & "" & lblId.Text & "-" & lblDocId.Text & "" & MIMEType & "','" & MIMEType & "'"
                        Case ".xlsx"
                            MIMEType = ".xlsx"
                            Strsql &= ",'" & strPath & "" & lblId.Text & "-" & lblDocId.Text & "" & MIMEType & "','" & MIMEType & "'"
                        Case ".pdf"
                            MIMEType = ".pdf"
                            Strsql &= ",'" & strPath & "" & lblId.Text & "-" & lblDocId.Text & "" & MIMEType & "','" & MIMEType & "'"
                        Case ".doc"
                            MIMEType = ".doc"
                            Strsql &= ",'" & strPath & "" & lblId.Text & "-" & lblDocId.Text & "" & MIMEType & "','" & MIMEType & "'"
                        Case ".docx"
                            MIMEType = ".docx"
                            Strsql &= ",'" & strPath & "" & lblId.Text & "-" & lblDocId.Text & "" & MIMEType & "','" & MIMEType & "'"
                        Case ".txt"
                            MIMEType = ".txt"
                            Strsql &= ",'" & strPath & "" & lblId.Text & "-" & lblDocId.Text & "" & MIMEType & "','" & MIMEType & "'"
                        Case ".htm", ".html"
                            MIMEType = ".html"
                            Strsql &= ",'" & strPath & "" & lblId.Text & "-" & lblDocId.Text & "" & MIMEType & "','" & MIMEType & "'"
                        Case Else
                            MC.MessageBox(Me, "Not a valid file format")
                            Exit Sub
                    End Select

                End If

                Strsql &= ",'" & txtDocDetail.Text & "','" & txtDocPage.Text & "','" & sEmpNo & "',getdate(),"
                Strsql &= "'" & sEmpNo & "',getdate())"


                Dim Y As Integer = MD.Execute(Strsql)
                If Y > 0 Then
                    Func.UploadFile(sEmpNo, FileUpload1, lblId.Text & "-" & lblDocId.Text & MIMEType, strPath)
                    Me.gData()
                    Me.MyGridBind()
                    MC.MessageBox(Me, "บันทึกลงฐานข้อมูลเรียบร้อยแล้ว")


                    lblStatus.Text = ""
                    lblDocId.Text = ""

                    lblADocPage.Text = ""
                    lblAFile.Text = ""
                    lblADocDeatil.Text = ""
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
        End If

    End Sub
    Private Sub UploadFile()
        'Upload and save file at directory
        Dim sEmpNo As String = Session("EmpNo")
        If FileUpload1.HasFile Then
            Dim extension As String = System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName).ToLower()
            Dim MIMEType As String = Nothing
            Dim fname As String = lblId.Text + "-" + lblDocId.Text
            Try

                Select Case extension

                    Case ".jpg", ".jpeg", ".jpe"
                        MIMEType = ".jpg"
                        Dim X As String = Path.GetFileName("" & fname & "" & MIMEType & "")
                        X = Server.MapPath("..\Document\Contract\" & X)
                        FileUpload1.PostedFile.SaveAs(X)
                    Case ".csv"
                        MIMEType = ".csv"
                        Dim X As String = Path.GetFileName("" & fname & "" & MIMEType & "")
                        X = Server.MapPath("..\Document\Contract\" & X)
                        FileUpload1.PostedFile.SaveAs(X)
                    Case ".xls"
                        MIMEType = ".xls"
                        Dim X As String = Path.GetFileName("" & fname & "" & MIMEType & "")
                        X = Server.MapPath("..\Document\Contract\" & X)
                        FileUpload1.PostedFile.SaveAs(X)
                    Case ".xlsx"
                        MIMEType = ".xlsx"
                        Dim X As String = Path.GetFileName("" & fname & "" & MIMEType & "")
                        X = Server.MapPath("..\Document\Contract\" & X)
                        FileUpload1.PostedFile.SaveAs(X)
                    Case ".pdf"
                        MIMEType = ".pdf"
                        Dim X As String = Path.GetFileName("" & fname & "" & MIMEType & "")
                        X = Server.MapPath("..\Document\Contract\" & X)
                        FileUpload1.PostedFile.SaveAs(X)
                    Case ".doc"
                        MIMEType = ".doc"
                        Dim X As String = Path.GetFileName("" & fname & "" & MIMEType & "")
                        X = Server.MapPath("..\Document\Contract\" & X)
                        FileUpload1.PostedFile.SaveAs(X)
                    Case ".docx"
                        MIMEType = ".docx"
                        Dim X As String = Path.GetFileName("" & fname & "" & MIMEType & "")
                        X = Server.MapPath("..\Document\Contract\" & X)
                        FileUpload1.PostedFile.SaveAs(X)
                    Case ".txt"
                        MIMEType = ".txt"
                        Dim X As String = Path.GetFileName("" & fname & "" & MIMEType & "")
                        X = Server.MapPath("..\Document\Contract\" & X)
                        FileUpload1.PostedFile.SaveAs(X)
                    Case ".htm", ".html"
                        MIMEType = ".html"
                        Dim X As String = Path.GetFileName("" & fname & "" & MIMEType & "")
                        X = Server.MapPath("..\Document\Contract\" & X)
                        FileUpload1.PostedFile.SaveAs(X)
                    Case Else
                        MC.MessageBox(Me, "Not a valid file format")
                        Exit Sub
                End Select
            Catch ex As Exception
                Dim ses As String = System.Web.HttpContext.Current.Session.SessionID
                Dim url As String = HttpContext.Current.Request.FilePath
                Dim browser As String = " Type:" + Request.Browser.Type + " Version:" + Request.Browser.Version + " Browser:" + Request.Browser.Browser

                MC.MessageBox(Me, "Can not upload file!")
                MC.ErrLog(url, ses, ex.ToString, sEmpNo, browser)
            End Try
        End If

    End Sub
    Protected Sub bCancelFile_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bCancelFile.Click
        txtDocDetail.Text = ""
        txtDocPage.Text = ""
        lblDocId.Text = ""
        lblStatus.Text = ""

        lblAFile.Text = ""
        lblADocDeatil.Text = ""
        lblADocPage.Text = ""
    End Sub
    Protected Sub bSaveAndSend_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bSaveAndSend.Click
        Dim X As String = Request.QueryString("id")
        Dim sEmpNo As String = Session("EmpNo")

        If txtContractName.Text.Trim = "" Then
            Me.ClearAlert()
            lblContractName.Text = "กรุณากรอกชื่อสัญญา"
            txtContractName.Focus()
            Exit Sub
        End If

        If txtReceiveDate.Text.Year = 1 Then
            Me.ClearAlert()
            lblAdate1.Text = "กรุณากรอกวันที่รับเรื่อง"
            Exit Sub
        End If

        If ddlProcess.SelectedValue = "0" Then
            Me.ClearAlert()
            lblProcess.Text = "กรุณาเลือกวิธีจัดจ้าง"
            Exit Sub
        End If
        If txtMatetial.Text.Trim = "" Then
            Me.ClearAlert()
            lblMaterail.Text = "กรุณากรอกสิ่งที่จะซื้อ/จ้าง"
            txtMatetial.Focus()
            Exit Sub
        End If
        If txtTaxId.Text.Trim = "" Then
            Me.ClearAlert()
            lblTaxId.Text = "กรุณากรอกเลขที่ประจำตัวผู้เสียภาษีของคู่สัญญา"
            txtTaxId.Focus()
            Exit Sub
        End If
        If txtSaleName.Text.Trim = "" Then
            Me.ClearAlert()
            lblSaleName.Text = "กรุณากรอกชื่อผู้ขาย/ผู้รับจ้าง"
            txtSaleName.Focus()
            Exit Sub
        End If
        If txtKeyword.Text.Trim = "" Then
            Me.ClearAlert()
            lblKeyword.Text = "กรุณากรอกคำค้นหา"
            txtKeyword.Focus()
            Exit Sub
        End If

        If ddlApp1.SelectedValue = "0" Then
            Me.ClearAlert()
            lblAApp1.Text = "กรุณาเลือกชื่อหัวหนัานิติกรรมและคดี"
            Exit Sub
        End If
        If ddlApp2.SelectedValue = "0" Then
            Me.ClearAlert()
            lblAApp2.Text = "กรุณาเลือกชื่อผู้อำนวยการ"
            Exit Sub
        End If

        If lblIdName3.Text.Trim = "" Then
            Me.ClearAlert()
            lblBuyName.Text = "กรุณาเลือกชื่อผู้มีอำนาจลงนาม"
            Exit Sub
        End If

        If ddlGuarantee.SelectedValue <> 0 And txtGuaranteeId.Text = "" Then
            MC.MessageBox(Me, "กรุณากรอกเลขที่หลักประกัน")
            txtGuaranteeId.Focus()
            Exit Sub
        End If

        If ddlGuarantee.SelectedValue = 0 And txtGuaranteeId.Text <> "" Then
            lblGaranteeId.Text = "กรุณาเลือกหลักประกันสัญญา"
            Exit Sub
        End If

        If txtMoney.Text.Trim = "" Then
            Me.ClearAlert()
            lblMoney.Text = "กรุณากรอกจำนวนเงินในสัญญา"
            txtMoney.Focus()
            Exit Sub
        End If


        If FCKeditor1.Value.Trim = "" Then
            Me.ClearAlert()
            MC.MessageBox(Me, "กรุณากรอกรายละเอียด")
            FCKeditor1.Focus()
            TabContainer1.ActiveTabIndex = 1
            Exit Sub
        End If

        Me.UpdateActive()
        Me.Auto()

        If txtNo.Text = "" Then
            Me.GenContractNo()
        End If

        Me.SaveData(2)

        'Me.ClearData()
        'lblMainStatus.Text = "Add"

        'Me.gData()
        'Me.MyGridBind()

        Response.Redirect("../Src/ContractPreviewList.aspx", True)
    End Sub
    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
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

    Protected Sub bSaveFCK_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bSaveFCK.Click
        Dim sEmpNo As String = Session("EmpNo")
        Dim strsql As String

        Call bSave_Click(sender, e)

        If txtNo.Text.Trim = "" Then
            MC.MessageBox(Me, "กรุณาบันทึกข้อมูลพื้นฐานก่อน")
            Exit Sub
        End If

        Try

            strsql = "update contract_data set message=?, "
            strsql &= " updated_by=?,updated_date=? "
            strsql &= " where contract_id=? "

            cn = New OleDbConnection(MD.Strcon)
            cmd = New OleDbCommand(strsql, cn)
            MD.CreateParam(cmd, "TTDT")


            cmd.Parameters("@P1").Value = FCKeditor1.Value
            cmd.Parameters("@P2").Value = sEmpNo
            cmd.Parameters("@P3").Value = DateTime.Parse(Date.Now)
            cmd.Parameters("@P4").Value = lblId.Text

            cn.Open()
            cmd.ExecuteNonQuery()
            MC.MessageBox(Me, "บันทึกลงฐานข้อมูลเรียบร้อยแล้ว")


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

    Protected Sub link2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles link2.Click
        Dim X As String = Request.QueryString("id")

        If X <> "" Then
            Response.Redirect("../Src/ContractEditList.aspx", True)
        Else
            Response.Redirect("../Src/ContractPreviewList.aspx", True)
        End If

    End Sub
    Protected Sub linkHome_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles linkHome.Click
        Response.Redirect(MD.pHome, True)
    End Sub

    Protected Sub bDelContract_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bDelContract.Click
        txtNameContract.Text = ""
        lblIdContract.Text = ""
    End Sub

    Protected Sub bDelTitle_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bDelTitle.Click
        txtTitle.Text = ""
        lblTitle.Text = ""
    End Sub

    Protected Sub bEditNo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bEditNo.Click
        txtNo.ReadOnly = False
    End Sub
End Class
