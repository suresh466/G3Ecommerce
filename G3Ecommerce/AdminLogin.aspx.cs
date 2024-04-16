using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace G3Ecommerce
{
    public partial class AdminLogin : System.Web.UI.Page
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["g3ecommerce"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Check if admin email cookie exists
                if (AdminEmailCookieExists())
                {
                    // Redirect to Admin Dashboard
                    Response.Redirect("/AdminDashboard.aspx");
                }
            }
        }

        private bool AdminEmailCookieExists()
        {
            return Request.Cookies["adminEmail"] != null;
        }


        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            // Validate admin credentials
            if (ValidateAdmin(username, password))
            {
                SetAdminEmailCookie(username);

                // Redirect to product addition page
                Response.Redirect("/AdminDashboard.aspx");
            }
            else
            {
                lblMessage.Text = "Invalid username or password.";
                lblMessage.Visible = true;
            }
        }

        private bool ValidateAdmin(string username, string password)
        {
            bool isValid = false;

            // Query to retrieve hashed password for the given username
            string query = "SELECT password FROM Admins WHERE username = @Username";

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Username", username);

                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null)
                {
                    string hashedPasswordFromDB = result.ToString();

                    // Verify hashed password
                    isValid = VerifyPassword(password, hashedPasswordFromDB);
                }
            }

            return isValid;
        }

        private bool VerifyPassword(string password, string hashedPassword)
        {
            // Compute the hash of the input password
            string hashedInputPassword = ComputeHash(password);

            // Compare the hashed input password with the stored hashed password
            return hashedInputPassword.Equals(hashedPassword);
        }

        private string ComputeHash(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                // Compute hash of the input string
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));

                // Convert byte array to a hexadecimal string
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    builder.Append(hashBytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        private void AddAdmin(string username, string email, string password)
        {
            // Hash the password before storing it
            string hashedPassword = ComputeHash(password);

            // Insert admin into the database
            string query = "INSERT INTO Admins (username, email, password) VALUES (@Username, @Email, @Password)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@Password", hashedPassword);
                command.Parameters.AddWithValue("@Email", email);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        // Call this method to add an admin with username "admin" and password "g3ecommerce"
        private void AddDefaultAdmin()
        {
            string defaultUsername = "admin";
            string defaultPassword = "g3ecommerce";
            string email = "admin@example.com";

            // Add the default admin if it doesn't already exist
            if (!AdminExists(defaultUsername))
            {
                AddAdmin(defaultUsername, email, defaultPassword);
            }
        }

        private void SetAdminEmailCookie(string username)
        {
            // Query the admin's email from the database
            string query = "SELECT email FROM Admins WHERE username = @Username";

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Username", username);

                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null)
                {
                    string email = result.ToString();

                    // Create a secure cookie with the admin's email
                    HttpCookie adminEmailCookie = new HttpCookie("adminEmail", email);
                    adminEmailCookie.HttpOnly = true;
                    adminEmailCookie.Secure = true; // Set to secure

                    // Add the cookie to the response
                    Response.Cookies.Add(adminEmailCookie);
                }
            }
        }

        private bool AdminExists(string username)
        {
            string query = "SELECT COUNT(*) FROM Admins WHERE username = @Username";

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Username", username);

                connection.Open();
                int count = (int)command.ExecuteScalar();

                return count > 0;
            }
        }
    }
}
