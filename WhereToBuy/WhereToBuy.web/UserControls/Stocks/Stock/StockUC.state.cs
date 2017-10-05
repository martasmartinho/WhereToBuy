using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhereToBuy.web.UserControls.Stocks.Stock
{
    public partial class StockUC
    {
        WhereToBuy.entities.Stock stock;
        WhereToBuy.entities.Stock stockP50;
        WhereToBuy.entities.Stock stockP60;
        WhereToBuy.entities.Stock stockP70;
        WhereToBuy.entities.Stock stockP80;
        WhereToBuy.entities.Stock stockP90;

        /// <summary>
        /// set selected object
        /// </summary>
        /// <param name="stock">object</param>
        void SetSelectedStock(WhereToBuy.entities.Stock stock)
        {
            this.stock = stock;
            ViewState["SelectedStock"] = stock;

        }

        /// <summary>
        /// set selected object
        /// </summary>
        /// <param name="stockP50">object</param>
        void SetStockP50(WhereToBuy.entities.Stock stockP50)
        {
            this.stockP50 = stockP50;
            ViewState["SelectedStockP50"] = stockP50;

        }

       

        /// <summary>
        /// set selected object
        /// </summary>
        /// <param name="stockP60">object</param>
        void SetStockP60(WhereToBuy.entities.Stock stockP60)
        {
            this.stockP60 = stockP60;
            ViewState["SelectedStockP60"] = stockP60;

        }

        /// <summary>
        /// set selected object
        /// </summary>
        /// <param name="stockP70">object</param>
        void SetStockP70(WhereToBuy.entities.Stock stockP70)
        {
            this.stockP70 = stockP70;
            ViewState["SelectedStockP70"] = stockP70;

        }

        /// <summary>
        /// set selected object
        /// </summary>
        /// <param name="stockP80">object</param>
        void SetStockP80(WhereToBuy.entities.Stock stockP80)
        {
            this.stockP80 = stockP80;
            ViewState["SelectedStockP80"] = stockP80;

        }

        /// <summary>
        /// set selected object
        /// </summary>
        /// <param name="stockP90">object</param>
        void SetStockP90(WhereToBuy.entities.Stock stockP90)
        {
            this.stockP90 = stockP90;
            ViewState["SelectedStockP90"] = stockP90;

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
        public bool SelectedStockP50Exist
        {
            get { return (ViewState["SelectedStockP50"] != null); }
        }

        /// <summary>
        /// returns is selected stock exists
        /// </summary>
        public bool SelectedStockP60Exist
        {
            get { return (ViewState["SelectedStockP60"] != null); }
        }

        /// <summary>
        /// returns is selected stock exists
        /// </summary>
        public bool SelectedStockP70Exist
        {
            get { return (ViewState["SelectedStockP70"] != null); }
        }


        /// <summary>
        /// returns is selected stock exists
        /// </summary>
        public bool SelectedStockP80Exist
        {
            get { return (ViewState["SelectedStockP80"] != null); }
        }



        /// <summary>
        /// returns is selected stock exists
        /// </summary>
        public bool SelectedStockP90Exist
        {
            get { return (ViewState["SelectedStockP90"] != null); }
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
        /// <returns>selected stock</returns>
        public WhereToBuy.entities.Stock GetSelectedStockP50()
        {
            return (WhereToBuy.entities.Stock)ViewState["SelectedStockP50"];
        }


        /// <summary>
        /// returns selected object
        /// </summary>
        /// <returns>selected stock</returns>
        public WhereToBuy.entities.Stock GetSelectedStockP60()
        {
            return (WhereToBuy.entities.Stock)ViewState["SelectedStockP60"];
        }



        /// <summary>
        /// returns selected object
        /// </summary>
        /// <returns>selected stock</returns>
        public WhereToBuy.entities.Stock GetSelectedStockP70()
        {
            return (WhereToBuy.entities.Stock)ViewState["SelectedStockP70"];
        }



        /// <summary>
        /// returns selected object
        /// </summary>
        /// <returns>selected stock</returns>
        public WhereToBuy.entities.Stock GetSelectedStockP80()
        {
            return (WhereToBuy.entities.Stock)ViewState["SelectedStockP80"];
        }



        /// <summary>
        /// returns selected object
        /// </summary>
        /// <returns>selected stock</returns>
        public WhereToBuy.entities.Stock GetSelectedStockP90()
        {
            return (WhereToBuy.entities.Stock)ViewState["SelectedStockP90"];
        }


    }
}