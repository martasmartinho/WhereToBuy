using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhereToBuy.entities;

namespace WhereToBuy.core
{
    public partial class Suppliers
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="supplier"></param>
        public void Store(Supplier supplier)
        {

            if (supplier.EditionMode == false)
            {
                // No futuro validar permissões
            }
            else
            {
                // No futuro validar permissões
            }


            try
            {
                engine.Data.Suppliers.Store(supplier);
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
        /// <param name="supplier"></param>
        public void Delete(Supplier supplier)
        {

            // No futuro validar permissões

            try
            {
                engine.Data.Suppliers.Delete(supplier);
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
