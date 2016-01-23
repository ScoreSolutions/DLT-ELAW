<%@ Page Title="เชื่อมโยงข้อมูลกฎหมาย" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="LinkDataLaw.aspx.vb" Inherits="Src_LinkDataLaw" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style8
        {
            font-family: "MS Sans Serif", "Microsoft Sans Serif", AngsanaUPC;
            font-size: 14px;
            color: #ffffff;
            font-weight : normal;
            text-decoration: none;
            vertical-align: top;
            padding: 1px;
            text-align : right;
            width: 6%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <table class="style5">
                <tr>
                    <td class="HeaderGreen" colspan="2">
                        เชื่อมโยงข้อมูลกฎหมาย</td>
                </tr>
                <tr>
                    <td class="style8">
                        รหัส</td>
                    <td>
                        <asp:TextBox ID="txtId" runat="server" Text='<%# BindField("link_id") %>' 
                            CssClass="ssddl" Width="80px" ReadOnly="True"></asp:TextBox>
                        &nbsp;<asp:CheckBox ID="chkSecret" runat="server" ForeColor="White" Text="ความลับ" 
                            Visible="False" />
                    </td>
                </tr>
                <tr>
                    <td class="style8">
                        เรื่อง</td>
                    <td>
                        <asp:TextBox ID="txtTitle" runat="server" Text='<%# BindField("title") %>' CssClass="ssddl" Width="595px"></asp:TextBox>
                        <asp:Label ID="lblAtitle" runat="server" CssClass="sslbl_red"></asp:Label>
                        <asp:Label ID="lblMainStatus" runat="server" Visible="False"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style8">
                        รายละเอียด</td>
                    <td>
                        <asp:TextBox ID="txtDetail" runat="server" Text='<%# BindField("detail") %>' CssClass="ssddl" Rows="4" 
                             TextMode="MultiLine" Width="600px"></asp:TextBox>
                        <asp:Label ID="lblADetail" runat="server" CssClass="sslbl_red"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style8">&nbsp;
                        </td>
                    <td>
                        <asp:Button ID="bSave" runat="server" CssClass="ssbtn" Text="บันทึก" 
                            Width="80px" />
                        <asp:Button ID="bSelect" runat="server" CssClass="ssbtn" Text="เลือกข้อมูลกฎหมาย" 
                            Width="144px" />
                        <asp:Button ID="bDel" runat="server" CssClass="ssbtn" Text="ลบ" Width="80px" />
                        <asp:Label ID="lblBstate" runat="server" CssClass="sslbl_red"></asp:Label>
                    </td>
                </tr>
            </table>
            
            <table class="style5" width="45%">
                <tr>
                    <td>
                        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
                            AllowSorting="True" AutoGenerateColumns="False" BackColor="White" 
                            BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="1" 
                            CssClass="GridViewStyle" GridLines="Vertical" PageSize="20" Width="100%">
                            <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                            <Columns>
                               <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:CheckBox ID="cb1" runat="server" AutoPostBack="true"  
                                        OnCheckedChanged="cb1_Checked" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    &nbsp;<asp:CheckBox ID="cb1" runat="server" />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle Width="1%" />
                            </asp:TemplateField>
                                <asp:TemplateField HeaderText="ชื่อเอกสาร" SortExpression="keyword">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txttype" runat="server" Text='<%# Bind("subtype_name") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lbltype" runat="server" Text='<%# Bind("subtype_name") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="30%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ชื่อเอกสาร" SortExpression="keyword">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("doc_name") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("doc_name") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="65%" />
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
</asp:Content>

