<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="~/users/_CSC/Jama123.aspx.vb" Inherits="ponweb.Jama123" EnableViewStateMac="true" %>


<%@ Register src="CtlUserCSC.ascx" tagname="CtlUserCSC" tagprefix="uc2" %>

<!DOCTYPE html>
<html lang="en">
<meta charset="utf-8">

<head id="Head1" runat="server">
<title>Loucha Pathap manipur.. Jamabandi print</title>   

<script type = "text/javascript">
 function SetTarget() {
     document.forms[0].target = "_blank";
 }
 

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

        .style65
        {
            text-decoration: no 
            font-family: "Courier New", Courier, monospace;
        }
        .style93
        {
            margin-left: 0px;
        }
        .style42
        {
            color: #663300;
            text-decoration: no 
            background-color: #CCFF99;
        }
        .style106
        {
            height: 16px;
            text-align: left;
            background-color: White;
        }
        .style32
        {
            background-color: #669999;
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
                        
        .style126
        {
            padding: 1px 4px;
            height: 30px;
            text-align: right;
            width: 468px;
            border-bottom-style: solid;
            border-bottom-width: 1px;
        }
        .style127
        {
            text-align: right;
            width: 468px;
        }
        .style129
        {
            text-align: left;
            width: 606px;
        }
        .style133
        {
            padding: 1px 4px;
            height: 30px;
            text-align: right;
            border-bottom-style: solid;
            border-bottom-width: 1px;
        }
        .style134
        {
            text-align: left;
            width: 293px;
        }
        .style138
        {
            text-align: right;
            }
        .style139
        {
            padding: 1px 4px;
            height: 30px;
            text-align: left;
            border-bottom-style: solid;
            border-bottom-width: 1px;
            width: 293px;
        }
        .style141
        {
            padding: 1px 4px;
            height: 30px;
            text-align: left;
            width: 606px;
            border-bottom-style: solid;
            border-bottom-width: 1px;
        }
        .style143
        {
            color: #003300;
            font-family: "Courier New", Courier, monospace;
            font-weight: bold;
        }
        .style145
        {
            text-align: left;
            width: 175px;
        }
        .style146
        {
            padding: 1px 4px;
            height: 30px;
            text-align: left;
            border-bottom-style: solid;
            border-bottom-width: 1px;
            width: 175px;
        }
        </style>
</head>
<%--<script type="text/javascript" src="http://code.jquery.com/jquery-1.7.2.js"></script>--%>
<body>
    <form id="Jama123" runat="server" autocomplete="off" align="centre">
         <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server"> 
        <ContentTemplate>  
                            <table align="center" width="80%">
                                <tr>
                                    <td colspan="6">
       
                            <uc2:CtlUserCSC ID="CtlUserCSC1" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        District</td>
                                    <td>
                                        <span lang="en-us"><span class="style65" 
                                            style="text-decoration:   font-size: large;">
                                        <asp:DropDownList ID="lDistrict" runat="server" AutoPostBack="True" 
                                            Font-Size="Medium" Height="27px" TabIndex="6" Width="165px">
                                        </asp:DropDownList>
                                        <br />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                            ControlToValidate="lDistrict" Display="None" ErrorMessage="District" 
                                            InitialValue="Select District" ValidationGroup="jama"></asp:RequiredFieldValidator>
                                        <br />
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
                                            ControlToValidate="lDistrict" Display="None" ErrorMessage="Invalid District" 
                                            ValidationExpression="^[0-9]{1,2}$" ValidationGroup="jama"></asp:RegularExpressionValidator>
                                        </span></span></td>
                                    <td>
                                        Circle</td>
                                    <td>
                                        <span lang="en-us"><span class="style65" 
                                            style="text-decoration:   font-size: large;">
                                        <asp:DropDownList ID="lCircle" runat="server" AutoPostBack="True" 
                                            Font-Size="Medium" Height="27px" TabIndex="6" Width="165px">
                                        </asp:DropDownList>
                                        <br />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                                            ControlToValidate="lCircle" Display="None" ErrorMessage="Circle" 
                                            InitialValue="Select Circle" ValidationGroup="jama"></asp:RequiredFieldValidator>
                                        <br />
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" 
                                            ControlToValidate="lCircle" Display="None" ErrorMessage="Invalid Circle" 
                                            ValidationExpression="^[0-9]{1,5}$" ValidationGroup="jama"></asp:RegularExpressionValidator>
                                        </span></span></td>
                                    <td>
                                        Village</td>
                                    <td>
                                        <span lang="en-us"><span class="style65" 
                                            style="text-decoration:   font-size: large;">
                                        <asp:DropDownList ID="lVillage" runat="server" AutoPostBack="True" 
                                            CssClass="style93" Font-Size="Medium" Height="26px" TabIndex="6" Width="227px">
                                        </asp:DropDownList>
                                        <br />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                            ControlToValidate="lCircle" Display="None" ErrorMessage="Village" 
                                            InitialValue="Select Village" ValidationGroup="jama"></asp:RequiredFieldValidator>
                                        <br />
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" 
                                            ControlToValidate="lVillage" Display="None" ErrorMessage="Invalid village" 
                                            ValidationExpression="^[0-9]{1,10}$" ValidationGroup="jama"></asp:RegularExpressionValidator>
                                        </span></span></td>
                                </tr>
                                <tr>
                                    <td>
                                        <span lang="en-us">New PattaNo</span></td>
                                    <td>
                                        <span lang="en-us">
                                        <asp:TextBox ID="txtPattaNo" runat="server" autocomplete="off" 
                                            AutoCompleteType="Disabled" Font-Size="Medium" Height="20px" MaxLength="6" 
                                            TabIndex="7" Width="97px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                            ControlToValidate="txtPattaNo" Display="Dynamic" ErrorMessage="New pattano" 
                                            ValidationGroup="jama">*</asp:RequiredFieldValidator>
                                        <br />
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                                            ControlToValidate="txtPattaNo" Display="None" ErrorMessage="Invalid Patta No" 
                                            style="font-size: x-small; font-style: italic" 
                                            ValidationExpression="^[0-9]{1,6}$" ValidationGroup="jama"></asp:RegularExpressionValidator>
                                        </span></td>
                                    <td>
                                        <span lang="en-us">New DagNo</span></td>
                                    <td>
                                        <span lang="en-us">
                                        <asp:TextBox ID="txtDagNo" runat="server" autocomplete="off" 
                                            AutoCompleteType="Disabled" Font-Size="Medium" Height="20px" MaxLength="6" 
                                            TabIndex="7" Width="97px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="redag" runat="server" 
                                            ControlToValidate="txtDagNo" Display="Dynamic" ErrorMessage="New DagNo" 
                                            ValidationGroup="jama">*</asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revDag" runat="server" 
                                            ControlToValidate="txtDagNo" Display="None" ErrorMessage="Invalid Dag No" 
                                            style="font-size: x-small; font-style: italic" 
                                            ValidationExpression="^[0-9]{1,6}$" ValidationGroup="jama"></asp:RegularExpressionValidator>
                                        </span></td>
                                    <td>
                                        <span lang="en-us">
                                        <asp:Button ID="btnHtmlReport" runat="server" CssClass="style143" 
                                            Text="JAMABANDI" Width="89px" />
                                        </span></td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                    <td colspan="5">
                                        <asp:Label ID="lberror" runat="server" CssClass="style42"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6">
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                                            BackColor="#669999" HeaderText="Plz select/correct:" Height="30px" 
                                            style="font-size: small; font-style: italic" ValidationGroup="jama" 
                                            Width="100%" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6">
                                        <p class="MsoNormal" 
                                            style="margin-bottom: 0in; margin-bottom: .0001pt; line-height: normal; font-size: medium; text-align: center; width: 1002px; color: #FFFFFF; background-color: #669999;">
                                            <span lang="en-us"><span class="style32">For any discripancy the concern 
                                            Revenue/Registration Departments may be contacted. Designed and developed by
                                            </span></span>
                                            <asp:Image ID="Image3" runat="server" Height="26px" 
                                                ImageUrl="~/images/nic_logo.jpg" Width="51px" />
                                        </p>
                                    </td>
                                </tr>
                            </table>
       
                       
                
           
        </ContentTemplate>
        </asp:UpdatePanel>
  
       
   
    </form>
</body>
</html>
