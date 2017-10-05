using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhereToBuy.entities;

namespace WhereToBuy.core
{
    public partial class ProductsMatching
    {
        string _namespace = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
         string _className = "ProductsMatching";


        CoreEngine  engine;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="engine"></param>
        public ProductsMatching(CoreEngine engine)
        {
            this.engine = engine;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public ProductMatching Get(Supplier supplier, string code, string supplementCode, int setProductToLevel, int setStockToLevel)
        {
            // No futuro validar permissões
            try
            {
                return engine.Data.ProductsMatching.Get(supplier, code, supplementCode, DataState.All, setProductToLevel, setStockToLevel);
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


        public ProductMatching Get(string supplierCode, string code, string supplementCode, int setSupplierToLevel, int setProductToLevel, int setStockToLevel)
        {
            // No futuro validar permissões
            try
            {
                return engine.Data.ProductsMatching.Get(supplierCode, code, supplementCode, DataState.All, setSupplierToLevel, setProductToLevel, setStockToLevel);
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
        public List<ProductMatching> Get(Supplier supplier, string code, string supplementCode, DataState dataState, bool withCustomization, bool closeReset, string orderBy, int setProductToLevel, int setStockToLevel)
        {

            try
            {
                return engine.Data.ProductsMatching.Get(supplier, code, supplementCode, dataState, withCustomization, closeReset, orderBy, setProductToLevel, setStockToLevel);
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
        public List<ProductMatching> Get(DataState dataState, int setSupplierToLevel, int setProductToLevel, int setStockToLevel)
        {

            try
            {
                return engine.Data.ProductsMatching.Get(dataState, setSupplierToLevel, setProductToLevel, setStockToLevel);
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
