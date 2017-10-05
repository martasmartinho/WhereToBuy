using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhereToBuy.web.UserControls.QuotationWarnings
{
    public partial class QuotationWarningsUC
    {
        WhereToBuy.entities.QuotationWarning selectedQuotationWarning;
        WhereToBuy.entities.Supplier selectedSupplier;
        WhereToBuy.entities.WarningType selectedWarningType;

        /// <summary>
        /// set selected object
        /// </summary>
        /// <param name="selectedQuotationWarning">object</param>
        void SetSelectedQuotationWarning(WhereToBuy.entities.QuotationWarning selectedQuotationWarning)
        {
            this.selectedQuotationWarning = selectedQuotationWarning;
            ViewState["SelectedQuotationWarning"] = selectedQuotationWarning;

        }

        /// <summary>
        /// set selected object
        /// </summary>
        /// <param name="selectedSupplier">object</param>
        void SetSelectedSupplier(WhereToBuy.entities.Supplier selectedSupplier)
        {
            this.selectedSupplier = selectedSupplier;
            ViewState["SelectedSupplier"] = selectedSupplier;

        }


        /// <summary>
        /// set selected object
        /// </summary>
        /// <param name="selectedWarningType">object</param>
        void SetSelectedWarningType(WhereToBuy.entities.WarningType selectedWarningType)
        {
            this.selectedWarningType = selectedWarningType;
            ViewState["SelectedWarningType"] = selectedWarningType;

        }


        /// <summary>
        /// returns if selected object exists
        /// </summary>
        public bool SelectedQuotationWarningExist
        {
            get { return (ViewState["SelectedQuotationWarning"] != null); }
        }

        /// <summary>
        /// returns if selected object exists
        /// </summary>
        public bool SelectedSupplierExist
        {
            get { return (ViewState["SelectedSupplier"] != null); }
        }

        /// <summary>
        /// returns is selected object exists
        /// </summary>
        public bool SelectedWarningTypeExist
        {
            get { return (ViewState["SelectedWarningType"] != null); }
        }


        /// <summary>
        /// returns selected object
        /// </summary>
        /// <returns>selected object</returns>
        public WhereToBuy.entities.QuotationWarning GetSelectedQuotationWarning()
        {
            return (WhereToBuy.entities.QuotationWarning)ViewState["SelectedQuotationWarning"];
        }

        /// <summary>
        /// returns selected object
        /// </summary>
        /// <returns>selected object</returns>
        public WhereToBuy.entities.Supplier GetSelectedSupplier()
        {
            return (WhereToBuy.entities.Supplier)ViewState["SelectedSupplier"];
        }

        /// <summary>
        /// returns selected object
        /// </summary>
        /// <returns>selected object</returns>
        public WhereToBuy.entities.WarningType GetSelectedWarningType()
        {
            return (WhereToBuy.entities.WarningType)ViewState["SelectedWarningType"];
        }

        
    }
}