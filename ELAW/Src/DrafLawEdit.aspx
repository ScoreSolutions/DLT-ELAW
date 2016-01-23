<%@ Page Title="ร่างกฎหมาย" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" MaintainScrollPositionOnPostback="true" CodeFile="DrafLawEdit.aspx.vb" Inherits="Src_DrafLawEdit" %>

<%@ Register assembly="FredCK.FCKeditorV2" namespace="FredCK.FCKeditorV2" tagprefix="FCKeditorV2" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<%@ Register src="../UserControl/ComboBox.ascx" tagname="ComboBox" tagprefix="uc1" %>
<%@ Register src="../UserControl/DatePicker.ascx" tagname="DatePicker" tagprefix="uc2" %>
<%@ Register src="../UserControl/TextBox.ascx" tagname="TextBox" tagprefix="uc3" %>
<%@ Register src="../UserControl/TextDate.ascx" tagname="TextDate" tagprefix="uc4" %>

<%@ Register src="../UserControl/ctlPrintLaw.ascx" tagname="ctlPrintLaw" tagprefix="uc5" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style2
        {
            font: 12px Arial,"Courier New", Courier, monospace;
            color: #000000;
            font-weight : normal;
            text-decoration: none;
            vertical-align: top;
            padding: 1px;
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
            width: 13%;
        }
        .style4
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
            width: 13%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <%--    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
        <script type="text/javascript">

            function openwindow(Page, Id) {
                window.open("../Src/" + Page + ".aspx?id=" + Id, "_new", "location=no,status=no,resizable=yes,width=750,height=700,scrollbars=yes,menubar=no");
            }

        </script>


    <table class="form" cellpadding="1" cellspacing="1" frame="border"  border="0" width="100%">
        <tr>
            <td class="sslbl">
                <table border="0" cellpadding="1" cellspacing="1" class="form" width="100%">
                    <tr>
                        <td class="HeaderGreenTab" colspan="4">
                        <asp:LinkButton ID="linkHome" runat="server">Home</asp:LinkButton>
                        &nbsp;&gt;
                        <asp:LinkButton ID="link1" runat="server">ติดตามงานกฎหมาย</asp:LinkButton>
                        &nbsp;&gt;
                        <asp:LinkButton ID="link2" runat="server">แก้ไขร่างกฎหมาย</asp:LinkButton>
                        &nbsp;&gt;
                        <asp:LinkButton ID="link3" runat="server">ร่างกฎหมาย</asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td class="style3">
                            อ้างอิงหนังสือรับเรื่อง</td>
                        <td class="sslbl" width="50%" colspan="2">
                                    <asp:TextBox ID="txtTitleBookIn" runat="server" CssClass="ssddl" 
                                         Width="500px" ReadOnly="True"></asp:TextBox>
                                    <asp:Button ID="bSelectTitle" runat="server" CssClass="ssbtn" Text="เลือก" 
                                        Width="51px" />
                                    <asp:Button ID="bDelTitle" runat="server" CssClass="ssbtn" Text="ลบ" 
                                        Width="51px" />
                                         <asp:Label ID="lblTitle" runat="server" Visible="False"></asp:Label>
                        </td>
                        <td class="sslbl" width="35%" >&nbsp;
                            </td>
                    </tr>
                    <tr>
                        <td class="style3">
                            เลขที่</td>
                        <td class="sslbl" width="50%">
                            <asp:TextBox ID="txtLawId" runat="server" CssClass="ssddl" Width="100px" ReadOnly="True" />
                            <asp:Label ID="Label9" runat="server" ForeColor="Red" Text="(Auto)"></asp:Label>
                            <asp:Label ID="lblRefID" runat="server" Visible="false"></asp:Label>
                        </td>
                        <td class="sslbl_right" width="15%">&nbsp;
                            </td>
                        <td class="sslbl" width="35%" >&nbsp;
                            </td>
                    </tr>
                    <tr>
                        <td class="style3">
                            ประเภทกฎหมาย</td>
                        <td class="sslbl" width="50%">
                            <uc1:ComboBox ID="ddlLawType" runat="server" CssClass="ssddl" Width="400" 
                                IsNotNull="true" IsDefaultValue="false" AutoPosBack="true" />
                        </td>
                        <td class="sslbl_right" width="15%">
                            </td>
                        <td class="sslbl" width="35%" >&nbsp;
                            </td>
                    </tr>
                    <tr>
                        <td class="style3" >
                            ประเภทย่อย</td>
                        <td class="sslbl" >
                            <uc1:ComboBox ID="ddlLawSubType" runat="server" CssClass="ssddl" Width="400" IsNotNull="true" IsDefaultValue="false" />
                        </td>
                        <td class="sslbl_right" >&nbsp;
                            </td>
                        <td class="sslbl" >&nbsp;
                            </td>
                    </tr>
                    <tr>
                        <td class="style3" >
                            สถานะกฎหมาย&nbsp;</td>
                        <td class="sslbl" >
                            <uc1:ComboBox ID="ddlLawStatus" runat="server" CssClass="ssddl" Width="400" IsNotNull="false" Enabled="false" IsDefaultValue="false" />
                        </td>
                        <td class="sslbl_right" >&nbsp;
                            </td>
                        <td class="sslbl" >&nbsp;
                            </td>
                    </tr>
                    <tr>
                        <td class="style3" >ความเร่งด่วน&nbsp;</td>
                        <td class="sslbl" >                                            
                            <uc1:ComboBox ID="ddlLawLevel" runat="server" CssClass="ssddl" Width="130" IsNotNull="true" IsDefaultValue="false" />
                        </td>
                        <td class="sslbl_right" >&nbsp;
                            </td>
                        <td class="sslbl" >&nbsp;
                            </td>
                    </tr>

                    <tr>
                        <td class="style3">
                            วันที่</td>
                        <td class="sslbl">
                            <uc4:TextDate ID="dpDate" runat="server" IsNotNull="true" />
                        </td>
                        <td class="sslbl_right">&nbsp;
                            </td>
                        <td class="sslbl" >&nbsp;
                            </td>
                    </tr>
                    <tr>
                        <td class="style3">
                            เรื่อง</td>
                        <td class="sslbl" colspan="3">
                            <asp:TextBox ID="txtTitle" runat="server" CssClass="ssddl" Width="500px"></asp:TextBox>
                            <asp:Label ID="lblChkTitle" runat="server" CssClass="sslbl_red" Text="*"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style3">
                            คำค้นหา</td>
                        <td class="style2" colspan="3">
                            <asp:TextBox ID="txtKeyword" runat="server" CssClass="ssddl" Width="500px"></asp:TextBox>
                            <asp:Label ID="lblChkKeyword" runat="server" CssClass="sslbl_red" Text="*"></asp:Label>
                            <asp:Label ID="lblActive" runat="server" Visible="false" ></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style3">
                            &nbsp;รายละเอียด</td>
                        <td class="sslbl" colspan="3">
                            <cc1:TabContainer ID="TabContainer1" runat="server"  AutoPostBack="true" Width="98%">
                                <cc1:TabPanel runat="server" HeaderText="ยกร่างกฎหมาย" ID="TabPanel1" TabIndex="0" >
                                <HeaderTemplate>
                                    ยกร่างกฎหมาย
                                </HeaderTemplate>                    
                                <ContentTemplate>
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" >
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblChkMessage" runat="server" CssClass="sslbl_red" 
                                                    Visible="False" ></asp:Label>
                                                <FCKeditorV2:FCKeditor ID="txtMessage" runat="server" BasePath="~/fckeditor/" 
                                                    Height="500px" Width="100%">
                                                </FCKeditorV2:FCKeditor>
                                                <asp:Button ID="bPreview" runat="server" CssClass="ssbtn" Height="26px" 
                                                    Text="แสดงตัวอย่าง" Width="111px" Visible="False" />

                                                <uc5:ctlPrintLaw ID="ctlPrintLaw1" runat="server" Visible="False" />

                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>                  
                                </cc1:TabPanel>
                                <cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="เอกสารประกอบ" TabIndex="1">
                                <HeaderTemplate>
                                    เอกสารประกอบ
                                </HeaderTemplate>
                                <ContentTemplate>
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional" >
                                    <ContentTemplate>
                                      <div class="TabContentBlue">
                                         <table border="0" cellpadding="0" cellspacing="0" class="form" width="100%">
                                             <tr>
                                                <td colspan="2">&nbsp;</td>
                                             </tr>
                                             <tr>
                                                 <td class="sslbl_right" width="15%" >ไฟล์แนบ</td>
                                                 <td class="sslbl" width="85%" >
                                                     <asp:FileUpload ID="FileUpload1" runat="server" CssClass="ssddl" Width="400px" />
                                                     <asp:Label ID="lblAFile" runat="server" CssClass="sslbl_red" Text="*"></asp:Label><br />
                                                     <asp:Label ID="lblFileName" runat="server" Visible="False" ></asp:Label>
                                                     <asp:Label ID="lblStatus" runat="server" Visible="False" ></asp:Label>
                                                     <asp:Label ID="lblDocId" runat="server" Visible="False" ></asp:Label>

                                                 </td>
                                             </tr>
                                             <tr>
                                                 <td class="sslbl_right">รายละเอียด</td>
                                                 <td class="sslbl">
                                                     <asp:TextBox ID="txtDocDetail" runat="server" CssClass="ssddl" Width="400px"></asp:TextBox>
                                                     <asp:Label ID="lblADocDeatil" runat="server" CssClass="sslbl_red" Text="*"></asp:Label>
                                                 </td>
                                             </tr>
                                             <tr>
                                                 <td class="sslbl_right">จำนวนหน้า</td>
                                                 <td class="sslbl">
                                                     <uc3:TextBox ID="txtDocPage" runat="server" Width="60" TextKey="TextInt" TextAlign="AlignRight" IsNotNull="true" />
                                                 </td>
                                             </tr>
                                             <tr>
                                                 <td class="sslbl">&nbsp;</td>
                                                 <td>
                                                     <asp:Button ID="bSaveFile" runat="server" CssClass="ssbtn" Text="บันทึก" Width="80px" />
                                                     <asp:Button ID="bCancelFile" runat="server" CssClass="ssbtn" Text="ยกเลิก" Width="80px" />
                                                 </td>
                                             </tr>
                                             <tr>
                                                 <td class="sslbl" colspan="2" width="100%">
                                                     <asp:GridView ID="gvDocList" runat="server" AllowPaging="True"
                                                         AllowSorting="True" AutoGenerateColumns="False" BackColor="White" 
                                                         BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="1" 
                                                         CssClass="GridViewStyle" GridLines="Vertical" Width="100%">
                                                        <AlternatingRowStyle BackColor="Gainsboro" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="รายละเอียด">
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="txtTitle" runat="server" Text='<%# Bind("title") %>'></asp:TextBox>
                                                                </EditItemTemplate>
                                                                <ItemTemplate>
                                                                     <asp:Label ID="lblTitle" runat="server" Text='<%# Bind("title") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="70%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="จำนวนหน้า">
                                                                <EditItemTemplate>
                                                                     <uc3:TextBox ID="txtDocPage" runat="server" Text='<%# Bind("page") %>' Width="40" TextKey="TextInt" TextAlign="AlignRight" />
                                                                </EditItemTemplate>
                                                                <ItemTemplate>
                                                                     <asp:Label ID="lblDocPage" runat="server" Text='<%# Bind("page") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" Width="20%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="ดาวน์โหลด">
                                                                <ItemTemplate>
                                                                      <asp:Label ID="lblPath" runat="server"  ></asp:Label>
                                                                      <asp:Label ID="lblLink" runat="server" Visible="false" Text='<%# Bind("file_path") %>' ></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ShowHeader="False">
                                                                <ItemTemplate>
                                                                 <asp:ImageButton ID="imgDelete" runat="server" CausesValidation="False" CommandArgument='<%# Bind("document_id") %>' 
                                                                     CommandName="Delete" ImageUrl="~/Images/DeleteFolderHS.png" Text="Delete" ToolTip="ลบ" OnClientClick="return confirm('ยืนยันการลบรายการ?');" />
                                                                </ItemTemplate>
                                                                <ItemStyle Width="15px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ShowHeader="False">
                                                                <ItemTemplate>
                                                                     <asp:ImageButton ID="imgEdit" runat="server" CausesValidation="False" CommandArgument='<%# Bind("document_id") %>'
                                                                         CommandName="Edit" ImageUrl="~/Images/Edit.gif" Text="Edit" ToolTip="แก้ไข" />
                                                                </ItemTemplate>
                                                                <ItemStyle Width="15px" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <EmptyDataTemplate>
                                                        </EmptyDataTemplate>
                                                        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                                        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                                                        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                                        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                                                        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                                                    </asp:GridView>
                                                 </td>
                                             </tr>
                                          </table>
                                        </div> 
                                     </ContentTemplate>
                                     <Triggers>
                                        <asp:PostBackTrigger ControlID="bSaveFile" runat="server" />
                                    </Triggers>
                                     </asp:UpdatePanel>
                                </ContentTemplate>                         
                                </cc1:TabPanel>
                                <cc1:TabPanel runat="server" HeaderText="หัวหน้าพิจารณา" ID="TabPanel3" TabIndex="2" >
                                <HeaderTemplate>
                                    หัวหน้าพิจารณา
                                </HeaderTemplate>                    
                                <ContentTemplate>
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" >
                                        <tr>
                                            <td>
                                                <asp:Panel ID="pnlLeaderMessage" runat="server" Height="500px" Width="100%" ScrollBars="Auto"  BorderWidth="1" BorderColor="#000000">
                                                    <asp:Label ID="lblLeaderMessage" runat="server"  ></asp:Label>
                                                </asp:Panel>
                                                <asp:Label ID="lblChkLeaderMessage" runat="server" CssClass="sslbl_red" 
                                                    Visible="False" ></asp:Label>
                                                <FCKeditorV2:FCKeditor ID="txtLeaderMessage" runat="server" BasePath="~/fckeditor/" 
                                                    Height="500px" Width="100%" Visible="false" >
                                                </FCKeditorV2:FCKeditor>
                                                
                                                <uc5:ctlPrintLaw ID="ctlPrintLaw2" runat="server" Visible="False" />
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>                  
                                </cc1:TabPanel>
                            </cc1:TabContainer>
                        </td>
                    </tr>
                    <tr>
                        <td class="style3">&nbsp;ส่งต่อเพื่อตรวจ</td>
                        <td class="sslbl" colspan="3">
                            <uc1:ComboBox ID="ddlSendTo" runat="server" CssClass="ssddl" Width="285" IsNotNull="true" IsDefaultValue="true" DefaultValue="" />
                        </td>
                    </tr>
                    <tr>
                        <td class="style3">&nbsp;บันทึกเพิ่มเติม</td>
                        <td class="sslbl" colspan="3">
                            <uc3:TextBox ID="txtNote" runat="server" TextType="TextBox" IsNotNull="false" TextMode="MultiLine" Rows="5" Cols="100" />
                            
                        </td>
                    </tr>
                    <tr>
                        <td class="style4">&nbsp;
                            </td>
                        <td class="sslbl" colspan="3">
                            <asp:Button ID="bSave" runat="server" CssClass="ssbtn" Height="26px" 
                                Text="รายการถูกบันทึก" Width="111px" />
                            <asp:Button ID="bSaveSend" runat="server" CssClass="ssbtn" Height="26px" 
                                Text="รายการถูกบันทึกพร้อมส่ง" Width="155px" OnClientClick="return confirm('ยืนยันการส่งข้อมูล');" />
                            <asp:Button ID="bCancel" runat="server" CssClass="ssbtn" Text="ยกเลิก" 
                                Width="80px" />
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
<%--        </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>

