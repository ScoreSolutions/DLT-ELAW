<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DrafLawComparePreview.aspx.vb" Inherits="Src_DrafLawComparePreview" %>

<%@ Register src="../UserControl/ctlPrintLaw.ascx" tagname="ctlPrintLaw" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>เปรียบเทียบเวอร์ชั่น-LAW2401</title>
</head>
<link rel="stylesheet" type="text/css" href="../StyleSheet/StyleClass.css" />
<body>
    <form id="form1" runat="server">
    <div>
        <table style="background-color: #FFFFFF;" border="0" cellpadding="0" cellspacing="0" width="100%" >
            <tr>
                    <td class="HeaderGreen" colspan="3">เปรียบเทียบเวอร์ชั่นร่างกฎหมาย</td>
            </tr>
            <tr>
                <td class="sslbl" colspan="3">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td class="sslbl_right" style="width:15%;" ><b>เลขที่:</b>&nbsp;</td>
                            <td class="sslbl" style="width:35%">
                                <font size="4px" color="navy">
                                    <asp:Label ID="lblLeftLawID" runat="server" ></asp:Label>
                                </font>
                            </td>
                            <td class="sslbl_right" style="width:15%" ><b>เลขที่:</b>&nbsp;</td>
                            <td class="sslbl" style="width:35%">
                                <font size="4px" color="navy">
                                    <asp:Label ID="lblRightLawID" runat="server" ></asp:Label>
                                </font>
                            </td>
                        </tr>
                        <tr>
                            <td class="sslbl_right"><b>ประเภทกฎหมาย:</b>&nbsp;</td>
                            <td class="sslbl" ><asp:Label ID="lblLeftLawType" runat="server" ></asp:Label></td>
                            <td class="sslbl_right"><b>ประเภทกฎหมาย:</b>&nbsp;</td>
                            <td class="sslbl" ><asp:Label ID="lblRightLawType" runat="server" ></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="sslbl_right"><b>ประเภทย่อย:</b>&nbsp;</td>
                            <td class="sslbl" ><asp:Label ID="lblLeftLawSubType" runat="server" ></asp:Label></td>
                            <td class="sslbl_right"><b>ประเภทย่อย:</b>&nbsp;</td>
                            <td class="sslbl" ><asp:Label ID="lblRightLawSubType" runat="server" ></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="sslbl_right"><b>สถานะกฎหมาย:</b>&nbsp;</td>
                            <td class="sslbl" ><asp:Label ID="lblLeftStatus" runat="server" ></asp:Label></td>
                            <td class="sslbl_right"><b>สถานะกฎหมาย:</b>&nbsp;</td>
                            <td class="sslbl" ><asp:Label ID="lblRightStatus" runat="server" ></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="sslbl_right"><b>วันที่ร่างกฎหมาย:</b>&nbsp;</td>
                            <td class="sslbl" ><asp:Label ID="lblLeftCreateDate" runat="server" ></asp:Label></td>
                            <td class="sslbl_right"><b>วันที่ร่างกฎหมาย:</b>&nbsp;</td>
                            <td class="sslbl" ><asp:Label ID="lblRightCreateDate" runat="server" ></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="sslbl_right"><b>วันที่แก้ไข:</b>&nbsp;</td>
                            <td class="sslbl" >
                                <asp:Label ID="lblLeftUpdateDate" runat="server" ></asp:Label>
                                
                            </td>
                            <td class="sslbl_right"><b>วันที่แก้ไข:</b>&nbsp;</td>
                            <td class="sslbl" >
                                <asp:Label ID="lblRightUpdateDate" runat="server" ></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="sslbl_right">&nbsp;</td>
                            <td class="sslbl" align="right" >
                                <uc1:ctlPrintLaw ID="CtlLeftPrintLaw" runat="server" />
                            </td>
                            <td class="sslbl_right">&nbsp;</td>
                            <td class="sslbl" align="right" >
                                <uc1:ctlPrintLaw ID="ctlRightPrintLaw" runat="server" />
                            </td>
                        </tr>
                        
                    </table>
                </td>
            </tr>
            <tr>
                <td style="width:49%" valign="top" class="sslbl">
                    <asp:Panel ID="pnlLeft" runat="server" Height="580px" Width="100%" ScrollBars="Auto" BorderWidth="1" BorderColor="Black" BackColor="#FFFFFF" >
                        <asp:Label ID="lblLeftMessage" runat="server" Text=""></asp:Label>
                    </asp:Panel>
                </td>
                <td style="width:2%">&nbsp;</td>
                <td style="width:49%" valign="top" class="sslbl">
                    <asp:Panel ID="pnlRight" runat="server" Height="580px" Width="100%" ScrollBars="Auto" BorderWidth="1" BorderColor="Black" BackColor="#FFFFFF">
                        <asp:Label ID="lblRightMessage" runat="server" Text=""></asp:Label>
                    </asp:Panel>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
