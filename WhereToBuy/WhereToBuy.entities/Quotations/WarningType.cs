using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhereToBuy.entities
{
    //this classe describes a warning type
    [Serializable]
    public class WarningType:BaseEntity 
    {
        #region Atributs

        private string description;
        private short severity;
        private string notes;
        private string icon;

        #endregion

        #region Constructors

        /// <summary>
        /// Empty contructor
        /// </summary>
        public WarningType()
        { }

        /// <summary>
        /// WarningType  constructor
        /// </summary>
        /// <param name="code">WarningType code</param>
        /// <param name="description">WarningType description</param>
        /// /// <param name="severity">WarningType severity, as bigger more severious</param>
        /// /// <param name="notes">WarningType notes</param>
        /// /// <param name="Icon">WarningType icon path</param>
        /// <param name="inactive">active =true and inactive =false</param>
        /// <param name="editionMode">is in edition = true and id is not = false</param>
        public WarningType(string code, string description, short severity, string notes, string icon, bool inactive, bool editionMode)
            : base(code, inactive, editionMode)
        {
            
            this.description = description;
            this.severity = severity;
            this.notes = notes;
            this.icon = icon;

          
        }

        #endregion

        #region Properties

        /// <summary>
        /// WarningType description
        /// </summary>
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        /// <summary>
        /// WarningType severity
        /// </summary>
        public short Severity
        {
            get { return severity; }
            set { severity = value; }
        }


        /// <summary>
        /// WarningType notes
        /// </summary>
        public string Notes
        {
            get { return notes; }
            set { notes = value; }
        }


        /// <summary>
        /// WarningType icon path
        public string Icon
        {
            get { return icon; }
            set { icon = value; }
        }

        #endregion


        #region OverrideMethods

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return description;
        }

        /// <summary>
        /// Check if one WarningType is equal to another WarningType by its code
        /// </summary>
        /// <param name="obj">WarningType to compare</param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj == null || obj == DBNull.Value)
            {
                return false;
            }

            return (base.Code == ((WarningType)obj).Code);
        }

        /// <summary>
        /// Check if two WarningTypes are equal
        /// </summary>
        /// <param name="warningType1">WarningType one</param>
        /// <param name="warningType2">WarningType two</param>
        /// <returns></returns>
        public static bool operator ==(WarningType warningType1, WarningType warningType2)
        {
            if ((object)warningType1 == null && (object)warningType2 == null)
            {
                return true;
            }
            else if ((object)warningType1 == null ^ (object)warningType2 == null)
            {
                return false;
            }
            return warningType1.Equals(warningType2);
        }

        /// <summary>
        /// Check if two WarningTypes are diferent
        /// </summary>
        /// <param name="warningType1">WarningType one</param>
        /// <param name="warningType2">WarningType two</param>
        /// <returns></returns>
        public static bool operator !=(WarningType warningType1, WarningType warningType2)
        {
            if ((object)warningType1 == null && (object)warningType2 == null)
            {
                return false;
            }
            else if ((object)warningType1 == null ^ (object)warningType2 == null)
            {
                return true;
            }
            return !warningType1.Equals(warningType2);
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
