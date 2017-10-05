using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WhereToBuy.entities;
using WhereToBuy.web.UserControls;
using WhereToBuy.web.UserControls.Brands.BrandsSelBox;
using WhereToBuy.web.UserControls.Suppliers.SuppliersSelBox;

namespace WhereToBuy.web.App.Brands.BrandMatching
{
    public partial class BrandMatching : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ((MessageUC)(BrandMatchingUC.FindControl("MessageUC"))).SubmitButtonClick += BrandMatching_MessageButton;
            ((SuppliersSelBox)(BrandMatchingUC.FindControl("SuppliersSelBox"))).SubmitButtonClick += BrandMatching_SupplierClickButton;
            ((BrandsSelBox)(BrandMatchingUC.FindControl("BrandsSelBox"))).SubmitButtonClick += BrandMatching_BrandClickButton;
        }


        private void BrandMatching_BrandClickButton(object sender, BrandSelBoxEventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "bdc", "invokeDummyButton('" + "Brands" + "');", true);
        }


        private void BrandMatching_MessageButton(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ic", "invokeButtonClick();", true);
        }


        private void BrandMatching_SupplierClickButton(object sender, SupplierSelBoxEventArgs e)
        {
            string arg = "Suppliers";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "sdc", "invokeDummyButton('" + arg + "');", true);
        }
    }
}