using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WhereToBuy.web.UserControls;
using WhereToBuy.web.UserControls.Suppliers.SuppliersSelBox;
using WhereToBuy.web.UserControls.WarningTypes.WarningTypesSelBox;

namespace WhereToBuy.web.App.QuotationWarnings
{
    public partial class QuotationWarnings : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ((MessageUC)(this.QuotationWarningsUC.FindControl("MessageUC"))).SubmitButtonClick += QuotationWarnings_MessageButton;
            ((SuppliersSelBox)(QuotationWarningsUC.FindControl("SuppliersSelBox"))).SubmitButtonClick += QuotationWarningsSuppliers_ClickButton;
            ((WarningTypesSelBox)(QuotationWarningsUC.FindControl("WarningTypesSelBox"))).SubmitButtonClick += QuotationWarningsTypes_ClickButton;


            // Autentication validation
            if (Session["ActualUser"] == null)
            {
                string returnUrlQueryString;

                returnUrlQueryString = string.Format("returnUrl={0}", Server.UrlEncode(Request.AppRelativeCurrentExecutionFilePath));
                if (Request.QueryString.Count > 0)
                {
                    returnUrlQueryString += Server.UrlEncode(string.Format("?{0}", Request.QueryString.ToString()));
                }

                return;
            }


        }


        private void QuotationWarningsTypes_ClickButton(object sender, WarningTypeSelBoxEventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "sp", "invokeDummyButton('" + "WarningTypes" + "');", true);
        }

        private void QuotationWarningsSuppliers_ClickButton(object sender, SupplierSelBoxEventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "sp", "invokeDummyButton('" + "Suppliers" + "');", true);
        }



        private void QuotationWarnings_MessageButton(object sender, EventArgs e)
        {
            //string btn = ((Button)sender).ClientID;

            ScriptManager.RegisterStartupScript(this, this.GetType(), "bdc", "invokeButtonClick();", true);
        }
    }
}