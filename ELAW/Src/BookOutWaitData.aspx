<%@ Page Title="รอตรวจสอบ-LAW6104" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="BookOutWaitData.aspx.vb" Inherits="Src_BookOutWaitData" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript">

    function openwindow(Page, Id) {
    window.open("../Src/" + Page + ".aspx?id=" + Id, "_new", "location=no,status=no,resizable=yes,width=750,height=700,scrollbars=yes,menubar=no");
    }

    </script>
    <div align="left">
        <table class="form" cellpadding="1" cellspacing="1" frame="border" 
        border="0" width="200">
            <tr>
                <td class="HeaderGreen" colspan="2">
                    <asp:LinkButton ID="linkHome" runat="server">Home</asp:LinkButton>
                                    &nbsp;&gt;
                                    <asp:LinkButton ID="link1" runat="server">ทะเบียนหนังสือนำส่ง</asp:LinkButton>
                                    &nbsp;&gt;
                                    <asp:LinkButton ID="link2" runat="server">รอตรวจสอบ</asp:LinkButton>&nbsp;&gt;
                                    <asp:LinkButton ID="link3" runat="server">รายละเอียด</asp:LinkButton></td>
            </tr>
            <tr>
                <td class="style1">
            ลงวันที่ :</td>
                <td class="sslbl" width="85%">
                    <asp:Label ID="lblDate" runat="server"><%#BindField("dates")%></asp:Label>
                    <asp:Label ID="lblMainStatus" runat="server" 
                Visible="False"></asp:Label>
                    <asp:Label ID="lblId" runat="server" 
                Visible="False"></asp:Label>
                    <asp:Label ID="lblChkBookNo" runat="server" 
                CssClass="sslbl_red"></asp:Label>
                    <asp:Label ID="lblIdNew" runat="server" 
                Visible="False"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    สถานะหนังสือ :</td>
                <td class="sslbl">
                    <asp:Label ID="lblStatus" runat="server"><%#BindField("status_name")%></asp:Label>
                    <asp:Label ID="lblStatusId" runat="server" Text='<%# BindField("status_id") %>' 
                        Visible="False" ></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    ชนิดหนังสือ :</td>
                <td class="sslbl">
                    <asp:Label ID="lblType" runat="server"><%#BindField("bookkind_name")%></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    ประเภทหนังสือ :</td>
                <td class="sslbl">
                    <asp:Label ID="lblBookType" runat="server"><%#BindField("type_name")%></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    เจ้าของเรื่อง :</td>
                <td class="sslbl">
                    <asp:Label ID="lblCreateName" runat="server"><%#BindField("createname")%></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    อ้างอิงเรื่อง :</td>
                <td class="sslbl">
                    <asp:Label ID="lblRefTitle" runat="server"><%#BindField("ref_title")%></asp:Label>
                    <asp:Label ID="lblRefType" runat="server" Visible="False"><%#BindField("ref_type")%></asp:Label>
                    <asp:Label ID="lblRefId" runat="server" Visible="False"><%#BindField("ref_id")%></asp:Label>
                    &nbsp;<asp:LinkButton ID="LinkDetail" runat="server">ดูรายละเอียด</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td class="style1">
            ผู้ลงนามในหนังสือ :</td>
                <td class="sslbl">
                    <asp:Label ID="lblName" runat="server"><%#BindField("signname")%></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style1">
            ความเร่งด่วน :</td>
                <td class="sslbl" >
                    <asp:Label ID="lblPriority" runat="server"><%#BindField("priority_name")%></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style1">
            เรื่อง :</td>
                <td class="sslbl" >
                    <asp:Label ID="lblTopic" runat="server"><%#BindField("topic")%></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style1">
            เรียน :</td>
                <td class="sslbl" >
                    <asp:Label ID="lblPresent" runat="server"><%#BindField("present")%></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style1">
            เนื้อความ :</td>
                <td class="sslbl" >
                    <asp:Label ID="lblMessage" runat="server"><%#BindField("message")%></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style1">
            ลงท้าย :</td>
                <td class="sslbl" >
                    <asp:Label ID="lblPostScript" runat="server"><%#BindField("postscript")%></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style1">
            คำลงท้าย(ชื่อ) :</td>
                <td class="sslbl" >
                    <asp:Label ID="lblPostName" runat="server"><%#BindField("postname")%></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style1">
            คำลงท้าย(ตำแหน่ง) :</td>
                <td class="sslbl" >
                    <asp:Label ID="lblPostPosition" runat="server"><%#BindField("post_pos")%></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style1">
            ข้อมูลผู้ติดต่อ :</td>
                <td class="sslbl" >
                    <asp:Label ID="lblContact" runat="server"><%#BindField("contact")%></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style1">
            หมายเหตุ :</td>
                <td class="sslbl" >
                    <asp:Label ID="lblComment" runat="server"><%#BindField("comment")%></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style1">
            คำค้นหา :</td>
                <td class="sslbl" >
                    <asp:Label ID="lblKeyword" runat="server"><%#BindField("keyword")%></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style1">
            ส่งต่อ :</td>
                <td class="sslbl" >
                    <asp:Label ID="lblSendto" runat="server"><%#BindField("sendname")%></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="HeaderGreen" colspan="2">
                    เอกสารประกอบ</td>
            </tr>
            <tr>
                <td class="style1" colspan="2">
                    <asp:GridView runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" CellPadding="1" GridLines="Vertical" BackColor="White" BorderColor="#999999" BorderWidth="1px" BorderStyle="None" CssClass="GridViewStyle" Width="100%" ID="GridView1">
                        <RowStyle BackColor="#EEEEEE" ForeColor="Black"></RowStyle>
                        <AlternatingRowStyle BackColor="Gainsboro"></AlternatingRowStyle>
                        <Columns>
                            <asp:TemplateField HeaderText="ชื่อเอกสาร">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("title") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label11" runat="server" Text='<%# Bind("title") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="70%"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="จำนวนหน้า">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("page") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label12" runat="server" Text='<%# Bind("page") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="20%"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ดาวน์โหลด">
                                <ItemTemplate>
                                    <asp:Label ID="lblLink" runat="server" Text=""></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField ShowHeader="False" Visible ="false" >
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" 
                                                 CommandName="Delete" ImageUrl="~/Images/DeleteFolderHS.png" Text="Delete" 
                                                 ToolTip="ลบ" />
                                </ItemTemplate>
                                <ItemStyle Width="15px"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField ShowHeader="False" Visible ="false">
                                <EditItemTemplate>
                                    <asp:ImageButton ID="ImageButton3" runat="server" CausesValidation="True" 
                                                 CommandName="Update" ImageUrl="~/Image/save.png" Text="Update" />
                                    <asp:ImageButton ID="ImageButton2" runat="server" CausesValidation="False" 
                                                 CommandName="Cancel" ImageUrl="~/Images/cancel2.bmp" Text="Cancel" />
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImageButton4" runat="server" CausesValidation="False" 
                                                 CommandName="Edit" ImageUrl="~/Images/Edit.gif" Text="Edit" ToolTip="แก้ไข" />
                                </ItemTemplate>
                                <ItemStyle Width="15px"></ItemStyle>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                            <table width="100%">
                                <tr>
                                    <td align="center">
                                        <asp:Label ID="Label13" runat="server" CssClass="sslbl_red" 
                                                     Text="***ไม่พบข้อมูล***"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
                        <FooterStyle BackColor="#CCCCCC" ForeColor="Black"></FooterStyle>
                        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White">
                        </HeaderStyle>
                        <PagerStyle HorizontalAlign="Center" BackColor="#999999" ForeColor="Black">
                        </PagerStyle>
                        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White">
                        </SelectedRowStyle>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td class="HeaderGreen" colspan="2">
                    ความเห็นหัวหน้ากลุ่ม</td>
            </tr>
            <tr>
                <td class="style1">
                    ความเห็น :</td>
                <td class="sslbl" >
                    <asp:RadioButtonList ID="rdoApp" runat="server" CssClass="sslbl" 
                                                RepeatColumns="3" AutoPostBack="True">
                                                <asp:ListItem Value="T" Selected="True">ผ่าน</asp:ListItem>
                                                <asp:ListItem Value="F">ส่งให้เจ้าของเรื่องแก้ไข</asp:ListItem>
                                                <asp:ListItem Value="N">ส่งให้นิติกรแก้ไข</asp:ListItem>
                                            </asp:RadioButtonList>
                    <asp:Label ID="lblChkApp" runat="server" 
                CssClass="sslbl_red"></asp:Label>
                                            </td>
            </tr>
            <tr>
                <td class="style1">
                    ส่งต่อ :</td>
                <td class="sslbl" >
                        <asp:DropDownList ID="ddlSentTo" runat="server" 
                    Height="20px" Width="250px" CssClass="ssddl" Enabled="False">
                        </asp:DropDownList>
                    <asp:Label ID="lblChkSend" runat="server" 
                CssClass="sslbl_red"></asp:Label>
                                            </td>
            </tr>
            <tr>
                <td class="style1">
                    บันทึก :</td>
                <td class="sslbl" >
                                            <asp:TextBox ID="txtComment" runat="server" Text='<%# BindField("sendto_comment") %>' 
                        CssClass="ssddl" Rows="15" 
                                                TextMode="MultiLine" Width="600px"></asp:TextBox>
                                            <br />
                    <asp:Label ID="lblChkComment" runat="server" 
                CssClass="sslbl_red"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style2">
                    &nbsp;</td>
                <td class="sslbl" >
                    <asp:Button ID="bSave" runat="server" 
                CssClass="ssbtn" Text="บันทึก" 
                                                Width="85px" Height="26px" />
                                        <asp:Button ID="bSaveAndSend" runat="server" 
                CssClass="ssbtn" Text="บันทึกพร้อมส่ง" 
                                                Width="100px" Height="26px" />
                                        </td>
            </tr>
        </table>
    </div>
</asp:Content>

<asp:Content ID="Content3" runat="server" contentplaceholderid="head">

    <style type="text/css">
        .style1
        {
            color: #000000;
            font-weight : normal;
            text-decoration: none;
            vertical-align: top;
            padding: 1px;
            text-align : right;
            font-style: normal;
            font-variant: normal;
            font-size: 12px;
            line-height: normal;
            font-family: Arial, "Courier New", Courier, monospace;
            }
        .style2
        {
            width: 12%;
        }
    </style>

</asp:Content>


