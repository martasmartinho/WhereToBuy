using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WhereToBuy.web.UserControls;

namespace WhereToBuy.web.App.Supplements.Supplement
{
    public partial class Supplement : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ((MessageUC)(SupplementUC.FindControl("MessageUC"))).SubmitButtonClick += Supplement_MessageButton;

        }

        private void Supplement_MessageButton(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ic", "invokeButtonClick();", true);

        }
    }
}