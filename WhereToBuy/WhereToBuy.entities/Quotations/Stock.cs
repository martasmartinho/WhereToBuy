using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhereToBuy.entities
{
    /// <summary>
    /// this class describes a stock status
    /// </summary>
    [Serializable]
    public class Stock : BaseEntity
    {

        #region Atributs

        private string description;
        private short availabilityLevel;
        private Stock stockCodeExpirationP50;
        private Stock stockCodeExpirationP60;
        private Stock stockCodeExpirationP70;
        private Stock stockCodeExpirationP80;
        private Stock stockCodeExpirationP90;
        private string notes;
        private Dictionary<string, object> metaInfo;


        #endregion


        #region Constructors

        /// <summary>
        /// Empty contructor
        /// </summary>
        public Stock()
        { }



        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="code">stock code = "D0001"</param>
        /// <param name="description">stock description</param>
        /// <param name="availabilityLevel">sotock availability level = [-5, 5]</param>
        /// <param name="inactive">actice =true and inactive =false</param>
        /// <param name="editionMode">is in edition = true and id is not = false</param>
        /// <param name="stockCodeExpirationP50">stock code in percentile 50 of is expiration date = "D0005"</param>
        /// <param name="stockCodeExpirationP60">stock code in percentile 60 of is expiration date = "D0006"</param>
        /// <param name="stockCodeExpirationP70">stock code in percentile 70 of is expiration date = "D0007"</param>
        /// <param name="stockCodeExpirationP80">stock code in percentile 80 of is expiration date = "D0008"</param>
        /// <param name="stockCodeExpirationP90">stock code in percentile 90 of is expiration date = "D0009"</param>
        /// <param name="notes">Stock notes</param>
        public Stock(string code, string description, short availabilityLevel, bool inactive, bool editionMode,
                        Stock stockCodeExpirationP50, Stock stockCodeExpirationP60, Stock stockCodeExpirationP70,
                        Stock stockCodeExpirationP80, Stock stockCodeExpirationP90, string notes)
            : base(code, inactive, editionMode)
        {

            this.description = description;
            this.availabilityLevel = availabilityLevel;
            this.stockCodeExpirationP50 = stockCodeExpirationP50;
            this.stockCodeExpirationP60 = stockCodeExpirationP60;
            this.stockCodeExpirationP70 = stockCodeExpirationP70;
            this.stockCodeExpirationP80 = stockCodeExpirationP80;
            this.stockCodeExpirationP90 = stockCodeExpirationP90;
            this.notes = notes;


        }


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="code">stock code = "D0001"</param>
        /// <param name="description">stock description</param>
        /// <param name="availabilityLevel">sotock availability level = [-5, 5]</param>
        /// <param name="inactive">actice =true and inactive =false</param>
        /// <param name="editionMode">is in edition = true and id is not = false</param>
        public Stock(string code, string description, short availabilityLevel, bool inactive, bool editionMode)
            : base(code, inactive, editionMode)
        {

            this.description = description;
            this.availabilityLevel = availabilityLevel;
            
        }


        #endregion


        #region Properties

        /// <summary>
        /// Stock description
        /// </summary>
        public string Description
        {
            get { return description; }
            set { description = value; }
        }


        /// <summary>
        /// sotock availability level
        /// </summary>
        public short AvailabilityLevel 
        { 
            get 
            { 
                return availabilityLevel; 
            } 
            set 
            { 
                availabilityLevel = value; 
            } 
        }


        /// <summary>
        /// stock code in percentile 50 of is expiration date
        /// </summary>
        public Stock StockCodeExpirationP50 
        { 
            get 
            { 
                return stockCodeExpirationP50; 
            } 
            set 
            { 
                stockCodeExpirationP50 = value; 
            } 
        }


        /// <summary>
        /// stock code in percentile 60 of is expiration date
        /// </summary>
        public Stock StockCodeExpirationP60 
        { 
            get 
            { 
                return stockCodeExpirationP60; 
            } 
            set 
            { 
                stockCodeExpirationP60 = value; 
            } 
        }


        /// <summary>
        /// stock code in percentile 70 of is expiration date
        /// </summary>
        public Stock StockCodeExpirationP70 
        { 
            get 
            { 
                return stockCodeExpirationP70; 
            } 
            set 
            { 
                stockCodeExpirationP70 = value; 
            } 
        }


        /// <summary>
        /// stock code in percentile 80 of is expiration date
        /// </summary>
        public Stock StockCodeExpirationP80 
        { 
            get 
            { 
                return stockCodeExpirationP80; 
            } 
            set 
            { 
                stockCodeExpirationP80 = value; 
            } 
        }


        /// <summary>
        /// stock code in percentile 90 of is expiration date
        /// </summary>
        public Stock StockCodeExpirationP90 
        { 
            get 
            { 
                return stockCodeExpirationP90; 
            } 
            set 
            { 
                stockCodeExpirationP90 = value; 
            } 
        }


        /// <summary>
        /// Stock notes
        /// </summary>
        public string Notes 
        { 
            get 
            { 
                return notes; 
            } 
            set 
            { 
                notes = value; 
            } 
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
            return string.Format("[{0}]-{1} ({2})", base.Code, description, availabilityLevel.ToString());
        }

        /// <summary>
        /// Check if one stock is equal to another stock by its code
        /// </summary>
        /// <param name="obj">stock to compare</param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj == null || obj == DBNull.Value)
            {
                return false;
            }

            return (base.Code == ((Stock)obj).Code);
        }

        /// <summary>
        /// Check if two stocks are equal
        /// </summary>
        /// <param name="stock1">stock one</param>
        /// <param name="stock2">stock two</param>
        /// <returns></returns>
        public static bool operator ==(Stock stock1, Stock stock2)
        {
            if ((object)stock1 == null && (object)stock2 == null)
            {
                return true;
            }
            else if ((object)stock1 == null ^ (object)stock2 == null)
            {
                return false;
            }
            return stock1.Equals(stock2);
        }

        /// <summary>
        /// Check if two stocks are diferent
        /// </summary>
        /// <param name="stock1">stock one</param>
        /// <param name="stock2">stock two</param>
        /// <returns></returns>
        public static bool operator !=(Stock stock1, Stock stock2)
        {
            if ((object)stock1 == null && (object)stock2 == null)
            {
                return false;
            }
            else if ((object)stock1 == null ^ (object)stock2 == null)
            {
                return true;
            }
            return !stock1.Equals(stock2);
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
