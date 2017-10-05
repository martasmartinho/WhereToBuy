using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhereToBuy.entities
{
    /// <summary>
    /// This classe a quotation rule
    /// </summary>
    [Serializable]
    public class QuotationRule
    {

        #region Atributs

        private Supplier supplier;
        private Brand brand;
        private Category category;
        private Stock stock;
        private short expirationHours;
        private Stock substituteStock;
        private DateTime? dataReset = null;
        private string notes;
        private DateTime version;
        private bool editionMode;
        private Dictionary<string, object> metaInfo;

        #endregion

        #region Constructors

        /// <summary>
        /// Empty contructor
        /// </summary>
        public QuotationRule()
        { }


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="supplier">Supplier</param>
        /// <param name="brand">Brand</param>
        /// <param name="category">Category</param>
        /// <param name="stock">Stock</param>
        /// <param name="expirationHours">Hours for rule expiration</param>
        /// <param name="substituteStock">Substitute stock</param>
        /// <param name="dataReset">Date when rule reset</param>
        /// <param name="notes">rule notes</param>
        /// <param name="editionMode">is in edition = true and id is not = false</param>
        public QuotationRule(Supplier supplier, Brand brand, Category category, Stock stock, short expirationHours,
                                Stock substituteStock, DateTime dataReset, string notes, bool editionMode)
        {

            this.supplier = supplier;
            this.brand = brand;
            this.category = category;
            this.stock = stock;
            this.expirationHours = expirationHours;
            this.substituteStock = substituteStock;
            this.dataReset = dataReset;
            this.notes = notes;
            this.editionMode = editionMode;

        }


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="supplier">Supplier</param>
        /// <param name="brand">Brand</param>
        /// <param name="category">Category</param>
        /// <param name="stock">Stock</param>
        /// <param name="expirationHours">Hours for rule expiration</param>
        /// <param name="editionMode">is in edition = true and id is not = false</param>
        public QuotationRule(Supplier supplier, Brand brand, Category category, Stock stock, short expirationHours, bool editionMode)
        {

            this.supplier = supplier;
            this.brand = brand;
            this.category = category;
            this.stock = stock;
            this.expirationHours = expirationHours;
            this.editionMode = editionMode;

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
        /// Brand
        /// </summary>
        public Brand Brand
        {
            get { return brand; }
            set { brand = value; }
        }


        /// <summary>
        /// Category
        /// </summary>
        public Category Category
        {
            get { return category; }
            set { category = value; }
        }


        /// <summary>
        /// Stock
        /// </summary>
        public Stock Stock
        {
            get { return stock; }
            set { stock = value; }
        }



        /// <summary>
        /// Expiration hours
        /// </summary>
        public short ExpitationHours
        {
            get { return expirationHours; }
            set { expirationHours = value; }
        }


        /// <summary>
        /// Substitute stock
        /// </summary>
        public Stock SubstituteStock
        {
            get { return substituteStock; }
            set { substituteStock = value; }
        }


        /// <summary>
        /// Rule data for reset
        /// </summary>
        public DateTime? DataReset
        {
            get { return dataReset; }
            set { dataReset = value; }
        }


        /// <summary>
        /// Rule notes
        /// </summary>
        public string Notes
        {
            get { return notes; }
            set { notes = value; }
        }



        /// <summary>
        /// Is in edition mode
        /// </summary>
        public bool EditionMode
        {
            get { return editionMode; }
            set { editionMode = value; }
        }


        /// <summary>
        /// QuotationRule version
        /// </summary>
        public DateTime Version
        {
            get { return version; }
            set { version = value; }
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
            return string.Format("([{0}] [{1}] [{2}] [{3}] - {4}", supplier.Code, brand.Code, category.Code, stock.Code, expirationHours.ToString());
        }

        /// <summary>
        /// Check if one Rule is equal to another rule by its unique code
        /// </summary>
        /// <param name="obj">rule to compare</param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj == null || obj == DBNull.Value)
            {
                return false;
            }

            return (supplier == ((QuotationRule)obj).Supplier && brand == ((QuotationRule)obj).Brand && category == ((QuotationRule)obj).Category && stock == ((QuotationRule)obj).Stock);
        }

        /// <summary>
        /// Check if two rules are equal
        /// </summary>
        /// <param name="quotationRule1">rules one</param>
        /// <param name="quotationRule2">rules two</param>
        /// <returns></returns>
        public static bool operator ==(QuotationRule quotationRule1, QuotationRule quotationRule2)
        {
            if ((object)quotationRule1 == null && (object)quotationRule2 == null)
            {
                return true;
            }
            else if ((object)quotationRule1 == null ^ (object)quotationRule2 == null)
            {
                return false;
            }
            return quotationRule1.Equals(quotationRule2);
        }

        /// <summary>
        /// Check if two rules are diferent
        /// </summary>
        /// <param name="brand1">rule one</param>
        /// <param name="brand2">rule two</param>
        /// <returns></returns>
        public static bool operator !=(QuotationRule quotationRule1, QuotationRule quotationRule2)
        {
            if ((object)quotationRule1 == null && (object)quotationRule2 == null)
            {
                return false;
            }
            else if ((object)quotationRule1 == null ^ (object)quotationRule2 == null)
            {
                return true;
            }
            return !quotationRule1.Equals(quotationRule2);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            int hashSignature = 13 * 36;


            if (supplier == null && brand == null && category == null && stock == null)
            {
                return hashSignature;
            }

            if (supplier != null && brand != null && category != null && stock != null)
            {
                hashSignature = hashSignature * (Math.Abs((this.supplier.GetHashCode() -
                                                    (Math.Abs((this.brand.GetHashCode() - (Math.Abs((this.category.GetHashCode() - this.stock.GetHashCode())))))) + 1) + 1) + 1);
            }
            else
            {
                if (supplier != null && brand != null && category != null)
                {
                    hashSignature = hashSignature * (Math.Abs((this.supplier.GetHashCode() -
                                                    (Math.Abs((this.brand.GetHashCode() - this.category.GetHashCode())))) + 1) + 1);
                }
                else
                {
                    if (supplier != null && brand != null)
                    {
                        hashSignature = hashSignature * (Math.Abs((this.supplier.GetHashCode() - this.brand.GetHashCode())) + 1);
                    }
                    else
                    {
                        if (supplier != null)
                        {
                            hashSignature = hashSignature * this.supplier.GetHashCode();
                        }
                        else
                        {
                            hashSignature = hashSignature * this.brand.GetHashCode();
                        }
                    }
                }
            }

            return hashSignature;
        }



        #endregion
    }
}
