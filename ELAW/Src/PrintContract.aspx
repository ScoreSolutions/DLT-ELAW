<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PrintContract.aspx.vb" Inherits="Src_PrintContract" %>

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
                        
                                <table border="0" cellpadding="1" cellspacing="1" class="style4" width="100%" 
                                    align="right">
                                    <tr>
                                        <td class="style2" width="15%" colspan="4" style="text-align: right;" 
                                            align="right">
                                            <asp:Label ID="Label10" runat="server" Font-Bold="True" Font-Names="AngsanaUPC" 
                                                Font-Size="16pt" Text="สัญญาเลขที่"></asp:Label>
&nbsp;<asp:Label ID="lblNo" runat="server" Text='<%# BindField("contract_no") %>' Font-Names="Angsana New" Font-Size="16pt"></asp:Label>
                                            <asp:ImageButton ID="ImageButton1" runat="server" 
                                                ImageUrl="~/images/print.gif" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style2" width="15%" colspan="4" style="text-align: center;">
                                    <asp:Label ID="lblSubType" runat="server" 
                                        Text='<%# BindField("subtype_name") %>' Font-Bold="True" Font-Size="16pt" 
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
                                    <asp:Label ID="lblDetail" runat="server" 
                                        Text='<%# BindField("message") %>'></asp:Label>
                                        </td>
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
