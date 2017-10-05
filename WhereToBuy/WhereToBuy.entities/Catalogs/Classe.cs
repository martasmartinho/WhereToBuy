using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhereToBuy.entities
{

    /// <summary>
    /// This classe describes a product class
    /// </summary>
    [Serializable]
    public class Classe:BaseEntity
    {

        #region Atributs

        private string description;
        private Catalog catalog;
        private double range;
        private decimal rangeMinValue;
        private string notes;
        Dictionary<string, object> metaInfo;

        #endregion

        #region Constructors

        /// <summary>
        /// Empty contructor
        /// </summary>
        public Classe()
        { }

        /// <summary>
        /// Classe constructor
        /// </summary>
        /// <param name="code">Classe code</param>
        /// <param name="description">Classe description</param>
        /// /// <param name="catalog">Classe catalog</param>
        /// /// <param name="range">Classe profit range</param>
        /// /// <param name="rangeMinValue">Classe profit min range</param>
        /// /// <param name="notes">Classe notes</param>
        /// <param name="inactive">actice =true and inactive =false</param>
        /// <param name="editionMode">is in edition = true and id is not = false</param>
        public Classe(string code, string description, Catalog catalog, double range, decimal rangeMinValue, string notes, bool inactive, bool editionMode):base(code, inactive, editionMode)
        {
            
            this.description = description;
            this.catalog = catalog;
            this.range = range;
            this.rangeMinValue = rangeMinValue;
            this.notes = notes;
          
        }

        #endregion

        #region Properties

        /// <summary>
        /// Product class description
        /// </summary>
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        /// <summary>
        /// Classe catalog
        /// </summary>
        public Catalog Catalog
        {
            get { return catalog; }
            set { catalog = value; }
        }

        /// <summary>
        /// profit range
        /// </summary>
        public double Range
        {
            get { return range; }
            set { range = value; }
        }

        /// <summary>
        /// profit min value range
        /// </summary>
        public decimal RangeMinValue
        {
            get { return rangeMinValue; }
            set { rangeMinValue = value; }
        }

        /// <summary>
        /// Classe notes
        /// </summary>
        public string Notes
        {
            get { return notes; }
            set { notes = value; }
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

            return (base.Code == ((Classe)obj).Code);
        }

        /// <summary>
        /// Check if two classes are equal
        /// </summary>
        /// <param name="classe1">classe one</param>
        /// <param name="classe2">classe two</param>
        /// <returns></returns>
        public static bool operator ==(Classe classe1, Classe classe2)
        {
            if ((object)classe1 == null && (object)classe2 == null)
            {
                return true;
            }
            else if ((object)classe1 == null ^ (object)classe2 == null)
            {
                return false;
            }
            return classe1.Equals(classe2);
        }

        /// <summary>
        /// Check if two classes are diferent
        /// </summary>
        /// <param name="classe1">classe one</param>
        /// <param name="classe2">classe two</param>
        /// <returns></returns>
        public static bool operator !=(Classe classe1, Classe classe2)
        {
            if ((object)classe1 == null && (object)classe2 == null)
            {
                return false;
            }
            else if ((object)classe1 == null ^ (object)classe2 == null)
            {
                return true;
            }
            return !classe1.Equals(classe2);
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
