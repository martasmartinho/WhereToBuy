using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WhereToBuy.entities;

namespace WhereToBuy.web.UserControls.Brands.BrandsSelBox
{
    public partial class BrandsSelBox : System.Web.UI.UserControl
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
             
            }
      
            
        }

        protected void lkBtnSearch_Click(object sender, EventArgs e)
        {

            txtBrand.Focus();
            SubmitButtonClick(lkBtnSearch, new BrandSelBoxEventArgs(null, ""));
            RefreshListView();
        }


     

        protected void lkBtnItem_Click(object sender, EventArgs e)
        {
            WhereToBuy.entities.Brand brand;

            lvBrands.SelectedIndex = Convert.ToInt32((((LinkButton)sender).CommandArgument));
            brand = LoadBrand(((LinkButton)sender).Text.Split('-')[0].TrimStart().TrimEnd());
            txtBrand.Text = brand.ToString();

            lvBrands.Items.Clear();
            lvBrands.DataBind();
            SelectedBrandUpdate(this, new BrandSelBoxEventArgs(brand, ""));
        }

       

 
    }
}