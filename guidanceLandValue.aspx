<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="guidanceLandValue.aspx.vb" Inherits="ponweb.guidanceLandValue" %>

<%@ Register src="CtlPublic.ascx" tagname="CtlPublic" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Land Valuation</title>
    <style type="text/css">
        .style6
        {
           
            padding: 1px 4px;
            background-color: #E6F4F7;
                height: 390px;
                width: 1163px;
            border-top-style: solid;
            border-top-width: 1px;
        }
        .style8
        {
            width: 106%;
        }
        .style23
        {
            background-color: #E6F4F7;
        }
        .style24
        {
            width: 100%;
            text-align: center;
            background-color: #E6F4F7;
        }
        .style25
        {
            background-color: #E6F4F7;
            text-decoration: underline;
        }
        .style26
        {
            text-align: right;
        }
        .style27
        {
            text-align: left;
        }
        .style28
        {
            width: 100%;
            text-align: right;
            background-color: #E6F4F7;
        }
        .style30
        {
            background-color: #E6F4F7;
            text-align: right;
            width: 562px;
        }
        .style31
        {
            width: 430px;
            background-color: #E6F4F7;
            text-align: left;
        }
        .style35
        {
            border: 1px solid #008000;
        }
        .style36
        {
            border-top: 1px solid #008000;
            width: 100%;
                text-align: right;
                background-color: #E6F4F7;
                height: 8px;
        }
        .style37
        {
            height: 8px;
            text-align: right;
            width: 562px;
        }
        .style38
        {
            border-color: #008000;
        }
        .style40
        {
            padding: 1px 4px;
        }
        .style42
        {
            height: 8px;
            text-align: left;
        }
        .style46
        {
            border-color: #000080;
            text-align: right;
                    width: 562px;
                padding: 0;
            }
        .style48
        {
            background-color: #FFFFCC;
        }
        .style49
        {
            text-align: left;
            border-bottom-style: solid;
            border-bottom-width: 1px;
            padding: 1px 4px;
        }
        .style50
        {
            text-align: right;
            border-bottom-style: solid;
            border-bottom-width: 1px;
            padding: 1px 4px;
        }
        .style53
        {
            border-color: #008000;
            text-align: left;
            padding: 0;
            width: 171px;
        }
        .style56
        {
            text-align: center;
            text-decoration: underline;
        }
        .style57
        {
            padding: 1px 4px;
            background-color: #E6F4F7;
            text-align: left;
            border-bottom-style: solid;
            border-bottom-width: 1px;
        }
        .style58
        {
            border-color: #008000;
            padding: 0;
            text-align: left;
            background-color: #E6F4F7;
            width: 171px;
        }
        .style59
        {
            border-color: #008000;
            padding: 0;
            width: 100%;
            text-align: right;
            background-color: #E6F4F7;
        }
        .style61
        {
            border-color: #000080;
            padding: 0;
            background-color: #E6F4F7;
            text-align: left;
        }
        .style62
        {
            border-color: #000080;
            padding: 0;
            width: 562px;
            text-align: right;
            background-color: #E6F4F7;
        }
        .style63
        {
            border-color: #000080;
            text-align: left;
            padding: 0;
        }
        .style64
        {
            padding: 1px 4px;
            text-align: center;
                background-color: #E6F4F7;
            width: 171px;
            border-bottom-style: solid;
            border-bottom-width: 1px;
        }
        .style65
        {
            text-align: left;
            border-top-style: solid;
            border-top-width: 1px;
            padding: 1px 4px;
        }
        .style66
        {
            text-align: right;
            border-top-style: solid;
            border-top-width: 1px;
            padding: 1px 4px;
        }
        .style67
        {
            border-color: #000080;
            padding: 0;
            background-color: #E6F4F7;
                text-align: left;
                width: 401px;
        }
        .style69
        {
            width: 23px;
            text-align: center;
            background-color: #E6F4F7;
        }
        .style70
        {
            padding: 1px 4px;
            background-color: #E6F4F7;
            text-align: right;
            width: 562px;
            border-bottom-style: solid;
            border-bottom-width: 1px;
        }
        .style71
        {
            padding: 1px 4px;
            width: 100%;
            text-align: right;
            background-color: #E6F4F7;
            border-bottom-style: solid;
            border-bottom-width: 1px;
        }
        .style73
        {
            text-align: center;
            background-color: #808080;
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
        .style32
        {
            background-color: #669999;
        }
        </style>
</head>
<%--<script type="text/javascript" src="http://code.jquery.com/jquery-1.7.2.js"></script>--%>
<body>
    <form id="form1" runat="server">
        <div>
        
            <uc1:CtlPublic ID="CtlPublic1" runat="server" />
        
        </div>
       
    
    
         <table class="style6" align="center">
        <tr>
            <td class="style69" rowspan="9">
                <asp:Image ID="Image1" runat="server" Height="368px" 
                    ImageUrl="~/images/vline.png" Width="1px" />
            </td>
        </tr>
        <tr>
            <td class="style24" colspan="2">
                &nbsp;</td>
            <td style="font-family: 'Times New Roman', Times, serif; font-size: medium; " 
                colspan="4" class="style25">
                <span class="style48">D</span><span class="style48" lang="en-us"> </span>e<span 
                    class="style48" lang="en-us"> </span>t<span class="style48" lang="en-us">
                </span>a<span class="style48" lang="en-us"> </span>i<span class="style48" 
                    lang="en-us"> </span>l<span class="style48" lang="en-us"> </span>&nbsp;<span 
                    class="style48" lang="en-us"> </span>S<span class="style48" lang="en-us">
                </span>c<span class="style48" lang="en-us"> </span>h<span class="style48" 
                    lang="en-us"> </span>e<span class="style48" lang="en-us"> </span>d<span 
                    class="style48" lang="en-us"> </span>u<span class="style48" lang="en-us">
                </span>l<span class="style48" lang="en-us"> </span>e<span class="style48" 
                    lang="en-us"> </span>&nbsp;<span class="style48" lang="en-us"> </span>o<span 
                    class="style48" lang="en-us"> </span>f<span class="style48" lang="en-us">
                </span>&nbsp;<span class="style48" lang="en-us"> </span>U<span class="style48" 
                    lang="en-us"> </span>n<span class="style48" lang="en-us"> </span>i<span 
                    class="style48" lang="en-us"> </span>t<span class="style48" lang="en-us">
                </span>s<span lang="en-us"> </span></td>
        </tr>
        <tr>
            <td class="style24" colspan="2">
                <table class="style8">
                    <tr>
                        <td class="style66">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Schedule of Units
                           
                           
                           
                            
                            
                            
                        </td>
                        <td class="style65" colspan="2">
                            
                          
                            <asp:DropDownList ID="ddSoU" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddSoU_SelectedIndexChanged">
                                <asp:ListItem Text="--Select Unit--" Value=""></asp:ListItem>
                            </asp:DropDownList>
                           
                           
                           
                            
                            
                            
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="style50" colspan="2">
                            Rate</td>
                        <td class="style49">
                            <asp:DropDownList ID="ddRate" runat="server" AppendDataBoundItems="True" Width="100px">
                            </asp:DropDownList>
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="style26" colspan="2">
                            &nbsp;</td>
                        <td class="style27">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="style56" colspan="3">
    
    
        TRANSACT MINIMUM GUIDANCE VALUE</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    </table>
            </td>
            <td align="center" colspan="4" class="style23" >
                                <asp:TextBox ID="txtDetails" runat="server" Height="99px" 
                                    style="margin-left: 0px" Width="619px" AutoPostBack="True" Enabled="FALSE" 
                                    Font-Bold="True" Font-Italic="True" Font-Names="Times New Roman" 
                                    Font-Size="Larger" TextMode="MultiLine"></asp:TextBox>
                                <br />
            </td>
        </tr>
        <tr>
            <td class="style36" colspan="2">
                &nbsp;<span class="style48">Area(Choose and Enter any One)</span><br 
                    class="style35" />
            </td>
            <td class="style37">
            </td>
            <td class="style42" colspan="3">
            </td>
        </tr>
        <tr>
            <td class="style28" colspan="2">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:RadioButton 
                    ID="RadioButton1" runat="server" Text="Hectar" AutoPostBack="True" 
                    GroupName="Area" />
                &nbsp;<asp:RadioButton ID="RadioButton2" runat="server" Text="Acre" 
                    AutoPostBack="True" GroupName="Area" />
                &nbsp;<asp:RadioButton ID="RadioButton3" runat="server" Text="Square Feet" 
                    AutoPostBack="True" GroupName="Area" />
                <br class="style35" />
            </td>
            
            
            
            
            
            <td class="style46">
                Computed Min Guidance Value Rs</td>
            <td class="style63" colspan="3">
                <asp:TextBox ID="txtComp" runat="server" 
                    Width="112px" Enabled="false"></asp:TextBox>
            </td>
            
            
            
            
            
        </tr>
        <tr>
        
        
        
            <td class="style59">
                <span class="style40">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Hectar
                </span>
                  
                <br class="style38" />
            </td>
            <td class="style53">
                <asp:TextBox ID="txtHectar" runat="server" Width="112px" AutoPostBack="True" 
                    Enabled="false" style="height: 22px"></asp:TextBox>
                  
                </td>
        
        
        
            <td class="style62">
                <span lang="en-us"> Actual Min Guidance Value Rs</span></td>
            <td class="style63" colspan="3">
                <asp:TextBox ID="txtAct" runat="server" 
                    Width="112px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style59">
                Acre&nbsp;</td>
            <td class="style58">
                <asp:TextBox ID="txtAcre" runat="server" Width="112px" AutoPostBack="True" 
                    Enabled="false" height="22px"></asp:TextBox>
            </td>
            <td class="style62">
                Registration Fee Rs</td>
            <td class="style61" colspan="3">
                <asp:TextBox ID="txtReg" runat="server" Width="112px" Enabled="false"></asp:TextBox>
                <asp:CheckBox ID="cbHome" runat="server" Text="Home Visit" 
                    AutoPostBack="True" />
            </td>
        </tr>
        <tr>
            <td class="style59">
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                Square Feet</td>
            <td class="style58">
                <asp:TextBox ID="txtSqrfeet" runat="server" Width="112px" 
                    AutoPostBack="True" Enabled="false" height="22px"></asp:TextBox>
            </td>
            <td class="style62">
                <span lang="en-us">S</span>tamp Value<span lang="en-us"> </span>Rs</td>
            <td class="style67">
                <asp:TextBox ID="txtstmp" 
                    runat="server" Width="112px" Enabled="false"></asp:TextBox>
                <asp:RadioButton ID="rbNonMunicipal" runat="server" GroupName="Stamp" 
                    Text="Non-Municipal" AutoPostBack="True" />
                <span lang="en-us">&nbsp;</span><asp:RadioButton ID="rbMunicipal" runat="server" GroupName="Stamp" 
                    Text="Municipal" AutoPostBack="True" />
            </td>
            <td class="style61">
                &nbsp;</td>
            <td class="style57">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style71">
                &nbsp;</td>
            <td class="style64">
                &nbsp;</td>
            <td class="style70">
                <asp:Button ID="Button1" runat="server" Text="Calculate Consideration Value" 
                    Width="211px" />
                </td>
            <td class="style57" colspan="3">
                <span lang="en-us">&nbsp;&nbsp;&nbsp; </span>
                <asp:Button ID="Button2" runat="server" Text="Reset...." Width="92px" />
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
                
            <br />
        
   
    </form>
    </body>
</html>
