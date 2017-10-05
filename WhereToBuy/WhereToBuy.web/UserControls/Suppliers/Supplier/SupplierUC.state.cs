using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhereToBuy.web.UserControls.Suppliers.Supplier
{
    public partial class SupplierUC
    {
        WhereToBuy.entities.Supplier supplier;


        /// <summary>
        /// set selected object
        /// </summary>
        /// <param name="supplier">object</param>
        void SetSelectedSupplier(WhereToBuy.entities.Supplier supplier)
        {
            this.supplier = supplier;
            ViewState["SelectedSupplier"] = supplier;

        }



        /// <summary>
        /// returns is selected state exists
        /// </summary>
        public bool SelectedSupplierExist
        {
            get { return (ViewState["SelectedSupplier"] != null); }
        }


        /// <summary>
        /// returns selected object
        /// </summary>
        /// <returns>selected state</returns>
        public WhereToBuy.entities.Supplier GetSelectedSupplier()
        {
            return (WhereToBuy.entities.Supplier)ViewState["SelectedSupplier"];
        }
    }
}