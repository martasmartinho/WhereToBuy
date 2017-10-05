using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhereToBuy.entities;

namespace WhereToBuy.core
{
    public partial class Catalogs
    {

        string _namespace = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
        string _className = "Catalogs";


        CoreEngine  engine;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="engine"></param>
        public Catalogs(CoreEngine engine)
        {
            this.engine = engine;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public Catalog Get(string code)
        {
            // No futuro validar permissões
            try
            {
                return engine.Data.Catalogs.Get(code, DataState.All);
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
        public List<Catalog> Get(DataState dataState)
        {

            try
            {
                return engine.Data.Catalogs.Get(dataState);
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
        public List<Catalog> Get(string code, string description, DataState dataState, string orderby)
        {

            try
            {
                return engine.Data.Catalogs.Get(code.Split(' '), description.Split(' '), dataState, orderby);
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

        public List<Catalog> Get(string code, bool returnList)
        {
            // No futuro validar permissões
            try
            {
                return engine.Data.Catalogs.Get(DataState.Active);
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
