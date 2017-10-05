using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WhereToBuy.entities;

namespace WhereToBuy.web.UserControls.QuotationRules.QuotationRules
{
    public partial class QuotationRulesUC : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SuppliersSelBox.SelectedSupplierUpdate += QuotationRulesUC_SelectedSupplierUpdate;
            SuppliersSelBox.SupplierSelBoxMessage += QuotationRulesUC_SupplierSelBoxMessage;
            CategoriesSelBox.SelectedCategoryUpdate += QuotationRulesUC_SelectedCategoryUpdate;
            CategoriesSelBox.CategoriesSelBoxMessage += QuotationRulesUC_CategorySelBoxMessage;
            BrandsSelBox.SelectedBrandUpdate += QuotationRulesUC_SelectedBrandUpdate;
            BrandsSelBox.BrandSelBoxMessage += QuotationRulesUC_BrandSelBoxMessage;
            StocksSelBox.SelectedStockUpdate += QuotationRulesUC_SelectedStockUpdate;
            StocksSelBox.StocksSelBoxMessage += QuotationRulesUC_StockSelBoxMessage;

            // Load page
            if (!Page.IsPostBack)
            {
                // load data
                UpdateData();

            }
        }

        private void QuotationRulesUC_StockSelBoxMessage(object sender, Stocks.StocksSelBox.StocksSelBoxEventArgs e)
        {
            this.MessageUC.ShowError("Error", e.Message);
            return;
        }

        private void QuotationRulesUC_SelectedStockUpdate(object sender, Stocks.StocksSelBox.StocksSelBoxEventArgs e)
        {
            SetSelectedStock(e.Stock);
        }

        private void QuotationRulesUC_BrandSelBoxMessage(object sender, Brands.BrandsSelBox.BrandSelBoxEventArgs e)
        {
            this.MessageUC.ShowError("Error", e.Message);
            return;
        }

        private void QuotationRulesUC_SelectedBrandUpdate(object sender, Brands.BrandsSelBox.BrandSelBoxEventArgs e)
        {
            SetSelectedBrand(e.Brand);
        }

        private void QuotationRulesUC_CategorySelBoxMessage(object sender, Categories.CategoriesSelBox.CategoriesSelBoxEventArgs e)
        {
            this.MessageUC.ShowError("Error", e.Message);
            return;
        }

        private void QuotationRulesUC_SelectedCategoryUpdate(object sender, Categories.CategoriesSelBox.CategoriesSelBoxEventArgs e)
        {
            SetSelectedCategory(e.Category);
        }

        private void QuotationRulesUC_SupplierSelBoxMessage(object sender, Suppliers.SuppliersSelBox.SupplierSelBoxEventArgs e)
        {
            this.MessageUC.ShowError("Error", e.Message);
            return;
        }

        private void QuotationRulesUC_SelectedSupplierUpdate(object sender, Suppliers.SuppliersSelBox.SupplierSelBoxEventArgs e)
        {
            SetSelectedSupplier(e.Supplier);
        }



        #region Grid


        protected void gvQuotationRules_Sorting(object sender, GridViewSortEventArgs e)
        {
            if (ViewState["QuotationRuleOrderBy"].ToString().TrimEnd().ToLower() != e.SortExpression.ToString().TrimEnd().ToLower())
            {
                ViewState["QuotationRuleOrderBy"] = e.SortExpression.ToString().TrimEnd();
                ViewState["QuotationRuleOrderByType"] = "ASC";
            }

            if (ViewState["QuotationRuleOrderByType"].ToString().TrimEnd() == "ASC")
            {
                ViewState["QuotationRuleOrderByType"] = "DESC";
            }
            else
            {
                ViewState["QuotationRuleOrderByType"] = "ASC";
            }
            RefreshGridView();
            UpdatePanel1.Update();
        }




        #region RowItem


        protected void gvQuotationRules_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            LinkButton linkSelect;
          
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                linkSelect = (LinkButton)e.Row.FindControl("lnkSelect");
                e.Row.Attributes["onclick"] = this.Page.ClientScript.GetPostBackEventReference(linkSelect, "");
            }

        }




        protected void gvQuotationRules_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string returnUrlQueryString;
            string[] code;

            if (e.CommandName.ToLower().Trim() == "select")
            {
                if (gvQuotationRules.SelectedIndex != int.Parse(e.CommandArgument.ToString()))
                {
                    gvQuotationRules.SelectedIndex = int.Parse(e.CommandArgument.ToString());

                    LoadObject((Supplier)gvQuotationRules.SelectedDataKey.Values[0], (Category)gvQuotationRules.SelectedDataKey.Values[1], 
                               (Brand)gvQuotationRules.SelectedDataKey.Values[2], (Stock)gvQuotationRules.SelectedDataKey.Values[3]);
                }

            }
            else if (e.CommandName.ToLower().Trim() == "openselect" && e.CommandArgument.ToString() != "")
            {
                code = e.CommandArgument.ToString().Split(';');
                returnUrlQueryString = string.Format("returnUrl={0}&supplier={1}&category={2}&brand={3}&stock={4} ", Server.UrlEncode(Request.AppRelativeCurrentExecutionFilePath),
                                                                                                                Server.UrlEncode(code[0]),
                                                                                                                Server.UrlEncode(code[2]),
                                                                                                                Server.UrlEncode(code[1]),
                                                                                                                Server.UrlEncode(code[3]));
                Response.Redirect(string.Format("{0}?{1}", Application["QuotationRulePage"].ToString().TrimEnd(), returnUrlQueryString), true);
            }


        }


        protected void gvQuotationRules_RowCreated(object sender, GridViewRowEventArgs e)
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



        protected void gvQuotationRules_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {



        }


        protected void gvQuotationRules_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        #endregion




        #region Pager

        protected void gvQuotationRules_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvQuotationRules.PageIndex = e.NewPageIndex;

        }

        protected void gvQuotationRules_PageIndexChanged(object sender, EventArgs e)
        {
            RefreshGridView();
        }

        protected void PageFooter_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

            if (e.CommandName == "ChangePage")
            {
                gvQuotationRules.PageIndex = Convert.ToInt32(e.CommandArgument) - 1;
            }


        }

        protected void btnPage_Click(object sender, EventArgs e)
        {

            if (((LinkButton)sender).ID.ToString() == "btnBackward")
            {
                if (gvQuotationRules.PageIndex == 0)
                {
                    return;
                }
                gvQuotationRules.PageIndex = gvQuotationRules.PageIndex - 1;
            }
            else
            {
                if (gvQuotationRules.PageIndex == gvQuotationRules.PageCount - 1)
                {
                    return;
                }
                gvQuotationRules.PageIndex = gvQuotationRules.PageIndex + 1;
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

            Response.Redirect(string.Format("{0}?{1}", Application["QuotationRulePage"].ToString().TrimEnd(), Server.UrlEncode(returnUrlQueryString)), true);
        }

        protected void btnUpdateElement_Click(object sender, EventArgs e)
        {
            string returnUrlQueryString;

            if (!SelectedQuotationRuleExist)
            {
                this.MessageUC.ShowError("QuotationWarning", "Must select a rule to update");//traduzir
                return;
            }



            returnUrlQueryString = string.Format("returnUrl={0}&supplier={1}&category={2}&brand={3}&stock={4} ", Server.UrlEncode(Request.AppRelativeCurrentExecutionFilePath),
                                                                                                                ((WhereToBuy.entities.QuotationRule)ViewState["SelectedQuotationRule"]).Supplier.Code,
                                                                                                                ((WhereToBuy.entities.QuotationRule)ViewState["SelectedQuotationRule"]).Category.Code,
                                                                                                                ((WhereToBuy.entities.QuotationRule)ViewState["SelectedQuotationRule"]).Brand.Code,
                                                                                                                Server.UrlEncode(((WhereToBuy.entities.QuotationRule)ViewState["SelectedQuotationRule"]).Stock.Code));
            //if (Request.QueryString.Count > 0)
            //{
            //    returnUrlQueryString += Server.UrlEncode(string.Format("?{0}", Request.QueryString.ToString()));
            //}
            Response.Redirect(string.Format("{0}?{1}", Application["QuotationRulePage"].ToString().TrimEnd(), returnUrlQueryString), true);
        }

        protected void btnClean_Click(object sender, EventArgs e)
        {
            ClearFilter();

        }


        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SetSelectedQuotationRule(null);
            gvQuotationRules.PageIndex = 0;
            gvQuotationRules.SelectedIndex = -1;
            RefreshGridView();
            UpdatePanel1.Update();

        }



        protected void RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            switch ((sender as RadioButton).ID)
            {

                case "btnbtnCustomization":
                    btnAll.Checked = false;
                    btnReset.Checked = false;
                    break;
                case "btnReset":
                    btnAll.Checked = false;
                    btnCustomization.Checked = false;
                    break;
                case "btnAll":
                    btnReset.Checked = false;
                    btnCustomization.Checked = false;
                    break;

                default:
                    btnAll.Checked = false;
                    btnCustomization.Checked = true;
                    btnReset.Checked = false;

                    break;
            }

        }


        #endregion

        #region Links

        #endregion

        #region TextBox

        #endregion
    }
}