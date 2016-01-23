<%@ Page Title="บันทึกคำพิพากษา-LAW3110" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="CaseAttDecision.aspx.vb" Inherits="Src_CaseAttDecision" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <table border="0" cellpadding="1" cellspacing="1" class="form" width="100%">
                <tr>
                    <td class="HeaderGreenTab" colspan="2" width="15%">
                     <asp:LinkButton ID="linkHome" runat="server">Home</asp:LinkButton>
                    &nbsp;&gt;
                    <asp:LinkButton ID="link1" runat="server">งานคดี</asp:LinkButton>
                    &nbsp;&gt;
                    <asp:LinkButton ID="link2" runat="server">บันทึกคำพิพากษา</asp:LinkButton>
                        &nbsp;&gt;
                        <asp:LinkButton ID="link3" runat="server">บันทึก</asp:LinkButton>
                        </td>
                </tr>
                <tr>
                    <td class="sslbl_right" width="15%">
                        ไฟล์แนบ</td>
                    <td class="sslbl" width="85%">
                        <asp:FileUpload runat="server" CssClass="ssddl" Width="600px" ID="FileUpload1">
                        </asp:FileUpload>
                        <asp:Label runat="server" CssClass="sslbl_red" ID="lblAFile"></asp:Label>
                        <asp:Label runat="server" ID="lblDocStatus" Visible="False"></asp:Label>
                        <asp:Label runat="server" ID="lblDocId" Visible="False"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="sslbl_right" width="15%">
                        ชื่อเอกสาร</td>
                    <td class="sslbl">
                        <asp:TextBox runat="server" CssClass="ssddl" Width="600px" ID="txtDocDetail"></asp:TextBox>
                        <asp:Label runat="server" CssClass="sslbl_red" ID="lblADetail1"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="sslbl_right" width="15%">
                        จำนวนหน้า</td>
                    <td class="sslbl">
                        <asp:TextBox runat="server" MaxLength="6" CssClass="ssddl" Width="60px" 
                ID="txtDocPage"></asp:TextBox>
                        <asp:Label runat="server" CssClass="sslbl_red" ID="lblAPage"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="sslbl" width="100">
                        &nbsp;</td>
                    <td>
                        <asp:Button runat="server" Text="บันทึก" CssClass="ssbtn" Width="80px" 
                ID="bSaveFile"></asp:Button>
                        <asp:Button runat="server" Text="ยกเลิก" CssClass="ssbtn" Width="80px" 
                ID="bCancelFile"></asp:Button>
                    </td>
                </tr>
                <tr>
                    <td class="sslbl" colspan="2" width="100%">
                        <asp:GridView runat="server" AllowPaging="True" AllowSorting="True" 
                AutoGenerateColumns="False" CellPadding="1" GridLines="Vertical" 
                BackColor="White" BorderColor="#999999" BorderWidth="1px" BorderStyle="None" 
                CssClass="GridViewStyle" Width="100%" ID="GridView1">
                            <RowStyle BackColor="#EEEEEE" ForeColor="Black"></RowStyle>
                            <AlternatingRowStyle BackColor="Gainsboro"></AlternatingRowStyle>
                            <Columns>
                                <asp:TemplateField HeaderText="ชื่อเอกสาร">
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("title") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("title") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemStyle Width="70%"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="จำนวนหน้า" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("page") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("page") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="20%"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ดาวน์โหลด">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLink" runat="server" Text=""></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ลบ" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" 
                                                        CommandName="Delete" 
                                ImageUrl="~/Images/DeleteFolderHS.png" Text="Delete" 
                                                        ToolTip="ลบ" />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle Width="25px" HorizontalAlign="Center"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="แก้ไข" HeaderStyle-HorizontalAlign="Center" >
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImageButton4" runat="server" CausesValidation="False" 
                                                        CommandName="Edit" ImageUrl="~/Images/Edit.gif" 
                                Text="Edit" ToolTip="แก้ไข" />
                                    </ItemTemplate>
                                   
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle Width="35px" HorizontalAlign="Center"></ItemStyle>
                                </asp:TemplateField>
                            </Columns>
                           <EmptyDataTemplate>
                                        <table width="100%">
                                        <tr>
                                        <td align="center">
                                        <asp:Label ID="Label8" runat="server" CssClass="sslbl_red" 
                                        Text="***ไม่พบข้อมูล***"></asp:Label>
                                        </td>
                                        </tr>
                                        </table>
                             </EmptyDataTemplate>
                            <FooterStyle BackColor="#CCCCCC" ForeColor="Black"></FooterStyle>
                            <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White">
                            </HeaderStyle>
                            <PagerStyle HorizontalAlign="Center" BackColor="#999999" ForeColor="Black">
                            </PagerStyle>
                            <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White">
                            </SelectedRowStyle>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
                 <Triggers>
                <asp:PostBackTrigger ControlID="bSaveFile" />
                 </Triggers>
        
    </asp:UpdatePanel>
</asp:Content>

