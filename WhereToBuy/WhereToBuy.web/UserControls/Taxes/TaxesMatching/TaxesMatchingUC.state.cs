﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WhereToBuy.entities;

namespace WhereToBuy.web.UserControls.Taxes.TaxesMatching
{
    public partial class TaxesMatchingUC
    {
        WhereToBuy.entities.TaxMatching selectedMatching;
        Supplier selectedSupplier;


        /// <summary>
        /// set selected matching
        /// </summary>
        /// <param name="selectedMatching">matching</param>
        void SetSelectedMatching(WhereToBuy.entities.TaxMatching selectedMatching)
        {
            this.selectedMatching = selectedMatching;
            ViewState["SelectedTaxMatching"] = selectedMatching;

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
            get { return (ViewState["SelectedTaxMatching"] != null); }
        }


        /// <summary>
        /// returns selected object
        /// </summary>
        /// <returns>selected matching</returns>
        public WhereToBuy.entities.TaxMatching GetSelectedMatching()
        {
            return (WhereToBuy.entities.TaxMatching)ViewState["SelectedTaxMatching"];
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