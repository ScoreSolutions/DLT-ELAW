<%@ Control Language="VB" AutoEventWireup="false" CodeFile="TextDate.ascx.vb" Inherits="UserControl_TextDate" %>

<asp:TextBox ID="TextBox1" runat="server" AutoPostBack="false" CssClass="ssddl" 
    Width="100"  ></asp:TextBox>
<a href="javascript:NewCssCal('<%=TextBox1.ClientID %>','DDMMYYYY')" >
    <img src="../Images/cal.png"  border="0" 
    id="ImageButton1" runat="server" style="vertical-align:baseline;" /></a>
<asp:Label ID="Label1" runat="server" Text="*" ForeColor="Red"></asp:Label>