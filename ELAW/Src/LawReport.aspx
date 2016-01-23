<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="LawReport.aspx.vb" Inherits="Src_LawReport" %>

<%@ Register src="../UserControl/DatePicker.ascx" tagname="DatePicker" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table class="form">
        <tr>
            <td class="HeaderGreen" colspan="2">
                        <asp:LinkButton ID="linkHome" runat="server">Home</asp:LinkButton>
                        &nbsp;&gt;
                        <asp:LinkButton ID="link1" runat="server">ติดตามงานกฎหมาย</asp:LinkButton>
                        &nbsp;&gt;
                        <asp:LinkButton ID="link2" runat="server">รายงานผลการดำเนินงานด้านกฎหมาย</asp:LinkButton>
                    </td>
        </tr>
        <tr>
            <td class="sslbl_right" width="10%">
                หน่วยงาน</td>
            <td class="sslbl">
                <asp:DropDownList ID="ddlDiv" runat="server" 
                    Height="20px" Width="300px" CssClass="ssddl" AutoPostBack="True">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="sslbl_right" width="10%">
                ชื่อ-สกุล</td>
            <td class="sslbl">
                <asp:DropDownList ID="ddlName" runat="server" 
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

