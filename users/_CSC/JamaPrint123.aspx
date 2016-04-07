<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="~/users/_CSC/JamaPrint123.aspx.vb" Inherits="ponweb.Jamaprint123" EnableViewStateMac="true"%>

<!DOCTYPE html>
<html lang="en">
<meta charset="utf-8">

<head id="Head1" runat="server">
<title>Loucha Pathap manipur.. Jamabandi</title> 
    
    <script language="javascript" type="text/javascript">
        function print_Jama() {
            var ButtonControl = document.getElementById("btnprintJama");
            ButtonControl.style.visibility = "hidden";

            var ButtonControl22 = document.getElementById("btnBack");
            ButtonControl22.style.visibility = "hidden";

            window.print();
            ButtonControl.style.visibility = "visible";
            ButtonControl22.style.visibility = "visible";
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
        .style7
        {
            background-color: #FFCC66;
            margin-left: 0px;
        }
        .style17
        {
            padding: 1px 4px;
        }
           .style19
           {
               height: 19px;
               font-size: x-small;
               color: #993300;
           }
           .style20
           {
               height: 19px;
               font-size: x-small;
               color: #336600;
           }
           .style21
           {
               border-top-style: solid;
               border-top-width: 1px;
               padding: 1px 4px;
           }
           .style22
           {
               border-top-style: solid;
               border-top-width: 1px;
               padding: 1px 4px;
               text-align: right;
           }
           </style>
                
    </head>
   
<body>
    <form id="form1" runat="server" autocomplete="off">
   
    <table>
                 
              
                <tr>
                    
                    <td colspan="4" >
                   <div>
                    <input type="button" id="btnprintJama" value="Print JAMABANDI" 
                           onclick="print_Jama()" class="style20"  />
                      <input type="button" id="btnBack" value="<= Back" onclick="history.go(-1);" 
                           class="style19" /></div>
                        
                        <asp:GridView ID="GridView2" runat="server"  
                  AutoGenerateColumns="False" BackImageUrl="~/images/BgRORHead.png" 
                  BorderStyle="None" ShowFooter="False"  
            Style="position: static" GridLines="None" Width="1281px" Height="131px" 
                    PageSize="7" ShowHeader="False">
                  <Columns>
                      <asp:BoundField DataField="id1" />
                      <asp:BoundField DataField="id2" />
                      <asp:BoundField DataField="id3" />
                      <asp:BoundField DataField="id4" />
                      <asp:BoundField DataField="id5" />
                      <asp:BoundField DataField="id6" />
                      <asp:BoundField DataField="id7" />
                      <asp:BoundField DataField="id8" />
                      <asp:BoundField DataField="id9" />
                      <asp:BoundField DataField="id10" />
                      <asp:BoundField DataField="id11" />
                      <asp:BoundField DataField="id12" />
                      <asp:BoundField DataField="id13" />
                      <asp:BoundField DataField="id14" />
                  </Columns>
                        </asp:GridView>
                  <%--<div class="style21"></div>--%>
              <asp:GridView ID="GridView1" runat="server"  
                  AutoGenerateColumns="False" BackImageUrl="~/images/BgRORDetail.png" 
                  BorderStyle="None" ShowFooter="True"  
            Style="position: static" GridLines="None" Width="1296px" Height="368px">
                  <PagerSettings Position="Bottom" />
                  <Columns>
                      <asp:BoundField DataField="id1" />
                      <asp:BoundField DataField="id2" />
                      <asp:BoundField DataField="id3" />
                      <asp:BoundField DataField="id4" />
                      <asp:BoundField DataField="id5" />
                      <asp:BoundField DataField="id6" />
                      <asp:BoundField DataField="id7" />
                      <asp:BoundField DataField="id8" />
                      <asp:BoundField DataField="id9" />
                      <asp:BoundField DataField="id10" />
                      <asp:BoundField DataField="id11" />
                      <asp:BoundField DataField="id12" />
                      <asp:BoundField DataField="id13" />
                      <asp:BoundField DataField="id14" />
                  </Columns>
                        </asp:GridView>
                  <tr>
                  <td class="style21">
                  
                        <asp:Label ID="lbTimeStamp" runat="server" Text="Label"></asp:Label>
                  
                  </td>
                  <td class="style21">
                  
                        <asp:Label ID="lbWords" runat="server" 
                            Visible="False"></asp:Label>
                        
                        <span lang="en-us">&nbsp;</span><asp:Label ID="lbTotArea" runat="server" Visible="False"></asp:Label>
                        
                  </td>
                  <td class="style21">
                  
                        <span lang="en-us" class="style17">
                        <asp:Label ID="lbsc" runat="server" Font-Size="XX-Small" ForeColor="#CCCCCC"></asp:Label>
                        
                        </span>
                  
                  </td>
                  <td class="style22">
                  
                      <asp:ImageButton ID="imageBar1" 
                            runat="server" Height="31px" Width="485px" 
                            CssClass="style7" />
                    
                  </td>
                  </tr>
                        
                    </td>
                    
            
                    
   </div>
      </table>    
    </form>
</body>
</html>
