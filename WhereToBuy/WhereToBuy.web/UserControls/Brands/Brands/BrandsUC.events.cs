using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhereToBuy.web.UserControls.Brands.Brands
{
    public class BrandsUCEventArgs : EventArgs
    {

        WhereToBuy.entities.Brand brand;
        string message = string.Empty;


        public BrandsUCEventArgs(WhereToBuy.entities.Brand brand, string message)
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


    public delegate void BrandsUCMessageHandler(object sender, BrandsUCEventArgs e);

    public partial class BrandsMatchingUC
    {
        public event BrandsUCMessageHandler BrandsUCMessage;

        protected virtual void OnBrandsUCMessage(BrandsUCEventArgs e)
        {
            if (BrandsUCMessage != null)  // Isto é nulo se nenhum codigo está à escuta deste envento
            {
                BrandsUCMessage(this, e);
            }
        }
    }
}
