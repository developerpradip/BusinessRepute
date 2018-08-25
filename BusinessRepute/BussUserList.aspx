<%@ Page Title="Business Reputation" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="BussUserList.aspx.cs" Inherits="BusinessRepute.BussUserList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="StyleSection" runat="server">
    <link href="Content/style.css" rel="stylesheet" type="text/css" />
    <script type='text/javascript'>
        function AddConsultNotes(objLink) {
            var row = objLink.parentNode.parentNode;
            var pname = row.childNodes[2].innerHTML;
            var lastConsultDate = row.childNodes[14].innerHTML;
            var lastNotes = row.childNodes[17].innerHTML;
            var coordinatorId= row.childNodes[12].innerHTML;
            var doctorId= row.childNodes[13].innerHTML;
            var patientId= row.childNodes[1].innerHTML;
            var treatmentId = row.childNodes[11].innerHTML; 
    
            $('#lblPatientName').html(pname);
            $('#lblLastConsultDate').html(lastConsultDate);
            $('#lblLastConsultNotes').html(lastNotes);
            $('#ContentSection_hdnDoctorId').val(doctorId);
            $('#ContentSection_hdnCoordinatorId').val(coordinatorId);
            $('#ContentSection_hdnPatientId').val(patientId);
            $('#ContentSection_hdnTreatmentId').val(treatmentId);
            $('#ContentSection_hdnLastFollowupDate').val(lastConsultDate);

            $('#ConsultModal').modal('show')
            return true;
        }
        </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentSection" runat="server">
    <form id="Form1" runat="server">
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
                        </div>
                    </div>
                </div>
            </div>
        </header>
        <h3> &nbsp; <asp:Label  ID="Label1" runat="server"  Text=" Business Users List" /></h3>
    
        <div class="error-display">
            <div class="container">
                <div class="row">
                    <asp:Panel id="divMessageDisplay" role="alert" runat="server" >
                        <asp:Label runat="server" ID="FailureText" Text="" />
                    </asp:Panel>
                </div>
            </div>
        </div>
        <div class="row">  
            <div class="col-lg-10 ">  
                <div class="table-responsive">  
                    <asp:GridView ID="gridUserList" runat="server" AutoGenerateColumns="false" CellPadding="2"  GridLines ="None"  Width="100%" AllowPaging="true" 
                        PageSize="5" AllowSorting="true" ForeColor="#333333" OnRowCommand="gridUserList_RowCommand" OnRowDataBound="gridUserList_RowDataBound" 
                        OnPageIndexChanging="gridUserList_PageIndexChanging" AlternatingRowStyle-BackColor="#CCCCCC" AlternatingRowStyle-BorderColor="#CCCCCC" 
                        PagerStyle-HorizontalAlign="Center" PagerStyle-VerticalAlign="Middle" ShowHeaderWhenEmpty="True" CssClass="table">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:BoundField DataField="UserId" HeaderText="Id"  >
                                </asp:BoundField>
                                
                                <%--<asp:BoundField HeaderText="Name" DataField="Username"  >
                                    <ControlStyle Font-Bold="True" /> 
                                    <ItemStyle Font-Bold="True" ForeColor="Blue" Font-Size="9pt" />
                                </asp:BoundField>--%>

                                <asp:BoundField HeaderText="FirstName"  DataField="FirstName" >
                                    <ControlStyle Font-Bold="True" /> 
                                </asp:BoundField>
                                <asp:BoundField HeaderText="LastName"  DataField="LastName" >
                                    <ControlStyle Font-Bold="True" /> 
                                </asp:BoundField>

                               <asp:BoundField HeaderText="Email" DataField="EmailId" >
                                    <ControlStyle Font-Bold="True" />
                                </asp:BoundField>
                               <asp:BoundField HeaderText="Phone" DataField="PhonenUmber" >
                                    <ControlStyle Font-Bold="True" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Role" DataField="BusinessRoleName" >
                                    
                                    <ControlStyle Font-Bold="True" />
                                </asp:BoundField>
                                
                                
                                <asp:TemplateField  >
                                    <ItemTemplate>
                                        <%--ADD THE View LINK BUTTON--%>
                                        <asp:LinkButton ID="lnkView" HeaderText="View"  name="lnkView" Runat="server"  class="btn btn-primary" 
                                            OnClientClick="return EditUserInfo(this)" CommandArgument ="<%# ((GridViewRow) Container).RowIndex %>"  CommandName="Edit">  

                                            <span class="glyphicon glyphicon-tag" aria-hidden="true"></span> Edit</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                            </Columns>  
                           
                            <EditRowStyle BackColor="#999999" />
                                    <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True"/>
                                    <HeaderStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
                                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                    </asp:GridView>
                </div>
            </div>
            
        </div>
        <div class="row">
            </div>
        <%--<div class="patient-details">
            <div class="container">
        --%>        <div class="row">
                    <div class="col-sm-2">
                        <asp:button class="btn btn-primary btn-sm btn-block" type="Add User" runat="server" Text="Add User" ID="btnAddUser" OnClick="btnAddUser_Click"></asp:button>
                    </div>
                </div>
                <div class="row"><span><label> </label></span></div>
            <%--</div>
        </div>--%>   
    </form>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ScriptSection" runat="server">
</asp:Content>
