<%@ Page Language="VB" AutoEventWireup="false" CodeFile="BookOutPreview.aspx.vb" Inherits="Src_BookOutPreview" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>พิมพ์หนังสือนำส่ง-LAW6402</title>
 <link href="../StyleSheet/StyleSheet.css" rel="stylesheet" type="text/css" /> 
    
   
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    <div align="left">
        <table class="form" cellpadding="1" cellspacing="1" frame="border" 
        border="0" width="700">
            <tr>
                <td class="" colspan="3" align="right">
                                            <asp:ImageButton ID="ImageButton1" runat="server" 
                                                ImageUrl="~/images/print.gif" />
                </td>
            </tr>
            <tr>
                <td class="" colspan="3" align="center" width="100%">
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/HeadBig.png" />
                </td>
            </tr>
            <tr>
                <td class="style3" align ="left" width="33%"  >
                    <asp:Label ID="Label3" runat="server" Font-Names="Cordia New" Font-Size="16pt" 
                        Text="ที่" ></asp:Label>
                &nbsp;<asp:Label ID="lblBookOutNo" runat="server" Font-Names="Cordia New" 
                        Font-Size="16pt"><%#BindField("bookout_no")%></asp:Label>
                </td>
                <td class="style4" align ="right" width="33%"  >
                    &nbsp;</td>
                <td class="sslbl" align ="left" width="33%" >
                    <asp:Label ID="Label7" runat="server" Font-Names="Cordia New" Font-Size="16pt" 
                        Text="กรมการขนส่งทางบก"  ></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style3" align ="right" >
                    &nbsp;</td>
                <td class="style4" align ="right" >
                    &nbsp;</td>
                <td class="sslbl" align ="left" >
                    <asp:Label ID="Label8" runat="server" Font-Names="Cordia New" Font-Size="16pt" 
                        Text="ถนนพหลโยธิน กทม.10900"  ></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style3" align ="right" >
                    &nbsp;</td>
                <td class="style4" align ="right" >
                    <asp:Label ID="lblDateThai" runat="server" Font-Names="Cordia New" 
                        Font-Size="16pt"></asp:Label>
                </td>
                <td class="sslbl" align ="left" >
                    <asp:Label ID="lblDate" runat="server" Font-Names="Cordia New" 
                        Font-Size="16pt" Text='<%# BindField("dates") %>' Visible="False"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style1" colspan="3">
                    <asp:Label ID="Label5" runat="server" Font-Names="Cordia New" Font-Size="16pt" 
                        Text="เรื่อง" ></asp:Label>
                &nbsp;<asp:Label ID="lblTopic" runat="server" Font-Names="Cordia New" 
                        Font-Size="16pt"><%#BindField("topic")%></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style1" colspan="3">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style1" colspan="3">
                    <asp:Label ID="Label6" runat="server" Font-Names="Cordia New" Font-Size="16pt" 
                        Text="เรียน" ></asp:Label>
                &nbsp;<asp:Label ID="lblPresent" runat="server" Font-Names="Cordia New" 
                        Font-Size="16pt"><%#BindField("present")%></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="3">
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="lblMessage" runat="server" Font-Names="Cordia New" 
                        Font-Size="16pt"><%#BindField("message")%></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style2">
                    &nbsp;</td>
                <td class="sslbl" align ="center"  >
                    <asp:Label ID="lblPostScript" runat="server" Font-Names="Cordia New" 
                        Font-Size="16pt"><%#BindField("postscript")%></asp:Label>
                </td>
                <td class="sslbl" >
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style2">
                    &nbsp;</td>
                <td class="sslbl" align ="center"  >
                    <asp:Label ID="lblPostName" runat="server" Font-Names="Cordia New" 
                        Font-Size="16pt"><%#BindField("postname")%></asp:Label>
                </td>
                <td class="sslbl" >
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style2">
                    &nbsp;</td>
                <td class="sslbl" align ="center"  >
                    <asp:Label ID="lblPostPosition" runat="server" Font-Names="Cordia New" 
                        Font-Size="16pt"><%#BindField("post_pos")%></asp:Label>
                </td>
                <td class="sslbl" >
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style2">
                    <asp:Label ID="Label2" runat="server" Font-Names="Cordia New" Font-Size="16pt" 
                        Text="กฎหมาย"></asp:Label>
                </td>
                <td class="sslbl" colspan="2" >
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style2">
                    <asp:Label ID="Label9" runat="server" Font-Names="Cordia New" Font-Size="16pt" 
                        Text="โทร 0-2272-5406"></asp:Label>
                </td>
                <td class="sslbl" colspan="2" >
                    &nbsp;</td>
            </tr>
            </table>
    </div>
    
    </div>
    </form>
</body>
</html>
