using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WhereToBuy.entities;

namespace WhereToBuy.web.UserControls.Categories.CategoryMatching
{
    public partial class CategoryMatchingUC
    {
        WhereToBuy.entities.CategoryMatching selectedMatching;
        WhereToBuy.entities.Category selectedCategory;
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

        /// <summary>
        ///Set selectedCategory
        /// </summary>
        /// <param name="selectedCategory">state</param>
        void SetSelectedCategory(WhereToBuy.entities.Category selectedCategory)
        {
            this.selectedCategory = selectedCategory;
            ViewState["SelectedCategory"] = selectedCategory;
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
        /// returns if there is a selected state
        /// </summary>
        public bool SelectedCategoryExist
        {
            get { return (ViewState["SelectedCategory"] != null); }
        }


        /// <summary>
        /// returns selected object
        /// </summary>
        /// <returns>selected state</returns>
        public WhereToBuy.entities.Category GetSelectedCategory()
        {
            return (WhereToBuy.entities.Category)ViewState["SelectedCategory"];
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