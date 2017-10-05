using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhereToBuy.web.UserControls.Taxes.TaxMatching
{
    public class TaxMatchingUCEventArgs : EventArgs
    {


        WhereToBuy.entities.TaxMatching taxMatching;
        string message = string.Empty;


        public TaxMatchingUCEventArgs(WhereToBuy.entities.TaxMatching taxMatching, string message)
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


    public delegate void TaxMatchingUCMessageHandler(object sender, TaxMatchingUCEventArgs e);

    public partial class TaxMatchingUC
    {

        public event TaxMatchingUCMessageHandler TaxMatchingUCMessage;

        protected virtual void OnTaxMatchingUCMessage(TaxMatchingUCEventArgs e)
        {
            if (TaxMatchingUCMessage != null)  // Isto é nulo se nenhum codigo está à escuta deste envento
            {
                TaxMatchingUCMessage(this, e);
            }
        }
    }
}