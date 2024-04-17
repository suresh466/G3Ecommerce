using System;
using System.Collections.Generic;
using System.Linq;
using G3Ecommerce.Models;
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
            User authenticatedUser = AuthenticationManager.AuthenticateUser(email, username);

            if (authenticatedUser != null)
            {
                // Update cookies with user information
                HttpCookie userNameCookie = new HttpCookie("email", authenticatedUser.Email);
                userNameCookie.Expires = DateTime.Now.AddDays(7);
                Response.Cookies.Add(userNameCookie);

                HttpCookie isLoggedInCookie = new HttpCookie("IsLoggedIn", "true");
                isLoggedInCookie.Expires = DateTime.Now.AddDays(1);
                Response.Cookies.Add(isLoggedInCookie);

                ((SiteMaster)this.Master).ShowNotification("Successfully logged in!", "success");


                // Redirect the user to the home page after successful login
                Response.Redirect("~/Default.aspx");
            }
        }
    }
}