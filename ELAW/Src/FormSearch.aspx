<%@ Page Title="ค้นหาแบบฟอร์ม-LAW7204" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="FormSearch.aspx.vb" Inherits="Src_FormSearch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">

        .style10
        {
            font-family: "MS Sans Serif", "Microsoft Sans Serif", AngsanaUPC;
            color: White;
            font-weight: bold;
            font-size: 18px;
            background-color: #7C9517;
            height: 36px;
            padding: 2px;
        }
    

        .style9
        {
            font-family: "MS Sans Serif", "Microsoft Sans Serif", AngsanaUPC;
            font-size: 14px;
            color: #FFFFFF;
            font-weight : bold;
            text-decoration: none;
            vertical-align: text-top;
            width: 125px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <table class="form" border="1" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="style10" colspan="2" title="-">
                        ค้นหาแบบฟอร์ม&nbsp;</td>
                </tr>
                <tr>
                    <td class="style9">
                        รายละเอียด</td>
                    <td>
                        <asp:TextBox ID="txtDetail" runat="server" Width="500px" MaxLength="250" 
                            CssClass="ssddl"></asp:TextBox>
                        <asp:Label ID="lblId" runat="server" Visible="False"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style9">
                    </td>
                    <td>
                        <asp:Button ID="bSearch" runat="server" CssClass="ssbtn" Text="ค้นหา" />
                    </td>
                </tr>
                <tr>
                    <td class="GridViewStyle" colspan="2">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="GridView1" runat="server" AllowSorting="True" 
                    AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" 
                    BorderStyle="None" BorderWidth="1px" CellPadding="1" 
                    CssClass="GridViewStyle" GridLines="Vertical" PageSize="20" AllowPaging="True" 
                            Width="100%">
                                    <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="ชื่อแบบฟอร์ม">
                                            <ItemTemplate>
                                                <asp:Label ID="lblLink" runat="server" Text=""></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="50%" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="detail" HeaderText="รายละเอียด" 
                                    SortExpression="detail"></asp:BoundField>
                                    </Columns>
                                    <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                    <PagerStyle BackColor="#999999" ForeColor="Black" 
                        HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" 
                        ForeColor="White" />
                                    <HeaderStyle BackColor="#000084" Font-Bold="True" 
                        ForeColor="White" />
                                    <AlternatingRowStyle BackColor="#DCDCDC" />
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    
    </asp:Content>

