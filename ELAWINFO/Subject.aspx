<%@ Page Language="VB" MasterPageFile="MasterPageProfile.master" AutoEventWireup="false" CodeFile="Subject.aspx.vb" Inherits="Profile_Subject" %>


<%@ Register src="DatePicker.ascx" tagname="DatePicker" tagprefix="uc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <style type="text/css">

.ssddl 
{
	font: 12px Arial,"Courier New", Courier, monospace;
	color: #000000;
	font-weight :normal;
	text-decoration: none;
	vertical-align:top ;
	padding: 1px;
}

    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <center>
    <div align="center">   
        <table style="width: 100%;">
            <tr>
                <td align="center" colspan="2">
                    <b>รายงานแสดงข้อมูลการตอบคำถามรายวัน</b></td>
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
                <td align="right">&nbsp;</td>
                <td align = "left">
                    <asp:Button ID="btnFind" runat="server" Text="แสดงข้อมูล" Width="100px" />
                </td>
            </tr>
            <tr>
                <td align="left" colspan="2">&nbsp;</td>
            </tr>
            <tr>
                <td align="center" colspan="2">
            <asp:Repeater runat="server" ID="rptSubject" >             
                <ItemTemplate>
                <table  border="1" width="600px">
                     <tr>
                        <td colspan="2"  align="center">
                            <b><asp:Label ID="lblsubject_desc" runat="server" /></b></td>
                        </tr>               
                        <asp:Repeater ID="rptQuestion" runat="server">
                        <ItemTemplate>
                                <tr>
                                  <td align="left" valign="top">
                                      <b>
                                      คำถามที่ <asp:Label ID="lblNo" runat="server" Text="<%# Convert.ToString(Container.ItemIndex + 1) %>"></asp:Label>
                                      <asp:Label ID="lblQuestion" runat="server" /></b>
                                      <br />
                                      <asp:Repeater ID="rptAnswer" runat="server">
                                        <ItemTemplate>
                                        <table width="100%">
                                             <tr bgcolor="#dbe6ec">
                                               <td align="left" valign="top">
                                               คำตอบที่ <asp:Label ID="lblNo" runat="server" Text="<%# Convert.ToString(Container.ItemIndex + 1) %>"></asp:Label>
                                                  <asp:Label ID="lblAnswer" runat="server" /></td>                                                                                    
                                              </tr>
                                          </table>    
                                         </ItemTemplate>
                                         </asp:Repeater>
                                      </td>
                                </tr>
                        </ItemTemplate>
                    </asp:Repeater>             
                </table>     
            </ItemTemplate>
            </asp:Repeater>             
       </td>
       </tr>
      </table>               
    </div>        
</center>
</asp:Content>
