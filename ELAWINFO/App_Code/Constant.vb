Imports Microsoft.VisualBasic
Imports System.Web.Configuration

Public Class Constant
    Public Const pwdExpDay As Int16 = 90        'จำนวนวันที่จะให้คำนวณวันหมดอายุของรหัสผ่านผู้ใช้งาน
    Public Const DirectorPosID As Integer = 4   'ตำแหน่งของผู้อำนวยการ
    Public Const searchSession As String = "SEARCH_SESSION"    'ชื่อของ Session กรณีกดปุ่มค้นหา ไม่ว่าหน้าไหนๆ

    Public Shared Function GetFullDate() As String
        Dim month As String = ""
        Select Case DateTime.Now.Month
            Case 1
                month = "January"
            Case 2
                month = "Febuary"
            Case 3
                month = "March"
            Case 4
                month = "April"
            Case 5
                month = "May"
            Case 6
                month = "June"
            Case 7
                month = "July"
            Case 8
                month = "August"
            Case 9
                month = "September"
            Case 10
                month = "October"
            Case 11
                month = "November"
            Case 12
                month = "December"
        End Select
        Return month & ", " & DateTime.Now.Day.ToString() & " " & DateTime.Now.Year.ToString()
    End Function

    Public Shared Function Date2DB(ByVal sDate As String) As String '// Parameter format is dd/MM/yyyy
        Dim sReturn As String = ""
        Dim sTemp() As String
        Dim sBuffer As String = ""
        'Dim dTmp As Date
        Try
            sTemp = sDate.Split("/")
            If sTemp.Length <> 3 Then
                sReturn = "" '// Invalid date format
            ElseIf Val(sTemp(2)) < 1900 Then        '// Minimum date
                sReturn = ""
            ElseIf Val(sTemp(0)) < 1 Or Val(sTemp(0)) > 31 Then   '// day
                sReturn = ""
            ElseIf Val(sTemp(1)) < 1 Or Val(sTemp(1)) > 12 Then  '// Month
                sReturn = ""
            Else
                'dTmp = CDate(sDate)
                If IsDate(sTemp(1) & "/" & sTemp(0) & "/" & sTemp(2)) Or IsDate(sTemp(0) & "/" & sTemp(1) & "/" & sTemp(2)) Then
                    If Val(sTemp(1)) > 12 Then  '// Swap
                        sBuffer = sTemp(1)
                        sTemp(1) = sTemp(0)
                        sTemp(0) = sBuffer
                    End If
                    '// วันที่
                    If Val(sTemp(2)) > 2500 Then sTemp(2) = (Val(sTemp(2)) - 543).ToString
                    sReturn = sTemp(2) & "-" & Format(Val(sTemp(1)), "0#") & "-" & Format(Val(sTemp(0)), "0#")

                End If


            End If
        Catch ex As Exception
            sReturn = ""
        End Try
        Return sReturn
    End Function
    Public Shared Function ConDate(ByVal sDate As String) As Date
        Dim T1 As Date
        Dim sReturn As Date
        T1 = Date2DB(sDate)

        If T1.Year > 2500 Then
            sReturn = DateAdd(DateInterval.Year, -543, T1)
        Else
            sReturn = DateAdd(DateInterval.Year, 543, T1)
            'sReturn = T1
        End If

        Return sReturn

    End Function
    Public Function DB2Date(ByVal sDate As String) As Date '// Parameter format is yyyy-MM-dd
        Dim sReturn As Date = "1/1/1900"
        Dim sTemp(2) As String
        Dim sBuffer As String = ""

        Try
            'sTemp = sDate.Split("/")
            sTemp(2) = sDate.Substring(0, 4)
            sTemp(1) = sDate.Substring(5, 2)
            sTemp(0) = sDate.Substring(8, 2)
            If sTemp.Length <> 3 Then
                'sReturn = "" '// Invalid date format
            ElseIf Val(sTemp(2)) < 1900 Then        '// Minimum date
                'sReturn = ""
            ElseIf Val(sTemp(0)) < 1 Or Val(sTemp(0)) > 31 Then   '// day
                'sReturn = ""
            ElseIf Val(sTemp(1)) < 1 Or Val(sTemp(1)) > 12 Then  '// Month
                'sReturn = ""
            Else
                'dTmp = CDate(sDate)
                If IsDate(sTemp(1) & "/" & sTemp(0) & "/" & sTemp(2)) Or IsDate(sTemp(0) & "/" & sTemp(1) & "/" & sTemp(2)) Then
                    If Val(sTemp(1)) > 12 Then  '// Change month to date
                        sBuffer = sTemp(1)
                        sTemp(1) = sTemp(0)
                        sTemp(0) = sBuffer
                    End If
                    '// วันที่
                    If Year(Today) > 2500 Then sTemp(2) = (Val(sTemp(2)) + 543).ToString
                    sReturn = DateSerial(sTemp(2), sTemp(1), sTemp(0)) 'sTemp(2) & "-" & Format(Val(sTemp(1)), "0#") & "-" & Format(Val(sTemp(0)), "0#")
                End If
            End If
        Catch ex As Exception
            'sReturn = ""
        End Try
        Return sReturn
    End Function

    Public Shared Function BaseURL(ByVal req As HttpRequest) As String
        Return req.Url.Host & ConfigurationManager.AppSettings("UploadURL").ToString()
        'Return "172.20.254.73/ElawUpload/"

    End Function

    Partial Public Class MenuID

        Public Const DrafLawList As Integer = 79
        Public Const DrafLawEditList As Integer = 83
        Public Const DrafLawApproveList As Integer = 80
        Public Const DrafLawStatusList As Integer = 84
        Public Const ImportList As Integer = 40
        Public Const ImportSearch As Integer = 41
        Public Const CaseDataList As Integer = 68
        Public Const CaseApproveList As Integer = 30
        Public Const CaseEditList As Integer = 48
        Public Const CaseStatusList As Integer = 52
        Public Const CaseDicisionList As Integer = 66
        Public Const ShowDicision As Integer = 72
        Public Const ContractPreviewList As Integer = 36
        Public Const ContractApproveList As Integer = 31
        Public Const ContractEditList As Integer = 33
        Public Const ContractWarningList As Integer = 37
        Public Const ContractCancelList As Integer = 32
        Public Const BookInDataList As Integer = 54
        Public Const BookInWaitList As Integer = 64
        Public Const BookInCloseJob As Integer = 70
        Public Const BookOutDataList As Integer = 71
        Public Const BookOutWait As Integer = 58
        Public Const BookOutEditList As Integer = 60
    End Class
End Class
