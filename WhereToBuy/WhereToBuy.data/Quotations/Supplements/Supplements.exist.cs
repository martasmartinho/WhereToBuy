using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhereToBuy.entities;

namespace WhereToBuy.data
{
    public partial class Supplements
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="supplement"></param>
        /// <param name="dataState"></param>
        /// <returns></returns>
        public bool Exists(Supplement supplement, DataState dataState)
        {
            return this.Exists(supplement.Code, dataState);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <param name="dataState"></param>
        /// <returns></returns>
        public bool Exists(string code, DataState dataState)
        {
            if (Count(code, dataState) > 0)
            {
                return true;
            }

            return false;
        }
    }
}
