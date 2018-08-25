<%@ Page Title="Business Reputation Login" Language="C#"  MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="BusinessRepute.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="StyleSection" runat="server">
    <link href="Content/bootstrap-slider.min.css" rel="stylesheet" type="text/css" />
    <link href="Content/style.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        label {
            display: inline;
            font-size: 1.0em;
            font-weight: 500;
            padding: 0px;
            margin-left:-30px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentSection" runat="server">
    <header>
        <h3> &nbsp; <asp:Label  ID="lblheader" runat="server"  Text="Business Reputation Login" /></h3>
    </header>
    <form id="Form1" runat="server">
        <div class="error-display">
            <div class="container">
                <div class="row">
                    <asp:Panel id="divMessageDisplay" role="alert" runat="server" >
                        <asp:Label runat="server" ID="FailureText" Text="" />
                    </asp:Panel>
                </div>
            </div>
        </div>
        

        <div class="login-form">
            
            <div class="form-group">
                <h4>Please sign in</h4>
            </div>
            <div class="form-group">
                <asp:TextBox ID="txtUssername" runat="server" type="text" class="form-control" Width="190px" placeholder="Username" required="required" />
            </div>
            <div class="form-group">
                <asp:TextBox TextMode="Password"  ID="txtPassword" runat="server" type="password" Width="190px" class="form-control" placeholder="Password" required="required"/>
            </div>
            <div class="form-group">
                <asp:CheckBox ID="chkRememberMe" style="margin-left:-40px" align="left" type="checkbox" textAlign="right"  runat="server" name="chkRememberMe" Width="170px"  Text="Remember Me"> </asp:CheckBox>
            </div>
            <div class="form-group">
                <asp:Button ID="btnLogin"  Text="Login" runat="server" Class="btn btn-lg btn-primary " OnClick="btnLogin_Click" /> 
                 
            </div>
            <div class="form-group">
                <a href="ResetPassword.aspx" class="pull-left" ><span class="glyphicon glyphicon-cd" aria-hidden="true"></span>Forgot Password?</a>
            </div>
        </div>
        
    
    </form>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ScriptSection" runat="server">
</asp:Content>