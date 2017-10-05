using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhereToBuy.web.UserControls.WorryingTerms.WorryingTerm
{
    public partial class WorryingTermUC
    {
        WhereToBuy.entities.WorryingTerm worryingTerm;


        /// <summary>
        /// set selected object
        /// </summary>
        /// <param name="worryingTerm">object</param>
        void SetSelectedWorryingTerm(WhereToBuy.entities.WorryingTerm worryingTerm)
        {
            this.worryingTerm = worryingTerm;
            ViewState["SelectedWorryingTerm"] = worryingTerm;

        }



        /// <summary>
        /// returns is selected state exists
        /// </summary>
        public bool SelectedWorryingTermExist
        {
            get { return (ViewState["SelectedWorryingTerm"] != null); }
        }


        /// <summary>
        /// returns selected object
        /// </summary>
        /// <returns>selected state</returns>
        public WhereToBuy.entities.WorryingTerm GetSelectedWorryingTerm()
        {
            return (WhereToBuy.entities.WorryingTerm)ViewState["SelectedWorryingTerm"];
        }
    }
}