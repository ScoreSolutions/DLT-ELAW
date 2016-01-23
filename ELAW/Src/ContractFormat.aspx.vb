Imports System.Data
Imports System.Data.OleDb
Partial Class Src_ContractFormat
    Inherits System.Web.UI.Page
    Dim MD As New MainData
    Dim MC As New MainClass
    Dim DV As DataView
    Public iRow As Integer
    Dim DS As DataSet
    Dim iRec As Integer
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Try
                Me.GetData()
                Me.MyDataBind()
            Catch ex As Exception

            End Try

        Else
            DS = Session("GetContractFormat")
            iRec = ViewState("iRec")
        End If
    End Sub
    Private Sub GetData()
        Dim X As String = Request.QueryString("id")
        Dim sql As String

        sql = "select * from contract_subtype s "
        sql &= " where s.subtype_id = '" & X & "'"

        DS = MD.GetDataset(sql)
        Session("GetContractFormat") = DS
        iRec = 0
        ViewState("iRec") = iRec
    End Sub
    Public Sub MyDataBind()
        FCKeditor2.DataBind()
        lblSubType.DataBind()
    End Sub
    Public Function BindField(ByVal FieldName As String) As String
        Dim DT As DataTable = DS.Tables(0)
        Select Case FieldName
            Case "cost"
                If IsDBNull(DT.Rows(iRec)("cost")) Then
                    Return "0.00"

                Else
                    Dim P1 As Double = DT.Rows(iRec)("cost")
                    Return P1.ToString("#,##0.00")
                End If
            Case "date_rp"
                If IsDBNull(DT.Rows(iRec)("date_rp")) Then
                    Return "-"

                Else
                    Dim P1 As Date = DT.Rows(iRec)("date_rp")
                    Return P1.ToString("dd/MM/yyyy HH:mm")
                End If
            Case Else
                Return DT.Rows(iRec)(FieldName) & ""
        End Select
    End Function
    Protected Sub bSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bSave.Click
        Dim X As String = Request.QueryString("id")
        Try
            Dim Strsql As String
            Strsql = "Update contract_subtype Set format='" & FCKeditor2.Value & "' "
            Strsql &= "where subtype_id='" & X & "'"


            Dim Y As Integer = MD.Execute(Strsql)
            If Y > 0 Then
                MC.MessageBox(Me, "บันทึกลงฐานข้อมูลเรียบร้อยแล้ว")
            Else
                MC.MessageBox(Me, "ไม่สามารถบันทึกลงฐานข้อมูลได้")
            End If
        Catch ex As Exception
            MC.MessageBox(Me, ex.ToString)
        End Try
    End Sub
    Protected Sub bCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bCancel.Click
        FCKeditor2.Value = System.String.Empty
    End Sub
    Protected Sub link2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles link2.Click
        Response.Redirect("../Src/ContractFormatList.aspx", True)
    End Sub
End Class
