using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhereToBuy.entities;

namespace WhereToBuy.core
{
    public partial class WarningTypes
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="warningType"></param>
        public void Store(WarningType warningType)
        {

            if (warningType.EditionMode == false)
            {
                // No futuro validar permissões
            }
            else
            {
                // No futuro validar permissões
            }


            try
            {
                engine.Data.WarningTypes.Store(warningType);
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
        /// <param name="warningType"></param>
        public void Delete(WarningType warningType)
        {

            // No futuro validar permissões

            try
            {
                engine.Data.WarningTypes.Delete(warningType);
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
