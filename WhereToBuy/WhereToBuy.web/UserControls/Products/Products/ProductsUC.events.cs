using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhereToBuy.web.UserControls.Products.Products
{
    public class ProductsUCEventArgs : EventArgs
    {

        WhereToBuy.entities.Product product;
        string message = string.Empty;


        public ProductsUCEventArgs(WhereToBuy.entities.Product product, string message)
        {
            this.product = product;
            this.message = message;
        }


        public WhereToBuy.entities.Product Product
        {
            get { return product; }
        }


        public string Message
        {
            get { return message; }
        }
    }


    public delegate void ProductsUCMessageHandler(object sender, ProductsUCEventArgs e);
    public partial class ProductsUC
    {
        public event ProductsUCMessageHandler ProductsUCMessage;

        protected virtual void OnProductsUCMessage(ProductsUCEventArgs e)
        {
            if (ProductsUCMessage != null)  // Isto é nulo se nenhum codigo está à escuta deste envento
            {
                ProductsUCMessage(this, e);
            }
        }
    }
}