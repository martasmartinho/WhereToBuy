using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WhereToBuy.entities;

namespace WhereToBuy.web.UserControls.Products.ProductMatching
{
    public partial class ProductMatchingUC
    {
        WhereToBuy.entities.ProductMatching selectedMatching;
        WhereToBuy.entities.Product selectedProduct;
        Supplier selectedSupplier;
        Stock selectedStock;


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
        ///Set selectedProduct
        /// </summary>
        /// <param name="selectedProduct">product</param>
        void SetSelectedProduct(WhereToBuy.entities.Product selectedProduct)
        {
            this.selectedProduct = selectedProduct;
            ViewState["SelectedProduct"] = selectedProduct;
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
        /// Set selected stock
        /// </summary>
        /// <param name="selectedSupplier"></param>
        void SetSelectedStock(Stock selectedStock)
        {
            this.selectedStock = selectedStock;
            ViewState["SelectedStock"] = selectedStock;

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
        /// returns if there is a selected product
        /// </summary>
        public bool SelectedProductExist
        {
            get { return (ViewState["SelectedProduct"] != null); }
        }


        /// <summary>
        /// returns selected object
        /// </summary>
        /// <returns>selected product</returns>
        public WhereToBuy.entities.Product GetSelectedProduct()
        {
            return (WhereToBuy.entities.Product)ViewState["SelectedProduct"];
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


        /// <summary>
        /// returns if there is a selected Stock
        /// </summary>
        public bool SelectedStockExist
        {
            get { return (ViewState["SelectedStock"] != null); }
        }


        /// <summary>
        /// returns selected object
        /// </summary>
        /// <returns>selected Stock</returns>
        public Stock GetSelectedStock()
        {
            return (Stock)ViewState["SelectedStock"];
        }
    }
}