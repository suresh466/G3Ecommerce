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

            // Define your connection string
            string connectionString = ConfigurationManager.ConnectionStrings["g3ecommerce"].ConnectionString;

            // Write your SQL query to fetch items based on the category ID
            string query = "SELECT item_id, item_name, item_price FROM Items WHERE category_id = @categoryId";

            // Create a SqlConnection object to connect to the database
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Create a SqlCommand object to execute the SQL query
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add parameters to the SQL query
                    command.Parameters.AddWithValue("@categoryId", categoryId);

                    // Open the database connection
                    connection.Open();

                    // Execute the SQL query and read the results
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Iterate through the result set and populate the foodItems list
                        while (reader.Read())
                        {
                            int itemId = reader.GetInt32(0);
                            string itemName = reader.GetString(1);
                            decimal itemPrice = reader.GetDecimal(2);

                            // Create a new FoodItem object and add it to the list
                            foodItems.Add(new FoodItem(itemId, itemName, itemPrice));
                        }
                    }
                }
            }

            return foodItems;
        }


        private List<FoodItem> FetchAllItems()
        {
            // Implement logic to fetch all items from the database
            // You can use Entity Framework, ADO.NET, or any other data access method here
            // For demonstration purposes, we'll use sample data
            List<FoodItem> foodItems = new List<FoodItem>
    {
        new FoodItem(1, "Pizza", 10.99m),
        new FoodItem(2, "Burger", 5.99m),
        new FoodItem(3, "Salad", 7.99m)
    };

            return foodItems;
        }
    }
}