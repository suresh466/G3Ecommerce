<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="AdminDashboard.aspx.cs" Inherits="G3Ecommerce.AdminDashboard" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <h2>Admin Dashboard</h2>
        <asp:Button ID="btnLogout" runat="server" Text="Logout" OnClick="btnLogout_Click" CssClass="btn btn-danger" />
        <h3>Products</h3>
        <p>
            <asp:Label ID="lblError" runat="server"
                EnableViewState="false" CssClass="text-danger"></asp:Label>
        </p>
        <div class="row">
            <div class="col-md-6 table-responsive">
                <asp:GridView ID="grdItems" runat="server" SelectedIndex="0" PageSize="4"
                    AutoGenerateColumns="False" DataKeyNames="item_id"
                    DataSourceID="SqlDataSource1" AllowPaging="True"
                    CssClass="table table-bordered table-striped table-condensed"
                    OnPreRender="grdItems_PreRender" OnSelectedIndexChanged="grdItems_SelectedIndexChanged">
                    <Columns>
                        <asp:BoundField DataField="item_id" HeaderText="ID"
                            ReadOnly="True">
                            <ItemStyle CssClass="col-xs-2" />
                        </asp:BoundField>
                        <asp:BoundField DataField="item_name" HeaderText="Name">
                            <ItemStyle CssClass="col-xs-6" />
                        </asp:BoundField>
                        <asp:BoundField DataField="item_price" HeaderText="Price">
                            <ItemStyle CssClass="col-xs-3" />
                        </asp:BoundField>
                        <asp:BoundField DataField="item_description" HeaderText="Description">
                            <ItemStyle CssClass="col-xs-5" />
                        </asp:BoundField>
                        <asp:BoundField DataField="item_image" HeaderText="Image">
                            <ItemStyle CssClass="col-xs-4" />
                        </asp:BoundField>
                        <asp:CommandField ButtonType="Link" ShowSelectButton="true">
                            <ItemStyle CssClass="col-xs-1" />
                        </asp:CommandField>
                    </Columns>
                    <HeaderStyle />
                    <PagerSettings Mode="NumericFirstLast" />
                    <PagerStyle CssClass="pagerStyle"
                        BackColor="#8c8c8c" HorizontalAlign="Center" />
                    <SelectedRowStyle CssClass="warning" />
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server"
                    ConnectionString="<%$ ConnectionStrings:g3ecommerce %>"
                    SelectCommand="SELECT [item_id], [item_name], [item_price], [category_id], [item_description], [item_image] FROM [Items]"></asp:SqlDataSource>
            </div>

            <div class="col-md-6">
                <asp:DetailsView ID="dvItem" runat="server"
                    DataSourceID="SqlDataSource2" DataKeyNames="item_id"
                    AutoGenerateRows="False"
                    CssClass="table table-bordered table-condensed"
                    OnItemDeleted="dvItem_ItemDeleted"
                    OnItemDeleting="dvItem_ItemDeleting"
                    OnItemInserted="dvItem_ItemInserted"
                    OnItemUpdated="dvItem_ItemUpdated">
                    <Fields>
                        <asp:TemplateField HeaderText="ID">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblID" Text='<%# Bind("item_id") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Name">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblName" Text='<%# Bind("item_name") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox runat="server" ID="txtName" Text='<%# Bind("item_name") %>' CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvName" runat="server"
                                    ControlToValidate="txtName" CssClass="text-danger"
                                    ErrorMessage="Name is a required field." Text="*">
                                </asp:RequiredFieldValidator>

                            </EditItemTemplate>
                            <InsertItemTemplate>
                                <div class="col-xs-11 col-insert">
                                    <asp:TextBox runat="server" ID="txtName"
                                        Text='<%# Bind("item_name") %>' MaxLength="50"
                                        CssClass="form-control"></asp:TextBox>
                                </div>
                                <asp:RequiredFieldValidator ID="rfvName" runat="server"
                                    ControlToValidate="txtName" CssClass="text-danger"
                                    ErrorMessage="Name is a required field." Text="*">
                                </asp:RequiredFieldValidator>
                            </InsertItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Price">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblPrice" Text='<%# Bind("item_price") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox runat="server" ID="txtPrice" Text='<%# Bind("item_price") %>' CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvPrice" runat="server"
                                    ControlToValidate="txtPrice" CssClass="text-danger"
                                    ErrorMessage="Price is a required field." Text="*">
                                </asp:RequiredFieldValidator>
                            </EditItemTemplate>
                            <InsertItemTemplate>
                                <div class="col-xs-11 col-insert">
                                    <asp:TextBox runat="server" ID="txtPrice"
                                        Text='<%# Bind("item_price") %>' MaxLength="50"
                                        CssClass="form-control"></asp:TextBox>
                                </div>
                                <asp:RequiredFieldValidator ID="rfvPrice" runat="server"
                                    ControlToValidate="txtPrice" CssClass="text-danger"
                                    ErrorMessage="Price is a required field." Text="*">
                                </asp:RequiredFieldValidator>
                            </InsertItemTemplate>
                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="Category">
                            <EditItemTemplate>
                                <div class="col-xs-11 col-edit">
                                    <asp:DropDownList runat="server" ID="ddlCategory"
                                        DataSourceID="SqlDataSource3"
                                        DataTextField="category_name" DataValueField="category_id"
                                        SelectedValue='<%# Bind("category_id") %>'
                                        CssClass="form-control">
                                    </asp:DropDownList>
                                </div>
                            </EditItemTemplate>
                            <InsertItemTemplate>
                                <div class="col-xs-11 col-insert">
                                    <asp:DropDownList runat="server" ID="ddlCategory"
                                        DataSourceID="SqlDataSource3"
                                        DataTextField="category_name" DataValueField="category_id"
                                        SelectedValue='<%# Bind("category_id") %>'
                                        CssClass="form-control">
                                    </asp:DropDownList>
                                </div>
                            </InsertItemTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblCategory"
                                    Text='<%# Bind("category_id") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Description">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblDescription" Text='<%# Bind("item_description") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox runat="server" ID="txtDescription" Text='<%# Bind("item_description") %>' CssClass="form-control"></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Image">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblImage" Text='<%# Bind("item_image") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox runat="server" ID="txtImage" Text='<%# Bind("item_image") %>' CssClass="form-control"></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ButtonType="Link" ShowDeleteButton="true" ShowEditButton="true" ShowInsertButton="true" />
                    </Fields>
                    <RowStyle BackColor="#e7e7e7" />
                    <CommandRowStyle BackColor="#8c8c8c" ForeColor="white" />
                </asp:DetailsView>
                <asp:SqlDataSource ID="SqlDataSource2" runat="server"
                    ConnectionString="<%$ ConnectionStrings:g3ecommerce %>"
                    ConflictDetection="CompareAllValues"
                    OldValuesParameterFormatString="original_{0}"
                    SelectCommand="SELECT [item_id], [item_name], [item_price], [category_id], [item_description], [item_image]
        FROM [Items] 
        WHERE ([item_id] = @Id)"
                    DeleteCommand="DELETE FROM [Items] 
        WHERE [item_id] = @original_item_id"
                    InsertCommand="INSERT INTO [Items] ([item_name], [item_price], [category_id], [item_description], [item_image]) 
        VALUES (@item_name, @item_price, @category_id, @item_description, @item_image)"
                    UpdateCommand="UPDATE [Items] SET [item_name] = @item_name, 
                      [item_price] = @item_price, 
                      [category_id] = @category_id, 
                      [item_description] = @item_description,
                      [item_image] = @item_image
                    WHERE [item_id] = @original_item_id">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="grdItems" Name="Id"
                            PropertyName="SelectedValue" Type="Int32" />
                    </SelectParameters>
                    <DeleteParameters>
                        <asp:Parameter Name="original_item_id" Type="Int32" />
                    </DeleteParameters>
                    <UpdateParameters>
                        <asp:Parameter Name="item_name" Type="String" />
                        <asp:Parameter Name="item_price" Type="Decimal" />
                        <asp:Parameter Name="category_id" Type="Int32" />
                        <asp:Parameter Name="item_description" Type="String" />
                        <asp:Parameter Name="item_image" Type="String" />
                        <asp:Parameter Name="original_item_id" Type="Int32" />
                    </UpdateParameters>
                    <InsertParameters>
                        <asp:Parameter Name="item_name" Type="String" />
                        <asp:Parameter Name="item_price" Type="Decimal" />
                        <asp:Parameter Name="category_id" Type="Int32" />
                        <asp:Parameter Name="item_description" Type="String" />
                        <asp:Parameter Name="item_image" Type="String" />
                    </InsertParameters>
                </asp:SqlDataSource>


                <asp:SqlDataSource ID="SqlDataSource3" runat="server"
                    ConnectionString='<%$ ConnectionStrings:g3ecommerce %>'
                    SelectCommand="SELECT [category_id], [category_name] 
                        FROM [Categories] ORDER BY [category_name]"></asp:SqlDataSource>

                <p>
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server"
                        HeaderText="Please correct the following errors:"
                        CssClass="text-danger" />
                    <p>
                        <asp:Label ID="Label1" runat="server"
                            EnableViewState="false" CssClass="text-danger"></asp:Label>
                    </p>
            </div>
        </div>



        <h3>Categories</h3>
        <asp:GridView ID="gvCategories" runat="server" AutoGenerateColumns="False"
            SelectedIndex="0" PageSize="4"
            AllowPaging="True"
            CssClass="table table-bordered table-striped table-condensed"
            OnPreRender="gvCategories_PreRender"
            DataSourceID="dsCategories"
            DataKeyNames="category_id" OnSelectedIndexChanged="gvCategories_SelectedIndexChanged">
            <Columns>
                <asp:BoundField DataField="category_id" HeaderText="Category ID" />
                <asp:BoundField DataField="category_name" HeaderText="Category Name" />
                <asp:BoundField DataField="details" HeaderText="Details" />
                <asp:BoundField DataField="picture_url" HeaderText="Picture URL" />
                <asp:CommandField ButtonType="Link" ShowSelectButton="true">
                    <ItemStyle CssClass="col-xs-1" />
                </asp:CommandField>
            </Columns>

            <HeaderStyle />
            <PagerSettings Mode="NumericFirstLast" />
            <PagerStyle CssClass="pagerStyle"
                BackColor="#8c8c8c" HorizontalAlign="Center" />
            <SelectedRowStyle CssClass="warning" />
        </asp:GridView>



        <asp:DetailsView ID="dvCategory" runat="server" DataSourceID="dsCategories2" DataKeyNames="category_id"
            AutoGenerateRows="False" CssClass="table table-bordered table-condensed" Visible="False" 
             OnItemDeleted="dvCategory_ItemDeleted"
            OnItemDeleting="dvCategory_ItemDeleting"
            OnItemInserted="dvCategory_ItemInserted"
            OnItemUpdated="dvCategory_ItemUpdated"
            >
            <Fields>
                <asp:TemplateField HeaderText="ID">
    <ItemTemplate>
        <asp:Label runat="server" ID="lblCategoryID" Text='<%# Bind("category_id") %>'></asp:Label>
    </ItemTemplate>
</asp:TemplateField>
                <asp:TemplateField HeaderText="Category Name">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblCategoryName" Text='<%# Bind("category_name") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox runat="server" ID="txtCategoryName" Text='<%# Bind("category_name") %>' CssClass="form-control"></asp:TextBox>
                    </EditItemTemplate>
                    <InsertItemTemplate>
                        <asp:TextBox runat="server" ID="txtInsertCategoryName" CssClass="form-control" Text='<%# Bind("category_name") %>'></asp:TextBox>
                    </InsertItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Details">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblDetails" Text='<%# Bind("details") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox runat="server" ID="txtDetails" Text='<%# Bind("details") %>' CssClass="form-control"></asp:TextBox>
                    </EditItemTemplate>
                    <InsertItemTemplate>
                        <asp:TextBox runat="server" ID="txtInsertDetails" CssClass="form-control" Text='<%# Bind("details")%>' ></asp:TextBox>
                    </InsertItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Picture URL">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblPictureUrl" Text='<%# Bind("picture_url") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox runat="server" ID="txtPictureUrl" Text='<%# Bind("picture_url") %>' CssClass="form-control"></asp:TextBox>
                    </EditItemTemplate>
                    <InsertItemTemplate>
                        <asp:TextBox runat="server" ID="txtPictureUrl" CssClass="form-control" Text='<%# Bind("picture_url")%>' ></asp:TextBox>
                    </InsertItemTemplate>
                </asp:TemplateField>

                <asp:CommandField ButtonType="Link" ShowDeleteButton="True" ShowEditButton="True" ShowInsertButton="True" />
            </Fields>
        </asp:DetailsView>

        <asp:SqlDataSource ID="dsCategories" runat="server" ConnectionString="<%$ ConnectionStrings:g3ecommerce %>"
            SelectCommand="SELECT [category_id],[category_name],[details],[picture_url] FROM Categories" >
        </asp:SqlDataSource>

        <asp:SqlDataSource ID="dsCategories2" runat="server" ConnectionString="<%$ ConnectionStrings:g3ecommerce %>"
            SelectCommand="SELECT [category_id],[category_name],[details],[picture_url] FROM Categories WHERE category_id = @category_id" DeleteCommand="DELETE FROM Categories WHERE category_id = @category_id"
            InsertCommand="INSERT INTO Categories (category_name, details, picture_url) VALUES (@category_name, @details, @picture_url)"
            UpdateCommand="UPDATE Categories SET category_name = @category_name, details=@details, picture_url=@picture_url WHERE category_id = @category_id">
            <SelectParameters>
                <asp:ControlParameter ControlID="gvCategories" Name="category_id"
                    PropertyName="SelectedValue" Type="Int32" />
            </SelectParameters>
            <DeleteParameters>
                <asp:Parameter Name="category_id" Type="Int32" />
            </DeleteParameters>
            <InsertParameters>
                <asp:Parameter Name="category_name" Type="String" />
                <asp:Parameter Name="details" Type="String" />
                <asp:Parameter Name="picture_url" Type="String" />
            </InsertParameters>
            <UpdateParameters>
                <asp:Parameter Name="details" Type="String" />
                <asp:Parameter Name="picture_url" Type="String" />
                <asp:Parameter Name="category_name" Type="String" />
                <asp:Parameter Name="category_id" Type="Int32" />
            </UpdateParameters>
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
            SelectCommand="SELECT * FROM Orders"></asp:SqlDataSource>
    </div>
</asp:Content>
