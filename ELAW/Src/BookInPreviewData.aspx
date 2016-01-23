<%@ Page Title="รายละเอียดหนังสือรับ-LAW5406" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="BookInPreviewData.aspx.vb" Inherits="Src_BookInPreviewData" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript">

    function openwindow(Page, Id) {
    window.open("../Src/" + Page + ".aspx?id=" + Id, "_new", "location=no,status=no,resizable=yes,width=750,height=700,scrollbars=yes,menubar=no");
    }

    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table class="form" cellpadding="1" cellspacing="1" frame="border" 
        border="0" width="100%">
                <tr>
                    <td class="HeaderGreenTab">
                        <table class="form" width="100%">
                            <tr>
                                <td class="HeaderGreen">
                                    <asp:LinkButton ID="linkHome" runat="server">Home</asp:LinkButton>
                                    &nbsp;&gt;
                                    <asp:LinkButton ID="link1" runat="server">ทะเบียนหนังสือรับ</asp:LinkButton>
                                    &nbsp;&gt;
                                    <asp:LinkButton ID="link2" runat="server">ข้อมูลหนังสือรับ</asp:LinkButton>
                                     &nbsp;&gt;
                                     <asp:LinkButton ID="link3" runat="server">รายละเอียด</asp:LinkButton>
                                </td>
                                <td class="HeaderTab" align="right">
                                    &nbsp;</td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="sslbl">
                       
                                <table border="0" cellpadding="1" cellspacing="1" class="form" width="100%">
                                    <tr>
                                        <td class="sslbl_bold">
                                            ลำดับ</td>
                                        <td class="sslbl" width="35%">
                                            <asp:Label ID="lblRunNo" runat="server" Text='<%# BindField("runno") %>'></asp:Label>
                                        </td>
                                        <td class="sslbl_bold">
                                            &nbsp;</td>
                                        <td class="sslbl" width="35%" >
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="sslbl_bold">
                                            เลขที่หนังสือ</td>
                                        <td class="sslbl" width="35%">
                                            <asp:Label ID="lblBookNo" runat="server" Text='<%# BindField("bookin_no") %>'></asp:Label>
                                            <asp:Label ID="lblId" runat="server" Visible="False"></asp:Label>
                                            <asp:Label ID="lblIdNew" runat="server"></asp:Label>
                                        </td>
                                        <td class="sslbl_bold">
                                            สถานะหนังสือ</td>
                                        <td class="sslbl" width="35%">
                                            <asp:Label ID="lblStatus" runat="server" Text='<%# BindField("status_name") %>'></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="sslbl_bold">
                                            วันที่</td>
                                        <td class="sslbl">
                                            <asp:Label ID="lblRecieveDate" runat="server" 
                                        Text='<%# BindField("recieve_date") %>'></asp:Label>
                                        </td>
                                        <td class="sslbl_bold">
                                            ที่มา</td>
                                        <td class="sslbl" >
                                            <asp:Label ID="lblFrom" runat="server" Text='<%# BindField("from_name") %>'></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="sslbl_bold">
                                            ลงวันที่</td>
                                        <td class="sslbl">
                                            <asp:Label ID="lblStampDate" runat="server" 
                                        Text='<%# BindField("stamp_date") %>'></asp:Label>
                                        </td>
                                        <td class="sslbl_bold">
                                            คำค้นหา</td>
                                        <td class="sslbl" >
                                            <asp:Label ID="lblKeyword" runat="server" Text='<%# BindField("keyword") %>'></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="sslbl_bold">
                                            ความเร่งด่วน</td>
                                        <td class="sslbl">
                                            <asp:Label ID="lblPriority" runat="server" Text='<%# BindField("priority_name") %>'></asp:Label>
                                        </td>
                                        <td class="sslbl_bold">
                                            ประเภทหนังสือ</td>
                                        <td class="sslbl" >
                                            <asp:Label ID="lblBookType" runat="server" Text='<%# BindField("bookkind_name") %>'></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="sslbl_bold">
                                            เรื่อง</td>
                                        <td class="sslbl">
                                            <asp:Label ID="lblTopic" runat="server" Text='<%# BindField("topic") %>'></asp:Label>
                                        </td>
                                        <td class="sslbl_bold">
                                            ผู้ป้อนข้อมูล</td>
                                        <td class="sslbl" >
                                            <asp:Label ID="lblCreate" runat="server" Text='<%# BindField("creation_name") %>'></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="sslbl_bold">
                                            เสนอ</td>
                                        <td class="sslbl">
                                            <asp:Label ID="lblPresent" runat="server" Text='<%# BindField("present") %>'></asp:Label>
                                        </td>
                                        <td class="sslbl_bold">
                                            ส่งถึง</td>
                                        <td class="sslbl" >
                                            <asp:Label ID="lblSendTo" runat="server" Text='<%# BindField("send_name") %>'></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="sslbl_bold">
                                            หน่วยงาน</td>
                                        <td class="sslbl">
                                            <asp:GridView ID="gdvDIV" runat="server" AutoGenerateColumns="False" 
                                                CellPadding="0" GridLines="None" ShowHeader="False" Width="200px">
                                                <RowStyle BackColor="White" />
                                                <Columns>
                                                    <asp:BoundField DataField="div_name" ShowHeader="False" />
                                                </Columns>
                                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                <EditRowStyle BackColor="#2461BF" />
                                                <AlternatingRowStyle BackColor="White" />
                                            </asp:GridView>
                                        </td>
                                        <td class="sslbl_bold">
                                            &nbsp;</td>
                                        <td class="sslbl">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="HeaderGreen" colspan="4">
                                            ผู้อำนวยการจ่ายงาน</td>
                                    </tr>
                                    <tr>
                                        <td class="sslbl_bold">
                                            ส่งต่อ</td>
                                        <td class="sslbl">
                                            <asp:Label ID="lblSendto1" runat="server" Text='<%# BindField("sendto1") %>'></asp:Label>
                                        </td>
                                        <td class="sslbl_bold">
                                            &nbsp;</td>
                                        <td class="sslbl">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="sslbl_bold">
                                            บันทึก</td>
                                        <td class="sslbl">
                                            <asp:Label ID="lblSendto_comment" runat="server" 
                                                Text='<%# BindField("sendto_comment1") %>'></asp:Label>
                                        </td>
                                        <td class="sslbl_bold">
                                            &nbsp;</td>
                                        <td class="sslbl">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="sslbl_bold">
                                            วันที่</td>
                                        <td class="sslbl">
                                            <asp:Label ID="lblSendto_date1" runat="server" 
                                                Text='<%# BindField("sendto_date1") %>'></asp:Label>
                                        </td>
                                        <td class="sslbl_bold">
                                            &nbsp;</td>
                                        <td class="sslbl">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="HeaderGreen" colspan="4">
                                            หัวหน้าจ่ายงาน</td>
                                    </tr>
                                    <tr>
                                        <td class="sslbl_bold">
                                            ส่งต่อ</td>
                                        <td class="sslbl">
                                            <asp:Label ID="lblSendto2" runat="server" Text='<%# BindField("sendto2") %>'></asp:Label>
                                        </td>
                                        <td class="sslbl_bold">
                                            &nbsp;</td>
                                        <td class="sslbl">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="sslbl_bold">
                                            บันทึก</td>
                                        <td class="sslbl">
                                            <asp:Label ID="lblSendto_comment1" runat="server" 
                                                Text='<%# BindField("sendto_comment2") %>'></asp:Label>
                                        </td>
                                        <td class="sslbl_bold">
                                            &nbsp;</td>
                                        <td class="sslbl">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="sslbl_bold">
                                            วันที่</td>
                                        <td class="sslbl">
                                            <asp:Label ID="lblSendto_date2" runat="server" 
                                                Text='<%# BindField("sendto_date2") %>'></asp:Label>
                                        </td>
                                        <td class="sslbl_bold">
                                            &nbsp;</td>
                                        <td class="sslbl">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="HeaderGreen" colspan="4">
                                            เรื่องอ้างอิง</td>
                                    </tr>
                                    <tr>
                                        <td class="sslbl_bold" colspan="4">
                                            <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
                                                AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" 
                                                ForeColor="#333333" GridLines="None" PageSize="20" 
                                                ShowHeader="False" Width="100%">
                                                <RowStyle BackColor="White" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="ลำดับ">
                                                    <ItemStyle Width="3%" HorizontalAlign="Center"></ItemStyle>

                                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                    <ItemTemplate>
                                                    <%#iRow%>
                                                    .

                                                    </ItemTemplate>
                                                    </asp:TemplateField>
<asp:TemplateField HeaderText="รหัส" Visible="false">

<ItemTemplate>
<asp:Label ID="lblId" runat="server" Text='<%# Bind("id") %>'></asp:Label>
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="ประเภท" Visible="false">

<ItemTemplate>
<asp:Label ID="lbltype" runat="server" Text='<%# Bind("type_id") %>'></asp:Label>
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="ประเภท" >

<ItemTemplate>
<asp:Label ID="lbllinktype" runat="server" Text='<%# Bind("link_type") %>'></asp:Label>
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField SortExpression="name" HeaderText="เรื่อง">
<ItemTemplate>
<asp:LinkButton id="LinkName" runat="server" Text='<%# Bind("name") %>' CommandName="cName" ></asp:LinkButton> 
</ItemTemplate>
</asp:TemplateField>
                                                        
                                                   
                                                </Columns>
                                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
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
                                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                <EditRowStyle BackColor="#2461BF" />
                                                <AlternatingRowStyle BackColor="White" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="sslbl_bold">
                                            <asp:Label ID="lblPrintComment" runat="server" Font-Bold="True" 
                                                ForeColor="Black" Visible="False"></asp:Label>
                                        </td>
                                        <td class="sslbl">
                                            &nbsp;</td>
                                        <td class="sslbl_bold">
                                            &nbsp;</td>
                                        <td class="sslbl_right">
                                            <asp:Label ID="lblPrint" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                         
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

