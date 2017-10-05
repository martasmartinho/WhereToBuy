using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WhereToBuy.web.UserControls.Catalogs.CatalogsSelBox
{
    public partial class CatalogsSelBox : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string teste = txtCatalog.Text;
            //if (ViewState["SelectedCatalog"] != null)
            //{
            //    UpdateData((WhereToBuy.entities.Catalog)ViewState["SelectedCatalog"], false);
            //}
            //else
            //{
            //    UpdateData("", false);
            //}


            //UpdateData(txtCatalog, false);

            if (IsPostBack)
            {
                if (!(String.IsNullOrEmpty(txtCatalog.Text.Trim())))
                {
                    txtCatalog.Attributes["value"] = txtCatalog.Text;
                    //or txtPwd.Attributes.Add("value",txtPwd.Text);
                }

            }

        }


        protected void lkBtnSearch_Click(object sender, EventArgs e)
        {

            txtCatalog.Focus();
            SubmitButtonClick(lkBtnSearch, new CatalogsSelBoxEventArgs(null, ""));
            RefreshListView();
        }




        protected void lkBtnItem_Click(object sender, EventArgs e)
        {
            WhereToBuy.entities.Catalog catalog;

            lvCatalogs.SelectedIndex = Convert.ToInt32((((LinkButton)sender).CommandArgument));
            catalog = LoadCatalog(((LinkButton)sender).Text.Split('-')[0].TrimStart().TrimEnd());
            txtCatalog.Text = catalog.ToString();

            lvCatalogs.Items.Clear();
            lvCatalogs.DataBind();
            SelectedCatalogUpdate(this, new CatalogsSelBoxEventArgs(catalog, ""));
        }

        protected void btnDummy_Click(object sender, EventArgs e)
        {
            RefreshListView();


        }

        protected void txtCatalog_DataBinding(object sender, EventArgs e)
        {
            string tetse = txtCatalog.Text;
        }

        protected void txtCatalog_Init(object sender, EventArgs e)
        {

        }

        protected void txtCatalog_TextChanged(object sender, EventArgs e)
        {
            string tetse = txtCatalog.Text;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string tetse = txtCatalog.Text;
        }
    }
}