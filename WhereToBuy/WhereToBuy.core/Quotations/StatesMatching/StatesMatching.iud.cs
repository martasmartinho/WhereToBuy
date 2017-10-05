using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhereToBuy.entities;

namespace WhereToBuy.core
{
    public partial class StatesMatching
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="stateMatching"></param>
        public void Store(StateMatching stateMatching)
        {

            if (stateMatching.EditionMode == false)
            {
                // No futuro validar permissões
            }
            else
            {
                // No futuro validar permissões
            }


            try
            {
                engine.Data.StatesMatching.Store(stateMatching);
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
        public void Delete(StateMatching stateMatching)
        {

            // No futuro validar permissões

            try
            {
                engine.Data.StatesMatching.Delete(stateMatching);
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
