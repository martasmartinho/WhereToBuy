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
    public partial class StocksMatching
    {
        string _namespace = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
        string _className = "StocksMatching";
        string _procedureGetName = "StockMatchingSelect";


        DataEngine engine;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="engine"></param>
        public StocksMatching(DataEngine engine)
        {
            this.engine = engine;
        }


        public StockMatching Get(Supplier supplier, string code, DataState dataState, int setStockToLevel)
        {

            StockMatching stockMatching;
            SqlDataReader sqlDataReader;
            List<SqlParameter> sqlParameters;
            bool connectionOn = !engine.SqlServer.IsConnectionOpen();

            sqlParameters = new List<SqlParameter>();

            sqlParameters.Add(new SqlParameter("@WhereClause", ""));
            switch (dataState)
            {
                case DataState.Active:
                    sqlParameters[sqlParameters.Count - 1].Value = string.Format("[FornecedorCodigo]='{0}' AND matching.[Codigo]='{1}' AND matching.[Inativo]='false'", SQLStrings.CleanDangerousText(supplier.Code), SQLStrings.CleanDangerousText(code));
                    break;
                case DataState.Inactive:
                    sqlParameters[sqlParameters.Count - 1].Value = string.Format("[FornecedorCodigo]='{0}' AND matching.[Codigo]='{1}' AND matching.[Inativo]='true'", SQLStrings.CleanDangerousText(supplier.Code), SQLStrings.CleanDangerousText(code));
                    break;
                case DataState.All:
                    sqlParameters[sqlParameters.Count - 1].Value = string.Format("[FornecedorCodigo]='{0}' AND matching.[Codigo]='{1}'", SQLStrings.CleanDangerousText(supplier.Code), SQLStrings.CleanDangerousText(code));
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
                stockMatching = this.Deserialize(ref sqlDataReader);
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

            if (setStockToLevel > 0 && stockMatching.MetaInfo["Stock.Code"] != DBNull.Value && stockMatching.MetaInfo["Stock.Code"].ToString() != "")
            {
                stockMatching.MapTo = engine.Stocks.Get((string)(stockMatching.MetaInfo["Stock.Code"]), dataState, 0, 0, 0, 0, 0);

            }

            stockMatching.Supplier = supplier;
           
            return stockMatching;

        }


        public StockMatching Get(string supplierCode, string code, DataState dataState, int setSupplierToLevel, int setStockToLevel)
        {

            StockMatching stockMatching;
            SqlDataReader sqlDataReader;
            List<SqlParameter> sqlParameters;
            bool connectionOn = !engine.SqlServer.IsConnectionOpen();

            sqlParameters = new List<SqlParameter>();

            sqlParameters.Add(new SqlParameter("@WhereClause", ""));
            switch (dataState)
            {
                case DataState.Active:
                    sqlParameters[sqlParameters.Count - 1].Value = string.Format("[FornecedorCodigo]='{0}' AND matching.[Codigo]='{1}' AND matching.[Inativo]='false'", SQLStrings.CleanDangerousText(supplierCode), SQLStrings.CleanDangerousText(code));
                    break;
                case DataState.Inactive:
                    sqlParameters[sqlParameters.Count - 1].Value = string.Format("[FornecedorCodigo]='{0}' AND matching.[Codigo]='{1}' AND matching.[Inativo]='true'", SQLStrings.CleanDangerousText(supplierCode), SQLStrings.CleanDangerousText(code));
                    break;
                case DataState.All:
                    sqlParameters[sqlParameters.Count - 1].Value = string.Format("[FornecedorCodigo]='{0}' AND matching.[Codigo]='{1}'", SQLStrings.CleanDangerousText(supplierCode), SQLStrings.CleanDangerousText(code));
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
                        string.Format("{0} ({1}) ({2}) ", GlobalVariables.Resource.GetString("RecordNotFoundString", GlobalVariables.Culture), SQLStrings.CleanDangerousText(supplierCode), SQLStrings.CleanDangerousText(code)));
                }

                sqlDataReader.Read();
                stockMatching = this.Deserialize(ref sqlDataReader);
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

            if (setStockToLevel > 0 && stockMatching.MetaInfo["Stock.Code"].ToString() != DBNull.Value.ToString() && stockMatching.MetaInfo["Stock.Code"].ToString() != "")
            {
                stockMatching.MapTo = engine.Stocks.Get((string)(stockMatching.MetaInfo["Stock.Code"]), dataState, 0, 0, 0, 0, 0);

            }
           

            if (setSupplierToLevel > 0)
            {
                stockMatching.Supplier = engine.Suppliers.Get((string)(stockMatching.MetaInfo["Supplier.Code"]), dataState);
            }
            else
            {
                stockMatching.Supplier = new Supplier
                {
                    Code = (string)(stockMatching.MetaInfo["Supplier.Code"]),
                    Name = (string)(stockMatching.MetaInfo["Supplier.Name"])
                };
            }


            return stockMatching;

        }


        public List<StockMatching> Get(DataState dataState, int setSupplierToLevel, int setStockToLevel)
        {

            List<StockMatching> stocksMatching;
            SqlDataReader sqlDataReader;
            List<SqlParameter> sqlParameters;
            bool connectionOn = !engine.SqlServer.IsConnectionOpen();

            stocksMatching = new List<StockMatching>();
            sqlParameters = new List<SqlParameter>();

            sqlParameters.Add(new SqlParameter("@WhereClause", ""));
            switch (dataState)
            {
                case DataState.Active:
                    sqlParameters[sqlParameters.Count - 1].Value = "[Inativo]='false'";
                    break;
                case DataState.Inactive:
                    sqlParameters[sqlParameters.Count - 1].Value = "[Inativo]='true'";
                    break;
                case DataState.All:
                    sqlParameters[sqlParameters.Count - 1].Value = string.Empty;
                    break;
                default:
                    throw new MyException(_namespace, _className, "Get()", string.Format("{0}!", GlobalVariables.Resource.GetString("ForeseenEnumeratorString", GlobalVariables.Culture).ToLower()));
            }

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
                    return stocksMatching;  //lista vazia
                }

                while (sqlDataReader.Read())
                {
                    stocksMatching.Add(Deserialize(ref sqlDataReader));
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

          
            if (setStockToLevel > 0)
            {

                foreach (StockMatching stockMatching in stocksMatching)
                {
                    if (stockMatching.MetaInfo["Stock.Code"].ToString() != DBNull.Value.ToString() && stockMatching.MetaInfo["Stock.Code"].ToString() != "")
                    {
                        stockMatching.MapTo = engine.Stocks.Get((string)(stockMatching.MetaInfo["Stock.Code"]), dataState);
                    }
                }

            }
           

            if (setSupplierToLevel > 0)
            {

                foreach (StockMatching stockMatching in stocksMatching)
                {
                    stockMatching.Supplier = engine.Suppliers.Get((string)(stockMatching.MetaInfo["Supplier.Code"]), dataState);
                }

            }
            else
            {
                foreach (StockMatching stockMatching in stocksMatching)
                {

                    stockMatching.Supplier = new Supplier
                    {
                        Code = (string)(stockMatching.MetaInfo["Supplier.Code"]),
                        Name = (string)(stockMatching.MetaInfo["Supplier.Name"])
                    };
                }
            }





            return stocksMatching;
        }


        public List<StockMatching> Get(Supplier supplier, string code, DataState dataState, string orderBy, int setStockToLevel)
        {

            List<StockMatching> stocksMatching;
            SqlDataReader sqlDataReader;
            List<SqlParameter> sqlParameters;
            bool connectionOn = !engine.SqlServer.IsConnectionOpen();
            string queryFilter = string.Empty;

            stocksMatching = new List<StockMatching>();
            sqlParameters = new List<SqlParameter>();

            if (supplier != null)
            {
                queryFilter = string.Format("[FornecedorCodigo]='{0}'", SQLStrings.CleanDangerousText(supplier.Code));
            }

            if (code != "")
            {
                if (queryFilter != string.Empty)
                {
                    queryFilter += " AND";
                }

                queryFilter += string.Format(" matching.[Codigo]='{0}'", SQLStrings.CleanDangerousText(code));
            }



            if (queryFilter != string.Empty && dataState != DataState.All)
            {
                queryFilter += " AND";
            }


            sqlParameters.Add(new SqlParameter("@WhereClause", ""));
            switch (dataState)
            {
                case DataState.Active:
                    sqlParameters[sqlParameters.Count - 1].Value = queryFilter + " matching.[Inativo]='false'";
                    break;
                case DataState.Inactive:
                    sqlParameters[sqlParameters.Count - 1].Value = queryFilter + " matching.[Inativo]='true'";
                    break;
                case DataState.All:
                    sqlParameters[sqlParameters.Count - 1].Value = queryFilter;
                    break;
                case DataState.None:
                    sqlParameters[sqlParameters.Count - 1].Value = queryFilter + " matching.[Inativo]='false' AND MapTo IS NULL";
                    break;
                default:
                    throw new MyException(_namespace, _className, "Get()", string.Format("{0}!", GlobalVariables.Resource.GetString("ForeseenEnumeratorString", GlobalVariables.Culture).ToLower()));
            }

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
                    return stocksMatching;  //lista vazia
                }

                while (sqlDataReader.Read())
                {
                    stocksMatching.Add(Deserialize(ref sqlDataReader));

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




            foreach (StockMatching stockMatching in stocksMatching)
            {
                if (setStockToLevel > 0)
                {
                    if (stockMatching.MetaInfo["Stock.Code"].ToString() != DBNull.Value.ToString() && stockMatching.MetaInfo["Stock.Code"].ToString() != "")
                    {
                        stockMatching.MapTo = engine.Stocks.Get((string)(stockMatching.MetaInfo["Stock.Code"]), DataState.All);
                    }
                }


                stockMatching.Supplier = new Supplier
                {
                    Code = (string)(stockMatching.MetaInfo["Supplier.Code"]),
                    Name = (string)(stockMatching.MetaInfo["Supplier.Name"])
                };

            }


            return stocksMatching;
        }
    }
}
