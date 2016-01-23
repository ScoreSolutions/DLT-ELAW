﻿<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AddGuarantee.aspx.vb" Inherits="Src_AddGuarantee" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">

table.form 
{
	/*background-color: #FFFFFF;*/
	width:100%;
    height: 36px;
}

table.form 
{
	width:100%;
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

.sslbl
{
    font: 12px Arial,"Courier New", Courier, monospace;
	color: #000000;
	font-weight :normal;
	text-decoration: none;
	vertical-align:top ;
	padding: 1px;
   	
}
.ssddl 
{
	font: 12px Arial,"Courier New", Courier, monospace;
	color: #000000;
	font-weight :normal;
	text-decoration: none;
	vertical-align:top ;
	padding: 1px;
}

.sslbl_red
{
    font: 12px Arial,"Courier New", Courier, monospace;
	color: Red;
	font-weight :normal;
	padding: 1px;
   	
}



        .style10
        {
            font-family: "MS Sans Serif", "Microsoft Sans Serif", AngsanaUPC;
            font-size: 14px;
            color: #FFFFFF;
            font-weight : bold;
            text-decoration: none;
            vertical-align: text-top;
            width: 97px;
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
	font: 12px Arial,"Courier New", Courier, monospace;
	padding-left: 5px;
	padding-right: 5px;
	padding-top: 4px;
	padding-bottom: 4px;
	color: Black;
	background-image: url(../images/th_bg.gif);
    
}


    </style>
</head>
<body>
    <form id="form1" runat="server">

            <table class="form" border="0" cellpadding="1" 
        cellspacing="1">
                <tr>
                    <td class="HeaderGreen" colspan="2">
                        หลักประกันสัญญา</td>
                </tr>
                <tr>
                    <td class="sslbl">
                        หลักประกันสัญญา</td>
                    <td class="sslbl">
                        <asp:TextBox ID="txtStatus" runat="server" Width="500px" MaxLength="250" 
                            CssClass="ssddl"></asp:TextBox>
                        <asp:Label ID="lblStatus" runat="server" Visible="False"></asp:Label>
                        <asp:Label ID="lblId" runat="server" Visible="False"></asp:Label>
                        &nbsp;
                        <asp:Label ID="lblGuarantee" runat="server" CssClass="sslbl_red"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style10">
                        &nbsp;</td>
                    <td class="sslbl">
                        <asp:Button ID="bSave" runat="server" CssClass="ssbtn" Text="บันทึก" 
                            Width="80px" />
                        <asp:Button ID="bCancel" runat="server" CssClass="ssbtn" Text="ยกเลิก" 
                            Width="80px" />
                        <asp:Button ID="bSearch" runat="server" CssClass="ssbtn" Text="ค้นหา" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" width="100%">
                        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
                            AllowSorting="True" AutoGenerateColumns="False" BackColor="White" 
                            BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="1" 
                            CssClass="GridViewStyle" GridLines="Vertical" PageSize="20" Width="100%">
                            <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                            <Columns>
                                <asp:TemplateField HeaderText="หลักประกันสัญญา" SortExpression="guarantee_name">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("guarantee_name") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("guarantee_name") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="40%" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="creation_by" HeaderText="ชื่อผู้สร้าง" 
                                    SortExpression="creation_by">
                                    <ItemStyle Width="20%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="created_date" DataFormatString="{0:dd/MM/yyyy}" 
                                    HeaderText="วันที่สร้าง" HtmlEncode="False" SortExpression="created_date">
                                    <ItemStyle Width="10%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="updated_by" HeaderText="ชื่อผู้แก้ไข" 
                                    SortExpression="updated_by">
                                    <ItemStyle Width="20%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="updated_date" DataFormatString="{0:dd/MM/yyyy}" 
                                    HeaderText="วันที่แก้ไข" SortExpression="updated_date">
                                    <ItemStyle Width="10%" />
                                </asp:BoundField>
                                <asp:TemplateField ShowHeader="False">
                                    <ItemStyle Width="15px" />
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" 
                                            CommandName="Delete" ImageUrl="~/Images/DeleteFolderHS.png" Text="Delete" 
                                            ToolTip="ลบ" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ShowHeader="False">
                                    <EditItemTemplate>
                                        <asp:ImageButton ID="ImageButton3" runat="server" CausesValidation="True" 
                                            CommandName="Update" ImageUrl="~/Image/save.png" Text="Update" />
                                        <asp:ImageButton ID="ImageButton2" runat="server" CausesValidation="False" 
                                            CommandName="Cancel" ImageUrl="~/Images/cancel2.bmp" Text="Cancel" />
                                    </EditItemTemplate>
                                    <ItemStyle Width="15px" />
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImageButton4" runat="server" CausesValidation="False" 
                                            CommandName="Edit" ImageUrl="~/Images/Edit.gif" Text="Edit" 
                                            ToolTip="แก้ไข" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                            <AlternatingRowStyle BackColor="#DCDCDC" />
                        </asp:GridView>
                    </td>
                </tr>
            </table>

    <br />
    </form>
</body>
</html>
