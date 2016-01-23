<%@ Page Title="ค้นหาเอกสาร-LAW1203" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false"  CodeFile="ImportSearch.aspx.vb" Inherits="Src_ImportSearch" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   
    <script type="text/javascript">
        function navAway(url) {
            window.open("../Document_Import/" + url);
        }
    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        <table class="form" width="100%" border="0"  >
               <tr valign="top">
                 <td>	
                 		<!--content -->
                 		
                            <div class="HeaderGreen">
                                <asp:LinkButton ID="linkHome" runat="server">Home</asp:LinkButton>
                                &nbsp;&gt;
                                <asp:LinkButton ID="link1" runat="server">บริการข้อมูลกฎหมาย</asp:LinkButton>
                                &nbsp;&gt;
                                <asp:LinkButton ID="link2" runat="server">ค้นหาเอกสาร</asp:LinkButton>
                           </div>
                           	
                      
                        <!--end content -->
                 </td>
               </tr>
             </table>
            <table border="0" cellpadding="1" cellspacing="1" class="form">
             
                <tr>
                    <td class="sslbl_right">
                        ประเภทเอกสาร</td>
                    <td>
                        <asp:DropDownList ID="DDType" runat="server" CssClass="ssddl" Width="450px" 
                AutoPostBack="True">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="sslbl_right">
                        ชนิดเอกสาร</td>
                    <td>
                        <asp:DropDownList ID="DDLawType" runat="server" CssClass="ssddl" Width="450px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="sslbl_right">
                        รหัสเอกสาร</td>
                    <td>
                        <asp:TextBox ID="txtDocId" runat="server" 
                            CssClass="ssddl" MaxLength="15" 
                                                Width="100px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="sslbl_right">
                        คำค้นหา</td>
                    <td>
                        <asp:TextBox ID="txtDocName" runat="server" CssClass="ssddl" Width="600px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="sslbl">&nbsp;
                        </td>
                    <td>
                        <asp:Button ID="bSave" runat="server" CssClass="ssbtn" Text="ค้นหา" 
                    Width="80px" />
                        <asp:TextBox ID="txtKeyword" runat="server" CssClass="ssddl" Width="600px" Visible ="false" ></asp:TextBox>
                    </td>
                </tr>
            </table>
            <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
                AllowSorting="True" AutoGenerateColumns="False" BackColor="White" 
                BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="1" 
                CssClass="GridViewStyle" GridLines="Vertical" Width="100%">
                <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                <Columns>
                    <asp:BoundField DataField="doc_id" HeaderText="รหัสเอกสาร">
                    <ItemStyle Width="10%" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="ประเภทเอกสาร">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("doc_type") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label3" runat="server" Text='<%# Bind("doc_type") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="12%" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="doc_subtype" HeaderText="ชนิดเอกสาร">
                    <ItemStyle Width="12%" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="ชื่อเอกสาร">
                        <ItemTemplate>
                            <asp:Label ID="lblLinkFile" runat="server" Text=""></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="25%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="คำค้นหา">
                        <ItemTemplate>
                            <asp:Label ID="lblLink" runat="server" Text=""></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="30%" />
                    </asp:TemplateField>
                </Columns>
                <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
              <EmptyDataTemplate>
                    <table width ="100%">
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

