using G3Ecommerce.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;
using System.Web;

namespace G3Ecommerce.Models
{
    public class CartItemList : IEnumerable<CartItem>
    {
        private List<CartItem> cartItems;
        string connectionString = ConfigurationManager.ConnectionStrings["g3ecommerce"].ConnectionString;


        public CartItemList()
        {
            cartItems = new List<CartItem>();
        }

        public IEnumerator<CartItem> GetEnumerator()
        {
            return cartItems.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int Count
        {
            get { return cartItems.Count; }
        }

        public CartItem this[int index]
        {
            get { return cartItems[index]; }
            set { cartItems[index] = value; }
        }

        public CartItem this[string id]
        {
            get
            {
                foreach (CartItem c in cartItems)
                    if (c.FoodItem.Id == Int32.Parse(id)) return c;
                return null;
            }
        }

        public static CartItemList GetCartItemsForUser(int userId)
        {
            CartItemList cart = new CartItemList();

            string connectionString = ConfigurationManager.ConnectionStrings["g3ecommerce"].ConnectionString;
            string query = "SELECT oi.item_id, oi.quantity, f.item_name, f.item_price FROM OrderItems oi INNER JOIN Orders o ON oi.order_id = o.order_id INNER JOIN Items f ON oi.item_id = f.item_id WHERE o.customer_id = @userId AND o.order_status = 'IN_CART'";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@userId", userId);
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int itemId = reader.GetInt32(0);
                            int quantity = reader.GetInt32(1);
                            string itemName = reader.GetString(2);
                            decimal itemPrice = reader.GetDecimal(3);

                            FoodItem foodItem = new FoodItem(itemId, itemName, itemPrice);
                            cart.AddItem(foodItem, quantity);
                        }
                    }
                }
            }

            return cart;
        }

        public void AddItem(FoodItem foodItem, int quantity)
        {
            CartItem c = new CartItem(foodItem, quantity);
            cartItems.Add(c);
        }

        public void RemoveAt(int index)
        {
            cartItems.RemoveAt(index);
        }

        public void Clear(int userId)
        {
            string query = "DELETE FROM Orders WHERE customer_id = @userId";

            // Create a SqlConnection object to connect to the database
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Create a SqlCommand object to execute the SQL query
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add parameters to the SQL query
                    command.Parameters.AddWithValue("@userId", userId);

                    // Open the database connection
                    connection.Open();

                    // Execute the SQL query
                    command.ExecuteNonQuery();
                }
            }


            // Clear the cart items list
            cartItems.Clear();
        }


    }
}
