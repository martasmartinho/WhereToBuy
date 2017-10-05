using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WhereToBuy.entities;

namespace WhereToBuy.web.UserControls.Taxes.TaxMatching
{
    public partial class TaxMatchingUC : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SuppliersSelBox.SelectedSupplierUpdate += TaxMatchingUC_SelectedSupplierUpdate;
            SuppliersSelBox.SupplierSelBoxMessage += TaxMatchingUC_SupplierSelBoxMessage;

            TaxSelBox.SelectedTaxUpdate += TaxMatchingUC_SelectedTaxUpdate;
            TaxSelBox.TaxesSelBoxMessage += TaxMatchingUC_TaxSelBoxMessage;
            // Load page
            if (!Page.IsPostBack)
            {
                string code = string.Empty;
                string supplierCode = string.Empty;
                DataState dataState = DataState.None;

                if (Page.Request.QueryString["Code"] != null && Page.Request.QueryString["SupplierCode"] != null)
                {
                    code = Page.Request.QueryString["Code"].ToString().TrimEnd();
                    supplierCode = Page.Request.QueryString["SupplierCode"].ToString().TrimEnd();
                }


                // load data
                UpdateData(supplierCode, code, dataState);

            }
        }

        private void TaxMatchingUC_TaxSelBoxMessage(object sender, TaxSelBox.TaxesSelBoxEventArgs e)
        {
            this.MessageUC.ShowError("Error", e.Message);
            return;
        }

        private void TaxMatchingUC_SelectedTaxUpdate(object sender, TaxSelBox.TaxesSelBoxEventArgs e)
        {
            SetSelectedTax(e.Tax);
        }

       
        private void TaxMatchingUC_SupplierSelBoxMessage(object sender, Suppliers.SuppliersSelBox.SupplierSelBoxEventArgs e)
        {
            this.MessageUC.ShowError("Error", e.Message);
            return;
        }

        private void TaxMatchingUC_SelectedSupplierUpdate(object sender, Suppliers.SuppliersSelBox.SupplierSelBoxEventArgs e)
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
            string code = txtCode.Text.TrimStart().TrimEnd();
            Supplier supplier = GetSelectedSupplier();
            if (supplier == null)
            {
                //this.MessageUC.ShowError("Error", "Supplier required");
                return;
            }
           
            LoadTaxMatching(supplier.Code, code);
        }
    }
}