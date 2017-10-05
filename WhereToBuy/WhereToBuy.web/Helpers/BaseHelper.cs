using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WhereToBuy.core;

namespace WhereToBuy.web.Helpers
{
    public class BaseHelper
    {
        public static CoreEngine engine;
        /// <summary>
        /// This method removes all session variables from actual session
        /// </summary>
        public static void SessaoReinicia()
        {
            HttpContext.Current.Session.RemoveAll();
        }
    }
}