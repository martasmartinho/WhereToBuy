using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhereToBuy.web.UserControls.Suppliers.Suppliers
{
    public class SuppliersUCEventArgs : EventArgs
    {

        WhereToBuy.entities.Supplier supplier;
        string message = string.Empty;


        public SuppliersUCEventArgs(WhereToBuy.entities.Supplier supplier, string message)
        {
            this.supplier = supplier;
            this.message = message;
        }


        public WhereToBuy.entities.Supplier Supplier
        {
            get { return supplier; }
        }


        public string Message
        {
            get { return message; }
        }
    }


    public delegate void SuppliersUCMessageHandler(object sender, SuppliersUCEventArgs e);

    public partial class SuppliersUC
    {
        public event SuppliersUCMessageHandler SuppliersUCMessage;

        protected virtual void OnSuppliersUCMessage(SuppliersUCEventArgs e)
        {
            if (SuppliersUCMessage != null)  // Isto é nulo se nenhum codigo está à escuta deste envento
            {
                SuppliersUCMessage(this, e);
            }
        }
    }
}