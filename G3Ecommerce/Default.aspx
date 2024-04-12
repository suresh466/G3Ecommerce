<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="G3Ecommerce._Default" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <!-- Hero Section -->
    <section class="hero-section text-white py-5">
        <div class="container text-center">
            <h1 class="display-4 mb-4">Special Offers!</h1>
            <p class="lead">Check out our exclusive deals and discounts.</p>
            <a href="#" class="btn btn-primary btn-lg">Order Now</a>
        </div>
    </section>

    <!-- Categories Section -->
    <section class="categories-section py-5">
        <div class="container">
            <h2 class="text-center mb-4">Browse Categories</h2>
            <div class="row">
                <!-- Category Cards -->
                <div class="row" id="categoryPlaceholder" runat="server">
    </div>
            </div>
        </div>
    </section>

    <section class="todays-special-section py-5">
        <div class="container">
            <h2 class="text-center mb-4">Today's Specials</h2>
        </div>
    </section>
</asp:Content>
