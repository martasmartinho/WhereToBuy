using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhereToBuy.entities
{ 
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class ProductDetail:BaseEntity1
    {
        #region Atributes
        private string productCode;
        private Supplier supplier;
        private string description;
        private short descriptionScore;
        private bool isDescriptionDisable;
        private string features;
        private short featuresScore;
        private bool isFeaturesDisable;
        private string link;
        private short linkScore;
        private bool isLinkDisable;
        private string image;
        private short imageScore;
        private bool isImageDisable;
        private bool automaticUpdate;
        private bool needManualUpdate;
        private int contentConcernIndex;
        private Dictionary<string, object> metaInfo;
        #endregion


        #region Constructors

        /// <summary>
        /// Empty contructor
        /// </summary>
        public ProductDetail()
        { }

        /// <summary>
        /// ProductDetail constructor
        /// </summary>
        /// <param name="productCode">Product code</param>
        /// <param name="supplierCode">Supplier code</param>
        /// <param name="inactive">actice =true and inactive =false</param>
        /// <param name="editionMode">is in edition = true and id is not = false</param>
        public ProductDetail(string productCode, Supplier supplier, bool inactive, bool editionMode)
            : base(inactive, editionMode)
        {
            
            this.productCode = productCode;
            this.supplier = supplier;
          
        }

        #endregion


        #region Properties

        /// <summary>
        /// meta information collection
        /// </summary>
        public Dictionary<string, object> MetaInfo
        {
            get { return metaInfo; }
            set { metaInfo = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int ContentConcernIndex
        {
            get { return contentConcernIndex; }
            set { contentConcernIndex = value; }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public bool NeddManualUpdate
        {
            get { return needManualUpdate; }
            set { needManualUpdate = value; }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public bool AutomaticUpdate
        {
            get { return automaticUpdate; }
            set { automaticUpdate = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsImageDisable
        {
            get { return isImageDisable; }
            set { isImageDisable = value; }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public short ImageScore
        {
            get { return imageScore; }
            set { imageScore = value; }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public string Image
        {
            get { return image; }
            set { image = value; }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public bool IsLinkDisable
        {
            get { return isLinkDisable; }
            set { isLinkDisable = value; }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public short LinkScore
        {
            get { return linkScore; }
            set { linkScore = value; }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public string Link
        {
            get { return link; }
            set { link = value; }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public bool IsFeaturesDisable
        {
            get { return isFeaturesDisable; }
            set { isFeaturesDisable = value; }
        }
        
        
        /// <summary>
        /// 
        /// </summary>
        public short FeaturesScore
        {
            get { return featuresScore; }
            set { featuresScore = value; }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public string Features
        {
            get { return features; }
            set { features = value; }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public bool IsDescriptionDisable
        {
            get { return isDescriptionDisable; }
            set { isDescriptionDisable = value; }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public short DescriptionScore
        {
            get { return descriptionScore; }
            set { descriptionScore = value; }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        
        
        /// <summary>
        /// 
        /// </summary>
        public Supplier Supplier
        {
            get { return supplier; }
            set { supplier = value; }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public string ProductCode
        {
            get { return productCode; }
            set { productCode = value; }
        }

        #endregion

        #region OverrideMethods

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("[{0}] [{1}] - {2}", productCode, supplier, description);
        }

        /// <summary>
        /// Check if one productDetail is equal to another productDetail by its code
        /// </summary>
        /// <param name="obj">productDetail to compare</param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj == null || obj == DBNull.Value)
            {
                return false;
            }

            return (ProductCode == ((ProductDetail)obj).ProductCode && Supplier == ((ProductDetail)obj).Supplier);
        }

        /// <summary>
        /// Check if two productDetails are equal
        /// </summary>
        /// <param name="productDetail1">productDetail one</param>
        /// <param name="productDetail2">productDetail two</param>
        /// <returns></returns>
        public static bool operator ==(ProductDetail productDetail1, ProductDetail productDetail2)
        {
            if ((object)productDetail1 == null && (object)productDetail2 == null)
            {
                return true;
            }
            else if ((object)productDetail1 == null ^ (object)productDetail2 == null)
            {
                return false;
            }
            return productDetail1.Equals(productDetail2);
        }

        /// <summary>
        /// Check if two productDetails are diferent
        /// </summary>
        /// <param name="productDetail1">productDetail one</param>
        /// <param name="productDetail2">productDetail two</param>
        /// <returns></returns>
        public static bool operator !=(ProductDetail productDetail1, ProductDetail productDetail2)
        {
            if ((object)productDetail1 == null && (object)productDetail2 == null)
            {
                return false;
            }
            else if ((object)productDetail1 == null ^ (object)productDetail2 == null)
            {
                return true;
            }
            return !productDetail1.Equals(productDetail2);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            int hashSignature = 13 * 43;

            if (productCode == null && supplier == null)
            {
                return hashSignature;
            }

            if (productCode != null && supplier != null)
            {
                hashSignature = hashSignature * (Math.Abs((productCode.GetHashCode() - supplier.GetHashCode())) + 1);
            }
            else
            {
                if (supplier != null)
                {
                    hashSignature = hashSignature * supplier.GetHashCode();
                }
                else
                {
                    hashSignature = hashSignature * productCode.GetHashCode();
                }
            }

            return hashSignature;
        }


        #endregion
    }
}
