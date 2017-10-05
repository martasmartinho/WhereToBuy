using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhereToBuy.web.UserControls.Stocks.StockMatching
{
    public class StockMatchingUCEventArgs : EventArgs
    {


        WhereToBuy.entities.StockMatching supplementMatching;
        string message = string.Empty;


        public StockMatchingUCEventArgs(WhereToBuy.entities.StockMatching supplementMatching, string message)
        {
            this.supplementMatching = supplementMatching;
            this.message = message;
        }


        public WhereToBuy.entities.StockMatching StockMatching
        {
            get { return supplementMatching; }
        }


        public string Message
        {
            get { return message; }
        }
    }


    public delegate void StockMatchingUCMessageHandler(object sender, StockMatchingUCEventArgs e);

    public partial class StockMatchingUC
    {
        public event StockMatchingUCMessageHandler StockMatchingUCMessage;

        protected virtual void OnStockMatchingUCMessage(StockMatchingUCEventArgs e)
        {
            if (StockMatchingUCMessage != null)  // Isto é nulo se nenhum codigo está à escuta deste envento
            {
                StockMatchingUCMessage(this, e);
            }
        }
    }
}