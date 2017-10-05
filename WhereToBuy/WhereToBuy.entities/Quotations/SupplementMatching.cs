using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhereToBuy.entities
{
    /// <summary>
    /// This classe describes a matching between external supplement from a supllier and system internal supplement
    /// </summary>
    [Serializable]
    public class SupplementMatching : BaseEntity
    {
        #region Atributs

        private Supplier supplier;
        private string description;
        private Supplement mapTo;
        private Dictionary<string, object> metaInfo;

        #endregion

        #region Constructors

        /// <summary>
        /// Empty contructor
        /// </summary>
        public SupplementMatching()
        { }

        /// <summary>
        /// SupplementMatching constructor
        /// </summary>
        /// <param name="supplier">Supplier</param>
        /// <param name="code">External supplement code</param>
        /// <param name="description">External supplement description</param>
        /// <param name="mapTo">Internal supplement</param>
        /// <param name="inactive">actice =true and inactive =false</param>
        /// <param name="editionMode">is in edition = true and id is not = false</param>
        public SupplementMatching(Supplier supplier, string code, string description, Supplement mapTo, bool inactive, bool editionMode)
            : base(code, inactive, editionMode)
        {

            this.supplier = supplier;
            this.description = description;
            this.mapTo = mapTo;

        }

        /// <summary>
        /// SupplementMatching constructor
        /// </summary>
        /// <param name="supplier">Supplier</param>
        /// <param name="code">External supplement code</param>
        /// <param name="description">External supplement description</param>
        /// <param name="inactive">actice =true and inactive =false</param>
        /// <param name="editionMode">is in edition = true and id is not = false</param>
        public SupplementMatching(Supplier supplier, string code, string description, bool inactive, bool editionMode)
            : base(code, inactive, editionMode)
        {

            this.supplier = supplier;
            this.description = description;

        }

        #endregion

        #region Properties

        /// <summary>
        /// Supplier
        /// </summary>
        public Supplier Supplier
        {
            get { return supplier; }
            set { supplier = value; }
        }


        /// <summary>
        /// External supplement description
        /// </summary>
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        /// <summary>
        /// Internal supplement for matching
        /// </summary>
        public Supplement MapTo
        {
            get { return mapTo; }
            set { mapTo = value; }
        }

        /// <summary>
        /// meta information collection
        /// </summary>
        public Dictionary<string, object> MetaInfo
        {
            get { return metaInfo; }
            set { metaInfo = value; }
        }


        #endregion


        #region OverrideMethods

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("([{0}] [{1}] - {2}", supplier.Code, base.Code, MapTo.Code);
        }

        /// <summary>
        /// Check if one matching is equal to another matching by its code
        /// </summary>
        /// <param name="obj">supplement to compare</param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj == null || obj == DBNull.Value)
            {
                return false;
            }

            return (base.Code == ((SupplementMatching)obj).Code && Supplier == ((SupplementMatching)obj).Supplier);
        }

        /// <summary>
        /// Check if two matchings are equal
        /// </summary>
        /// <param name="supplementMatching1">matching one</param>
        /// <param name="supplementMatching2">matching two</param>
        /// <returns></returns>
        public static bool operator ==(SupplementMatching supplementMatching1, SupplementMatching supplementMatching2)
        {
            if ((object)supplementMatching1 == null && (object)supplementMatching2 == null)
            {
                return true;
            }
            else if ((object)supplementMatching1 == null ^ (object)supplementMatching2 == null)
            {
                return false;
            }
            return supplementMatching1.Equals(supplementMatching2);
        }

        /// <summary>
        /// Check if two matchings are diferent
        /// </summary>
        /// <param name="brand1">matching one</param>
        /// <param name="brand2">matching two</param>
        /// <returns></returns>
        public static bool operator !=(SupplementMatching supplementMatching1, SupplementMatching supplementMatching2)
        {
            if ((object)supplementMatching1 == null && (object)supplementMatching2 == null)
            {
                return false;
            }
            else if ((object)supplementMatching1 == null ^ (object)supplementMatching2 == null)
            {
                return true;
            }
            return !supplementMatching1.Equals(supplementMatching2);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            int hashSignature = 13 * 43;

            if (base.Code == null && supplier == null)
            {
                return hashSignature;
            }

            if (base.Code != null && supplier != null)
            {
                hashSignature = hashSignature * (Math.Abs((base.Code.GetHashCode() - this.supplier.GetHashCode())) + 1);
            }
            else
            {
                if (supplier != null)
                {
                    hashSignature = hashSignature * this.supplier.GetHashCode();
                }
                else
                {
                    hashSignature = hashSignature * base.Code.GetHashCode();
                }
            }

            return hashSignature;
        }


        #endregion
    }
}
