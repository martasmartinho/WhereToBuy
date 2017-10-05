using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhereToBuy.entities;

namespace WhereToBuy.data
{
    public partial class Users
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="dataState"></param>
        /// <returns></returns>
        public bool Exists(User user, DataState dataState)
        {
            return this.Exists(user.Username, dataState);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="dataState"></param>
        /// <returns></returns>
        public bool Exists(string username, DataState dataState)
        {
            if (Count(username, dataState) > 0)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="dataState"></param>
        /// <returns></returns>
        public bool Exists(string username, string password, DataState dataState)
        {
            if (Count(username, password, dataState) > 0)
            {
                return true;
            }

            return false;
        }
    }
}
