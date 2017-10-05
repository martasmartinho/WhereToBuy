using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WhereToBuy.core;
using WhereToBuy.entities;
using WhereToBuy.entities.specs;
using WhereToBuy.utils.GlobalVariables;

namespace WhereToBuy.web.UserControls.Brands.Brand
{
    public partial class BrandUC
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
                LoadBrand(code);

            }

            UpdatePanel1.Update();

        }


        void SetFormEnvironment()
        {
            txtCode.MaxLength = BrandSpecs.Code_MaxSize;
            txtDescription.MaxLength = BrandSpecs.Description_MaxSize;

            // CONFIGURAR REQUIRED'S
            if (BrandSpecs.Code_Necesssary)
            {
                txtCode.Attributes.Add("required", "");
            }

            if (BrandSpecs.Description_Necesssary)
            {
                txtDescription.Attributes.Add("required", "");
            }

            New();
        }


        void BindObjectToPage()
        {
            txtCode.Text = this.brand.Code;
            txtDescription.Text = this.brand.Description;

            cbxInactive.Checked = this.brand.Inactive;
            lblMode.Text = (this.brand.EditionMode == false) ? GlobalVariables.Resource.GetString("InsertString", GlobalVariables.Culture) : GlobalVariables.Resource.GetString("UpdateString", GlobalVariables.Culture);// traduzir
            lblCreation.Text = (this.brand.EditionMode == false) ? GlobalVariables.Resource.GetString("AutomaticString", GlobalVariables.Culture) : this.brand.Creation.ToString("dddd, dd-MMM-yyyy HH:mm");
            lblVersion.Text = (this.brand.EditionMode == false) ? GlobalVariables.Resource.GetString("AutomaticString", GlobalVariables.Culture) : this.brand.Version.ToString("dddd, dd-MMM-yyyy HH:mm");


            ViewState["Version"] = this.brand.Version.ToBinary().ToString();
            ViewState["Creation"] = this.brand.Creation.ToBinary().ToString();
            ViewState["EditionMode"] = (this.brand.EditionMode == false) ? "false" : "true";
        }


        void BindPageToObjet()
        {
            this.brand = new WhereToBuy.entities.Brand();
            this.brand.Code = txtCode.Text.TrimEnd().ToUpper();
            this.brand.Description = txtDescription.Text.TrimEnd();
            this.brand.Inactive = cbxInactive.Checked;
            this.brand.Version = DateTime.FromBinary(long.Parse(ViewState["Version"].ToString()));
            this.brand.Creation = DateTime.FromBinary(long.Parse(ViewState["Creation"].ToString()));
            this.brand.EditionMode = (ViewState["EditionMode"].ToString().TrimEnd().ToLower() == "false") ? false : true;
        }


        void LoadBrand(string code)
        {
            try
            {
                this.engine = new CoreEngine(Application["ConnectionString"].ToString().TrimEnd(), (User)Session["ActualUser"]);
                this.brand = this.engine.Brands.Get(code);
                this.brand.EditionMode = true;
                
                BindObjectToPage();
                txtCode.Enabled = !this.brand.EditionMode;
                lnkCodeSearch.Enabled = false;
                txtDescription.Focus();

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
                this.engine.Brands.Store(this.brand);
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
                this.engine.Brands.Delete(this.brand);
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
            Response.Redirect(string.Format("{0}?{1}", Application["BrandsPage"].ToString().TrimEnd(), returnUrlQueryString), true);
        }


        void New()
        {
            this.brand = BrandSpecs.New();
            txtCode.Enabled = !this.brand.EditionMode;
            txtCode.Text = "";
            txtDescription.Text = "";
            cbxInactive.Checked = false;
            lblCreation.Text = DateTime.Now.ToString();
            lblVersion.Text = DateTime.Now.ToString();
            lblMode.Text = "Insert";
            lnkCodeSearch.Enabled = true;

            BindObjectToPage();
            txtCode.Focus();
        }
    }
}