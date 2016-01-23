<%@ Page Title="แก้ไขข้อมูลสัญญา-LAW4204" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="ContractEditList.aspx.vb" Inherits="Src_ContractEditList" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table class="form" border="0" cellpadding="1" cellspacing="1">
                <tr>
                    <td class="HeaderGreen" colspan="2">
                        <asp:LinkButton ID="linkHome" runat="server">Home</asp:LinkButton>
                        &nbsp;&gt;
                        <asp:LinkButton ID="link1" runat="server">งานสัญญา</asp:LinkButton>
                        &nbsp;&gt;
                        <asp:LinkButton ID="link2" runat="server">แก้ไขข้อมูลสัญญา</asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td class="sslbl_right">
                        ประเภทสัญญา&nbsp;</td>
                    <td>
                        <asp:DropDownList ID="ddlContract" runat="server" 
                    Height="20px" Width="300px" CssClass="ssddl">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="sslbl_right">
                        เลขที่สัญญา</td>
                    <td>
                        <asp:TextBox ID="txtNo" runat="server" CssClass="ssddl" MaxLength="16"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="sslbl_right">
                        สิ่งที่จะซื้อ/จ้าง</td>
                    <td>
                        <asp:TextBox ID="txtMatetial" runat="server" CssClass="ssddl" Width="300px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="sslbl_right">
                        เลขประจำตัวผู้เสียภาษีของคู่สัญญา</td>
                    <td>
                        <asp:TextBox ID="txtTaxId" runat="server" CssClass="ssddl" Width="300px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="sslbl_right">
                        ชื่อสัญญา</td>
                    <td>
                        <asp:TextBox ID="txtContractName" runat="server" CssClass="ssddl" Width="600px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="sslbl_right">
                        ชื่อผู้ขาย/ผู้รับจ้าง</td>
                    <td>
                        <asp:TextBox ID="txtSaleName" runat="server" CssClass="ssddl" Width="600px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="sslbl_right">
                        คำค้นหา</td>
                    <td>
                        <asp:TextBox ID="txtKeyword" runat="server" CssClass="ssddl" Width="600px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="sslbl_right">&nbsp;
                        </td>
                    <td>
                        <asp:Button ID="bSearch" runat="server" CssClass="ssbtn" Text="ค้นหา" />
                    </td>
                </tr>
            </table>
            <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
                AllowSorting="True" AutoGenerateColumns="False" BackColor="White" 
                BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="1" 
                CssClass="GridViewStyle" GridLines="Vertical" PageSize="20" Width="100%">
                <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Literal ID="SHOW" runat="server" Text='<%#ImagesGet(Eval("warning")) %>'></asp:Literal>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="30px" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="contract_no" HeaderText="เลขที่สัญญา" 
                        SortExpression="contract_no" ItemStyle-Width="13%" />
                    <asp:BoundField DataField="subtype_name" HeaderText="ประเภทสัญญา(ชื่อสัญญา)" 
                        SortExpression="subtype_name" ItemStyle-Width="25%" />
                    <asp:BoundField DataField="dates_recieve" DataFormatString="{0:dd/MM/yy}" 
                        HeaderText="รับเรื่อง" HtmlEncode="False" 
                        SortExpression="dates_recieve" ItemStyle-Width="7%" />
                    <asp:BoundField DataField="status_name" HeaderText="สถานะ" 
                        SortExpression="status_name" ItemStyle-Width="15%" />
                    <asp:BoundField DataField="material" HeaderText="สิ่งที่ซื้อ/จ้าง" 
                        SortExpression="material" ItemStyle-Width="15%" />
                    <asp:BoundField DataField="user_sale" HeaderText="ชื่อผู้ขาย/ผู้รับจ้าง" 
                        SortExpression="user_sale" ItemStyle-Width="15%" />
                    <asp:BoundField DataField="creation_name" HeaderText="เจ้าของเรื่อง" 
                        SortExpression="creation_name" ItemStyle-Width="10%" />
                    <asp:CommandField EditText="เลือก" ShowEditButton="True">
                    <ItemStyle Font-Bold="True" ForeColor="#000099" HorizontalAlign="Center" />
                    </asp:CommandField>
                </Columns>
                <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                <EmptyDataTemplate>
                    <table width ="100%">
                        <tr>
                            <td align="center">
                                <asp:Label ID="Label8" runat="server" CssClass="sslbl_red" 
                                    Text="***ไม่พบข้อมูล***"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                <AlternatingRowStyle BackColor="#DCDCDC" />
            </asp:GridView>

        </ContentTemplate>
    </asp:UpdatePanel>
    </asp:Content>

