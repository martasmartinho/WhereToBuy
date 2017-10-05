using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhereToBuy.web.UserControls.Stocks.Stock
{
    public class StockUCEventArgs : EventArgs
    {
        WhereToBuy.entities.Stock stock;
        string message = string.Empty;


        public StockUCEventArgs(WhereToBuy.entities.Stock stock, string message)
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


    public delegate void StockUCMessageHandler(object sender, StockUCEventArgs e);

    public partial class StockUC
    {
        public event StockUCMessageHandler StockUCMessage;

        protected virtual void OnStockUCMessage(StockUCEventArgs e)
        {
            if (StockUCMessage != null)  // Isto é nulo se nenhum codigo está à escuta deste envento
            {
                StockUCMessage(this, e);
            }
        }
    }
}