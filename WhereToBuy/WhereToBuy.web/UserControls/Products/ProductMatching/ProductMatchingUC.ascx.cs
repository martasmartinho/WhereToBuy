using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WhereToBuy.entities;

namespace WhereToBuy.web.UserControls.Products.ProductMatching
{
    public partial class ProductMatchingUC : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            string code = string.Empty;
            string supplierCode = string.Empty;
            string supplement = string.Empty;
            DataState dataState = DataState.None;

            SuppliersSelBox.SelectedSupplierUpdate += ProductMatchingUC_SelectedSupplierUpdate;
            SuppliersSelBox.SupplierSelBoxMessage += ProductMatchingUC_SupplierSelBoxMessage;
            ProductsSelBox.SelectedProductUpdate += ProductMatchingUC_SelectedProductUpdate;
            ProductsSelBox.ProductSelBoxMessage += ProductMatchingUC_ProductSelBoxMessage;
            StocksSelBox.SelectedStockUpdate += ProductMatchingUC_SelectedStockUpdate;
            StocksSelBox.StocksSelBoxMessage += ProductMatchingUC_StockMessage;

            // Load page
            if (!Page.IsPostBack)
            {
                if (Page.Request.QueryString["Code"] != null && Page.Request.QueryString["SupplierCode"] != null && Page.Request.QueryString["Supplement"] != null)
                {
                    code = Page.Request.QueryString["Code"].ToString().TrimEnd();
                    supplierCode = Page.Request.QueryString["SupplierCode"].ToString().TrimEnd();
                    supplement = Page.Request.QueryString["Supplement"].ToString().TrimEnd();
                }

                // load data
                UpdateData(supplierCode, code, supplement, dataState);
            }
        }


        private void ProductMatchingUC_SelectedProductUpdate(object sender, ProductsSelBox.ProductSelBoxEventArgs e)
        {
            SetSelectedProduct(e.Product);
        }


        private void ProductMatchingUC_ProductSelBoxMessage(object sender, ProductsSelBox.ProductSelBoxEventArgs e)
        {
            this.MessageUC.ShowError("Error", e.Message);
            return;
        }


        private void ProductMatchingUC_SupplierSelBoxMessage(object sender, Suppliers.SuppliersSelBox.SupplierSelBoxEventArgs e)
        {
            this.MessageUC.ShowError("Error", e.Message);
            return;
        }


        private void ProductMatchingUC_SelectedSupplierUpdate(object sender, Suppliers.SuppliersSelBox.SupplierSelBoxEventArgs e)
        {
            SetSelectedSupplier(e.Supplier);
        }

        private void ProductMatchingUC_SelectedStockUpdate(object sender, Stocks.StocksSelBox.StocksSelBoxEventArgs e)
        {
            SetSelectedStock(e.Stock);
        }

        private void ProductMatchingUC_StockMessage(object sender, Stocks.StocksSelBox.StocksSelBoxEventArgs e)
        {
            this.MessageUC.ShowError("Error", e.Message);
            return;
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

        protected void lnkCodeSearch_Click(object sender, EventArgs e)
        {
            Supplier supplier = GetSelectedSupplier();
            string code = txtCode.Text.TrimStart().TrimEnd();

            if (supplier == null)
            {
                this.MessageUC.ShowError("QuotationWarning", "Supplier is required");
                UpdatePanel1.Update();
                return;
            }

            if (code.TrimStart().TrimEnd() == "")
            {
                this.MessageUC.ShowError("QuotationWarning", "Code is required");
                return;
            }

            LoadProductMatching(supplier.Code, code, "");
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