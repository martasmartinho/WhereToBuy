using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhereToBuy.web.UserControls.WarningTypes.WarningTypesSelBox
{
    public class WarningTypeSelBoxEventArgs : EventArgs
    {
         WhereToBuy.entities.WarningType warningType;
        string message = string.Empty;



        public WarningTypeSelBoxEventArgs(WhereToBuy.entities.WarningType warningType, string message)
        {
            this.warningType = warningType;
            this.message = message;
        }


        public WhereToBuy.entities.WarningType WarningType
        {
            get { return warningType; }
        }

        public string Message
        {
            get { return message; }
        }

    }


    public delegate void WarningTypeSelBoxHandler(object sender, WarningTypeSelBoxEventArgs e);
    public partial class WarningTypesSelBox
    {
        public event WarningTypeSelBoxHandler SubmitButtonClick;

        protected void OnSubmitButtonClick(WarningTypeSelBoxEventArgs e)
        {
            if (SubmitButtonClick != null)
            {
                SubmitButtonClick(this, e);
            }
        }

        public event WarningTypeSelBoxHandler SelectedWarningTypeUpdate;

        protected void OnSelectedBrandUpdate(WarningTypeSelBoxEventArgs e)
        {

            if (SelectedWarningTypeUpdate != null)
            {
                SelectedWarningTypeUpdate(this, e);
            }
        }


        public event WarningTypeSelBoxHandler WarningTypeSelBoxMessage;

        protected virtual void OnBrandSelBoxMessageHandlerMessage(WarningTypeSelBoxEventArgs e)
        {
            if (WarningTypeSelBoxMessage != null)  // Isto é nulo se nenhum codigo está à escuta deste envento
            {
                WarningTypeSelBoxMessage(this, e);
            }
        }
    }

}