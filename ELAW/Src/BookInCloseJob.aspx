﻿<%@ Page Title="รับงาน-LAW5207" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="BookInCloseJob.aspx.vb" Inherits="Src_BookInCloseJob" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table class="form" border="0" cellpadding="1" cellspacing="1">
                <tr>
                    <td class="HeaderGreen" colspan="2">
                       <asp:LinkButton ID="linkHome" runat="server">Home</asp:LinkButton>
                        &nbsp;&gt;
                        <asp:LinkButton ID="link1" runat="server">ทะเบียนหนังสือรับ</asp:LinkButton>
                        &nbsp;&gt; 
                        <asp:LinkButton ID="link2" runat="server">รับงาน</asp:LinkButton>
                      </td>
                </tr>
                <tr>
                    <td class="sslbl_right">
                        ประเภทหนังสือ</td>
                    <td>
                        <asp:DropDownList ID="ddlType" runat="server" 
                    Height="20px" Width="300px" CssClass="ssddl">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="sslbl_right">
                        เลขที่หนังสือ</td>
                    <td>
                        <asp:TextBox ID="txtBookNo" runat="server" CssClass="ssddl" MaxLength="15"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="sslbl_right">
                        เรื่อง</td>
                    <td>
                        <asp:TextBox ID="txtTopic" runat="server" CssClass="ssddl" Width="600px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="sslbl_right">
                        ที่มา</td>
                    <td>
                        <asp:TextBox ID="txtFrom" runat="server" CssClass="ssddl" Width="600px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="sslbl_right">
                        คำค้นหา</td>
                    <td>
                        <asp:TextBox ID="txtKeyword" runat="server" CssClass="ssddl" Width="600px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="sslbl_right">
                        &nbsp;</td>
                    <td>
                        <asp:Button ID="bSearch" runat="server" CssClass="ssbtn" Text="ค้นหา" />
                    </td>
                </tr>
            </table>
            <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
                AllowSorting="True" AutoGenerateColumns="False" BackColor="White" 
                BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="1" 
                CssClass="GridViewStyle" GridLines="Vertical" PageSize="20" Width="100%">
                <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                <Columns>
                    <asp:BoundField DataField="bookin_no" HeaderText="หมายเลขหนังสือ" 
                        SortExpression="bookin_no" />
                    <asp:BoundField DataField="topic" HeaderText="เรื่อง" SortExpression="topic" />
                    <asp:BoundField DataField="bookkind_name" HeaderText="ประเภทหนังสือ" 
                        SortExpression="bookkind_name" />
                    <asp:BoundField DataField="status_name" HeaderText="สถานะหนังสือ" 
                        SortExpression="status_name" />
                    <asp:BoundField DataField="recieve_date" 
                        DataFormatString="{0:dd/MM/yyyy HH:mm}" HeaderText="วันที่รับเรือง" 
                        SortExpression="recieve_date" />
                    <asp:BoundField DataField="from_name" HeaderText="ที่มา" 
                        SortExpression="from_name" />
                    <asp:CommandField EditText="เลือก" ShowEditButton="True">
                    <ItemStyle Font-Bold="True" ForeColor="#000099" HorizontalAlign="Center" />
                    </asp:CommandField>
                </Columns>
                <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                <EmptyDataTemplate>
                                        <table width="100%">
                                        <tr>
                                        <td align="center">
                                        <asp:Label ID="Label8" runat="server" CssClass="sslbl_red" 
                                        Text="***ไม่พบข้อมูล***"></asp:Label>
                                        </td>
                                        </tr>
                                        </table>
                </EmptyDataTemplate>
                <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                <AlternatingRowStyle BackColor="#DCDCDC" />
            </asp:GridView>
            <asp:HiddenField ID="hdfEmpNo" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

