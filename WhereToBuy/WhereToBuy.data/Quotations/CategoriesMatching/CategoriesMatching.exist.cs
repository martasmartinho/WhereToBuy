using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhereToBuy.entities;

namespace WhereToBuy.data
{
    public partial class CategoriesMatching
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoryMatching"></param>
        /// <param name="dataState"></param>
        /// <returns></returns>
        public bool Exists(CategoryMatching categoryMatching, DataState dataState)
        {
            return this.Exists(categoryMatching.Supplier, categoryMatching.Code, dataState);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <param name="dataState"></param>
        /// <returns></returns>
        public bool Exists(string supplierCode, string code, DataState dataState)
        {
            if (Count(supplierCode, code, dataState) > 0)
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
        public bool Exists(Supplier supplier, string code, DataState dataState)
        {
            if (Count(supplier, code, dataState) > 0)
            {
                return true;
            }

            return false;
        }
    }
}
