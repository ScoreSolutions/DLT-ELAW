<%@ Page Language="VB" MasterPageFile="~/MasterPageProfile.master" AutoEventWireup="false" CodeFile="LawArticle.aspx.vb" Inherits="LawArticle" title="บทความที่น่าสนใจ" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
<asp:Panel ID="pnlListData" runat="server">
<table width="100%" cellpadding="3" cellspacing="3">
    <tr>
        <td align="center" class="WTitle">บทความที่น่าสนใจ</td>
    </tr>
    <tr>
        <td align="center">
        <asp:Repeater ID="rptLawArticle" runat="server">
            <HeaderTemplate>
                <table width="100%" cellpadding="3" cellspacing="3">
            </HeaderTemplate>
                <ItemTemplate>
                    <tr class="WSubDetail">
                        <td align="left" width="650px">
                            <asp:Label ID="lblSubject_desc" runat="server" Text='<%# Bind("name")  %>'></asp:Label>
                        </td>
                        <td  align="center" width="100px">
                            <asp:LinkButton ID="lbtnLawArticle" runat="server" Font-Underline="true" CommandArgument='<%# Bind("file_id")  %>'
                            CommandName="Download">ดาวน์โหลด</asp:LinkButton>
                        </td>
                    </tr>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <tr class="WSubAltDetail">
                        <td  align="left" width="650px">
                            <asp:Label ID="lblSubject_desc" runat="server" Text='<%# Bind("name")  %>'></asp:Label>
                        </td>
                        <td  align="center" width="100px">
                            <asp:LinkButton ID="lbtnLawArticle" runat="server" Font-Underline="true" CommandArgument='<%# Bind("file_id")  %>'
                            CommandName="Download">ดาวน์โหลด</asp:LinkButton>
                        </td>
                    </tr>
                </AlternatingItemTemplate>
                <FooterTemplate>
                </table>
                </FooterTemplate>
            </asp:Repeater>
        </td>
    </tr>
    <tr>
        <td align="center"></td>
    </tr>
</table>
</asp:Panel>
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>

