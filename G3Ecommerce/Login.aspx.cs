using System;
using System.Collections.Generic;
using System.Linq;
using G3Ecommerce;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace G3Ecommerce
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblErrorMessage.Visible = false; // Hide the error message label initially
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            string hashedPassword = HashPassword(password);

            if (AuthenticationManager.AuthenticateUser(email, username, hashedPassword))
            {
                Session["LoggedInUser"] = new User { Email = email, Username = username };
                Response.Redirect("~/Default.aspx");
            }
            else
            {
                lblErrorMessage.Visible = true; // Show the error message label
                lblErrorMessage.Text = "Invalid email, username, or password."; // Set the error message text
            }
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
