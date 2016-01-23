<%@ Page Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="NoAuth.aspx.vb" Inherits="Menu_NoAuth" title="Access Denied" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table class="Sky2" style="width: 100%; height: 500px">
        <tr>
            <td colspan="3" style="height: 133px; text-align: center" valign="top">
                &nbsp;<br />
                <asp:Label ID="lblProject" runat="server" Font-Bold="True" Font-Size="X-Large" ForeColor="Crimson">คุณไม่มีสิทธิ์ในการใช้งานเมนูนี้</asp:Label></td>
        </tr>
        <tr>
            <td style="width: 100px; height: 45px">
            </td>
            <td style="width: 100px; height: 45px; text-align: center;">
            </td>
            <td style="width: 100px; height: 45px">
            </td>
        </tr>
        <tr>
            <td style="width: 100px; height: 43px">
            </td>
            <td style="width: 100px; height: 43px">
            </td>
            <td style="width: 100px; height: 43px">
            </td>
        </tr>
    </table>
</asp:Content>

