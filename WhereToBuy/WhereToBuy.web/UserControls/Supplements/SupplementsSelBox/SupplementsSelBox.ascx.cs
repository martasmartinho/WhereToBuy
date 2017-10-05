using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WhereToBuy.web.UserControls.Supplements.SuppementsSelBox
{
    public partial class SupplementsSelBox : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void lkBtnSearch_Click(object sender, EventArgs e)
        {

            txtSupplement.Focus();
            SubmitButtonClick(lkBtnSearch, new SupplementsSelBoxEventArgs(null, ""));
            RefreshListView();
        }




        protected void lkBtnItem_Click(object sender, EventArgs e)
        {
            WhereToBuy.entities.Supplement supplement;

            lvSupplements.SelectedIndex = Convert.ToInt32((((LinkButton)sender).CommandArgument));
            supplement = LoadSupplement(((LinkButton)sender).Text.Split('-')[0].TrimStart().TrimEnd());
            txtSupplement.Text = supplement.ToString();

            lvSupplements.Items.Clear();
            lvSupplements.DataBind();
            SelectedSupplementUpdate(this, new SupplementsSelBoxEventArgs(supplement, ""));
        }
    }
}