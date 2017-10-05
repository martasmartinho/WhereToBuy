using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WhereToBuy.web.UserControls;
using WhereToBuy.web.UserControls.Products.ProductsSelBox;
using WhereToBuy.web.UserControls.Stocks.StocksSelBox;
using WhereToBuy.web.UserControls.Suppliers.SuppliersSelBox;

namespace WhereToBuy.web.App.Products.ProductMatching
{
    public partial class ProductMatching : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ((MessageUC)(ProductMatchingUC.FindControl("MessageUC"))).SubmitButtonClick += ProductMatching_MessageButton;
            ((SuppliersSelBox)(ProductMatchingUC.FindControl("SuppliersSelBox"))).SubmitButtonClick += ProductMatching_SupplierClickButton;
            ((ProductsSelBox)(ProductMatchingUC.FindControl("ProductsSelBox"))).SubmitButtonClick += ProductMatching_ProductClickButton;
            ((StocksSelBox)(ProductMatchingUC.FindControl("StocksSelBox"))).SubmitButtonClick += ProductMatching_StockClickButton;
        }


        private void ProductMatching_ProductClickButton(object sender, ProductSelBoxEventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "bdc", "invokeDummyButton('" + "Products" + "');", true);
        }


        private void ProductMatching_MessageButton(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ic", "invokeButtonClick();", true);
        }


        private void ProductMatching_SupplierClickButton(object sender, SupplierSelBoxEventArgs e)
        {
            string arg = "Suppliers";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "sdc", "invokeDummyButton('" + arg + "');", true);
        }

        private void ProductMatching_StockClickButton(object sender, StocksSelBoxEventArgs e)
        {
            string btn = ((Button)sender).ClientID;

            ScriptManager.RegisterStartupScript(this, this.GetType(), "bdc", "showDropdown('" + btn + "');", true);
        }
    }
}