using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhereToBuy.entities;

namespace WhereToBuy.core
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Brands
    {

        string _namespace = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
        string _className = "Brand";


        CoreEngine  engine;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="engine"></param>
        public Brands(CoreEngine engine)
        {
            this.engine = engine;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public Brand Get(string code)
        {
            // No futuro validar permissões
            try
            {
                return engine.Data.Brands.Get(code, DataState.All);
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
        public List<Brand> Get(string searchString, bool returnList)
        {

            try
            {
                return engine.Data.Brands.Get(searchString);
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
        public List<Brand> Get(DataState dataState)
        {

            try
            {
                return engine.Data.Brands.Get(dataState);
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
        /// <param name="description"></param>
        /// <param name="dataState"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        public List<Brand> Get(string code, string description, DataState dataState, string orderby)
        {
            string[] codes = { };
            string[] descriptions = { };

            if (code != "")
            {
                codes = code.Split(' ');
            }
            if (description != "")
            {
                descriptions = description.Split(' ');
            }
           
            try
            {
                return engine.Data.Brands.Get(codes, descriptions, dataState, orderby);
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
