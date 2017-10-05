using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhereToBuy.entities;

namespace WhereToBuy.core
{
    public partial class BrandsMatching
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="brandMatching"></param>
        public void Store(BrandMatching brandMatching)
        {

            if (brandMatching.EditionMode == false)
            {
                // No futuro validar permissões
            }
            else
            {
                // No futuro validar permissões
            }


            try
            {
                engine.Data.BrandsMatching.Store(brandMatching);
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
        /// <param name="brandMatching"></param>
        public void Delete(BrandMatching brandMatching)
        {

            // No futuro validar permissões

            try
            {
                engine.Data.BrandsMatching.Delete(brandMatching);
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
