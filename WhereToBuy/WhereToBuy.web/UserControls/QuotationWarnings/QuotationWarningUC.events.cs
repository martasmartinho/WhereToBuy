using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhereToBuy.web.UserControls.QuotationWarnings
{
    public class QuotationWarningsUCEventArgs : EventArgs
    {

        WhereToBuy.entities.QuotationWarning quotationWarning;
        string message = string.Empty;


        public QuotationWarningsUCEventArgs(WhereToBuy.entities.QuotationWarning quotationWarning, string message)
        {
            this.quotationWarning = quotationWarning;
            this.message = message;
        }


        public WhereToBuy.entities.QuotationWarning QuotationWarning
        {
            get { return quotationWarning; }
        }


        public string Message
        {
            get { return message; }
        }
    }


    public delegate void QuotationWarningsUCMessageHandler(object sender, QuotationWarningsUCEventArgs e);

    public partial class QuotationWarningsUC
    {
        public event QuotationWarningsUCMessageHandler QuotationWarningsUCMessage;

        protected virtual void OnQuotationWarningsUCMessage(QuotationWarningsUCEventArgs e)
        {
            if (QuotationWarningsUCMessage != null)  // Isto é nulo se nenhum codigo está à escuta deste envento
            {
                QuotationWarningsUCMessage(this, e);
            }
        }
    }
}