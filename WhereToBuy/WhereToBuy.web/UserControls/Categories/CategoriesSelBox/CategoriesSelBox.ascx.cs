using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WhereToBuy.web.UserControls.Categories.CategoriesSelBox
{
    public partial class CategoriesSelBox : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void lkBtnSearch_Click(object sender, EventArgs e)
        {

            txtCategory.Focus();

            SubmitButtonClick(lkBtnSearch, new CategoriesSelBoxEventArgs(null, ""));

            RefreshListView();
        }




        protected void lkBtnItem_Click(object sender, EventArgs e)
        {
            WhereToBuy.entities.Category category;

            lvCategories.SelectedIndex = Convert.ToInt32((((LinkButton)sender).CommandArgument));
            category = LoadCategory(((LinkButton)sender).Text.Split('-')[0].TrimStart().TrimEnd());
            txtCategory.Text = category.ToString();

            lvCategories.Items.Clear();
            lvCategories.DataBind();
            SelectedCategoryUpdate(this, new CategoriesSelBoxEventArgs(category, ""));
        }
    }
}