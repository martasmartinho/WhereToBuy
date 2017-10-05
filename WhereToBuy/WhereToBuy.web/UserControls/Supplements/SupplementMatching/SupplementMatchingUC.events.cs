using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhereToBuy.web.UserControls.Supplements.SupplementMatching
{
    public class SupplementMatchingUCEventArgs : EventArgs
    {


        WhereToBuy.entities.SupplementMatching supplementMatching;
        string message = string.Empty;


        public SupplementMatchingUCEventArgs(WhereToBuy.entities.SupplementMatching supplementMatching, string message)
        {
            this.supplementMatching = supplementMatching;
            this.message = message;
        }


        public WhereToBuy.entities.SupplementMatching SupplementMatching
        {
            get { return supplementMatching; }
        }


        public string Message
        {
            get { return message; }
        }
    }


    public delegate void SupplementMatchingUCMessageHandler(object sender, SupplementMatchingUCEventArgs e);

    public partial class SupplementMatchingUC
    {
        public event SupplementMatchingUCMessageHandler SupplementMatchingUCMessage;

        protected virtual void OnSupplementMatchingUCMessage(SupplementMatchingUCEventArgs e)
        {
            if (SupplementMatchingUCMessage != null)  // Isto é nulo se nenhum codigo está à escuta deste envento
            {
                SupplementMatchingUCMessage(this, e);
            }
        }
    }
}