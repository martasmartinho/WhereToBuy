using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WhereToBuy.web.UserControls.States.StatesSelBox
{
    public partial class StatesSelBox : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void lkBtnSearch_Click(object sender, EventArgs e)
        {
            //txtState.Text = "pr";
            txtState.Focus();
            SubmitButtonClick(lkBtnSearch, new StateSelBoxEventArgs(null, ""));
            RefreshListView();
        }


        protected void lkBtnItem_Click(object sender, EventArgs e)
        {
            WhereToBuy.entities.State state;

            lvStates.SelectedIndex = Convert.ToInt32((((LinkButton)sender).CommandArgument));
            state = LoadState(((LinkButton)sender).Text.Split('-')[0].TrimStart().TrimEnd());
            txtState.Text = state.ToString();

            lvStates.Items.Clear();
            lvStates.DataBind();
            SelectedStateUpdate(this, new StateSelBoxEventArgs(state, ""));
        }

        protected void txtState_TextChanged(object sender, EventArgs e)
        {

        }
    }
}