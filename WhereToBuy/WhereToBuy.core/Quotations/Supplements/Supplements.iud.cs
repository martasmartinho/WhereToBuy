using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhereToBuy.entities;

namespace WhereToBuy.core
{
    public partial class Supplements
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="supplement"></param>
        public void Store(Supplement supplement)
        {

            if (supplement.EditionMode == false)
            {
                // No futuro validar permissões
            }
            else
            {
                // No futuro validar permissões
            }


            try
            {
                engine.Data.Supplements.Store(supplement);
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
        /// <param name="supplement"></param>
        public void Delete(Supplement supplement)
        {

            // No futuro validar permissões

            try
            {
                engine.Data.Supplements.Delete(supplement);
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
