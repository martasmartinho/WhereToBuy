using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhereToBuy.entities;

namespace WhereToBuy.core
{
    public partial class ProductDetails
    {
        string _namespace = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
        string _className = "ProductDetail";


        CoreEngine engine;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="engine"></param>
        public ProductDetails(CoreEngine engine)
        {
            this.engine = engine;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns>
        public ProductDetail Get(string productCode, string supplierCode)
        {
            // No futuro validar permissões
            try
            {
                return engine.Data.ProductDetails.Get(productCode, supplierCode, DataState.All);
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
        public List<ProductDetail> Get(string productCode, DataState dataState)
        {

            try
            {
                return engine.Data.ProductDetails.Get(productCode, dataState);
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
        /// <param name="dataState"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        public List<ProductDetail> Get(string code, DataState dataState, string orderby)
        {

            try
            {
                return engine.Data.ProductDetails.Get(code.Split(' '), dataState, orderby);
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
