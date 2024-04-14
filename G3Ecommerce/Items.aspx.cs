using System;
using G3Ecommerce.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

namespace G3Ecommerce
{

    public partial class Items : System.Web.UI.Page
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["g3ecommerce"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindItems();
            }

        }

        private void BindItems()
        {
            // Retrieve the selected category ID from the query string
            int categoryId;
            bool categorySelected = int.TryParse(Request.QueryString["category"], out categoryId);

            // Fetch items based on the selected category, if any
            List<FoodItem> foodItems = new List<FoodItem>();

            if (categorySelected)
            {
                // Fetch items from the selected category
                foodItems = FetchItemsByCategory(categoryId);
            }
            else
            {
                // Fetch all items
                foodItems = FetchAllItems();
            }

            // Bind data to GridView
            GridView1.DataSource = foodItems;
            GridView1.DataBind();
        }

        private List<FoodItem> FetchItemsByCategory(int categoryId)
        {
            List<FoodItem> foodItems = new List<FoodItem>();

            string query = "SELECT item_id, item_name, item_price FROM Items WHERE category_id = @categoryId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@categoryId", categoryId);

                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int itemId = reader.GetInt32(0);
                            string itemName = reader.GetString(1);
                            decimal itemPrice = reader.GetDecimal(2);

                            foodItems.Add(new FoodItem(itemId, itemName, itemPrice));
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle the exception
                    // For example: Log the error, display an error message to the user, etc.
                }
            }

            return foodItems;
        }

        private List<FoodItem> FetchAllItems()
        {
            List<FoodItem> foodItems = new List<FoodItem>();

            string query = "SELECT item_id, item_name, item_price FROM Items";

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int itemId = reader.GetInt32(0);
                            string itemName = reader.GetString(1);
                            decimal itemPrice = reader.GetDecimal(2);

                            foodItems.Add(new FoodItem(itemId, itemName, itemPrice));
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle the exception
                    // For example: Log the error, display an error message to the user, etc.
                }
            }

            return foodItems;
        }

        protected void AddToCart_Click(object sender, EventArgs e)
        {
            Button btnAddToCart = (Button)sender;
            int itemId = Convert.ToInt32(btnAddToCart.CommandArgument);

            // Find the corresponding item in the GridView
            GridViewRow row = (GridViewRow)btnAddToCart.NamingContainer;
            TextBox txtQuantity = (TextBox)row.FindControl("txtQuantity");

            int quantity = Convert.ToInt32(2);

            // Get the current user's ID (you need to implement this based on your authentication system)
            int customerId = GetCurrentUserId();

            // Check if the user already has an "IN_CART" order
            int orderId = GetCartOrderId(customerId);

            // If no "IN_CART" order exists, create a new order
            if (orderId == -1)
            {
                orderId = CreateNewOrder(customerId);
            }

            // Add the item to the order or update the quantity if it already exists in the order
            AddOrUpdateOrderItem(orderId, itemId, quantity);

            // Update the total amount of the order
            UpdateOrderTotalAmount(orderId);
        }

        private int GetCurrentUserId()
        {
            // Implement logic to get the current user's ID
            // For example: return HttpContext.Current.User.Identity.GetUserId();
            return 1; // Placeholder value, replace with actual implementation
        }

        private int GetCartOrderId(int customerId)
        {
            int orderId = -1;

            // Write your SQL query to retrieve the ID of the user's "IN_CART" order
            string query = "SELECT order_id FROM Orders WHERE customer_id = @customerId AND order_status = 'IN_CART'";

            // Create a SqlConnection object to connect to the database
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Create a SqlCommand object to execute the SQL query
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add parameters to the SQL query
                    command.Parameters.AddWithValue("@customerId", customerId);

                    // Open the database connection
                    connection.Open();

                    // Execute the SQL query and read the result (if any)
                    object result = command.ExecuteScalar();

                    // Check if a result was returned (i.e., if the user has an "IN_CART" order)
                    if (result != null && result != DBNull.Value)
                    {
                        // Convert the result to an integer (order_id)
                        orderId = Convert.ToInt32(result);
                    }
                }
            }

            // Return the order ID (or -1 if no "IN_CART" order was found)
            return orderId;
        }


        private int CreateNewOrder(int customerId)
        {
            int orderId = -1;

            // Define your SQL query to insert a new order
            string query = "INSERT INTO Orders (customer_id, total_amount, order_status) VALUES (@customerId, 0, 'IN_CART'); SELECT SCOPE_IDENTITY();";

            // Create a SqlConnection object to connect to the database
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Create a SqlCommand object to execute the SQL query
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add parameters to the SQL query
                    command.Parameters.AddWithValue("@customerId", customerId);

                    // Open the database connection
                    connection.Open();

                    // Execute the SQL query and retrieve the generated order ID
                    object result = command.ExecuteScalar();

                    // Check if a result was returned
                    if (result != null && result != DBNull.Value)
                    {
                        // Convert the result to an integer (order_id)
                        orderId = Convert.ToInt32(result);
                    }
                }
            }

            // Return the generated order ID
            return orderId;
        }


        private void AddOrUpdateOrderItem(int orderId, int itemId, int quantity)
        {
            // Define your SQL query to check if the item already exists in the order
            string query = "SELECT order_item_id, quantity FROM OrderItems WHERE order_id = @orderId AND item_id = @itemId";

            // Create a SqlConnection object to connect to the database
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Create a SqlCommand object to execute the SQL query
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add parameters to the SQL query
                    command.Parameters.AddWithValue("@orderId", orderId);
                    command.Parameters.AddWithValue("@itemId", itemId);

                    // Open the database connection
                    connection.Open();

                    // Execute the SQL query and read the result (if any)
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Item already exists in the order, update the quantity
                            int orderItemId = reader.GetInt32(0);
                            int existingQuantity = reader.GetInt32(1);

                            // Calculate the new quantity and total price
                            int newQuantity = existingQuantity + quantity;
                            decimal totalPrice = GetItemPrice(itemId) * newQuantity;

                            // Update the existing OrderItems record with the new quantity and total price
                            UpdateOrderItem(orderItemId, newQuantity, totalPrice);
                        }
                        else
                        {
                            // Item does not exist in the order, insert a new record into the OrderItems table
                            InsertOrderItem(orderId, itemId, quantity);
                        }
                    }
                }
            }
        }

        private void UpdateOrderItem(int orderItemId, int quantity, decimal totalPrice)
        {
            // Define your SQL query to update the quantity and total price of the OrderItems record
            string query = "UPDATE OrderItems SET quantity = @quantity, total_price = @totalPrice WHERE order_item_id = @orderItemId";

            // Create a SqlConnection object to connect to the database
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Create a SqlCommand object to execute the SQL query
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add parameters to the SQL query
                    command.Parameters.AddWithValue("@quantity", quantity);
                    command.Parameters.AddWithValue("@totalPrice", totalPrice);
                    command.Parameters.AddWithValue("@orderItemId", orderItemId);

                    // Open the database connection
                    connection.Open();

                    // Execute the SQL query
                    command.ExecuteNonQuery();
                }
            }
        }

        private void InsertOrderItem(int orderId, int itemId, int quantity)
        {
            // Define your SQL query to insert a new record into the OrderItems table
            string query = "INSERT INTO OrderItems (order_id, item_id, quantity, total_price) VALUES (@orderId, @itemId, @quantity, @totalPrice)";

            // Calculate the total price of the item
            decimal totalPrice = GetItemPrice(itemId) * quantity;

            // Create a SqlConnection object to connect to the database
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Create a SqlCommand object to execute the SQL query
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add parameters to the SQL query
                    command.Parameters.AddWithValue("@orderId", orderId);
                    command.Parameters.AddWithValue("@itemId", itemId);
                    command.Parameters.AddWithValue("@quantity", quantity);
                    command.Parameters.AddWithValue("@totalPrice", totalPrice);

                    // Open the database connection
                    connection.Open();

                    // Execute the SQL query
                    command.ExecuteNonQuery();
                }
            }
        }

        private decimal GetItemPrice(int itemId)
        {
            decimal itemPrice = 0; 

            // Define SQL query to retrieve the price of the item with the given itemId
            string query = "SELECT item_price FROM Items WHERE item_id = @itemId";

            // Create a SqlConnection object to connect to the database
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Create a SqlCommand object to execute the SQL query
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add parameters to the SQL query
                    command.Parameters.AddWithValue("@itemId", itemId);

                    // Open the database connection
                    connection.Open();

                    // Execute the SQL query and read the result (if any)
                    object result = command.ExecuteScalar();

                    // Check if a result was returned
                    if (result != null && result != DBNull.Value)
                    {
                        // Convert the result to a decimal (item_price)
                        itemPrice = Convert.ToDecimal(result);
                    }
                }
            }

            // Return the item price
            return itemPrice;
        }


        private void UpdateOrderTotalAmount(int orderId)
        {
            // Define your SQL query to calculate the total price of all items in the order
            string query = "SELECT SUM(total_price) FROM OrderItems WHERE order_id = @orderId";

            // Initialize totalAmount to 0
            decimal totalAmount = 0;

            // Create a SqlConnection object to connect to the database
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Create a SqlCommand object to execute the SQL query
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add parameters to the SQL query
                    command.Parameters.AddWithValue("@orderId", orderId);

                    // Open the database connection
                    connection.Open();

                    // Execute the SQL query and read the result (if any)
                    object result = command.ExecuteScalar();

                    // Check if a result was returned
                    if (result != null && result != DBNull.Value)
                    {
                        // Convert the result to a decimal (total price)
                        totalAmount = Convert.ToDecimal(result);
                    }
                }
            }

            // Define your SQL query to update the total amount of the order
            string updateQuery = "UPDATE Orders SET total_amount = @totalAmount WHERE order_id = @orderId";

            // Create a SqlConnection object to connect to the database
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Create a SqlCommand object to execute the SQL query
                using (SqlCommand command = new SqlCommand(updateQuery, connection))
                {
                    // Add parameters to the SQL query
                    command.Parameters.AddWithValue("@totalAmount", totalAmount);
                    command.Parameters.AddWithValue("@orderId", orderId);

                    // Open the database connection
                    connection.Open();

                    // Execute the SQL query to update the total amount of the order
                    command.ExecuteNonQuery();
                }
            }
        }



    }
}