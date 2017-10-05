using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhereToBuy.web.UserControls.WarningTypes.WarningTypes
{
    public class WarningTypesUCEventArgs : EventArgs
    {

        WhereToBuy.entities.WarningType warningType;
        string message = string.Empty;


        public WarningTypesUCEventArgs(WhereToBuy.entities.WarningType warningType, string message)
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


    public delegate void WarningTypesUCMessageHandler(object sender, WarningTypesUCEventArgs e);

    public partial class WarningTypesUC
    {
        public event WarningTypesUCMessageHandler WarningTypesUCMessage;

        protected virtual void OnWarningTypesUCMessage(WarningTypesUCEventArgs e)
        {
            if (WarningTypesUCMessage != null)  // Isto é nulo se nenhum codigo está à escuta deste envento
            {
                WarningTypesUCMessage(this, e);
            }
        }
    }
}