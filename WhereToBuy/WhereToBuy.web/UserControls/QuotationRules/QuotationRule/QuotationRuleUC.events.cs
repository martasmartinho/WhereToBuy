using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhereToBuy.web.UserControls.QuotationRules.QuotationRule
{
    public class QuotationRuleUCEventArgs : EventArgs
    {
        WhereToBuy.entities.QuotationRule quotationRule;
        string message = string.Empty;


        public QuotationRuleUCEventArgs(WhereToBuy.entities.QuotationRule quotationRule, string message)
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


    public delegate void QuotationRuleUCMessageHandler(object sender, QuotationRuleUCEventArgs e);

    public partial class QuotationRuleUC
    {
        public event QuotationRuleUCMessageHandler QuotationRuleUCMessage;

        protected virtual void OnQuotationRuleUCMessage(QuotationRuleUCEventArgs e)
        {
            if (QuotationRuleUCMessage != null)  // Isto é nulo se nenhum codigo está à escuta deste envento
            {
                QuotationRuleUCMessage(this, e);
            }
        }
    }
}