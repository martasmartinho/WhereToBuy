using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhereToBuy.web.UserControls.WorryingTerms.WorryingTerms
{
    public partial class WorryingTermsUC
    {
        WhereToBuy.entities.WorryingTerm selectedWorryingTerm;


        /// <summary>
        /// set selected object
        /// </summary>
        /// <param name="selectedWorryingTerm">object</param>
        void SetSelectedWorryingTerm(WhereToBuy.entities.WorryingTerm selectedWorryingTerm)
        {
            this.selectedWorryingTerm = selectedWorryingTerm;
            ViewState["SelectedWorryingTerm"] = selectedWorryingTerm;

        }


        /// <summary>
        /// returns is selected object exists
        /// </summary>
        public bool SelectedWorryingTermExist
        {
            get { return (ViewState["SelectedWorryingTerm"] != null); }
        }


        /// <summary>
        /// returns selected object
        /// </summary>
        /// <returns>selected object</returns>
        public WhereToBuy.entities.WorryingTerm GetSelectedWorryingTerm()
        {
            return (WhereToBuy.entities.WorryingTerm)ViewState["SelectedWorryingTerm"];
        }
    }
}