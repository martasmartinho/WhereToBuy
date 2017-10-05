using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WhereToBuy.entities;

namespace WhereToBuy.web.UserControls.States.StateMatching
{
    public partial class StateMatchingUC
    {
        WhereToBuy.entities.StateMatching selectedMatching;
        WhereToBuy.entities.State selectedState;
        Supplier selectedSupplier;

        /// <summary>
        /// set selected matching
        /// </summary>
        /// <param name="selectedMatching">matching</param>
        void SetSelectedMatching(WhereToBuy.entities.StateMatching selectedMatching)
        {
            this.selectedMatching = selectedMatching;
            ViewState["SelectedStateMatching"] = selectedMatching;

        }

        /// <summary>
        ///Set selectedState
        /// </summary>
        /// <param name="selectedState">state</param>
        void SetSelectedState(WhereToBuy.entities.State selectedState)
        {
            this.selectedState = selectedState;
            ViewState["SelectedState"] = selectedState;
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
            get { return (ViewState["SelectedStateMatching"] != null); }
        }


        /// <summary>
        /// returns selected object
        /// </summary>
        /// <returns>selected matching</returns>
        public WhereToBuy.entities.StateMatching GetSelectedMatching()
        {
            return (WhereToBuy.entities.StateMatching)ViewState["SelectedStateMatching"];
        }



        /// <summary>
        /// returns if there is a selected state
        /// </summary>
        public bool SelectedStateExist
        {
            get { return (ViewState["SelectedState"] != null); }
        }


        /// <summary>
        /// returns selected object
        /// </summary>
        /// <returns>selected state</returns>
        public WhereToBuy.entities.State GetSelectedState()
        {
            return (WhereToBuy.entities.State)ViewState["SelectedState"];
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