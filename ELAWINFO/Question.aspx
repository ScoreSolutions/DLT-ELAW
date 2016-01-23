<%@ Page Language="VB" MasterPageFile="MasterPageProfile.master" AutoEventWireup="false" CodeFile="Question.aspx.vb" Inherits="Profile_Question" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <center>
    <div>       
    <table style="width:100%;">
            <tr>
                <td colspan="2"><h1>Ẻ�ͺ���</h1></td>
            </tr>
            <tr>
                <td colspan="2" align="left" >
                    <asp:LinkButton ID="lbtnCreate" runat="server">���ҧ�Ӷ������</asp:LinkButton>
                    &nbsp;
                    <asp:LinkButton ID="lbtnEdit" runat="server">��䢤Ӷ�����</asp:LinkButton>
                </td>
            </tr>
    </table> 
        <asp:Panel ID="PanelCreate" runat="server">
        <table style="width:100%;">
            <tr>
                <td align="left" width="20%" colspan="2">���ҧ��Ǣ������</td>
            </tr>
            <tr>
                <td  width = "20%" align = "right">����ͧ : </td>
                <td align ="left"><asp:TextBox ID="txtELAW" runat="server" Width="380px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td  width = "20%" align = "right">�ӹǹ�ѹ�������Ǣ�� :</td>
                <td align ="left"><asp:TextBox ID="txtNumberDay" runat="server" Width="80px"></asp:TextBox>
                    &nbsp;�ѹ
                </td>
            </tr>
            <tr>
                <td  width = "20%" align = "right">���ͼ�����ҧ:</td>
                <td align ="left"><asp:TextBox ID="txtCreateUser" runat="server" Width="198px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" width="20%">
                    �ӹǹ��Ǣ�� :</td>
                <td align="left">
                    <asp:TextBox ID="txtNumberELAW" runat="server"></asp:TextBox>
                    &nbsp;<asp:Button ID="btnAddELAW" runat="server" Text="������Ǣ��" Width="80px" />
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
                                            <table width="400">
                                                <tr>
                                                    <td align="right" width="80px">
                                                        <asp:Label ID="lbl" runat="server" Text="<%# Bind('lbl')%>"></asp:Label>
                                                        <br></br>
                                                    </td>
                                                    <td align="left" width="400">
                                                        <asp:TextBox ID="txt" runat="server" Width="200"></asp:TextBox>
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
                    &nbsp;</td>
                <td align="left">
                    <asp:Button ID="btnCreateELAW" runat="server" Enabled="False" 
                        Text="�ѹ�֡������" Width="120px" />
                </td>
            </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="pnlEdit" runat="server" Visible="false" >
        <table style="width:100%;">
            <tr>
                <td colspan="2" align="left" width="20%">�����Ǣ�����</td>
            </tr>
            <tr>
                <td align="right" width="20%">
                    ��Ǣ�� :</td>
                <td align="left">
                    <asp:DropDownList ID="ddlSubj" runat="server" Width="500px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td  width = "20%" align = "right">&nbsp;</td>
                <td align ="left">
                    <asp:Button ID="btnSearch" runat="server" Text="������Ǣ��" Width="100px" />
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
                                        �Ӷ����� <asp:Label ID="lblNo" runat="server" Text="<%# Convert.ToString(Container.ItemIndex + 1) %>"></asp:Label>
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
            <tr>
                <td align="right" width="20%">
                    &nbsp;</td>
                <td align="left">
                    <asp:Button ID="btnUpdate" runat="server" Text="��Ѻ��ا������" Width="100px" Visible="false" />
                </td>
            </tr>
        </table> 
        </asp:Panel> 
    </div>
</center>
</asp:Content>
