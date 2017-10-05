using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhereToBuy.web.UserControls.WorryingTerms.WorryingTerm
{
    public class WorryingTermUCEventArgs : EventArgs
    {
        WhereToBuy.entities.WorryingTerm worryingTerm;
        string message = string.Empty;


        public WorryingTermUCEventArgs(WhereToBuy.entities.WorryingTerm worryingTerm, string message)
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


    public delegate void WorryingTermUCMessageHandler(object sender, WorryingTermUCEventArgs e);

    public partial class WorryingTermUC
    {
        public event WorryingTermUCMessageHandler WorryingTermUCMessage;

        protected virtual void OnWorryingTermUCMessage(WorryingTermUCEventArgs e)
        {
            if (WorryingTermUCMessage != null)  // Isto é nulo se nenhum codigo está à escuta deste envento
            {
                WorryingTermUCMessage(this, e);
            }
        }
    }
}