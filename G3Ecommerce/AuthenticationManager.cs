using G3Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace G3Ecommerce
{
    public class AuthenticationManager
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["g3ecommerce"].ConnectionString;

        public static User AuthenticateUser(string email, string username)
        {
            // Check if the user exists in the database
            User existingUser = GetUserByEmail(email);

            // If the user exists, return it
            if (existingUser != null)
            {
                return existingUser;
            }
            else
            {
                // If the user does not exist, create a new user
                CreateUser(email, username);
                // Return the newly created user
                return GetUserByEmail(email);
            }
        }

        private static User GetUserByEmail(string email)
        {
            // Define your SQL query to retrieve the user by email
            string query = "SELECT customer_id, customer_name, customer_email, customer_address, customer_phone FROM Customers WHERE customer_email = @Email";

            // Create a User object to hold the user details
            User user = null;

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
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Check if a record was found
                        if (reader.Read())
                        {
                            // Create a new User object and populate its properties
                            user = new User
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("customer_id")),
                                Username = reader.GetString(reader.GetOrdinal("customer_name")),
                                Email = reader.GetString(reader.GetOrdinal("customer_email")),
                                
                            };
                        }
                    }
                }
            }

            return user; // Return the user object (null if not found)
        }

        private static void CreateUser(string email, string username)
        {
            // Define your SQL query to insert a new user
            string query = "INSERT INTO Customers (customer_email, customer_name) VALUES (@Email, @Username)";

            // Create a SqlConnection object to connect to the database
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Create a SqlCommand object to execute the SQL query
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add parameters to the SQL query
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Username", username);

                    // Open the database connection
                    connection.Open();

                    // Execute the SQL query
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}