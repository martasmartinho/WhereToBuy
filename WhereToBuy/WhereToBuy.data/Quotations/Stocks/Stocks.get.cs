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
    public partial class Stocks
    {

        string _namespace = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
        string _className = "Stocks";
        string _procedureGetName = "StockSelect";


        DataEngine engine;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="engine"></param>
        public Stocks(DataEngine engine)
        {
            this.engine = engine;
        }


        public Stock Get(string code, DataState dataState, int setStockCodeExpirationP50ToLevel, int setStockCodeExpirationP60ToLevel, int setStockCodeExpirationP70ToLevel, int setStockCodeExpirationP80ToLevel, int setStockCodeExpirationP90ToLevel)
        {

            Stock stock;
            SqlDataReader sqlDataReader;
            List<SqlParameter> sqlParameters;
            bool connectionOn = !engine.SqlServer.IsConnectionOpen();

            sqlParameters = new List<SqlParameter>();

            sqlParameters.Add(new SqlParameter("@WhereClause", ""));
            switch (dataState)
            {
                case DataState.Active:
                    sqlParameters[sqlParameters.Count - 1].Value = string.Format("[Codigo]='{0}' AND [Inativo]='false'", code);
                    break;
                case DataState.Inactive:
                    sqlParameters[sqlParameters.Count - 1].Value = string.Format("[Codigo]='{0}' AND [Inativo]='true'", code);
                    break;
                case DataState.All:
                    sqlParameters[sqlParameters.Count - 1].Value = string.Format("[Codigo]='{0}'", code);
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
                        string.Format("{0} {1}", GlobalVariables.Resource.GetString("RecordNotFoundString", GlobalVariables.Culture),code));
                }

                sqlDataReader.Read();
                stock = this.Deserialize(ref sqlDataReader);
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

            if (setStockCodeExpirationP50ToLevel > 0 && stock.MetaInfo["StockCodeExpirationP50.Code"] != DBNull.Value)
            {
                stock.StockCodeExpirationP50 = engine.Stocks.Get((string)(stock.MetaInfo["StockCodeExpirationP50.Code"]), DataState.All, 0, 0, 0, 0, 0);

            }

            if (setStockCodeExpirationP60ToLevel > 0 && stock.MetaInfo["StockCodeExpirationP60.Code"] != DBNull.Value)
            {
                stock.StockCodeExpirationP60 = engine.Stocks.Get((string)(stock.MetaInfo["StockCodeExpirationP60.Code"]), DataState.All, 0, 0, 0, 0, 0);

            }

            if (setStockCodeExpirationP70ToLevel > 0 && stock.MetaInfo["StockCodeExpirationP70.Code"] != DBNull.Value)
            {
                stock.StockCodeExpirationP70 = engine.Stocks.Get((string)(stock.MetaInfo["StockCodeExpirationP70.Code"]), DataState.All, 0, 0, 0, 0, 0);

            }

            if (setStockCodeExpirationP80ToLevel > 0 && stock.MetaInfo["StockCodeExpirationP80.Code"] != DBNull.Value)
            {
                stock.StockCodeExpirationP80 = engine.Stocks.Get((string)(stock.MetaInfo["StockCodeExpirationP80.Code"]), DataState.All, 0, 0, 0, 0, 0);
            }

            if (setStockCodeExpirationP90ToLevel > 0 && stock.MetaInfo["StockCodeExpirationP90.Code"] != DBNull.Value)
            {
                stock.StockCodeExpirationP90 = engine.Stocks.Get((string)(stock.MetaInfo["StockCodeExpirationP90.Code"]), DataState.All, 0, 0, 0, 0, 0);
            }

            
            return stock;

        }


        public Stock Get(string code, DataState dataState)
        {

            Stock stock;
            SqlDataReader sqlDataReader;
            List<SqlParameter> sqlParameters;
            bool connectionOn = !engine.SqlServer.IsConnectionOpen();

            sqlParameters = new List<SqlParameter>();

            sqlParameters.Add(new SqlParameter("@WhereClause", ""));
            switch (dataState)
            {
                case DataState.Active:
                    sqlParameters[sqlParameters.Count - 1].Value = string.Format("[Codigo]='{0}' AND [Inativo]='false'", code);
                    break;
                case DataState.Inactive:
                    sqlParameters[sqlParameters.Count - 1].Value = string.Format("[Codigo]='{0}' AND [Inativo]='true'", code);
                    break;
                case DataState.All:
                    sqlParameters[sqlParameters.Count - 1].Value = string.Format("[Codigo]='{0}'", code);
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
                        string.Format("{0} {1}", GlobalVariables.Resource.GetString("RecordNotFoundString", GlobalVariables.Culture), code));
                }

                sqlDataReader.Read();
                stock = this.Deserialize(ref sqlDataReader);
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


            return stock;

        }


        public List<Stock> Get(DataState dataState, int setStockCodeExpirationP50ToLevel, int setStockCodeExpirationP60ToLevel, int setStockCodeExpirationP70ToLevel, int setStockCodeExpirationP80ToLevel, int setStockCodeExpirationP90ToLevel)
        {

            List<Stock> stocks;
            SqlDataReader sqlDataReader;
            List<SqlParameter> sqlParameters;
            bool connectionOn = !engine.SqlServer.IsConnectionOpen();

            stocks = new List<Stock>();
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
                    return stocks;  //lista vazia
                }

                while (sqlDataReader.Read())
                {
                    stocks.Add(Deserialize(ref sqlDataReader));
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

          
            foreach (Stock stock in stocks)
            {

                if (setStockCodeExpirationP50ToLevel > 0 && stock.MetaInfo["StockCodeExpirationP50.Code"] != DBNull.Value && stock.MetaInfo["StockCodeExpirationP50.Code"].ToString() != "")
                {
                    stock.StockCodeExpirationP50 = engine.Stocks.Get((string)(stock.MetaInfo["StockCodeExpirationP50.Code"]), DataState.All, 0, 0, 0, 0, 0);

                }

                if (setStockCodeExpirationP60ToLevel > 0 && stock.MetaInfo["StockCodeExpirationP60.Code"] != DBNull.Value && stock.MetaInfo["StockCodeExpirationP60.Code"].ToString() != "")
                {
                    stock.StockCodeExpirationP60 = engine.Stocks.Get((string)(stock.MetaInfo["StockCodeExpirationP60.Code"]), DataState.All, 0, 0, 0, 0, 0);

                }

                if (setStockCodeExpirationP70ToLevel > 0 && stock.MetaInfo["StockCodeExpirationP70.Code"] != DBNull.Value && stock.MetaInfo["StockCodeExpirationP70.Code"].ToString() != "")
                {
                    stock.StockCodeExpirationP70 = engine.Stocks.Get((string)(stock.MetaInfo["StockCodeExpirationP70.Code"]), DataState.All, 0, 0, 0, 0, 0);

                }

                if (setStockCodeExpirationP80ToLevel > 0 && stock.MetaInfo["StockCodeExpirationP80.Code"] != DBNull.Value && stock.MetaInfo["StockCodeExpirationP80.Code"].ToString() != "")
                {
                    stock.StockCodeExpirationP80 = engine.Stocks.Get((string)(stock.MetaInfo["StockCodeExpirationP80.Code"]), DataState.All, 0, 0, 0, 0, 0);
                }

                if (setStockCodeExpirationP90ToLevel > 0 && stock.MetaInfo["StockCodeExpirationP90.Code"] != DBNull.Value && stock.MetaInfo["StockCodeExpirationP90.Code"].ToString() != "")
                {
                    stock.StockCodeExpirationP90 = engine.Stocks.Get((string)(stock.MetaInfo["StockCodeExpirationP90.Code"]), DataState.All, 0, 0, 0, 0, 0);
                }

            }

            return stocks;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <param name="description"></param>
        /// <param name="dataState"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        public List<Stock> Get(string[] code, string[] description, DataState dataState, string orderby, int setStockCodeExpirationP50ToLevel, int setStockCodeExpirationP60ToLevel, int setStockCodeExpirationP70ToLevel, int setStockCodeExpirationP80ToLevel, int setStockCodeExpirationP90ToLevel)
        {

            List<Stock> stocks;
            SqlDataReader sqlDataReader;
            List<SqlParameter> sqlParameters;
            bool connectionOn = !engine.SqlServer.IsConnectionOpen();

            stocks = new List<Stock>();
            sqlParameters = new List<SqlParameter>();


            string where = "";

            // Codigo
            if (code.Length > 0)
            {
                for (int i = 0; i < code.Length; i++)
                {
                    // Limpar valor de texto perigoso
                    //code[i] = SQLStrings.CleanDangerousText(code[i].ToString());

                    if (where.Length > 0)
                    {
                        where += "AND ";
                    }
                    if (code[i].ToString() != "")
                    {
                        where += string.Format("[Codigo] LIKE '%{0}%' ", code[i].ToString());
                    }
                    
                }
            }


            // Descrição
            if (description.Length > 0)
            {
                for (int i = 0; i < description.Length; i++)
                {
                    // Limpar valor de texto perigoso
                    description[i] = description[i].ToString();

                    if (where.Length > 0)
                    {
                        where += "AND ";
                    }

                    if (description[i].ToString() != "")
                    {
                        where += string.Format("[Descricao] COLLATE LATIN1_GENERAL_CI_AI LIKE '%{0}%' ", description[i].ToString());
                    }
                }
            }


            // Estado de dados
            switch (dataState)
            {
                case DataState.Active:
                    if (where.Length > 0)
                    {
                        where += "AND ";
                    }
                    where += "[Inativo]='false'";
                    break;
                case DataState.Inactive:
                    if (where.Length > 0)
                    {
                        where += "AND ";
                    }
                    where += "[Inativo]='true'";
                    break;
                case DataState.All:
                    break;
                default:
                    throw new MyException(_namespace, _className, "Get()", string.Format("{0}!", GlobalVariables.Resource.GetString("ForeseenEnumeratorString", GlobalVariables.Culture).ToLower()));
            }


            if (where.Trim().Length > 0)
            {
                sqlParameters.Add(new SqlParameter("@WhereClause", where));
            }

            if (orderby.Trim().Length > 0)
            {
                sqlParameters.Add(new SqlParameter("@OrderByClause", SQLStrings.CleanDangerousText(orderby)));
            }
            else
            {
                sqlParameters.Add(new SqlParameter("@OrderByClause", "[Descricao] ASC"));
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
                    return stocks;  //lista vazia
                }

                while (sqlDataReader.Read())
                {
                    stocks.Add(Deserialize(ref sqlDataReader));
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
            foreach (Stock stock in stocks)
            {

                if (setStockCodeExpirationP50ToLevel > 0 && stock.MetaInfo["StockCodeExpirationP50.Code"] != DBNull.Value && stock.MetaInfo["StockCodeExpirationP50.Code"].ToString() != "")
                {
                    stock.StockCodeExpirationP50 = engine.Stocks.Get((string)(stock.MetaInfo["StockCodeExpirationP50.Code"]), DataState.All, 0, 0, 0, 0, 0);

                }

                if (setStockCodeExpirationP60ToLevel > 0 && stock.MetaInfo["StockCodeExpirationP60.Code"] != DBNull.Value && stock.MetaInfo["StockCodeExpirationP60.Code"].ToString() != "")
                {
                    stock.StockCodeExpirationP60 = engine.Stocks.Get((string)(stock.MetaInfo["StockCodeExpirationP60.Code"]), DataState.All, 0, 0, 0, 0, 0);

                }

                if (setStockCodeExpirationP70ToLevel > 0 && stock.MetaInfo["StockCodeExpirationP70.Code"] != DBNull.Value && stock.MetaInfo["StockCodeExpirationP70.Code"].ToString() != "")
                {
                    stock.StockCodeExpirationP70 = engine.Stocks.Get((string)(stock.MetaInfo["StockCodeExpirationP70.Code"]), DataState.All, 0, 0, 0, 0, 0);

                }

                if (setStockCodeExpirationP80ToLevel > 0 && stock.MetaInfo["StockCodeExpirationP80.Code"] != DBNull.Value && stock.MetaInfo["StockCodeExpirationP80.Code"].ToString() != "")
                {
                    stock.StockCodeExpirationP80 = engine.Stocks.Get((string)(stock.MetaInfo["StockCodeExpirationP80.Code"]), DataState.All, 0, 0, 0, 0, 0);
                }

                if (setStockCodeExpirationP90ToLevel > 0 && stock.MetaInfo["StockCodeExpirationP90.Code"] != DBNull.Value && stock.MetaInfo["StockCodeExpirationP90.Code"].ToString() != "")
                {
                    stock.StockCodeExpirationP90 = engine.Stocks.Get((string)(stock.MetaInfo["StockCodeExpirationP90.Code"]), DataState.All, 0, 0, 0, 0, 0);
                }

            }


            return stocks;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchString"></param>
        /// <returns></returns>
        public List<Stock> Get(string searchString)
        {

            List<Stock> stocks;
            SqlDataReader sqlDataReader;
            List<SqlParameter> sqlParameters;
            bool connectionOn = !engine.SqlServer.IsConnectionOpen();

            stocks = new List<Stock>();
            sqlParameters = new List<SqlParameter>();

            sqlParameters.Add(new SqlParameter("@WhereClause", ""));
            sqlParameters[sqlParameters.Count - 1].Value = string.Format("([Codigo] LIKE '%{0}%' OR [Descricao] LIKE '%{0}%') AND [Inativo]='false'", searchString);


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
                        string.Format("{0} {1}", GlobalVariables.Resource.GetString("RecordNotFoundString", GlobalVariables.Culture), searchString));
                }

                if (!sqlDataReader.HasRows)
                {
                    sqlDataReader.Dispose();
                    engine.SqlServer.CloseConnection();
                    return stocks;  //lista vazia
                }

                while (sqlDataReader.Read())
                {
                    stocks.Add(Deserialize(ref sqlDataReader));
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

            return stocks;

        }

    }
}
