using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WhereToBuy.entities;

namespace WhereToBuy.web.UserControls.States.StateMatching
{
    public partial class StateMatchingUC : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var res = StatesSelBox.FindControl("txtState");

            string teste = ((TextBox)res).Text;

            SuppliersSelBox.SelectedSupplierUpdate += StatesMatchingUC_SelectedSupplierUpdate;
            SuppliersSelBox.SupplierSelBoxMessage += StatesMatchingUC_SupplierSelBoxMessage;

            StatesSelBox.SelectedStateUpdate += StatesMatchingUC_SelectedStateUpdate;
            StatesSelBox.StateSelBoxMessage += StatesMatchingUC_StateSelBoxMessage;

            if (!IsPostBack)
            {
                string code = string.Empty;
                string supplierCode = string.Empty;
                DataState dataState = DataState.None;

                if (Page.Request.QueryString["Code"] != null && Page.Request.QueryString["SupplierCode"] != null)
                {
                    code = Page.Request.QueryString["Code"].ToString().TrimEnd();
                    supplierCode = Page.Request.QueryString["SupplierCode"].ToString().TrimEnd();
                }

                UpdateData(supplierCode, code, dataState);
            }
        }

        private void StatesMatchingUC_SelectedStateUpdate(object sender, StatesSelBox.StateSelBoxEventArgs e)
        {
            SetSelectedState(e.State);
        }

        private void StatesMatchingUC_StateSelBoxMessage(object sender, StatesSelBox.StateSelBoxEventArgs e)
        {
            this.MessageUC.ShowError("Error", e.Message);
            return;
        }

        private void StatesMatchingUC_SupplierSelBoxMessage(object sender, Suppliers.SuppliersSelBox.SupplierSelBoxEventArgs e)
        {
            this.MessageUC.ShowError("Error", e.Message);
            return;
        }

        private void StatesMatchingUC_SelectedSupplierUpdate(object sender, Suppliers.SuppliersSelBox.SupplierSelBoxEventArgs e)
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
            Supplier supplier=  GetSelectedSupplier();
            if (supplier == null)
            {
                this.MessageUC.ShowError("Error", "Supplier required");
                return;
            }
           
            LoadStateMatching(supplier.Code, code);
        }
    }
}