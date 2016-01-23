Imports System.Data
Imports System.Data.OleDb
Partial Class Src_PrintLaw
    Inherits System.Web.UI.Page
    Dim cn As OleDbConnection
    Dim cmd As OleDbCommand
    Dim MD As New MainData
    Dim MC As New MainClass
    Dim DV As DataView
    Public iRow As Integer
    Dim DS As DataSet
    Dim iRec As Integer
    Dim DVLst As DataView
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
        Dim X As String = Request.QueryString("id")

        If Not Page.IsPostBack Then

            If X <> "" Then
                'Preview, Approve, Edit
                Dim sql As String

                sql = "select l.law_id,l.title,l.message "
                sql &= " from law_data l "
                sql &= " where l.law_id='" & X & "'"


                DS = MD.GetDataset(sql)
                Session("PrintLawData") = DS
                iRec = 0
                ViewState("iRec") = iRec


                Me.MyDataBind()

                If Request.QueryString("type") = "D" Then
                    lblDraft.Visible = True
                Else
                    lblCrut.Visible = True
                End If

            Else
                'Add New
                Dim sql As String


                sql = "select l.law_id,l.title,l.message "
                sql &= " from law_data l "


                DS = MD.GetDataset(sql)
                Session("PrintLawData") = DS
                iRec = 0
                ViewState("iRec") = iRec


            End If

        Else


            DS = Session("PrintLawData")
            iRec = ViewState("iRec")

        End If


    End Sub
    Public Function BindField(ByVal FieldName As String) As String
        'BindField when Preview,Approve,Edit
        Dim DT As DataTable = DS.Tables(0)
        Select Case FieldName
            Case "creation_date"
                If IsDBNull(DT.Rows(iRec)("creation_date")) Then
                    Return ""
                Else
                    Dim P1 As Date = DT.Rows(iRec)("creation_date")
                    Return P1.ToString("dd/MM/yyyy")
                End If
            Case Else
                Return DT.Rows(iRec)(FieldName) & ""
        End Select
    End Function
    Public Sub MyDataBind()
        lblTitle.DataBind()
        lblMsg.DataBind()
    End Sub
    Protected Sub ImageButton1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
        ImageButton1.Visible = False
        Response.Write("<script language=javascript>")
        Response.Write("{print();}")
        Response.Write("</script>")
    End Sub

End Class