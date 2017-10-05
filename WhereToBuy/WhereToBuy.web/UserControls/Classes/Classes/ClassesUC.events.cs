using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhereToBuy.web.UserControls.Classes.Classes
{
    public class ClassesUCEventArgs : EventArgs
    {

        WhereToBuy.entities.Classe classe;
        string message = string.Empty;


        public ClassesUCEventArgs(WhereToBuy.entities.Classe classe, string message)
        {
            this.classe = classe;
            this.message = message;
        }


        public WhereToBuy.entities.Classe Classe
        {
            get { return classe; }
        }


        public string Message
        {
            get { return message; }
        }
    }


    public delegate void ClassesUCMessageHandler(object sender, ClassesUCEventArgs e);

    public partial class ClassesUC
    {
        public event ClassesUCMessageHandler ClassesUCMessage;

        protected virtual void OnClassesUCMessage(ClassesUCEventArgs e)
        {
            if (ClassesUCMessage != null)  // Isto é nulo se nenhum codigo está à escuta deste envento
            {
                ClassesUCMessage(this, e);
            }
        }
    }
}