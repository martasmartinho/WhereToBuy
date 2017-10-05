using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WhereToBuy.web.UserControls.QuotationWarnings
{
    public partial class QuotationWarningsUC : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SuppliersSelBox.SelectedSupplierUpdate += QuotationWarningsUC_SelectedSupplierUpdate;
            SuppliersSelBox.SupplierSelBoxMessage += QuotationWarningsUC_SupplierSelBoxMessage;
            WarningTypesSelBox.SelectedWarningTypeUpdate += QuotationWarningsUC_SelectedWarningTypeUpdate;
            WarningTypesSelBox.WarningTypeSelBoxMessage += QuotationWarningsUC_WarningTypeSelBoxMessage;

            // Load page
            if (!Page.IsPostBack)
            {
                // load data
                UpdateData();

            }
        }


        private void QuotationWarningsUC_WarningTypeSelBoxMessage(object sender, WarningTypes.WarningTypesSelBox.WarningTypeSelBoxEventArgs e)
        {
            this.MessageUC.ShowError("Error", e.Message);
            return;
        }

        private void QuotationWarningsUC_SelectedWarningTypeUpdate(object sender, WarningTypes.WarningTypesSelBox.WarningTypeSelBoxEventArgs e)
        {
            SetSelectedWarningType(e.WarningType);
        }

        private void QuotationWarningsUC_SupplierSelBoxMessage(object sender, Suppliers.SuppliersSelBox.SupplierSelBoxEventArgs e)
        {
            this.MessageUC.ShowError("Error", e.Message);
            return;
        }

        private void QuotationWarningsUC_SelectedSupplierUpdate(object sender, Suppliers.SuppliersSelBox.SupplierSelBoxEventArgs e)
        {
            SetSelectedSupplier(e.Supplier);
        }



        #region Grid


        protected void gvQuotationWarnings_Sorting(object sender, GridViewSortEventArgs e)
        {
            if (ViewState["QuotationWarningOrderBy"].ToString().TrimEnd().ToLower() != e.SortExpression.ToString().TrimEnd().ToLower())
            {
                ViewState["QuotationWarningOrderBy"] = e.SortExpression.ToString().TrimEnd();
                ViewState["QuotationWarningOrderByType"] = "ASC";
            }

            if (ViewState["QuotationWarningOrderByType"].ToString().TrimEnd() == "ASC")
            {
                ViewState["QuotationWarningOrderByType"] = "DESC";
            }
            else
            {
                ViewState["QuotationWarningOrderByType"] = "ASC";
            }
            RefreshGridView();
            UpdatePanel1.Update();
        }




        #region RowItem


        protected void gvQuotationWarnings_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            LinkButton linkSelect;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                linkSelect = (LinkButton)e.Row.FindControl("lnkSelect");
                e.Row.Attributes["onclick"] = this.Page.ClientScript.GetPostBackEventReference(linkSelect, "");
            }

        }




        protected void gvQuotationWarnings_RowCommand(object sender, GridViewCommandEventArgs e)
        {
           
            if (e.CommandName.ToLower().Trim() == "select")
            {
                if (gvQuotationWarnings.SelectedIndex != int.Parse(e.CommandArgument.ToString()))
                {
                    gvQuotationWarnings.SelectedIndex = int.Parse(e.CommandArgument.ToString());

                    //LoadObject((Supplier)gvQuotationWarnings.SelectedDataKey.Values[0], (Category)gvQuotationWarnings.SelectedDataKey.Values[1],
                    //           (Brand)gvQuotationWarnings.SelectedDataKey.Values[2], (Stock)gvQuotationWarnings.SelectedDataKey.Values[3]);
                }

            }
        }


        protected void gvQuotationWarnings_RowCreated(object sender, GridViewRowEventArgs e)
        {
            LinkButton linkSelect;
            


            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                linkSelect = (LinkButton)e.Row.FindControl("lnkSelect");
                linkSelect.CommandArgument = e.Row.RowIndex.ToString();


            }
        }



      

        #endregion




        #region Pager

        protected void gvQuotationWarnings_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvQuotationWarnings.PageIndex = e.NewPageIndex;

        }

        protected void gvQuotationWarnings_PageIndexChanged(object sender, EventArgs e)
        {
            RefreshGridView();
        }

        protected void PageFooter_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

            if (e.CommandName == "ChangePage")
            {
                gvQuotationWarnings.PageIndex = Convert.ToInt32(e.CommandArgument) - 1;
            }


        }

        protected void btnPage_Click(object sender, EventArgs e)
        {

            if (((LinkButton)sender).ID.ToString() == "btnBackward")
            {
                if (gvQuotationWarnings.PageIndex == 0)
                {
                    return;
                }
                gvQuotationWarnings.PageIndex = gvQuotationWarnings.PageIndex - 1;
            }
            else
            {
                if (gvQuotationWarnings.PageIndex == gvQuotationWarnings.PageCount - 1)
                {
                    return;
                }
                gvQuotationWarnings.PageIndex = gvQuotationWarnings.PageIndex + 1;
            }

            RefreshGridView();


        }


        #endregion

        #region Others


        #endregion

        #endregion

        #region Buttons


    

        protected void btnClean_Click(object sender, EventArgs e)
        {
            ClearFilter();

        }


        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SetSelectedQuotationWarning(null);
            gvQuotationWarnings.PageIndex = 0;
            gvQuotationWarnings.SelectedIndex = -1;
            RefreshGridView();
            UpdatePanel1.Update();

        }




        #endregion

        #region Links

        #endregion

        #region TextBox

        #endregion
    }
}