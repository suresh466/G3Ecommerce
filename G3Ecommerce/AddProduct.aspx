<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="AddProduct.aspx.cs" Inherits="G3Ecommerce.AddProduct" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <h2>Add Product</h2>
        <asp:Label ID="lblMessage" runat="server" Visible="false" ForeColor="Red"></asp:Label>
        <br />
        <asp:DropDownList ID="ddlCategories" runat="server" AppendDataBoundItems="True">
            <asp:ListItem Text="Select Category" Value="-1"></asp:ListItem>
        </asp:DropDownList>
        <br />
        <asp:TextBox ID="txtProductName" runat="server" placeholder="Product Name"></asp:TextBox>
        <br />
        <asp:TextBox ID="txtProductPrice" runat="server" placeholder="Product Price"></asp:TextBox>
        <br />
        <asp:Button ID="btnAddProduct" runat="server" Text="Add Product" OnClick="btnAddProduct_Click" />
        <br />
        <br />
        <asp:HyperLink ID="lnkBack" runat="server" NavigateUrl="~/AdminDashboard.aspx" Text="Back to Dashboard"></asp:HyperLink>
    </div>
</asp:Content>
