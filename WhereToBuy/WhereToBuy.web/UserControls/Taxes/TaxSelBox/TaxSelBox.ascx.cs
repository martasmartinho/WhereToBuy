using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WhereToBuy.web.UserControls.Taxes.TaxSelBox
{
    public partial class TaxSelBox : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void lkBtnSearch_Click(object sender, EventArgs e)
        {

            txtTax.Focus();
            SubmitButtonClick(lkBtnSearch, new TaxesSelBoxEventArgs(null, ""));
            RefreshListView();
        }




        protected void lkBtnItem_Click(object sender, EventArgs e)
        {
            WhereToBuy.entities.Tax tax;

            lvTaxes.SelectedIndex = Convert.ToInt32((((LinkButton)sender).CommandArgument));
            tax = LoadTax(((LinkButton)sender).Text.Split('-')[0].TrimStart().TrimEnd());
            txtTax.Text = tax.ToString();

            lvTaxes.Items.Clear();
            lvTaxes.DataBind();
            SelectedTaxUpdate(this, new TaxesSelBoxEventArgs(tax, ""));
        }
    }
}