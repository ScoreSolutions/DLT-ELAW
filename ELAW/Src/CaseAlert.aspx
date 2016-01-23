<%@ Page Title="แจ้งเตือน-LAW3201" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="CaseAlert.aspx.vb" Inherits="Src_CaseAlert" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <script type="text/javascript">

    function openwindow(Page, Id) {
        window.open("../Src/" + Page + ".aspx?id=" + Id, "_new", "location=no,status=no,resizable=yes,width=750,height=700,scrollbars=yes,menubar=no");
    }

</script>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <table class="form" border="0" cellpadding="1" cellspacing="1">
                <tr>
                    <td class="HeaderGreen" colspan="2">
                        <asp:LinkButton ID="linkHome" runat="server">Home</asp:LinkButton>
                        &nbsp;&gt;
                        <asp:LinkButton ID="link1" runat="server">งานคดี</asp:LinkButton>
                        &nbsp;&gt;
                        <asp:LinkButton ID="link2" runat="server">แจ้งเตือน</asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td class="sslbl_right" width="15%">
                        ประเภทคดี&nbsp;</td>
                    <td width="85%">
                        <asp:DropDownList ID="ddlCase" runat="server" 
                    Height="20px" Width="300px" CssClass="ssddl" AutoPostBack="True">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="sslbl_right">
                        สถานะคดี</td>
                    <td>
                        <asp:DropDownList ID="ddlStatus" runat="server" 
                    Height="20px" Width="300px" CssClass="ssddl">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="sslbl_right">
                        หมายเลขคดีดำ</td>
                    <td>
                        <asp:TextBox ID="txtBlackNo" runat="server" CssClass="ssddl" MaxLength="10"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="sslbl_right">
                        หมายเลขคดีแดง</td>
                    <td>
                        <asp:TextBox ID="txtRedNo" runat="server" CssClass="ssddl" MaxLength="10"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="sslbl_right">
                        ชื่อโจทก์</td>
                    <td>
                        <asp:TextBox ID="txtProsecutor" runat="server" CssClass="ssddl" Width="600px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="sslbl_right">
                        จำเลย</td>
                    <td>
                        <asp:TextBox ID="txtDefendant" runat="server" CssClass="ssddl" Width="600px"></asp:TextBox>
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
                    <td class="style9">
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
                    <asp:TemplateField ShowHeader="False" Visible="False">
                        <ItemTemplate>
                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("case_no") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="black_no" HeaderText="หมายเลขคดีดำ" 
                        SortExpression="black_no" />
                    <asp:BoundField DataField="red_no" HeaderText="หมายเลขคดีแดง" 
                        SortExpression="red_no" />
                    <asp:BoundField DataField="status_name" HeaderText="สถานะคดี" 
                        SortExpression="status_name" />
                    <asp:BoundField DataField="type_name" HeaderText="ประเภทคดี" 
                        SortExpression="type_name" />
                     <asp:BoundField DataField="prosecutor" HeaderText="โจทก์" 
                        SortExpression="prosucutor" />   
                    <asp:BoundField DataField="defendant" HeaderText="จำเลย" 
                        SortExpression="defendant" />
                    
                    <asp:BoundField DataField="fullname" HeaderText="เจ้าของเรื่อง" 
                        SortExpression="fullname" />
                    <asp:TemplateField ShowHeader="False">
                        <ItemTemplate>
                            <asp:LinkButton ID="LnkSelect" runat="server" CommandName="cSelect">เลือก</asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="3%" />
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>
                    <table class="style5">
                        <tr>
                            <td align="center">
                                <asp:Label ID="Label8" runat="server" CssClass="GEmpty" Text="ไม่พบข้อมูล"></asp:Label>
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

