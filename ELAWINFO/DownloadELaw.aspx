<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPageProfile.master" AutoEventWireup="false" CodeFile="DownloadELaw.aspx.vb" Inherits="DownloadELaw" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <center>
    <div>
     <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="conditional">
            <ContentTemplate>    
    <table style="width:100%;">
            <tr>
                <td><h1>บทความ</h1></td>
            </tr>
            </table> 
        <asp:Panel ID="PanelCreate" runat="server">
        <table style="width:100%;">
            <tr>
                <td  width = "20%" align = "center">
                    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
                        AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" 
                        CssClass="GridViewStyle" DataKeyNames="file_id" ForeColor="#333333" 
                        GridLines="None" Width="100%">
                        <EditRowStyle BackColor="#2461BF" />
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:TemplateField HeaderText="เอกสาร">
                                <ItemTemplate>
                                    <b>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("name") %>'></asp:Label>
                                    </b>
                                    <br />
                                    <asp:HiddenField ID="HF" runat="server" Value='<%# Bind("file_id") %>' />
                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("Detail") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Width="80%" />
                            </asp:TemplateField>
                            <%--<asp:TemplateField HeaderText="ผู้อัพโหลด">
                                <ItemTemplate>
                                    <asp:Label ID="lblcreation_by" runat="server" Text='<%# Bind("creation_by") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="20%" />
                            </asp:TemplateField>--%>
                            <asp:TemplateField HeaderText="เอกสาร">
                                <ItemTemplate>
                                    <asp:Label ID="lblLink" runat="server" Text=""></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="20%" />
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
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
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

