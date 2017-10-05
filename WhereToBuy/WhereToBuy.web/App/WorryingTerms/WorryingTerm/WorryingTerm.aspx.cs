using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WhereToBuy.web.UserControls;

namespace WhereToBuy.web.App.WorryingTerms.WorryingTerm
{
    public partial class WorryingTerm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ((MessageUC)(WorryingTermUC.FindControl("MessageUC"))).SubmitButtonClick += WorryingTerm_MessageButton;
        }



        private void WorryingTerm_MessageButton(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ic", "invokeButtonClick();", true);

        }
    }

}