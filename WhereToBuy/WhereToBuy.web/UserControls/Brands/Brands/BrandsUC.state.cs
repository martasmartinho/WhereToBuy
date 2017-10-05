using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhereToBuy.web.UserControls.Brands.Brands
{
    public partial class BrandsUC
    {
        WhereToBuy.entities.Brand selectedBrand;


        /// <summary>
        /// set selected object
        /// </summary>
        /// <param name="selectedBrand">object</param>
        void SetSelectedBrand(WhereToBuy.entities.Brand selectedBrand)
        {
            this.selectedBrand = selectedBrand;
            ViewState["SelectedBrand"] = selectedBrand;
        }

       
        /// <summary>
        /// returns is selected object exists
        /// </summary>
        public bool SelectedBrandExist
        {
            get { return (ViewState["SelectedBrand"] != null); }
        }


        /// <summary>
        /// returns selected object
        /// </summary>
        /// <returns>selected object</returns>
        public WhereToBuy.entities.Brand GetSelectedBrand()
        {
            return (WhereToBuy.entities.Brand)ViewState["SelectedBrand"];
        }
    }
}