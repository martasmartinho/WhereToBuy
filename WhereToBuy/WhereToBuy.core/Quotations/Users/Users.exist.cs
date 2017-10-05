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
    public partial class Users
    {

        public bool Exists(string username, DataState dataState, ref string info)
        {

            try
            {

                if (engine.Data.Users.Exists(username, dataState))
                {
                    return true;
                }

                switch (dataState)
                {
                    case DataState.Active:
                        if (engine.Data.Users.Exists(username, DataState.Inactive))
                        {
                            info += string.Format("{0} {1}!", GlobalVariables.Resource.GetString("UserString", GlobalVariables.Culture), GlobalVariables.Resource.GetString("ExistInactiveString", GlobalVariables.Culture));
                            return false;
                        }

                        info += string.Format("{0} {1}!", GlobalVariables.Resource.GetString("UserString", GlobalVariables.Culture), GlobalVariables.Resource.GetString("NotExistString", GlobalVariables.Culture));
                        return false;

                    case DataState.Inactive:
                        if (engine.Data.Users.Exists(username, DataState.Active))
                        {
                            info += string.Format("{0} {1}!", GlobalVariables.Resource.GetString("UserString", GlobalVariables.Culture), GlobalVariables.Resource.GetString("ExistActiveString", GlobalVariables.Culture));
                            return false;
                        }

                        info += string.Format("{0} {1}!", GlobalVariables.Resource.GetString("UserString", GlobalVariables.Culture), GlobalVariables.Resource.GetString("NotExistString", GlobalVariables.Culture));
                        return false;

                    case DataState.All:

                        info += string.Format("{0} {1}!", GlobalVariables.Resource.GetString("UserString", GlobalVariables.Culture), GlobalVariables.Resource.GetString("NotExistString", GlobalVariables.Culture));
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


        public bool Exists(string username, string password, DataState dataState, ref string info)
        {

            try
            {

                if (engine.Data.Users.Exists(username, password, dataState))
                {
                    return true;
                }

                switch (dataState)
                {
                    case DataState.Active:
                        if (engine.Data.Users.Exists(username, password, DataState.Inactive))
                        {
                            info += string.Format("{0} {1}!", GlobalVariables.Resource.GetString("UserString", GlobalVariables.Culture), GlobalVariables.Resource.GetString("ExistInactiveString", GlobalVariables.Culture));
                            return false;
                        }

                        info += string.Format("{0} {1}!", GlobalVariables.Resource.GetString("UserString", GlobalVariables.Culture), GlobalVariables.Resource.GetString("NotExistString", GlobalVariables.Culture));
                        return false;

                    case DataState.Inactive:
                        if (engine.Data.Users.Exists(username, password, DataState.Active))
                        {
                            info += string.Format("{0} {1}!", GlobalVariables.Resource.GetString("UserString", GlobalVariables.Culture), GlobalVariables.Resource.GetString("ExistActiveString", GlobalVariables.Culture));
                            return false;
                        }

                        info += string.Format("{0} {1}!", GlobalVariables.Resource.GetString("UserString", GlobalVariables.Culture), GlobalVariables.Resource.GetString("NotExistString", GlobalVariables.Culture));
                        return false;

                    case DataState.All:

                        info += string.Format("{0} {1}!", GlobalVariables.Resource.GetString("UserString", GlobalVariables.Culture), GlobalVariables.Resource.GetString("NotExistString", GlobalVariables.Culture));
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
