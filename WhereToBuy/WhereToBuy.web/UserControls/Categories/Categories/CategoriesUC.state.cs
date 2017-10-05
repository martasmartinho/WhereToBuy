using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhereToBuy.web.UserControls.Categories.Categories
{
    public partial class CategoriesUC
    {
        WhereToBuy.entities.Category selectedCategory;


        /// <summary>
        /// set selected object
        /// </summary>
        /// <param name="selectedCategory">object</param>
        void SetSelectedCategory(WhereToBuy.entities.Category selectedCategory)
        {
            this.selectedCategory = selectedCategory;
            ViewState["SelectedCategory"] = selectedCategory;

        }


        /// <summary>
        /// returns is selected object exists
        /// </summary>
        public bool SelectedCategoryExist
        {
            get { return (ViewState["SelectedCategory"] != null); }
        }


        /// <summary>
        /// returns selected object
        /// </summary>
        /// <returns>selected object</returns>
        public WhereToBuy.entities.Category GetSelectedCategory()
        {
            return (WhereToBuy.entities.Category)ViewState["SelectedCategory"];
        }
    }
}