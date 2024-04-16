using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace G3Ecommerce
{
    public partial class ItemManager : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void grdItems_PreRender(object sender, EventArgs e)
        {
            // Ensure the header row is displayed as the table header
            grdItems.HeaderRow.TableSection = TableRowSection.TableHeader;
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

        protected void dvItem_ItemDeleting(object sender, DetailsViewDeleteEventArgs e)
        {
            // You can add any specific logic here for item deleting if needed
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
    }
}