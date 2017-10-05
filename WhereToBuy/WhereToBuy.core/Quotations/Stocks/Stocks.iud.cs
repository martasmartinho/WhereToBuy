using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhereToBuy.entities;

namespace WhereToBuy.core
{
    public partial class Stocks
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="stock"></param>
        public void Store(Stock stock)
        {

            if (stock.EditionMode == false)
            {
                // No futuro validar permissões
            }
            else
            {
                // No futuro validar permissões
            }


            try
            {
                engine.Data.Stocks.Store(stock);
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
        /// <param name="stock"></param>
        public void Delete(Stock stock)
        {

            // No futuro validar permissões

            try
            {
                engine.Data.Stocks.Delete(stock);
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
