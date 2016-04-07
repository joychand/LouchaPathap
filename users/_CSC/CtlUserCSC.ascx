<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="CtlUserCSC.ascx.vb" Inherits="ponweb.CtlUserCSC" %>
<style type="text/css">
    
    .style9
    {
        height: auto;
        width: 999px;
    }
    .style12
    {
        width: 100%;
        height: 110px;
    }
    .style13
    {
        background-color: #0066FF;
    }
    </style>



<div class="style9">
    
       <table class="style12">
           <tr>
               <td>
    
                <asp:Image ID="Image1" runat="server" ImageUrl="~/images/head1.jpg" Height="91px" 
                       Width="987px" style="margin-left:0;" />
                    
               </td>
           </tr>
             <tr>
             <td>
                 <asp:Label ID="lblUsrinfo" runat="server" 
                     style="font-weight: 700; font-style: italic; color: #990033; font-size: small"></asp:Label>
             </td>
             </td></tr>
                  
            <tr>
               <td class="style13" height="40" style="clip: rect(auto, auto, auto, auto)">
           <asp:Menu ID="Menu1" runat="server" BackColor="#0066FF" 
                DataSourceID="SiteMapDataSource1" DynamicHorizontalOffset="2" Font-Bold="True" 
                Font-Names="Mangal" Font-Size="Small"  Height="19px" 
                Orientation="Horizontal" StaticDisplayLevels="2" 
                StaticSubMenuIndent="10px" ForeColor="White" 
            MaximumDynamicDisplayLevels="2">
                <StaticMenuStyle BorderColor="Red" />
                <StaticSelectedStyle BackColor="Blue" />
                <StaticMenuItemStyle HorizontalPadding="0px" VerticalPadding="0px" 
                    ItemSpacing="3px" />
                <DynamicHoverStyle BackColor="#284E98" ForeColor="White" />
                <DynamicMenuStyle BackColor="#B5C7DE" />
                <DynamicSelectedStyle BackColor="#507CD1" />
                <DynamicMenuItemStyle HorizontalPadding="0px" VerticalPadding="0px" />
                <StaticHoverStyle BackColor="#284E98" ForeColor="White" />
            </asp:Menu>
   <asp:SiteMapDataSource ID="SiteMapDataSource1" SiteMapProvider="smUserCSC" 
                runat="server" />


               </td>
           </tr>
       </table>
       
    </div>
  
  
    
    
    
    
    
    








