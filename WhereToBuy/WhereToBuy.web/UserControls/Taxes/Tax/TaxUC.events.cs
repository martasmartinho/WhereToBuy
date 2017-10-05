using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhereToBuy.web.UserControls.Taxes.Tax
{
    public class TaxUCEventArgs : EventArgs
    {
        WhereToBuy.entities.Tax tax;
        string message = string.Empty;


        public TaxUCEventArgs(WhereToBuy.entities.Tax tax, string message)
        {
            this.tax = tax;
            this.message = message;
        }


        public WhereToBuy.entities.Tax Tax
        {
            get { return tax; }
        }


        public string Message
        {
            get { return message; }
        }
    }


    public delegate void TaxUCMessageHandler(object sender, TaxUCEventArgs e);

    public partial class TaxUC
    {
        public event TaxUCMessageHandler TaxUCMessage;

        protected virtual void OnTaxUCMessage(TaxUCEventArgs e)
        {
            if (TaxUCMessage != null)  // Isto é nulo se nenhum codigo está à escuta deste envento
            {
                TaxUCMessage(this, e);
            }
        }
    }
}