using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhereToBuy.web.UserControls.Categories.Categories
{
    public class CategoriesUCEventArgs : EventArgs
    {

        WhereToBuy.entities.Category category;
        string message = string.Empty;


        public CategoriesUCEventArgs(WhereToBuy.entities.Category category, string message)
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


    public delegate void CategoriesUCMessageHandler(object sender, CategoriesUCEventArgs e);

    public partial class CategoriesUC
    {
        public event CategoriesUCMessageHandler CategoriesUCMessage;

        protected virtual void OnCategoriesUCMessage(CategoriesUCEventArgs e)
        {
            if (CategoriesUCMessage != null)  // Isto é nulo se nenhum codigo está à escuta deste envento
            {
                CategoriesUCMessage(this, e);
            }
        }
    }
}