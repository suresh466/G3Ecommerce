<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Checkout.aspx.cs" Inherits="G3Ecommerce.Checkout" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">


    <main class="container">
        
   
            <h1>Check Out Page</h1>
            <h3>Contact Information</h3>

            <div class="form-group">
                <label class="control-label col-sm-3">Email Address:</label>
                <div class="col-sm-5">
                    <asp:TextBox ID="txtEmail1" runat="server" CssClass="form-control" TextMode="Email"></asp:TextBox>
                </div>
                <div class="col-sm-4">
                    <asp:RequiredFieldValidator ID="rfvEmail1" runat="server" 
                        ErrorMessage="Email is required" CssClass="text-danger" 
                        Display="Dynamic" ControlToValidate="txtEmail1"></asp:RequiredFieldValidator>
                     <asp:CompareValidator ID="cvEmailMatch" runat="server" ControlToCompare="txtEmail1"
            ControlToValidate="txtEmail2" Operator="Equal" Type="String"
            ErrorMessage="Emails do not match" CssClass="text-danger" Display="Dynamic"></asp:CompareValidator>
                </div>
            </div>

            <div class="form-group">
                <label class="control-label col-sm-3">Email Re-entry:</label>
                <div class="col-sm-5">
                    <asp:TextBox ID="txtEmail2" runat="server" CssClass="form-control" TextMode="Email"></asp:TextBox>
                </div>
                <div class="col-sm-4">
                    <asp:RequiredFieldValidator ID="rfvEmail2" runat="server" 
                        ErrorMessage="Email is required" CssClass="text-danger" 
                        Display="Dynamic" ControlToValidate="txtEmail2"></asp:RequiredFieldValidator> 
                </div>
            </div>

            <div class="form-group">
                <label class="control-label col-sm-3">First Name:</label>
                <div class="col-sm-5">
                    <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control"></asp:TextBox>  
                </div>
                <div class="col-sm-4">
                    <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" 
                        ErrorMessage="First name is required" CssClass="text-danger" 
                        Display="Dynamic" ControlToValidate="txtFirstName"></asp:RequiredFieldValidator>
                </div>
            </div>

            <div class="form-group">
                <label class="control-label col-sm-3">Last Name:</label>
                <div class="col-sm-5">
                    <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-sm-4">
                    <asp:RequiredFieldValidator ID="rfvLastName" runat="server" 
                        ErrorMessage="Last name is required" CssClass="text-danger" 
                        Display="Dynamic" ControlToValidate="txtLastName"></asp:RequiredFieldValidator>
                </div>
            </div>

            <div class="form-group">
                    <label class="control-label col-sm-3">Date of Birth:</label>
                    <div class="col-sm-5">
                        <asp:TextBox ID="txtDOB" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="revDOB" runat="server" ControlToValidate="txtDOB"
                            ValidationExpression="\d{4}-\d{2}-\d{2}" ErrorMessage="Invalid date format (YYYY-MM-DD)"
                            CssClass="text-danger" Display="Dynamic"></asp:RegularExpressionValidator>
                    </div>
                </div>

            <div class="form-group">
                <label class="control-label col-sm-3">Phone Number:</label>
                <div class="col-sm-5">
                    <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control" TextMode="Phone"></asp:TextBox>
                </div>
                <div class="col-sm-4">
                    <asp:RequiredFieldValidator ID="rfvPhoneNumber" runat="server" 
                        ErrorMessage="Phone number is required." CssClass="text-danger"
                        Display="Dynamic" ControlToValidate="txtPhone"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="revPhone" runat="server" ControlToValidate="txtPhone"
                            ValidationExpression="^\d{10}$" ErrorMessage="Invalid phone number format. (Should be 10 digit)" CssClass="text-danger"
                            Display="Dynamic"></asp:RegularExpressionValidator>
                </div>
            </div>        

            <h3>Billing Address</h3>
            <div class="form-group">
                <label class="control-label col-sm-3">Address:</label>
                <div class="col-sm-5">
                    <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-sm-4">
                    <asp:RequiredFieldValidator ID="rfvStreetAddress" runat="server" 
                        ErrorMessage="Street Address is required" CssClass="text-danger" 
                        Display="Dynamic" ControlToValidate="txtAddress"></asp:RequiredFieldValidator>
                </div>
            </div>

            <div class="form-group">
                <label class="control-label col-sm-3">City:</label>
                <div class="col-sm-5">
                    <asp:TextBox ID="txtCity" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-sm-4">
                    <asp:RequiredFieldValidator ID="rfvCity" runat="server" 
                        ErrorMessage="City is required" CssClass="text-danger" 
                        Display="Dynamic" ControlToValidate="txtCity"></asp:RequiredFieldValidator>
                </div>
            </div>

            

            <div class="form-group">
                <label class="control-label col-sm-3">Zip code:</label>
                <div class="col-sm-5">
                    <asp:TextBox ID="txtZip" runat="server" CssClass="form-control" MaxLength="5"></asp:TextBox>
                </div>
                <div class="col-sm-4">
                    <asp:RequiredFieldValidator ID="rfvZip" runat="server" 
                        ErrorMessage="Zip is required" CssClass="text-danger" 
                        Display="Dynamic" ControlToValidate="txtZip"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="revZip" runat="server" ControlToValidate="txtZip"
                            ValidationExpression="^\d{5}$" ErrorMessage="Invalid ZIP code. Must be 5 character long" CssClass="text-danger"
                            Display="Dynamic"></asp:RegularExpressionValidator>
                </div>
            </div>  

            

            <div class="form-group">
    <div class="col-sm-12">
        <asp:Button ID="btnCheckOut" runat="server" Text="Check Out" CssClass="btn btn-primary"
            OnClick="btnCheckOut_Click" />
        <asp:Button ID="btnCancel" runat="server" Text="Cancel Order" CssClass="btn btn-secondary"
            CausesValidation="False" OnClick="btnCancel_Click" />
        <asp:LinkButton ID="lbtnContinueShopping" runat="server" CssClass="btn btn-success"
            PostBackUrl="~/Default.aspx" CausesValidation="False">Continue Shopping</asp:LinkButton>
    </div>
</div>
    
    </main>
</asp:Content>


