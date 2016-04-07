<%@ Page Language="vb" AutoEventWireup="false" CodeBehind=""~/users/_CSC/frmLogTrl_csc.aspx.vb" Inherits="ponweb.frmLogTrl_csc" EnableViewStateMac="true" %>

<%@ Register src="CtlUserCSC.ascx" tagname="CtlUserCSC" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <meta http-equiv="X-UA-Compatible" content="IE=9" />
    <link rel="shortcut icon" href="~/favicon.ico" type="image/icon"/> <link rel="icon" href="~/favicon.ico" type="image/icon" />
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
        
        
    </script>
    <style type="text/css">

        .style42
        {
            font-size: medium;
        }
        .style43
        {
            text-align: left;
        }
        p.MsoNormal
	{margin-top:0in;
	margin-right:0in;
	margin-bottom:10.0pt;
	margin-left:0in;
	line-height:115%;
	font-size:11.0pt;
	font-family:"Calibri","sans-serif";
            width: 622px;
        }
                        
        .style44
        {
            width: 100%;
        }
        .style45
        {
            width: 62px;
        }
                        
        </style>
</head>
<body>
    <form id="frmLogTrl_csc" runat="server" autocomplete="false">
  <div class="style43">
    
        <table class="style44">
            <tr>
                <td class="style45">
                    &nbsp;</td>
                <td>
    
        <uc1:CtlUserCSC ID="CtlUserCSC1" runat="server" />
    
                </td>
            </tr>
            <tr>
                <td class="style45">
                    &nbsp;</td>
                <td>
    
        <asp:Panel ID="Panel1" runat="server" BackColor="#FFFFCC" BorderStyle="None" 
            Font-Size="Medium" Height="186px" HorizontalAlign="Center" Width="914px">
            <span class="style43">
            <br />
            : My last Activities :</span><br />
            Previous visit:
            <asp:Label ID="lbLastVisit" runat="server" ForeColor="#0000CC"></asp:Label>
            &nbsp;
            <br />
            IP of the client:
            <asp:Label ID="lbClientIPVisit" runat="server" ForeColor="#000066"></asp:Label>
            <br />
            <br />
            Last Failed LogIn Attempt:
            <asp:Label ID="lbLastFailVisit" runat="server" ForeColor="#990033"></asp:Label>
            &nbsp;&nbsp;&nbsp;
            <br />
            IP of the client:
            <asp:Label ID="lbClientIPFailVisit" runat="server" ForeColor="#993333"></asp:Label>
            <br />
            <br />
            <span class="style42">
            <asp:Button ID="btnClosePanel" runat="server" BorderStyle="None" 
                Font-Underline="False" ForeColor="#336600" 
                style="font-weight: 700; height: 22px;" Text="C l o s e" />
            </span>
        </asp:Panel>
                </td>
            </tr>
            <tr>
                <td class="style45">
                    &nbsp;</td>
                <td>
        <span class="style42">
        
        <p class="MsoNormal" 
                    
                    
            style="margin-bottom: 0in; margin-bottom: .0001pt; line-height: normal; font-size: medium; text-align: center; width: 999px; color: #FFFFFF; background-color: #669999;">
                    <span lang="en-us"><span class="style32">For any discripancy the concern 
                    Revenue/Registration Departments may be contacted. Designed and developed by
                    </span></span>
                    <asp:Image ID="Image3" runat="server" Height="26px" 
                        ImageUrl="~/images/nic_logo.jpg" Width="51px" />
                </p>
       
        </span>
        
                   
                    
                </td>
            </tr>
        </table>
        <br />
        <span class="style42">
        
        <span lang="en-us">&nbsp; </span>
        <br />
        <br />
       
        </span>
        
                   
                    
    </div>
   
    </form>
    
</body>
</html>
