using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WhereToBuy.core;
using WhereToBuy.entities;
using WhereToBuy.entities.specs;

namespace WhereToBuy.web.UserControls.Supplements.Supplement
{
    public partial class SupplementUC
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
                LoadSupplement(code);

            }

            UpdatePanel1.Update();

        }


        void SetFormEnvironment()
        {

            txtCode.MaxLength = SupplementSpecs.Code_MaxSize;
            txtDescription.MaxLength = SupplementSpecs.Description_MaxSize;
            txtAddText.MaxLength = SupplementSpecs.TextToAdd_MaxSize;
            txtRemoveText.MaxLength = SupplementSpecs.TextToRemove_MaxSize;

            //REQUIRED'S config
            if (SupplementSpecs.Code_Necesssary)
            {
                txtCode.Attributes.Add("required", "");
            }

            if (SupplementSpecs.Description_Necesssary)
            {
                txtDescription.Attributes.Add("required", "");
            }

           
            New();


        }

        void BindObjectToPage()
        {

            txtCode.Text = this.supplement.Code;
            txtDescription.Text = this.supplement.Description;

            txtAddText.Text = this.supplement.TextToAdd ;
            txtRemoveText.Text = this.supplement.TextToRemove;

            cbxInactive.Checked = this.supplement.Inactive;
            lblMode.Text = (this.supplement.EditionMode == false) ? "(Insert)" : "(Update)";// traduzir
            lblCreation.Text = (this.supplement.EditionMode == false) ? "(Automatic)" : this.supplement.Creation.ToString("dddd, dd-MMM-yyyy HH:mm");
            lblVersion.Text = (this.supplement.EditionMode == false) ? "(Automatic)" : this.supplement.Version.ToString("dddd, dd-MMM-yyyy HH:mm");


            ViewState["Version"] = this.supplement.Version.ToBinary().ToString();
            ViewState["Creation"] = this.supplement.Creation.ToBinary().ToString();
            ViewState["EditionMode"] = (this.supplement.EditionMode == false) ? "false" : "true";
            UpdatePanel1.Update();
        }


        void BindPageToObjet()
        {
            this.supplement = new WhereToBuy.entities.Supplement();

            this.supplement.Code = txtCode.Text.TrimEnd().ToUpper();
            this.supplement.Description = txtDescription.Text.TrimEnd();

            this.supplement.TextToAdd = txtAddText.Text.TrimStart().TrimEnd();
            this.supplement.TextToRemove = txtRemoveText.Text.TrimStart().TrimEnd();

            this.supplement.Inactive = cbxInactive.Checked;
            this.supplement.Version = DateTime.FromBinary(long.Parse(ViewState["Version"].ToString()));
            this.supplement.Creation = DateTime.FromBinary(long.Parse(ViewState["Creation"].ToString()));
            this.supplement.EditionMode = (ViewState["EditionMode"].ToString().TrimEnd().ToLower() == "false") ? false : true;

        }

        void LoadSupplement(string code)
        {

            try
            {
                this.engine = new CoreEngine(Application["ConnectionString"].ToString().TrimEnd(), (User)Session["ActualUser"]);
                this.supplement = this.engine.Supplements.Get(code);

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
                this.engine.Supplements.Store(this.supplement);

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
                this.engine.Supplements.Delete(this.supplement);
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

            Response.Redirect(string.Format("{0}?{1}", Application["SupplementsPage"].ToString().TrimEnd(), returnUrlQueryString), true);
        }

        void New()
        {
            this.supplement = SupplementSpecs.New();
            //txtCode.Text = string.Empty;
            //txtDescription.Text = string.Empty;
            //txtSupplementDesignation.Text = string.Empty;
            //cbxInactive.Checked = false;
            //lblCreation.Text = DateTime.Now.ToString();
            //lblVersion.Text = DateTime.Now.ToString();
            //lblMode.Text = "Insert";

            BindObjectToPage();

            txtCode.Focus();
        }
    }
}