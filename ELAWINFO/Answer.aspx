<%@ Page Language="VB" MasterPageFile="MasterPageProfile.master" AutoEventWireup="false" CodeFile="Answer.aspx.vb" Inherits="Profile_Answer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <center>
    <div>   
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        <table style="width: 100%;">
           <tr>
                <td align="center" colspan="2" >
                    <h1>
                        ตอบคำถาม</h1>
                </td>
            </tr>
           <tr>        
           <td  width = "20%" align = "right">หัวข้อกฏหมาย : </td>
                <td align ="left">
                    
                    <asp:DropDownList ID="ddlSubj" runat="server" AutoPostBack="True" Width="400px">
                    </asp:DropDownList>
               </td>
            </tr>
            <tr>        
           <td  width = "20%" align = "right">&nbsp;</td>
                <td align ="left">
                    
                    <asp:LinkButton ID="lbtnDownload" runat="server" Visible="false" >ดาวน์โหลดเอกสาร</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2" valign="top" >
                <table width="100%" cellpadding="3" cellspacing="3">
                    <tr>
                        <td align="center">
                        <asp:Repeater ID="RepeaterAnswer" runat="server">
                        <ItemTemplate>
                            <table width="400px" cellpadding="3" cellspacing="3">
                                <tr>
                                    <td align="right" width="60px">
                                        <asp:Label ID="lbl" runat="server" 
                                            Text="<%#Choose(Convert.toString(Container.itemIndex +1))%>"></asp:Label>
                                    </td>
                                    <td align="left" width="300px">
                                        <asp:Label ID="txtSubj_question" runat="server" 
                                            Text="<%# Bind('subj_question')%>" Width="300px" ></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" width="60px">
                                        <%--<asp:Label ID="lblAnswer" runat="server" 
                                            Text="<%#ChooseAnswer(Convert.toString(Container.itemIndex +1))%>"></asp:Label>--%>
                                        <asp:HiddenField ID="HiddenField" runat="server" 
                                            Value="<%# Bind('question_id')%>" />
                                    </td>
                                    <td align="left" width="300px">
                                        <asp:TextBox ID="txtAnswer" runat="server" Text="" Width="300px" TextMode="MultiLine" Height="50px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:Repeater>
                        </td>
                    </tr>
                </table> 
                </td>
            </tr>
           <tr>        
                <td align ="center" colspan="2" >
                    <asp:Button ID="btnCreateAnswer" runat="server" Text="บันทึกข้อมูล" 
                        Width="120px" />
                </td>       
            </tr>
            </table>
        </ContentTemplate>
        </asp:UpdatePanel>             
    </div>        
</center>
</asp:Content>

