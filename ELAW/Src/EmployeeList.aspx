<%@ Page Title="ข้อมูลผุ้ใช้งาน-LAW8210" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="EmployeeList.aspx.vb" Inherits="Src_EmployeeList" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <table class="form" border="0" cellpadding="1" 
        cellspacing="1">
            <tr>
                <td class="HeaderGreen" colspan="2">
                    ข้อมูลผู้ใช้งาน</td>
            </tr>
            <tr>
                <td class="sslbl_right" title="ผู้ใช้งาน-LAW9102">
                    ชื่อ&nbsp;</td>
                <td class="style9" title="ผู้ใช้งาน-LAW9102">
                    <asp:TextBox ID="txtFirstName" runat="server" CssClass="ssddl" MaxLength="128" 
                        Width="343px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="sslbl_right" title="ผู้ใช้งาน-LAW9102">
                    นามสกุล</td>
                <td class="style9" title="ผู้ใช้งาน-LAW9102">
                    <asp:TextBox ID="txtLastName" runat="server" CssClass="ssddl" MaxLength="128" 
                        Width="343px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="sslbl_right" title="ผู้ใช้งาน-LAW9102">
                    &nbsp;</td>
                <td class="style9" title="ผู้ใช้งาน-LAW9102">
                    <asp:Button ID="bSearch" runat="server" CssClass="ssbtn" Text="ค้นหา" />
                </td>
            </tr>
            <tr>
                <td class="style9" title="ผู้ใช้งาน-LAW9102" colspan="2">
                        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
                            AllowSorting="True" AutoGenerateColumns="False" BackColor="White" 
                            BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="1" 
                            CssClass="GridViewStyle" GridLines="Vertical" PageSize="20" Width="100%">
                            <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                            <Columns>
                                <asp:TemplateField HeaderText="รหัส" SortExpression="empid" Visible="False">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("empid") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("empid") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="10%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="เพศ" SortExpression="sex" Visible="False">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("sex") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("sex") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="5%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="คำนำหน้าชื่อ" SortExpression="prefix" 
                                    Visible="False">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextPrefix" runat="server" Text='<%# Bind("prefix") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="LabelPrefix" runat="server" Text='<%# Bind("prefix") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="5%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ชื่อ" SortExpression="firstname">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextFirstname" runat="server" Text='<%# Bind("firstname") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="LabelFirstname" runat="server" Text='<%# Bind("firstname") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="20%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="นามสกุล" SortExpression="lastname">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextLastname" runat="server" Text='<%# Bind("lastname") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="LabelLastname" runat="server" Text='<%# Bind("lastname") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="20%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="วันเกิด" SortExpression="birthday" 
                                    Visible="False">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBirthday" runat="server" Text='<%# Bind("birthday") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="LabelBirthday" runat="server" Text='<%# Bind("birthday") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="5%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="เบอร์โทรศัพท์บ้าน" SortExpression="phonehome" 
                                    Visible="False">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextPhonehome" runat="server" Text='<%# Bind("phonehome") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="LabelPhonehome" runat="server" Text='<%# Bind("phonehome") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="5%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="เบอร์ที่ทำงาน" SortExpression="phonework" 
                                    Visible="False">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextPhonework" runat="server" Text='<%# Bind("phonework") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="LabelPhonework" runat="server" Text='<%# Bind("phonework") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="10%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="เบอร์มือถือ" SortExpression="phonemobile" 
                                    Visible="False">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextPhonemobile" runat="server" 
                                            Text='<%# Bind("phonemobile") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="LabelPhonemobile" runat="server" 
                                            Text='<%# Bind("phonemobile") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="10%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="อีเมล์" SortExpression="email" Visible="False">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextMail" runat="server" Text='<%# Bind("email") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="LabelMail" runat="server" Text='<%# Bind("email") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="10%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="หน่วยงาน" SortExpression="dept_id">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextDept" runat="server" Text='<%# Bind("dept_name") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="LabelDept" runat="server" Text='<%# Bind("dept_name") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="20%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ส่วนงาน" SortExpression="div_id">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextDiv" runat="server" Text='<%# Bind("div_name") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="LabelDiv" runat="server" Text='<%# Bind("div_name") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="20%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ตำแหน่ง" SortExpression="pos_id">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextPos" runat="server" Text='<%# Bind("pos_name") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="LabelPos" runat="server" Text='<%# Bind("pos_name") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="20%" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="creation_by" HeaderText="ชื่อผู้สร้าง" 
                                    SortExpression="creation_by" Visible="False">
                                    <ItemStyle Width="10%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="created_date" DataFormatString="{0:dd/MM/yyyy}" 
                                    HeaderText="วันที่สร้าง" HtmlEncode="False" SortExpression="created_date" 
                                    Visible="False">
                                    <ItemStyle Width="10%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="updated_by" HeaderText="ชื่อผู้แก้ไข" 
                                    SortExpression="updated_by" Visible="False">
                                    <ItemStyle Width="10%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="updated_date" DataFormatString="{0:dd/MM/yyyy}" 
                                    HeaderText="วันที่แก้ไข" SortExpression="updated_date" Visible="False">
                                    <ItemStyle Width="10%" />
                                </asp:BoundField>
                                <asp:TemplateField ShowHeader="False" Visible="False">
                                    <ItemStyle Width="15px" />
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" 
                                            CommandName="Delete" ImageUrl="~/Images/DeleteFolderHS.png" Text="Delete" 
                                            ToolTip="ลบ" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                   <asp:TemplateField ShowHeader="False">
                                    <ItemStyle Width="15px" />
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImageButtonEdit" runat="server" CausesValidation="False" 
                                            CommandName="Edit" ImageUrl="~/Images/Edit.gif" Text="Delete" 
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
    </ContentTemplate>
</asp:UpdatePanel>
<br />
</asp:Content>

