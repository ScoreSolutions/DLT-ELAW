<%@ Page Title="เปรียบเทียบเวอร์ชั่นร่างกฎหมาย-LAW2409" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="DrafLawCompare.aspx.vb" Inherits="Src_DrafLawCompare" %>

<%@ Register src="../UserControl/ComboBox.ascx" tagname="ComboBox" tagprefix="uc1" %>
<%@ Register src="../UserControl/TextBox.ascx" tagname="TextBox" tagprefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table class="form" border="0" cellpadding="1" cellspacing="1">
                <tr>
                    <td class="HeaderGreen" colspan="2">
                        <asp:LinkButton ID="linkHome" runat="server">Home</asp:LinkButton>
                        &nbsp;&gt;
                        <asp:LinkButton ID="link1" runat="server">ติดตามงานกฎหมาย</asp:LinkButton>
                        &nbsp;&gt;
                        <asp:LinkButton ID="link2" runat="server">เปรียบเทียบเวอร์ชั่นร่างกฎหมาย</asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td class="sslbl_right">เลขที่อ้างอิง</td>
                    <td>
                        <uc1:ComboBox ID="ddlRefID" CssClass="ssddl"  Width="850" runat="server" IsDefaultValue="false" IsNotNull="false" AutoPosBack="true" />
                    </td>
                </tr>
                <tr>
                    <td class="sslbl_right">เปรียบเทียบระหว่าง</td>
                    <td>
                        <uc1:ComboBox ID="ddlLawIdLeft" CssClass="ssddl" Width="400" runat="server" IsNotNull="false" IsDefaultValue="false" />
                        &nbsp;และ&nbsp;
                        <uc1:ComboBox ID="ddlLawIdRight" CssClass="ssddl" Width="400" runat="server" IsNotNull="false" IsDefaultValue="false" />
                    </td>
                </tr>
                <tr><td colspan="2">&nbsp;</td></tr>
                <tr>
                    <td class="sslbl_right">&nbsp;</td>
                    <td align="center">
                        <asp:Button ID="bResult" runat="server" CssClass="ssbtn" Width="100" Text="แสดงผลลัพธ์" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

