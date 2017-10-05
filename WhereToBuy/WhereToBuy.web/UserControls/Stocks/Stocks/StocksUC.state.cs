using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhereToBuy.web.UserControls.Stocks.Stocks
{
    public partial class StocksUC
    {
        WhereToBuy.entities.Stock selectedStock;


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
        public bool SelectedStockExist
        {
            get { return (ViewState["SelectedStock"] != null); }
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