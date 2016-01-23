﻿<%@ Page Title="พิจารณาร่างกฎหมาย-LAW2105" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="DrafLawApprove.aspx.vb" MaintainScrollPositionOnPostback="true" Inherits="Src_DrafLawApprove" %>

<%@ Register assembly="FredCK.FCKeditorV2" namespace="FredCK.FCKeditorV2" tagprefix="FCKeditorV2" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<%@ Register src="../UserControl/ComboBox.ascx" tagname="ComboBox" tagprefix="uc1" %>
<%@ Register src="../UserControl/DatePicker.ascx" tagname="DatePicker" tagprefix="uc2" %>
<%@ Register src="../UserControl/TextBox.ascx" tagname="TextBox" tagprefix="uc3" %>

<%@ Register src="../UserControl/TextDate.ascx" tagname="TextDate" tagprefix="uc4" %>

<%@ Register src="../UserControl/ctlPrintLaw.ascx" tagname="ctlPrintLaw" tagprefix="uc5" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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
                        <asp:LinkButton ID="link2" runat="server">ร่างกฎหมายรอพิจารณา</asp:LinkButton>
                        &nbsp;&gt;
                        <asp:LinkButton ID="link3" runat="server">พิจารณาร่างกฎหมาย</asp:LinkButton>
                     </td>
                    </tr>
                    <tr>
                        <td class="sslbl_right">อ้างอิงหนังสือรับเรือง</td>
                        <td class="sslbl">
                                    <asp:TextBox ID="txtTitleBookIn" runat="server" CssClass="ssddl" 
                                         Width="500px"></asp:TextBox>
                                    <asp:Button ID="bSelectTitle" runat="server" CssClass="ssbtn" Text="เลือก" 
                                        Width="51px" Visible="False" />
                                         <asp:Label ID="lblTitle" runat="server" Visible="False"></asp:Label>
                                    <asp:LinkButton ID="LinkDetail" runat="server">ดูรายละเอียด</asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td class="sslbl_right">เลขที่</td>
                        <td class="sslbl">
                            <asp:TextBox ID="txtLawId" runat="server" CssClass="ssddl" Width="100px" ReadOnly="True" />
                            <asp:Label ID="Label9" runat="server" ForeColor="Red" Text="(Auto)"></asp:Label>
                            <asp:Label ID="lblRefID" runat="server" Visible="false" ></asp:Label>
                            <asp:Label ID="lblIsDirector" runat="server" Visible="false" Text="N" ></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="sslbl_right" width="20%">
                            ประเภทกฎหมาย</td>
                        <td class="sslbl" colspan="3">
                            <uc1:ComboBox ID="ddlLawType" runat="server" CssClass="ssddl" Width="505" IsNotNull="true" IsDefaultValue="false" AutoPosBack="true" />
                        </td>
                    </tr>
                    <tr>
                        <td class="sslbl_right" >
                            ประเภทย่อย</td>
                        <td class="sslbl" colspan="3" >
                            <uc1:ComboBox ID="ddlLawSubType" runat="server" CssClass="ssddl" Width="505" IsNotNull="true" IsDefaultValue="false" />
                        </td>
                    </tr>
                    <tr>
                        <td class="sslbl_right" >
                            สถานะกฎหมาย&nbsp;</td>
                        <td class="sslbl" colspan="3" >
                            <uc1:ComboBox ID="ddlLawStatus" runat="server" CssClass="ssddl" Width="505" IsNotNull="false" Enabled="false" IsDefaultValue="false" />
                        </td>
                    </tr>
                    <tr>
                        <td class="sslbl_right" >ความเร่งด่วน&nbsp;</td>
                        <td class="sslbl" >                                            
                            <uc1:ComboBox ID="ddlLawLevel" runat="server" CssClass="ssddl" Width="130" IsNotNull="true" IsDefaultValue="false" />
                        </td>
                        <td class="sslbl_right" >
                            &nbsp;</td>
                        <td class="sslbl" >
                            &nbsp;</td>
                    </tr>

                    <tr>
                        <td class="sslbl_right">
                            วันที่</td>
                        <td class="sslbl">
                            <uc4:TextDate ID="dpDate" runat="server" IsNotNull="true" />
                        </td>
                        <td class="sslbl_right">
                            &nbsp;</td>
                        <td class="sslbl" >
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="sslbl_right">
                            เรื่อง</td>
                        <td class="sslbl" colspan="3">
                            <asp:TextBox ID="txtTitle" runat="server" CssClass="ssddl" Width="500px"></asp:TextBox>
                            <asp:Label ID="lblChkTitle" runat="server" CssClass="sslbl_red" Text="*"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="sslbl_right">
                            คำค้นหา</td>
                        <td class="sslbl" colspan="3">
                            <asp:TextBox ID="txtKeyword" runat="server" CssClass="ssddl" Width="500px"></asp:TextBox>
                            <asp:Label ID="lblChkKeyword" runat="server" CssClass="sslbl_red" Text="*"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="sslbl_right">
                            &nbsp;รายละเอียด</td>
                        <td class="sslbl" colspan="3">
                            <cc1:TabContainer ID="TabContainer1" runat="server"  AutoPostBack="true" 
                                Width="98%" ActiveTabIndex="0" >
                                <cc1:TabPanel runat="server" HeaderText="หัวหน้าพิจารณา" ID="TabPanel3" TabIndex="0" >
                                <HeaderTemplate>
                                    หัวหน้าพิจารณา
                                </HeaderTemplate>                    
                                <ContentTemplate>
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" >
                                        <tr>
                                            <td>
                                                <asp:Panel ID="pnlLeaderMessage" runat="server" Height="500px" Width="100%" 
                                                    Visible="False" BorderWidth="1px" BorderColor="Black">
                                                    <asp:Label ID="lblLeaderMessage" runat="server"  ></asp:Label>
                                                </asp:Panel>
                                                <asp:Label ID="lblChkLeaderMessage" runat="server" CssClass="sslbl_red" 
                                                    Visible="False" ></asp:Label>
                                                <FCKeditorV2:FCKeditor ID="txtLeaderMessage" runat="server" BasePath="~/fckeditor/" 
                                                    Height="500px" Width="100%">
                                                </FCKeditorV2:FCKeditor>
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                <uc5:ctlPrintLaw ID="ctlPrintLaw1" runat="server" Visible="false" />
                                            </td>
                                        </tr>
                                        
                                    </table>
                                </ContentTemplate>                  
                                </cc1:TabPanel>
                                <cc1:TabPanel runat="server" HeaderText="ผอ.พิจารณา" ID="TabPanel4" TabIndex="1"  >
                                <HeaderTemplate>
                                    ผอ.พิจารณา
                                </HeaderTemplate>                    
                                <ContentTemplate>
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" >
                                        <tr>
                                            <td>
                                                <asp:Panel ID="pnlDirectorMessage" runat="server" Height="500px" Width="100%" Visible="false" BorderWidth="1" BorderColor="#000000">
                                                    <asp:Label ID="lblDirectorMessage" runat="server"  ></asp:Label>
                                                </asp:Panel>
                                                <asp:Label ID="lblChkDirectorMessage" runat="server" CssClass="sslbl_red" 
                                                    Visible="False" ></asp:Label>
                                                <FCKeditorV2:FCKeditor ID="txtDirectorMessage" runat="server" BasePath="~/fckeditor/" 
                                                    Height="500px" Width="100%">
                                                </FCKeditorV2:FCKeditor>
                                                <uc5:ctlPrintLaw ID="ctlPrintDraftDir" runat="server" Visible="false" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblLeaderNote" runat="server" Visible="false" ></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>                  
                                </cc1:TabPanel>
                                <cc1:TabPanel runat="server" HeaderText="ยกร่างกฎหมาย" ID="TabPanel1" TabIndex="2" >
                                <HeaderTemplate>
                                    ยกร่างกฎหมาย
                                </HeaderTemplate>                    
                                <ContentTemplate>
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" >
                                        <tr>
                                            <td>
                                                <asp:Panel ID="Panel1" runat="server" Height="500px" Width="100%" BorderWidth="1" BorderColor="#000000">
                                                    <asp:Label ID="lblLawerMessage" runat="server"  ></asp:Label>
                                                </asp:Panel>
                                                <uc5:ctlPrintLaw ID="ctlPrintLawer" runat="server" Visible="false" />
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>                  
                                </cc1:TabPanel>
                                <cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="เอกสารประกอบ" TabIndex="3">
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
                            </cc1:TabContainer>
                        </td>
                    </tr>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <tr>
                                <td class="sslbl_right" valign="bottom">&nbsp;การพิจารณา</td>
                                <td class="sslbl" colspan="3" >
                                    <asp:RadioButtonList ID="rdiApprove" runat="server" AutoPostBack="true" RepeatDirection="Horizontal" >
                                        <asp:ListItem Selected="True" Text="อนุมัติ" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="แก้ไข" Value="0"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <asp:Panel ID="pnlDirOption" runat="server" Visible="false" >
                                <tr>
                                    <td class="sslbl_right" valign="bottom">&nbsp;ส่งกลับไปยัง</td>
                                    <td class="sslbl" colspan="3" >
                                        <asp:RadioButtonList ID="rdiSendBack" runat="server" AutoPostBack="true" RepeatDirection="Horizontal" >
                                            <asp:ListItem Selected="True" Text="หัวหน้างาน" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="นิติกร" Value="0"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                            </asp:Panel>
                            <asp:Panel ID="pnlSendTo" runat="server">
                                <tr>
                                    <td class="sslbl_right" valign="bottom">
                                        <asp:Label ID="lblSendTo" runat="server" Text="ส่งต่อเพื่อตรวจ" ></asp:Label>
                                    </td>
                                    <td class="sslbl" colspan="3">
                                        <uc1:ComboBox ID="ddlSendTo" runat="server" CssClass="ssddl" Width="285" IsNotNull="true" IsDefaultValue="true" DefaultValue="" />
                                    </td>
                                </tr>
                            </asp:Panel>
                            <asp:Panel ID="pnlSendToLawer" runat="server" Visible="false">
                                <tr>
                                    <td class="sslbl_right">&nbsp;
                                        <asp:Label ID="lblSendToLawer" runat="server" Text="ส่งกลับนิติกร" ></asp:Label>
                                    </td>
                                    <td class="sslbl" colspan="3">
                                        <uc1:ComboBox ID="ddlSendBackLawer" runat="server" CssClass="ssddl" Width="285" IsNotNull="true" IsDefaultValue="true" DefaultValue="" />
                                    </td>
                                </tr>
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    
                    
                    <tr>
                        <td class="sslbl_right">&nbsp;บันทึกเพิ่มเติม</td>
                        <td class="sslbl" colspan="3">
                            <uc3:TextBox ID="txtNote" runat="server" TextType="TextBox" IsNotNull="false" TextMode="MultiLine" Rows="5" Cols="100" />
                            
                        </td>
                    </tr>
                    <tr>
                        <td class="sslbl">
                            &nbsp;</td>
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

