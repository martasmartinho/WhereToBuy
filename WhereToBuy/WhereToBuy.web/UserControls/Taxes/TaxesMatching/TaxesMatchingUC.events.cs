using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhereToBuy.web.UserControls.Taxes.TaxesMatching
{
    public class TaxesMatchingUCEventArgs : EventArgs
    {


        WhereToBuy.entities.TaxMatching taxMatching;
        string message = string.Empty;


        public TaxesMatchingUCEventArgs(WhereToBuy.entities.TaxMatching taxMatching, string message)
        {
            this.taxMatching = taxMatching;
            this.message = message;
        }


        public WhereToBuy.entities.TaxMatching TaxMatching
        {
            get { return taxMatching; }
        }


        public string Message
        {
            get { return message; }
        }
    }


    public delegate void TaxesMatchingUCMessageHandler(object sender, TaxesMatchingUCEventArgs e);

    public partial class TaxesMatchingUC
    {
        public event TaxesMatchingUCMessageHandler TaxesMatchingUCMessage;

        protected virtual void OnTaxesMatchingUCMessage(TaxesMatchingUCEventArgs e)
        {
            if (TaxesMatchingUCMessage != null)  // Isto é nulo se nenhum codigo está à escuta deste envento
            {
                TaxesMatchingUCMessage(this, e);
            }
        }
    }
}