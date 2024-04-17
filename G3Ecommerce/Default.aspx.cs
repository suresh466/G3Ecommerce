using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Configuration;
using System.Web.UI.WebControls;

namespace G3Ecommerce
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCategories();
            }

        }

        private void LoadCategories()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["g3ecommerce"].ConnectionString;

            // SQL query to retrieve categories
            string query = "SELECT category_id, category_name, details, picture_url FROM Categories";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                // Check if there are any categories
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string categoryName = reader["category_name"].ToString();
                        string categoryDetails = reader["details"].ToString();
                        string picture_url = reader["picture_url"].ToString();


                        // Generate HTML for category card
                        LiteralControl categoryCard = new LiteralControl();
                        categoryCard.Text = $@"
<div class='col-lg-4 col-md-6 mb-4'>
    <div class='card'>
        <div class='card-body'>
            <h5 class='card-title'>{categoryName}</h5>
            <p class='card-text'>{categoryDetails}</p>
            <div class='position-relative overflow-hidden img-container' style='width: 300px; height: 200px;'>
                <img class='img-fluid w-100 h-100' src='{picture_url}' alt='{categoryName}'>
                
            </div>
<div class='overlay d-flex align-items-center justify-content-center text-white m-2'>
                    <a href='Items.aspx?category={reader["category_id"]}' class='btn btn-primary'>Explore</a>
                </div>
        </div>
    </div>
</div>";



                        // Add category card to the placeholder
                        categoryPlaceholder.Controls.Add(categoryCard);
                    }
                }
                else
                {
                    ((SiteMaster)this.Master).ShowNotification("Database Error!", "warning");

                    // Handle case where no categories are found
                }

                reader.Close();
            }
        }
        }
}