<%@ Page Title="ประวัติการร่างกฎหมาย" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="DrafLawList.aspx.vb" Inherits="Src_DrafLawList" %>

<%@ Register src="../UserControl/ComboBox.ascx" tagname="ComboBox" tagprefix="uc1" %>
<%@ Register src="../UserControl/TextBox.ascx" tagname="TextBox" tagprefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table class="form" border="0" cellpadding="1" cellspacing="1">
                <tr>
                     <td class="HeaderGreen" colspan="2">
                        <asp:LinkButton ID="linkHome" runat="server">Home</asp:LinkButton>
                        &nbsp;&gt;
                        <asp:LinkButton ID="link1" runat="server">ติดตามงานกฎหมาย</asp:LinkButton>
                        &nbsp;&gt;
                        <asp:LinkButton ID="link2" runat="server">ประวัติการร่างกฎหมาย</asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td class="sslbl_right">ประเภทกฎหมาย</td>
                    <td>
                        <uc1:ComboBox ID="ddlLawType" CssClass="ssddl" AutoPosBack="true" Width="300" runat="server" IsNotNull="false" />
                    </td>
                </tr>
                <tr>
                    <td class="sslbl_right">ประเภทย่อย</td>
                    <td>
                        <uc1:ComboBox ID="ddlSubType" CssClass="ssddl" Width="300" runat="server" IsNotNull="false" />
                    </td>
                </tr>
                <tr>
                    <td class="sslbl_right">สถานะกฎหมาย</td>
                    <td>
                        <uc1:ComboBox ID="ddlStatus" CssClass="ssddl" Width="300" runat="server" IsNotNull="false" />
                    </td>
                </tr>
                <tr>
                    <td class="sslbl_right">ความเร่งด่วน</td>
                    <td>
                        <uc1:ComboBox ID="ddlLevel" CssClass="ssddl" Width="200" runat="server" IsNotNull="false" />
                    </td>
                </tr>
                <tr>
                    <td class="sslbl_right">เรื่อง</td>
                    <td>
                        <uc2:TextBox ID="txtTitle" runat="server" IsNotNull="false" TextKey="TextChar" Width="600" />
                    </td>
                </tr>
                <tr>
                    <td class="sslbl_right">คำค้นหา</td>
                    <td>
                        <uc2:TextBox ID="txtKeyword" runat="server" IsNotNull="false" TextKey="TextChar" Width="600" />
                    </td>
                </tr>
                <tr>
                    <td class="sslbl_right">&nbsp;</td>
                    <td>
                        <asp:Button ID="bSearch" runat="server" CssClass="ssbtn" Text="ค้นหา" />
                    </td>
                </tr>
            </table>
        
            <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
                AllowSorting="True" AutoGenerateColumns="False" BackColor="White" 
                BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="1" 
                CssClass="GridViewStyle" GridLines="Vertical" PageSize="20" Width="100%">
                <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                <Columns>
                    <asp:BoundField DataField="law_id" HeaderText="เลขที่" 
                        SortExpression="law_id" >
                    <ItemStyle Width="7%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="title" HeaderText="เรื่อง" SortExpression="title" >
                    <ItemStyle Width="27%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="type_name" HeaderText="ประเภทกฎหมาย" 
                        SortExpression="type_name" >
                    <ItemStyle Width="13%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="subtype_name" HeaderText="ประเภทย่อย" 
                        SortExpression="subtype_name" >
                    <ItemStyle Width="23%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="status_name" HeaderText="สถานะกฎหมาย" 
                        SortExpression="status_name" >
                    <ItemStyle Width="20%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="level_name" HeaderText="ความเร่งด่วน" 
                        SortExpression="level_name" >
                    <ItemStyle Width="10%" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="" SortExpression="">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkSelected" runat="server" 
                                CommandArgument='<%# Bind("law_id")  %>' CommandName="Selected" 
                                Font-Bold="True" ForeColor="#000099" Text="เลือก"></asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="30px" />
                        <HeaderStyle Width="30px" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="active" HeaderText="active">
                        <ControlStyle CssClass="zHidden" />
                        <FooterStyle CssClass="zHidden" />
                        <HeaderStyle CssClass="zHidden" />
                        <ItemStyle CssClass="zHidden" />
                    </asp:BoundField>
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
                <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                <AlternatingRowStyle BackColor="#DCDCDC" />
            </asp:GridView>
                   
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

