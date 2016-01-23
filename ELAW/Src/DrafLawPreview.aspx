<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DrafLawPreview.aspx.vb" Inherits="Src_DrafLawPreview" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>แสดงตัวอย่างร่างกฎหมาย</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Panel ID="pnlPreview" runat="server" Height="580px" Width="100%" ScrollBars="Auto" BorderWidth="1" BorderColor="Black" BackColor="#FFFFFF" >
                <asp:Label ID="lblPreview" runat="server" Text=""></asp:Label>
            </asp:Panel>
        </div>
    </form>
</body>
</html>
