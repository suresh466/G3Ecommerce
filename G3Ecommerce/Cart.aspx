<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Cart.aspx.cs" Inherits="G3Ecommerce.Cart" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <h2>Your Cart</h2>
        <hr />
        <asp:ListBox ID="lstCart" runat="server" CssClass="list-group"></asp:ListBox>
        <div class="mt-3 text-right">
            <asp:Button ID="btnRemove" runat="server" Text="Remove Item" CssClass="btn btn-primary mr-2" OnClick="btnRemove_Click" />
            <asp:Button ID="btnEmpty" runat="server" Text="Empty Cart" CssClass="btn btn-danger mr-2" OnClick="btnEmpty_Click" />
            <asp:Button ID="btnCheckOut" runat="server" Text="Proceed to Checkout" CssClass="btn btn-success" OnClick="btnCheckOut_Click" />
        </div>
        <asp:Label ID="lblMessage" runat="server" CssClass="text-danger mt-3" EnableViewState="False"></asp:Label>
    </div>
</asp:Content>
