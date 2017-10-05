using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhereToBuy.web.UserControls.Taxes.Taxes
{
    public class TaxesUCEventArgs : EventArgs
    {

        WhereToBuy.entities.Tax tax;
        string message = string.Empty;


        public TaxesUCEventArgs(WhereToBuy.entities.Tax tax, string message)
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


    public delegate void TaxesUCMessageHandler(object sender, TaxesUCEventArgs e);

    public partial class TaxesUC
    {
        public event TaxesUCMessageHandler TaxesUCMessage;

        protected virtual void OnTaxesUCMessage(TaxesUCEventArgs e)
        {
            if (TaxesUCMessage != null)  // Isto é nulo se nenhum codigo está à escuta deste envento
            {
                TaxesUCMessage(this, e);
            }
        }
    }
}