using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhereToBuy.web.UserControls.Taxes.TaxSelBox
{
    public class TaxesSelBoxEventArgs : EventArgs
    {

        WhereToBuy.entities.Tax tax;
        string message = string.Empty;



        public TaxesSelBoxEventArgs(WhereToBuy.entities.Tax tax, string message)
        {
            this.tax = tax;
            this.message = message;
        }


        public WhereToBuy.entities.Tax Tax
        {
            get { return tax; }
        }

        public string Message
        {
            get { return message; }
        }

    }


    public delegate void TaxesSelBoxHandler(object sender, TaxesSelBoxEventArgs e);

    public partial class TaxSelBox
    {
        public event TaxesSelBoxHandler SubmitButtonClick;

        protected void OnSubmitButtonClick(TaxesSelBoxEventArgs e)
        {
            if (SubmitButtonClick != null)
            {
                SubmitButtonClick(this, e);
            }
        }

        public event TaxesSelBoxHandler SelectedTaxUpdate;

        protected void OnSelectedTaxUpdate(TaxesSelBoxEventArgs e)
        {

            if (SelectedTaxUpdate != null)
            {
                SelectedTaxUpdate(this, e);
            }
        }


        public event TaxesSelBoxHandler TaxesSelBoxMessage;

        protected virtual void OnTaxesSelBoxMessageHandlerMessage(TaxesSelBoxEventArgs e)
        {
            if (TaxesSelBoxMessage != null)  // Isto é nulo se nenhum codigo está à escuta deste envento
            {
                TaxesSelBoxMessage(this, e);
            }
        }
    }
}