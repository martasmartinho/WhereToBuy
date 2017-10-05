using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WhereToBuy.core;
using WhereToBuy.entities;
using WhereToBuy.entities.specs;

namespace WhereToBuy.web.UserControls.WarningTypes.WarningType
{
    public partial class WarningTypeUC
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
                LoadWarningType(code);

            }

            UpdatePanel1.Update();

        }


        void SetFormEnvironment()
        {

            txtCode.MaxLength = WarningTypeSpecs.Code_MaxSize;
            txtDescription.MaxLength = WarningTypeSpecs.Description_MaxSize;
            txtNotes.MaxLength = WarningTypeSpecs.Notes_MaxSize;

            //REQUIRED'S config
            if (WarningTypeSpecs.Code_Necesssary)
            {
                txtCode.Attributes.Add("required", "");
            }

            if (WarningTypeSpecs.Description_necesssary)
            {
                txtDescription.Attributes.Add("required", "");
            }
                    
            if (WarningTypeSpecs.Severity_Necesssary)
            {
                txtSeverity.Attributes.Add("required", "");
            }

            New();


        }

        void BindObjectToPage()
        {

            txtCode.Text = this.warningType.Code;
            txtDescription.Text = this.warningType.Description;

            txtNotes.Text = this.warningType.Notes;
            txtSeverity.Text = this.warningType.Severity.ToString();

            cbxInactive.Checked = this.warningType.Inactive;
            lblMode.Text = (this.warningType.EditionMode == false) ? "(Insert)" : "(Update)";// traduzir
            lblCreation.Text = (this.warningType.EditionMode == false) ? "(Automatic)" : this.warningType.Creation.ToString("dddd, dd-MMM-yyyy HH:mm");
            lblVersion.Text = (this.warningType.EditionMode == false) ? "(Automatic)" : this.warningType.Version.ToString("dddd, dd-MMM-yyyy HH:mm");


            ViewState["Version"] = this.warningType.Version.ToBinary().ToString();
            ViewState["Creation"] = this.warningType.Creation.ToBinary().ToString();
            ViewState["EditionMode"] = (this.warningType.EditionMode == false) ? "false" : "true";
            UpdatePanel1.Update();
        }


        void BindPageToObjet()
        {
            this.warningType = new WhereToBuy.entities.WarningType();

            this.warningType.Code = txtCode.Text.TrimEnd().ToUpper();
            this.warningType.Description = txtDescription.Text.TrimEnd();

            this.warningType.Notes = txtNotes.Text.TrimEnd().TrimEnd();
            this.warningType.Severity = short.Parse(txtSeverity.Text.Trim());

            this.warningType.Inactive = cbxInactive.Checked;
            this.warningType.Version = DateTime.FromBinary(long.Parse(ViewState["Version"].ToString()));
            this.warningType.Creation = DateTime.FromBinary(long.Parse(ViewState["Creation"].ToString()));
            this.warningType.EditionMode = (ViewState["EditionMode"].ToString().TrimEnd().ToLower() == "false") ? false : true;

        }

        void LoadWarningType(string code)
        {

            try
            {
                this.engine = new CoreEngine(Application["ConnectionString"].ToString().TrimEnd(), (User)Session["ActualUser"]);
                this.warningType = this.engine.WarningTypes.Get(code);

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
                this.engine.WarningTypes.Store(this.warningType);

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
                this.engine.WarningTypes.Delete(this.warningType);
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

            Response.Redirect(string.Format("{0}?{1}", Application["WarningTypesPage"].ToString().TrimEnd(), returnUrlQueryString), true);
        }

        void New()
        {
            this.warningType = WarningTypeSpecs.New();
            //txtCode.Text = string.Empty;
            //txtDescription.Text = string.Empty;
            //txtWarningTypeDesignation.Text = string.Empty;
            //cbxInactive.Checked = false;
            //lblCreation.Text = DateTime.Now.ToString();
            //lblVersion.Text = DateTime.Now.ToString();
            //lblMode.Text = "Insert";

            BindObjectToPage();

            txtCode.Focus();
        }
    }
}