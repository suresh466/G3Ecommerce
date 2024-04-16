<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ItemManager.aspx.cs" Inherits="G3Ecommerce.ItemManager" MasterPageFile="~/Site.Master"%>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
    <div class="col-md-6 table-responsive">
    <asp:GridView ID="grdItems" runat="server" SelectedIndex="0"
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
    SelectCommand="SELECT [item_id], [item_name], [item_price], [category_id], [item_description], [item_image] FROM [Items]">
</asp:SqlDataSource>   
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
                                        CssClass="form-control"></asp:DropDownList>
                                </div>
                            </EditItemTemplate>
                            <InsertItemTemplate>
                                <div class="col-xs-11 col-insert">
                                    <asp:DropDownList runat="server" ID="ddlCategory" 
                                        DataSourceID="SqlDataSource3" 
                                        DataTextField="category_name" DataValueField="category_id" 
                                        SelectedValue='<%# Bind("category_id") %>'
                                        CssClass="form-control"></asp:DropDownList>
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
                        FROM [Categories] ORDER BY [category_name]">
                </asp:SqlDataSource>

                <p><asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                        HeaderText="Please correct the following errors:" 
                        CssClass="text-danger" />
                <p><asp:Label ID="lblError" runat="server" 
                    EnableViewState="false" CssClass="text-danger"></asp:Label></p>
            </div>
        </div>




    </asp:Content>

