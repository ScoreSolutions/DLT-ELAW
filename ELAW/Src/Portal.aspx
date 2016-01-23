<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="Portal.aspx.vb" Inherits="Src_Portal" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table class="style1">
        <tr>
            <td class="HeaderGreen" colspan="2">
                Home</td>
        </tr>
        <tr>
            <td width="33%" style="width: 67%">
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4" 
                    ForeColor="#333333" GridLines="None" ShowHeader="False">
        <RowStyle BackColor="White" />
        <Columns>
            <asp:BoundField DataField="menu_name" ItemStyle-Font-Bold ="true" ItemStyle-ForeColor="Blue" ItemStyle-Font-Size="Large"   />
            <asp:CommandField ButtonType="Image" SelectImageUrl="~/Images/new_01.gif" 
                ShowSelectButton="True" />
            
        </Columns>
        <EmptyDataTemplate>
                    <table width="100%">
                        <tr>
                            <td align="center">
                                <asp:Label ID="Label8" runat="server" CssClass="sslbl_red" Font-Bold ="true" Font-Size ="Large"  
                                    Text="***ไม่พบงานใหม่***"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <EditRowStyle BackColor="#2461BF" />
        <AlternatingRowStyle BackColor="White" />
    </asp:GridView>
            </td>
            <td width="33%">
            </td>
        </tr>
    </table>
</asp:Content>

