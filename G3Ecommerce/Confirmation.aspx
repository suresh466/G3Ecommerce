<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Confirmation.aspx.cs" Inherits="G3Ecommerce.Confirmation" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <p>Your Order is being processed. Delicious food is on the way. Why not browse our menu meanwhile?</p>
        <asp:Button runat="server" ID="BrowseMenu" Text="Browse Our Menu" PostBackUrl="~/Items.aspx"/>
    </div>
</asp:Content>