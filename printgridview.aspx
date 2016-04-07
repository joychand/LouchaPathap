<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="printgridview.aspx.vb" Inherits="ponweb.printgridview" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<meta http-equiv="X-UA-Compatible" content="IE=9" />
    <title>PrintROR Page</title>
    <link rel="shortcut icon" href="~/favicon.ico" type="image/icon"/> <link rel="icon" href="~/favicon.ico" type="image/icon" />
    <style type="text/css">
        .style7
        {
            width: 46px;
            text-align: left;
        }
        #Submit1
        {
            width: 153px;
        }
        .style14
        {
            width: 193px;
            font-weight: bold;
        }
        .style17
        {
            color: #FF0000;
        }
        .style19
        {
            width: 32px;
        }
        .style27
        {
            width: 640px;
            height: 87px;
        }
        .style28
        {
            height: 87px;
        }
        .style38
        {
            background-color: #FFFFFF;
        }
       
        .style52
        {
            width: 640px;
            font-size: medium;
            height: 10px;
        }
        .style53
        {
            height: 10px;
        }
        .style54
        {
            width: 181px;
        }
        .style57
        {
            height: 84px;
        }
        .style59
        {
            height: 84px;
            width: 640px;
        }
        #printpagebutton
        {
            width: 119px;
        }
        .style66
        {
            width: 537px;
        }
        .style69
        {
            font-size: small;
        }
        .style70
        {
            width: 48px;
        }
        .style75
        {
            width: 258px;
        }
        .style81
        {
            width: 45px;
        }
        .style85
        {
            font-style: italic;
            color: #006600;
            font-size: large;
            font-weight: bold;
            background-color: #FFFFFF;
        }
        .style86
        {
            font-size: x-large;
            text-decoration: underline;
        }
        .style87
        {
            width: 653px;
            font-size: x-large;
            text-decoration: underline;
        }
        .style88
        {
            text-align: left;
        }
    </style>
    <script type="text/javascript">
function printpage()
  {
  window.print()
  }
    </script>
    <%--<script type="text/javascript">
    function printpage() {
        //Get the print button and put it into a variable
//        var printButton = document.getElementById("printpagebutton");
        var backButton   = document.getElementById("Back_button")
        //Set the print button visibility to 'hidden' 
//        printButton.style.visibility = 'hidden';
         backButton.style.visibility = 'hidden';
        //Print the page content
        window.print()
        //Set the print button to 'visible' again 
        //[Delete this line if you want it to stay hidden after printing]
//        printButton.style.visibility = 'visible';
        backButton.style.visibility = 'visible';
    }
</script>--%>
<link rel="stylesheet" type="text/css" media="print" href ="~/stylesheet1.css" />
</head>
<body>
    <form id="form1" runat="server">
  <div>
      <i><span class="style17">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span>
      <table style="width: 80%; background-color: #FFFFFF; margin-left: 0px;" 
          cellpadding="0" cellspacing="0">
          <tr>
              <td class="style81">
                  <span class="style38"></span>
              </td>
              <td class="style86" style="text-align: center" colspan="2">
                  <p class="style88">
                  Manipur Jamabandi Online<span lang="en-us"> </span>
                    <asp:Label 
                        ID="lbdteffect" runat="server" BorderColor="White" CssClass="style85"></asp:Label>
      <i>
                  <span class="style38"></span>
      </i>
                  </p>
              </td>
          </tr>
          <tr>
              <td class="style81">
                  &nbsp;</td>
              <td class="style87" style="text-align: center">
                    <asp:Label 
                        ID="lblerror" runat="server" style="color: #003300; background-color: #FF0000" 
                        Visible="False"></asp:Label>
                </td>
              <td style="text-align: center">
                        <i><span class="style17">
                    <asp:Button ID="Back_button" runat="server" Text="Back" 
                        PostBackUrl="~/queryROR.aspx" />
                    </span></i></td>
          </tr>
      </table>
      </i>
  </div>
    <div>
    
        <table style="width:80%;" cellpadding="0" cellspacing="0">
            <tr>
                <td class="style7">
                    1.District:</td>
                <td class="style14">
                    <asp:Label ID="Label1" runat="server" Text="Label" 
                        style="font-style: italic; color: #0033CC; font-size: large;"></asp:Label>
                </td>
                <td class="style19">
                    2.Circle:</td>
                <td class="style54">
                    <asp:Label ID="Label2" runat="server" Text="Label" 
                        
                        style="font-style: italic; color: #0000FF; font-size: large; font-weight: 700;"></asp:Label>
                </td>
                <td class="style70">
                    3.Village:</td>
                <td class="style75">
                    <asp:Label ID="Label3" runat="server" Text="Label" 
                        
                        style="font-style: italic; color: #0033CC; font-size: large; font-weight: 700;"></asp:Label>
                </td>
            </tr>
            </table>
        <table style="width:80%;" cellpadding="0" cellspacing="0">
            <tr>
                <td class="style52">
                    &nbsp;</td>
                <td class="style53">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style27" valign="top">
                    4.Pattadar Details:<br />
                    <asp:GridView ID="GridView2" runat="server" Width="538px" 
                        AutoGenerateColumns="False" Height="30px" HorizontalAlign="Left" 
                        style="margin-left: 0px">
                        <Columns>
                            <asp:BoundField DataField="ownno" HeaderText="Slno" >
                                <ItemStyle Font-Size="Small" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Name" HeaderText="Name" >
                                <HeaderStyle HorizontalAlign="Left" Font-Size="Small" />
                                <ItemStyle Font-Size="Small" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Father" HeaderText="Father/Husband" >
                                <HeaderStyle HorizontalAlign="Left" Font-Size="Small" />
                                <ItemStyle Font-Size="Small" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Address" HeaderText="Address" >
                                <HeaderStyle HorizontalAlign="Left" Font-Size="Small" />
                                <ItemStyle Font-Size="Small" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </td>
                <td class="style28">
                    &nbsp;</td>
            </tr>
            </table>
        <table style="width: 80%; " cellpadding="0" 
            cellspacing="0">
            <tr>
                <td class="style59" valign="top">
                    5.Plot Details:<br />
                    <asp:GridView ID="GridView1" runat="server" Width="538px" 
                        AutoGenerateColumns="False" Height="16px" HorizontalAlign="Left" 
                        style="margin-right: 0px">
                        <Columns>
                            <asp:BoundField DataField="NewDagno" HeaderText="NewDagno" >
                                <ControlStyle Font-Size="Small" />
                                <HeaderStyle Font-Size="Small" HorizontalAlign="Center" />
                                <ItemStyle Font-Size="Small" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="OldPattaNo" HeaderText="OldPattaNo">
                                <ControlStyle Font-Size="Small" />
                                <HeaderStyle Font-Size="Small" HorizontalAlign="Center" />
                                <ItemStyle Font-Size="Small" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="NewPattaNo" HeaderText="NewPattaNo">
                                <HeaderStyle Font-Size="Small" HorizontalAlign="Center" />
                                <ItemStyle Font-Size="Small" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Area_acre" HeaderText="Area (Acre)" >
                                <HeaderStyle Font-Size="Small" HorizontalAlign="Center" />
                                <ItemStyle Font-Size="Small" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Area" HeaderText="Area(Hectare)" >
                                <HeaderStyle Font-Size="Small" HorizontalAlign="Center" />
                                <ItemStyle Font-Size="Small" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="landClass" HeaderText="LandClass" >
                                <HeaderStyle Font-Size="Small" HorizontalAlign="Center" />
                                <ItemStyle Font-Size="Small" HorizontalAlign="Center" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </td>
                <td class="style57">
                    &nbsp;
                </td>
            </tr>
            </table>
        <table style="width: 100%; line-height: normal;" cellpadding="0" 
            cellspacing="0">
            <tr>
                <td class="style66">
                    Note: For any discripancy please contact the concerned SDC</td>
                <td>
                    &nbsp;
                </td>
            </tr>
            </table> 
            </div>
        <div id ="footer" class="style38" 
            style="background-color: #FFFFFF; color: #000000;">
            <hr />
            &nbsp;<span class="style69">Printed on</span>&nbsp;&nbsp;<asp:Label ID="lblDate" 
                runat="server" style="font-size: x-small"></asp:Label>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<i><input id="printpagebutton" type="button" value="Print this page" onclick="printpage()"/></i>
            <asp:Label ID="Label4" runat="server" Text="Label"></asp:Label>
    </div>
            </form>
            </body>
            </html>