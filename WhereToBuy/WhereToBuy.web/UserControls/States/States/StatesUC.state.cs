using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhereToBuy.web.UserControls.States.States
{
    public partial class StatesUC
    {
        WhereToBuy.entities.State selectedState;


        /// <summary>
        /// set selected object
        /// </summary>
        /// <param name="selectedState">object</param>
        void SetSelectedState(WhereToBuy.entities.State selectedState)
        {
            this.selectedState = selectedState;
            ViewState["SelectedState"] = selectedState;

        }


        /// <summary>
        /// returns is selected object exists
        /// </summary>
        public bool SelectedStateExist
        {
            get { return (ViewState["SelectedState"] != null); }
        }


        /// <summary>
        /// returns selected object
        /// </summary>
        /// <returns>selected object</returns>
        public WhereToBuy.entities.State GetSelectedState()
        {
            return (WhereToBuy.entities.State)ViewState["SelectedState"];
        }


    }
}