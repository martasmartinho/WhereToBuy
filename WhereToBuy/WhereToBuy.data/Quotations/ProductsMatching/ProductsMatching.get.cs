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
    public partial class ProductsMatching
    {
        string _namespace = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
        string _className = "ProductsMatching";
        string _procedureGetName = "ProductMatchingSelect";


        DataEngine engine;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="engine"></param>
        public ProductsMatching(DataEngine engine)
        {
            this.engine = engine;
        }


        public ProductMatching Get(Supplier supplier, string code, string supplementCode, DataState dataState, int setProductToLevel, int setStockLevel)
        {

            ProductMatching productMatching;
            SqlDataReader sqlDataReader;
            List<SqlParameter> sqlParameters;
            bool connectionOn = !engine.SqlServer.IsConnectionOpen();

            sqlParameters = new List<SqlParameter>();

            sqlParameters.Add(new SqlParameter("@WhereClause", ""));
            switch (dataState)
            {
                case DataState.Active:
                    sqlParameters[sqlParameters.Count - 1].Value = string.Format("m.[FornecedorCodigo]='{0}' AND ..[Codigo]='{1}' AND m.[ComplementoCodigo]='{2}' AND m.[Inativo]='false'", SQLStrings.CleanDangerousText(supplier.Code), 
                                                                                    SQLStrings.CleanDangerousText(code), SQLStrings.CleanDangerousText(supplementCode));
                    break;
                case DataState.Inactive:
                    sqlParameters[sqlParameters.Count - 1].Value = string.Format("m.[FornecedorCodigo]='{0}' AND m.[Codigo]='{1}' AND m.[ComplementoCodigo]='{2}' AND m.[Inativo]='true'", SQLStrings.CleanDangerousText(supplier.Code), SQLStrings.CleanDangerousText(code),
                                                                                    SQLStrings.CleanDangerousText(supplementCode));
                    break;
                case DataState.All:
                    sqlParameters[sqlParameters.Count - 1].Value = string.Format("m.[FornecedorCodigo]='{0}' AND m.[Codigo]='{1}' AND m.[ComplementoCodigo]='{2}'", SQLStrings.CleanDangerousText(supplier.Code), SQLStrings.CleanDangerousText(code),
                                                                                    SQLStrings.CleanDangerousText(supplementCode));
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
                productMatching = this.Deserialize(ref sqlDataReader);
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

            if ( productMatching.MetaInfo["Product.Code"] != DBNull.Value && (string)productMatching.MetaInfo["Product.Code"] != "")
            {
                if (setProductToLevel > 0)
	            {
		             productMatching.MapTo = engine.Products.Get((string)(productMatching.MetaInfo["Product.Code"]), dataState, 0);
	            }
                else
	            {
                    productMatching.MapTo = new Product
                    {
                        Code = (string)(productMatching.MetaInfo["Product.Code"]),
                        Description = (string)(productMatching.MetaInfo["Product.Description"])
                    };
	            }
            }


            if (string.IsNullOrEmpty(productMatching.MetaInfo["Stock.Code"].ToString()) && productMatching.MetaInfo["Stock.Code"].ToString() != "")
            {
                if (setProductToLevel > 0)
                {
                    productMatching.ReplacementStock = engine.Stocks.Get((string)(productMatching.MetaInfo["Stock.Code"]), dataState);
                }
                else
                {
                    productMatching.MapTo = new Product
                    {
                        Code = (string)(productMatching.MetaInfo["Stock.Code"]),
                        Description = (string)(productMatching.MetaInfo["Stock.Description"])
                    };
                }

            }

            productMatching.Supplier = supplier;
            return productMatching;

        }


        public ProductMatching Get(string supplierCode, string code, string supplementCode, DataState dataState, int setSupplierToLevel, int setProductToLevel, int setStockToLevel)
        {

            ProductMatching productMatching;
            SqlDataReader sqlDataReader;
            List<SqlParameter> sqlParameters;
            bool connectionOn = !engine.SqlServer.IsConnectionOpen();

            sqlParameters = new List<SqlParameter>();

            sqlParameters.Add(new SqlParameter("@WhereClause", ""));
            switch (dataState)
            {
                 case DataState.Active:
                    sqlParameters[sqlParameters.Count - 1].Value = string.Format("m.[FornecedorCodigo]='{0}' AND m.[Codigo]='{1}' AND m.[ComplementoCodigo]='{2}' AND m.[Inativo]='false'", SQLStrings.CleanDangerousText(supplierCode), 
                                                                                    SQLStrings.CleanDangerousText(code), SQLStrings.CleanDangerousText(supplementCode));
                    break;
                case DataState.Inactive:
                    sqlParameters[sqlParameters.Count - 1].Value = string.Format("m.[FornecedorCodigo]='{0}' AND m.[Codigo]='{1}' AND m.[ComplementoCodigo]='{2}' AND m.[Inativo]='true'", SQLStrings.CleanDangerousText(supplierCode), SQLStrings.CleanDangerousText(code),
                                                                                    SQLStrings.CleanDangerousText(supplementCode));
                    break;
                case DataState.All:
                    sqlParameters[sqlParameters.Count - 1].Value = string.Format("m.[FornecedorCodigo]='{0}' AND m.[Codigo]='{1}' AND m.[ComplementoCodigo]='{2}'", SQLStrings.CleanDangerousText(supplierCode), SQLStrings.CleanDangerousText(code),
                                                                                    SQLStrings.CleanDangerousText(supplementCode));
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
                productMatching = this.Deserialize(ref sqlDataReader);
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

            if ( productMatching.MetaInfo["Product.Code"] != DBNull.Value && (string)productMatching.MetaInfo["Product.Code"] != "")
            {
                if (setProductToLevel > 0)
	            {
		             productMatching.MapTo = engine.Products.Get((string)(productMatching.MetaInfo["Product.Code"]), dataState, 0);
	            }
                else
	            {
                    productMatching.MapTo = new Product
                    {
                        Code = (string)(productMatching.MetaInfo["Product.Code"]),
                        Description = (string)(productMatching.MetaInfo["Product.Description"])
                    };
	            }
            }


            if (string.IsNullOrEmpty(productMatching.MetaInfo["Stock.Code"].ToString()) && productMatching.MetaInfo["Stock.Code"].ToString() != "")
            {
                if (setProductToLevel > 0)
                {
                    productMatching.ReplacementStock = engine.Stocks.Get((string)(productMatching.MetaInfo["Stock.Code"]), dataState);
                }
                else
                {
                    productMatching.MapTo = new Product
                    {
                        Code = (string)(productMatching.MetaInfo["Stock.Code"]),
                        Description = (string)(productMatching.MetaInfo["Stock.Description"])
                    };
                }

            }


            if (setSupplierToLevel > 0)
            {
                productMatching.Supplier = engine.Suppliers.Get((string)(productMatching.MetaInfo["Supplier.Code"]), dataState);
            }
            else
            {
                productMatching.Supplier = new Supplier
                {
                    Code = (string)(productMatching.MetaInfo["Supplier.Code"]),
                    Name = (string)(productMatching.MetaInfo["Supplier.Name"])
                };
            }


            return productMatching;

        }


        public List<ProductMatching> Get(Supplier supplier, string code, string supplementCode, DataState dataState, bool withCustomization, bool closeReset, string orderBy, int setProductToLevel, int setStockToLevel)
        {

            List<ProductMatching> productsMatching;
            SqlDataReader sqlDataReader;
            List<SqlParameter> sqlParameters;
            bool connectionOn = !engine.SqlServer.IsConnectionOpen();
            string queryFilter = string.Empty;

            productsMatching = new List<ProductMatching>();
            sqlParameters = new List<SqlParameter>();

            if (supplier != null)
            {
                queryFilter = string.Format("m.[FornecedorCodigo]='{0}'", SQLStrings.CleanDangerousText(supplier.Code));
            }

            if (code != "")
            {
                if (queryFilter != string.Empty)
                {
                    queryFilter += " AND";
                }

                queryFilter += string.Format(" m.[Codigo] LIKE '%{0}%'", SQLStrings.CleanDangerousText(code));
            }

            if (supplementCode != "")
            {
                if (queryFilter != string.Empty)
                {
                    queryFilter += " AND";
                }

                queryFilter += string.Format(" m.[ComplementoCodigo]='{0}'", SQLStrings.CleanDangerousText(supplementCode));
            }

            if (withCustomization)
            {
                if (queryFilter != string.Empty)
                {
                    queryFilter += " AND";
                }

                queryFilter += " [DataReset] IS NOT NULL";

            }

            if (closeReset)
            {
                if (queryFilter != string.Empty)
                {
                    queryFilter += " AND";
                }

                queryFilter += string.Format(" [DataReset] BETWEEN '{0}' AND '{1}'", DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.AddDays(30).ToString("yyyy-MM-dd"));
            }

            if (queryFilter != string.Empty && dataState != DataState.All)
            {
                queryFilter += " AND";
            }


            sqlParameters.Add(new SqlParameter("@WhereClause", ""));
            switch (dataState)
            {
                case DataState.Active:
                    sqlParameters[sqlParameters.Count - 1].Value = queryFilter + " m.[Inativo]='false'";
                    break;
                case DataState.Inactive:
                    sqlParameters[sqlParameters.Count - 1].Value = queryFilter + " m.[Inativo]='true'";
                    break;
                case DataState.All:
                    sqlParameters[sqlParameters.Count - 1].Value = queryFilter;
                    break;
                case DataState.None:
                    sqlParameters[sqlParameters.Count - 1].Value = queryFilter + " m.[Inativo]='false' AND MapTo IS NULL";
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
                    return productsMatching;  //lista vazia
                }

                while (sqlDataReader.Read())
                {
                    productsMatching.Add(Deserialize(ref sqlDataReader));

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
            foreach (ProductMatching productMatching in productsMatching)
            {
                if ( productMatching.MetaInfo["Product.Code"] != DBNull.Value && (string)productMatching.MetaInfo["Product.Code"] != "")
                {
                    if (setProductToLevel > 0)
	                {
		                 productMatching.MapTo = engine.Products.Get((string)(productMatching.MetaInfo["Product.Code"]), dataState, 0);
	                }
                    else
	                {
                        productMatching.MapTo = new Product
                        {
                            Code = (string)(productMatching.MetaInfo["Product.Code"]),
                            Description = (string)(productMatching.MetaInfo["Product.Description"])
                        };
	                }
                }


                if (string.IsNullOrEmpty(productMatching.MetaInfo["Stock.Code"].ToString()) && productMatching.MetaInfo["Stock.Code"].ToString() != "")
                {
                    if (setProductToLevel > 0)
                    {
                        productMatching.ReplacementStock = engine.Stocks.Get((string)(productMatching.MetaInfo["Stock.Code"]), dataState);
                    }
                    else
                    {
                        productMatching.ReplacementStock = new Stock
                        {
                            Code = (string)(productMatching.MetaInfo["Stock.Code"]),
                            Description = (string)(productMatching.MetaInfo["Stock.Description"])
                        };
                    }

                }


                productMatching.Supplier = new Supplier
                {
                    Code = (string)(productMatching.MetaInfo["Supplier.Code"]),
                    Name = (string)(productMatching.MetaInfo["Supplier.Name"])
                };

            }


            return productsMatching;
        }


        public List<ProductMatching> Get(DataState dataState, int setSupplierToLevel, int setProductToLevel, int setStockToLevel)
        {

            List<ProductMatching> productsMatching;
            SqlDataReader sqlDataReader;
            List<SqlParameter> sqlParameters;
            bool connectionOn = !engine.SqlServer.IsConnectionOpen();

            productsMatching = new List<ProductMatching>();
            sqlParameters = new List<SqlParameter>();

            sqlParameters.Add(new SqlParameter("@WhereClause", ""));
            switch (dataState)
            {
                case DataState.Active:
                    sqlParameters[sqlParameters.Count - 1].Value = "m.[Inativo]='false'";
                    break;
                case DataState.Inactive:
                    sqlParameters[sqlParameters.Count - 1].Value = "m.[Inativo]='true'";
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
                    return productsMatching;  //lista vazia
                }

                while (sqlDataReader.Read())
                {
                    productsMatching.Add(Deserialize(ref sqlDataReader));
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
            foreach (ProductMatching productMatching in productsMatching)
            {
                if ( productMatching.MetaInfo["Product.Code"] != DBNull.Value && (string)productMatching.MetaInfo["Product.Code"] != "")
                {
                    if (setProductToLevel > 0)
	                {
		                 productMatching.MapTo = engine.Products.Get((string)(productMatching.MetaInfo["Product.Code"]), dataState, 0);
	                }
                    else
	                {
                        productMatching.MapTo = new Product
                        {
                            Code = (string)(productMatching.MetaInfo["Product.Code"]),
                            Description = (string)(productMatching.MetaInfo["Product.Description"])
                        };
	                }
                }
          
            
                if (string.IsNullOrEmpty(productMatching.MetaInfo["Stock.Code"].ToString()))
                {
                    if (setProductToLevel > 0)
                    {
                        productMatching.ReplacementStock = engine.Stocks.Get((string)(productMatching.MetaInfo["Stock.Code"]), dataState);
                    }
                    else
                    {
                        productMatching.MapTo = new Product
                        {
                            Code = (string)(productMatching.MetaInfo["Stock.Code"]),
                            Description = (string)(productMatching.MetaInfo["Stock.Description"])
                        };
                    }

                }


                productMatching.Supplier = new Supplier
                {
                    Code = (string)(productMatching.MetaInfo["Supplier.Code"]),
                    Name = (string)(productMatching.MetaInfo["Supplier.Name"])
                };

            }
            return productsMatching;
        }
    }
}
