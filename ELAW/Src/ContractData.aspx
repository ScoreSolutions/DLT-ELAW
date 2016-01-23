<%@ Page Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" ValidateRequest="false" CodeFile="ContractData.aspx.vb" Inherits="Src_ContractData" title="บันทึกสัญญา-LAW4107" %>

<%@ Register assembly="FredCK.FCKeditorV2" namespace="FredCK.FCKeditorV2" tagprefix="FCKeditorV2" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
 
<%@ Register src="../UserControl/DatePicker.ascx" tagname="DatePicker" tagprefix="uc1" %>
 
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server" >
    
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
                        &gt;
                                <asp:LinkButton ID="link1" runat="server">งานสัญญา</asp:LinkButton>
&gt;
                                <asp:LinkButton ID="link2" runat="server">แก้ไขข้อมูลสัญญา</asp:LinkButton>
&gt;
                                <asp:LinkButton ID="link3" runat="server">รายละเอียด</asp:LinkButton>
                        </div>
                           	
                      
                        <!--end content -->
                 </td>
               </tr>
             </table>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    

                    <ContentTemplate>
         <cc1:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" 
        Width="100%">
             
        <cc1:TabPanel runat="server" HeaderText="ข้อมูลหลัก" ID="TabPanel1" TabIndex="1">
            <HeaderTemplate>
                
 ข้อมูลหลัก
 
            </HeaderTemplate>
            
            <ContentTemplate>
                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                <ContentTemplate >
               

            <div class="TabContentBlue">
                
                

                
                
                       
                                
                                

                       
                                <table border="0" cellpadding="1" cellspacing="1" class="form" width="100%">
                                     
                                    

                                    

                                     
                                    <tr>
                                        

                                        <td class="sslbl_right" width="22%">
                                            

                                            อ้างอิงหนังสือรับเรื่อง</td>
                                        

                                        <td class="sslbl" colspan="2">
                                            

                                            <asp:TextBox ID="txtTitle" runat="server" CssClass="ssddl" 
                                                Text='<%# BindField("topic") %>' Width="450px" ReadOnly="True"></asp:TextBox>
                                            

                                            <asp:Button ID="bSelectTitle" runat="server" CssClass="ssbtn" Text="เลือก" 
                                                Width="51px" />
                                            

                                            <asp:Button ID="bDelTitle" runat="server" CssClass="ssbtn" Text="ลบ" 
                                                Width="51px" />
                                            

                                            <asp:Label ID="lblTitle" runat="server" Visible="False"><%#BindField("ref_bookin")%></asp:Label>
                                            

                                        </td>
                                        

                                    </tr>
                                    

                                    <tr>
                                        

                                        <td class="sslbl_right" width="22%">
                                            

                                            เลขที่สัญญา</td>
                                        

                                        <td class="sslbl" colspan="2">
                                            

                                            <asp:TextBox ID="txtNo" runat="server" CssClass="ssddl" MaxLength="16" 
                                                ReadOnly="True" Text='<%# BindField("contract_no") %>' Width="120px" />
                                            

                                            <asp:Label ID="Label9" runat="server" ForeColor="Red" Text="(Auto)"></asp:Label>
                                            

                                            <asp:Button ID="bEditNo" runat="server" CssClass="ssbtn" Text="แก้ไข" 
                                                Width="51px" />
                                            

                                            <asp:Label ID="lblId" runat="server" Text='<%# BindField("contract_id") %>' 
                                                Visible="False"></asp:Label>
                                            

                                            <asp:Label ID="lblMainStatus" runat="server" Visible="False"></asp:Label>
                                            

                                        </td>
                                        

                                    </tr>
                                    

                                    <tr>
                                        

                                        <td class="sslbl_right">
                                            

                                            ประเภทสัญญา</td>
                                        

                                        <td class="sslbl" colspan="2" >
                                            

                                            <asp:DropDownList ID="ddlSubContract" runat="server" CssClass="ssddl" 
                                                Height="20px" Width="285px">
                                                

                                            </asp:DropDownList>
                                            

                                            <asp:Button ID="bGenerate" runat="server" CssClass="ssbtn" 
                                                Text="กำหนดรูปแบบสัญญา" Width="130px" />
                                            

                                        </td>
                                        

                                    </tr>
                                        

                                        <tr>
                                            

                                            <td class="sslbl_right">
                                                

                                                ชื่อสัญญา</td>
                                            

                                            <td class="sslbl" colspan="2">
                                                

                                                <asp:TextBox ID="txtContractName" runat="server" CssClass="ssddl" Rows="4" 
                                                    Text='<%# BindField("contract_name") %>' TextMode="MultiLine" Width="600px"></asp:TextBox>
                                                

                                                <asp:Label ID="Label21" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                                

                                                <asp:Label ID="lblContractName" runat="server" CssClass="sslbl_red"></asp:Label>
                                                

                                            </td>
                                            

                                    </tr>
                                        

                                        <tr>
                                            

                                            <td class="sslbl_right">
                                                

                                                วันที่รับเรื่อง</td>
                                            

                                            <td class="style1">
                                                

                                                <uc1:DatePicker ID="txtReceiveDate" runat="server" IsNotNull ="true"  />
                                                

                                                <asp:Label ID="lblAdate1" runat="server" CssClass="sslbl_red"></asp:Label>
                                                

                                            </td>
                                            

                                            <td class="sslbl_right">
                                                

                                                </td>
                                            

                                        </tr>
                                    

                                    <tr>
                                        

                                        <td class="sslbl_right">
                                            

                                            วันที่คู่สัญญามาลงนาม</td>
                                        

                                        <td class="style1" >
                                            

                                            <uc1:DatePicker ID="txtComeSignDate" runat="server" />
                                            

                                            <asp:Label ID="lblADate3" runat="server" CssClass="sslbl_red"></asp:Label>
                                            

                                        </td>
                                        

                                        <td class="sslbl_right" >
                                            

                                            </td>
                                        

                                    </tr>
                                    

                                    <tr>
                                        

                                        <td class="sslbl_right">
                                            

                                            วันที่ผู้มีอำนาจลงนามในสัญญา</td>
                                        

                                        <td class="style1" >
                                            

                                            <uc1:DatePicker ID="txtSignDate" runat="server" />
                                            

                                            <asp:Label ID="lblADate4" runat="server" CssClass="sslbl_red"></asp:Label>
                                            

                                        </td>
                                        

                                        <td class="sslbl_right" >
                                            

                                            </td>
                                        

                                    </tr>
                                    

                                    <tr>
                                        

                                        <td class="sslbl_right">
                                            

                                            วันที่เริ่มต้นสัญญา</td>
                                        

                                        <td class="style1" >
                                            

                                            <uc1:DatePicker ID="txtDateStart" runat="server" IsNotNull="false" />
                                            

                                        </td>
                                        

                                        <td class="sslbl_right" >
                                            

                                            </td>
                                        

                                    </tr>
                                    

                                    <tr>
                                        

                                        <td class="sslbl_right">
                                            

                                            วันที่สิ้นสุดสัญญา</td>
                                        

                                        <td class="style1" >
                                            

                                            <uc1:DatePicker ID="txtDateEnd" runat="server" IsNotNull="false"/>
                                            

                                        </td>
                                        

                                        <td class="sslbl_right" >
                                            

                                            </td>
                                        

                                    </tr>
                                    

                                    <tr>
                                        

                                        <td class="sslbl_right">
                                            

                                            สถานะ</td>
                                        

                                        <td class="style1" >
                                            

                                            <asp:DropDownList ID="ddlStatus" runat="server" CssClass="ssddl" Height="20px" 
                                                Width="405px">
                                                

                                            </asp:DropDownList>
                                            

                                            <asp:Label ID="lblStatusId" runat="server" Visible="False"></asp:Label>
                                            

                                        </td>
                                        

                                        <td class="sslbl_right" >
                                            

                                            </td>
                                        

                                    </tr>
                                    

                                    <tr>
                                        

                                        <td class="sslbl_right">
                                            

                                            วิธีจัดจ้าง</td>
                                        

                                        <td class="style1" colspan="2" >
                                            

                                            <asp:DropDownList ID="ddlProcess" runat="server" CssClass="ssddl" Height="20px" 
                                                Width="405px">
                                                

                                            </asp:DropDownList>
                                            

                                            <asp:Label ID="Label14" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                            

                                            <asp:Label ID="lblProcess" runat="server" CssClass="sslbl_red"></asp:Label>
                                            

                                        </td>
                                        

                                    </tr>
                                    

                                    <tr>
                                        

                                        <td class="sslbl_right">
                                            

                                            ระบุสิ่งที่จะซื้อ/จ้าง</td>
                                        

                                        <td class="style1" colspan="2" >
                                            

                                            <asp:TextBox ID="txtMatetial" runat="server" CssClass="ssddl" 
                                                Text='<%# BindField("material") %>' Width="400px"></asp:TextBox>
                                            

                                            <asp:Label ID="Label15" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                            

                                            <asp:Label ID="lblMaterail" runat="server" CssClass="sslbl_red"></asp:Label>
                                            

                                        </td>
                                        

                                    </tr>
                                    

                                    <tr>
                                        

                                        <td class="sslbl_right">
                                            

                                            เลขประจำตัวผู้เสียภาษีของคู่สัญญา</td>
                                        

                                        <td class="style1" colspan="2" >
                                            

                                            <asp:TextBox ID="txtTaxId" runat="server" CssClass="ssddl" 
                                                Text='<%# BindField("tax_id") %>' Width="400px" MaxLength="13"></asp:TextBox>
                                            

                                            <asp:Label ID="Label16" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                            

                                            <asp:Label ID="lblTaxId" runat="server" CssClass="sslbl_red"></asp:Label>
                                            

                                        </td>
                                        

                                    </tr>
                                    

                                    <tr>
                                        

                                        <td class="sslbl_right">
                                            

                                            หลักประกันสัญญา</td>
                                        

                                        <td class="style1" colspan="2" >
                                            

                                            <asp:DropDownList ID="ddlGuarantee" runat="server" CssClass="ssddl" 
                                                Height="20px" Width="405px">
                                                

                                            </asp:DropDownList>
                                            

                                            <asp:Button ID="bAddGuarantee" runat="server" CssClass="ssbtn" Text="เพิ่ม" 
                                                Width="50px" />
                                            

                                            <asp:Label ID="lblGaranteeId" runat="server" CssClass="sslbl_red"></asp:Label>
                                            

                                        </td>
                                        

                                    </tr>
                                    

                                    <tr>
                                        

                                        <td class="sslbl_right">
                                            

                                            เลขที่หลักประกัน</td>
                                        

                                        <td class="style1" colspan="2" >
                                            

                                            <asp:TextBox ID="txtGuaranteeId" runat="server" CssClass="ssddl" MaxLength="25" 
                                                Text='<%# BindField("guarantee_no") %>' Width="400px"></asp:TextBox>
                                            

                                            <asp:Label ID="lblGarantee" runat="server" CssClass="sslbl_red"></asp:Label>
                                            

                                        </td>
                                        

                                    </tr>
                                    

                                    <tr>
                                        

                                        <td class="sslbl_right">
                                            

                                            จำนวนเงินหลักประกัน</td>
                                        

                                        <td class="style1" >
                                            

                                            <asp:TextBox ID="txtgMoney" runat="server" CssClass="ssddl_right" 
                                                MaxLength="11" Text='<%# BindField("money_guarantee") %>' Width="100px"></asp:TextBox>
                                            

                                            บาท</td>
                                        

                                        <td class="sslbl" >
                                            

                                            </td>
                                        

                                    </tr>
                                    

                                    <tr>
                                        

                                        <td class="sslbl_right">
                                            

                                            ชื่อผู้ขาย/ผู้รับจ้าง</td>
                                        

                                        <td class="style1" colspan="2">
                                            

                                            <asp:TextBox ID="txtSaleName" runat="server" CssClass="ssddl" Rows="4" 
                                                Text='<%# BindField("user_sale") %>' TextMode="MultiLine" Width="600px"></asp:TextBox>
                                            

                                            <asp:Label ID="Label17" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                            

                                            <asp:Label ID="lblSaleName" runat="server" CssClass="sslbl_red"></asp:Label>
                                            

                                        </td>
                                        

                                    </tr>
                                    

                                    <tr>
                                        

                                        <td class="sslbl_right">
                                            

                                            คำค้นหา</td>
                                        

                                        <td class="style1" colspan="2" >
                                            

                                            <asp:TextBox ID="txtKeyword" runat="server" CssClass="ssddl" Rows="4" 
                                                Text='<%# BindField("keyword") %>' TextMode="MultiLine" Width="600px"></asp:TextBox>
                                            

                                            <asp:Label ID="Label18" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                            

                                            <asp:Label ID="lblKeyword" runat="server" CssClass="sslbl_red"></asp:Label>
                                            

                                        </td>
                                        

                                    </tr>
                                    

                                    <tr>
                                        

                                        <td class="sslbl_right">
                                            

                                            หัวหน้ากลุ่มนิติกรรมและคดี</td>
                                        

                                        <td class="style1" colspan="2" >
                                            

                                            <asp:DropDownList ID="ddlApp1" runat="server" CssClass="ssddl" Height="20px" 
                                                Width="285px">
                                                

                                            </asp:DropDownList>
                                            

                                            <asp:Label ID="lblAApp1" runat="server" CssClass="sslbl_red"></asp:Label>
                                            

                                        </td>
                                        

                                    </tr>
                                    

                                    <tr>
                                        

                                        <td class="sslbl_right">
                                            

                                            ผู้อำนวยการ</td>
                                        

                                        <td class="style1" colspan="2" >
                                            

                                            <asp:DropDownList ID="ddlApp2" runat="server" CssClass="ssddl" Height="20px" 
                                                Width="285px">
                                                

                                            </asp:DropDownList>
                                            

                                            <asp:Label ID="lblAApp2" runat="server" CssClass="sslbl_red"></asp:Label>
                                            

                                        </td>
                                        

                                    </tr>
                                        

                                        <tr>
                                            

                                            <td class="sslbl_right">
                                                

                                                ผู้มีอำนาจลงนาม</td>
                                            

                                            <td class="sslbl" colspan="2" >
                                                

                                                <asp:TextBox ID="txtName3" runat="server" CssClass="ssddl" ReadOnly="True" 
                                                    Text='<%# BindField("name3") %>' Width="280px"></asp:TextBox>
                                                

                                                <asp:Button ID="bSelect3" runat="server" CssClass="ssbtn" Text="เลือก" />
                                                

                                                <asp:Label ID="Label19" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                                

                                                <asp:Label ID="lblIdName3" runat="server" Text='<%# BindField("user_buy") %>' 
                                                    Visible="False"></asp:Label>
                                                

                                                <asp:Label ID="lblBuyName" runat="server" CssClass="sslbl_red"></asp:Label>
                                                

                                            </td>
                                            

                                    </tr>
                                        

                                        <tr>
                                            

                                            <td class="sslbl_right">
                                                

                                                จำนวนเงินในสัญญา</td>
                                            

                                            <td class="style1" colspan="2" >
                                                

                                                <asp:TextBox ID="txtMoney" runat="server" CssClass="ssddl_right" 
                                                    Text='<%# BindField("money") %>' Width="100px" MaxLength="11"></asp:TextBox>
                                                

                                                บาท<asp:Label ID="Label20" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                                

                                                <asp:Label ID="lblMoney" runat="server" CssClass="sslbl_red"></asp:Label>
                                                

                                            </td>
                                            

                                        </tr>
                                    

                                    <tr>
                                        

                                        <td class="sslbl_right">
                                            

                                            อ้างอิงสัญญา</td>
                                        

                                        <td class="sslbl" colspan="2">
                                            

                                            <asp:TextBox ID="txtNameContract" runat="server" CssClass="ssddl" 
                                                ReadOnly="True" Text='<%# BindField("contract2") %>' Width="500px"></asp:TextBox>
                                            

                                            <asp:Button ID="bSelectContract" runat="server" CssClass="ssbtn" Text="เลือก" />
                                            

                                            <asp:Button ID="bDelContract" runat="server" CssClass="ssbtn" Text="ลบ" />
                                            

                                            <asp:Label ID="lblIdContract" runat="server" 
                                                Text='<%# BindField("ref_contract") %>' Visible="False"></asp:Label>
                                            

                                        </td>
                                        

                                    </tr>
                                    

                                    <tr>
                                        

                                        <td class="style9">
                                            

                                            </td>
                                        

                                        <td class="style1" >
                                            

                                            <asp:Button ID="bSave" runat="server" CssClass="ssbtn" Text="บันทึก" 
                                                Width="80px" />
                                            

                                            <asp:Button ID="bSaveAndSend" runat="server" CssClass="ssbtn" 
                                                Text="บันทึกพร้อมส่ง" Width="100px" />
                                            

                                            <asp:Button ID="bCancel" runat="server" CssClass="ssbtn" Text="ยกเลิก" 
                                                Width="80px" />
                                            

                                        </td>
                                        

                                        <td class="sslbl" >
                                            

                                            <asp:Label ID="lblPrint" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
                                            

                                            </td>
                                        

                                    </tr>
                                    

                                </table>
                       
                                
                                
                    
                


                </div>
            </ContentTemplate>
                </asp:UpdatePanel>     
 
            </ContentTemplate>
            
                
        </cc1:TabPanel>
      
                
             <cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="รายละเอียด" TabIndex="2">
                 <HeaderTemplate>
                     
 
                     รายละเอียด
 
                 </HeaderTemplate>
                 
               <ContentTemplate>
                   

               <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                   

        <ContentTemplate>
            
            &nbsp;<table border="0" cellpadding="1" cellspacing="1" class="form" width="100%">
              

             <tr>
                                        

                                        <td class="">
                                        
                                            
 
                                            

                                        
                                            &nbsp;</td>
                                        

                                        <td class="" align="right">
                                            

                                            <asp:Button ID="bSaveFCK" runat="server" CssClass="ssbtn" Text="บันทึก" 
                                                Width="80px" />
                                            

                                            </td>
                                        

                                    </tr>
                        

                        <tr>
                            

                            <td class="" colspan="2">
                                

                                <FCKeditorV2:FCKeditor ID="FCKeditor1" runat="server" BasePath="~/fckeditor/" 
                                    Height="500px" Value='<%# BindField("message") %>'>
                                </FCKeditorV2:FCKeditor>
                                
                                
 
                                

                                
                            </td>
                            

              </tr>
                        

                        <tr>
                            <td class="">
                                &nbsp;</td>
                            <td align="right" class="">
                                &nbsp;</td>
                </tr>
                        

                        </table> 
                     
          
        
 
            

 
                     
          
        </ContentTemplate>
                   

    </asp:UpdatePanel>
                   
 
               </ContentTemplate> 
                 
             </cc1:TabPanel>
        
       
        
             <cc1:TabPanel ID="TabPanel3" runat="server" HeaderText="เอกสารประกอบ" TabIndex="3">
                 <ContentTemplate>
                     
 
         <asp:UpdatePanel ID="UpdatePanelDOC" runat="server">
                   
        <ContentTemplate>
                  <div class="TabContentBlue">
                    
                                     
 

                    
                                     <table border="0" cellpadding="1" cellspacing="1" class="form" width="100%">
                                         

                                         <tr>
                                             

                                             <td class="sslbl_right" >
                                                 

                                                 ไฟล์แนบ</td>
                                             

                                             <td class="sslbl" width="85%">
                                                 

                                                 <asp:FileUpload ID="FileUpload1" runat="server" CssClass="ssddl" 
                                                     Width="600px" />
                                                 

                                                 <asp:Label ID="lblAFile" runat="server" CssClass="sslbl_red"></asp:Label>
                                                 

                                                 <asp:Label ID="lblStatus" runat="server" Visible="False"></asp:Label>
                                                 

                                                 <asp:Label ID="lblDocId" runat="server" Visible="False"></asp:Label>
                                                 

                                             </td>
                                             

                                         </tr>
                                         

                                         <tr>
                                             

                                             <td class="sslbl_right">
                                                 
 
                                                 ชื่อเอกสาร</td>
                                             

                                             <td class="sslbl">
                                               
                                                         

                                                         <asp:TextBox ID="txtDocDetail" runat="server" CssClass="ssddl" Width="600px"></asp:TextBox>
                                                         

                                                         <asp:Label ID="lblADocDeatil" runat="server" CssClass="sslbl_red"></asp:Label>
                                                         

                                                                                               
                                             
                                             </td>
                                             

                                         </tr>
                                         

                                         <tr>
                                             

                                             <td class="sslbl_right">
                                                 

                                                 จำนวนหน้า</td>
                                             

                                             <td class="sslbl">
                                                 

                                                 <asp:TextBox ID="txtDocPage" runat="server" CssClass="ssddl" MaxLength="6" 
                                                     Width="60px"></asp:TextBox>
                                                 

                                                 <asp:Label ID="lblADocPage" runat="server" CssClass="sslbl_red"></asp:Label>
                                                 

                                             </td>
                                             

                                         </tr>
                                         

                                         <tr>
                                             

                                             <td class="sslbl">
                                                 

                                                 </td>
                                             

                                             <td>
                                                
                                                 
 
                                                 

                                                
                                                 <asp:Button ID="bSaveFile" runat="server" CssClass="ssbtn" Text="บันทึก" 
                                                     Width="80px" />
                                                 

                                                 <asp:Button ID="bCancelFile" runat="server" CssClass="ssbtn" Text="ยกเลิก" 
                                                     Width="80px" />
                                                
                                                 
 
                                                 

                                                
                                             </td>
                                             

                                         </tr>
                                         

                                         <tr>
                                             

                                             <td class="sslbl" colspan="2" width="100%">
                                                 
 
                                                 

                                                 <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
                                                     AllowSorting="True" AutoGenerateColumns="False" BackColor="White" 
                                                     BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="1" 
                                                     CssClass="GridViewStyle" GridLines="Vertical" Width="100%">
                                                             <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                                                             <AlternatingRowStyle BackColor="Gainsboro" />
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
                                                                 <asp:TemplateField ShowHeader="False">
                                                                     <ItemTemplate>
                                                                         <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" 
                                                                             CommandName="Delete" ImageUrl="~/Images/DeleteFolderHS.png" Text="Delete" 
                                                                             ToolTip="ลบ" />
                                                                     </ItemTemplate>
                                                                 
                                                                     <ItemStyle Width="15px" />
                                                                 </asp:TemplateField>
                                                                 <asp:TemplateField ShowHeader="False">
                                                                     <EditItemTemplate>
                                                                         <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="True" 
                                                                             CommandName="Update" ImageUrl="~/Image/save.png" Text="Update" />
                                                                         <asp:ImageButton ID="ImageButton2" runat="server" CausesValidation="False" 
                                                                             CommandName="Cancel" ImageUrl="~/Images/cancel2.bmp" Text="Cancel" />
                                                                     </EditItemTemplate>
                                                                 
                                                                     <ItemTemplate>
                                                                         <asp:ImageButton ID="ImageButton2" runat="server" CausesValidation="False" 
                                                                             CommandName="Edit" ImageUrl="~/Images/Edit.gif" Text="Edit" ToolTip="แก้ไข" />
                                                                     </ItemTemplate>
                                                                 
                                                                     <ItemStyle Width="15px" />
                                                                 </asp:TemplateField>
                                                             </Columns>
                                                 
                                                             <EmptyDataTemplate>
                                                             </EmptyDataTemplate>
                                                 
                                                             <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                                             <HeaderStyle BackColor="#000084" Font-Bold="True" 
                                                     ForeColor="White" />
                                                             <PagerStyle BackColor="#999999" ForeColor="Black" 
                                                     HorizontalAlign="Center" />
                                                             <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" 
                                                     ForeColor="White" />
                                                         </asp:GridView>
                                                 
 
                                                 
 
                                                 
 </td>
                                             

                                         </tr>
                                         

                                     </table>
                     
                                     
 
                                     

                     
                     </div> 
                 
             </ContentTemplate>
                                      <Triggers>
                <asp:PostBackTrigger ControlID="bSaveFile" />
                 </Triggers>  

    </asp:UpdatePanel>
                 </ContentTemplate>
                 
             </cc1:TabPanel>
             
             <cc1:TabPanel ID="TabPanel4" runat="server" HeaderText="หัวหน้าลงนาม" TabIndex="4">
                 <ContentTemplate>
                     

                     <div class="TabContentBlue">
                         

                     <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                         

                         <ContentTemplate>
                        
                            
                                     

                                     
 
                                     

                        
                            
                                     <table border="0" cellpadding="1" cellspacing="1" class="form" width="100%">
                                         

                                        <tr>
                                            

                                        <td class="sslbl_bold" colspan="2">
                                            

                                            ความคิดเห็น</td>
                                            

                                    </tr>
                                         

                                         <tr>
                                             

                                             <td class="sslbl" colspan="2" >
                                                 

                                                 <asp:Label ID="lblApp1" runat="server" Class="sslbl" 
                                                     Text='<%# BindField("witness1_app") %>'></asp:Label>
                                                 

                                             </td>
                                             

                                         </tr>
                                         

                                         <tr>
                                             

                                             <td class="sslbl" colspan="2">
                                                 

                                                 <FCKeditorV2:FCKeditor ID="FCKeditor2" runat="server" BasePath="~/fckeditor/" 
                                                     Height="500px" Value='<%# BindField("witness1_msg") %>'>
                                                     

                                                 </FCKeditorV2:FCKeditor>
                                                 

                                             </td>
                                             

                                         </tr>
                                         

                                         <tr>
                                             

                                             <td class="sslbl_bold" colspan="2">
                                                 

                                                 บันทึก</td>
                                             

                                         </tr>
                                         

                                         <tr>
                                             

                                             <td class="sslbl" width="200" colspan="2">
                                                 

                                                 <asp:Label ID="lblAppComment1" runat="server" Class="sslbl" 
                                                     Text='<%# BindField("witness1_comment") %>'></asp:Label>
                                                 

                                             </td>
                                             

                                         </tr>
                                         

                                         <tr>
                                             

                                             <td class="style1" width="200">
                                                 

                                                 ลงชื่อ

                                                 <asp:Label ID="lblAppName1" runat="server" Class="sslbl" 
                                                     Text='<%# BindField("name1") %>'></asp:Label>
                                                 

                                             </td>
                                             

                                             <td class="style1">
                                                 

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
                                           

                                        <td class="sslbl_bold" colspan="2">
                                            

                                            ความคิดเห็น</td>
                                           

                                    </tr>
                                         

                                         <tr>
                                             

                                             <td class="sslbl" colspan="2" >
                                                 

                                                 <asp:Label ID="lblApp2" runat="server" Class="sslbl" 
                                                     Text='<%# BindField("witness2_app") %>'></asp:Label>
                                                 

                                             </td>
                                             

                                         </tr>
                                         

                                         <tr>
                                             

                                             <td class="sslbl" colspan="2">
                                                 

                                                 <FCKeditorV2:FCKeditor ID="FCKeditor3" runat="server" BasePath="~/fckeditor/" 
                                                     Height="500px" Value='<%# BindField("witness2_msg") %>'>
                                                     

                                                 </FCKeditorV2:FCKeditor>
                                                 

                                             </td>
                                             

                                         </tr>
                                         

                                         <tr>
                                             

                                             <td class="sslbl_bold" width="200" colspan="2">
                                                 

                                                 บันทึก</td>
                                             

                                         </tr>
                                         

                                         <tr>
                                             

                                             <td class="sslbl" colspan="2" width="200">
                                                 

                                                 <asp:Label ID="lblAppComment2" runat="server" Class="sslbl" 
                                                     Text='<%# BindField("witness2_comment") %>'></asp:Label>
                                                 

                                             </td>
                                             

                                         </tr>
                                         

                                         <tr>
                                             

                                             <td class="sslbl" width="200">
                                                 

                                                 ลงชื่อ

                                                 <asp:Label ID="lblAppName2" runat="server" Class="sslbl" 
                                                     Text='<%# BindField("name2") %>'></asp:Label>
                                                 

                                             </td>
                                             

                                             <td class="sslbl">
                                                 

                                                 </td>
                                             

                                         </tr>
                                      

                                     </table>
                           
                                     
 
                                     

                           
                         </ContentTemplate>
                         

                     </asp:UpdatePanel>
                     

                     </div> 
                 
 
                 </ContentTemplate>
                 
             </cc1:TabPanel>
             
    </cc1:TabContainer>
    

  </ContentTemplate>
 
                </asp:UpdatePanel>                          
</asp:Content>
<asp:Content ID="Content3" runat="server" contentplaceholderid="head">

    <style type="text/css">
        .style1
        {
            font: 12px Arial,"Courier New", Courier, monospace;
            color: #000000;
            font-weight : normal;
            text-decoration: none;
            vertical-align: top;
            padding: 1px;
        }
    </style>

</asp:Content>
