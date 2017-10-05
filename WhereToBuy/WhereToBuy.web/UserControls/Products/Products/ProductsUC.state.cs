using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WhereToBuy.entities;

namespace WhereToBuy.web.UserControls.Products.Products
{
    public partial class ProductsUC
    {
        WhereToBuy.entities.Product selectedProduct;
        Supplier selectedSupplier;
        Brand selectedBrand;
        Category selectedCategory;

        /// <summary>
        /// set selected matching
        /// </summary>
        /// <param name="selectedProduct">matching</param>
        void SetSelectedProduct(WhereToBuy.entities.Product selectedProduct)
        {
            this.selectedProduct = selectedProduct;
            ViewState["SelectedProduct"] = selectedProduct;
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
        void SetSelectedBrand(Brand selectedBrand)
        {
            this.selectedBrand = selectedBrand;
            ViewState["SelectedBrand"] = selectedBrand;
        }

        void SetSelectedCategory(Category selectedCategory)
        {
            this.selectedCategory = selectedCategory;
            ViewState["SelectedCategory"] = selectedCategory;
        }


        /// <summary>
        /// returns is selected product exists
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
        /// returns if there is a selected brand
        /// </summary>
        public bool SelectedBrandExist
        {
            get { return (ViewState["SelectedBrand"] != null); }
        }


        /// <summary>
        /// returns selected object
        /// </summary>
        /// <returns>selected category</returns>
        public Brand GetSelectedBrand()
        {
            return (Brand)ViewState["SelectedBrand"];
        }

        /// <summary>
        /// returns if there is a selected category
        /// </summary>
        public bool SelectedCategoryExist
        {
            get { return (ViewState["SelectedCategory"] != null); }
        }


        /// <summary>
        /// returns selected object
        /// </summary>
        /// <returns>selected category</returns>
        public Category GetSelectedCategory()
        {
            return (Category)ViewState["SelectedCategory"];
        }
    }
}