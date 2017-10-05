using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WhereToBuy.core;
using WhereToBuy.entities;
using WhereToBuy.entities.specs;

namespace WhereToBuy.web.UserControls.WorryingTerms.WorryingTerm
{
    public partial class WorryingTermUC
    {
        CoreEngine engine;

        public void UpdateData(string term, DataState dataState)
        {
            
            if (txtTerm.MaxLength == 0)
            {
                SetFormEnvironment();
            }

            txtTerm.Text = term.TrimEnd();

            if (term != "")
            {
                LoadWorryingTerm(term);

            }

            UpdatePanel1.Update();

        }


        void SetFormEnvironment()
        {

            txtTerm.MaxLength = WorryingTermSpecs.Term_MaxSize;
            txtNotes.MaxLength = WorryingTermSpecs.Notes_MaxSize;

            //REQUIRED'S config
            if (WorryingTermSpecs.Term_Necesssary)
            {
                txtTerm.Attributes.Add("required", "");
            }


            if (WorryingTermSpecs.Index_Necesssary)
            {
                txtIndex.Attributes.Add("required", "");
            }

            New();


        }

        void BindObjectToPage()
        {

            txtTerm.Text = this.worryingTerm.Term;
            txtNotes.Text = this.worryingTerm.Notes;

            txtIndex.Text = this.worryingTerm.Index.ToString();

            cbxInactive.Checked = this.worryingTerm.Inactive;
            lblMode.Text = (this.worryingTerm.EditionMode == false) ? "(Insert)" : "(Update)";// traduzir
            lblCreation.Text = (this.worryingTerm.EditionMode == false) ? "(Automatic)" : this.worryingTerm.Creation.ToString("dddd, dd-MMM-yyyy HH:mm");
            lblVersion.Text = (this.worryingTerm.EditionMode == false) ? "(Automatic)" : this.worryingTerm.Version.ToString("dddd, dd-MMM-yyyy HH:mm");


            ViewState["Version"] = this.worryingTerm.Version.ToBinary().ToString();
            ViewState["Creation"] = this.worryingTerm.Creation.ToBinary().ToString();
            ViewState["EditionMode"] = (this.worryingTerm.EditionMode == false) ? "false" : "true";
            UpdatePanel1.Update();
        }


        void BindPageToObjet()
        {
            this.worryingTerm = new WhereToBuy.entities.WorryingTerm();

            this.worryingTerm.Term = txtTerm.Text.TrimEnd();
            this.worryingTerm.Notes = txtNotes.Text.TrimEnd();

            this.worryingTerm.Index = short.Parse(txtIndex.Text.Trim());

            this.worryingTerm.Inactive = cbxInactive.Checked;
            this.worryingTerm.Version = DateTime.FromBinary(long.Parse(ViewState["Version"].ToString()));
            this.worryingTerm.Creation = DateTime.FromBinary(long.Parse(ViewState["Creation"].ToString()));
            this.worryingTerm.EditionMode = (ViewState["EditionMode"].ToString().TrimEnd().ToLower() == "false") ? false : true;

        }

        void LoadWorryingTerm(string code)
        {

            try
            {
                this.engine = new CoreEngine(Application["ConnectionString"].ToString().TrimEnd(), (User)Session["ActualUser"]);
                this.worryingTerm = this.engine.WorryingTerms.Get(code);

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
                this.engine.WorryingTerms.Store(this.worryingTerm);

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
                this.engine.WorryingTerms.Delete(this.worryingTerm);
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

            Response.Redirect(string.Format("{0}?{1}", Application["WorryingTermsPage"].ToString().TrimEnd(), returnUrlQueryString), true);
        }

        void New()
        {
            this.worryingTerm = WorryingTermSpecs.New();
           
            BindObjectToPage();

            txtTerm.Focus();
        }
    }
}