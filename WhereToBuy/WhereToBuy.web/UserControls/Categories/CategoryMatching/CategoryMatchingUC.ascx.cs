using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WhereToBuy.entities;
using WhereToBuy.web.UserControls.Suppliers.SuppliersSelBox;

namespace WhereToBuy.web.UserControls.Categories.CategoryMatching
{
    public partial class CategoryMatchingUC : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SuppliersSelBox.SelectedSupplierUpdate += CategoryMatchingUC_SelectedSupplierUpdate;
            SuppliersSelBox.SupplierSelBoxMessage += CategoryMatchingUC_SupplierSelBoxMessage;

            CategoriesSelBox.SelectedCategoryUpdate += CategoryMatchingUC_SelectedCategoryUpdate;
            CategoriesSelBox.CategoriesSelBoxMessage += CategoryMatchingUC_CategorySelBoxMessage;
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

        private void CategoryMatchingUC_CategorySelBoxMessage(object sender, CategoriesSelBox.CategoriesSelBoxEventArgs e)
        {
            this.MessageUC.ShowError("Error", e.Message);
            return;
        }

        private void CategoryMatchingUC_SelectedCategoryUpdate(object sender, CategoriesSelBox.CategoriesSelBoxEventArgs e)
        {
            SetSelectedCategory(e.Category);
        }


        private void CategoryMatchingUC_SupplierSelBoxMessage(object sender, Suppliers.SuppliersSelBox.SupplierSelBoxEventArgs e)
        {
            this.MessageUC.ShowError("Error", e.Message);
            return;
        }

        private void CategoryMatchingUC_SelectedSupplierUpdate(object sender, Suppliers.SuppliersSelBox.SupplierSelBoxEventArgs e)
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

            LoadCategoryMatching(supplier.Code, code);
        }
    }
}