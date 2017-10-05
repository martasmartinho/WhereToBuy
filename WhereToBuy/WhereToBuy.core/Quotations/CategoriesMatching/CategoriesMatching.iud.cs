using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhereToBuy.entities;

namespace WhereToBuy.core
{
    public partial class CategoriesMatching
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="brandMatching"></param>
        public void Store(CategoryMatching categoryMatching)
        {

            if (categoryMatching.EditionMode == false)
            {
                // No futuro validar permissões
            }
            else
            {
                // No futuro validar permissões
            }


            try
            {
                engine.Data.CategoriesMatching.Store(categoryMatching);
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
        /// <param name="categoryMatching"></param>
        public void Delete(CategoryMatching categoryMatching)
        {

            // No futuro validar permissões

            try
            {
                engine.Data.CategoriesMatching.Delete(categoryMatching);
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
