using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhereToBuy.entities;

namespace WhereToBuy.data
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Brands
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="brand"></param>
        /// <param name="dataState"></param>
        /// <returns></returns>
        public bool Exists(Brand brand, DataState dataState)
        {
            return this.Exists(brand.Code, dataState);
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
    }
}
