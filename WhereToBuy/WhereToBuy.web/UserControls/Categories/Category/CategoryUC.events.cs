using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhereToBuy.web.UserControls.Categories.Category
{
    public class CategoryUCEventArgs : EventArgs
    {
        WhereToBuy.entities.Category category;
        string message = string.Empty;


        public CategoryUCEventArgs(WhereToBuy.entities.Category category, string message)
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


    public delegate void CategoryUCMessageHandler(object sender, CategoryUCEventArgs e);

    public partial class CategoryUC
    {
        public event CategoryUCMessageHandler CategoryUCMessage;

        protected virtual void OnCategoryUCMessage(CategoryUCEventArgs e)
        {
            if (CategoryUCMessage != null)  // Isto é nulo se nenhum codigo está à escuta deste envento
            {
                CategoryUCMessage(this, e);
            }
        }
    }
}