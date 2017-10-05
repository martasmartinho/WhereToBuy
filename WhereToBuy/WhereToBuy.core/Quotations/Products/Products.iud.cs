using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhereToBuy.entities;

namespace WhereToBuy.core
{
    public partial class Products
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="product"></param>
        public void Store(Product product)
        {

            if (product.EditionMode == false)
            {
                // No futuro validar permissões
            }
            else
            {
                // No futuro validar permissões
            }


            try
            {
                engine.Data.Products.Store(product);
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
        /// <param name="product"></param>
        public void Delete(Product product)
        {

            // No futuro validar permissões

            try
            {
                engine.Data.Products.Delete(product);
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
