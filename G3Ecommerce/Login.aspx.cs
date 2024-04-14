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

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string username = txtUsername.Text.Trim();

            // Implement logic to authenticate the user against the database
            if (AuthenticationManager.AuthenticateUser(email, username))
            {
                // Session["LoggedInUser"] = new User { Email = email, Username = username };
                // Redirect the user to the home page after successful login
                Response.Redirect("~/Default.aspx");
            }
            else
            {
                // Display error message
            }
        }
    }
}