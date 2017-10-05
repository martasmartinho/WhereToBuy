using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhereToBuy.entities;

namespace WhereToBuy.core
{
    public partial class ProductDetails
    {
         
        /// <summary>
        /// 
        /// </summary>
        /// <param name="productDetail"></param>
        public void Store(ProductDetail productDetail)
        {

            if (productDetail.EditionMode == false)
            {
                // No futuro validar permissões
            }
            else
            {
                // No futuro validar permissões
            }


            try
            {
                engine.Data.ProductDetails.Store(productDetail);
            }
            catch (MyException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productDetail"></param>
        public void Delete(ProductDetail productDetail)
        {

            // No futuro validar permissões

            try
            {
                engine.Data.ProductDetails.Delete(productDetail);
            }
            catch (MyException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
