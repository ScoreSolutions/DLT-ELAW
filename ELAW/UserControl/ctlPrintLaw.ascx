<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ctlPrintLaw.ascx.vb" Inherits="UserControl_ctlPrintLaw" %>
<asp:HiddenField ID="hidLawID" runat="server" ></asp:HiddenField>
<asp:Label ID="lblLabelPrint" runat="server" Text="พิมพ์ : "  ></asp:Label>
<asp:RadioButtonList ID="rdiType" runat="server"  RepeatDirection="Horizontal" RepeatLayout="Flow" >
    <asp:ListItem Selected Text="ร่าง" Value="D" ></asp:ListItem>
    <asp:ListItem Text="กฎหมาย" Value="L" ></asp:ListItem>
</asp:RadioButtonList>
<asp:ImageButton ID="btnPrint" runat="server" ImageUrl="~/Images/print.gif" ToolTip="พิมพ์" />
