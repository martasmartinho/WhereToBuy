﻿using System;
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
    public partial class BrandsMatching
    {
        string _namespace = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
        string _className = "BrandsMatching";
        string _procedureGetName = "BrandMatchingSelect";


        DataEngine engine;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="engine"></param>
        public BrandsMatching(DataEngine engine)
        {
            this.engine = engine;
        }


        public BrandMatching Get(Supplier supplier, string code, DataState dataState, int setBrandToLevel)
        {

            BrandMatching brandMatching;
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
                brandMatching = this.Deserialize(ref sqlDataReader);
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

            if (setBrandToLevel > 0 && brandMatching.MetaInfo["Brand.Code"] != DBNull.Value && (string)brandMatching.MetaInfo["Brand.Code"] != "")
            {
                brandMatching.MapTo = engine.Brands.Get((string)(brandMatching.MetaInfo["Brand.Code"]), dataState);

            }

            brandMatching.Supplier = supplier;
            return brandMatching;

        }


        public BrandMatching Get(string supplierCode, string code, DataState dataState, int setSupplierToLevel, int setBrandToLevel)
        {

            BrandMatching brandMatching;
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
                brandMatching = this.Deserialize(ref sqlDataReader);
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

            if (setBrandToLevel > 0 && brandMatching.MetaInfo["Brand.Code"].ToString() != DBNull.Value.ToString() && brandMatching.MetaInfo["Brand.Code"].ToString() != DBNull.Value.ToString())
            {
                brandMatching.MapTo = engine.Brands.Get((string)(brandMatching.MetaInfo["Brand.Code"]), dataState);

            }


            if (setSupplierToLevel > 0)
            {
                brandMatching.Supplier = engine.Suppliers.Get((string)(brandMatching.MetaInfo["Supplier.Code"]), dataState);
            }
            else
            {
                brandMatching.Supplier = new Supplier
                {
                    Code = (string)(brandMatching.MetaInfo["Supplier.Code"]),
                    Name = (string)(brandMatching.MetaInfo["Supplier.Name"])
                };
            }


            return brandMatching;

        }



        public List<BrandMatching> Get(Supplier supplier, string code, DataState dataState, string orderBy, int setBrandToLevel)
        {

            List<BrandMatching> brandsMatching;
            SqlDataReader sqlDataReader;
            List<SqlParameter> sqlParameters;
            bool connectionOn = !engine.SqlServer.IsConnectionOpen();
            string queryFilter = string.Empty;

            brandsMatching = new List<BrandMatching>();
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
                    return brandsMatching;  //lista vazia
                }

                while (sqlDataReader.Read())
                {
                    brandsMatching.Add(Deserialize(ref sqlDataReader));

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




            foreach (BrandMatching brandMatching in brandsMatching)
            {
                if (setBrandToLevel > 0)
                {
                    if (brandMatching.MetaInfo["Brand.Code"].ToString() != DBNull.Value.ToString() && brandMatching.MetaInfo["Brand.Code"].ToString() != "")
                    {
                        brandMatching.MapTo = engine.Brands.Get((string)(brandMatching.MetaInfo["Brand.Code"]), DataState.All);
                    }
                }


                brandMatching.Supplier = new Supplier
                {
                    Code = (string)(brandMatching.MetaInfo["Supplier.Code"]),
                    Name = (string)(brandMatching.MetaInfo["Supplier.Name"])
                };

            }


            return brandsMatching;
        }


        public List<BrandMatching> Get(DataState dataState, int setSupplierToLevel, int setBrandToLevel)
        {

            List<BrandMatching> brandsMatching;
            SqlDataReader sqlDataReader;
            List<SqlParameter> sqlParameters;
            bool connectionOn = !engine.SqlServer.IsConnectionOpen();

            brandsMatching = new List<BrandMatching>();
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
                    return brandsMatching;  //lista vazia
                }

                while (sqlDataReader.Read())
                {
                    brandsMatching.Add(Deserialize(ref sqlDataReader));
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


            if (setBrandToLevel > 0)
            {

                foreach (BrandMatching brandMatching in brandsMatching)
                {
                    if (brandMatching.MetaInfo["Brand.Code"].ToString() != DBNull.Value.ToString())
                    {
                        brandMatching.MapTo = engine.Brands.Get((string)(brandMatching.MetaInfo["Brand.Code"]), dataState);
                    }
                }

            }


            if (setSupplierToLevel > 0)
            {

                foreach (BrandMatching brandMatching in brandsMatching)
                {
                    brandMatching.Supplier = engine.Suppliers.Get((string)(brandMatching.MetaInfo["Supplier.Code"]), dataState);
                }

            }
            else
            {
                foreach (BrandMatching brandMatching in brandsMatching)
                {

                    brandMatching.Supplier = new Supplier
                    {
                        Code = (string)(brandMatching.MetaInfo["Supplier.Code"]),
                        Name = (string)(brandMatching.MetaInfo["Supplier.Name"])
                    };
                }
            }





            return brandsMatching;
        }

    }
}
