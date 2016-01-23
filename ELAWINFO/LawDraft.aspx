<%@ Page Language="VB" MasterPageFile="~/MasterPageProfile.master" AutoEventWireup="false" CodeFile="LawDraft.aspx.vb" Inherits="LawDraft" title="แสดงความคิดเห็นด้านกฏหมาย" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
<asp:Panel ID="pnlListData" runat="server">
<table width="100%" cellpadding="3" cellspacing="3">
    <tr>
        <td align="center" class="WTitle">ร่างกฎหมายรับฟังความคิดเห็น</td>
    </tr>
    <tr>
        <td align="center">
        <asp:Repeater ID="rptLawDraft" runat="server">
            <HeaderTemplate>
                <table width="100%" cellpadding="3" cellspacing="3">
            </HeaderTemplate>
            <ItemTemplate>
                <tr class="WSubDetail">
                    <td  align="left" width="700px">
                        <asp:Label ID="lblSubject_desc" runat="server" Text='<%# Bind("subject_desc")  %>'></asp:Label>
                    </td>
                    <td  align="center" width="100px">
                        <asp:LinkButton ID="lbtnLawDraft" runat="server" Font-Underline="true" CommandArgument='<%# Bind("subject_id")  %>'
                        CommandName="Comment">แสดงความเห็น</asp:LinkButton>
                    </td>
                </tr>
            </ItemTemplate>
            <AlternatingItemTemplate>
                <tr class="WSubAltDetail">
                    <td  align="left" width="700px">
                        <asp:Label ID="lblSubject_desc" runat="server" Text='<%# Bind("subject_desc")  %>'></asp:Label>
                    </td>
                    <td  align="center" width="100px">
                        <asp:LinkButton ID="lbtnLawDraft" runat="server" Font-Underline="true" CommandArgument='<%# Bind("subject_id")  %>'
                        CommandName="Comment">แสดงความเห็น</asp:LinkButton>
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
        <td align="center" class="WTitle">แสดงความคิดเห็นเกี่ยวกับร่างกฏหมาย</td>
    </tr>
    <tr>
        <td align="left">
            &nbsp;&nbsp;
            <b><asp:Label ID="lblLawSubject" runat="server" Text=""></asp:Label></b>
            <asp:Label ID="lblDownload" runat="server" Text="[ดาวน์โหลดเอกสาร]" Visible="false"></asp:Label>
        </td>
    </tr>
    <tr>
        <td align="left">
        </td>
    </tr>
    <tr>
        <td align="center">
            <asp:Repeater ID="rptListQuestion" runat="server">
                <HeaderTemplate>
                    <table cellpadding="5" cellspacing="3" width="100%">
                </HeaderTemplate>
                <ItemTemplate>
                    <tr class="WSubDetail">
                        <td align="left" width="700px">
                            [<asp:Label ID="lbl" runat="server" Text="<%# Convert.ToString(Container.ItemIndex + 1) %>"></asp:Label>]
                            <asp:Label ID="txtSubj_question" runat="server" Text="<%# Bind('subj_question')%>"></asp:Label>
                        </td>
                     </tr>
                     <tr>
                        <td align="left">
                            &nbsp;<asp:TextBox ID="txtAnswer" runat="server" Height="50px" TextMode="MultiLine" Width="600px">
                            </asp:TextBox>
                            <asp:HiddenField ID="HiddenField" runat="server" Value="<%# Bind('question_id')%>" />
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
        </td>
    </tr>
    <tr>
        <td align="center">
            <asp:Button ID="btnEnter" runat="server" Text="บันทึก" Width="150px" />
            &nbsp;
            <asp:Button ID="btnBack" runat="server" Text="ยกเลิก" Width="150px" />
        </td>
    </tr>
</table>
</asp:Panel>
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>

