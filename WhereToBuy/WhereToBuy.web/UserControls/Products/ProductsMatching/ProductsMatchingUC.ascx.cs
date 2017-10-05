using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WhereToBuy.entities;
using WhereToBuy.web.UserControls.Suppliers.SuppliersSelBox;

namespace WhereToBuy.web.UserControls.Products.ProductsMatching
{
    public partial class ProductsMatchingUC : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SuppliersSelBox.SelectedSupplierUpdate += ProductsMatchingUC_SelectedSupplierUpdate;
            SuppliersSelBox.SupplierSelBoxMessage += ProductsMatchingUC_SupplierSelBoxMessage;
        }


        #region Grid


        protected void ProductMatchingGridView_Sorting(object sender, GridViewSortEventArgs e)
        {
            if (ViewState["ProductMatchingOrderBy"].ToString().TrimEnd().ToLower() != e.SortExpression.ToString().TrimEnd().ToLower())
            {
                ViewState["ProductMatchingOrderBy"] = e.SortExpression.ToString().TrimEnd();
                ViewState["ProductMatchingOrderByType"] = "ASC";
            }

            if (ViewState["ProductMatchingOrderByType"].ToString().TrimEnd() == "ASC")
            {
                ViewState["ProductMatchingOrderByType"] = "DESC";
            }
            else
            {
                ViewState["ProductMatchingOrderByType"] = "ASC";
            }

            RefreshGridView();
            UpdatePanel1.Update();
        }


        #region RowItem


        protected void ProductMatchingGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            LinkButton linkSelect;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                linkSelect = (LinkButton)e.Row.FindControl("lnkSelect");
                e.Row.Attributes["onclick"] = this.Page.ClientScript.GetPostBackEventReference(linkSelect, "");
            }
        }


        protected void ProductMatchingGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string returnUrlQueryString;
            string supplerCode;
            string supplement;
            string externalCode;

            if (e.CommandName.ToLower().Trim() == "select")
            {
                ProductMatchingGridView.SelectedIndex = int.Parse(e.CommandArgument.ToString());
                LoadProductMatching(ProductMatchingGridView.SelectedDataKey.Values[1] as Supplier, ProductMatchingGridView.SelectedDataKey.Values[0].ToString(), ProductMatchingGridView.SelectedDataKey.Values[2].ToString());
            }
            else if (e.CommandName.ToLower().Trim() == "openselect" && e.CommandArgument.ToString() != "")
            {
                supplerCode = ((sender as GridView).Rows[int.Parse(e.CommandArgument.ToString())].FindControl("SupplierLabel") as Label).Text.Replace("<p/>", " ").Split(' ').First();
                supplement = ((sender as GridView).Rows[int.Parse(e.CommandArgument.ToString())].FindControl("SupplementLabel") as Label).Text.Replace("<p/>", " ").Split(' ').First();
                externalCode = ((sender as GridView).Rows[int.Parse(e.CommandArgument.ToString())].FindControl("CodeLabel") as Label).Text.Replace("<p/>", " ").Split(' ').First();
                returnUrlQueryString = string.Format("returnUrl={0}&supplierCode={1}&code={2}&supplement={3} ", Server.UrlEncode(Request.AppRelativeCurrentExecutionFilePath), supplerCode, externalCode, supplement);
                Response.Redirect(string.Format("{0}?{1}", Application["ProductMatchingPage"].ToString().TrimEnd(), returnUrlQueryString), true);
            }
        }


        protected void ProductMatchingGridView_RowCreated(object sender, GridViewRowEventArgs e)
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


        protected void ProductMatchingGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            ProductMatchingGridView.PageIndex = e.NewPageIndex;
        }


        protected void ProductMatchingGridView_PageIndexChanged(object sender, EventArgs e)
        {
            RefreshGridView();
        }


        protected void PageFooter_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "ChangePage")
            {
                ProductMatchingGridView.PageIndex = Convert.ToInt32(e.CommandArgument) - 1;
            }
        }


        protected void PageButton_Click(object sender, EventArgs e)
        {
            if (((LinkButton)sender).ID.ToString() == "BackwardButton")
            {
                if (ProductMatchingGridView.PageIndex == 0)
                {
                    return;
                }
                ProductMatchingGridView.PageIndex = ProductMatchingGridView.PageIndex - 1;
            }
            else
            {
                if (ProductMatchingGridView.PageIndex == ProductMatchingGridView.PageCount - 1)
                {
                    return;
                }
                ProductMatchingGridView.PageIndex = ProductMatchingGridView.PageIndex + 1;
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
            //if (Request.QueryString.Count > 0)
            //{
            //    returnUrlQueryString += Server.UrlEncode(string.Format("?{0}", Request.QueryString.ToString()));
            //}

            Response.Redirect(string.Format("{0}?{1}", Application["ProductMatchingPage"].ToString().TrimEnd(), returnUrlQueryString), true);
        }

        protected void UpdateElementButton_Click(object sender, EventArgs e)
        {
            string returnUrlQueryString;

            if (ViewState["SelectedProductMatching"] == null)
            {
                this.MessageUC.ShowError("QuotationWarning", "Must select a product matching to update");//traduzir
                return;
            }



            returnUrlQueryString = string.Format("returnUrl={0}&supplierCode={1}&code={2}&supplement={3} ", Server.UrlEncode(Request.AppRelativeCurrentExecutionFilePath),
                                                             ((WhereToBuy.entities.ProductMatching)ViewState["SelectedProductMatching"]).Supplier.Code,
                                                             ((WhereToBuy.entities.ProductMatching)ViewState["SelectedProductMatching"]).Code, ((WhereToBuy.entities.ProductMatching)ViewState["SelectedProductMatching"]).Supplement);
            //if (Request.QueryString.Count > 0)
            //{
            //    returnUrlQueryString += Server.UrlEncode(string.Format("?{0}", Request.QueryString.ToString()));
            //}
            Response.Redirect(string.Format("{0}?{1}", Application["ProductMatchingPage"].ToString().TrimEnd(), returnUrlQueryString), true);
        }

        protected void btnClean_Click(object sender, EventArgs e)
        {
            ClearFilter();

        }


        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SetSelectedMatching(null);
            ProductMatchingGridView.PageIndex = 0;
            ProductMatchingGridView.SelectedIndex = -1;
            RefreshGridView();
            UpdatePanel1.Update();

        }



        protected void RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            switch ((sender as RadioButton).ID)
            {

                case "ActiveRadioButton":
                    AllRadioButton.Checked = false;
                    InactiveRadioButton.Checked = false;
                    MatchingRadioButton.Checked = false;
                    CustomRadioButton.Checked = false;
                    ResetRadioButton.Checked = false;

                    break;
                case "InactiveRadioButton":
                    AllRadioButton.Checked = false;
                    ActiveRadioButton.Checked = false;
                    MatchingRadioButton.Checked = false;
                    CustomRadioButton.Checked = false;
                    ResetRadioButton.Checked = false;
                    break;
                case "AllRadioButton":
                    ActiveRadioButton.Checked = false;
                    InactiveRadioButton.Checked = false;
                    MatchingRadioButton.Checked = false;
                    CustomRadioButton.Checked = false;
                    ResetRadioButton.Checked = false;
                    break;
                case "MatchingRadioButton":
                    AllRadioButton.Checked = false;
                    ActiveRadioButton.Checked = false;
                    InactiveRadioButton.Checked = false;
                    CustomRadioButton.Checked = false;
                    ResetRadioButton.Checked = false;
                    break;
                case "ResetButton":
                    AllRadioButton.Checked = false;
                    ActiveRadioButton.Checked = false;
                    InactiveRadioButton.Checked = false;
                    MatchingRadioButton.Checked = false;
                    CustomRadioButton.Checked = false;
                   
                    break;
                case "CustomButton":
                    AllRadioButton.Checked = false;
                    ActiveRadioButton.Checked = false;
                    InactiveRadioButton.Checked = false;
                    MatchingRadioButton.Checked = false;
                    ResetRadioButton.Checked = false;
                    break;
                default:
                    AllRadioButton.Checked = false;
                    ActiveRadioButton.Checked = false;
                    InactiveRadioButton.Checked = false;
                    CustomRadioButton.Checked = false;
                    ResetRadioButton.Checked = false;
                    MatchingRadioButton.Checked = true;
                    break;
            }

        }


        #endregion


        #region Links

        #endregion


        #region TextBox

        #endregion


        #region SelBox


        private void ProductsMatchingUC_SupplierSelBoxMessage(object sender, SupplierSelBoxEventArgs e)
        {
            this.MessageUC.ShowError("Error", e.Message);
            return;
        }


        private void ProductsMatchingUC_SelectedSupplierUpdate(object sender, SupplierSelBoxEventArgs e)
        {
            SetSelectedSupplier(e.Supplier);
        }


        #endregion

    }
}