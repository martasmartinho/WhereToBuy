using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using WhereToBuy.core;
using WhereToBuy.entities;
using WhereToBuy.entities.specs;
using WhereToBuy.utils.GlobalVariables;

namespace WhereToBuy.web.UserControls.QuotationRules.QuotationRule
{
    public partial class QuotationRuleUC
    {
        CoreEngine engine;

        public void UpdateData(string supplier, string brand, string category, string stock)
        {

            SetFormEnvironment();

            

            if (supplier != "")
            {
                LoadQuotationRule(supplier, brand, category, stock);

            }

            UpdatePanel1.Update();

        }


        void SetFormEnvironment()
        {
            var tbx = SuppliersSelBox.FindControl("txtSupplier");
            ((TextBox)tbx).Attributes.Add("required", "");
            
            tbx = BrandsSelBox.FindControl("txtBrand");
            ((TextBox)tbx).Attributes.Add("required", "");

            tbx = CategoriesSelBox.FindControl("txtCategory");
            ((TextBox)tbx).Attributes.Add("required", "");

            tbx = StocksSelBox.FindControl("txtStock");
            ((TextBox)tbx).Attributes.Add("required", "");


            SetListBoxEnvironment();


            New();
        }

        private void SetListBoxEnvironment()
        {

            txtDataReset.Text = string.Empty;
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


        void BindObjectToPage()
        {
            

            txtExpiration.Text = this.quotationRule.ExpitationHours.ToString();
            txtNotes.Text = this.quotationRule.Notes;
            txtDataReset.Text = "";
            if (this.quotationRule.DataReset != null)
            {
                txtDataReset.Text = ((DateTime)this.quotationRule.DataReset).ToString("dd-MM-yyyy");
            }



            lblMode.Text = (this.quotationRule.EditionMode == false) ? GlobalVariables.Resource.GetString("InsertString", GlobalVariables.Culture) : GlobalVariables.Resource.GetString("UpdateString", GlobalVariables.Culture);// traduzir
            lblVersion.Text = (this.quotationRule.EditionMode == false) ? GlobalVariables.Resource.GetString("AutomaticString", GlobalVariables.Culture) : this.quotationRule.Version.ToString("dddd, dd-MMM-yyyy HH:mm");

            ViewState["Version"] = this.quotationRule.Version.ToBinary().ToString();
            ViewState["EditionMode"] = (this.quotationRule.EditionMode == false) ? "false" : "true";
            UpdatePanel1.Update();
        }


        void BindPageToObjet()
        {
            this.quotationRule= new WhereToBuy.entities.QuotationRule();

            this.quotationRule.ExpitationHours = short.Parse(txtExpiration.Text);
            this.quotationRule.Notes = txtNotes.Text.TrimEnd().TrimStart();
            this.quotationRule.DataReset = txtDataReset.Text == "" ? this.quotationRule.DataReset : DateTime.Parse(txtDataReset.Text);

            this.quotationRule.Supplier = (entities.Supplier)ViewState["SelectedSupplier"];
            this.quotationRule.Brand = (entities.Brand)ViewState["SelectedBrand"];
            this.quotationRule.Category = (entities.Category)ViewState["SelectedCategory"];
            this.quotationRule.Stock = (entities.Stock)ViewState["SelectedStock"];
            this.quotationRule.SubstituteStock = (entities.Stock)ViewState["SelectedSubstituteStock"];

            this.quotationRule.Version = DateTime.FromBinary(long.Parse(ViewState["Version"].ToString()));
            this.quotationRule.EditionMode = (ViewState["EditionMode"].ToString().TrimEnd().ToLower() == "false") ? false : true;

        }



        void LoadQuotationRule(string supplier, string brand, string category, string stock)
        {
            
            try
            {
                this.engine = new CoreEngine(Application["ConnectionString"].ToString().TrimEnd(), (User)Session["ActualUser"]);
                this.quotationRule = this.engine.QuotationRules.Get(supplier, brand, category, stock, 1);

                // set viewstates
                SetSupplier(quotationRule.Supplier);
                SetBrand(quotationRule.Brand);
                SetCategory(quotationRule.Category); 
                SetStock(quotationRule.Stock);

                var tbx = SuppliersSelBox.FindControl("txtSupplier"); ;
                ((TextBox)tbx).Text = this.quotationRule.Supplier.ToString();

                tbx = BrandsSelBox.FindControl("txtBrand");
                ((TextBox)tbx).Text = this.quotationRule.Brand.ToString();

                tbx = CategoriesSelBox.FindControl("txtCategory");
                ((TextBox)tbx).Text = this.quotationRule.Category.ToString();

                tbx = StocksSelBox.FindControl("txtStock");
                ((TextBox)tbx).Text = this.quotationRule.Stock.ToString();

                

                if (this.quotationRule.SubstituteStock != null)
                {
                    SetSubstituteStock(quotationRule.SubstituteStock);
                    tbx = StocksSelBox1.FindControl("txtStock");
                    ((TextBox)tbx).Text = this.quotationRule.SubstituteStock.ToString();
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
                this.engine.QuotationRules.Store(this.quotationRule);

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
                this.engine.QuotationRules.Delete(this.quotationRule);
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

           
            Response.Redirect(string.Format("{0}?{1}", Application["QuotationRulesPage"].ToString().TrimEnd(), returnUrlQueryString), true);
        }



        void New()
        {
            this.quotationRule= QuotationRuleSpecs.New();
            SetSelectedQuotationRule(this.quotationRule);
            SetSupplier(this.quotationRule.Supplier);
            SetBrand(this.quotationRule.Brand);
            SetCategory(this.quotationRule.Category);
            SetStock(this.quotationRule.Stock);
            SetSubstituteStock(this.quotationRule.SubstituteStock);

            var tbx = SuppliersSelBox.FindControl("txtSupplier");
            ((TextBox)tbx).Text = "";
            ((TextBox)tbx).Focus();

            tbx = BrandsSelBox.FindControl("txtBrand");
            ((TextBox)tbx).Text = "";

            tbx = CategoriesSelBox.FindControl("txtCategory");
            ((TextBox)tbx).Text = "";

            tbx = StocksSelBox.FindControl("txtStock");
            ((TextBox)tbx).Text = "";

            tbx = StocksSelBox1.FindControl("txtStock");
            ((TextBox)tbx).Text = "";

            BindObjectToPage();

        }

    }
}