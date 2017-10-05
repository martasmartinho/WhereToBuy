using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhereToBuy.entities;

namespace WhereToBuy.core
{
    public partial class Catalogs
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="catalog"></param>
        public void Store(Catalog catalog)
        {

            if (catalog.EditionMode == false)
            {
                // No futuro validar permissões
            }
            else
            {
                // No futuro validar permissões
            }


            try
            {
                engine.Data.Catalogs.Store(catalog);
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
        /// <param name="catalog"></param>
        public void Delete(Catalog catalog)
        {

            // No futuro validar permissões

            try
            {
                engine.Data.Catalogs.Delete(catalog);
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
