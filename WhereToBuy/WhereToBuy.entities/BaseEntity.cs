using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhereToBuy.entities
{
    /// <summary>
    /// This class describes a base entity to be inherited by other
    /// </summary>
    [Serializable]
    public class BaseEntity:BaseEntity1 
    {
        #region Atributes

        private string code;
      

       

        #endregion

        #region Constructors

        /// <summary>
        /// Empty constructor
        /// </summary>
        public BaseEntity() { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="code">Entity code</param>
        /// <param name="inactive">if entity is inactive</param>
        /// <param name="creation">Creation date</param>
        /// <param name="version">Entity version</param>
        public BaseEntity(string code, bool inactive, bool editionMode):base(inactive, editionMode)
        {
            this.code = code;
            
        }

        #endregion

        #region Properties

       
        /// <summary>
        /// Entity unique code
        /// </summary>
        public string Code
        {
            get { return code; }
            set { code = value.ToUpper(); }
        }
        #endregion

    }
}
