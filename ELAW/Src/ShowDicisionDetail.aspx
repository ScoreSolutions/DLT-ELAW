<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ShowDicisionDetail.aspx.vb" Inherits="Src_ShowDicisionDetail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ดาวน์โหลดคำพิพากษา</title>
</head>
<body>
    <form id="form1" runat="server">
            <asp:GridView ID="GridView2" runat="server" AllowPaging="True" 
                AllowSorting="True" AutoGenerateColumns="False" BackColor="White" 
                BorderStyle="None" CellPadding="2" GridLines="None" Width="60%" 
                ShowHeader="False">
                <RowStyle ForeColor="#000066" />
                <Columns>
                    
                    <asp:TemplateField HeaderText="ชื่อเอกสาร">
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("title") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("title") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemStyle Width="70%" />
                    </asp:TemplateField>
               
                    <asp:TemplateField HeaderText="ดาวน์โหลด">
                        <ItemTemplate>
                            <asp:Label ID="lblLink" runat="server" Text=""></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="30%" />
                    </asp:TemplateField>
                   
                </Columns>
                <EmptyDataTemplate>
                    <table class="style5">
                        <tr>
                            <td align="center">
                                <asp:Label ID="Label8" runat="server" CssClass="GEmpty" Text="ไม่พบข้อมูล"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <FooterStyle BackColor="White" ForeColor="#000066" />
                <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
            </asp:GridView>
            </form>
</body>
</html>
