<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="UserPermission.aspx.vb" Inherits="Src_UserPermission" %>

    
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table class="form" border="1" cellpadding="0" 
        cellspacing="0">
                <tr>
                    <td class="HeaderGreen" title="ผู้ใช้งาน-LAW9102">
                        กำหนดสิทธิ์ผู้ใช้งาน<asp:Label ID="lblTxt" runat="server"></asp:Label>
                        <asp:Label ID="lblId" runat="server" Visible="False"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style9" title="ผู้ใช้งาน-LAW9102">
                        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
                            AllowSorting="True" AutoGenerateColumns="False" BackColor="White" 
                            BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="1" 
                            CssClass="GridViewStyle" GridLines="Vertical" PageSize="20" Width="100%">
                            <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                            <Columns>
                                <asp:TemplateField HeaderText="รหัส" SortExpression="empid" Visible="False">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("empid") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("empid") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="10%" />
                                </asp:TemplateField>
                           
                                <asp:TemplateField HeaderText="ชื่อ" SortExpression="firstname">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextFirstname" runat="server" Text='<%# Bind("firstname") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="LabelFirstname" runat="server" Text='<%# Bind("firstname") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="20%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="นามสกุล" SortExpression="lastname">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextLastname" runat="server" Text='<%# Bind("lastname") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="LabelLastname" runat="server" Text='<%# Bind("lastname") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="20%" />
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="หน่วยงาน" SortExpression="dept_id">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextDept" runat="server" Text='<%# Bind("dept_name") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="LabelDept" runat="server" Text='<%# Bind("dept_name") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="20%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ส่วนงาน" SortExpression="div_id">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextDiv" runat="server" Text='<%# Bind("div_name") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="LabelDiv" runat="server" Text='<%# Bind("div_name") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="20%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ตำแหน่ง" SortExpression="pos_id">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextPos" runat="server" Text='<%# Bind("pos_name") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="LabelPos" runat="server" Text='<%# Bind("pos_name") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="20%" />
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

