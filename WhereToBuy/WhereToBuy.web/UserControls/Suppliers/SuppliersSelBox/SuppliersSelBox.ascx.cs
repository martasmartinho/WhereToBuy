using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WhereToBuy.entities;
using WhereToBuy.web.UserControls.BrandsMatching;

namespace WhereToBuy.web.UserControls.Suppliers.SuppliersSelBox
{
    public partial class SuppliersSelBox : System.Web.UI.UserControl
    {
       
        protected void Page_Load(object sender, EventArgs e)
        {
            
            ///SupplierTextBox.Focus();
            if (!IsPostBack)
            {
                
            }
          
        }

        protected void SupplierSearchButton_Click(object sender, EventArgs e)
        {
           
            txtSupplier.Focus();
            SubmitButtonClick(SupplierSearchButton, new SupplierSelBoxEventArgs(null, ""));
            RefreshListView();
        }


        protected void SelectedItemButton_Click(object sender, EventArgs e)
        {
            entities.Supplier supplier;

            SupplierListView.SelectedIndex = Convert.ToInt32((((LinkButton)sender).CommandArgument));
            supplier = LoadSupplier(((LinkButton)sender).Text.Split('-')[0].TrimStart().TrimEnd());
            txtSupplier.Text = supplier.ToString();

            SupplierListView.Items.Clear();
            SupplierListView.DataBind();
            SelectedSupplierUpdate(this, new SupplierSelBoxEventArgs(supplier, ""));
        }

     
    }
}