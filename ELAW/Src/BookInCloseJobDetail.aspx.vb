Imports System.Data
Imports System.Data.OleDb
Partial Class Src_BookInCloseJobDetail
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

        Me.ChkPermis()

        If Not Page.IsPostBack Then

            If X <> "" Then
                'Preview, Approve, Edit
                Dim sql As String

                sql = " select b.bookin_id,b.system_no,b.bookin_no,b.bookkind_id,k.bookkind_name,  "
                sql &= "b.status_id,s.status_name,b.topic,b.keyword,b.recieve_date,b.stamp_date,b.from_name, "
                sql &= "b.sendto,b.present,b.priority_id,e.firstname+' '+e.lastname creation_name,e1.firstname+' '+e1.lastname send_name,"
                sql &= "p.priority_name "
                sql &= "from bookin_data b inner join book_kind k "
                sql &= "on b.bookkind_id=k.bookkind_id inner join book_status s "
                sql &= "on b.status_id=s.status_id inner join employee e "
                sql &= "on b.creation_by=e.empid inner join employee e1  "
                sql &= "on b.sendto=e1.empid inner join book_priority p "
                sql &= "on b.priority_id=p.priority_id "
                sql &= " where b.bookin_id='" & X & "'"


                DS = MD.GetDataset(sql)
                Session("BookInCloseJob") = DS
                iRec = 0
                ViewState("iRec") = iRec


                Me.MyDataBind()

            Else
                'Add New
                Dim sql As String

                sql = " select b.bookin_id,b.system_no,b.bookin_no,b.bookkind_id,k.bookkind_name,  "
                sql &= "b.status_id,s.status_name,b.topic,b.keyword,b.recieve_date,b.stamp_date,b.from_name, "
                sql &= "b.sendto,b.present,b.priority_id,e.firstname+' '+e.lastname creation_name,e1.firstname+' '+e1.lastname send_name,"
                sql &= "p.priority_name "
                sql &= "from bookin_data b inner join book_kind k "
                sql &= "on b.bookkind_id=k.bookkind_id inner join book_status s "
                sql &= "on b.status_id=s.status_id inner join employee e "
                sql &= "on b.creation_by=e.empid inner join employee e1  "
                sql &= "on b.sendto=e1.empid inner join book_priority p "
                sql &= "on b.priority_id=p.priority_id "

                DS = MD.GetDataset(sql)
                Session("BookInCloseJob") = DS
                iRec = 0
                ViewState("iRec") = iRec

            End If

        Else


            DS = Session("BookInCloseJob")
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
            Case "recieve_date"
                If IsDBNull(DT.Rows(iRec)("recieve_date")) Then
                    Return ""
                Else
                    Dim P1 As Date = DT.Rows(iRec)("recieve_date")
                    Return P1.ToString("dd/MM/yyyy HH:mm")
                End If
            Case "stamp_date"
                If IsDBNull(DT.Rows(iRec)("stamp_date")) Then
                    Return ""
                Else
                    Dim P1 As Date = DT.Rows(iRec)("stamp_date")
                    Return P1.ToString("dd/MM/yyyy")
                End If
            Case Else
                Return DT.Rows(iRec)(FieldName) & ""
        End Select
    End Function
    Public Sub MyDataBind()
        lblBookNo.DataBind()
        lblBookType.DataBind()
        lblCreate.DataBind()
        lblFrom.DataBind()
        lblKeyword.DataBind()
        lblPresent.DataBind()
        lblPriority.DataBind()
        lblRecieveDate.DataBind()
        lblSendTo.DataBind()
        lblStampDate.DataBind()
        lblStatus.DataBind()
        lblTopic.DataBind()
    End Sub
    Protected Sub bApp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bApp.Click
        If txtComment.Text = "" Then
            lblAComment.Text = "***กรุณากรอกบันทึก"

            Exit Sub
        End If

        Me.UpdateActive()
        Me.Auto()

        Me.SaveData()
    End Sub
    Private Sub SaveData()
        Dim X As String = Request.QueryString("id")
        Dim sEmpNo As String = Session("EmpNo")
        Dim strsql As String = ""

        strsql = "insert into bookin_data (bookin_id,system_no,bookin_no,from_name,bookkind_id,status_id,priority_id, "
        strsql &= "keyword,topic,recieve_date,stamp_date,present,sendto,sendto_comment,sendto1,sendto_comment1,  "
        strsql &= "sendto2,sendto_comment2,close_comment,"
        strsql &= "creation_by,created_date,updated_by,updated_date,runno )"
        strsql &= "select '" & lblIdNew.Text & "',system_no,bookin_no,from_name,bookkind_id,5,priority_id, "
        strsql &= "keyword,topic,recieve_date,stamp_date,present,sendto,sendto_comment,sendto1,sendto_comment1,  "
        strsql &= "sendto2,sendto_comment2,'" & txtComment.Text & "',"
        strsql &= "creation_by,created_date,'" & sEmpNo & "',getdate(),runno "
        strsql &= "from bookin_data where bookin_id='" & X & "'"

        Dim Y As Integer = MD.Execute(strsql)
        If Y > 0 Then
            MC.MessageBox(Me, "บันทึกลงฐานข้อมูลเรียบร้อยแล้ว")
            lblAComment.Text = ""
            Response.Redirect("../Src/BookInCloseJob.aspx?", True)
        Else
            MC.MessageBox(Me, "ไม่สามารถบันทึกลงฐานข้อมูลได้")
        End If

    End Sub
    Private Sub Auto()
        'Genarate Bookin_id
        Dim sqlTmp As String = ""
        Dim comTmp As OleDbCommand = New OleDbCommand
        Dim drTmp As OleDbDataReader
        Dim tmpMemberID As Integer = 0
        Dim tmpMemberID2 As String = ""


        Dim sYear As String
        sYear = Date.Now.Year

        Dim sAuto As String = sYear

        sqlTmp = "SELECT TOP 1 bookin_id FROM bookin_data "
        sqlTmp &= " WHERE left(bookin_id,4) ='" & sAuto & "'"
        sqlTmp &= " ORDER BY bookin_id DESC"

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

                tmpMemberID2 = Right(drTmp.Item("bookin_id"), 4)

                tmpMemberID = CInt(tmpMemberID2) + 1
                lblIdNew.Text = sAuto + tmpMemberID.ToString("-0000")

            End With
        Catch
            lblIdNew.Text = sAuto + "-0001"
        End Try
        cn.Close()

    End Sub
    Private Sub UpdateActive()
        Dim X As String = Request.QueryString("id")
        Dim sEmpNo As String = Session("EmpNo")

        Try
            Dim Strsql As String
            Strsql = "update bookin_data set  "
            Strsql &= " active = 0 "
            Strsql &= " where bookin_id='" & X & "'"

            Dim Y As Integer = MD.Execute(Strsql)

        Catch ex As Exception
            Dim ses As String = System.Web.HttpContext.Current.Session.SessionID
            Dim url As String = HttpContext.Current.Request.FilePath
            Dim browser As String = " Type:" + Request.Browser.Type + " Version:" + Request.Browser.Version + " Browser:" + Request.Browser.Browser

            MC.MessageBox(Me, ex.ToString)
            MC.ErrLog(url, ses, ex.ToString, sEmpNo, browser)
        End Try

    End Sub
    Protected Sub bAppCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bAppCancel.Click
        txtComment.Text = ""
        lblAComment.Text = ""
    End Sub
    Protected Sub link2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles link2.Click
        Response.Redirect("../Src/BookInCloseJob.aspx", True)
    End Sub
    Protected Sub linkHome_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles linkHome.Click
        Response.Redirect(MD.pHome, True)
    End Sub

End Class

