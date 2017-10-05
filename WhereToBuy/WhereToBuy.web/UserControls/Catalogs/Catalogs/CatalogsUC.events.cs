using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhereToBuy.web.UserControls.Catalogs.Catalogs
{
    public class CatalogsUCEventArgs : EventArgs
    {
        WhereToBuy.entities.Catalog catalog;
        string message = string.Empty;


        public CatalogsUCEventArgs(WhereToBuy.entities.Catalog catalog, string message)
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


    public delegate void CatalogsUCMessageHandler(object sender, CatalogsUCEventArgs e);

    public partial class CatalogsUC
    {
        public event CatalogsUCMessageHandler CatalogsUCMessage;

        protected virtual void OnCatalogsUCMessage(CatalogsUCEventArgs e)
        {
            if (CatalogsUCMessage != null)  // Isto é nulo se nenhum codigo está à escuta deste envento
            {
                CatalogsUCMessage(this, e);
            }
        }
    }

}