<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frmUserLogin.aspx.vb" Inherits="ponweb.frmUserLogin" %>

<%@ Register assembly="MSCaptcha" namespace="MSCaptcha" tagprefix="cc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>LogIn Authentication...</title>
    <script language="javascript" type="text/javascript" src="hash/md5.js"></script>

   <script language="javascript" type="text/javascript">


        
        function pwdinhash() {
            var pwdinput, temppwd; 

            var tseed;
            
            
            pwdinput = document.getElementById('txtpwd');
            temppwd = pwdinput.value;


            tseed = document.getElementById('lSeed');
           
            
            if (temppwd.length != 0) {               
              
                pwdinput.value = hex_md5(hex_md5(hex_md5(pwdinput.value)) + tseed.value);               
                                              
            }
        }

 
        function Confirmation() {
            var win = window.close('', '_self');
            if (confirm("Are you sure you want to close the form?") == true)
                win.close();
            else
               return false;
        }       	 
</script>
    <style type="text/css">

        .style12
        {
            border: 1px dotted #F3E4E0;
            text-align: right;
            width: 289px;
        }
        .style23
        {
            border: 1px dotted #F3E4E0;
            text-align: left;
            width: 262px;
        }
        .style7
        {
            color: #FFFFFF;
            font-weight: bold;
            text-decoration: underline;
        }
        .style33
        {
            padding: 0;
            text-align: left;
            background-color: #F3E4E0;
            width: 147px;
        }
        .style34
        {
            border-style: dotted;
            border-width: 1px;
width: 48%;
            background-color: #663300;
        }
        .style35
        {
            border: 1px dotted #F3E4E0;
            text-align: center;
        }
        .style36
        {
            border: 1px dotted #F3E4E0;
            text-align: center;
        }
        .style37
        {
            color: #FFFF00;
            font-size: small;
            text-decoration: underline;
        }
        #txtpwd
        {
            width: 146px;
        }
        .style43
        {
            background-color: #FFFFFF;
        }
        .style44
        {
            color: #FFFFFF;
            text-align: center;
            background-color: #669999;
        }
        .style45
        {
            text-align: center;
            background-color: #3366CC;
        }
        .style46
        {
            border-bottom: 2px solid #800000;
            padding: 1px 4px;
            text-align: center;
        }
        .style48
        {
            color: #FFFFFF;
            font-size: large;
            font-family: "Courier New", Courier, monospace;
            background-color: #003300;
            height: 27px;
        }
        .style49
        {
            color: #FFFFFF;
        }
        .style50
        {
            border: 1px dotted #F3E4E0;
            text-align: left;
            width: 154px;
        }
        .style51
        {
            background-color: #663300;
        }
        .style52
        {
            height: 46px;
        }
        .style53
        {
            height: 47px;
            text-align: center;
        }
        .style54
        {
            border: 1px dotted #F3E4E0;
            text-align: center;
            background-color: #3366CC;
        }
        .style55
        {
            font-family: "Courier New", Courier, monospace;
            font-size: medium;
            color: #FFFFFF;
            text-decoration: underline;
        }
        </style>
</head>
<body onload="javascript:window.history.forward(-1)">
    <form id="form1" runat="server">
    <div class="style46">
    
        <asp:Image ID="Image1" runat="server" ImageUrl="~/images/head1.jpg" />
        <p class="style48">
                 <asp:Button ID="btnPP" runat="server" 
                    Height="19px" Text="B a c k   t o  P u b l i c    P a g e " 
                    Width="201px" BorderStyle="None" CssClass="style43" 
                     CausesValidation="False" />
                <span lang="en-us">&nbsp;&nbsp;&nbsp; </span>LogIn Screen for Authenticated User</p></div>
    
     <input id="go1" type="hidden" size="64" name="go1" runat="server"/>
    
     <input id="lSeed" type="hidden" size="64" name="Hidden4" runat="server"/>
    <div class="style52">
    
    </div>
   
    <div>
    <table class="style34" align="center">
        <tr>
            <td class="style54" colspan="2">
       
                <span class="style55" lang="en-us">Wel come </span></td>
            <td class="style35" rowspan="5">
       
                <asp:Image ID="Image2" runat="server" CssClass="style51" Height="104px" 
                    ImageUrl="~/images/LockIco.PNG" Width="117px" />
            </td>
        </tr>
        <tr>
            <td class="style12">
                <span lang="en-us" class="style49">LogIn Id</span></td>
            <td class="style23">
                <asp:TextBox ID="txtUserID" runat="server" MaxLength="20" Width="147px" 
                    TabIndex="1" BackColor="#CCCCFF"  autocomplete = "off" AutoCompleteType="Disabled"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style12">
                <span lang="en-us" class="style49">Password</span></td>
            <td class="style23">
                <input id="txtpwd" tabindex="2" class="style33" runat="server" type="password" autocomplete="off"/>
            </td>
        </tr>
        <tr>
             <%--<td class="style12">
          <cc2:CaptchaControl ID="ccjoin" runat="server" CaptchaBackgroundNoise="none" 
            CaptchaLength="5" CaptchaHeight="35" CaptchaWidth="120" CaptchaLineNoise="Medium" 
            CaptchaMinTimeout="10" CaptchaMaxTimeout="500" Height="33px" 
            NoiseColor="AliceBlue" Width="162px" BackColor="#FFCC99" BorderStyle="None" />
       
                </td>--%>
            <%-- <td class="style23">
       
      <asp:TextBox ID="txtCap" runat="server" Width="146px" Font-Size="Small" TabIndex="4" 
                    Font-Bold="False" onclick="javascript:this.value='';" ForeColor="#003300" 
                    style="font-weight: 700" autocomplete="off" AutoCompleteType="Disabled">Captcha Image Here</asp:TextBox>
            </td>--%>
        </tr>
        <tr>
            <td class="style12">
       
                &nbsp;</td>
            <td class="style50">
                <asp:Button ID="btnLogIn" runat="server" Height="19px" Text="L o g I n " 
                    Width="146px" BorderStyle="None" CssClass="style43" />
                </td>
        </tr>
        <tr>
        <%--<td colspan="3" class="style36">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ControlToValidate="txtCap" Display="Dynamic" ErrorMessage="Plz enter captcha" 
                    InitialValue="Captcha Image Here" style="text-align: center; color: #FFFFFF"></asp:RequiredFieldValidator>
            </td>--%>
        </tr>
        <tr>
            <td class="style35" colspan="3">
                <asp:Label ID="lberror" runat="server" CssClass="style7" ForeColor="White"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="3" class="style45">
                <span class="style37" lang="en-us">The application pages would perfectly be 
                rendered and would be viewed best in IE7 with 1280 by 960 pixels.</span></td>
        </tr>
        </table>
   </div>
   <div class="style53">
    
       </div>
   <div class="style44">
       For any discripancy and genuine information the Revenue/Registration Departments may be contacted.
       Designed and developed by<asp:Image ID="Image3" runat="server" Height="26px" 
           ImageUrl="~/images/nic_logo.jpg" Width="51px" />
   &nbsp;

   </div>
    
    </form>
</body>
</html>
