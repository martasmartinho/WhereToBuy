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
    public class WorryingTerm:BaseEntity1
    {
        #region Atributs

        private string term;
        private short index;
        private string notes;
        
        #endregion

        #region Constructors

        /// <summary>
        /// Empty contructor
        /// </summary>
        public WorryingTerm()
        { }

        /// <summary>
        /// WorryingTerm constructor
        /// </summary>
        /// <param name="term">text to search</param>
        /// <param name="index">index</param>
        /// <param name="notes">notes</param>
        /// <param name="inactive"></param>
        /// <param name="editionMode"></param>
        public WorryingTerm(string term, short index, string notes, bool inactive, bool editionMode)
            : base(inactive, editionMode)
        {
            
            this.term = term;
            this.index = index;
            this.notes = notes;
          
        }

        #endregion

        #region Properties

        /// <summary>
        /// Term
        /// </summary>
        public string Term
        {
            get { return term; }
            set { term = value; }
        }

        /// <summary>
        /// WorryingTerm index
        /// </summary>
        public short Index
        {
            get { return index; }
            set { index = value; }
        }


        /// <summary>
        /// WorryingTerm notes
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
            return string.Format("[{0}]-{1}", term, index);
        }

        /// <summary>
        /// Check if one WorryingTerm is equal to another WorryingTerm by its code
        /// </summary>
        /// <param name="obj">WorryingTerm to compare</param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj == null || obj == DBNull.Value)
            {
                return false;
            }

            return (Term == ((WorryingTerm)obj).Term);
        }

        /// <summary>
        /// Check if two WorryingTerm are equal
        /// </summary>
        /// <param name="worryingTerm1">WorryingTerm one</param>
        /// <param name="worryingTerm2">WorryingTerm two</param>
        /// <returns></returns>
        public static bool operator ==(WorryingTerm worryingTerm1, WorryingTerm worryingTerm2)
        {
            if ((object)worryingTerm1 == null && (object)worryingTerm2 == null)
            {
                return true;
            }
            else if ((object)worryingTerm1 == null ^ (object)worryingTerm2 == null)
            {
                return false;
            }
            return worryingTerm1.Equals(worryingTerm2);
        }

        /// <summary>
        /// Check if two WorryingTerm are diferent
        /// </summary>
        /// <param name="worryingTerm1">WorryingTerm one</param>
        /// <param name="worryingTerm2">WorryingTerm two</param>
        /// <returns></returns>
        public static bool operator !=(WorryingTerm worryingTerm1, WorryingTerm worryingTerm2)
        {
            if ((object)worryingTerm1 == null && (object)worryingTerm2 == null)
            {
                return false;
            }
            else if ((object)worryingTerm1 == null ^ (object)worryingTerm2 == null)
            {
                return true;
            }
            return !worryingTerm1.Equals(worryingTerm2);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            int hashSignature = 13 * 36;

            if (term == null)
            {
                return hashSignature;
            }
            return hashSignature * term.GetHashCode();
        }


        #endregion
    }
}
