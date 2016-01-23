<%@ Page Language="VB" MasterPageFile="~/MasterPageProfile.master" AutoEventWireup="false" CodeFile="LawNews.aspx.vb" Inherits="LawNews" title="ข่าวสำนักกฎหมาย" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
<asp:Panel ID="pnlListData" runat="server">
<table width="100%" cellpadding="3" cellspacing="3">
    <tr>
        <td align="center" class="WTitle">ข่าวสำนักกฎหมาย</td>
    </tr>
    <tr>
        <td align="center">
        <asp:Repeater ID="rptLawNews" runat="server">
            <HeaderTemplate>
                <table width="100%" cellpadding="3" cellspacing="3">
            </HeaderTemplate>
                <ItemTemplate>
                    <tr class="WSubDetail">
                        <td align="left" width="700px">
                            <asp:Label ID="lblNews_title" runat="server" Text='<%# Bind("news_title")  %>'></asp:Label>
                        </td>
                        <td  align="center" width="100px">
                            <asp:LinkButton ID="lbtnNews_id" runat="server" Font-Underline="true" CommandArgument='<%# Bind("news_id")  %>'
                            CommandName="Detail">รายละเอียด</asp:LinkButton>
                        </td>
                    </tr>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <tr class="WSubAltDetail">
                        <td align="left" width="700px">
                            <asp:Label ID="lblNews_title" runat="server" Text='<%# Bind("news_title")  %>'></asp:Label>
                        </td>
                        <td  align="center" width="100px">
                            <asp:LinkButton ID="lbtnNews_id" runat="server" Font-Underline="true" CommandArgument='<%# Bind("news_id")  %>'
                            CommandName="Detail">รายละเอียด</asp:LinkButton>
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

<asp:Panel ID="pnlEnterData" runat="server">
<table width="100%" cellpadding="3" cellspacing="3">
    <tr>
        <td align="center" class="WTitle">ข่าวสำนักกฎหมาย</td>
    </tr>
    <tr>
        <td align="left">
            &nbsp;&nbsp;
            <b><asp:Label ID="lblLawSubject" runat="server" Text=""></asp:Label></b>
        </td>
    </tr>
    <tr>
        <td align="center">
            <asp:Repeater ID="rptListNewsDetail" runat="server">
                <HeaderTemplate>
                    <table cellpadding="5" cellspacing="3" width="100%">
                </HeaderTemplate>
                <ItemTemplate>
                      <tr>
                        <td align="left">
                            &nbsp;&nbsp;<asp:Label ID="lblNews_title" runat="server" Text='<%# Bind("news_detail")  %>'></asp:Label>
                        </td>
                    </tr>
                    
                 <tr>
                
                    <td align="right">
                            <asp:LinkButton ID="LinkButtonLoad" runat="server" CommandName="Load" CommandArgument='<%# Bind("news_id")  %>' >ดาวน์โหลด</asp:LinkButton>
                        </td>
                 </tr>
                 
                </ItemTemplate>
                <FooterTemplate>
                    <tr>
                        <td align="right">
                            <asp:LinkButton ID="lbtnLawNewsBack" runat="server" CommandName="Back">กลับ...</asp:LinkButton>
                        </td>
                    </tr>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
        </td>
    </tr>
</table>
</asp:Panel>
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>

