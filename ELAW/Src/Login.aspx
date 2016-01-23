<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Login.aspx.vb" Inherits="Src_Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
 <form id="form1" runat="server">
<head>
<title>Legal Affairs Bureau  -  Department of Land Transport</title>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

<link rel=stylesheet type=text/css href="../StyleSheet/css/login.css" />
<link rel=stylesheet type=text/css href="../StyleSheet/css/ie6.css" />
<link rel=stylesheet type=text/css href="../StyleSheet/css/ie7.css" />
<link rel=stylesheet type=text/css href="../StyleSheet/css/ie8.css" />
<script type="text/javascript" src="../StyleSheet/js/layout.js"></script></head>
 <script type="text/javascript">

     function clickButton(e, buttonid) {

         var evt = e ? e : window.event;

         var bt = document.getElementById(buttonid);

         if (bt) {

             if (evt.keyCode == 13) {

                 bt.click();

                 return false;

             }
         }
     }

    </script>


<body onLoad="P7_equalCols2(1,'left','div','right','div')">
<div id="content">
			<div id=header>
           	  <div class=logo><img src="../images/Menu/top_header.png" /></div>
			  <div class="mainMenu"><strong>ลงชื่อเข้าสู่ระบบ</strong></div>	
		      <div id="content2">
            
<table width="100%" border="0" height="393px" align="center" >
               <tr valign="top">
                 <td>	
          <div id="loginMain">
                          <div class="subtxtlogin"><strong> ชื่อผู้ใช้:</strong></div>
                          
                          <div class="sublogin">
                            
                              <asp:TextBox ID="txtUsername" runat="server" CssClass="textinput" MaxLength="16"></asp:TextBox>
                          </div>
                          
                          <div class="subtxtlogin"><strong>รหัสผ่าน:</strong></div>
                          
                          <div class="sublogin">
                           
                              <asp:TextBox ID="txtPassword" runat="server" CssClass="textinput" TextMode="Password" MaxLength="16"></asp:TextBox>
                          </div>
                           
 					 <div class="txtlogin">		
                       <div class="txtRemember">
                           <asp:CheckBox ID="ChkRememberMe" runat="server" Text="บันทึกการใช้งานของฉัน" 
                               Visible="False" />
                       </div>
                                    <div>
                                        <hr width="160px" align="left" />
                                    </div>
                            <div class="txtForgot">
                                        <a href="#"></a>                            </div>
              </div>          
                                  <div class="btnLogin">
                                  		<div class="txtbtnLogin"><strong><a href="#"><asp:LinkButton ID="lnkLogin" runat="server">เข้าสู่ระบบ</asp:LinkButton></a></strong></div>
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
        </body>
        </form> 
 </HTML>
