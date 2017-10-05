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
        string _namespace = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
         string _className = "StocksMatching";


        CoreEngine  engine;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="engine"></param>
        public StocksMatching(CoreEngine engine)
        {
            this.engine = engine;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public StockMatching Get(Supplier supplier, string code, int setStockToLevel)
        {
            // No futuro validar permissões
            try
            {
                return engine.Data.StocksMatching.Get(supplier, code, DataState.All, setStockToLevel);
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


        public StockMatching Get(string supplierCode, string code, int setSupplierToLevel, int setStockToLevel)
        {
            // No futuro validar permissões
            try
            {
                return engine.Data.StocksMatching.Get(supplierCode, code, DataState.All, setSupplierToLevel, setStockToLevel);
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
        /// <param name="dataState"></param>
        /// <returns></returns>
        public List<StockMatching> Get(DataState dataState, int setSupplierToLevel, int setStockToLevel)
        {

            try
            {
                return engine.Data.StocksMatching.Get(dataState, setSupplierToLevel, setStockToLevel);
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
        /// <param name="dataState"></param>
        /// <returns></returns>
        public List<StockMatching> Get(Supplier supplier, string code, DataState dataState, string orderBy, int setStockToLevel)
        {

            try
            {
                return engine.Data.StocksMatching.Get(supplier, code, dataState, orderBy, setStockToLevel);
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
