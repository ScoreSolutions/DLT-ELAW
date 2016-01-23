<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPageProfile.master" AutoEventWireup="false" CodeFile="ClientQuetions.aspx.vb" Inherits="ClientQuetions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <center>
    <div>       
    <table style="width:95%;">
            <tr>
                <td class="subtitle">ฝากคำถามทางกฏหมาย</td>
            </tr>
            </table> 
        <asp:Panel ID="PanelCreate" runat="server">
        <table style="width:95%;">
            <tr>
                <td  width = "15%" align="left" valign="top" colspan="2" >
                <fieldset>
                    <legend><b>หัวข้อกฏหมายที่สนใจ</b></legend>
                    <table style="width:100%;" cellpadding="3" cellspacing="3">
                        <tr valign="top" hight="300px">
                            <td>
                                    <legend><b>กฏหมาย</b></legend>
                                    </b>
                                    <asp:CheckBoxList ID="cblLAW_TYPE" runat="server" RepeatColumns="2" RepeatDirection="Vertical" Width="100%">
                                    </asp:CheckBoxList>
                            </td>
                            <td>
                                    <legend><b>สัญญา</b></legend>
                                    </b>
                                    <asp:CheckBoxList ID="cblCONTRACT_TYPE" runat="server" RepeatColumns="2" RepeatDirection="Vertical" Width="100%">
                                    </asp:CheckBoxList>
                            </td>
                            <td>
                                    <legend><b>คดี</b></legend>
                                    </b>
                                    <asp:CheckBoxList ID="cblCASE_TYPE" runat="server" RepeatColumns="2" RepeatDirection="Vertical" Width="100%">
                                    </asp:CheckBoxList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" align="left" valign="middle" >
                                <legend><b>อื่นๆ</b></legend>
                                </b>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" align="left" valign="middle" >
                                <asp:TextBox ID="txtOther" runat="server" Width="600px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </fieldset>
                </td>
            </tr>
            <tr>
                <td align="right" valign="top" width="15%">
                    &nbsp;</td>
                <td align="left" width="85%">
                    &nbsp;</td>
            </tr>
            <tr>
                <td align="right" valign="top" width="15%">
                    คำถาม :</td>
                <td align="left">
                    <asp:TextBox ID="txtQuestion" runat="server" Height="100px" 
                        TextMode="MultiLine" Width="500px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" width="15%">
                    ชื่อ-นามสกุล :</td>
                <td align="left">
                    <asp:TextBox ID="txtName" runat="server" Width="250px"></asp:TextBox></td>
            </tr>
            <tr>
                <td align="right" width="20%">
                    อีเมล์ :</td>
                <td align="left">
                    <asp:TextBox ID="txtEmail" runat="server" Width="250px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" width="20%">
                    <asp:Label ID="lblIDQuestion" runat="server" Text="" Visible="false" ></asp:Label>
                </td>
                <td align="left">
                    <asp:Button ID="btnSent" runat="server" Text="ส่งคำถาม" Width="120px" />
                </td>
            </tr>
            </table>
        </asp:Panel>
    </div>
</center>
</asp:Content>

