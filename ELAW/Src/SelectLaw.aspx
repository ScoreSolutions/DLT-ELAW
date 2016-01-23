<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SelectLaw.aspx.vb" Inherits="Src_SelectLaw" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../StyleSheet/StyleSheet.css" rel="stylesheet" type="text/css" /> 
   
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate >
    
    
        <table width="706px">
            <tr>
                <td class="HeaderGreenTab" colspan="2">
                    ข้อมูลกฎหมาย</td>
            </tr>
            <tr>
                <td class="sslbl_gray" width="15%">
                    ประเภทกฎหมาย</td>
                <td class="sslbl_gray">
                    <asp:DropDownList ID="ddlType" runat="server" CssClass="ssddl" Height="20px" 
                        Width="300px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="sslbl_gray" width="15%">
                    เรื่อง</td>
                <td class="sslbl_gray">
                    <asp:TextBox ID="txtTitle" runat="server" CssClass="ssddl" Width="500px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="sslbl_gray" width="15%">
                    คำค้นหา</td>
                <td class="sslbl_gray">
                    <asp:TextBox ID="txtKeyword" runat="server" CssClass="ssddl" Width="500px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="sslbl_gray" width="15%">
                    &nbsp;</td>
                <td class="sslbl_gray">
                    <asp:Button ID="bSave" runat="server" CssClass="ssbtn" Text="ค้นหา" 
                        Width="80px" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
                        AllowSorting="True" AutoGenerateColumns="False" BackColor="White" 
                        BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="1" 
                        CssClass="GridViewStyle" GridLines="Vertical" PageSize="20" Width="100%">
                        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                        <Columns>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:CheckBox ID="cb1" runat="server" AutoPostBack="true" 
                                        OnCheckedChanged="cb1_Checked" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    &nbsp;<asp:CheckBox ID="cb1" runat="server" />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle Width="1%" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="subtype_name" HeaderText="ประเภทเอกสาร" 
                                SortExpression="subtype_name">
                                <ItemStyle Width="20%" />
                            </asp:BoundField>
                         <asp:TemplateField HeaderText="ชื่อเอกสาร">
                            <ItemTemplate>
                            <asp:Label ID="lblLinkDoc" runat="server" Text=""></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="45%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="คำค้นหา">
                            <ItemTemplate>
                            <asp:Label ID="lblLink" runat="server" Text=""></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="30%" />
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
    
    
    </ContentTemplate>
    </asp:UpdatePanel>
    </div>
    <p>
                <table class="style1" width ="706px">
                    <tr>
                        <td align="center" >
                    <asp:Button ID="bSelect" runat="server" CssClass="ssbtn" Text="เลือก" 
                        Width="80px" />
                        </td>
                    </tr>
        </table>
    </p>
    </form>
</body>
</html>
