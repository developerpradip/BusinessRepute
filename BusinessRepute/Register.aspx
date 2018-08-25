<%@ Page Title="Business Reputation User Registration" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="BusinessRepute.Register" %>
<asp:Content ID="Content1" runat="server" contentplaceholderid="StyleSection">
    <link href="Content/style.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentSection" runat="server">
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
                            <asp:HiddenField id="HiddenField1" runat="server" />
                            <asp:HiddenField id="hdnEditUserID" runat="server" />
                        </div>
                    </div>
                </div>
            </div>
        </header>
            <%--<h3> &nbsp; <asp:Label  ID="lblClientName" runat="server"  Text="  Business signup" /></h3>
    </header>--%>
    
    
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
                    <h4 class="mb-3">Business Details: </h4>
                    <div class="row">
                        <div class="col-sm-4">
                            <div class="form-group">
                                <label for="txtBusinessName" ><small class="text-muted">Business Name: </small></label>
                                <asp:TextBox CssClass="form-control" id="txtBusinessName" width="200px" runat="server" required="" autofocus=""/>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-4">
                            <div class="form-group">
                                <label for="cmbBusinessType" ><small class="text-muted">Business Type: </small></label>
                                <asp:DropDownList CssClass="form-control" id="cmbBusinessType" width="200px" runat="server" autofocus=""/>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <asp:panel class="form-group" runat="server" ID="pnlLogoInfo">
                                <label for="txtBusinessName" ><small class="text-muted">Business Logo: </small></label>
                                <asp:FileUpload ID="flupldClientLogo" runat="server" type="file" CssClass="form-control" Width="250px" autofocus="" aria-describedby="businessLogoHelp"/>
                                <small id="businessLogoHelp" class="text-muted">Upload your business Logo.</small>
                            </asp:panel>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-4">
                            <div class="form-group">
                                <label for="txtBusinessAddress1" ><small class="text-muted">Business Address: </small></label>                         
                                    <asp:TextBox CssClass="form-control"  width="185px" id="txtBusinessAddress1" runat="server"  required="" autofocus=""/>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label for="txtBusinessAddress2" ><small class="text-muted">Business Address 2: </small></label>
                                <asp:TextBox CssClass="form-control" id="txtBusinessAddress2" Width="200px" runat="server"/>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-4">
                            <div class="form-group">
                                <label for="txtBusinessPhone" ><small class="text-muted">Business Phone: </small></label>
                                <asp:TextBox CssClass="form-control" id="txtBusinessPhone" Width="190px" TextMode="Phone" runat="server" required="" autofocus=""/>
                                <asp:RegularExpressionValidator Display="Dynamic" ValidationGroup="ValidationGroup1" ID="RegularExpressionValidator4" runat="server" ErrorMessage="Phone Number is not valid" ControlToValidate="txtBusinessPhone" CssClass="field-validation-error" ValidationExpression="[0-9+-]*"></asp:RegularExpressionValidator>
                            </div>
                        </div>
                        <%--<div class="col-sm-3">
                            <div class="form-group">
                                <label for="txtBusinessFax" ><small class="text-muted">Business Fax : </small></label>
                                <asp:TextBox CssClass="form-control" id="txtBusinessFax" runat="server" number=""/>
                                <asp:RegularExpressionValidator ValidationGroup="ValidationGroup1" ID="RegularExpressionValidator2" runat="server" ErrorMessage="Fax Number is not valid" ControlToValidate="txtBusinessFax" CssClass="field-validation-error" ValidationExpression="[0-9+]*"></asp:RegularExpressionValidator>
                            </div>
                        </div>--%>
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label for="txtBusinessEmail" ><small class="text-muted">Business Email: </small></label>

                                <asp:TextBox CssClass="form-control" id="txtBusinessEmail" runat="server"  Width="225px" type="email" required="" autofocus="" aria-describedby="emailHelp"/>
                                <small id="emailHelp" class="form-text text-muted">We'll never share your email with anyone else.</small>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-3">
                            <div class="form-group">
                                <label for="txtBussCity" ><small class="text-muted">City: </small></label>
                                <asp:TextBox CssClass="form-control" id="txtBussCity" runat="server" required="" autofocus=""/>
                            </div>
                        </div>
                        <div class="col-sm-3">
                            <div class="form-group">
                                <label for="txtBussState" ><small class="text-muted">State: </small></label>
                                <asp:TextBox CssClass="form-control" id="txtBussState" runat="server" />
                            </div>
                        </div>
                        <div class="col-sm-3">
                            <div class="form-group">
                                <label for="txtBusinessZip" ><small class="text-muted">Zip code: </small></label>

                                <asp:TextBox CssClass="form-control" id="txtBusinessZip" runat="server" width="90px" TextMode="Number" required="" autofocus="" aria-describedby="emailHelp"/>
                            </div>
                        </div>
                    </div>
                    

                    
                </div>
            </div>
            <asp:Panel class="register-form" runat="server" ID="pnlUserDetails" >
                <div class="register-form-field">
                    <h4 class="mb-3" >Business User Details</h4>
                    <%--<div class="row" >
                        <div class="col-sm-3">
                            <div class="form-group">
                                <label for="txtBussOwnerFirstname" class="col-form-label-sm" ><small class="text-muted">First Name: </small></label> 
                                <asp:TextBox CssClass="form-control form-control-sm" id="txtBussOwnerFirstname" runat="server" required="" autofocus="" />
                            </div>
                        </div>
                        <div class="col-sm-3">
                            <div class="form-group">
                                <label for="txtBussOwnerLastname" class="col-form-label-sm"><small class="text-muted">Last Name: </small></label>
                                <asp:TextBox CssClass="form-control form-control-sm" id="txtBussOwnerLastname" runat="server" required="" autofocus="" />
                            </div>
                        </div>

                        <div class="col-sm-3">
                            <div class="form-group">
                                <label for="txtBussOwnerLastname" class="col-form-label-sm"><small class="text-muted">Role: </small></label>
                                <asp:DropDownList CssClass="form-control form-control-sm" width="150px" id="cmbRoleType" runat="server" required="" autofocus="" />
                            </div>
                        </div>
                    </div>--%>
               
                    <asp:panel class="row" runat="server" ID="pnlLoginInfo">
                        <div class="col-sm-3">
                            <div class="form-group">
                                <label for="txtUserName" ><small class="text-muted">Login Name: </small></label>
                                <asp:TextBox CssClass="form-control" id="txtUserName" runat="server"  required="" autofocus="" />
                                        
                            </div>
                        </div>
                        <div class="col-sm-3">
                            <div class="form-group">
                                <label for="txtPassword" ><small class="text-muted">Password : </small></label>
                                <asp:TextBox CssClass="form-control" id="txtPassword" type="password" runat="server" required="" autofocus="" />
                                <asp:RegularExpressionValidator Display="Dynamic" ValidationGroup="ValidationGroup1" ID="RegularExpressionValidator1" runat="server" ErrorMessage="Password should contain minimum 8 characters, at least 1 Alphabet and 1 Number ."  ControlToValidate="txtPassword" CssClass="field-validation-error" ValidationExpression="^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$"></asp:RegularExpressionValidator>
                                <%--<small id="smallPassword" class="form-text text-muted">Password must be 8-20 characters long and contain letters and numbers .</small>--%>
                            </div>
                        </div>
                        <div class="col-sm-3">
                            <div class="form-group">
                                <label for="txtConfirmPassword" ><small class="text-muted">Confirm Password: </small></label>
                                <asp:TextBox CssClass="form-control" id="txtConfirmPassword" type="password" runat="server" required="" autofocus="" />
                                <asp:CompareValidator  Display="Dynamic" ValidationGroup="ValidationGroup1" ID="CompareValidator1" runat="server" ControlToValidate="txtConfirmPassword" ControlToCompare="txtPassword"  CssClass="field-validation-error" ErrorMessage="Password and Confirm password are not matching." />
                            </div>
                        </div>
                    </asp:panel>
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
            </asp:Panel>
            
                        
        <div class="patient-details">
            <div class="container">
                <div class="row">
                    <div class="col-sm-2">
                        <asp:button class="btn btn-primary btn-lg btn-block" type="submit" runat="server" Text="Save" ID="btnSave" OnClick="btnSave_Click"></asp:button>
                    </div>
                    <div class="col-sm-2">
                        <asp:button class="btn btn-default btn-lg btn-block" type="reset" runat="server" Text="Reset"></asp:button>
                    </div>
                    <asp:HiddenField ID="hdnBusinessDetailsID" runat="server" />
                </div>
                <div class="row"><span><label> </label></span></div>
            </div>
        </div>    
    </form>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ScriptSection" runat="server">
</asp:Content>