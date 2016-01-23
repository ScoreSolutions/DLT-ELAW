<%@ Page Title="ปรับปรุงสถานะกฎหมาย-LAW2108" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="DrafLawStatus.aspx.vb" MaintainScrollPositionOnPostback="true" Inherits="Src_DrafLawStatus" %>

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
                        <asp:LinkButton ID="link2" runat="server">ปรับปรุงสถานะกฎหมาย</asp:LinkButton>
                    &nbsp;&gt;
                        <asp:LinkButton ID="link3" runat="server">รายละเอียด</asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td class="sslbl_right">อ้างอิงหนังสือรับเรื่อง</td>
                        <td class="sslbl">
                                    <asp:TextBox ID="txtTitleBookIn" runat="server" CssClass="ssddl" 
                                         Width="500px" ReadOnly="True"></asp:TextBox>
                                    <asp:Button ID="bSelectTitle" runat="server" CssClass="ssbtn" Text="เลือก" 
                                        Width="51px" Visible="False" />
                                    <asp:Button ID="bDelTitle" runat="server" CssClass="ssbtn" Text="ลบ" 
                                        Width="51px" Visible="False" />
                                         <asp:Label ID="lblTitle" runat="server"  
                                        Visible="False"></asp:Label>
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
                            <asp:Label ID="lblCurrStatusID" runat="server" Visible="false" ></asp:Label>
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
                            สถานะกฎหมาย</td>
                        <td class="sslbl" colspan="3" >
                            <uc1:ComboBox ID="ddlLawStatus" runat="server" CssClass="ssddl" Width="505" IsNotNull="true" IsDefaultValue="false" AutoPosBack="true" />
                        </td>
                    </tr>
                    <tr>
                        <td class="sslbl_right" >
                            สถานะอื่นๆ</td>
                        <td class="sslbl" colspan="3" >
                            <uc3:TextBox ID="txtOtherStatus" runat="server" Width="500" Enabled="false" IsNotNull="true" />
                        </td>
                    </tr>
                    <tr>
                        <td class="sslbl_right" >ความเร่งด่วน</td>
                        <td class="sslbl" colspan="3" >                                            
                            <uc1:ComboBox ID="ddlLawLevel" runat="server" CssClass="ssddl" Width="130" IsNotNull="true" IsDefaultValue="false" />
                        </td>
                    </tr>

                    <tr>
                        <td class="sslbl_right">
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
                            <cc1:TabContainer ID="TabContainer1" runat="server"  AutoPostBack="true" Width="98%" >
                                <cc1:TabPanel runat="server" HeaderText="รายละเอียดกฎหมาย" ID="TabPanel4" TabIndex="1" >
                                <HeaderTemplate>
                                    รายละเอียดกฎหมาย
                                </HeaderTemplate>
                                <ContentTemplate>
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" >
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblValidMessage" runat="server" CssClass="sslbl_red" 
                                                    Visible="False" ></asp:Label>

                                                <FCKeditorV2:FCKeditor ID="txtDirectorMessage" runat="server" BasePath="~/fckeditor/" 
                                                    Height="500px" Width="100%" Visible="False" >
                                                </FCKeditorV2:FCKeditor>

                                                <asp:Panel ID="pnlDirectorMessage" runat="server" Height="500px" Width="100%"  
                                                    BorderWidth="1px" BorderColor="Black" ScrollBars="Auto" >
                                                    <asp:Label ID="lblDirectorMessage" runat="server"  ></asp:Label>
                                                </asp:Panel>
                                                <uc5:ctlPrintLaw ID="ctlPrintLaw1" runat="server" Visible="False" />
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
                                             <tbody style="display:none">
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
                                             </tbody>
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
                    <tr>
                        <td class="sslbl_right">&nbsp;บันทึกเพิ่มเติม</td>
                        <td class="sslbl" colspan="3">
                            <uc3:TextBox ID="txtNote" runat="server" TextType="TextBox" IsNotNull="false" TextMode="MultiLine" Rows="5" Cols="100" />
                        </td>
                    </tr>
                    <tr>
                        <td class="sslbl">&nbsp;
                            </td>
                        <td class="sslbl" colspan="3">
                            <asp:Button ID="bSave" runat="server" CssClass="ssbtn" Height="26px" 
                                Text="รายการถูกบันทึก" Width="111px" />
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

