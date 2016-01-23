Imports System.IO
Imports System.Data
Imports System.Data.OleDb
Partial Class Src_BookOutPreview
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
                sql &= "b.status_id,s.status_name,b.topic,b.keyword,b.dates,e.firstname+' '+e.lastname createname, "
                sql &= "b.user_sign,e1.firstname+' '+e1.lastname signname,b.priority_id,p.priority_name,b.sendto,e2.firstname+' '+e2.lastname sendname, "
                sql &= "b.sendto_app,b.sendto_comment "
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
        lblDateThai.Text = Left(lblDate.Text, 2) + " " + mnt + " " + Right(lblDate.Text, 4)

 
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
