using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhereToBuy.entities;

namespace WhereToBuy.core
{
    public partial class WorryingTerms
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="worryingTerm"></param>
        public void Store(WorryingTerm worryingTerm)
        {

            if (worryingTerm.EditionMode == false)
            {
                // No futuro validar permissões
            }
            else
            {
                // No futuro validar permissões
            }


            try
            {
                engine.Data.WorryingTerms.Store(worryingTerm);
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
        /// <param name="worryingTerm"></param>
        public void Delete(WorryingTerm worryingTerm)
        {

            // No futuro validar permissões

            try
            {
                engine.Data.WorryingTerms.Delete(worryingTerm);
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
