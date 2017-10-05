using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WhereToBuy.entities;

namespace WhereToBuy.web.UserControls.Stocks.StockMatching
{
    public partial class StockMatchingUC
    {
        WhereToBuy.entities.StockMatching selectedMatching;
        WhereToBuy.entities.Stock selectedStock;
        Supplier selectedSupplier;

        /// <summary>
        /// set selected matching
        /// </summary>
        /// <param name="selectedMatching">matching</param>
        void SetSelectedMatching(WhereToBuy.entities.StockMatching selectedMatching)
        {
            this.selectedMatching = selectedMatching;
            ViewState["SelectedStockMatching"] = selectedMatching;

        }

        /// <summary>
        ///Set selectedStock
        /// </summary>
        /// <param name="selectedStock">state</param>
        void SetSelectedStock(WhereToBuy.entities.Stock selectedStock)
        {
            this.selectedStock = selectedStock;
            ViewState["SelectedStock"] = selectedStock;
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
            get { return (ViewState["SelectedStockMatching"] != null); }
        }


        /// <summary>
        /// returns selected object
        /// </summary>
        /// <returns>selected matching</returns>
        public WhereToBuy.entities.StockMatching GetSelectedMatching()
        {
            return (WhereToBuy.entities.StockMatching)ViewState["SelectedStockMatching"];
        }



        /// <summary>
        /// returns if there is a selected state
        /// </summary>
        public bool SelectedStockExist
        {
            get { return (ViewState["SelectedStock"] != null); }
        }


        /// <summary>
        /// returns selected object
        /// </summary>
        /// <returns>selected state</returns>
        public WhereToBuy.entities.Stock GetSelectedStock()
        {
            return (WhereToBuy.entities.Stock)ViewState["SelectedStock"];
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