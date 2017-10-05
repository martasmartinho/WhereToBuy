using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WhereToBuy.entities;

namespace WhereToBuy.web.UserControls.Brands.BrandMatching
{
    public partial class BrandMatchingUC : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string code = string.Empty;
            string supplierCode = string.Empty;
            DataState dataState = DataState.None;

            SuppliersSelBox.SelectedSupplierUpdate += BrandMatchingUC_SelectedSupplierUpdate;
            SuppliersSelBox.SupplierSelBoxMessage += BrandMatchingUC_SupplierSelBoxMessage;
            BrandsSelBox.SelectedBrandUpdate += BrandMatchingUC_SelectedBrandUpdate;
            BrandsSelBox.BrandSelBoxMessage += BrandMatchingUC_BrandSelBoxMessage;

            // Load page
            if (!Page.IsPostBack)
            {
                if (Page.Request.QueryString["Code"] != null && Page.Request.QueryString["SupplierCode"] != null)
                {
                    code = Page.Request.QueryString["Code"].ToString().TrimEnd();
                    supplierCode = Page.Request.QueryString["SupplierCode"].ToString().TrimEnd();
                }

                // load data
                UpdateData(supplierCode, code, dataState);
            }
        }
        

        private void BrandMatchingUC_SelectedBrandUpdate(object sender, BrandsSelBox.BrandSelBoxEventArgs e)
        {
            SetSelectedBrand(e.Brand);
        }


        private void BrandMatchingUC_BrandSelBoxMessage(object sender, BrandsSelBox.BrandSelBoxEventArgs e)
        {
            this.MessageUC.ShowError("Error", e.Message);
            return;
        }


        private void BrandMatchingUC_SupplierSelBoxMessage(object sender, Suppliers.SuppliersSelBox.SupplierSelBoxEventArgs e)
        {
            this.MessageUC.ShowError("Error", e.Message);
            return;
        }


        private void BrandMatchingUC_SelectedSupplierUpdate(object sender, Suppliers.SuppliersSelBox.SupplierSelBoxEventArgs e)
        {
            SetSelectedSupplier(e.Supplier);
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

            LoadBrandMatching(supplier.Code, code);
        }
    }
}