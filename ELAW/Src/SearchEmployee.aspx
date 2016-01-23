<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="SearchEmployee.aspx.vb" Inherits="Src_SearchEmployee" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
	
    <style type="text/css">


        .style8
        {
            font-family: "MS Sans Serif", "Microsoft Sans Serif", AngsanaUPC;
            font-size: 14px;
            color: #000000;
            font-weight : bold;
            text-decoration: none;
            vertical-align: text-top;
            height: 28px;
        }
        .style9
        {
            height: 28px;
        }
    </style>



</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script language="JavaScript" type="text/javascript" src="../cbrte/html2xhtml.js" ></script>
	<script language="JavaScript" type="text/javascript" src="../cbrte/richtext_compressed.js"></script>
<script type="text/javascript" language="javascript">
    document.oncontextmenu = MouseHandler;
    function MouseHandler() {
        return false;
    }

</script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table class="form" border="1" cellpadding="0" 
        cellspacing="0">
                <tr>
                    <td class="HeaderGreen" colspan="2">
                        ค้นหาพนักงาน<cc1:toolkitscriptmanager ID="ToolkitScriptManager1" runat="server" 
                    enablescriptglobalization="True">
                        </cc1:toolkitscriptmanager>
                    </td>
                </tr>
                <tr>
                    <td class="sslbl">
                        สถานะสัญญา</td>
                    <td class="sslbl">
                        <asp:TextBox ID="txtStatus" runat="server" Width="500px" MaxLength="128"></asp:TextBox>
                        <asp:Label ID="lblStatus" runat="server"></asp:Label>
                        <asp:Label ID="lblId" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style8">
                    </td>
                    <td class="style9">
                        <asp:Button ID="bSave" runat="server" CssClass="ssbtn" Text="บันทึก" 
                    Width="80px" />
                        <asp:Button ID="bCancel" runat="server" CssClass="ssbtn" Text="ยกเลิก" 
                    Width="80px" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" width="100%">
                        <asp:GridView ID="GridView1" runat="server" AllowSorting="True" 
                    AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" 
                    BorderStyle="None" BorderWidth="1px" CellPadding="1" 
                    CssClass="GridViewStyle" GridLines="Vertical" PageSize="20" 
                    AllowPaging="True">
                            <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                            <Columns>
                                <asp:BoundField DataField="firstname" HeaderText="ชื่อ" 
                                    SortExpression="firstname" />
                                <asp:BoundField DataField="lastname" HeaderText="สกุล" 
                                    SortExpression="lastname" />
                                <asp:CommandField ShowSelectButton="True" />
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

