using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhereToBuy.entities;
using WhereToBuy.utils.GlobalVariables;

namespace WhereToBuy.core
{
    public partial class QuotationRules
    {
        public bool Exists(Supplier supplier, Brand brand, Category category, Stock stock, ref string info)
        {

            try
            {

                if (engine.Data.QuotationRules.Exists(supplier, brand, category, stock))
                {
                    return true;
                }

                info += string.Format("{0} {1}!", GlobalVariables.Resource.GetString("QuotationRuleString", GlobalVariables.Culture), GlobalVariables.Resource.GetString("NotExistString", GlobalVariables.Culture));
                return false;

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
