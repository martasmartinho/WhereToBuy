using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhereToBuy.web.UserControls.Categories.CategoryMatching
{
    public class CategoryMatchingUCEventArgs : EventArgs
    {


        WhereToBuy.entities.CategoryMatching categoryMatching;
        string message = string.Empty;


        public CategoryMatchingUCEventArgs(WhereToBuy.entities.CategoryMatching categoryMatching, string message)
        {
            this.categoryMatching = categoryMatching;
            this.message = message;
        }


        public WhereToBuy.entities.CategoryMatching CategoryMatching
        {
            get { return categoryMatching; }
        }


        public string Message
        {
            get { return message; }
        }
    }


    public delegate void CategoryMatchingUCMessageHandler(object sender, CategoryMatchingUCEventArgs e);

    public partial class CategoryMatchingUC
    {
        public event CategoryMatchingUCMessageHandler CategoryMatchingUCMessage;

        protected virtual void OnCategoryMatchingUCMessage(CategoryMatchingUCEventArgs e)
        {
            if (CategoryMatchingUCMessage != null)  // Isto é nulo se nenhum codigo está à escuta deste envento
            {
                CategoryMatchingUCMessage(this, e);
            }
        }
    }
}