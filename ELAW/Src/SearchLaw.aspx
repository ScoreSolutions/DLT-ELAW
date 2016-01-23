<%@ Page Title="ค้นหาข้อมูลกฎหมาย-LAW1204" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="SearchLaw.aspx.vb" Inherits="Src_SearchLaw" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <script type="text/javascript">

    function openwindow(Page, Id) {
        window.open("../Src/" + Page + ".aspx?id=" + Id, "_new", "location=no,status=no,resizable=yes,width=750,height=700,scrollbars=yes,menubar=no");
    }

</script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table class="form" border="0" cellpadding="1" cellspacing="1">
                <tr>
                    <td class="HeaderGreen" colspan="4">
                        <asp:LinkButton ID="linkHome" runat="server">Home</asp:LinkButton>
                        &nbsp;&gt;
                        <asp:LinkButton ID="link1" runat="server">บริการข้อมูลกฎหมาย</asp:LinkButton>
                        &nbsp;&gt;
                        <asp:LinkButton ID="link2" runat="server">ค้นหาข้อมูลกฎหมาย</asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td class="sslbl_right">
                        คำค้นหา (เท่าที่ทราบ)</td>
                    <td colspan="3">
                        <asp:TextBox ID="txtKeyword" runat="server" CssClass="ssddl" Width="600px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="sslbl_right">
                        ชื่อเอกสาร</td>
                    <td colspan="3">
                        <asp:TextBox ID="txtTitle" runat="server" CssClass="ssddl" Width="600px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="sslbl_right">
                        ประเภทการค้นหา</td>
                    <td class="sslbl">
                        <asp:Label ID="Label9" runat="server" CssClass="sslbl_bold" Text="ประเภทกฎหมาย"></asp:Label>
                        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
                            CellPadding="0" GridLines="None" ShowHeader="False" 
                            Width="200px">
                            <RowStyle BackColor="White" />
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
                                <asp:BoundField DataField="type_name" ShowHeader="False" />
                            </Columns>
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <EditRowStyle BackColor="#2461BF" />
                            <AlternatingRowStyle BackColor="White" />
                        </asp:GridView>
                        <asp:Button ID="bSearch" runat="server" CssClass="ssbtn" Text="เริ่มการค้นหา" 
                            Width="100px" />
                        <asp:Button ID="bClear" runat="server" CssClass="ssbtn" Text="ล้างการค้นหา" 
                            Width="100px" />
                    </td>
                    <td valign="top">
                        <asp:Label ID="Label10" runat="server" CssClass="sslbl_bold" Text="ประเภทสัญญา"></asp:Label>
                        <asp:GridView ID="gdvContract" runat="server" AutoGenerateColumns="False" 
                            CellPadding="0" GridLines="None" ShowHeader="False" Width="200px">
                            <RowStyle BackColor="White" />
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="cb2" runat="server" AutoPostBack="true" 
                                            OnCheckedChanged="cb2_Checked" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        &nbsp;<asp:CheckBox ID="cb2" runat="server" />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Width="1%" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="type_name" ShowHeader="False" />
                            </Columns>
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <EditRowStyle BackColor="#2461BF" />
                            <AlternatingRowStyle BackColor="White" />
                        </asp:GridView>
                    </td>
                    <td valign="top">
                        <asp:Label ID="Label11" runat="server" CssClass="sslbl_bold" Text="ประเภทคดี"></asp:Label>
                        <asp:GridView ID="gdvCase" runat="server" AutoGenerateColumns="False" 
                            CellPadding="0" GridLines="None" ShowHeader="False" Width="200px">
                            <RowStyle BackColor="White" />
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="cb3" runat="server" AutoPostBack="true" 
                                            OnCheckedChanged="cb3_Checked" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        &nbsp;<asp:CheckBox ID="cb3" runat="server" />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Width="1%" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="type_name" ShowHeader="False" />
                            </Columns>
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <EditRowStyle BackColor="#2461BF" />
                            <AlternatingRowStyle BackColor="White" />
                        </asp:GridView>
                    </td>
                </tr>
            </table>
            <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
                AllowSorting="True" AutoGenerateColumns="False" BackColor="White" 
                BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="1" 
                CssClass="GridViewStyle" GridLines="Vertical" PageSize="20" Width="100%">
                <RowStyle BackColor="White" ForeColor="Black" />
                <Columns>
                    <asp:TemplateField ShowHeader="False" Visible="False">
                        <ItemTemplate>
                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("doc_id") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="subtype_name" HeaderText="ชนิดเอกสาร" 
                        SortExpression="subtype_name">
                    <ItemStyle Width="25%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="doc_name" HeaderText="ชื่อเอกสาร" 
                        SortExpression="doc_name">
                    <ItemStyle Width="72%" />
                    </asp:BoundField>
                    <asp:TemplateField ShowHeader="False">
                        <ItemTemplate>
                            <asp:LinkButton ID="LnkSelect" runat="server" CommandName="cSelect">เลือก</asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="3%" />
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

