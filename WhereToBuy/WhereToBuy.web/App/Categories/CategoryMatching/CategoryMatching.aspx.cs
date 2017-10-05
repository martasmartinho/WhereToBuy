using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WhereToBuy.web.UserControls;
using WhereToBuy.web.UserControls.Categories.CategoriesSelBox;
using WhereToBuy.web.UserControls.Suppliers.SuppliersSelBox;

namespace WhereToBuy.web.App.Categories.CategoryMatching
{
    public partial class CategoryMatching : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ((MessageUC)(CategoryMatchingUC.FindControl("MessageUC"))).SubmitButtonClick += CategoryMatching_MessageButton;
            ((SuppliersSelBox)(CategoryMatchingUC.FindControl("SuppliersSelBox"))).SubmitButtonClick += CategoryMatching_SupplierClickButton;

            ((CategoriesSelBox)(CategoryMatchingUC.FindControl("CategoriesSelBox"))).SubmitButtonClick += CategoryMatching_CategoryClickButton;



            // Load page
            if (!Page.IsPostBack)
            {

            }
        }

        private void CategoryMatching_CategoryClickButton(object sender, CategoriesSelBoxEventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "bdc", "invokeDummyButton('" + "Categories" + "');", true);
        }



        private void CategoryMatching_MessageButton(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ic", "invokeButtonClick();", true);

        }

        private void CategoryMatching_SupplierClickButton(object sender, SupplierSelBoxEventArgs e)
        {
            string arg = "Suppliers";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "sdc", "invokeDummyButton('" + arg + "');", true);
        }

    }
}