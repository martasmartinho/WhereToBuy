using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhereToBuy.web.UserControls.BrandsMatching
{
    public class BrandsMatchingUCEventArgs : EventArgs
    {
       
        WhereToBuy.entities.BrandMatching brandMatching;
        string message = string.Empty;
    

        public BrandsMatchingUCEventArgs(WhereToBuy.entities.BrandMatching brandMatching, string message)
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


    public delegate void BrandsMatchingUCMessageHandler(object sender, BrandsMatchingUCEventArgs e);

    public partial class BrandsMatchingUC
    {
        public event BrandsMatchingUCMessageHandler BrandsMatchingUCMessage;
        
        protected virtual void OnBrandsMatchingUCMessage(BrandsMatchingUCEventArgs e)
        {
            if (BrandsMatchingUCMessage != null)  // Isto é nulo se nenhum codigo está à escuta deste envento
            {
                BrandsMatchingUCMessage(this, e);
            }
        }
    }
}