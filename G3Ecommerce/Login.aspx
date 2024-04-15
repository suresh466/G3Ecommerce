<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Login.aspx.cs" Inherits="G3Ecommerce.Login" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <div>
            <asp:TextBox ID="txtEmail" runat="server" placeholder="Email" /><br />
            <asp:TextBox ID="txtUsername" runat="server" placeholder="Username" /><br />
            <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" />
        </div>
</asp:Content>
