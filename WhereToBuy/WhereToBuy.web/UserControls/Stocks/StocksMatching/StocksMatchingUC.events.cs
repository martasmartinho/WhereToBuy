using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhereToBuy.web.UserControls.Stocks.StocksMatching
{
    public class StocksMatchingUCEventArgs : EventArgs
    {


        WhereToBuy.entities.StockMatching stockMatching;
        string message = string.Empty;


        public StocksMatchingUCEventArgs(WhereToBuy.entities.StockMatching stockMatching, string message)
        {
            this.stockMatching = stockMatching;
            this.message = message;
        }


        public WhereToBuy.entities.StockMatching StockMatching
        {
            get { return stockMatching; }
        }


        public string Message
        {
            get { return message; }
        }
    }


    public delegate void StocksMatchingUCMessageHandler(object sender, StocksMatchingUCEventArgs e);

    public partial class StocksMatchingUC
    {
        public event StocksMatchingUCMessageHandler StocksMatchingUCMessage;

        protected virtual void OnStocksMatchingUCMessage(StocksMatchingUCEventArgs e)
        {
            if (StocksMatchingUCMessage != null)  // Isto é nulo se nenhum codigo está à escuta deste envento
            {
                StocksMatchingUCMessage(this, e);
            }
        }
    }
}