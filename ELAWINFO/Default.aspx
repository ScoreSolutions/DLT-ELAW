<%@ Page Language="VB" MasterPageFile="MasterPageProfile.master" AutoEventWireup="false"
    CodeFile="Default.aspx.vb" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript">
        function openwindow(Page, Id) {
            window.open("../Src/" + Page + ".aspx?id=" + Id, "_new", "location=no,status=no,resizable=yes,width=750,height=700,scrollbars=yes,menubar=no");
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <table width="100%" cellpadding="3" cellspacing="3">
                <tr>
                    <td align="center" colspan="2" class="WTitle">
                        ��ҧ�������Ѻ�ѧ�����Դ���
                    </td>
                </tr>
                <tr>
                    <td align="center" width="100px">
                        <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/text-file-2.png" />
                    </td>
                    <td align="left" valign="top">
                        <asp:Repeater ID="rptLawDraft" runat="server">
                            <HeaderTemplate>
                                <table width="100%" cellpadding="3" cellspacing="3">
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr class="WSubDetail">
                                    <td align="left" width="500px">
                                        <asp:Label ID="lblSubject_desc" runat="server" Text='<%# Bind("subject_desc")  %>'></asp:Label>
                                    </td>
                                    <td align="center" width="100px">
                                        <asp:LinkButton ID="lbtnLawDraft" runat="server" Font-Underline="true" CommandArgument='<%# Bind("subject_id")  %>'
                                            CommandName="Comment">�ʴ��������</asp:LinkButton>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <AlternatingItemTemplate>
                                <tr class="WSubAltDetail">
                                    <td align="left" width="500px">
                                        <asp:Label ID="lblSubject_desc" runat="server" Text='<%# Bind("subject_desc")  %>'></asp:Label>
                                    </td>
                                    <td align="center" width="100px">
                                        <asp:LinkButton ID="lbtnLawDraft" runat="server" Font-Underline="true" CommandArgument='<%# Bind("subject_id")  %>'
                                            CommandName="Comment">�ʴ��������</asp:LinkButton>
                                    </td>
                                </tr>
                            </AlternatingItemTemplate>
                            <FooterTemplate>
                                <tr>
                                    <td align="right" colspan="2">
                                        <asp:LinkButton ID="lbtnLawDraft" runat="server" CommandName="Readmore">��ҹ���...</asp:LinkButton>
                                    </td>
                                </tr>
                                </table>
                            </FooterTemplate>
                        </asp:Repeater>
                    </td>
                </tr>
                <!--����������-->
                <tr>
                    <td align="center">
                        &nbsp;
                    </td>
                    <td align="right">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="2" class="WTitle">
                        ����������
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:Image ID="Image4" runat="server" ImageUrl="~/Images/ico-law.png" />
                    </td>
                    <td align="left" valign="top">
                        <!--����з�ǧ -->
                        <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="Medium" Text="����Ҫ�ѭ�ѵ�/����з�ǧ"
                            ForeColor="#003366"></asp:Label>
                        <asp:Repeater ID="RepeaterLaw1" runat="server">
                            <HeaderTemplate>
                                <table width="100%" cellpadding="3" cellspacing="3">
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr class="WSubDetail">
                                    <td align="left" width="500px">
                                        <asp:Label ID="lblSubject_desc" runat="server" Text='<%# Bind("name")  %>'></asp:Label>
                                        <asp:LinkButton ID="LinkAlert" CommandName="Select" runat="server" Text='<%#ImagesGet(Eval("chk")) %>'
                                            ToolTip="����">
                                        </asp:LinkButton>
                                    </td>
                                    <td align="center" width="100px">
                                        <asp:LinkButton ID="lbtnLaw" runat="server" Font-Underline="true" CommandArgument='<%# Bind("file_id")  %>'
                                            CommandName="Download">��ǹ���Ŵ</asp:LinkButton>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <AlternatingItemTemplate>
                                <tr class="WSubAltDetail">
                                    <td align="left" width="500px">
                                        <asp:Label ID="lblLawName" runat="server" Text='<%# Bind("name")  %>'></asp:Label>
                                        <asp:LinkButton ID="LinkAlert" CommandName="Select" runat="server" Text='<%#ImagesGet(Eval("chk")) %>'
                                            ToolTip="����">
                                        </asp:LinkButton>
                                    </td>
                                    <td align="center" width="100px">
                                        <asp:LinkButton ID="lbtnLaw" runat="server" Font-Underline="true" CommandArgument='<%# Bind("file_id")  %>'
                                            CommandName="Download">��ǹ���Ŵ</asp:LinkButton>
                                    </td>
                                </tr>
                            </AlternatingItemTemplate>
                            <FooterTemplate>
                                <tr>
                                    <td align="right" colspan="2">
                                        <asp:LinkButton ID="lbtnLawLink" runat="server" CommandName="Readmore">��ҹ���...</asp:LinkButton>
                                    </td>
                                </tr>
                                </table>
                            </FooterTemplate>
                        </asp:Repeater>
                        <!--����º -->
                        <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Size="Medium" Text="����º"
                            ForeColor="#003366"></asp:Label>
                        <asp:Repeater ID="RepeaterLaw2" runat="server">
                            <HeaderTemplate>
                                <table width="100%" cellpadding="3" cellspacing="3">
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr class="WSubDetail">
                                    <td align="left" width="500px">
                                        <asp:Label ID="lblSubject_desc" runat="server" Text='<%# Bind("name")  %>'></asp:Label>
                                        <asp:LinkButton ID="LinkAlert" CommandName="Select" runat="server" Text='<%#ImagesGet(Eval("chk")) %>'
                                            ToolTip="����">
                                        </asp:LinkButton>
                                    </td>
                                    <td align="center" width="100px">
                                        <asp:LinkButton ID="lbtnLaw" runat="server" Font-Underline="true" CommandArgument='<%# Bind("file_id")  %>'
                                            CommandName="Download">��ǹ���Ŵ</asp:LinkButton>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <AlternatingItemTemplate>
                                <tr class="WSubAltDetail">
                                    <td align="left" width="500px">
                                        <asp:Label ID="lblLawName" runat="server" Text='<%# Bind("name")  %>'></asp:Label>
                                        <asp:LinkButton ID="LinkAlert" CommandName="Select" runat="server" Text='<%#ImagesGet(Eval("chk")) %>'
                                            ToolTip="����">
                                        </asp:LinkButton>
                                    </td>
                                    <td align="center" width="100px">
                                        <asp:LinkButton ID="lbtnLaw" runat="server" Font-Underline="true" CommandArgument='<%# Bind("file_id")  %>'
                                            CommandName="Download">��ǹ���Ŵ</asp:LinkButton>
                                    </td>
                                </tr>
                            </AlternatingItemTemplate>
                            <FooterTemplate>
                                <tr>
                                    <td align="right" colspan="2">
                                        <asp:LinkButton ID="lbtnLawLink" runat="server" CommandName="Readmore">��ҹ���...</asp:LinkButton>
                                    </td>
                                </tr>
                                </table>
                            </FooterTemplate>
                        </asp:Repeater>
                        <!--��С�� -->
                        <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Size="Medium" Text="��С��"
                            ForeColor="#003366"></asp:Label>
                        <asp:Repeater ID="RepeaterLaw3" runat="server">
                            <HeaderTemplate>
                                <table width="100%" cellpadding="3" cellspacing="3">
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr class="WSubDetail">
                                    <td align="left" width="500px">
                                        <asp:Label ID="lblSubject_desc" runat="server" Text='<%# Bind("name")  %>'></asp:Label>
                                        <asp:LinkButton ID="LinkAlert" CommandName="Select" runat="server" Text='<%#ImagesGet(Eval("chk")) %>'
                                            ToolTip="����">
                                        </asp:LinkButton>
                                    </td>
                                    <td align="center" width="100px">
                                        <asp:LinkButton ID="lbtnLaw" runat="server" Font-Underline="true" CommandArgument='<%# Bind("file_id")  %>'
                                            CommandName="Download">��ǹ���Ŵ</asp:LinkButton>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <AlternatingItemTemplate>
                                <tr class="WSubAltDetail">
                                    <td align="left" width="500px">
                                        <asp:Label ID="lblLawName" runat="server" Text='<%# Bind("name")  %>'></asp:Label>
                                        <asp:LinkButton ID="LinkAlert" CommandName="Select" runat="server" Text='<%#ImagesGet(Eval("chk")) %>'
                                            ToolTip="����">
                                        </asp:LinkButton>
                                    </td>
                                    <td align="center" width="100px">
                                        <asp:LinkButton ID="lbtnLaw" runat="server" Font-Underline="true" CommandArgument='<%# Bind("file_id")  %>'
                                            CommandName="Download">��ǹ���Ŵ</asp:LinkButton>
                                    </td>
                                </tr>
                            </AlternatingItemTemplate>
                            <FooterTemplate>
                                <tr>
                                    <td align="right" colspan="2">
                                        <asp:LinkButton ID="lbtnLawLink" runat="server" CommandName="Readmore">��ҹ���...</asp:LinkButton>
                                    </td>
                                </tr>
                                </table>
                            </FooterTemplate>
                        </asp:Repeater>
                        <!--���� -->
                        <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Size="Medium" Text="����"
                            ForeColor="#003366"></asp:Label>
                        <asp:Repeater ID="RepeaterLaw4" runat="server">
                            <HeaderTemplate>
                                <table width="100%" cellpadding="3" cellspacing="3">
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr class="WSubDetail">
                                    <td align="left" width="500px">
                                        <asp:Label ID="lblSubject_desc" runat="server" Text='<%# Bind("name")  %>'></asp:Label>
                                        <asp:LinkButton ID="LinkAlert" CommandName="Select" runat="server" Text='<%#ImagesGet(Eval("chk")) %>'
                                            ToolTip="����">
                                        </asp:LinkButton>
                                    </td>
                                    <td align="center" width="100px">
                                        <asp:LinkButton ID="lbtnLaw" runat="server" Font-Underline="true" CommandArgument='<%# Bind("file_id")  %>'
                                            CommandName="Download">��ǹ���Ŵ</asp:LinkButton>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <AlternatingItemTemplate>
                                <tr class="WSubAltDetail">
                                    <td align="left" width="500px">
                                        <asp:Label ID="lblLawName" runat="server" Text='<%# Bind("name")  %>'></asp:Label>
                                        <asp:LinkButton ID="LinkAlert" CommandName="Select" runat="server" Text='<%#ImagesGet(Eval("chk")) %>'
                                            ToolTip="����">
                                        </asp:LinkButton>
                                    </td>
                                    <td align="center" width="100px">
                                        <asp:LinkButton ID="lbtnLaw" runat="server" Font-Underline="true" CommandArgument='<%# Bind("file_id")  %>'
                                            CommandName="Download">��ǹ���Ŵ</asp:LinkButton>
                                    </td>
                                </tr>
                            </AlternatingItemTemplate>
                            <FooterTemplate>
                                <tr>
                                    <td align="right" colspan="2">
                                        <asp:LinkButton ID="lbtnLawLink" runat="server" CommandName="Readmore">��ҹ���...</asp:LinkButton>
                                    </td>
                                </tr>
                                </table>
                            </FooterTemplate>
                        </asp:Repeater>
                    </td>
                </tr>
                <!-- ��շ����ʹ� -->
                <tr>
                    <td align="center">
                        &nbsp;
                    </td>
                    <td align="right">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="2" class="WTitle">
                        ��շ����ʹ�
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:Image ID="Image9" runat="server" ImageUrl="~/Images/iconlaw_s.png" />
                    </td>
                    <td align="left" valign="top">
                        <asp:Repeater ID="RepeaterCase" runat="server">
                            <HeaderTemplate>
                                <table width="100%" cellpadding="3" cellspacing="3">
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr class="WSubDetail">
                                    <td align="left" width="500px">
                                        <asp:Label ID="lblSubject_desc" runat="server" Text='<%# Bind("name")  %>'></asp:Label>
                                        <asp:LinkButton ID="LinkAlert" CommandName="Select" runat="server" Text='<%#ImagesGet(Eval("chk")) %>'
                                            ToolTip="����">
                                        </asp:LinkButton>
                                    </td>
                                    <td align="center" width="100px">
                                        <asp:LinkButton ID="lbtnLawArticle" runat="server" Font-Underline="true" CommandArgument='<%# Bind("file_id")  %>'
                                            CommandName="Download">��ǹ���Ŵ</asp:LinkButton>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <AlternatingItemTemplate>
                                <tr class="WSubAltDetail">
                                    <td align="left" width="500px">
                                        <asp:Label ID="lblSubject_desc" runat="server" Text='<%# Bind("name")  %>'></asp:Label>
                                        <asp:LinkButton ID="LinkAlert" CommandName="Select" runat="server" Text='<%#ImagesGet(Eval("chk")) %>'
                                            ToolTip="����">
                                        </asp:LinkButton>
                                    </td>
                                    <td align="center" width="100px">
                                        <asp:LinkButton ID="lbtnLawArticle" runat="server" Font-Underline="true" CommandArgument='<%# Bind("file_id")  %>'
                                            CommandName="Download">��ǹ���Ŵ</asp:LinkButton>
                                    </td>
                                </tr>
                            </AlternatingItemTemplate>
                            <FooterTemplate>
                                <tr>
                                    <td align="right" colspan="2">
                                        <asp:LinkButton ID="lbtnLawArticle" runat="server" CommandName="Readmore">��ҹ���...</asp:LinkButton>
                                    </td>
                                </tr>
                                </table>
                            </FooterTemplate>
                        </asp:Repeater>
                    </td>
                </tr>
                <!-- �����������ʹ� -->
                <tr>
                    <td align="center">
                        &nbsp;
                    </td>
                    <td align="right">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="2" class="WTitle">
                        �����������ʹ�
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/article_icon.gif" />
                    </td>
                    <td align="left" valign="top">
                        <asp:Repeater ID="rptLawArticle" runat="server">
                            <HeaderTemplate>
                                <table width="100%" cellpadding="3" cellspacing="3">
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr class="WSubDetail">
                                    <td align="left" width="500px">
                                        <asp:Label ID="lblSubject_desc" runat="server" Text='<%# Bind("name")  %>'></asp:Label>
                                    </td>
                                    <td align="center" width="100px">
                                        <asp:LinkButton ID="lbtnLawArticle" runat="server" Font-Underline="true" CommandArgument='<%# Bind("file_id")  %>'
                                            CommandName="Download">��ǹ���Ŵ</asp:LinkButton>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <AlternatingItemTemplate>
                                <tr class="WSubAltDetail">
                                    <td align="left" width="500px">
                                        <asp:Label ID="lblSubject_desc" runat="server" Text='<%# Bind("name")  %>'></asp:Label>
                                    </td>
                                    <td align="center" width="100px">
                                        <asp:LinkButton ID="lbtnLawArticle" runat="server" Font-Underline="true" CommandArgument='<%# Bind("file_id")  %>'
                                            CommandName="Download">��ǹ���Ŵ</asp:LinkButton>
                                    </td>
                                </tr>
                            </AlternatingItemTemplate>
                            <FooterTemplate>
                                <tr>
                                    <td align="right" colspan="2">
                                        <asp:LinkButton ID="lbtnLawArticle" runat="server" CommandName="Readmore">��ҹ���...</asp:LinkButton>
                                    </td>
                                </tr>
                                </table>
                            </FooterTemplate>
                        </asp:Repeater>
                    </td>
                </tr>
                <!--�����ӹѡ������-->
                <tr>
                    <td align="center">
                        &nbsp;
                    </td>
                    <td align="right">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="2" class="WTitle">
                        �����ӹѡ������
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:Image ID="Image3" runat="server" ImageUrl="~/Images/newsIcon.jpg" />
                    </td>
                    <td align="left" valign="top">
                        <asp:Repeater ID="rptLawNews" runat="server">
                            <HeaderTemplate>
                                <table width="100%" cellpadding="3" cellspacing="3">
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr class="WSubDetail">
                                    <td align="left" width="500px">
                                        <asp:Label ID="lblNews_title" runat="server" Text='<%# Bind("news_title")  %>'></asp:Label>
                                    </td>
                                    <td align="center" width="100px">
                                        <asp:LinkButton ID="lbtnNews_id" runat="server" Font-Underline="true" CommandArgument='<%# Bind("news_id")  %>'
                                            CommandName="Detail">��������´</asp:LinkButton>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <AlternatingItemTemplate>
                                <tr class="WSubAltDetail">
                                    <td align="left" width="500px">
                                        <asp:Label ID="lblNews_title" runat="server" Text='<%# Bind("news_title")  %>'></asp:Label>
                                    </td>
                                    <td align="center" width="100px">
                                        <asp:LinkButton ID="lbtnNews_id" runat="server" Font-Underline="true" CommandArgument='<%# Bind("news_id")  %>'
                                            CommandName="Detail">��������´</asp:LinkButton>
                                    </td>
                                </tr>
                            </AlternatingItemTemplate>
                            <FooterTemplate>
                                <tr>
                                    <td align="right" colspan="2">
                                        <asp:LinkButton ID="lbtnLawNews" runat="server" CommandName="Readmore">��ҹ���...</asp:LinkButton>
                                    </td>
                                </tr>
                                </table>
                            </FooterTemplate>
                        </asp:Repeater>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        &nbsp;
                    </td>
                    <td align="right">
                        &nbsp;
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
