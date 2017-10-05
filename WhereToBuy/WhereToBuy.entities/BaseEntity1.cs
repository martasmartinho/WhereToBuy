using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhereToBuy.entities
{
    [Serializable]
    public class BaseEntity1
    {

        #region Atributes

        private bool inactive;
        private DateTime creation;
        private DateTime version;
        private bool editionMode;



        #endregion

        #region Constructors

        /// <summary>
        /// Empty constructor
        /// </summary>
        public BaseEntity1() { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="code">Entity code</param>
        /// <param name="inactive">if entity is inactive</param>
        /// <param name="creation">Creation date</param>
        /// <param name="version">Entity version</param>
        public BaseEntity1(bool inactive, bool editionMode)
        {
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

        


        #endregion
    }
}
