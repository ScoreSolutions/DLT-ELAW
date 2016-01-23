<%@ Control Language="VB" AutoEventWireup="false" CodeFile="DatePicker.ascx.vb" Inherits="UserControl_DatePicker" %>

<asp:TextBox ID="txtDate" runat="server" AutoPostBack="false" CssClass="ssddl" 
    Width="100px"  ></asp:TextBox>
<a href="javascript:NewCssCal('<%=txtDate.ClientID %>','DDMMYYYY')" >
    <img src="../Images/cal.png"  border="0" 
    id="imgButton" runat="server" style="vertical-align:baseline;" /></a>
<asp:Label ID="lblChkDate1" runat="server" Text="*" ForeColor="Red"></asp:Label>