using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using WhereToBuy.core;
using WhereToBuy.entities;
using WhereToBuy.entities.specs;

namespace WhereToBuy.web.UserControls.Stocks.Stock
{
    public partial class StockUC
    {
        CoreEngine engine;

        public void UpdateData(string code, DataState dataState)
        {

            if (txtCode.MaxLength == 0)
            {
                SetFormEnvironment();
            }

            

            if (code != "")
            {
                LoadStock(code);

            }

            UpdatePanel1.Update();

        }


        void SetFormEnvironment()
        {

            txtCode.MaxLength = StockSpecs.Code_MaxSize;
            txtDescription.MaxLength = StockSpecs.Description_MaxSize;

           

            //REQUIRED'S config
            if (StockSpecs.Code_Necesssary)
            {
                txtCode.Attributes.Add("required", "");
            }

            if (StockSpecs.Description_Necesssary)
            {
                txtDescription.Attributes.Add("required", "");
            }
            if (StockSpecs.AvailabilityLevel_Necesssary)
            {
                txtDescription.Attributes.Add("required", "");
            }
            
            New();


        }

        void BindObjectToPage()
        {

            txtCode.Text = this.stock.Code;
            txtDescription.Text = this.stock.Description;
            txtAvailabilityLevel.Text = this.stock.AvailabilityLevel.ToString();
           
            cbxInactive.Checked = this.stock.Inactive;
            lblMode.Text = (this.stock.EditionMode == false) ? "(Insert)" : "(Update)";// traduzir
            lblCreation.Text = (this.stock.EditionMode == false) ? "(Automatic)" : this.stock.Creation.ToString("dddd, dd-MMM-yyyy HH:mm");
            lblVersion.Text = (this.stock.EditionMode == false) ? "(Automatic)" : this.stock.Version.ToString("dddd, dd-MMM-yyyy HH:mm");




            ViewState["Version"] = this.stock.Version.ToBinary().ToString();
            ViewState["Creation"] = this.stock.Creation.ToBinary().ToString();
            ViewState["EditionMode"] = (this.stock.EditionMode == false) ? "false" : "true";
            UpdatePanel1.Update();
        }


        void BindPageToObjet()
        {
            this.stock = new WhereToBuy.entities.Stock();

            this.stock.Code = txtCode.Text.TrimEnd().ToUpper();
            this.stock.Description = txtDescription.Text.TrimEnd();
            this.stock.AvailabilityLevel = short.Parse(txtAvailabilityLevel.Text);

            this.stock.StockCodeExpirationP50 = (entities.Stock)ViewState["SelectedStockP50"];
            this.stock.StockCodeExpirationP60 = (entities.Stock)ViewState["SelectedStockP60"];
            this.stock.StockCodeExpirationP70 = (entities.Stock)ViewState["SelectedStockP70"];
            this.stock.StockCodeExpirationP80 = (entities.Stock)ViewState["SelectedStockP80"];
            this.stock.StockCodeExpirationP90 = (entities.Stock)ViewState["SelectedStockP90"];


            this.stock.Inactive = cbxInactive.Checked;
            this.stock.Version = DateTime.FromBinary(long.Parse(ViewState["Version"].ToString()));
            this.stock.Creation = DateTime.FromBinary(long.Parse(ViewState["Creation"].ToString()));
            this.stock.EditionMode = (ViewState["EditionMode"].ToString().TrimEnd().ToLower() == "false") ? false : true;

        }

        void LoadStock(string code)
        {

            try
            {
                this.engine = new CoreEngine(Application["ConnectionString"].ToString().TrimEnd(), (User)Session["ActualUser"]);
                this.stock = this.engine.Stocks.Get(code, DataState.All, 1, 1, 1, 1, 1);

                if (this.stock.StockCodeExpirationP50 != null)
                {
                    SetStockP50(stock.StockCodeExpirationP50);

                    var tbx = StocksP50SelBox.FindControl("txtStock");
                    ((TextBox)tbx).Text = this.stock.StockCodeExpirationP50.ToString();
                   
                }

                if (this.stock.StockCodeExpirationP50 != null)
                {
                    SetStockP50(stock.StockCodeExpirationP50);

                    var tbx = StocksP50SelBox.FindControl("txtStock");
                    ((TextBox)tbx).Text = this.stock.StockCodeExpirationP50.ToString();

                }

                if (this.stock.StockCodeExpirationP60 != null)
                {
                    SetStockP60(stock.StockCodeExpirationP60);

                    var tbx = StocksP60SelBox.FindControl("txtStock");
                    ((TextBox)tbx).Text = this.stock.StockCodeExpirationP60.ToString();

                }

                if (this.stock.StockCodeExpirationP70 != null)
                {
                    SetStockP70(stock.StockCodeExpirationP70);

                    var tbx = StocksP70SelBox.FindControl("txtStock");
                    ((TextBox)tbx).Text = this.stock.StockCodeExpirationP70.ToString();

                }

                if (this.stock.StockCodeExpirationP80 != null)
                {
                    SetStockP50(stock.StockCodeExpirationP80);

                    var tbx = StocksP80SelBox.FindControl("txtStock");
                    ((TextBox)tbx).Text = this.stock.StockCodeExpirationP80.ToString();

                }

                if (this.stock.StockCodeExpirationP90 != null)
                {
                    SetStockP50(stock.StockCodeExpirationP90);

                    var tbx = StocksP90SelBox.FindControl("txtStock");
                    ((TextBox)tbx).Text = this.stock.StockCodeExpirationP90.ToString();

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
                this.engine.Stocks.Store(this.stock);

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
                this.engine.Stocks.Delete(this.stock);
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

           
            Response.Redirect(string.Format("{0}?{1}", Application["StocksPage"].ToString().TrimEnd(), returnUrlQueryString), true);
        }

        void New()
        {
            this.stock = StockSpecs.New();
            SetSelectedStock(this.stock);
            SetStockP50(this.stock.StockCodeExpirationP50);
            SetStockP60(this.stock.StockCodeExpirationP60);
            SetStockP70(this.stock.StockCodeExpirationP70);
            SetStockP80(this.stock.StockCodeExpirationP80);
            SetStockP90(this.stock.StockCodeExpirationP90);

            var tbx = StocksP50SelBox.FindControl("txtStock");
            ((TextBox)tbx).Text = "";

            tbx = StocksP60SelBox.FindControl("txtStock");
            ((TextBox)tbx).Text = "";

            tbx = StocksP70SelBox.FindControl("txtStock");
            ((TextBox)tbx).Text = "";

            tbx = StocksP80SelBox.FindControl("txtStock");
            ((TextBox)tbx).Text = "";

            tbx = StocksP90SelBox.FindControl("txtStock");
            ((TextBox)tbx).Text = "";

            BindObjectToPage();

            txtCode.Focus();
        }
    }
}