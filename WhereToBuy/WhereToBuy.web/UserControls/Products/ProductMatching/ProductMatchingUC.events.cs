using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhereToBuy.web.UserControls.Products.ProductMatching
{
    public class ProductMatchingUCEventArgs : EventArgs
    {

        WhereToBuy.entities.ProductMatching productMatching;
        string message = string.Empty;


        public ProductMatchingUCEventArgs(WhereToBuy.entities.ProductMatching productMatching, string message)
        {
            this.productMatching = productMatching;
            this.message = message;
        }


        public WhereToBuy.entities.ProductMatching ProductMatching
        {
            get { return productMatching; }
        }


        public string Message
        {
            get { return message; }
        }
    }


    public delegate void ProductMatchingUCMessageHandler(object sender, ProductMatchingUCEventArgs e);

    public partial class ProductMatchingUC
    {
        public event ProductMatchingUCMessageHandler ProductMatchingUCMessage;

        protected virtual void OnProductMatchingUCMessage(ProductMatchingUCEventArgs e)
        {
            if (ProductMatchingUCMessage != null)  // Isto é nulo se nenhum codigo está à escuta deste envento
            {
                ProductMatchingUCMessage(this, e);
            }
        }
    }
}