using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhereToBuy.web.UserControls.QuotationRules.QuotationRules
{
    public partial class QuotationRulesUC
    {
        List<WhereToBuy.entities.QuotationRule> quotationRules;
        WhereToBuy.entities.QuotationRule selectedQuotationRule;
        WhereToBuy.entities.Supplier selectedSupplier;
        WhereToBuy.entities.Category selectedCategory;
        WhereToBuy.entities.Brand selectedBrand;
        WhereToBuy.entities.Stock selectedStock;


        // <summary>
        /// set selected object
        /// </summary>
        /// <param name="selectedQuotationRule">object</param>
        void SetQuotationRules(List<WhereToBuy.entities.QuotationRule> quotationRules)
        {
            this.quotationRules = quotationRules;
            ViewState["QuotationRules"] = quotationRules;

        }

        /// <summary>
        /// set selected object
        /// </summary>
        /// <param name="selectedQuotationRule">object</param>
        void SetSelectedQuotationRule(WhereToBuy.entities.QuotationRule selectedQuotationRule)
        {
            this.selectedQuotationRule = selectedQuotationRule;
            ViewState["SelectedQuotationRule"] = selectedQuotationRule;

        }

        /// <summary>
        /// set selected object
        /// </summary>
        /// <param name="selectedSupplier">object</param>
        void SetSelectedSupplier(WhereToBuy.entities.Supplier selectedSupplier)
        {
            this.selectedSupplier = selectedSupplier;
            ViewState["SelectedSupplier"] = selectedSupplier;

        }


        /// <summary>
        /// set selected object
        /// </summary>
        /// <param name="selectedCategory">object</param>
        void SetSelectedCategory(WhereToBuy.entities.Category selectedCategory)
        {
            this.selectedCategory = selectedCategory;
            ViewState["SelectedCategory"] = selectedCategory;

        }


        /// <summary>
        /// set selected object
        /// </summary>
        /// <param name="selectedBrand">object</param>
        void SetSelectedBrand(WhereToBuy.entities.Brand selectedBrand)
        {
            this.selectedBrand = selectedBrand;
            ViewState["SelectedBrand"] = selectedBrand;

        }


        /// <summary>
        /// set selected object
        /// </summary>
        /// <param name="selectedStock">object</param>
        void SetSelectedStock(WhereToBuy.entities.Stock selectedStock)
        {
            this.selectedStock = selectedStock;
            ViewState["SelectedStock"] = selectedStock;

        }



        /// <summary>
        /// returns is selected object exists
        /// </summary>
        public bool SelectedQuotationRuleExist
        {
            get { return (ViewState["SelectedQuotationRule"] != null); }
        }

        /// <summary>
        /// returns is selected object exists
        /// </summary>
        public bool SelectedSupplierExist
        {
            get { return (ViewState["SelectedSupplier"] != null); }
        }

        /// <summary>
        /// returns is selected object exists
        /// </summary>
        public bool SelectedCategoryExist
        {
            get { return (ViewState["SelectedCategory"] != null); }
        }


        /// <summary>
        /// returns is selected object exists
        /// <returns>selected object</returns>
        public bool SelectedBrandExist
        {
            get { return (ViewState["SelectedBrand"] != null); }
        }

        /// <summary>
        /// returns is selected object exists
        /// </summary>
        public bool SelectedStockExist
        {
            get { return (ViewState["SelectedStock"] != null); }
        }


        /// <summary>
        /// returns selected object
        /// </summary>
        /// <returns>selected object</returns>
        public List<WhereToBuy.entities.QuotationRule> GetQuotationRules()
        {
            return (List<WhereToBuy.entities.QuotationRule>)ViewState["QuotationRules"];
        }

        /// <summary>
        /// returns selected object
        /// </summary>
        /// <returns>selected object</returns>
        public WhereToBuy.entities.QuotationRule GetSelectedQuotationRule()
        {
            return (WhereToBuy.entities.QuotationRule)ViewState["SelectedQuotationRule"];
        }

        /// <summary>
        /// returns selected object
        /// </summary>
        /// <returns>selected object</returns>
        public WhereToBuy.entities.Supplier GetSelectedSupplier()
        {
            return (WhereToBuy.entities.Supplier)ViewState["SelectedSupplier"];
        }

        /// <summary>
        /// returns selected object
        /// </summary>
        /// <returns>selected object</returns>
        public WhereToBuy.entities.Category GetSelectedCategory()
        {
            return (WhereToBuy.entities.Category)ViewState["SelectedCategory"];
        }

        /// <summary>
        /// returns selected object
        /// </summary>
        /// <returns>selected object</returns>
        public WhereToBuy.entities.Brand GetSelectedBrand()
        {
            return (WhereToBuy.entities.Brand)ViewState["SelectedBrand"];
        }

        /// <summary>
        /// returns selected object
        /// </summary>
        /// <returns>selected object</returns>
        public WhereToBuy.entities.Stock GetSelectedStock()
        {
            return (WhereToBuy.entities.Stock)ViewState["SelectedStock"];
        }
    }
}