using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhereToBuy.entities
{
    /// <summary>
    /// This classe describes a matching between external category from a supllier and system internal category
    /// </summary>
    [Serializable]
    public class CategoryMatching : BaseEntity
    {
        #region Atributs

        private Supplier supplier;
        private string description;
        private Category mapTo;
        private Dictionary<string, object> metaInfo;

        #endregion

        #region Constructors

        /// <summary>
        /// Empty contructor
        /// </summary>
        public CategoryMatching()
        { }

        /// <summary>
        /// CategoryMatching constructor
        /// </summary>
        /// <param name="supplier">Supplier</param>
        /// <param name="code">External category code</param>
        /// <param name="description">External category description</param>
        /// <param name="mapTo">Internal category</param>
        /// <param name="inactive">actice =true and inactive =false</param>
        /// <param name="editionMode">is in edition = true and id is not = false</param>
        public CategoryMatching(Supplier supplier, string code, string description, Category mapTo, bool inactive, bool editionMode)
            : base(code, inactive, editionMode)
        {

            this.supplier = supplier;
            this.description = description;
            this.mapTo = mapTo;

        }

        /// <summary>
        /// BrandMatching constructor
        /// </summary>
        /// <param name="supplier">Supplier</param>
        /// <param name="code">External category code</param>
        /// <param name="description">External category description</param>
        /// <param name="inactive">actice =true and inactive =false</param>
        /// <param name="editionMode">is in edition = true and id is not = false</param>
        public CategoryMatching(Supplier supplier, string code, string description, bool inactive, bool editionMode)
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
        /// External category description
        /// </summary>
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        /// <summary>
        /// Internal category for matching
        /// </summary>
        public Category MapTo
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
        /// <param name="obj">brand to compare</param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj == null || obj == DBNull.Value)
            {
                return false;
            }

            return (base.Code == ((CategoryMatching)obj).Code && Supplier == ((CategoryMatching)obj).Supplier);
        }

        /// <summary>
        /// Check if two matchings are equal
        /// </summary>
        /// <param name="categoryMatching1">matching one</param>
        /// <param name="categoryMatching2">matching two</param>
        /// <returns></returns>
        public static bool operator ==(CategoryMatching categoryMatching1, CategoryMatching categoryMatching2)
        {
            if ((object)categoryMatching1 == null && (object)categoryMatching2 == null)
            {
                return true;
            }
            else if ((object)categoryMatching1 == null ^ (object)categoryMatching2 == null)
            {
                return false;
            }
            return categoryMatching1.Equals(categoryMatching2);
        }

        /// <summary>
        /// Check if two matchings are diferent
        /// </summary>
        /// <param name="brand1">matching one</param>
        /// <param name="brand2">matching two</param>
        /// <returns></returns>
        public static bool operator !=(CategoryMatching categoryMatching1, CategoryMatching categoryMatching2)
        {
            if ((object)categoryMatching1 == null && (object)categoryMatching2 == null)
            {
                return false;
            }
            else if ((object)categoryMatching1 == null ^ (object)categoryMatching2 == null)
            {
                return true;
            }
            return !categoryMatching1.Equals(categoryMatching2);
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
