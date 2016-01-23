<%@ Page Title="ทะเบียนหนังสือรับ-LAW5101" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="BookInData.aspx.vb" Inherits="Src_BookInData" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<%@ Register src="../UserControl/DatePicker.ascx" tagname="DatePicker" tagprefix="uc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <table class="form" cellpadding="1" cellspacing="1" frame="border" 
        border="0" width="100%">
                <tr>
                    <td class="HeaderGreenTab">
                        <table class="form" width="100%">
                            <tr>
                                <td class="HeaderGreen">
                                    <asp:LinkButton ID="linkHome" runat="server">Home</asp:LinkButton>
                                    &nbsp;&gt;
                                    <asp:LinkButton ID="link1" runat="server">ทะเบียนหนังสือรับ</asp:LinkButton>
                                    &nbsp;&gt;
                                    <asp:LinkButton ID="link2" runat="server">บันทึกหนังสือรับ</asp:LinkButton>
                                    &nbsp;&gt;
                                    <asp:LinkButton ID="link3" runat="server">รายละเอียด</asp:LinkButton>
                                </td>
                                <td class="style9" align="right">
                                    </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="sslbl">
                        <asp:MultiView ID="MultiViewMaster" runat="server">
                            <asp:View ID="View23" runat="server">
                                <table border="0" cellpadding="1" cellspacing="1" class="form" width="100%">
                                    <tr>
                                        <td class="sslbl_right" width="10%">
                                            ลำดับ</td>
                                        <td class="sslbl" width="40%">
                                            <asp:Label ID="lblRunNoShow" runat="server" CssClass="sslbl_bold" 
                                                Text='<%# BindField("runno") %>'></asp:Label>
                                            <asp:Label ID="lblRunNo" runat="server" Text='<%# BindField("runbook") %>' 
                                                CssClass="sslbl_bold" Visible="False"></asp:Label>
                                        </td>
                                        <td class="sslbl_right" width="10%">&nbsp;
                                            </td>
                                        <td class="sslbl" width="40%" >&nbsp;
                                            </td>
                                    </tr>
                                    <tr>
                                        <td class="sslbl_right" width="10%">
                                            เลขที่หนังสือรับ</td>
                                        <td class="sslbl" width="40%">
                                            <asp:TextBox ID="txtBookNo" runat="server" CssClass="ssddl" 
                                                Text='<%# BindField("bookin_no") %>' Width="100px" MaxLength="15"></asp:TextBox>
                                            <asp:Label ID="Label3" runat="server" CssClass="sslbl_red" Text="*"></asp:Label>
                                            <asp:Label ID="lblChkBookNo" runat="server" CssClass="sslbl_red"></asp:Label>
                                            <asp:Label ID="lblMainStatus" runat="server" Visible="False"></asp:Label>
                                            <asp:Label ID="lblId" runat="server" Visible="False"></asp:Label>
                                            <asp:Label ID="lblBookNo" runat="server" Text='<%# BindField("system_no") %>' 
                                                Visible="False"></asp:Label>
                                        </td>
                                        <td class="sslbl_right" width="10%">
                                            สถานะหนังสือ</td>
                                        <td class="sslbl" width="40%">
                                            <asp:DropDownList ID="ddlBookStatus" runat="server" CssClass="ssddl" 
                                                Enabled="False" Width="285px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="sslbl_right">
                                            วันที่รับหนังสือ</td>
                                        <td class="sslbl">
                                            <uc1:DatePicker ID="txtDate" runat="server" Text='<%# BindField("recieve_date") %>' IsNotNull ="true"  />
                                            <asp:Label ID="lblChkDate1" runat="server" CssClass="sslbl_red"></asp:Label>
                                        </td>
                                        <td class="sslbl_right">
                                            เวลา</td>
                                        <td class="sslbl" >
                                            <asp:DropDownList ID="ddlTimeHr" runat="server" CssClass="ssddl" Height="20px" 
                                                Width="65px">
                                                <asp:ListItem>-เลือก-</asp:ListItem>
                                                <asp:ListItem>00</asp:ListItem>
                                                <asp:ListItem>01</asp:ListItem>
                                                <asp:ListItem>02</asp:ListItem>
                                                <asp:ListItem>03</asp:ListItem>
                                                <asp:ListItem>04</asp:ListItem>
                                                <asp:ListItem>05</asp:ListItem>
                                                <asp:ListItem>06</asp:ListItem>
                                                <asp:ListItem>07</asp:ListItem>
                                                <asp:ListItem>08</asp:ListItem>
                                                <asp:ListItem>09</asp:ListItem>
                                                <asp:ListItem>10</asp:ListItem>
                                                <asp:ListItem>11</asp:ListItem>
                                                <asp:ListItem>12</asp:ListItem>
                                                <asp:ListItem>13</asp:ListItem>
                                                <asp:ListItem>14</asp:ListItem>
                                                <asp:ListItem>15</asp:ListItem>
                                                <asp:ListItem>16</asp:ListItem>
                                                <asp:ListItem>17</asp:ListItem>
                                                <asp:ListItem>18</asp:ListItem>
                                                <asp:ListItem>19</asp:ListItem>
                                                <asp:ListItem>20</asp:ListItem>
                                                <asp:ListItem>21</asp:ListItem>
                                                <asp:ListItem>22</asp:ListItem>
                                                <asp:ListItem>23</asp:ListItem>
                                            </asp:DropDownList>
                                            :
                                            <asp:DropDownList ID="ddlTimeMin" runat="server" CssClass="ssddl" Height="20px" 
                                                Width="65px">
                                                <asp:ListItem>-เลือก-</asp:ListItem>
                                                <asp:ListItem>00</asp:ListItem>
                                                <asp:ListItem>01</asp:ListItem>
                                                <asp:ListItem>02</asp:ListItem>
                                                <asp:ListItem>03</asp:ListItem>
                                                <asp:ListItem>04</asp:ListItem>
                                                <asp:ListItem>05</asp:ListItem>
                                                <asp:ListItem>06</asp:ListItem>
                                                <asp:ListItem>07</asp:ListItem>
                                                <asp:ListItem>08</asp:ListItem>
                                                <asp:ListItem>09</asp:ListItem>
                                                <asp:ListItem>10</asp:ListItem>
                                                <asp:ListItem>11</asp:ListItem>
                                                <asp:ListItem>12</asp:ListItem>
                                                <asp:ListItem>13</asp:ListItem>
                                                <asp:ListItem>14</asp:ListItem>
                                                <asp:ListItem>15</asp:ListItem>
                                                <asp:ListItem>16</asp:ListItem>
                                                <asp:ListItem>17</asp:ListItem>
                                                <asp:ListItem>18</asp:ListItem>
                                                <asp:ListItem>19</asp:ListItem>
                                                <asp:ListItem>20</asp:ListItem>
                                                <asp:ListItem>21</asp:ListItem>
                                                <asp:ListItem>22</asp:ListItem>
                                                <asp:ListItem>23</asp:ListItem>
                                                <asp:ListItem>24</asp:ListItem>
                                                <asp:ListItem>25</asp:ListItem>
                                                <asp:ListItem>26</asp:ListItem>
                                                <asp:ListItem>27</asp:ListItem>
                                                <asp:ListItem>28</asp:ListItem>
                                                <asp:ListItem>29</asp:ListItem>
                                                <asp:ListItem>30</asp:ListItem>
                                                <asp:ListItem>31</asp:ListItem>
                                                <asp:ListItem>32</asp:ListItem>
                                                <asp:ListItem>33</asp:ListItem>
                                                <asp:ListItem>34</asp:ListItem>
                                                <asp:ListItem>35</asp:ListItem>
                                                <asp:ListItem>36</asp:ListItem>
                                                <asp:ListItem>37</asp:ListItem>
                                                <asp:ListItem>38</asp:ListItem>
                                                <asp:ListItem>39</asp:ListItem>
                                                <asp:ListItem>40</asp:ListItem>
                                                <asp:ListItem>41</asp:ListItem>
                                                <asp:ListItem>42</asp:ListItem>
                                                <asp:ListItem>43</asp:ListItem>
                                                <asp:ListItem>44</asp:ListItem>
                                                <asp:ListItem>45</asp:ListItem>
                                                <asp:ListItem>46</asp:ListItem>
                                                <asp:ListItem>47</asp:ListItem>
                                                <asp:ListItem>48</asp:ListItem>
                                                <asp:ListItem>49</asp:ListItem>
                                                <asp:ListItem>50</asp:ListItem>
                                                <asp:ListItem>51</asp:ListItem>
                                                <asp:ListItem>52</asp:ListItem>
                                                <asp:ListItem>53</asp:ListItem>
                                                <asp:ListItem>54</asp:ListItem>
                                                <asp:ListItem>55</asp:ListItem>
                                                <asp:ListItem>56</asp:ListItem>
                                                <asp:ListItem>57</asp:ListItem>
                                                <asp:ListItem>58</asp:ListItem>
                                                <asp:ListItem>59</asp:ListItem>
                                     
                                            </asp:DropDownList>
                                            <asp:Label ID="Label6" runat="server" CssClass="sslbl_red" Text="*"></asp:Label>
                                            <asp:Label ID="lblChkTime" runat="server" CssClass="sslbl_red"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="sslbl_right">
                                            หนังสือลงวันที่</td>
                                        <td class="sslbl">
                                            <uc1:DatePicker ID="txtInDate" runat="server" Text='<%# BindField("stamp_date") %>' IsNotNull ="true" />
                                            <asp:Label ID="lblChkDate2" runat="server" CssClass="sslbl_red"></asp:Label>
                                        </td>
                                        <td class="sslbl_right">
                                            ที่มา</td>
                                        <td class="sslbl" >
                                            <asp:TextBox ID="txtFrom" runat="server" Text='<%# BindField("from_name") %>' 
                                                CssClass="ssddl" Width="280px"></asp:TextBox>
                                            <asp:Label ID="Label7" runat="server" CssClass="sslbl_red" Text="*"></asp:Label>
                                            <asp:Label ID="lblChkFrom" runat="server" CssClass="sslbl_red"></asp:Label>
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="sslbl_right">
                                            ความเร่งด่วน</td>
                                        <td class="sslbl">
                                            <asp:DropDownList ID="ddlPriority" runat="server" CssClass="ssddl" 
                                                Height="20px" Width="92px">
                                            </asp:DropDownList>
                                            <asp:Label ID="Label11" runat="server" CssClass="sslbl_red" Text="*"></asp:Label>
                                        </td>
                                        <td class="sslbl_right">
                                            คำค้นหา</td>
                                        <td class="sslbl" >
                                            <asp:TextBox ID="txtKeyword" runat="server" Text='<%# BindField("keyword") %>' 
                                                CssClass="ssddl" Width="280px"></asp:TextBox>
                                            <asp:Label ID="Label8" runat="server" CssClass="sslbl_red" Text="*"></asp:Label>
                                            <asp:Label ID="lblChkKeyword" runat="server" CssClass="sslbl_red"></asp:Label>
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="sslbl_right">
                                            เรื่อง</td>
                                        <td class="sslbl">
                                            <asp:TextBox ID="txtTopic" runat="server" Text='<%# BindField("topic") %>' CssClass="ssddl" Width="300px"></asp:TextBox>
                                            <asp:Label ID="Label9" runat="server" CssClass="sslbl_red" Text="*"></asp:Label>
                                            <asp:Label ID="lblChkTopic" runat="server" CssClass="sslbl_red"></asp:Label>
                                        </td>
                                        <td class="sslbl_right">
                                            ประเภทหนังสือ</td>
                                        <td class="sslbl" >
                                            <asp:DropDownList ID="ddlBookType" runat="server" CssClass="ssddl" 
                                                Width="285px">
                                            </asp:DropDownList>
                                            <asp:Label ID="Label12" runat="server" CssClass="sslbl_red" Text="*"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="sslbl_right">
                                            เสนอ</td>
                                        <td class="sslbl">
                                            <asp:TextBox ID="txtTo" runat="server" Text='<%# BindField("present") %>' CssClass="ssddl" Width="300px"></asp:TextBox>
                                            <asp:Label ID="Label10" runat="server" CssClass="sslbl_red" Text="*"></asp:Label>
                                            <asp:Label ID="lblChkTo" runat="server" CssClass="sslbl_red"></asp:Label>
                                        </td>
                                        <td class="sslbl_right">
                                            ผู้ป้อนข้อมูล</td>
                                        <td class="sslbl" >
                                            <asp:TextBox ID="txtCreate" runat="server"  CssClass="ssddl" Width="280px" 
                                                ReadOnly="True"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="sslbl_right">
                                            หน่วยงาน</td>
                                        <td class="sslbl">
                                            <asp:GridView ID="gdvDIV" runat="server" AutoGenerateColumns="False" 
                                                CellPadding="0" GridLines="None" ShowHeader="False" Width="200px">
                                                <RowStyle BackColor="White" />
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
                                                    <asp:BoundField DataField="div_name" ShowHeader="False" />
                                                </Columns>
                                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                <EditRowStyle BackColor="#2461BF" />
                                                <AlternatingRowStyle BackColor="White" />
                                            </asp:GridView>
                                            <asp:Label ID="lblChkDiv" runat="server" CssClass="sslbl_red"></asp:Label>
                                        </td>
                                        <td class="sslbl_right">
                                            ส่งถึง</td>
                                        <td class="sslbl" >
                                            <asp:DropDownList ID="ddlToName" runat="server" CssClass="ssddl" Height="20px" 
                                                Width="285px">
                                            </asp:DropDownList>
                                            <asp:Label ID="Label13" runat="server" CssClass="sslbl_red" Text="*"></asp:Label>
                                            <asp:Label ID="lblChkSendTo" runat="server" CssClass="sslbl_red"></asp:Label>
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="sslbl">&nbsp;
                                            </td>
                                        <td class="sslbl" colspan="3">
                                            <asp:Button ID="bSave" runat="server" CssClass="ssbtn" Height="26px" 
                                                Text="รายการถูกบันทึก" Width="111px" />
                                            <asp:Button ID="bSaveSend" runat="server" CssClass="ssbtn" Height="26px" 
                                                Text="รายการถูกบันทึกพร้อมส่ง" Width="155px" />
                                            <asp:Button ID="bCancel" runat="server" CssClass="ssbtn" Text="ยกเลิก" 
                                                Width="80px" />
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </asp:View>
                        </asp:MultiView>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
                 
   
      
            <table class="form" cellpadding="1" cellspacing="1" frame="border" 
        border="0" width="100%">
                <tr>
                    <td class="HeaderGreen_1">
                        <table class="form">
                            <tr>
                                <td class="HeaderGreen">
                                    เอกสารแนบ</td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="sslbl">
                        <asp:MultiView ID="MultiViewDoc" runat="server">
                            <asp:View ID="View18" runat="server">
                                <table class="form" border="0" cellpadding="1" cellspacing="1" width="100%">
                                    <tr>
                                        <td class="sslbl_right" width="10%">
                                            แนบไฟล์</td>
                                        <td class="sslbl">
                                            <asp:FileUpload ID="FileUpload1" runat="server" CssClass="ssddl" 
                                        Width="600px" />
                                            <asp:Label ID="lblChkFile" runat="server" CssClass="sslbl_red"></asp:Label>
                                            <asp:Label ID="lblDocStatus" runat="server" Visible="False"></asp:Label>
                                            <asp:Label ID="lblDocId" runat="server" Visible="False"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="sslbl_right" width="100">
                                            ชื่อเอกสาร</td>
                                        <td class="sslbl">
                                            <asp:TextBox ID="txtDocDetail" runat="server" CssClass="ssddl" Width="600px"></asp:TextBox>
                                            <asp:Label ID="lblChkDetail" runat="server" CssClass="sslbl_red"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="sslbl_right" width="100">
                                            จำนวนหน้า</td>
                                        <td class="sslbl">
                                            <asp:TextBox ID="txtDocPage" runat="server" CssClass="ssddl" Width="60px" 
                                        MaxLength="6"></asp:TextBox>
                                            <asp:Label ID="lblChkPage" runat="server" CssClass="sslbl_red"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="sslbl" width="100">&nbsp;
                                            </td>
                                        <td>
                                            <asp:Button ID="bSaveFile" runat="server" CssClass="ssbtn" Text="บันทึก" 
                                        Width="80px" />
                                            <asp:Button ID="bCancelFile" runat="server" CssClass="ssbtn" Text="ยกเลิก" 
                                        Width="80px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="sslbl" width="100%" colspan="2">
                                   
                                                <ContentTemplate>
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
                                                            <asp:TemplateField ShowHeader="False">
                                                                <ItemStyle Width="15px" />
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" 
                                                                        CommandName="Delete" ImageUrl="~/Images/DeleteFolderHS.png" Text="Delete" 
                                                                        ToolTip="ลบ" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ShowHeader="False">
                                                                <EditItemTemplate>
                                                                    <asp:ImageButton ID="ImageButton3" runat="server" CausesValidation="True" 
                                                                        CommandName="Update" ImageUrl="~/Image/save.png" Text="Update" />
                                                                    <asp:ImageButton ID="ImageButton2" runat="server" CausesValidation="False" 
                                                                        CommandName="Cancel" ImageUrl="~/Images/cancel2.bmp" Text="Cancel" />
                                                                </EditItemTemplate>
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="ImageButton4" runat="server" CausesValidation="False" 
                                                                        CommandName="Edit" ImageUrl="~/Images/Edit.gif" Text="Edit" ToolTip="แก้ไข" />
                                                                </ItemTemplate>
                                                                <ItemStyle Width="15px" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                                        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                                       
                                                        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                                                        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                                                        <AlternatingRowStyle BackColor="#DCDCDC" />
                                                    </asp:GridView>
                                                </ContentTemplate>
                                        
                                        </td>
                                    </tr>
                                </table>
                            </asp:View>
                        </asp:MultiView>
            </table>
        </ContentTemplate>
 
                  
                  </asp:Content>










