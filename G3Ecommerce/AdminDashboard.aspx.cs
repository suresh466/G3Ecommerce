using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace G3Ecommerce
{
    public partial class AdminDashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Cookies["adminEmail"] == null)
            {
                Response.Redirect("AdminLogin.aspx");
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            
            if (Request.Cookies["adminEmail"] != null)
            {
                HttpCookie adminEmailCookie = new HttpCookie("adminEmail");
                adminEmailCookie.Expires = DateTime.Now.AddDays(-1); // Set the expiration date in the past to invalidate the cookie
                Response.Cookies.Add(adminEmailCookie);
            }

            Response.Redirect("AdminLogin.aspx");
        }
    }
}