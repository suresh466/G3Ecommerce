<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Items.aspx.cs" Inherits="G3Ecommerce.Items" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <asp:Repeater ID="ItemsRepeater" runat="server">
            <ItemTemplate>
                <div class="col-lg-4 col-md-6 mb-4">
                    <div class="card h-100">
<img class="card-img-top" src='<%# Eval("ItemImage") %>' alt='<%# Eval("ItemName") %>' style="width: auto; max-height: 200px;">
                        <div class="card-body">
                            <h4 class="card-title"><%# Eval("ItemName") %></h4>
                            <h5><%# Eval("Price", "{0:C}") %></h5>
                            <p class="card-text"><%# Eval("ItemDescription") %></p>
                            <asp:TextBox ID="txtQuantity" runat="server" CssClass="form-control mb-2" Text="1" type="number" min="1" max="10"></asp:TextBox>
                            <asp:Button ID="btnAddToCart" runat="server" Text="Add to Cart" CssClass="btn btn-primary" OnClick="AddToCart_Click" CommandArgument='<%# Eval("Id") %>' />
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Content>
