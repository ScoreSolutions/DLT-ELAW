Imports System.IO
Imports System.Data
Imports System.Data.OleDb
Partial Class Src_BookOutRunNo
    Inherits System.Web.UI.Page
    Dim cn As OleDbConnection
    Dim cmd As OleDbCommand
    Dim MD As New MainData
    Dim MC As New MainClass
    Dim DV As DataView
    Dim DVLst As DataView
    Public iRow As Integer
    Dim DS As DataSet
    Dim iRec As Integer
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
    Private Sub BookNo()
        Dim strsql As String
        Dim oDs As New DataSet

        strsql = "select book_no from book_type where booktype_id=2 "

        oDs = MD.GetDataset(strsql)
        If oDs.Tables(0).Rows.Count > 0 Then
            txtBookNo.Text = oDs.Tables(0).Rows(0).Item("book_no").ToString
        End If
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim X As String = Request.QueryString("id")
        Dim status As String = Request.QueryString("status")

        If Not Page.IsPostBack Then

            If X <> "" Then
                'Preview, Approve, Edit
                Dim sql As String

                sql = " select b.bookout_id,b.system_no,b.bookout_no,b.bookkind_id,k.bookkind_name,b.present,  "
                sql &= "b.message,b.postscript,b.postname,b.post_pos,b.comment,b.contact,  "
                sql &= "b.status_id,s.status_name,b.topic,b.keyword,b.dates,e.firstname+' '+e.lastname createname, "
                sql &= "b.user_sign,e1.firstname+' '+e1.lastname signname,b.priority_id,p.priority_name,b.sendto,e2.firstname+' '+e2.lastname sendname "
                sql &= "from bookout_data b inner join book_kind k "
                sql &= "on b.bookkind_id=k.bookkind_id inner join book_status s "
                sql &= "on b.status_id=s.status_id inner join employee e "
                sql &= "on b.creation_by=e.empid inner join employee e1 "
                sql &= "on b.user_sign=e1.empid inner join book_priority p "
                sql &= "on b.priority_id=p.priority_id inner join employee e2 "
                sql &= "on b.sendto=e2.empid "
                sql &= "where b.active=1 "
                sql &= "and b.bookout_id='" & X & "'"

                DS = MD.GetDataset(sql)
                Session("BookOutRunNo") = DS
                iRec = 0
                ViewState("iRec") = iRec

                lblId.Text = X

                Me.MyDataBind()
                Me.BookNo()

            Else
                'Add New

                Dim sql As String

                sql = "select * from bookout_data "

                DS = MD.GetDataset(sql)
                Session("BookOutRunNo") = DS
                iRec = 0
                ViewState("iRec") = iRec


                lblId.Text = ""
                lblMainStatus.Text = "Add"

            End If
        Else

            DS = Session("BookOutRunNo")
            iRec = ViewState("iRec")
        End If

    End Sub
    Public Function BindField(ByVal FieldName As String) As String
        'BindField when Preview,Approve,Edit
        Dim DT As DataTable = DS.Tables(0)
        Select Case FieldName
            Case "cost"
                If IsDBNull(DT.Rows(iRec)("cost")) Then
                    Return "0.00"
                Else
                    Dim P1 As Double = DT.Rows(iRec)("cost")
                    Return P1.ToString("#,##0.00")
                End If
            Case "dates"
                If IsDBNull(DT.Rows(iRec)("dates")) Then
                    Return ""
                Else
                    Dim P1 As Date = DT.Rows(iRec)("dates")
                    Return P1.ToString("dd/MM/yyyy")
                End If

            Case Else
                Return DT.Rows(iRec)(FieldName) & ""
        End Select
    End Function
    Public Sub MyDataBind()
        txtBookNo.DataBind()
        lblName.DataBind()
        lblStatus.DataBind()
        lblDate.DataBind()
        lblCreateName.DataBind()
        lblType.DataBind()
        lblPriority.DataBind()
        lblTopic.DataBind()
        lblPresent.DataBind()
        lblMessage.DataBind()
        lblPostScript.DataBind()
        lblPostName.DataBind()
        lblPostPosition.DataBind()
        lblContact.DataBind()
        lblComment.DataBind()
        lblKeyword.DataBind()
        lblSendto.DataBind()
    End Sub
    Protected Sub bSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bSave.Click
        Dim sEmpNo As String = Session("EmpNo")
        Dim strsql As String
        Dim chkState As String = "10"


        If txtBookNo.Text = "" Then
            lblChkBookNo.Text = "***กรุณากรอกเลขที่หนังสือ"
            txtBookNo.Focus()
            Exit Sub
        End If


        Me.UpdateActive()
        Me.Auto()

        strsql = "insert into bookout_data (bookout_id,system_no,dates,bookkind_id, "
        strsql &= "user_request,user_sign,status_id,priority_id,keyword,topic,present, "
        strsql &= "message,postscript,postname,post_pos,contact,comment,sendto,sendto_app,sendto_comment, "
        strsql &= "sendto1,sendto1_app,sendto_comment1,bookout_no,"
        strsql &= "creation_by,created_date,updated_by,updated_date,ref_type,ref_id,ref_title,booktype_id,run_date) "
        strsql &= "select '" & lblIdNew.Text & "',system_no,dates,bookkind_id,   "
        strsql &= "user_request,user_sign,'" & chkState & "',priority_id,keyword,topic,present, "
        strsql &= "message,postscript,postname,post_pos,contact,comment,sendto,sendto_app,sendto_comment, "
        strsql &= "sendto1,sendto1_app,sendto_comment1,'" & txtBookNo.Text & "',"
        strsql &= "creation_by,created_date,'" & sEmpNo & "',getdate(),ref_type,ref_id,ref_title,booktype_id,getdate()  "
        strsql &= "from bookout_data where bookout_id='" & lblId.Text & "'"

        Dim Y As Integer = MD.Execute(strsql)
        If Y > 0 Then
            lblChkBookNo.Text = ""
            Response.Redirect("../Src/BookOutRunNoList.aspx?menu=6")
        End If

    End Sub
    Private Sub Auto()
        'Genarate Bookout_id
        Dim sqlTmp As String = ""
        Dim comTmp As OleDbCommand = New OleDbCommand
        Dim drTmp As OleDbDataReader
        Dim tmpMemberID As Integer = 0
        Dim tmpMemberID2 As String = ""


        Dim sYear As String
        sYear = Date.Now.Year

        Dim sAuto As String = sYear

        sqlTmp = "SELECT TOP 1 bookout_id FROM bookout_data "
        sqlTmp &= " WHERE left(bookout_id,4) ='" & sAuto & "'"
        sqlTmp &= " ORDER BY bookout_id DESC"

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

                tmpMemberID2 = Right(drTmp.Item("bookout_id"), 4)

                tmpMemberID = CInt(tmpMemberID2) + 1
                lblIdNew.Text = sAuto + tmpMemberID.ToString("-0000")

            End With
        Catch
            lblIdNew.Text = sAuto + "-0001"
        End Try
        cn.Close()

    End Sub
    Private Sub UpdateActive()
        'ก่อนเปลี่ยนสถานะให้ update active=0
        Dim sEmpNo As String = Session("EmpNo")

        Try
            Dim Strsql As String
            Strsql = "update bookout_data set  "
            Strsql &= " active = 0 "
            Strsql &= " where bookout_id='" & lblId.Text & "'"

            Dim Y As Integer = MD.Execute(Strsql)

        Catch ex As Exception
            Dim ses As String = System.Web.HttpContext.Current.Session.SessionID
            Dim url As String = HttpContext.Current.Request.FilePath
            Dim browser As String = " Type:" + Request.Browser.Type + " Version:" + Request.Browser.Version + " Browser:" + Request.Browser.Browser

            MC.MessageBox(Me, ex.ToString)
            MC.ErrLog(url, ses, ex.ToString, sEmpNo, browser)
        End Try

    End Sub
    Protected Sub linkHome_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles linkHome.Click
        Response.Redirect(MD.pHome, True)
    End Sub
    Protected Sub link2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles link2.Click
        Response.Redirect("../Src/BookOutRunNoList.aspx", True)
    End Sub
End Class
