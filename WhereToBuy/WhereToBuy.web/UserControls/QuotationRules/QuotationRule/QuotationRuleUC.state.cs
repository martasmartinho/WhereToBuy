using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhereToBuy.web.UserControls.QuotationRules.QuotationRule
{
    public partial class QuotationRuleUC
    {
        WhereToBuy.entities.QuotationRule quotationRule;
        WhereToBuy.entities.Supplier supplier;
        WhereToBuy.entities.Brand brand;
        WhereToBuy.entities.Category category;
        WhereToBuy.entities.Stock stock;
        WhereToBuy.entities.Stock substituteStock;

        /// <summary>
        /// set selected object
        /// </summary>
        /// <param name="quotationRule">object</param>
        void SetSelectedQuotationRule(WhereToBuy.entities.QuotationRule quotationRule)
        {
            this.quotationRule = quotationRule;
            ViewState["SelectedQuotationRule"] = quotationRule;

        }

        /// <summary>
        /// set selected object
        /// </summary>
        /// <param name="supplier">object</param>
        void SetSupplier(WhereToBuy.entities.Supplier supplier)
        {
            this.supplier = supplier;
            ViewState["SelectedSupplier"] = supplier;

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
        /// <param name="substituteStock">object</param>
        void SetSubstituteStock(WhereToBuy.entities.Stock substituteStock)
        {
            this.substituteStock = substituteStock;
            ViewState["SelectedSubstituteStock"] = substituteStock;

        }

        /// <summary>
        /// returns is selected quotationRule exists
        /// </summary>
        public bool SelectedQuotationRuleExist
        {
            get { return (ViewState["SelectedQuotationRule"] != null); }
        }

        /// <summary>
        /// returns is selected supplier exists
        /// </summary>
        public bool SelectedSupplierExist
        {
            get { return (ViewState["SelectedSupplier"] != null); }
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
        /// returns is selected substituteStock exists
        /// </summary>
        public bool SelectedSubstituteStockExist
        {
            get { return (ViewState["SelectedSubstituteStock"] != null); }
        }



        /// <summary>
        /// returns selected object
        /// </summary>
        /// <returns>selected quotationRule</returns>
        public WhereToBuy.entities.QuotationRule GetSelectedQuotationRule()
        {
            return (WhereToBuy.entities.QuotationRule)ViewState["SelectedQuotationRule"];
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