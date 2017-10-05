using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhereToBuy.entities
{
    /// <summary>
    /// This class describes a user
    /// </summary>
    [Serializable]
    public class User:BaseEntity1
    {
         #region Atributs

        private string username;
        private string password;
        private string name;
        private string email;
        private string mobile;
        private string sms;
        private bool administrator;
        private Language language;

        #endregion

        #region Constructors

        /// <summary>
        /// Empty contructor
        /// </summary>
        public User()
        { }

       /// <summary>
       /// Constructor
       /// </summary>
       /// <param name="username">User username</param>
       /// <param name="password">User password</param>
       /// <param name="name">User name</param>
       /// <param name="inactive">is inactive</param>
       /// <param name="editionMode">is in edition mode</param>
        public User(string username, string password, string name, string email, string mobile, string sms, bool administrator, bool inactive, bool editionMode):base(inactive, editionMode)
        {
            
            this.username = username;
            this.password = password;
            this.name = name;
            this.email = email;
            this.mobile = mobile;
            this.sms = sms;
            this.administrator = administrator;
          
        }

        #endregion

        #region Properties

        /// <summary>
        /// User username
        /// </summary>
        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        /// <summary>
        /// User password
        /// </summary>
        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        /// <summary>
        /// User name
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        /// User Email
        /// </summary>
        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        /// <summary>
        /// User mobile
        /// </summary>
        public string Mobile
        {
            get { return mobile; }
            set { mobile = value; }
        }

        /// <summary>
        /// User Sms
        /// </summary>
        public string Sms
        {
            get { return sms; }
            set { sms = value; }
        }

        /// <summary>
        /// User has administrator account
        /// </summary>
        public bool Administrator
        {
            get { return administrator; }
            set { administrator = value; }
        }

        /// <summary>
        /// User language
        /// </summary>
        public Language Language
        {
            get { return language; }
            set { language = value; }
        }

        #endregion

        #region OverrideMethods

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return name;
        }

        /// <summary>
        /// Check if one user is equal to another user by its username
        /// </summary>
        /// <param name="obj">user to compare</param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj == null || obj == DBNull.Value)
            {
                return false;
            }

            return (username == ((User)obj).Username);
        }

        /// <summary>
        /// Check if two users are equal
        /// </summary>
        /// <param name="user1">user one</param>
        /// <param name="user2">user two</param>
        /// <returns></returns>
        public static bool operator ==(User user1, User user2)
        {
            if ((object)user1 == null && (object)user2 == null)
            {
                return true;
            }
            else if ((object)user1 == null ^ (object)user2 == null)
            {
                return false;
            }
            return user1.Equals(user2);
        }

        /// <summary>
        /// Check if two users are diferent
        /// </summary>
        /// <param name="user1">user one</param>
        /// <param name="user2">user two</param>
        /// <returns></returns>
        public static bool operator !=(User user1, User user2)
        {
            if ((object)user1 == null && (object)user2 == null)
            {
                return false;
            }
            else if ((object)user1 == null ^ (object)user2 == null)
            {
                return true;
            }
            return !user1.Equals(user2);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            int hashSignature = 13 * 36;

            if (username == null)
            {
                return hashSignature;
            }
            return hashSignature * username.GetHashCode();
        }


        #endregion

    }
}
