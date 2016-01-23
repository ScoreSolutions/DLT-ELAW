<%@ Page Title="ดูรายละเอียด-LAW4102" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" ValidateRequest="false" CodeFile="ContractPreview.aspx.vb" Inherits="Src_ContractPreview" %>

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
                 		<div class="HeaderGreen">
                        <asp:LinkButton ID="linkHome" runat="server">Home</asp:LinkButton>
                        &nbsp;&gt; <asp:LinkButton ID="link1" runat="server">งานสัญญา</asp:LinkButton>
&nbsp;&gt;
                                <asp:LinkButton ID="link2" runat="server">ข้อมูลสัญญา</asp:LinkButton>
&nbsp;&gt;
                                <asp:LinkButton ID="link3" runat="server">รายละเอียด</asp:LinkButton>
                        </div>
                           	
                      
                        <!--end content -->
                 </td>
               </tr>
             </table>
    <cc1:TabContainer ID="TabContainer1" runat="server"  Width="100%" 
        ActiveTabIndex="0">
        <cc1:TabPanel runat="server" HeaderText="ข้อมูลหลัก" ID="TabPanel1" TabIndex="1">
            <HeaderTemplate>
                ข้อมูลหลัก
            </HeaderTemplate>
            <ContentTemplate>
             <div class="TabContentBlue">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <table border="0" cellpadding="1" cellspacing="1" class="form" width="100%">
                            <tr>
                                <td class="sslbl_black">
                                    เลขที่สัญญา</td>
                                <td class="sslbl">
                                    <asp:Label ID="lblNo" runat="server" Text='<%# BindField("contract_no") %>'></asp:Label>
                                    <asp:Label ID="lblId" runat="server" Text='<%# BindField("contract_id") %>' 
                                        Visible="False"></asp:Label>
                                    <asp:Label ID="lblIdNew" runat="server" Visible="False"></asp:Label>
                                </td>
                                <td class="sslbl_black">&nbsp;
                                    </td>
                                <td class="sslbl" width="30%" align="right">&nbsp;
                                    </td>
                            </tr>
                            <tr>
                                <td class="sslbl_black">
                                    ชื่อสัญญา</td>
                                <td class="sslbl">
                                    <asp:Label ID="lblContractName" runat="server" 
                                        Text='<%# BindField("contract_name") %>'></asp:Label>
                                </td>
                                <td class="sslbl_black">&nbsp;
                                    </td>
                                <td class="sslbl" width="30%" align="right">&nbsp;
                                    </td>
                            </tr>
                            <tr>
                                <td class="sslbl_black">
                                    ประเภทสัญญา</td>
                                <td class="sslbl">
                                    <asp:Label ID="lblSubType" runat="server" 
                                        Text='<%# BindField("subtype_name") %>'></asp:Label>
                                </td>
                                <td class="sslbl_black">
                                    วันที่รับเรื่อง</td>
                                <td class="sslbl" width="30%">
                                    <asp:Label ID="lblDateRecieve" runat="server" 
                                        Text='<%# BindField("dates_recieve") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="sslbl_black">
                                    วันที่คู่สัญญามาลงนาม</td>
                                <td class="sslbl">
                                    <asp:Label ID="lblDateComesign" runat="server" 
                                        Text='<%# BindField("dates_comesign") %>'></asp:Label>
                                </td>
                                <td class="sslbl_black">
                                    วันที่ลงนามในสัญญา</td>
                                <td class="sslbl" width="30%">
                                    <asp:Label ID="lblDateSing" runat="server" 
                                        Text='<%# BindField("dates_sign") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="sslbl_black">
                                    วันที่เริ่มต้นสัญญา</td>
                                <td class="sslbl">
                                    <asp:Label ID="lblDateStart" runat="server" 
                                        Text='<%# BindField("dates_start") %>'></asp:Label>
                                </td>
                                <td class="sslbl_black">
                                    วันที่สิ้นสุดสัญญา</td>
                                <td class="sslbl" width="30%">
                                    <asp:Label ID="lblDateFinish" runat="server" 
                                        Text='<%# BindField("dates_finish") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="sslbl_black">
                                    สถานะ</td>
                                <td class="sslbl">
                                    <asp:Label ID="lblStatus" runat="server" Text='<%# BindField("status_name") %>'></asp:Label>
                                </td>
                                <td class="sslbl_black">
                                    วิธีจัดจ้าง</td>
                                <td class="sslbl" width="30%">
                                    <asp:Label ID="lblProcess" runat="server" 
                                        Text='<%# BindField("process_name") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="sslbl_black">
                                    ระบุสิ่งที่จะซื้อ/จ้าง</td>
                                <td class="sslbl">
                                    <asp:Label ID="lblMaterial" runat="server" 
                                        Text='<%# BindField("material") %>'></asp:Label>
                                </td>
                                <td class="sslbl_black">
                                    เลขประจำตัวผู้เสียภาษีของคู่ัสัญญา</td>
                                <td class="sslbl" width="30%">
                                    <asp:Label ID="lblTax_id" runat="server" 
                                        Text='<%# BindField("tax_id") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="sslbl_black">
                                    หลักประกันสัญญา</td>
                                <td class="sslbl">
                                    <asp:Label ID="lblGuarantee" runat="server" 
                                        Text='<%# BindField("guarantee_name") %>'></asp:Label>
                                </td>
                                <td class="sslbl_black">
                                    เลขที่หลักประกัน</td>
                                <td class="sslbl" width="30%">
                                    <asp:Label ID="lblGuaranteeNo" runat="server" 
                                        Text='<%# BindField("guarantee_no") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="sslbl_black">
                                    ชื่อผู้ขาย/ผู้รับจ้าง</td>
                                <td class="sslbl">
                                    <asp:Label ID="lblUserSale" runat="server" Text='<%# BindField("user_sale") %>'></asp:Label>
                                </td>
                                <td class="sslbl_black">
                                    คำค้นหา</td>
                                <td class="sslbl" width="30%">
                                    <asp:Label ID="lblKeyword" runat="server" Text='<%# BindField("keyword") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="sslbl_black">
                                    หัวหน้ากลุ่มนิติกรรมและคดี</td>
                                <td class="sslbl">
                                    <asp:Label ID="lblIdName1" runat="server" Text='<%# BindField("name1") %>'></asp:Label>
                                </td>
                                <td class="sslbl_black">
                                    ผู้อำนวยการ</td>
                                <td class="sslbl" width="30%">
                                    <asp:Label ID="lblIdName2" runat="server" Text='<%# BindField("name2") %>'></asp:Label>
                                    </td>
                            </tr>
                            <tr>
                                <td class="sslbl_black">
                                    ผู้มีอำนาจลงนาม</td>
                                <td class="sslbl">
                                    <asp:Label ID="lblIdName3" runat="server" Text='<%# BindField("name3") %>'></asp:Label>
                                </td>
                                <td class="sslbl_black">
                                    จำนวนเงินประกัน</td>
                                <td class="sslbl" width="30%">
                                    <asp:Label ID="lblMoney" runat="server" Text='<%# BindField("money") %>'></asp:Label>
                                    &nbsp;บาท</td>
                            </tr>
                            <tr>
                                <td class="sslbl_black">
                                    อ้างอิงหนังสือรับเรื่อง</td>
                                <td class="sslbl" colspan="3">
                                    <asp:Label ID="lblTopic" runat="server" Text='<%# BindField("topic") %>'></asp:Label>
                                    &nbsp;<asp:LinkButton ID="LinkDetail" runat="server">ดูรายละเอียด</asp:LinkButton>
                                </td>
                            </tr>
                            <tr>
                                <td class="sslbl_black">
                                    อ้างอิงหนังสือนำส่งเรื่อง</td>
                                <td class="sslbl" colspan="3">
                                    <asp:GridView ID="GridView4" runat="server" AllowPaging="True" 
                                        AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" 
                                        ForeColor="#333333" GridLines="None" PageSize="20" ShowHeader="False" 
                                        Width="100%">
                                        <RowStyle BackColor="White" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="ลำดับ">
                                                <ItemStyle HorizontalAlign="Center" Width="3%" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <%#iRow%> .
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="รหัส" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblIdBook" runat="server" Text='<%# Bind("bookout_id") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="10%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="เลขที่หนังสือนำส่ง">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblIdBookNo" runat="server" Text='<%# Bind("bookout_no") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="20%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="เรื่อง" SortExpression="topic">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="LinkName" runat="server" CommandName="cName" 
                                                        Text='<%# Bind("topic") %>'></asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle Width="70%" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                        <EmptyDataTemplate>
                                        </EmptyDataTemplate>
                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                        <EditRowStyle BackColor="#2461BF" />
                                        <AlternatingRowStyle BackColor="White" />
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td class="sslbl_black">
                                    อ้างอิงสัญญา</td>
                                <td class="sslbl" colspan="3">
                                    <asp:GridView ID="GridView5" runat="server" AllowPaging="True" 
                                        AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" 
                                        ForeColor="#333333" GridLines="None" PageSize="20" ShowHeader="False" 
                                        Width="100%">
                                        <RowStyle BackColor="White" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="ลำดับ">
                                                <ItemStyle HorizontalAlign="Center" Width="3%" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <%#iRow%> .
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                           
                                            <asp:TemplateField HeaderText="เลขที่สัญญา">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblIdContractRef" runat="server" Text='<%# Bind("contract_no") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="20%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="เรื่อง" SortExpression="contract_name">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="LinkContractName" runat="server" CommandName="cLinkContract" 
                                                        Text='<%# Bind("contract_name") %>'></asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle Width="70%" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                        <EmptyDataTemplate>
                                        </EmptyDataTemplate>
                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                        <EditRowStyle BackColor="#2461BF" />
                                        <AlternatingRowStyle BackColor="White" />
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td class="style7">&nbsp;
                                    </td>
                                <td class="style8">&nbsp;
                                    </td>
                                <td class="style9">&nbsp;
                                    </td>
                                <td class="sslbl_right" width="30%">
                                    <asp:Button ID="bPrint" runat="server" Text="พิมพ์สัญญา" Visible="False" />
                                    <asp:Label ID="lblPrint" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
                </div> 
            </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="รายละเอียด" TabIndex="2">
            <ContentTemplate>
            <div class="TabContentBlue">
            
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <FCKeditorV2:FCKeditor ID="FCKeditor1" runat="server" BasePath="~/fckeditor/" 
                            Height="500px" Value='<%# BindField("message") %>'>
                        </FCKeditorV2:FCKeditor>
                    </ContentTemplate>
                </asp:UpdatePanel>
                </div> 
            </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel ID="TabPanel3" runat="server" HeaderText="เอกสารประกอบสัญญา" TabIndex="3">
            <ContentTemplate>
            <div class="TabContentBlue">
                <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                    <ContentTemplate>
                        <table border="0" cellpadding="1" cellspacing="1" class="form" width="100%">
                            <tr>
                                <td class="sslbl" width="100%">
                                    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
                                        AllowSorting="True" AutoGenerateColumns="False" BackColor="White" 
                                        BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="1" 
                                        CssClass="GridViewStyle" GridLines="Vertical" Width="100%">
                                        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="ชื่อเอกสาร">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("title") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("title") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="70%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="จำนวนหน้า">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("page") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("page") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="20%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ดาวน์โหลด">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblLink" runat="server" Text=""></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                        <EmptyDataTemplate>
                                            <table class="style5">
                                                <tr>
                                                    <td align="center">
                                                        <asp:Label ID="Label8" runat="server" CssClass="GEmpty" Text="ไม่พบข้อมูล"></asp:Label>
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
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
                </div> 
            </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel ID="TabPanel4" runat="server" HeaderText="หัวหน้าลงนาม" TabIndex="4">
        <ContentTemplate >
        <div class="TabContentBlue">
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
            
                        
                                <table border="0" cellpadding="1" cellspacing="1" class="form" width="100%">
                                    <tr>
                                <td class="sslbl_bold">
                                    ความคิดเห็น</td>
                            </tr>
                                    <tr>
                                        <td class="style1">
                                            <asp:RadioButtonList ID="rdoApp" runat="server" CssClass="sslbl" 
                                                RepeatColumns="2">
                                                <asp:ListItem Value="T">ลงนาม</asp:ListItem>
                                                <asp:ListItem Value="F">แก้ไข</asp:ListItem>
                                            </asp:RadioButtonList>
                                            <asp:Label ID="lblAApp1" runat="server" CssClass="sslbl_red"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="sslbl_right">
                                            <FCKeditorV2:FCKeditor ID="FCKeditor2" runat="server" BasePath="~/fckeditor/" 
                                                Height="500px" Value='<%# BindField("witness1_msg") %>'>
                                            </FCKeditorV2:FCKeditor>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="sslbl_bold" >
                                            บันทึก</td>
                                    </tr>
                                    <tr>
                                        <td class="style1">
                                            <asp:TextBox ID="txtAppComent" runat="server" CssClass="ssddl" Rows="15" 
                                                Text='<%# BindField("witness1_comment") %>' TextMode="MultiLine" Width="100%"></asp:TextBox>
                                            <asp:Label ID="lblAAppComment" runat="server" CssClass="sslbl_red"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style1">
                                            <asp:Label ID="lblAppText" runat="server" Text="ลงชื่อ"></asp:Label>
                                            &nbsp;
                                            <asp:Label ID="lblAppName" runat="server" CssClass="sslbl" 
                                                Text='<%# BindField("name1") %>'></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style2">
                                            <asp:Button ID="bApp" runat="server" CssClass="ssbtn" Text="บันทึก" 
                                                Width="80px" />
                                            <asp:Button ID="bAppAndSend" runat="server" CssClass="ssbtn" 
                                                Text="บันทึกพร้อมส่ง" Width="159px" />
                                            <asp:Button ID="bAppCancel" runat="server" CssClass="ssbtn" Text="ยกเลิก" 
                                                Width="80px" />
                                        </td>
                                    </tr>
                                </table>
                     
      
        </ContentTemplate>
    </asp:UpdatePanel> 
    </div> 
        </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel ID="TabPanel5" runat="server" HeaderText="ผู้อำนวยการลงนาม" TabIndex="5">
            <ContentTemplate>
            <div class="TabContentBlue">
            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
        <ContentTemplate>
          
                   
                                <table border="0" cellpadding="1" cellspacing="1" class="form" width="100%">
                                    <tr>
                                <td class="sslbl_bold">
                                    ความคิดเห็น</td>
                            </tr>
                                    <tr>
                                        <td class="sslbl">
                                            <asp:RadioButtonList ID="rdoApp1" runat="server" CssClass="sslbl" 
                                                RepeatColumns="2">
                                                <asp:ListItem Value="T">ลงนาม</asp:ListItem>
                                                <asp:ListItem Value="F">แก้ไข</asp:ListItem>
                                            </asp:RadioButtonList>
                                            <asp:Label ID="lblAApp2" runat="server" CssClass="sslbl_red"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="sslbl_right">
                                            <FCKeditorV2:FCKeditor ID="FCKeditor3" runat="server" BasePath="~/fckeditor/" 
                                                Height="500px" Value='<%# BindField("witness2_msg") %>'>
                                            </FCKeditorV2:FCKeditor>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td  class="sslbl_bold">
                                            บันทึก</td>
                                    </tr>
                                    <tr>
                                        <td class="sslbl_นสก">
                                            <asp:TextBox ID="txtAppComment1" runat="server" CssClass="ssddl" Rows="15" 
                                                Text='<%# BindField("witness2_comment") %>' TextMode="MultiLine" Width="100%"></asp:TextBox>
                                            <asp:Label ID="lblAAppComment2" runat="server" CssClass="sslbl_red"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="sslbl">
                                            ลงชื่อ&nbsp;
                                            <asp:Label ID="lblAppName1" runat="server" CssClass="sslbl" 
                                                Text='<%# BindField("name2") %>'></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style14">
                                            <asp:Button ID="bApp1" runat="server" CssClass="ssbtn" Text="บันทึก" 
                                                Width="80px" />
                                            <asp:Button ID="bAppAndSend1" runat="server" CssClass="ssbtn" 
                                                Text="บันทึกพร้อมส่ง" Width="159px" />
                                            <asp:Button ID="bAppCancel1" runat="server" CssClass="ssbtn" Text="ยกเลิก" 
                                                Width="80px" />
                                        </td>
                                    </tr>
                                </table>
                         
         
        </ContentTemplate>
    </asp:UpdatePanel> 
    </div> 
            </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel ID="TabPanel6" runat="server" HeaderText="ยกเลิกการร่างสัญญา" TabIndex="6">
        <ContentTemplate>
        <div class="TabContentBlue">
            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
        <ContentTemplate>
          
                                        <table border="0" cellpadding="1" cellspacing="1" class="form">
                                        <tr>
                                <td class="sslbl_right">
                                    สาเหตุการยกเลิก</td>
                                            <td width="85%">
                                                <asp:DropDownList ID="DDCancel" runat="server" CssClass="ssddl" Height="20px" 
                                                    Width="350px">
                                                </asp:DropDownList>
                                            </td>
                            </tr>
                                            <tr>
                                                <td class="sslbl_right">
                                                    บันทึก</td>
                                                <td width="85%">
                                                    <asp:TextBox ID="txtCancelComment" runat="server" CssClass="ssddl" Rows="15" 
                                                        Text='<%# BindField("cancel_comment") %>' TextMode="MultiLine" Width="100%"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style10">&nbsp;
                                                    </td>
                                                <td>
                                                    <asp:Button ID="bCancel" runat="server" CssClass="ssbtn" Text="ยกเลิกการร่างสัญญา" 
                                                        Width="120px" />
                                                </td>
                                            </tr>
                                        </table>
                                
                   
        </ContentTemplate>
    </asp:UpdatePanel> 
    </div>  
        </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel ID="TabPanel7" runat="server" HeaderText="ปรับปรุงสถานะ" TabIndex="7">
        <ContentTemplate>
        <div class="TabContentBlue">
            <asp:UpdatePanel ID="UpdatePanel6" runat="server">
        <ContentTemplate>
         
                                
                                        <table border="0" cellpadding="1" cellspacing="1" class="form">
                                        <tr>
                                <td class="sslbl_right">
                                    สถานะสัญญา</td>
                                            <td width="85%">
                                                <asp:DropDownList ID="DDStatus" runat="server" CssClass="ssddl" Height="20px" 
                                                    Width="350px">
                                                </asp:DropDownList>
                                            </td>
                            </tr>
                                            <tr>
                                                <td class="sslbl_right">
                                                    บันทึก</td>
                                                <td width="85%">
                                                    <asp:TextBox ID="txtComment" runat="server" CssClass="ssddl" Rows="15" 
                                                        Text='<%# BindField("witness1_comment") %>' TextMode="MultiLine" Width="100%"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style12">&nbsp;
                                                    </td>
                                                <td>
                                                    <asp:Button ID="bUpdateStatus" runat="server" CssClass="ssbtn" 
                                                        Text="ปรับปรุงสถานะ" Width="100px" />
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





