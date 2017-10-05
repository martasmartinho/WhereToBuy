using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WhereToBuy.web.UserControls.Products.Product
{
    public partial class ProductUC : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            CategoriesSelBox.SelectedCategoryUpdate += Product_SelectedCategory;
            CategoriesSelBox.CategoriesSelBoxMessage += Product_CategoryMessage;
            BrandsSelBox.SelectedBrandUpdate += Product_SelectedBrand;
            BrandsSelBox.BrandSelBoxMessage += Product_BrandMessage;
            TaxesSelBox.SelectedTaxUpdate += Product_SelectedTax;
            TaxesSelBox.TaxesSelBoxMessage += Product_TaxMessage;
            //SuppliersSelBox.SelectedSupplierUpdate += Product_SelectedSupplier;
            //SuppliersSelBox.SupplierSelBoxMessage += Product_SupplierMessage;
            // Load page
            if (!Page.IsPostBack)
            {
               
                string code = string.Empty;


                if (Page.Request.QueryString["code"] != null)
                {
                    code = Page.Request.QueryString["code"].ToString().TrimEnd();
                }


                // load data
                UpdateData(code);

            }
        }

       


        #region Grid


        protected void gvDetails_Sorting(object sender, GridViewSortEventArgs e)
        {
            if (ViewState["DetailsOrderBy"].ToString().TrimEnd().ToLower() != e.SortExpression.ToString().TrimEnd().ToLower())
            {
                ViewState["DetailsOrderBy"] = e.SortExpression.ToString().TrimEnd();
                ViewState["DetailsOrderByType"] = "ASC";
            }

            if (ViewState["DetailsOrderByType"].ToString().TrimEnd() == "ASC")
            {
                ViewState["DetailsOrderByType"] = "DESC";
            }
            else
            {
                ViewState["DetailsOrderByType"] = "ASC";
            }

            //RefreshGridView();
            UpdatePanel1.Update();
        }


        #region RowItem


        protected void gvDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            LinkButton linkSelect;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                linkSelect = (LinkButton)e.Row.FindControl("lnkSelect");
                e.Row.Attributes["onclick"] = this.Page.ClientScript.GetPostBackEventReference(linkSelect, "");
            }
        }


        protected void gvDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string returnUrlQueryString;
            string code;

            if (e.CommandName.ToLower().Trim() == "select")
            {
                gvDetails.SelectedIndex = int.Parse(e.CommandArgument.ToString());
                LoadDetail(gvDetails.SelectedDataKey.Values[0].ToString(), gvDetails.SelectedDataKey.Values[1].ToString());
            }
            else if (e.CommandName.ToLower().Trim() == "openselect" && e.CommandArgument.ToString() != "")
            {
                code = ((sender as GridView).Rows[int.Parse(e.CommandArgument.ToString())].FindControl("lblCode") as Label).Text;
                returnUrlQueryString = string.Format("returnUrl={0}&code={1} ", Server.UrlEncode(Request.AppRelativeCurrentExecutionFilePath), code);
                Response.Redirect(string.Format("{0}?{1}", Application["BrandPage"].ToString().TrimEnd(), returnUrlQueryString), true);
            }

        }


        protected void gvDetails_RowCreated(object sender, GridViewRowEventArgs e)
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


        protected void gvDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDetails.PageIndex = e.NewPageIndex;
        }


        protected void gvDetails_PageIndexChanged(object sender, EventArgs e)
        {
            //RefreshGridView();
        }


        protected void PageFooter_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "ChangePage")
            {
                gvDetails.PageIndex = Convert.ToInt32(e.CommandArgument) - 1;
            }
        }


        protected void btnPage_Click(object sender, EventArgs e)
        {

            if (((LinkButton)sender).ID.ToString() == "btnBackward")
            {
                if (gvDetails.PageIndex == 0)
                {
                    return;
                }
                gvDetails.PageIndex = gvDetails.PageIndex - 1;
            }
            else
            {
                if (gvDetails.PageIndex == gvDetails.PageCount - 1)
                {
                    return;
                }
                gvDetails.PageIndex = gvDetails.PageIndex + 1;
            }

            //RefreshGridView();
        }


        #endregion


        #region Others


        #endregion


        #endregion


        #region Private Events

        //private void Product_SupplierMessage(object sender, Suppliers.SuppliersSelBox.SupplierSelBoxEventArgs e)
        //{
        //    this.MessageUC.ShowError("Error", e.Message);
        //    return;
        //}

        //private void Product_SelectedSupplier(object sender, Suppliers.SuppliersSelBox.SupplierSelBoxEventArgs e)
        //{
        //    SetSupplier(e.Supplier);
        //}

        private void Product_TaxMessage(object sender, Taxes.TaxSelBox.TaxesSelBoxEventArgs e)
        {
            this.MessageUC.ShowError("Error", e.Message);
            return;
        }

        private void Product_BrandMessage(object sender, Brands.BrandsSelBox.BrandSelBoxEventArgs e)
        {
            this.MessageUC.ShowError("Error", e.Message);
            return;
        }

        private void Product_CategoryMessage(object sender, Categories.CategoriesSelBox.CategoriesSelBoxEventArgs e)
        {
            this.MessageUC.ShowError("Error", e.Message);
            return;
        }

      
        private void Product_SelectedTax(object sender, Taxes.TaxSelBox.TaxesSelBoxEventArgs e)
        {
            SetTax(e.Tax);
        }

        private void Product_SelectedBrand(object sender, Brands.BrandsSelBox.BrandSelBoxEventArgs e)
        {
            SetBrand(e.Brand);
        }

        private void Product_SelectedCategory(object sender, Categories.CategoriesSelBox.CategoriesSelBoxEventArgs e)
        {
            SetCategory(e.Category);
        }

        #endregion


        protected void btnNew_Click(object sender, EventArgs e)
        {
            New();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            Delete();
        }

        protected void UpdatePanel1_Init(object sender, EventArgs e)
        {

        }

        protected void btnOk_Click(object sender, EventArgs e)
        {

            Save();


        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Cancel();
        }

        protected void SelectedItemButton_Click(object sender, EventArgs e)
        {
            //switch (((sender as LinkButton).CommandArgument))
            //{
            //    case "OneDay": txtDataReset.Text = DateTime.Now.AddDays(1).ToString("dd-MM-yyyy");
            //        break;
            //    case "TwoDay": txtDataReset.Text = DateTime.Now.AddDays(2).ToString("dd-MM-yyyy");
            //        break;
            //    case "ThreeDay": txtDataReset.Text = DateTime.Now.AddDays(3).ToString("dd-MM-yyyy");
            //        break;
            //    case "FourDay": txtDataReset.Text = DateTime.Now.AddDays(4).ToString("dd-MM-yyyy");
            //        break;
            //    case "FiveDay": txtDataReset.Text = DateTime.Now.AddDays(5).ToString("dd-MM-yyyy");
            //        break;
            //    case "SixDay": txtDataReset.Text = DateTime.Now.AddDays(6).ToString("dd-MM-yyyy");
            //        break;
            //    case "OneWeek": txtDataReset.Text = DateTime.Now.AddDays(7).ToString("dd-MM-yyyy");
            //        break;
            //    case "TwoWeek": txtDataReset.Text = DateTime.Now.AddDays(14).ToString("dd-MM-yyyy");
            //        break;
            //    case "ThreeWeek": txtDataReset.Text = DateTime.Now.AddDays(21).ToString("dd-MM-yyyy");
            //        break;
            //    case "OneMonth": txtDataReset.Text = DateTime.Now.AddMonths(1).ToString("dd-MM-yyyy");
            //        break;
            //    case "TwoMonth": txtDataReset.Text = DateTime.Now.AddMonths(2).ToString("dd-MM-yyyy");
            //        break;
            //    default:
            //        break;
            //}
        }

        protected void lnkCodeSearch_Click(object sender, EventArgs e)
        {
            LoadProduct(txtCode.Text.TrimEnd().TrimStart());
        }


     
    }
}