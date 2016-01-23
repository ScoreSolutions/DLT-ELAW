<%@ Page Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="ShowUsers.aspx.vb" Inherits="Users_ShowUsers" title="ข้อมูลผู้ใช้งาน" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
<TABLE style="WIDTH: 100%" class="form" border="1" cellpadding="0" cellspacing="0"><TBODY><TR>
    <TD style="COLOR: white; HEIGHT: 12px; TEXT-ALIGN: center" class="HeaderGreen" 
        colSpan=2>เพิ่มผู้ใช้งาน<asp:Label id="lblState" runat="server" 
            Font-Size="Small" Visible="False"></asp:Label> <asp:Label id="lblId" runat="server" Font-Size="Small" Text="Label" Visible="False"></asp:Label>
        <asp:Label ID="lblTstate" runat="server"></asp:Label>
    </TD></TR><TR><TD style="WIDTH: 70px; HEIGHT: 4px; TEXT-ALIGN: right" align=left>
        ชื่อเข้าระบบ</TD><TD style="WIDTH: 766px; HEIGHT: 4px; TEXT-ALIGN: left" contentEditable=""><asp:TextBox id="txtId" runat="server" Font-Bold="True" Width="104px" MaxLength="7" ToolTip="กรอกชื่อเข้าระบบ"></asp:TextBox>&nbsp; </TD></TR><TR><TD style="WIDTH: 70px; HEIGHT: 3px; TEXT-ALIGN: right" align=left><asp:Label id="Label7" runat="server" Font-Bold="True" Width="48px" Text="รหัสผ่าน"></asp:Label></TD><TD style="WIDTH: 766px; HEIGHT: 3px; TEXT-ALIGN: left"><asp:TextBox id="txtPwd" runat="server" Width="104px" ToolTip="กรอกรหัสผ่าน"></asp:TextBox></TD></TR><TR><TD style="WIDTH: 70px; HEIGHT: 3px; TEXT-ALIGN: right" align=left><asp:Label id="Label10" runat="server" Font-Bold="True" Width="72px" Text="คำนำหน้าชื่อ" __designer:wfdid="w7"></asp:Label></TD><TD style="WIDTH: 766px; HEIGHT: 3px; TEXT-ALIGN: left"><asp:DropDownList id="DDPreName" runat="server" Width="112px" __designer:wfdid="w8"></asp:DropDownList></TD></TR><TR><TD style="WIDTH: 70px; TEXT-ALIGN: right" align=left><asp:Label id="Label3" runat="server" Font-Bold="True" Text="ชื่อจริง"></asp:Label></TD><TD style="WIDTH: 766px; TEXT-ALIGN: left"><asp:TextBox id="txtFname" runat="server" Width="254px" ToolTip="กรอกชื่อจริง"></asp:TextBox></TD></TR><TR><TD style="WIDTH: 70px; TEXT-ALIGN: right" align=left><asp:Label id="Label8" runat="server" Font-Bold="True" Text="นามสกุล"></asp:Label></TD><TD style="WIDTH: 766px; TEXT-ALIGN: left"><asp:TextBox id="txtLname" runat="server" Width="254px" ToolTip="กรอกนามสกุล"></asp:TextBox></TD></TR><TR><TD style="WIDTH: 70px; TEXT-ALIGN: right" align=left><asp:Label id="Label9" runat="server" Font-Bold="True" Text="แผนก"></asp:Label></TD><TD style="WIDTH: 766px; TEXT-ALIGN: left"><asp:DropDownList id="DDDept" runat="server" Width="256px">
                            </asp:DropDownList></TD></TR><TR><TD style="HEIGHT: 18px; BACKGROUND-COLOR: #006699; TEXT-ALIGN: center" align=left colSpan=2><asp:Button id="bSave" onclick="bSave_Click" runat="server" Font-Bold="True" Text="บันทึก" ToolTip="บันทึกข้อมูล"></asp:Button> <asp:Button id="bCancel" onclick="bCancel_Click" runat="server" Font-Bold="True" Text="ยกเลิก" ToolTip="ยกเลิก"></asp:Button>&nbsp;<asp:Button id="bChange" onclick="bChange_Click" runat="server" Font-Bold="True" ForeColor="Red" Text="เปลี่ยนรหัส" __designer:wfdid="w29"></asp:Button> <asp:Label id="Label1" runat="server" Text="Label" Visible="False"></asp:Label></TD></TR></TBODY></TABLE><BR /><TABLE style="WIDTH: 100%" class="Sky"><TBODY><TR><TD colSpan=3><asp:Label id="Label2" runat="server" Font-Bold="True" Width="40px" Text="ค้นหา : "></asp:Label>&nbsp;<asp:DropDownList id="DDSearch" runat="server" Width="112px" ToolTip="เลือกเงื่อนไขที่ต้องการค้นหา">
                                <asp:ListItem Value="0">ชื่อเข้าระบบ</asp:ListItem>
                                <asp:ListItem Value="1">ชื่อจริง</asp:ListItem>
                                <asp:ListItem Value="2">นามสกุล</asp:ListItem>
                            </asp:DropDownList> <asp:TextBox id="txtSearch" runat="server" Width="300px" ToolTip="กรอกข้อมูลที่ต้องการค้นหา"></asp:TextBox> <asp:Button id="bSearch" onclick="bSearch_Click" runat="server" Font-Bold="True" Text="ค้นหา" ToolTip="ค้นหาข้อมูล"></asp:Button></TD></TR><TR><TD colSpan=3><asp:GridView style="WIDTH: 100%" id="GridView1" runat="server" Width="784px" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="1" CssClass="GridViewStyle" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" PageSize="20">
<FooterStyle BackColor="White" ForeColor="#000066"></FooterStyle>
<Columns>
<asp:TemplateField HeaderText="ลำดับ">
<ItemStyle Width="25px" HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
<ItemTemplate>
                                            <%#iRow%>
                                            .
                                        
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField SortExpression="firstname" HeaderText="ชื่อ"><EditItemTemplate>
                                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("firstname") %>'></asp:TextBox>
                                        
</EditItemTemplate>

<ItemStyle Width="15%"></ItemStyle>
<ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("firstname") %>'></asp:Label>
                                        
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField SortExpression="lastname" HeaderText="สกุล"><EditItemTemplate>
                                            <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("lastname") %>'></asp:TextBox>
                                        
</EditItemTemplate>

<ItemStyle Width="15%"></ItemStyle>
<ItemTemplate>
                                            <asp:Label ID="Label4" runat="server" Text='<%# Bind("lastname") %>'></asp:Label>
                                        
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField ShowHeader="False">
<ItemStyle Width="15px"></ItemStyle>
<ItemTemplate>
                                            <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" CommandName="Delete"
                                                ImageUrl="~/Image/DeleteFolderHS.png" Text="Delete" ToolTip="ลบ" />
                                        
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField ShowHeader="False"><EditItemTemplate>
                                            <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="True" CommandName="Update"
                                                ImageUrl="~/Image/save.png" Text="Update" />
                                            <asp:ImageButton ID="ImageButton2" runat="server" CausesValidation="False" CommandName="Cancel"
                                                ImageUrl="~/Image/cancel2.bmp" Text="Cancel" />
                                        
</EditItemTemplate>

<ItemStyle Width="15px"></ItemStyle>
<ItemTemplate>
                                            <asp:ImageButton ID="ImageButton2" runat="server" CausesValidation="False" CommandName="Edit"
                                                ImageUrl="~/Image/Edit.gif" Text="Edit" ToolTip="แก้ไข" />
                                        
</ItemTemplate>
</asp:TemplateField>
</Columns>

<RowStyle BackColor="PaleTurquoise" ForeColor="#000066"></RowStyle>
<EmptyDataTemplate>
                                    <div style="border: solid 2px; color: Red; font-size: 10pt; font-weight: bold; text-align: center">
                                        ไม่พบข้อมูลที่ค้นหา</div>
                                
</EmptyDataTemplate>

<SelectedRowStyle BackColor="#669999" ForeColor="White" Font-Bold="True"></SelectedRowStyle>

<PagerStyle BackColor="SteelBlue" ForeColor="White" HorizontalAlign="Center" Font-Bold="True"></PagerStyle>

<HeaderStyle BackColor="#006699" BorderStyle="None" Height="20px" ForeColor="White" Font-Bold="True"></HeaderStyle>

<AlternatingRowStyle BackColor="LightCyan"></AlternatingRowStyle>
</asp:GridView> </TD></TR><TR><TD colSpan=3><asp:Label id="Label5" runat="server" Width="728px"></asp:Label></TD></TR></TBODY></TABLE>
</ContentTemplate>
    </asp:UpdatePanel>
    &nbsp; &nbsp;
</asp:Content>

