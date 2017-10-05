using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhereToBuy.web.UserControls.Products.Product
{
    public class ProductUCEventArgs : EventArgs
    {
        WhereToBuy.entities.Product product;
        string message = string.Empty;


        public ProductUCEventArgs(WhereToBuy.entities.Product product, string message)
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

    public delegate void ProductUCMessageHandler(object sender, ProductUCEventArgs e);

    public partial class ProductUC
    {
        public event ProductUCMessageHandler ProductUCMessage;

        protected virtual void OnProductUCMessage(ProductUCEventArgs e)
        {
            if (ProductUCMessage != null)  // Isto é nulo se nenhum codigo está à escuta deste envento
            {
                ProductUCMessage(this, e);
            }
        }
    }
}