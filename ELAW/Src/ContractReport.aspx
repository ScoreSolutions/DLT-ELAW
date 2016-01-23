<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="ContractReport.aspx.vb" Inherits="Src_ContractReport" %>

<%@ Register src="../UserControl/DatePicker.ascx" tagname="DatePicker" tagprefix="uc1" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table class="form">
        <tr>
            <td class="HeaderGreen" colspan="2">
                รายงานการทำสัญญา</td>
        </tr>
        <tr>
            <td class="sslbl_right" width="10%">
                ประเภทสัญญา</td>
            <td class="sslbl">
                        <asp:DropDownList ID="ddlContract" runat="server" 
                    Height="20px" Width="300px" CssClass="ssddl">
                        </asp:DropDownList>
                    </td>
        </tr>
        <tr>
            <td class="sslbl_right" width="10%">
                ชื่อ-สกุล</td>
            <td class="sslbl">
                        <asp:DropDownList ID="ddlFullName" runat="server" 
                    Height="20px" Width="300px" CssClass="ssddl">
                        </asp:DropDownList>
                    </td>
        </tr>
        <tr>
            <td class="sslbl_right" width="10%">
                จากวันที่</td>
            <td class="sslbl">
                <uc1:DatePicker ID="DatePicker1" runat="server" IsNotNull ="true"  />
            </td>
        </tr>
        <tr>
            <td class="sslbl_right" width="10%">
                ถึงวันที่</td>
            <td class="sslbl">
                <uc1:DatePicker ID="DatePicker2" runat="server" IsNotNull ="true"  />
            </td>
        </tr>
        <tr>
            <td class="sslbl_right" width="10%">
                &nbsp;</td>
            <td class="sslbl">
                <asp:Button ID="bPreview" runat="server" CssClass="ssbtn" Text="ดูรายงาน" 
                    Width="101px" />
            </td>
        </tr>
    </table>
</asp:Content>

