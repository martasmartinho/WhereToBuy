using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhereToBuy.entities
{
    /// <summary>
    /// This classe describes a matching between external stock from a supllier and system internal stock
    /// </summary>
    [Serializable]
    public class StockMatching: BaseEntity
    {
        #region Atributs

        private Supplier supplier;
        private string description;
        private Stock mapTo;
        private Dictionary<string, object> metaInfo;

        #endregion

        #region Constructors

        /// <summary>
        /// Empty contructor
        /// </summary>
        public StockMatching()
        { }

        /// <summary>
        /// StockMatching constructor
        /// </summary>
        /// <param name="supplier">Supplier</param>
        /// <param name="code">External stock code</param>
        /// <param name="description">External stock description</param>
        /// <param name="mapTo">Internal stock</param>
        /// <param name="inactive">actice =true and inactive =false</param>
        /// <param name="editionMode">is in edition = true and id is not = false</param>
        public StockMatching(Supplier supplier, string code, string description, Stock mapTo, bool inactive, bool editionMode)
            : base(code, inactive, editionMode)
        {

            this.supplier = supplier;
            this.description = description;
            this.mapTo = mapTo;

        }

        /// <summary>
        /// StockMatching constructor
        /// </summary>
        /// <param name="supplier">Supplier</param>
        /// <param name="code">External stock code</param>
        /// <param name="description">External stock description</param>
        /// <param name="inactive">actice =true and inactive =false</param>
        /// <param name="editionMode">is in edition = true and id is not = false</param>
        public StockMatching(Supplier supplier, string code, string description, bool inactive, bool editionMode)
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
        /// External stock description
        /// </summary>
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        /// <summary>
        /// Internal stock for matching
        /// </summary>
        public Stock MapTo
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
        /// <param name="obj">stock to compare</param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj == null || obj == DBNull.Value)
            {
                return false;
            }

            return (base.Code == ((StockMatching)obj).Code && Supplier == ((StockMatching)obj).Supplier);
        }

        /// <summary>
        /// Check if two matchings are equal
        /// </summary>
        /// <param name="stockMatching1">matching one</param>
        /// <param name="stockMatching2">matching two</param>
        /// <returns></returns>
        public static bool operator ==(StockMatching stockMatching1, StockMatching stockMatching2)
        {
            if ((object)stockMatching1 == null && (object)stockMatching2 == null)
            {
                return true;
            }
            else if ((object)stockMatching1 == null ^ (object)stockMatching2 == null)
            {
                return false;
            }
            return stockMatching1.Equals(stockMatching2);
        }

        /// <summary>
        /// Check if two matchings are diferent
        /// </summary>
        /// <param name="brand1">matching one</param>
        /// <param name="brand2">matching two</param>
        /// <returns></returns>
        public static bool operator !=(StockMatching stockMatching1, StockMatching stockMatching2)
        {
            if ((object)stockMatching1 == null && (object)stockMatching2 == null)
            {
                return false;
            }
            else if ((object)stockMatching1 == null ^ (object)stockMatching2 == null)
            {
                return true;
            }
            return !stockMatching1.Equals(stockMatching2);
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
