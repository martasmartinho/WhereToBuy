using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhereToBuy.entities;

namespace WhereToBuy.core
{
    public partial class SuppliersBrands
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="supplierBrand"></param>
        public void Store(SupplierBrand supplierBrand)
        {

            if (supplierBrand.EditionMode == false)
            {
                // No futuro validar permissões
            }
            else
            {
                // No futuro validar permissões
            }


            try
            {
                engine.Data.SuppliersBrands.Store(supplierBrand);
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
        /// <param name="supplierBrand"></param>
        public void Delete(SupplierBrand supplierBrand)
        {

            // No futuro validar permissões

            try
            {
                engine.Data.SuppliersBrands.Delete(supplierBrand);
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
