using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhereToBuy.entities;

namespace WhereToBuy.core
{
    public partial class TaxesMatching
    {
        string _namespace = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
         string _className = "TaxesMatching";


        CoreEngine  engine;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="engine"></param>
        public TaxesMatching(CoreEngine engine)
        {
            this.engine = engine;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public TaxMatching Get(Supplier supplier, string code, int setTaxToLevel)
        {
            // No futuro validar permissões
            try
            {
                return engine.Data.TaxesMatching.Get(supplier, code, DataState.All, setTaxToLevel);
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


        public TaxMatching Get(string supplierCode, string code, int setSupplierToLevel, int setTaxToLevel)
        {
            // No futuro validar permissões
            try
            {
                return engine.Data.TaxesMatching.Get(supplierCode, code, DataState.All, setSupplierToLevel, setTaxToLevel);
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
        public List<TaxMatching> Get(DataState dataState, int setSupplierToLevel, int setTaxToLevel)
        {

            try
            {
                return engine.Data.TaxesMatching.Get(dataState, setSupplierToLevel, setTaxToLevel);
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
        public List<TaxMatching> Get(Supplier supplier, string code, DataState dataState, string orderBy, int setTaxToLevel)
        {

            try
            {
                return engine.Data.TaxesMatching.Get(supplier, code, dataState, orderBy, setTaxToLevel);
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
