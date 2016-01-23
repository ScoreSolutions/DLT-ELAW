Imports System.Data

Partial Class Src_DrafLawCompare
    Inherits System.Web.UI.Page
    Dim DVLst As DataView
    Dim iRow As Integer
    Dim MD As New MainData
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            SetRefIdCombo()
        End If
    End Sub
    Private Sub SetRefIdCombo()
        Dim sEmpNo As String = Page.User.Identity.Name
        Dim uData As New LoginUser
        uData.GetUserData(sEmpNo)

        Dim sql As String = ""
        sql += " select  ld.ref_id , ld.title "
        sql += " from law_data ld"
        sql += " inner join (select distinct ld.ref_id , (select MAX(law_id) "
        sql += "            from law_data l"
        sql += "            where(l.ref_id = ld.ref_id)"
        sql += "            group by ref_id) maxid"
        sql += "            from law_data ld) l on l.maxid=ld.law_id "
        sql += " where 1=1 "


        If uData.PosID = GetLeaderPosID(uData.DivID) Then
            If uData.PosID <> Constant.DirectorPosID Then
                sql += " and ld.sendto = '" & sEmpNo & "' "
            End If
        ElseIf uData.PosID <> Constant.DirectorPosID Then
            sql += " and ld.creation_by = '" & sEmpNo & "' "
        End If
        sql += " order by ld.ref_id desc"

        ddlRefID.SetItemList(MD.GetDataTable(sql), "title", "ref_id")
        ddlRefID_SelectedIndexChanged(Nothing, Nothing)
    End Sub
    Private Function GetLeaderPosID(ByVal DivID As Integer) As Integer
        Dim sql As String = ""
        sql += " select pos_id from division where div_id = " & DivID
        Return Convert.ToInt64(MD.GetDataTable(sql).Rows(0)("pos_id"))
    End Function
    Protected Sub ddlRefID_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlRefID.SelectedIndexChanged
        Dim sql As String = ""
        sql += " select ld.law_id,ld.law_id + ' ประเภท: ' + lt.type_name + ' สถานะ: ' + ls.status_name law_desc"
        sql += " from law_data ld "
        sql += " inner join law_status ls on ls.status_id=ld.status_id"
        sql += " inner join law_type lt on lt.type_id=ls.type_id "
        sql += " where ld.ref_id='" & ddlRefID.SelectedValue & "' "
        sql += " order by ld.law_id desc"

        Dim dt As DataTable = MD.GetDataTable(sql)

        ddlLawIdLeft.SetItemList(dt, "law_desc", "law_id")
        ddlLawIdRight.SetItemList(dt, "law_desc", "law_id")
    End Sub
    Protected Sub bResult_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bResult.Click
        Dim popupScript As String = " window.open('../Src/DrafLawComparePreview.aspx?LawIdLeft=" & ddlLawIdLeft.SelectedValue & "&LawIdRight=" & ddlLawIdRight.SelectedValue & "', '_blank','resizable=no,scrollbars=no,status=no,location=no,top=10,left=10,width=1015,height=700');"
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Compare", popupScript, True)
    End Sub

    Protected Sub linkHome_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles linkHome.Click
        Response.Redirect(MD.pHome, True)
    End Sub
End Class
