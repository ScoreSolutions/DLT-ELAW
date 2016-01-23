<%@ Page Title="แสดงคำพิพากษา-LAW3211" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="ShowDecision.aspx.vb" Inherits="Src_ShowDecision" %>

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
                    <asp:LinkButton ID="link2" runat="server">แสดงคำพิพากษา</asp:LinkButton>
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
                        <asp:TextBox ID="txtDefendant" runat="server" CssClass="ssddl" Width="600px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="sslbl_right">
                        จำเลย</td>
                    <td>
                        <asp:TextBox ID="txtProsecutor" runat="server" CssClass="ssddl" Width="600px"></asp:TextBox>
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
                        <asp:Label ID="lblCaseId" runat="server" Visible="False"></asp:Label>
                    </td>
                </tr>
            </table>
            <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
                AllowSorting="True" AutoGenerateColumns="False" BackColor="White" 
                BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="1" 
                CssClass="GridViewStyle" GridLines="Vertical" PageSize="20" Width="100%">
                <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                <Columns>
                    <asp:TemplateField HeaderText="case_no" SortExpression="case_no" 
                        Visible="False">
                        <ItemTemplate>
                            <asp:Label ID="Label4" runat="server" Text='<%# Bind("case_no") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
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
                    <asp:CommandField SelectText="เลือก" ShowSelectButton="True">
                    <ItemStyle ForeColor="Blue" HorizontalAlign="Left" />
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
                <SelectedRowStyle Font-Bold="True" ForeColor="Black" />
                <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                <AlternatingRowStyle BackColor="#DCDCDC" />
            </asp:GridView>
            <br />
            <asp:Label ID="lblHeader" runat="server" style="font-weight: 700"></asp:Label>
            <br />
            <asp:GridView ID="GridView2" runat="server" AllowPaging="True" 
                AllowSorting="True" AutoGenerateColumns="False" BackColor="White" 
                BorderStyle="None" CellPadding="2" GridLines="None" Width="60%" 
                ShowHeader="False">
                <RowStyle ForeColor="#000066" />
                <Columns>
                    
                    <asp:TemplateField HeaderText="ชื่อเอกสาร">
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("title") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("title") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemStyle Width="70%" />
                    </asp:TemplateField>
               
                    <asp:TemplateField HeaderText="ดาวน์โหลด">
                        <ItemTemplate>
                            <asp:Label ID="lblLink" runat="server" Text=""></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="30%" />
                    </asp:TemplateField>
                   
                </Columns>
                <EmptyDataTemplate>
                    <table class="style5">
                        <tr>
                            <td align="center">
                                <asp:Label ID="Label8" runat="server" CssClass="GEmpty" Text="ไม่พบข้อมูล"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <FooterStyle BackColor="White" ForeColor="#000066" />
                <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
            </asp:GridView>
            <br>
            <br></br>
            <br></br>
            </br>
   
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

