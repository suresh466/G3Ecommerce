<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Cart.aspx.cs" Inherits="G3Ecommerce.Cart" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <h2>Your Cart</h2>
        <hr />
<asp:ListView ID="lstCart" runat="server"  ItemPlaceholderID="itemPlaceholder" OnItemDataBound="lstCart_ItemDataBound">
           <ItemTemplate>
    <div class="list-group-item m-2">
        <div class="row">
            <div class="col-md-2">
                <img src='<%# Eval("FoodItem.ItemImage") %>' class="img-thumbnail" alt='<%# Eval("FoodItem.ItemName") %>' style="max-width: 100px; max-height: 100px;" />
            </div>
            <div class="col-md-4">
                <h5 class="mt-1"><%# Eval("FoodItem.ItemName") %></h5>
                <p><%# Eval("FoodItem.ItemDescription") %></p>
            </div>
            <div class="col-md-2">
                <p class="mt-1"><strong>Price:</strong> <%# Eval("FoodItem.Price", "{0:C}") %></p>
                <p><strong>Quantity:</strong> <%# Eval("Quantity") %></p>
            </div>
            <div class="col-md-4 text-right">
    <asp:Button ID="btnRemove" runat="server" Text="Remove" CommandArgument='<%# Eval("FoodItem.Id") %>' OnClick="btnRemove_Click" />
</div>

        </div>
    </div>
</ItemTemplate>
            <EmptyDataTemplate>
                <div class="list-group-item">Your cart is empty</div>
            </EmptyDataTemplate>
            <LayoutTemplate>
                <div id="itemPlaceholder" runat="server"></div>
            </LayoutTemplate>
        </asp:ListView>
        <div class="mt-3 text-right">
            <asp:Button ID="btnEmpty" runat="server" Text="Empty Cart"  OnClick="btnEmpty_Click" />
            <asp:Button ID="btnCheckOut" runat="server" Text="Proceed to Checkout"  OnClick="btnCheckOut_Click" />
        </div>
        <asp:Label ID="lblMessage" runat="server"  EnableViewState="False"></asp:Label>
    </div>
</asp:Content>
