using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhereToBuy.entities;

namespace WhereToBuy.core
{
    public partial class Categories
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="category"></param>
        public void Store(Category category)
        {

            if (category.EditionMode == false)
            {
                // No futuro validar permissões
            }
            else
            {
                // No futuro validar permissões
            }


            try
            {
                engine.Data.Categories.Store(category);
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
        /// <param name="category"></param>
        public void Delete(Category category)
        {

            // No futuro validar permissões

            try
            {
                engine.Data.Categories.Delete(category);
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
