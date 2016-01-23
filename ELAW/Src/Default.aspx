<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="Src_Default" %>


<%@ Register src="../UserControl/TextBox.ascx" tagname="TextBox" tagprefix="uc2" %>


<%@ Register src="../UserControl/ComboBox.ascx" tagname="ComboBox" tagprefix="uc1" %>



<%@ Register src="../UserControl/TextDate.ascx" tagname="TextDate" tagprefix="uc3" %>



<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>



<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../StyleSheet/StyleSheet.css" rel="stylesheet" type="text/css" /> 
     <link href="../StyleSheet/StyleClass.css" rel="stylesheet" type="text/css" /> 
    <script type="text/javascript" language="javascript" src="../scripts/novajava.js"></script>
    <script type="text/javascript" language="javascript" src="../scripts/JScript.js"></script>
    <script type="text/javascript" language="javascript" src="../scripts/datetimepicker_css.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
    
    
    </div>
    
    <uc2:TextBox ID="txtTest" runat="server" TextType="TextBox" TextAlign="AlignRight" TextKey="TextDouble" IsNotNull="true" />

    <uc1:ComboBox ID="ComboBox1" runat="server" />
    <uc3:TextDate ID="TextDate1" runat="server" />
    <cc1:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" 
        Width="277px">
        <cc1:TabPanel runat="server" HeaderText="H1" ID="TabPanel1" TabIndex="0">
        </cc1:TabPanel>
        <cc1:TabPanel runat="server" HeaderText="H2" ID="TabPanel2">
        </cc1:TabPanel>
    </cc1:TabContainer>
    </form>
</body>
</html>
