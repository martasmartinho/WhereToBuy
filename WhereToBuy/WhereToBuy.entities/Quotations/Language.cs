using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhereToBuy.entities
{
    /// <summary>
    /// this class describes a language
    /// </summary>
    [Serializable]
    public class Language:BaseEntity 
    {
        #region Atributs

        private string description;

        #endregion

        #region Constructors

        /// <summary>
        /// Empty contructor
        /// </summary>
        public Language()
        { }

        /// <summary>
        /// Language constructor
        /// </summary>
        /// <param name="code">Language code</param>
        /// <param name="description">Language description</param>
        /// <param name="inactive">actice =true and inactive =false</param>
        /// <param name="editionMode">is in edition = true and id is not = false</param>
        public Language(string code, string description, bool inactive, bool editionMode)
            : base(code, inactive, editionMode)
        {
            
            this.description = description;
          
        }

        #endregion

        #region Properties

        /// <summary>
        /// Language description
        /// </summary>
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        #endregion


        #region OverrideMethods

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("[{0}] {1}", base.Code, description);
        }

        /// <summary>
        /// Check if one language is equal to another language by its code
        /// </summary>
        /// <param name="obj">language to compare</param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj == null || obj == DBNull.Value)
            {
                return false;
            }

            return (base.Code == ((Language)obj).Code);
        }

        /// <summary>
        /// Check if two language are equal
        /// </summary>
        /// <param name="language1">language one</param>
        /// <param name="language2">language two</param>
        /// <returns></returns>
        public static bool operator ==(Language language1, Language language2)
        {
            if ((object)language1 == null && (object)language2 == null)
            {
                return true;
            }
            else if ((object)language1 == null ^ (object)language2 == null)
            {
                return false;
            }
            return language1.Equals(language2);
        }

        /// <summary>
        /// Check if two language are diferent
        /// </summary>
        /// <param name="language1">language one</param>
        /// <param name="language2">language two</param>
        /// <returns></returns>
        public static bool operator !=(Language language1, Language language2)
        {
            if ((object)language1 == null && (object)language2 == null)
            {
                return false;
            }
            else if ((object)language1 == null ^ (object)language2 == null)
            {
                return true;
            }
            return !language1.Equals(language2);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            int hashSignature = 13 * 36;

            if (base.Code == null)
            {
                return hashSignature;
            }
            return hashSignature * base.Code.GetHashCode();
        }


        #endregion
    }
}
