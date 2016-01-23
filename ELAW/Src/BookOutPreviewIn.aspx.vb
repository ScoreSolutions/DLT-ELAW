Imports System.Data
Imports System.Data.OleDb
Partial Class Src_BookOutPreviewIn
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
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim X As String = Request.QueryString("id")
        Dim status As String = Request.QueryString("status")

        If Not Page.IsPostBack Then

            If X <> "" Then
                'Preview, Approve, Edit
                Dim sql As String

                sql = " select b.bookout_id,b.system_no,b.bookout_no,b.bookkind_id,k.bookkind_name,b.present,  "
                sql &= "b.message,b.postscript,b.postname,b.post_pos,b.comment,b.contact,  "
                sql &= "b.status_id,s.status_name,b.topic,b.keyword,b.dates,f.name createname, "
                sql &= "b.user_sign,f1.name signname,b.priority_id,p.priority_name,b.sendto,f2.name sendname,f2.short_name pos, "
                sql &= "b.sendto_app,b.sendto_comment,f3.name sendname1,f3.short_name pos1,b.sendto_date,b.sendto1_date, "
                sql &= "b.sendto_comment1 "
                sql &= "from bookout_data b inner join book_kind k "
                sql &= "on b.bookkind_id=k.bookkind_id inner join book_status s "
                sql &= "on b.status_id=s.status_id left join fullname f "
                sql &= "on b.creation_by=f.empid left join fullname f1 "
                sql &= "on b.user_sign=f1.empid inner join book_priority p "
                sql &= "on b.priority_id=p.priority_id left join fullname f2 "
                sql &= "on b.sendto=f2.empid left join fullname f3 "
                sql &= "on b.sendto1=f3.empid "
                sql &= "where b.active=1 "
                sql &= "and b.bookout_id='" & X & "'"

                DS = MD.GetDataset(sql)
                Session("BookOutWaitData") = DS
                iRec = 0
                ViewState("iRec") = iRec


                Me.MyDataBind()

            Else
                'Add New
                Dim sql As String

                sql = "select * from bookout_data "

                DS = MD.GetDataset(sql)
                Session("BookOutWaitData") = DS
                iRec = 0
                ViewState("iRec") = iRec

            End If

        Else

            DS = Session("BookOutWaitData")
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
            Case "sendto_date"
                If IsDBNull(DT.Rows(iRec)("sendto_date")) Then
                    Return ""
                Else
                    Dim P1 As Date = DT.Rows(iRec)("sendto_date")
                    Return P1.ToString("dd MMM yyyy")
                End If
            Case "sendto1_date"
                If IsDBNull(DT.Rows(iRec)("sendto1_date")) Then
                    Return ""
                Else
                    Dim P1 As Date = DT.Rows(iRec)("sendto1_date")
                    Return P1.ToString("dd MMM yyyy")
                End If

            Case Else
                Return DT.Rows(iRec)(FieldName) & ""
        End Select
    End Function
    Public Sub MyDataBind()

        lblDate.DataBind()
        lblTopic.DataBind()
        lblPresent.DataBind()
        lblMessage.DataBind()
        lblPostScript.DataBind()
        lblPostName.DataBind()
        lblPostPosition.DataBind()
        lblBookOutNo.DataBind()

        Dim mnt As String = MonthThai(Right(Left(lblDate.Text, 5), 2))
        Dim day As String
        If Left(lblDate.Text, 1) = "0" Then
            day = Right(Left(lblDate.Text, 2), 1)
        Else
            day = Left(lblDate.Text, 2)
        End If
        lblDateThai.Text = day + " " + mnt + " " + Right(lblDate.Text, 4)
        lblPos.DataBind()
        lblPos1.DataBind()
        lblComment.DataBind()
        lblName.DataBind()
        lblSendtoDate.DataBind()
        lblComment1.DataBind()
        lblName1.DataBind()
        lblPos2.DataBind()
        lblSendtoDate1.DataBind()
    End Sub
    Protected Sub ImageButton1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
        'Print Document
        ImageButton1.Visible = False
        Response.Write("<script language=javascript>")
        Response.Write("{print();}")
        Response.Write("</script>")
    End Sub
    Function MonthThai(ByVal MonthThaiExIndex As String) As String
        Dim strMonthThai As String = ""
        Select Case MonthThaiExIndex
            Case "01"
                strMonthThai = "มกราคม"
            Case "02"
                strMonthThai = "กุมภาพันธ์"
            Case "03"
                strMonthThai = "มีนาคม"
            Case "04"
                strMonthThai = "เมษายน"
            Case "05"
                strMonthThai = "พฤษภาคม"
            Case "06"
                strMonthThai = "มิถุนายน"
            Case "07"
                strMonthThai = "กรกฎาคม"
            Case "08"
                strMonthThai = "สิงหาคม"
            Case "09"
                strMonthThai = "กันยายน"
            Case "10"
                strMonthThai = "ตุลาคม"
            Case "11"
                strMonthThai = "พฤศจิกายน"
            Case "12"
                strMonthThai = "ธันวาคม"
        End Select
        Return strMonthThai

    End Function
End Class
