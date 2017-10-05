using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhereToBuy.web.UserControls.Brands.Brand
{
    public class BrandUCEventArgs : EventArgs
    {

        WhereToBuy.entities.Brand brand;
        string message = string.Empty;


        public BrandUCEventArgs(WhereToBuy.entities.Brand brand, string message)
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


    public delegate void BrandUCMessageHandler(object sender, BrandUCEventArgs e);


    public partial class BrandUC
    {
        public event BrandUCMessageHandler BrandUCMessage;

        protected virtual void OnBrandUCMessage(BrandUCEventArgs e)
        {
            if (BrandUCMessage != null)  // Isto é nulo se nenhum codigo está à escuta deste envento
            {
                BrandUCMessage(this, e);
            }
        }
    }
}