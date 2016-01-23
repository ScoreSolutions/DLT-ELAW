<%@ Page Language="VB" AutoEventWireup="false" CodeFile="LawDetail2.aspx.vb" Inherits="Src_LawDetail2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">

        .GridViewStyle
{   
    font-family: "MS Sans Serif", "Microsoft Sans Serif", AngsanaUPC;
    font-size:14px;
    border: 1px solid gray;
	padding: 4px;
	height:auto;
	text-align: left;
	
}
th {
	background-color: #007DB3;
	font-size: 14px;
	font-family: "MS Sans Serif", "Microsoft Sans Serif", AngsanaUPC;
	padding-left: 5px;
	padding-right: 5px;
	padding-top: 4px;
	padding-bottom: 4px;
	color: Black;
	background-image: url(../images/th_bg.gif);
	BORDER-BOTTOM: #ffffff 1px solid;
	BORDER-LEFT: #ffffff 1px solid;
	BORDER-RIGHT: #ffffff 1px solid;
	BORDER-TOP: #ffffff 1px solid;
     
}
        .style1
        {
            width: 100%;
        }
        </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <table class="style1">
            <tr>
                <td>
                                            <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="Medium" 
                                                Text="ดาวน์โหลดไฟล์"></asp:Label>
                                        </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
                        AllowSorting="True" AutoGenerateColumns="False" CellPadding="1" 
                        CssClass="GridViewStyle" PageSize="20" Width="100%" ForeColor="#333333" 
                        GridLines="None">
                        <RowStyle BackColor="White" />
                        <Columns>
                            
                            <asp:BoundField DataField="subtype_name" HeaderText="ประเภท" ItemStyle-Width="40%">
<ItemStyle Width="40%"></ItemStyle>
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="ชื่อเอกสาร">
                            <ItemTemplate>
                            <asp:Label ID="lblLinkFile" runat="server" Font-Bold="False" ForeColor="#330099"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="50%" />
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="ดาวน์โหลด">
                            <ItemTemplate>
                            <asp:Label ID="lblLinkDownload" runat="server" Font-Bold="False" ForeColor="#330099"></asp:Label>
                            </ItemTemplate>
                                 <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle Width="10%" HorizontalAlign="Center" />
                            </asp:TemplateField>
                             
                        </Columns>
                        <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#2461BF" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
    
    </div>
    </form>

</body>
</html>
