using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhereToBuy.web.UserControls.Products.ProductsSelBox
{
    public class ProductSelBoxEventArgs : EventArgs
    {
        WhereToBuy.entities.Product product;
        string message = string.Empty;



        public ProductSelBoxEventArgs(WhereToBuy.entities.Product product, string message)
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


    public delegate void ProductSelBoxHandler(object sender, ProductSelBoxEventArgs e);

    public partial class ProductsSelBox
    {
        public event ProductSelBoxHandler SubmitButtonClick;

        protected void OnSubmitButtonClick(ProductSelBoxEventArgs e)
        {
            if (SubmitButtonClick != null)
            {
                SubmitButtonClick(this, e);
            }
        }

        public event ProductSelBoxHandler SelectedProductUpdate;

        protected void OnSelectedBrandUpdate(ProductSelBoxEventArgs e)
        {

            if (SelectedProductUpdate != null)
            {
                SelectedProductUpdate(this, e);
            }
        }


        public event ProductSelBoxHandler ProductSelBoxMessage;

        protected virtual void OnBrandSelBoxMessageHandlerMessage(ProductSelBoxEventArgs e)
        {
            if (ProductSelBoxMessage != null)  // Isto é nulo se nenhum codigo está à escuta deste envento
            {
                ProductSelBoxMessage(this, e);
            }
        }
    }
}