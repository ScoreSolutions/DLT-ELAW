<%@ Page Title="ยกเลิกหนังสือนำส่ง-LAW6410" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="BookOutCancel.aspx.vb" Inherits="Src_BookOutCancel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script type="text/javascript">
function popupwindown(url) {
window.open(url, "_new", "location=no,status=no,resizable=yes,width=750,height=700,scrollbars=yes,menubar=no");
}
</script>
<script type="text/javascript">
    function winopen(theURL, winName, width, height, scollbar) {
        var setfocus;
        setfocus = window.open(theURL, winName, 'resizable=yes,scrollbars=' + scollbar + ',width=' + width + ',height=' + height + ',top=300,left=300');
        setfocus.focus();
    }
</script>
<script type="text/javascript">

    function openwindow(Page, Id) {
        window.open("../Src/" + Page + ".aspx?id=" + Id, "_new", "location=no,status=no,resizable=yes,width=750,height=700,scrollbars=yes,menubar=no");
    }

</script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table class="form" border="0" cellpadding="1" cellspacing="1">
                <tr>
                    <td class="HeaderGreen" colspan="2" title="ทะเบียนหนังสือนำส่ง">
                        <asp:LinkButton ID="linkHome" runat="server">Home</asp:LinkButton>
                                    &nbsp;&gt;
                                    <asp:LinkButton ID="link1" runat="server">ทะเบียนหนังสือนำส่ง</asp:LinkButton>
                                    &nbsp;&gt;
                                    <asp:LinkButton ID="link2" runat="server">ยกเลิกหนังสือนำส่ง</asp:LinkButton>
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
                    <asp:TemplateField ShowHeader="False" Visible="False">
                        <ItemTemplate>
                            <asp:Label ID="lblbookoutid" runat="server" Text='<%# Bind("bookout_id") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="bookout_no" HeaderText="เลขที่หนังสือ" 
                        SortExpression="bookout_no">
                    <ItemStyle Width="10%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="topic" HeaderText="เรื่อง" SortExpression="topic">
                    <ItemStyle Width="28%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="bookkind_name" HeaderText="ประเภทหนังสือ" 
                        SortExpression="bookkind_name">
                    <ItemStyle Width="15%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="status_name" HeaderText="สถานะหนังสือ" 
                        SortExpression="status_name">
                    <ItemStyle Width="20%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="dates" DataFormatString="{0:dd/MM/yyyy}" 
                        HeaderText="วันที่" SortExpression="dates">
                    <ItemStyle Width="8%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="createname" HeaderText="เจ้าของเรื่อง" 
                        SortExpression="createname">
                    <ItemStyle Width="15%" />
                    </asp:BoundField>
                  
                    <asp:CommandField SelectText="ยกเลิก" ShowSelectButton="True" ItemStyle-Width="5%">
                    <ItemStyle ForeColor="Red" />
                    </asp:CommandField>
                  
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

