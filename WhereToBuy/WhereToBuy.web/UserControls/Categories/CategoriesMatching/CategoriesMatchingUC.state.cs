using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WhereToBuy.entities;

namespace WhereToBuy.web.UserControls.Categories.CategoriesMatching
{
    public partial class CategoriesMatchingUC
    {
        WhereToBuy.entities.CategoryMatching selectedMatching;
        Supplier selectedSupplier;


        /// <summary>
        /// set selected matching
        /// </summary>
        /// <param name="selectedMatching">matching</param>
        void SetSelectedMatching(WhereToBuy.entities.CategoryMatching selectedMatching)
        {
            this.selectedMatching = selectedMatching;
            ViewState["SelectedCategoryMatching"] = selectedMatching;

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
            get { return (ViewState["SelectedCategoryMatching"] != null); }
        }


        /// <summary>
        /// returns selected object
        /// </summary>
        /// <returns>selected matching</returns>
        public WhereToBuy.entities.CategoryMatching GetSelectedMatching()
        {
            return (WhereToBuy.entities.CategoryMatching)ViewState["SelectedCategoryMatching"];
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