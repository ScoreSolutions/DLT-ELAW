<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PrintLaw.aspx.vb" Inherits="Src_PrintLaw" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
   <link href="../StyleSheet/StyleSheet.css" rel="stylesheet" type="text/css" /> 
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>  
            <table  cellpadding="0" cellspacing="0" frame="border" 
        border="0" width="700px" class="style4">
                <tr>
                    <td class="sslbl_black_n">
                        
                                <table border="0" cellpadding="1" cellspacing="1" class="style4" width="100%" align="right">
                                    <tr>
                                        <td class="style2" width="15%" colspan="4" style="text-align: right;" 
                                            align="right">
                                            <asp:ImageButton ID="ImageButton1" runat="server" 
                                                ImageUrl="~/images/print.gif" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="4">
                                            <asp:Label ID="lblDraft" runat="server" Text="ร่าง" Height="50" Font-Size="Large" Visible="false" ></asp:Label>
                                            <asp:Label ID="lblCrut" runat="server" Text="<img src='../Images/HeadBig.png' >" Height="50" Font-Size="Large" Visible="false" ></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style2" width="15%" colspan="4" style="text-align: center;">
                                    <asp:Label ID="lblTitle" runat="server" 
                                        Text='<%# BindField("title") %>' Font-Bold="True" Font-Size="16pt" 
                                                Font-Names="Angsana New"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style9" width="25%">
                                            &nbsp;</td>
                                        <td class="style7" width="25%">
                                            &nbsp;</td>
                                        <td align="right" class="style5">
                                            &nbsp;</td>
                                        <td class="sslbl_black_n" width="25%">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="sslbl_black_n" colspan="4">
                                    <asp:Label ID="lblMsg" runat="server" 
                                        Text='<%# BindField("message") %>' Font-Bold="True" Font-Size="16pt" 
                                                Font-Names="Angsana New"></asp:Label>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="sslbl_black" colspan="4">
                                                    &nbsp;</td>
                                    </tr>
                                    </table>
                    
            </table>

    </form>

</body>
</html>
