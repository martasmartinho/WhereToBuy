using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhereToBuy.entities;

namespace WhereToBuy.core
{
    public partial class Products
    {

         string _namespace = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
        string _className = "Products";


        CoreEngine  engine;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="engine"></param>
        public Products(CoreEngine engine)
        {
            this.engine = engine;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public Product Get(string code, int setDetails)
        {
            // No futuro validar permissões
            try
            {
                return engine.Data.Products.Get(code, DataState.All, setDetails);
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
        public Product Get(string code, int setCategoryToLevel, 
                            int setBrandToLevel, int setSupplierToLevel, int setTaxToLevel, int setStockToLevel, int setDetails)
        {
            // No futuro validar permissões
            try
            {
                return engine.Data.Products.Get(code, DataState.All, setCategoryToLevel, 
                            setBrandToLevel, setSupplierToLevel, setTaxToLevel, setStockToLevel, setDetails);
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
        public List<Product> Get(DataState dataState, int setCategoryToLevel,
                            int setBrandToLevel, int setSupplierToLevel, int setTaxToLevel, int setStockToLevel, bool? discontinued)
        {

            try
            {
                return engine.Data.Products.Get(dataState, setCategoryToLevel,
                                            setBrandToLevel, setSupplierToLevel, setTaxToLevel, setStockToLevel, discontinued);
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
        public List<Product> Get(string searchString, bool returnList)
        {

            try
            {
                return engine.Data.Products.Get(searchString);
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
        public List<Product> Get(ProductFilter filter)
        {

            try
            {
                return engine.Data.Products.Get(filter);
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
        /// <param name="filter"></param>
        /// <param name="brand"></param>
        /// <param name="category"></param>
        /// <param name="supplier"></param>
        /// <param name="partnumber"></param>
        /// <returns></returns>
        public List<Product> Get(ProductFilter filter, Brand brand, Category category, Supplier supplier, string partnumber, string orderBy)
        {

            try
            {
                return engine.Data.Products.Get(filter, brand, category, supplier, partnumber, orderBy);
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
