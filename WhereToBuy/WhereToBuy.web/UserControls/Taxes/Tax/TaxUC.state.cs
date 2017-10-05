using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhereToBuy.web.UserControls.Taxes.Tax
{
    public partial class TaxUC
    {
        WhereToBuy.entities.Tax tax;


        /// <summary>
        /// set selected object
        /// </summary>
        /// <param name="tax">object</param>
        void SetSelectedTax(WhereToBuy.entities.Tax tax)
        {
            this.tax = tax;
            ViewState["SelectedTax"] = tax;

        }



        /// <summary>
        /// returns is selected state exists
        /// </summary>
        public bool SelectedTaxExist
        {
            get { return (ViewState["SelectedTax"] != null); }
        }


        /// <summary>
        /// returns selected object
        /// </summary>
        /// <returns>selected state</returns>
        public WhereToBuy.entities.Tax GetSelectedTax()
        {
            return (WhereToBuy.entities.Tax)ViewState["SelectedTax"];
        }
    }
}