using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WhereToBuy.web.UserControls;
using WhereToBuy.web.UserControls.Brands.BrandsSelBox;
using WhereToBuy.web.UserControls.Categories.CategoriesSelBox;
using WhereToBuy.web.UserControls.Stocks.StocksSelBox;
using WhereToBuy.web.UserControls.Suppliers.SuppliersSelBox;

namespace WhereToBuy.web.App.QuotationRules.QuotationRules
{
    public partial class QuotationRules : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ((MessageUC)(this.QuotationRulesUC.FindControl("MessageUC"))).SubmitButtonClick += QuotationRules_MessageButton;
            ((SuppliersSelBox)(QuotationRulesUC.FindControl("SuppliersSelBox"))).SubmitButtonClick += QuotationRulesSuppliers_ClickButton;
            ((CategoriesSelBox)(QuotationRulesUC.FindControl("CategoriesSelBox"))).SubmitButtonClick += QuotationRulesCategories_ClickButton;
            ((BrandsSelBox)(QuotationRulesUC.FindControl("BrandsSelBox"))).SubmitButtonClick += QuotationRulesBrands_ClickButton;
            ((StocksSelBox)(QuotationRulesUC.FindControl("StocksSelBox"))).SubmitButtonClick += QuotationRulesStocks_ClickButton;


            // Autentication validation
            if (Session["ActualUser"] == null)
            {
                string returnUrlQueryString;

                returnUrlQueryString = string.Format("returnUrl={0}", Server.UrlEncode(Request.AppRelativeCurrentExecutionFilePath));
                if (Request.QueryString.Count > 0)
                {
                    returnUrlQueryString += Server.UrlEncode(string.Format("?{0}", Request.QueryString.ToString()));
                }

                return;
            }


        }

        private void QuotationRulesStocks_ClickButton(object sender, StocksSelBoxEventArgs e)
        {
            string btn = ((Button)sender).ClientID;

            ScriptManager.RegisterStartupScript(this, this.GetType(), "bdc", "showDropdown('" + btn + "');", true);
        }

        private void QuotationRulesBrands_ClickButton(object sender, BrandSelBoxEventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "sp", "invokeDummyButton('" + "Brands" + "');", true);
        }

        private void QuotationRulesCategories_ClickButton(object sender, CategoriesSelBoxEventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "sp", "invokeDummyButton('" + "Categories" + "');", true);
        }

        private void QuotationRulesSuppliers_ClickButton(object sender, SupplierSelBoxEventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "sp", "invokeDummyButton('" + "Suppliers" + "');", true);
        }



        private void QuotationRules_MessageButton(object sender, EventArgs e)
        {
            //string btn = ((Button)sender).ClientID;

            ScriptManager.RegisterStartupScript(this, this.GetType(), "bdc", "invokeButtonClick();", true);
        }
    }
}