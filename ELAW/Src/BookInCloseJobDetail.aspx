<%@ Page Title="ปิดงาน-LAW5108" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="BookInCloseJobDetail.aspx.vb" Inherits="Src_BookInCloseJobDetail" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
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
                        <asp:LinkButton ID="link2" runat="server">รับงาน</asp:LinkButton>
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
                                        <td class="sslbl_bold"  >
                                            เลขที่หนังสือ</td>
                                        <td class="sslbl_bold" width="35%" >
                                            <asp:Label ID="lblBookNo" runat="server" Text='<%# BindField("bookin_no") %>'></asp:Label>
                                            <asp:Label ID="lblId" runat="server" Visible="False"></asp:Label>
                                            <asp:Label ID="lblIdNew" runat="server"></asp:Label>
                                        </td>
                                        <td class="sslbl_bold" width="15%" >
                                            สถานะหนังสือ</td>
                                        <td class="sslbl" width="35%"  >
                                            <asp:Label ID="lblStatus" runat="server" Text='<%# BindField("status_name") %>'></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="sslbl_bold" >
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
                                        <td class="sslbl_bold" >
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
                                        <td class="sslbl_bold" >
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
                                        <td class="sslbl_bold" >
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
                                        <td class="sslbl_bold" >
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
                                </table>
                         
            </table>
   
                    <table border="0" cellpadding="1" cellspacing="1" class="form" width="100%">
                        <tr>
                            <td class="sslbl_bold" >
                                            บันทีก</td>
                        </tr>
                        <tr>
                            <td class="sslbl_bold">
                                <asp:TextBox ID="txtComment" runat="server" CssClass="ssddl" Rows="15" 
                                    TextMode="MultiLine" Width="98%"></asp:TextBox>
                                <asp:Label ID="lblAComment" runat="server" CssClass="sslbl_red"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="sslbl_bold" >
                                            <asp:Button ID="bApp" runat="server" CssClass="ssbtn" Text="บันทึก" 
                                                Width="80px" />
                                            <asp:Button ID="bAppCancel" runat="server" CssClass="ssbtn" Text="ยกเลิก" 
                                                Width="80px" />
                            </td>
                        </tr>
                    </table>
           
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

