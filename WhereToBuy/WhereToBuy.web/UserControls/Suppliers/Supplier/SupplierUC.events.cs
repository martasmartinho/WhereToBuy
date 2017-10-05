using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhereToBuy.web.UserControls.Suppliers.Supplier
{
    public class SupplierUCEventArgs : EventArgs
    {
        WhereToBuy.entities.Supplier supplier;
        string message = string.Empty;


        public SupplierUCEventArgs(WhereToBuy.entities.Supplier supplier, string message)
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


    public delegate void SupplierUCMessageHandler(object sender, SupplierUCEventArgs e);

    public partial class SupplierUC
    {
        public event SupplierUCMessageHandler SupplierUCMessage;

        protected virtual void OnSupplierUCMessage(SupplierUCEventArgs e)
        {
            if (SupplierUCMessage != null)  // Isto é nulo se nenhum codigo está à escuta deste envento
            {
                SupplierUCMessage(this, e);
            }
        }
    }
}