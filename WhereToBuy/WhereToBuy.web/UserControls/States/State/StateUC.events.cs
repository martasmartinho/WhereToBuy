using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhereToBuy.web.UserControls.States.State
{
    public class StateUCEventArgs : EventArgs
    {
        WhereToBuy.entities.State state;
        string message = string.Empty;


        public StateUCEventArgs(WhereToBuy.entities.State state, string message)
        {
            this.state = state;
            this.message = message;
        }


        public WhereToBuy.entities.State State
        {
            get { return state; }
        }


        public string Message
        {
            get { return message; }
        }
    }


    public delegate void StateUCMessageHandler(object sender, StateUCEventArgs e);

    public partial class StateUC
    {
        public event StateUCMessageHandler StateUCMessage;

        protected virtual void OnStateUCMessage(StateUCEventArgs e)
        {
            if (StateUCMessage != null)  // Isto é nulo se nenhum codigo está à escuta deste envento
            {
                StateUCMessage(this, e);
            }
        }
    }
}