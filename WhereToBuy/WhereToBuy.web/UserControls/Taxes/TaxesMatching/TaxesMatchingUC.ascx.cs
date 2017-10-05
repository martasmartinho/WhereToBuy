using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WhereToBuy.entities;
using WhereToBuy.web.UserControls.Suppliers.SuppliersSelBox;

namespace WhereToBuy.web.UserControls.Taxes.TaxesMatching
{
    public partial class TaxesMatchingUC : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SuppliersSelBox.SelectedSupplierUpdate += TaxesMatchingUC_SelectedSupplierUpdate;
            SuppliersSelBox.SupplierSelBoxMessage += TaxesMatchingUC_SupplierSelBoxMessage;
        }

        private void TaxesMatchingUC_SupplierSelBoxMessage(object sender, SupplierSelBoxEventArgs e)
        {
            this.MessageUC.ShowError("Error", e.Message);
            return;


        }

        private void TaxesMatchingUC_SelectedSupplierUpdate(object sender, SupplierSelBoxEventArgs e)
        {
            SetSelectedSupplier(e.Supplier);
        }




        #region Grid



        protected void gvTaxesMatching_Sorting(object sender, GridViewSortEventArgs e)
        {
            if (ViewState["TaxMatchingOrderBy"].ToString().TrimEnd().ToLower() != e.SortExpression.ToString().TrimEnd().ToLower())
            {
                ViewState["TaxMatchingOrderBy"] = e.SortExpression.ToString().TrimEnd();
                ViewState["TaxMatchingOrderByType"] = "ASC";
            }

            if (ViewState["TaxMatchingOrderByType"].ToString().TrimEnd() == "ASC")
            {
                ViewState["TaxMatchingOrderByType"] = "DESC";
            }
            else
            {
                ViewState["TaxMatchingOrderByType"] = "ASC";
            }

            RefreshGridView();
            UpdatePanel1.Update();
        }




        #region RowItem


        protected void gvTaxesMatching_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            LinkButton linkSelect;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                linkSelect = (LinkButton)e.Row.FindControl("lnkSelect");
                e.Row.Attributes["onclick"] = this.Page.ClientScript.GetPostBackEventReference(linkSelect, "");

            }

        }


        protected void gvTaxesMatching_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string returnUrlQueryString;
            string code;
            string externalCode;

            if (e.CommandName.ToLower().Trim() == "select")
            {
                gvTaxesMatching.SelectedIndex = int.Parse(e.CommandArgument.ToString());
                LoadTaxMatching(gvTaxesMatching.SelectedDataKey.Values[1] as Supplier, gvTaxesMatching.SelectedDataKey.Values[0].ToString());
            }
            else if (e.CommandName.ToLower().Trim() == "openselect" && e.CommandArgument.ToString() != "")
            {
                code = ((sender as GridView).Rows[int.Parse(e.CommandArgument.ToString())].FindControl("SupplierLabel") as Label).Text.Replace("<p/>", " ").Split(' ').First();
                externalCode = ((sender as GridView).Rows[int.Parse(e.CommandArgument.ToString())].FindControl("lblCode") as Label).Text.Replace("<p/>", " ").Split(' ').First();
                returnUrlQueryString = string.Format("returnUrl={0}&supplierCode={1}&code={2} ", Server.UrlEncode(Request.AppRelativeCurrentExecutionFilePath), code, externalCode);
                Response.Redirect(string.Format("{0}?{1}", Application["TaxMatchingPage"].ToString().TrimEnd(), returnUrlQueryString), true);
            }
        }

        protected void gvTaxesMatching_RowCreated(object sender, GridViewRowEventArgs e)
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

        protected void gvTaxesMatching_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvTaxesMatching.PageIndex = e.NewPageIndex;

        }

        protected void gvTaxesMatching_PageIndexChanged(object sender, EventArgs e)
        {
            RefreshGridView();
        }

        protected void PageFooter_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

            if (e.CommandName == "ChangePage")
            {
                gvTaxesMatching.PageIndex = Convert.ToInt32(e.CommandArgument) - 1;
            }


        }

        protected void btnPage_Click(object sender, EventArgs e)
        {

            if (((LinkButton)sender).ID.ToString() == "btnBackward")
            {
                if (gvTaxesMatching.PageIndex == 0)
                {
                    return;
                }
                gvTaxesMatching.PageIndex = gvTaxesMatching.PageIndex - 1;
            }
            else
            {
                if (gvTaxesMatching.PageIndex == gvTaxesMatching.PageCount - 1)
                {
                    return;
                }
                gvTaxesMatching.PageIndex = gvTaxesMatching.PageIndex + 1;
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

            Response.Redirect(string.Format("{0}?{1}", Application["TaxMatchingPage"].ToString().TrimEnd(), returnUrlQueryString), true);
        }

        protected void btnUpdateElement_Click(object sender, EventArgs e)
        {
            string returnUrlQueryString;

            if (ViewState["SelectedTaxMatching"] == null)
            {
                this.MessageUC.ShowError("QuotationWarning", "Must select a brand matching to update");//traduzir
                return;
            }



            returnUrlQueryString = string.Format("returnUrl={0}&supplierCode={1}&code={2} ", Server.UrlEncode(Request.AppRelativeCurrentExecutionFilePath), ((WhereToBuy.entities.TaxMatching)ViewState["SelectedTaxMatching"]).Supplier.Code, ((WhereToBuy.entities.TaxMatching)ViewState["SelectedTaxMatching"]).Code);
            //if (Request.QueryString.Count > 0)
            //{
            //    returnUrlQueryString += Server.UrlEncode(string.Format("?{0}", Request.QueryString.ToString()));
            //}
            Response.Redirect(string.Format("{0}?{1}", Application["TaxMatchingPage"].ToString().TrimEnd(), returnUrlQueryString), true);
        }

        protected void btnClean_Click(object sender, EventArgs e)
        {
            ClearFilter();

        }


        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SetSelectedMatching(null);
            gvTaxesMatching.PageIndex = 0;
            gvTaxesMatching.SelectedIndex = -1;
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