using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WhereToBuy.entities;

namespace WhereToBuy.web.UserControls.BrandsMatching
{
    public partial class BrandsMatchingUC
    {
        BrandMatching selectedMatching;
        Supplier selectedSupplier;


        /// <summary>
        /// set selected matching
        /// </summary>
        /// <param name="selectedMatching">matching</param>
        void SetSelectedMatching(BrandMatching selectedMatching)
        {
            this.selectedMatching = selectedMatching;
            ViewState["SelectedBrandMatching"] = selectedMatching;
        }


        void SetSelectedSupplier(Supplier selectedSupplier)
        {
            this.selectedSupplier = selectedSupplier;
            ViewState["SelectedSupplier"] = selectedSupplier;
        }


        /// <summary>
        /// returns is selected matching exists
        /// </summary>
        public bool SelectedMatchingExist
        {
            get { return (ViewState["SelectedBrandMatching"] != null); }
        }


        /// <summary>
        /// returns selected object
        /// </summary>
        /// <returns>selected matching</returns>
        public BrandMatching GetSelectedMatching()
        {
            return (BrandMatching)ViewState["SelectedBrandMatching"];
        }


        /// <summary>
        /// returns if there is a selected suppliers
        /// </summary>
        public bool SelectedSupplierExist
        {
            get { return (ViewState["SelectedSupplier"] != null); }
        }


        /// <summary>
        /// returns selected object
        /// </summary>
        /// <returns>selected supplier</returns>
        public Supplier GetSelectedSupplier()
        {
            return (Supplier)ViewState["SelectedSupplier"];
        }
    }
}