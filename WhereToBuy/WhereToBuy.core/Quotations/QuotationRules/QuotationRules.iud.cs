using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhereToBuy.entities;

namespace WhereToBuy.core
{
    public partial class QuotationRules
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="quotationRule"></param>
        public void Store(QuotationRule quotationRule)
        {

            if (quotationRule.EditionMode == false)
            {
                // No futuro validar permissões
            }
            else
            {
                // No futuro validar permissões
            }


            try
            {
                engine.Data.QuotationRules.Store(quotationRule);
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
        /// <param name="quotationRule"></param>
        public void Delete(QuotationRule    quotationRule)
        {

            // No futuro validar permissões

            try
            {
                engine.Data.QuotationRules.Delete(quotationRule);
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
