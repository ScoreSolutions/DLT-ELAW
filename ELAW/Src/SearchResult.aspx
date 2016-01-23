<%@ Page Language="VB" AutoEventWireup="false"  CodeFile="SearchResult.aspx.vb" Inherits="Src_SearchResult" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ค้นหาเอกสาร</title>
    <style type="text/css">
        .style1
        {
            color: buttontext;
            font-family: "Microsoft Sans Serif", "Microsoft Sans Serif", Tahoma, Arial, sans-serif;
            font-size: 8pt;
            border-style: none;
            border-color: inherit;
            border-width: medium;
            margin: 0;
            padding: 0;
            background-color: buttonface;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" 
                    enablescriptglobalization="True">
                </cc1:ToolkitScriptManager>
                <asp:Button ID="Button1" runat="server" Text="Button" />
            </ContentTemplate>
        </asp:UpdatePanel>
    
    </div>
    </form>
</body>
</html>
