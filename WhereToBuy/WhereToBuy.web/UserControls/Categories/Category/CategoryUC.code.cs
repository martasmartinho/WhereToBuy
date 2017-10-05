using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WhereToBuy.core;
using WhereToBuy.entities;
using WhereToBuy.entities.specs;
using WhereToBuy.utils.GlobalVariables;

namespace WhereToBuy.web.UserControls.Categories.Category
{
    public partial class CategoryUC
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
                LoadCategory(code);

            }

            UpdatePanel1.Update();

        }


        void SetFormEnvironment()
        {

            txtCode.MaxLength = CategorySpecs.Code_MaxSize;
            txtDescription.MaxLength = CategorySpecs.Description_MaxSize;

            //REQUIRED'S config
            if (CategorySpecs.Code_Necesssary)
            {
                txtCode.Attributes.Add("required", "");
            }

            if (CategorySpecs.Description_Necesssary)
            {
                txtDescription.Attributes.Add("required", "");
            }

            if (CategorySpecs.UnityWeightAverage_Necesssary)
            {
                txtUnityWeightAverage.Attributes.Add("required", "");
            }

            if (CategorySpecs.MinPriceAllowed_Necesssary)
            {
                txtMinPriceAllowed.Attributes.Add("required", "");
            }

            
            if (CategorySpecs.MaxPriceAllowed_Necesssary)
            {
                txtMinPriceAllowed.Attributes.Add("required", "");
            }

          

            New();


        }

        void BindObjectToPage()
        {

            txtCode.Text = this.category.Code;
            txtDescription.Text = this.category.Description;

            txtUnityWeightAverage.Text = this.category.UnityWeightAverage.ToString("0.00");
            txtMinPriceAllowed.Text = this.category.MinPriceAllowed.ToString("0.00");
            txtMaxPriceAllowed.Text = this.category.MaxPriceAllowed.ToString("0.00");
            txtMaxPriceAmplitude.Text = this.category.MaxPriceAmplitude.ToString("0.00");

            cbxInactive.Checked = this.category.Inactive;
            lblMode.Text = (this.category.EditionMode == false) ? GlobalVariables.Resource.GetString("InsertString", GlobalVariables.Culture) : GlobalVariables.Resource.GetString("UpdateString", GlobalVariables.Culture);// traduzir
            lblCreation.Text = (this.category.EditionMode == false) ? GlobalVariables.Resource.GetString("AutomaticString", GlobalVariables.Culture) : this.category.Creation.ToString("dddd, dd-MMM-yyyy HH:mm");
            lblVersion.Text = (this.category.EditionMode == false) ? GlobalVariables.Resource.GetString("AutomaticString", GlobalVariables.Culture) : this.category.Version.ToString("dddd, dd-MMM-yyyy HH:mm");


            ViewState["Version"] = this.category.Version.ToBinary().ToString();
            ViewState["Creation"] = this.category.Creation.ToBinary().ToString();
            ViewState["EditionMode"] = (this.category.EditionMode == false) ? "false" : "true";
            UpdatePanel1.Update();
        }


        void BindPageToObjet()
        {
            this.category = new WhereToBuy.entities.Category();

            this.category.Code = txtCode.Text.TrimEnd().ToUpper();
            this.category.Description = txtDescription.Text.TrimEnd();

            this.category.UnityWeightAverage = double.Parse(txtUnityWeightAverage.Text.TrimEnd().Replace('.', ','));
            this.category.MinPriceAllowed = decimal.Parse(txtMinPriceAllowed.Text.TrimEnd().Replace('.', ','));
            this.category.MaxPriceAllowed = decimal.Parse(txtMaxPriceAllowed.Text.TrimEnd().Replace('.', ','));
            this.category.MaxPriceAmplitude = double.Parse(txtMaxPriceAmplitude.Text.TrimEnd().Replace('.', ','));

            this.category.Inactive = cbxInactive.Checked;
            this.category.Version = DateTime.FromBinary(long.Parse(ViewState["Version"].ToString()));
            this.category.Creation = DateTime.FromBinary(long.Parse(ViewState["Creation"].ToString()));
            this.category.EditionMode = (ViewState["EditionMode"].ToString().TrimEnd().ToLower() == "false") ? false : true;

        }

        void LoadCategory(string code)
        {

            try
            {
                this.engine = new CoreEngine(Application["ConnectionString"].ToString().TrimEnd(), (User)Session["ActualUser"]);
                this.category = this.engine.Categories.Get(code);

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
                this.engine.Categories.Store(this.category);

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
                this.engine.Categories.Delete(this.category);
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

            Response.Redirect(string.Format("{0}?{1}", Application["CategoriesPage"].ToString().TrimEnd(), returnUrlQueryString), true);
        }

        void New()
        {
            this.category = CategorySpecs.New();
           
            BindObjectToPage();

            txtCode.Focus();
        }
    }
}