using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhereToBuy.entities
{
    /// <summary>
    /// This classe describes a tax
    /// </summary>
    [Serializable]
    public class Tax: BaseEntity
    {
        #region Atributs

        private string description;
        private string taxDesignation;
        private double taxValue;

        #endregion

        #region Constructors

        /// <summary>
        /// Empty contructor
        /// </summary>
        public Tax()
        { }

        /// <summary>
        /// Tax constructor
        /// </summary>
        /// <param name="code">Tax code</param>
        /// <param name="description">Tax description</param>
        /// <param name="inactive">actice =true and inactive =false</param>
        /// <param name="editionMode">is in edition = true and id is not = false</param>
        public Tax(string code, string description, string taxDesignation, double taxValue, bool inactive, bool editionMode):base(code, inactive, editionMode)
        {
            
            this.description = description;
            this.taxDesignation = taxDesignation;
            this.taxValue = taxValue;
          
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


        /// <summary>
        /// Tax designation
        /// </summary>
        public string TaxDesignation
        {
            get { return taxDesignation; }
            set { taxDesignation = value; }
        }

        /// <summary>
        /// Tax value
        /// </summary>
        public double TaxValue
        {
            get { return taxValue; }
            set { taxValue = value; }
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
        /// Check if one tax is equal to another tax by its code
        /// </summary>
        /// <param name="obj">tax to compare</param>
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
        /// Check if two taxes are equal
        /// </summary>
        /// <param name="tax1">tax one</param>
        /// <param name="tax2">tax two</param>
        /// <returns></returns>
        public static bool operator ==(Tax tax1, Tax tax2)
        {
            if ((object)tax1 == null && (object)tax2 == null)
            {
                return true;
            }
            else if ((object)tax1 == null ^ (object)tax2 == null)
            {
                return false;
            }
            return tax1.Equals(tax2);
        }

        /// <summary>
        /// Check if two taxes are diferent
        /// </summary>
        /// <param name="tax1">tax one</param>
        /// <param name="tax2">tax two</param>
        /// <returns></returns>
        public static bool operator !=(Tax tax1, Tax tax2)
        {
            if ((object)tax1 == null && (object)tax2 == null)
            {
                return false;
            }
            else if ((object)tax1 == null ^ (object)tax2 == null)
            {
                return true;
            }
            return !tax1.Equals(tax2);
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
