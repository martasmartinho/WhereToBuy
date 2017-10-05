using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WhereToBuy.entities;

namespace WhereToBuy.web.UserControls.Stocks.Stock
{
    public partial class StockUC : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            StocksP50SelBox.SelectedStockUpdate += StockUC_SelectedStockP50Update;
            StocksP50SelBox.StocksSelBoxMessage += StockUC_StockP50SelBoxMessage;
            StocksP60SelBox.SelectedStockUpdate += StockUC_SelectedStockP60Update;
            StocksP60SelBox.StocksSelBoxMessage += StockUC_StockP60SelBoxMessage;
            StocksP70SelBox.SelectedStockUpdate += StockUC_SelectedStockP70Update;
            StocksP70SelBox.StocksSelBoxMessage += StockUC_StockP70SelBoxMessage;
            StocksP80SelBox.SelectedStockUpdate += StockUC_SelectedStockP80Update;
            StocksP80SelBox.StocksSelBoxMessage += StockUC_StockP80SelBoxMessage;
            StocksP90SelBox.SelectedStockUpdate += StockUC_SelectedStockP90Update;
            StocksP90SelBox.StocksSelBoxMessage += StockUC_StockP90SelBoxMessage;


            // Load page
            if (!Page.IsPostBack)
            {
                string code = string.Empty;
                DataState dataState = DataState.None;

                if (Page.Request.QueryString["Code"] != null)
                {
                    code = Page.Request.QueryString["Code"].ToString().TrimEnd();
                }


                // load data
                UpdateData(code, dataState);

            }
            else
            {
                this.stock = (entities.Stock)ViewState["SelectedStock"];
            }
        }

        private void StockUC_StockP90SelBoxMessage(object sender, StocksSelBox.StocksSelBoxEventArgs e)
        {
            this.MessageUC.ShowError("Error", e.Message);
            return;
        }

        private void StockUC_SelectedStockP90Update(object sender, StocksSelBox.StocksSelBoxEventArgs e)
        {
            SetStockP90(e.Stock);
        }

        private void StockUC_StockP80SelBoxMessage(object sender, StocksSelBox.StocksSelBoxEventArgs e)
        {
            this.MessageUC.ShowError("Error", e.Message);
            return;
        }

        private void StockUC_SelectedStockP80Update(object sender, StocksSelBox.StocksSelBoxEventArgs e)
        {
            SetStockP80(e.Stock);
        }

        private void StockUC_StockP70SelBoxMessage(object sender, StocksSelBox.StocksSelBoxEventArgs e)
        {
            this.MessageUC.ShowError("Error", e.Message);
            return;
        }

        private void StockUC_SelectedStockP70Update(object sender, StocksSelBox.StocksSelBoxEventArgs e)
        {
            SetStockP70(e.Stock);
        }

        private void StockUC_StockP60SelBoxMessage(object sender, StocksSelBox.StocksSelBoxEventArgs e)
        {
            this.MessageUC.ShowError("Error", e.Message);
            return;
        }

        private void StockUC_SelectedStockP60Update(object sender, StocksSelBox.StocksSelBoxEventArgs e)
        {
            SetStockP60(e.Stock);
        }

        private void StockUC_StockP50SelBoxMessage(object sender, StocksSelBox.StocksSelBoxEventArgs e)
        {
            this.MessageUC.ShowError("Error", e.Message);
            return;
        }

        private void StockUC_SelectedStockP50Update(object sender, StocksSelBox.StocksSelBoxEventArgs e)
        {
            SetStockP50(e.Stock);
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            New();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            Delete();
        }

        protected void UpdatePanel1_Init(object sender, EventArgs e)
        {

        }

        protected void btnOk_Click(object sender, EventArgs e)
        {

            Save();


        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Cancel();
        }

        protected void lnkCodeSearch_Click(object sender, EventArgs e)
        {
            string code = txtCode.Text.TrimStart().TrimEnd();
            LoadStock(code);
        }
    }
}