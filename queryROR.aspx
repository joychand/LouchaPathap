<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="queryROR.aspx.vb" Inherits="ponweb.queryROR" %>


<%@ Register src="CtlPublic.ascx" tagname="CtlPublic" tagprefix="uc1" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Patta on Web</title>
    <meta http-equiv="X-UA-Compatible" content="IE=9" />
    <link rel="shortcut icon" href="~/favicon.ico" type="image/icon"/> <link rel="icon" href="~/favicon.ico" type="image/icon" />
    <style type="text/css">


        .style38
        {
            background-color: #66CCFF;
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
        .style42
        {
            color: #663300;
            text-decoration: no 
            background-color: #CCFF99;
        }
        .style65
        {
            text-decoration: no 
            font-family: "Courier New", Courier, monospace;
        }
        .style75
        {
            font-family: "Courier New", Courier, monospace;
            color: #0000CC;
            font-size: medium;
        }
        .style85
        {
            height: 30px;
            text-align: left;
            width: 353px;
        }
        .style76
        {
            font-family: "Courier New", Courier, monospace;
            color: #0000FF;
            font-size: medium;
        }
        .style93
        {
            margin-left: 0px;
        }
        .style101
        {
            font-family: "Courier New", Courier, monospace;
        }
        .style112
        {
            text-align: left;
        }
        .style115
        {
            background-color: #66CCFF;
            text-align: left;
            border-bottom-color: #C0C0C0;
            border-bottom-width: 1px;
        }
        .style117
        {
            height: 15px;
            background-color: #CCCCCC;
            text-align: center;
        }
        .style122
        {
            text-align: center;
            font-weight: bold;
            background-color: #CCFFCC;
        }
        .style124
        {
            width: 1139px;
        }
        .style126
        {
            width: 195px;
            text-align: right;
        }
        .style128
        {
            width: 357px;
        }
        .style130
        {
            width: 259px;
        }
        .style131
        {
            height: 30px;
            text-align: left;
            width: 172px;
        }
        .style132
        {
            width: 100px;
        }
        .style32
        {
            background-color: #669999;
        }
        </style>
</head>
<body>
    <form id="form1" runat="server" autocomplete="off">
    <div class="style112">
    
    
        <uc1:CtlPublic ID="CtlPublic1" runat="server" />
    
    
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
                            
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table align="center" class="style124"  >
                    <tr>
                        <td class="style122" colspan="7" >
                            &nbsp;&nbsp;</td>
                            <tr>
                                <td class="style126">
                                    <span lang="en-us"><span class="style65" 
                                        style="text-decoration:   font-size: large;">District </span>
                                    <td class="style128">
                                        <span lang="en-us"><span class="style65" 
                                            style="text-decoration:   font-size: large;">
                                        <asp:DropDownList ID="lDistrict" runat="server" 
                                            Font-Size="Medium" Height="27px" TabIndex="6" Width="165px" 
                                            AutoPostBack="True">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                            ControlToValidate="lDistrict" InitialValue="Select District" 
                                            Display="None" ValidationGroup="pblRor">*</asp:RequiredFieldValidator>
                                        </span>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" 
                                            ControlToValidate="lDistrict" ErrorMessage="Invalid District" 
                                            ValidationExpression="^[0-9]{1,2}$" Display="None" 
                                            ValidationGroup="pblRor"></asp:RegularExpressionValidator>
                                        <td class="style130">
                                            <span lang="en-us">Circle</span><td class="style130">
                                                <span lang="en-us"><span class="style65" 
                                                    style="text-decoration:   font-size: large;">
                                                <asp:DropDownList ID="lCircle" runat="server" AutoPostBack="True" 
                                                    Font-Size="Medium" Height="27px" TabIndex="6" Width="165px">
                                                </asp:DropDownList>
                                                <span lang="en-us">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                                    ControlToValidate="lCircle" Display="Dynamic" InitialValue="Select Circle" 
                                                    ValidationGroup="pblRor">*</asp:RequiredFieldValidator>
                                                </span>
                                                </span></span>
                                                <td class="style132">
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" 
                                                        ErrorMessage="Invalid Circle" ControlToValidate="lCircle" Display="None" 
                                                        ValidationExpression="^[0-9]{1,5}$" ValidationGroup="pblRor"></asp:RegularExpressionValidator>
                                                    <td class="style112" colspan="2">
                                                        &nbsp;</td>
                                                </td>
                                            </td>
                                        </td>
                                        </span>
                                    </td>
                                    </span>
                                </td>
                        </tr>
                        <tr>
                            <td class="style126">
                                Village</td>
                            <td class="style128">
                                &nbsp;<asp:DropDownList ID="lVillage" runat="server" 
                                    CssClass="style93" Font-Size="Medium" Height="27px" TabIndex="6" 
                                    Width="214px" AutoPostBack="True">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                                    ControlToValidate="lVillage" Display="Dynamic" 
                                    InitialValue="Select Village" ValidationGroup="pblRor">*</asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" 
                                    ErrorMessage="Invalid Village" ControlToValidate="lVillage" Display="None" 
                                    ValidationExpression="^[0-9]{1,10}$" ValidationGroup="pblRor"></asp:RegularExpressionValidator>
                            </td>
                            <td class="style130">
                                <span lang="en-us" style="font-size: small">NewPattaNo.</span></td>
                            <td class="style130">
                                <span lang="en-us">
                                <asp:TextBox ID="txtPattaNo" runat="server" Font-Size="Medium" Height="20px" 
                                    MaxLength="6" TabIndex="7" Width="97px" autocomplete = "off" AutoCompleteType="Disabled"></asp:TextBox>
                                </span>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                                    ControlToValidate="txtPattaNo" Display="Dynamic" ValidationGroup="pblRor">*</asp:RequiredFieldValidator>
                                <br />
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                                    ControlToValidate="txtPattaNo" ErrorMessage="Invalid Patta No." 
                                    style="font-style: italic; font-size: small" 
                                    ValidationExpression="^[0-9]{1,6}$" Display="None" 
                                    ValidationGroup="pblRor"></asp:RegularExpressionValidator>
                            </td>
                            <td class="style132">
                                <span lang="en-us" style="font-size: small">NewDagNo</span></td>
                            </span>
                            </td>
                            <td class="style131">
                                <span lang="en-us">&nbsp;<asp:TextBox ID="txtNewDagNo" runat="server" 
                                    Font-Size="Medium" Height="20px" MaxLength="6" TabIndex="7" Width="97px" 
                                    autocomplete = "off" AutoCompleteType="Disabled"></asp:TextBox>
                                </span>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                                    ControlToValidate="txtNewDagNo" Display="Dynamic" ValidationGroup="pblRor">*</asp:RequiredFieldValidator>
                                <br />
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
                                    ControlToValidate="txtNewDagNo" ErrorMessage="Invalid Dag No" 
                                    style="font-style: italic; font-size: small" 
                                    ValidationExpression="^[0-9]{1,6}$" Display="None" 
                                    EnableClientScript="False" ValidationGroup="pbRor"></asp:RegularExpressionValidator>
                            </td>
                            <td class="style85">
                                <asp:Button ID="btnCheck" runat="server" CssClass="style101" 
                                    Font-Names="Segoe UI" Font-Size="Medium" Height="27px" TabIndex="2" 
                                    Text="Submit" Width="119px"/>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="style117" colspan="7">
                                <asp:Label ID="lberror" runat="server" CssClass="style42"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="style112" colspan="3">
                                <asp:Label ID="cap1" runat="server" CssClass="style76"></asp:Label>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:ValidationSummary 
                                    ID="ValidationSummary1" runat="server" HeaderText="Plz select/correct" 
                                    ValidationGroup="pblRor" />
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Label ID="lblRecord" runat="server" style="color: #FF0000"></asp:Label>
                            </td>
                            <td class="style112" colspan="4">
                            <asp:Label ID="cap2" runat="server" CssClass="style75"></asp:Label>
                            </td>
                        </tr>
                    </tr>
                    <tr>
                        <td class="style112" colspan="3">
                            <asp:GridView ID="GridViewPlot" runat="server" AutoGenerateColumns="False" 
                                BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" 
                                CaptionAlign="Left" CellPadding="3" CssClass="style38" EnableInsert="False" 
                                Font-Size="Small" HorizontalAlign="Left" SaveButtonID="" TabIndex="5" 
                                Width="96%">
                                <FooterStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                <RowStyle ForeColor="#000066" HorizontalAlign="Left" VerticalAlign="Middle" />
                                <Columns>
                                    <asp:BoundField DataField="NewDagNo" HeaderText="NewDagNo">
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="OldPattaNo" HeaderText="OldPattaNo" />
                                    <asp:BoundField DataField="NewPattaNo" HeaderText="NewPattaNo" />
                                    <asp:BoundField DataField="Area_acre" HeaderText="Area (in Acre)">
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Area" HeaderText="Area (in Hectre)">
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="landClass" HeaderText="LandClass" />
                                </Columns>
                                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" 
                                    HorizontalAlign="Left" VerticalAlign="Middle" />
                                <EditRowStyle HorizontalAlign="Left" />
                                <AlternatingRowStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                            </asp:GridView>
                        </td>
                        <td class="style112" colspan="4">
                            <asp:GridView ID="GridViewOwner" runat="server" AutoGenerateColumns="False" 
                                BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" 
                                CaptionAlign="Left" CellPadding="3" CssClass="style115" EnableInsert="False" 
                                Font-Size="Small" HorizontalAlign="Left" SaveButtonID="" TabIndex="5" 
                                Width="97%">
                                <FooterStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                <RowStyle ForeColor="#000066" HorizontalAlign="Left" VerticalAlign="Middle" />
                                <Columns>
                                    <asp:BoundField DataField="ownno" HeaderText="Slno" />
                                    <asp:BoundField DataField="name" HeaderText="Pattadar" ReadOnly="True">
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Father" HeaderText="Father/Husband" ReadOnly="True">
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Address" FooterText="User Role" HeaderText="Address">
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                </Columns>
                                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" 
                                    HorizontalAlign="Left" VerticalAlign="Middle" />
                                <EditRowStyle HorizontalAlign="Left" />
                                <AlternatingRowStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td class="style112" colspan="3">
                            &nbsp;</td>
                        <td class="style112" colspan="4">
                            <asp:Button ID="Button2" runat="server" PostBackUrl="~/printgridview.aspx" 
                                style="text-align: left" Text="Print View" Visible="False" />
                        </td>
                    </tr>
                </table>
               
                <p class="MsoNormal" 
                    style="margin-bottom: 0in; margin-bottom: .0001pt; line-height: normal; font-size: medium; text-align: center; width: 100%; color: #FFFFFF; background-color: #669999;">
                    <span lang="en-us"><span class="style32">For any discripancy the concern 
                    Revenue/Registration Departments may be contacted. Designed and developed by
                    </span></span>
                    <asp:Image ID="Image3" runat="server" Height="26px" 
                        ImageUrl="~/images/nic_logo.jpg" Width="51px" />
                </p>
                
            </ContentTemplate>
        </asp:UpdatePanel>
    
    </div>
    </form>
</body>
</html>
