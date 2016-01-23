<%@ Page Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="TopDownload.aspx.vb" Inherits="Src_TopDownload" title="สถิติการดาวน์โหลด" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table class="form" border="0" cellpadding="1" 
        cellspacing="1">
                <tr>
                    <td class="HeaderGreen">
                        สถิติการดาวน์โหลด</td>
                </tr>
                <tr>
                    <td width="100%">
                        <asp:GridView ID="GridView1" runat="server" 
                            AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" 
                            CssClass="GridViewStyle" GridLines="None" PageSize="20" Width="100%" 
                            ForeColor="#333333">
                            <RowStyle BackColor="White" ForeColor="#333333" />
                            <Columns>
                              
                                <asp:TemplateField HeaderText="ประเภทเอกสาร" SortExpression="subtype_name">
                                  
                                    <ItemTemplate>
                                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("subtype_name") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="70%" />
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="จำนวน(ครั้ง)" SortExpression="qty">
                                     <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("qty") %>'></asp:Label>
                                    </ItemTemplate>
                                     <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle Width="10%" HorizontalAlign="Center" />
                                </asp:TemplateField>
                
                                <asp:BoundField DataField="updated_date" DataFormatString="{0:dd/MM/yyyy HH:mm}" 
                                    HeaderText="วันเวลาดาวน์โหลดล่าสุด" SortExpression="updated_date">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle Width="20%" HorizontalAlign="Center" />
                                </asp:BoundField>
                               
                              
                            </Columns>
                            <FooterStyle BackColor="#990000" ForeColor="White" Font-Bold="True" />
                            <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                            <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                            <AlternatingRowStyle BackColor="White" />
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <br />
</asp:Content>

