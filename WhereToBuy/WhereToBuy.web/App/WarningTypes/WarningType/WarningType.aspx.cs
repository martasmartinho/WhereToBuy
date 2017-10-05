using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WhereToBuy.web.UserControls;

namespace WhereToBuy.web.App.WarningTypes.WarningType
{
    public partial class WarningType : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ((MessageUC)(WarningTypeUC.FindControl("MessageUC"))).SubmitButtonClick += WarningType_MessageButton;

        }


        private void WarningType_MessageButton(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ic", "invokeButtonClick();", true);

        }
    }
}