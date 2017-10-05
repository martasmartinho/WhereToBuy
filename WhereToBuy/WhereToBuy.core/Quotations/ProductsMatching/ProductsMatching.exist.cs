using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhereToBuy.entities;
using WhereToBuy.utils.GlobalVariables;

namespace WhereToBuy.core
{
    public partial class ProductsMatching
    {
        public bool Exists(Supplier supplier, string code, string supplementCode, DataState dataState, ref string info)
        {

            try
            {

                if (engine.Data.ProductsMatching.Exists(supplier, code, supplementCode, dataState))
                {
                    return true;
                }

                switch (dataState)
                {
                    case DataState.Active:
                        if (engine.Data.ProductsMatching.Exists(supplier, code, supplementCode, DataState.Inactive))
                        {
                            info += string.Format("{0} {1}!", GlobalVariables.Resource.GetString("ProductMatchingString", GlobalVariables.Culture), GlobalVariables.Resource.GetString("ExistInactiveString", GlobalVariables.Culture));
                            return false;
                        }

                        info += string.Format("{0} {1}!", GlobalVariables.Resource.GetString("ProductMatchingString", GlobalVariables.Culture), GlobalVariables.Resource.GetString("NotExistString", GlobalVariables.Culture));
                        return false;

                    case DataState.Inactive:
                        if (engine.Data.ProductsMatching.Exists(supplier, code, supplementCode, DataState.Active))
                        {
                            info += string.Format("{0} {1}!", GlobalVariables.Resource.GetString("ProductMatchingString", GlobalVariables.Culture), GlobalVariables.Resource.GetString("ExistActiveString", GlobalVariables.Culture));
                            return false;
                        }

                        info += string.Format("{0} {1}!", GlobalVariables.Resource.GetString("ProductMatchingString", GlobalVariables.Culture), GlobalVariables.Resource.GetString("NotExistString", GlobalVariables.Culture));
                        return false;

                    case DataState.All:

                        info += string.Format("{0} {1}!", GlobalVariables.Resource.GetString("ProductMatchingString", GlobalVariables.Culture), GlobalVariables.Resource.GetString("NotExistString", GlobalVariables.Culture));
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


        public bool Exists(string supplierCode, string code, string supplementCode, DataState dataState, ref string info)
        {

            try
            {

                if (engine.Data.ProductsMatching.Exists(supplierCode, code, supplementCode, dataState))
                {
                    return true;
                }

                switch (dataState)
                {
                    case DataState.Active:
                        if (engine.Data.ProductsMatching.Exists(supplierCode, code, supplementCode, DataState.Inactive))
                        {
                            info += string.Format("{0} {1}!", GlobalVariables.Resource.GetString("ProductMatchingString", GlobalVariables.Culture), GlobalVariables.Resource.GetString("ExistInactiveString", GlobalVariables.Culture));
                            return false;
                        }

                        info += string.Format("{0} {1}!", GlobalVariables.Resource.GetString("ProductMatchingString", GlobalVariables.Culture), GlobalVariables.Resource.GetString("NotExistString", GlobalVariables.Culture));
                        return false;

                    case DataState.Inactive:
                        if (engine.Data.ProductsMatching.Exists(supplierCode, code, supplementCode, DataState.Active))
                        {
                            info += string.Format("{0} {1}!", GlobalVariables.Resource.GetString("ProductMatchingString", GlobalVariables.Culture), GlobalVariables.Resource.GetString("ExistActiveString", GlobalVariables.Culture));
                            return false;
                        }

                        info += string.Format("{0} {1}!", GlobalVariables.Resource.GetString("ProductMatchingString", GlobalVariables.Culture), GlobalVariables.Resource.GetString("NotExistString", GlobalVariables.Culture));
                        return false;

                    case DataState.All:

                        info += string.Format("{0} {1}!", GlobalVariables.Resource.GetString("ProductMatchingString", GlobalVariables.Culture), GlobalVariables.Resource.GetString("NotExistString", GlobalVariables.Culture));
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
