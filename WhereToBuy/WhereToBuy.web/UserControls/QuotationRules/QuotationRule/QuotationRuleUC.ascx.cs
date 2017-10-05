using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WhereToBuy.web.UserControls.QuotationRules.QuotationRule
{
    public partial class QuotationRuleUC : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SuppliersSelBox.SelectedSupplierUpdate += QuotationRule_SelectedSupplier;
            SuppliersSelBox.SupplierSelBoxMessage += QuotationRule_SupplierMessage;
            CategoriesSelBox.SelectedCategoryUpdate += QuotationRule_SelectedCategory;
            CategoriesSelBox.CategoriesSelBoxMessage += QuotationRule_CategoryMessage;
            BrandsSelBox.SelectedBrandUpdate += QuotationRule_SelectedBrand;
            BrandsSelBox.BrandSelBoxMessage += QuotationRule_BrandMessage;
            StocksSelBox.SelectedStockUpdate += QuotationRule_SelectedStock;
            StocksSelBox1.SelectedStockUpdate += QuotationRule_SelectedStockSubstitute;
            StocksSelBox.StocksSelBoxMessage += QuotationRule_StockMessage;
            // Load page
            if (!Page.IsPostBack)
            {
                string supplier = string.Empty;
                string brand = string.Empty;
                string category = string.Empty;
                string stock = string.Empty;


                if (Page.Request.QueryString["supplier"] != null)
                {
                    supplier = Page.Request.QueryString["supplier"].ToString().TrimEnd();
                    brand = Page.Request.QueryString["brand"].ToString().TrimEnd();
                    category = Page.Request.QueryString["category"].ToString().TrimEnd();
                    stock = Page.Request.QueryString["stock"].ToString().TrimEnd();
                }


                // load data
                UpdateData(supplier, brand, category, stock);

            }
        }

        private void QuotationRule_StockMessage(object sender, Stocks.StocksSelBox.StocksSelBoxEventArgs e)
        {
            this.MessageUC.ShowError("Error", e.Message);
            return;
        }

        private void QuotationRule_BrandMessage(object sender, Brands.BrandsSelBox.BrandSelBoxEventArgs e)
        {
            this.MessageUC.ShowError("Error", e.Message);
            return;
        }

        private void QuotationRule_CategoryMessage(object sender, Categories.CategoriesSelBox.CategoriesSelBoxEventArgs e)
        {
            this.MessageUC.ShowError("Error", e.Message);
            return;
        }

        private void QuotationRule_SupplierMessage(object sender, Suppliers.SuppliersSelBox.SupplierSelBoxEventArgs e)
        {
            this.MessageUC.ShowError("Error", e.Message);
            return;
        }

        private void QuotationRule_SelectedStockSubstitute(object sender, Stocks.StocksSelBox.StocksSelBoxEventArgs e)
        {
            SetSubstituteStock(e.Stock);
        }

        private void QuotationRule_SelectedStock(object sender, Stocks.StocksSelBox.StocksSelBoxEventArgs e)
        {
            SetStock(e.Stock);
        }

        private void QuotationRule_SelectedBrand(object sender, Brands.BrandsSelBox.BrandSelBoxEventArgs e)
        {
            SetBrand(e.Brand);
        }

        private void QuotationRule_SelectedCategory(object sender, Categories.CategoriesSelBox.CategoriesSelBoxEventArgs e)
        {
            SetCategory(e.Category);
        }

        private void QuotationRule_SelectedSupplier(object sender, Suppliers.SuppliersSelBox.SupplierSelBoxEventArgs e)
        {
            SetSupplier(e.Supplier);
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            New();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            Delete();
        }

        protected void UpdatePanel1_Init(object sender, EventArgs e)
        {

        }

        protected void btnOk_Click(object sender, EventArgs e)
        {

            Save();


        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Cancel();
        }

        protected void SelectedItemButton_Click(object sender, EventArgs e)
        {
            switch (((sender as LinkButton).CommandArgument))
            {
                case "OneDay": txtDataReset.Text = DateTime.Now.AddDays(1).ToString("dd-MM-yyyy");
                    break;
                case "TwoDay": txtDataReset.Text = DateTime.Now.AddDays(2).ToString("dd-MM-yyyy");
                    break;
                case "ThreeDay": txtDataReset.Text = DateTime.Now.AddDays(3).ToString("dd-MM-yyyy");
                    break;
                case "FourDay": txtDataReset.Text = DateTime.Now.AddDays(4).ToString("dd-MM-yyyy");
                    break;
                case "FiveDay": txtDataReset.Text = DateTime.Now.AddDays(5).ToString("dd-MM-yyyy");
                    break;
                case "SixDay": txtDataReset.Text = DateTime.Now.AddDays(6).ToString("dd-MM-yyyy");
                    break;
                case "OneWeek": txtDataReset.Text = DateTime.Now.AddDays(7).ToString("dd-MM-yyyy");
                    break;
                case "TwoWeek": txtDataReset.Text = DateTime.Now.AddDays(14).ToString("dd-MM-yyyy");
                    break;
                case "ThreeWeek": txtDataReset.Text = DateTime.Now.AddDays(21).ToString("dd-MM-yyyy");
                    break;
                case "OneMonth": txtDataReset.Text = DateTime.Now.AddMonths(1).ToString("dd-MM-yyyy");
                    break;
                case "TwoMonth": txtDataReset.Text = DateTime.Now.AddMonths(2).ToString("dd-MM-yyyy");
                    break;
                default:
                    break;
            }
        }
    }
}