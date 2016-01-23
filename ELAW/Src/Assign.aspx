<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="Assign.aspx.vb" Inherits="Src_Assign" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">

        




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
    <table class="form" border="1" cellpadding="0" 
        cellspacing="0" __designer:mapid="274">
        <tr __designer:mapid="275">
            <td class="HeaderGreen" title="ผู้ใช้งาน-LAW9102" __designer:mapid="276">
                มอบหมายงาน</td>
        </tr>
        <tr __designer:mapid="278">
            <td class="style9" title="มอบหมายงาน-LAW9102" __designer:mapid="279">
                <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
                            AllowSorting="True" AutoGenerateColumns="False" BackColor="White" 
                            BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="1" 
                            CssClass="GridViewStyle" GridLines="Vertical" PageSize="20" 
                    Width="100%">
                    <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                    <Columns>
                        <asp:TemplateField HeaderText="ชื่อ-สกุล" SortExpression="fullname">
                            <ItemTemplate>
                                <asp:Label ID="lblMd" runat="server" Text='<%# Bind("fullname") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="30%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ส่วนงาน" SortExpression="div_name">
                            <ItemTemplate>
                                <asp:Label ID="LabelG1" runat="server" Text='<%# Bind("div_name") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="65%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="เลือก">
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkAlert" CommandName ="Select" runat="server" 
                                            Text='<%#ImagesGet(Eval("chk")) %>' ToolTip="เปิด/ปิด">
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
</asp:Content>

