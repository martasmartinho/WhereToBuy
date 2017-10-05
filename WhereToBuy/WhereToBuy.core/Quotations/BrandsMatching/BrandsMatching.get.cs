using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhereToBuy.entities;

namespace WhereToBuy.core
{
    public partial class BrandsMatching
    {
         string _namespace = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
         string _className = "BrandsMatching";


        CoreEngine  engine;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="engine"></param>
        public BrandsMatching(CoreEngine engine)
        {
            this.engine = engine;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public BrandMatching Get(Supplier supplier, string code, int setBrandToLevel)
        {
            // No futuro validar permissões
            try
            {
                return engine.Data.BrandsMatching.Get(supplier, code, DataState.All, setBrandToLevel);
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


        public BrandMatching Get(string supplierCode, string code, int setSupplierToLevel, int setBrandToLevel)
        {
            // No futuro validar permissões
            try
            {
                return engine.Data.BrandsMatching.Get(supplierCode, code, DataState.All, setSupplierToLevel, setBrandToLevel);
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
        public List<BrandMatching> Get(Supplier supplier, string code, DataState dataState, string orderBy, int setBrandToLevel)
        {

            try
            {
                return engine.Data.BrandsMatching.Get(supplier, code, dataState,orderBy, setBrandToLevel);
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
        public List<BrandMatching> Get(DataState dataState, int setSupplierToLevel, int setBrandToLevel)
        {

            try
            {
                return engine.Data.BrandsMatching.Get(dataState, setSupplierToLevel, setBrandToLevel);
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
