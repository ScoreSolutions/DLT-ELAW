<%@ Page Title="บันทึกข้อมูลคดี-LAW3206" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" ValidateRequest="false" CodeFile="CaseData.aspx.vb" Inherits="Src_CaseData" %>

<%@ Register assembly="FredCK.FCKeditorV2" namespace="FredCK.FCKeditorV2" tagprefix="FCKeditorV2" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>


<%@ Register src="../UserControl/DatePicker.ascx" tagname="DatePicker" tagprefix="uc1" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <table width="100%" border="0"  >
               <tr valign="top">
                 <td>	
                 		<!--content -->
                 		
                            <div class="HeaderGreen">
                        <asp:LinkButton ID="linkHome" runat="server">Home</asp:LinkButton>
                        &nbsp;&gt;
                                <asp:LinkButton ID="link1" runat="server">งานคดี</asp:LinkButton>
&nbsp;&gt;
                                <asp:LinkButton ID="link2" runat="server">บันทึกข้อมูลคดี</asp:LinkButton>
                        &nbsp;&gt;
                                <asp:LinkButton ID="link3" runat="server">รายละเอียด</asp:LinkButton>
                        </div>
                           	
                      
                        <!--end content -->
                 </td>
               </tr>
             </table>
    <cc1:TabContainer ID="TabContainer1" runat="server" Width="100%" 
        ActiveTabIndex="0" BorderStyle="NotSet">
        <cc1:TabPanel runat="server" HeaderText="ข้อมูลหลัก" ID="TabPanel1" TabIndex="1">
            <HeaderTemplate>ข้อมูลหลัก
            </HeaderTemplate>
            <ContentTemplate><div class="TabContentBlue"><asp:UpdatePanel ID="UpdatePanel5" runat="server"><ContentTemplate><table border="0" cellpadding="1" cellspacing="1" class="form" width="100%"><tr><td class="sslbl_right" width="20%">อ้างอิงหนังสือรับเรื่อง</td><td class="sslbl" width="35%">
                <asp:TextBox ID="txtTitle" runat="server" CssClass="ssddl" 
                                        Text='<%# BindField("topic") %>' Width="450px" 
                    ReadOnly="True"></asp:TextBox></td><td class="sslbl"  width="35%"><asp:Button ID="bSelectTitle" runat="server" CssClass="ssbtn" Text="เลือก" 
                                        Width="51px" />
                    <asp:Button ID="bDelTitle" runat="server" CssClass="ssbtn" Text="ลบ" 
                        Width="51px" />
                    <asp:Label ID="lblTitle" runat="server" Visible="False"><%#BindField("ref_bookin")%></asp:Label></td></tr><tr><td class="sslbl_right" width="20%">ประเภทคดี</td><td class="sslbl" width="35%"><asp:DropDownList ID="ddlCaseType" runat="server" AutoPostBack="True" 
                                          CssClass="ssddl" Height="20px" Width="285px"></asp:DropDownList><asp:Label ID="lblId" runat="server" Visible="False"></asp:Label><asp:Label ID="lblMainStatus" runat="server" Visible="False"></asp:Label><asp:Label ID="lblCaseNo" runat="server" Text='<%# BindField("case_no") %>' 
                                          Visible="False"></asp:Label></td><td class="sslbl" width="35%">&#160;</td></tr><tr><td class="sslbl_right" width="15%">สถานะ</td><td class="sslbl"><asp:DropDownList ID="ddlStatus" runat="server" CssClass="ssddl" Height="20px" 
                                          Width="285px"></asp:DropDownList></td><td class="sslbl"  width="35%">&#160;</td></tr><tr><td class="sslbl_right" width="15%">หมายเลขคดีดำ</td><td class="sslbl"><asp:TextBox ID="txtBlackNo" runat="server" CssClass="ssddl" MaxLength="10" 
                                          Text='<%# BindField("black_no") %>'></asp:TextBox></td><td class="sslbl"  width="35%">&#160;</td></tr><tr><td class="sslbl_right" width="15%">หมายเลขคดีแดง</td><td class="sslbl"><asp:TextBox ID="txtRedNo" runat="server" CssClass="ssddl" MaxLength="10" 
                                          Text='<%# BindField("red_no") %>'></asp:TextBox></td><td class="sslbl"  width="35%">&#160;</td></tr>
                <tr>
                    <td class="sslbl_right" width="15%">
                        ชื่อศาล</td>
                    <td class="sslbl">
                        <asp:TextBox ID="txtCourtName" runat="server" CssClass="ssddl" 
                            Text='<%# BindField("cname") %>' Width="450px"></asp:TextBox>
                    </td>
                    <td class="sslbl" width="35%">&nbsp;
                        </td>
                </tr>
                <tr><td class="sslbl_right" width="15%">สำนักงานอัยการ</td><td class="sslbl"><asp:DropDownList ID="ddlCourt" runat="server" AutoPostBack="True" 
                                          CssClass="ssddl" Height="20px" Width="285px"></asp:DropDownList></td><td class="sslbl"  width="35%">&#160;</td></tr><tr><td class="sslbl_right">ชื่อพนักงานอัยการ</td><td class="sslbl"><asp:DropDownList ID="ddlAttorney" runat="server" CssClass="ssddl" 
                                        Height="20px" Width="285px"></asp:DropDownList></td><td class="sslbl">&#160;</td></tr><tr><td class="sslbl_right">ปิดหมาย/รับหมาย</td><td class="sslbl">
                <asp:RadioButtonList ID="rdbcloserecieve" runat="server" CssClass="sslbl" 
                                        RepeatColumns="3"><asp:ListItem Value="0">ปิดหมาย</asp:ListItem><asp:ListItem Value="1">รับหมาย</asp:ListItem>
                    <asp:ListItem Value="2">ส่งฟ้อง</asp:ListItem>
                </asp:RadioButtonList></td><td class="sslbl">
                    <asp:Label ID="lblARecieve" runat="server" CssClass="sslbl_red"></asp:Label>
                </td></tr><tr><td class="sslbl_right">วันที่</td><td class="sslbl"><uc1:DatePicker ID="txtReceiveDate" runat="server" Text='<%# BindField("recieve_date") %>' IsNotNull ="true"  /><asp:Label ID="lblARecieveDate" runat="server" CssClass="sslbl_red"></asp:Label></td><td class="sslbl">&#160;</td></tr><tr><td class="sslbl_right">โจทก์</td><td class="style2"><asp:TextBox ID="txtProsecutor" runat="server" CssClass="ssddl" Rows="3" 
                                        Text='<%# BindField("prosecutor") %>' TextMode="MultiLine" Width="99%"></asp:TextBox></td><td class="style2"><asp:Label ID="Label16" runat="server" ForeColor="Red" Text="*"></asp:Label><asp:Label ID="lblAProsecutor" runat="server" CssClass="sslbl_red"></asp:Label></td></tr><tr><td class="sslbl_right">โจทก์ร่วม</td><td class="sslbl"><asp:TextBox ID="txtProsecutor1" runat="server" CssClass="ssddl" Rows="3" 
                                          Text='<%# BindField("prosecutor1") %>' TextMode="MultiLine" Width="99%"></asp:TextBox></td><td class="sslbl">&#160;</td></tr><tr><td class="sslbl_right">จำเลย</td><td class="sslbl"><asp:TextBox ID="txtDefendant" runat="server" CssClass="ssddl" Rows="3" 
                                        Text='<%# BindField("defendant") %>' TextMode="MultiLine" Width="99%"></asp:TextBox></td><td class="sslbl"><asp:Label ID="Label15" runat="server" ForeColor="Red" Text="*"></asp:Label><asp:Label ID="lblADefedant" runat="server" CssClass="sslbl_red"></asp:Label></td></tr><tr><td class="sslbl_right">จำเลยร่วม</td><td class="sslbl"><asp:TextBox ID="txtDefendant1" runat="server" CssClass="ssddl" Rows="3" 
                                          Text='<%# BindField("defendant1") %>' TextMode="MultiLine" Width="99%"></asp:TextBox></td><td class="sslbl">&#160;</td></tr><tr><td class="sslbl_right">หัวหน้ากลุ่มงานนิติกรรมและคดี</td><td class="sslbl"><asp:DropDownList ID="ddlApp1" runat="server" CssClass="ssddl" Height="20px" 
                                          Width="285px"></asp:DropDownList></td><td class="sslbl">&#160;</td></tr><tr><td class="sslbl_right">ผู้อำนวยการ</td><td class="sslbl"><asp:DropDownList ID="ddlApp2" runat="server" CssClass="ssddl" Height="20px" 
                                          Width="285px"></asp:DropDownList></td><td class="sslbl">&#160;</td></tr><tr><td class="sslbl_right">คำค้นหา</td><td class="sslbl"><asp:TextBox ID="txtKeyword" runat="server" CssClass="ssddl" Rows="1" 
                                        Text='<%# BindField("keyword") %>' Width="99%"></asp:TextBox></td><td class="sslbl">&#160;<asp:Label ID="Label17" runat="server" ForeColor="Red" Text="*"></asp:Label><asp:Label ID="lblAKeyword" runat="server" CssClass="sslbl_red"></asp:Label></td></tr>
                <tr>
                    <td class="sslbl_right">
                        อ้างอิงคดี</td>
                    <td class="sslbl" colspan="2">
                        <asp:TextBox ID="txtNameCase" runat="server" CssClass="ssddl" 
                            ReadOnly="True" Text='<%# BindField("case2") %>' Width="500px"></asp:TextBox>
                        <asp:Button ID="bSelectCase" runat="server" CssClass="ssbtn" Text="เลือก" />
                        <asp:Button ID="bDelCase" runat="server" CssClass="ssbtn" Text="ลบ" />
                        <asp:Label ID="lblIdCase" runat="server" 
                            Text='<%# BindField("ref_case") %>' Visible="False"></asp:Label>
                    </td>
                </tr>
                <tr><td class="sslbl">&#160;</td><td class="sslbl" colspan="2"><asp:Button ID="bSave" runat="server" CssClass="ssbtn" Text="บันทึก" 
                                          Width="80px" /><asp:Button ID="bSaveAndSend" runat="server" CssClass="ssbtn" 
                                          Text="บันทึกพร้อมส่ง" Width="159px" /><asp:Button ID="bCancel" runat="server" CssClass="ssbtn" Text="ยกเลิก" 
                                          Width="80px" /></td></tr></table></ContentTemplate></asp:UpdatePanel></div></ContentTemplate>
        
</cc1:TabPanel>
        <cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="รายละเอียด" TabIndex="2">
            <HeaderTemplate>รายละเอียด</HeaderTemplate>
            
<ContentTemplate><asp:UpdatePanel ID="UpdatePanel2" runat="server"><ContentTemplate><table border="0" cellpadding="1" cellspacing="1" class="form"><tr><td align="right">&#160;</td>
    <td align="right"><asp:Button ID="bSaveFCK" runat="server" CssClass="ssbtn" Text="บันทึก" 
                                        Width="80px" /></td></tr></table><FCKeditorV2:FCKeditor ID="FCKeditor1" runat="server" BasePath="~/fckeditor/" 
                            Height="500px" Value='<%# BindField("detail") %>'></FCKeditorV2:FCKeditor></ContentTemplate></asp:UpdatePanel></ContentTemplate>
        
</cc1:TabPanel>

<cc1:TabPanel ID="TabPanel6" runat="server" HeaderText="หัวหน้าพิจารณา" TabIndex="6">
            <ContentTemplate><div class="TabContentBlue"><asp:UpdatePanel ID="UpdatePanel7" runat="server"><ContentTemplate><table border="0" cellpadding="1" cellspacing="1" class="form" width="100%"><tr><td class="sslbl_right">ความคิดเห็น</td><td class="sslbl" width="85%"><asp:Label ID="lblApp1_App" runat="server"></asp:Label></td></tr><tr><td class="sslbl_right">บันทึก</td><td><asp:TextBox ID="txtAppComent" runat="server" CssClass="ssddl" Rows="15" 
                                        Text='<%# BindField("app1_comment") %>' TextMode="MultiLine" Width="100%" 
                                        ReadOnly="True"></asp:TextBox><br /></td></tr><tr><td class="sslbl_right"><asp:Label ID="lblAppText" runat="server" Text="ลงชื่อ"></asp:Label></td><td><asp:Label ID="lblAppName" runat="server" CssClass="sslbl" 
                                        Text='<%# BindField("appname") %>'></asp:Label></td></tr><tr><td class="sslbl_right">วันที่</td><td><asp:Label ID="lblApp1Date" runat="server" CssClass="sslbl" 
                                        Text='<%# BindField("app1_date") %>'></asp:Label></td></tr></table></ContentTemplate></asp:UpdatePanel></div>
            </ContentTemplate>
            
            
        </cc1:TabPanel>
        <cc1:TabPanel ID="TabPanel3" runat="server" HeaderText="เอกสารประกอบ" TabIndex="3">
            <ContentTemplate><div class="TabContentBlue"><table border="0" cellpadding="1" cellspacing="1" class="form" width="100%"><tr><td class="sslbl_right" width="15%">ไฟล์แนบ</td><td class="sslbl" width="85%"><asp:FileUpload ID="FileUpload1" runat="server" CssClass="ssddl" 
                                        Width="600px" /><asp:Label ID="lblAFile" runat="server" CssClass="sslbl_red"></asp:Label><asp:Label ID="lblDocStatus" runat="server" Visible="False"></asp:Label><asp:Label ID="lblDocId" runat="server" Visible="False"></asp:Label></td></tr><tr><td class="sslbl_right" width="15%">ชื่อเอกสาร</td><td class="sslbl"><asp:TextBox ID="txtDocDetail" runat="server" CssClass="ssddl" Width="600px"></asp:TextBox><asp:Label ID="lblADetail1" runat="server" CssClass="sslbl_red"></asp:Label></td></tr><tr><td class="sslbl_right" width="15%">จำนวนหน้า</td><td class="sslbl"><asp:TextBox ID="txtDocPage" runat="server" CssClass="ssddl" MaxLength="6" 
                                        Width="60px"></asp:TextBox><asp:Label ID="lblAPage" runat="server" CssClass="sslbl_red"></asp:Label></td></tr><tr><td class="sslbl" width="100">&nbsp;</td><td><asp:Button ID="bSaveFile" runat="server" CssClass="ssbtn" Text="บันทึก" 
                                        Width="80px" /><asp:Button ID="bCancelFile" runat="server" CssClass="ssbtn" Text="ยกเลิก" 
                                        Width="80px" /></td></tr><tr><td class="sslbl" width="100%" colspan="2"><asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
                                        AllowSorting="True" AutoGenerateColumns="False" BackColor="White" 
                                        BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="1" 
                                        CssClass="GridViewStyle" GridLines="Vertical" Width="100%"><AlternatingRowStyle BackColor="Gainsboro" /><Columns><asp:TemplateField HeaderText="ชื่อเอกสาร"><EditItemTemplate><asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("title") %>'></asp:TextBox></EditItemTemplate><ItemTemplate><asp:Label ID="Label1" runat="server" Text='<%# Bind("title") %>'></asp:Label></ItemTemplate><ItemStyle Width="70%" /></asp:TemplateField><asp:TemplateField HeaderText="จำนวนหน้า"><EditItemTemplate><asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("page") %>'></asp:TextBox></EditItemTemplate><ItemTemplate><asp:Label ID="Label2" runat="server" Text='<%# Bind("page") %>'></asp:Label></ItemTemplate><ItemStyle HorizontalAlign="Center" Width="20%" /></asp:TemplateField><asp:TemplateField HeaderText="ดาวน์โหลด"><ItemTemplate><asp:Label ID="lblLink" runat="server" Text=""></asp:Label></ItemTemplate><ItemStyle HorizontalAlign="Center" Width="10%" /></asp:TemplateField><asp:TemplateField ShowHeader="False"><ItemTemplate><asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" 
                                                        CommandName="Delete" ImageUrl="~/Images/DeleteFolderHS.png" Text="Delete" 
                                                        ToolTip="ลบ" /></ItemTemplate><ItemStyle Width="15px" /></asp:TemplateField><asp:TemplateField ShowHeader="False"><EditItemTemplate><asp:ImageButton ID="ImageButton3" runat="server" CausesValidation="True" 
                                                        CommandName="Update" ImageUrl="~/Image/save.png" Text="Update" /><asp:ImageButton ID="ImageButton2" runat="server" CausesValidation="False" 
                                                        CommandName="Cancel" ImageUrl="~/Images/cancel2.bmp" Text="Cancel" /></EditItemTemplate><ItemTemplate><asp:ImageButton ID="ImageButton4" runat="server" CausesValidation="False" 
                                                        CommandName="Edit" ImageUrl="~/Images/Edit.gif" Text="Edit" ToolTip="แก้ไข" /></ItemTemplate><ItemStyle Width="15px" /></asp:TemplateField></Columns><EmptyDataTemplate><table width="100%"><tr><td align="center"><asp:Label ID="Label8" runat="server" CssClass="sslbl_red" 
                                        Text="***ไม่พบข้อมูล***"></asp:Label></td></tr></table></EmptyDataTemplate><FooterStyle BackColor="#CCCCCC" ForeColor="Black" /><HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" /><PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" /><RowStyle BackColor="#EEEEEE" ForeColor="Black" /><SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" /></asp:GridView></td></tr></table></div></ContentTemplate>
        
</cc1:TabPanel>
        <cc1:TabPanel ID="TabPanel4" runat="server" HeaderText="ข้อมูลศาล/อัยการนัด วันครบกำหนด" TabIndex="4">
            <ContentTemplate><div class="TabContentBlue"><asp:UpdatePanel ID="UpdatePanel3" runat="server"><ContentTemplate><table border="0" cellpadding="1" cellspacing="1" class="form" width="100%"><tr valign="top"><td class="sslbl_right" width="15%">รายละเอียด</td><td class="sslbl" width="85%"><asp:TextBox ID="txtCourtDetail" runat="server" CssClass="ssddl" Width="600px"></asp:TextBox><asp:Label ID="lblADetail" runat="server" CssClass="sslbl_red"></asp:Label><asp:Label ID="lblCourtStatus" runat="server" Visible="False"></asp:Label><asp:Label ID="lblCourtDate" runat="server" Visible="False"></asp:Label></td></tr><tr><td class="sslbl_right" width="15%">วันที่</td><td class="sslbl"><asp:TextBox ID="txtDate1" runat="server" CssClass="ssddl" Width="159px" />&#160;<cc1:CalendarExtender ID="txtDate1_CalendarExtender" runat="server" 
                                        format="dd/MM/yyyy" popupbuttonid="Image2" targetcontrolid="txtDate1"></cc1:CalendarExtender><asp:ImageButton ID="Image2" runat="server" 
                                        AlternateText="Click to show calendar" ImageUrl="../images/cal.png" />&nbsp;<asp:CheckBox ID="chkAlert1" runat="server" Text="ตั้งเตือน" /><asp:Label ID="lblADate1" runat="server" CssClass="sslbl_red"></asp:Label></td></tr><tr><td class="sslbl" width="100">&#160;</td><td><asp:Button ID="bSaveCourt" runat="server" CssClass="ssbtn" Text="บันทึก" 
                                        Width="80px" /><asp:Button ID="bCourtCancel" runat="server" CssClass="ssbtn" Text="ยกเลิก" 
                                        Width="80px" /></td></tr><tr><td class="sslbl" width="100%" colspan="2"><asp:GridView ID="GridView2" runat="server" AllowPaging="True" 
                                        AllowSorting="True" AutoGenerateColumns="False" BackColor="White" 
                                        BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="1" 
                                        CssClass="GridViewStyle" GridLines="Vertical" Width="100%"><RowStyle BackColor="#EEEEEE" ForeColor="Black" /><Columns><asp:TemplateField HeaderText="รายละเอียด"><EditItemTemplate><asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("title") %>'></asp:TextBox></EditItemTemplate><ItemTemplate><asp:Label ID="Label3" runat="server" Text='<%# Bind("title") %>'></asp:Label></ItemTemplate><ItemStyle Width="80%" /></asp:TemplateField><asp:TemplateField HeaderText="วันที่"><EditItemTemplate><asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("dates") %>'></asp:TextBox></EditItemTemplate><ItemTemplate><asp:Label ID="Label1" runat="server" 
                                                        Text='<%# Bind("dates", "{0:dd/MM/yyyy}") %>'></asp:Label></ItemTemplate><ItemStyle Width="20%" /></asp:TemplateField><asp:TemplateField HeaderText="alert_id" Visible="False"><EditItemTemplate><asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("alert_id") %>'></asp:TextBox></EditItemTemplate><ItemTemplate><asp:Label ID="Label2" runat="server" Text='<%# Bind("alert_id") %>'></asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField><ItemTemplate><asp:LinkButton ID="LinkAlert" CommandName ="Select" runat="server" Text='<%#ImagesGet(Eval("alert")) %>' ToolTip="เปิด/ปิด">
                                                </asp:LinkButton></ItemTemplate><ItemStyle HorizontalAlign="Center" Width="30px" /><HeaderStyle HorizontalAlign="Center" /></asp:TemplateField><asp:TemplateField ShowHeader="False"><ItemStyle Width="15px" /><ItemTemplate><asp:ImageButton ID="ImageButton5" runat="server" CausesValidation="False" 
                                                        CommandName="Delete" ImageUrl="~/Images/DeleteFolderHS.png" Text="Delete" 
                                                        ToolTip="ลบ" /></ItemTemplate></asp:TemplateField><asp:TemplateField ShowHeader="False"><ItemStyle Width="15px" /><ItemTemplate><asp:ImageButton ID="ImageButton8" runat="server" CausesValidation="False" 
                                                        CommandName="Edit" ImageUrl="~/Images/Edit.gif" Text="Edit" ToolTip="แก้ไข" /></ItemTemplate></asp:TemplateField></Columns><FooterStyle BackColor="#CCCCCC" ForeColor="Black" /><PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" /><EmptyDataTemplate><table width="100%"><tr><td align="center"><asp:Label ID="Label8" runat="server" CssClass="sslbl_red" 
                                        Text="***ไม่พบข้อมูล***"></asp:Label></td></tr></table></EmptyDataTemplate><SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" /><HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" /><AlternatingRowStyle BackColor="#DCDCDC" /></asp:GridView></td></tr></table></ContentTemplate></asp:UpdatePanel></div></ContentTemplate>
        
</cc1:TabPanel>


 
        
        
        <cc1:TabPanel ID="TabPanel5" runat="server" HeaderText="ข้อมูลขยายเวลายื่นคำชี้แจง" TabIndex="5">
            <ContentTemplate><div class="TabContentBlue"><asp:UpdatePanel ID="UpdatePanel4" runat="server"><ContentTemplate><table border="0" cellpadding="1" cellspacing="1" class="form" width="100%"><tr valign="top"><td class="sslbl_right" width="15%">รายละเอียด</td><td class="sslbl" width="85%"><asp:TextBox ID="txtExplainDetail" runat="server" CssClass="ssddl" 
                                        Width="600px"></asp:TextBox><asp:Label ID="lblAExplain" runat="server" CssClass="sslbl_red"></asp:Label><asp:Label ID="lblExplainStatus" runat="server" Visible="False"></asp:Label><asp:Label ID="lblExplainDate" runat="server" Visible="False"></asp:Label></td></tr><tr><td class="sslbl_right" width="15%">วันที่</td>
                    <td class="sslbl" width="100%"><asp:TextBox ID="txtDate2" runat="server" CssClass="ssddl" Width="159px" />&#160;<cc1:CalendarExtender ID="txtDate1_CalendarExtender0" runat="server" 
                                        format="dd/MM/yyyy" popupbuttonid="Image3" targetcontrolid="txtDate2"></cc1:CalendarExtender><asp:ImageButton ID="Image3" runat="server" 
                                        AlternateText="Click to show calendar" ImageUrl="../images/cal.png" />&nbsp;<asp:CheckBox ID="chkAlert2" runat="server" Text="ตั้งเตือน" /><asp:Label ID="lblADate2" runat="server" CssClass="sslbl_red"></asp:Label></td></tr><tr><td class="sslbl" width="100">&#160;</td><td><asp:Button ID="bExplainSave" runat="server" CssClass="ssbtn" Text="บันทึก" 
                                        Width="80px" /><asp:Button ID="bExplainCancel" runat="server" CssClass="ssbtn" Text="ยกเลิก" 
                                        Width="80px" /></td></tr>
                <tr>
                    <td class="sslbl" colspan="2" width="100%">
                        <asp:GridView ID="GridView3" runat="server" AllowPaging="True" 
                            AllowSorting="True" AutoGenerateColumns="False" BackColor="White" 
                            BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="1" 
                            CssClass="GridViewStyle" GridLines="Vertical" Width="100%">
                            <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                            <Columns>
                                <asp:TemplateField HeaderText="รายละเอียด">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("title") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("title") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="80%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="วันที่">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox5" runat="server" Text='<%# Bind("dates") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label5" runat="server" 
                                            Text='<%# Bind("dates", "{0:dd/MM/yyyy}") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="20%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="alert_id" Visible="False">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("alert_id") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("alert_id") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkAlert2" runat="server" CommandName="Select" 
                                            Text='<%#ImagesGet(Eval("alert")) %>' ToolTip="เปิด/ปิด">
                                                </asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="30px" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField ShowHeader="False">
                                    <ItemStyle Width="30px" />
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImageButton9" runat="server" CausesValidation="False" 
                                            CommandName="Delete" ImageUrl="~/Images/DeleteFolderHS.png" Text="Delete" 
                                            ToolTip="ลบ" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ShowHeader="False">
                                    <ItemStyle Width="30px" />
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImageButton12" runat="server" CausesValidation="False" 
                                            CommandName="Edit" ImageUrl="~/Images/Edit.gif" Text="Edit" ToolTip="แก้ไข" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                            <EmptyDataTemplate>
                                <table width="100%">
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
                    </td>
                </tr>
                <tr><td class="sslbl_right" width="15%">&nbsp;</td></tr></table></ContentTemplate></asp:UpdatePanel>
            
            
</ContentTemplate>
        
</cc1:TabPanel>

    </cc1:TabContainer>
    
 </asp:Content>









