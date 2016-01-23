<%@ Page Title="แก้ไขหนังสือ-LAW6106" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="BookOutEditList.aspx.vb" Inherits="Src_BookOutEditList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table class="form" border="0" cellpadding="1" cellspacing="1">
                <tr>
                    <td class="HeaderGreen" colspan="2" title="ทะเบียนหนังสือนำส่ง">
                        <asp:LinkButton ID="linkHome" runat="server">Home</asp:LinkButton>
                                    &nbsp;&gt;
                                    <asp:LinkButton ID="link1" runat="server">ทะเบียนหนังสือนำส่ง</asp:LinkButton>
                                    &nbsp;&gt;
                                    <asp:LinkButton ID="link2" runat="server">แก้ไขหนังสือ</asp:LinkButton>
                        </td>
                </tr>
                <tr>
                    <td class="sslbl_right">
                        ประเภทหนังสือ</td>
                    <td>
                        <asp:DropDownList ID="ddlType" runat="server" 
                    Height="20px" Width="300px" CssClass="ssddl">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="sslbl_right">
                        สถานะหนังสือ</td>
                    <td>
                        <asp:DropDownList ID="ddlStatus" runat="server" 
                    Height="20px" Width="300px" CssClass="ssddl">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="sslbl_right">
                        เลขที่หนังสือ</td>
                    <td>
                        <asp:TextBox ID="txtBookNo" runat="server" CssClass="ssddl" MaxLength="15"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="sslbl_right">
                        เรื่อง</td>
                    <td>
                        <asp:TextBox ID="txtTopic" runat="server" CssClass="ssddl" Width="600px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="sslbl_right">
                        คำค้นหา</td>
                    <td>
                        <asp:TextBox ID="txtKeyword" runat="server" CssClass="ssddl" Width="600px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="sslbl">
                        &nbsp;</td>
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
                    <asp:BoundField DataField="bookout_no" HeaderText="เลขที่หนังสือ" 
                        SortExpression="bookout_no">
                    <ItemStyle Width="10%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="topic" HeaderText="เรื่อง" SortExpression="topic">
                    <ItemStyle Width="40%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="bookkind_name" HeaderText="ประเภทหนังสือ" 
                        SortExpression="bookkind_name">
                    <ItemStyle Width="20%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="status_name" HeaderText="สถานะหนังสือ" 
                        SortExpression="status_name">
                    <ItemStyle Width="20%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="dates" DataFormatString="{0:dd/MM/yyyy}" 
                        HeaderText="วันที่" SortExpression="dates">
                    <ItemStyle Width="10%" />
                    </asp:BoundField>
                    <asp:CommandField EditText="เลือก" ShowEditButton="True">
                    <ItemStyle Font-Bold="True" ForeColor="#000099" HorizontalAlign="Center" />
                    </asp:CommandField>
                    
                    <asp:TemplateField ShowHeader="False">
                    <ItemStyle Width="15px" />
                    <ItemTemplate >
                        <asp:LinkButton ID="lnkDel" runat="server" ForeColor ="red" Font-Bold ="true"  CommandName="Delete">ลบ</asp:LinkButton>
                    </ItemTemplate>
                    </asp:TemplateField> 
                    
                </Columns>
                <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
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
                <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                <AlternatingRowStyle BackColor="#DCDCDC" />
            </asp:GridView>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

