using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WhereToBuy.core;
using WhereToBuy.entities;
using WhereToBuy.entities.specs;

namespace WhereToBuy.web.UserControls.Suppliers.Supplier
{
    public partial class SupplierUC
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
                LoadSupplier(code);

            }

            UpdatePanel1.Update();

        }


        /// <summary>
        /// 
        /// </summary>
        void SetFormEnvironment()
        {

            txtCode.MaxLength = SupplierSpecs.Code_MaxSize;
            txtName.MaxLength = SupplierSpecs.Name_MaxSize;
            txtAddress.MaxLength = SupplierSpecs.Address_MaxSize;
            txtZipCode.MaxLength = SupplierSpecs.ZipCode_MaxSize;
            txtCity.MaxLength = SupplierSpecs.City_MaxSize;
            txtTaxNumber.MaxLength = SupplierSpecs.IdentificationNumber_MaxSize;
            txtMobile.MaxLength = SupplierSpecs.CellPhone_MaxSize;
            txtSms.MaxLength = SupplierSpecs.Sms_MaxSize;
            txtEmail.MaxLength = SupplierSpecs.Email_MaxSize;
            txtUsername.MaxLength = SupplierSpecs.Username_MaxSize;
            txtPassword.MaxLength = SupplierSpecs.Password_MaxSize;
            
           

            //REQUIRED'S config
            if (SupplierSpecs.Code_Necesssary)
            {
                txtCode.Attributes.Add("required", "");
            }

            if (SupplierSpecs.Name_Necesssary)
            {
                txtName.Attributes.Add("required", "");
            }

            if (SupplierSpecs.Address_Necesssary)
            {
                txtAddress.Attributes.Add("required", "");
            }

            if (SupplierSpecs.ZipCode_Necesssary)
            {
                txtZipCode.Attributes.Add("required", "");
            }

            if (SupplierSpecs.City_Necesssary)
            {
                txtCity.Attributes.Add("required", "");
            }

            if (SupplierSpecs.IdentificationNumber_Necesssary)
            {
                txtTaxNumber.Attributes.Add("required", "");
            }

            if (SupplierSpecs.CellPhone_Necesssary)
            {
                txtMobile.Attributes.Add("required", "");
            }

            if (SupplierSpecs.Sms_Necesssary)
            {
                txtSms.Attributes.Add("required", "");
            }

            if (SupplierSpecs.Email_Necesssary)
            {
                txtEmail.Attributes.Add("required", "");
            }

            if (SupplierSpecs.Username_Necesssary)
            {
                txtUsername.Attributes.Add("required", "");
            }

            if (SupplierSpecs.Password_Necesssary)
            {
                txtPassword.Attributes.Add("required", "");
            }

            New();


        }

        /// <summary>
        /// 
        /// </summary>
        void BindObjectToPage()
        {

            txtCode.Text = this.supplier.Code;
            txtName.Text = this.supplier.Name;
            txtAddress.Text = this.supplier.Address;
            txtZipCode.Text = this.supplier.ZipCode;
            txtCity.Text = this.supplier.City;
            txtTaxNumber.Text = this.supplier.IdentificationNumber;
            
            txtSalesmanName.Text = this.supplier.Salesman;
            txtEmail.Text = this.supplier.Email;
            txtPhone.Text = this.supplier.Phone;
            txtMobile.Text = this.supplier.Cellphone;
            txtSms.Text = this.supplier.SMS;
            txtUsername.Text = this.supplier.Username;
            txtPassword.Text = this.supplier.Password;
            txtPassword.Attributes.Add("value", this.supplier.Password);
            cbxActiveAccess.Checked = this.supplier.ActiveOnlineAccess;


            txtExpirationHours.Text = this.supplier.SuggestionExpirationHours.ToString();
            txtProductDescriptionScore.Text = this.supplier.InicialScoreDescription.ToString();
            txtProductFeaturesScore.Text = this.supplier.InicialScoreFeatures.ToString();
            txtProductImageScore.Text = this.supplier.InicialScoreImage.ToString();
            txtProductLinkScore.Text = this.supplier.InicialScoreLink.ToString();
            txtProductPriceTrust.Text = this.supplier.ProductPriceTrust.ToString("0.00").Replace(',','.');
            txtProductAvailableTrust.Text = this.supplier.ProductAvailableTrust.ToString("0.00").Replace(',', '.');
            //txtTrustIndex.Text = this.supplier.TrustIndex.ToString("0.00").Replace(',', '.');
            cbxAutomaticMatching.Checked = this.supplier.AutomaticProductMatching;
            cbxAutomaticProducts.Checked = this.supplier.ActomaticProductCreation;
            cbxShowProductDetail.Checked = this.supplier.InfoProductDetailAvailable;
            cbxAutomaticProductUpdate.Checked = this.supplier.InactiveAutomaticUpdateSuggestion;
            cbxShowDescription.Checked = this.supplier.InactiveDescriptionSuggestion;
            cbxShowFeatures.Checked = this.supplier.InactiveFeatureSuggestion;
            cbxShowLink.Checked = this.supplier.InactiveLinkSuggestion;
            cbxShowImage.Checked = this.supplier.InactiveImageSuggestion;
           
             
            

            cbxInactive.Checked = this.supplier.Inactive;
            lblMode.Text = (this.supplier.EditionMode == false) ? "(Insert)" : "(Update)";// traduzir
            lblCreation.Text = (this.supplier.EditionMode == false) ? "(Automatic)" : this.supplier.Creation.ToString("dddd, dd-MMM-yyyy HH:mm");
            lblVersion.Text = (this.supplier.EditionMode == false) ? "(Automatic)" : this.supplier.Version.ToString("dddd, dd-MMM-yyyy HH:mm");


            ViewState["Version"] = this.supplier.Version.ToBinary().ToString();
            ViewState["Creation"] = this.supplier.Creation.ToBinary().ToString();
            ViewState["EditionMode"] = (this.supplier.EditionMode == false) ? "false" : "true";
            UpdatePanel1.Update();
        }

        /// <summary>
        /// 
        /// </summary>
        void BindPageToObjet()
        {
            this.supplier = new WhereToBuy.entities.Supplier();

            this.supplier.Code = txtCode.Text.TrimEnd().ToUpper();
            this.supplier.Name = txtName.Text.TrimEnd();
            this.supplier.Address = txtAddress.Text;
            this.supplier.ZipCode = txtZipCode.Text;
            this.supplier.City = txtCity.Text;
            this.supplier.IdentificationNumber = txtTaxNumber.Text.Trim();
            
            this.supplier.Salesman= txtSalesmanName.Text;
            this.supplier.Email = txtEmail.Text;
            this.supplier.Phone = txtPhone.Text;
            this.supplier.Cellphone = txtMobile.Text;
            this.supplier.SMS = txtSms.Text;
            this.supplier.Username = txtUsername.Text;
            this.supplier.Password = txtPassword.Text;

            this.supplier.ActiveOnlineAccess = cbxActiveAccess.Checked;


            this.supplier.SuggestionExpirationHours = short.Parse(txtExpirationHours.Text);
            this.supplier.InicialScoreDescription = short.Parse(txtProductDescriptionScore.Text);
            this.supplier.InicialScoreFeatures = short.Parse(txtProductFeaturesScore.Text);
            this.supplier.InicialScoreImage = short.Parse(txtProductImageScore.Text);
            this.supplier.InicialScoreLink = short.Parse(txtProductLinkScore.Text);
            this.supplier.ProductPriceTrust = double.Parse(txtProductPriceTrust.Text.TrimEnd().Replace('.', ','));
            this.supplier.ProductAvailableTrust = double.Parse(txtProductAvailableTrust.Text.TrimEnd().Replace('.', ','));
            //this.supplier.TrustIndex = double.Parse(txtTrustIndex.Text.TrimEnd().Replace('.', ','));
            this.supplier.AutomaticProductMatching = cbxAutomaticMatching.Checked;
            this.supplier.ActomaticProductCreation = cbxAutomaticProducts.Checked;
            this.supplier.InfoProductDetailAvailable = cbxShowProductDetail.Checked;
            this.supplier.InactiveAutomaticUpdateSuggestion = cbxAutomaticProductUpdate.Checked;
            this.supplier.InactiveDescriptionSuggestion = cbxShowDescription.Checked;
            this.supplier.InactiveFeatureSuggestion = cbxShowFeatures.Checked;
            this.supplier.InactiveLinkSuggestion = cbxShowLink.Checked;
            this.supplier.InactiveImageSuggestion = cbxShowImage.Checked;
           

            this.supplier.Inactive = cbxInactive.Checked;
            this.supplier.Version = DateTime.FromBinary(long.Parse(ViewState["Version"].ToString()));
            this.supplier.Creation = DateTime.FromBinary(long.Parse(ViewState["Creation"].ToString()));
            this.supplier.EditionMode = (ViewState["EditionMode"].ToString().TrimEnd().ToLower() == "false") ? false : true;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        void LoadSupplier(string code)
        {

            try
            {
                this.engine = new CoreEngine(Application["ConnectionString"].ToString().TrimEnd(), (User)Session["ActualUser"]);
                this.supplier = this.engine.Suppliers.Get(code);

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
                this.engine.Suppliers.Store(this.supplier);

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
                this.engine.Suppliers.Delete(this.supplier);
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

            Response.Redirect(string.Format("{0}?{1}", Application["SuppliersPage"].ToString().TrimEnd(), returnUrlQueryString), true);
        }

        void New()
        {
            this.supplier = SupplierSpecs.New();
            txtUsername.Text = "";
            txtPassword.Text = "";

            //txtCode.Text = string.Empty;
            //txtName.Text = string.Empty;
            //txtSupplierDesignation.Text = string.Empty;
            //cbxInactive.Checked = false;
            //lblCreation.Text = DateTime.Now.ToString();
            //lblVersion.Text = DateTime.Now.ToString();
            //lblMode.Text = "Insert";

            BindObjectToPage();

            txtCode.Focus();
        }
    }
}