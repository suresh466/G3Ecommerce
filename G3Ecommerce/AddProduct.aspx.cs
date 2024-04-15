using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI.WebControls;

namespace G3Ecommerce
{
    public partial class AddProduct : System.Web.UI.Page
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["g3ecommerce"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCategories();
                if (!IsValidAdmin())
                {
                    Response.Redirect("AdminLogin.aspx");
                }
            }
        }

        private bool IsValidAdmin()
        {
            HttpCookie adminEmailCookie = Request.Cookies["adminEmail"];
            if (adminEmailCookie != null)
            {
                string adminEmail = adminEmailCookie.Value;
                return AdminExists(adminEmail);
            }
            return false;
        }

        private bool AdminExists(string email)
        {
            string query = "SELECT COUNT(*) FROM Admins WHERE email = @Email";

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Email", email);

                connection.Open();
                int count = (int)command.ExecuteScalar();

                return count > 0;
            }
        }

        protected void btnAddProduct_Click(object sender, EventArgs e)
        {
            string productName = txtProductName.Text.Trim();
            decimal productPrice = Convert.ToDecimal(txtProductPrice.Text.Trim());
            int categoryId = Convert.ToInt32(ddlCategories.SelectedValue);

            AddProductToDatabase(productName, productPrice, categoryId);

            // Redirect to some confirmation page or refresh the page
            Response.Redirect("/AdminDashboard");
        }

        private void AddProductToDatabase(string productName, decimal productPrice, int categoryId)
        {
            string query = "INSERT INTO Items (item_name, item_price, category_id) VALUES (@ProductName, @ProductPrice, @CategoryId)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@ProductName", productName);
                command.Parameters.AddWithValue("@ProductPrice", productPrice);
                command.Parameters.AddWithValue("@CategoryId", categoryId);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        private void LoadCategories()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT category_id, category_name FROM Categories";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        ddlCategories.DataSource = reader;
                        ddlCategories.DataTextField = "category_name";
                        ddlCategories.DataValueField = "category_id";
                        ddlCategories.DataBind();
                    }
                }
            }
        }
    }


}
