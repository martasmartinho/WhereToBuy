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

        string _namespace = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
        string _className = "Stocks";


        CoreEngine engine;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="engine"></param>
        public Stocks(CoreEngine engine)
        {
            this.engine = engine;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public Stock Get(string code)
        {
            // No futuro validar permissões
            try
            {
                return engine.Data.Stocks.Get(code, DataState.All);
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
        /// <param name="code"></param>
        /// <returns></returns>
        public Stock Get(string code, DataState dataState, int setStockCodeExpirationP50ToLevel, int setStockCodeExpirationP60ToLevel, int setStockCodeExpirationP70ToLevel, int setStockCodeExpirationP80ToLevel, int setStockCodeExpirationP90ToLevel)
        {
            // No futuro validar permissões
            try
            {
                return engine.Data.Stocks.Get(code, dataState, setStockCodeExpirationP50ToLevel, setStockCodeExpirationP60ToLevel, setStockCodeExpirationP70ToLevel, setStockCodeExpirationP80ToLevel, setStockCodeExpirationP90ToLevel);
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
        public List<Stock> Get(DataState dataState, int setStockCodeExpirationP50ToLevel, int setStockCodeExpirationP60ToLevel, int setStockCodeExpirationP70ToLevel, int setStockCodeExpirationP80ToLevel, int setStockCodeExpirationP90ToLevel)
        {

            try
            {
                return engine.Data.Stocks.Get(dataState, setStockCodeExpirationP50ToLevel, setStockCodeExpirationP60ToLevel, setStockCodeExpirationP70ToLevel, setStockCodeExpirationP80ToLevel, setStockCodeExpirationP90ToLevel);
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
        /// <param name="code"></param>
        /// <param name="description"></param>
        /// <param name="dataState"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        public List<Stock> Get(string code, string description, DataState dataState, string orderby, int setStockCodeExpirationP50ToLevel, int setStockCodeExpirationP60ToLevel, int setStockCodeExpirationP70ToLevel, int setStockCodeExpirationP80ToLevel, int setStockCodeExpirationP90ToLevel)
        {

            try
            {
                return engine.Data.Stocks.Get(code.Split(' '), description.Split(' '), dataState, orderby, setStockCodeExpirationP50ToLevel, setStockCodeExpirationP60ToLevel, setStockCodeExpirationP70ToLevel, setStockCodeExpirationP80ToLevel, setStockCodeExpirationP90ToLevel);
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
        public List<Stock> Get(string searchString, bool returnList)
        {

            try
            {
                return engine.Data.Stocks.Get(searchString);
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
