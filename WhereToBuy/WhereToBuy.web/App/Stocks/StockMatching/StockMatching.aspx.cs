using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WhereToBuy.web.UserControls;
using WhereToBuy.web.UserControls.Stocks.StocksSelBox;
using WhereToBuy.web.UserControls.Suppliers.SuppliersSelBox;

namespace WhereToBuy.web.App.Stocks.StockMatching
{
    public partial class StockMatching : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ((MessageUC)(StockMatchingUC.FindControl("MessageUC"))).SubmitButtonClick += StockMatching_MessageButton;
            ((SuppliersSelBox)(StockMatchingUC.FindControl("SuppliersSelBox"))).SubmitButtonClick += StockMatching_SupplierClickButton;

            ((StocksSelBox)(StockMatchingUC.FindControl("StocksSelBox"))).SubmitButtonClick += StockMatching_StockClickButton;



            // Load page
            if (!Page.IsPostBack)
            {

            }
        }

        private void StockMatching_StockClickButton(object sender, UserControls.Stocks.StocksSelBox.StocksSelBoxEventArgs e)
        {
            string btn = ((Button)sender).ClientID;

            ScriptManager.RegisterStartupScript(this, this.GetType(), "bdc", "showDropdown('" + btn + "');", true);
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "bdc", "invokeDummyButton('" + "Stocks" + "');", true);
        }



        private void StockMatching_MessageButton(object sender, EventArgs e)
        {

            ScriptManager.RegisterStartupScript(this, this.GetType(), "ic", "invokeButtonClick();", true);

        }

        private void StockMatching_SupplierClickButton(object sender, SupplierSelBoxEventArgs e)
        {
            string arg = "Suppliers";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "sdc", "invokeDummyButton('" + arg + "');", true);
        }

    }
}