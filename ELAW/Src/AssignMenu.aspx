<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="AssignMenu.aspx.vb" Inherits="Src_AssignMenu" %>

<%@ Register src="../UserControl/DatePicker.ascx" tagname="DatePicker" tagprefix="uc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate >
    
        <table border="1" cellpadding="0" cellspacing="0" class="form">
            <tr>
                <td class="HeaderGreen" title="ผู้ใช้งาน-LAW9102" valign="bottom">
                    มอบหมายงาน</td>
            </tr>
            <tr>
                <td align="right" title="ผู้ใช้งาน-LAW9102" valign="bottom">
                    <asp:Label ID="lblFrom" runat="server" CssClass="ssddl"></asp:Label>
                    &nbsp;--&gt;
                    <asp:Label ID="lblTo" runat="server" CssClass="ssddl"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="left" class="sslbl" title="ผู้ใช้งาน-LAW9102" valign="bottom">
                    จากวันที่
                    <uc1:DatePicker ID="txtDateFrom" runat="server" IsNotNull="false" />
                    &nbsp;ถึงวันที่
                    <uc1:DatePicker ID="txtDateTo" runat="server" IsNotNull="false" />
                    <asp:DropDownList ID="ddlDay" runat="server" Visible="False">
                        <asp:ListItem>1</asp:ListItem>
                        <asp:ListItem>2</asp:ListItem>
                        <asp:ListItem>3</asp:ListItem>
                        <asp:ListItem>4</asp:ListItem>
                        <asp:ListItem>5</asp:ListItem>
                        <asp:ListItem>6</asp:ListItem>
                        <asp:ListItem>7</asp:ListItem>
                        <asp:ListItem>8</asp:ListItem>
                        <asp:ListItem>9</asp:ListItem>
                        <asp:ListItem>10</asp:ListItem>
                        <asp:ListItem>11</asp:ListItem>
                        <asp:ListItem>12</asp:ListItem>
                        <asp:ListItem>13</asp:ListItem>
                        <asp:ListItem>14</asp:ListItem>
                        <asp:ListItem>15</asp:ListItem>
                        <asp:ListItem>16</asp:ListItem>
                        <asp:ListItem>17</asp:ListItem>
                        <asp:ListItem>18</asp:ListItem>
                        <asp:ListItem>19</asp:ListItem>
                        <asp:ListItem>20</asp:ListItem>
                        <asp:ListItem>21</asp:ListItem>
                        <asp:ListItem>22</asp:ListItem>
                        <asp:ListItem>23</asp:ListItem>
                        <asp:ListItem>24</asp:ListItem>
                        <asp:ListItem>25</asp:ListItem>
                        <asp:ListItem>26</asp:ListItem>
                        <asp:ListItem>27</asp:ListItem>
                        <asp:ListItem>28</asp:ListItem>
                        <asp:ListItem>29</asp:ListItem>
                        <asp:ListItem>30</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="style9" title="มอบหมายงาน-LAW9102">
                    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
                        AllowSorting="True" AutoGenerateColumns="False" BackColor="White" 
                        BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="1" 
                        CssClass="GridViewStyle" GridLines="Vertical" PageSize="20" Width="100%">
                        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                        <Columns>
                            <asp:TemplateField HeaderText="เมนู" SortExpression="menu_name">
                                <ItemTemplate>
                                    <asp:Label ID="lblMd" runat="server" Text='<%# Bind("menu_name") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="70%" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="date_from" DataFormatString="{0:dd/MM/yyyy}" 
                                HeaderText="จากวันที่" HtmlEncode="False" ItemStyle-Width="15%" 
                                SortExpression="date_from" />
                            <asp:BoundField DataField="date_to" DataFormatString="{0:dd/MM/yyyy}" 
                                HeaderText="ถึงวันที่" HtmlEncode="False" ItemStyle-Width="15%" 
                                SortExpression="date_to" />
                            <asp:TemplateField HeaderText="เลือก">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkAlert" runat="server" CommandName="Select" 
                                        Text='<%#ImagesGet(Eval("chk")) %>' ToolTip="เปิด/ปิด">
                                    </asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
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
    </asp:Content>

