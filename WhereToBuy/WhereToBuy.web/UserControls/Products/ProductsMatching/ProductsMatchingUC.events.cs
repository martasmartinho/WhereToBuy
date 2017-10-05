using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhereToBuy.web.UserControls.Products.ProductsMatching
{
    public class ProductsMatchingUCEventArgs : EventArgs
    {

        WhereToBuy.entities.ProductMatching productMatching;
        string message = string.Empty;


        public ProductsMatchingUCEventArgs(WhereToBuy.entities.ProductMatching productMatching, string message)
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


    public delegate void ProductsMatchingUCMessageHandler(object sender, ProductsMatchingUCEventArgs e);
    public partial class ProductsMatchingUC
    {
        public event ProductsMatchingUCMessageHandler ProductsMatchingUCMessage;

        protected virtual void OnProductsMatchingUCMessage(ProductsMatchingUCEventArgs e)
        {
            if (ProductsMatchingUCMessage != null)  // Isto é nulo se nenhum codigo está à escuta deste envento
            {
                ProductsMatchingUCMessage(this, e);
            }
        }
    }
}