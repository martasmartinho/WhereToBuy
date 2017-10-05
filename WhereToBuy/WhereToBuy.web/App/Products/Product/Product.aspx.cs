using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WhereToBuy.web.UserControls;
using WhereToBuy.web.UserControls.Brands.BrandsSelBox;
using WhereToBuy.web.UserControls.Categories.CategoriesSelBox;
using WhereToBuy.web.UserControls.Suppliers.SuppliersSelBox;
using WhereToBuy.web.UserControls.Taxes.TaxSelBox;

namespace WhereToBuy.web.App.Products.Product
{
    public partial class Product : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ((MessageUC)(ProductUC.FindControl("MessageUC"))).SubmitButtonClick += Product_MessageButton;
            ((BrandsSelBox)(ProductUC.FindControl("BrandsSelBox"))).SubmitButtonClick += Product_BrandClickButton;
            ((CategoriesSelBox)(ProductUC.FindControl("CategoriesSelBox"))).SubmitButtonClick += Product_CategoryClickButton;
            ((TaxSelBox)(ProductUC.FindControl("TaxesSelBox"))).SubmitButtonClick += Product_TaxClickButton;
            //((SuppliersSelBox)(ProductUC.FindControl("SuppliersSelBox"))).SubmitButtonClick += Product_SuppliersClickButton;
        }

        //private void Product_SuppliersClickButton(object sender, SupplierSelBoxEventArgs e)
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "bdc", "invokeDummyButton('" + "Suppliers" + "');", true);
        //}


        private void Product_BrandClickButton(object sender, BrandSelBoxEventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "bdc", "invokeDummyButton('" + "Brands" + "');", true);
        }

        private void Product_TaxClickButton(object sender, TaxesSelBoxEventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "bdc", "invokeDummyButton('" + "Taxes" + "');", true);
        }


        private void Product_MessageButton(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ic", "invokeButtonClick();", true);
        }


       
        private void Product_CategoryClickButton(object sender, CategoriesSelBoxEventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "bdc", "invokeDummyButton('" + "Categories" + "');", true);
        }
    }
}