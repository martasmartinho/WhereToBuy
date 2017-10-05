using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WhereToBuy.web.UserControls.Products.ProductsSelBox
{
    public partial class ProductsSelBox : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }

        protected void lkBtnSearch_Click(object sender, EventArgs e)
        {

            txtProduct.Focus();
            SubmitButtonClick(lkBtnSearch, new ProductSelBoxEventArgs(null, ""));
            RefreshListView();
        }




        protected void lkBtnItem_Click(object sender, EventArgs e)
        {
            WhereToBuy.entities.Product warningType;

            lvProducts.SelectedIndex = Convert.ToInt32((((LinkButton)sender).CommandArgument));
            warningType = LoadProduct(((LinkButton)sender).Text.Split('-')[0].TrimStart().TrimEnd());
            txtProduct.Text = warningType.ToString();

            lvProducts.Items.Clear();
            lvProducts.DataBind();
            SelectedProductUpdate(this, new ProductSelBoxEventArgs(warningType, ""));
        }
        
    }
}