using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WhereToBuy.entities;

namespace WhereToBuy.web.UserControls.Suppliers.SuppliersSelBox
{
    public class SupplierSelBoxEventArgs : EventArgs
    {

        WhereToBuy.entities.Supplier supplier;
        string message = string.Empty;
        


        public SupplierSelBoxEventArgs(WhereToBuy.entities.Supplier supplier, string message)
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


    public delegate void SupplierSelBoxHandler(object sender, SupplierSelBoxEventArgs e);

    public partial class SuppliersSelBox
    {

        public event SupplierSelBoxHandler SubmitButtonClick;

        protected void OnSubmitButtonClick(SupplierSelBoxEventArgs e)
        {
            if (SubmitButtonClick != null)
            {
                SubmitButtonClick(this, e);
            }
        }

        public event SupplierSelBoxHandler SelectedSupplierUpdate;

        protected void OnSelectedSupplierUpdate(SupplierSelBoxEventArgs e)
        {
            
            if (SelectedSupplierUpdate != null)
            {
                SelectedSupplierUpdate(this, e);
            }
        }


        public event SupplierSelBoxHandler SupplierSelBoxMessage;
        
        protected virtual void OnSupplierSelBoxMessageHandlerMessage(SupplierSelBoxEventArgs e)
        {
            if (SupplierSelBoxMessage != null)  // Isto é nulo se nenhum codigo está à escuta deste envento
            {
                SupplierSelBoxMessage(this, e);
            }
        }
    }

   

}