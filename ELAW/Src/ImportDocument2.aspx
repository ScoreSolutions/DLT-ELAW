<%@ Page Title="นำเข้าเอกสาร-LAW1102" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="ImportDocument2.aspx.vb" Inherits="Src_ImportDocument2" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<%@ Register src="../UserControl/DatePicker.ascx" tagname="DatePicker" tagprefix="uc1" %>
<%@ Register src="../UserControl/ComboBox.ascx" tagname="ComboBox" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <script type="text/javascript">
function CheckNumericKeyInfo(e) {
e = e || window.event;
ch = e.which || e.keyCode;
if (ch != null) {
if ((ch >= 48 && ch <= 57)
|| ch == 0 || ch == 8
|| ch == 59|| ch == 13) return true;
}
//alert('กรุณาใส่ค่าที่เป็นตัวเลข');
return false;
}
</script>
 <script type="text/javascript">
    // ย้ายข้อมูลที่เลือกจาก list box หนึ่งไปยัง listbox หนึ่ง
    function lstmove(lst1, lst2) {
        lsSrc = document.getElementById(lst1);
        lsDes = document.getElementById(lst2);

        for (var i = 0; i < lsSrc.length; i++) {
            opt = lsSrc.options[i];
            if (opt.selected)
                lsDes.add(new Option(opt.text, opt.value));
        }

        for (var i = lsSrc.length - 1; i >= 0; i--) {
            opt = lsSrc.options[i];
            if (opt.selected)
                lsSrc.remove(i);
        }
    }

    // ย้ายข้อมูลทั้งหมดจาก list box หนึ่งไปยัง listbox หนึ่ง
    function lstmoveall(lst1, lst2) {
        lsSrc = document.getElementById(lst1);
        lsDes = document.getElementById(lst2);

        for (var i = 0; i < lsSrc.length; i++) {
            opt = lsSrc.options[i];
            lsDes.add(new Option(opt.text, opt.value));
        }

        for (var i = lsSrc.length - 1; i >= 0; i--) {
            opt = lsSrc.options[i];
            lsSrc.remove(i);
        }
    }

    function getlstvalue(lst, tmp) {
        zlst = document.getElementById(lst);
        ztmp = document.getElementById(tmp);
        ret = '';
        for (var i = 0; i < zlst.length; i++) {
            ret = ret + zlst.options[i].value;
            if (i < zlst.length - 1)
                ret = ret + ',';
        }
        //ztmp.value = ret;
    }
</script>


<table class="form" width="100%" border="0"  >
               <tr valign="top">
                 <td>	
                 		<!--content -->
                 		
                            <div class="HeaderGreen">
                                <asp:LinkButton ID="linkHome" runat="server">Home</asp:LinkButton>
                                &nbsp;&gt;
                                <asp:LinkButton ID="link1" runat="server">บริการข้อมูลกฎหมาย</asp:LinkButton>
&nbsp;&gt;
                                <asp:LinkButton ID="link2" runat="server">ตารางนำเข้าเอกสาร</asp:LinkButton>
&nbsp;&gt;
                                <asp:LinkButton ID="link3" runat="server">นำเข้าเอกสาร</asp:LinkButton>
                        </div>
                           	
                      
                        <!--end content -->
                 </td>
               </tr>
             </table>
    <cc1:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0"
        Width="100%" AutoPostBack="True">
        <cc1:TabPanel runat="server" HeaderText="ข้อมูลหลัก" ID="TabPanel1" TabIndex="1" Height="100%">
            <HeaderTemplate>
ข้อมูลหลัก
</HeaderTemplate>
            
<ContentTemplate>
<div class="TabContentBlue"><asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate >
<table border="0" cellpadding="1" cellspacing="1" class="form">
            <tr><td class="sslbl_right" width="15%">รหัสเอกสาร</td><td>
                    <asp:TextBox ID="txtDocId" runat="server" CssClass="ssddl" MaxLength="15" 
                        ReadOnly="True" Width="100px"></asp:TextBox>&#160;
                    <asp:Label ID="Label12" runat="server" ForeColor="Red" Text="(Auto)"></asp:Label>
                    <asp:CheckBox ID="chkSecret" runat="server" CssClass="sslbl" ForeColor="Black" Text="เฉพาะสำนักกฎหมาย" />
                    <asp:Label ID="lblStatus" runat="server" Visible="False"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="sslbl_right">อ้างอิงร่างกฎหมาย</td>
                <td><asp:TextBox ID="txtLawID" runat="server" CssClass="ssddl" ReadOnly="True" Width="100px"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="sslbl_right">ประเภทเอกสาร</td>
                <td>
                    <asp:DropDownList ID="DDType" runat="server" AutoPostBack="True" 
                        CssClass="ssddl" Width="450px"><asp:ListItem Value="1">กฎหมาย</asp:ListItem><asp:ListItem 
                        Value="2">คดี</asp:ListItem><asp:ListItem Value="3">สัญญา</asp:ListItem><asp:ListItem 
                        Value="4">ข้อหารือ</asp:ListItem></asp:DropDownList>
                </td></tr>
            <tr><td class="sslbl_right">ชนิดเอกสาร</td>
                <td><asp:DropDownList ID="DDLawType" runat="server" CssClass="ssddl" Width="450px" 
                        AutoPostBack="True"></asp:DropDownList>
                    <asp:Button ID="bRename" runat="server" Text="เปลี่ยนรหัสเอกสาร" 
                        Visible="False" />
                    <asp:Label ID="lblOldId" runat="server"></asp:Label>
                    <asp:Label ID="lblOldLink" runat="server"></asp:Label>
                </td></tr>
            <tr>
                <td class="sslbl_right">ชื่อเอกสาร</td>
                <td><asp:TextBox ID="txtDocName" runat="server" CssClass="ssddl" 
                        Text='<%# BindField("doc_name") %>' Width="600px"></asp:TextBox>
                        <asp:Label ID="Label13" runat="server" ForeColor="Red" Text="*"></asp:Label>
                    <asp:Label ID="lblAName" runat="server" ForeColor="Red"></asp:Label>
                    </td></tr><tr>
                <td class="sslbl_right">วันที่นำเข้า</td>
                <td>
                    <uc1:DatePicker ID="txtReceiveDate" runat="server" Text='<%# BindField("dates_recieve") %>' />
                    <asp:Label ID="lblADate" runat="server" ForeColor="Red"></asp:Label>
                        <asp:Label ID="Label17" runat="server" ForeColor="Red" Text="*"></asp:Label>
                    </td></tr><tr>
                <td class="sslbl_right">ไฟล์เอกสาร</td>
                <td>
                    <asp:FileUpload ID="FileUpload1" runat="server" CssClass="ssddl" 
                        Width="600px" />
                        <asp:Label ID="Label14" runat="server" ForeColor="Red" Text="*"></asp:Label>
                    <asp:Label ID="lblAFile" runat="server" ForeColor="Red"></asp:Label>
                    <asp:LinkButton ID="lbtnDownload" runat="server" Visible="False">เอกสารเดิม</asp:LinkButton>
                    </td></tr><tr><td class="sslbl_right">จำนวนหน้า</td><td>
                    <asp:TextBox ID="txtDocPage" runat="server" CssClass="ssddl" 
            MaxLength="6" Text='<%# BindField("doc_page") %>' Width="60px"></asp:TextBox>
                    <asp:Label ID="Label16" runat="server" ForeColor="Red" Text="*"></asp:Label>
                    <asp:Label ID="lblAPage" runat="server" ForeColor="Red"></asp:Label>
                    </td></tr>
                <tr>
                    <td class="sslbl_right">
                        ยกเลิกเอกสาร</td>
                    <td>
                        <asp:CheckBox ID="chkCancel" runat="server" CssClass="sslbl" ForeColor="Black" 
                            Text="ยกเลิกเอกสาร" />
                    </td>
                </tr>
                <tr>
                    <td class="sslbl_right">
                        สาเหตุที่ยกเลิก</td>
                    <td>
                        <asp:TextBox ID="txtCancel_Comment" runat="server" CssClass="ssddl" Rows="3" 
                            Text='<%# BindField("cancel_comment") %>' TextMode="MultiLine" Width="600px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td ></td>
                    <td>
                        <asp:Button ID="bSave" runat="server" CssClass="ssbtn" Text="บันทึก" 
                            Width="80px" /><asp:Button ID="bCancel" runat="server" CssClass="ssbtn" 
                            Text="ยกเลิก" Width="80px" />
                        <asp:Button ID="bAddNew" runat="server" CssClass="ssbtn" Text="เพิ่มใหม่" 
                            Width="80px" />
                    </td></tr></caption></table>
        <table  width="100%"><tr>
            <td class="HeaderGreen" colspan="2">คำค้นหา</td></tr>
            <tr>
                <td class="sslbl_right" width="15%">
                    คำค้นหา</td>
                <td style="width: 95%" valign="top" width="85%">
                    <asp:TextBox ID="txtKey1" runat="server" CssClass="ssddl" Rows="1" 
                        Width="500px"></asp:TextBox>
                    &nbsp;หน้า
                    <asp:TextBox ID="txtPage1" runat="server" CssClass="ssddl" MaxLength="20" 
                        onkeypress="return CheckNumericKeyInfo(event);" Rows="1" Width="150px"></asp:TextBox>
                    &nbsp;<asp:Button ID="bAdd" runat="server" CssClass="ssbtn" Text="เพิ่ม" 
                        Width="80px" />
                </td>
            </tr>
            <tr><td class="style12">&#160;</td>
                <td>
                    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
                        AllowSorting="True" AutoGenerateColumns="False" CssClass="GridViewStyle" 
                        PageSize="20" Width="100%">
                        <Columns>
                            <asp:TemplateField HeaderText="คำค้นหา" SortExpression="keyword">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("keyword") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("keyword") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle Width="85%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="หน้า" SortExpression="pages">
                                <ItemTemplate>
                                    <asp:Label ID="lpages" runat="server" Text='<%# Bind("pages") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ลบ" ShowHeader="False">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" 
                                        CommandName="Delete" ImageUrl="~/Images/DeleteFolderHS.png" Text="Delete" 
                                        ToolTip="ลบ" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td></tr>
            <tr>
                <td class="style12">&#160;</td><td>&nbsp;</td></tr></table>
</ContentTemplate>
      <Triggers>
                <asp:PostBackTrigger ControlID="bSave" />
                 </Triggers>

</asp:UpdatePanel>
</div>
            
            
</ContentTemplate>
        
</cc1:TabPanel>
        <cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="เชื่อมโยงเอกสาร" TabIndex="2" Height="100%">
            <HeaderTemplate>
เชื่อมโยงเอกสาร
</HeaderTemplate>
            
<ContentTemplate>
<div class="TabContentBlue"><asp:UpdatePanel ID="UpdatePanel1" runat="server"><ContentTemplate><table  width ="100%"><tr>
    <td class="sslbl_right" width="15%">
        คำค้นหา</td>
    <td width="85%">
        <asp:TextBox ID="txtKeyword" runat="server" CssClass="ssddl" Width="445px"></asp:TextBox>
        &nbsp;<asp:Button ID="bSearchLaw" runat="server" Text="ค้นหา" Width="70px" />
        <asp:Label ID="lblId" runat="server" Text='<%# BindField("link_id") %>' 
            Visible="False"></asp:Label>
    </td>
    </tr><tr>
                <td class="sslbl_right">ประเภทเอกสาร</td><td>
                <asp:DropDownList ID="DDTypeSearch" runat="server" AutoPostBack="True" 
                    CssClass="ssddl" Width="450px">
                </asp:DropDownList>
                </td></tr><tr>
        <td class="sslbl_right">ชนิดเอกสาร</td><td>
        <asp:DropDownList ID="ddlLawType" runat="server" 
            CssClass="ssddl" Width="450px" Height="20px">
        </asp:DropDownList>
        </td>
        <td>&nbsp;
            </td>
    </tr>
    <tr>
        <td class="HeaderGreen" colspan="2">
            เอกสารทั้งหมด</td>
        <td>
            &nbsp;</td>
    </tr>
    <tr><td class="style8" colspan="2">
        <div class="AddData">
            <asp:GridView ID="GridView2" runat="server" AllowSorting="True" 
                AutoGenerateColumns="False" BackColor="White" BorderColor="#3366CC" 
                BorderStyle="None" BorderWidth="1px" CellPadding="4" CssClass="GridViewStyle" 
                Width="100%" AllowPaging="True">
                <RowStyle BackColor="White" ForeColor="#003399" />
                <Columns>
                    <asp:CommandField SelectText="เลือก" ShowSelectButton="True">
                        <ItemStyle ForeColor="Green" HorizontalAlign="Center" Width="5%" />
                    </asp:CommandField>
                    <asp:TemplateField HeaderText="ประเภทเอกสาร">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("subtype_name") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label3" runat="server" Text='<%# Bind("subtype_name") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="30%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ชื่อเอกสาร">
                        <ItemTemplate>
                            <asp:Label ID="lblLinkFile" runat="server" Font-Bold="False" 
                                ForeColor="#330099"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="70%" />
                    </asp:TemplateField>
                </Columns>
                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                <SelectedRowStyle Font-Bold="False" />
                <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
            </asp:GridView>
        </div>
        </td></tr>
    <tr>
        <td class="HeaderGreen" colspan="2">
            เอกสารที่เลือก</td>
    </tr>
    <tr>
        <td class="style8" colspan="2">
            <div class="AddData">
                <asp:GridView ID="GridView3" runat="server" AllowPaging="True" 
                    AllowSorting="True" AutoGenerateColumns="False" BackColor="White" 
                    BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                    CssClass="GridViewStyle" Width="100%">
                    <RowStyle BackColor="White" ForeColor="#003399" />
                    <Columns>
                        <asp:CommandField SelectText="ยกเลิก" ShowSelectButton="True">
                            <ItemStyle ForeColor="Red" HorizontalAlign="Center" Width="5%" />
                        </asp:CommandField>
                        <asp:TemplateField HeaderText="ประเภทเอกสาร">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox6" runat="server" Text='<%# Bind("subtype_name") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label10" runat="server" Text='<%# Bind("subtype_name") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="30%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ชื่อเอกสาร">
                            <ItemTemplate>
                                <asp:Label ID="lblLinkFile" runat="server" Font-Bold="False" 
                                    ForeColor="#330099"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="70%" />
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                    <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                    <SelectedRowStyle Font-Bold="False" />
                    <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
                </asp:GridView>
            </div>
        </td>
    </tr>
    </table></ContentTemplate>
      
        </asp:UpdatePanel></div>
            
            
</ContentTemplate>
        
</cc1:TabPanel>
    </cc1:TabContainer>
    

</asp:Content>




