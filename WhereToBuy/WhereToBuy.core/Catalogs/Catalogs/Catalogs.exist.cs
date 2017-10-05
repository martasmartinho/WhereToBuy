using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhereToBuy.entities;
using WhereToBuy.utils;
using WhereToBuy.utils.GlobalVariables;

namespace WhereToBuy.core
{
    public partial class Catalogs
    {
        public bool Exists(string code, DataState dataState, ref string info)
        {

            try
            {

                if (engine.Data.Catalogs.Exists(code, dataState))
                {
                    return true;
                }

                switch (dataState)
                {
                    case DataState.Active:
                        if (engine.Data.Catalogs.Exists(code, DataState.Inactive))
                        {
                            info += string.Format("{0} {1}!", "Catalog", GlobalVariables.Resource.GetString("ExistInactiveString", GlobalVariables.Culture));
                            return false;
                        }

                        info += string.Format("{0} {1}!", "Catalog", GlobalVariables.Resource.GetString("NotExistString", GlobalVariables.Culture));
                        return false;

                    case DataState.Inactive:
                        if (engine.Data.Catalogs.Exists(code, DataState.Active))
                        {
                            info += string.Format("{0} {1}!", "Catalog", GlobalVariables.Resource.GetString("ExistActiveString", GlobalVariables.Culture));
                            return false;
                        }

                        info += string.Format("{0} {1}!", "Catalog", GlobalVariables.Resource.GetString("NotExistString", GlobalVariables.Culture));
                        return false;

                    case DataState.All:

                        info += string.Format("{0} {1}!", "Catalog", GlobalVariables.Resource.GetString("NotExistString", GlobalVariables.Culture));
                        return false;

                    default:
                        throw new MyException(_namespace, _className, "Exists()", string.Format("{0}!", GlobalVariables.Resource.GetString("ForeseenEnumeratorString", GlobalVariables.Culture).ToLower()));
                }
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
