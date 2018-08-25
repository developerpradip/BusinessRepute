<%@ Page Title="Business Reputation Reset Password" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="ResetPassword.aspx.cs" Inherits="BusinessRepute.ResetPassword" %>
<asp:Content ID="Content1" runat="server" contentplaceholderid="StyleSection">
    <link href="Content/style.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentSection" runat="server">
   <header>
            <h3> <asp:Label  ID="lblClientName" runat="server"  Text="  Reset password" /></h3>
    </header>
    
    <form id="Form1"  runat="server">
        <div class="error-display">
            <div class="container">
                <div class="row">
                    <asp:Panel id="divMessageDisplay" role="alert" runat="server" >
                        <asp:Label runat="server" ID="FailureText" Text="" />
                    </asp:Panel>
                </div>
            </div>
        </div>
        <asp:panel runat="server" class="login-form" id="pnlEmail">
            <div class="container">
                <div class="row">
                    <div class="col-sm-3">
                        <div class="form-group">
                            <label for="txtEmail" class="col-form-label-sm" ><small class="text-muted">Registered email Id: </small></label> 
                            <asp:TextBox CssClass="form-control" width="225px" type="email" id="txtEmail" runat="server" required="" autofocus="" />
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-3">
                        <div class="form-group">
                            <asp:Button ID="btnResetPwd" type="button"  class="btn btn-primary" runat="server" TabIndex="4" Text="Forgot Password" OnClick="btnChangePassword_Click" Width="150px" ></asp:Button>
                        </div>
                    </div>
                </div>
            </div>
        </asp:panel>    
        <asp:panel runat="server" class="login-form" id="pnlResetPassword">
            <div class="container">
                <div class="row">
                    <div class="col-sm-2">
                        <div class="form-group">
                            <label for="txtNewPassword" class="col-form-label-sm" ><small class="text-muted">New Password: </small></label> 
                        </div>
                    </div>
                    
                                
                    <div class="col-sm-2">
                        <div class="form-group">
                            <asp:TextBox CssClass="form-control form-control-sm" width="170px" type="password" id="txtNewPassword" runat="server" required="" autofocus="" />
                        </div>
                    </div>
                    <div class="col-sm-4">
                        <asp:RegularExpressionValidator display="Dynamic" ValidationGroup="ValidationGrpResetPwd" ID="RgExpNewPwd" runat="server" ErrorMessage="Password should contain minimum 8 characters, at least 1 Alphabet and 1 Number ."  ControlToValidate="txtNewPassword" CssClass="field-validation-error" ValidationExpression="^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$"></asp:RegularExpressionValidator>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-2">
                        <div class="form-group">
                            <label for="txtConfirmPassword" class="col-form-label-sm" ><small class="text-muted">Confirm Password: </small></label> 
                        </div>
                    </div>
                    <div class="col-sm-2">
                        <div class="form-group">
                            <asp:TextBox CssClass="form-control form-control-sm" width="170px" type="password" id="txtConfirmPassword" runat="server" required="" autofocus="" />
                        </div>
                    </div>
                    <div class="col-sm-4">
                        <asp:CompareValidator display="Dynamic" ValidationGroup="ValidationGrpResetPwd" ID="CmpValConfirmPwd" runat="server" ControlToValidate="txtConfirmPassword" ControlToCompare="txtNewPassword"  CssClass="field-validation-error" ErrorMessage="Password and Confirm password are not matching." />
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-2">
                        <div class="form-group">
                            <asp:Button ID="btnResetPassword" class="btn btn-primary" runat="server" TabIndex="4" Text="Reset Password" OnClick="btnResetPassword_Click" Width="137px" ></asp:Button>
                        </div>
                    </div>
                </div>
            </div>
        </asp:panel>
    </form>       
</asp:Content>
