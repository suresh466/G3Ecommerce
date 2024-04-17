using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace G3Ecommerce
{
    public partial class AdminDashboard : System.Web.UI.Page
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["g3ecommerce"].ConnectionString;

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

        protected void grdItems_PreRender(object sender, EventArgs e)
        {
            // Ensure the header row is displayed as the table header
            grdItems.HeaderRow.TableSection = TableRowSection.TableHeader;
        }

        protected void gvCategories_PreRender(object sender, EventArgs e)
        {
            // Ensure the header row is displayed as the table header
            gvCategories.HeaderRow.TableSection = TableRowSection.TableHeader;
        }

        protected void dvItem_ItemUpdated(object sender, DetailsViewUpdatedEventArgs e)
        {
            if (e.Exception != null)
            {
                lblError.Text = DatabaseErrorMessage(e.Exception.Message);
                e.ExceptionHandled = true;
                e.KeepInEditMode = true;
            }
            else if (e.AffectedRows == 0)
                lblError.Text = ConcurrencyErrorMessage();
            else
                grdItems.DataBind();
        }

        protected void dvCategory_ItemUpdated(object sender, DetailsViewUpdatedEventArgs e)
        {
            if (e.Exception != null)
            {
                lblError.Text = DatabaseErrorMessage(e.Exception.Message);
                e.ExceptionHandled = true;
                e.KeepInEditMode = true;
            }
            else if (e.AffectedRows == 0)
                lblError.Text = ConcurrencyErrorMessage();
            else
                gvCategories.DataBind();
        }

        protected void dvItem_ItemDeleted(object sender, DetailsViewDeletedEventArgs e)
        {
            if (e.Exception != null)
            {
                lblError.Text = DatabaseErrorMessage(e.Exception.Message);
                e.ExceptionHandled = true;
            }
            else if (e.AffectedRows == 0)
                lblError.Text = ConcurrencyErrorMessage();
            else
                grdItems.DataBind();
        }

        protected void dvCategory_ItemDeleted(object sender, DetailsViewDeletedEventArgs e)
        {
            if (e.Exception != null)
            {
                lblError.Text = DatabaseErrorMessage(e.Exception.Message);
                e.ExceptionHandled = true;
            }
            else if (e.AffectedRows == 0)
                lblError.Text = ConcurrencyErrorMessage();
            else
                gvCategories.DataBind();
        }

        protected void dvItem_ItemInserted(object sender, DetailsViewInsertedEventArgs e)
        {
            if (e.Exception != null)
            {
                lblError.Text = DatabaseErrorMessage(e.Exception.Message);
                e.ExceptionHandled = true;
                e.KeepInInsertMode = true;
            }
            else
                grdItems.DataBind();
        }

        protected void dvCategory_ItemInserted(object sender, DetailsViewInsertedEventArgs e)
        {
            if (e.Exception != null)
            {
                lblError.Text = DatabaseErrorMessage(e.Exception.Message);
                e.ExceptionHandled = true;
                e.KeepInInsertMode = true;
            }
            else
                gvCategories.DataBind();
        }

        protected void dvItem_ItemDeleting(object sender, DetailsViewDeleteEventArgs e)
        {
        }

        protected void dvCategory_ItemDeleting(object sender, DetailsViewDeleteEventArgs e)
        {
        }

        private string DatabaseErrorMessage(string errorMsg)
        {
            return $"<b>A database error has occurred:</b> {errorMsg}";
        }

        private string ConcurrencyErrorMessage()
        {
            return "Another user may have updated that record. Please try again.";
        }

        protected void grdItems_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void gvCategories_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lnkDetails = (LinkButton)e.Row.FindControl("lnkDetails");
                if (lnkDetails != null)
                {
                    lnkDetails.CommandArgument = e.Row.RowIndex.ToString();
                }
            }
        }
      

        protected void gvCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            dvCategory.Visible = true;
        }


    }



}