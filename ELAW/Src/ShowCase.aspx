<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ShowCase.aspx.vb" Inherits="Src_ShowCase" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
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
.HeaderGreen
{
	font-family: "MS Sans Serif", "Microsoft Sans Serif", AngsanaUPC;
	color:#4D4D4D;
	font-weight:bold;
	font-size: 16px;
	background-color:#CAEBF4;
	height :25px;
	padding: 2px;
    width: 100%;
}
.sslbl_right
{
    font-family: "MS Sans Serif", "Microsoft Sans Serif", AngsanaUPC;
 	font-size: 14px;
	color: #000000;
	font-weight :normal;
	text-decoration: none;
	vertical-align:top ;
	padding: 1px;
	text-align :right ;
   	
}
.ssddl 
{
	font-family: "MS Sans Serif", "Microsoft Sans Serif", AngsanaUPC;
  	font-size: 13px;
	color: #000000;
	font-weight :normal;
	text-decoration: none;
	vertical-align:top ;
	padding: 1px;
}

.ssbtn {
	font-family: "MS Sans Serif", "Microsoft Sans Serif", AngsanaUPC;
	font-size: 14px;
	text-decoration: none;
	width: 70px;
	cursor:hand;
}

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
	/*BORDER-BOTTOM: #ffffff 1px solid;
	BORDER-LEFT: #ffffff 1px solid;
	BORDER-RIGHT: #ffffff 1px solid;
	BORDER-TOP: #ffffff 1px solid;*/
     
}
.GridViewStyle a
{    font-weight: 700;
}

    </style>
</head>
<body>
    <form id="form1" runat="server">
      
        <ContentTemplate>
         <div class ="style2">
            <table class="style2" border="0" cellpadding="1" 
        cellspacing="1" width ="100%">
                <tr>
                    <td class="HeaderGreen" colspan="2">
                        ค้นหาคดี</td>
                </tr>
                <tr>
                    <td  bgcolor="#EBF1F3" class="sslbl_right">
                        ประเภทคดี</td>
                    <td  bgcolor="#EBF1F3">
                    <asp:DropDownList ID="ddlType" runat="server" CssClass="ssddl" Width="255px">
                    </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td  bgcolor="#EBF1F3" class="sslbl_right">
                        ค้นหา</td>
                    <td  bgcolor="#EBF1F3">
                        <asp:TextBox ID="txtName" runat="server" Width="500px" MaxLength="128" 
                            CssClass="ssddl" Height="19px"></asp:TextBox>
                        <asp:Label ID="lblStatus" runat="server"></asp:Label>
                        <asp:Label ID="lblId" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td  bgcolor="#EBF1F3" class="style1">
                        &nbsp;</td>
                    <td  bgcolor="#EBF1F3">
                        <asp:Button ID="bSearch" runat="server" CssClass="ssbtn" Text="ค้นหา" 
                    Width="80px" />
                        <asp:Button ID="bCancel" runat="server" CssClass="ssbtn" Text="ยกเลิก" 
                    Width="80px" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center" class="style3">
                        <asp:GridView ID="GridView1" runat="server" AllowSorting="True" 
                    AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" 
                    BorderStyle="None" BorderWidth="1px" CellPadding="1" 
                    CssClass="GridViewStyle" GridLines="Vertical" PageSize="20" 
                    AllowPaging="True" Width="100%">
                            <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                            <Columns>
                               <asp:BoundField DataField="type_name" HeaderText="ประเภทคดี" SortExpression="type_name" />
                                <asp:TemplateField HeaderText="เลขที่คดี" SortExpression="case_id" Visible ="false" >
                                   
                                    <ItemTemplate>
                                        <asp:Label ID="lblBlack" runat="server" Text='<%# Bind("case_id") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="คดีแดง" SortExpression="all_no" >
                                   
                                    <ItemTemplate>
                                        <asp:Label ID="lblRed" runat="server" Text='<%# Bind("all_no") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                               
                                <asp:TemplateField HeaderText="เลือก">
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkCaseNo" runat="server" CommandArgument='<%# Eval("case_id") %>'
                                    CommandName="SelectTitle">
         Select</asp:LinkButton>
                            </ItemTemplate>
                                    <ItemStyle Width="10%" />
                        </asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                            <PagerStyle BackColor="#999999" ForeColor="Black" 
                        HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" 
                        ForeColor="White" />
                            <HeaderStyle BackColor="#000084" Font-Bold="True" 
                        ForeColor="White" />
                            <AlternatingRowStyle BackColor="#DCDCDC" />
                        </asp:GridView>
                    </td>
                </tr>
            </table>
    </div>   
        </ContentTemplate>
    </form>
</body>
</html>
