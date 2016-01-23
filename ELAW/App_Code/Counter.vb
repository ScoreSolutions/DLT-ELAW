Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data

Public Class Counter
    Const PublicValue As String = "PUBLIC"

    Public Shared Function setPublicCounter() As Boolean
        'เริ่มนับเมื่อมีประชาชนเข้าใช้หน้าจอ
        Dim MD As New MainData
        Dim dt As DataTable = MD.GetDataTable("select top 1 qty from law_counter where law_id='" & PublicValue & "'")
        Dim sql As String = ""
        If dt.Rows.Count > 0 Then
            sql += "update law_counter "
            sql += " set qty = " & Convert.ToDouble(dt.Rows(0)("qty")) + 1
            sql += " where law_id='" & PublicValue & "'"
        Else
            'เพิ่มข้อมูลให้กรณีที่ประชาชนเข้าเป็นคนแรก (ถือเป็นเกียรติอย่างสูงเลยนะเนี่ยะ ^o^)
            sql += "insert into law_counter "
            sql += " values('" & PublicValue & "',1 )"
        End If

        Return (MD.Execute(sql) > 0)
    End Function

    Public Shared Function setLawCounter(ByVal LawID As String) As Boolean
        'เริ่มนับเมื่อมีผู้ดาวน์โหลดกฎหมาย
        Dim MD As New MainData
        Dim dt As DataTable = MD.GetDataTable("select top 1 qty from law_counter where law_id='" & LawID & "'")
        Dim sql As String = ""
        If dt.Rows.Count > 0 Then
            sql += "update law_counter "
            sql += " set qty = " & Convert.ToDouble(dt.Rows(0)("qty")) + 1
            sql += " where law_id='" & LawID & "'"
        Else
            'เพิ่มข้อมูลให้กรณีที่ประชาชนดาวน์โหลดกฎหมายนี้เป็นคนแรก (ถือเป็นเกียรติอย่างสูงเลยนะเนี่ยะ ^o^)
            sql += "insert into law_counter "
            sql += " values('" & LawID & "',1 )"
        End If

        Return (MD.Execute(sql) > 0)
    End Function

    Public Shared Function getPublicCounter() As Integer
        'แสดงจำนวนครั้งที่มีประชาชนเข้าใช้หน้าจอนี้
        Dim ret As Integer = 0
        Dim MD As New MainData
        Dim dt As DataTable = MD.GetDataTable("select top 1 qty from law_counter where law_id='" & PublicValue & "'")
        If dt.Rows.Count > 0 Then
            ret = Convert.ToInt64(dt.Rows(0)("qty"))
        End If

        Return ret
    End Function

    Public Shared Function getLawCounter(ByVal LawID As String) As Integer
        'แสดงจำนวนครั้งที่มีการดาวน์โหลดกฎหมายนี้
        Dim ret As Integer = 0
        Dim MD As New MainData
        Dim dt As DataTable = MD.GetDataTable("select top 1 qty from law_counter where law_id='" & LawID & "'")
        If dt.Rows.Count > 0 Then
            ret = Convert.ToInt64(dt.Rows(0)("qty"))
        End If

        Return ret
    End Function
End Class
