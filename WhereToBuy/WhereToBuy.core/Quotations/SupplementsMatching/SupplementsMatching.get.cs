using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhereToBuy.entities;

namespace WhereToBuy.core
{
    public partial class SupplementsMatching
    {
        string _namespace = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
         string _className = "SupplementsMatching";


        CoreEngine  engine;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="engine"></param>
        public SupplementsMatching(CoreEngine engine)
        {
            this.engine = engine;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public SupplementMatching Get(Supplier supplier, string code, int setSupplementToLevel)
        {
            // No futuro validar permissões
            try
            {
                return engine.Data.SupplementsMatching.Get(supplier, code, DataState.All, setSupplementToLevel);
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


        public SupplementMatching Get(string supplierCode, string code, int setSupplierToLevel, int setSupplementToLevel)
        {
            // No futuro validar permissões
            try
            {
                return engine.Data.SupplementsMatching.Get(supplierCode, code, DataState.All, setSupplierToLevel, setSupplementToLevel);
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
        public List<SupplementMatching> Get(DataState dataState, int setSupplierToLevel, int setSupplementToLevel)
        {

            try
            {
                return engine.Data.SupplementsMatching.Get(dataState, setSupplierToLevel, setSupplementToLevel);
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
        public List<SupplementMatching> Get(Supplier supplier, string code, DataState dataState, string orderBy, int setSupplementToLevel)
        {

            try
            {
                return engine.Data.SupplementsMatching.Get(supplier, code, dataState, orderBy, setSupplementToLevel);
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
