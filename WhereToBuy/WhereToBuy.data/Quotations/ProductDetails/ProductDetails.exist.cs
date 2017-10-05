using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhereToBuy.entities;

namespace WhereToBuy.data
{
    public partial class ProductDetails
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="productDetail"></param>
        /// <param name="dataState"></param>
        /// <returns></returns>
        public bool Exists(ProductDetail productDetail, DataState dataState)
        {
            return this.Exists(productDetail.ProductCode, productDetail.Supplier.Code, dataState);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productCode"></param>
        /// <param name="dataState"></param>
        /// <returns></returns>
        public bool Exists(string productCode, string supplierCode, DataState dataState)
        {
            if (Count(productCode, supplierCode, dataState) > 0)
            {
                return true;
            }

            return false;
        }
    }
}
