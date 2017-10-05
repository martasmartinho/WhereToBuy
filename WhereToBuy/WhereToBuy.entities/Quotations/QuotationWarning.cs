using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhereToBuy.entities
{
    public class QuotationWarning
    {

        #region Atributs

        private Guid id;
        private string productCode;
        private string supplementCode;
        private Supplier supplier;
        private WarningType warningType;
        private DateTime date;
        private string description;
        private DateTime creation;
        private Dictionary<string, object> metaInfo;

        #endregion


        #region Constructors

        /// <summary>
        /// Empty contructor
        /// </summary>
        public QuotationWarning()
        { }


      /// <summary>
      /// 
      /// </summary>
      /// <param name="supplier"></param>
      /// <param name="productCode"></param>
      /// <param name="supplementCode"></param>
      /// <param name="warningType"></param>
      /// <param name="date"></param>
      /// <param name="description"></param>
      /// <param name="editionMode"></param>
        public QuotationWarning(Supplier supplier, string productCode, string supplementCode, WarningType warningType, DateTime date,
                        string description, bool editionMode)
        {

            this.supplier = supplier;
            this.productCode = productCode;
            this.supplementCode = supplementCode;
            this.date = date;
            this.description = description;
            this.warningType = warningType;

        }

        

        #endregion


        #region Properties

        /// <summary>
        /// QuotationWarning Id
        /// </summary>
        public Guid Id
        {
            get { return id; }
            set { id = value; }
        }

        /// <summary>
        /// Supplier
        /// </summary>
        public Supplier Supplier
        {
            get { return supplier; }
            set { supplier = value; }
        }

        /// <summary>
        /// Warnning type
        /// </summary>
        public WarningType WarningType
        {
            get { return warningType; }
            set { warningType = value; }
        }


        /// <summary>
        /// External product code
        /// </summary>
        public string ProductCode
        {
            get { return productCode; }
            set { productCode = value; }
        }


        /// <summary>
        /// External supplement code
        /// </summary>
        public string SupplementCode
        {
            get { return supplementCode; }
            set { supplementCode = value; }
        }



        /// <summary>
        /// Entity creation date
        /// </summary>
        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }

       
        /// <summary>
        /// External brand description
        /// </summary>
        public string Description
        {
            get { return description; }
            set { description = value; }
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
        /// Entity creation date
        /// </summary>
        public DateTime Creation
        {
            get { return creation; }
            set { creation = value; }
        }

        #endregion


        #region OverrideMethods

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("([{0}] [{1}] - {2}", supplier.Code, warningType.Code, description);
        }


        /// <summary>
        /// Check if a warning is equal to another by its code
        /// </summary>
        /// <param name="obj">warning to compare</param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj == null || obj == DBNull.Value)
            {
                return false;
            }

            return (Id == ((QuotationWarning)obj).Id);
        }


        /// <summary>
        /// Check if two warnings are equal
        /// </summary>
        /// <param name="warning1">warning one</param>
        /// <param name="warning2">warning two</param>
        /// <returns></returns>
        public static bool operator ==(QuotationWarning warning1, QuotationWarning warning2)
        {
            if ((object)warning1 == null && (object)warning2 == null)
            {
                return true;
            }
            else if ((object)warning1 == null ^ (object)warning2 == null)
            {
                return false;
            }
            return warning1.Equals(warning2);
        }


        /// <summary>
        /// Check if two warnings are diferent
        /// </summary>
        /// <param name="brand1">warning one</param>
        /// <param name="brand2">warning two</param>
        /// <returns></returns>
        public static bool operator !=(QuotationWarning warning1, QuotationWarning warning2)
        {
            if ((object)warning1 == null && (object)warning2 == null)
            {
                return false;
            }
            else if ((object)warning1 == null ^ (object)warning2 == null)
            {
                return true;
            }
            return !warning1.Equals(warning2);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            int hashSignature = 13 * 43;

            if ( Id != null)
            {
                hashSignature = hashSignature * Id.GetHashCode();
            }
            
            return hashSignature;
        }


        #endregion
    }
}
