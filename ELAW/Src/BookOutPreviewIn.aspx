<%@ Page Language="VB" AutoEventWireup="false" CodeFile="BookOutPreviewIn.aspx.vb" Inherits="Src_BookOutPreviewIn" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit.HTMLEditor" tagprefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
 <link href="../StyleSheet/StyleSheet.css" rel="stylesheet" type="text/css" /> 
   
    <style type="text/css">
        .style3
        {
            width: 100%;
        }
        .style2
        {
            border-bottom-style: solid;
            border-bottom-width: 1px;
     
        }
       
    </style>
   
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    <div align="left">
    <!--รูปครุฑ-->
    <table class="" width ="700">
            <tr>
                <td width="33%">
                    </td>
                <td class="" width="33%" align="center">
               
                    <asp:Label ID="Label10" runat="server" Font-Names="Cordia New" Font-Size="33pt" 
                        Text="บันทึกข้อความ" Font-Bold="True" ></asp:Label>
                         
                </td>
                <td width="33%">
                                            <asp:ImageButton ID="ImageButton1" runat="server" 
                                                ImageUrl="~/images/print.gif" />
                </td>
            </tr>
            <tr>
                <td width="33%">
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/HeadSmall.png" />
                    </td>
                <td class="" width="33%">
               
                    &nbsp;</td>
                <td width="33%">
                    <asp:Label ID="lblDate" runat="server" Font-Names="Cordia New" 
                        Font-Size="16pt" Text='<%# BindField("dates") %>' Visible="False"></asp:Label>
                    </td>
            </tr>
        </table>
         <!--ส่วนราชการ-->
                <table class="" width ="700">
            <tr>
                <td width="15%">
                    <asp:Label ID="Label7" runat="server" Font-Names="Cordia New" Font-Size="19pt"  Font-Bold ="true" 
                        Text="ส่วนราชการ"  ></asp:Label>
                </td>
                <td class="style2" width="90%">
               
                    <asp:Label ID="Label8" runat="server" Font-Names="Cordia New" Font-Size="16pt" 
                        Text="สำนักกฎหมาย   โทร. 02 271 8791" Font-Overline="False" 
                        Font-Underline="False"  ></asp:Label>
                         
                </td>
            </tr>
        </table>
      <!--ที่-->  
        <table class="" width ="700">
            <tr>
                <td width="5%">
                    <asp:Label ID="Label3" runat="server" Font-Names="Cordia New" Font-Size="19pt" Font-Bold ="true"  
                        Text="ที่" ></asp:Label>
                </td>
                <td class="style2" width="30%">
                <asp:Label ID="lblBookOutNo" runat="server" Font-Names="Cordia New" 
                        Font-Size="16pt"><%#BindField("bookout_no")%></asp:Label>
                         
                </td>
                <td width="7%">
                    <asp:Label ID="Label11" runat="server" Font-Names="Cordia New" Font-Size="19pt" Font-Bold ="true"  
                        Text="วันที่"></asp:Label> 
                </td>
                <td class="style2" width="60%">
                    <asp:Label ID="lblDateThai" runat="server" Font-Names="Cordia New" 
                        Font-Size="16pt"></asp:Label>
                   
                
                </td>
            </tr>
        </table>
        <!--เรื่อง-->
                <table class="" width ="700">
            <tr>
                <td>
                    <asp:Label ID="Label5" runat="server" Font-Names="Cordia New" Font-Size="19pt" Font-Bold ="true"  
                        Text="เรื่อง" ></asp:Label>
                </td>
                <td class="style2" width="100%">
               
                <asp:Label ID="lblTopic" runat="server" Font-Names="Cordia New" 
                        Font-Size="16pt"><%#BindField("topic")%></asp:Label>
                         
                </td>
            </tr>
        </table>
        <!--เรียน-->
                <table class="" width ="700">
            <tr>
                <td>
                    <asp:Label ID="Label6" runat="server" Font-Names="Cordia New" Font-Size="16pt" 
                        Text="เรียน" Font-Bold="True" ></asp:Label>
                </td>
                <td class="style2" width="100%">
               
                    <asp:Label ID="lblPresent" runat="server" Font-Names="Cordia New" 
                        Font-Size="16pt"><%#BindField("present")%></asp:Label>
                         
                </td>
            </tr>
        </table>
        <!--<br />-->
        <table cellpadding="1" cellspacing="1" frame="border" 
        border="0" width="700">
            <tr>
                <td colspan="4">
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="lblMessage" runat="server" Font-Names="Cordia New" 
                        Font-Size="16pt"><%#BindField("message")%></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style5">
                    &nbsp;</td>
                <td class="style5">
                    &nbsp;</td>
                <td class="sslbl" colspan="2" >
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style5">
                    &nbsp;</td>
                <td class="style5">
                    &nbsp;</td>
                <td class="sslbl" align ="center"  >
                    <asp:Label ID="lblPostName" runat="server" Font-Names="Cordia New" 
                        Font-Size="16pt"><%#BindField("postname")%></asp:Label>
                </td>
                <td class="" >
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style5">
                    &nbsp;</td>
                <td class="style5">
                    &nbsp;</td>
                <td class="sslbl" align ="center"  >
                    <asp:Label ID="lblPostPosition" runat="server" Font-Names="Cordia New" 
                        Font-Size="16pt"><%#BindField("post_pos")%></asp:Label>
                </td>
                <td class="" >
                    <asp:Label ID="lblPostScript" runat="server" Font-Names="Cordia New" 
                        Font-Size="16pt" Visible="False"><%#BindField("postscript")%></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style5">
                    <asp:Label ID="Label12" runat="server" Font-Names="Cordia New" Font-Size="16pt" 
                        Text="เรียน" ></asp:Label>
                &nbsp;<asp:Label ID="lblPos1" runat="server" Font-Names="Cordia New" 
                        Font-Size="16pt" Text='<%# BindField("pos1") %>'></asp:Label>
                                        </td>
                <td class="style5">
                    &nbsp;</td>
                <td class="sslbl" colspan="2" >
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="sslbl" colspan="3">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="lblComment" runat="server" Font-Names="Cordia New" 
                        Font-Size="16pt"><%#BindField("sendto_comment")%></asp:Label>
                    &nbsp;</td>
                <td class="" >
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style5" align="center" >
                    &nbsp;</td>
                <td class="style5" align="center" >
                    &nbsp;</td>
                <td class="sslbl" >
                    &nbsp;</td>
                <td class="" >
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style5" align="center" >
                    (
                    <asp:Label ID="lblName" runat="server" Font-Names="Cordia New" Font-Size="16pt" 
                        Text='<%# BindField("sendname") %>'></asp:Label>
                                        &nbsp;)</td>
                <td class="style5" align="center" >
                    &nbsp;</td>
                <td class="sslbl" >
                    &nbsp;</td>
                <td class="" >
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style5" align="center" >
                    <asp:Label ID="lblPos" runat="server" Font-Names="Cordia New" Font-Size="16pt" Text='<%# BindField("pos") %>'></asp:Label>
                                        </td>
                <td class="style5" align="center" >
                    &nbsp;</td>
                <td class="sslbl" >
                    &nbsp;</td>
                <td class="" >
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style5" align="center" >
                    <asp:Label ID="lblSendtoDate" runat="server" Font-Names="Cordia New" Font-Size="16pt" 
                        Text='<%# BindField("sendto_date") %>'></asp:Label>
                                        </td>
                <td class="style5" align="center" >
                    &nbsp;</td>
                <td class="sslbl" >
                    &nbsp;</td>
                <td class="" >
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style5" align="center" >
                    &nbsp;</td>
                <td class="style5" align="center" >
                    &nbsp;</td>
                <td class="sslbl" >
                    &nbsp;</td>
                <td class="" >
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="sslbl" align="left" colspan="3" >
                    <asp:Label ID="Label13" runat="server" Font-Names="Cordia New" Font-Size="16pt" 
                        Text="เรียน" ></asp:Label>
                                        </td>
                <td class="" >
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="sslbl" align="left" colspan="3" >
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="lblComment1" runat="server" Font-Names="Cordia New" 
                        Font-Size="16pt"><%#BindField("sendto_comment1")%></asp:Label>
                                        </td>
                <td class="" >
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style5" align="center" >
                    &nbsp;</td>
                <td class="style5" align="center" >
                    &nbsp;</td>
                <td class="sslbl" >
                    &nbsp;</td>
                <td class="" >
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style5" align="center" >
                    (
                    <asp:Label ID="lblName1" runat="server" Font-Names="Cordia New" Font-Size="16pt" 
                        Text='<%# BindField("sendname1") %>'></asp:Label>
                                        &nbsp;)</td>
                <td class="style5" align="center" >
                    &nbsp;</td>
                <td class="sslbl" >
                    &nbsp;</td>
                <td class="" >
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style5" align="center" >
                    <asp:Label ID="lblPos2" runat="server" Font-Names="Cordia New" Font-Size="16pt" 
                        Text='<%# BindField("pos1") %>'></asp:Label>
                                        </td>
                <td class="style5" align="center" >
                    &nbsp;</td>
                <td class="sslbl" >
                    &nbsp;</td>
                <td class="" >
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style5" align="center" >
                    <asp:Label ID="lblSendtoDate1" runat="server" Font-Names="Cordia New" Font-Size="16pt" 
                        Text='<%# BindField("sendto1_date") %>'></asp:Label>
                                        </td>
                <td class="style5" align="center" >
                    &nbsp;</td>
                <td class="sslbl" >
                    &nbsp;</td>
                <td class="" >
                    &nbsp;</td>
            </tr>
            </table>
            
    </div>
    
    </div>
    </form>
</body>
</html>
