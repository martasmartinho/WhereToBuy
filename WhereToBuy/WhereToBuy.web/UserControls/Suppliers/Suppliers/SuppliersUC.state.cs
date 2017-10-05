using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhereToBuy.web.UserControls.Suppliers.Suppliers
{
    public partial class SuppliersUC
    {

        WhereToBuy.entities.Supplier selectedSupplier;


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
        /// returns is selected object exists
        /// </summary>
        public bool SelectedSupplierExist
        {
            get { return (ViewState["SelectedSupplier"] != null); }
        }


        /// <summary>
        /// returns selected object
        /// </summary>
        /// <returns>selected object</returns>
        public WhereToBuy.entities.Supplier GetSelectedSupplier()
        {
            return (WhereToBuy.entities.Supplier)ViewState["SelectedSupplier"];
        }
    }
}