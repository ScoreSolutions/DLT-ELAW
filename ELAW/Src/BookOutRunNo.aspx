<%@ Page Title="ออกเลขหนังสือ-LAW6109" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="BookOutRunNo.aspx.vb" Inherits="Src_BookOutRunNo" %>

<%@ Register assembly="FredCK.FCKeditorV2" namespace="FredCK.FCKeditorV2" tagprefix="FCKeditorV2" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div align="left">
        <table class="form" cellpadding="1" cellspacing="1" frame="border" 
        border="0" width="200">
            <tr>
                <td class="HeaderGreen" colspan="2">
                                      
                        <asp:LinkButton ID="linkHome" runat="server">Home</asp:LinkButton>
                                    &nbsp;&gt;
                                    <asp:LinkButton ID="link1" runat="server">ทะเบียนหนังสือนำส่ง</asp:LinkButton>
                                    &nbsp;&gt;
                                    <asp:LinkButton ID="link2" runat="server">ออกเลขหนังสือ</asp:LinkButton>
                                    &nbsp;&gt;
                                    <asp:LinkButton ID="link3" runat="server">รายละเอียด</asp:LinkButton>
                       </td>
                    </td>
            </tr>
            <tr>
                <td class="sslbl_right" width="20%">
            เลขที่หนังสือ :</td>
                <td class="sslbl" width="80%">
                    <asp:TextBox ID="txtBookNo" runat="server"  
                CssClass="ssddl" Width="100px" MaxLength="15"></asp:TextBox>
                                            <asp:Label ID="lblChkBookNo" runat="server" 
                CssClass="sslbl_red"></asp:Label>
                    <asp:Label ID="lblMainStatus" runat="server" 
                Visible="False"></asp:Label>
                    <asp:Label ID="lblId" runat="server" 
                Visible="False"></asp:Label>
                    <asp:Label ID="lblIdNew" runat="server" 
                Visible="False"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="sslbl_right">
            ลงวันที่ :</td>
                <td class="sslbl">
                    <asp:Label ID="lblDate" runat="server"><%#BindField("dates")%></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="sslbl_right">
                    สถานะหนังสือ :</td>
                <td class="sslbl">
                    <asp:Label ID="lblStatus" runat="server"><%#BindField("status_name")%></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="sslbl_right">
                    ชนิดหนังสือ :</td>
                <td class="sslbl">
                    <asp:Label ID="lblType" runat="server"><%#BindField("bookkind_name")%></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="sslbl_right">
                    เจ้าของเรื่อง :</td>
                <td class="sslbl">
                    <asp:Label ID="lblCreateName" runat="server"><%#BindField("createname")%></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="sslbl_right">
            ผู้ลงนามในหนังสือ :</td>
                <td class="sslbl">
                    <asp:Label ID="lblName" runat="server"><%#BindField("signname")%></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="sslbl_right">
            ความเร่งด่วน :</td>
                <td class="sslbl" >
                    <asp:Label ID="lblPriority" runat="server"><%#BindField("priority_name")%></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="sslbl_right">
            เรื่อง :</td>
                <td class="sslbl" >
                    <asp:Label ID="lblTopic" runat="server"><%#BindField("topic")%></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="sslbl_right">
            เรียน :</td>
                <td class="sslbl" >
                    <asp:Label ID="lblPresent" runat="server"><%#BindField("present")%></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="sslbl_right">
            เนื้อความ :</td>
                <td class="sslbl" >
                    <asp:Label ID="lblMessage" runat="server"><%#BindField("message")%></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="sslbl_right">
            ลงท้าย :</td>
                <td class="sslbl" >
                    <asp:Label ID="lblPostScript" runat="server"><%#BindField("postscript")%></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="sslbl_right">
            คำลงท้าย(ชื่อ) :</td>
                <td class="sslbl" >
                    <asp:Label ID="lblPostName" runat="server"><%#BindField("postname")%></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="sslbl_right">
            คำลงท้าย(ตำแหน่ง) :</td>
                <td class="sslbl" >
                    <asp:Label ID="lblPostPosition" runat="server"><%#BindField("post_pos")%></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="sslbl_right">
            ข้อมูลผู้ติดต่อ :</td>
                <td class="sslbl" >
                    <asp:Label ID="lblContact" runat="server"><%#BindField("contact")%></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="sslbl_right">
            หมายเหตุ :</td>
                <td class="sslbl" >
                    <asp:Label ID="lblComment" runat="server"><%#BindField("comment")%></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="sslbl_right">
            คำค้นหา :</td>
                <td class="sslbl" >
                    <asp:Label ID="lblKeyword" runat="server"><%#BindField("keyword")%></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="sslbl_right">
            ส่งต่อ :</td>
                <td class="sslbl" >
                    <asp:Label ID="lblSendto" runat="server"><%#BindField("sendname")%></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style8">
                    &nbsp;</td>
                <td class="sslbl" >
                    <asp:Button ID="bSave" runat="server" 
                CssClass="ssbtn" Text="ออกเลขหนังสือ" 
                                                Width="100px" Height="26px" />
                                        &nbsp;&nbsp;</td>
            </tr>
        </table>
    </div>
</asp:Content>

