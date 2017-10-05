using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WhereToBuy.entities;
using WhereToBuy.web.UserControls.Brands.BrandsSelBox;
using WhereToBuy.web.UserControls.Categories.CategoriesSelBox;
using WhereToBuy.web.UserControls.Suppliers.SuppliersSelBox;

namespace WhereToBuy.web.UserControls.Products.Products
{
    public partial class ProductsUC : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SuppliersSelBox.SelectedSupplierUpdate += ProductsUC_SelectedSupplierUpdate;
            SuppliersSelBox.SupplierSelBoxMessage += ProductsUC_SupplierSelBoxMessage;
            CategoriesSelBox.SelectedCategoryUpdate += ProductsUC_SelectedCategoryUpdate;
            CategoriesSelBox.CategoriesSelBoxMessage += ProductsUC_CategorySelBoxMessage;
            BrandsSelBox.SelectedBrandUpdate += ProductsUC_SelectedBrandUpdate;
            BrandsSelBox.BrandSelBoxMessage += ProductsUC_BrandSelBoxMessage;

            // Load page
            if (!Page.IsPostBack)
            {
                // load data
                UpdateData();

            }
        }


        #region Grid


        protected void ProductGridView_Sorting(object sender, GridViewSortEventArgs e)
        {
            if (ViewState["ProductOrderBy"].ToString().TrimEnd().ToLower() != e.SortExpression.ToString().TrimEnd().ToLower())
            {
                ViewState["ProductOrderBy"] = e.SortExpression.ToString().TrimEnd();
                ViewState["ProductOrderByType"] = "ASC";
            }

            else if (ViewState["ProductOrderByType"].ToString().TrimEnd() == "ASC")
            {
                ViewState["ProductOrderByType"] = "DESC";
            }
            else
            {
                ViewState["ProductOrderByType"] = "ASC";
            }

            RefreshGridView();
            UpdatePanel1.Update();
        }


        #region RowItem


        protected void ProductGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            LinkButton linkSelect;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                linkSelect = (LinkButton)e.Row.FindControl("lnkSelect");
                e.Row.Attributes["onclick"] = this.Page.ClientScript.GetPostBackEventReference(linkSelect, "");
            }
        }


        protected void ProductGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string returnUrlQueryString;
            string code;

            if (e.CommandName.ToLower().Trim() == "select")
            {
                ProductGridView.SelectedIndex = int.Parse(e.CommandArgument.ToString());
                LoadProduct(ProductGridView.SelectedDataKey.Values[0].ToString());
            }
            else if (e.CommandName.ToLower().Trim() == "openselect" && e.CommandArgument.ToString() != "")
            {
                code = ((sender as GridView).Rows[int.Parse(e.CommandArgument.ToString())].FindControl("ProductLabel") as Label).Text.Replace("<p/>", " ").Replace("<strong>", string.Empty).Split(' ').First();
                returnUrlQueryString = string.Format("returnUrl={0}&code={1}", Server.UrlEncode(Request.AppRelativeCurrentExecutionFilePath), code);
                Response.Redirect(string.Format("{0}?{1}", Application["ProductPage"].ToString().TrimEnd(), returnUrlQueryString), true);
            }
        }


        protected void ProductGridView_RowCreated(object sender, GridViewRowEventArgs e)
        {
            LinkButton linkSelect;
            LinkButton btnSelect;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                linkSelect = (LinkButton)e.Row.FindControl("lnkSelect");
                linkSelect.CommandArgument = e.Row.RowIndex.ToString();

                btnSelect = (LinkButton)e.Row.FindControl("btnSelect");
                btnSelect.CommandArgument = e.Row.RowIndex.ToString();
            }

        }


        #endregion


        #region Pager


        protected void ProductGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            ProductGridView.PageIndex = e.NewPageIndex;
        }


        protected void ProductGridView_PageIndexChanged(object sender, EventArgs e)
        {
            RefreshGridView();
        }


        protected void PageFooter_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "ChangePage")
            {
                ProductGridView.PageIndex = Convert.ToInt32(e.CommandArgument) - 1;
            }
        }


        protected void PageButton_Click(object sender, EventArgs e)
        {
            if (((LinkButton)sender).ID.ToString() == "BackwardButton")
            {
                if (ProductGridView.PageIndex == 0)
                {
                    return;
                }
                ProductGridView.PageIndex = ProductGridView.PageIndex - 1;
            }
            else
            {
                if (ProductGridView.PageIndex == ProductGridView.PageCount - 1)
                {
                    return;
                }
                ProductGridView.PageIndex = ProductGridView.PageIndex + 1;
            }

            RefreshGridView();
        }


        #endregion


        #region Others



        #endregion


        #endregion


        #region Buttons


        protected void NewElementButton_Click(object sender, EventArgs e)
        {

            string returnUrlQueryString;

            returnUrlQueryString = string.Format("returnUrl={0}", Server.UrlEncode(Request.AppRelativeCurrentExecutionFilePath));
            Response.Redirect(string.Format("{0}?{1}", Application["ProductPage"].ToString().TrimEnd(), returnUrlQueryString), true);
        }

        protected void UpdateElementButton_Click(object sender, EventArgs e)
        {
            string returnUrlQueryString;

            if (ViewState["SelectedProduct"] == null)
            {
                this.MessageUC.ShowError("Product", "Must select a product to update");//traduzir
                return;
            }

            returnUrlQueryString = string.Format("returnUrl={0}&code={1} ", Server.UrlEncode(Request.AppRelativeCurrentExecutionFilePath), ((WhereToBuy.entities.Product)ViewState["SelectedProduct"]).Code);
            Response.Redirect(string.Format("{0}?{1}", Application["ProductPage"].ToString().TrimEnd(), returnUrlQueryString), true);
        }

        protected void btnClean_Click(object sender, EventArgs e)
        {
            ClearFilter();

        }


        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SetSelectedProduct(null);
            ProductGridView.PageIndex = 0;
            ProductGridView.SelectedIndex = -1;
            RefreshGridView();
            UpdatePanel1.Update();

        }



        protected void RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            switch ((sender as RadioButton).ID)
            {

                case "CurrentRadioButton":
                    AllRadioButton.Checked = false;
                    InactiveRadioButton.Checked = false;
                    DiscontinuedRadioButton.Checked = false;
                    ManualUpdateRadioButton.Checked = false;
                    IPCRadioButton.Checked = false;

                    break;
                case "InactiveRadioButton":
                    AllRadioButton.Checked = false;
                    CurrentRadioButton.Checked = false;
                    DiscontinuedRadioButton.Checked = false;
                    ManualUpdateRadioButton.Checked = false;
                    IPCRadioButton.Checked = false;
                    break;
                case "AllRadioButton":
                    CurrentRadioButton.Checked = false;
                    InactiveRadioButton.Checked = false;
                    DiscontinuedRadioButton.Checked = false;
                    ManualUpdateRadioButton.Checked = false;
                    IPCRadioButton.Checked = false;
                    break;
                case "IPCRadioButton":
                    AllRadioButton.Checked = false;
                    CurrentRadioButton.Checked = false;
                    InactiveRadioButton.Checked = false;
                    DiscontinuedRadioButton.Checked = false;
                    ManualUpdateRadioButton.Checked = false;
                    
                    break;
                case "ManualUpdateRadioButton":
                    AllRadioButton.Checked = false;
                    CurrentRadioButton.Checked = false;
                    InactiveRadioButton.Checked = false;
                    DiscontinuedRadioButton.Checked = false;
                    IPCRadioButton.Checked = false;

                    break;
                case "DiscontinuedRadioButton":
                    AllRadioButton.Checked = false;
                    CurrentRadioButton.Checked = false;
                    InactiveRadioButton.Checked = false;
                    ManualUpdateRadioButton.Checked = false;
                    IPCRadioButton.Checked = false;
                    break;
                default:
                    AllRadioButton.Checked = false;
                    CurrentRadioButton.Checked = false;
                    InactiveRadioButton.Checked = false;
                    DiscontinuedRadioButton.Checked = false;
                    ManualUpdateRadioButton.Checked = false;
                    IPCRadioButton.Checked = false;
                    break;
            }

        }


        #endregion


        #region Links

        #endregion


        #region TextBox

        #endregion


        #region SelBox


        private void ProductsUC_SupplierSelBoxMessage(object sender, SupplierSelBoxEventArgs e)
        {
            this.MessageUC.ShowError("Error", e.Message);
            return;
        }


        private void ProductsUC_SelectedSupplierUpdate(object sender, SupplierSelBoxEventArgs e)
        {
            SetSelectedSupplier(e.Supplier);
        }


        private void ProductsUC_CategorySelBoxMessage(object sender, CategoriesSelBoxEventArgs e)
        {
            this.MessageUC.ShowError("Error", e.Message);
            return;
        }


        private void ProductsUC_SelectedCategoryUpdate(object sender, CategoriesSelBoxEventArgs e)
        {
            SetSelectedCategory(e.Category);
        }

        private void ProductsUC_BrandSelBoxMessage(object sender, BrandSelBoxEventArgs e)
        {
            this.MessageUC.ShowError("Error", e.Message);
            return;
        }


        private void ProductsUC_SelectedBrandUpdate(object sender, BrandSelBoxEventArgs e)
        {
            SetSelectedBrand(e.Brand);
        }

        #endregion
    }
}