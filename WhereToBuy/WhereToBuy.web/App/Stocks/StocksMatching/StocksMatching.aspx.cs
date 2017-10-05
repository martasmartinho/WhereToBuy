using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WhereToBuy.entities;
using WhereToBuy.web.UserControls;
using WhereToBuy.web.UserControls.Suppliers.SuppliersSelBox;

namespace WhereToBuy.web.App.Stocks.StocksMatching
{
    public partial class StocksMatching : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ((MessageUC)(StocksMatchingUC.FindControl("MessageUC"))).SubmitButtonClick += StocksMatching_MessageButton;
            ((SuppliersSelBox)(StocksMatchingUC.FindControl("SuppliersSelBox"))).SubmitButtonClick += StocksMatching_ClickButton;

            // Autentication validation
            if (Session["ActualUser"] == null)
            {
                string returnUrlQueryString;

                returnUrlQueryString = string.Format("returnUrl={0}", Server.UrlEncode(Request.AppRelativeCurrentExecutionFilePath));
                if (Request.QueryString.Count > 0)
                {
                    returnUrlQueryString += Server.UrlEncode(string.Format("?{0}", Request.QueryString.ToString()));
                }

                //Response.Redirect(string.Format("{0}?{1}", Application["Default"].ToString().TrimEnd(), returnUrlQueryString), true);
                return;
            }


            // Load page
            if (!Page.IsPostBack)
            {
                string code = string.Empty;
                string supplierCode = string.Empty;
                DataState dataState = DataState.None;

                if (Page.Request.QueryString["Code"] != null && Page.Request.QueryString["SupplierCode"] != null)
                {
                    code = Page.Request.QueryString["Code"].ToString().TrimEnd();
                    supplierCode = Page.Request.QueryString["SupplierCode"].ToString().TrimEnd();
                }


                // load data
                StocksMatchingUC.UpdateData(supplierCode, code, dataState);



            }
        }

        private void StocksMatching_ClickButton(object sender, SupplierSelBoxEventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "sp", "invokeDummyButton('" + "Suppliers" + "');", true);
        }

        private void StocksMatching_MessageButton(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ic", "invokeButtonClick();", true);
        }
    }
}