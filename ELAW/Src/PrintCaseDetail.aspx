<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PrintCaseDetail.aspx.vb" Inherits="Src_PrintCaseDetail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>พิมพ์รายละเอียด</title>
    <style type="text/css">
body, div, p, h1, h2, h3, h4, ul, li, table,Textarea
{
	border-style: none;
    border-color: inherit;
    border-width: medium;
    margin: 0;
    font-family: Microsoft Sans Serif,Microsoft Sans Serif,Tahoma, Arial, sans-serif;
    /*font-family: "MS Sans Serif", "Microsoft Sans Serif", AngsanaUPC;*/
	font-size: 8pt;
  
}
table.form 
{
	/*background-color: #FFFFFF;*/
	width:100%;
    height: 36px;
}
table.form 
{
	/*background-color: #FFFFFF;*/
	width:100%;
}
 
.sslbl
{
    font-family: "MS Sans Serif", "Microsoft Sans Serif", AngsanaUPC;
  
	font-size: 14px;
	color: #000000;
	font-weight :normal;
	text-decoration: none;
	vertical-align:top ;
	padding: 1px;
   	
}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    <div align="left">
        <table class="form" cellpadding="1" cellspacing="1" frame="border" 
        border="0" width="700">
            <tr>
                <td class="" colspan="3" align="right">
                                            <asp:ImageButton ID="ImageButton1" runat="server" 
                                                ImageUrl="~/images/print.gif" />
                </td>
            </tr>
            <tr>
                <td class="" colspan="3" align="center" width="100%">
                    <asp:Label ID="Label10" runat="server" Font-Names="Cordia New" Font-Size="33pt" 
                        Text="บันทึกข้อความ" Font-Bold="True" ></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style3" align ="left" width="33%"  >
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/HeadSmall.png" />
                </td>
                <td class="style4" align ="right" width="33%"  >
                    &nbsp;</td>
                <td class="sslbl" align ="left" width="33%" >
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style3" align ="left" colspan="3" style="width: 66%"  >
                    <asp:Label ID="Label7" runat="server" Font-Names="Cordia New" Font-Size="19pt"  Font-Bold ="true" 
                        Text="ส่วนราชการ"  ></asp:Label>
                &nbsp;<asp:Label ID="Label8" runat="server" Font-Names="Cordia New" Font-Size="16pt" 
                        Text="สำนักกฎหมาย   โทร. 02 271 8791" Font-Overline="False" 
                        Font-Underline="False"  ></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style3" align ="left" colspan="3" >
                    <asp:Label ID="lblDetail" runat="server" Font-Names="Cordia New" 
                        Font-Size="16pt"><%#BindField("detail")%></asp:Label>
                </td>
            </tr>
            </table>
    </div>
    
    </div>
    </form>
</body>
</html>
