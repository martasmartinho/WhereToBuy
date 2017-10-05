using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhereToBuy.web.UserControls.Supplements.Supplement
{
    public partial class SupplementUC
    {
        WhereToBuy.entities.Supplement supplement;


        /// <summary>
        /// set selected object
        /// </summary>
        /// <param name="supplement">object</param>
        void SetSelectedSupplement(WhereToBuy.entities.Supplement supplement)
        {
            this.supplement = supplement;
            ViewState["SelectedSupplement"] = supplement;

        }



        /// <summary>
        /// returns is selected state exists
        /// </summary>
        public bool SelectedSupplementExist
        {
            get { return (ViewState["SelectedSupplement"] != null); }
        }


        /// <summary>
        /// returns selected object
        /// </summary>
        /// <returns>selected state</returns>
        public WhereToBuy.entities.Supplement GetSelectedSupplement()
        {
            return (WhereToBuy.entities.Supplement)ViewState["SelectedSupplement"];
        }
    }
}