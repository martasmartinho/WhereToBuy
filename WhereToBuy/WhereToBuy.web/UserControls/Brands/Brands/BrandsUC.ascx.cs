using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WhereToBuy.web.UserControls.Brands.Brands
{
    public partial class BrandsUC : System.Web.UI.UserControl
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            UpdatePanel1.Update();
        }


        #region Grid


        protected void gvBrands_Sorting(object sender, GridViewSortEventArgs e)
        {
            if (ViewState["BrandOrderBy"].ToString().TrimEnd().ToLower() != e.SortExpression.ToString().TrimEnd().ToLower())
            {
                ViewState["BrandOrderBy"] = e.SortExpression.ToString().TrimEnd();
                ViewState["BrandOrderByType"] = "ASC";
            }

            if (ViewState["BrandOrderByType"].ToString().TrimEnd() == "ASC")
            {
                ViewState["BrandOrderByType"] = "DESC";
            }
            else
            {
                ViewState["BrandOrderByType"] = "ASC";
            }

            RefreshGridView();
            UpdatePanel1.Update();
        }


        #region RowItem


        protected void gvBrands_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            LinkButton linkSelect;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                linkSelect = (LinkButton)e.Row.FindControl("lnkSelect");
                e.Row.Attributes["onclick"] = this.Page.ClientScript.GetPostBackEventReference(linkSelect, "");
            }
        }
      

        protected void gvBrands_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string returnUrlQueryString;
            string code;

            if (e.CommandName.ToLower().Trim() == "select")
            {
                gvBrands.SelectedIndex = int.Parse(e.CommandArgument.ToString());
                LoadObject(gvBrands.SelectedDataKey.Values[0].ToString());
            }
            else if (e.CommandName.ToLower().Trim() == "openselect" && e.CommandArgument.ToString() != "")
            {
                code = ((sender as GridView).Rows[int.Parse(e.CommandArgument.ToString())].FindControl("lblCode") as Label).Text;
                returnUrlQueryString = string.Format("returnUrl={0}&code={1} ", Server.UrlEncode(Request.AppRelativeCurrentExecutionFilePath), code);
                Response.Redirect(string.Format("{0}?{1}", Application["BrandPage"].ToString().TrimEnd(), returnUrlQueryString), true);
            }

        }


        protected void gvBrands_RowCreated(object sender, GridViewRowEventArgs e)
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


        protected void gvBrands_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvBrands.PageIndex = e.NewPageIndex;
        }


        protected void gvBrands_PageIndexChanged(object sender, EventArgs e)
        {
            RefreshGridView();
        }


        protected void PageFooter_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "ChangePage")
            {
                gvBrands.PageIndex = Convert.ToInt32(e.CommandArgument) - 1;
            }
        }


        protected void btnPage_Click(object sender, EventArgs e)
        {

            if (((LinkButton)sender).ID.ToString() == "btnBackward")
            {
                if (gvBrands.PageIndex == 0)
                {
                    return;
                }
                gvBrands.PageIndex = gvBrands.PageIndex - 1;
            }
            else
            {
                if (gvBrands.PageIndex == gvBrands.PageCount - 1)
                {
                    return;
                }
                gvBrands.PageIndex = gvBrands.PageIndex + 1;
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
            Response.Redirect(string.Format("{0}?{1}", Application["BrandPage"].ToString().TrimEnd(), returnUrlQueryString), true);
        }


        protected void btnUpdateElement_Click(object sender, EventArgs e)
        {
            string returnUrlQueryString;

            if (ViewState["SelectedBrand"] == null)
            {
                this.MessageUC.ShowError("Brand", "Must select a brand to update");//traduzir
                return;
            }

            returnUrlQueryString = string.Format("returnUrl={0}&code={1} ", Server.UrlEncode(Request.AppRelativeCurrentExecutionFilePath), ((WhereToBuy.entities.Brand)ViewState["SelectedBrand"]).Code);
            Response.Redirect(string.Format("{0}?{1}", Application["BrandPage"].ToString().TrimEnd(), returnUrlQueryString), true);
        }


        protected void btnClean_Click(object sender, EventArgs e)
        {
            ClearFilter();
        }


        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SetSelectedBrand(null);
            gvBrands.PageIndex = 0;
            gvBrands.SelectedIndex = -1;
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
                    break;
                case "btnInactive":
                    btnAll.Checked = false;
                    btnActive.Checked = false;
                    break;
                case "btnAll":
                    btnActive.Checked = false;
                    btnInactive.Checked = false;
                    break;
              
                default:
                    btnAll.Checked = false;
                    btnActive.Checked = true;
                    btnInactive.Checked = false;
                    
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