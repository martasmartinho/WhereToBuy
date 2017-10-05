using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using WhereToBuy.core;
using WhereToBuy.entities;
using WhereToBuy.entities.specs;

namespace WhereToBuy.web.UserControls.States.StateMatching
{
    public partial class StateMatchingUC
    {
        CoreEngine engine;

        public void UpdateData(string supplierCode, string code, DataState dataState)
        {
            TextBox textBox = (TextBox)StatesSelBox.FindControl("txtState");



            if (txtCode.MaxLength == 0)
            {
                SetFormEnvironment();
            }

            txtCode.Text = code.TrimEnd();
            if (supplierCode != "" && code != "")
            {
                LoadStateMatching(supplierCode, code);
                textBox.Focus();
            }

            UpdatePanel1.Update();

        }


        void SetFormEnvironment()
        {

            txtCode.MaxLength = StateMatchingSpecs.Code_MaxSize;
            txtDescription.MaxLength = StateMatchingSpecs.Description_MaxSize;
            // CONFIGURAR REQUIRED'S
            if (StateMatchingSpecs.Code_Necesssary)
            {
                txtCode.Attributes.Add("required", "");
            }

            if (StateMatchingSpecs.Description_Necesssary)
            {
                txtDescription.Attributes.Add("required", "");
            }


            New();


        }

        void BindObjectToPage()
        {
            txtCode.Text = this.selectedMatching.Code;
            txtDescription.Text = this.selectedMatching.Description;

            cbxInactive.Checked = this.selectedMatching.Inactive;
            lblMode.Text = (this.selectedMatching.EditionMode == false) ? "(Insert)" : "(Update)";// traduzir
            lblCreation.Text = (this.selectedMatching.EditionMode == false) ? "(Automatic)" : this.selectedMatching.Creation.ToString("dddd, dd-MMM-yyyy HH:mm");
            lblCreation.Text = (this.selectedMatching.EditionMode == false) ? "(Automatic)" : this.selectedMatching.Version.ToString("dddd, dd-MMM-yyyy HH:mm");


            ViewState["Version"] = this.selectedMatching.Version.ToBinary().ToString();
            ViewState["Creation"] = this.selectedMatching.Creation.ToBinary().ToString();
            ViewState["EditionMode"] = (this.selectedMatching.EditionMode == false) ? "false" : "true";
        }


        void BindPageToObjet()
        {
            this.selectedMatching = new WhereToBuy.entities.StateMatching();

            this.selectedMatching.Supplier = GetSelectedSupplier();
            this.selectedMatching.Code = txtCode.Text.TrimEnd().ToUpper();
            this.selectedMatching.Description = txtDescription.Text.TrimEnd();

            this.selectedMatching.Inactive = cbxInactive.Checked;
            this.selectedMatching.MapTo = GetSelectedState();

            this.selectedMatching.Version = DateTime.FromBinary(long.Parse(ViewState["Version"].ToString()));
            this.selectedMatching.Creation = DateTime.FromBinary(long.Parse(ViewState["Creation"].ToString()));
            this.selectedMatching.EditionMode = (ViewState["EditionMode"].ToString().TrimEnd().ToLower() == "false") ? false : true;
        }

        void LoadStateMatching(string supplierCode, string code)
        {

            try
            {
                this.engine = new CoreEngine(Application["ConnectionString"].ToString().TrimEnd(), (User)Session["ActualUser"]);
                this.selectedMatching = this.engine.StatesMatching.Get(supplierCode, code, 1, 1);

                SetSelectedMatching(this.selectedMatching);

                SetSelectedSupplier(this.selectedMatching.Supplier);
                SuppliersSelBox.UpdateData(GetSelectedSupplier(), true);

                if (this.selectedMatching.MapTo != null)
                {
                    SetSelectedState(this.selectedMatching.MapTo);
                    StatesSelBox.UpdateData(GetSelectedState(), false);
                }

                BindObjectToPage();
                UpdatePanel1.Update();
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
                this.engine.StatesMatching.Store(this.selectedMatching);

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
                this.engine.StatesMatching.Delete(this.selectedMatching);
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

            Response.Redirect(string.Format("{0}?{1}", Application["StatesMatchingPage"].ToString().TrimEnd(), returnUrlQueryString), true);
        }

        void New()
        {
            SetSelectedMatching(StateMatchingSpecs.New());

            SetSelectedSupplier(null);
            SetSelectedState(null);

            SuppliersSelBox.UpdateData("", true);
            StatesSelBox.UpdateData("", false);
            txtCode.Text = "";
            txtDescription.Text = "";
            cbxInactive.Checked = false;
            lblCreation.Text = DateTime.Now.ToString();
            lblVersion.Text = DateTime.Now.ToString();
            lblMode.Text = "Insert";

            BindObjectToPage();
        }
    }
}