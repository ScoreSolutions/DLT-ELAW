﻿<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="Attorney.aspx.vb" Inherits="Src_Attorney" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table class="form" border="0" cellpadding="1" 
        cellspacing="1">
                <tr>
                    <td class="HeaderGreen" colspan="2">
                        ข้อมูลอัยการ</td>
                </tr>
                <tr>
                    <td class="style10">
                        ชื่อศาล</td>
                    <td class="sslbl">
                        <asp:DropDownList ID="ddlCourt" runat="server" 
                            CssClass="ssddl" Height="20px" Width="200px" AutoPostBack="True">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="style10">
                        ชื่ออัยการ</td>
                    <td class="sslbl">
                        <asp:TextBox ID="txtStatus" runat="server" CssClass="ssddl" MaxLength="250" 
                            Width="500px"></asp:TextBox>
                        <asp:Label ID="lblStatus" runat="server" Visible="False"></asp:Label>
                        <asp:Label ID="lblId" runat="server" Visible="False"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style10">
                        เบอร์โทรศัพท์&nbsp;</td>
                    <td class="sslbl">
                        <asp:TextBox ID="txtStatus0" runat="server" CssClass="ssddl" MaxLength="250" 
                            Width="500px"></asp:TextBox>
                            
                    </td>
                </tr>
                <tr>
                    <td class="style10">
                        &nbsp;</td>
                    <td class="sslbl">
                        <asp:Button ID="bSave" runat="server" CssClass="ssbtn" Text="บันทึก" 
                            Width="80px" />
                        <asp:Button ID="bCancel" runat="server" CssClass="ssbtn" Text="ยกเลิก" 
                            Width="80px" />
                        <asp:Button ID="bSearch" runat="server" CssClass="ssbtn" Text="ค้นหา" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" width="100%">
                        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
                            AllowSorting="True" AutoGenerateColumns="False" BackColor="White" 
                            BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="1" 
                            CssClass="GridViewStyle" GridLines="Vertical" PageSize="20" Width="100%">
                            <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                            <Columns>
                                <asp:TemplateField Visible="False">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("court_id") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("court_id") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="10%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ชื่อศาล" SortExpression="court_name">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("court_name") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("court_name") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="20%" />
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="ชื่ออัยการ" SortExpression="attorney_name">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("attorney_name") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("attorney_name") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="20%" />
                                </asp:TemplateField>
                                
                                 <asp:TemplateField HeaderText="เบอร์โทรศัพท์" SortExpression="attorney_name">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextTel" runat="server" Text='<%# Bind("tel") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="LabelTel" runat="server" Text='<%# Bind("tel") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="20%" />
                                </asp:TemplateField>
                                
                                
                                <asp:BoundField DataField="creation_by" HeaderText="ชื่อผู้สร้าง" 
                                    SortExpression="creation_by">
                                    <ItemStyle Width="10%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="created_date" DataFormatString="{0:dd/MM/yyyy}" 
                                    HeaderText="วันที่สร้าง" HtmlEncode="False" SortExpression="created_date">
                                    <ItemStyle Width="10%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="updated_by" HeaderText="ชื่อผู้แก้ไข" 
                                    SortExpression="updated_by">
                                    <ItemStyle Width="10%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="updated_date" DataFormatString="{0:dd/MM/yyyy}" 
                                    HeaderText="วันที่แก้ไข" SortExpression="updated_date">
                                    <ItemStyle Width="20%" />
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
                                            CommandName="Edit" ImageUrl="~/Images/Edit.gif" Text="Edit" 
                                            ToolTip="แก้ไข" />
                                    </ItemTemplate>
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
