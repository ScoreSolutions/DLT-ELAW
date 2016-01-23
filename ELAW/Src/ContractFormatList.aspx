<%@ Page Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="ContractFormatList.aspx.vb" Inherits="Src_ContractFormatList" title="รูปแบบสัญญา-LAW4208" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table class="form" border="0" cellpadding="1" cellspacing="1" width="100%">
                <tr>
                    <td class="HeaderGreen" colspan="2">
                        <asp:LinkButton ID="linkHome" runat="server">Home</asp:LinkButton>
                        &nbsp;&gt;
                        <asp:LinkButton ID="link1" runat="server">งานสัญญา</asp:LinkButton>
                        &nbsp;&gt;
                        <asp:LinkButton ID="link2" runat="server">รูปแบบสัญญา</asp:LinkButton>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style7">
                        ประเภทสัญญา&nbsp;</td>
                    <td width="90%">
                        <asp:DropDownList ID="ddlContract" runat="server" AutoPostBack="True" 
                    Height="20px" Width="200px" CssClass="ssddl">
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
            <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
                AllowSorting="True" AutoGenerateColumns="False" BackColor="White" 
                BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="1" 
                CssClass="GridViewStyle" GridLines="Vertical" PageSize="20" Width="100%">
                <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                <Columns>
                    <asp:BoundField DataField="subtype_name" HeaderText="ประเภทสัญญา">
                    <ItemStyle Width="40%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="type_name" HeaderText="ประเภทย่อยสัญญา">
                    <ItemStyle Width="50%" />
                    </asp:BoundField>
                    <asp:CommandField EditText="เลือก" ShowEditButton="True">
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
        </ContentTemplate>
    </asp:UpdatePanel>
                    </asp:Content>

