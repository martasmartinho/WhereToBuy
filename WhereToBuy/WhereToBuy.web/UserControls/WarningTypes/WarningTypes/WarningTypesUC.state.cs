using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhereToBuy.web.UserControls.WarningTypes.WarningTypes
{
    public partial class WarningTypesUC
    {
        WhereToBuy.entities.WarningType selectedWarningType;


        /// <summary>
        /// set selected object
        /// </summary>
        /// <param name="selectedWarningType">object</param>
        void SetSelectedWarningType(WhereToBuy.entities.WarningType selectedWarningType)
        {
            this.selectedWarningType = selectedWarningType;
            ViewState["SelectedWarningType"] = selectedWarningType;

        }


        /// <summary>
        /// returns is selected object exists
        /// </summary>
        public bool SelectedWarningTypeExist
        {
            get { return (ViewState["SelectedWarningType"] != null); }
        }


        /// <summary>
        /// returns selected object
        /// </summary>
        /// <returns>selected object</returns>
        public WhereToBuy.entities.WarningType GetSelectedWarningType()
        {
            return (WhereToBuy.entities.WarningType)ViewState["SelectedWarningType"];
        }
    }
}