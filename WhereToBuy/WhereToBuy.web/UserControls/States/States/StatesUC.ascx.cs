using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WhereToBuy.web.UserControls.States.States
{
    public partial class StatesUC : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        #region Grid





        protected void gvStates_Sorting(object sender, GridViewSortEventArgs e)
        {
            if (ViewState["StateOrderBy"].ToString().TrimEnd().ToLower() != e.SortExpression.ToString().TrimEnd().ToLower())
            {
                ViewState["StateOrderBy"] = e.SortExpression.ToString().TrimEnd();
                ViewState["StateOrderByType"] = "ASC";
            }

            if (ViewState["StateOrderByType"].ToString().TrimEnd() == "ASC")
            {
                ViewState["StateOrderByType"] = "DESC";
            }
            else
            {
                ViewState["StateOrderByType"] = "ASC";
            }

            RefreshGridView();
            UpdatePanel1.Update();
        }




        #region RowItem


        protected void gvStates_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            LinkButton linkSelect;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                linkSelect = (LinkButton)e.Row.FindControl("lnkSelect");
                e.Row.Attributes["onclick"] = this.Page.ClientScript.GetPostBackEventReference(linkSelect, "");

            }

        }




        protected void gvStates_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string returnUrlQueryString;
            string code;

            if (e.CommandName.ToLower().Trim() == "select")
            {
                gvStates.SelectedIndex = int.Parse(e.CommandArgument.ToString());
                LoadObject(gvStates.SelectedDataKey.Values[0].ToString());
            }

            if (e.CommandName.ToLower().Trim() == "openselect" && e.CommandArgument.ToString() != "")
            {
                code = ((sender as GridView).Rows[int.Parse(e.CommandArgument.ToString())].FindControl("lblCode") as Label).Text;
                returnUrlQueryString = string.Format("returnUrl={0}&code={1} ", Server.UrlEncode(Request.AppRelativeCurrentExecutionFilePath), code);
                Response.Redirect(string.Format("{0}?{1}", Application["StatePage"].ToString().TrimEnd(), returnUrlQueryString), true);
            }
        }

        protected void gvStates_RowCreated(object sender, GridViewRowEventArgs e)
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

        protected void gvStates_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvStates.PageIndex = e.NewPageIndex;

        }

        protected void gvStates_PageIndexChanged(object sender, EventArgs e)
        {
            RefreshGridView();
        }

        protected void PageFooter_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

            if (e.CommandName == "ChangePage")
            {
                gvStates.PageIndex = Convert.ToInt32(e.CommandArgument) - 1;
            }


        }

        protected void btnPage_Click(object sender, EventArgs e)
        {

            if (((LinkButton)sender).ID.ToString() == "btnBackward")
            {
                if (gvStates.PageIndex == 0)
                {
                    return;
                }
                gvStates.PageIndex = gvStates.PageIndex - 1;
            }
            else
            {
                if (gvStates.PageIndex == gvStates.PageCount - 1)
                {
                    return;
                }
                gvStates.PageIndex = gvStates.PageIndex + 1;
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

            Response.Redirect(string.Format("{0}?{1}", Application["StatePage"].ToString().TrimEnd(), returnUrlQueryString), true);
        }

        protected void btnUpdateElement_Click(object sender, EventArgs e)
        {
            string returnUrlQueryString;

            if (ViewState["SelectedState"] == null)
            {
                this.MessageUC.ShowError("QuotationWarning", "Must select a state to update");//traduzir
                return;
            }



            returnUrlQueryString = string.Format("returnUrl={0}&code={1} ", Server.UrlEncode(Request.AppRelativeCurrentExecutionFilePath), ((WhereToBuy.entities.State)ViewState["SelectedState"]).Code);
            //if (Request.QueryString.Count > 0)
            //{
            //    returnUrlQueryString += Server.UrlEncode(string.Format("?{0}", Request.QueryString.ToString()));
            //}
            Response.Redirect(string.Format("{0}?{1}", Application["StatePage"].ToString().TrimEnd(), returnUrlQueryString), true);
        }

        protected void btnClean_Click(object sender, EventArgs e)
        {
            ClearFilter();

        }


        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SetSelectedState(null);
            gvStates.PageIndex = 0;
            gvStates.SelectedIndex = -1;
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