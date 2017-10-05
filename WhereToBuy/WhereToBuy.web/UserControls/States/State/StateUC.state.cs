using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhereToBuy.web.UserControls.States.State
{
    public partial class StateUC
    {
        WhereToBuy.entities.State state;


        /// <summary>
        /// set selected object
        /// </summary>
        /// <param name="state">object</param>
        void SetSelectedState(WhereToBuy.entities.State state)
        {
            this.state = state;
            ViewState["SelectedState"] = state;

        }



        /// <summary>
        /// returns is selected state exists
        /// </summary>
        public bool SelectedStateExist
        {
            get { return (ViewState["SelectedState"] != null); }
        }


        /// <summary>
        /// returns selected object
        /// </summary>
        /// <returns>selected state</returns>
        public WhereToBuy.entities.State GetSelectedState()
        {
            return (WhereToBuy.entities.State)ViewState["SelectedState"];
        }
    }
}