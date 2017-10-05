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


namespace WhereToBuy.web.App.QuotationRules.QuotationRule
{
    public partial class QuotationRule : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ((MessageUC)QuotationRuleUC.FindControl("MessageUC")).SubmitButtonClick += QuotationRule_MessageButton;

            ((StocksSelBox)(QuotationRuleUC.FindControl("StocksSelBox"))).SubmitButtonClick += QuotationRule_StockClickButton;
            ((StocksSelBox)(QuotationRuleUC.FindControl("StocksSelBox1"))).SubmitButtonClick += QuotationRule_StockClickButton;
            ((SuppliersSelBox)(QuotationRuleUC.FindControl("SuppliersSelBox"))).SubmitButtonClick += QuotationRule_SupplierClickButton;
            ((BrandsSelBox)(QuotationRuleUC.FindControl("BrandsSelBox"))).SubmitButtonClick += QuotationRule_BrandClickButton;
            ((CategoriesSelBox)(QuotationRuleUC.FindControl("CategoriesSelBox"))).SubmitButtonClick += QuotationRule_CategorieClickButton;
        }

        private void QuotationRule_CategorieClickButton(object sender, CategoriesSelBoxEventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "bdc", "invokeDummyButton('" + "Categories" + "');", true);
        }

        private void QuotationRule_BrandClickButton(object sender, BrandSelBoxEventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "bdc", "invokeDummyButton('" + "Brands" + "');", true);
        }

        private void QuotationRule_SupplierClickButton(object sender, SupplierSelBoxEventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "bdc", "invokeDummyButton('" + "Suppliers" + "');", true);
        }

        private void QuotationRule_StockClickButton(object sender, StocksSelBoxEventArgs e)
        {
            string btn = ((Button)sender).ClientID;

            ScriptManager.RegisterStartupScript(this, this.GetType(), "bdc", "showDropdown('" + btn + "');", true);
        }



        private void QuotationRule_MessageButton(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ic", "invokeButtonClick();", true);

        }

        
    }
}