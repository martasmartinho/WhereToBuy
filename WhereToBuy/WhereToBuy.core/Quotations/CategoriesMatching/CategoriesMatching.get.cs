using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhereToBuy.entities;

namespace WhereToBuy.core
{
    public partial class CategoriesMatching
    {

        string _namespace = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
         string _className = "CategoriessMatching";


        CoreEngine  engine;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="engine"></param>
        public CategoriesMatching(CoreEngine engine)
        {
            this.engine = engine;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public CategoryMatching Get(Supplier supplier, string code, int setBrandToLevel)
        {
            // No futuro validar permissões
            try
            {
                return engine.Data.CategoriesMatching.Get(supplier, code, DataState.All, setBrandToLevel);
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


        public CategoryMatching Get(string supplierCode, string code, int setSupplierToLevel, int setBrandToLevel)
        {
            // No futuro validar permissões
            try
            {
                return engine.Data.CategoriesMatching.Get(supplierCode, code, DataState.All, setSupplierToLevel, setBrandToLevel);
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
        public List<CategoryMatching> Get(DataState dataState, int setSupplierToLevel, int setBrandToLevel)
        {

            try
            {
                return engine.Data.CategoriesMatching.Get(dataState, setSupplierToLevel, setBrandToLevel);
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
        public List<CategoryMatching> Get(Supplier supplier, string code, DataState dataState, string orderBy, int setCategoryToLevel)
        {

            try
            {
                return engine.Data.CategoriesMatching.Get(supplier, code, dataState, orderBy, setCategoryToLevel);
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
