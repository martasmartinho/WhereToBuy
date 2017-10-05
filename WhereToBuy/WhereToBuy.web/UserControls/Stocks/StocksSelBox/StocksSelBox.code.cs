using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WhereToBuy.core;
using WhereToBuy.entities;

namespace WhereToBuy.web.UserControls.Stocks.StocksSelBox
{
    public partial class StocksSelBox
    {
        CoreEngine engine;
        bool required = false;

        public void UpdateData(string code, bool required)
        {
            this.required = required;

            if (ViewState["StockOrderBy"] == null)
            {
                SetFormEnvironment();
            }

            txtStock.Text = code.TrimEnd();


            if (code != "")
            {
                RefreshListView();
            }

        }

        public void UpdateData(WhereToBuy.entities.Stock stock, bool required)
        {
            this.required = required;

            if (ViewState["SupplierOrderBy"] == null)
            {
                SetFormEnvironment();
            }

            txtStock.Text = stock.ToString();

        }


        void SetFormEnvironment()
        {
            ViewState.Add("StockOrderBy", "[Codigo]");
            ViewState.Add("StockOrderByType", "ASC");

            if (this.required)
            {
                txtStock.Attributes.Add("required", "");
            }


            SetListBoxEnvironment();

        }



        private void SetListBoxEnvironment()
        {

            txtStock.Text = string.Empty;
            lvStocks.DataKeyNames = new string[] { "Code", "Description" };
            lvStocks.Items.Clear();

        }


        void RefreshListView()
        {

            List<WhereToBuy.entities.Stock> stocks;
            string code = txtStock.Text.TrimStart().TrimEnd();

            if (code != "")
            {
                try
                {
                    engine = new CoreEngine(Application["ConnectionString"].ToString().TrimEnd(), (User)Session["ActualUser"]);

                    stocks = engine.Stocks.Get(code, true);
                    engine = null;

                    // show data
                    lvStocks.DataSource = stocks;
                    lvStocks.DataBind();
                    UpdatePanel1.Update();

                }
                catch (MyException ex)
                {
                    StocksSelBoxMessage(this, new StocksSelBoxEventArgs(null, ex.Message));
                    return;


                }
                catch (Exception ex)
                {
                    StocksSelBoxMessage(this, new StocksSelBoxEventArgs(null, ex.Message));
                    return;
                }

            }
            else
            {
                lvStocks.Items.Clear();
                lvStocks.DataBind();
            }

        }

        WhereToBuy.entities.Stock LoadStock(string code)
        {
            WhereToBuy.entities.Stock stock;
            stock = new WhereToBuy.entities.Stock();

            try
            {

                engine = new CoreEngine(Application["ConnectionString"].ToString().TrimEnd(), (User)Session["ActualUser"]);
                stock = engine.Stocks.Get(code);
                engine = null;
            }
            catch (MyException ex)
            {
                StocksSelBoxMessage(this, new StocksSelBoxEventArgs(null, ex.Message));
                return stock;
            }
            catch (Exception ex)
            {
                StocksSelBoxMessage(this, new StocksSelBoxEventArgs(null, ex.Message));
                return stock;
            }
            return stock;
        }


        void Clear()
        {
            txtStock.Text = "";
            lvStocks.Items.Clear();
            lvStocks.DataBind();
        }
    }
}