using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WhereToBuy.entities;

namespace WhereToBuy.web.UserControls.Suppliers.Suppliers
{
    public partial class SuppliersUC : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Load page
            if (!Page.IsPostBack)
            {
                string code = string.Empty;
                DataState dataState = DataState.None;

                if (Page.Request.QueryString["Code"] != null)
                {
                    code = Page.Request.QueryString["Code"].ToString().TrimEnd();
                }


                // load data
                UpdateData(code, dataState);



            }
        }



        #region Grid





        protected void gvSuppliers_Sorting(object sender, GridViewSortEventArgs e)
        {
            if (ViewState["SupplierOrderBy"].ToString().TrimEnd().ToLower() != e.SortExpression.ToString().TrimEnd().ToLower())
            {
                ViewState["SupplierOrderBy"] = e.SortExpression.ToString().TrimEnd();
                ViewState["SupplierOrderByType"] = "ASC";
            }

            if (ViewState["SupplierOrderByType"].ToString().TrimEnd() == "ASC")
            {
                ViewState["SupplierOrderByType"] = "DESC";
            }
            else
            {
                ViewState["SupplierOrderByType"] = "ASC";
            }

            RefreshGridView();
            UpdatePanel1.Update();
        }




        #region RowItem


        protected void gvSuppliers_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            LinkButton linkSelect;
            Panel panelExtraInformation;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                linkSelect = (LinkButton)e.Row.FindControl("lnkSelect");
                e.Row.Attributes["onclick"] = this.Page.ClientScript.GetPostBackEventReference(linkSelect, "");

                panelExtraInformation = (Panel)e.Row.FindControl("pnlExtraInformation");
                panelExtraInformation.Visible = false;
            }

        }




        protected void gvSuppliers_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName.ToLower().Trim() == "select")
            {

                if (gvSuppliers.SelectedIndex != int.Parse(e.CommandArgument.ToString()))
                {
                    gvSuppliers.SelectedIndex = int.Parse(e.CommandArgument.ToString());

                    LoadObject(gvSuppliers.SelectedDataKey.Values[0].ToString());
                }

            }

            if (e.CommandName.ToLower().Trim() != "sort" && e.CommandArgument.ToString() != "" && int.Parse(e.CommandArgument.ToString()) != -1)
            {
                if (e.CommandName.ToLower().Trim() == "show")
                {
                    ((sender as GridView).Rows[int.Parse(e.CommandArgument.ToString())].FindControl("btnShow") as LinkButton).Visible = false;
                    ((sender as GridView).Rows[int.Parse(e.CommandArgument.ToString())].FindControl("pnlExtraInformation") as Panel).Visible = true;
                    ((sender as GridView).Rows[int.Parse(e.CommandArgument.ToString())].FindControl("btnHidde") as LinkButton).Visible = true;
                    gvSuppliers.SelectedIndex = int.Parse(e.CommandArgument.ToString());
                }
                else if (e.CommandName.ToLower().Trim() == "hidde")
                {
                    ((sender as GridView).Rows[int.Parse(e.CommandArgument.ToString())].FindControl("btnHidde") as LinkButton).Visible = false;
                    ((sender as GridView).Rows[int.Parse(e.CommandArgument.ToString())].FindControl("pnlExtraInformation") as Panel).Visible = false;
                    ((sender as GridView).Rows[int.Parse(e.CommandArgument.ToString())].FindControl("btnShow") as LinkButton).Visible = true;
                    gvSuppliers.SelectedIndex = int.Parse(e.CommandArgument.ToString());
                }
                else if (e.CommandName.ToLower().Trim() == "openselect")
                {
                    string code = ((sender as GridView).Rows[int.Parse(e.CommandArgument.ToString())].FindControl("lblCode") as Label).Text;
                    string returnUrlQueryString;
                    returnUrlQueryString = string.Format("returnUrl={0}&code={1} ", Server.UrlEncode(Request.AppRelativeCurrentExecutionFilePath), code);
                    Response.Redirect(string.Format("{0}?{1}", Application["SupplierPage"].ToString().TrimEnd(), returnUrlQueryString), true);
                }
            }
        }


        protected void gvSuppliers_RowCreated(object sender, GridViewRowEventArgs e)
        {
            LinkButton linkSelect;
            LinkButton btnSelect;
            LinkButton btnShow;
            LinkButton btnHidde;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                linkSelect = (LinkButton)e.Row.FindControl("lnkSelect");
                linkSelect.CommandArgument = e.Row.RowIndex.ToString();

                btnSelect = (LinkButton)e.Row.FindControl("btnSelect");
                btnSelect.CommandArgument = e.Row.RowIndex.ToString();

                btnShow = (LinkButton)e.Row.FindControl("btnShow");
                btnShow.CommandArgument = e.Row.RowIndex.ToString();

                btnHidde = (LinkButton)e.Row.FindControl("btnHidde");
                btnHidde.CommandArgument = e.Row.RowIndex.ToString();
            }
        }



        protected void gvSuppliers_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {



        }


        protected void gvSuppliers_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        #endregion




        #region Pager

        protected void gvSuppliers_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvSuppliers.PageIndex = e.NewPageIndex;

        }

        protected void gvSuppliers_PageIndexChanged(object sender, EventArgs e)
        {
            RefreshGridView();
        }

        protected void PageFooter_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

            if (e.CommandName == "ChangePage")
            {
                gvSuppliers.PageIndex = Convert.ToInt32(e.CommandArgument) - 1;
            }


        }

        protected void btnPage_Click(object sender, EventArgs e)
        {

            if (((LinkButton)sender).ID.ToString() == "btnBackward")
            {
                if (gvSuppliers.PageIndex == 0)
                {
                    return;
                }
                gvSuppliers.PageIndex = gvSuppliers.PageIndex - 1;
            }
            else
            {
                if (gvSuppliers.PageIndex == gvSuppliers.PageCount - 1)
                {
                    return;
                }
                gvSuppliers.PageIndex = gvSuppliers.PageIndex + 1;
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

            Response.Redirect(string.Format("{0}?{1}", Application["SupplierPage"].ToString().TrimEnd(), returnUrlQueryString), true);
        }

        protected void btnUpdateElement_Click(object sender, EventArgs e)
        {
            string returnUrlQueryString;

            if (!SelectedSupplierExist)
            {
                this.MessageUC.ShowError("QuotationWarning", "Must select a supplier to update");//traduzir
                return;
            }



            returnUrlQueryString = string.Format("returnUrl={0}&code={1} ", Server.UrlEncode(Request.AppRelativeCurrentExecutionFilePath), ((WhereToBuy.entities.Supplier)ViewState["SelectedSupplier"]).Code);
            //if (Request.QueryString.Count > 0)
            //{
            //    returnUrlQueryString += Server.UrlEncode(string.Format("?{0}", Request.QueryString.ToString()));
            //}
            Response.Redirect(string.Format("{0}?{1}", Application["SupplierPage"].ToString().TrimEnd(), returnUrlQueryString), true);
        }

        protected void btnClean_Click(object sender, EventArgs e)
        {
            ClearFilter();

        }


        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SetSelectedSupplier(null);
            gvSuppliers.PageIndex = 0;
            gvSuppliers.SelectedIndex = -1;
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