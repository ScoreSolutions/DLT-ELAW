<%@ Page Language="VB"  AutoEventWireup="false" CodeFile="frmLogin.aspx.vb" Inherits="frmLogin" meta:resourcekey="th" UICulture="th" Title="เข้าสู่ระบบ" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">

<head id="Head1" runat="server">
    <title></title>
    <link href="../StyleSheet/StyleSheet.css" rel="stylesheet" type="text/css" /> 
    
    <style type="text/css">
        .style1
        {
            width: 100%;
            height:623px;
            background: url(../images/background.jpg);
        }
        .style2
        {
            height: 53%;
        }
    </style>
    
</head>

<body>

    <form id="form1" runat="server">   
    <table class="style1" width="33%">
        <tr>
            <td height="33%" width="33%">
                &nbsp;</td>
            <td style="text-align: center" width="34%">
                &nbsp;</td>
            <td width="33%">
                &nbsp;</td>
        </tr>
        <tr>
            <td height="33%" width="33%">
                &nbsp;</td>
            <td align="center" height="34%" width="34%">
                <table class="GridViewStyle">
                    <tr>
                        <td bgcolor="#336699" colspan="2" style="height: 24px; text-align: center">
                            <asp:Label ID="Label8" runat="server" Font-Bold="True" Font-Size="Medium" ForeColor="White"
                                Text="เข้าสู่ระบบ"></asp:Label></td>
                    </tr>
                    <tr>
                        <td bgcolor="#99ccff" style="width: 100px; text-align: right" align="center" 
                            height="33%" width="33%">
                            <asp:Label ID="Label6" runat="server" Font-Bold="True" ForeColor="Navy" Text="ชื่อเข้าระบบ : "></asp:Label></td>
                        <td bgcolor="#99ccff" style="width: 100px">
                            <asp:TextBox ID="txtUsername" runat="server" MaxLength="16" Width="155px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td bgcolor="#99ccff" style="width: 100px; height: 14px; text-align: right" 
                            width="33%">
                            <asp:Label ID="Label7" runat="server" Font-Bold="True" ForeColor="Navy" Text="รหัสผ่าน : "></asp:Label></td>
                        <td bgcolor="#99ccff" style="width: 100px; height: 14px">
                            <asp:TextBox ID="txtPassword" runat="server" MaxLength="16" TextMode="Password" Width="155px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td bgcolor="#336699" colspan="2" style="text-align: center">
                            <asp:Button ID="cmdLogin" runat="server" Font-Bold="True" OnClick="cmdLogin_Click" Text="Log In" />&nbsp;</td>
                    </tr>
                </table>
                </td>
            <td height="33%" width="33%">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style2" height="33%" width="33%">
                </td>
            <td class="style2" height="34%" style="text-align: center" width="34%">
                </td>
            <td class="style2" height="33%" width="33%">
                </td>
        </tr>
    </table>
    </form>
</body>
</html>
