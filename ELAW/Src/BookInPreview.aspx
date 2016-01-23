<%@ Page Title="รายละเอียดการจ่ายงาน-LAW5103" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="BookInPreview.aspx.vb" Inherits="Src_BookInPreview" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>


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
                                    <asp:LinkButton ID="link2" runat="server">รอจ่ายงาน</asp:LinkButton>
                                     &nbsp;&gt;
                                     <asp:LinkButton ID="link3" runat="server">รายละเอียด</asp:LinkButton>
                                    </td>
                                <td class="HeaderTab" align="right">&nbsp;
                                    </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="sslbl">
                        <asp:MultiView ID="MultiViewMaster" runat="server">
                            <asp:View ID="View23" runat="server">
                                <table border="0" cellpadding="1" cellspacing="1" class="form" width="100%">
                                    <tr>
                                        <td class="style9">
                                            เลขที่หนังสือ</td>
                                        <td class="sslbl" width="35%">
                                            <asp:Label ID="lblBookNo" runat="server" Text='<%# BindField("bookin_no") %>'></asp:Label>
                                            <asp:Label ID="lblId" runat="server" Visible="False"></asp:Label>
                                            <asp:Label ID="lblIdNew" runat="server"></asp:Label>
                                        </td>
                                        <td class="style8">
                                            สถานะหนังสือ</td>
                                        <td class="sslbl" width="35%" >
                                            <asp:Label ID="lblStatus" runat="server" Text='<%# BindField("status_name") %>'></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style9">
                                            วันที่</td>
                                        <td class="sslbl">
                                            <asp:Label ID="lblRecieveDate" runat="server" 
                                        Text='<%# BindField("recieve_date") %>'></asp:Label>
                                        </td>
                                        <td class="style8">
                                            ที่มา</td>
                                        <td class="sslbl" >
                                            <asp:Label ID="lblFrom" runat="server" Text='<%# BindField("from_name") %>'></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style9">
                                            ลงวันที่</td>
                                        <td class="sslbl">
                                            <asp:Label ID="lblStampDate" runat="server" 
                                        Text='<%# BindField("stamp_date") %>'></asp:Label>
                                        </td>
                                        <td class="style8">
                                            คำค้นหา</td>
                                        <td class="sslbl" >
                                            <asp:Label ID="lblKeyword" runat="server" Text='<%# BindField("keyword") %>'></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style9">
                                            ความเร่งด่วน</td>
                                        <td class="sslbl">
                                            <asp:Label ID="lblPriority" runat="server" Text='<%# BindField("priority_name") %>'></asp:Label>
                                        </td>
                                        <td class="style8">
                                            ประเภทหนังสือ</td>
                                        <td class="sslbl" >
                                            <asp:Label ID="lblBookType" runat="server" Text='<%# BindField("bookkind_name") %>'></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style9">
                                            เรื่อง</td>
                                        <td class="sslbl">
                                            <asp:Label ID="lblTopic" runat="server" Text='<%# BindField("topic") %>'></asp:Label>
                                        </td>
                                        <td class="style8">
                                            ผู้ป้อนข้อมูล</td>
                                        <td class="sslbl" >
                                            <asp:Label ID="lblCreate" runat="server" Text='<%# BindField("creation_name") %>'></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style9">
                                            เสนอ</td>
                                        <td class="sslbl">
                                            <asp:Label ID="lblPresent" runat="server" Text='<%# BindField("present") %>'></asp:Label>
                                        </td>
                                        <td class="style8">
                                            ส่งถึง</td>
                                        <td class="sslbl">
                                            <asp:Label ID="lblSendTo" runat="server" Text='<%# BindField("send_name") %>'></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style9">
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
                                        <td class="style8">&nbsp;
                                            </td>
                                        <td class="sslbl">&nbsp;
                                            </td>
                                    </tr>
                                </table>
                            </asp:View>
                        </asp:MultiView>
            </table>
           
                        <asp:MultiView ID="MultiViewApp" runat="server">
                            <asp:View ID="View22" runat="server">
                                <table border="0" cellpadding="1" cellspacing="1" class="form" width="100%">
                                    <tr>
                                        <td class="HeaderGreen" width="15%" colspan="2">
                                            จ่ายงาน</td>
                                    </tr>
                                    <tr>
                                        <td class="style8">
                                            ประเภทการส่งต่อ</td>
                                        <td class="sslbl" width="85%">
                                            <asp:RadioButtonList ID="rdbSend" runat="server" CssClass="sslbl" 
                                                RepeatColumns="3" AutoPostBack="True">
                                                <asp:ListItem Value="0" Selected="True">ส่งให้หัวหน้ากลุ่มจ่ายงาน</asp:ListItem>
                                                <asp:ListItem Value="1">ส่งตรงถึงเจ้าของเรื่อง</asp:ListItem>
                                                <asp:ListItem Value="2">ส่งเรื่องกลับธุรการ</asp:ListItem>
                                            </asp:RadioButtonList>
                                            <asp:CheckBox ID="ChkBack" runat="server" AutoPostBack="True" 
                                                Text="ส่งเรื่องกลับธุรการ" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style8">
                                            ส่งต่อ</td>
                                        <td class="sslbl" width="85%">
                                            <asp:DropDownList ID="ddlSendName" runat="server" CssClass="ssddl" 
                                                Height="20px" Width="285px">
                                            </asp:DropDownList>
                                            <asp:Label ID="lblAApp" runat="server" CssClass="sslbl_red"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style8">
                                            บันทึก</td>
                                        <td>
                                            <asp:TextBox ID="txtComment" runat="server" CssClass="ssddl" Rows="15" 
                                                Text='<%# BindField("sendto_comment") %>' TextMode="MultiLine" Width="600px"></asp:TextBox>
                                            <asp:Label ID="lblAComment" runat="server" CssClass="sslbl_red"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style8">
                                            <asp:Label ID="lblAppText" runat="server" Text="ลงชื่อ"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblAppName" runat="server" CssClass="sslbl"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style8">&nbsp;</td>
                                        <td align="right">
                                            <asp:Label ID="lblPrint" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style8">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:Button ID="bApp" runat="server" CssClass="ssbtn" Text="บันทึก" 
                                                Width="80px" />
                                            <asp:Button ID="bAppAndSend" runat="server" CssClass="ssbtn" 
                                                Text="บันทึกพร้อมส่ง" Width="159px" />
                                            <asp:Button ID="bAppCancel" runat="server" CssClass="ssbtn" Text="ยกเลิก" 
                                                Width="80px" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:View>
                        </asp:MultiView>
       
        </ContentTemplate>
    </asp:UpdatePanel>
                     
</asp:Content>

<asp:Content ID="Content3" runat="server" contentplaceholderid="head">

    <style type="text/css">
        .style8
        {
            font-family: "MS Sans Serif", "Microsoft Sans Serif", AngsanaUPC;
            font-size: 14px;
            color: #000000;
            font-weight : bold;
            text-decoration: none;
            vertical-align: top;
            padding: 1px;
            width: 15%;
        }
        .style9
        {
            font-family: "MS Sans Serif", "Microsoft Sans Serif", AngsanaUPC;
            font-size: 14px;
            color: #000000;
            font-weight : bold;
            text-decoration: none;
            vertical-align: top;
            padding: 1px;
            width: 14%;
        }
    </style>

</asp:Content>


