using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhereToBuy.entities;

namespace WhereToBuy.data
{
    public partial class Suppliers
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="supplier"></param>
        /// <param name="dataState"></param>
        /// <returns></returns>
        public bool Exists(Supplier supplier, DataState dataState)
        {
            return this.Exists(supplier.Code, dataState);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <param name="dataState"></param>
        /// <returns></returns>
        public bool Exists(string code, DataState dataState)
        {
            if (Count(code, dataState) > 0)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <param name="dataState"></param>
        /// <returns></returns>
        public bool Exists(string code, string username, string password, DataState dataState)
        {
            if (Count(code, username, password, dataState) > 0)
            {
                return true;
            }

            return false;
        }
    }
}
