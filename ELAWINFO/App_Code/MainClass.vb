Imports Microsoft.VisualBasic
Imports System.Data
Imports System.DBNull
Imports System.Security.Cryptography
Imports System.Data.SqlClient
Imports System.Data.OleDb

Public Class MainClass
    Dim MD As New MainData
    Public Sub MessageBox(ByVal oParent As Object, ByVal sMsg As String)

        Static Dim handlerPages As New Hashtable

        Dim sb As New StringBuilder()
        Dim oFormObject As System.Web.UI.Control

        sMsg = sMsg.Replace("'", "\'")
        sMsg = sMsg.Replace(Chr(34), "\" & Chr(34))
        sMsg = sMsg.Replace(vbCrLf, "\n")
        'sMsg = "<script language=javascript>alert(""" & sMsg & """);</script>"
        sMsg = "alert('" & sMsg & "');"
        sb = New StringBuilder()
        sb.Append(sMsg)

        For Each oFormObject In oParent.Controls
            If TypeOf oFormObject Is HtmlForm Then
                Exit For
            End If
        Next

        ' Add the javascript after the form object so that the 
        ' message doesn't appear on a blank screen.
        'oFormObject.Controls.AddAt(oFormObject.Controls.Count, New LiteralControl(sb.ToString()))
        'oParent.ClientScript.RegisterClientScriptBlock(oParent.GetType(), Guid.NewGuid().ToString(), sb.ToString)

        ScriptManager.RegisterStartupScript(oParent, oParent.GetType(), Guid.NewGuid().ToString(), sb.ToString, True) '  "alert('xxxx');", True)

    End Sub

    Public Sub OpenWindow(ByVal oParent As Object, ByVal sURL As String)

        Static Dim handlerPages As New Hashtable
        Dim sMsg As String = ""
        Dim sb As New StringBuilder()
        Dim oFormObject As System.Web.UI.Control

        sURL = sURL.Replace("'", "\'")
        sURL = sURL.Replace(Chr(34), "\" & Chr(34))
        sURL = sURL.Replace("\", "\\")
        sURL = sURL.Replace(vbCrLf, "\n")
        'sMsg = "<script language=javascript>alert(""" & sMsg & """);</script>"
        sMsg = "window.open('" & sURL & "','_blank');"
        sb = New StringBuilder()
        sb.Append(sMsg)

        For Each oFormObject In oParent.Controls
            If TypeOf oFormObject Is HtmlForm Then
                Exit For
            End If
        Next

        ' Add the javascript after the form object so that the 
        ' message doesn't appear on a blank screen.
        'oFormObject.Controls.AddAt(oFormObject.Controls.Count, New LiteralControl(sb.ToString()))
        'oParent.ClientScript.RegisterClientScriptBlock(oParent.GetType(), Guid.NewGuid().ToString(), sb.ToString)

        ScriptManager.RegisterStartupScript(oParent, oParent.GetType(), Guid.NewGuid().ToString(), sb.ToString, True) '  "alert('xxxx');", True)

    End Sub

    Public Sub CloseWindow(ByVal oParent As Object)
        Static Dim handlerPages As New Hashtable
        Dim sMsg As String = ""
        Dim sb As New StringBuilder()
        Dim oFormObject As System.Web.UI.Control

        sMsg = "window.close();"
        sb = New StringBuilder()
        sb.Append(sMsg)

        For Each oFormObject In oParent.Controls
            If TypeOf oFormObject Is HtmlForm Then
                Exit For
            End If
        Next

        ' Add the javascript after the form object so that the 
        ' message doesn't appear on a blank screen.
        'oFormObject.Controls.AddAt(oFormObject.Controls.Count, New LiteralControl(sb.ToString()))
        'oParent.ClientScript.RegisterClientScriptBlock(oParent.GetType(), Guid.NewGuid().ToString(), sb.ToString)

        ScriptManager.RegisterClientScriptBlock(oParent, oParent.GetType(), Guid.NewGuid().ToString(), sb.ToString, True) '  "alert('xxxx');", True)

    End Sub

    Public Sub showModalDialog(ByVal oParent As Object, ByVal sURL As String, ByVal sAguments As String)
        'vReturnValue = window.showModalDialog(sURL [, vFreeArgument] [, sOrnaments]);
        'dialogHeight: sHeight()
        'dialogLeft: sXpos()
        'dialogTop: sYpos()
        'dialogWidth: sWidth()
        'center: ( yes | no | 1 | 0 | on | off ) 
        'dialogHide: ( yes | no | 1 | 0 | on | off ) 
        'edge: ( sunken | raised ) 
        'help: ( yes | no | 1 | 0 | on | off ) 
        'resizable: ( yes | no | 1 | 0 | on | off ) 
        'scroll: ( yes | no | 1 | 0 | on | off ) 
        'status: ( yes | no | 1 | 0 | on | off ) 
        'unadorned: ( yes | no | 1 | 0 | on | off ) 
        Static Dim handlerPages As New Hashtable
        Dim sMsg As String = ""
        Dim sb As New StringBuilder()
        Dim oFormObject As System.Web.UI.Control

        sURL = sURL.Replace("'", "\'")
        sURL = sURL.Replace(Chr(34), "\" & Chr(34))
        sURL = sURL.Replace("\", "\\")
        sURL = sURL.Replace(vbCrLf, "\n")
        'sMsg = "<script language=javascript>alert(""" & sMsg & """);</script>"
        If sAguments = "" Then
            sMsg = "window.showModalDialog('" & sURL & "');"
        Else
            sMsg = "window.showModalDialog('" & sURL & "','" & sAguments & "');"
        End If
        sb = New StringBuilder()
        sb.Append(sMsg)

        For Each oFormObject In oParent.Controls
            If TypeOf oFormObject Is HtmlForm Then
                Exit For
            End If
        Next

        ' Add the javascript after the form object so that the 
        ' message doesn't appear on a blank screen.
        'oFormObject.Controls.AddAt(oFormObject.Controls.Count, New LiteralControl(sb.ToString()))
        'oParent.ClientScript.RegisterClientScriptBlock(oParent.GetType(), Guid.NewGuid().ToString(), sb.ToString)

        ScriptManager.RegisterStartupScript(oParent, oParent.GetType(), Guid.NewGuid().ToString(), sb.ToString, True) '  "alert('xxxx');", True)

    End Sub

    Public Sub Test(ByVal oParent As Object)
        ScriptManager.RegisterClientScriptBlock(oParent, oParent.GetType(), Guid.NewGuid().ToString(), "<script language=vbscript> msgbox(" & Chr(34) & "xx" & Chr(34) & ") </script>", False)
    End Sub

    Public Function Date2DB(ByVal sDate As String) As String '// Parameter format is dd/MM/yyyy
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
    Public Function ConDate(ByVal sDate As String) As Date
        Dim T1 As Date
        Dim sReturn As Date
        Dim Main As New MainClass
        T1 = Main.Date2DB(sDate)

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
    Public Function ChkPassword(ByVal sUsername As String, ByVal sPassword As String) As String

        Dim X As String = ""
        '1. Create a connection
        'Const strConnString As String = "connection string"
        Dim objConn As New SqlConnection(MD.strConnIMP)

        '2. Create a command object for the query
        Dim strSQL As String = "SELECT COUNT(*) FROM DLT.dbo.Users u inner join DLT.dbo.Employee e " & _
        "on u.empid=e.empid " & _
        "WHERE u.loginname=@Username AND u.password=@Password and e.status=1 "
        Dim objCmd As New SqlCommand(strSQL, objConn)

        '3. Create parameters
        Dim paramUsername As SqlParameter
        paramUsername = New SqlParameter("@Username", SqlDbType.VarChar, 25)
        paramUsername.Value = sUsername
        objCmd.Parameters.Add(paramUsername)


        'Encrypt the password
        Dim md5Hasher As New MD5CryptoServiceProvider()

        Dim hashedDataBytes As Byte()
        Dim encoder As New UTF8Encoding()

        hashedDataBytes = md5Hasher.ComputeHash(encoder.GetBytes(sPassword))

        Dim paramPwd As SqlParameter
        paramPwd = New SqlParameter("@Password", SqlDbType.Binary, 16)
        paramPwd.Value = hashedDataBytes
        objCmd.Parameters.Add(paramPwd)


        'Insert the records into the database
        objConn.Open()
        Dim iResults As Integer = objCmd.ExecuteScalar()

        If iResults = 1 Then
            ''The user was found in the DB
           
            ' Update Last Logon
            Dim objTrans As SqlTransaction
            objTrans = objConn.BeginTransaction(IsolationLevel.ReadCommitted)
            Dim sql As String = ""
            sql += "update users"
            sql += " set last_logon = getdate()"
            sql += " where loginname = '" & sUsername & "'"
            Dim cmd As New SqlCommand(sql, objConn, objTrans)
            If cmd.ExecuteNonQuery > 0 Then
                objTrans.Commit()
            Else
                objTrans.Rollback()
            End If
            'sReturn = True
            'Return sReturn
            X = sUsername
        End If
        objConn.Close()
        Return X

    End Function

    Public Function ChkPermission(ByVal sUsername As String, ByVal sUrl As String) As Boolean
        Dim X As Boolean = False
        Dim oDs As New DataSet
        Dim strsql As String

        strsql = "select g.empid from usergroup g inner join users u   "
        strsql &= "on g.empid=u.empid "
        strsql &= "where upper(u.loginname)='" & sUsername.ToUpper & "' and g.group_id in"
        strsql &= "(select p.group_id from permission p inner join menu m "
        strsql &= "on p.menu_id=m.menu_id where m.menu_url like '%" & sUrl & "%' and p.isupdate = 'T') "

        oDs = MD.GetDataset(strsql)

        If oDs.Tables(0).Rows.Count > 0 Then

            X = True
            Return X
        Else
            X = False
            Return X
        End If

    End Function
    Public Function ChkLogin(ByVal sUsername As String) As Boolean
        Dim X As Boolean = False
        If sUsername <> "" Then
            X = True
            Return X
        Else
            X = False
            Return X
        End If
    End Function
    Public Function ErrLog(ByVal url As String, ByVal sess As String, ByVal msg As String, ByVal emp As String, ByVal browser As String) As Boolean
        Dim strsql As String

        strsql = "insert into error_log   "
        strsql &= "(url,session_id,message,empid,dates,browser) values "
        strsql &= "('" & url & "','" & sess & "','" & Replace(msg, "'", "''") & "','" & emp & "',getdate(),'" & browser & "' )  "

        If MD.Execute(strsql) > 0 Then
            Return True
        Else
            Return False
        End If

    End Function

End Class
