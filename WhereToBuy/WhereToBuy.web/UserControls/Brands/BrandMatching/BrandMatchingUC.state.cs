using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WhereToBuy.entities;

namespace WhereToBuy.web.UserControls.Brands.BrandMatching
{
    public partial class BrandMatchingUC
    {
        WhereToBuy.entities.BrandMatching selectedMatching;
        WhereToBuy.entities.Brand selectedBrand;
        Supplier selectedSupplier;


        /// <summary>
        /// set selected matching
        /// </summary>
        /// <param name="selectedMatching">matching</param>
        void SetSelectedMatching(WhereToBuy.entities.BrandMatching selectedMatching)
        {
            this.selectedMatching = selectedMatching;
            ViewState["SelectedBrandMatching"] = selectedMatching;

        }


        /// <summary>
        ///Set selectedBrand
        /// </summary>
        /// <param name="selectedBrand">product</param>
        void SetSelectedBrand(WhereToBuy.entities.Brand selectedBrand)
        {
            this.selectedBrand = selectedBrand;
            ViewState["SelectedBrand"] = selectedBrand;
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
            get { return (ViewState["SelectedBrandMatching"] != null); }
        }


        /// <summary>
        /// returns selected object
        /// </summary>
        /// <returns>selected matching</returns>
        public WhereToBuy.entities.BrandMatching GetSelectedMatching()
        {
            return (WhereToBuy.entities.BrandMatching)ViewState["SelectedBrandMatching"];
        }
        

        /// <summary>
        /// returns if there is a selected product
        /// </summary>
        public bool SelectedBrandExist
        {
            get { return (ViewState["SelectedBrand"] != null); }
        }


        /// <summary>
        /// returns selected object
        /// </summary>
        /// <returns>selected brand</returns>
        public WhereToBuy.entities.Brand GetSelectedBrand()
        {
            return (WhereToBuy.entities.Brand)ViewState["SelectedBrand"];
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