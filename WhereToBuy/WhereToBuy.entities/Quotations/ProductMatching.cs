using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhereToBuy.entities
{
    [Serializable]
    public class ProductMatching: BaseEntity
    {
        
        #region Atributs

        private Supplier supplier;
        private string supplement;
        private Product mapTo;
        private string description;
        private int quotationExpireHours;
        private Stock replacementStock;
        private bool needPreventionPricesOut;
        private bool needPreventionFakeStock;
        private DateTime? dataReset;
        private string notes;
        private Dictionary<string, object> metaInfo;
    
        #endregion

        #region Constructors

        /// <summary>
        /// Empty contructor
        /// </summary>
        public ProductMatching()
        { }

        /// <summary>
        /// ProductMatching constructor
        /// </summary>
        /// <param name="supplier">Supplier</param>
        /// <param name="code">External Product code</param>
        /// /// <param name="supplement">External Supplement code code</param>
        /// <param name="description">External Product description</param>
        /// <param name="mapTo">Internal Product</param>
        /// <param name="inactive">actice =true and inactive =false</param>
        /// <param name="editionMode">is in edition = true and id is not = false</param>
        public ProductMatching(Supplier supplier, string code, string supplement, string description, Product mapTo, bool inactive, bool editionMode)
            : base(code, inactive, editionMode)
        {

            this.supplier = supplier;
            this.supplement = supplement;
            this.description = description;
            this.mapTo = mapTo;

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
        /// External supplement code
        /// </summary>
        public string Supplement
        {
            get { return supplement; }
            set { supplement = value; }
        }
       
        /// <summary>
        /// External product description
        /// </summary>
        public string Description
        {
            get { return description; }
            set { description = value; }
        }


        /// <summary>
        /// Internal Product for matching
        /// </summary>
        public Product MapTo
        {
            get { return mapTo; }
            set { mapTo = value; }
        }

       
        /// <summary>
        /// Hours to quotation expire
        /// </summary>
        public int QuotationExpireHours
        {
            get { return quotationExpireHours; }
            set { quotationExpireHours = value; }
        }
       

        /// <summary>
        /// Replacement stock
        /// </summary>
        public Stock ReplacementStock
        {
            get { return replacementStock; }
            set { replacementStock = value; }
        }

       
        /// <summary>
        /// Checks if need prevention if price is out
        /// </summary>
        public bool NeedPreventionPricesOut
        {
            get { return needPreventionPricesOut; }
            set { needPreventionPricesOut = value; }
        }


        /// <summary>
        /// Checks if need prevention if fake stock
        /// </summary>
        public bool NeedPreventionFakeStock
        {
            get { return needPreventionFakeStock; }
            set { needPreventionFakeStock = value; }
        }

        
        /// <summary>
        /// Date o reset data
        /// </summary>
        public DateTime? DataReset
        {
            get { return dataReset; }
            set { dataReset = value; }
        }

        /// <summary>
        /// meta information collection
        /// </summary>
        public Dictionary<string, object> MetaInfo
        {
            get { return metaInfo; }
            set { metaInfo = value; }
        }

        /// <summary>
        /// Internal notes
        /// </summary>
        public string Notes
        {
            get { return notes; }
            set { notes = value; }
        }


        #endregion


        #region OverrideMethods

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("([{0}] [{1}] - {2}", supplier.Code, base.Code, supplement, MapTo.Code);
        }

        /// <summary>
        /// Check if one matching is equal to another matching by its code
        /// </summary>
        /// <param name="obj">product matching to compare</param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj == null || obj == DBNull.Value)
            {
                return false;
            }

            return (base.Code == ((ProductMatching)obj).Code && 
                    Supplier == ((ProductMatching)obj).Supplier && 
                    Supplement == ((ProductMatching)obj).Supplement);
        }

        /// <summary>
        /// Check if two matchings are equal
        /// </summary>
        /// <param name="productMatching1">matching one</param>
        /// <param name="productMatching2">matching two</param>
        /// <returns></returns>
        public static bool operator ==(ProductMatching productMatching1, ProductMatching productMatching2)
        {
            if ((object)productMatching1 == null && (object)productMatching2 == null)
            {
                return true;
            }
            else if ((object)productMatching1 == null ^ (object)productMatching2 == null)
            {
                return false;
            }
            return productMatching1.Equals(productMatching2);
        }

        /// <summary>
        /// Check if two matchings are diferent
        /// </summary>
        /// <param name="productMatching1">matching one</param>
        /// <param name="productMatching2">matching two</param>
        /// <returns></returns>
        public static bool operator !=(ProductMatching productMatching1, ProductMatching productMatching2)
        {
            if ((object)productMatching1 == null && (object)productMatching2 == null)
            {
                return false;
            }
            else if ((object)productMatching1 == null ^ (object)productMatching2 == null)
            {
                return true;
            }
            return !productMatching1.Equals(productMatching2);
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
