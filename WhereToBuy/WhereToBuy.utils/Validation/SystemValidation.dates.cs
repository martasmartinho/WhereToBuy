using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using WhereToBuy.utils;

namespace WhereToBuy.utils
{
    public static partial class SystemValidation
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="label"></param>
        /// <param name="value"></param>
        /// <param name="mandatory"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        public static bool Data(string label, DateTime? value, bool mandatory, DateTime min, DateTime max, ref string info)
        {
            bool flag1 = true;
            string str1 = "";
            if (!value.HasValue && mandatory)
            {
                string str2 = string.Format("{0}#{1}$ {2}", str1 , GlobalVariables.GlobalVariables.Resource.GetString("TypeString", GlobalVariables.GlobalVariables.Culture) ,  GlobalVariables.GlobalVariables.Resource.GetString("MustBeFilledString", GlobalVariables.GlobalVariables .Culture).ToLower());
                bool flag2 = false;
                info += string.Format("[{0}]", (object)label);
                info += str2;
                return flag2;
            }
            DateTime? nullable = value;
            DateTime dateTime1 = min;
            if ((nullable.HasValue ? (nullable.GetValueOrDefault() < dateTime1 ? 1 : 0) : 0) != 0)
            {
                str1 += string.Format("#{0}$ {1}", GlobalVariables.GlobalVariables.Resource.GetString("BoundString", GlobalVariables.GlobalVariables.Culture), GlobalVariables.GlobalVariables.Resource.GetString("LowerDateString", GlobalVariables.GlobalVariables.Culture).ToLower());
                flag1 = false;
            }
            nullable = value;
            DateTime dateTime2 = max;
            if ((nullable.HasValue ? (nullable.GetValueOrDefault() > dateTime2 ? 1 : 0) : 0) != 0)
            {
                str1 += string.Format("#{0}$ {1}", GlobalVariables.GlobalVariables.Resource.GetString("BoundString", GlobalVariables.GlobalVariables.Culture),  GlobalVariables.GlobalVariables.Resource.GetString("HigherDateString", GlobalVariables.GlobalVariables.Culture).ToLower());
                flag1 = false;
            }
            if (str1.Length > 0)
            {
                info += string.Format("[{0}]", (object)label);
                info += str1;
            }
            return flag1;
        }

    }
}
