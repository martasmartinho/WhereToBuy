using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhereToBuy.entities
{
    /// <summary>
    /// Thos class describes a grup
    /// </summary>
    [Serializable]
    public class Group: BaseEntity
    {
        #region Atributs

        private string description;

        #endregion

        #region Constructors

        /// <summary>
        /// Empty contructor
        /// </summary>
        public Group()
        { }

        /// <summary>
        /// Group constructor
        /// </summary>
        /// <param name="code">Group code</param>
        /// <param name="description">Group description</param>
        /// <param name="inactive">actice =true and inactive =false</param>
        /// <param name="editionMode">is in edition = true and id is not = false</param>
        public Group(string code, string description, bool inactive, bool editionMode)
            : base(code, inactive, editionMode)
        {
            
            this.description = description;
          
        }

        #endregion

        #region Properties

        /// <summary>
        /// Group description
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
            return string.Format("[{0}]-{1}", base.Code, description);
        }

        /// <summary>
        /// Check if one Group is equal to another Group by its code
        /// </summary>
        /// <param name="obj">Group to compare</param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj == null || obj == DBNull.Value)
            {
                return false;
            }

            return (base.Code == ((Group)obj).Code);
        }

        /// <summary>
        /// Check if two Groups are equal
        /// </summary>
        /// <param name="group1">Group one</param>
        /// <param name="group2">Group two</param>
        /// <returns></returns>
        public static bool operator ==(Group group1, Group group2)
        {
            if ((object)group1 == null && (object)group2 == null)
            {
                return true;
            }
            else if ((object)group1 == null ^ (object)group2 == null)
            {
                return false;
            }
            return group1.Equals(group2);
        }

        /// <summary>
        /// Check if two groups are diferent
        /// </summary>
        /// <param name="group1">group one</param>
        /// <param name="group2">group two</param>
        /// <returns></returns>
        public static bool operator !=(Group group1, Group group2)
        {
            if ((object)group1 == null && (object)group2 == null)
            {
                return false;
            }
            else if ((object)group1 == null ^ (object)group2 == null)
            {
                return true;
            }
            return !group1.Equals(group2);
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
