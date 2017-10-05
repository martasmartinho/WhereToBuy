using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhereToBuy.web.UserControls.Stocks.Stocks
{
    public class StocksUCEventArgs : EventArgs
    {

        WhereToBuy.entities.Stock stock;
        string message = string.Empty;


        public StocksUCEventArgs(WhereToBuy.entities.Stock stock, string message)
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


    public delegate void StocksUCMessageHandler(object sender, StocksUCEventArgs e);

    public partial class StocksUC
    {
        public event StocksUCMessageHandler StocksUCMessage;

        protected virtual void OnStocksUCMessage(StocksUCEventArgs e)
        {
            if (StocksUCMessage != null)  // Isto é nulo se nenhum codigo está à escuta deste envento
            {
                StocksUCMessage(this, e);
            }
        }
    }
}