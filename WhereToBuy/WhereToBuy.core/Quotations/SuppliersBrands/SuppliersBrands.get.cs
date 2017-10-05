using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhereToBuy.entities;

namespace WhereToBuy.core
{
    public partial class SuppliersBrands
    {
        string _namespace = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
        //string _className = "SuppliersBrands";


        CoreEngine engine;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="engine"></param>
        public SuppliersBrands(CoreEngine engine)
        {
            this.engine = engine;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="brand"></param>
        /// <returns></returns>
        public SupplierBrand Get(Supplier supplier, Brand brand)
        {
            // No futuro validar permissões
            try
            {
                return engine.Data.SuppliersBrands.Get(supplier, brand);
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


        public SupplierBrand Get(string supplierCode, string brandCode, int setSupplierToLevel, int setBrandToLevel)
        {
            // No futuro validar permissões
            try
            {
                return engine.Data.SuppliersBrands.Get(supplierCode, brandCode, setSupplierToLevel, setBrandToLevel);
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
        public List<SupplierBrand> Get(int setSupplierToLevel, int setBrandToLevel)
        {

            try
            {
                return engine.Data.SuppliersBrands.Get(setSupplierToLevel, setBrandToLevel);
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
        /// <param name="setBrandToLevel"></param>
        /// <returns></returns>
        public List<SupplierBrand> Get(Supplier supplier, int setBrandToLevel)
        {

            try
            {
                return engine.Data.SuppliersBrands.Get(supplier, setBrandToLevel);
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
