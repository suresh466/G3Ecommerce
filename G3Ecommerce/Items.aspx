<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Site.Master" CodeBehind="Items.aspx.cs" Inherits="G3Ecommerce.Items" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize="10">
        <Columns>
            <asp:BoundField DataField="id" HeaderText="Item ID" />
            <asp:BoundField DataField="ItemName" HeaderText="Item Name" />
            <asp:BoundField DataField="Price" HeaderText="Price" />
        </Columns>
    </asp:GridView>
</asp:Content>
