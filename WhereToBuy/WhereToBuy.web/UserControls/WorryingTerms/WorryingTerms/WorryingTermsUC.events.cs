using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhereToBuy.web.UserControls.WorryingTerms.WorryingTerms
{
    public class WorryingTermsUCEventArgs : EventArgs
    {

        WhereToBuy.entities.WorryingTerm worryingTerm;
        string message = string.Empty;


        public WorryingTermsUCEventArgs(WhereToBuy.entities.WorryingTerm worryingTerm, string message)
        {
            this.worryingTerm = worryingTerm;
            this.message = message;
        }


        public WhereToBuy.entities.WorryingTerm WorryingTerm
        {
            get { return worryingTerm; }
        }


        public string Message
        {
            get { return message; }
        }
    }


    public delegate void WorryingTermsUCMessageHandler(object sender, WorryingTermsUCEventArgs e);

    public partial class WorryingTermsUC
    {
        public event WorryingTermsUCMessageHandler WorryingTermsUCMessage;

        protected virtual void OnWorryingTermsUCMessage(WorryingTermsUCEventArgs e)
        {
            if (WorryingTermsUCMessage != null)  // Isto é nulo se nenhum codigo está à escuta deste envento
            {
                WorryingTermsUCMessage(this, e);
            }
        }
    }
}