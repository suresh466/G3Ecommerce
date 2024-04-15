﻿using System;
using System.Collections.Generic;
using System.Linq;
using G3Ecommerce.Models;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

namespace G3Ecommerce
{
    public partial class Checkout : System.Web.UI.Page
    {
        private Customer customer;
        private string connectionString = ConfigurationManager.ConnectionStrings["g3ecommerce"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Populate customer information if available
                PopulateCustomerInformation();
            }

        }

        private void PopulateCustomerInformation()
        {
            

            // Assuming you have a method to retrieve customer information based on user ID
            Customer customer = GetCustomerInfo();

            if (customer != null)
            {
                // Populate email and username fields
                txtEmail1.Text = customer.EmailAddress;
                txtEmail2.Text = customer.EmailAddress;
                txtFirstName.Text = customer.FirstName;
                txtAddress.Text = customer.Address;
                // Similarly, populate other fields if needed
            }
        }

        // Method to retrieve customer information based on user ID
        private Customer GetCustomerInfo()
        {
            int userId = -1;
            HttpCookie emailCookie = Request.Cookies["email"];
            if (emailCookie != null)
            {
                string userEmail = emailCookie.Value;

                // Query the database to fetch the user ID based on the email
                userId = GetUserIdByEmail(userEmail); 

            }
            string query = "SELECT * FROM Customers WHERE customer_id = @UserId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        customer = new Customer
                        {
                            Id = Convert.ToInt32(reader["customer_id"]),
                            EmailAddress = reader["customer_email"].ToString(),
                            FirstName = reader["customer_name"].ToString(),
                            Address = reader["customer_address"].ToString(),
                            // Populate other fields as needed
                        };
                    }
                }
            }

            return customer;
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

        protected void btnCheckOut_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                this.GetCustomerData();
                Response.Redirect("~/Confirmation.aspx");
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Session.Remove("Cart");
            Session.Remove("Customer");
            Response.Redirect("~/Default.aspx");
        }
        private void LoadCustomerData()
        {
            if (customer != null)
            {
                txtFirstName.Text = customer.FirstName;
                txtLastName.Text = customer.LastName;
                txtEmail1.Text = customer.EmailAddress;
                txtPhone.Text = customer.Phone;
                txtAddress.Text = customer.Address;
                txtCity.Text = customer.City;
                txtZip.Text = customer.Zip;
                
            }
        }
        private void GetCustomerData()
        {
            if (customer == null) customer = new Customer();
            customer.FirstName = txtFirstName.Text;
            customer.LastName = txtLastName.Text;
            customer.EmailAddress = txtEmail1.Text;
            customer.Phone = txtPhone.Text;
            customer.Address = txtAddress.Text;
            customer.City = txtCity.Text;
            customer.Zip = txtZip.Text;
            

        }
        
    }
}