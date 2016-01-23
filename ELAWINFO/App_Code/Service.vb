Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.Data

Imports System.IO
Imports System.Web.UI
Imports Microsoft.VisualBasic

Imports System.Text
Imports System.Drawing.Imaging
Imports System.Drawing.Printing
Imports System.Collections.Generic

Imports System.Drawing.Image
Imports System.Security.Cryptography
Imports System.Diagnostics
<WebService(Namespace:="http://tempuri.org/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class Service
    Inherits System.Web.Services.WebService
    Private Class LocalPrint
        Implements IDisposable

        Private m_currentPageIndex As Integer
        Private m_streams As IList(Of Stream)
        Private Function CreateStream(ByVal name As String, _
        ByVal fileNameExtension As String, _
        ByVal encoding As Encoding, ByVal mimeType As String, _
        ByVal willSeek As Boolean) As Stream
            Dim stream As Stream = New MemoryStream()  'New FileStream(name + "." + fileNameExtension, FileMode.Create)
            m_streams.Add(stream) 'New MemoryStream()  '
            Return stream
        End Function
        Private Sub PrintPDF(ByVal sFileName As String)

            With New Process
                .StartInfo.Verb = "print"
                .StartInfo.CreateNoWindow = False
                .StartInfo.WindowStyle = ProcessWindowStyle.Hidden
                .StartInfo.FileName = sFileName
                .Start()
                .WaitForExit(10000)
                .CloseMainWindow()
                .Close()
            End With

        End Sub
        Private Sub PrintPage(ByVal sender As Object, ByVal ev As PrintPageEventArgs)
            'Dim pageImage As New Metafile(m_streams(m_currentPageIndex))
            Dim PageImage As System.Drawing.Image
            PageImage = System.Drawing.Image.FromStream(m_streams(m_currentPageIndex))
            ev.Graphics.DrawImage(PageImage, ev.PageBounds)
            m_currentPageIndex += 1
            ev.HasMorePages = (m_currentPageIndex < m_streams.Count)
        End Sub
        Private Sub Print()
            Dim printerName As String = System.Configuration.ConfigurationManager.AppSettings("AutoPrinterName").ToString   '"Adobe PDF"

            If m_streams Is Nothing Or m_streams.Count = 0 Then
                Return
            End If

            Dim printDoc As New PrintDocument()
            printDoc.PrinterSettings.PrinterName = printerName
            If Not printDoc.PrinterSettings.IsValid Then
                Dim msg As String = String.Format("Can't find printer ""{0}"".", printerName)
                Console.WriteLine(msg)
                Return
            End If
            AddHandler printDoc.PrintPage, AddressOf PrintPage
            printDoc.Print()
        End Sub
        Public Overloads Sub Dispose() Implements IDisposable.Dispose
            If Not (m_streams Is Nothing) Then
                Dim stream As Stream
                For Each stream In m_streams
                    stream.Close()
                Next
                m_streams = Nothing
            End If
        End Sub

    End Class
    Private strConnIMP As String = System.Configuration.ConfigurationManager.AppSettings("strConnIMP")
    Private strConnectionString As String = System.Configuration.ConfigurationManager.AppSettings("stringconn")        '// stroe connection string
    Private Const SecurityKey As String = "DLT-ELAW"
    <WebMethod()> Public Function TestConnection() As Boolean
        Return True
    End Function
    <WebMethod()> Public Function getConnectionString() As String
        Return strConnectionString
    End Function
    <WebMethod()> Public Function TestDBConnect() As Boolean
        Dim objcon As New System.Data.OleDb.OleDbConnection(strConnectionString)
        Try
            objcon.Open()
            Return True
        Catch ex As Exception
            Return False
        End Try
        objcon.Dispose()
    End Function
    <WebMethod()> _
    Public Function InitConnection() As Boolean
        Dim isConnected As Boolean = False
        Dim objCon As New OleDbConnection
        Try
            objCon = New OleDbConnection(strConnectionString)
            objCon.Open()
            isConnected = True
        Catch ex As Exception
            isConnected = False
        End Try
        objCon.Dispose()
        Return isConnected
    End Function
    <WebMethod()> _
    Public Function HelloWorld() As String
        Return "Hello World"
    End Function
    <WebMethod()> _
   Public Function getDataSetTableName(ByVal SQL As String, ByVal TableName As String) As DataSet
        Dim objCon As New OleDb.OleDbConnection
        Dim objDs As New DataSet
        Dim objDa As OleDb.OleDbDataAdapter = Nothing
        Try
            ''objCon.ConnectionString = My.Settings.ConnectionString.ToString()
            ''objCon.Open()
            objDa = New OleDb.OleDbDataAdapter(SQL, strConnectionString)
            objDa.Fill(objDs, TableName)

        Catch ex As Exception
            'MsgBox(ex.Message, MsgBoxStyle.Exclamation)
        Finally
            'objDs = Nothing
            objDa.Dispose()
            objCon.Dispose()
        End Try
        Return objDs
    End Function
    <WebMethod()> _
    Public Function getDataSet(ByVal SQL As String) As System.Data.DataSet

        Dim objCon As New OleDb.OleDbConnection
        Dim objDs As New System.Data.DataSet("Table1")
        Dim objDa As OleDb.OleDbDataAdapter = Nothing
        Try
            'objCon.ConnectionString = getConnectionString()
            'objCon.Open()
            objDa = New OleDb.OleDbDataAdapter(SQL, strConnectionString)
            objDa.Fill(objDs, "Table1")

        Catch ex As Exception
            MsgBox(ex.Message.ToString())
        Finally
            'objDs = Nothing
            objDa.Dispose()
            objCon.Dispose()
        End Try
        Return objDs

    End Function
    <WebMethod()> _
    Public Function ExecCommand(ByVal SQL As String) As Boolean

        Dim objCon As New OleDb.OleDbConnection
        Dim objCmd As New OleDb.OleDbCommand
        Try
            objCon.ConnectionString = strConnectionString
            objCon.Open()
            objCmd.CommandText = SQL
            objCmd.Connection = objCon
            objCmd.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            Return False
        Finally
            objCmd.Dispose()
            objCon.Dispose()
        End Try

    End Function
    <WebMethod()> _
    Public Function ExecCommandRowRet(ByVal SQL As String) As Integer

        Dim objCon As New OleDb.OleDbConnection
        Dim objCmd As New OleDb.OleDbCommand
        Dim nRow As Integer = 0
        Try
            objCon.ConnectionString = strConnectionString
            objCon.Open()
            objCmd.CommandText = SQL
            objCmd.Connection = objCon
            nRow = objCmd.ExecuteNonQuery()
            'Return True
        Catch ex As Exception
            'nRow = 0
        Finally
            objCmd.Dispose()
            objCon.Dispose()
        End Try

        Return nRow

    End Function
    <WebMethod()> Public Function getServerDate() As Date
        Return Date.Now
    End Function
    Public Function db2Date(ByVal sDate As Object) As Date
        Dim nDif As Integer = 0
        If Year(Now) <> Today.Year Then nDif = 543
        If IsDBNull(sDate) Then
            Return Nothing
        Else
            Return DateSerial(CInt(sDate.ToString.Substring(0, 4)) + nDif, CInt(sDate.ToString.Substring(5, 2)), CInt(sDate.ToString.Substring(8, 2)))
        End If
    End Function
    Public Function Date2db(ByVal sDate As Date) As String
        Dim sYear As Integer
        'Dim nDif As Integer = 0
        'If Year(Now) <> Today.Year Then nDif = 543
        If IsDate(sDate) = False Then
            Return ""
        Else
            sYear = sDate.Year
            If sYear > 2500 Then sYear = sYear - 543
            Return sYear & Format(sDate.Month, "-0#") & Format(sDate.Day, "-0#")
        End If
    End Function
    Private Function ConvertDataReaderToDataSet(ByVal reader As OleDbDataReader) As DataSet

        Dim dataSet As DataSet = New DataSet()

        Dim schemaTable As DataTable = reader.GetSchemaTable()

        Dim dataTable As DataTable = New DataTable()

        Dim intCounter As Integer

        For intCounter = 0 To schemaTable.Rows.Count - 1

            Dim dataRow As DataRow = schemaTable.Rows(intCounter)

            Dim columnName As String = CType(dataRow("ColumnName"), String)

            Dim column As DataColumn = New DataColumn(columnName, CType(dataRow("DataType"), Type))

            dataTable.Columns.Add(column)

        Next

        dataSet.Tables.Add(dataTable)

        While reader.Read()

            Dim dataRow As DataRow = dataTable.NewRow()

            For intCounter = 0 To reader.FieldCount - 1

                dataRow(intCounter) = reader.GetValue(intCounter)

            Next

            dataTable.Rows.Add(dataRow)

        End While

        Return dataSet

    End Function
    <WebMethod()> Public Function getVersions() As String

        Return "1.0.0"

    End Function
    <WebMethod()> Public Function SaveTextToFile(ByVal strData As String, _
     ByVal FullPath As String) As String

        'Dim Contents As String
        Dim bAns As Boolean = False
        Dim objReader As StreamWriter
        Try

            objReader = New StreamWriter(FullPath)
            objReader.Write(strData)
            objReader.Close()
            bAns = True
        Catch Ex As Exception
            'ErrInfo = Ex.Message
            Return Ex.Message.ToString
        End Try
        Return "OK"
    End Function
    <WebMethod()> Public Function GetFullName(ByVal id As String) As String

        Dim oDs As New DataSet
        Dim nResult As String = ""

        Dim SQL As String = "select name from fullname where empid='" & id & "' "

        Try
            oDs = getDataSet(SQL)

            If oDs.Tables(0).Rows.Count > 0 Then
                nResult = oDs.Tables(0).Rows(0).Item("name").ToString
            End If

        Catch ex As Exception
            nResult = ""
        End Try

        oDs.Dispose()

        Return nResult

    End Function
    <WebMethod()> Public Function GetDocType() As DataSet
        Dim strSql As String = ""
        Dim oDataset As New DataSet("Table1")
        Try

            strSql = "select * from document_type order by doc_name  "

            oDataset = getDataSet(strSql)

        Catch ex As Exception

        End Try

        Return oDataset

    End Function
    <WebMethod()> Public Function GetDocSubType(ByVal type As Integer) As DataTable
        Dim strSql As String = ""
        Dim oDs As New DataSet
        Dim TableName As String = "Table1"
        Dim DT As New DataTable(TableName)
        Try

            strSql = "select * from document_type where doc_id=" & type & "   "

            oDs = getDataSet(strSql)

            If oDs.Tables(0).Rows.Count > 0 Then
                Dim SQL As String = ""
                SQL = "" & oDs.Tables(0).Rows(0).Item("strsql").ToString & ""

                Dim DA As New OleDbDataAdapter(SQL, strConnectionString)

                Try
                    DA.Fill(DT)
                Catch x1 As Exception
                    Err.Raise(60002, , x1.Message)
                End Try

                Return DT
            End If

        Catch ex As Exception

        End Try

        Return DT

    End Function
    <WebMethod()> Public Function SearchKeyWord(ByVal txtKeyword As String, ByVal Type As String, ByVal TypeContract As String, ByVal TypeCase As String) As DataTable
        Dim strSql As String = ""

        strSql = " select d.doc_id,case when d.cancel=1 then d.doc_name+' (ยกเลิก : '+d.cancel_comment+')' else d.doc_name end doc_name,"
        strSql &= " v.name subtype_name,d.doc_type ,d.doc_subtype "
        strSql &= " from import_document d  "
        strSql &= " inner join import_keywords k"
        strSql &= " on d.doc_id=k.doc_id inner join DOCUMENT_TYPE t"
        strSql &= " on d.doc_type=t.doc_id  "
        strSql &= " inner join import_document_subtype v "
        strSql &= " on d.doc_subtype=v.id and v.tbl=t.ref_table "
        strSql &= " where d.secret = 0 And d.active = 1 "

        If Type <> "" Then
            strSql &= " and t.doc_id=1 and v.type_id  in (" & Type & ") and d.secret = 0 And d.active = 1  "
            strSql &= setKeyWord(txtKeyword)
        End If

        If TypeContract <> "" Then
            If Type <> "" Then
                strSql &= " or "
            Else
                strSql &= " and "
            End If
            strSql &= " t.doc_id=3 and v.type_id  in (" & TypeContract & ") and d.secret = 0 And d.active = 1 "
            strSql &= setKeyWord(txtKeyword)
        End If

        If TypeCase <> "" Then
            If Type <> "" Then
                strSql &= " or "
            Else
                If TypeContract <> "" Then
                    strSql &= " or "
                Else
                    strSql &= " and "
                End If
            End If
            strSql &= "t.doc_id=2 and v.type_id  in (" & TypeCase & ") and d.secret = 0 And d.active = 1 "
            strSql &= setKeyWord(txtKeyword)
        End If

        strSql &= setKeyWord(txtKeyword)

        strSql &= " group by d.doc_id,case when d.cancel=1 then d.doc_name+' (ยกเลิก : '+d.cancel_comment+')' else d.doc_name end,v.name,d.doc_type ,d.doc_subtype    "
        strSql &= " order by d.doc_type ,d.doc_subtype "



        Dim TableName As String = "Table1"

        Dim DA As New OleDbDataAdapter(strSql, strConnectionString)
        Dim DT As New DataTable(TableName)

        Try
            DA.Fill(DT)
        Catch x1 As Exception
            Err.Raise(60002, , x1.Message)
        End Try

        Return DT

    End Function
    <WebMethod()> Public Function setKeyWord(ByVal txtKeyword As String) As String
        Dim ret As String = ""
        Dim sql As String = "select keyword from keyword_data order by keyword"
        'Dim dt As DataTable = MD.GetDataTable(sql)

        Dim TableName As String = "Table1"

        Dim da As New OleDbDataAdapter(sql, strConnectionString)
        Dim dt As New DataTable(TableName)
        da.Fill(dt)
        Dim txtKey() As String = Split(txtKeyword, " ")
        For i As Integer = 0 To txtKey.Length() - 1
            For Each dr As DataRow In dt.Rows
                Dim nWord As Integer = InStr(dr("keyword").ToString(), txtKey(i), CompareMethod.Text)
                If nWord > 0 Then
                    Dim whText As String = " k.keyword like '%" & txtKey(i) & "%' "
                    If ret = "" Then
                        ret = whText
                    Else
                        ret += " or " & whText
                    End If
                End If
            Next
        Next
        If ret <> "" Then
            ret = " and (" & ret & ")"
        End If

        Return ret
    End Function
    <WebMethod()> Public Function SaveKeyword(ByVal txtWord As String) As Boolean
        Dim objCon As New OleDb.OleDbConnection
        Dim objCmd As New OleDb.OleDbCommand

        Dim txtKey() As String = Split(txtWord, " ")

        Try

            For Each txt As String In txtKey
                Dim sql As String = ""
                sql += " select keyword "
                sql += " from keyword_data "
                sql += " where keyword = '" & txt & "'"


                Dim TableName As String = "Table1"

                Dim da As New OleDbDataAdapter(sql, strConnectionString)
                Dim dt As New DataTable(TableName)
                da.Fill(dt)
                If dt.Rows.Count = 0 Then
                    Dim strsql As String
                    strsql = "insert into keyword_data (keyword) values ('" & txt & "')"

                    objCon.ConnectionString = strConnectionString
                    objCon.Open()

                    objCmd = New OleDb.OleDbCommand(strsql, objCon)
                    objCmd.ExecuteNonQuery()

                End If
            Next

            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function
    <WebMethod()> Public Function DataLaw() As DataTable
        Dim strSql As String = ""

        strSql = " select type_id,type_name  "
        strSql &= "from law_type  "


        Dim TableName As String = "Table1"

        Dim DA As New OleDbDataAdapter(strSql, strConnectionString)
        Dim DT As New DataTable(TableName)

        Try
            DA.Fill(DT)
        Catch x1 As Exception
            Err.Raise(60002, , x1.Message)
        End Try

        Return DT

    End Function
    <WebMethod()> Public Function DataContract() As DataTable
        Dim strSql As String = ""

        strSql = " select type_id,type_name  "
        strSql &= "from contract_type  "

        Dim TableName As String = "Table1"

        Dim DA As New OleDbDataAdapter(strSql, strConnectionString)
        Dim DT As New DataTable(TableName)

        Try
            DA.Fill(DT)
        Catch x1 As Exception
            Err.Raise(60002, , x1.Message)
        End Try

        Return DT

    End Function
    <WebMethod()> Public Function DataCase() As DataTable
        Dim strSql As String = ""

        strSql = " select type_id,type_name  "
        strSql &= "from case_type  "

        Dim TableName As String = "Table1"

        Dim DA As New OleDbDataAdapter(strSql, strConnectionString)
        Dim DT As New DataTable(TableName)

        Try
            DA.Fill(DT)
        Catch x1 As Exception
            Err.Raise(60002, , x1.Message)
        End Try

        Return DT

    End Function
    <WebMethod()> Public Function SearchDetail(ByVal id As String) As DataTable
        Dim strSql As String = ""

        'strSql = " select d.doc_id,case when d.cancel=1 then d.doc_name+' (ยกเลิก : '+d.cancel_comment+')' else d.doc_name end doc_name,"
        'strSql &= " v.name subtype_name,d.doc_type ,d.doc_subtype "
        'strSql &= " from import_document d  "
        'strSql &= " inner join import_keywords k"
        'strSql &= " on d.doc_id=k.doc_id inner join DOCUMENT_TYPE t"
        'strSql &= " on d.doc_type=t.doc_id  "
        'strSql &= " inner join import_document_subtype v "
        'strSql &= " on d.doc_subtype=v.id and v.tbl=t.ref_table "
        'strSql &= " where d.secret = 0 And d.active = 1 "

        'strSql = " select im.doc_id,v.name subtype_name,"
        'strSql &= " case when im.cancel=1 then im.doc_name+' (ยกเลิก : '+im.cancel_comment+')' else im.doc_name end doc_name"
        'strSql &= " from import_document im"
        'strSql &= " inner join DOCUMENT_TYPE t"
        'strSql &= " on im.doc_type=t.doc_id "
        'strSql &= " inner join import_document_subtype v"
        'strSql &= " on im.doc_subtype=v.id and v.tbl=t.ref_table"
        'strSql &= " inner join LINKLAW_DATA d"
        'strSql &= " on im.doc_id=d.title "
        'strSql &= " where im.doc_id in"
        'strSql &= " (select title"
        'strSql &= " from LINKLAW_DATA "
        'strSql &= " where link_id in"
        'strSql &= " (select link_id from LINK_DETAIL where doc_id ='" & id & "') union select '" & id & "')"
        'strSql &= " and im.secret = 0 And im.active = 1"

        strSql = " select im.doc_id,v.name subtype_name,"
        strSql &= " case when im.cancel=1 then im.doc_name+' (ยกเลิก : '+im.cancel_comment+')' else im.doc_name end doc_name"
        strSql &= " from import_document im"
        strSql &= " inner join DOCUMENT_TYPE t"
        strSql &= " on im.doc_type=t.doc_id "
        strSql &= " inner join import_document_subtype v"
        strSql &= " on im.doc_subtype=v.id and v.tbl=t.ref_table"
        strSql &= " inner join LINKLAW_DATA d"
        strSql &= " on im.doc_id=d.title "
        strSql &= " where im.doc_id in"
        strSql &= " (select title"
        strSql &= " from LINKLAW_DATA "
        strSql &= " where link_id in"
        strSql &= " (select link_id from LINK_DETAIL where doc_id ='" & id & "') union select '" & id & "')"
        strSql &= " And im.active = 1"


        Dim TableName As String = "Table1"

        Dim DA As New OleDbDataAdapter(strSql, strConnectionString)
        Dim DT As New DataTable(TableName)

        Try
            DA.Fill(DT)
        Catch x1 As Exception
            Err.Raise(60002, , x1.Message)
        End Try

        Return DT

    End Function
    <WebMethod()> Public Function GetDocIdDownload(ByVal id As String) As DataTable
        Dim strSql As String = ""

        strSql = "select d.doc_id, "
        strSql &= "case when d.cancel=1 then d.doc_name+' (ยกเลิก : '+d.cancel_comment+')' else d.doc_name end doc_name,"
        strSql &= "d.file_path,d.mime_type "
        strSql &= "from import_document d "
        strSql &= "where d.doc_id='" & id & "' and d.active=1 "


        Dim TableName As String = "Table1"

        Dim DA As New OleDbDataAdapter(strSql, strConnectionString)
        Dim DT As New DataTable(TableName)

        Try
            DA.Fill(DT)
        Catch x1 As Exception
            Err.Raise(60002, , x1.Message)
        End Try

        Return DT

    End Function
    <WebMethod()> Public Function GetDocIdDownload2(ByVal id As String) As DataTable
        Dim strSql As String = ""

        strSql = "select d.doc_id, "
        strSql &= "case when d.cancel=1 then d.doc_name+' (ยกเลิก : '+d.cancel_comment+')' else d.doc_name end doc_name,"
        strSql &= "d.file_path,d.mime_type "
        strSql &= "from import_document d "
        strSql &= "where d.doc_id='" & id & "' and d.active=1 "


        Dim TableName As String = "Table1"

        Dim DA As New OleDbDataAdapter(strSql, strConnectionString)
        Dim DT As New DataTable(TableName)

        Try
            DA.Fill(DT)
        Catch x1 As Exception
            Err.Raise(60002, , x1.Message)
        End Try

        Return DT

    End Function
    <WebMethod()> Public Function GetDocIdDownloadSelect(ByVal id As String) As DataTable
        Dim strSql As String = ""

        strSql = " select im.doc_id,v.name subtype_name,"
        strSql &= " case when im.cancel=1 then im.doc_name+' (ยกเลิก : '+im.cancel_comment+')' else im.doc_name end doc_name"
        strSql &= " from import_document im"
        strSql &= " inner join DOCUMENT_TYPE t"
        strSql &= " on im.doc_type=t.doc_id "
        strSql &= " inner join import_document_subtype v"
        strSql &= " on im.doc_subtype=v.id and v.tbl=t.ref_table"

        strSql &= " where im.doc_id in"
        strSql &= " (select doc_id"
        strSql &= " from link_detail "
        strSql &= " where link_id in"
        strSql &= " (select link_id from LINKLAW_DATA where title  ='" & id & "') union select '" & id & "' )"

        strSql &= " and im.secret = 0 And im.active = 1"



        Dim TableName As String = "Table1"

        Dim DA As New OleDbDataAdapter(strSql, strConnectionString)
        Dim DT As New DataTable(TableName)

        Try
            DA.Fill(DT)
        Catch x1 As Exception
            Err.Raise(60002, , x1.Message)
        End Try

        Return DT

    End Function
    <WebMethod()> Public Function setPublicCounter() As Boolean
        'เริ่มนับเมื่อมีประชาชนเข้าใช้หน้าจอ
        Dim objCon As New OleDb.OleDbConnection
        Dim objCmd As New OleDb.OleDbCommand
        Const PublicValue As String = "PUBLIC"
        Dim str As String = "select top 1 qty from law_counter where law_id='" & PublicValue & "'"
        Dim TableName As String = "Table1"

        Dim DA As New OleDbDataAdapter(str, strConnectionString)
        Dim DT As New DataTable(TableName)
        DA.Fill(DT)

        Dim sql As String = ""
        If DT.Rows.Count > 0 Then
            sql += "update law_counter "
            sql += " set qty = " & Convert.ToDouble(DT.Rows(0)("qty")) + 1
            sql += " where law_id='" & PublicValue & "'"
        Else
            'เพิ่มข้อมูลให้กรณีที่ประชาชนเข้าเป็นคนแรก (ถือเป็นเกียรติอย่างสูงเลยนะเนี่ยะ ^o^)
            sql += "insert into law_counter "
            sql += " values('" & PublicValue & "',1 )"
        End If

        Try

            objCon.ConnectionString = strConnectionString
            objCon.Open()

            objCmd = New OleDb.OleDbCommand(sql, objCon)
            objCmd.ExecuteNonQuery()

            Return True

        Catch ex As Exception
            Err.Raise(60002, , ex.Message)
            Return False
        End Try

    End Function
    <WebMethod()> Public Function getPublicCounter() As Integer
        'แสดงจำนวนครั้งที่มีประชาชนเข้าใช้หน้าจอนี้
        Const PublicValue As String = "PUBLIC"
        Dim ret As Integer = 0

        Dim str As String = "select top 1 qty from law_counter where law_id='" & PublicValue & "'"

        Dim TableName As String = "Table1"

        Dim DA As New OleDbDataAdapter(str, strConnectionString)
        Dim DT As New DataTable(TableName)
        DA.Fill(DT)
        If DT.Rows.Count > 0 Then
            ret = Convert.ToInt64(DT.Rows(0)("qty"))
        End If

        Return ret
    End Function
    <WebMethod()> Public Function setLawCounter(ByVal LawID As String) As Boolean
        'เริ่มนับเมื่อมีประชาชนเข้าใช้หน้าจอ
        Dim objCon As New OleDb.OleDbConnection
        Dim objCmd As New OleDb.OleDbCommand
        Dim str As String = "select top 1 qty from law_counter where law_id='" & LawID & "'"
        Dim TableName As String = "Table1"

        Dim DA As New OleDbDataAdapter(str, strConnectionString)
        Dim DT As New DataTable(TableName)
        DA.Fill(DT)

        Dim sql As String = ""
        If DT.Rows.Count > 0 Then
            sql += "update law_counter "
            sql += " set qty = " & Convert.ToDouble(DT.Rows(0)("qty")) + 1
            sql += " where law_id='" & LawID & "'"
        Else
            'เพิ่มข้อมูลให้กรณีที่ประชาชนดาวน์โหลดกฎหมายนี้เป็นคนแรก (ถือเป็นเกียรติอย่างสูงเลยนะเนี่ยะ ^o^)
            sql += "insert into law_counter "
            sql += " values('" & LawID & "',1 )"
        End If

        Try

            objCon.ConnectionString = strConnectionString
            objCon.Open()

            objCmd = New OleDb.OleDbCommand(sql, objCon)
            objCmd.ExecuteNonQuery()

            Return True

        Catch ex As Exception
            Err.Raise(60002, , ex.Message)
            Return False
        End Try

    End Function
    <WebMethod()> Public Function getLawCounter(ByVal LawID As String) As Integer
        'แสดงจำนวนครั้งที่มีประชาชนเข้าใช้หน้าจอนี้
        Dim ret As Integer = 0

        Dim str As String = "select top 1 qty from law_counter where law_id='" & LawID & "'"

        Dim TableName As String = "Table1"

        Dim DA As New OleDbDataAdapter(str, strConnectionString)
        Dim DT As New DataTable(TableName)
        DA.Fill(DT)
        If DT.Rows.Count > 0 Then
            ret = Convert.ToInt64(DT.Rows(0)("qty"))
        End If

        Return ret
    End Function
End Class
