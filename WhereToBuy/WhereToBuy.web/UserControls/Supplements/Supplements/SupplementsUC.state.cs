using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhereToBuy.web.UserControls.Supplements.Supplements
{
    public partial class SupplementsUC
    {
        WhereToBuy.entities.Supplement selectedSupplement;


        /// <summary>
        /// set selected object
        /// </summary>
        /// <param name="selectedSupplement">object</param>
        void SetSelectedSupplement(WhereToBuy.entities.Supplement selectedSupplement)
        {
            this.selectedSupplement = selectedSupplement;
            ViewState["SelectedSupplement"] = selectedSupplement;

        }


        /// <summary>
        /// returns is selected object exists
        /// </summary>
        public bool SelectedSupplementExist
        {
            get { return (ViewState["SelectedSupplement"] != null); }
        }


        /// <summary>
        /// returns selected object
        /// </summary>
        /// <returns>selected object</returns>
        public WhereToBuy.entities.Supplement GetSelectedSupplement()
        {
            return (WhereToBuy.entities.Supplement)ViewState["SelectedSupplement"];
        }
    }
}