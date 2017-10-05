using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhereToBuy.entities;

namespace WhereToBuy.core
{
    public partial class SupplementsMatching
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="supplementMatching"></param>
        public void Store(SupplementMatching supplementMatching)
        {

            if (supplementMatching.EditionMode == false)
            {
                // No futuro validar permissões
            }
            else
            {
                // No futuro validar permissões
            }


            try
            {
                engine.Data.SupplementsMatching.Store(supplementMatching);
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
        public void Delete(SupplementMatching supplementMatching)
        {

            // No futuro validar permissões

            try
            {
                engine.Data.SupplementsMatching.Delete(supplementMatching);
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
