using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhereToBuy.web.UserControls.Categories.CategoriesSelBox
{
    public class CategoriesSelBoxEventArgs : EventArgs
    {

        WhereToBuy.entities.Category category;
        string message = string.Empty;



        public CategoriesSelBoxEventArgs(WhereToBuy.entities.Category category, string message)
        {
            this.category = category;
            this.message = message;
        }


        public WhereToBuy.entities.Category Category
        {
            get { return category; }
        }

        public string Message
        {
            get { return message; }
        }

    }


    public delegate void CategoriesSelBoxHandler(object sender, CategoriesSelBoxEventArgs e);

    public partial class CategoriesSelBox
    {
        public event CategoriesSelBoxHandler SubmitButtonClick;

        protected void OnSubmitButtonClick(CategoriesSelBoxEventArgs e)
        {
            if (SubmitButtonClick != null)
            {
                SubmitButtonClick(this, e);
            }
        }

        public event CategoriesSelBoxHandler SelectedCategoryUpdate;

        protected void OnSelectedCategoryUpdate(CategoriesSelBoxEventArgs e)
        {

            if (SelectedCategoryUpdate != null)
            {
                SelectedCategoryUpdate(this, e);
            }
        }


        public event CategoriesSelBoxHandler CategoriesSelBoxMessage;

        protected virtual void OnCategoriesSelBoxMessageHandlerMessage(CategoriesSelBoxEventArgs e)
        {
            if (CategoriesSelBoxMessage != null)  // Isto é nulo se nenhum codigo está à escuta deste envento
            {
                CategoriesSelBoxMessage(this, e);
            }
        }
    }
}