using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhereToBuy.web.UserControls.Catalogs.Catalogs
{
    public partial class CatalogsUC
    {
        WhereToBuy.entities.Catalog selectedCatalog;


        /// <summary>
        /// set selected object
        /// </summary>
        /// <param name="selectedCatalog">object</param>
        void SetSelectedCatalog(WhereToBuy.entities.Catalog selectedCatalog)
        {
            this.selectedCatalog = selectedCatalog;
            ViewState["SelectedCatalog"] = selectedCatalog;

        }


        /// <summary>
        /// returns is selected object exists
        /// </summary>
        public bool SelectedCatalogExist
        {
            get { return (ViewState["SelectedCatalog"] != null); }
        }


        /// <summary>
        /// returns selected object
        /// </summary>
        /// <returns>selected object</returns>
        public WhereToBuy.entities.Catalog GetSelectedCatalog()
        {
            return (WhereToBuy.entities.Catalog)ViewState["SelectedCatalog"];
        }
    }
}