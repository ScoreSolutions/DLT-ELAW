<%@ Page Language="VB" AutoEventWireup="false" CodeFile="BookInPopupPreview.aspx.vb" Inherits="Src_BookInPopupPreview" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>รายละเอียดหนังสือรับ</title>
    <link href="../StyleSheet/StyleSheet.css" rel="stylesheet" type="text/css" /> 


    


    </head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>


            <table  cellpadding="0" cellspacing="0" frame="border" 
        border="0" width="700px">
                <tr>
                    <td class="sslbl_black_n">
                        
                                &nbsp;</table>

                                <table border="0" cellpadding="1" cellspacing="1" class="form" width="100%">
                                    <tr>
                                        <td colspan="4" style="text-align: right;" align="right" 
                                            class="sslbl_right">
                                            <asp:ImageButton ID="ImageButton1" runat="server" 
                                                ImageUrl="~/images/print.gif" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="15%" colspan="4" style="text-align: center;">
                                            <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="Medium" 
                                                Text="ทะเบียนหนังสือรับ"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="15%" colspan="4" style="text-align: center;">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="sslbl_black" width="15%">
                                            &nbsp;</td>
                                        <td class="sslbl">
                                            &nbsp;</td>
                                        <td align="right" class="sslbl_black">
                                            เลขที่หนังสือ</td>
                                        <td class="sslbl_black_n" width="35%">
                                            <asp:Label ID="lblBookNo" runat="server" Text='<%# BindField("bookin_no") %>'></asp:Label>
                                            <asp:Label ID="lblIdNew" runat="server"></asp:Label>
                                            <asp:Label ID="lblId" runat="server"></asp:Label>
                                            <asp:Label ID="lblSysNo" runat="server" Text='<%# BindField("system_no") %>' 
                                                Visible="False"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="sslbl_black" width="15%">
                                            &nbsp;</td>
                                        <td class="sslbl">
                                            &nbsp;</td>
                                        <td align="right" class="sslbl_black">
                                            ความเร่งด่วน</td>
                                        <td class="sslbl_black_n" width="35%">
                                            <asp:Label ID="lblPriority" runat="server" Text='<%# BindField("priority_name") %>'></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="sslbl_black" width="15%">
                                            &nbsp;</td>
                                        <td class="sslbl_black" align="right">
                                            ลงวันที่</td>
                                        <td align="char" class="sslbl">
                                            <asp:Label ID="lblStampDateThai" runat="server" 
                                                ></asp:Label>
                                        </td>
                                        <td class="sslbl_black_n" width="35%">
                                            <asp:Label ID="lblStampDate" runat="server" 
                                                Text='<%# BindField("stamp_date") %>' Visible="False"></asp:Label>
                                            </td>
                                    </tr>
                                    <tr>
                                        <td class="sslbl_black" width="15%">
                                            &nbsp;</td>
                                        <td class="sslbl_black" align="right">
                                            วันที่รับเรื่อง</td>
                                        <td align="char" class="sslbl">
                                            <asp:Label ID="lblRecieveDateThai" runat="server" 
                                                Text='<%# BindField("recieve_date") %>'></asp:Label>
                                        </td>
                                        <td class="sslbl_black_n" width="35%">
                                            <asp:Label ID="lblRecieveDate" runat="server" 
                                                Text='<%# BindField("recieve_date") %>' Visible="False"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="sslbl_black" width="15%">
                                            ชนิดหนังสือ</td>
                                        <td class="sslbl" align="left">
                                            <asp:Label ID="lblBookType" runat="server" 
                                                Text='<%# BindField("bookkind_name") %>'></asp:Label>
                                        </td>
                                        <td align="char" class="sslbl">
                                            &nbsp;</td>
                                        <td class="sslbl_black_n" width="35%">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="sslbl_black" width="15%">
                                            เรื่อง</td>
                                        <td class="sslbl" align="left">
                                            <asp:Label ID="lblTopic" runat="server" Text='<%# BindField("topic") %>'></asp:Label>
                                        </td>
                                        <td align="char" class="sslbl">
                                            &nbsp;</td>
                                        <td class="sslbl_black_n" width="35%">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="sslbl_black" width="15%">
                                            จาก</td>
                                        <td class="sslbl" align="left">
                                            <asp:Label ID="lblFrom" runat="server" Text='<%# BindField("from_name") %>'></asp:Label>
                                        </td>
                                        <td align="char" class="sslbl">
                                            &nbsp;</td>
                                        <td class="sslbl_black_n" width="35%">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="sslbl_black" width="15%">
                                            สถานะหนังสือ</td>
                                        <td class="sslbl">
                                            <asp:Label ID="lblStatus" runat="server" Text='<%# BindField("status_name") %>'></asp:Label>
                                        </td>
                                        <td class="">
                                            &nbsp;</td>
                                        <td class="sslbl_black_n" width="35%" >
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="sslbl_black" colspan="4">
_____________________________________________________________________________ 
</tr>
                                    <tr>
                                        <td class="sslbl_black">
                                            เสนอ</td>
                                        <td class="sslbl">
                                            <asp:Label ID="lblPresent" runat="server" Text='<%# BindField("present") %>'></asp:Label>
                                        </td>
                                        <td class="">
                                            &nbsp;</td>
                                        <td class="sslbl_black_n">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="sslbl_black">
                                            ส่งถึง</td>
                                        <td class="sslbl">
                                            <asp:Label ID="lblSendTo" runat="server" Text='<%# BindField("send_name") %>'></asp:Label>
                                        </td>
                                        <td class="">
                                            &nbsp;</td>
                                        <td class="sslbl_black_n" >
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="sslbl_black">
                                            คำค้นหา</td>
                                        <td class="sslbl">
                                            <asp:Label ID="lblKeyword" runat="server" Text='<%# BindField("keyword") %>'></asp:Label>
                                        </td>
                                        <td class="">
                                            &nbsp;</td>
                                        <td class="sslbl_black_n">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="sslbl_black">
                                            &nbsp;</td>
                                        <td class="sslbl">
                                            &nbsp;</td>
                                        <td class="">
                                            &nbsp;</td>
                                        <td class="sslbl_black_n" >
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="sslbl_black">
                                            ผู้ป้อนข้อมูล</td>
                                        <td class="sslbl">
                                            <asp:Label ID="lblCreate" runat="server" 
                                                Text='<%# BindField("creation_name") %>'></asp:Label>
                                        </td>
                                        <td class="">
                                            &nbsp;</td>
                                        <td class="sslbl_black_n">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="sslbl_black">
                                            &nbsp;</td>
                                        <td class="sslbl">
                                            &nbsp;</td>
                                        <td class="">
                                            &nbsp;</td>
                                        <td class="sslbl_black_n" >
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="sslbl_black" colspan="4">
                                                    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
                                                        AllowSorting="True" AutoGenerateColumns="False" 
                                                        CssClass="GridViewStyle" Width="100%">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="ชื่อเอกสาร">
                                                                
                                                                <EditItemTemplate>
                                                                    
                                                                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("title") %>'></asp:TextBox>
                                                                    
                                                                </EditItemTemplate>
                                                                
                                                                <ItemTemplate>
                                                                    
                                                                    <asp:Label ID="Label9" runat="server" Text='<%# Bind("title") %>'></asp:Label>
                                                                    
                                                                </ItemTemplate>
                                                                
                                                                <ItemStyle Width="50%" />
                                                                
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="จำนวนหน้า" HeaderStyle-HorizontalAlign="Center">
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("page") %>'></asp:TextBox>
                                                                </EditItemTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("page") %>'></asp:Label>
                                                                </ItemTemplate>

<HeaderStyle HorizontalAlign="Center"></HeaderStyle>

                                                                <ItemStyle HorizontalAlign="Center" Width="20%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="ดาวน์โหลด" HeaderStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblLink" runat="server" Text=""></asp:Label>
                                                                </ItemTemplate>

<HeaderStyle HorizontalAlign="Center"></HeaderStyle>

                                                                <ItemStyle HorizontalAlign="Center" Width="15%" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </td>
                                    </tr>
                                    <tr>
                                        <td class="sslbl_black">
                                            &nbsp;</td>
                                        <td class="sslbl">
                                            &nbsp;</td>
                                        <td class="">
                                            &nbsp;</td>
                                        <td class="sslbl_black_n" >
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="sslbl_black">
                                            เรียน
                                            <asp:Label ID="lblSendPosition" runat="server" 
                                                Text='<%# BindField("pos1") %>'></asp:Label>
                                        </td>
                                        <td class="sslbl">
                                            &nbsp;</td>
                                        <td class="">
                                            &nbsp;</td>
                                        <td class="sslbl_black_n" >
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="sslbl" colspan="3">
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:Label ID="lblComment1" runat="server" Text='<%# BindField("sendto_comment1") %>'></asp:Label>
                                        </td>
                                        <td class="sslbl_black_n" >
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="style2" colspan="3">
                                        </td>
                                        <td class="style2" >
                                            </td>
                                    </tr>
                                    <tr>
                                        <td class="style2" colspan="3">
                                            &nbsp;</td>
                                        <td class="style2" >
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="style2" colspan="3">
                                            &nbsp;</td>
                                        <td class="style2" >
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="sslbl" align ="center" colspan="2" >
                                            (
                                            <asp:Label ID="lblSendTo1" runat="server" Text='<%# BindField("send_name") %>'></asp:Label>
                                        &nbsp;)</td>
                                        <td class="">
                                            &nbsp;</td>
                                        <td class="sslbl_black_n" >
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="sslbl" align ="center" colspan="2" >
                                            <asp:Label ID="lblPos" runat="server" Text='<%# BindField("pos") %>'></asp:Label>
                                        </td>
                                        <td class="">
                                            &nbsp;</td>
                                        <td class="sslbl_black_n" >
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="sslbl" align ="center" colspan="2" >
                                            <asp:Label ID="lblSendToDate1" runat="server" Text='<%# BindField("sendto_date1") %>'></asp:Label>
                                        </td>
                                        <td class="">
                                            &nbsp;</td>
                                        <td class="sslbl_black_n" >
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="sslbl" align ="center" colspan="2" >
                                            &nbsp;</td>
                                        <td class="">
                                            &nbsp;</td>
                                        <td class="sslbl_black_n" >
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="sslbl_black" colspan="2" >
                                            เรียน
                                            <asp:Label ID="lblSendTo2" runat="server" Text='<%# BindField("send_name2") %>'></asp:Label>
                                        </td>
                                        <td class="">
                                            &nbsp;</td>
                                        <td class="sslbl_black_n" >
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="sslbl"  colspan="3" >
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:Label ID="lblComment2" runat="server" 
                                                Text='<%# BindField("sendto_comment2") %>'></asp:Label>
                                        </td>
                                        <td class="sslbl_black_n" >
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="sslbl"  colspan="3" >
                                            &nbsp;</td>
                                        <td class="sslbl_black_n" >
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="sslbl"  colspan="3" >
                                            &nbsp;</td>
                                        <td class="sslbl_black_n" >
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="sslbl"  colspan="3" >
                                            &nbsp;</td>
                                        <td class="sslbl_black_n" >
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="sslbl"  colspan="2" align ="center"  >
                                            (
                                            <asp:Label ID="lblSendToLeader" runat="server" Text='<%# BindField("send_name1") %>'></asp:Label>
                                        &nbsp;)</td>
                                        <td class="">
                                            </td>
                                        <td class="" >
                                            </td>
                                    </tr>
                                    <tr>
                                        <td class="sslbl"  colspan="2" align ="center"  >
                                            <asp:Label ID="lblPos1" runat="server" Text='<%# BindField("pos1") %>'></asp:Label>
                                        </td>
                                        <td class="">
                                            &nbsp;</td>
                                        <td class="" >
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="sslbl"  colspan="2" align ="center"  >
                                            <asp:Label ID="lblSendToDate2" runat="server" 
                                                Text='<%# BindField("sendto_date2") %>'></asp:Label>
                                        </td>
                                        <td class="">
                                            &nbsp;</td>
                                        <td class="" >
                                            &nbsp;</td>
                                    </tr>
                                    </table>
                    
    </form>
</body>
</html>
