using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhereToBuy.web.UserControls.Supplements.Supplement
{
    public class SupplementUCEventArgs : EventArgs
    {
        WhereToBuy.entities.Supplement supplement;
        string message = string.Empty;


        public SupplementUCEventArgs(WhereToBuy.entities.Supplement supplement, string message)
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


    public delegate void SupplementUCMessageHandler(object sender, SupplementUCEventArgs e);

    public partial class SupplementUC
    {
        public event SupplementUCMessageHandler SupplementUCMessage;

        protected virtual void OnSupplementUCMessage(SupplementUCEventArgs e)
        {
            if (SupplementUCMessage != null)  // Isto é nulo se nenhum codigo está à escuta deste envento
            {
                SupplementUCMessage(this, e);
            }
        }
    }
}