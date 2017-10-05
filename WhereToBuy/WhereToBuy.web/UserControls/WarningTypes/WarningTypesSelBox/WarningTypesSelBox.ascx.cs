using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WhereToBuy.web.UserControls.WarningTypes.WarningTypesSelBox
{
    public partial class WarningTypesSelBox : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
             
            }
        }

        protected void lkBtnSearch_Click(object sender, EventArgs e)
        {

            txtWarningType.Focus();
            SubmitButtonClick(lkBtnSearch, new WarningTypeSelBoxEventArgs(null, ""));
            RefreshListView();
        }


     

        protected void lkBtnItem_Click(object sender, EventArgs e)
        {
            WhereToBuy.entities.WarningType warningType;

            lvWarningTypes.SelectedIndex = Convert.ToInt32((((LinkButton)sender).CommandArgument));
            warningType = LoadWarningType(((LinkButton)sender).Text.Split('-')[0].TrimStart().TrimEnd());
            txtWarningType.Text = warningType.ToString();

            lvWarningTypes.Items.Clear();
            lvWarningTypes.DataBind();
            SelectedWarningTypeUpdate(this, new WarningTypeSelBoxEventArgs(warningType, ""));
        }
        
    }
}