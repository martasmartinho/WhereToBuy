using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhereToBuy.entities;

namespace WhereToBuy.data
{
    public partial class WorryingTerms
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="worryingTerm"></param>
        /// <param name="dataState"></param>
        /// <returns></returns>
        public bool Exists(WorryingTerm worryingTerm, DataState dataState)
        {
            return this.Exists(worryingTerm.Term, dataState);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="term"></param>
        /// <param name="dataState"></param>
        /// <returns></returns>
        public bool Exists(string term, DataState dataState)
        {
            if (Count(term, dataState) > 0)
            {
                return true;
            }

            return false;
        }
    }
}
