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
    public class BaseEntity
    {
        #region Atributes

        private string code;
        private bool inactive;
        private DateTime creation;
        private DateTime version;
        private bool editionMode;

       

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
        public BaseEntity(string code, bool inactive, bool editionMode)
        {
            this.code = code;
            this.inactive = inactive;
            this.editionMode = editionMode;
           
        }

        #endregion

        #region Properties

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
        /// Entity creation date
        /// </summary>
        public DateTime Creation
        {
            get { return creation; }
            set { creation = value; }
        }
        
        
        /// <summary>
        /// If is inactive
        /// </summary>
        public bool Inactive
        {
            get { return inactive; }
            set { inactive = value; }
        }
        
        /// <summary>
        /// Entity unique code
        /// </summary>
        public string Code
        {
            get { return code; }
            set { code = value; }
        }
        #endregion

    }
}
