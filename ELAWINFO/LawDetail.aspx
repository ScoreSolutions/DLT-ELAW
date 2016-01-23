<%@ Page Language="VB" AutoEventWireup="false" CodeFile="LawDetail.aspx.vb" Inherits="LawDetail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">

    <title>ดาวน์โหลดไฟล์</title>
    <style type="text/css">
        .style1
        {
            width: 100%;
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
	BORDER-BOTTOM: #ffffff 1px solid;
	BORDER-LEFT: #ffffff 1px solid;
	BORDER-RIGHT: #ffffff 1px solid;
	BORDER-TOP: #ffffff 1px solid;
     
}
        </style>
    <script type="text/javascript">

        function openwindow(Page, Id) {
            window.open("../Src/" + Page + ".aspx?id=" + Id, "_blank", "location=no,status=no,resizable=yes,width=750,height=700,scrollbars=yes,menubar=no");
        }
        </script>


</head>

<body>

    <form id="form1" runat="server">
    
    <div>
    
        <table class="style1">
            <tr>
                <td colspan="2">
                    <asp:Label ID="lblDocName" runat="server"></asp:Label>
                    <asp:Label ID="lblDocCancel" runat="server" ForeColor="Red"></asp:Label>
                    <asp:LinkButton ID="LinkDownload" runat="server" style="font-weight: 700" 
                        Visible="False">ดาวน์โหลด</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td>
                                            <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="Medium" 
                                                Text="กฎหมายอื่นที่เกี่ยวข้อง"></asp:Label>
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
                            <asp:TemplateField ShowHeader="False">
                            <ItemStyle Width="25px"></ItemStyle>
                            <ItemTemplate>
                            <asp:ImageButton ID="ImageButton3" runat="server" CausesValidation="False" CommandArgument="1"
                            CommandName="Select" ImageUrl="~/Images/FindHS.png" Text="Select" ToolTip="คลิกเพื่อดูกฎหมายที่เกี่ยวข้อง" />

                            </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="ชื่อเอกสาร">
                            <ItemTemplate>
                            <asp:Label ID="lblLinkFile" runat="server" Font-Bold="False" ForeColor="#330099"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="30%" />
                            </asp:TemplateField>
                             
                            <asp:BoundField>
                            <ItemStyle Width="70%"></ItemStyle>
                            </asp:BoundField>
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
                    <asp:GridView ID="gDetail" runat="server" AllowPaging="True" 
                        AllowSorting="True" AutoGenerateColumns="False" CellPadding="1" 
                        CssClass="GridViewStyle" PageSize="20" Width="100%" ForeColor="#333333" 
                        GridLines="None" ShowHeader="False" BorderStyle="None" 
        >
                        <RowStyle BackColor="#E1EAF7" />
                        <Columns>
                            
                            <asp:TemplateField HeaderText="ชื่อเอกสาร">
                            <ItemTemplate>
                            <asp:LinkButton ID="lblLinkFileDetail" runat="server" Font-Bold="False" 
                                    ForeColor="#330099" ></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle Width="90%" />
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="ดาวน์โหลด">
                            <ItemTemplate>
                            <asp:Label ID="lblLinkDownloadDetail" runat="server" Font-Bold="False" 
                                    ForeColor="#330099"></asp:Label>
                            </ItemTemplate>
                                 <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle Width="10%" HorizontalAlign="Center" />
                            </asp:TemplateField>
                             
                        </Columns>
                        <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#E1EAF7" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#2461BF" />
                        <AlternatingRowStyle BackColor="#E1EAF7" />
                    </asp:GridView>
    </form>
</body>
</html>
