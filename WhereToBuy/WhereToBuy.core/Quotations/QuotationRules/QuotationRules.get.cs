using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhereToBuy.entities;

namespace WhereToBuy.core
{
    public partial class QuotationRules
    {
        string _namespace = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
         //string _className = "QuotationRules";


        CoreEngine  engine;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="engine"></param>
        public QuotationRules(CoreEngine engine)
        {
            this.engine = engine;
        }


        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="supplier"></param>
        /// <param name="brand"></param>
        /// <param name="category"></param>
        /// <param name="stock"></param>
        /// <param name="withCustomization"></param>
        /// <param name="closeReset"></param>
        /// <param name="setSubstituteStockToLevel"></param>
        /// <returns></returns>
        public QuotationRule Get(Supplier supplier, Brand brand, Category category, Stock stock, bool withCustomization, bool closeReset, int setSubstituteStockToLevel)
        {
            // No futuro validar permissões
            try
            {
                return engine.Data.QuotationRules.Get(supplier, brand, category, stock, withCustomization, closeReset, setSubstituteStockToLevel);
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

        public QuotationRule Get(string supplier, string brand, string category, string stock, int setSubstituteStockToLevel)
        {
            // No futuro validar permissões
            try
            {
                return engine.Data.QuotationRules.Get(supplier, brand, category, stock, setSubstituteStockToLevel);
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
        /// <param name="supplier"></param>
        /// <param name="brand"></param>
        /// <param name="category"></param>
        /// <param name="stock"></param>
        /// <param name="withCustomization"></param>
        /// <param name="closeReset"></param>
        /// <param name="setSubstituteStockToLevel"></param>
        /// <returns></returns>
        public List<QuotationRule> Get(Supplier supplier, Brand brand, Category category, Stock stock, bool withCustomization, bool closeReset, int setSubstituteStockToLevel, string orderBy)
        {
            // No futuro validar permissões
            try
            {
                return engine.Data.QuotationRules.Get(supplier, brand, category, stock, withCustomization, closeReset, setSubstituteStockToLevel, orderBy);
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
        /// <param name="supplier"></param>
        /// <param name="withCustomization"></param>
        /// <param name="closeReset"></param>
        /// <param name="setBrandToLevel"></param>
        /// <param name="setCategoryToLevel"></param>
        /// <param name="setStockToLevel"></param>
        /// <param name="setSubstituteStockToLevel"></param>
        /// <returns></returns>
        public List<QuotationRule> Get(Supplier supplier, bool withCustomization, bool closeReset, int setBrandToLevel, int setCategoryToLevel, int setStockToLevel, int setSubstituteStockToLevel)
        {
            // No futuro validar permissões
            try
            {
                return engine.Data.QuotationRules.Get(supplier, withCustomization, closeReset, setBrandToLevel, setCategoryToLevel, setStockToLevel, setSubstituteStockToLevel);
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
        /// <param name="supplier"></param>
        /// <param name="brand"></param>
        /// <param name="withCustomization"></param>
        /// <param name="closeReset"></param>
        /// <param name="setCategoryToLevel"></param>
        /// <param name="setStockToLevel"></param>
        /// <param name="setSubstituteStockToLevel"></param>
        /// <returns></returns>
        public List<QuotationRule> Get(Supplier supplier, Brand brand, bool withCustomization, bool closeReset, int setCategoryToLevel, int setStockToLevel, int setSubstituteStockToLevel)
        {
            // No futuro validar permissões
            try
            {
                return engine.Data.QuotationRules.Get(supplier, brand, withCustomization, closeReset, setCategoryToLevel, setStockToLevel, setSubstituteStockToLevel);
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
        /// <param name="supplier"></param>
        /// <param name="brand"></param>
        /// <param name="category"></param>
        /// <param name="withCustomization"></param>
        /// <param name="closeReset"></param>
        /// <param name="setStockToLevel"></param>
        /// <param name="setSubstituteStockToLevel"></param>
        /// <returns></returns>
        public List<QuotationRule> Get(Supplier supplier, Brand brand, Category category, bool withCustomization, bool closeReset, int setStockToLevel, int setSubstituteStockToLevel)
        {
            // No futuro validar permissões
            try
            {
                return engine.Data.QuotationRules.Get(supplier, brand, category, withCustomization, closeReset, setStockToLevel, setSubstituteStockToLevel);
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
        /// <param name="withCustomization"></param>
        /// <param name="closeReset"></param>
        /// <param name="setSupplierToLevel"></param>
        /// <param name="setBrandToLevel"></param>
        /// <param name="setCategoryToLevel"></param>
        /// <param name="setStockToLevel"></param>
        /// <param name="setSubstituteStockToLevel"></param>
        /// <returns></returns>
        public List<QuotationRule> Get(bool withCustomization, bool closeReset, int setSupplierToLevel, int setBrandToLevel, int setCategoryToLevel, int setStockToLevel, int setSubstituteStockToLevel)
        {

            try
            {
                return engine.Data.QuotationRules.Get(withCustomization, closeReset, setSupplierToLevel, setBrandToLevel, setCategoryToLevel, setStockToLevel, setSubstituteStockToLevel);
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
