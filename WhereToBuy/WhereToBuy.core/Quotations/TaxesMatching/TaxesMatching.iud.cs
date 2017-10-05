using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhereToBuy.entities;

namespace WhereToBuy.core
{
    public partial class TaxesMatching
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="taxMatching"></param>
        public void Store(TaxMatching taxMatching)
        {

            if (taxMatching.EditionMode == false)
            {
                // No futuro validar permissões
            }
            else
            {
                // No futuro validar permissões
            }


            try
            {
                engine.Data.TaxesMatching.Store(taxMatching);
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
        /// <param name="taxMatching"></param>
        public void Delete(TaxMatching taxMatching)
        {

            // No futuro validar permissões

            try
            {
                engine.Data.TaxesMatching.Delete(taxMatching);
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
