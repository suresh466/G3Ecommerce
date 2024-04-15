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

            // Display cart items on the page
            DisplayCart(cartItems);
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
                // Cookie not found or user not logged in
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
            lstCart.Items.Clear();

            // Display cart items in a ListBox or any other suitable control
            foreach (CartItem item in cartItems)
            {
                lstCart.Items.Add(item.Display());
            }
        }

        protected void btnRemove_Click(object sender, EventArgs e)
        {
            // Check if any item is selected
            if (lstCart.SelectedIndex > -1)
            {
                // Get the index of the selected item
                int selectedIndex = lstCart.SelectedIndex;

                // Get the CartItemList for the current user
                CartItemList cart = CartItemList.GetCartItemsForUser(GetCurrentUserId());

                // Retrieve the FoodItem associated with the selected item
                FoodItem foodItem = cart[selectedIndex].FoodItem;

                // Remove the item from the database
                RemoveItem(GetCurrentUserId(), foodItem.Id);

                // Remove the item from the CartItemList
                // cart.RemoveAt(selectedIndex);

                // Re-bind the cart items to the list
                DisplayCartItems();
            }
            else
            {
                lblMessage.Text = "Please select the item you want to remove.";
            }
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
                // Show error message indicating the cart is empty
                lblMessage.Text = "Cart is empty. Please add items before checking out.";
            }
        }
    }
}