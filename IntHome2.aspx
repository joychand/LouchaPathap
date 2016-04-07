<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="IntHome2.aspx.vb" Inherits="ponweb.IntHome2" %>



<%@ Register src="CtlPublic1.ascx" tagname="CtlPublic1" tagprefix="uc1" %>



<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >

<head id="Head1" runat="server">
    <title></title>
    <meta http-equiv="X-UA-Compatible" content="IE=9" />
    <link rel="shortcut icon" href="favicon.ico" type="image/icon"/> <link rel="icon" href="favicon.ico" type="image/icon" />
    <style type="text/css">
        p.MsoNormal
	{margin-top:0in;
	margin-right:0in;
	margin-bottom:10.0pt;
	margin-left:0in;
	line-height:115%;
	font-size:11.0pt;
	font-family:"Calibri","sans-serif";
	}
        .style11
        {
            width: 596px;
        }
        .style24
        {
            height: 100%;
        }
        a {
    color: #0254EB
}
.more {
width: 400px;
background-color: #f0f0f0;
margin: 10px;
}
.morecontent span {
display: none;
}
.maps
{
width: 400px;
background-color: #f0f0f0;
margin: 10px;	
}
   </style>
                        
        
  
     <script type="text/javascript"
    src="http://ajax.microsoft.com/ajax/jquery/jquery-1.4.4.min.js">
    </script>
    
  <script type="text/javascript">
$(function() {
var showChar = 120, showtxt = "more", hidetxt = "less";
$('.more').each(function() {
var content = $(this).text();
if (content.length > showChar) {
var con = content.substr(0, showChar);
var hcon = content.substr(showChar, content.length - showChar);
var txt= con +  '<span class="dots">...</span><span class="morecontent"><span>' + hcon + '</span>&nbsp;&nbsp;<a href="" class="moretxt">' + showtxt + '</a></span>';
$(this).html(txt);
}
});
$(".moretxt").click(function() {
if ($(this).hasClass("sample")) {
$(this).removeClass("sample");
$(this).text(showtxt);
} else {
$(this).addClass("sample");
$(this).text(hidetxt);
}
$(this).parent().prev().toggle();
$(this).prev().toggle();
return false;
});
});
</script>
<script type="text/javascript">
function redirect() {
  location.href = 'maps.html';
}
</script>
    
</head>
   
<body>
    
    <table class="style10">
   
        <tr>
        <td>
        <div style="width:200px!important;">
        <form id="form1" runat="server" class="style24">
       <uc1:CtlPublic1 ID="CtlPublic1" runat="server" />
   
          
                    
                    
                    
                    </form>
        </div>
        
        </td>
           
            <td rowspan="6" style="text-align:left">
            <br />
             <br />
             <br />
             
                <i>Online louchaPathap </i><span lang="en-us" style=""><i>is introduced with an aim to 
                provide delivery of Jamabandi through CSC.
                <br />
                <br />
                Citizens including land owners, litigants and banking authorities&nbsp; to have a 
                quicker cross checks of their needed documents. More to be added ...</i></span></td>
                 <td rowspan="6" style="width:50%">
                 <br />
                 <br />
                 <br />
                 <div style=" width:50%; text-align:left">
                 
                 
                 <img src="images/nambol.png" height="350px" width="400px" alt=""/>
                 <div style="text-align:center">
                 <span style="background-color:Lime"><i> 9-Nambol Bazar</i> </span>
                 
                 </div>
                 </div>
               </td>
        </tr>
        <%--<tr>
            <td class="style76">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style76">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style76">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style76">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style76">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style76">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style76">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style76">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>--%>
        </table>
        <table class="style10">
          <tr>
            <td colspan="2">
                <p class="MsoNormal" 
                    
                    
            style="margin-bottom: 0in; margin-bottom: .0001pt; line-height: normal; font-size: medium; text-align: center; width: 100%; color: #FFFFFF; background-color: #669999;">
                    <span lang="en-us"><span class="style32">For any discripancy the concern 
                    Revenue/Registration Departments may be contacted. Designed and developed by
                    </span></span>
                    <asp:Image ID="Image3" runat="server" Height="26px" 
                        ImageUrl="~/images/nic_logo.jpg" Width="51px" />
                </p></td>
        </tr>
        </table>
      
        

   
        
        
                    
                    
                    
                    <%--<asp:ScriptManager ID="ScriptManager1" runat="server" />
 
                    <asp:UpdatePanel ID="up1" runat="server">
                    <%--<Triggers>
                        <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                    </Triggers>
                    <ContentTemplate>
                       
                        <%--<asp:Literal ID="Literal1" runat="server"></asp:Literal>
                        <asp:AdRotator ID="AdRotator2" runat="server" 
                            AdvertisementFile="~/rotateimages.xml" 
                            Target="_blank" Height="250px" Width="400px" CssClass="style15" />
                       
                    </ContentTemplate>
                    </asp:UpdatePanel>   --%>
                    
               
               
   
   
       
   <%--</form>--%>
  
   </body>
</html>
