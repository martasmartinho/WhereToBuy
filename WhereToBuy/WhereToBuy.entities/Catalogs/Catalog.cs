using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhereToBuy.entities
{
    public class Catalog: BaseEntity
    {
        #region Atributs

        private string description;
        private string notes;

        #endregion

        #region Constructors

        /// <summary>
        /// Empty contructor
        /// </summary>
        public Catalog()
        { }

        /// <summary>
        /// Catalog constructor
        /// </summary>
        /// <param name="code">Catalog code</param>
        /// <param name="description">Catalog description</param>
        /// <param name="inactive">actice =true and inactive =false</param>
        /// <param name="editionMode">is in edition = true and id is not = false</param>
        public Catalog(string code, string description, string notes, bool inactive, bool editionMode)
            : base(code, inactive, editionMode)
        {
            
            this.description = description;
            this.notes = notes;
          
        }

        #endregion

        #region Properties

        /// <summary>
        /// Catalog description
        /// </summary>
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        /// <summary>
        /// Catalog notes
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
            return string.Format("[{0}]-{1}", base.Code, description);
        }

        /// <summary>
        /// Check if one Catalog is equal to another Catalog by its code
        /// </summary>
        /// <param name="obj">Catalog to compare</param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj == null || obj == DBNull.Value)
            {
                return false;
            }

            return (base.Code == ((Catalog)obj).Code);
        }

        /// <summary>
        /// Check if two Catalogs are equal
        /// </summary>
        /// <param name="catalog1">Catalog one</param>
        /// <param name="catalog2">Catalog two</param>
        /// <returns></returns>
        public static bool operator ==(Catalog catalog1, Catalog catalog2)
        {
            if ((object)catalog1 == null && (object)catalog2 == null)
            {
                return true;
            }
            else if ((object)catalog1 == null ^ (object)catalog2 == null)
            {
                return false;
            }
            return catalog1.Equals(catalog2);
        }

        /// <summary>
        /// Check if two Catalogs are diferent
        /// </summary>
        /// <param name="catalog1">Catalog one</param>
        /// <param name="catalog2">Catalog two</param>
        /// <returns></returns>
        public static bool operator !=(Catalog catalog1, Catalog catalog2)
        {
            if ((object)catalog1 == null && (object)catalog2 == null)
            {
                return false;
            }
            else if ((object)catalog1 == null ^ (object)catalog2 == null)
            {
                return true;
            }
            return !catalog1.Equals(catalog2);
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
