using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WhereToBuy.web.UserControls;
using WhereToBuy.web.UserControls.Supplements.SuppementsSelBox;
using WhereToBuy.web.UserControls.Suppliers.SuppliersSelBox;

namespace WhereToBuy.web.App.Supplements.SupplementMatching
{
    public partial class SupplementMatching : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ((MessageUC)(SupplementMatchingUC.FindControl("MessageUC"))).SubmitButtonClick += SupplementMatching_MessageButton;
            ((SuppliersSelBox)(SupplementMatchingUC.FindControl("SuppliersSelBox"))).SubmitButtonClick += SupplementMatching_SupplierClickButton;

            ((SupplementsSelBox)(SupplementMatchingUC.FindControl("SupplementsSelBox"))).SubmitButtonClick += SupplementMatching_SupplementClickButton;



            // Load page
            if (!Page.IsPostBack)
            {

            }
        }

        private void SupplementMatching_SupplementClickButton(object sender, SupplementsSelBoxEventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "bdc", "invokeDummyButton('" + "Supplements" + "');", true);
        }



        private void SupplementMatching_MessageButton(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ic", "invokeButtonClick();", true);

        }

        private void SupplementMatching_SupplierClickButton(object sender, SupplierSelBoxEventArgs e)
        {
            string arg = "Suppliers";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "sdc", "invokeDummyButton('" + arg + "');", true);
        }

    }
}