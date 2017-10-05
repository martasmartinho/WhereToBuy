using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhereToBuy.data;
using WhereToBuy.entities;

namespace WhereToBuy.core
{
    public class CoreEngine
    {

        #region LoacalVariables
        // variaveis locais
        string connectionString;
        User authenticatedUser;
        Language actualLanguage;
        
       
        DataEngine data;

        #region specs
        Brands brands;
        Users users;
        WarningTypes warningTypes;
        Languages languages;
        Suppliers suppliers;
        Categories categories;
        Supplements supplements;
        States states;
        Taxes taxes;
        WorryingTerms worryingTerms;
        BrandsMatching brandsMatching;
        CategoriesMatching categoriesMatching;
        StocksMatching stocksMatching;
        SupplementsMatching supplementsMatching;
        StatesMatching statesMatching;
        TaxesMatching taxesMatching;
        SuppliersBrands suppliersBrands;
        Stocks stocks;
        QuotationRules quotationRules;
        Products products;
        QuotationWarnings quotationWarnings;
        ProductsMatching productsMatching;
        ProductDetails productDetails;
        #endregion


        #region Catalogs

        Groups groups;
        Catalogs catalogs;
        Classes classes;

        #endregion

        #endregion


        #region Constructors

        public CoreEngine(string connectionString)
        {
            this.connectionString = connectionString;
           
        }


        public CoreEngine(string connectionString, User authenticatedUser)
        {
            this.connectionString = connectionString;
            this.authenticatedUser = authenticatedUser;
          
        }

        public CoreEngine(string connectionString, User authenticatedUser, Language actualLanguage)
        {
            this.connectionString = connectionString;
            this.authenticatedUser = authenticatedUser;
            this.actualLanguage=actualLanguage;
        }



        #endregion

        #region Properties

        public string ConnectionString
        {
            get { return connectionString; }
        }


        public User AuthenticatedUser
        {
            get { return authenticatedUser; }
        }


        public Language ActualLanguage
        {
            get { return actualLanguage; }
        }


        public DataEngine Data
        {
            get
            {
                data = new DataEngine(this.connectionString);
                return data;
            }
        }


        #region specs

        public Brands Brands
        {
            get
            {
                if (brands == null)
                {
                    brands = new Brands(this);
                }
                return brands;
            }
        }

        public Users Users
        {
            get
            {
                if (users == null)
                {
                    users = new Users(this);
                }
                return users;
            }
        }


        public WarningTypes WarningTypes
        {
            get
            {
                if (warningTypes == null)
                {
                    warningTypes = new WarningTypes(this);
                }
                return warningTypes;
            }
        }

        public Languages Languages
        {
            get
            {
                if (languages == null)
                {
                    languages = new Languages(this);
                }
                return languages;
            }
        }

        public Suppliers Suppliers
        {
            get
            {
                if (suppliers == null)
                {
                    suppliers = new Suppliers(this);
                }
                return suppliers;
            }
        }

        public Categories Categories
        {
            get
            {
                if (categories == null)
                {
                    categories = new Categories(this);
                }
                return categories;
            }
        }


        public Supplements Supplements
        {
            get
            {
                if (supplements == null)
                {
                    supplements = new Supplements(this);
                }
                return supplements;
            }
        }


        public States States
        {
            get
            {
                if (states == null)
                {
                    states = new States(this);
                }
                return states;
            }
        }

        public Taxes Taxes
        {
            get
            {
                if (taxes == null)
                {
                    taxes = new Taxes(this);
                }
                return taxes;
            }
        }

        public WorryingTerms WorryingTerms
        {
            get
            {
                if (worryingTerms == null)
                {
                    worryingTerms = new WorryingTerms(this);
                }
                return worryingTerms;
            }
        }

        public BrandsMatching BrandsMatching
        {
            get
            {
                if (brandsMatching == null)
                {
                    brandsMatching = new BrandsMatching(this);
                }
                return brandsMatching;
            }
        }

        public CategoriesMatching CategoriesMatching
        {
            get
            {
                if (categoriesMatching == null)
                {
                    categoriesMatching = new CategoriesMatching(this);
                }
                return categoriesMatching;
            }
        }

        public StocksMatching StocksMatching
        {
            get
            {
                if (stocksMatching == null)
                {
                    stocksMatching = new StocksMatching(this);
                }
                return stocksMatching;
            }
        }



        public StatesMatching StatesMatching
        {
            get
            {
                if (statesMatching == null)
                {
                    statesMatching = new StatesMatching(this);
                }
                return statesMatching;
            }
        }



        public SupplementsMatching SupplementsMatching
        {
            get
            {
                if (supplementsMatching == null)
                {
                    supplementsMatching = new SupplementsMatching(this);
                }
                return supplementsMatching;
            }
        }



        public TaxesMatching TaxesMatching
        {
            get
            {
                if (taxesMatching == null)
                {
                    taxesMatching = new TaxesMatching(this);
                }
                return taxesMatching;
            }
        }


        public SuppliersBrands SuppliersBrands
        {
            get
            {
                if (suppliersBrands == null)
                {
                    suppliersBrands = new SuppliersBrands(this);
                }
                return suppliersBrands;
            }
        }

        public Stocks Stocks
        {
            get
            {
                if (stocks == null)
                {
                    stocks = new Stocks(this);
                }
                return stocks;
            }
        }


        public QuotationRules QuotationRules
        {
            get
            {
                if (quotationRules == null)
                {
                    quotationRules = new QuotationRules(this);
                }
                return quotationRules;
            }
        }


        public Products Products
        {
            get
            {
                if (products == null)
                {
                    products = new Products(this);
                }
                return products;
            }
        }

        public QuotationWarnings QuotationWarnings
        {
            get
            {
                if (quotationWarnings == null)
                {
                    quotationWarnings = new QuotationWarnings(this);
                }
                return quotationWarnings;
            }
        }

        public ProductsMatching ProductsMatching
        {
            get
            {
                if (productsMatching == null)
                {
                    productsMatching = new ProductsMatching(this);
                }
                return productsMatching;
            }
        }

        #endregion


        #region Catalogs

        public Groups Groups
        {
            get
            {
                if (groups == null)
                {
                    groups = new Groups(this);
                }
                return groups;
            }
        }

        public Catalogs Catalogs
        {
            get
            {
                if (catalogs == null)
                {
                    catalogs = new Catalogs(this);
                }
                return catalogs;
            }
        }
        


        public Classes Classes
        {
            get
            {
                if (classes == null)
                {
                    classes = new Classes(this);
                }
                return classes;
            }
        }


       

        #endregion

        #endregion
    }
}
