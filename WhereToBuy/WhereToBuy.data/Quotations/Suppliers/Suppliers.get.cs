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
    public partial class Suppliers
    {

        string _namespace = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
        string _className = "Suppliers";
        string _procedureGetName = "SupplierSelect";


        DataEngine engine;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="engine"></param>
        public Suppliers(DataEngine engine)
        {
            this.engine = engine;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="dataState"></param>
        /// <returns></returns>
        public Supplier Get(string codigo, DataState dataState)
        {

            Supplier supplier;
            SqlDataReader sqlDataReader;
            List<SqlParameter> sqlParameters;
            bool connectionOn = !engine.SqlServer.IsConnectionOpen();

            sqlParameters = new List<SqlParameter>();

            sqlParameters.Add(new SqlParameter("@WhereClause", ""));
            switch (dataState)
            {
                case DataState.Active:
                    sqlParameters[sqlParameters.Count - 1].Value = string.Format("[Codigo]='{0}' AND [Inativo]='false'", SQLStrings.CleanDangerousText(codigo));
                    break;
                case DataState.Inactive:
                    sqlParameters[sqlParameters.Count - 1].Value = string.Format("[Codigo]='{0}' AND [Inativo]='true'", SQLStrings.CleanDangerousText(codigo));
                    break;
                case DataState.All:
                    sqlParameters[sqlParameters.Count - 1].Value = string.Format("[Codigo]='{0}'", SQLStrings.CleanDangerousText(codigo));
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
                        string.Format("{0} {1}", GlobalVariables.Resource.GetString("RecordNotFoundString", GlobalVariables.Culture), SQLStrings.CleanDangerousText(codigo)));
                }

                sqlDataReader.Read();
                supplier = this.Deserialize(ref sqlDataReader);
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

            return supplier;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataState"></param>
        /// <returns></returns>
        public List<Supplier> Get(DataState dataState)
        {

            List<Supplier> suppliers;
            SqlDataReader sqlDataReader;
            List<SqlParameter> sqlParameters;
            bool connectionOn = !engine.SqlServer.IsConnectionOpen();

            suppliers = new List<Supplier>();
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

            sqlParameters.Add(new SqlParameter("@OrderByClause", "[Nome] ASC"));

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
                    return suppliers;  //lista vazia
                }

                while (sqlDataReader.Read())
                {
                    suppliers.Add(Deserialize(ref sqlDataReader));
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

            return suppliers;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <param name="name"></param>
        /// <param name="dataState"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        public List<Supplier> Get(string[] code, string[] name, DataState dataState, string orderby)
        {

            List<Supplier> suppliers;
            SqlDataReader sqlDataReader;
            List<SqlParameter> sqlParameters;
            bool connectionOn = !engine.SqlServer.IsConnectionOpen();

            suppliers = new List<Supplier>();
            sqlParameters = new List<SqlParameter>();


            string where = "";

            // Codigo
            if (code.Length > 0)
            {
                for (int i = 0; i < code.Length; i++)
                {
                    // Limpar valor de texto perigoso
                    code[i] = SQLStrings.CleanDangerousText(code[i].ToString());

                    if (where.Length > 0)
                    {
                        where += "AND ";
                    }

                    where += string.Format("[Codigo] LIKE '%{0}%' ", code[i].ToString());
                }
            }


            // Descrição
            if (name.Length > 0)
            {
                for (int i = 0; i < name.Length; i++)
                {
                    // Limpar valor de texto perigoso
                    name[i] = SQLStrings.CleanDangerousText(name[i].ToString());

                    if (where.Length > 0)
                    {
                        where += "AND ";
                    }

                    where += string.Format("[Nome] COLLATE LATIN1_GENERAL_CI_AI LIKE '%{0}%' ", name[i].ToString());

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
                sqlParameters.Add(new SqlParameter("@OrderByClause", "[Name] ASC"));
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
                    return suppliers;  //lista vazia
                }

                while (sqlDataReader.Read())
                {
                    suppliers.Add(Deserialize(ref sqlDataReader));
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

            return suppliers;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchString"></param>
        /// <returns></returns>
        public List<Supplier> Get(string searchString)
        {

            List<Supplier> suppliers;
            SqlDataReader sqlDataReader;
            List<SqlParameter> sqlParameters;
            bool connectionOn = !engine.SqlServer.IsConnectionOpen();

            suppliers = new List<Supplier>();
            sqlParameters = new List<SqlParameter>();

            sqlParameters.Add(new SqlParameter("@WhereClause", ""));
            sqlParameters[sqlParameters.Count - 1].Value = string.Format("([Codigo] LIKE '%{0}%' OR [Nome] LIKE '%{0}%') AND [Inativo]='false'", SQLStrings.CleanDangerousText(searchString));


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
                        string.Format("{0} {1}", GlobalVariables.Resource.GetString("RecordNotFoundString", GlobalVariables.Culture), SQLStrings.CleanDangerousText(searchString)));
                }

                if (!sqlDataReader.HasRows)
                {
                    sqlDataReader.Dispose();
                    engine.SqlServer.CloseConnection();
                    return suppliers;  //lista vazia
                }

                while (sqlDataReader.Read())
                {
                    suppliers.Add(Deserialize(ref sqlDataReader));
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

            return suppliers;

        }
    }
}
