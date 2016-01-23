<%@ Page Title="ทะเบียนหนังสือนำส่ง-LAW6107" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" ValidateRequest="false" CodeFile="BookOutdata.aspx.vb" Inherits="Src_BookOutdata" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<%@ Register assembly="FredCK.FCKeditorV2" namespace="FredCK.FCKeditorV2" tagprefix="FCKeditorV2" %>


<%@ Register src="../UserControl/DatePicker.ascx" tagname="DatePicker" tagprefix="uc1" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript">

    function openwindow(Page, Id) {
    window.open("../Src/" + Page + ".aspx?id=" + Id, "_new", "location=no,status=no,resizable=yes,width=750,height=700,scrollbars=yes,menubar=no");
    }

    </script>
    <table width="100%" border="0"  >
               <tr valign="top">
                 <td>	
                 		<!--content -->
                 		
                    <div class="HeaderGreen">
                       <asp:LinkButton ID="LinkHome" runat="server">Home</asp:LinkButton>
                    &nbsp;&gt;
                    <asp:LinkButton ID="Link1" runat="server">ทะเบียนหนังสือนำส่ง</asp:LinkButton>
                    &nbsp;&gt;
                    <asp:LinkButton ID="Link2" runat="server">บันทึกหนังสือนำส่ง</asp:LinkButton>
                    &nbsp;&gt;
                    <asp:LinkButton ID="Link3" runat="server">รายละเอียด</asp:LinkButton>
                        </div>
                </td>
               </tr>
             </table>
     <cc1:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0"
        Width="100%">
        <cc1:TabPanel runat="server" HeaderText="ข้อมูลหลัก" ID="TabPanel1" TabIndex="1" Height="100%">
            <HeaderTemplate>ข้อมูลหลัก</HeaderTemplate>
            
<ContentTemplate><asp:UpdatePanel ID="UpdatePanel1" runat="server"><ContentTemplate ><table border="0" cellpadding="1" cellspacing="1" class="form" frame="border" 
            width="200"><tr><td class="style3">ลำดับ</td><td class="sslbl" width="35%"><asp:Label 
            ID="lblRunNoShow" runat="server" CssClass="sslbl_bold" 
            Text='<%# BindField("runno") %>'></asp:Label><asp:Label ID="lblRunNo" 
            runat="server" CssClass="sslbl_bold" Text='<%# BindField("runbook") %>' 
            Visible="False"></asp:Label></td><td class="sslbl_right" width="15%">&nbsp;</td><td class="sslbl" width="35%">&#160;</td></tr><tr><td class="style3">สถานะหนังสือ</td><td class="sslbl" width="35%"><asp:DropDownList 
        ID="ddlBookStatus" runat="server" CssClass="ssddl" Enabled="False" 
        Width="255px"></asp:DropDownList></td><td class="sslbl_right" width="15%"><asp:Label ID="lblMainStatus" runat="server" Visible="False"></asp:Label></td><td class="sslbl" width="35%"><asp:Label 
        ID="lblId" runat="server" Visible="False"></asp:Label><asp:Label 
        ID="lblBookNo" runat="server" Text='<%# BindField("system_no") %>' 
        Visible="False"></asp:Label><asp:Label ID="lblChkBookNo" runat="server" 
        CssClass="sslbl_red"></asp:Label></td></tr><tr><td 
            class="style4">ชนิดหนังสือ</td><td class="style2" width="35%"><asp:DropDownList 
                ID="ddlKindId" runat="server" CssClass="ssddl" Width="150px"></asp:DropDownList><asp:Label 
                ID="Label1" runat="server" CssClass="sslbl_red" Text="*"></asp:Label></td><td 
            class="style1" width="15%"></td><td class="style2" width="35%"></td></tr><tr><td 
        class="style4">ประเภทหนังสือ</td><td class="style2" width="35%"><asp:DropDownList 
            ID="ddlBookType" runat="server" CssClass="ssddl" Width="150px"></asp:DropDownList><asp:Label 
            ID="Label15" runat="server" CssClass="sslbl_red" Text="*"></asp:Label></td><td 
        class="style1" width="15%">&#160;</td><td class="style2" width="35%">&#160;</td></tr><tr><td class="style3">ลงวันที่</td><td class="sslbl"><uc1:DatePicker 
        ID="txtDate" runat="server" IsNotNull="true" Text='<%# BindField("dates") %>' /><asp:Label 
        ID="lblChkDates" runat="server" CssClass="sslbl_red"></asp:Label></td><td class="sslbl_right">&#160;</td><td class="sslbl">&#160;</td></tr><tr><td class="style3">อ้างอิงเรื่อง</td><td class="sslbl" colspan="3"><asp:TextBox ID="txtTitle" runat="server" CssClass="ssddl" 
                        Text='<%# BindField("ref_title") %>' Width="450px" 
        ReadOnly="True"></asp:TextBox>&#160;<asp:Button ID="bSelectTitle" runat="server" CssClass="ssbtn" Text="เลือก" 
                        Width="51px" /><asp:Button ID="bDelTitle" runat="server" CssClass="ssbtn" Text="ลบ" 
        Width="51px" />&#160;<asp:Label ID="lblTitle" 
        runat="server" Visible="False"><%# BindField("ref_id") %></asp:Label><asp:Label ID="lblType" runat="server" Visible="False"><%#BindField("booktype_id")%></asp:Label></td></tr><tr><td class="style3">ผู้ลงนามในหนังสือ</td><td class="sslbl"><asp:TextBox 
            ID="txtName1" runat="server" CssClass="ssddl" 
            Text='<%# BindField("signname") %>' Width="200px"></asp:TextBox>&#160;<asp:Button 
            ID="bSelect1" runat="server" CssClass="ssbtn" Text="เลือก" Width="51px" />&#160;<asp:Label 
            ID="Label2" runat="server" CssClass="sslbl_red" Text="*"></asp:Label><asp:Label 
            ID="lblName" runat="server" Visible="False"><%# BindField("user_sign") %></asp:Label><asp:Label 
            ID="lblChkName" runat="server" CssClass="sslbl_red"></asp:Label></td><td class="sslbl_right"></td><td class="sslbl">&#160;&#160;</td></tr><tr><td class="style3">เจ้าของเรื่อง</td><td class="sslbl"><asp:TextBox 
        ID="txtCreateName" runat="server" CssClass="ssddl" ReadOnly="True" 
        Width="250px"></asp:TextBox></td><td class="sslbl_right">&#160;</td><td class="sslbl">&#160;</td></tr><tr><td class="style3">ความเร่งด่วน</td><td 
        class="sslbl"><asp:DropDownList ID="ddlPriority" runat="server" 
        CssClass="ssddl" Width="150px"></asp:DropDownList><asp:Label ID="Label3" 
        runat="server" CssClass="sslbl_red" Text="*"></asp:Label></td><td class="sslbl_right">&#160;</td><td 
        class="sslbl" style=""></td></tr><tr><td class="style3">เรื่อง</td><td class="sslbl" colspan="3"><asp:TextBox ID="txtTopic" runat="server" CssClass="ssddl" 
                        Text='<%# BindField("topic") %>' Width="500px"></asp:TextBox><asp:Label ID="Label4" runat="server" CssClass="sslbl_red" Text="*"></asp:Label><asp:Label ID="lblChkTopic" runat="server" CssClass="sslbl_red"></asp:Label></td></tr><tr><td class="style3">เรียน</td><td class="sslbl" colspan="3"><asp:TextBox ID="txtPresent" runat="server" CssClass="ssddl" 
                        Text='<%# BindField("present") %>' Width="500px"></asp:TextBox><asp:Label ID="Label5" runat="server" CssClass="sslbl_red" Text="*"></asp:Label><asp:Label ID="lblPresent" runat="server" CssClass="sslbl_red"></asp:Label></td></tr><tr>
        <td class="style3" width="200">เนื้อความ</td><td class="sslbl" colspan="3"><FCKeditorV2:FCKeditor ID="FCKeditor2" runat="server" BasePath="~/fckeditor/" 
                        Height="500px" Value='<%# BindField("message") %>'></FCKeditorV2:FCKeditor><asp:Button ID="bPreview" runat="server" CssClass="ssbtn" Height="26px" 
            Text="แสดงตัวอย่าง" Width="87px" /><asp:Label ID="lblChkMessage" runat="server" CssClass="sslbl_red"></asp:Label></td></tr><tr><td class="style3">ลงท้าย</td><td class="sslbl" colspan="3"><asp:TextBox ID="txtPostscript" runat="server" CssClass="ssddl" 
                        Text='<%# BindField("postscript") %>' Width="500px"></asp:TextBox><asp:Label 
            ID="Label6" runat="server" CssClass="sslbl_red" Text="*" Visible="False"></asp:Label><asp:Label ID="lblChkPostScript" runat="server" CssClass="sslbl_red"></asp:Label></td></tr><tr><td class="style3">คำลงท้าย(ชื่อ)</td><td class="sslbl" colspan="3"><asp:TextBox ID="txtPostName" runat="server" CssClass="ssddl" 
                        Text='<%# BindField("postname") %>' Width="250px"></asp:TextBox><asp:Label 
            ID="Label7" runat="server" CssClass="sslbl_red" Text="*" Visible="False"></asp:Label><asp:Label ID="lblChkPostName" runat="server" CssClass="sslbl_red"></asp:Label></td></tr><tr><td class="style3">คำลงท้าย(ตำแหน่ง)</td><td class="sslbl" colspan="3"><asp:TextBox ID="txtPostPosition" runat="server" CssClass="ssddl" 
                        Text='<%# BindField("post_pos") %>' Width="250px"></asp:TextBox><asp:Label 
            ID="Label8" runat="server" CssClass="sslbl_red" Text="*" Visible="False"></asp:Label><asp:Label ID="lblChkPostPos" runat="server" CssClass="sslbl_red"></asp:Label></td></tr><tr><td class="style3">ข้อมูลผู้ติดต่อ</td><td class="sslbl" colspan="3"><asp:TextBox ID="txtContact" runat="server" CssClass="ssddl" Rows="4" 
                        Text='<%# BindField("contact") %>' TextMode="MultiLine" Width="100%"></asp:TextBox></td></tr><tr><td class="style3">หมายเหตุ</td><td class="sslbl" colspan="3"><asp:TextBox ID="txtComment" runat="server" CssClass="ssddl" 
                        Text='<%# BindField("comment") %>' Width="500px"></asp:TextBox></td></tr><tr><td class="style3">คำค้นหา</td><td class="sslbl" colspan="3"><asp:TextBox ID="txtKeyword" runat="server" CssClass="ssddl" 
                        Text='<%# BindField("keyword") %>' Width="500px"></asp:TextBox><asp:Label 
            ID="Label9" runat="server" CssClass="sslbl_red" Text="*" Visible="False"></asp:Label><asp:Label ID="lblChkKeyword" runat="server" CssClass="sslbl_red"></asp:Label></td></tr><tr><td class="style3">ประเภทการส่งต่อ</td><td class="sslbl" colspan="3"><asp:RadioButtonList ID="rdbSend" runat="server" AutoPostBack="True" 
            CssClass="sslbl" RepeatColumns="2"><asp:ListItem Value="0">ส่งให้ผู้อำนวยการตรวจ</asp:ListItem><asp:ListItem Value="1" Selected="True">ส่งให้หัวหน้าตรวจ</asp:ListItem></asp:RadioButtonList></td></tr><tr><td class="style3">ส่งต่อ</td><td class="style7" colspan="3"><asp:DropDownList ID="ddlSentTo" runat="server" CssClass="ssddl" Width="255px"></asp:DropDownList><asp:Label ID="Label10" runat="server" CssClass="sslbl_red" Text="*"></asp:Label></td></tr><tr><td class="style5">&#160;</td><td class="sslbl" colspan="3"><asp:Button ID="bSave" runat="server" CssClass="ssbtn" Height="26px" 
                        Text="รายการถูกบันทึก" Width="99px" />&#160;<asp:Button ID="bSaveAndSend" runat="server" CssClass="ssbtn" Height="26px" 
                        Text="รายการถูกบันทึกพร้อมส่ง" Width="155px" />&#160;<asp:Button ID="bCancel" runat="server" CssClass="ssbtn" Text="ยกเลิก" 
                        Width="80px" /></td></tr></table></ContentTemplate></asp:UpdatePanel></ContentTemplate> 
</cc1:TabPanel>
         <cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="เอกสารประกอบ" TabIndex ="2" Height ="100%">
             <ContentTemplate><table border="0" cellpadding="1" cellspacing="1" class="form" width="100%"><tr><td class="sslbl_right" width="15%">ไฟล์แนบ</td><td class="sslbl" width="85%"><asp:FileUpload ID="FileUpload1" runat="server" CssClass="ssddl" 
                                 Width="600px" /><asp:Label ID="lblAFile" runat="server" CssClass="sslbl_red"></asp:Label><asp:Label ID="lblDocStatus" runat="server" Visible="False"></asp:Label><asp:Label ID="lblDocId" runat="server" Visible="False"></asp:Label></td></tr><tr><td class="sslbl_right" width="15%">ชื่อเอกสาร</td><td class="sslbl"><asp:TextBox ID="txtDocDetail" runat="server" CssClass="ssddl" Width="600px"></asp:TextBox><asp:Label ID="lblADetail1" runat="server" CssClass="sslbl_red"></asp:Label></td></tr><tr><td class="sslbl_right" width="15%">จำนวนหน้า</td><td class="sslbl"><asp:TextBox ID="txtDocPage" runat="server" CssClass="ssddl" MaxLength="6" 
                                 Width="60px"></asp:TextBox><asp:Label ID="lblAPage" runat="server" CssClass="sslbl_red"></asp:Label></td></tr><tr><td class="sslbl" width="100">&nbsp;</td><td><asp:Button ID="bSaveFile" runat="server" CssClass="ssbtn" Text="บันทึก" 
                                 Width="80px" /><asp:Button ID="bCancelFile" runat="server" CssClass="ssbtn" Text="ยกเลิก" 
                                 Width="80px" /></td></tr><tr><td class="sslbl" colspan="2" width="100%"><asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
                                 AllowSorting="True" AutoGenerateColumns="False" BackColor="White" 
                                 BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="1" 
                                 CssClass="GridViewStyle" GridLines="Vertical" Width="100%"><Columns><asp:TemplateField HeaderText="ชื่อเอกสาร"><EditItemTemplate><asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("title") %>'></asp:TextBox></EditItemTemplate><ItemTemplate><asp:Label ID="Label11" runat="server" Text='<%# Bind("title") %>'></asp:Label></ItemTemplate><ItemStyle Width="70%" /></asp:TemplateField><asp:TemplateField HeaderText="จำนวนหน้า"><EditItemTemplate><asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("page") %>'></asp:TextBox></EditItemTemplate><ItemTemplate><asp:Label ID="Label12" runat="server" Text='<%# Bind("page") %>'></asp:Label></ItemTemplate><ItemStyle HorizontalAlign="Center" Width="20%" /></asp:TemplateField><asp:TemplateField HeaderText="ดาวน์โหลด"><ItemTemplate><asp:Label ID="lblLink" runat="server" Text=""></asp:Label></ItemTemplate><ItemStyle HorizontalAlign="Center" Width="10%" /></asp:TemplateField><asp:TemplateField ShowHeader="False"><ItemTemplate><asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" 
                                                 CommandName="Delete" ImageUrl="~/Images/DeleteFolderHS.png" Text="Delete" 
                                                 ToolTip="ลบ" /></ItemTemplate><ItemStyle Width="15px" /></asp:TemplateField><asp:TemplateField ShowHeader="False"><EditItemTemplate><asp:ImageButton ID="ImageButton3" runat="server" CausesValidation="True" 
                                                 CommandName="Update" ImageUrl="~/Image/save.png" Text="Update" /><asp:ImageButton ID="ImageButton2" runat="server" CausesValidation="False" 
                                                 CommandName="Cancel" ImageUrl="~/Images/cancel2.bmp" Text="Cancel" /></EditItemTemplate><ItemTemplate><asp:ImageButton ID="ImageButton4" runat="server" CausesValidation="False" 
                                                 CommandName="Edit" ImageUrl="~/Images/Edit.gif" Text="Edit" ToolTip="แก้ไข" /></ItemTemplate><ItemStyle Width="15px" /></asp:TemplateField></Columns><AlternatingRowStyle BackColor="Gainsboro" /><EmptyDataTemplate><table width="100%"><tr><td align="center"><asp:Label ID="Label13" runat="server" CssClass="sslbl_red" 
                                                     Text="***ไม่พบข้อมูล***"></asp:Label></td></tr></table></EmptyDataTemplate><FooterStyle BackColor="#CCCCCC" ForeColor="Black" /><HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" /><PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" /><RowStyle BackColor="#EEEEEE" ForeColor="Black" /><SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" /></asp:GridView></td></tr></table>
             </ContentTemplate>
         </cc1:TabPanel>
         
         <cc1:TabPanel ID="TabPanel3" runat="server" HeaderText="ยกเลิกเอกสาร" TabIndex="3" Height="100%">
             <ContentTemplate><table class="style1"><tr><td class="sslbl_right">สาเหตุการยกเลิก</td><td><asp:TextBox ID="txtCancelComment" runat="server" CssClass="ssddl" Rows="5" 
                                 TextMode="MultiLine" Width="500px"></asp:TextBox><asp:Label ID="Label14" runat="server" CssClass="sslbl_red" Text="*"></asp:Label><asp:Label ID="lblChkCancelComment" runat="server" CssClass="sslbl_red"></asp:Label></td></tr><tr><td class="style2">&nbsp;</td><td style="text-align: left"><asp:Button ID="bCancelDoc" runat="server" CssClass="ssbtn" Text="ยกเลิกเอกสาร" 
                                 Width="128px" /></td></tr></table>
             </ContentTemplate>
         </cc1:TabPanel>
         
         </cc1:TabContainer> 

   
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
            height: 24px;
        }
        .style2
        {
            color: #000000;
            font-weight : normal;
            text-decoration: none;
            vertical-align: top;
            padding: 1px;
            font-style: normal;
            font-variant: normal;
            font-size: 12px;
            line-height: normal;
            font-family: Arial, "Courier New", Courier, monospace;
            height: 24px;
        }
        .style3
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
            width: 19%;
        }
        .style4
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
            height: 24px;
            width: 19%;
        }
        .style5
        {
            color: #000000;
            font-weight : normal;
            text-decoration: none;
            vertical-align: top;
            padding: 1px;
            font-style: normal;
            font-variant: normal;
            font-size: 12px;
            line-height: normal;
            font-family: Arial, "Courier New", Courier, monospace;
            width: 19%;
        }
        .style7
        {
            color: #000000;
            font-weight : normal;
            text-decoration: none;
            vertical-align: top;
            padding: 1px;
            font-style: normal;
            font-variant: normal;
            font-size: 12px;
            line-height: normal;
            font-family: Arial, "Courier New", Courier, monospace;
        }
    </style>

    </asp:Content>





