using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using WhereToBuy.core;
using WhereToBuy.entities;
using WhereToBuy.entities.specs;
using WhereToBuy.utils.GlobalVariables;

namespace WhereToBuy.web.UserControls.Products.ProductMatching
{
    public partial class ProductMatchingUC
    {
        CoreEngine engine;

        public void UpdateData(string supplierCode, string code, string supplement, DataState dataState)
        {
            TextBox textBox = (TextBox)ProductsSelBox.FindControl("txtProduct");

            if (txtCode.MaxLength == 0)
            {
                SetFormEnvironment();
            }

            //txtCode.Text = code.TrimEnd();

            if (supplierCode != "" && code != "" && supplement != "")
            {
                LoadProductMatching(supplierCode, code, supplement);
                textBox.Focus();
            }

            UpdatePanel1.Update();
        }


        void SetFormEnvironment()
        {
            var tbx = SuppliersSelBox.FindControl("txtSupplier");
            ((TextBox)tbx).Attributes.Add("required", "");

           
            txtCode.MaxLength = ProductMatchingSpecs.Code_MaxSize;
            txtSupplement.MaxLength = ProductMatchingSpecs.Supplement_MaxSize;

            //// CONFIGURAR REQUIRED'S
            if (ProductMatchingSpecs.Code_Necesssary)
            {
                txtCode.Attributes.Add("required", "");
            }

            if (ProductMatchingSpecs.Supplement_Necesssary)
            {
                txtSupplement.Attributes.Add("required", "");
            }

            SetListBoxEnvironment();

            New();
        }

        void BindObjectToPage()
        {
            var tbx = ProductsSelBox.FindControl("txtProduct");
            ((TextBox)tbx).Text = string.Empty;
            if (SelectedProductExist)
            {
                ((TextBox)tbx).Text = (GetSelectedProduct()).ToString();
            }
            tbx = StocksSelBox.FindControl("txtStock");
            ((TextBox)tbx).Text = string.Empty;
            if (SelectedStockExist)
            {
                ((TextBox)tbx).Text = this.selectedMatching.ReplacementStock.ToString();
            }
            txtCode.Text = this.selectedMatching.Code;
            txtSupplement.Text = this.selectedMatching.Supplement;
            txtExpiration.Text = this.selectedMatching.QuotationExpireHours.ToString();
            txtNotes.Text = this.selectedMatching.Notes;
            txtDataReset.Text = "";
            if (this.selectedMatching.DataReset != null)
            {
                txtDataReset.Text = ((DateTime)this.selectedMatching.DataReset).ToString("dd-MM-yyyy");
            }

            cbxInactive.Checked = this.selectedMatching.Inactive;
            lblMode.Text = (this.selectedMatching.EditionMode == false) ? GlobalVariables.Resource.GetString("InsertString", GlobalVariables.Culture) : GlobalVariables.Resource.GetString("UpdateString", GlobalVariables.Culture);
            lblCreation.Text = (this.selectedMatching.EditionMode == false) ? GlobalVariables.Resource.GetString("AutomaticString", GlobalVariables.Culture) : this.selectedMatching.Creation.ToString("dddd, dd-MMM-yyyy HH:mm");
            ViewState["Version"] = this.selectedMatching.Version.ToBinary().ToString();
            ViewState["Creation"] = this.selectedMatching.Creation.ToBinary().ToString();
            ViewState["EditionMode"] = (this.selectedMatching.EditionMode == false) ? "false" : "true";
            UpdatePanel1.Update();
        }


        private void SetListBoxEnvironment()
        {

            txtDataReset.Text = "";
            lvDataReset.DataKeyNames = new string[] { "Key", "Value" };
            //lvDataReset.Items.Clear();
            Dictionary<entities.DataReset, string> dic = new Dictionary<DataReset, string>();
            List<entities.DataReset> list = new List<entities.DataReset>();
            dic.Add(DataReset.OneDay, GlobalVariables.Resource.GetString("OneDayString", GlobalVariables.Culture));
            dic.Add(DataReset.TwoDay, GlobalVariables.Resource.GetString("TwoDayString", GlobalVariables.Culture));
            dic.Add(DataReset.ThreeDay, GlobalVariables.Resource.GetString("ThreeDayString", GlobalVariables.Culture));
            dic.Add(DataReset.FourDay, GlobalVariables.Resource.GetString("FourDayString", GlobalVariables.Culture));
            dic.Add(DataReset.FiveDay, GlobalVariables.Resource.GetString("FiveDayString", GlobalVariables.Culture));
            dic.Add(DataReset.SixDay, GlobalVariables.Resource.GetString("SixDayString", GlobalVariables.Culture));
            dic.Add(DataReset.OneWeek, GlobalVariables.Resource.GetString("OneWeekString", GlobalVariables.Culture));
            dic.Add(DataReset.TwoWeek, GlobalVariables.Resource.GetString("TwoWeekString", GlobalVariables.Culture));
            dic.Add(DataReset.ThreeWeek, GlobalVariables.Resource.GetString("ThreeWeekString", GlobalVariables.Culture));
            dic.Add(DataReset.OneMonth, GlobalVariables.Resource.GetString("OneMonthString", GlobalVariables.Culture));
            dic.Add(DataReset.TwoMonth, GlobalVariables.Resource.GetString("TwoMonthString", GlobalVariables.Culture));


            lvDataReset.DataSource = dic;
            lvDataReset.DataBind();
            //UpdatePanel1.Update();

        }

       

        void BindPageToObjet()
        {
            this.selectedMatching = new WhereToBuy.entities.ProductMatching();
            this.selectedMatching.Supplier = GetSelectedSupplier();
            this.selectedMatching.Code = txtCode.Text.TrimEnd().ToUpper();
            this.selectedMatching.Supplement = txtSupplement.Text.TrimEnd();
            this.selectedMatching.MapTo = GetSelectedProduct();
            this.selectedMatching.ReplacementStock = GetSelectedStock();
            this.selectedMatching.QuotationExpireHours = int.Parse(txtExpiration.Text);
            this.selectedMatching.Notes = txtNotes.Text.TrimEnd().TrimStart();
            if (txtDataReset.Text != "")
            {
                this.selectedMatching.DataReset = Convert.ToDateTime(txtDataReset.Text);
            }
            else
            {
                this.selectedMatching.DataReset = null;
            }
           
            this.selectedMatching.NeedPreventionFakeStock = cbxDPPD.Checked;
            this.selectedMatching.NeedPreventionPricesOut = cbxDPPD2.Checked;
            this.selectedMatching.Inactive = cbxInactive.Checked;
            this.selectedMatching.Version = DateTime.FromBinary(long.Parse(ViewState["Version"].ToString()));
            this.selectedMatching.Creation = DateTime.FromBinary(long.Parse(ViewState["Creation"].ToString()));
            this.selectedMatching.EditionMode = (ViewState["EditionMode"].ToString().TrimEnd().ToLower() == "false") ? false : true;
        }


        void LoadProductMatching(string supplierCode, string code, string supplement)
        {
            try
            {
                this.engine = new CoreEngine(Application["ConnectionString"].ToString().TrimEnd(), (User)Session["ActualUser"]);
                this.selectedMatching = this.engine.ProductsMatching.Get(supplierCode, code, supplement, 1, 1, 1);
                SetSelectedMatching(this.selectedMatching);
                SetSelectedSupplier(this.selectedMatching.Supplier);
                SuppliersSelBox.UpdateData(GetSelectedSupplier(), true);


                if (this.selectedMatching.MapTo != null)
                {
                    SetSelectedProduct(this.selectedMatching.MapTo);
                    
                }

                if (this.selectedMatching.ReplacementStock != null)
                {
                    SetSelectedStock(selectedMatching.ReplacementStock);
                }


                BindObjectToPage();
                engine = null;
            }
            catch (MyException ex)
            {
                this.MessageUC.ShowError("Erro", ex.Message);
                return;
            }
            catch (Exception ex)
            {
                this.MessageUC.ShowError("Erro", ex.Message);
                return;
            }
        }


        void Save()
        {
            BindPageToObjet();

            try
            {
                this.engine = new CoreEngine(Application["ConnectionString"].ToString().TrimEnd(), (User)Session["ActualUser"]);
                this.engine.ProductsMatching.Store(this.selectedMatching);
                engine = null;

                New();
            }
            catch (MyException ex)
            {
                this.MessageUC.ShowError("Erro", ex.Message);
                return;
            }
            catch (Exception ex)
            {
                this.MessageUC.ShowError("Erro", ex.Message);
                return;
            }
        }


        void Delete()
        {
            BindPageToObjet();

            try
            {
                this.engine = new CoreEngine(Application["ConnectionString"].ToString().TrimEnd(), (User)Session["ActualUser"]);
                this.engine.ProductsMatching.Delete(this.selectedMatching);
                New();
                engine = null;
            }
            catch (MyException ex)
            {
                this.MessageUC.ShowError("Erro", ex.Message);
                return;
            }
            catch (Exception ex)
            {
                this.MessageUC.ShowError("Erro", ex.Message);
                return;
            }
        }


        void Cancel()
        {
            string returnUrlQueryString;
            returnUrlQueryString = string.Format("returnUrl={0}", Server.UrlEncode(Request.AppRelativeCurrentExecutionFilePath));
            Response.Redirect(string.Format("{0}?{1}", Application["ProductsMatchingPage"].ToString().TrimEnd(), returnUrlQueryString), true);
        }


        void New()
        {
            var tbx = StocksSelBox.FindControl("txtStock");
            ((TextBox)tbx).Text = "";
            tbx = SuppliersSelBox.FindControl("txtSupplier");
            ((TextBox)tbx).Text = "";
            tbx = ProductsSelBox.FindControl("txtProduct");
            ((TextBox)tbx).Text = "";
            SetSelectedMatching(ProductMatchingSpecs.New());
            SetSelectedSupplier(null);
            SetSelectedProduct(null);
            SetSelectedStock(null);
            txtCode.Text = "";
            txtSupplement.Text = "";
            cbxInactive.Checked = false;
            lblCreation.Text = DateTime.Now.ToString();
            lblVersion.Text = DateTime.Now.ToString();
            lblMode.Text = "Insert";
            BindObjectToPage();
        }
    }
}