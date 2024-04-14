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

        public static bool AuthenticateUser(string email, string username)
        {
            bool isAuthenticated = false;

            // Define your SQL query to check if the user exists and the provided password is correct
            string query = "SELECT COUNT(*) FROM Customers WHERE (customer_email = @Email OR customer_name = @Username)";

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
                    int count = (int)command.ExecuteScalar();

                    // Check if the count is greater than 0, indicating a match was found
                    isAuthenticated = count > 0;
                }
            }

            return isAuthenticated;
        }
    }
}