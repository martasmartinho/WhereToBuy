using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WhereToBuy.entities;
using WhereToBuy.web.UserControls;
using WhereToBuy.web.UserControls.States.StateMatching;
using WhereToBuy.web.UserControls.States.StatesSelBox;
using WhereToBuy.web.UserControls.Suppliers.SuppliersSelBox;

namespace WhereToBuy.web.App.States.StateMatching
{
    public partial class StateMatching : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var res = ((StatesSelBox)(StateMatchingUC.FindControl("StatesSelBox"))).FindControl("txtState");
            string teste = ((TextBox)res).Text;
            ((MessageUC)(StateMatchingUC.FindControl("MessageUC"))).SubmitButtonClick += StateMatching_MessageButton;
            ((SuppliersSelBox)(StateMatchingUC.FindControl("SuppliersSelBox"))).SubmitButtonClick += StateMatching_SupplierClickButton;

            ((StatesSelBox)(StateMatchingUC.FindControl("StatesSelBox"))).SubmitButtonClick += StateMatching_StateClickButton;

            
            // Load page
            if (!Page.IsPostBack)
            {
               

            }

        }

        private void StateMatching_StateClickButton(object sender, StateSelBoxEventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "bdc", "invokeDummyButton('" + "States" + "');", true);
        }


        private void StateMatching_MessageButton(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ic", "invokeButtonClick();", true);

        }

        private void StateMatching_SupplierClickButton(object sender, SupplierSelBoxEventArgs e)
        {
            string arg = "Suppliers";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "sdc", "invokeDummyButton('" + arg + "');", true);
        }


    }
}