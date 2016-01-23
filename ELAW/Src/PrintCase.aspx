<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PrintCase.aspx.vb" Inherits="Src_PrintCase" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>พิมพ์ข้อมูลคดี</title>
    <style type="text/css">

table.form 
{
	/*background-color: #FFFFFF;*/
	width:100%;
    height: 36px;
}

table.form 
{
	width:100%;
}
.sslbl_black
{
    font: 12px Arial,"Courier New", Courier, monospace;
	color: #000000;
	font-weight :bold;
	text-decoration: none;
	vertical-align:top ;
	padding: 1px;
   	
}

.sslbl
{
    font: 12px Arial,"Courier New", Courier, monospace;
	color: #000000;
	font-weight :normal;
	text-decoration: none;
	vertical-align:top ;
	padding: 1px;
   	
}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
                        </asp:ScriptManager>
    <div>
    
              
                        
                        <table border="0" cellpadding="1" cellspacing="1" width ="700"">
                        
                            <tr>
                                <td class="sslbl_black" align="right" colspan="3">
                                    <asp:ImageButton ID="ImageButton1" runat="server" 
                                        ImageUrl="~/images/print.gif" />
                                </td>
                            </tr>
                            <tr>
                                <td align="center" class="sslbl_black" colspan="3">
                                    <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="Medium" 
                                        Text="ข้อมูลคดี"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" class="sslbl_black" colspan="3">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="sslbl_black">
                                    ประเภทคดี</td>
                                <td class="sslbl">
                                    <asp:Label ID="lblCaseType" runat="server" Text='<%# BindField("type_name") %>'></asp:Label>
                                    <asp:Label ID="lblId" runat="server" Text='<%# BindField("case_id") %>' 
                                        Visible="False"></asp:Label>
                                    <asp:Label ID="lblCaseNo" runat="server" Text='<%# BindField("case_no") %>' 
                                        Visible="False"></asp:Label>
                                    <asp:Label ID="lblIdNew" runat="server" Visible="False"></asp:Label>
                                </td>
                                <td class="sslbl" width="30%">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="sslbl_black">
                                    สถานะ</td>
                                <td class="sslbl">
                                    <asp:Label ID="lblStatus" runat="server" Text='<%# BindField("status_name") %>'></asp:Label>
                                    <asp:Label ID="lblStatusId" runat="server" Text='<%# BindField("status_id") %>' 
                                        Visible="False"></asp:Label>
                                </td>
                                <td class="sslbl" width="30%">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="sslbl_black">
                                    หมายเลขคดีดำ</td>
                                <td class="sslbl">
                                    <asp:Label ID="lblBlackNo" runat="server" Text='<%# BindField("black_no") %>'></asp:Label>
                                </td>
                                <td class="sslbl" width="30%">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="sslbl_black">
                                    หมายเลขคดีแดง</td>
                                <td class="sslbl">
                                    <asp:Label ID="lblRedNo" runat="server" Text='<%# BindField("red_no") %>'></asp:Label>
                                </td>
                                <td class="sslbl" width="30%">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="sslbl_black">
                                    สำนักงานอัยการ</td>
                                <td class="sslbl">
                                    <asp:Label ID="lblCourt" runat="server" Text='<%# BindField("court_name") %>'></asp:Label>
                                </td>
                                <td class="sslbl" width="30%">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="sslbl_black">
                                    ชื่อพนักงานอัยการ</td>
                                <td class="sslbl">
                                    <asp:Label ID="lblAttornney" runat="server" 
                                        Text='<%# BindField("attorney_name") %>'></asp:Label>
                                    &nbsp;<asp:Label ID="lbl" runat="server" Font-Bold="True" Text="โทร. "></asp:Label>
                                    &nbsp;<asp:Label ID="lblTel" runat="server" Text='<%# BindField("tel") %>'></asp:Label>
                                </td>
                                <td class="sslbl" width="30%">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="sslbl_black">
                                    ปิดหมาย/รับหมาย</td>
                                <td class="sslbl">
                                    <asp:Label ID="lblCloseRecieve" runat="server"></asp:Label>
                                </td>
                                <td class="sslbl" width="30%">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="sslbl_black">
                                    วันที่</td>
                                <td class="sslbl">
                                    <asp:Label ID="lblRecieveDate" runat="server" 
                                        Text='<%# BindField("recieve_date") %>'></asp:Label>
                                </td>
                                <td class="sslbl" width="30%">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="sslbl_black">
                                    โจทก์</td>
                                <td class="sslbl">
                                    <asp:Label ID="lblDefandent" runat="server" 
                                        Text='<%# BindField("defendant") %>'></asp:Label>
                                </td>
                                <td class="sslbl" width="30%">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="sslbl_black">
                                    โจทก์ร่วม</td>
                                <td class="sslbl">
                                    <asp:Label ID="lblDefandent1" runat="server" 
                                        Text='<%# BindField("defendant1") %>'></asp:Label>
                                </td>
                                <td class="sslbl" width="30%">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="sslbl_black">
                                    จำเลย</td>
                                <td class="sslbl">
                                    <asp:Label ID="lblProsecutor" runat="server" 
                                        Text='<%# BindField("prosecutor") %>'></asp:Label>
                                </td>
                                <td class="sslbl" width="30%">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="sslbl_black">
                                    จำเลยร่วม</td>
                                <td class="sslbl">
                                    <asp:Label ID="lblProsecutor1" runat="server" 
                                        Text='<%# BindField("prosecutor1") %>'></asp:Label>
                                </td>
                                <td class="sslbl" width="30%">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="sslbl_black">
                                    หัวหน้ากลุ่มงานนิติกรรมและคดี</td>
                                <td class="sslbl">
                                    <asp:Label ID="lblApp1" runat="server" 
                                        Text='<%# BindField("appname1") %>'></asp:Label>
                                </td>
                                <td class="sslbl" width="30%">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="sslbl_black">
                                    ผู้อำนวยการ</td>
                                <td class="sslbl">
                                    <asp:Label ID="lblApp2" runat="server" Text='<%# BindField("appname2") %>'></asp:Label>
                                </td>
                                <td class="sslbl" width="30%">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="sslbl_black">
                                    คำค้นหา</td>
                                <td class="sslbl">
                                    <asp:Label ID="lblKeyword" runat="server" Text='<%# BindField("keyword") %>'></asp:Label>
                                </td>
                                <td class="sslbl" width="30%">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="sslbl_black">
                                    รายละเอียด</td>
                                <td class="sslbl">
                                    <asp:Label ID="lblDetail" runat="server" Text='<%# BindField("detail") %>'></asp:Label>
                                </td>
                                <td class="sslbl" width="30%">
                                    &nbsp;</td>
                            </tr>
                        </table>
                    


    </div>
    </form>
</body>
</html>
