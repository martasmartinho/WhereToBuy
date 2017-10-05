using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhereToBuy.entities;
using WhereToBuy.entities.specs;
using WhereToBuy.utils;
using WhereToBuy.utils.GlobalVariables;

namespace WhereToBuy.data
{
    public partial class CategoriesMatching
    {
         string _namespace = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
         string _className = "CategoriesMatching";
        string _procedureGetName = "CategoryMatchingSelect";


        DataEngine engine;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="engine"></param>
        public CategoriesMatching(DataEngine engine)
        {
            this.engine = engine;
        }


        public CategoryMatching Get(Supplier supplier, string code, DataState dataState, int setCategoryToLevel)
        {

            CategoryMatching categoryMatching;
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
                categoryMatching = this.Deserialize(ref sqlDataReader);
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

            if (setCategoryToLevel > 0 && categoryMatching.MetaInfo["Category.Code"] != DBNull.Value && categoryMatching.MetaInfo["Category.Code"].ToString() != "")
            {
                categoryMatching.MapTo = engine.Categories.Get((string)(categoryMatching.MetaInfo["Category.Code"]), dataState);

            }

            categoryMatching.Supplier = supplier;
           
            return categoryMatching;

        }


        public CategoryMatching Get(string supplierCode, string code, DataState dataState, int setSupplierToLevel, int setCategoryToLevel)
        {

            CategoryMatching categoryMatching;
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
                categoryMatching = this.Deserialize(ref sqlDataReader);
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

            if (setCategoryToLevel > 0 && categoryMatching.MetaInfo["Category.Code"].ToString() != DBNull.Value.ToString() && categoryMatching.MetaInfo["Category.Code"].ToString() != "")
            {
                categoryMatching.MapTo = engine.Categories.Get((string)(categoryMatching.MetaInfo["Category.Code"]), dataState);

            }
           

            if (setSupplierToLevel > 0)
            {
                categoryMatching.Supplier = engine.Suppliers.Get((string)(categoryMatching.MetaInfo["Supplier.Code"]), dataState);
            }
            else
            {
                categoryMatching.Supplier = new Supplier
                {
                    Code = (string)(categoryMatching.MetaInfo["Supplier.Code"]),
                    Name = (string)(categoryMatching.MetaInfo["Supplier.Name"])
                };
            }


            return categoryMatching;

        }

        public List<CategoryMatching> Get(DataState dataState, int setSupplierToLevel, int setCategoryToLevel)
        {

            List<CategoryMatching> categoriesMatching;
            SqlDataReader sqlDataReader;
            List<SqlParameter> sqlParameters;
            bool connectionOn = !engine.SqlServer.IsConnectionOpen();

            categoriesMatching = new List<CategoryMatching>();
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
                    return categoriesMatching;  //lista vazia
                }

                while (sqlDataReader.Read())
                {
                    categoriesMatching.Add(Deserialize(ref sqlDataReader));
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

          
            if (setCategoryToLevel > 0)
            {

                foreach (CategoryMatching categoryMatching in categoriesMatching)
                {
                    if (categoryMatching.MetaInfo["Category.Code"].ToString() != DBNull.Value.ToString() && categoryMatching.MetaInfo["Category.Code"].ToString() != "")
                    {
                        categoryMatching.MapTo = engine.Categories.Get((string)(categoryMatching.MetaInfo["Category.Code"]), dataState);
                    }
                }

            }
           

            if (setSupplierToLevel > 0)
            {

                foreach (CategoryMatching categoryMatching in categoriesMatching)
                {
                    categoryMatching.Supplier = engine.Suppliers.Get((string)(categoryMatching.MetaInfo["Supplier.Code"]), dataState);
                }

            }
            else
            {
                foreach (CategoryMatching categoryMatching in categoriesMatching)
                {


                    categoryMatching.Supplier = new Supplier
                    {
                        Code = (string)(categoryMatching.MetaInfo["Supplier.Code"]),
                        Name = (string)(categoryMatching.MetaInfo["Supplier.Name"])
                    };
                }
            }





            return categoriesMatching;
        }

        public List<CategoryMatching> Get(Supplier supplier, string code, DataState dataState, string orderBy, int setCategoryToLevel)
        {

            List<CategoryMatching> categoriesMatching;
            SqlDataReader sqlDataReader;
            List<SqlParameter> sqlParameters;
            bool connectionOn = !engine.SqlServer.IsConnectionOpen();
            string queryFilter = string.Empty;

            categoriesMatching = new List<CategoryMatching>();
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
                    return categoriesMatching;  //lista vazia
                }

                while (sqlDataReader.Read())
                {
                    categoriesMatching.Add(Deserialize(ref sqlDataReader));

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




            foreach (CategoryMatching categoryMatching in categoriesMatching)
            {
                if (setCategoryToLevel > 0)
                {
                    if (categoryMatching.MetaInfo["Category.Code"].ToString() != DBNull.Value.ToString() && categoryMatching.MetaInfo["Category.Code"].ToString() != "")
                    {
                        categoryMatching.MapTo = engine.Categories.Get((string)(categoryMatching.MetaInfo["Category.Code"]), DataState.All);
                    }
                }


                categoryMatching.Supplier = new Supplier
                {
                    Code = (string)(categoryMatching.MetaInfo["Supplier.Code"]),
                    Name = (string)(categoryMatching.MetaInfo["Supplier.Name"])
                };

            }


            return categoriesMatching;
        }
    }
}
