using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhereToBuy.entities
{
    /// <summary>
    /// This classe describes the trust level between a supplier and a especific brand
    /// </summary>
    [Serializable]
    public class SupplierBrand
    {
        #region Atributs

        private Supplier supplier;
        private Brand brand;
        private double trust;
        private string notes;
        private bool editionMode;
        private DateTime version;
        private Dictionary<string, object> metaInfo;

        #endregion

        #region Constructors

        /// <summary>
        /// Empty contructor
        /// </summary>
        public SupplierBrand()
        { }

       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="supplier"></param>
        /// <param name="brand"></param>
        /// <param name="trust"></param>
        /// <param name="notes"></param>
        /// <param name="editionMode">is in edition = true and id is not = false</param>
        public SupplierBrand(Supplier supplier, Brand brand, double trust, string notes, bool editionMode)
        {

            this.supplier = supplier;
            this.brand = brand;
            this.trust = trust;
            this.notes = notes;
            this.EditionMode = editionMode;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="supplier"></param>
        /// <param name="brand"></param>
        /// <param name="trust"></param>
        /// <param name="notes"></param>
        /// <param name="editionMode">is in edition = true and id is not = false</param>
        public SupplierBrand(Supplier supplier, Brand brand, double trust, bool editionMode)
        {

            this.supplier = supplier;
            this.brand = brand;
            this.trust = trust;
            this.EditionMode = editionMode;

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
        /// Trust level
        /// </summary>
        public double Trust
        {
            get { return trust; }
            set { trust = value; }
        }


        /// <summary>
        /// Notes
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
        /// Entity version
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
            return string.Format("([{0}] [{1}] - {2}", supplier.Code, brand.Code, trust.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj == null || obj == DBNull.Value)
            {
                return false;
            }

            return (supplier == ((SupplierBrand)obj).Supplier && brand == ((SupplierBrand)obj).Brand);
        }

        /// <summary>
        /// Check if two supplierBrand are equal
        /// </summary>
        /// <param name="supplierBrand1">matching one</param>
        /// <param name="supplierBrand2">matching two</param>
        /// <returns></returns>
        public static bool operator ==(SupplierBrand supplierBrand1, SupplierBrand supplierBrand2)
        {
            if ((object)supplierBrand1 == null && (object)supplierBrand2 == null)
            {
                return true;
            }
            else if ((object)supplierBrand1 == null ^ (object)supplierBrand2 == null)
            {
                return false;
            }
            return supplierBrand1.Equals(supplierBrand2);
        }

        /// <summary>
        /// Check if two matchings are diferent
        /// </summary>
        /// <param name="brand1">matching one</param>
        /// <param name="brand2">matching two</param>
        /// <returns></returns>
        public static bool operator !=(SupplierBrand supplierBrand1, SupplierBrand supplierBrand2)
        {
            if ((object)supplierBrand1 == null && (object)supplierBrand2 == null)
            {
                return false;
            }
            else if ((object)supplierBrand1 == null ^ (object)supplierBrand2 == null)
            {
                return true;
            }
            return !supplierBrand1.Equals(supplierBrand2);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            int hashSignature = 13 * 43;

            if (brand == null && supplier == null)
            {
                return hashSignature;
            }

            if (brand != null && supplier != null)
            {
                hashSignature = hashSignature * (Math.Abs((this.brand.GetHashCode() - this.supplier.GetHashCode())) + 1);
            }
            else
            {
                if (supplier != null)
                {
                    hashSignature = hashSignature * this.supplier.GetHashCode();
                }
                else
                {
                    hashSignature = hashSignature * brand.GetHashCode();
                }
            }

            return hashSignature;
        }


        #endregion
    }
}
