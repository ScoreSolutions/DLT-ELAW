Imports System.Data
Imports System.Data.OleDb
Imports System.Configuration

Public Class MainData


    Public strConnIMP As String = ConfigurationManager.AppSettings("strConnIMP").ToString()
    Dim PV As String = ConfigurationManager.ConnectionStrings("SqlConnectionString").ConnectionString
    Public RPas As String = ConfigurationManager.AppSettings("RPas").ToString()
    Public RServer As String = ConfigurationManager.AppSettings("RServer").ToString()

    Dim m_Database As String = ConfigurationManager.AppSettings("m_Database").ToString()
    Public Strcon As String
    Dim m_Identity As Integer
    Public RId As String = ConfigurationManager.AppSettings("RId").ToString()

    Public RDb As String = ConfigurationManager.AppSettings("RDb").ToString()
    Public EMPID As String
    Public FullName As String
    Public sFinishContract As String = "9"
    Public pLogin As String = "../Src/Login.aspx"
    Public pNoAut As String = "../Src/NoAuth.aspx"
    Public pHome As String = "../Src/Portal.aspx"

    Dim _errMsg As String = ""
    Public Sub New()
        Strcon = PV & "database=" & m_Database
    End Sub
    Public Sub New(ByVal DBName As String)
        m_Database = DBName
        Strcon = PV & "database=" & m_Database
    End Sub
    Public Property Database() As String
        Get
            Return m_Database
        End Get
        Set(ByVal Value As String)
            m_Database = Value
            Strcon = PV & "database=" & m_Database
        End Set
    End Property
    Public ReadOnly Property Identity() As Integer
        Get
            Return m_Identity
        End Get
    End Property
    Public ReadOnly Property ErrMsg() As String
        Get
            Return _errMsg
        End Get
    End Property

    Public Function GetDataset(ByVal Strsql As String, _
        Optional ByVal DatasetName As String = "Dataset1", _
        Optional ByVal TableName As String = "Table") As DataSet
        Dim DA As New OleDbDataAdapter(Strsql, Strcon)
        Dim DS As New DataSet(DatasetName)
        Try
            DA.Fill(DS, TableName)
        Catch x1 As Exception
            Err.Raise(60002, , x1.Message)
        End Try
        Return DS
    End Function
    Public Function GetDataTable(ByVal Strsql As String, _
         Optional ByVal TableName As String = "Table") As DataTable
        Dim DA As New OleDbDataAdapter(Strsql, Strcon)
        Dim DT As New DataTable(TableName)

        Try
            DA.Fill(DT)
        Catch x1 As Exception
            Err.Raise(60002, , x1.Message)
        End Try
        Return DT
    End Function
    Public Function CreateCommand(ByVal Strsql As String) As OleDbCommand
        Dim cmd As New OleDbCommand(Strsql)
        Return cmd
    End Function
    Public Function Execute(ByVal Strsql As String) As Integer
        Dim cmd As New OleDbCommand(Strsql)
        Dim X As Integer = Me.Execute(cmd)
        Return X
    End Function
    Public Function Execute(ByRef Cmd As OleDbCommand) As Integer
        Dim Cn As New OleDbConnection(Strcon)
        Cn.Open()
        Dim trans As OleDbTransaction = Cn.BeginTransaction(IsolationLevel.ReadCommitted)
        Cmd.Connection = Cn
        Cmd.Transaction = trans
        Dim X As Integer
        Try

            X = Cmd.ExecuteNonQuery()
            trans.Commit()
        Catch ex As OleDbException
            X = -1
            trans.Rollback()
            _errMsg = ex.Message
            Throw New ApplicationException(ex.Message, ex)
        Catch ex As ApplicationException
            X = -1
            trans.Rollback()
            _errMsg = ex.Message
            Throw ex
        Finally
            Cn.Close()
        End Try
        Return X
    End Function
    Public Function ExecuteGrid(ByRef Cmd As OleDbCommand, Optional ByVal IdentityCheck As Boolean = False) As Integer
        Dim Cn As New OleDbConnection(Strcon)
        Cmd.Connection = Cn
        Dim X As Integer
        Try
            Cn.Open()
            m_Identity = 0
            X = Cmd.ExecuteNonQuery()
            If IdentityCheck = True Then
                Dim cmd3 As New OleDbCommand("select @@identity", Cn)
                m_Identity = cmd3.ExecuteScalar
            End If
        Catch
            X = -1
        Finally
            Cn.Close()
        End Try
        Return X
    End Function
    Public Sub CreateParam(ByRef Cmd As OleDbCommand, ByVal StrType As String)
        'T:Text, M:Memo, Y:Currency, D:Datetime, I:Integer, S:Single, B:Boolean, P: Picture
        Dim i As Integer
        Dim j As String
        For i = 1 To Len(StrType)
            j = UCase(Mid(StrType, i, 1))
            Dim P1 As New OleDbParameter
            P1.ParameterName = "@P" & i
            Select Case j
                Case "T"
                    P1.OleDbType = OleDbType.VarChar
                Case "M"
                    P1.OleDbType = OleDbType.LongVarChar
                Case "Y"
                    P1.OleDbType = OleDbType.Currency
                Case "D"
                    P1.OleDbType = OleDbType.Date
                Case "I"
                    P1.OleDbType = OleDbType.Integer
                Case "S"
                    P1.OleDbType = OleDbType.Decimal
                Case "B"
                    P1.OleDbType = OleDbType.Boolean
                Case "P"
                    P1.OleDbType = OleDbType.Binary
            End Select
            Cmd.Parameters.Add(P1)
        Next
    End Sub
End Class


