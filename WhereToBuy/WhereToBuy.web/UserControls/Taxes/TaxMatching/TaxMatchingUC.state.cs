using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WhereToBuy.entities;

namespace WhereToBuy.web.UserControls.Taxes.TaxMatching
{
    public partial class TaxMatchingUC
    {
        WhereToBuy.entities.TaxMatching selectedMatching;
        WhereToBuy.entities.Tax selectedTax;
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

        /// <summary>
        ///Set selectedTax
        /// </summary>
        /// <param name="selectedTax">state</param>
        void SetSelectedTax(WhereToBuy.entities.Tax selectedTax)
        {
            this.selectedTax = selectedTax;
            ViewState["SelectedTax"] = selectedTax;
            //lblMarcaSelecionada.Text = string.Format("[{0}] {1}", this.selectedMatching.Codigo.TrimEnd(), this.selectedMatching.Descricao.TrimEnd());

            //dispara um evento a anunciar a nova seleção
            //OnMarcasPesquisaSelecao(new MarcasPesquisaEventArgs(this.selectedMatching));
        }

        /// <summary>
        /// Set selected supplier
        /// </summary>
        /// <param name="selectedSupplier"></param>
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
        /// returns if there is a selected state
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


        /// <summary>
        /// returns if there is a selected supplier
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