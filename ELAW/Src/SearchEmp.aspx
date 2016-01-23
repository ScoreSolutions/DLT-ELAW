<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SearchEmp.aspx.vb" Inherits="Src_SearchEmp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title></title>
    <link href="../StyleSheet/StyleSheet.css" rel="stylesheet" type="text/css" /> 
    
       
    <style type="text/css">
        .style1
        {
            width: 55px;
        }
    </style>
    
       
</head>
<body>
    <form id="form1" runat="server">
      
        <ContentTemplate>
         <div class ="Black">
            <table class="form" border="0" cellpadding="1" 
        cellspacing="1" style="width: 706px">
                <tr>
                    <td class="HeaderGreen" colspan="2">
                        ค้นหาพนักงาน
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
                                <asp:TemplateField HeaderText="รหัส" Visible="False">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("empid") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("empid") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ชื่อ" SortExpression="fullname">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("fullname") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("fullname") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="เลือก">
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%# Eval("empid") %>'
                                    CommandName="Selectemp">
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
