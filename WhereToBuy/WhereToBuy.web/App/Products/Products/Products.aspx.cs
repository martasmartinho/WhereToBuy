using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WhereToBuy.entities;
using WhereToBuy.web.UserControls;
using WhereToBuy.web.UserControls.Brands.BrandsSelBox;
using WhereToBuy.web.UserControls.Categories.CategoriesSelBox;
using WhereToBuy.web.UserControls.Suppliers.SuppliersSelBox;

namespace WhereToBuy.web.App.Products.Products
{
    public partial class Products : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ((MessageUC)(this.ProductsUC.FindControl("MessageUC"))).SubmitButtonClick += Products_MessageButton;
            ((SuppliersSelBox)(ProductsUC.FindControl("SuppliersSelBox"))).SubmitButtonClick += ProductsSuppliers_ClickButton;
            ((CategoriesSelBox)(ProductsUC.FindControl("CategoriesSelBox"))).SubmitButtonClick += ProductsCategories_ClickButton;
            ((BrandsSelBox)(ProductsUC.FindControl("BrandsSelBox"))).SubmitButtonClick += ProductsBrands_ClickButton;
            


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

      

        private void ProductsBrands_ClickButton(object sender, BrandSelBoxEventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "sp", "invokeDummyButton('" + "Brands" + "');", true);
        }

        private void ProductsCategories_ClickButton(object sender, CategoriesSelBoxEventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "sp", "invokeDummyButton('" + "Categories" + "');", true);
        }

        private void ProductsSuppliers_ClickButton(object sender, SupplierSelBoxEventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "sp", "invokeDummyButton('" + "Suppliers" + "');", true);
        }

        private void Products_MessageButton(object sender, EventArgs e)
        {
            //string btn = ((Button)sender).ClientID;

            ScriptManager.RegisterStartupScript(this, this.GetType(), "bdc", "invokeButtonClick();", true);
        }
    }
}