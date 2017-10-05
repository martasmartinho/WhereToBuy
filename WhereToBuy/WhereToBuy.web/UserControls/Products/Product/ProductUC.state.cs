using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WhereToBuy.entities;

namespace WhereToBuy.web.UserControls.Products.Product
{
    public partial class ProductUC
    {
        WhereToBuy.entities.Product product;
        WhereToBuy.entities.Supplier supplier;
        WhereToBuy.entities.Tax tax;
        WhereToBuy.entities.Brand brand;
        WhereToBuy.entities.Category category;
        WhereToBuy.entities.Stock stock;
        WhereToBuy.entities.Stock stock_U1;
        WhereToBuy.entities.Stock stock_U2;
        WhereToBuy.entities.Stock stock_U3;
        WhereToBuy.entities.ProductDetail productDetail;

        /// <summary>
        /// set selected object
        /// </summary>
        /// <param name="product">object</param>
        void SetSelectedProduct(WhereToBuy.entities.Product product)
        {
            this.product = product;
            ViewState["SelectedProduct"] = product;

        }


        /// <summary>
        /// set selected object
        /// </summary>
        /// <param name="product">object</param>
        void SetSelectedProductDetail(WhereToBuy.entities.ProductDetail productDetail)
        {
            this.productDetail = productDetail;
            ViewState["SelectedProductDetail"] = productDetail;

        }



        /// <summary>
        /// set selected object
        /// </summary>
        /// <param name="product">object</param>
        void SetSupplier(WhereToBuy.entities.Supplier supplier)
        {
            this.supplier = supplier;
            ViewState["SelectedSupplier"] = supplier;

        }


        /// <summary>
        /// set selected object
        /// </summary>
        /// <param name="tax">object</param>
        void SetTax(WhereToBuy.entities.Tax tax)
        {
            this.tax = tax;
            ViewState["SelectedTax"] = tax;

        }



        /// <summary>
        /// set selected object
        /// </summary>
        /// <param name="brand">object</param>
        void SetBrand(WhereToBuy.entities.Brand brand)
        {
            this.brand = brand;
            ViewState["SelectedBrand"] = brand;

        }

        /// <summary>
        /// set selected object
        /// </summary>
        /// <param name="category">object</param>
        void SetCategory(WhereToBuy.entities.Category category)
        {
            this.category = category;
            ViewState["SelectedCategory"] = category;

        }

        /// <summary>
        /// set selected object
        /// </summary>
        /// <param name="stock">object</param>
        void SetStock(WhereToBuy.entities.Stock stock)
        {
            this.stock = stock;
            ViewState["SelectedStock"] = stock;

        }


        /// <summary>
        /// set selected object
        /// </summary>
        /// <param name="stock">object</param>
        void SetStock_U1(WhereToBuy.entities.Stock stock_U1)
        {
            this.stock_U1 = stock_U1;
            ViewState["SelectedStock_U1"] = stock_U1;
        }

        /// <summary>
        /// set selected object
        /// </summary>
        /// <param name="stock">object</param>
        void SetStock_U2(WhereToBuy.entities.Stock stock_U2)
        {
            this.stock_U2 = stock_U2;
            ViewState["SelectedStock_U2"] = stock_U2;

        }

        /// <summary>
        /// set selected object
        /// </summary>
        /// <param name="substituteStock">object</param>
        void SetStock_U3(WhereToBuy.entities.Stock stock_U3)
        {
            this.stock_U3 = stock_U3;
            ViewState["SelectedStock_U3"] = stock_U3;

        }

        /// <summary>
        /// returns is selected product exists
        /// </summary>
        public bool SelectedProductExist
        {
            get { return (ViewState["SelectedProduct"] != null); }
        }

        /// <summary>
        /// returns is selected product detail exists
        /// </summary>
        public bool SelectedProductDetailExist
        {
            get { return (ViewState["SelectedProductDetail"] != null); }
        }


        /// <summary>
        /// returns is selected supplier exists
        /// </summary>
        public bool SelectedSupplierExist
        {
            get { return (ViewState["SelectedSupplier"] != null); }
        }


        /// <summary>
        /// returns is selected tax exists
        /// </summary>
        public bool SelectedTaxExist
        {
            get { return (ViewState["SelectedTax"] != null); }
        }

        /// <summary>
        /// returns is selected brand exists
        /// </summary>
        public bool SelectedBrand
        {
            get { return (ViewState["SelectedBrand"] != null); }
        }

        /// <summary>
        /// returns is selected category exists
        /// </summary>
        public bool SelectedCategoryExist
        {
            get { return (ViewState["SelectedCategory"] != null); }
        }


        /// <summary>
        /// returns is selected stock exists
        /// </summary>
        public bool SelectedStockExist
        {
            get { return (ViewState["SelectedStock"] != null); }
        }

        /// <summary>
        /// returns is selected stock exists
        /// </summary>
        public bool SelectedStock_U1_Exist
        {
            get { return (ViewState["SelectedStock_U1"] != null); }
        }


        /// <summary>
        /// returns is selected stock exists
        /// </summary>
        public bool SelectedStock_U2_Exist
        {
            get { return (ViewState["SelectedStock_U2"] != null); }
        }


        /// <summary>
        /// returns is selected stock exists
        /// </summary>
        public bool SelectedStock_U3_Exist
        {
            get { return (ViewState["SelectedStock_U3"] != null); }
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
        /// returns selected object
        /// </summary>
        /// <returns>selected product detail</returns>
        public WhereToBuy.entities.ProductDetail GetSelectedProductDetail()
        {
            return (WhereToBuy.entities.ProductDetail)ViewState["SelectedProductDetail"];
        }


        /// <summary>
        /// returns selected object
        /// </summary>
        /// <returns>selected supplier</returns>
        public WhereToBuy.entities.Supplier GetSelectedSupplier()
        {
            return (WhereToBuy.entities.Supplier)ViewState["SelectedSupplier"];
        }



        /// <summary>
        /// returns selected object
        /// </summary>
        /// <returns>selected tax</returns>
        public WhereToBuy.entities.Tax GetSelectedTax()
        {
            return (WhereToBuy.entities.Tax)ViewState["SelectedTax"];
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
        /// returns selected object
        /// </summary>
        /// <returns>selected category</returns>
        public WhereToBuy.entities.Category GetSelectedCategory()
        {
            return (WhereToBuy.entities.Category)ViewState["SelectedCategory"];
        }



        /// <summary>
        /// returns selected object
        /// </summary>
        /// <returns>selected stock</returns>
        public WhereToBuy.entities.Stock GetSelectedStock()
        {
            return (WhereToBuy.entities.Stock)ViewState["SelectedStock"];
        }



        /// <summary>
        /// returns selected object
        /// </summary>
        /// <returns>selected substitute stock</returns>
        public WhereToBuy.entities.Stock GetSelectedSubstituteStock()
        {
            return (WhereToBuy.entities.Stock)ViewState["SelectedSubstituteStock"];
        }
    }
}