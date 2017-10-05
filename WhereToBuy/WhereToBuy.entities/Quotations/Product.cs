using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhereToBuy.entities;

namespace WhereToBuy.entities
{
    /// <summary>
    /// this class describes a brand
    /// </summary>
    [Serializable]
    public class Product : BaseEntity
    {

        #region Atributs

        string description;
        string partnumber;
        Category category;
        Brand brand;
        Tax tax;
        Supplier supplier;
        decimal costPrice;
        DateTime costPrice_Date;
        decimal costPrice_U1;
        DateTime costPrice_U1Date;
        decimal costPrice_U2;
        DateTime costPrice_U2Date;
        decimal costPrice_U3;
        DateTime costPrice_U3Date;
        Stock stock;
        DateTime stock_Date;
        Stock stock_U1;
        DateTime stock_U1Date;
        Stock stock_U2;
        DateTime stock_U2Date;
        Stock stock_U3;
        DateTime stock_U3Date;
        bool discontinued;
        private Dictionary<string, object> metaInfo;
        private List<ProductDetail> details;
        int contentConcernIndex;
        private double icp;
        private string icpFormula;
        private double icpCE;
        private string icpCEFormula;
        private double icpCT;
        private string icpCTFormula;
        private double icpCF;
        private string icpCFFormula;
        private double eep;
        private string eepFormula;
        private double eed;
        private string eedFormula;
        private double icd;
        private string icdFormula;
        private double icdCE;
        private string icdCEFormula;
        private double icdCT;
        private string icdCTFormula;
        private double icdCF;
        private string icdCFFormula;


        #endregion

        #region Constructors

        /// <summary>
        /// Empty contructor
        /// </summary>
        public Product()
        { }

        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="code"></param>
        /// <param name="description"></param>
        /// <param name="partnumber"></param>
        /// <param name="category"></param>
        /// <param name="brand"></param>
        /// <param name="tax"></param>
        /// <param name="supplier"></param>
        /// <param name="discontinued"></param>
        /// <param name="inactive">actice =true and inactive =false</param>
        /// <param name="editionMode">is in edition = true and id is not = false</param>
        public Product(string code, string description, string partnumber, Category category, Brand brand,
                            Tax tax, Supplier supplier, bool discontinued, bool inactive, bool editionMode)
            : base(code, inactive, editionMode)
        {

            this.description = description;
            this.partnumber = partnumber;
            this.category = category;
            this.brand = brand;
            this.tax = tax;
            this.supplier = supplier;
            this.discontinued = discontinued;
        }

       
       

        #endregion


        #region Properties

        /// <summary>
        /// product description
        /// </summary>
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        /// <summary>
        /// product partnumber
        /// </summary>
        public string Partnumber
        {
            get { return partnumber; }
            set { partnumber = value; }
        }


        /// <summary>
        /// Category
        /// </summary>
        public Category Category
        {
            get { return category; }
            set { category = value; }
        }


        /// <summary>
        /// Brand
        /// </summary>
        public Brand Brand
        {
            get { return brand; }
            set { brand = value; }
        }


        /// <summary>
        /// Tax
        /// </summary>
        public Tax Tax
        {
            get { return tax; }
            set { tax = value; }
        }


        /// <summary>
        /// Supplier
        /// </summary>
        public Supplier Supplier
        {
            get { return supplier; }
            set { supplier = value; }
        }

       

        /// <summary>
        /// Product actual price
        /// </summary>
        public decimal CostPrice 
        { 
            get 
            { 
                return costPrice; 
            } 
            set 
            { 
                costPrice = value; 
            } 
        }



        /// <summary>
        /// Price changed date
        /// </summary>
        public DateTime CostPrice_Date 
        { 
            get 
            { 
                return costPrice_Date; 
            } 
            set 
            { 
                costPrice_Date = value; 
            } 
        }


        /// <summary>
        /// Product last price
        /// </summary>
        public decimal CostPrice_U1 
        { 
            get 
            { 
                return costPrice_U1; 
            } 
            set 
            { 
                costPrice_U1 = value; 
            } 
        }


        /// <summary>
        /// Last price changed data
        /// </summary>
        public DateTime CostPrice_U1Date 
        { 
            get 
            { 
                return costPrice_U1Date; 
            } 
            set 
            { 
                costPrice_U1Date = value; 
            } 
        }



        /// <summary>
        /// 
        /// </summary>
        public decimal CostPrice_U2 
        { 
            get 
            { 
                return costPrice_U2; 
            } 
            set 
            { 
                costPrice_U2 = value; 
            } 
        }


        /// <summary>
        /// 
        /// </summary>
        public DateTime CostPrice_U2Date 
        { 
            get 
            { 
                return costPrice_U2Date; 
            } 
            set 
            { 
                costPrice_U2Date = value; 
            } 
        }



        /// <summary>
        /// 
        /// </summary>
        public decimal CostPrice_U3 
        { 
            get 
            { 
                return costPrice_U3; 
            } 
            set 
            { 
                costPrice_U3 = value; 
            } 
        }


        /// <summary>
        /// 
        /// </summary>
        public DateTime CostPrice_U3Date 
        { 
            get 
            { 
                return costPrice_U3Date; 
            } 
            set 
            { 
                costPrice_U3Date = value; 
            } 
        }


        /// <summary>
        /// Product actual stock
        /// </summary>
        public Stock Stock 
        { 
            get 
            { 
                return stock; 
            } 
            set 
            { 
                stock = value; 
            } 
        }


        /// <summary>
        /// Stock changed date
        /// </summary>
        public DateTime Stock_Date 
        { 
            get 
            { 
                return stock_Date; 
            } 
            set 
            { 
                stock_Date = value; 
            } 
        }


        /// <summary>
        /// product last stock
        /// </summary>
        public Stock Stock_U1 
        { 
            get 
            { 
                return stock_U1; 
            } 
            set 
            { 
                stock_U1 = value; 
            } 
        }


        /// <summary>
        /// last Stock changed date
        /// </summary>
        public DateTime Stock_U1Date 
        { 
            get 
            { 
                return stock_U1Date; 
            } 
            set 
            { 
                stock_U1Date = value; 
            } 
        }


        /// <summary>
        /// product last2 stock
        /// </summary>
        public Stock Stock_U2 
        { 
            get 
            { 
                return stock_U2; 
            } 
            set 
            { 
                stock_U2 = value; 
            } 
        }


        /// <summary>
        /// last2 Stock changed date
        /// </summary>
        public DateTime Stock_U2Date 
        { 
            get 
            { 
                return stock_U2Date; 
            } 
            set 
            { 
                stock_U2Date = value; 
            } 
        }


        /// <summary>
        /// product last3 stock
        /// </summary>
        public Stock Stock_U3 
        { 
            get 
            { 
                return stock_U3; 
            } 
            set 
            { 
                stock_U3 = value; 
            } 
        }


        /// <summary>
        /// last3 Stock changed date
        /// </summary>
        public DateTime Stock_U3Date 
        { 
            get 
            { 
                return stock_U3Date; 
            } 
            set 
            { 
                stock_U3Date = value; 
            } 
        }




        /// <summary>
        /// product is descontinued true or false
        /// </summary>
        public bool Discontinued
        {
            get { return discontinued; }
            set { discontinued = value; }
        }


        /// <summary>
        /// product content Concern Index
        /// </summary>
        public int ContentConcernIndex
        {
            get { return contentConcernIndex; }
            set { contentConcernIndex = value; }
        }


        /// <summary>
        /// meta information collection
        /// </summary>
        public Dictionary<string, object> MetaInfo
        {
            get { return metaInfo; }
            set { metaInfo = value; }
        }


        /// <summary>
        /// Product details List
        /// </summary>
        public List<ProductDetail> Details
        {
            get 
            {
                if (details == null)
                {
                    details = new List<ProductDetail>();
                }
                return details; 
            }
            set { details = value; }
        }


        /// <summary>
        /// 
        /// </summary>
        public double ICP
        {
            get { return icp; }
            set { icp = value; }
        }


        /// <summary>
        /// 
        /// </summary>
        public string ICPFormula
        {
            get { return icpFormula; }
            set { icpFormula = value; }
        }


        /// <summary>
        /// 
        /// </summary>
        public double ICPCE
        {
            get { return icpCE; }
            set { icpCE = value; }
        }

        
        /// <summary>
        /// 
        /// </summary>
        public string ICPCEFormula
        {
            get { return icpCEFormula; }
            set { icpCEFormula = value; }
        }

       
        /// <summary>
        /// 
        /// </summary>
        public double ICPCT
        {
            get { return icpCT; }
            set { icpCT = value; }
        }


       /// <summary>
       /// 
       /// </summary>
        public string ICPCTFormula
        {
            get { return icpCTFormula; }
            set { icpCTFormula = value; }
        }


        /// <summary>
        /// 
        /// </summary>
        public double ICPCF
        {
            get { return icpCF; }
            set { icpCF = value; }
        }


        /// <summary>
        /// 
        /// </summary>
        public string ICPCFFormula
        {
            get { return icpCFFormula; }
            set { icpCFFormula = value; }
        }


        /// <summary>
        /// 
        /// </summary>
        public double EEP
        {
            get { return eep; }
            set { eep = value; }
        }


        /// <summary>
        /// 
        /// </summary>
        public string EEPFormula
        {
            get { return eepFormula; }
            set { eepFormula = value; }
        }

        
        /// <summary>
        /// 
        /// </summary>
        public double EED
        {
            get { return eed; }
            set { eed = value; }
        }


        /// <summary>
        /// 
        /// </summary>
        public string EEDFormula
        {
            get { return eedFormula; }
            set { eedFormula = value; }
        }


        /// <summary>
        /// 
        /// </summary>
        public double ICD
        {
            get { return icd; }
            set { icd = value; }
        }


        /// <summary>
        /// 
        /// </summary>
        public string ICDFormula
        {
            get { return icdFormula; }
            set { icdFormula = value; }
        }


        /// <summary>
        /// 
        /// </summary>
        public double ICDCE
        {
            get { return icdCE; }
            set { icdCE = value; }
        }

        
        /// <summary>
        /// 
        /// </summary>
        public string ICDCEFormula
        {
            get { return icdCEFormula; }
            set { icdCEFormula = value; }
        }


        /// <summary>
        /// 
        /// </summary>
        public double ICDCT
        {
            get { return icdCT; }
            set { icdCT = value; }
        }


        /// <summary>
        /// 
        /// </summary>
        public string ICDCTFormula
        {
            get { return icdCTFormula; }
            set { icdCTFormula = value; }
        }


        /// <summary>
        /// 
        /// </summary>
        public double ICDCF
        {
            get { return icdCF; }
            set { icdCF = value; }
        }


        /// <summary>
        /// 
        /// </summary>
        public string ICDCFFormula
        {
            get { return icdCFFormula; }
            set { icdCFFormula = value; }
        }


        #endregion


        #region OverrideMethods

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("([{0}] {1}", base.Code, description);
        }

        /// <summary>
        /// Verifies if one matching is equal to another matching by its code
        /// </summary>
        /// <param name="obj">brand to compare</param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj == null || obj == DBNull.Value)
            {
                return false;
            }

            return (base.Code == ((Product)obj).Code);
        }

        /// <summary>
        /// Verifies if two products are equal
        /// </summary>
        /// <param name="product1">product one</param>
        /// <param name="product2">product two</param>
        /// <returns></returns>
        public static bool operator ==(Product product1, Product product2)
        {
            if ((object)product1 == null && (object)product2 == null)
            {
                return true;
            }
            else if ((object)product1 == null ^ (object)product2 == null)
            {
                return false;
            }
            return product1.Equals(product2);
        }

        /// <summary>
        /// Verifies if two products are diferent
        /// </summary>
        /// <param name="brand1">product one</param>
        /// <param name="brand2">product two</param>
        /// <returns></returns>
        public static bool operator !=(Product product1, Product product2)
        {
            if ((object)product1 == null && (object)product2 == null)
            {
                return false;
            }
            else if ((object)product1 == null ^ (object)product2 == null)
            {
                return true;
            }
            return !product1.Equals(product2);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            int hashSignature = 13 * 43;

            if (base.Code == null)
            {
                return hashSignature;
            }

            return hashSignature * base.Code.GetHashCode();
        }


        #endregion

    }
}
