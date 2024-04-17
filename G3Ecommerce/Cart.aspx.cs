using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using G3Ecommerce.Models;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

namespace G3Ecommerce
{
    public partial class Cart : Page
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["g3ecommerce"].ConnectionString;
        

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Check if the user is logged in
                if (UserIsLoggedIn())
                {
                    // Retrieve cart items for the logged-in user from the database
                    DisplayCartItems();
                }
                else
                {
                    ((SiteMaster)this.Master).ShowNotification("Please Login to continue.", "message");

                    // Redirect to the login page if the user is not logged in
                    Response.Redirect("~/Login.aspx");
                }
            }
        }

        private bool UserIsLoggedIn()
        {
            // Check if the "IsLoggedIn" cookie exists and has the value "true"
            return Request.Cookies["IsLoggedIn"] != null && Request.Cookies["IsLoggedIn"].Value == "true";
        }


        private void DisplayCartItems()
        {
            // Get the current logged-in user's ID
            int userId = GetCurrentUserId();

            // Retrieve cart items for the logged-in user with order status "In_cart" from the database
            CartItemList cartItems = CartItemList.GetCartItemsForUser(userId);

            lstCart.DataSource = cartItems;
            lstCart.DataBind();
        }

        protected void lstCart_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            // Check if the ListViewItem is an Item
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                // Find the Remove button in the ListViewItem
                Button btnRemove = (Button)e.Item.FindControl("btnRemove");

                // Set the CSS class for the Remove button
                btnRemove.CssClass = "btn btn-danger";
            }
        }

        private int GetCurrentUserId()
        {
            // Retrieve the user's email from the cookie
            HttpCookie emailCookie = Request.Cookies["email"]; 
            if (emailCookie != null)
            {
                string userEmail = emailCookie.Value;

                // Query the database to fetch the user ID based on the email
                int userId = GetUserIdByEmail(userEmail); // Implement this method to fetch the user ID

                return userId;
            }
            else
            {
                ((SiteMaster)this.Master).ShowNotification("An error occured!", "error");

                return -1; // Or throw an exception, or handle accordingly based on your application logic
            }
        }

        private int GetUserIdByEmail(string email)
        {
            int userId = -1; // Default value if user not found

            // Define your SQL query to retrieve the user ID by email
            string query = "SELECT customer_id FROM Customers WHERE customer_email = @Email";

            // Create a SqlConnection object to connect to the database
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Create a SqlCommand object to execute the SQL query
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add parameters to the SQL query
                    command.Parameters.AddWithValue("@Email", email);

                    // Open the database connection
                    connection.Open();

                    // Execute the SQL query and read the result
                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        userId = Convert.ToInt32(result);
                    }
                }
            }

            return userId;
        }

        private void DisplayCart(CartItemList cartItems)
        {
            lstCart.DataSource = cartItems;
            lstCart.DataBind();
        }

        protected void btnRemove_Click(object sender, EventArgs e)
        {
            // Check if any item is selected
            Button btnRemove = (Button)sender;

            // Get the item ID from the command argument of the button
            int itemId = Convert.ToInt32(btnRemove.CommandArgument);

            // Remove the item from the cart
            RemoveItem(GetCurrentUserId(), itemId);

            // Rebind the cart items to the list view
            DisplayCartItems();
        }

        private void RemoveItem(int userId, int itemId)
        {

           
            string query = "DELETE FROM OrderItems WHERE order_id IN (SELECT order_id FROM Orders WHERE customer_id = @userId AND order_status = 'IN_CART') AND item_id = @itemId";

            // Create a SqlConnection object to connect to the database
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Create a SqlCommand object to execute the SQL query
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add parameters to the SQL query
                    command.Parameters.AddWithValue("@userId", userId);
                    command.Parameters.AddWithValue("@itemId", itemId);

                    // Open the database connection
                    connection.Open();

                    // Execute the SQL query
                    command.ExecuteNonQuery();
                }
            }


            // After executing the query, the FoodItem associated with the given ID will be deleted from the database
        }



        protected void btnEmpty_Click(object sender, EventArgs e)
        {
            // Get the cart items for the current user
            CartItemList cart = CartItemList.GetCartItemsForUser(GetCurrentUserId());
            int userId = GetCurrentUserId();

            // Clear the cart items
            cart.Clear(userId);

            // Re-bind the cart items to the list
            DisplayCartItems();
        }

        protected void btnCheckOut_Click(object sender, EventArgs e)
        {
            int userId = GetCurrentUserId();
            CartItemList cartItems = CartItemList.GetCartItemsForUser(userId);
            // Check if the cart has any items
            if (cartItems.Count > 0)
            {
                // Proceed to the checkout page
                Response.Redirect("Checkout.aspx");
            }
            else
            {
                ((SiteMaster)this.Master).ShowNotification("Cart Empty!", "warning");

                // Show error message indicating the cart is empty
                lblMessage.Text = "Cart is empty. Please add items before checking out.";
            }
        }
    }
}