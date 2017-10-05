using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhereToBuy.web.UserControls.States.StatesSelBox
{
    public class StateSelBoxEventArgs : EventArgs
    {

        WhereToBuy.entities.State state;
        string message = string.Empty;



        public StateSelBoxEventArgs(WhereToBuy.entities.State state, string message)
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


    public delegate void StateSelBoxHandler(object sender, StateSelBoxEventArgs e);

    public partial class StatesSelBox
    {
        public event StateSelBoxHandler SubmitButtonClick;

        protected void OnSubmitButtonClick(StateSelBoxEventArgs e)
        {
            if (SubmitButtonClick != null)
            {
                SubmitButtonClick(this, e);
            }
        }

        public event StateSelBoxHandler SelectedStateUpdate;

        protected void OnSelectedStateUpdate(StateSelBoxEventArgs e)
        {

            if (SelectedStateUpdate != null)
            {
                SelectedStateUpdate(this, e);
            }
        }


        public event StateSelBoxHandler StateSelBoxMessage;

        protected virtual void OnStateSelBoxMessageHandlerMessage(StateSelBoxEventArgs e)
        {
            if (StateSelBoxMessage != null)  // Isto é nulo se nenhum codigo está à escuta deste envento
            {
                StateSelBoxMessage(this, e);
            }
        }
    }
}