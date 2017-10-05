using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhereToBuy.utils.GlobalVariables;

namespace WhereToBuy.data
{
    public enum SqlOperationType
    { Insert, Update, Delete }

    public class Enumerators
    {
        public string GetSqlOperationTypeString(SqlOperationType operation)
        {
            string translatedString = "";
            switch (operation)
            {
                case SqlOperationType.Insert: translatedString = GlobalVariables.Resource.GetString("InsertString", GlobalVariables.Culture);
                    break;
                case SqlOperationType.Update: translatedString = GlobalVariables.Resource.GetString("UpdateString", GlobalVariables.Culture);
                    break;
                case SqlOperationType.Delete: translatedString = GlobalVariables.Resource.GetString("InsertString", GlobalVariables.Culture);
                    break;
                default:;
                    break;
            }

            return translatedString;
        }
    }
}
