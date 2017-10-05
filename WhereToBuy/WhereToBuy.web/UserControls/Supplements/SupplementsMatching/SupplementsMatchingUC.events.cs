using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhereToBuy.web.UserControls.Supplements.SupplementsMatching
{
    public class SupplementsMatchingUCEventArgs : EventArgs
    {


        WhereToBuy.entities.SupplementMatching supplementMatching;
        string message = string.Empty;


        public SupplementsMatchingUCEventArgs(WhereToBuy.entities.SupplementMatching supplementMatching, string message)
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


    public delegate void SupplementsMatchingUCMessageHandler(object sender, SupplementsMatchingUCEventArgs e);

    public partial class SupplementsMatchingUC
    {
        public event SupplementsMatchingUCMessageHandler SupplementsMatchingUCMessage;

        protected virtual void OnSupplementsMatchingUCMessage(SupplementsMatchingUCEventArgs e)
        {
            if (SupplementsMatchingUCMessage != null)  // Isto é nulo se nenhum codigo está à escuta deste envento
            {
                SupplementsMatchingUCMessage(this, e);
            }
        }
    }
}