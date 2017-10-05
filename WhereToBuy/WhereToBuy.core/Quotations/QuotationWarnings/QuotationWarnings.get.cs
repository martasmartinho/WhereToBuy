using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhereToBuy.entities;

namespace WhereToBuy.core
{
    public partial class QuotationWarnings
    {
        string _namespace = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
         //string _className = "QuotationWarnings";


        CoreEngine  engine;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="engine"></param>
        public QuotationWarnings(CoreEngine engine)
        {
            this.engine = engine;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="supplier"></param>
        /// <param name="warningType"></param>
        /// <param name="orderBy"></param>
        /// <param name="setTypeToLevel"></param>
        /// <returns></returns>
        public List<QuotationWarning> Get(Supplier supplier, WarningType warningType, string orderBy, int setTypeToLevel)
        {

            try
            {
                return engine.Data.QuotationWarnings.Get(supplier, warningType, orderBy, setTypeToLevel);
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
