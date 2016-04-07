<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="~/users/ChgPwd.aspx.vb" Inherits="ponweb.ChgPwd" %>
<%@ Register assembly="MSCaptcha" namespace="MSCaptcha" tagprefix="cc3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<meta http-equiv="X-UA-Compatible" content="IE=9" />
    <title>LouchaPathap</title>
    <link rel="shortcut icon" href="~/favicon.ico" type="image/icon"/> <link rel="icon" href="~/favicon.ico" type="image/icon" />
       <script language="javascript" type="text/javascript" src="../users/hash/md5.js"></script>

   <script language="javascript" type="text/javascript">
         //Disable right mouse click Script
       var message = "Function Disabled!";

       ///////////////////////////////////
       function clickIE4() {
           if (event.button == 2) {
               alert(message);
               return false;
           }
       }

       function clickNS4(e) {
           if (document.layers || document.getElementById && !document.all) {
               if (e.which == 2 || e.which == 3) {
                   alert(message);
                   return false;
               }
           }
       }

       if (document.layers) {
           document.captureEvents(Event.MOUSEDOWN);
           document.onmousedown = clickNS4;
       }
       else if (document.all && !document.getElementById) {
           document.onmousedown = clickIE4;
       }

       document.oncontextmenu = new Function("alert(message);return false")
        
        
        function pwdinhash2() {
            var pwdinput,newpwd,confpwd,holdpwd,pattern; 
            var tseed;    
            pattern = /^(?=.*\d)(?=.*[A-Z]).(?=.*[@#$%]).{8,15}$/
            oldpwd = document.getElementById('oldpwd');
            newpwd = document.getElementById('txtnewpwd');
            confpwd = document.getElementById('txtconfmpwd');
            temppwd = oldpwd.value;
            
            tseed = document.getElementById('clseed');          
           
            if (temppwd.length != 0) {                
              
                document.getElementById("oldpwd").value = hex_md5(hex_md5(hex_md5(oldpwd.value)) + tseed.value);
                     
              }
             if ( newpwd.value.search(pattern)!=-1) {
                newpwd.value= hex_md5(hex_md5(newpwd.value));
                
                document.getElementById('txtnewpwd').value = newpwd.value + hex_md5(hex_md5(newpwd.value + tseed.value));
                
              }
              else
              {
               alert(' New password string wrong'+ '\n'+  '\n' + ' atleast 1 uppercase character' + '\n' + ' atleast 1 digit' + '\n' + ' atleast one of the special character [@#%$]' + 'atleast 8 and max 15 characters' );
               return false;
              }
             if (confpwd.value.search(pattern)!=-1)
              {
                confpwd.value= hex_md5(hex_md5(confpwd.value));
                document.getElementById('txtconfmpwd').value = confpwd.value + hex_md5(hex_md5(confpwd.value + tseed.value));
              
               }
              else
              {
                alert('Confirm password string wrong'+ '\n'+  '\n' + ' atleast 1 uppercase character' + '\n' + 'atleast 1 digit' + '\n' + 'atleast one of the special character [@#%$]' + 'atleast 10 and max 15 characters' );
                
                return false;
              } 
              return true;  
        } 
       function CheckJSEnabled()

{
document.getElementById("content").style.visibility = 'visible';
document.getElementById("chgpwd").style.visibility = 'visible';
document.getElementById("jsdisable").style.visibility = 'hidden';

}
</script>
    <style type="text/css">
        .style1
        {
            width: 100%;
            visibility: hidden;
        }
        .style2
        {
            width: 115px;
            text-align: right;
        }
        .style3
        {
            width: 290px;
        }
        #jsdisable
        {
            text-align: center;
            font-size: large;
            font-weight: 700;
            color: #FF3300;
        }
        .style4
        {
            color: #FF0000;
        }
        .style5
        {
            width: 290px;
            text-align: right;
        }
        .style14
        {
            text-align: left;
        }
        </style>
</head>

<body onload="CheckJSEnabled()">
    <form id="form1" runat="server" autocomplete="false">
    <div id = "container" style="height: 312px; width: 1094px; text-align: left; margin:0 auto;">
        <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
        <br />
        <br />
        <div id = "jsdisable" style="margin:0 auto;" >
                   Please enable Javascript or use javascript support Browser to use this feature
        </div>
    <div id="content" style=  "text-align: center" "height: 100%; float: right; visibility:visible ">
        <table class="style1" id = "chgpwd">
            <tr>
                <td class="style3">
                    &nbsp;</td>
                <td class="style2">
                    Old Password</td>
                <td style="text-align: left">
                    <asp:TextBox ID="oldpwd" runat="server" Width="149px" TextMode="Password" AutoCompleteType="Disabled"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style3">
                    &nbsp;</td>
                <td class="style2">
                    New Password</td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtnewpwd" runat="server" Width="148px" textmode="Password" AutoCompleteType="Disabled"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style5">
                    &nbsp;</td>
                <td class="style2">
                    Confirm Password</td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtconfmpwd" runat="server" Width="148px" textmode="Password" 
                        AutoCompleteType="Disabled" style="margin-left: 0px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style3">
                    &nbsp;</td>
<%--                <td class="style2">
                     <cc3:CaptchaControl ID="ccjoin" runat="server" CaptchaBackgroundNoise="none" 
            CaptchaLength="5" CaptchaHeight="35" CaptchaWidth="100" CaptchaLineNoise="Medium" 
            CaptchaMinTimeout="10" CaptchaMaxTimeout="500" Height="33px" 
            NoiseColor="AliceBlue" Width="120px" BackColor="#FFCC99" BorderStyle="None" /></td>
                <td style="text-align: left">
                     <asp:TextBox ID="txtCap" runat="server" Width="146px" Font-Size="Small" TabIndex="4" 
                    Font-Bold="False" onclick="javascript:this.value='';" ForeColor="#003300" 
                    style="font-weight: 700" autocomplete="off" AutoCompleteType="Disabled">Captcha Image Here</asp:TextBox>
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                         ControlToValidate="txtCap" Display="Dynamic" ErrorMessage="Plz enter Captcha" 
                         InitialValue="Captcha Image Here" style="font-size: small"></asp:RequiredFieldValidator>
                        </td>--%>
                <asp:HiddenField ID="clseed" runat="server" />
            </tr>
            <tr>
                <td class="style3">
                    &nbsp;</td>
                <td class="style2">
                    &nbsp;</td>
                        <td style="text-align: left">
                            <asp:Button ID="Button1" runat="server" Text="Submit" />
                        </td>
                    </tr>
            <tr>
                <td class="style3">
                    &nbsp;</td>
                <td class="style2">
                    &nbsp;</td>
                        <td style="text-align: left">
                            <asp:Label ID="lblerror" runat="server"></asp:Label>
                        </td>
                    </tr>
            <tr>
                <td class="style3">
                    &nbsp;</td>
                <td class="style2">
                    &nbsp;</td>
                        <td style="text-align: left; color: #003399;">
                            Password string must contain:</td>
                    </tr>
            <tr>
                <td class="style3">
                    &nbsp;</td>
                <td class="style2">
                    &nbsp;</td>
                        <td style="text-align: left; color: #0033CC;">
                            1. atleast one digit</td>
                    </tr>
            <tr>
                <td class="style3">
                    &nbsp;</td>
                <td class="style2">
                    &nbsp;</td>
                        <td style="text-align: left; color: #0033CC;">
                            2. atleast one uppercase character</td>
                    </tr>
            <tr>
                <td class="style3">
                    &nbsp;</td>
                <td class="style2">
                    &nbsp;</td>
                        <td style="text-align: left; color: #0033CC;">
                            3. atleast one of th special characters [<span class="style4">@#%$</span>]</td>
                    </tr>
            <tr>
                <td class="style3">
                    &nbsp;</td>
                <td class="style2">
                    &nbsp;</td>
                        <td style="text-align: left; color: #0033CC;">
                            4. atleast min 8 characters atleast min 8 characters max 15 characters</td>
                    </tr>
                    
                    
                    
                </table>
        <div class="style14">
                 <p class="MsoNormal" 
                    
                    
            style="margin-bottom: 0in; margin-bottom: .0001pt; line-height: normal; font-size: medium; text-align: center; width: 999px; color: #FFFFFF; background-color: #669999;">
                    <span lang="en-us"><span class="style32">For any discripancy the concern 
                    Revenue/Registration Departments may be contacted. Designed and developed by
                    </span></span>
                    <asp:Image ID="Image3" runat="server" Height="26px" 
                        ImageUrl="~/images/nic_logo.jpg" Width="51px" />
                </p>
        <br />
      <tr>
                    
                 <td>
                 </div>
        </td>
                    </tr>
   </div>
  
    </div>
    
    </form>
    
</body>
</html>
