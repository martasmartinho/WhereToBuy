using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhereToBuy.web.UserControls.QuotationRules.QuotationRules
{
    public class QuotationRulesUCEventArgs : EventArgs
    {

        WhereToBuy.entities.QuotationRule quotationRule;
        string message = string.Empty;


        public QuotationRulesUCEventArgs(WhereToBuy.entities.QuotationRule quotationRule, string message)
        {
            this.quotationRule = quotationRule;
            this.message = message;
        }


        public WhereToBuy.entities.QuotationRule QuotationRule
        {
            get { return quotationRule; }
        }


        public string Message
        {
            get { return message; }
        }
    }


    public delegate void QuotationRulesUCMessageHandler(object sender, QuotationRulesUCEventArgs e);

    public partial class QuotationRulesUC
    {
        public event QuotationRulesUCMessageHandler QuotationRulesUCMessage;

        protected virtual void OnQuotationRulesUCMessage(QuotationRulesUCEventArgs e)
        {
            if (QuotationRulesUCMessage != null)  // Isto é nulo se nenhum codigo está à escuta deste envento
            {
                QuotationRulesUCMessage(this, e);
            }
        }
    }
}