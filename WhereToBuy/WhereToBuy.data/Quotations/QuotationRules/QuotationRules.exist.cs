using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhereToBuy.entities;

namespace WhereToBuy.data
{
    public partial class QuotationRules
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="quotationRule"></param>
        /// <returns></returns>
        public bool Exists(QuotationRule quotationRule)
        {
            return this.Exists(quotationRule.Supplier, quotationRule.Brand, quotationRule.Category, quotationRule.Stock);
        }

       /// <summary>
       /// 
       /// </summary>
       /// <param name="supplier"></param>
       /// <param name="brand"></param>
       /// <param name="category"></param>
       /// <param name="stock"></param>
       /// <returns></returns>
        public bool Exists(Supplier supplier, Brand brand, Category category, Stock stock)
        {
            if (Count(supplier.Code, brand.Code, category.Code, stock.Code) > 0)
            {
                return true;
            }

            return false;
        }
    }
}
