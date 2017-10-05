using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using WhereToBuy.core;
using WhereToBuy.entities;
using WhereToBuy.entities.specs;

namespace WhereToBuy.web.UserControls.Supplements.SupplementMatching
{
    public partial class SupplementMatchingUC
    {
        CoreEngine engine;

        public void UpdateData(string supplierCode, string code, DataState dataSupplement)
        {
            if (txtCode.MaxLength != SupplementMatchingSpecs.Code_MaxSize)
            {
                SetFormEnvironment();
            }

            txtCode.Text = code.TrimEnd();
            if (supplierCode != "" && code != "")
            {
                LoadSupplementMatching(supplierCode, code);
            }

            UpdatePanel1.Update();

        }


        void SetFormEnvironment()
        {

            txtCode.MaxLength = SupplementMatchingSpecs.Code_MaxSize;
            txtDescription.MaxLength = SupplementMatchingSpecs.Description_MaxSize;
            // CONFIGURAR REQUIRED'S
            if (SupplementMatchingSpecs.Code_Necesssary)
            {
                txtCode.Attributes.Add("required", "");
            }

            if (SupplementMatchingSpecs.Description_Necesssary)
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

            UpdatePanel1.Update();
        }


        void BindPageToObjet()
        {
            this.selectedMatching = new WhereToBuy.entities.SupplementMatching();

            this.selectedMatching.Supplier = GetSelectedSupplier();
            this.selectedMatching.Code = txtCode.Text.TrimEnd().ToUpper();
            this.selectedMatching.Description = txtDescription.Text.TrimEnd();

            this.selectedMatching.Inactive = cbxInactive.Checked;
            this.selectedMatching.MapTo = GetSelectedSupplement();

            this.selectedMatching.Version = DateTime.FromBinary(long.Parse(ViewState["Version"].ToString()));
            this.selectedMatching.Creation = DateTime.FromBinary(long.Parse(ViewState["Creation"].ToString()));
            this.selectedMatching.EditionMode = (ViewState["EditionMode"].ToString().TrimEnd().ToLower() == "false") ? false : true;
        }

        void LoadSupplementMatching(string supplierCode, string code)
        {

            try
            {
                this.engine = new CoreEngine(Application["ConnectionString"].ToString().TrimEnd(), (User)Session["ActualUser"]);
                this.selectedMatching = this.engine.SupplementsMatching.Get(supplierCode, code, 1, 1);

                SetSelectedMatching(this.selectedMatching);

                SetSelectedSupplier(this.selectedMatching.Supplier);

                SuppliersSelBox.UpdateData(GetSelectedSupplier(), true);

                if (this.selectedMatching.MapTo != null)
                {
                    SetSelectedSupplement(this.selectedMatching.MapTo);

                    var tbx = SupplementsSelBox.FindControl("txtSupplement");
                    ((TextBox)tbx).Text = this.selectedMatching.MapTo.ToString();
                    //SupplementsSelBox.UpdateData(GetSelectedSupplement(), false);
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
                this.engine.SupplementsMatching.Store(this.selectedMatching);

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
                this.engine.SupplementsMatching.Delete(this.selectedMatching);
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

            Response.Redirect(string.Format("{0}?{1}", Application["SupplementsMatchingPage"].ToString().TrimEnd(), returnUrlQueryString), true);
        }

        void New()
        {
            SetSelectedMatching(SupplementMatchingSpecs.New());

            SetSelectedSupplier(null);
            SetSelectedSupplement(null);

            SuppliersSelBox.UpdateData("", true);
            var tbx = SupplementsSelBox.FindControl("txtSupplement");
            ((TextBox)tbx).Text = "";
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