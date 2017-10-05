using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WhereToBuy.core;
using WhereToBuy.entities;
using WhereToBuy.entities.specs;

namespace WhereToBuy.web.UserControls.States.State
{
    public partial class StateUC
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
                LoadState(code);

            }

            UpdatePanel1.Update();

        }


        void SetFormEnvironment()
        {

            txtCode.MaxLength = StateSpecs.Code_MaxSize;
            txtDescription.MaxLength = StateSpecs.Description_MaxSize;
            // CONFIGURAR REQUIRED'S
            if (StateSpecs.Code_Necesssary)
            {
                txtCode.Attributes.Add("required", "");
            }

            if (StateSpecs.Description_Necesssary)
            {
                txtDescription.Attributes.Add("required", "");
            }

            New();


        }

        void BindObjectToPage()
        {

            txtCode.Text = this.state.Code;
            txtDescription.Text = this.state.Description;

            cbxInactive.Checked = this.state.Inactive;
            lblMode.Text = (this.state.EditionMode == false) ? "(Insert)" : "(Update)";// traduzir
            lblCreation.Text = (this.state.EditionMode == false) ? "(Automatic)" : this.state.Creation.ToString("dddd, dd-MMM-yyyy HH:mm");
            lblVersion.Text = (this.state.EditionMode == false) ? "(Automatic)" : this.state.Version.ToString("dddd, dd-MMM-yyyy HH:mm");


            ViewState["Version"] = this.state.Version.ToBinary().ToString();
            ViewState["Creation"] = this.state.Creation.ToBinary().ToString();
            ViewState["EditionMode"] = (this.state.EditionMode == false) ? "false" : "true";
            UpdatePanel1.Update();
        }


        void BindPageToObjet()
        {
            this.state = new WhereToBuy.entities.State();

            this.state.Code = txtCode.Text.TrimEnd().ToUpper();
            this.state.Description = txtDescription.Text.TrimEnd();
            this.state.Inactive = cbxInactive.Checked;
            this.state.Version = DateTime.FromBinary(long.Parse(ViewState["Version"].ToString()));
            this.state.Creation = DateTime.FromBinary(long.Parse(ViewState["Creation"].ToString()));
            this.state.EditionMode = (ViewState["EditionMode"].ToString().TrimEnd().ToLower() == "false") ? false : true;
           
        }

        void LoadState(string code)
        {

            try
            {
                this.engine = new CoreEngine(Application["ConnectionString"].ToString().TrimEnd(), (User)Session["ActualUser"]);
                this.state = this.engine.States.Get(code);

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
                this.engine.States.Store(this.state);

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
                this.engine.States.Delete(this.state);
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

            Response.Redirect(string.Format("{0}?{1}", Application["StatesPage"].ToString().TrimEnd(), returnUrlQueryString), true);
        }

        void New()
        {
            this.state = StateSpecs.New();
            txtCode.Text = "";
            txtDescription.Text = "";
            cbxInactive.Checked = false;
            lblCreation.Text = DateTime.Now.ToString();
            lblVersion.Text = DateTime.Now.ToString();
            lblMode.Text = "Insert";

            BindObjectToPage();

            txtCode.Focus();
        }
    }
}