using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhereToBuy.web.UserControls.Categories.CategoriesMatching
{
    public class CategoriesMatchingUCEventArgs : EventArgs
    {


        WhereToBuy.entities.CategoryMatching categoryMatching;
        string message = string.Empty;


        public CategoriesMatchingUCEventArgs(WhereToBuy.entities.CategoryMatching categoryMatching, string message)
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


    public delegate void CategoriesMatchingUCMessageHandler(object sender, CategoriesMatchingUCEventArgs e);

    public partial class CategoriesMatchingUC
    {
        public event CategoriesMatchingUCMessageHandler CategoriesMatchingUCMessage;

        protected virtual void OnCategoriesMatchingUCMessage(CategoriesMatchingUCEventArgs e)
        {
            if (CategoriesMatchingUCMessage != null)  // Isto é nulo se nenhum codigo está à escuta deste envento
            {
                CategoriesMatchingUCMessage(this, e);
            }
        }
    }
}