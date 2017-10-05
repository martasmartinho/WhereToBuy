using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhereToBuy.entities;

namespace WhereToBuy.core
{
    public partial class Languages
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="language"></param>
        public void Store(Language language)
        {

            if (language.EditionMode == false)
            {
                // No futuro validar permissões
            }
            else
            {
                // No futuro validar permissões
            }


            try
            {
                engine.Data.Languages.Store(language);
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
        /// <param name="language"></param>
        public void Delete(Language language)
        {

            // No futuro validar permissões

            try
            {
                engine.Data.Languages.Delete(language);
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
