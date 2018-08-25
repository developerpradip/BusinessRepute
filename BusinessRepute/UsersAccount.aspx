<%@ Page Title="Business Reputation User Activation" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="UsersAccount.aspx.cs" Inherits="BusinessRepute.UsersAccount" %>
<asp:Content ID="Content1" runat="server" contentplaceholderid="StyleSection">
    <link href="Content/style.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentSection" runat="server">
   <header>
            <h3> &nbsp; <asp:Label  ID="lblClientName" runat="server"  Text="  User Activation" /></h3>
    </header>
    <div>
        <h4> <asp:Label  ID="lblResponse" runat="server"  Text="  " /></h4>
    </div>
</asp:Content>
