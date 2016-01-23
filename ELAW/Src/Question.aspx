<%@ Page Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="Question.aspx.vb" Inherits="Profile_Question" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <center>
    <div>       
    <table style="width:100%;">
            <tr>
                <td colspan="2" class="HeaderGreen"><b>แบบสอบถาม</b></td>
            </tr>
            <tr>
                <td colspan="2" align="left" >
                    <asp:LinkButton ID="lbtnCreate" runat="server">สร้างคำถามใหม่</asp:LinkButton>
                    &nbsp;
                    <asp:LinkButton ID="lbtnEdit" runat="server">แก้ไขคำถามเก่า</asp:LinkButton>
                </td>
            </tr>
    </table> 
        <asp:Panel ID="PanelCreate" runat="server">
        <table style="width:100%;">
            <tr>
                <td align="left" width="20%" colspan="2">สร้างหัวข้อแบบสอบถามใหม่</td>
            </tr>
            <tr>
                <td  width = "20%" align = "right">เรื่อง : </td>
                <td align ="left"><asp:TextBox ID="txtELAW" runat="server" Width="380px" TextMode="MultiLine" Height="50px" ></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td  width = "20%" align = "right">จำนวนวันที่ตั้งหัวข้อ :</td>
                <td align ="left"><asp:TextBox ID="txtNumberDay" runat="server" Width="80px"></asp:TextBox>
                    &nbsp;วัน
                </td>
            </tr>
            <tr>
                <td  width = "20%" align = "right">ชื่อผู้สร้าง:</td>
                <td align ="left"><asp:TextBox ID="txtCreateUser" runat="server" Width="198px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" width="20%">
                    จำนวนหัวข้อ :</td>
                <td align="left">
                    <asp:TextBox ID="txtNumberELAW" runat="server" Width="30px"></asp:TextBox>
                    &nbsp;<asp:Button ID="btnAddELAW" runat="server" Text="เพิ่มหัวข้อ" Width="80px" />
                </td>
            </tr>
            <tr>
                <td align="right" width="20%">
                    เอกสาร :&nbsp;</td>
                <td align="left">
                    <asp:FileUpload ID="FileUpload" runat="server" Width="380px" />
                </td>
            </tr>
            <tr>
                <td align="right" width="20%">
                    &nbsp;</td>
                <td align="left">
                    <asp:Panel ID="Panel" runat="server">
                        <table style="width:100%;">
                            <tr>
                                <td>
                                    <asp:Repeater ID="rptText" runat="server">
                                        <ItemTemplate>
                                            <table width="500">
                                                <tr>
                                                    <td align="right" width="100px">
                                                        <asp:Label ID="lbl" runat="server" Text="<%# Bind('lbl')%>"></asp:Label>
                                                        <br></br>
                                                    </td>
                                                    <td align="left" width="400">
                                                        <asp:TextBox ID="txt" runat="server" Width="300" TextMode="MultiLine"  Height="50px" ></asp:TextBox>
                                                        <br></br>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td align="right" width="20%">
                    <asp:Label ID="lblID" runat="server" Visible="False"></asp:Label>
                </td>
                <td align="left">
                    <asp:Button ID="btnCreateELAW" runat="server" Enabled="False" 
                        Text="บันทึกข้อมูล" Width="120px" />
                </td>
            </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="pnlEdit" runat="server" Visible="false" >
        <table style="width:100%;">
            <tr>
                <td colspan="2" align="left" width="20%">แก้ไขหัวข้อแบบสอบถามเก่า</td>
            </tr>
            <tr>
                <td align="right" width="20%">
                    หัวข้อ :</td>
                <td align="left">
                    <asp:DropDownList ID="ddlSubj" runat="server" Width="500px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td  width = "20%" align = "right">&nbsp;</td>
                <td align ="left">
                    <asp:Button ID="btnSearch" runat="server" Text="ค้นหาหัวข้อ" Width="100px" />
                </td>
            </tr>
            <tr>
                <td align="right" width="20%">
                    <asp:HiddenField ID="HFSubj_id" runat="server" Value="<%# Bind('question_id')%>" />
                </td>
                <td align="left">
                    <asp:Repeater ID="RepeaterQuestions" runat="server">
                        <ItemTemplate>
                            <table width="500">
                                <tr>
                                    <td align="right" width="80">
                                        คำถามที่ <asp:Label ID="lblNo" runat="server" Text="<%# Convert.ToString(Container.ItemIndex + 1) %>"></asp:Label>
                                        &nbsp;<br></br>
                                    </td>
                                    <td align="left" width="420">
                                        <asp:TextBox ID="txt" runat="server" Width="400" Text="<%# Bind('subj_question')%>"></asp:TextBox>
                                        <asp:HiddenField ID="HF" runat="server" Value="<%# Bind('question_id')%>" />
                                        <br></br>
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:Repeater>
                </td>
            </tr>
            <tr id="trow" runat="server" visible="false">
                <td align="right" width="20%">
                    <asp:Label ID="lblFileID" runat="server" Visible="False"></asp:Label>
                    เอกสาร :&nbsp;</td>
                <td align="left">
                    <asp:FileUpload ID="FileUpload1" runat="server" Width="380px" />
                </td>
            </tr>
            <tr>
                <td align="right" width="20%">
                    &nbsp;</td>
                <td align="left">
                    <asp:Button ID="btnUpdate" runat="server" Text="ปรับปรุงข้อมูล" Width="100px" Visible="false" />
                    <asp:Button ID="bDel" runat="server" ForeColor="Red" Text="ลบ" 
                        Visible="False" />
                </td>
            </tr>
        </table> 
        </asp:Panel> 
    </div>
</center>
</asp:Content>
