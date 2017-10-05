using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhereToBuy.web.UserControls.WarningTypes.WarningType
{
    public class WarningTypeUCEventArgs : EventArgs
    {
        WhereToBuy.entities.WarningType warningType;
        string message = string.Empty;


        public WarningTypeUCEventArgs(WhereToBuy.entities.WarningType warningType, string message)
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


    public delegate void WarningTypeUCMessageHandler(object sender, WarningTypeUCEventArgs e);

    public partial class WarningTypeUC
    {
        public event WarningTypeUCMessageHandler WarningTypeUCMessage;

        protected virtual void OnWarningTypeUCMessage(WarningTypeUCEventArgs e)
        {
            if (WarningTypeUCMessage != null)  // Isto é nulo se nenhum codigo está à escuta deste envento
            {
                WarningTypeUCMessage(this, e);
            }
        }
    }
}