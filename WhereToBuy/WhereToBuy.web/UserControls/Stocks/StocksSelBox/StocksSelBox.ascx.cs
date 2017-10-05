using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WhereToBuy.web.UserControls.Stocks.StocksSelBox
{
    public partial class StocksSelBox : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            
        }

        protected void lkBtnSearch_Click(object sender, EventArgs e)
        {

            txtStock.Focus();
            SubmitButtonClick(btnStockDummy, new StocksSelBoxEventArgs(null, ""));
            RefreshListView();
        }




        protected void lkBtnItem_Click(object sender, EventArgs e)
        {
            string code = string.Empty;
            WhereToBuy.entities.Stock stock;

            lvStocks.SelectedIndex = Convert.ToInt32((((LinkButton)sender).CommandArgument));
            code = ((LinkButton)sender).Text.Split(']')[0].TrimStart().TrimEnd().Remove(0,1);
            stock = LoadStock(code);
            txtStock.Text = stock.ToString();

            lvStocks.Items.Clear();
            lvStocks.DataBind();
            SelectedStockUpdate(this, new StocksSelBoxEventArgs(stock, ""));
        }

       
    }
}