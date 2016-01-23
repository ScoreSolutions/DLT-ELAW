<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="Permission2.aspx.vb" Inherits="Src_Permission2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">


        .style11
        {
            font-family: "MS Sans Serif", "Microsoft Sans Serif", AngsanaUPC;
            color: White;
            font-weight: bold;
            font-size: 18px;
            background-color: #7C9517;
            height: 37px;
            padding: 2px;
        }
        




        .style9
        {
            font-family: "MS Sans Serif", "Microsoft Sans Serif", AngsanaUPC;
            font-size: 14px;
            color: #FFFFFF;
            font-weight : none;
            text-decoration: none;
            vertical-align: text-top;
            padding: 2px;
            }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table class="form" border="1" cellpadding="0" 
        cellspacing="0">
                <tr>
                    <td class="HeaderGreen" title="ผู้ใช้งาน-LAW9102">
                        กำหนดสิทธิ์เมนู<asp:Label ID="lblTxt" runat="server"></asp:Label>
                        <asp:Label ID="lblId" runat="server" Visible="False"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style9" title="ผู้ใช้งาน-LAW9102">
                        <asp:DataList ID="DataListModule" runat="server" CellPadding="0"
                        ItemStyle-BorderStyle="Solid" OnSelectedIndexChanged="DataListStatus_SelectedIndexChanged"
                        RepeatColumns="5" RepeatDirection="Horizontal" Style="width: 100%" 
                            ForeColor="#333333">
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <SelectedItemStyle BackColor="#D1DDF1" ForeColor="#333333" Font-Bold="True" />
                        <ItemTemplate>
                        <asp:LinkButton ID="B1" runat="server" CommandName="select" Font-Bold="true" Font-Names="Tahoma"
                        Font-Size="10pt" ForeColor="Navy" Text='<%#Eval("module_name")%>'></asp:LinkButton>
                        </ItemTemplate>
                        <AlternatingItemStyle BackColor="White" />
                        <ItemStyle BorderStyle="Solid" BackColor="#EFF3FB" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        </asp:DataList>
                      
                        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
                            AllowSorting="True" AutoGenerateColumns="False" BackColor="White" 
                            BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="1" 
                            CssClass="GridViewStyle" GridLines="Vertical" PageSize="20" Width="100%">
                            <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                            <Columns>
                             <asp:TemplateField HeaderText="Module" SortExpression="module_name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMd" runat="server" Text='<%# Bind("module_name") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="30%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Menu" SortExpression="menu_name">
                                    <ItemTemplate>
                                        <asp:Label ID="LabelG1" runat="server" Text='<%# Bind("menu_name") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="65%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="เลือก">
                                    <ItemTemplate>

                                    <asp:LinkButton ID="LinkAlert" CommandName ="Select" runat="server" Text='<%#ImagesGet(Eval("chk")) %>' ToolTip="เปิด/ปิด">
                                    </asp:LinkButton>

                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
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

