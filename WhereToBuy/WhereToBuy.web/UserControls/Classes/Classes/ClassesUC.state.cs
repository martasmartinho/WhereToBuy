using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhereToBuy.web.UserControls.Classes.Classes
{
    public partial class ClassesUC
    {
        WhereToBuy.entities.Classe selectedClasse;


        /// <summary>
        /// set selected object
        /// </summary>
        /// <param name="selectedClasse">object</param>
        void SetSelectedClasse(WhereToBuy.entities.Classe selectedClasse)
        {
            this.selectedClasse = selectedClasse;
            ViewState["SelectedClasse"] = selectedClasse;

        }


        /// <summary>
        /// returns is selected object exists
        /// </summary>
        public bool SelectedClasseExist
        {
            get { return (ViewState["SelectedClasse"] != null); }
        }


        /// <summary>
        /// returns selected object
        /// </summary>
        /// <returns>selected object</returns>
        public WhereToBuy.entities.Classe GetSelectedClasse()
        {
            return (WhereToBuy.entities.Classe)ViewState["SelectedClasse"];
        }
    }
}