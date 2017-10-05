using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WhereToBuy.entities;
using WhereToBuy.utils.GlobalVariables;
using WhereToBuy.web.UserControls.Suppliers.SuppliersSelBox;

namespace WhereToBuy.web.UserControls.BrandsMatching
{


    public partial class BrandsMatchingUC : System.Web.UI.UserControl
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            SuppliersSelBox.SelectedSupplierUpdate += BrandsMatchingUC_SelectedSupplierUpdate;
            SuppliersSelBox.SupplierSelBoxMessage += BrandsMatchingUC_SupplierSelBoxMessage;
        }


        #region Grid


        protected void BrandMatchingGridView_Sorting(object sender, GridViewSortEventArgs e)
        {
            if (ViewState["BrandMatchingOrderBy"].ToString().TrimEnd().ToLower() != e.SortExpression.ToString().TrimEnd().ToLower())
            {
                ViewState["BrandMatchingOrderBy"] = e.SortExpression.ToString().TrimEnd();
                ViewState["BrandMatchingOrderByType"] = "ASC";
            }

            if (ViewState["BrandMatchingOrderByType"].ToString().TrimEnd() == "ASC")
            {
                ViewState["BrandMatchingOrderByType"] = "DESC";
            }
            else
            {
                ViewState["BrandMatchingOrderByType"] = "ASC";
            }

            RefreshGridView();
            UpdatePanel1.Update();
        }


        #region RowItem


        protected void BrandMatchingGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            LinkButton linkSelect;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                linkSelect = (LinkButton)e.Row.FindControl("lnkSelect");
                e.Row.Attributes["onclick"] = this.Page.ClientScript.GetPostBackEventReference(linkSelect, "");
            }
        }


        protected void BrandMatchingGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string returnUrlQueryString;
            string code;
            string externalCode;

            if (e.CommandName.ToLower().Trim() == "select")
            {
                BrandMatchingGridView.SelectedIndex = int.Parse(e.CommandArgument.ToString());
                LoadBrandMatching(BrandMatchingGridView.SelectedDataKey.Values[1] as Supplier, BrandMatchingGridView.SelectedDataKey.Values[0].ToString());
            }
            else if (e.CommandName.ToLower().Trim() == "openselect" && e.CommandArgument.ToString() != "")
            {
                code = ((sender as GridView).Rows[int.Parse(e.CommandArgument.ToString())].FindControl("SupplierLabel") as Label).Text.Replace("<p/>", " ").Split(' ').First();
                externalCode = ((sender as GridView).Rows[int.Parse(e.CommandArgument.ToString())].FindControl("CodeLabel") as Label).Text.Replace("<p/>", " ").Split(' ').First();
                returnUrlQueryString = string.Format("returnUrl={0}&supplierCode={1}&code={2} ", Server.UrlEncode(Request.AppRelativeCurrentExecutionFilePath), code, externalCode);
                Response.Redirect(string.Format("{0}?{1}", Application["BrandMatchingPage"].ToString().TrimEnd(), returnUrlQueryString), true);
            }
        }


        protected void BrandMatchingGridView_RowCreated(object sender, GridViewRowEventArgs e)
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


        protected void BrandMatchingGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            BrandMatchingGridView.PageIndex = e.NewPageIndex;
        }


        protected void BrandMatchingGridView_PageIndexChanged(object sender, EventArgs e)
        {
            RefreshGridView();
        }


        protected void PageFooter_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "ChangePage")
            {
                BrandMatchingGridView.PageIndex = Convert.ToInt32(e.CommandArgument) - 1;
            }
        }


        protected void PageButton_Click(object sender, EventArgs e)
        {
            if (((LinkButton)sender).ID.ToString() == "BackwardButton")
            {
                if (BrandMatchingGridView.PageIndex == 0 )
                {
                    return;
                }
                BrandMatchingGridView.PageIndex = BrandMatchingGridView.PageIndex - 1;
            }
            else
            {
                if (BrandMatchingGridView.PageIndex == BrandMatchingGridView.PageCount - 1)
                {
                    return;
                }
                BrandMatchingGridView.PageIndex = BrandMatchingGridView.PageIndex + 1;
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
            
            Response.Redirect(string.Format("{0}?{1}", Application["BrandMatchingPage"].ToString().TrimEnd(), returnUrlQueryString), true);
        }

        protected void UpdateElementButton_Click(object sender, EventArgs e)
        {
            string returnUrlQueryString;

            if (ViewState["SelectedBrandMatching"] == null)
            {
                this.MessageUC.ShowError("QuotationWarning", "Must select a brand matching to update");//traduzir
                return;
            }



            returnUrlQueryString = string.Format("returnUrl={0}&supplierCode={1}&code={2} ", Server.UrlEncode(Request.AppRelativeCurrentExecutionFilePath), ((BrandMatching)ViewState["SelectedBrandMatching"]).Supplier.Code, ((BrandMatching)ViewState["SelectedBrandMatching"]).Code);
            //if (Request.QueryString.Count > 0)
            //{
            //    returnUrlQueryString += Server.UrlEncode(string.Format("?{0}", Request.QueryString.ToString()));
            //}
            Response.Redirect(string.Format("{0}?{1}", Application["BrandMatchingPage"].ToString().TrimEnd(), returnUrlQueryString), true);
        }

        protected void btnClean_Click(object sender, EventArgs e)
        {
            ClearFilter();
            
        }


        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SetSelectedMatching(null);
            BrandMatchingGridView.PageIndex = 0;
            BrandMatchingGridView.SelectedIndex = -1; 
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
                    break;
                case "InactiveRadioButton":
                    AllRadioButton.Checked = false;
                    ActiveRadioButton.Checked = false;
                    MatchingRadioButton.Checked = false;
                    break;
                case "AllRadioButton":
                    ActiveRadioButton.Checked = false;
                    InactiveRadioButton.Checked = false;
                    MatchingRadioButton.Checked = false;
                    break;
                case "MatchingRadioButton":
                    AllRadioButton.Checked = false;
                    ActiveRadioButton.Checked = false;
                    InactiveRadioButton.Checked = false;
                    break;
                default:
                    AllRadioButton.Checked = false;
                    ActiveRadioButton.Checked = false;
                    InactiveRadioButton.Checked = false;
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


        private void BrandsMatchingUC_SupplierSelBoxMessage(object sender, SupplierSelBoxEventArgs e)
        {
            this.MessageUC.ShowError("Error", e.Message);
            return;
        }


        private void BrandsMatchingUC_SelectedSupplierUpdate(object sender, SupplierSelBoxEventArgs e)
        {
            SetSelectedSupplier(e.Supplier);
        }

      
        #endregion

        

       


    }
}