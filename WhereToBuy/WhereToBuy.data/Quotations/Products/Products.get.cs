using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhereToBuy.entities;
using WhereToBuy.utils;
using WhereToBuy.utils.GlobalVariables;

namespace WhereToBuy.data
{
    public partial class Products
    {

        string _namespace = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
        string _className = "Products";
        string _procedureGetName = "ProductSelect";
        string _procedureGetViewName = "CurrentProductSelect";

        DataEngine engine;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="engine"></param>
        public Products(DataEngine engine)
        {
            this.engine = engine;
        }


        public Product Get(string code, DataState dataState, int setDetails)
        {

            Product product;
            SqlDataReader sqlDataReader;
            List<SqlParameter> sqlParameters;
            bool connectionOn = !engine.SqlServer.IsConnectionOpen();

            sqlParameters = new List<SqlParameter>();

            sqlParameters.Add(new SqlParameter("@WhereClause", ""));
            switch (dataState)
            {
                case DataState.Active:
                    sqlParameters[sqlParameters.Count - 1].Value = string.Format("p.[Codigo]='{0}' AND p.[Inativo]='false'", SQLStrings.CleanDangerousText(code));
                    break;
                case DataState.Inactive:
                    sqlParameters[sqlParameters.Count - 1].Value = string.Format("p.[Codigo]='{0}' AND p.[Inativo]='true'", SQLStrings.CleanDangerousText(code));
                    break;
                case DataState.All:
                    sqlParameters[sqlParameters.Count - 1].Value = string.Format("p.[Codigo]='{0}'", SQLStrings.CleanDangerousText(code));
                    break;
                default:
                    throw new MyException(_namespace, _className, "Get()", string.Format("{0}!", GlobalVariables.Resource.GetString("ForeseenEnumeratorString", GlobalVariables.Culture).ToLower()));
            }

            try
            {
                if (connectionOn)
                {
                    engine.SqlServer.OpenConnection();
                }

                sqlDataReader = engine.SqlServer.ExecuteReader(System.Data.CommandType.StoredProcedure, _procedureGetName, sqlParameters);

                if (!sqlDataReader.HasRows)
                {
                    sqlDataReader.Dispose();
                    engine.SqlServer.CloseConnection();
                    throw new MyException(_namespace, _className, "Get()",
                        string.Format("{0} {1}", GlobalVariables.Resource.GetString("RecordNotFoundString", GlobalVariables.Culture), SQLStrings.CleanDangerousText(code)));
                }

                sqlDataReader.Read();
                product = this.Deserialize(ref sqlDataReader);
                sqlDataReader.Dispose();

                if (connectionOn)
                {
                    engine.SqlServer.CloseConnection();
                }

            }
            catch (SqlException ex)
            {
                if (engine.SqlServer.IsConnectionOpen())
                {
                    engine.SqlServer.CloseConnection();
                }
                throw new MyException(GlobalVariables.ProjectName + "sqldatabase", MyException.OriginClassSqlError.SQLStoredProcedure, ex.Procedure, ex.Errors);
            }
            catch (MyException)
            {
                if (engine.SqlServer.IsConnectionOpen())
                {
                    engine.SqlServer.CloseConnection();
                }
                throw;
            }
            catch (Exception ex)
            {
                if (engine.SqlServer.IsConnectionOpen())
                {
                    engine.SqlServer.CloseConnection();
                }
                throw new MyException(_namespace, _className, "Get()", ex.Message);
            }


            if (product.MetaInfo["Brand.Code"] != DBNull.Value)
            {
                product.Brand = new Brand();
                product.Brand.Code = (product.MetaInfo["Brand.Code"]).ToString();
            }


            if (product.MetaInfo["Supplier.Code"] != DBNull.Value)
            {
                product.Supplier = new Supplier();
                product.Supplier.Code = (product.MetaInfo["Supplier.Code"]).ToString();
                //product.Supplier.InactiveAutomaticUpdateSuggestion = Convert.ToBoolean(product.MetaInfo["Supplier.InactiveAutomaticUpdateSuggestion"]);
                product.Supplier.ProductPriceTrust = Convert.ToDouble(product.MetaInfo["Supplier.ProductPriceTrust"]);
                product.Supplier.ProductAvailableTrust = Convert.ToDouble(product.MetaInfo["Supplier.ProductAvailableTrust"]);
            }

            if (product.MetaInfo["Category.Code"] != DBNull.Value)
            {
                product.Category = new Category();
                product.Category.Code = (product.MetaInfo["Category.Code"]).ToString();

            }

            if (product.MetaInfo["Tax.Code"] != DBNull.Value)
            {
                product.Tax = new Tax();
                product.Tax.Code = (product.MetaInfo["Tax.Code"]).ToString();

            }

            if (product.MetaInfo["Stock.Code"] != DBNull.Value && product.MetaInfo["Stock.Code"].ToString() != "")
            {
                product.Stock = new Stock();
                product.Stock.Code = (product.MetaInfo["Stock.Code"]).ToString();
                product.Stock.Description = (product.MetaInfo["Stock.Description"]).ToString();
            }

            if (product.MetaInfo["Stock_U1.Code"] != DBNull.Value && product.MetaInfo["Stock_U1.Code"].ToString() != "")
            {
                product.Stock_U1 = new Stock();
                product.Stock_U1.Code = (product.MetaInfo["Stock_U1.Code"]).ToString();
                product.Stock_U1.Description = (product.MetaInfo["Stock_U1.Description"]).ToString();
            }

            if (product.MetaInfo["Stock_U2.Code"] != DBNull.Value && product.MetaInfo["Stock_U2.Code"].ToString() != "")
            {
                product.Stock_U2 = new Stock();
                product.Stock_U2.Code = (product.MetaInfo["Stock_U2.Code"]).ToString();
                product.Stock_U2.Description = (product.MetaInfo["Stock_U2.Description"]).ToString();
            }

            if (product.MetaInfo["Stock_U3.Code"] != DBNull.Value & product.MetaInfo["Stock_U3.Code"].ToString() != "")
            {
                product.Stock_U3 = new Stock();
                product.Stock_U3.Code = (product.MetaInfo["Stock_U3.Code"]).ToString();
                product.Stock_U3.Description = (product.MetaInfo["Stock_U3.Description"]).ToString();
            }

            if (setDetails > 0)
            {
                product.Details = engine.ProductDetails.Get(code, DataState.Active);
            }

            return product;
        }


        public Product Get(string code, DataState dataState, int setCategoryToLevel, 
                            int setBrandToLevel, int setSupplierToLevel, int setTaxToLevel, int setStockToLevel, int setDetails)
        {

            Product product;
            SqlDataReader sqlDataReader;
            List<SqlParameter> sqlParameters;
            bool connectionOn = !engine.SqlServer.IsConnectionOpen();

            sqlParameters = new List<SqlParameter>();

            sqlParameters.Add(new SqlParameter("@WhereClause", ""));
            switch (dataState)
            {
                case DataState.Active:
                    sqlParameters[sqlParameters.Count - 1].Value = string.Format("p.[Codigo]='{0}'  AND p.[Inativo]='false'", SQLStrings.CleanDangerousText(code));
                    break;
                case DataState.Inactive:
                    sqlParameters[sqlParameters.Count - 1].Value = string.Format("p.[Codigo]='{0}' AND p.[Inativo]='true'", SQLStrings.CleanDangerousText(code));
                    break;
                case DataState.All:
                    sqlParameters[sqlParameters.Count - 1].Value = string.Format("p.[Codigo]='{0}'", SQLStrings.CleanDangerousText(code));
                    break;
                default:
                    throw new MyException(_namespace, _className, "Get()", string.Format("{0}!", GlobalVariables.Resource.GetString("ForeseenEnumeratorString", GlobalVariables.Culture).ToLower()));
            }

            try
            {
                if (connectionOn)
                {
                    engine.SqlServer.OpenConnection();
                }

                sqlDataReader = engine.SqlServer.ExecuteReader(System.Data.CommandType.StoredProcedure, _procedureGetName, sqlParameters);

                if (!sqlDataReader.HasRows)
                {
                    sqlDataReader.Dispose();
                    engine.SqlServer.CloseConnection();
                    throw new MyException(_namespace, _className, "Get()",
                        string.Format("{0} {1}", GlobalVariables.Resource.GetString("RecordNotFoundString", GlobalVariables.Culture), SQLStrings.CleanDangerousText(code)));
                }

                sqlDataReader.Read();
                product = this.Deserialize(ref sqlDataReader);
                sqlDataReader.Dispose();

                if (connectionOn)
                {
                    engine.SqlServer.CloseConnection();
                }

            }
            catch (SqlException ex)
            {
                if (engine.SqlServer.IsConnectionOpen())
                {
                    engine.SqlServer.CloseConnection();
                }
                throw new MyException(GlobalVariables.ProjectName + "sqldatabase", MyException.OriginClassSqlError.SQLStoredProcedure, ex.Procedure, ex.Errors);
            }
            catch (MyException)
            {
                if (engine.SqlServer.IsConnectionOpen())
                {
                    engine.SqlServer.CloseConnection();
                }
                throw;
            }
            catch (Exception ex)
            {
                if (engine.SqlServer.IsConnectionOpen())
                {
                    engine.SqlServer.CloseConnection();
                }
                throw new MyException(_namespace, _className, "Get()", ex.Message);
            }



            //PREENCHIMENTO DE CAMADAS DOS OBJETOS FILHO

            if (setBrandToLevel > 0 && product.MetaInfo["Brand.Code"] != DBNull.Value)
            {
                product.Brand = engine.Brands.Get((string)(product.MetaInfo["Brand.Code"]), dataState);

            }

            if (setSupplierToLevel > 0 && product.MetaInfo["Supplier.Code"] != DBNull.Value)
            {
                product.Supplier = engine.Suppliers.Get((string)(product.MetaInfo["Supplier.Code"]), dataState);

            }

            if (setTaxToLevel > 0 && product.MetaInfo["Tax.Code"] != DBNull.Value)
            {
                product.Tax = engine.Taxes.Get((string)(product.MetaInfo["Tax.Code"]), dataState);

            }

            if (setCategoryToLevel > 0 && product.MetaInfo["Category.Code"] != DBNull.Value)
            {
                product.Category = engine.Categories.Get((string)(product.MetaInfo["Category.Code"]), dataState);

            }

            if (setStockToLevel > 0)
            {
                if (product.MetaInfo["Stock.Code"] != DBNull.Value && product.MetaInfo["Stock.Code"].ToString() != "")
                {
                    product.Stock = engine.Stocks.Get((string)(product.MetaInfo["Stock.Code"]), dataState);
                }

                if (product.MetaInfo["Stock_U1.Code"] != DBNull.Value && product.MetaInfo["Stock_U1.Code"].ToString() != "")
                {
                    product.Stock_U1 = engine.Stocks.Get((string)(product.MetaInfo["Stock_U1.Code"]), dataState);
                }

                if (product.MetaInfo["Stock_U2.Code"] != DBNull.Value && product.MetaInfo["Stock_U2.Code"].ToString() != "")
                {
                    product.Stock_U2 = engine.Stocks.Get((string)(product.MetaInfo["Stock_U2.Code"]), dataState);
                }

                if (product.MetaInfo["Stock_U3.Code"] != DBNull.Value && product.MetaInfo["Stock_U3.Code"].ToString() != "")
                {
                    product.Stock_U3 = engine.Stocks.Get((string)(product.MetaInfo["Stock_U3.Code"]), dataState);
                }
            }

           

            if (setDetails > 0)
            {
                product.Details = engine.ProductDetails.Get(code, DataState.Active);
            }

            return product;
        }


        public List<Product> Get(DataState dataState,  int setCategoryToLevel,
                                            int setBrandToLevel, int setSupplierToLevel, int setTaxToLevel, int setStockToLevel, bool? discontinued)
        {

            List<Product> products;
            SqlDataReader sqlDataReader;
            List<SqlParameter> sqlParameters;
            bool connectionOn = !engine.SqlServer.IsConnectionOpen();
            string whereClause = string.Empty;
            products = new List<Product>();
            sqlParameters = new List<SqlParameter>();

            switch (dataState)
            {
                case DataState.Active:
                    whereClause = "[Inativo]='false'";
                    break;
                case DataState.Inactive:
                    whereClause = "[Inativo]='true'";
                    break;
                case DataState.All:
                    whereClause = string.Empty;
                    break;
                default:
                    throw new MyException(_namespace, _className, "Get()", string.Format("{0}!", GlobalVariables.Resource.GetString("ForeseenEnumeratorString", GlobalVariables.Culture).ToLower()));
            }

            if (discontinued != null) 
            {
                if (whereClause != string.Empty)
                {
                    whereClause += " AND ";
                }

                if ((bool)discontinued)
                {
                    whereClause += "[Descontinuado]='true'";
                }
                else
                {
                    whereClause += "[Descontinuado]='false'";
                }
            }

           


            sqlParameters.Add(new SqlParameter("@WhereClause", whereClause));
            sqlParameters.Add(new SqlParameter("@OrderByClause", "[Descricao] ASC"));

            try
            {
                if (connectionOn)
                {
                    engine.SqlServer.OpenConnection();
                }

                sqlDataReader = engine.SqlServer.ExecuteReader(System.Data.CommandType.StoredProcedure, _procedureGetName, sqlParameters);

                if (!sqlDataReader.HasRows)
                {
                    sqlDataReader.Dispose();
                    engine.SqlServer.CloseConnection();
                    return products;  //lista vazia
                }

                while (sqlDataReader.Read())
                {
                    products.Add(Deserialize(ref sqlDataReader));
                }

                sqlDataReader.Dispose();

                if (connectionOn)
                {
                    engine.SqlServer.CloseConnection();
                }

            }
            catch (SqlException ex)
            {
                if (engine.SqlServer.IsConnectionOpen())
                {
                    engine.SqlServer.CloseConnection();
                }
                throw new MyException(GlobalVariables.ProjectName + "sqldatabase", MyException.OriginClassSqlError.SQLStoredProcedure, ex.Procedure, ex.Errors);
            }
            catch (MyException)
            {
                if (engine.SqlServer.IsConnectionOpen())
                {
                    engine.SqlServer.CloseConnection();
                }
                throw;
            }
            catch (Exception ex)
            {
                if (engine.SqlServer.IsConnectionOpen())
                {
                    engine.SqlServer.CloseConnection();
                }
                throw new MyException(_namespace, _className, "Get()", ex.Message);
            }


            //PREENCHIMENTO DE CAMADAS DOS OBJETOS FILHO
           foreach (Product product in products)
            {
                if (setBrandToLevel > 0 && product.MetaInfo["Brand.Code"] != DBNull.Value)
                {
                    product.Brand = engine.Brands.Get((string)(product.MetaInfo["Brand.Code"]), dataState);

                }
                else
                {
                    product.Brand = new Brand();
                    product.Brand.Code = (product.MetaInfo["Brand.Code"]).ToString();
                    product.Brand.Description = (product.MetaInfo["Brand.Description"]).ToString();
                }

                if (setSupplierToLevel > 0 && product.MetaInfo["Supplier.Code"] != DBNull.Value)
                {
                    product.Supplier = engine.Suppliers.Get((string)(product.MetaInfo["Supplier.Code"]), dataState);

                }
                else
                {
                    product.Supplier = new Supplier();
                    product.Supplier.Code = (product.MetaInfo["Supplier.Code"]).ToString();
                    product.Supplier.Name = (product.MetaInfo["Supplier.Name"]).ToString();
                }

                if (setTaxToLevel > 0 && product.MetaInfo["Tax.Code"] != DBNull.Value)
                {
                    product.Tax = engine.Taxes.Get((string)(product.MetaInfo["Tax.Code"]), dataState);

                }
                else
                {
                    product.Tax = new Tax();
                    product.Tax.Code = (product.MetaInfo["Tax.Code"]).ToString();
                    product.Tax.Description = (product.MetaInfo["Tax.Description"]).ToString();
                }

                if (setCategoryToLevel > 0 && product.MetaInfo["Category.Code"] != DBNull.Value)
                {
                    product.Category = engine.Categories.Get((string)(product.MetaInfo["Category.Code"]), dataState);

                }
                else
                {
                    product.Category = new Category();
                    product.Category.Code = (product.MetaInfo["Category.Code"]).ToString();
                    product.Category.Description = (product.MetaInfo["Category.Description"]).ToString();
                }

                if (setStockToLevel > 0)
                {
                    if (product.MetaInfo["Stock.Code"] != DBNull.Value)
                    {
                        product.Stock = engine.Stocks.Get((string)(product.MetaInfo["Stock.Code"]), dataState);
                    }

                    if (product.MetaInfo["Stock_U1.Code"] != DBNull.Value)
                    {
                        product.Stock_U1 = engine.Stocks.Get((string)(product.MetaInfo["Stock_U1.Code"]), dataState);
                    }

                    if (product.MetaInfo["Stock_U2.Code"] != DBNull.Value)
                    {
                        product.Stock_U2 = engine.Stocks.Get((string)(product.MetaInfo["Stock_U2.Code"]), dataState);
                    }

                    if (product.MetaInfo["Stock_U3.Code"] != DBNull.Value)
                    {
                        product.Stock_U3 = engine.Stocks.Get((string)(product.MetaInfo["Stock_U3.Code"]), dataState);
                    }
                }
                else
                {
                    if (product.MetaInfo["Stock.Code"] != DBNull.Value)
                    {
                        product.Stock = new Stock();
                        product.Stock.Code = (product.MetaInfo["Stock.Code"]).ToString();
                        product.Stock.Description = (product.MetaInfo["Stock.Description"]).ToString();
                    }

                    if (product.MetaInfo["Stock_U1.Code"] != DBNull.Value)
                    {
                        product.Stock_U1 = new Stock();
                        product.Stock_U1.Code = (product.MetaInfo["Stock_U1.Code"]).ToString();
                        product.Stock_U1.Description = (product.MetaInfo["Stock_U1.Description"]).ToString();
                    }

                    if (product.MetaInfo["Stock_U2.Code"] != DBNull.Value)
                    {
                        product.Stock_U2 = new Stock();
                        product.Stock_U2.Code = (product.MetaInfo["Stock_U2.Code"]).ToString();
                        product.Stock_U2.Description = (product.MetaInfo["Stock_U2.Description"]).ToString();
                    }

                    if (product.MetaInfo["Stock_U3.Code"] != DBNull.Value)
                    {
                        product.Stock_U3 = new Stock();
                        product.Stock_U3.Code = (product.MetaInfo["Stock_U3.Code"]).ToString();
                        product.Stock_U3.Description = (product.MetaInfo["Stock_U3.Description"]).ToString();
                    }
                }
            }
            return products;
        }

        public List<Product> Get(ProductFilter filter, Brand brand,
                                            Category category, Supplier supplier, string partnumber, string orderBy)
        {

            List<Product> products;
            SqlDataReader sqlDataReader;
            List<SqlParameter> sqlParameters;
            bool connectionOn = !engine.SqlServer.IsConnectionOpen();
            string whereClause = string.Empty;
            products = new List<Product>();
            sqlParameters = new List<SqlParameter>();


            switch (filter)
            {
                case ProductFilter.Current:
                    whereClause = string.Empty;
                    break;
                case ProductFilter.Discontinued:
                    whereClause = "[Descontinuado]='true'";
                    break;
                case ProductFilter.IPCGreaterZero:
                    whereClause = "[IPC]>0";
                    break;
                case ProductFilter.ManualUpdate:
                    whereClause = "[AtualizacaoAutomatica] = 1";
                    break;
                case ProductFilter.Inactive:
                    whereClause = "p.[Inativo] = 'true'";
                    break;
                case ProductFilter.All:
                    whereClause = string.Empty;
                    break;
                default:
                    throw new MyException(_namespace, _className, "Get()", string.Format("{0}!", GlobalVariables.Resource.GetString("ForeseenEnumeratorString", GlobalVariables.Culture).ToLower()));
            }

            if (brand != null)
            {
                if (!string.IsNullOrEmpty(whereClause))
                {
                    whereClause += " AND";
                }
                whereClause += string.Format(" [MarcaCodigo] = '{0}'", brand.Code);
            }

            if (category != null)
            {
                if (!string.IsNullOrEmpty(whereClause))
                {
                    whereClause += " AND";
                }
                whereClause += string.Format(" [CategoriaCodigo] = '{0}'", category.Code);
            }

            if (supplier != null)
            {
                if (!string.IsNullOrEmpty(whereClause))
                {
                    whereClause += " AND";
                }
                whereClause += string.Format(" [FornecedorCodigo] = '{0}'", supplier.Code);
            }

            if (!string.IsNullOrEmpty(partnumber))
            {
                if (!string.IsNullOrEmpty(whereClause))
                {
                    whereClause += " AND";
                }
                whereClause += string.Format(" [Partnumber] = '{0}'", partnumber);
            }

        
            sqlParameters.Add(new SqlParameter("@WhereClause", whereClause));
            sqlParameters.Add(new SqlParameter("@OrderByClause", orderBy));

            try
            {
                if (connectionOn)
                {
                    engine.SqlServer.OpenConnection();
                }

                if (filter != ProductFilter.Inactive && filter != ProductFilter.All)
                {
                    sqlDataReader = engine.SqlServer.ExecuteReader(System.Data.CommandType.StoredProcedure, _procedureGetViewName, sqlParameters);
                }
                else
                {
                    sqlDataReader = engine.SqlServer.ExecuteReader(System.Data.CommandType.StoredProcedure, _procedureGetName, sqlParameters);
                }
                

                if (!sqlDataReader.HasRows)
                {
                    sqlDataReader.Dispose();
                    engine.SqlServer.CloseConnection();
                    return products;  //lista vazia
                }

                while (sqlDataReader.Read())
                {
                    products.Add(Deserialize(ref sqlDataReader));
                }

                sqlDataReader.Dispose();

                if (connectionOn)
                {
                    engine.SqlServer.CloseConnection();
                }

            }
            catch (SqlException ex)
            {
                if (engine.SqlServer.IsConnectionOpen())
                {
                    engine.SqlServer.CloseConnection();
                }
                throw new MyException(GlobalVariables.ProjectName + "sqldatabase", MyException.OriginClassSqlError.SQLStoredProcedure, ex.Procedure, ex.Errors);
            }
            catch (MyException)
            {
                if (engine.SqlServer.IsConnectionOpen())
                {
                    engine.SqlServer.CloseConnection();
                }
                throw;
            }
            catch (Exception ex)
            {
                if (engine.SqlServer.IsConnectionOpen())
                {
                    engine.SqlServer.CloseConnection();
                }
                throw new MyException(_namespace, _className, "Get()", ex.Message);
            }

            //PREENCHIMENTO DE CAMADAS DOS OBJETOS FILHO
            foreach (Product product in products)
            {
                if (product.MetaInfo["Brand.Code"] != DBNull.Value)
                {
                    product.Brand = new Brand();
                    product.Brand.Code = (product.MetaInfo["Brand.Code"]).ToString();
                }
             

                if (product.MetaInfo["Supplier.Code"] != DBNull.Value)
                {
                    product.Supplier = new Supplier();
                    product.Supplier.Code = (product.MetaInfo["Supplier.Code"]).ToString();
                    //product.Supplier.InactiveAutomaticUpdateSuggestion = Convert.ToBoolean(product.MetaInfo["Supplier.InactiveAutomaticUpdateSuggestion"]);
                    product.Supplier.ProductPriceTrust = Convert.ToDouble(product.MetaInfo["Supplier.ProductPriceTrust"]);
                    product.Supplier.ProductAvailableTrust = Convert.ToDouble(product.MetaInfo["Supplier.ProductAvailableTrust"]);
                }
                
                if (product.MetaInfo["Category.Code"] != DBNull.Value)
                {
                    product.Category = new Category();
                    product.Category.Code = (product.MetaInfo["Category.Code"]).ToString();

                }
           
                if (product.MetaInfo["Stock.Code"] != DBNull.Value && product.MetaInfo["Stock.Code"].ToString() != "")
                {
                    product.Stock = new Stock();
                    product.Stock.Code = (product.MetaInfo["Stock.Code"]).ToString();
                    product.Stock.Description = (product.MetaInfo["Stock.Description"]).ToString();
                }

                if (product.MetaInfo["Stock_U1.Code"] != DBNull.Value && product.MetaInfo["Stock_U1.Code"].ToString() != "")
                {
                    product.Stock_U1 = new Stock();
                    product.Stock_U1.Code = (product.MetaInfo["Stock_U1.Code"]).ToString();
                    product.Stock_U1.Description = (product.MetaInfo["Stock_U1.Description"]).ToString();
                }

                if (product.MetaInfo["Stock_U2.Code"] != DBNull.Value && product.MetaInfo["Stock_U2.Code"].ToString() != "")
                {
                    product.Stock_U2 = new Stock();
                    product.Stock_U2.Code = (product.MetaInfo["Stock_U2.Code"]).ToString();
                    product.Stock_U2.Description = (product.MetaInfo["Stock_U2.Description"]).ToString();
                }

                if (product.MetaInfo["Stock_U3.Code"] != DBNull.Value & product.MetaInfo["Stock_U3.Code"].ToString() != "")
                {
                    product.Stock_U3 = new Stock();
                    product.Stock_U3.Code = (product.MetaInfo["Stock_U3.Code"]).ToString();
                    product.Stock_U3.Description = (product.MetaInfo["Stock_U3.Description"]).ToString();
                }
             
            }

           
            return products;
        }

        public List<Product> Get(DataState dataState)
        {

            List<Product> products;
            SqlDataReader sqlDataReader;
            List<SqlParameter> sqlParameters;
            bool connectionOn = !engine.SqlServer.IsConnectionOpen();
            string whereClause = string.Empty;
            products = new List<Product>();
            sqlParameters = new List<SqlParameter>();

            switch (dataState)
            {
                case DataState.Active:
                    whereClause = "[Inativo]='false' AND [Descontinuado] = 'false'";
                    break;
                case DataState.Inactive:
                    whereClause = "[Inativo]='true' AND [Descontinuado] = 'false'";
                    break;
                case DataState.All:
                    whereClause = string.Empty;
                    break;
                default:
                    throw new MyException(_namespace, _className, "Get()", string.Format("{0}!", GlobalVariables.Resource.GetString("ForeseenEnumeratorString", GlobalVariables.Culture).ToLower()));
            }

           
            sqlParameters.Add(new SqlParameter("@WhereClause", whereClause));
            sqlParameters.Add(new SqlParameter("@OrderByClause", "[Descricao] ASC"));

            try
            {
                if (connectionOn)
                {
                    engine.SqlServer.OpenConnection();
                }

                sqlDataReader = engine.SqlServer.ExecuteReader(System.Data.CommandType.StoredProcedure, _procedureGetName, sqlParameters);

                if (!sqlDataReader.HasRows)
                {
                    sqlDataReader.Dispose();
                    engine.SqlServer.CloseConnection();
                    return products;  //lista vazia
                }

                while (sqlDataReader.Read())
                {
                    products.Add(Deserialize(ref sqlDataReader));
                }

                sqlDataReader.Dispose();

                if (connectionOn)
                {
                    engine.SqlServer.CloseConnection();
                }

            }
            catch (SqlException ex)
            {
                if (engine.SqlServer.IsConnectionOpen())
                {
                    engine.SqlServer.CloseConnection();
                }
                throw new MyException(GlobalVariables.ProjectName + "sqldatabase", MyException.OriginClassSqlError.SQLStoredProcedure, ex.Procedure, ex.Errors);
            }
            catch (MyException)
            {
                if (engine.SqlServer.IsConnectionOpen())
                {
                    engine.SqlServer.CloseConnection();
                }
                throw;
            }
            catch (Exception ex)
            {
                if (engine.SqlServer.IsConnectionOpen())
                {
                    engine.SqlServer.CloseConnection();
                }
                throw new MyException(_namespace, _className, "Get()", ex.Message);
            }

            return products;
        }

      
        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchString"></param>
        /// <returns></returns>
        public List<Product> Get(string searchString)
        {

            List<Product> products;
            SqlDataReader sqlDataReader;
            List<SqlParameter> sqlParameters;
            bool connectionOn = !engine.SqlServer.IsConnectionOpen();

            products = new List<Product>();
            sqlParameters = new List<SqlParameter>();

            sqlParameters.Add(new SqlParameter("@WhereClause", ""));
            sqlParameters[sqlParameters.Count - 1].Value = string.Format("p.[Codigo] LIKE '%{0}%' OR p.[Descricao] LIKE '%{0}%' AND p.[Inativo]='false'", SQLStrings.CleanDangerousText(searchString.TrimEnd().TrimStart()));


            try
            {
                if (connectionOn)
                {
                    engine.SqlServer.OpenConnection();
                }

                sqlDataReader = engine.SqlServer.ExecuteReader(System.Data.CommandType.StoredProcedure, _procedureGetName, sqlParameters);

                if (!sqlDataReader.HasRows)
                {
                    sqlDataReader.Dispose();
                    engine.SqlServer.CloseConnection();
                    return products;  //lista vazia
                }

                while (sqlDataReader.Read())
                {
                    products.Add(Deserialize(ref sqlDataReader));
                }
                sqlDataReader.Dispose();

                if (connectionOn)
                {
                    engine.SqlServer.CloseConnection();
                }

            }
            catch (SqlException ex)
            {
                if (engine.SqlServer.IsConnectionOpen())
                {
                    engine.SqlServer.CloseConnection();
                }
                throw new MyException(GlobalVariables.ProjectName + "sqldatabase", MyException.OriginClassSqlError.SQLStoredProcedure, ex.Procedure, ex.Errors);
            }
            catch (MyException)
            {
                if (engine.SqlServer.IsConnectionOpen())
                {
                    engine.SqlServer.CloseConnection();
                }
                throw;
            }
            catch (Exception ex)
            {
                if (engine.SqlServer.IsConnectionOpen())
                {
                    engine.SqlServer.CloseConnection();
                }
                throw new MyException(_namespace, _className, "Get()", ex.Message);
            }

            return products;

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchString"></param>
        /// <returns></returns>
        public List<Product> Get(ProductFilter filter)
        {

            List<Product> products;
            SqlDataReader sqlDataReader;
            List<SqlParameter> sqlParameters;
            string query = "SELECT * FROM ";
            bool connectionOn = !engine.SqlServer.IsConnectionOpen();
            bool completed = false;

            products = new List<Product>();
            sqlParameters = new List<SqlParameter>();

            switch (filter)
            {
                case ProductFilter.Current:
                    query += "ProdutosAtuaisView";
                    break;
                case ProductFilter.Discontinued:
                    products = this.Get(DataState.Active, 0, 0, 0, 0, 0, true);
                    completed = true;
                    break;
                case ProductFilter.IPCGreaterZero:
                    query = "ProdutosAtuaisView WHERE [ICP] > 0";
                    break;
                case ProductFilter.ManualUpdate:
                    query = "ProdutosAtuaisView WHERE [AtualizacaoAutomatica] = 1";
                    break;
                case ProductFilter.Inactive:
                    products = this.Get(DataState.Inactive);
                    completed = true;
                    break;
                case ProductFilter.All:
                    products = this.Get(DataState.All);
                    completed = true;
                    break;
                default:
                    throw new MyException(_namespace, _className, "Get()", string.Format("{0}!", GlobalVariables.Resource.GetString("ForeseenEnumeratorString", GlobalVariables.Culture).ToLower()));
            }

            if (completed)
            {
                try
                {
                    if (connectionOn)
                    {
                        engine.SqlServer.OpenConnection();
                    }

                    sqlDataReader = engine.SqlServer.ExecuteReader(System.Data.CommandType.Text, query, sqlParameters);

                    if (!sqlDataReader.HasRows)
                    {
                        sqlDataReader.Dispose();
                        engine.SqlServer.CloseConnection();
                        return products;  //lista vazia
                    }

                    if (!sqlDataReader.HasRows)
                    {
                        sqlDataReader.Dispose();
                        engine.SqlServer.CloseConnection();
                        return products;  //lista vazia
                    }

                    while (sqlDataReader.Read())
                    {
                        products.Add(Deserialize(ref sqlDataReader));
                    }
                    sqlDataReader.Dispose();

                    if (connectionOn)
                    {
                        engine.SqlServer.CloseConnection();
                    }

                }
                catch (SqlException ex)
                {
                    if (engine.SqlServer.IsConnectionOpen())
                    {
                        engine.SqlServer.CloseConnection();
                    }
                    throw new MyException(GlobalVariables.ProjectName + "sqldatabase", MyException.OriginClassSqlError.SQLStoredProcedure, ex.Procedure, ex.Errors);
                }
                catch (MyException)
                {
                    if (engine.SqlServer.IsConnectionOpen())
                    {
                        engine.SqlServer.CloseConnection();
                    }
                    throw;
                }
                catch (Exception ex)
                {
                    if (engine.SqlServer.IsConnectionOpen())
                    {
                        engine.SqlServer.CloseConnection();
                    }
                    throw new MyException(_namespace, _className, "Get()", ex.Message);
                }
            }
            return products;

        }

    }
}
