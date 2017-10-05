using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhereToBuy.entities
{
    /// <summary>
    /// This classe describe a state
    /// </summary>
    [Serializable]
    public class State: BaseEntity
    {
        #region Atributs

        private string description;

        #endregion

        #region Constructors

        /// <summary>
        /// Empty contructor
        /// </summary>
        public State()
        { }

        /// <summary>
        /// State constructor
        /// </summary>
        /// <param name="code">State code</param>
        /// <param name="description">State description</param>
        /// <param name="inactive">actice =true and inactive =false</param>
        /// <param name="editionMode">is in edition = true and id is not = false</param>
        public State(string code, string description, bool inactive, bool editionMode)
            : base(code, inactive, editionMode)
        {
            
            this.description = description;
          
        }

        #endregion

        #region Properties

        /// <summary>
        /// State description
        /// </summary>
        public string Description
        {
            get { return description; }
            set { description = value; }
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
        /// Check if one atate is equal to another state by its code
        /// </summary>
        /// <param name="obj">state to compare</param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj == null || obj == DBNull.Value)
            {
                return false;
            }

            return (base.Code == ((State)obj).Code);
        }

        /// <summary>
        /// Check if two states are equal
        /// </summary>
        /// <param name="state1">state one</param>
        /// <param name="state2">state two</param>
        /// <returns></returns>
        public static bool operator ==(State state1, State state2)
        {
            if ((object)state1 == null && (object)state2 == null)
            {
                return true;
            }
            else if ((object)state1 == null ^ (object)state2 == null)
            {
                return false;
            }
            return state1.Equals(state2);
        }

        /// <summary>
        /// Check if two states are diferent
        /// </summary>
        /// <param name="state1">state one</param>
        /// <param name="state2">state two</param>
        /// <returns></returns>
        public static bool operator !=(State state1, State state2)
        {
            if ((object)state1 == null && (object)state2 == null)
            {
                return false;
            }
            else if ((object)state1 == null ^ (object)state2 == null)
            {
                return true;
            }
            return !state1.Equals(state2);
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
