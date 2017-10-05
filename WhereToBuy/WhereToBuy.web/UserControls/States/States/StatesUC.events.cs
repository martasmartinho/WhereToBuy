using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhereToBuy.web.UserControls.States.States
{
    public class StatesUCEventArgs : EventArgs
    {

        WhereToBuy.entities.State state;
        string message = string.Empty;


        public StatesUCEventArgs(WhereToBuy.entities.State state, string message)
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


    public delegate void StatesUCMessageHandler(object sender, StatesUCEventArgs e);
    public partial class StatesUC
    {
        public event StatesUCMessageHandler StatesUCMessage;

        protected virtual void OnStatesUCMessage(StatesUCEventArgs e)
        {
            if (StatesUCMessage != null)  // Isto é nulo se nenhum codigo está à escuta deste envento
            {
                StatesUCMessage(this, e);
            }
        }
    }
}