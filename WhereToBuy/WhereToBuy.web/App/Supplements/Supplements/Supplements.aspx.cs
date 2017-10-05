using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WhereToBuy.web.UserControls;

namespace WhereToBuy.web.App.Supplements.Supplements
{
    public partial class Supplements : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            ((MessageUC)(SupplementsUC.FindControl("MessageUC"))).SubmitButtonClick += Supplements_MessageButton;


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


        }



        private void Supplements_MessageButton(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ic", "invokeButtonClick();", true);
        }
    }
}