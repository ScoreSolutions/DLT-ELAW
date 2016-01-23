<%@ Page Title="ข้อมูลเชื่อมโยงกฎหมาย" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="LinkLawList.aspx.vb" Inherits="Src_LinkLawList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table class="form" border="0" cellpadding="1" cellspacing="1">
                <tr>
                    <td class="HeaderGreen" colspan="2">
                        ข้อมูลเชื่อมโยงกฎหมาย</td>
                </tr>
                <tr>
                    <td class="sslbl_bold">
                        เรื่อง</td>
                    <td>
                        <asp:TextBox ID="txtTitle" runat="server" CssClass="ssddl" Width="600px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="sslbl_bold">
                        รายละเอียด</td>
                    <td>
                        <asp:TextBox ID="txtDetail" runat="server" CssClass="ssddl" Width="600px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="sslbl_bold">
                        &nbsp;</td>
                    <td>
                        <asp:Button ID="bSearch" runat="server" CssClass="ssbtn" Text="ค้นหา" />
                    </td>
                </tr>
                <tr>
                    <td class="GridViewStyle" colspan="2">
                        <ContentTemplate>
                            <asp:GridView ID="GridView1" runat="server" AllowSorting="True" 
                    AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" 
                    BorderStyle="None" BorderWidth="1px" CellPadding="1" 
                    CssClass="GridViewStyle" GridLines="Vertical" PageSize="20" AllowPaging="True" 
                            Width="100%">
                                <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                                <Columns>
                                    <asp:BoundField DataField="title" HeaderText="เรื่อง" 
                                    SortExpression="title">
                                        <ItemStyle Width="50%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="detail" HeaderText="รายละเอียด" 
                                    SortExpression="detail">
                                        <ItemStyle Width="50%" />
                                    </asp:BoundField>
                                   <asp:TemplateField ShowHeader="False">
                        <ItemStyle Width="15px" />
                        <ItemTemplate>
                            <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" 
                                CommandName="Delete" ImageUrl="~/Images/DeleteFolderHS.png" Text="Delete" 
                                ToolTip="ลบ" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="False">
                        <EditItemTemplate>
                            <asp:ImageButton ID="ImageButton3" runat="server" CausesValidation="True" 
                                CommandName="Update" ImageUrl="~/Image/save.png" Text="Update" />
                            <asp:ImageButton ID="ImageButton2" runat="server" CausesValidation="False" 
                                CommandName="Cancel" ImageUrl="~/Images/cancel2.bmp" Text="Cancel" />
                        </EditItemTemplate>
                        <ItemStyle Width="15px" />
                        <ItemTemplate>
                            <asp:ImageButton ID="ImageButton4" runat="server" CausesValidation="False" 
                                CommandName="Edit" ImageUrl="~/Images/Edit.gif" Text="Edit" ToolTip="แก้ไข" />
                        </ItemTemplate>
                    </asp:TemplateField>
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
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

