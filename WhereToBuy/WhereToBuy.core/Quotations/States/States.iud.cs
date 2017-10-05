using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhereToBuy.entities;

namespace WhereToBuy.core
{
    public partial class States
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="state"></param>
        public void Store(State state)
        {

            if (state.EditionMode == false)
            {
                // No futuro validar permissões
            }
            else
            {
                // No futuro validar permissões
            }


            try
            {
                engine.Data.States.Store(state);
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
        /// <param name="state"></param>
        public void Delete(State state)
        {

            // No futuro validar permissões

            try
            {
                engine.Data.States.Delete(state);
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
