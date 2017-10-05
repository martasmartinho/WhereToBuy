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
    public partial class SupplementsMatching
    {
        string _namespace = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
        string _className = "SupplementsMatching";
        string _procedureGetName = "SupplementMatchingSelect";


        DataEngine engine;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="engine"></param>
        public SupplementsMatching(DataEngine engine)
        {
            this.engine = engine;
        }


        public SupplementMatching Get(Supplier supplier, string code, DataState dataState, int setSupplementToLevel)
        {

            SupplementMatching supplementMatching;
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
                supplementMatching = this.Deserialize(ref sqlDataReader);
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

            if (setSupplementToLevel > 0 && supplementMatching.MetaInfo["Supplement.Code"] != DBNull.Value && supplementMatching.MetaInfo["Supplement.Code"].ToString() != "")
            {
                supplementMatching.MapTo = engine.Supplements.Get((string)(supplementMatching.MetaInfo["Supplement.Code"]), dataState);

            }

            supplementMatching.Supplier = supplier;
           
            return supplementMatching;

        }


        public SupplementMatching Get(string supplierCode, string code, DataState dataState, int setSupplierToLevel, int setSupplementToLevel)
        {

            SupplementMatching supplementMatching;
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
                supplementMatching = this.Deserialize(ref sqlDataReader);
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

            if (setSupplementToLevel > 0 && supplementMatching.MetaInfo["Supplement.Code"].ToString() != DBNull.Value.ToString() && supplementMatching.MetaInfo["Supplement.Code"].ToString() != "")
            {
                supplementMatching.MapTo = engine.Supplements.Get((string)(supplementMatching.MetaInfo["Supplement.Code"]), dataState);

            }
           

            if (setSupplierToLevel > 0)
            {
                supplementMatching.Supplier = engine.Suppliers.Get((string)(supplementMatching.MetaInfo["Supplier.Code"]), dataState);
            }
            else
            {
                supplementMatching.Supplier = new Supplier
                {
                    Code = (string)(supplementMatching.MetaInfo["Supplier.Code"]),
                    Name = (string)(supplementMatching.MetaInfo["Supplier.Name"])
                };
            }


            return supplementMatching;

        }

        public List<SupplementMatching> Get(DataState dataState, int setSupplierToLevel, int setSupplementToLevel)
        {

            List<SupplementMatching> supplementsMatching;
            SqlDataReader sqlDataReader;
            List<SqlParameter> sqlParameters;
            bool connectionOn = !engine.SqlServer.IsConnectionOpen();

            supplementsMatching = new List<SupplementMatching>();
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
                    return supplementsMatching;  //lista vazia
                }

                while (sqlDataReader.Read())
                {
                    supplementsMatching.Add(Deserialize(ref sqlDataReader));
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

          
            if (setSupplementToLevel > 0)
            {

                foreach (SupplementMatching supplementMatching in supplementsMatching)
                {
                    if (supplementMatching.MetaInfo["Supplement.Code"].ToString() != DBNull.Value.ToString() && supplementMatching.MetaInfo["Supplement.Code"].ToString() != "")
                    {
                        supplementMatching.MapTo = engine.Supplements.Get((string)(supplementMatching.MetaInfo["Supplement.Code"]), dataState);
                    }
                }

            }
           

            if (setSupplierToLevel > 0)
            {

                foreach (SupplementMatching supplementMatching in supplementsMatching)
                {
                    supplementMatching.Supplier = engine.Suppliers.Get((string)(supplementMatching.MetaInfo["Supplier.Code"]), dataState);
                }

            }
            else
            {
                foreach (SupplementMatching supplementMatching in supplementsMatching)
                {

                    supplementMatching.Supplier = new Supplier
                    {
                        Code = (string)(supplementMatching.MetaInfo["Supplier.Code"]),
                        Name = (string)(supplementMatching.MetaInfo["Supplier.Name"])
                    };
                }
            }





            return supplementsMatching;
        }

        public List<SupplementMatching> Get(Supplier supplier, string code, DataState dataState, string orderBy, int setSupplementToLevel)
        {

            List<SupplementMatching> supplementsMatching;
            SqlDataReader sqlDataReader;
            List<SqlParameter> sqlParameters;
            bool connectionOn = !engine.SqlServer.IsConnectionOpen();
            string queryFilter = string.Empty;

            supplementsMatching = new List<SupplementMatching>();
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
                    return supplementsMatching;  //lista vazia
                }

                while (sqlDataReader.Read())
                {
                    supplementsMatching.Add(Deserialize(ref sqlDataReader));

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




            foreach (SupplementMatching supplementMatching in supplementsMatching)
            {
                if (setSupplementToLevel > 0)
                {
                    if (supplementMatching.MetaInfo["Supplement.Code"].ToString() != DBNull.Value.ToString() && supplementMatching.MetaInfo["Supplement.Code"].ToString() != "")
                    {
                        supplementMatching.MapTo = engine.Supplements.Get((string)(supplementMatching.MetaInfo["Supplement.Code"]), DataState.All);
                    }
                }


                supplementMatching.Supplier = new Supplier
                {
                    Code = (string)(supplementMatching.MetaInfo["Supplier.Code"]),
                    Name = (string)(supplementMatching.MetaInfo["Supplier.Name"])
                };

            }


            return supplementsMatching;
        }
    }
}
