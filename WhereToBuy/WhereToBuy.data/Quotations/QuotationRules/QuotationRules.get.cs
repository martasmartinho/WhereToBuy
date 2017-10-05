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
    public partial class QuotationRules
    {

        string _namespace = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
        string _className = "QuotationRules";
        string _procedureGetName = "QuotationRuleSelect";


        DataEngine engine;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="engine"></param>
        public QuotationRules(DataEngine engine)
        {
            this.engine = engine;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="supplier"></param>
        /// <param name="brand"></param>
        /// <param name="category"></param>
        /// <param name="stock"></param>
        /// <param name="withCustomization"></param>
        /// <param name="closeReset"></param>
        /// <param name="setSubstituteStockToLevel"></param>
        /// <returns></returns>
        public QuotationRule Get(Supplier supplier, Brand brand, Category category, Stock stock, bool withCustomization, bool closeReset, int setSubstituteStockToLevel)
        {

            QuotationRule quotationRule;
            SqlDataReader sqlDataReader;
            List<SqlParameter> sqlParameters;
            string whereClauseValue = string.Empty;
            bool connectionOn = !engine.SqlServer.IsConnectionOpen();

            sqlParameters = new List<SqlParameter>();

            sqlParameters.Add(new SqlParameter("@WhereClause", ""));
            whereClauseValue = string.Format("[FornecedorCodigo]='{0}' AND [MarcaCodigo]='{1}' AND " +
                                                                         "[CategoriaCodigo]='{2}' AND [StockCodigo]='{3}'",
                                                                          SQLStrings.CleanDangerousText(supplier.Code),
                                                                          SQLStrings.CleanDangerousText(brand.Code),
                                                                          category.Code,
                                                                          stock.Code);
            if (withCustomization)
            {
                whereClauseValue += "AND [DataReset] IS NOT NULL ";
            }
            if (closeReset)
            {
                whereClauseValue += string.Format("AND [DataReset] >= '{0}'", DateTime.Now.AddDays(-30));
            }

            sqlParameters[sqlParameters.Count - 1].Value = whereClauseValue;

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
                        string.Format("{0} ({1} - {2} - {3} - {4})", GlobalVariables.Resource.GetString("RecordNotFoundString", GlobalVariables.Culture),
                                                                    SQLStrings.CleanDangerousText(supplier.Code),
                                                                    SQLStrings.CleanDangerousText(brand.Code),
                                                                    SQLStrings.CleanDangerousText(category.Code),
                                                                    SQLStrings.CleanDangerousText(stock.Code)
                                                                    ));
                }

                sqlDataReader.Read();
                quotationRule = this.Deserialize(ref sqlDataReader);
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

            if (setSubstituteStockToLevel > 0 && quotationRule.MetaInfo["SubstituteStock.Code"] != DBNull.Value)
            {
                quotationRule.SubstituteStock = engine.Stocks.Get((string)(quotationRule.MetaInfo["SubstituteStock.Code"]), DataState.Active);

            }

            quotationRule.Supplier = supplier;
            quotationRule.Brand = brand;
            quotationRule.Category = category;
            quotationRule.Stock = stock;
            return quotationRule;

        }

        public QuotationRule Get(string supplier, string brand, string category, string stock, int setSubstituteStockToLevel)
        {

            QuotationRule quotationRule;
            SqlDataReader sqlDataReader;
            List<SqlParameter> sqlParameters;
            string whereClauseValue = string.Empty;
            bool connectionOn = !engine.SqlServer.IsConnectionOpen();

            sqlParameters = new List<SqlParameter>();

            sqlParameters.Add(new SqlParameter("@WhereClause", ""));
            whereClauseValue = string.Format("[FornecedorCodigo]='{0}' AND [MarcaCodigo]='{1}' AND " +
                                                                         "[CategoriaCodigo]='{2}' AND [StockCodigo]='{3}'",
                                                                          SQLStrings.CleanDangerousText(supplier),
                                                                          SQLStrings.CleanDangerousText(brand),
                                                                          SQLStrings.CleanDangerousText(category),
                                                                          stock);
           
            sqlParameters[sqlParameters.Count - 1].Value = whereClauseValue;

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
                        string.Format("{0} ({1} - {2} - {3} - {4})", GlobalVariables.Resource.GetString("RecordNotFoundString", GlobalVariables.Culture),
                                                                    SQLStrings.CleanDangerousText(supplier),
                                                                    SQLStrings.CleanDangerousText(brand),
                                                                    SQLStrings.CleanDangerousText(category),
                                                                    stock
                                                                    ));
                }

                sqlDataReader.Read();
                quotationRule = this.Deserialize(ref sqlDataReader);
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

            if (setSubstituteStockToLevel > 0 && quotationRule.MetaInfo["SubstituteStock.Code"] != DBNull.Value)
            {
                quotationRule.SubstituteStock = engine.Stocks.Get((string)(quotationRule.MetaInfo["SubstituteStock.Code"]), DataState.All);

            }

            quotationRule.Supplier = engine.Suppliers.Get((string)(quotationRule.MetaInfo["Supplier.Code"]), DataState.Active);
            quotationRule.Brand = engine.Brands.Get((string)(quotationRule.MetaInfo["Brand.Code"]), DataState.Active);
            quotationRule.Category = engine.Categories.Get((string)(quotationRule.MetaInfo["Category.Code"]), DataState.Active);
            quotationRule.Stock = engine.Stocks.Get((string)(quotationRule.MetaInfo["Stock.Code"]), DataState.Active);

            return quotationRule;

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="supplier"></param>
        /// <param name="brand"></param>
        /// <param name="category"></param>
        /// <param name="stock"></param>
        /// <param name="withCustomization"></param>
        /// <param name="closeReset"></param>
        /// <param name="setSubstituteStockToLevel"></param>
        /// <returns></returns>
        public  List<QuotationRule> Get(Supplier supplier, Brand brand, Category category, Stock stock, bool withCustomization, bool closeReset, int setSubstituteStockToLevel, string orderBy)
        {
            List<QuotationRule> quotationRules;
            SqlDataReader sqlDataReader;
            List<SqlParameter> sqlParameters;
            string whereClauseValue = string.Empty;
            bool connectionOn = !engine.SqlServer.IsConnectionOpen();

            quotationRules = new List<QuotationRule>();
            sqlParameters = new List<SqlParameter>();

            sqlParameters.Add(new SqlParameter("@WhereClause", ""));



            if (supplier != null)
            {
                whereClauseValue += string.Format("[FornecedorCodigo]='{0}'", SQLStrings.CleanDangerousText(supplier.Code));
            }

            if (category != null)
            {
                if (whereClauseValue != string.Empty)
                {
                    whereClauseValue += " AND";
                }

                whereClauseValue += string.Format(" [CategoriaCodigo]='{0}'", SQLStrings.CleanDangerousText(category.Code));
            }

            if (brand != null)
            {
                if (whereClauseValue != string.Empty)
                {
                    whereClauseValue += " AND";
                }

                whereClauseValue += string.Format(" [MarcaCodigo]='{0}'", SQLStrings.CleanDangerousText(brand.Code));
            }

            if (stock != null)
            {
                if (whereClauseValue != string.Empty)
                {
                    whereClauseValue += " AND";
                }

                whereClauseValue += string.Format(" [StockCodigo]='{0}'", stock.Code);
            }

            if (withCustomization)
            {
                if (whereClauseValue != string.Empty)
                {
                    whereClauseValue += " AND";
                }

                whereClauseValue += " [DataReset] IS NOT NULL";
            }
            if (closeReset)
            {
                if (whereClauseValue != string.Empty)
                {
                    whereClauseValue += " AND";
                }

                whereClauseValue += string.Format(" [DataReset] BETWEEN '{0}' AND '{1}'", DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.AddDays(30).ToString("yyyy-MM-dd"));
            }

            sqlParameters[sqlParameters.Count - 1].Value = whereClauseValue;
            sqlParameters.Add(new SqlParameter("@OrderByClause", orderBy));


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
                    return quotationRules;  //empty list

                }

                while (sqlDataReader.Read())
                {
                    quotationRules.Add(Deserialize(ref sqlDataReader));
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

            foreach (QuotationRule quotationRule in quotationRules)
            {
                if (supplier == null)
                {
                    //quotationRule.Supplier = engine.Suppliers.Get((string)(quotationRule.MetaInfo["Supplier.Code"]), DataState.Active);
                    
                    quotationRule.Supplier = new Supplier
                    {
                        Code = (string)(quotationRule.MetaInfo["Supplier.Code"]),
                        Name = (string)(quotationRule.MetaInfo["Supplier.Name"])
                    };
                }
                else
                {
                    quotationRule.Supplier = supplier;
                }

                if (brand == null)
                {
                    //quotationRule.Brand = engine.Brands.Get((string)(quotationRule.MetaInfo["Brand.Code"]), DataState.Active);
                    quotationRule.Brand = new Brand
                    {
                        Code = (string)quotationRule.MetaInfo["Brand.Code"],
                        Description = (string)(quotationRule.MetaInfo["Brand.Description"].ToString())
                    };
                }
                else
                {
                    quotationRule.Brand = brand;
                }

                if (category == null)
                {
                   
                    //quotationRule.Category = engine.Categories.Get((string)(quotationRule.MetaInfo["Category.Code"]), DataState.Active);
                    quotationRule.Category = new Category
                    {
                        Code = (string)(quotationRule.MetaInfo["Category.Code"]),
                        Description = (string)(quotationRule.MetaInfo["Category.Description"])
                    };
                }
                else
                {
                    quotationRule.Category = category;
                }


                if (stock == null)
                {
                    //quotationRule.Stock = engine.Stocks.Get((string)(quotationRule.MetaInfo["Stock.Code"]), DataState.Active);
                    quotationRule.Stock = new Stock
                    {
                        Code = (string)(quotationRule.MetaInfo["Stock.Code"]),
                        Description = (string)(quotationRule.MetaInfo["Stock.Description"]),
                        AvailabilityLevel = (short)(quotationRule.MetaInfo["Stock.AvailabilityLevel"])

                    };

                }
                else
                {
                    quotationRule.Stock = stock;
                }

                if (setSubstituteStockToLevel > 0 && quotationRule.MetaInfo["SubstituteStock.Code"] != DBNull.Value && (string)quotationRule.MetaInfo["SubstituteStock.Code"] != "")
                {
                    //quotationRule.SubstituteStock = engine.Stocks.Get((string)(quotationRule.MetaInfo["SubstituteStock.Code"]), DataState.Active);
                    quotationRule.SubstituteStock = new Stock
                    {
                        Code = (string)(quotationRule.MetaInfo["SubstituteStock.Code"]),
                        Description = (string)(quotationRule.MetaInfo["SubstituteStock.Description"])
                    };

                }

            }

            return quotationRules;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="supplier"></param>
        /// <param name="withCustomization"></param>
        /// <param name="closeReset"></param>
        /// <param name="setBrandToLevel"></param>
        /// <param name="setCategoryToLevel"></param>
        /// <param name="setStockToLevel"></param>
        /// <param name="setSubstituteStockToLevel"></param>
        /// <returns></returns>
        public List<QuotationRule> Get(Supplier supplier, bool withCustomization, bool closeReset, int setBrandToLevel, int setCategoryToLevel, int setStockToLevel, int setSubstituteStockToLevel)
        {

            List<QuotationRule> quotationRules;
            SqlDataReader sqlDataReader;
            List<SqlParameter> sqlParameters;
            string whereClauseValue = string.Empty;
            bool connectionOn = !engine.SqlServer.IsConnectionOpen();

            quotationRules = new List<QuotationRule>();
            sqlParameters = new List<SqlParameter>();

            sqlParameters.Add(new SqlParameter("@WhereClause", ""));
            whereClauseValue = string.Format("[FornecedorCodigo]='{0}' ", SQLStrings.CleanDangerousText(supplier.Code));

            if (withCustomization)
            {
                whereClauseValue += "AND [DataReset] IS NOT NULL ";
            }
            if (closeReset)
            {
                whereClauseValue += string.Format("AND [DataReset] >= '{0}'", DateTime.Now.AddDays(-30));
            }
            sqlParameters[sqlParameters.Count - 1].Value = whereClauseValue;

            

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
                    return quotationRules;  //empty list

                }

                while (sqlDataReader.Read())
                {
                    quotationRules.Add(Deserialize(ref sqlDataReader));
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

            foreach (QuotationRule quotationRule in quotationRules)
            {
                if (setBrandToLevel > 0)
                {
                    quotationRule.Brand = engine.Brands.Get((string)(quotationRule.MetaInfo["Brand.Code"]), DataState.Active);
                }
                else
                {
                    quotationRule.Brand = new Brand
                    {
                        Code = (string)quotationRule.MetaInfo["Brand.Code"],
                        Description = (string)(quotationRule.MetaInfo["Brand.Description"].ToString())
                    };
                }

                if (setCategoryToLevel > 0)
                {
                    quotationRule.Category = engine.Categories.Get((string)(quotationRule.MetaInfo["Category.Code"]), DataState.Active);
                }
                else
                {
                    quotationRule.Category = new Category
                    {
                        Code = (string)(quotationRule.MetaInfo["Category.Code"]),
                        Description = (string)(quotationRule.MetaInfo["Category.Description"])
                    };
                }


                if (setStockToLevel > 0)
                {
                    quotationRule.Stock = engine.Stocks.Get((string)(quotationRule.MetaInfo["Stock.Code"]), DataState.Active);

                }
                else
                {
                    quotationRule.Stock = new Stock
                    {
                        Code = (string)(quotationRule.MetaInfo["Stock.Code"]),
                        Description = (string)(quotationRule.MetaInfo["Stock.Description"]),
                        AvailabilityLevel = (short)(quotationRule.MetaInfo["Stock.AvailabilityLevel"])

                    };
                }

                if (setSubstituteStockToLevel > 0 && quotationRule.MetaInfo["SubstituteStock.Code"] != DBNull.Value)
                {
                    quotationRule.Stock = new Stock
                    {
                        Code = (string)(quotationRule.MetaInfo["SubstituteStock.Code"]),
                        Description = (string)(quotationRule.MetaInfo["SubstituteStock.Description"])

                    };

                }

                quotationRule.Supplier = supplier;
            }

            return quotationRules;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="supplier"></param>
        /// <param name="brand"></param>
        /// <param name="withCustomization"></param>
        /// <param name="closeReset"></param>
        /// <param name="setCategoryToLevel"></param>
        /// <param name="setStockToLevel"></param>
        /// <param name="setSubstituteStockToLevel"></param>
        /// <returns></returns>
        public List<QuotationRule> Get(Supplier supplier, Brand brand, bool withCustomization, bool closeReset, int setCategoryToLevel, int setStockToLevel, int setSubstituteStockToLevel)
        {

            List<QuotationRule> quotationRules;
            SqlDataReader sqlDataReader;
            List<SqlParameter> sqlParameters;
            string whereClauseValue = string.Empty;
            bool connectionOn = !engine.SqlServer.IsConnectionOpen();

            quotationRules = new List<QuotationRule>();
            sqlParameters = new List<SqlParameter>();

            sqlParameters.Add(new SqlParameter("@WhereClause", ""));
            whereClauseValue = string.Format("[FornecedorCodigo]='{0}' AND [MarcaCodigo]='{1}' ",
                                                                          SQLStrings.CleanDangerousText(supplier.Code),
                                                                          SQLStrings.CleanDangerousText(brand.Code));
            if (withCustomization)
            {
                whereClauseValue += "AND [DataReset] IS NOT NULL ";
            }
            if (closeReset)
            {
                whereClauseValue += string.Format("AND [DataReset] >= '{0}'", DateTime.Now.AddDays(-30));
            }
            sqlParameters[sqlParameters.Count - 1].Value = whereClauseValue;

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
                    return quotationRules;  //empty list

                }

                while (sqlDataReader.Read())
                {
                    quotationRules.Add(Deserialize(ref sqlDataReader));
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

            foreach (QuotationRule quotationRule in quotationRules)
            {
                if (setCategoryToLevel > 0)
                {
                    quotationRule.Category = engine.Categories.Get((string)(quotationRule.MetaInfo["Category.Code"]), DataState.Active);
                }
                else
                {
                    quotationRule.Category = new Category
                    {
                        Code = (string)(quotationRule.MetaInfo["Category.Code"]),
                        Description = (string)(quotationRule.MetaInfo["Category.Description"])
                    };
                }


                if (setStockToLevel > 0)
                {
                    quotationRule.Stock = engine.Stocks.Get((string)(quotationRule.MetaInfo["Stock.Code"]), DataState.Active);

                }
                else
                {
                    quotationRule.Stock = new Stock
                    {
                        Code = (string)(quotationRule.MetaInfo["Stock.Code"]),
                        Description = (string)(quotationRule.MetaInfo["Stock.Description"]),
                        AvailabilityLevel = (short)(quotationRule.MetaInfo["Stock.AvailabilityLevel"])

                    };
                }

                if (setSubstituteStockToLevel > 0 && quotationRule.MetaInfo["SubstituteStock.Code"] != DBNull.Value)
                {
                    quotationRule.SubstituteStock = engine.Stocks.Get((string)(quotationRule.MetaInfo["SubstituteStock.Code"]), DataState.Active);

                }

                quotationRule.Supplier = supplier;
                quotationRule.Brand = brand;
            }


            return quotationRules;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="supplier"></param>
        /// <param name="brand"></param>
        /// <param name="category"></param>
        /// <param name="withCustomization"></param>
        /// <param name="closeReset"></param>
        /// <param name="setStockToLevel"></param>
        /// <param name="setSubstituteStockToLevel"></param>
        /// <returns></returns>
        public List<QuotationRule> Get(Supplier supplier, Brand brand, Category category, bool withCustomization, bool closeReset, int setStockToLevel, int setSubstituteStockToLevel)
        {

            List<QuotationRule> quotationRules;
            SqlDataReader sqlDataReader;
            List<SqlParameter> sqlParameters;
            string whereClauseValue = string.Empty;
            bool connectionOn = !engine.SqlServer.IsConnectionOpen();

            quotationRules = new List<QuotationRule>();
            sqlParameters = new List<SqlParameter>();

            sqlParameters.Add(new SqlParameter("@WhereClause", ""));
            whereClauseValue = string.Format("[FornecedorCodigo]='{0}' AND [MarcaCodigo]='{1}' AND [CategoriaCodigo]='{2}'",
                                                                          SQLStrings.CleanDangerousText(supplier.Code),
                                                                          SQLStrings.CleanDangerousText(brand.Code),
                                                                          SQLStrings.CleanDangerousText(category.Code));
            if (withCustomization)
            {
                whereClauseValue += "AND [DataReset] IS NOT NULL ";
            }
            if (closeReset)
            {
                whereClauseValue += string.Format("AND [DataReset] >= '{0}'", DateTime.Now.AddDays(-30));
            }
            sqlParameters[sqlParameters.Count - 1].Value = whereClauseValue;

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
                    return quotationRules;  //empty list

                }

                while (sqlDataReader.Read())
                {
                    quotationRules.Add(Deserialize(ref sqlDataReader));
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

            foreach (QuotationRule quotationRule in quotationRules)
            {
                if (setStockToLevel > 0)
                {
                    quotationRule.Stock = engine.Stocks.Get((string)(quotationRule.MetaInfo["Stock.Code"]), DataState.Active);

                }
                else
                {
                    quotationRule.Stock = new Stock
                    {
                        Code = (string)(quotationRule.MetaInfo["Stock.Code"]),
                        Description = (string)(quotationRule.MetaInfo["Stock.Description"]),
                        AvailabilityLevel = (short)(quotationRule.MetaInfo["Stock.AvailabilityLevel"])

                    };
                }

                if (setSubstituteStockToLevel > 0 && quotationRule.MetaInfo["SubstituteStock.Code"] != DBNull.Value)
                {
                    quotationRule.SubstituteStock = engine.Stocks.Get((string)(quotationRule.MetaInfo["SubstituteStock.Code"]), DataState.Active);

                }

                quotationRule.Supplier = supplier;
                quotationRule.Brand = brand;
                quotationRule.Category = category;
            }


            return quotationRules;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="withCustomization"></param>
        /// <param name="closeReset"></param>
        /// <param name="setSupplierToLevel"></param>
        /// <param name="setBrandToLevel"></param>
        /// <param name="setCategoryToLevel"></param>
        /// <param name="setStockToLevel"></param>
        /// <param name="setSubstituteStockToLevel"></param>
        /// <returns></returns>
        public List<QuotationRule> Get(bool withCustomization, bool closeReset, int setSupplierToLevel, int setBrandToLevel, int setCategoryToLevel, int setStockToLevel, int setSubstituteStockToLevel)
        {

            List<QuotationRule> quotationRules;
            SqlDataReader sqlDataReader;
            List<SqlParameter> sqlParameters;
            string whereClauseValue = string.Empty;
            bool connectionOn = !engine.SqlServer.IsConnectionOpen();

            quotationRules = new List<QuotationRule>();
            sqlParameters = new List<SqlParameter>();

            sqlParameters.Add(new SqlParameter("@WhereClause", ""));

            if (withCustomization)
            {
                whereClauseValue += "[DataReset] IS NOT NULL ";
            }
            if (closeReset)
            {
                if (whereClauseValue != string.Empty)
                {
                    whereClauseValue += "AND ";
                }

                whereClauseValue += string.Format("[DataReset] >= '{0}'", DateTime.Now.AddDays(-30));
            }
            sqlParameters[sqlParameters.Count - 1].Value = whereClauseValue;


            sqlParameters.Add(new SqlParameter("@OrderByClause", "[FornecedorNome] ASC"));

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
                    return quotationRules;  //empty list
                }

                while (sqlDataReader.Read())
                {
                    quotationRules.Add(Deserialize(ref sqlDataReader));
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

            foreach (QuotationRule quotationRule in quotationRules)
            {
                if (setSupplierToLevel > 0)
                {
                    quotationRule.Supplier = engine.Suppliers.Get((string)(quotationRule.MetaInfo["Supplier.Code"]), DataState.Active);
                }
                else
                {
                    quotationRule.Supplier = new Supplier
                    {
                        Code = (string)(quotationRule.MetaInfo["Supplier.Code"]),
                        Name = (string)(quotationRule.MetaInfo["Supplier.Description"])
                    };
                }

                if (setBrandToLevel > 0)
                {
                    quotationRule.Brand = engine.Brands.Get((string)(quotationRule.MetaInfo["Brand.Code"]), DataState.Active);
                }
                else
                {
                    quotationRule.Brand = new Brand
                    {
                        Code = (string)(quotationRule.MetaInfo["Brand.Code"]),
                        Description = (string)(quotationRule.MetaInfo["Brand.Description"])
                    };
                }

                if (setCategoryToLevel > 0)
                {
                    quotationRule.Category = engine.Categories.Get((string)(quotationRule.MetaInfo["Category.Code"]), DataState.Active);
                }
                else
                {
                    quotationRule.Category = new Category
                    {
                        Code = (string)(quotationRule.MetaInfo["Category.Code"]),
                        Description = (string)(quotationRule.MetaInfo["Category.Description"])
                    };
                }


                if (setStockToLevel > 0)
                {
                    quotationRule.Stock = engine.Stocks.Get((string)(quotationRule.MetaInfo["Stock.Code"]), DataState.Active);

                }
                else
                {
                    quotationRule.Stock = new Stock
                    {
                        Code = (string)(quotationRule.MetaInfo["Stock.Code"]),
                        Description = (string)(quotationRule.MetaInfo["Stock.Description"]),
                        AvailabilityLevel = (short)(quotationRule.MetaInfo["Stock.AvailabilityLevel"])

                    };
                }

                if (setSubstituteStockToLevel > 0 && quotationRule.MetaInfo["SubstituteStock.Code"] != DBNull.Value)
                {
                    quotationRule.SubstituteStock = engine.Stocks.Get((string)(quotationRule.MetaInfo["SubstituteStock.Code"]), DataState.Active);

                }
            }

            return quotationRules;
        }


    }
}
