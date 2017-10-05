using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhereToBuy.web.UserControls.Categories.Category
{
    public partial class CategoryUC
    {
        WhereToBuy.entities.Category category;


        /// <summary>
        /// set selected object
        /// </summary>
        /// <param name="category">object</param>
        void SetSelectedCategory(WhereToBuy.entities.Category category)
        {
            this.category = category;
            ViewState["SelectedCategory"] = category;

        }



        /// <summary>
        /// returns is selected state exists
        /// </summary>
        public bool SelectedCategoryExist
        {
            get { return (ViewState["SelectedCategory"] != null); }
        }


        /// <summary>
        /// returns selected object
        /// </summary>
        /// <returns>selected state</returns>
        public WhereToBuy.entities.Category GetSelectedCategory()
        {
            return (WhereToBuy.entities.Category)ViewState["SelectedCategory"];
        }
    }
}