<%@ Page Title="ผู้ใช้งาน-LAW8101" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" ValidateRequest="false" CodeFile="Employee.aspx.vb" Inherits="Src_Employee" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            
                    <table class="form" border="0" cellpadding="1" 
        cellspacing="1">
                        <tr>
                            <td class="HeaderGreen" colspan="2">
                                ผู้ใช้งาน</td>
                        </tr>
                        <tr>
                            <td class="sslbl_right" width="15%">
                                รหัส</td>
                            <td class="sslbl" width="85%">
                                <asp:TextBox ID="txtEmpID" runat="server" CssClass="ssddl" MaxLength="15"
                                Text='<%# BindField("empid") %>' 
                                Width="100px"></asp:TextBox>
                                <asp:Label ID="lblChkTitle" runat="server" CssClass="sslbl_red" Text="*"></asp:Label>
                                &nbsp;
                                <asp:Label ID="lblID" runat="server" CssClass="sslbl_red"></asp:Label>
                                <asp:Label ID="lblStatus" runat="server" Visible="False"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="sslbl_right">
                                เพศ</td>
                            <td class="sslbl">
                                <asp:DropDownList ID="ddlSex" runat="server" AutoPostBack="True" 
                                    CssClass="ssddl" Height="20px" Width="100px">
                                    <asp:ListItem Value="0">---โปรดเลือก---</asp:ListItem>
                                    <asp:ListItem Value="M">ชาย</asp:ListItem>
                                    <asp:ListItem Value="F">หญิง</asp:ListItem>
                                </asp:DropDownList>
                                <asp:Label ID="lblChkTitle7" runat="server" CssClass="sslbl_red" Text="*"></asp:Label>
                                &nbsp;
                                <asp:Label ID="lblSex" runat="server" CssClass="sslbl_red"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="sslbl_right">
                                คำนำหน้าชื่อ</td>
                            <td class="sslbl">
                                <asp:DropDownList ID="ddlPrefix" runat="server" AutoPostBack="True" 
                                    CssClass="ssddl" Height="20px" Width="100px">
                                </asp:DropDownList>
                                <asp:Label ID="lblChkTitle8" runat="server" CssClass="sslbl_red" Text="*"></asp:Label>
                                &nbsp;
                                <asp:Label ID="lblPreName" runat="server" CssClass="sslbl_red"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="sslbl_right">
                                ชื่อ</td>
                            <td class="sslbl">
                                <asp:TextBox ID="txtFirstName" runat="server" CssClass="ssddl" MaxLength="250" 
                                Text='<%# BindField("firstname") %>' 
                            Width="200px"></asp:TextBox>
                                <asp:Label ID="lblChkTitle0" runat="server" CssClass="sslbl_red" Text="*"></asp:Label>
                                &nbsp;
                                <asp:Label ID="lblName" runat="server" CssClass="sslbl_red"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="sslbl_right">
                                นามสกุล</td>
                            <td class="sslbl">
                                <asp:TextBox ID="txtLastname" runat="server" CssClass="ssddl" MaxLength="250" 
                                Text='<%# BindField("lastname") %>' 
                                    Width="200px"></asp:TextBox>
                                <asp:Label ID="lblChkTitle1" runat="server" CssClass="sslbl_red" Text="*"></asp:Label>
                                &nbsp;
                                <asp:Label ID="lblLastname" runat="server" CssClass="sslbl_red"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="sslbl_right">
                                ชื่อเข้าระบบ</td>
                            <td class="sslbl">
                                <asp:TextBox ID="txtLoginName" runat="server" CssClass="ssddl" MaxLength="250" 
                                    Text='<%# BindField("lastname") %>' Width="200px"></asp:TextBox>
                                <asp:Label ID="lblChkTitle6" runat="server" CssClass="sslbl_red" Text="*"></asp:Label>
                                &nbsp;
                                <asp:Label ID="lblLogin" runat="server" CssClass="sslbl_red"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="sslbl_right">
                                รหัสผ่าน</td>
                            <td class="sslbl">
                                <asp:TextBox ID="txtPassword" runat="server" CssClass="ssddl" MaxLength="250" 
                                    Text='<%# BindField("lastname") %>' Width="200px"></asp:TextBox>
                                <asp:Label ID="lblChkTitle2" runat="server" CssClass="sslbl_red" Text="*"></asp:Label>
                                &nbsp;
                                <asp:Label ID="lblPass" runat="server" CssClass="sslbl_red"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="sslbl_right">
                                วันเกิด</td>
                            <td class="sslbl">
                                 <asp:TextBox ID="txtbirthday" runat="server" CssClass="ssddl" 
                                                Text='<%# BindField("birthday") %>' Width="100px" />
                                            <cc1:CalendarExtender ID="CalendarExtender4" runat="server" 
                                                format="dd/MM/yyyy" popupbuttonid="Image2" targetcontrolid="txtbirthday">
                                            </cc1:CalendarExtender>
                                            <asp:ImageButton ID="Image2" runat="server" 
                                                AlternateText="Click to show calendar" ImageUrl="../images/cal.png" />
                                </td>
                        </tr>
                        <tr>
                            <td class="sslbl_right">
                                เบอร์โทรศัพท์บ้าน</td>
                            <td class="sslbl">
                                <asp:TextBox ID="txtPhoneHome" runat="server" CssClass="ssddl" MaxLength="200" 
                                Text='<%# BindField("phonehome") %>' 
                            Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="sslbl_right">
                                เบอร์ที่ทำงาน</td>
                            <td class="sslbl">
                                <asp:TextBox ID="txtPhoneOffice" runat="server" CssClass="ssddl" 
                                Text='<%# BindField("phonework") %>' 
                                    MaxLength="250" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="sslbl_right">
                                เบอร์มือถือ</td>
                            <td class="sslbl">
                                <asp:TextBox ID="txtPhoneMobile" runat="server" CssClass="ssddl" 
                                Text='<%# BindField("phonework") %>' 
                                    MaxLength="250" Width="200px" Wrap="False"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="sslbl_right">
                                อีเมล์</td>
                            <td class="sslbl">
                                <asp:TextBox ID="txtEmail" runat="server" CssClass="ssddl" 
                                    Text='<%# BindField("email") %>' MaxLength="250" 
                                    Width="300px" Wrap="False"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="sslbl_right">
                                หน่วยงาน</td>
                            <td class="sslbl">
                                <asp:DropDownList ID="ddlDept" runat="server" AutoPostBack="True" 
                                    CssClass="ssddl" Height="20px" Width="300px">
                                </asp:DropDownList>
                                <asp:Label ID="lblChkTitle3" runat="server" CssClass="sslbl_red" Text="*"></asp:Label>
                                &nbsp;
                                <asp:Label ID="lblDept" runat="server" CssClass="sslbl_red"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="sslbl_right">
                                ส่วนงาน</td>
                            <td class="sslbl">
                                <asp:DropDownList ID="ddlDiv" runat="server" AutoPostBack="True" 
                                    CssClass="ssddl" Height="20px" Width="300px">
                                </asp:DropDownList>
                                <asp:Label ID="lblChkTitle4" runat="server" CssClass="sslbl_red" Text="*"></asp:Label>
                                &nbsp;
                                <asp:Label ID="lblDiv" runat="server" CssClass="sslbl_red"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="sslbl_right">
                                ตำแหน่ง</td>
                            <td class="sslbl">
                                <asp:DropDownList ID="ddlPos" runat="server" AutoPostBack="True" 
                                    CssClass="ssddl" Height="20px" Width="300px">
                                </asp:DropDownList>
                                <asp:Label ID="lblChkTitle5" runat="server" CssClass="sslbl_red" Text="*"></asp:Label>
                                &nbsp;
                                <asp:Label ID="lblPos" runat="server" CssClass="sslbl_red"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="sslbl_right">&nbsp;
                                </td>
                            <td class="sslbl">
                                <asp:CheckBox ID="ChkStatus" runat="server" Text="ยกเลิกการใช้งาน" />
                            </td>
                        </tr>
                        <tr>
                            <td class="sslbl_right">&nbsp;
                                </td>
                            <td class="sslbl">
                                <asp:Button ID="bSave" runat="server" CssClass="ssbtn" Text="บันทึก" 
                            Width="80px" />
                                <asp:Button ID="bCancel" runat="server" CssClass="ssbtn" Text="ยกเลิก" 
                            Width="80px" />
                                <asp:Button ID="bResetPassword" runat="server" CssClass="ssbtn" ForeColor="Red" 
                                    Text="Reset Password" Width="120px" />
                            </td>
                        </tr>
                    </table>
      
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

