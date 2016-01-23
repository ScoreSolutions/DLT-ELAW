Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient

Public Class LoginUser
    Dim _UserEmpID As String
    Dim _LoginName As String
    Dim _Prefix As String
    Dim _PrefixName As String
    Dim _FirstName As String
    Dim _LastName As String
    Dim _posID As Int64
    Dim _deptID As Int64
    Dim _divID As Int64
    Dim _posName As String
    Dim _deptName As String
    Dim _divName As String
    Dim _lastLogon As DateTime
    Dim _forceChangePwd As String
    Dim _pwdExpireDate As Date


    Public ReadOnly Property UserEmpID() As String
        Get
            Return _UserEmpID
        End Get
    End Property
    Public ReadOnly Property LoginName() As String
        Get
            Return _LoginName
        End Get
    End Property
    Public ReadOnly Property Prefix() As String
        Get
            Return _Prefix
        End Get
    End Property
    Public ReadOnly Property PrefixName() As String
        Get
            Return _PrefixName
        End Get
    End Property
    Public ReadOnly Property FirstName() As String
        Get
            Return _FirstName
        End Get
    End Property
    Public ReadOnly Property LastName() As String
        Get
            Return _LastName
        End Get
    End Property
    Public ReadOnly Property PosID() As Int64
        Get
            Return _posID
        End Get
    End Property
    Public ReadOnly Property DeptID() As Int64
        Get
            Return _deptID
        End Get
    End Property
    Public ReadOnly Property DivID() As Int64
        Get
            Return _divID
        End Get
    End Property
    Public ReadOnly Property PosName() As String
        Get
            Return _posName
        End Get
    End Property
    Public ReadOnly Property DeptName() As String
        Get
            Return _deptName
        End Get
    End Property
    Public ReadOnly Property DivName() As String
        Get
            Return _divName
        End Get
    End Property
    Public ReadOnly Property LastLogon() As DateTime
        Get
            Return _lastLogon
        End Get
    End Property
    Public ReadOnly Property ForceChangePwd() As String
        Get
            Return _forceChangePwd
        End Get
    End Property
    Public ReadOnly Property PwdExpireDate() As Date
        Get
            Return _pwdExpireDate
        End Get
    End Property


    Public Sub GetUserData(ByVal UserID As String)

        Dim sql As String = ""
        sql += " select u.empid, u.loginname, e.prefix, pe.prefix_name, e.firstname,e.lastname,"
        sql += " e.pos_id, p.pos_name, e.dept_id, d.dept_name, e.div_id, di.div_name, "
        sql += " u.forcechangepwd, u.last_logon, u.pwdexpiredate"
        sql += " from users u"
        sql += " inner join EMPLOYEE e on e.empid=u.empid"
        sql += " left join PREFIX pe on pe.prefix_id=e.prefix"
        sql += " left join POSITION p on p.pos_id=e.pos_id"
        sql += " left join DEPARTMENT d on d.dept_id=e.dept_id"
        sql += " left join DIVISION di on di.div_id=e.div_id "
        sql += " where u.loginname='" & UserID & "'"

        Dim MD As New MainData
        Dim dt As DataTable = MD.GetDataTable(sql)

        If dt.Rows.Count = 1 Then
            Dim dr As DataRow = dt.Rows(0)
            If Convert.IsDBNull(dr("empid")) = False Then _UserEmpID = dr("empid").ToString()
            If Convert.IsDBNull(dr("loginname")) = False Then _LoginName = dr("loginname").ToString()
            If Convert.IsDBNull(dr("prefix")) = False Then _Prefix = dr("prefix").ToString()
            If Convert.IsDBNull(dr("prefix_name")) = False Then _PrefixName = dr("prefix_name").ToString()
            If Convert.IsDBNull(dr("firstname")) = False Then _FirstName = dr("firstname").ToString()
            If Convert.IsDBNull(dr("lastname")) = False Then _LastName = dr("lastname").ToString()
            If Convert.IsDBNull(dr("pos_id")) = False Then _posID = Convert.ToInt64(dr("pos_id"))
            If Convert.IsDBNull(dr("dept_id")) = False Then _deptID = Convert.ToInt64(dr("dept_id"))
            If Convert.IsDBNull(dr("div_ID")) = False Then _divID = Convert.ToInt64(dr("div_id"))
            If Convert.IsDBNull(dr("pos_name")) = False Then _posName = dr("pos_name").ToString()
            If Convert.IsDBNull(dr("dept_name")) = False Then _deptName = dr("dept_name").ToString()
            If Convert.IsDBNull(dr("div_name")) = False Then _divName = dr("div_name").ToString()
            If Convert.IsDBNull(dr("last_logon")) = False Then _lastLogon = Convert.ToDateTime(dr("last_logon"))
            If Convert.IsDBNull(dr("forcechangepwd")) = False Then _forceChangePwd = dr("forcechangepwd").ToString()
            If Convert.IsDBNull(dr("pwdexpiredate")) = False Then _pwdExpireDate = Convert.ToDateTime(dr("pwdexpiredate"))
        End If
    End Sub
End Class
