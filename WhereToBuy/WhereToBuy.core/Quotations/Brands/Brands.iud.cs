using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhereToBuy.entities;

namespace WhereToBuy.core
{
    public partial class Brands
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="brand"></param>
        public void Store(Brand brand)
        {

            if (brand.EditionMode == false)
            {
                // No futuro validar permissões
            }
            else
            {
                // No futuro validar permissões
            }


            try
            {
                engine.Data.Brands.Store(brand);
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
        /// <param name="brand"></param>
        public void Delete(Brand brand)
        {

            // No futuro validar permissões

            try
            {
                engine.Data.Brands.Delete(brand);
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
