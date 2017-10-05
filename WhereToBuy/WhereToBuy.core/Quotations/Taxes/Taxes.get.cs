using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhereToBuy.entities;

namespace WhereToBuy.core
{
    public partial class Taxes
    {

        string _namespace = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
        string _className = "Tax";


        CoreEngine  engine;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="engine"></param>
        public Taxes(CoreEngine engine)
        {
            this.engine = engine;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public Tax Get(string code)
        {
            // No futuro validar permissões
            try
            {
                return engine.Data.Taxes.Get(code, DataState.All);
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
        public List<Tax> Get(DataState dataState)
        {

            try
            {
                return engine.Data.Taxes.Get(dataState);
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
        public List<Tax> Get(string code, string description, DataState dataState, string orderby)
        {

            try
            {
                return engine.Data.Taxes.Get(code.Split(' '), description.Split(' '), dataState, orderby);
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
        public List<Tax> Get(string searchString, bool returnList)
        {

            try
            {
                return engine.Data.Taxes.Get(searchString);
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
