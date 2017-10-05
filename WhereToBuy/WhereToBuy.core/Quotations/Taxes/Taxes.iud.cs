using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhereToBuy.entities;

namespace WhereToBuy.core
{
    public partial class Taxes
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tax"></param>
        public void Store(Tax tax)
        {

            if (tax.EditionMode == false)
            {
                // No futuro validar permissões
            }
            else
            {
                // No futuro validar permissões
            }


            try
            {
                engine.Data.Taxes.Store(tax);
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
        /// <param name="tax"></param>
        public void Delete(Tax tax)
        {

            // No futuro validar permissões

            try
            {
                engine.Data.Taxes.Delete(tax);
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
