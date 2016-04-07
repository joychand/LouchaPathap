<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="IntHome.aspx.vb" Inherits="ponweb.IntHome1" %>



<%@ Register src="CtlPublic.ascx" tagname="CtlPublic" tagprefix="uc1" %>



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
    <form id="form1" runat="server" class="style24">
   <uc1:CtlPublic ID="CtlPublic1" runat="server" />
        
   </form>
    <table class="style10">
        <tr>
            <td class="12">
                <div style=" background-color:Aqua">
                <div style="text-align:center;">
                GIS Maps
                </div>
                <div class="more">
                GIS map  can be used for providing land maps to land holders. With proper customization GIS can be integrated with existing LR application LouchaPathap.<br />
&nbsp;Salient features : 
                    
                    •	Integration of spatial and textual data : Through Import option Shape file or adf file can be ported to PostGIS for linkage with textual data 
                    
                    •	Scale : Map display can be adjusted to desired scale 
                    
                    •	Distance between two points can be measured 
                    
                    •	Area calculation is automatic 
                    
                    •	Layers on Plot map : can be displayed and printed
                    
                    •	Plot division – can be done with the help of useful editing tools. Division by free hand or specifying area or drawing lines at an angle is possible. 
                    
                                        •	Plot division Size & shape can be adjusted<br />
                    •	Plot dimension display (distances between vertices) 
                    
                                        •	Alamats – Symbols on plots can be displayed and printed as well<br />
                    •	Grid - for help in drawing lines (Facility to rotate grid CW/ACW)<br />
                    •	Overlaying map on scanned image 
                    
                                        •	Print - Plot map along with adjoining plot maps and owner details can be printed to the desired scale<br />
                    •	Print – All Plots of an owner in multiple A4 sheets. Owner details printed on the last page. Desired layers can be highlighted and printed. 
                    
                    • Merging - of two polygons at a time                 </div>
                
                </div>
                <div class="maps">
                <span> As a pilot project, a digitized static cadastral map of 09-Nambol Bazar with plot data is being uploaded for the citizens <a href="maps.html"> Maps</a></span></div>
                </td>
            <td rowspan="6">
                <i>Online louchaPathap </i><span lang="en-us"><i>is introduced with an aim to 
                provide delivery of Jamabandi through CSC.
                <br />
                <br />
                Citizens including land owners, litigants and banking authorities&nbsp; to have a 
                quicker cross checks of their needed documents. More to be added ...</i></span></td>
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
