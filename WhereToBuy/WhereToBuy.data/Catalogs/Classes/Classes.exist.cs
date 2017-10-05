using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhereToBuy.entities;

namespace WhereToBuy.data
{
    public partial class Classes
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="classe"></param>
        /// <param name="dataState"></param>
        /// <returns></returns>
        public bool Exists(Classe classe, DataState dataState)
        {
            return this.Exists(classe.Code, dataState);
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
