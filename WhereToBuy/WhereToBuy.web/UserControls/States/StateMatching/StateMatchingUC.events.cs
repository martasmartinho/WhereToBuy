using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhereToBuy.web.UserControls.States.StateMatching
{
    public class StateMatchingUCEventArgs : EventArgs
    {


        WhereToBuy.entities.StateMatching stateMatching;
        string message = string.Empty;


        public StateMatchingUCEventArgs(WhereToBuy.entities.StateMatching stateMatching, string message)
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


    public delegate void StateMatchingUCMessageHandler(object sender, StateMatchingUCEventArgs e);

    public partial class StateMatchingUC
    {
        public event StateMatchingUCMessageHandler StateMatchingUCMessage;

        protected virtual void OnStateMatchingUCMessage(StateMatchingUCEventArgs e)
        {
            if (StateMatchingUCMessage != null)  // Isto é nulo se nenhum codigo está à escuta deste envento
            {
                StateMatchingUCMessage(this, e);
            }
        }
    }
}