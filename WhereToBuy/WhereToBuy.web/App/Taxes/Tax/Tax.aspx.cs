using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WhereToBuy.web.UserControls;

namespace WhereToBuy.web.App.Taxes.Tax
{
    public partial class Tax : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ((MessageUC)(TaxUC.FindControl("MessageUC"))).SubmitButtonClick += Tax_MessageButton;

        }



        private void Tax_MessageButton(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ic", "invokeButtonClick();", true);

        }
    }
}