using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhereToBuy.entities;

namespace WhereToBuy.core
{
    public partial class StatesMatching
    {
        string _namespace = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
         string _className = "StatesMatching";


        CoreEngine  engine;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="engine"></param>
        public StatesMatching(CoreEngine engine)
        {
            this.engine = engine;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public StateMatching Get(Supplier supplier, string code, int setStateToLevel)
        {
            // No futuro validar permissões
            try
            {
                return engine.Data.StatesMatching.Get(supplier, code, DataState.All, setStateToLevel);
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


        public StateMatching Get(string supplierCode, string code, int setSupplierToLevel, int setStateToLevel)
        {
            // No futuro validar permissões
            try
            {
                return engine.Data.StatesMatching.Get(supplierCode, code, DataState.All, setSupplierToLevel, setStateToLevel);
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
        public List<StateMatching> Get(DataState dataState, int setSupplierToLevel, int setStateToLevel)
        {

            try
            {
                return engine.Data.StatesMatching.Get(dataState, setSupplierToLevel, setStateToLevel);
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
        public List<StateMatching> Get(Supplier supplier, string code, DataState dataState, string orderBy, int setStateToLevel)
        {

            try
            {
                return engine.Data.StatesMatching.Get(supplier, code, dataState, orderBy, setStateToLevel);
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
