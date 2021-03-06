﻿<%@ Page Title="แก้ไขข้อมูลคดี-LAW3207" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="CaseEditList.aspx.vb" Inherits="Src_CaseEditList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table class="form" border="0" cellpadding="1" cellspacing="1">
                <tr>
                    <td class="HeaderGreen" colspan="2">
                        <asp:LinkButton ID="linkHome" runat="server">Home</asp:LinkButton>
                        &nbsp;&gt;
                        <asp:LinkButton ID="link1" runat="server">งานคดี</asp:LinkButton>
                        &nbsp;&gt;
                        <asp:LinkButton ID="link2" runat="server">แก้ไขข้อมูลคดี</asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td class="sslbl_right">
                        ประเภทคดี&nbsp;</td>
                    <td>
                        <asp:DropDownList ID="ddlCase" runat="server" 
                    Height="20px" Width="300px" CssClass="ssddl" AutoPostBack="True">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="sslbl_right">
                        หมายเลขคดีดำ</td>
                    <td>
                        <asp:TextBox ID="txtBlackNo" runat="server" CssClass="ssddl" MaxLength="10"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="sslbl_right">
                        หมายเลขคดีแดง</td>
                    <td>
                        <asp:TextBox ID="txtRedNo" runat="server" CssClass="ssddl" MaxLength="10"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="sslbl_right">
                        ชื่อโจทก์</td>
                    <td>
                        <asp:TextBox ID="txtProsecutor" runat="server" CssClass="ssddl" Width="600px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="sslbl_right">
                        จำเลย</td>
                    <td>
                        <asp:TextBox ID="txtDefendant" runat="server" CssClass="ssddl" Width="600px"></asp:TextBox>
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
                    <asp:BoundField DataField="black_no" HeaderText="หมายเลขคดีดำ" 
                        SortExpression="black_no" />
                    <asp:BoundField DataField="red_no" HeaderText="หมายเลขคดีแดง" 
                        SortExpression="red_no" />
                    <asp:BoundField DataField="status_name" HeaderText="สถานะคดี" 
                        SortExpression="status_name" />
                    <asp:BoundField DataField="type_name" HeaderText="ประเภทคดี" 
                        SortExpression="type_name" />
                      <asp:BoundField DataField="prosecutor" HeaderText="โจทก์" 
                        SortExpression="prosecutor" />  
                    <asp:BoundField DataField="defendant" HeaderText="จำเลย" 
                        SortExpression="defendant" />
                    
                    <asp:BoundField DataField="fullname" HeaderText="เจ้าของเรื่อง" 
                        SortExpression="fullname" />
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
 
        </ContentTemplate>
    </asp:UpdatePanel>
   
    </asp:Content>

