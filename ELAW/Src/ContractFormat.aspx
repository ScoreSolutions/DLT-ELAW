<%@ Page Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" ValidateRequest="false" CodeFile="ContractFormat.aspx.vb" Inherits="Src_ContractFormat" title="รูปแบบสัญญา-LAW4109" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<%@ Register assembly="FredCK.FCKeditorV2" namespace="FredCK.FCKeditorV2" tagprefix="FCKeditorV2" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style10
        {
            font-family: "MS Sans Serif", "Microsoft Sans Serif", AngsanaUPC;
            font-size: 14px;
            color: #000000;
            font-weight : bold;
            text-decoration: none;
            vertical-align: top;
            padding: 1px;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table class="form" border="0" cellpadding="1" cellspacing="1">
                        <tr>
                            <td class="HeaderGreen" colspan="2">
                                <asp:LinkButton ID="link1" runat="server">งานสัญญา</asp:LinkButton>
                                &nbsp;&gt;
                                <asp:LinkButton ID="link2" runat="server">รูปแบบสัญญา</asp:LinkButton>
                                &nbsp;&gt;
                                <asp:LinkButton ID="link3" runat="server">บันทึก</asp:LinkButton>
                            </td>
                        </tr>
                        <tr>
                            <td class="style10" width="10%">
                                ประเภทสัญญา</td>
                            <td class="sslbl" width="90%">
                                <asp:Label ID="lblSubType" runat="server" 
                                    Text='<%# BindField("subtype_name") %>'></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="style10" colspan="2">
                                <FCKeditorV2:FCKeditor ID="FCKeditor2" runat="server" BasePath="~/fckeditor/" 
                                    Height="500px" Value='<%#BindField("format")%>'>
                                </FCKeditorV2:FCKeditor>
                            </td>
                        </tr>
                        <tr>
                            <td class="style10" colspan="2">
                                <asp:Button ID="bSave" runat="server" CssClass="ssbtn" Text="บันทึก" 
                                    Width="80px" />
                                <asp:Button ID="bCancel" runat="server" CssClass="ssbtn" Text="ยกเลิก" 
                                    Width="80px" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
    </asp:UpdatePanel>
    <br />
</asp:Content>

