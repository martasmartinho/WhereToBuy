using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhereToBuy.entities
{
    [Serializable]
    public class Category:BaseEntity 
    {
        #region Atributs

        private string description;
        private double unityWeightAverage;
        private decimal minPriceAllowed;
        private decimal maxPriceAllowed;
        private double maxPriceAmplitude;
        private double trust;



        #endregion

        #region Constructors

        /// <summary>
        /// Empty contructor
        /// </summary>
        public Category()
        { }

        /// <summary>
        /// Category constructor
        /// </summary>
        /// <param name="code">Category code</param>
        /// <param name="description">Category description</param>
        /// <param name="inactive">actice =true and inactive =false</param>
        /// <param name="editionMode">is in edition = true and id is not = false</param>
        public Category(string code, string description, double unityWeightAverage, decimal minPriceAllowed, 
            decimal maxPriceAllowed, double maxPriceAmplitude, double trust, bool inactive, bool editionMode)
            : base(code, inactive, editionMode)
        {
            
            this.description = description;
            this.unityWeightAverage = unityWeightAverage;
            this.minPriceAllowed = minPriceAllowed;
            this.maxPriceAllowed = maxPriceAllowed;
            this.maxPriceAmplitude = maxPriceAmplitude;
            this.trust = trust;
          
        }

        #endregion

        #region Properties

        /// <summary>
        /// Category description
        /// </summary>
        public string Description
        {
            get { return description; }
            set { description = value; }
        }


        /// <summary>
        /// 
        /// </summary>
        public double UnityWeightAverage
        {
            get { return unityWeightAverage; }
            set { unityWeightAverage = value; }
        }


        /// <summary>
        /// 
        /// </summary>
        public decimal MinPriceAllowed
        {
            get { return minPriceAllowed; }
            set { minPriceAllowed = value; }
        }


        /// <summary>
        /// 
        /// </summary>
        public decimal MaxPriceAllowed
        {
            get { return maxPriceAllowed; }
            set { maxPriceAllowed = value; }
        }


        /// <summary>
        /// 
        /// </summary>
        public double MaxPriceAmplitude
        {
            get { return maxPriceAmplitude; }
            set { maxPriceAmplitude = value; }
        }


        /// <summary>
        /// 
        /// </summary>
        public double Trust
        {
            get { return trust; }
            set { trust = value; }
        }

        #endregion


        #region OverrideMethods

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("[{0}] {1}", base.Code, description);
        }

        /// <summary>
        /// Check if one language is equal to another language by its code
        /// </summary>
        /// <param name="obj">language to compare</param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj == null || obj == DBNull.Value)
            {
                return false;
            }

            return (base.Code == ((Category)obj).Code);
        }

        /// <summary>
        /// Check if two category are equal
        /// </summary>
        /// <param name="category1">category one</param>
        /// <param name="category2">category two</param>
        /// <returns></returns>
        public static bool operator ==(Category category1, Category category2)
        {
            if ((object)category1 == null && (object)category2 == null)
            {
                return true;
            }
            else if ((object)category1 == null ^ (object)category2 == null)
            {
                return false;
            }
            return category1.Equals(category2);
        }

        /// <summary>
        /// Check if two category are diferent
        /// </summary>
        /// <param name="category1">category one</param>
        /// <param name="category2">category two</param>
        /// <returns></returns>
        public static bool operator !=(Category category1, Category category2)
        {
            if ((object)category1 == null && (object)category2 == null)
            {
                return false;
            }
            else if ((object)category1 == null ^ (object)category2 == null)
            {
                return true;
            }
            return !category1.Equals(category2);
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
