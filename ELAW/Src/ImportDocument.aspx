<%@ Page Title="นำเข้าเอกสาร" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="ImportDocument.aspx.vb" Inherits="Src_ImportDocument" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

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
    <cc1:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" 
        Width="100%">
        <cc1:TabPanel runat="server" HeaderText="ข้อมูลหลัก" ID="TabPanel1" TabIndex="1">
            <ContentTemplate>
            <div class="TabContentBlue">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate >
   
                <table border="0" cellpadding="1" cellspacing="1" class="form">
                    <tr>
                        <td class="HeaderGreen" colspan="2">
                            นำเข้าเอกสาร</td>
                    </tr>
                    <tr>
                        <td class="sslbl_right" width="10%">
                            รหัสเอกสาร</td>
                        <td>
                            <asp:TextBox ID="txtDocId" runat="server" CssClass="ssddl" MaxLength="6" 
                                ReadOnly="True" Width="100px"></asp:TextBox>
                            <asp:CheckBox ID="chkSecret" runat="server" ForeColor="White" Text="ความลับ" />
                            <asp:Label ID="lblStatus" runat="server" Visible="False"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="sslbl_right">
                            ประเภทเอกสาร</td>
                        <td>
                            <asp:DropDownList ID="DDType" runat="server" AutoPostBack="True" 
                                CssClass="ssddl" Width="200px">
                                <asp:ListItem Value="1">กฎหมาย</asp:ListItem>
                                <asp:ListItem Value="2">คดี</asp:ListItem>
                                <asp:ListItem Value="3">สัญญา</asp:ListItem>
                                <asp:ListItem Value="4">ข้อหารือ</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="sslbl_right">
                            ชนิดเอกสาร</td>
                        <td>
                            <asp:DropDownList ID="DDLawType" runat="server" CssClass="ssddl" Width="200px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="sslbl_right">
                            ชื่อเอกสาร</td>
                        <td>
                            <asp:TextBox ID="txtDocName" runat="server" CssClass="ssddl" 
                                Text='<%# BindField("doc_name") %>' Width="600px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="sslbl_right">
                            วันที่นำเข้า</td>
                        <td>
                            <asp:TextBox ID="txtReceiveDate" runat="server" CssClass="ssddl" 
                                Text='<%# BindField("dates_recieve") %>' Width="100px" />
                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" format="dd/MM/yyyy" 
                                popupbuttonid="Image1" targetcontrolid="txtreceivedate" Enabled="True">
                            </cc1:CalendarExtender>
                            &nbsp;<asp:ImageButton ID="Image1" runat="server" 
                                AlternateText="Click to show calendar" ImageUrl="../images/cal.png" 
                                Width="16px" />
                        </td>
                    </tr>
                    <tr>
                        <td class="sslbl_right">
                            วันที่หมดอายุ</td>
                        <td>
                            <asp:TextBox ID="txtExpireDate" runat="server" CssClass="ssddl" 
                                Text='<%# BindField("dates_recieve") %>' Width="100px" />
                            &nbsp;<asp:ImageButton ID="Image2" runat="server" 
                                AlternateText="Click to show calendar" ImageUrl="../images/cal.png" 
                                Width="16px" />
                            <cc1:CalendarExtender ID="txtExpireDate_CalendarExtender" runat="server" 
                                Enabled="True" format="dd/MM/yyyy" popupbuttonid="Image2" 
                                targetcontrolid="txtExpireDate">
                            </cc1:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td class="sslbl_right">
                            ชื่อผู้นำเข้าเอกสาร</td>
                        <td>
                            <asp:TextBox ID="txtName1" runat="server" CssClass="ssddl" ReadOnly="True" 
                                Text='<%# BindField("name1") %>' Width="300px"></asp:TextBox>
                            &nbsp;<asp:Button ID="bSelect1" runat="server" CssClass="ssbtn" Text="เลือก" />
                            <asp:Label ID="lblIdName1" runat="server" Text='<%# BindField("name_imp") %>' 
                                Visible="False"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="sslbl_right">
                            ไฟล์เอกสาร</td>
                        <td>
                            <asp:FileUpload ID="FileUpload1" runat="server" CssClass="ssddl" 
                                Width="600px" />
                        </td>
                    </tr>
                    <tr>
                        <td class="sslbl_right">
                            จำนวนหน้า</td>
                        <td>
                            <asp:TextBox ID="txtDocPage" runat="server" CssClass="ssddl" MaxLength="6" 
                                Text='<%# BindField("doc_page") %>' Width="60px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="sslbl">&nbsp;
                            </td>
                        <td>
                            <asp:Button ID="bSave" runat="server" CssClass="ssbtn" Text="บันทึก" 
                                Width="80px" />
                            <asp:Button ID="bCancel" runat="server" CssClass="ssbtn" Text="ยกเลิก" 
                                Width="80px" />
                        </td>
                    </tr>
                </table>
           <table class="style5">
                              <tr>
                        <td class="HeaderGreen" colspan="2">
                            คำค้นหา</td>
                    </tr>
                            <tr>
                                <td class="sslbl_right" width="10%">
                                    1</td>
                                <td>
                                    <asp:TextBox ID="txtKey1" runat="server" CssClass="ssddl" Rows="1" 
                                        Width="600px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td width="5%" class="sslbl_right">
                                    2</td>
                                <td>
                                    <asp:TextBox ID="txtKey2" runat="server" CssClass="ssddl" Rows="1" 
                                        Width="600px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td width="5%" class="sslbl_right">
                                    3</td>
                                <td>
                                    <asp:TextBox ID="txtKey3" runat="server" CssClass="ssddl" Rows="1" 
                                        Width="600px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td width="5%" class="sslbl_right">
                                    4</td>
                                <td>
                                    <asp:TextBox ID="txtKey4" runat="server" CssClass="ssddl" Rows="1" 
                                        Width="600px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td width="5%" class="sslbl_right">
                                    5</td>
                                <td>
                                    <asp:TextBox ID="txtKey5" runat="server" CssClass="ssddl" Rows="1" 
                                        Width="600px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td width="5%">&nbsp;
                                    </td>
                                <td>
                                    <asp:Button ID="bAdd" runat="server" CssClass="ssbtn" Text="เพิ่ม" 
                                        Width="80px" />
                                    <asp:Button ID="bCancelAdd" runat="server" CssClass="ssbtn" Text="ยกเลิก" 
                                        Width="80px" />
                                </td>
                            </tr>
                            <tr>
                                <td width="5%" class="sslbl_right">
                                    ค้นหา</td>
                                <td>
                                    <asp:TextBox ID="txtSearch" runat="server" CssClass="ssddl" Rows="1" 
                                        Width="500px"></asp:TextBox>
                                    <asp:Button ID="bSearch" runat="server" CssClass="ssbtn" Text="ค้นหา" 
                                        Width="80px" />
                                    <asp:Button ID="bDel" runat="server" CssClass="ssbtn" Font-Bold="True" 
                                        ForeColor="Red" Text="ลบ" Width="80px" />
                                </td>
                            </tr>
                            <tr>
                                <td width="5%">&nbsp;
                                    </td>
                                <td>
                                    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
                                        AllowSorting="True" AutoGenerateColumns="False" BackColor="White" 
                                        BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="1" 
                                        CssClass="GridViewStyle" GridLines="Vertical" PageSize="20" Width="100%">
                                        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                                        <Columns>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="cb1" runat="server" AutoPostBack="true" 
                                                        OnCheckedChanged="cb1_Checked" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    &nbsp;<asp:CheckBox ID="cb1" runat="server" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle Width="1%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="คำค้นหา" SortExpression="keyword">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("keyword") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("keyword") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="95%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField ShowHeader="False">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Center" Width="4%" />
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" 
                                                        CommandName="Delete" ImageUrl="~/Images/DeleteFolderHS.png" Text="Delete" 
                                                        ToolTip="ลบ" />
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
                 <Triggers>
                <asp:PostBackTrigger ControlID="bSave" />
                 </Triggers>
                </asp:UpdatePanel>

                
                </div> 
            </ContentTemplate>
        
        </cc1:TabPanel>
        <cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="เชื่อมโยงเอกสาร" TabIndex="2">
            <HeaderTemplate>
                เชื่อมโยงเอกสาร
            </HeaderTemplate>
            <ContentTemplate>
             <div class="TabContentBlue">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                       
                        <table class="style5">
                            <tr>
                                <td class="sslbl_bold">
                                    คำค้นหา</td>
                                <td>
                                    <asp:TextBox ID="txtKeyword" runat="server" CssClass="ssddl" Width="600px"></asp:TextBox>
                                    &nbsp;<asp:Button ID="bSearchLaw" runat="server" Text="ค้นหา" Width="70px" />
                                </td>
                            </tr>
                            <tr>
                                <td class="sslbl_bold">
                                    ประเภทกฎหมาย</td>
                                <td>
                                    <asp:DropDownList ID="ddlLawType" runat="server" CssClass="ssddl" Height="20px" 
                                        Width="200px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="sslbl_bold">
                                    เลือกกฎหมายที่เกี่ยวข้อง</td>
                                <td class="">
                                    <asp:ListBox ID="ListBox1" runat="server" CssClass="ssddl" Height="241px" 
                                        Width="700px"></asp:ListBox>
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;
                                    </td>
                                <td>
                                    <asp:Button ID="Button2" runat="server" Text="Button" />
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;
                                    </td>
                                <td>
                                    <asp:ListBox ID="ListBox2" runat="server" CssClass="ssddl" Height="241px"
                                        Width="700px"></asp:ListBox>
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;
                                    </td>
                                <td>&nbsp;
                                    </td>
                            </tr>
                        </table>
                       
                    </ContentTemplate>
                </asp:UpdatePanel>
                </div> 
            </ContentTemplate>
        </cc1:TabPanel>
    </cc1:TabContainer>
   </asp:Content>

