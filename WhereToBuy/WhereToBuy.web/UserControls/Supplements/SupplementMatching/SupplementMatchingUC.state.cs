using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WhereToBuy.entities;

namespace WhereToBuy.web.UserControls.Supplements.SupplementMatching
{
    public partial class SupplementMatchingUC
    {
        WhereToBuy.entities.SupplementMatching selectedMatching;
        WhereToBuy.entities.Supplement selectedSupplement;
        Supplier selectedSupplier;

        /// <summary>
        /// set selected matching
        /// </summary>
        /// <param name="selectedMatching">matching</param>
        void SetSelectedMatching(WhereToBuy.entities.SupplementMatching selectedMatching)
        {
            this.selectedMatching = selectedMatching;
            ViewState["SelectedSupplementMatching"] = selectedMatching;

        }

        /// <summary>
        ///Set selectedSupplement
        /// </summary>
        /// <param name="selectedSupplement">state</param>
        void SetSelectedSupplement(WhereToBuy.entities.Supplement selectedSupplement)
        {
            this.selectedSupplement = selectedSupplement;
            ViewState["SelectedSupplement"] = selectedSupplement;
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
            get { return (ViewState["SelectedSupplementMatching"] != null); }
        }


        /// <summary>
        /// returns selected object
        /// </summary>
        /// <returns>selected matching</returns>
        public WhereToBuy.entities.SupplementMatching GetSelectedMatching()
        {
            return (WhereToBuy.entities.SupplementMatching)ViewState["SelectedSupplementMatching"];
        }



        /// <summary>
        /// returns if there is a selected state
        /// </summary>
        public bool SelectedSupplementExist
        {
            get { return (ViewState["SelectedSupplement"] != null); }
        }


        /// <summary>
        /// returns selected object
        /// </summary>
        /// <returns>selected state</returns>
        public WhereToBuy.entities.Supplement GetSelectedSupplement()
        {
            return (WhereToBuy.entities.Supplement)ViewState["SelectedSupplement"];
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