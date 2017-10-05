using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhereToBuy.web.UserControls.WarningTypes.WarningType
{
    public partial class WarningTypeUC
    {
        WhereToBuy.entities.WarningType warningType;


        /// <summary>
        /// set selected object
        /// </summary>
        /// <param name="warningType">object</param>
        void SetSelectedWarningType(WhereToBuy.entities.WarningType warningType)
        {
            this.warningType = warningType;
            ViewState["SelectedWarningType"] = warningType;

        }



        /// <summary>
        /// returns is selected state exists
        /// </summary>
        public bool SelectedWarningTypeExist
        {
            get { return (ViewState["SelectedWarningType"] != null); }
        }


        /// <summary>
        /// returns selected object
        /// </summary>
        /// <returns>selected state</returns>
        public WhereToBuy.entities.WarningType GetSelectedWarningType()
        {
            return (WhereToBuy.entities.WarningType)ViewState["SelectedWarningType"];
        }
    }
}