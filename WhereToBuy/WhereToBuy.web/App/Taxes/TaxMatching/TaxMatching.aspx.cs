using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WhereToBuy.entities;
using WhereToBuy.web.UserControls;
using WhereToBuy.web.UserControls.Suppliers.SuppliersSelBox;
using WhereToBuy.web.UserControls.Taxes.TaxSelBox;

namespace WhereToBuy.web.App.Taxes.TaxMatching
{
    public partial class TaxMatching : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ((MessageUC)(TaxMatchingUC.FindControl("MessageUC"))).SubmitButtonClick += TaxMatching_MessageButton;
            ((SuppliersSelBox)(TaxMatchingUC.FindControl("SuppliersSelBox"))).SubmitButtonClick += TaxMatching_SupplierClickButton;

            ((TaxSelBox)(TaxMatchingUC.FindControl("TaxSelBox"))).SubmitButtonClick += TaxMatching_TaxClickButton;

         

            // Load page
            if (!Page.IsPostBack)
            {
               
            }
        }

        private void TaxMatching_TaxClickButton(object sender, UserControls.Taxes.TaxSelBox.TaxesSelBoxEventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "bdc", "invokeDummyButton('" + "Taxes" + "');", true);
        }

       

        private void TaxMatching_MessageButton(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ic", "invokeButtonClick();", true);

        }

        private void TaxMatching_SupplierClickButton(object sender, SupplierSelBoxEventArgs e)
        {
            string arg = "Suppliers";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "sdc", "invokeDummyButton('" + arg + "');", true);
        }

    }
}