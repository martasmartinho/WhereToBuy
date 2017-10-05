using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhereToBuy.entities
{
    /// <summary>
    /// this class describes a brand
    /// </summary>
    [Serializable]
    public class Brand : BaseEntity
    {
        #region Atributs

        private string description;

        #endregion

        #region Constructors

        /// <summary>
        /// Empty contructor
        /// </summary>
        public Brand()
        { }

        /// <summary>
        /// Brand constructor
        /// </summary>
        /// <param name="code">Brand code</param>
        /// <param name="description">Brand description</param>
        /// <param name="inactive">actice =true and inactive =false</param>
        /// <param name="editionMode">is in edition = true and id is not = false</param>
        public Brand(string code, string description, bool inactive, bool editionMode):base(code, inactive, editionMode)
        {
            
            this.description = description;
          
        }

        #endregion

        #region Properties

        /// <summary>
        /// Brand description
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
        /// Check if one brand is equal to another brand by its code
        /// </summary>
        /// <param name="obj">brand to compare</param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj == null || obj == DBNull.Value)
            {
                return false;
            }

            return (base.Code == ((Brand)obj).Code);
        }

        /// <summary>
        /// Check if two brands are equal
        /// </summary>
        /// <param name="brand1">brand one</param>
        /// <param name="brand2">brand two</param>
        /// <returns></returns>
        public static bool operator ==(Brand brand1, Brand brand2)
        {
            if ((object)brand1 == null && (object)brand2 == null)
            {
                return true;
            }
            else if ((object)brand1 == null ^ (object)brand2 == null)
            {
                return false;
            }
            return brand1.Equals(brand2);
        }

        /// <summary>
        /// Check if two brands are diferent
        /// </summary>
        /// <param name="brand1">brand one</param>
        /// <param name="brand2">brand two</param>
        /// <returns></returns>
        public static bool operator !=(Brand brand1, Brand brand2)
        {
            if ((object)brand1 == null && (object)brand2 == null)
            {
                return false;
            }
            else if ((object)brand1 == null ^ (object)brand2 == null)
            {
                return true;
            }
            return !brand1.Equals(brand2);
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
