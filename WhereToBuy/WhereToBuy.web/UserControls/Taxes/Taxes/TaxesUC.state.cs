using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhereToBuy.web.UserControls.Taxes.Taxes
{
    public partial class TaxesUC
    {

        WhereToBuy.entities.Tax selectedTax;


        /// <summary>
        /// set selected object
        /// </summary>
        /// <param name="selectedTax">object</param>
        void SetSelectedTax(WhereToBuy.entities.Tax selectedTax)
        {
            this.selectedTax = selectedTax;
            ViewState["SelectedTax"] = selectedTax;

        }


        /// <summary>
        /// returns is selected object exists
        /// </summary>
        public bool SelectedTaxExist
        {
            get { return (ViewState["SelectedTax"] != null); }
        }


        /// <summary>
        /// returns selected object
        /// </summary>
        /// <returns>selected object</returns>
        public WhereToBuy.entities.Tax GetSelectedTax()
        {
            return (WhereToBuy.entities.Tax)ViewState["SelectedTax"];
        }
    }
}