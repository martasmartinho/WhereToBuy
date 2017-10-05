using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WhereToBuy.entities;


namespace WhereToBuy.web.UserControls.Products.ProductsMatching
{
    public partial class ProductsMatchingUC
    {
        WhereToBuy.entities.ProductMatching selectedMatching;
        Supplier selectedSupplier;
        string selectedSupplement;

        /// <summary>
        /// set selected matching
        /// </summary>
        /// <param name="selectedMatching">matching</param>
        void SetSelectedMatching(WhereToBuy.entities.ProductMatching selectedMatching)
        {
            this.selectedMatching = selectedMatching;
            ViewState["SelectedProductMatching"] = selectedMatching;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="selectedSupplier"></param>
        void SetSelectedSupplier(Supplier selectedSupplier)
        {
            this.selectedSupplier = selectedSupplier;
            ViewState["SelectedSupplier"] = selectedSupplier;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="selectedSupplement"></param>
        void SetSelectedSupplement(string selectedSupplement)
        {
            this.selectedSupplement = selectedSupplement;
            ViewState["SelectedSupplement"] = selectedSupplement;
        }


        /// <summary>
        /// returns is selected matching exists
        /// </summary>
        public bool SelectedMatchingExist
        {
            get { return (ViewState["SelectedProductMatching"] != null); }
        }


        /// <summary>
        /// returns selected object
        /// </summary>
        /// <returns>selected matching</returns>
        public WhereToBuy.entities.ProductMatching GetSelectedMatching()
        {
            return (WhereToBuy.entities.ProductMatching)ViewState["SelectedProductMatching"];
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

        /// <summary>
        /// returns if there is a selected supplement
        /// </summary>
        public bool SelectedSupplementExist
        {
            get { return (ViewState["SelectedSupplement"] != null); }
        }


        /// <summary>
        /// returns selected object
        /// </summary>
        /// <returns>selected supplement</returns>
        public string GetSelectedSupplement()
        {
            return (string)ViewState["SelectedSupplement"];
        }
    }
}