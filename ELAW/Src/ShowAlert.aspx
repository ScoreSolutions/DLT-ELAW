<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ShowAlert.aspx.vb" Inherits="Src_ShowAlert" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>รายละเอียดการแจ้งเตือน-LAW3402</title>
    <link href="../StyleSheet/StyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>


            <table  cellpadding="0" cellspacing="0" frame="border" 
        border="0" width="700px" class="style3">
                <tr>
                    <td class="sslbl_black_n">
                        
                                <table border="0" cellpadding="1" cellspacing="1" class="style3" width="100%">
                                    <tr>
                                        <td width="100%" colspan="4"  align="right">
                                            <asp:ImageButton ID="ImageButton1" runat="server" 
                                                ImageUrl="~/images/print.gif" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style2" width="15%" colspan="4" style="text-align: center;">
                                            <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="Medium" 
                                                Text="แจ้งเตือนคดี"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="sslbl_black" width="15%">
                                            หมายเลขคดีดำ</td>
                                        <td class="sslbl_black_n" width="35%">
                                            <asp:Label ID="lblBlackNo" runat="server" Text='<%# BindField("black_no") %>'></asp:Label>
                                        </td>
                                        <td align="right" class="sslbl_black">
                                            &nbsp;</td>
                                        <td class="sslbl_black_n" width="35%" align="right">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="sslbl_black" width="15%">
                                            หมายเลขคดีแดง</td>
                                        <td class="sslbl_black_n" width="35%">
                                            <asp:Label ID="lblRedNo" runat="server" Text='<%# BindField("red_no") %>'></asp:Label>
                                        </td>
                                        <td align="right" class="sslbl_black">
                                            &nbsp;</td>
                                        <td class="sslbl_black_n" width="35%" align="right">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="sslbl_black" width="15%">
                                            ประเภทคดี</td>
                                        <td class="sslbl_black_n" width="35%" align="char">
                                    <asp:Label ID="lblCaseType" runat="server" Text='<%# BindField("type_name") %>'></asp:Label>
                                        </td>
                                        <td align="char" class="sslbl_black_n">
                                            &nbsp;</td>
                                        <td class="sslbl_black_n" width="35%">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="sslbl_black" width="15%">
                                            สถานะคดี</td>
                                        <td class="sslbl_black_n" width="35%" align="char">
                                    <asp:Label ID="lblStatus" runat="server" 
                                        Text='<%# BindField("status_name") %>'></asp:Label>
                                        </td>
                                        <td align="char" class="sslbl_black_n">
                                            &nbsp;</td>
                                        <td class="sslbl_black_n" width="35%">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="sslbl_black">
                                            ชื่อพนักงานอัยการ</td>
                                        <td class="sslbl_black_n">
                                    <asp:Label ID="lblAttornney" runat="server" 
                                        Text='<%# BindField("attorney_name") %>'></asp:Label>
                                        </td>
                                        <td class="style1">
                                            &nbsp;</td>
                                        <td class="sslbl_black_n" >
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="sslbl_black">
                                            โจทก์</td>
                                        <td class="sslbl_black_n">
                                    <asp:Label ID="lblDefandent" runat="server" 
                                        Text='<%# BindField("defendant") %>'></asp:Label>
                                        </td>
                                        <td class="style1">
                                            &nbsp;</td>
                                        <td class="sslbl_black_n" >
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="sslbl_black">
                                            โจทก์(ร่วม)</td>
                                        <td class="sslbl_black_n">
                                    <asp:Label ID="lblDefandent1" runat="server" 
                                        Text='<%# BindField("defendant1") %>'></asp:Label>
                                        </td>
                                        <td class="style1">
                                            &nbsp;</td>
                                        <td class="sslbl_black_n" >
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="sslbl_black">
                                            จำเลย</td>
                                        <td class="sslbl_black_n">
                                    <asp:Label ID="lblProsecutor" runat="server" 
                                        Text='<%# BindField("prosecutor") %>'></asp:Label>
                                        </td>
                                        <td class="style1">
                                            &nbsp;</td>
                                        <td class="sslbl_black_n" >
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="sslbl_black">
                                            จำเลย(ร่วม)</td>
                                        <td class="sslbl_black_n">
                                    <asp:Label ID="lblProsecutor1" runat="server" 
                                        Text='<%# BindField("prosecutor") %>'></asp:Label>
                                        </td>
                                        <td class="style1">
                                            &nbsp;</td>
                                        <td class="sslbl_black_n" >
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="sslbl_black">
                                            &nbsp;</td>
                                        <td class="sslbl_black_n">
                                            &nbsp;</td>
                                        <td class="style1">
                                            &nbsp;</td>
                                        <td class="sslbl_black_n" >
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="sslbl_black">
                                            &nbsp;</td>
                                        <td class="sslbl_black_n">
                                            &nbsp;</td>
                                        <td class="style1">
                                            &nbsp;</td>
                                        <td class="sslbl_black_n" >
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="sslbl_black">
                                            &nbsp;</td>
                                        <td class="sslbl_black_n">
                                            &nbsp;</td>
                                        <td class="style1">
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
                                                            <asp:TemplateField HeaderText="เรื่อง">
                                                                
                                                                <EditItemTemplate>
                                                                    
                                                                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("title") %>'></asp:TextBox>
                                                                    
                                                                </EditItemTemplate>
                                                                
                                                                <ItemTemplate>
                                                                    
                                                                    <asp:Label ID="Label9" runat="server" Text='<%# Bind("title") %>'></asp:Label>
                                                                    
                                                                </ItemTemplate>
                                                                
                                                                <ItemStyle Width="50%" />
                                                                
                                                            </asp:TemplateField>
                                                            
                                                            <asp:BoundField DataField="dates" DataFormatString="{0:dd/MM/yyyy}" 
                                                                HeaderText="วันที่" HtmlEncode="False">
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" Width="20%" />
                                                            </asp:BoundField>
                                                            
                                                        </Columns>
                                                    </asp:GridView>
                                                </td>
                                    </tr>
                                    <tr>
                                        <td class="sslbl_black">
                                            &nbsp;</td>
                                        <td class="sslbl_black_n">
                                            &nbsp;</td>
                                        <td class="style1">
                                            &nbsp;</td>
                                        <td class="sslbl_black_n" >
                                            &nbsp;</td>
                                    </tr>
                                    </table>
                    
            </table>

    </form>
    
</body>
</html>
