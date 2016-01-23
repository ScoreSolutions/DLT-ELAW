<%@ Page Title="กำหนดสิทธิ์การใช้งาน-LAW8407" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="ModulePermission.aspx.vb" Inherits="Src_ModulePermission" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table class="form" border="0" cellpadding="1" 
        cellspacing="1">
                <tr>
                    <td class="HeaderGreen">
                        กำหนดสิทธิ์การใช้งาน</td>
                </tr>
                <tr>
                    <td width="100%">
                        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
                            AllowSorting="True" AutoGenerateColumns="False" BackColor="White" 
                            BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="1" 
                            CssClass="GridViewStyle" GridLines="Vertical" PageSize="20" Width="100%">
                            <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                            <Columns>
                                <asp:TemplateField HeaderText="กลุ่ม" SortExpression="module_name">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("group_name") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("group_name") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="50%" />
                                </asp:TemplateField>
                              
                                <asp:CommandField EditText="Add" HeaderText="เมนู" ShowEditButton="True" HeaderStyle-HorizontalAlign="Center">
                                    <ItemStyle Font-Bold="True" ForeColor="#000099" HorizontalAlign="Center" 
                                        Width="7%" />
                                </asp:CommandField>
                                <asp:CommandField DeleteText="Add" HeaderText="ผู้ใช้" ShowDeleteButton="True" HeaderStyle-HorizontalAlign="Center">
                                    <ItemStyle Font-Bold="True" ForeColor="#000099" HorizontalAlign="Center" 
                                        Width="7%" />
                                </asp:CommandField>
                            </Columns>
                            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                            <AlternatingRowStyle BackColor="#DCDCDC" />
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <br />
</asp:Content>

