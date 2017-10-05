using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WhereToBuy.entities;

namespace WhereToBuy.web.UserControls.Supplements.Supplements
{
    public partial class SupplementsUC : System.Web.UI.UserControl
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





        protected void gvSupplements_Sorting(object sender, GridViewSortEventArgs e)
        {
            if (ViewState["SupplementOrderBy"].ToString().TrimEnd().ToLower() != e.SortExpression.ToString().TrimEnd().ToLower())
            {
                ViewState["SupplementOrderBy"] = e.SortExpression.ToString().TrimEnd();
                ViewState["SupplementOrderByType"] = "ASC";
            }

            if (ViewState["SupplementOrderByType"].ToString().TrimEnd() == "ASC")
            {
                ViewState["SupplementOrderByType"] = "DESC";
            }
            else
            {
                ViewState["SupplementOrderByType"] = "ASC";
            }

            RefreshGridView();
            UpdatePanel1.Update();
        }




        #region RowItem


        protected void gvSupplements_RowDataBound(object sender, GridViewRowEventArgs e)
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




        protected void gvSupplements_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName.ToLower().Trim() == "select")
            {

                if (gvSupplements.SelectedIndex != int.Parse(e.CommandArgument.ToString()))
                {
                    gvSupplements.SelectedIndex = int.Parse(e.CommandArgument.ToString());

                    LoadObject(gvSupplements.SelectedDataKey.Values[0].ToString());
                }

            }

            if (e.CommandName.ToLower().Trim() != "sort" && e.CommandArgument.ToString() != "" && int.Parse(e.CommandArgument.ToString()) != -1)
            {
                if (e.CommandName.ToLower().Trim() == "show")
                {

                    ((sender as GridView).Rows[int.Parse(e.CommandArgument.ToString())].FindControl("btnShow") as LinkButton).Visible = false;
                    ((sender as GridView).Rows[int.Parse(e.CommandArgument.ToString())].FindControl("pnlExtraInformation") as Panel).Visible = true;
                    ((sender as GridView).Rows[int.Parse(e.CommandArgument.ToString())].FindControl("btnHidde") as LinkButton).Visible = true;
                    gvSupplements.SelectedIndex = int.Parse(e.CommandArgument.ToString());
                }
                else if (e.CommandName.ToLower().Trim() == "hidde")
                {

                    ((sender as GridView).Rows[int.Parse(e.CommandArgument.ToString())].FindControl("btnHidde") as LinkButton).Visible = false;
                    ((sender as GridView).Rows[int.Parse(e.CommandArgument.ToString())].FindControl("pnlExtraInformation") as Panel).Visible = false;
                    ((sender as GridView).Rows[int.Parse(e.CommandArgument.ToString())].FindControl("btnShow") as LinkButton).Visible = true;
                    gvSupplements.SelectedIndex = int.Parse(e.CommandArgument.ToString());
                }
                else if (e.CommandName.ToLower().Trim() == "openselect")
                {
                    string code = ((sender as GridView).Rows[int.Parse(e.CommandArgument.ToString())].FindControl("lblCode") as Label).Text;
                    string returnUrlQueryString;
                    returnUrlQueryString = string.Format("returnUrl={0}&code={1} ", Server.UrlEncode(Request.AppRelativeCurrentExecutionFilePath), code);
                    Response.Redirect(string.Format("{0}?{1}", Application["SupplementPage"].ToString().TrimEnd(), returnUrlQueryString), true);
                }
            }
        }


        protected void gvSupplements_RowCreated(object sender, GridViewRowEventArgs e)
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



        protected void gvSupplements_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {



        }


        protected void gvSupplements_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        #endregion




        #region Pager

        protected void gvSupplements_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvSupplements.PageIndex = e.NewPageIndex;

        }

        protected void gvSupplements_PageIndexChanged(object sender, EventArgs e)
        {
            RefreshGridView();
        }

        protected void PageFooter_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

            if (e.CommandName == "ChangePage")
            {
                gvSupplements.PageIndex = Convert.ToInt32(e.CommandArgument) - 1;
            }


        }

        protected void btnPage_Click(object sender, EventArgs e)
        {

            if (((LinkButton)sender).ID.ToString() == "btnBackward")
            {
                if (gvSupplements.PageIndex == 0)
                {
                    return;
                }
                gvSupplements.PageIndex = gvSupplements.PageIndex - 1;
            }
            else
            {
                if (gvSupplements.PageIndex == gvSupplements.PageCount - 1)
                {
                    return;
                }
                gvSupplements.PageIndex = gvSupplements.PageIndex + 1;
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

            Response.Redirect(string.Format("{0}?{1}", Application["SupplementPage"].ToString().TrimEnd(), returnUrlQueryString), true);
        }

        protected void btnUpdateElement_Click(object sender, EventArgs e)
        {
            string returnUrlQueryString;

            if (!SelectedSupplementExist)
            {
                this.MessageUC.ShowError("QuotationWarning", "Must select a supplement to update");//traduzir
                return;
            }



            returnUrlQueryString = string.Format("returnUrl={0}&code={1} ", Server.UrlEncode(Request.AppRelativeCurrentExecutionFilePath), ((WhereToBuy.entities.Supplement)ViewState["SelectedSupplement"]).Code);
            //if (Request.QueryString.Count > 0)
            //{
            //    returnUrlQueryString += Server.UrlEncode(string.Format("?{0}", Request.QueryString.ToString()));
            //}
            Response.Redirect(string.Format("{0}?{1}", Application["SupplementPage"].ToString().TrimEnd(), returnUrlQueryString), true);
        }

        protected void btnClean_Click(object sender, EventArgs e)
        {
            ClearFilter();

        }


        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SetSelectedSupplement(null);
            gvSupplements.PageIndex = 0;
            gvSupplements.SelectedIndex = -1;
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

        protected void btnHiddeShow_Click(object sender, EventArgs e)
        {
            //if (!SelectedSupplementExist)
            //{
            //    gvSupplements.SelectedIndex = int.Parse(((LinkButton)sender).CommandArgument.ToString());
            //    LoadObject(gvSupplements.SelectedDataKey.Values[0].ToString());

            //}

            //((LinkButton)sender).Visible = false;


            //Panel res = (gvSupplements.SelectedRow.FindControl("pnlExtraInformation") as Panel);

            //if (((LinkButton)sender).ID.Equals("btnShow"))
            //{
            //    res.Visible = true;
            //    (gvSupplements.SelectedRow.FindControl("btnHidde") as LinkButton).Visible = true;
            //}
            //else
            //{
            //    res.Visible = false;
            //    (gvSupplements.SelectedRow.FindControl("btnShow") as LinkButton).Visible = true;
            //}
        }

        protected void btnSelect_Click(object sender, EventArgs e)
        {
            //    if (!SelectedSupplementExist)
            //    {
            //        gvSupplements.SelectedIndex = int.Parse(((LinkButton)sender).CommandArgument.ToString());
            //        LoadObject(gvSupplements.SelectedDataKey.Values[0].ToString());

            //    }

            //    string returnUrlQueryString;


            //    returnUrlQueryString = string.Format("returnUrl={0}&code={1} ", Server.UrlEncode(Request.AppRelativeCurrentExecutionFilePath), ((WhereToBuy.entities.Supplement)ViewState["SelectedSupplement"]).Code);
            //    Response.Redirect(string.Format("{0}?{1}", Application["SupplementPage"].ToString().TrimEnd(), returnUrlQueryString), true);
        }




        #region Links

        #endregion

        #region TextBox

        #endregion
    }
}