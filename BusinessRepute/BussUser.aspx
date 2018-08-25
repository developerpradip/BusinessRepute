<%@ Page Title="Business User" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="BussUser.aspx.cs" Inherits="BusinessRepute.BussUser" %>
<asp:Content ID="Content1" ContentPlaceHolderID="StyleSection" runat="server">
    <link href="Content/style.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentSection" runat="server">
   <form id="Form1"  runat="server" >
    <header>
            <div class="container">

                <div class="row">
                    <div class="col-sm-4 mobile-show">
                        <div class="brand">
                            <asp:Image ID="imgClientLogo" ImageUrl="Images/BusinessReputeLogo.png" runat="server" ToolTip="Client Logo" />
                        </div>
                    </div>
                    <div class="col-sm-4">
                        <h2><asp:Label  ID="lblClientName" runat="server"  Text="Business Reputation"> </asp:Label><span style="color: #fff; font-size: 12px;"> Presents</span>
                        
                        </h2>
                            <asp:Label  ID="lblClientAddress" runat="server" Font-Bold="true">
                                <h5>
                                <asp:Label  ID="lblHeaderAddress1" runat="server"  >Pune</asp:Label><br />
                                <asp:Label  ID="lblHeaderAddress2" runat="server"  >MS India</asp:Label><br />
                                <asp:Label  ID="lblHeaderConatct" runat="server"  >Tel(982) 340-4248</asp:Label><br />
                                <asp:Label  ID="lblHeaderEmail" runat="server"  >Email:admin@BusinessRepute.xyz</asp:Label><br />
                                                                 
                                </h5>
                            </asp:Label>
                    </div>
                    <div class="col-sm-4 mobile-hide">
                        <div class="brand">
                            <asp:Image ID="imgClientLogo1" ImageUrl="Images/BusinessReputeLogo.png" runat="server" ToolTip="Client Logo" />  
                            <asp:HiddenField id="hdnBusinessDetailsID" runat="server" />
                            <asp:HiddenField id="hdnEditUserID" runat="server" />
                        </div>
                    </div>
                </div>
            </div>
        </header>
            <h3> &nbsp; <asp:Label  ID="label1" runat="server"  Text="  Business User" /></h3>
            <div class="error-display">
                <div class="container">
                    <div class="row">
                        <asp:Panel id="divMessageDisplay" role="alert" runat="server" >
                            <asp:Label runat="server" ID="FailureText" Text="" />
                        </asp:Panel>
                    </div>
                </div>
            </div>
            <div class="register-form">
                <div class="register-form-field">
                    <h4 class="mb-3" >User Profile</h4>
                    <div class="row">
                        <div class="col-sm-3">
                            <div class="form-group">
                                <label for="txtUserFirstname" class="col-form-label-sm" ><small class="text-muted">First Name: </small></label> 
                                <asp:TextBox CssClass="form-control form-control-sm" id="txtUserFirstname" runat="server" required="" autofocus="" />
                            </div>
                        </div>
                        <div class="col-sm-3">
                            <div class="form-group">
                                <label for="txtUserLastname" class="col-form-label-sm"><small class="text-muted">Last Name: </small></label>
                                <asp:TextBox CssClass="form-control form-control-sm" id="txtUserLastname" width="165px" runat="server" required="" autofocus="" />
                            </div>
                        </div>
                        <div class="col-sm-3">
                            <div class="form-group">
                                <label for="cmbRoleTypes" class="col-form-label-sm"><small class="text-muted">Role: </small></label>
                                <asp:DropDownList CssClass="form-control form-control-sm" width="170px" id="cmbRoleType" runat="server" required="" autofocus="" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-3">
                            <div class="form-group">
                                <label for="txtUserPhone" ><small class="text-muted">Phone No: </small></label>
                                <asp:TextBox CssClass="form-control" id="txtUserPhone" runat="server" required="" type="phone" Width="135px" TextMode="Phone" autofocus=""/>
                                <asp:RegularExpressionValidator Display="Dynamic" ValidationGroup="ValidationGroup1" ID="RegularExpressionValidator4" runat="server" ErrorMessage="Phone Number is not valid" ControlToValidate="txtUserPhone" CssClass="field-validation-error" ValidationExpression="[0-9+()-]*"></asp:RegularExpressionValidator>
                            </div>
                        </div>
                        
                        <div class="col-sm-9">
                            <div class="form-group">
                                <label for="txtUserEmail" ><small class="text-muted">Email: </small></label>

                                <asp:TextBox CssClass="form-control" id="txtUserEmail" runat="server" Width="190px" type="email" required="" autofocus="" aria-describedby="emailHelp"/>
                                <small id="emailHelp" class="form-text text-muted">We'll never share your email with anyone else.</small>
                            </div>
                        </div>
                    </div>
                    
                    <div class="row" >
                        <asp:Panel class="col-sm-9" id="pnlProfilePic" runat="server">
                            <div class="form-group">
                                <label for="txtProfilePicture" ><small class="text-muted">Profile Picture: </small></label>
                                <asp:FileUpload ID="flupldClientPic" runat="server" type="file" CssClass="form-control" Width="250px"  autofocus="" aria-describedby="userProfilePicHelp"/>
                                <small id="userProfilePicHelp" class="text-muted">Upload your Profile picture.</small>
                            </div>
                        </asp:Panel>
                    </div>
                  </div>
               
            </div>
            <%--<div class="register-form"> 
                <div class="register-form-field">
                    <div class="row">
                        <div class="col-sm-3">
                            <div class="form-group">
                                <asp:label for="txtUserName" id="lblUserName" runat="server" class="form-control"><small class="text-muted">User Name: </small></asp:label>
                                <asp:TextBox CssClass="form-control" id="txtUserName" runat="server"  required="" autofocus="" />
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:label for="txtPassword" id="lblPassword" runat="server" ><small class="text-muted">Password: </small></asp:label>
                                <asp:TextBox CssClass="form-control" id="txtPassword" type="password" runat="server" required="" ToolTip="Password should contain minimum 8 characters, At least 1 Alphabet and 1 Number."  autofocus="" aria-describedby="userPasswordHelp" />
                                <asp:RegularExpressionValidator ValidationGroup="ValidationGroup1" ID="RegularExpressionValidator1" runat="server" ErrorMessage="Should contain minimum 8 characters, At least 1 Alphabet and 1 Number."  ControlToValidate="txtPassword" CssClass="field-validation-error" ValidationExpression="^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$"></asp:RegularExpressionValidator>
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:label for="txtConfirmPassword" id="lblConfirmPassword" runat="server" CssClass="form-control"><small class="text-muted">Confirm Password: </small></asp:label>
                                <asp:TextBox CssClass="form-control" id="txtConfirmPassword" type="password" runat="server" required="" autofocus="" />
                                <asp:CompareValidator ValidationGroup="ValidationGroup1" ID="CompareValidator1" runat="server" ControlToValidate="txtConfirmPassword" ControlToCompare="txtPassword"  CssClass="field-validation-error" ErrorMessage="Password and Confirm password are not matching." />
                            </div>
                        </div>
                        <asp:
                            
                    </div>
                    <div class="row">
                        <div class="col-sm-3">
                            <div class="form-group">
                                <label for="txtSecurityQue1" ><small class="text-muted">Security Question 1: </small></label>
                                <asp:TextBox CssClass="form-control" id="txtSecurityQue1" runat="server" required="" autofocus="" />
                            </div>
                        </div>
                        <div class="col-sm-3">
                            <div class="form-group">
                                <label for="txtSecurityAns1" ><small class="text-muted">Answer 1:</small></label>
                                <asp:TextBox CssClass="form-control" id="txtSecurityAns1" runat="server"  required="" autofocus="" />
                            </div>
                        </div>
                            
                    </div>
                    
                    <div class="row">
                        <div class="col-sm-3">
                            <div class="form-group">
                                <label for="txtSecurityQue2" ><small class="text-muted">Security Question 2:</small> </label>
                                <asp:TextBox CssClass="form-control" id="txtSecurityQue2" runat="server" required="" autofocus="" />
                            </div>
                        </div>
                        <div class="col-sm-3">
                            <div class="form-group">
                                <label for="txtSecurityAns2" ><small class="text-muted">Answer 2:</small></label>
                                <asp:TextBox CssClass="form-control" id="txtSecurityAns2" runat="server" required="" autofocus="" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>--%>
            <div class="patient-details">
                <div class="register-form-field">
                <div class="container">
                    <div class="row">
                        <div class="col-sm-2">
                            <asp:button class="btn btn-primary btn-lg btn-block" type="submit" runat="server" Text="Save" ID="btnSave" OnClick="btnSave_Click"></asp:button>
                        </div>
                        <div class="col-sm-2">
                            <asp:button class="btn btn-primary btn-lg btn-block" type="submit" runat="server" Text="Delete User" ID="btnDelete" OnClientClick="return confirm('Do you really want to Delete the user?');"  OnClick="btnDelete_Click"></asp:button>
                        </div>
                        <div class="col-sm-2">
                            <asp:button ID="Reset" class="btn btn-default btn-lg btn-block" type="reset" runat="server" Text="Reset" OnClick="Reset_Click"></asp:button>
                            <asp:HiddenField id="hdnPageMode" runat="server" />
                        </div>
                    </div>
                
                </div>
            </div>
        </div>    
    </form>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ScriptSection" runat="server">
</asp:Content>
