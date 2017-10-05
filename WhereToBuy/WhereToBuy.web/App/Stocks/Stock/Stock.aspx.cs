using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WhereToBuy.web.UserControls;
using WhereToBuy.web.UserControls.Stocks.StocksSelBox;

namespace WhereToBuy.web.App.Stocks.Stock
{
    public partial class Stock : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ((MessageUC)(StockUC.FindControl("MessageUC"))).SubmitButtonClick += Stock_MessageButton;

            ((StocksSelBox)(StockUC.FindControl("StocksP50SelBox"))).SubmitButtonClick += Stock_StockPClickButton;
            ((StocksSelBox)(StockUC.FindControl("StocksP60SelBox"))).SubmitButtonClick += Stock_StockPClickButton;
            ((StocksSelBox)(StockUC.FindControl("StocksP70SelBox"))).SubmitButtonClick += Stock_StockPClickButton;
            ((StocksSelBox)(StockUC.FindControl("StocksP80SelBox"))).SubmitButtonClick += Stock_StockPClickButton;
            ((StocksSelBox)(StockUC.FindControl("StocksP90SelBox"))).SubmitButtonClick += Stock_StockPClickButton;

        }

        private void Stock_StockPClickButton(object sender, StocksSelBoxEventArgs e)
        {
           
            string btn = ((Button)sender).ClientID;

            ScriptManager.RegisterStartupScript(this, this.GetType(), "bdc", "showDropdown('" + btn + "');", true);

          
        }

      

        private void Stock_MessageButton(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ic", "invokeButtonClick();", true);

        }
    }
}