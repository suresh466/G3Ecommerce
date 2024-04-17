using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace G3Ecommerce
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void ShowNotification(string message, string category)
        {
            string script = $@"toastr.{category}('{message}', '{category.First().ToString().ToUpper() + category.Substring(1)}')";
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "notificationScript", script, true);
        }

        // Method to hide the notification panel
        public void HideNotification()
        {
            // notificationPanel.Visible = false;
        }
    }
}