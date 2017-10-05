using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhereToBuy.web.UserControls.Stocks.StocksSelBox
{
    public class StocksSelBoxEventArgs : EventArgs
    {
        WhereToBuy.entities.Stock stock;
        string message = string.Empty;



        public StocksSelBoxEventArgs(WhereToBuy.entities.Stock stock, string message)
        {
            this.stock = stock;
            this.message = message;
        }


        public WhereToBuy.entities.Stock Stock
        {
            get { return stock; }
        }

        public string Message
        {
            get { return message; }
        }

    }


    public delegate void StocksSelBoxHandler(object sender, StocksSelBoxEventArgs e);
    public partial class StocksSelBox
    {
        public event StocksSelBoxHandler SubmitButtonClick;

        protected void OnSubmitButtonClick(StocksSelBoxEventArgs e)
        {
            if (SubmitButtonClick != null)
            {
                SubmitButtonClick(pnlStockSelBox, e);
            }
        }

        public event StocksSelBoxHandler SelectedStockUpdate;

        protected void OnSelectedStockUpdate(StocksSelBoxEventArgs e)
        {

            if (SelectedStockUpdate != null)
            {
                SelectedStockUpdate(this, e);
            }
        }


        public event StocksSelBoxHandler StocksSelBoxMessage;

        protected virtual void OnStocksSelBoxMessageHandlerMessage(StocksSelBoxEventArgs e)
        {
            if (StocksSelBoxMessage != null)  // Isto é nulo se nenhum codigo está à escuta deste envento
            {
                StocksSelBoxMessage(this, e);
            }
        }
    }
}