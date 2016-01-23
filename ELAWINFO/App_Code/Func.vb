Imports Microsoft.VisualBasic
Imports System.IO
Imports System.Web.Configuration
Imports System.Net

Public Class Func
    Public Shared Sub DeleteFile(ByVal filePath As String)
        If File.Exists(filePath) = True Then
            File.Delete(filePath)
        End If
    End Sub
    Public Shared Function getServerPath() As String

        Return ConfigurationManager.AppSettings("UploadPath").ToString
        'Return "\\172.20.254.73\ElawUpload\"

    End Function

    Public Shared Function UploadFile(ByVal sEmpNo As String, ByVal FileUpload1 As FileUpload, ByVal fileName As String, ByVal fldName As String) As Boolean
        'Upload and save file at directory
        Dim ret As Boolean = True
        If FileUpload1.HasFile = True Then
            Dim extension As String = System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName).ToLower()
            'Ex. FileUpload1.PostedFile.FileName = D:\My Documents\XCS\SystemDoc\ReportName.docx
            Dim MIMEType As String = ""
            'Ex FilePath = "..\Document\Contract\"

            Try
                MIMEType = getMIMEType(FileUpload1.PostedFile.FileName)
                If MIMEType = "" Then
                    Return False
                    Exit Function
                End If

                Dim X As String = Path.GetFileName(fileName)
                X = Func.getServerPath() & fldName & X
                'Ex.  D:\ElawUpload\   Document\Contract\   fileName.ext

                If Directory.Exists(getServerPath() & fldName) = False Then
                    Directory.CreateDirectory(getServerPath() & fldName)
                    'Directory.CreateDirectory(getServerPath() & "\" & fldName)
                End If
                FileUpload1.PostedFile.SaveAs(X)
                ret = True
            Catch ex As Exception
                ret = False
                Dim ses As String = System.Web.HttpContext.Current.Session.SessionID
                Dim url As String = HttpContext.Current.Request.FilePath
                Dim brow As HttpBrowserCapabilities

                Dim browser As String = " Type:" + brow.Type + " Version:" + brow.Version + " Browser:" + brow.Browser
                Dim MC As New MainClass
                MC.ErrLog(url, ses, ex.ToString, sEmpNo, browser)
            End Try
        End If

        Return ret
    End Function

    Public Shared Function getMIMEType(ByVal vFileName As String) As String
        Dim extension As String = System.IO.Path.GetExtension(vFileName).ToLower()
        Dim MIMEType As String = ""

        Select Case extension
            Case ".jpg", ".jpeg", ".jpe"
                MIMEType = ".jpg"
            Case ".csv", ".xls", ".xlsx", ".pdf", ".doc", ".docx", ".txt"
                MIMEType = extension
            Case ".htm", ".html"
                MIMEType = ".html"
            Case Else
                MIMEType = ""
        End Select

        Return MIMEType
    End Function

    Public Shared Sub SaveErrLog(ByVal msg As String, ByVal sEmpNo As String)
        Dim ses As String = System.Web.HttpContext.Current.Session.SessionID
        Dim url As String = HttpContext.Current.Request.FilePath
        Dim req As HttpRequest
        Dim browser As String = " Type:" + req.Browser.Type + " Version:" + req.Browser.Version + " Browser:" + req.Browser.Browser
        Dim MC As New MainClass

        MC.ErrLog(url, ses, msg, sEmpNo, browser)
    End Sub
End Class
