using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhereToBuy.web.UserControls.Brands.Brand
{
    public partial class BrandUC
    {
        WhereToBuy.entities.Brand brand;


        /// <summary>
        /// set selected object
        /// </summary>
        /// <param name="brand">object</param>
        void SetSelectedBrand(WhereToBuy.entities.Brand brand)
        {
            this.brand = brand;
            ViewState["SelectedBrand"] = brand;
        }


        /// <summary>
        /// returns is selected brand exists
        /// </summary>
        public bool SelectedBrandExist
        {
            get { return (ViewState["SelectedBrand"] != null); }
        }


        /// <summary>
        /// returns selected object
        /// </summary>
        /// <returns>selected brand</returns>
        public WhereToBuy.entities.Brand GetSelectedBrand()
        {
            return (WhereToBuy.entities.Brand)ViewState["SelectedBrand"];
        }
    }
}