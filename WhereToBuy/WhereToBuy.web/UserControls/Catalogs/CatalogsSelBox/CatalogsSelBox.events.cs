using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhereToBuy.web.UserControls.Catalogs.CatalogsSelBox
{
    public class CatalogsSelBoxEventArgs : EventArgs
    {

        WhereToBuy.entities.Catalog catalog;
        string message = string.Empty;



        public CatalogsSelBoxEventArgs(WhereToBuy.entities.Catalog catalog, string message)
        {
            this.catalog = catalog;
            this.message = message;
        }


        public WhereToBuy.entities.Catalog Catalog
        {
            get { return catalog; }
        }

        public string Message
        {
            get { return message; }
        }

    }


    public delegate void CatalogsSelBoxHandler(object sender, CatalogsSelBoxEventArgs e);
    public partial class CatalogsSelBox
    {
        public event CatalogsSelBoxHandler SubmitButtonClick;

        protected void OnSubmitButtonClick(CatalogsSelBoxEventArgs e)
        {
            if (SubmitButtonClick != null)
            {
                SubmitButtonClick(this, e);
            }
        }

        public event CatalogsSelBoxHandler SelectedCatalogUpdate;

        protected void OnSelectedCatalogUpdate(CatalogsSelBoxEventArgs e)
        {

            if (SelectedCatalogUpdate != null)
            {
                SelectedCatalogUpdate(this, e);
            }
        }


        public event CatalogsSelBoxHandler CatalogsSelBoxMessage;

        protected virtual void OnCatalogsSelBoxMessageHandlerMessage(CatalogsSelBoxEventArgs e)
        {
            if (CatalogsSelBoxMessage != null)  // Isto é nulo se nenhum codigo está à escuta deste envento
            {
                CatalogsSelBoxMessage(this, e);
            }
        }
    }
}