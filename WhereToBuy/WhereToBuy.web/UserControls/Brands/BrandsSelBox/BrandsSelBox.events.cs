using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhereToBuy.web.UserControls.Brands.BrandsSelBox
{
    public class BrandSelBoxEventArgs : EventArgs
    {

        WhereToBuy.entities.Brand brand;
        string message = string.Empty;



        public BrandSelBoxEventArgs(WhereToBuy.entities.Brand brand, string message)
        {
            this.brand = brand;
            this.message = message;
        }


        public WhereToBuy.entities.Brand Brand
        {
            get { return brand; }
        }

        public string Message
        {
            get { return message; }
        }

    }


    public delegate void BrandSelBoxHandler(object sender, BrandSelBoxEventArgs e);
    public partial class BrandsSelBox
    {
        public event BrandSelBoxHandler SubmitButtonClick;

        protected void OnSubmitButtonClick(BrandSelBoxEventArgs e)
        {
            if (SubmitButtonClick != null)
            {
                SubmitButtonClick(this, e);
            }
        }

        public event BrandSelBoxHandler SelectedBrandUpdate;

        protected void OnSelectedBrandUpdate(BrandSelBoxEventArgs e)
        {

            if (SelectedBrandUpdate != null)
            {
                SelectedBrandUpdate(this, e);
            }
        }


        public event BrandSelBoxHandler BrandSelBoxMessage;

        protected virtual void OnBrandSelBoxMessageHandlerMessage(BrandSelBoxEventArgs e)
        {
            if (BrandSelBoxMessage != null)  // Isto é nulo se nenhum codigo está à escuta deste envento
            {
                BrandSelBoxMessage(this, e);
            }
        }
    }
}