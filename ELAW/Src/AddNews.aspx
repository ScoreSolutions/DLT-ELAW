<%@ Page Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="AddNews.aspx.vb" Inherits="Src_AddNews" title="เพิ่มข่าวสารและกิจกรรม" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<center>

    <asp:Panel ID="pnlListData" runat="server">
<table width="100%" cellpadding="3" cellspacing="3">
    <tr>
        <td align="center" class="HeaderGreen">เพิ่มข่าวสารและกิจกรรม</td>
    </tr>
    <tr>
        <td align="center" class="WTitle">
        <table style="width:100%;">
            <tr>
                <td align="left" width = "90%" colspan="2">&nbsp;</td>
            </tr>
            <tr>
                <td  width = "20%" align = "right">เรื่อง : </td>
                <td align ="left"><asp:TextBox ID="txtELAW" runat="server" Width="580px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" width = "10%" valign="top" >
                    รายละเอียด :</td>
                <td align="left">
                    <asp:TextBox ID="txtDetail" runat="server" Width="580px" 
                        TextMode="MultiLine" Height="61px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" valign="top" width="10%">
                    ไฟล์ :</td>
                <td align="left">
                    <asp:FileUpload ID="FileUpload" runat="server" Width="580px" />
                </td>
            </tr>
            <tr>
                <td align="right" width = "10%">
                    <asp:Label ID="lblID" runat="server" Visible="False"></asp:Label>
                </td>
                <td align="left">
                    <asp:Button ID="btnCreateELAW" runat="server" 
                        Text="บันทึกข้อมูล" Width="120px" />
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2" width = "10%">
                    </td>
            </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td align="center">
        <asp:Repeater ID="rptLawNews" runat="server">
            <HeaderTemplate>
                <table width="100%" cellpadding="3" cellspacing="3">
            </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td align="left" width="700px">
                            <asp:Label ID="lblNews_title" runat="server" Text='<%# Bind("news_title")  %>'></asp:Label>
                        </td>
                        <td  align="center" width="100px">
                            <asp:LinkButton ID="lbtnNews_id" runat="server" Font-Underline="true" CommandArgument='<%# Bind("news_id")  %>'
                            CommandName="Detail">รายละเอียด</asp:LinkButton>
                        </td>
                        <td  align="center" width="100px">
                            <asp:LinkButton ID="lbtnDelete" runat="server" Font-Underline="true" CommandArgument='<%# Bind("news_id")  %>'
                            CommandName="Delete">ลบ</asp:LinkButton>
                        </td>
                    </tr>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <tr style="background-color: #EFF3FB;">
                        <td align="left" width="700px">
                            <asp:Label ID="lblNews_title" runat="server" Text='<%# Bind("news_title")  %>'></asp:Label>
                        </td>
                        <td  align="center" width="100px">
                            <asp:LinkButton ID="lbtnNews_id" runat="server" Font-Underline="true" CommandArgument='<%# Bind("news_id")  %>'
                            CommandName="Detail">รายละเอียด</asp:LinkButton>
                        </td>
                        <td  align="center" width="100px">
                            <asp:LinkButton ID="lbtnDelete" runat="server" Font-Underline="true" CommandArgument='<%# Bind("news_id")  %>'
                            CommandName="Delete">ลบ</asp:LinkButton>
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
                            &nbsp;&nbsp;<asp:Label ID="lblNews_title" runat="server" Text='<%# Bind("news_detail")  %>'></asp:Label></td>
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

</center>
</asp:Content>




