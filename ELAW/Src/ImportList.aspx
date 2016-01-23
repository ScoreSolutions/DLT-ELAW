<%@ Page Title="ตารางนำเข้าเอกสาร-LAW1201" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="ImportList.aspx.vb" Inherits="Src_ImportList" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table border="0" cellpadding="1" cellspacing="1" class="form">
                <tr>
                    <td class="HeaderGreen" colspan="2">
                        <asp:LinkButton ID="linkHome" runat="server">Home</asp:LinkButton>
                        &nbsp;&gt;
                        <asp:LinkButton ID="link1" runat="server">บริการข้อมูลกฎหมาย</asp:LinkButton>
                        &nbsp;&gt;
                        <asp:LinkButton ID="link2" runat="server">ตารางนำเข้าเอกสาร</asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td class="sslbl_right">
                        ประเภทเอกสาร</td>
                    <td>
                        <asp:DropDownList ID="DDType" runat="server" CssClass="ssddl" Width="450px" 
                AutoPostBack="True">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="sslbl_right">
                        ชนิดเอกสาร</td>
                    <td>
                        <asp:DropDownList ID="DDLawType" runat="server" CssClass="ssddl" Width="450px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="sslbl_right">
                        รหัสเอกสาร</td>
                    <td>
                        <asp:TextBox ID="txtDocId" runat="server" 
                            CssClass="ssddl" MaxLength="15" 
                                                Width="100px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="sslbl_right">
                        คำค้นหา</td>
                    <td>
                        <asp:TextBox ID="txtDocName" runat="server" CssClass="ssddl" Width="600px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="sslbl">&nbsp;
                        </td>
                    <td>
                        <asp:Button ID="bSearch" runat="server" CssClass="ssbtn" Text="ค้นหา"
                    Width="80px" />
                    </td>
                </tr>
            </table>
          
            <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
                AllowSorting="True" AutoGenerateColumns="False" BackColor="White" 
                BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="1" 
                CssClass="GridViewStyle" GridLines="Vertical" Width="100%">
                <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                <Columns>
                    <asp:BoundField DataField="doc_id" HeaderText="รหัสเอกสาร">
                    <ItemStyle Width="12%" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="ประเภท">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("doc_type") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label3" runat="server" Text='<%# Bind("doc_type") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="12%" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="doc_typename" HeaderText="ชนิดเอกสาร">
                    <ItemStyle Width="18%" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="ชื่อเอกสาร">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("doc_name") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("doc_name") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="45%" />
                    </asp:TemplateField>
                  
                    <asp:TemplateField ShowHeader="False" HeaderText="ลบ">
                        <ItemStyle Width="25px" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" 
                                CommandName="Delete" ImageUrl="~/Images/DeleteFolderHS.png" Text="Delete" 
                                ToolTip="ลบ" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="False" HeaderText="แก้ไข">
                   
                        <ItemStyle Width="35px" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:ImageButton ID="ImageButton4" runat="server" CausesValidation="False" 
                                CommandName="Edit" ImageUrl="~/Images/Edit.gif" Text="Edit" ToolTip="แก้ไข" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="False" Visible ="false">
                        <ItemStyle Width="25px" />
                        <ItemTemplate>
                            <asp:ImageButton ID="ImageButton5" runat="server" CausesValidation="False" 
                                CommandName="Select" ImageUrl="~/Images/upload1.jpg" Text="Delete" 
                                ToolTip="เลือก" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                 <EmptyDataTemplate>
                    <table width ="100%">
                        <tr>
                            <td align="center">
                                <asp:Label ID="Label8" runat="server" CssClass="sslbl_red" 
                                    Text="***ไม่พบข้อมูล***"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                <AlternatingRowStyle BackColor="#DCDCDC" />
            </asp:GridView>
            </ContentTemplate>
    </asp:UpdatePanel>
 
</asp:Content>

