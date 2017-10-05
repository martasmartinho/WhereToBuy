using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhereToBuy.entities;

namespace WhereToBuy.core
{
    public partial class ProductsMatching
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="productMatching"></param>
        public void Store(ProductMatching productMatching)
        {

            if (productMatching.EditionMode == false)
            {
                // No futuro validar permissões
            }
            else
            {
                // No futuro validar permissões
            }


            try
            {
                engine.Data.ProductsMatching.Store(productMatching);
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
        /// <param name="productMatching"></param>
        public void Delete(ProductMatching productMatching)
        {

            // No futuro validar permissões

            try
            {
                engine.Data.ProductsMatching.Delete(productMatching);
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
