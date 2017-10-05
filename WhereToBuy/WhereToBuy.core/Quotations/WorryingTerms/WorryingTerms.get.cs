using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhereToBuy.entities;

namespace WhereToBuy.core
{
    public partial class WorryingTerms
    {

        string _namespace = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
        string _className = "WorryingTerm";


        CoreEngine  engine;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="engine"></param>
        public WorryingTerms(CoreEngine engine)
        {
            this.engine = engine;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns>
        public WorryingTerm Get(string term)
        {
            // No futuro validar permissões
            try
            {
                return engine.Data.WorryingTerms.Get(term, DataState.All);
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
        public List<WorryingTerm> Get(DataState dataState)
        {

            try
            {
                return engine.Data.WorryingTerms.Get(dataState);
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
        public List<WorryingTerm> Get(string code, DataState dataState, string orderby)
        {

            try
            {
                return engine.Data.WorryingTerms.Get(code.Split(' '), dataState, orderby);
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
