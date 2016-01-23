<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="SearchDocument.aspx.vb" Inherits="Src_SearchDocument" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">


    .style7
    {
        font-family: "MS Sans Serif", "Microsoft Sans Serif", AngsanaUPC;
        font-size: 14px;
        color: #FFFFFF;
        font-weight : bold;
        text-decoration: none;
        vertical-align: text-top;
        padding: 2px;
        width: 117px;
    }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table border="1" cellpadding="0" cellspacing="0" class="form">
        <tr>
            <td class="HeaderGreen" colspan="2">
                ค้นหาเอกสาร<cc1:toolkitscriptmanager ID="ToolkitScriptManager1" runat="server" 
                    enablescriptglobalization="True">
                </cc1:toolkitscriptmanager>
            </td>
        </tr>
        <tr>
            <td class="style7">
            ประเภทเอกสาร</td>
            <td>
                <asp:DropDownList ID="DDType" runat="server" CssClass="ssddl" Width="200px" 
                AutoPostBack="True">
                    <asp:ListItem Value="0">กฎหมาย</asp:ListItem>
                    <asp:ListItem Value="0">คดี</asp:ListItem>
                    <asp:ListItem Value="2">สัญญา</asp:ListItem>
                    <asp:ListItem Value="4">ข้อหารือ</asp:ListItem>
                </asp:DropDownList>
                <asp:DropDownList ID="DDLawType" runat="server" CssClass="ssddl" Width="200px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="style7">
                รหัสเอกสาร</td>
            <td>
                <asp:TextBox ID="txtDocId" runat="server" 
                            CssClass="ssddl" MaxLength="6" 
                                                Width="100px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="sslbl">
            ชื่อเอกสาร</td>
            <td>
                <asp:TextBox ID="TextBox1" runat="server" CssClass="ssddl" Width="600px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="sslbl">
                คำค้นหา</td>
            <td>
                <asp:TextBox ID="TextBox5" runat="server" CssClass="ssddl" Width="600px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="sslbl">
                &nbsp;</td>
            <td>
                <asp:Button ID="bSave" runat="server" CssClass="ssbtn" Text="ค้นหา" 
                    Width="80px" />
            </td>
        </tr>
        <tr>
            <td class="sslbl" colspan="2">
                <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
                                                AllowSorting="True" 
                AutoGenerateColumns="False" BackColor="White" 
                                                BorderColor="#999999" BorderStyle="None" 
                            BorderWidth="1px" CellPadding="1" 
                                                CssClass="GridViewStyle" GridLines="Vertical" 
                            PageSize="20" Width="100%">
                    <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                    <Columns>
                        <asp:BoundField DataField="suit_id" HeaderText="รหัสเอกสาร">
                        <ItemStyle Width="10%" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="ชื่อเอกสาร">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("creation_by") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("creation_by") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="70%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="หน้า(เอกสาร)">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("title") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("title") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" Width="30%" />
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

