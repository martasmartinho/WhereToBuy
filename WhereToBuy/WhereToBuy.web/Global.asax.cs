using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using WhereToBuy.core;

namespace WhereToBuy.web
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
           
            string connectionString;

            //core
            connectionString = (string)System.Configuration.ConfigurationManager.ConnectionStrings["WhereToBuyDatabase"].ConnectionString.ToString().TrimEnd();
           

            //Global variables
            Application["ConnectionString"] = connectionString;
            
            //pages
            Application["Images"] = (string)System.Configuration.ConfigurationManager.AppSettings["Images"];
            Application["Default"] = (string)System.Configuration.ConfigurationManager.AppSettings["Default"];
            Application["UserPage"] = (string)System.Configuration.ConfigurationManager.AppSettings["UserPage"];
            Application["BrandMatchingPage"] = (string)System.Configuration.ConfigurationManager.AppSettings["BrandMatchingPage"];
            Application["BrandsMatchingPage"] = (string)System.Configuration.ConfigurationManager.AppSettings["BrandsMatchingPage"];
            Application["BrandPage"] = (string)System.Configuration.ConfigurationManager.AppSettings["BrandPage"];
            Application["BrandsPage"] = (string)System.Configuration.ConfigurationManager.AppSettings["BrandsPage"];
            Application["StateMatchingPage"] = (string)System.Configuration.ConfigurationManager.AppSettings["StateMatchingPage"];
            Application["StatesMatchingPage"] = (string)System.Configuration.ConfigurationManager.AppSettings["StatesMatchingPage"];
            Application["StatePage"] = (string)System.Configuration.ConfigurationManager.AppSettings["StatePage"];
            Application["StatesPage"] = (string)System.Configuration.ConfigurationManager.AppSettings["StatesPage"];
            Application["TaxMatchingPage"] = (string)System.Configuration.ConfigurationManager.AppSettings["TaxMatchingPage"];
            Application["TaxesMatchingPage"] = (string)System.Configuration.ConfigurationManager.AppSettings["TaxesMatchingPage"];
            Application["TaxPage"] = (string)System.Configuration.ConfigurationManager.AppSettings["TaxPage"];
            Application["TaxesPage"] = (string)System.Configuration.ConfigurationManager.AppSettings["TaxesPage"];
            Application["WorryingTermPage"] = (string)System.Configuration.ConfigurationManager.AppSettings["WorryingTermPage"];
            Application["WorryingTermsPage"] = (string)System.Configuration.ConfigurationManager.AppSettings["WorryingTermsPage"];
            Application["WarningTypePage"] = (string)System.Configuration.ConfigurationManager.AppSettings["WarningTypePage"];
            Application["WarningTypesPage"] = (string)System.Configuration.ConfigurationManager.AppSettings["WarningTypesPage"];
            Application["SupplementMatchingPage"] = (string)System.Configuration.ConfigurationManager.AppSettings["SupplementMatchingPage"];
            Application["SupplementsMatchingPage"] = (string)System.Configuration.ConfigurationManager.AppSettings["SupplementsMatchingPage"];
            Application["SupplementPage"] = (string)System.Configuration.ConfigurationManager.AppSettings["SupplementPage"];
            Application["SupplementsPage"] = (string)System.Configuration.ConfigurationManager.AppSettings["SupplementsPage"];
            Application["CategoryMatchingPage"] = (string)System.Configuration.ConfigurationManager.AppSettings["CategoryMatchingPage"];
            Application["CategoriesMatchingPage"] = (string)System.Configuration.ConfigurationManager.AppSettings["CategoriesMatchingPage"];
            Application["CategoryPage"] = (string)System.Configuration.ConfigurationManager.AppSettings["CategoryPage"];
            Application["CategoriesPage"] = (string)System.Configuration.ConfigurationManager.AppSettings["CategoriesPage"];
            Application["StockMatchingPage"] = (string)System.Configuration.ConfigurationManager.AppSettings["StockMatchingPage"];
            Application["StocksMatchingPage"] = (string)System.Configuration.ConfigurationManager.AppSettings["StocksMatchingPage"];
            Application["StockPage"] = (string)System.Configuration.ConfigurationManager.AppSettings["StockPage"];
            Application["StocksPage"] = (string)System.Configuration.ConfigurationManager.AppSettings["StocksPage"];
            Application["SupplierPage"] = (string)System.Configuration.ConfigurationManager.AppSettings["SupplierPage"];
            Application["SuppliersPage"] = (string)System.Configuration.ConfigurationManager.AppSettings["SuppliersPage"];
            Application["QuotationRulePage"] = (string)System.Configuration.ConfigurationManager.AppSettings["QuotationRulePage"];
            Application["QuotationRulesPage"] = (string)System.Configuration.ConfigurationManager.AppSettings["QuotationRulesPage"];
            Application["QuotationWarningsPage"] = (string)System.Configuration.ConfigurationManager.AppSettings["QuotationWarningsPage"];
            Application["CatalogPage"] = (string)System.Configuration.ConfigurationManager.AppSettings["CatalogPage"];
            Application["CatalogsPage"] = (string)System.Configuration.ConfigurationManager.AppSettings["CatalogsPage"];
            Application["ClassPage"] = (string)System.Configuration.ConfigurationManager.AppSettings["ClassPage"];
            Application["ClassesPage"] = (string)System.Configuration.ConfigurationManager.AppSettings["ClassesPage"];
            Application["ProductMatchingPage"] = (string)System.Configuration.ConfigurationManager.AppSettings["ProductMatchingPage"];
            Application["ProductsMatchingPage"] = (string)System.Configuration.ConfigurationManager.AppSettings["ProductsMatchingPage"];
            Application["ProductPage"] = (string)System.Configuration.ConfigurationManager.AppSettings["ProductPage"];
            Application["ProductsPage"] = (string)System.Configuration.ConfigurationManager.AppSettings["ProductsPage"];
            Application["QuotationPage"] = (string)System.Configuration.ConfigurationManager.AppSettings["QuotationPage"];
            Application["QuotationsPage"] = (string)System.Configuration.ConfigurationManager.AppSettings["QuotationsPage"];
            

        }

        protected void Session_Start(object sender, EventArgs e)
        {
            Session["ActualUser"] = null;
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}