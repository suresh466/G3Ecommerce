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
            string query = "SELECT category_id, category_name FROM Categories";
            Console.WriteLine(query);
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
                

                        // Generate HTML for category card
                        LiteralControl categoryCard = new LiteralControl();
                        categoryCard.Text = $@"
                        <div class='col-lg-4 col-md-6 mb-4'>
                            <div class='card'>
                                <div class='card-body'>
                                    <h5 class='card-title'>{categoryName}</h5>
                                    <p class='card-text'>Description of {categoryName}.</p>
                                    <a href='Items.aspx?category={reader["category_id"]}' class='btn btn-primary'>Explore</a>
                                </div>
                            </div>
                        </div>";

                        // Add category card to the placeholder
                        categoryPlaceholder.Controls.Add(categoryCard);
                    }
                }
                else
                {
                    // Handle case where no categories are found
                }

                reader.Close();
            }
        }
        }
}