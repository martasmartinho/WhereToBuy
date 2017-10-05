using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhereToBuy.entities
{
    [Serializable]
    public class Supplement: BaseEntity
    {
        #region Atributs

        private string description;
        private string textToAdd;
        private string textToRemove;

        #endregion

        #region Constructors

        /// <summary>
        /// Empty contructor
        /// </summary>
        public Supplement()
        { }

        /// <summary>
        /// Brand constructor
        /// </summary>
        /// <param name="code">Brand code</param>
        /// <param name="description">Brand description</param>
        /// <param name="inactive">actice =true and inactive =false</param>
        /// <param name="editionMode">is in edition = true and id is not = false</param>
        public Supplement(string code, string textToAdd, string textToRemove, string description, bool inactive, bool editionMode)
            : base(code, inactive, editionMode)
        {
            
            this.description = description;
            this.textToAdd = textToAdd;
            this.textToRemove = textToRemove;
          
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
        /// Text to add
        /// </summary>
        public string TextToAdd
        {
            get { return textToAdd; }
            set { textToAdd = value; }
        }

        /// <summary>
        /// Text to remove
        /// </summary>
        public string TextToRemove
        {
            get { return textToRemove; }
            set { textToRemove = value; }
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
        /// Check if one sepplement is equal to another supplement by its code
        /// </summary>
        /// <param name="obj">brand to compare</param>
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
        /// Check if two supplements are equal
        /// </summary>
        /// <param name="supplement1">supplement one</param>
        /// <param name="supplement2">supplement two</param>
        /// <returns></returns>
        public static bool operator ==(Supplement supplement1, Supplement supplement2)
        {
            if ((object)supplement1 == null && (object)supplement2 == null)
            {
                return true;
            }
            else if ((object)supplement1 == null ^ (object)supplement2 == null)
            {
                return false;
            }
            return supplement1.Equals(supplement2);
        }

        /// <summary>
        /// Check if two supplements are diferent
        /// </summary>
        /// <param name="brand1">supplement one</param>
        /// <param name="supplement2">supplement two</param>
        /// <returns></returns>
        public static bool operator !=(Supplement supplement1, Supplement supplement2)
        {
            if ((object)supplement1 == null && (object)supplement2 == null)
            {
                return false;
            }
            else if ((object)supplement1 == null ^ (object)supplement2 == null)
            {
                return true;
            }
            return !supplement1.Equals(supplement2);
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
