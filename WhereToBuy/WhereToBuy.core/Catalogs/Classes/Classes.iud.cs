using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhereToBuy.entities;

namespace WhereToBuy.core
{
    public partial class Classes
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="classe"></param>
        public void Store(Classe classe)
        {

            if (classe.EditionMode == false)
            {
                // No futuro validar permissões
            }
            else
            {
                // No futuro validar permissões
            }


            try
            {
                engine.Data.Classes.Store(classe);
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
        /// <param name="classe"></param>
        public void Delete(Classe classe)
        {

            // No futuro validar permissões

            try
            {
                engine.Data.Classes.Delete(classe);
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
