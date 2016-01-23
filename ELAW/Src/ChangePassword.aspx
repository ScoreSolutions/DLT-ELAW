<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ChangePassword.aspx.vb" Inherits="Src_ChangePassword" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
<link rel=stylesheet type=text/css href="../StyleSheet/css/login.css" />
<link rel=stylesheet type=text/css href="../StyleSheet/css/ie6.css" />
<link rel=stylesheet type=text/css href="../StyleSheet/css/ie7.css" />
<link rel=stylesheet type=text/css href="../StyleSheet/css/ie8.css" />
<script type="text/javascript" src="../StyleSheet/js/layout.js"></script></head>
</head>
<body onLoad="P7_equalCols2(1,'left','div','right','div')">
<body>
    <form id="form1" runat="server">
 <div id="content">
			<div id=header>
           	  <div class=logo><img src="../images/Menu/top_header.png" /></div>
			  <div class="mainMenu"><strong>เปลี่ยนรหัสผ่าน :<asp:Label ID="lblUserName" 
                      runat="server"></asp:Label>
                  </strong></div>	
		      <div id="content2">
            
<table width="100%" border="0" height="393px" align="center" >
               <tr valign="top">
                 <td>	
          <div id="loginMain">
                          <div class="subtxtlogin"><strong> รหัสผ่าน:</strong></div>
                          
                          <div class="sublogin">
                            
                              <asp:TextBox ID="txtPassword" runat="server" CssClass="textinput" 
                                  MaxLength="16" TextMode="Password"></asp:TextBox>
                          </div>
                          
                          <div class="subtxtlogin"><strong>ยืนยันรหัสผ่าน:</strong></div>
                          
                          <div class="sublogin">
                           
                              <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="textinput" 
                                  TextMode="Password" MaxLength="16"></asp:TextBox>
                          </div>
                           
 					 <div class="txtlogin">		
                    
              </div>          
                                  <div class="btnLogin">
                                  		<div class="txtbtnLogin"><strong><a href="#"><asp:LinkButton ID="lnkLogin" runat="server">ตกลง</asp:LinkButton></a></strong></div>
                                  </div>
            </div>                 </td>
               </tr>
               <tr valign="top">
                 <td>&nbsp;</td>
               </tr>
             </table>                        
</div>
</div>
			<div id=footer> กรมการขนส่งทางบก 1032 ถนนพหลโยธิน แขวงจอมพล เขตจตุจักร กรุงเทพมหานคร 10900. โทรศัพท์ (หมายเลขกลาง) 0-2271-8888<br />
		    Call Center และศูนย์คุ้มครองผู้โดยสารสาธารณะ : โทรศัพท์ 1584 </div>
</div>
 </div>
 </div>

    </form>
</body>
</html>
