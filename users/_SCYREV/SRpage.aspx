<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="~/users/_SCYREV/SRpage.aspx.vb" Inherits="ponweb.SRpage"  EnableViewStateMac="true"%>
<%@ Register src="CtlUserSR.ascx" tagname="CtlUserSR" tagprefix="uc2" %>
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
        .style19
        {
            text-align: left;
        }
        .style21
        {
            width: 23px;
            text-align: right;
        }
        .style22
        {
        }
        .style23
        {
            text-align: right;
            color: #FF0000;
            font-style: italic;
        }
        .style27
        {
            width: 47px;
        }
        .style28
        {
            width: 8px;
        }
        .style29
        {
            text-align: left;
        }
        .style30
        {
            font-size: small;
            text-align: left;
            color: #000099;
            font-style: italic;
        }
        .style31
        {
            font-size: small;
            color: #660066;
            font-style: italic;
            text-align: right;
        }
        .style32
        {
            color: #669999;
        }
        .style35
        {
            text-align: left;
            width: 8px;
        }
        .style36
        {
            font-size: small;
            text-align: left;
            color: #FF0000;
            font-style: italic;
        }
        .style37
        {
            font-size: small;
            color: #FF0000;
            font-style: italic;
            text-align: right;
        }
        .style38
        {
            color: #CC3300;
        }
        .style40
        {
            width: 100px;
        }
        .style41
        {
            text-align: left;
            width: 101px;
        }
        .style42
        {
            margin-left: 240px;
        }
        .style44
        {
            width: 1004px;
        }
    </style>


</head>
<body>
    <form id="SRpage" runat="server" autocomplete="off">
    <div style="height: 312px; width: 1094px; margin:0 auto;" id="container">
    <div >
        <uc2:CtlUserSR ID="CtlUserSR1" runat="server" />
        </div>    
    
        
       <div id="content"  style="text-align: left; margin-top: 79px;height: 100%; width: 1094px;float:inherit;margin:0 auto;">
           <asp:ScriptManager ID="ScriptManager1" runat="server">
           </asp:ScriptManager>
        
           <asp:UpdatePanel ID="UpdatePanel1" runat="server">
               <ContentTemplate>
                   <table style="width:990px;">
                       <tr>
                           <td class="style41" rowspan="6">
                               <br />
                           </td>
                           <td class="style21">
                               &nbsp;</td>
                           <td class="style28">
                               &nbsp;</td>
                           <td style="text-align: center" class="style22" colspan="2">
                               <h2 style="text-align: left; text-decoration: underline;">
                                   Land Search<span lang="en-us"> -- </span>
                                   <asp:Label ID="lberr" runat="server" CssClass="style38"></asp:Label>
                               </h2>
                           </td>
                       </tr>
                       <tr>
                           <td class="style21">
                               District:</td>
                           <td class="style35">
                               <asp:DropDownList ID="ddlDistrict" runat="server" Height="27px" Width="166px" 
                                   AutoPostBack="True" Font-Bold="True" ForeColor="Maroon" Font-Size="10">
                               </asp:DropDownList>
                               <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                                   ErrorMessage="Invalid District" ControlToValidate="ddlDistrict" Display="None" 
                                   ValidationExpression="^[0-9]{1,2}$" ValidationGroup="SR"></asp:RegularExpressionValidator>
                               <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                   ErrorMessage="Select one District" ControlToValidate="ddlDistrict" 
                                   Display="None" InitialValue="Select District" ValidationGroup="SR"></asp:RequiredFieldValidator>
                           </td>
                           <td class="style27">
                               Circle:</td>
                           <td>
                               <asp:DropDownList ID="ddlCircle" runat="server" AutoPostBack="True" 
                                   Height="27px" Width="166px">
                               </asp:DropDownList>
                               <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                   ErrorMessage="Select one Circle" ControlToValidate="ddlCircle" Display="None" 
                                   InitialValue="Select Circle" ValidationGroup="SR"></asp:RequiredFieldValidator>
                               <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
                                   ErrorMessage="Invalid Circle" ControlToValidate="ddlCircle" Display="None" 
                                   ValidationExpression="^[0-9]{1,5}$" ValidationGroup="SR"></asp:RegularExpressionValidator>
                           </td>
                           <td>
                               &nbsp;</td>
                       </tr>
                       <tr>
                           <td class="style21">
                               Village:</td>
                           <td class="style35">
                               <asp:DropDownList ID="ddlVillage" runat="server" AutoPostBack="True" 
                                   Height="27px" Width="166px">
                               </asp:DropDownList>
                               <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                                   ErrorMessage="Select Village" ControlToValidate="ddlVillage" Display="None" 
                                   InitialValue="Select Village" ValidationGroup="SR"></asp:RequiredFieldValidator>
                               <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" 
                                   ErrorMessage="Invalid Village" ControlToValidate="ddlVillage" Display="None" 
                                   ValidationExpression="^[0-9]{1,10}$" ValidationGroup="SR"></asp:RegularExpressionValidator>
                           </td>
                           <td class="style27">
                               Owner:</td>
                           <td>
                               <asp:DropDownList ID="ddlLandclass" runat="server" AutoPostBack="True" 
                                   Height="27px" Width="166px">
                               </asp:DropDownList>
                               <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                                   ErrorMessage="select one owner" ControlToValidate="ddlLandclass" Display="None" 
                                   InitialValue="Select Owner" ValidationGroup="SR"></asp:RequiredFieldValidator>
                               <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" 
                                   ErrorMessage="Invalid Owner type" ControlToValidate="ddlLandclass" 
                                   Display="None" ValidationExpression="[a-zA-Z]+[ a-zA-Z/]*" ValidationGroup="SR"></asp:RegularExpressionValidator>  
                               <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="True" 
                                   style="color: #CC0000; font-style: italic; font-size: small" 
                                   Text="Include area size" Visible="False" />
                           </td>
                       </tr>
                       <tr>
                           
                           <td class="style29" colspan="3">
                               &nbsp;</td>
                           <td>
                               <asp:Panel ID="Panel1" runat="server" Visible="False" Width="373px">
                                   <div class="style19">
                                       <span class="style36">Area size From</span><span class="style32"><i><asp:TextBox 
                                           ID="txtareafrm" runat="server" Width="51px"></asp:TextBox>
                                       </i></span><span class="style30">
                                       <asp:RequiredFieldValidator ID="rvTxtFrm" runat="server" 
                                           ControlToValidate="txtareafrm" Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator>
                                       </span><span class="style36">To</span><span class="style32"><i><asp:TextBox 
                                           ID="txtareaTo" runat="server" Width="54px"></asp:TextBox>
                                       </i>
                                       <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                           ControlToValidate="txtareaTo" Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator>
                                       </span><span class="style37">(hectare)</span><span class="style31"> </span>
                                       <asp:CompareValidator ID="CompareValidator1" runat="server" 
                                           ControlToCompare="txtareafrm" ControlToValidate="txtareaTo" Display="Dynamic" 
                                           ErrorMessage="Invalid area range" Operator="GreaterThanEqual" 
                                           style="font-size: small; color: #FF0000; font-style: italic" Type="Double"></asp:CompareValidator>
                                   </div>
                               </asp:Panel>
                           </td>
                       </tr>
                       <tr>
                          
                           <td class="style23" colspan="3">
                               <asp:Button ID="Button1" runat="server" style="text-align: center" 
                                   Text="Search" ValidationGroup="SR" />
                           </td>
                       </tr>
                       </tr>
                       <tr>
                           <td class="style23" colspan="3">
                               <asp:ValidationSummary ID="ValidationSummary1" runat="server" Height="35px" 
                                   ShowMessageBox="True" ShowSummary="False" ValidationGroup="SR" />
                           </td>
                       </tr>
                   </table>
                   <table style="width: 100%;">
                       <tr>
                           <td class="style40">
                               &nbsp;
                           </td>
                           <td style="text-align: center">
                               &nbsp;
                               <asp:Label ID="lblMessage" runat="server" 
                                   style="color: #0000FF; text-decoration: underline; text-align: right" 
                                   Visible="False"></asp:Label>
                               &nbsp;
                           </td>
                       </tr>
                       <tr>
                           <td class="style40">
                               &nbsp;
                           </td>
                           <td class="style42">
                               &nbsp; <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
                                   AutoGenerateColumns="False" BackColor="#DEBA84" BorderColor="#DEBA84" 
                                   BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2" 
                                   Height="16px" PageSize="20" style="margin-left: 2px; margin-right: 0px;" 
                                   Width="900px">
                                   <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
                                   <Columns>
                                       <asp:BoundField DataField="NewDagno" HeaderText="Dagno">
                                           <ControlStyle Width="10%" />
                                           <HeaderStyle Width="10%" />
                                       </asp:BoundField>
                                       <asp:BoundField DataField="Newpattano" HeaderText="Pattano">
                                           <ControlStyle Width="10%" />
                                           <HeaderStyle Width="10%" />
                                       </asp:BoundField>
                                       <asp:BoundField DataField="area" HeaderText="Area(hectare)">
                                           <HeaderStyle Width="10%" />
                                       </asp:BoundField>
                                       <asp:BoundField DataField="name" HeaderText="Name">
                                           <ControlStyle Width="30%" />
                                           <HeaderStyle HorizontalAlign="Left" Width="30%" />
                                           <ItemStyle Font-Size="Medium" HorizontalAlign="Left" Width="30%" />
                                       </asp:BoundField>
                                       <asp:BoundField DataField="address" HeaderText="Address">
                                           <ControlStyle Width="30%" />
                                           <HeaderStyle HorizontalAlign="Left" Width="30%" />
                                           <ItemStyle HorizontalAlign="Left" Width="30%" />
                                       </asp:BoundField>
                                   </Columns>
                                   <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                                   <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
                                   <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
                                   <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
                               </asp:GridView>
                           </td>
                       </tr>
                       <tr>
                           <td colspan="2">
                               <p class="style44" 
                                   
                                   style="margin-bottom: 0in; margin-bottom: .0001pt; line-height: normal; font-size: medium; text-align: center; color: #FFFFFF; background-color: #669999;">
                                   <span lang="en-us"><span class="style32" style="color: #FFFFFF">For any 
                                   discripancy the concern Revenue/Registration Departments may be contacted. 
                                   Designed and developed by </span></span>
                                   <asp:Image ID="Image3" runat="server" Height="26px" 
                                       ImageUrl="~/images/nic_logo.jpg" Width="51px" />
                               </p>
                           </td>
                       </tr>
                   </table>
                   </div>
               </ContentTemplate>
               
           </asp:UpdatePanel>
           
</div>
      
 </div>

    </form>
</body>
</html>
