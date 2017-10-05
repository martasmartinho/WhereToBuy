using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhereToBuy.entities;

namespace WhereToBuy.data
{
    public partial class ProductsMatching
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="productMatching"></param>
        /// <param name="dataState"></param>
        /// <returns></returns>
        public bool Exists(ProductMatching productMatching, DataState dataState)
        {
            return this.Exists(productMatching.Supplier, productMatching.Code, productMatching.Supplement, dataState);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <param name="dataState"></param>
        /// <returns></returns>
        public bool Exists(string supplierCode, string code, string supplementCode, DataState dataState)
        {
            if (Count(supplierCode, code, supplementCode, dataState) > 0)
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
        public bool Exists(Supplier supplier, string code, string supplementCode, DataState dataState)
        {
            if (Count(supplier, code, supplementCode, dataState) > 0)
            {
                return true;
            }

            return false;
        }
    }
}
