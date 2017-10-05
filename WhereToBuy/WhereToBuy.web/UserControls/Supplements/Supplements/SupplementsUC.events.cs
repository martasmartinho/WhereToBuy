using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhereToBuy.web.UserControls.Supplements.Supplements
{
    public class SupplementsUCEventArgs : EventArgs
    {

        WhereToBuy.entities.Supplement supplement;
        string message = string.Empty;


        public SupplementsUCEventArgs(WhereToBuy.entities.Supplement supplement, string message)
        {
            this.supplement = supplement;
            this.message = message;
        }


        public WhereToBuy.entities.Supplement Supplement
        {
            get { return supplement; }
        }


        public string Message
        {
            get { return message; }
        }
    }


    public delegate void SupplementsUCMessageHandler(object sender, SupplementsUCEventArgs e);

    public partial class SupplementsUC
    {
        public event SupplementsUCMessageHandler SupplementsUCMessage;

        protected virtual void OnSupplementsUCMessage(SupplementsUCEventArgs e)
        {
            if (SupplementsUCMessage != null)  // Isto é nulo se nenhum codigo está à escuta deste envento
            {
                SupplementsUCMessage(this, e);
            }
        }
    }
}