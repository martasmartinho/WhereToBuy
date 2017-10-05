using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhereToBuy.entities;

namespace WhereToBuy.data
{
    public partial class SuppliersBrands
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="brandMatching"></param>
        /// <returns></returns>
        public bool Exists(SupplierBrand brandMatching)
        {
            return this.Exists(brandMatching.Supplier, brandMatching.Brand);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="supplierCode"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public bool Exists(string supplierCode, string code)
        {
            if (Count(supplierCode, code) > 0)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="supplier"></param>
        /// <param name="brand"></param>
        /// <returns></returns>
        public bool Exists(Supplier supplier, Brand brand)
        {
            if (Count(supplier, brand) > 0)
            {
                return true;
            }

            return false;
        }
    }
}
