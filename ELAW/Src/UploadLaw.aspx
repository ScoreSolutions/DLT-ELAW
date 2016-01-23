<%@ Page Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="UploadLaw.aspx.vb" Inherits="Src_UploadLaw" title="อัพโหลดกฎหมายใหม่" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <center>
        <div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="conditional">
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnCreateELAW" />
                </Triggers>
                <ContentTemplate>
                    <table style="width:100%;">
                        <tr>
                            <td class="HeaderGreen">
                                <b>อัพโหลดกฎหมายใหม่</b></td>
                        </tr>
                    </table>
                    <asp:Panel ID="PanelCreate" runat="server">
                        <table style="width:100%;">
                            <tr>
                                <td align="left" width = "90%" colspan="2">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td  width = "20%" align = "right">
                                    ประเภท :</td>
                                <td align ="left">
                                    <asp:DropDownList ID="ddlLawType" runat="server" CssClass="ssddl" Height="20px" 
                                        Width="200px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" width="20%">
                                    ชื่อกฎหมาย :
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtELAW" runat="server" Width="580px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" width = "10%" valign="top" >
                                    รายละเอียด :</td>
                                <td align="left">
                                    <asp:TextBox ID="txtDetail" runat="server" Width="580px" 
                        TextMode="MultiLine" Height="61px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" width = "10%">
                                    ไฟล์ :</td>
                                <td align="left">
                                    <asp:FileUpload ID="FileUpload" runat="server" Width="580px" />
                                </td>
                            </tr>
                            <tr>
                                <td align="right" width = "10%">
                                    <asp:Label ID="lblID" runat="server" Visible="False"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Button ID="btnCreateELAW" runat="server" 
                        Text="บันทึกข้อมูล" Width="120px" />
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2" width = "10%">
                                    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" DataKeyNames="file_id"
                                        AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" 
                                        CssClass="GridViewStyle" GridLines="None" Width="100%" 
                        ForeColor="#333333">
                                        <EditRowStyle BackColor="#2461BF" />
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                        
                                            <asp:BoundField DataField="wname" HeaderText="ประเภท" />
                                        
                                            <asp:TemplateField HeaderText="ชื่อกฎหมาย">
                                                <ItemTemplate>
                                                    <b>
                                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("name") %>'></asp:Label>
                                                    </b>
                                                    <br />
                                                    <asp:HiddenField ID="HF" runat="server" Value='<%# Bind("file_id") %>' />
                                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("Detail") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="75%" HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ดาวน์โหลด">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblLink" runat="server" Text=""></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="15%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField ShowHeader="False">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" 
                                                        CommandName="Delete" ImageUrl="~/Images/DeleteFolderHS.png" Text="Delete" 
                                                        ToolTip="ลบ" />
                                                </ItemTemplate>
                                                <ItemStyle Width="15px" />
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
                                        <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle BackColor="#EFF3FB" />
                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </center>
</asp:Content>

