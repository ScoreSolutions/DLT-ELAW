<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPageProfile.master" AutoEventWireup="false" CodeFile="StatOfInterest.aspx.vb" Inherits="StatOfInterest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<center>
    <div>   
    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        <table style="width: 100%;">
           <tr>
                <td align="center" >
                    <h1>
                        สถิติความสนใจกฎหมายของประชาชน</h1>
                </td>
            </tr>
            <tr>
                <td align ="center"><b>กฏหมาย</b></td>
            </tr>
            <tr>
                <td align="center" valign="top" >
                            <asp:GridView ID="gdvStatLaw" runat="server" 
                                AutoGenerateColumns="False" CellPadding="4" CssClass="GridViewStyle" 
                                ForeColor="#333333" GridLines="None" Width="300px">
                                <RowStyle BackColor="#EFF3FB" />
                                <Columns>
                                    <asp:TemplateField HeaderText="ลำดับที่">
                                        <ItemTemplate>
                                             <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Law_Type" HeaderStyle-ForeColor="White" 
                                        HeaderText="หมวด" SortExpression="Law_Type">
                                        <HeaderStyle ForeColor="White" />
                                        <ItemStyle HorizontalAlign="Left" Width="60%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="stat" HeaderStyle-ForeColor="White" 
                                        HeaderText="จำนวน" SortExpression="stat">
                                        <HeaderStyle ForeColor="White" />
                                        <ItemStyle HorizontalAlign="Center" Width="20%" />
                                    </asp:BoundField>
                                </Columns>
                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                <EmptyDataTemplate>
                                    <table width="100%">
                                        <tr>
                                            <td align="center">
                                                <asp:Label ID="Label8" runat="server" CssClass="sslbl_red" Text="***ไม่พบข้อมูล***"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </EmptyDataTemplate>
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" 
                                    Height="30px" />
                                <EditRowStyle BackColor="#2461BF" />
                                <AlternatingRowStyle BackColor="White" />
                            </asp:GridView>
                            <asp:Label ID="lblLaw" runat="server" ForeColor="Red" Text="ไม่มีข้อมูล" Visible="False"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align ="center"><b>สัญญา</b></td>
            </tr>
            <tr>
                <td align="center" valign="top" >
                            <asp:GridView ID="gdvStatContact" runat="server" 
                                AutoGenerateColumns="False" CellPadding="4" CssClass="GridViewStyle" 
                                ForeColor="#333333" GridLines="None" Width="300px">
                                <RowStyle BackColor="#EFF3FB" />
                                <Columns>
                                    <asp:TemplateField HeaderText="ลำดับที่">
                                        <ItemTemplate>
                                             <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Law_Type" HeaderStyle-ForeColor="White" 
                                        HeaderText="หมวด" SortExpression="Law_Type">
                                        <HeaderStyle ForeColor="White" />
                                        <ItemStyle HorizontalAlign="Left" Width="60%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="stat" HeaderStyle-ForeColor="White" 
                                        HeaderText="จำนวน" SortExpression="stat">
                                        <HeaderStyle ForeColor="White" />
                                        <ItemStyle HorizontalAlign="Center" Width="20%" />
                                    </asp:BoundField>
                                </Columns>
                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                <EmptyDataTemplate>
                                    <table width="100%">
                                        <tr>
                                            <td align="center">
                                                <asp:Label ID="Label8" runat="server" CssClass="sslbl_red" Text="***ไม่พบข้อมูล***"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </EmptyDataTemplate>
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" 
                                    Height="30px" />
                                <EditRowStyle BackColor="#2461BF" />
                                <AlternatingRowStyle BackColor="White" />
                            </asp:GridView>
                            <asp:Label ID="lblCont" runat="server" ForeColor="Red" Text="ไม่มีข้อมูล" Visible="False"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align ="center"><b>คดี</b></td>
            </tr>
            <tr>
                <td align="center" valign="top" >
                            <asp:GridView ID="gdvStatCase" runat="server" 
                                AutoGenerateColumns="False" CellPadding="4" CssClass="GridViewStyle" 
                                ForeColor="#333333" GridLines="None" Width="300px">
                                <RowStyle BackColor="#EFF3FB" />
                                <Columns>
                                    <asp:TemplateField HeaderText="ลำดับที่">
                                        <ItemTemplate>
                                             <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Law_Type" HeaderStyle-ForeColor="White" 
                                        HeaderText="หมวด" SortExpression="Law_Type">
                                        <HeaderStyle ForeColor="White" />
                                        <ItemStyle HorizontalAlign="Left" Width="60%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="stat" HeaderStyle-ForeColor="White" 
                                        HeaderText="จำนวน" SortExpression="stat">
                                        <HeaderStyle ForeColor="White" />
                                        <ItemStyle HorizontalAlign="Center" Width="20%" />
                                    </asp:BoundField>
                                </Columns>
                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                <EmptyDataTemplate>
                                    <table width="100%">
                                        <tr>
                                            <td align="center">
                                                <asp:Label ID="Label8" runat="server" CssClass="sslbl_red" Text="***ไม่พบข้อมูล***"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </EmptyDataTemplate>
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" 
                                    Height="30px" />
                                <EditRowStyle BackColor="#2461BF" />
                                <AlternatingRowStyle BackColor="White" />
                            </asp:GridView>
                            <asp:Label ID="lblCase" runat="server" ForeColor="Red" Text="ไม่มีข้อมูล" Visible="False"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align ="center"><b>อื่นๆ</b></td>
            </tr>
            <tr>
                <td align="center" valign="top" >
                            <asp:GridView ID="gdvOther" runat="server" 
                                AutoGenerateColumns="False" CellPadding="4" CssClass="GridViewStyle" 
                                ForeColor="#333333" GridLines="None" Width="300px">
                                <RowStyle BackColor="#EFF3FB" />
                                <Columns>
                                    <asp:TemplateField HeaderText="ลำดับที่">
                                        <ItemTemplate>
                                             <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Law_Type" HeaderStyle-ForeColor="White" 
                                        HeaderText="หมวด" SortExpression="Law_Type">
                                        <HeaderStyle ForeColor="White" />
                                        <ItemStyle HorizontalAlign="Left" Width="60%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="stat" HeaderStyle-ForeColor="White" 
                                        HeaderText="จำนวน" SortExpression="stat">
                                        <HeaderStyle ForeColor="White" />
                                        <ItemStyle HorizontalAlign="Center" Width="20%" />
                                    </asp:BoundField>
                                </Columns>
                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                <EmptyDataTemplate>
                                    <table width="100%">
                                        <tr>
                                            <td align="center">
                                                <asp:Label ID="Label8" runat="server" CssClass="sslbl_red" Text="***ไม่พบข้อมูล***"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </EmptyDataTemplate>
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" 
                                    Height="30px" />
                                <EditRowStyle BackColor="#2461BF" />
                                <AlternatingRowStyle BackColor="White" />
                            </asp:GridView>
                            <asp:Label ID="lblOther" runat="server" ForeColor="Red" Text="ไม่มีข้อมูล" Visible="False"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="center" valign="top">
                    &nbsp;</td>
            </tr>
            <tr>
                <td align="center" >
                    <h1>
                        คำถามจากประชาชน</h1>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:GridView ID="gdvWQuestion" runat="server" AutoGenerateColumns="False" 
                        CellPadding="4" CssClass="GridViewStyle" ForeColor="#333333" GridLines="None" 
                        Width="100%" AllowPaging="True">
                        <RowStyle BackColor="#EFF3FB" />
                        <Columns>
                            <asp:TemplateField HeaderText="ลำดับที่" HeaderStyle-Width="50px">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                                <HeaderStyle Width="50px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="คำถาม" HeaderStyle-Width="400px">
                                <ItemTemplate>
                                    <table width="100%" cellpadding="2" cellspacing="2" >
                                        <tr>
                                            <td valign="top" align="left" >
                                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("Question")  %>'></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                                <HeaderStyle Width="400px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="รายละเอียด">
                                <ItemTemplate>
                                    <table width="100%" cellpadding="2" cellspacing="2" >
                                        <tr>
                                            <td valign="top" align="left">
                                            <asp:Label ID="lblCommentBy" runat="server" Text='<%# Bind("creation_by")  %>'></asp:Label>
                                            (<asp:Label ID="Label2" runat="server" Text='<%# Bind("Email")  %>'></asp:Label>)
                                            <br />
                                            <asp:Label ID="lblUpdateDate" runat="server" Text='<%#String.Format("{0:MM/dd/yyyy}", DataBinder.Eval(Container.DataItem, "created_date"))%>' Font-Size="10px" Font-Italic="true"></asp:Label> 
                                                <asp:Label ID="lbla" runat="server" Text="#" ForeColor="#cf6a12" Font-Size="10px" Font-Italic="true"></asp:Label>
                                            </td> 
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <EmptyDataTemplate>
                            <table width="100%">
                                <tr>
                                    <td align="center">
                                        <asp:Label ID="Label9" runat="server" CssClass="sslbl_red" 
                                            Text="***ไม่พบข้อมูล***"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" 
                            Height="30px" />
                        <EditRowStyle BackColor="#2461BF" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>
                    <asp:Label ID="lblQuestion" runat="server" ForeColor="Red" Text="ไม่มีข้อมูล" Visible="False"></asp:Label>
                </td>
            </tr>
            </table>
        </ContentTemplate>
        </asp:UpdatePanel>    
    </div>        
</center>
</asp:Content>


