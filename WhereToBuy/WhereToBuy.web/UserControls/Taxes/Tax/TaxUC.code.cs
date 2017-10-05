using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WhereToBuy.core;
using WhereToBuy.entities;
using WhereToBuy.entities.specs;
using WhereToBuy.utils.GlobalVariables;

namespace WhereToBuy.web.UserControls.Taxes.Tax
{
    public partial class TaxUC
    {
        CoreEngine engine;

        public void UpdateData(string code, DataState dataState)
        {

            if (txtCode.MaxLength == 0)
            {
                SetFormEnvironment();
            }

            txtCode.Text = code.TrimEnd();

            if (code != "")
            {
                LoadTax(code);

            }

            UpdatePanel1.Update();

        }


        void SetFormEnvironment()
        {

            txtCode.MaxLength = TaxSpecs.Code_MaxSize;
            txtDescription.MaxLength = TaxSpecs.Description_MaxSize;
            txtTaxDesignation.MaxLength = TaxSpecs.TaxDesignation_MaxSize;

            //REQUIRED'S config
            if (TaxSpecs.Code_Necesssary)
            {
                txtCode.Attributes.Add("required", "");
            }

            if (TaxSpecs.Description_Necesssary)
            {
                txtDescription.Attributes.Add("required", "");
            }

            if (TaxSpecs.TaxDesignation_Necesssary)
            {
                txtTaxDesignation.Attributes.Add("required", "");
            }

            if (TaxSpecs.TaxValue_Necesssary)
            {
                txtTaxRate.Attributes.Add("required", "");
            }

            New();


        }

        void BindObjectToPage()
        {

            txtCode.Text = this.tax.Code;
            txtDescription.Text = this.tax.Description;

            txtTaxDesignation.Text = this.tax.TaxDesignation;
            txtTaxRate.Text = this.tax.TaxValue.ToString("0.00").Replace(',','.');

            cbxInactive.Checked = this.tax.Inactive;
            lblMode.Text = (this.tax.EditionMode == false) ? GlobalVariables.Resource.GetString("InsertString", GlobalVariables.Culture) : GlobalVariables.Resource.GetString("UpdateString", GlobalVariables.Culture);// traduzir
            lblCreation.Text = (this.tax.EditionMode == false) ? GlobalVariables.Resource.GetString("AutomaticString", GlobalVariables.Culture) : this.tax.Creation.ToString("dddd, dd-MMM-yyyy HH:mm");
            lblVersion.Text = (this.tax.EditionMode == false) ? GlobalVariables.Resource.GetString("AutomaticString", GlobalVariables.Culture) : this.tax.Version.ToString("dddd, dd-MMM-yyyy HH:mm");


            ViewState["Version"] = this.tax.Version.ToBinary().ToString();
            ViewState["Creation"] = this.tax.Creation.ToBinary().ToString();
            ViewState["EditionMode"] = (this.tax.EditionMode == false) ? "false" : "true";
            UpdatePanel1.Update();
        }


        void BindPageToObjet()
        {
            this.tax = new WhereToBuy.entities.Tax();

            this.tax.Code = txtCode.Text.TrimEnd().ToUpper();
            this.tax.Description = txtDescription.Text.TrimEnd();

            this.tax.TaxDesignation = txtTaxDesignation.Text.TrimEnd().TrimEnd();
            this.tax.TaxValue = double.Parse(txtTaxRate.Text.TrimEnd().Replace('.',','));

            this.tax.Inactive = cbxInactive.Checked;
            this.tax.Version = DateTime.FromBinary(long.Parse(ViewState["Version"].ToString()));
            this.tax.Creation = DateTime.FromBinary(long.Parse(ViewState["Creation"].ToString()));
            this.tax.EditionMode = (ViewState["EditionMode"].ToString().TrimEnd().ToLower() == "false") ? false : true;

        }

        void LoadTax(string code)
        {

            try
            {
                this.engine = new CoreEngine(Application["ConnectionString"].ToString().TrimEnd(), (User)Session["ActualUser"]);
                this.tax = this.engine.Taxes.Get(code);

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
                this.engine.Taxes.Store(this.tax);

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
                this.engine.Taxes.Delete(this.tax);
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

            //if (Request.QueryString.Count > 0)
            //{
            //    string c = Request.QueryString.ToString();
            //    returnUrlQueryString += Server.UrlEncode(string.Format("?{0}", Request.QueryString.ToString()));
            //}

            Response.Redirect(string.Format("{0}?{1}", Application["TaxesPage"].ToString().TrimEnd(), returnUrlQueryString), true);
        }

        void New()
        {
            this.tax = TaxSpecs.New();
            //txtCode.Text = string.Empty;
            //txtDescription.Text = string.Empty;
            //txtTaxDesignation.Text = string.Empty;
            //cbxInactive.Checked = false;
            //lblCreation.Text = DateTime.Now.ToString();
            //lblVersion.Text = DateTime.Now.ToString();
            //lblMode.Text = "Insert";

            BindObjectToPage();

            txtCode.Focus();
        }
    }
}