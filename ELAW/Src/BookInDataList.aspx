<%@ Page Title="ทะเบียนหนังสือรับ-LAW5205" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="BookInDataList.aspx.vb" Inherits="Src_BookInDataList" %>

<%@ Register src="../UserControl/DatePicker.ascx" tagname="DatePicker" tagprefix="uc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   
<script type="text/javascript">
function popupwindown(url) {
window.open(url, "_new", "location=no,status=no,resizable=yes,width=750,height=700,scrollbars=yes,menubar=no");
}
</script>

<script type="text/javascript">
function winopen(theURL,winName,width,height,scollbar) { 
var setfocus;
setfocus = window.open(theURL,winName,'resizable=yes,scrollbars='+ scollbar +',width='+ width +',height='+ height +',top=300,left=300');
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
            <table class="form" border="0" cellpadding="1" cellspacing="1" 
                id="lblbookin_id">
                <tr>
                    <td class="HeaderGreen" colspan="2">
                        <asp:LinkButton ID="linkHome" runat="server">Home</asp:LinkButton>
                        &nbsp;&gt;
                        <asp:LinkButton ID="link1" runat="server">ทะเบียนหนังสือรับ</asp:LinkButton>
                        &nbsp;&gt; 
                        <asp:LinkButton ID="link2" runat="server">ข้อมูลหนังสือรับ</asp:LinkButton>
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
                        หน่วยงาน</td>
                    <td>
                        <asp:DropDownList ID="ddlDiv" runat="server" CssClass="ssddl" Height="20px" 
                            Width="300px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="sslbl_right">
                        วันที่รับเรื่อง</td>
                    <td>
                        <uc1:DatePicker ID="txtDateFrom" runat="server" IsNotNull ="false"  />
                        &nbsp;ถึง
                        <uc1:DatePicker ID="txtDateTo" runat="server" IsNotNull ="false" />
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
                        เจ้าของเรื่อง</td>
                    <td>
                        <asp:TextBox ID="txtLawyer" runat="server" CssClass="ssddl" Width="300px"></asp:TextBox>
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
                        ที่มา</td>
                    <td>
                        <asp:TextBox ID="txtFrom" runat="server" CssClass="ssddl" Width="600px"></asp:TextBox>
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
                    <td class="sslbl_right">
                        &nbsp;</td>
                    <td>
                        <asp:Button ID="bSearch" runat="server" CssClass="ssbtn" Text="ค้นหา" />
                        &nbsp;</td>
                </tr>
            </table>
            <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
                AllowSorting="True" AutoGenerateColumns="False" BackColor="White" 
                BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="1" 
                CssClass="GridViewStyle" GridLines="Vertical" PageSize="20" Width="100%">
                <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                <Columns>
                    <asp:BoundField DataField="runno" HeaderText="ลำดับ" 
                        SortExpression="runno" ItemStyle-Width="2%" >
                    <ItemStyle Width="2%" />
                    </asp:BoundField>
                    <asp:TemplateField ShowHeader="False" Visible="False" ItemStyle-Width="10%">
                        <ItemTemplate>
                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("bookin_id") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="10%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="เลขที่หนังสือ" SortExpression="bookin_no" ItemStyle-Width="10%">
                        <ItemTemplate>
                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("bookin_no") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="10%" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="topic" HeaderText="เรื่อง" SortExpression="topic" 
                        ItemStyle-Width="30%" >
                    <ItemStyle Width="30%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="bookkind_name" HeaderText="ประเภทหนังสือ" 
                        SortExpression="bookkind_name" ItemStyle-Width="10%" >
                    <ItemStyle Width="10%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="status_name" HeaderText="สถานะหนังสือ" 
                        SortExpression="status_name" ItemStyle-Width="15%" >
                    <ItemStyle Width="15%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="recieve_date" 
                        DataFormatString="{0:dd/MM/yyyy HH:mm}" HeaderText="วันที่รับเรือง" 
                        SortExpression="recieve_date" ItemStyle-Width="12%" >
                    <ItemStyle Width="12%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="from_name" HeaderText="ที่มา" 
                        SortExpression="from_name" ItemStyle-Width="10%" >
                     <ItemStyle Width="10%" />
                    </asp:BoundField>
                     <asp:BoundField DataField="createname" HeaderText="เจ้าของเรื่อง" 
                        SortExpression="createname" ItemStyle-Width="10%" >
                    <ItemStyle Width="10%" />
                    </asp:BoundField>
                    <asp:CommandField SelectText="เลือก" ShowSelectButton="True">
                    <ItemStyle ForeColor="Blue" />
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

