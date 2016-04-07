<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="IntHome.aspx.vb" Inherits="ponweb.IntHome" %>



<%@ Register src="CtlPublic.ascx" tagname="CtlPublic" tagprefix="uc1" %>



<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >

<head runat="server">
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
   
</head>
   
<body>
    <form id="form1" runat="server" class="style24">
   <uc1:CtlPublic ID="CtlPublic1" runat="server" />
   
          
                    
                    
                    
                    </form>
    <table class="style10">
        <tr>
             <td rowspan="12" style="width:50%">
                 <div >
                 <div style="text-align:center">
                 <span style="background-color:Lime"> </span>
                 
                 </div>
                 <img src="images/nambol.png" alt="" height="380px" width="500px"/>
                 </div>
               </td>
            <td rowspan="6" style="vertical-align:middle; color:#0066FF; font-size:large;width:50%; word-wrap:break-word">
            <div style=" width:50%; height:100%">
                <div style="text-align:center;">
                <div class="more">
                <i>Online louchaPathap </i><span lang="en-us"><i>is introduced with an aim to 
                provide delivery of Jamabandi through CSC.
                <br />
                
                Citizens including land owners, litigants and banking authorities&nbsp;&nbsp;to have a 
                quicker cross checks of their needed documents More to be added ...</i></span>
                </div>
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
