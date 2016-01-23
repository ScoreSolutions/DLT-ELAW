<%@ Page Title="" Language="VB" MasterPageFile="MasterPageProfile.master" AutoEventWireup="false" CodeFile="SearchLawData.aspx.vb" Inherits="SearchLawData" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <script type="text/javascript">

    function openwindow(Page, Id) {
    window.open("" + Page + ".aspx?id=" + Id, "_new", "location=no,status=no,resizable=yes,width=700,height=700,scrollbars=yes,menubar=no");
    }

    </script>
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table class="" width="100%">
        <tr>
            <td width="" colspan="5" class ="subtitle" align="center">
                    ค้นหาข้อมูลกฎหมาย</td>
        </tr>
        <tr>
            <td width="">
                    &nbsp;</td>
            <td colspan="4" align="left" width="" valign="top">
                    &nbsp;</td>
        </tr>
        <tr>
            <td width="">
                    คำค้นหา</td>
            <td colspan="4" align="left" width="" valign="top">
                    <asp:TextBox ID="txtKeyword" runat="server" CssClass="ssddl" Width="400px"></asp:TextBox>
                    <asp:Button ID="bSearch" runat="server" CssClass="ssbtn" Text="เริ่มการค้นหา" 
                        Width="80px" />
                    <asp:Button ID="bClear" runat="server" CssClass="ssbtn" Text="ล้างการค้นหา" 
                        Width="80px" />
                </td>
        </tr>
        <tr>
            <td width="40%" valign ="top" >
                    ประเภท</td>
            <td width="20%" align="left" valign="top">
                    <asp:GridView ID="grdLaw" runat="server" AutoGenerateColumns="False" 
                        CellPadding="0" GridLines="None" ShowHeader="False" Width="200px">
                        <Columns>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:CheckBox ID="cb1" runat="server" AutoPostBack="true" 
                                         />
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
                    </asp:GridView>
                </td>
            <td width="20%" align="left" valign="top">
                    <asp:GridView ID="grdContract" runat="server" AutoGenerateColumns="False" 
                        CellPadding="0" GridLines="None" ShowHeader="False" Width="200px">
                        <Columns>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:CheckBox ID="cb2" runat="server" AutoPostBack="true" 
                                         />
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
                    </asp:GridView>
                </td>
            <td width="20%" align="left" valign="top">
                    <asp:GridView ID="grdCase" runat="server" AutoGenerateColumns="False" 
                        CellPadding="0" GridLines="None" ShowHeader="False" Width="200px">
                        <Columns>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:CheckBox ID="cb3" runat="server" AutoPostBack="true" 
                                         />
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
                    </asp:GridView>
                </td>
            <td>
                    &nbsp;</td>
        </tr>
        <tr>
            <td colspan="5">
                    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
                        AutoGenerateColumns="False" CellPadding="4" CssClass="GridViewStyle" 
                        GridLines="None" Width="100%" PageSize="20" ForeColor="#333333">
                        <RowStyle BackColor="#EFF3FB" />
                        <Columns>
                            <asp:TemplateField ShowHeader="False" Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("doc_id") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="subtype_name" HeaderText="ชนิดเอกสาร"   
                                SortExpression="subtype_name" HeaderStyle-ForeColor ="White">
<HeaderStyle ForeColor="White"></HeaderStyle>

                            <ItemStyle Width="25%" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="doc_name" HeaderText="ชื่อเอกสาร" 
                                SortExpression="doc_name" HeaderStyle-ForeColor ="White" >
<HeaderStyle ForeColor="White"></HeaderStyle>

                            <ItemStyle Width="72%" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:TemplateField ShowHeader="False">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LnkSelect" runat="server" CommandName="cSelect">เลือก</asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="3%" />
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
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
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" 
                            Height="30px" />
                        <EditRowStyle BackColor="#2461BF" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>
                </td>
        </tr>
        <tr>
            <td>
                    &nbsp;</td>
            <td width="25%">
                    &nbsp;</td>
            <td width="25%">
                    &nbsp;</td>
            <td width="28%">
                    &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
</asp:Content>

