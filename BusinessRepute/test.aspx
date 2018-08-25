<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="test.aspx.cs" Inherits="BusinessRepute.test" %>
<asp:Content ID="Content1" ContentPlaceHolderID="StyleSection" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentSection" runat="server">
    <form runat="server">
        <div class="container">
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
                <div class="col-sm-2">
                    <asp:Button Text="Send Test Email" ID ="btnSendEmail" runat="server" class="btn-lg btn-primary" width="180px"  OnClick="btnSendEmail_Click" />

                </div>
                <div class="col-sm-2">
                    <input id="btnAdd" type="button" class="btn-lg btn-danger" value="Add Div"  width="200px" />
                </div>
                
            </div>
            <div class="row">
                <div class="col-sm-5">
                </div>
            </div>
        </div>
        <div id="TextBoxContainer">

        </div>
       <p></p>
        <asp:Panel id="pnlLoginBox" runat="server" class="container">
            <div class="row">
                <div class="col-sm-1">
                    <asp:Label id="lblUserName" runat="server">User Name :</asp:Label>
                </div>
                <div class="col-sm-2">
                    <asp:TextBox CssClass="form-control" id="txtUserName" placeholder="User name" runat="server"  TextMode="SingleLine" width="200px"  />
                </div>
            </div>
            <div class="row">
                <div class="col-sm-1">
                    <asp:Label id="Label1" runat="server">Password</asp:Label>
                </div>
                <div class="col-sm-2">
                    <asp:TextBox CssClass="form-control" id="txtPassword" runat="server" placeholder="Password" TextMode="Password" width="200px"   />
                </div>
            </div>
            <div class="row">
                <div class="col-sm-3">
                    <asp:Button id="btnLogin"  class="btn-sm btn-warning" runat="server" Text="Login" OnClick="btnLogin_Click"></asp:Button>
                    <asp:HiddenField id="hdnAuthenticated"  runat="server" ></asp:HiddenField>
                </div>
            </div>
        </asp:Panel>
         
        <asp:Panel id="pnlSQlBox" runat="server">
            <div class="row">
                <div class="col-sm-12">
                        <asp:TextBox CssClass="form-control" id="txtSQlQuery" runat="server" placeholder="Sql query"  TextMode="MultiLine" width="800px" />
                </div>
            </div>
            <div class="row">
                 <div class="col-sm-12">
                    <asp:Button id="btnGet" type="button" value="Get Values"  class="btn btn-success" Text="Run Query" runat="server" width="100px" OnClick="btnGet_Click"  />
                    
                </div>  
            </div>
            <div class="row">
                <div class="col-sm-12">
                    <asp:GridView ID="gridPatientList" runat="server" AutoGenerateColumns="true" CellPadding="2"  GridLines ="None"  Width="100%" AllowPaging="True" 
                        AllowSorting="false" ForeColor="#333333" AlternatingRowStyle-BackColor="#CCCCCC" AlternatingRowStyle-BorderColor="#CCCCCC"  CssClass="table table-bordered" 
                        EmptyDataText="Patient record is not available." RowHeaderColumn="TreatmentId" EnableSortingAndPagingCallbacks="false" AllowCustomPaging="false" 
                        OnPageIndexChanging="gridPatientList_PageIndexChanging">
                        <pagerstyle cssclass="gridview" />
                    </asp:GridView>
                </div>
            </div>
            
         </asp:Panel>
    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptSection" runat="server">
    <script type="text/javascript">
        $(function () {
            $("#btnAdd").bind("click", function () {
                var div = $("<div />");
                div.html(GetDynamicTextBox("pradip") + "<br /><br />");
                $("#TextBoxContainer").append(div);
            });
            $("#btnGet").bind("click", function () {
                var values = "";
                $("input[name=DynamicTextBox]").each(function () {
                    values += $(this).val() + "\n";
                });
                alert(values);
            });
            $("body").on("click", ".remove", function () {
                $(this).closest("div").remove();
            });
        });
        function GetDynamicTextBox(value) {
            return '<input name = "DynamicTextBox" type="text" value = "' + value + '" />&nbsp;' +
            '<input type="button" value="Remove" class="remove" />'
        }
    </script>

</asp:Content>
