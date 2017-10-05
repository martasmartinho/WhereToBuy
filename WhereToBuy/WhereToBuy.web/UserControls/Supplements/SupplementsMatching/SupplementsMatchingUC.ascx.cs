using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WhereToBuy.entities;

namespace WhereToBuy.web.UserControls.Supplements.SupplementsMatching
{
    public partial class SupplementsMatchingUC : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SuppliersSelBox.SelectedSupplierUpdate += SupplementsMatchingUC_SelectedSupplierUpdate;
            SuppliersSelBox.SupplierSelBoxMessage += SupplementsMatchingUC_SupplierSelBoxMessage;
        }

        private void SupplementsMatchingUC_SupplierSelBoxMessage(object sender, Suppliers.SuppliersSelBox.SupplierSelBoxEventArgs e)
        {
            this.MessageUC.ShowError("Error", e.Message);
            return;
        }

        private void SupplementsMatchingUC_SelectedSupplierUpdate(object sender, Suppliers.SuppliersSelBox.SupplierSelBoxEventArgs e)
        {
            SetSelectedSupplier(e.Supplier);
        }

        #region Grid



        protected void gvSupplementsMatching_Sorting(object sender, GridViewSortEventArgs e)
        {
            if (ViewState["SupplementMatchingOrderBy"].ToString().TrimEnd().ToLower() != e.SortExpression.ToString().TrimEnd().ToLower())
            {
                ViewState["SupplementMatchingOrderBy"] = e.SortExpression.ToString().TrimEnd();
                ViewState["SupplementMatchingOrderByType"] = "ASC";
            }

            if (ViewState["SupplementMatchingOrderByType"].ToString().TrimEnd() == "ASC")
            {
                ViewState["SupplementMatchingOrderByType"] = "DESC";
            }
            else
            {
                ViewState["SupplementMatchingOrderByType"] = "ASC";
            }

            RefreshGridView();
            UpdatePanel1.Update();
        }




        #region RowItem


        protected void gvSupplementsMatching_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            LinkButton linkSelect;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                linkSelect = (LinkButton)e.Row.FindControl("lnkSelect");
                e.Row.Attributes["onclick"] = this.Page.ClientScript.GetPostBackEventReference(linkSelect, "");

            }

        }


        protected void gvSupplementsMatching_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string returnUrlQueryString;
            string code;
            string externalCode;

            if (e.CommandName.ToLower().Trim() == "select")
            {
                gvSupplementsMatching.SelectedIndex = int.Parse(e.CommandArgument.ToString());
                LoadSupplementMatching(gvSupplementsMatching.SelectedDataKey.Values[1] as Supplier, gvSupplementsMatching.SelectedDataKey.Values[0].ToString());
            }
            else if (e.CommandName.ToLower().Trim() == "openselect" && e.CommandArgument.ToString() != "")
            {
                code = ((sender as GridView).Rows[int.Parse(e.CommandArgument.ToString())].FindControl("SupplierLabel") as Label).Text.Replace("<p/>", " ").Split(' ').First();
                externalCode = ((sender as GridView).Rows[int.Parse(e.CommandArgument.ToString())].FindControl("lblCode") as Label).Text.Replace("<p/>", " ").Split(' ').First();
                returnUrlQueryString = string.Format("returnUrl={0}&supplierCode={1}&code={2} ", Server.UrlEncode(Request.AppRelativeCurrentExecutionFilePath), code, externalCode);
                Response.Redirect(string.Format("{0}?{1}", Application["SupplementMatchingPage"].ToString().TrimEnd(), returnUrlQueryString), true);
            }
        }

        protected void gvSupplementsMatching_RowCreated(object sender, GridViewRowEventArgs e)
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

        protected void gvSupplementsMatching_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvSupplementsMatching.PageIndex = e.NewPageIndex;

        }

        protected void gvSupplementsMatching_PageIndexChanged(object sender, EventArgs e)
        {
            RefreshGridView();
        }

        protected void PageFooter_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

            if (e.CommandName == "ChangePage")
            {
                gvSupplementsMatching.PageIndex = Convert.ToInt32(e.CommandArgument) - 1;
            }


        }

        protected void btnPage_Click(object sender, EventArgs e)
        {

            if (((LinkButton)sender).ID.ToString() == "btnBackward")
            {
                if (gvSupplementsMatching.PageIndex == 0)
                {
                    return;
                }
                gvSupplementsMatching.PageIndex = gvSupplementsMatching.PageIndex - 1;
            }
            else
            {
                if (gvSupplementsMatching.PageIndex == gvSupplementsMatching.PageCount - 1)
                {
                    return;
                }
                gvSupplementsMatching.PageIndex = gvSupplementsMatching.PageIndex + 1;
            }

            RefreshGridView();


        }


        #endregion

        #region Others


        #endregion

        #endregion

        #region Buttons


        protected void btnNewElement_Click(object sender, EventArgs e)
        {

            string returnUrlQueryString;

            returnUrlQueryString = string.Format("returnUrl={0}", Server.UrlEncode(Request.AppRelativeCurrentExecutionFilePath));
            //if (Request.QueryString.Count > 0)
            //{
            //    returnUrlQueryString += Server.UrlEncode(string.Format("?{0}", Request.QueryString.ToString()));
            //}

            Response.Redirect(string.Format("{0}?{1}", Application["SupplementMatchingPage"].ToString().TrimEnd(), returnUrlQueryString), true);
        }

        protected void btnUpdateElement_Click(object sender, EventArgs e)
        {
            string returnUrlQueryString;

            if (ViewState["SelectedSupplementMatching"] == null)
            {
                this.MessageUC.ShowError("QuotationWarning", "Must select a brand matching to update");//traduzir
                return;
            }



            returnUrlQueryString = string.Format("returnUrl={0}&supplierCode={1}&code={2} ", Server.UrlEncode(Request.AppRelativeCurrentExecutionFilePath), ((WhereToBuy.entities.SupplementMatching)ViewState["SelectedSupplementMatching"]).Supplier.Code, ((WhereToBuy.entities.SupplementMatching)ViewState["SelectedSupplementMatching"]).Code);
            //if (Request.QueryString.Count > 0)
            //{
            //    returnUrlQueryString += Server.UrlEncode(string.Format("?{0}", Request.QueryString.ToString()));
            //}
            Response.Redirect(string.Format("{0}?{1}", Application["SupplementMatchingPage"].ToString().TrimEnd(), returnUrlQueryString), true);
        }

        protected void btnClean_Click(object sender, EventArgs e)
        {
            ClearFilter();

        }


        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SetSelectedMatching(null);
            gvSupplementsMatching.PageIndex = 0;
            gvSupplementsMatching.SelectedIndex = -1;
            RefreshGridView();
            UpdatePanel1.Update();

        }



        protected void RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            switch ((sender as RadioButton).ID)
            {

                case "btnActive":
                    btnAll.Checked = false;
                    btnInactive.Checked = false;
                    btnMatching.Checked = false;
                    break;
                case "btnInactive":
                    btnAll.Checked = false;
                    btnActive.Checked = false;
                    btnMatching.Checked = false;
                    break;
                case "btnAll":
                    btnActive.Checked = false;
                    btnInactive.Checked = false;
                    btnMatching.Checked = false;
                    break;
                case "btnMatching":
                    btnAll.Checked = false;
                    btnActive.Checked = false;
                    btnInactive.Checked = false;
                    break;
                default:
                    btnAll.Checked = false;
                    btnActive.Checked = false;
                    btnInactive.Checked = false;
                    btnMatching.Checked = true;
                    break;
            }

        }


        #endregion

        #region Links
        #endregion

        #region TextBox

        #endregion

        #region SelBox


        #endregion

    }
}