<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="CtlPublic.ascx.vb" Inherits="ponweb.ctlPublic" %>
<style type="text/css">
    
    .style9
    {
        height: 40%;
        width: 100%;
    }
    .style10
    {
        width: 100%;
    }
    .style11
    {
        background-color: #0066FF;
    }
    </style>



<div class="style9">

    <table class="style10">
        <tr>
            <td>

<asp:Image ID="Image1" runat="server" ImageUrl="~/images/head1.jpg" />
                </td>
        </tr>
        <tr>
            <td class="style11">
                <asp:Menu ID="Menu1" runat="server" BackColor="#0066FF" 
                DataSourceID="SiteMapDataSource1" DynamicHorizontalOffset="1" Font-Bold="True" 
                Font-Names="Mangal" Font-Size="Small"  Height="32px" 
                Orientation="Horizontal" StaticDisplayLevels="2" 
                StaticSubMenuIndent="10px" ForeColor="White" 
            MaximumDynamicDisplayLevels="2" Width="599px" ItemWrap="True">
                <StaticMenuStyle BorderColor="Red" />
                <StaticSelectedStyle BackColor="Blue" />
                <StaticMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" 
                    ItemSpacing="3px" />
                <DynamicHoverStyle BackColor="#284E98" ForeColor="White" />
                <DynamicMenuStyle BackColor="#B5C7DE" />
                <DynamicSelectedStyle BackColor="#507CD1" />
                <DynamicMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
                <StaticHoverStyle BackColor="#284E98" ForeColor="White" />
            </asp:Menu>
   <asp:SiteMapDataSource ID="SiteMapDataSource1" SiteMapProvider="smMAIN" 
                runat="server" />
       
            </td>
        </tr>
    </table>
    <br />
       
    </div>

  
  
    
    
    
    
    
    








