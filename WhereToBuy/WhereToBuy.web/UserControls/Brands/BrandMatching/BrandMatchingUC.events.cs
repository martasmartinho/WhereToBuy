using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhereToBuy.web.UserControls.Brands.BrandMatching
{
    public class BrandMatchingUCEventArgs : EventArgs
    {

        WhereToBuy.entities.BrandMatching brandMatching;
        string message = string.Empty;


        public BrandMatchingUCEventArgs(WhereToBuy.entities.BrandMatching brandMatching, string message)
        {
            this.brandMatching = brandMatching;
            this.message = message;
        }


        public WhereToBuy.entities.BrandMatching BrandMatching
        {
            get { return brandMatching; }
        }


        public string Message
        {
            get { return message; }
        }
    }


    public delegate void BrandMatchingUCMessageHandler(object sender, BrandMatchingUCEventArgs e);


    public partial class BrandMatchingUC
    {
        public event BrandMatchingUCMessageHandler BrandMatchingUCMessage;
        
        protected virtual void OnBrandMatchingUCMessage(BrandMatchingUCEventArgs e)
        {
            if (BrandMatchingUCMessage != null)  // Isto é nulo se nenhum codigo está à escuta deste envento
            {
                BrandMatchingUCMessage(this, e);
            }
        }
    }
}