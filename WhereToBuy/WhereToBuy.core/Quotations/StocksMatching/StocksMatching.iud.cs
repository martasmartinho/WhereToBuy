using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhereToBuy.entities;

namespace WhereToBuy.core
{
    public partial class StocksMatching
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="brandMatching"></param>
        public void Store(StockMatching stockMatching)
        {

            if (stockMatching.EditionMode == false)
            {
                // No futuro validar permissões
            }
            else
            {
                // No futuro validar permissões
            }


            try
            {
                engine.Data.StocksMatching.Store(stockMatching);
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
        public void Delete(StockMatching stockMatching)
        {

            // No futuro validar permissões

            try
            {
                engine.Data.StocksMatching.Delete(stockMatching);
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
