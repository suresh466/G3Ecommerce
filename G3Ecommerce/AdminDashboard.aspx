<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="AdminDashboard.aspx.cs" Inherits="G3Ecommerce.AdminDashboard" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <h2>Admin Dashboard</h2>
        <asp:Button ID="btnLogout" runat="server" Text="Logout" OnClick="btnLogout_Click" CssClass="btn btn-danger" />
        <h3>Products</h3>
        <asp:Button CssClass="btn btn-primary" ID="addProduct" runat="server" Text="Add Product" PostBackUrl="~/AddProduct.aspx"/>
        <asp:GridView ID="gvProducts" runat="server" AutoGenerateColumns="False" CssClass="gridview" DataSourceID="dsProducts">
            <Columns>
                <asp:BoundField DataField="item_id" HeaderText="Product ID" />
                <asp:BoundField DataField="item_name" HeaderText="Product Name" />
                <asp:BoundField DataField="item_price" HeaderText="Price" />
                <asp:BoundField DataField="category_id" HeaderText="Category" />
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="dsProducts" runat="server" ConnectionString="<%$ ConnectionStrings:g3ecommerce %>"
            SelectCommand="SELECT item_id, item_name, item_price, category_id FROM Items">
        </asp:SqlDataSource>

        <h3>Categories</h3>
        <asp:GridView ID="gvCategories" runat="server" AutoGenerateColumns="False" CssClass="gridview" DataSourceID="dsCategories">
            <Columns>
                <asp:BoundField DataField="category_id" HeaderText="Category ID" />
                <asp:BoundField DataField="category_name" HeaderText="Category Name" />
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="dsCategories" runat="server" ConnectionString="<%$ ConnectionStrings:g3ecommerce %>"
            SelectCommand="SELECT category_id, category_name FROM Categories">
        </asp:SqlDataSource>

        <h3>Orders</h3>
        <asp:GridView ID="gvOrders" runat="server" AutoGenerateColumns="False" CssClass="gridview" DataSourceID="dsOrders">
            <Columns>
                <asp:BoundField DataField="order_id" HeaderText="Order ID" />
                <asp:BoundField DataField="customer_id" HeaderText="Customer ID" />
                <asp:BoundField DataField="total_amount" HeaderText="Total Amount" />
                <asp:BoundField DataField="order_status" HeaderText="Order Status" />
                <asp:BoundField DataField="payment_method" HeaderText="Payment Method" />
                <asp:BoundField DataField="delivery_address" HeaderText="Delivery Address" />
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="dsOrders" runat="server" ConnectionString="<%$ ConnectionStrings:g3ecommerce %>"
            SelectCommand="SELECT * FROM Orders">
        </asp:SqlDataSource>
    </div>
</asp:Content>
