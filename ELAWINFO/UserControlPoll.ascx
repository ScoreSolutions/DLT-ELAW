<%@ Control Language="VB" AutoEventWireup="false" CodeFile="UserControlPoll.ascx.vb" Inherits="Profile_UserControlPoll" %>
          
<%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>--%>
<table style="width: 195px;">
                   <tr>       
                        <td align="left">
                            <span style="color: Blue;">ท่านมีความพึ่งพอใจในการสืบค้นข้อมูลกฎหมาย หรือไม่</span>
                        </td>
                   </tr> 
                   <tr>       
                        <td align="center">
                        <table  style="width: 150px; color: #6699FF;">
                           <tr>       
                                <td width="30px" align="center" >
                                    (5)</td>
                                <td align="left">
                                    <asp:RadioButton ID="RadioButton5" runat="server" GroupName = "Poll"  
                                        Text=" มากที่สุด" Checked="true"/>
                                </td>
                           </tr>
                           <tr>       
                                <td width="30px" align="center" >
                                    (4)</td>
                                <td align="left">
                                    <asp:RadioButton ID="RadioButton4" runat="server" GroupName = "Poll"  
                                        Text=" มาก" />
                                </td>
                           </tr>
                           <tr>       
                                <td width="30px" align="center" >
                                    (3)</td>
                                <td align="left">
                                    <asp:RadioButton ID="RadioButton3" runat="server" GroupName = "Poll"  
                                        Text=" ปานกลาง" />
                                </td>
                            <tr>       
                                <td width="30px" align="center" >
                                    (2)</td>
                                <td align="left">
                                    <asp:RadioButton ID="RadioButton2" runat="server" GroupName = "Poll"  
                                        Text=" น้อย" />
                                </td>
                           </tr>
                           <tr>       
                                <td width="30px" align="center" >
                                    (1)</td>
                                <td align="left">
                                    <asp:RadioButton ID="RadioButton1" runat="server" GroupName = "Poll"  
                                        Text=" น้อยที่สุด" />
                                </td>
                           </tr>
                        </table> 
                            
                        </td>
                   </tr>
                   <tr>       
                        <td align="center">
                            <asp:Button ID="btnVote" runat="server" Text="โหวต" Width="100px" />
                        </td>
                   </tr>
                </table> 

<table style="width: 195px;" cellpadding="3" cellspacing="3">
           <tr>       
                <td align="center">
                </td>
            </tr>
            <tr>
                <td align="center">
                <span style="color: Blue;">จำนวนผู้ให้คะแนน
                    <asp:Label ID="lblUser" runat="server" Text=""></asp:Label>&nbsp;คน</span></td>
            </tr>
           <tr>        
                <td align ="center">
                <table style="width: 140px; color: #6699FF;">
                   <tr>       
                        <td align="left" width="100px">(5) มากที่สุด</td>
                        <td align="right" width="100px"><asp:Label ID="lbl5" runat="server" Text="0"></asp:Label> %</td>
                   </tr> 
                   <tr>       
                        <td align="left">(4) มาก</td>
                        <td align="right"><asp:Label ID="lbl4" runat="server" Text="0"></asp:Label> %</td>
                   </tr>
                   <tr>       
                        <td align="left">(3) ปานกลาง</td>
                        <td align="right"><asp:Label ID="lbl3" runat="server" Text="0"></asp:Label> %</td>
                   </tr>
                   <tr>       
                        <td align="left">(2) น้อย</td>
                        <td align="right"><asp:Label ID="lbl2" runat="server" Text="0"></asp:Label> %</td>
                   </tr>
                   <tr>       
                        <td align="left">(1) น้อยที่สุด</td>
                        <td align="right"><asp:Label ID="lbl1" runat="server" Text="0"></asp:Label> %</td>
                   </tr>
                </table> 
                </td>       
            </tr>
            <tr>
                <td align ="center">
                </td>
            </tr>
            <tr>
                <td align ="center">
                    <%--จำนวนผู้เข้าชมทั้งหมด&nbsp;
                    <asp:Label ID="lblCount" runat="server" Text=""></asp:Label>
                    &nbsp;คน--%></td>
            </tr>
            </table>
<%--</ContentTemplate>
</asp:UpdatePanel>--%>
                