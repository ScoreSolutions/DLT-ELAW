<%@ Page Title="ดูรายละเอียด-LAW3404" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" ValidateRequest="false" CodeFile="CasePreview.aspx.vb" Inherits="Src_CasePreview" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<%@ Register assembly="FredCK.FCKeditorV2" namespace="FredCK.FCKeditorV2" tagprefix="FCKeditorV2" %>


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
                        <asp:LinkButton ID="linkHome" runat="server">Home</asp:LinkButton>
                        &nbsp;&gt;
                                <asp:LinkButton ID="link1" runat="server">งานคดี</asp:LinkButton>
&nbsp;&gt;
                                <asp:LinkButton ID="link2" runat="server">ข้อมูลคดี</asp:LinkButton>
                        &nbsp;&gt;
                                <asp:LinkButton ID="link3" runat="server">รายละเอียด</asp:LinkButton>
                        </div>
                           	
                      
                        <!--end content -->
                 </td>
               </tr>
             </table>
   
    <cc1:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" 
        Width="100%">
        <cc1:TabPanel runat="server" HeaderText="ข้อมูลหลัก" ID="TabPanel1" TabIndex="1">
            <HeaderTemplate>ข้อมูลหลัก
            </HeaderTemplate>
            <ContentTemplate><div class="TabContentBlue"><asp:UpdatePanel ID="UpdatePanel1" runat="server"><ContentTemplate><table border="0" cellpadding="1" cellspacing="1" class="form"><tr><td class="style1">ประเภทคดี</td><td class="sslbl"><asp:Label ID="lblCaseType" runat="server" Text='<%# BindField("type_name") %>'></asp:Label><asp:Label ID="lblId" runat="server" Text='<%# BindField("case_id") %>' 
                                        Visible="False"></asp:Label><asp:Label ID="lblCaseNo" runat="server" Text='<%# BindField("case_no") %>' 
                                        Visible="False"></asp:Label><asp:Label ID="lblIdNew" runat="server" Visible="False"></asp:Label></td><td class="sslbl_black">สถานะ</td><td class="sslbl" width="30%"><asp:Label ID="lblStatus" runat="server" Text='<%# BindField("status_name") %>'></asp:Label><asp:Label ID="lblStatusId" runat="server" Text='<%# BindField("status_id") %>' 
                                        Visible="False"></asp:Label></td></tr><tr><td class="style1">หมายเลขคดีดำ</td><td class="sslbl"><asp:Label ID="lblBlackNo" runat="server" Text='<%# BindField("black_no") %>'></asp:Label></td><td class="sslbl_black">หมายเลขคดีแดง</td><td class="sslbl" width="30%"><asp:Label ID="lblRedNo" runat="server" 
                                        Text='<%# BindField("red_no") %>'></asp:Label></td></tr><tr><td class="style1">สำนักงานอัยการ</td><td class="sslbl"><asp:Label ID="lblCourt" runat="server" Text='<%# BindField("court_name") %>'></asp:Label></td><td class="sslbl_black">ชื่อพนักงานอัยการ</td><td class="sslbl" width="30%"><asp:Label ID="lblAttornney" runat="server" 
                                        Text='<%# BindField("attorney_name") %>'></asp:Label><asp:Label ID="lbl" runat="server" Font-Bold="True" Text="โทร. "></asp:Label><asp:Label ID="lblTel" runat="server" Text='<%# BindField("tel") %>'></asp:Label></td></tr><tr><td class="style1">ปิดหมาย/รับหมาย/ส่งฟ้อง</td><td class="sslbl"><asp:Label ID="lblCloseRecieve" runat="server"></asp:Label></td><td class="sslbl_black">วันที่</td><td class="sslbl" width="30%"><asp:Label ID="lblRecieveDate" runat="server" 
                                        Text='<%# BindField("recieve_date") %>'></asp:Label></td></tr><tr><td class="style1">โจทก์</td><td class="sslbl"><asp:Label ID="lblProsecutor" runat="server" 
                                        Text='<%# BindField("prosecutor") %>'></asp:Label></td><td class="sslbl_black">โจทก์ร่วม</td><td class="sslbl" width="30%"><asp:Label ID="lblProsecutor1" runat="server" 
                                        Text='<%# BindField("prosecutor1") %>'></asp:Label></td></tr><tr><td class="style1">จำเลย</td><td class="sslbl"><asp:Label ID="lblDefandent" runat="server" 
                                        Text='<%# BindField("defendant") %>'></asp:Label></td><td class="sslbl_black">จำเลยร่วม</td><td class="sslbl" width="30%"><asp:Label ID="lblDefandent1" runat="server" 
                                        Text='<%# BindField("defendant1") %>'></asp:Label></td></tr><tr><td class="style1">หัวหน้ากลุ่มงานนิติกรรมและคดี</td><td class="sslbl"><asp:Label ID="lblApp1" runat="server" 
                                        Text='<%# BindField("appname1") %>'></asp:Label></td><td class="sslbl_black">ผู้อำนวยการ</td><td class="sslbl" width="30%"><asp:Label ID="lblApp2" runat="server" 
                                        Text='<%# BindField("appname2") %>'></asp:Label></td></tr><tr><td class="style1">คำค้นหา</td><td class="sslbl"><asp:Label ID="lblKeyword" runat="server" Text='<%# BindField("keyword") %>'></asp:Label></td><td class="sslbl_black">ชื่อศาล</td><td class="sslbl" width="30%"><asp:Label ID="lblCourtName" runat="server" Text='<%# BindField("cname") %>'></asp:Label></td></tr><tr><td class="style1">เจ้าของเรื่อง</td><td class="sslbl" colspan="3"><asp:Label ID="lblCreateName" runat="server" Text='<%# BindField("createname") %>'></asp:Label></td></tr><tr><td class="style1">อ้างอิงหนังสือรับเรื่อง</td><td class="sslbl" colspan="3"><asp:Label ID="lblTopic" runat="server" Text='<%# BindField("topic") %>'></asp:Label>&#160;<asp:LinkButton ID="LinkDetail" runat="server">ดูรายละเอียด</asp:LinkButton></td></tr><tr><td class="style1">อ้างอิงหนังสือนำส่งเรื่อง</td><td class="sslbl" colspan="3"><asp:GridView ID="GridView4" runat="server" AllowPaging="True" 
                                        AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" 
                                        ForeColor="#333333" GridLines="None" PageSize="20" ShowHeader="False" 
                                        Width="100%"><RowStyle BackColor="White" /><Columns><asp:TemplateField HeaderText="ลำดับ"><ItemStyle HorizontalAlign="Center" Width="3%" /><HeaderStyle HorizontalAlign="Center" /><ItemTemplate><%#iRow%>.</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="รหัส" Visible="false"><ItemTemplate><asp:Label ID="lblIdBook" runat="server" Text='<%# Bind("bookout_id") %>'></asp:Label></ItemTemplate><ItemStyle Width="10%" /></asp:TemplateField><asp:TemplateField HeaderText="เลขที่หนังสือนำส่ง" ><ItemTemplate><asp:Label ID="lblIdBookNo" runat="server" Text='<%# Bind("bookout_no") %>'></asp:Label></ItemTemplate><ItemStyle Width="20%" /></asp:TemplateField><asp:TemplateField HeaderText="เรื่อง" SortExpression="topic"><ItemTemplate><asp:LinkButton ID="LinkName" runat="server" CommandName="cName" 
                                                        Text='<%# Bind("topic") %>'></asp:LinkButton></ItemTemplate><ItemStyle Width="70%" /></asp:TemplateField></Columns><FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" /><PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" /><EmptyDataTemplate></EmptyDataTemplate><SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" /><HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" /><EditRowStyle BackColor="#2461BF" /><AlternatingRowStyle BackColor="White" /></asp:GridView></td></tr><tr><td class="style1">อ้างอิงคดี</td><td class="sslbl" colspan="3"><asp:GridView ID="GridView5" runat="server" AllowPaging="True" 
                                        AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" 
                                        ForeColor="#333333" GridLines="None" PageSize="20" ShowHeader="False" 
                                        Width="100%"><RowStyle BackColor="White" /><Columns><asp:TemplateField HeaderText="ลำดับ"><ItemStyle HorizontalAlign="Center" Width="3%" /><HeaderStyle HorizontalAlign="Center" /><ItemTemplate><%#iRow%>.</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="เลขที่สัญญา" Visible ="false" ><ItemTemplate><asp:Label ID="lblIdCaseRef" runat="server" 
                                                        Text='<%# Bind("case_id") %>'></asp:Label></ItemTemplate><ItemStyle Width="20%" /></asp:TemplateField><asp:TemplateField HeaderText="คดี" SortExpression="casename"><ItemTemplate><asp:LinkButton ID="LinkCaseName" runat="server" 
                                                        CommandName="cLinkCase" Text='<%# Bind("casename") %>'></asp:LinkButton></ItemTemplate><ItemStyle Width="70%" /></asp:TemplateField></Columns><FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" /><PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" /><EmptyDataTemplate></EmptyDataTemplate><SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" /><HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" /><EditRowStyle BackColor="#2461BF" /><AlternatingRowStyle BackColor="White" /></asp:GridView></td></tr><tr><td class="style1">&#160;</td><td class="sslbl">&#160;</td><td class="style10">&#160;</td><td class="sslbl_right" width="30%"><asp:Label ID="lblPrint" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label></td></tr></table></ContentTemplate></asp:UpdatePanel></div> 
            </ContentTemplate>
            </cc1:TabPanel>
        
        <cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="รายละเอียด" TabIndex="2">
            <ContentTemplate><div class="TabContentBlue"><asp:UpdatePanel ID="UpdatePanel2" runat="server"><ContentTemplate><table border="0" cellpadding="1" cellspacing="1" class="form"><tr><td class="sslbl"><FCKeditorV2:FCKeditor 
                    ID="FCKeditor2" runat="server" BasePath="~/fckeditor/" Height="500px" 
                    Value='<%# BindField("detail") %>'></FCKeditorV2:FCKeditor></td></tr><tr><td 
                        align="right" class="sslbl"><asp:LinkButton ID="LPrint1" runat="server">พิมพ์รายละเอียด</asp:LinkButton></td></tr></table></ContentTemplate></asp:UpdatePanel></div> 
            </ContentTemplate>
        </cc1:TabPanel>
        
        <cc1:TabPanel ID="TabPanel3" runat="server" HeaderText="เอกสารประกอบ" TabIndex="3">
            <ContentTemplate><div class="TabContentBlue"><asp:UpdatePanel ID="UpdatePanel3" runat="server"><ContentTemplate><table border="0" cellpadding="1" cellspacing="1" class="form" width="100%"><tr><td><asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
                                        AllowSorting="True" AutoGenerateColumns="False" BackColor="White" 
                                        BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="1" 
                                        CssClass="GridViewStyle" GridLines="Vertical" Width="100%"><RowStyle BackColor="#EEEEEE" ForeColor="Black" /><Columns><asp:TemplateField HeaderText="ชื่อเอกสาร"><EditItemTemplate><asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("title") %>'></asp:TextBox></EditItemTemplate><ItemTemplate><asp:Label ID="Label1" runat="server" Text='<%# Bind("title") %>'></asp:Label></ItemTemplate><ItemStyle Width="70%" /></asp:TemplateField><asp:TemplateField HeaderText="จำนวนหน้า"><EditItemTemplate><asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("page") %>'></asp:TextBox></EditItemTemplate><ItemTemplate><asp:Label ID="Label2" runat="server" Text='<%# Bind("page") %>'></asp:Label></ItemTemplate><ItemStyle HorizontalAlign="Center" Width="20%" /></asp:TemplateField><asp:TemplateField HeaderText="ดาวน์โหลด"><ItemTemplate><asp:Label ID="lblLink" runat="server" Text=""></asp:Label></ItemTemplate><ItemStyle HorizontalAlign="Center" Width="10%" /></asp:TemplateField></Columns><FooterStyle BackColor="#CCCCCC" ForeColor="Black" /><PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" /><EmptyDataTemplate><table width="100%"><tr><td align="center"><asp:Label ID="Label8" runat="server" CssClass="sslbl_red" 
                                        Text="***ไม่พบข้อมูล***"></asp:Label></td></tr></table></EmptyDataTemplate><SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" /><HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" /><AlternatingRowStyle BackColor="#DCDCDC" /></asp:GridView></td></tr></table></ContentTemplate></asp:UpdatePanel></div> 
            </ContentTemplate>
           </cc1:TabPanel>
           
        <cc1:TabPanel ID="TabPanel4" runat="server" HeaderText="ข้อมูลศาล/อัยการนัด วันครบกำหนด" TabIndex="4">
            <ContentTemplate><div class="TabContentBlue"><asp:UpdatePanel ID="UpdatePanel4" runat="server"><ContentTemplate><table border="0" cellpadding="1" cellspacing="1" class="form" width="100%"><tr><td><asp:GridView ID="GridView2" runat="server" AllowPaging="True" 
                                        AllowSorting="True" AutoGenerateColumns="False" BackColor="White" 
                                        BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="1" 
                                        CssClass="GridViewStyle" GridLines="Vertical" Width="100%"><RowStyle BackColor="#EEEEEE" ForeColor="Black" /><Columns><asp:TemplateField HeaderText="รายละเอียด"><EditItemTemplate><asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("title") %>'></asp:TextBox></EditItemTemplate><ItemTemplate><asp:Label ID="Label3" runat="server" Text='<%# Bind("title") %>'></asp:Label></ItemTemplate><ItemStyle Width="80%" /></asp:TemplateField><asp:TemplateField HeaderText="วันที่"><EditItemTemplate><asp:TextBox ID="TextBox6" runat="server" Text='<%# Bind("dates") %>'></asp:TextBox></EditItemTemplate><ItemTemplate><asp:Label ID="Label6" runat="server" 
                                                        Text='<%# Bind("dates", "{0:dd/MM/yyyy}") %>'></asp:Label></ItemTemplate><ItemStyle Width="20%" /></asp:TemplateField></Columns><FooterStyle BackColor="#CCCCCC" ForeColor="Black" /><PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" /><EmptyDataTemplate><table width="100%"><tr><td align="center"><asp:Label ID="Label8" runat="server" CssClass="sslbl_red" 
                                        Text="***ไม่พบข้อมูล***"></asp:Label></td></tr></table></EmptyDataTemplate><SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" /><HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" /><AlternatingRowStyle BackColor="#DCDCDC" /></asp:GridView></td></tr></table></ContentTemplate></asp:UpdatePanel></div> 
            </ContentTemplate>
            </cc1:TabPanel>
            
        <cc1:TabPanel ID="TabPanel5" runat="server" HeaderText="ข้อมูลขยายเวลายื่นคำชี้แจง" TabIndex="5">
            <ContentTemplate><div class="TabContentBlue"><asp:UpdatePanel ID="UpdatePanel5" runat="server"><ContentTemplate><table border="0" cellpadding="1" cellspacing="1" class="form" width="100%"><tr><td><asp:GridView ID="GridView3" runat="server" AllowPaging="True" 
                                        AllowSorting="True" AutoGenerateColumns="False" BackColor="White" 
                                        BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="1" 
                                        CssClass="GridViewStyle" GridLines="Vertical" Width="100%"><RowStyle BackColor="#EEEEEE" ForeColor="Black" /><Columns><asp:TemplateField HeaderText="รายละเอียด"><EditItemTemplate><asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("title") %>'></asp:TextBox></EditItemTemplate><ItemTemplate><asp:Label ID="Label4" runat="server" Text='<%# Bind("title") %>'></asp:Label></ItemTemplate><ItemStyle Width="80%" /></asp:TemplateField><asp:TemplateField HeaderText="วันที่"><EditItemTemplate><asp:TextBox ID="TextBox5" runat="server" Text='<%# Bind("dates") %>'></asp:TextBox></EditItemTemplate><ItemTemplate><asp:Label ID="Label5" runat="server" 
                                                        Text='<%# Bind("dates", "{0:dd/MM/yyyy}") %>'></asp:Label></ItemTemplate><ItemStyle Width="20%" /></asp:TemplateField></Columns><FooterStyle BackColor="#CCCCCC" ForeColor="Black" /><PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" /><EmptyDataTemplate><table width="100%"><tr><td align="center"><asp:Label ID="Label8" runat="server" CssClass="sslbl_red" 
                                        Text="***ไม่พบข้อมูล***"></asp:Label></td></tr></table></EmptyDataTemplate><SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" /><HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" /><AlternatingRowStyle BackColor="#DCDCDC" /></asp:GridView></td></tr></table></ContentTemplate></asp:UpdatePanel></div> 
            </ContentTemplate>
                   </cc1:TabPanel>
                   
        <cc1:TabPanel ID="TabPanel6" runat="server" HeaderText="หัวหน้าพิจารณา" TabIndex="6">
            <ContentTemplate><div class="TabContentBlue"><asp:UpdatePanel ID="UpdatePanel7" runat="server"><ContentTemplate><table border="0" cellpadding="1" cellspacing="1" class="form" width="100%"><tr><td class="sslbl_right">รายละเอียด</td><td class="sslbl" width="85%"><FCKeditorV2:FCKeditor ID="FCKeditorApp1" runat="server" 
                                           BasePath="~/fckeditor/" Height="500px" Value='<%# BindField("detail_app1") %>'></FCKeditorV2:FCKeditor></td></tr><tr><td class="sslbl_right">ความคิดเห็น</td><td class="sslbl" width="85%"><asp:RadioButtonList ID="rdoApp" runat="server" CssClass="sslbl" 
                                        RepeatColumns="2"><asp:ListItem Value="T">ผ่าน</asp:ListItem><asp:ListItem Value="F">แก้ไข</asp:ListItem></asp:RadioButtonList><asp:Label ID="lblAApp1" runat="server" CssClass="sslbl_red"></asp:Label></td></tr><tr><td class="sslbl_right">บันทึก</td><td><asp:TextBox ID="txtAppComent" runat="server" CssClass="ssddl" Rows="15" 
                                        Text='<%# BindField("app1_comment") %>' TextMode="MultiLine" Width="100%"></asp:TextBox><br /><asp:Label ID="lblAComment1" runat="server" CssClass="sslbl_red"></asp:Label></td></tr><tr><td class="sslbl_right"><asp:Label ID="lblAppText" runat="server" Text="ลงชื่อ"></asp:Label></td><td><asp:Label ID="lblAppName" runat="server" CssClass="sslbl" 
                                        Text='<%# BindField("appname1") %>'></asp:Label></td></tr><tr><td class="style8">&#160;</td><td><asp:Button ID="bApp" runat="server" CssClass="ssbtn" Text="บันทึก" 
                                           Width="80px" /><asp:Button ID="bAppAndSend" runat="server" CssClass="ssbtn" 
                                           Text="บันทึกพร้อมส่ง" Width="159px" /><asp:Button ID="bAppCancel" runat="server" CssClass="ssbtn" Text="ยกเลิก" 
                                           Width="80px" /></td></tr></table></ContentTemplate></asp:UpdatePanel></div>
            </ContentTemplate>
        </cc1:TabPanel>
        
        <cc1:TabPanel ID="TabPanel7" runat="server" HeaderText="ผู้อำนวยการพิจารณา" TabIndex="7">
            <ContentTemplate><div class="TabContentBlue"><asp:UpdatePanel ID="UpdatePanel8" runat="server"><ContentTemplate><table border="0" cellpadding="1" cellspacing="1" class="form" width="100%"><tr><td class="sslbl_right">รายละเอียด</td><td class="sslbl" width="85%"><FCKeditorV2:FCKeditor ID="FCKeditorApp2" runat="server" 
                                           BasePath="~/fckeditor/" Height="500px" Value='<%# BindField("detail_app2") %>'></FCKeditorV2:FCKeditor></td></tr><tr><td class="sslbl_right">ความคิดเห็น</td><td class="sslbl" width="85%"><asp:RadioButtonList ID="rdoApp1" runat="server" RepeatColumns="2"><asp:ListItem Value="T">ผ่าน</asp:ListItem><asp:ListItem Value="F">แก้ไข</asp:ListItem></asp:RadioButtonList><asp:Label ID="lblAApp2" runat="server" CssClass="sslbl_red"></asp:Label></td></tr><tr><td class="sslbl_right">บันทึก</td><td><asp:TextBox ID="txtAppComment1" runat="server" CssClass="ssddl" Rows="15" 
                                        Text='<%# BindField("app2_comment") %>' TextMode="MultiLine" Width="100%"></asp:TextBox><br /><asp:Label ID="lblAComment2" runat="server" CssClass="sslbl_red"></asp:Label></td></tr><tr><td class="sslbl_right">ลงชื่อ</td><td><asp:Label ID="lblAppName1" runat="server" CssClass="sslbl" 
                                        Text='<%# BindField("appname1") %>'></asp:Label></td></tr><tr><td class="sslbl_right">&#160;</td><td><asp:Button ID="bApp1" runat="server" CssClass="ssbtn" Text="บันทึก" 
                                           Width="80px" /><asp:Button ID="bAppAndSend1" runat="server" CssClass="ssbtn" 
                                           Text="บันทึกพร้อมส่ง" Width="159px" /><asp:Button ID="bAppCancel1" runat="server" CssClass="ssbtn" Text="ยกเลิก" 
                                           Width="80px" /></td></tr></table></ContentTemplate></asp:UpdatePanel></div> 
            </ContentTemplate>
        </cc1:TabPanel>
        
        <cc1:TabPanel ID="TabPanel8" runat="server" HeaderText="สถานะคดี" TabIndex="8">
            <HeaderTemplate>สถานะคดี
            </HeaderTemplate>
            <ContentTemplate><div class="TabContentBlue"><asp:UpdatePanel ID="UpdatePanel6" runat="server"><ContentTemplate><table border="0" cellpadding="1" cellspacing="1" class="form"><tr><td class="sslbl_right">สถานะคดี</td><td width="85%"><asp:DropDownList ID="DDStatus" runat="server" CssClass="ssddl" Height="20px" 
                                           Width="270px"></asp:DropDownList></td></tr><tr><td class="sslbl_right">บันทึก</td><td width="85%"><asp:TextBox ID="txtComment" runat="server" CssClass="ssddl" Rows="15" 
                                        Text='<%# BindField("app1_comment") %>' TextMode="MultiLine" Width="100%"></asp:TextBox></td></tr><tr><td class="style10">&#160;</td><td><asp:Button ID="bUpdateStatus" runat="server" CssClass="ssbtn" 
                                           Text="ปรับปรุงสถานะ" Width="100px" /></td></tr></table></ContentTemplate></asp:UpdatePanel></div> 
            </ContentTemplate>
        </cc1:TabPanel> 
    </cc1:TabContainer>
    
  
</asp:Content>








<asp:Content ID="Content3" runat="server" contentplaceholderid="head">

    </asp:Content>









