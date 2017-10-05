using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhereToBuy.web.UserControls.States.StatesMatching
{
    public class StatesMatchingUCEventArgs : EventArgs
    {


        WhereToBuy.entities.StateMatching stateMatching;
        string message = string.Empty;


        public StatesMatchingUCEventArgs(WhereToBuy.entities.StateMatching stateMatching, string message)
        {
            this.stateMatching = stateMatching;
            this.message = message;
        }


        public WhereToBuy.entities.StateMatching StateMatching
        {
            get { return stateMatching; }
        }


        public string Message
        {
            get { return message; }
        }
    }


    public delegate void StatesMatchingUCMessageHandler(object sender, StatesMatchingUCEventArgs e);

    public partial class StatesMatchingUC
    {
        public event StatesMatchingUCMessageHandler StatesMatchingUCMessage;

        protected virtual void OnStatesMatchingUCMessage(StatesMatchingUCEventArgs e)
        {
            if (StatesMatchingUCMessage != null)  // Isto é nulo se nenhum codigo está à escuta deste envento
            {
                StatesMatchingUCMessage(this, e);
            }
        }
    }
}