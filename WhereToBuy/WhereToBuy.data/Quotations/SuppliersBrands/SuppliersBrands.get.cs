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
    public partial class SuppliersBrands
    {
        string _namespace = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
        string _className = "SuppliersBrands";
        string _procedureGetName = "SupplierBrandSelect";


        DataEngine engine;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="engine"></param>
        public SuppliersBrands(DataEngine engine)
        {
            this.engine = engine;
        }


        public SupplierBrand Get(Supplier supplier, Brand brand)
        {

            SupplierBrand supplierBrand;
            SqlDataReader sqlDataReader;
            List<SqlParameter> sqlParameters;
            bool connectionOn = !engine.SqlServer.IsConnectionOpen();

            sqlParameters = new List<SqlParameter>();

            sqlParameters.Add(new SqlParameter("@WhereClause", ""));

            sqlParameters[sqlParameters.Count - 1].Value = string.Format("[FornecedorCodigo]='{0}' AND [MarcaCodigo]='{1}' ", SQLStrings.CleanDangerousText(supplier.Code), SQLStrings.CleanDangerousText(brand.Code));

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
                        string.Format("{0} ({1} - {2})", GlobalVariables.Resource.GetString("RecordNotFoundString", GlobalVariables.Culture), SQLStrings.CleanDangerousText(supplier.Code), SQLStrings.CleanDangerousText(brand.Code)));
                }

                sqlDataReader.Read();
                supplierBrand = this.Deserialize(ref sqlDataReader);
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
            supplierBrand.Supplier = supplier;
            supplierBrand.Brand = brand;


            return supplierBrand;

        }


        public SupplierBrand Get(string supplierCode, string brandCode, int setSupplierToLevel, int setBrandToLevel)
        {

            SupplierBrand supplierBrand;
            SqlDataReader sqlDataReader;
            List<SqlParameter> sqlParameters;
            bool connectionOn = !engine.SqlServer.IsConnectionOpen();

            sqlParameters = new List<SqlParameter>();

            sqlParameters.Add(new SqlParameter("@WhereClause", ""));

            sqlParameters[sqlParameters.Count - 1].Value = string.Format("[FornecedorCodigo]='{0}' AND [MarcaCodigo]='{1}'", SQLStrings.CleanDangerousText(supplierCode), SQLStrings.CleanDangerousText(brandCode));

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
                        string.Format("{0} ({1} - {2}) ", GlobalVariables.Resource.GetString("RecordNotFoundString", GlobalVariables.Culture), SQLStrings.CleanDangerousText(supplierCode), SQLStrings.CleanDangerousText(brandCode)));
                }

                sqlDataReader.Read();
                supplierBrand = this.Deserialize(ref sqlDataReader);
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
                supplierBrand.Brand = engine.Brands.Get((string)(supplierBrand.MetaInfo["Brand.Code"]), DataState.Active);

            }
            else
            {
                supplierBrand.Brand = new Brand
                {
                    Code = (string)(supplierBrand.MetaInfo["Brand.Code"]),
                    Description = (string)(supplierBrand.MetaInfo["Brand.Description"])
                };
            }


            if (setSupplierToLevel > 0)
            {
                supplierBrand.Supplier = engine.Suppliers.Get((string)(supplierBrand.MetaInfo["Supplier.Code"]), DataState.Active);
            }
            else
            {
                supplierBrand.Supplier = new Supplier
                {
                    Code = (string)(supplierBrand.MetaInfo["Supplier.Code"]),
                    Name = (string)(supplierBrand.MetaInfo["Supplier.Name"])
                };
            }


            return supplierBrand;

        }



        public List<SupplierBrand> Get(int setSupplierToLevel, int setBrandToLevel)
        {

            List<SupplierBrand> suppliersBrands;
            SqlDataReader sqlDataReader;
            List<SqlParameter> sqlParameters;
            bool connectionOn = !engine.SqlServer.IsConnectionOpen();

            suppliersBrands = new List<SupplierBrand>();
            sqlParameters = new List<SqlParameter>();

            sqlParameters.Add(new SqlParameter("@WhereClause", ""));
            

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
                    return suppliersBrands;  //lista vazia
                }

                while (sqlDataReader.Read())
                {
                    suppliersBrands.Add(Deserialize(ref sqlDataReader));
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

                foreach (SupplierBrand supplierBrand in suppliersBrands)
                {
                    supplierBrand.Brand = engine.Brands.Get((string)(supplierBrand.MetaInfo["Brand.Code"]), DataState.Active);
                }

            }
            else
            {
                foreach (SupplierBrand supplierBrand in suppliersBrands)
                {
                    supplierBrand.Brand = new Brand
                    {
                        Code = (string)(supplierBrand.MetaInfo["Brand.Code"]),
                        Description = (string)(supplierBrand.MetaInfo["Brand.Description"])
                    };
                }
            }


            if (setSupplierToLevel > 0)
            {

                foreach (SupplierBrand supplierBrand in suppliersBrands)
                {
                    supplierBrand.Supplier = engine.Suppliers.Get((string)(supplierBrand.MetaInfo["Supplier.Code"]), DataState.Active);
                }

            }
            else
            {
                foreach (SupplierBrand supplierBrand in suppliersBrands)
                {

                    supplierBrand.Supplier = new Supplier
                    {
                        Code = (string)(supplierBrand.MetaInfo["Supplier.Code"]),
                        Name = (string)(supplierBrand.MetaInfo["Supplier.Name"])
                    };
                }
            }





            return suppliersBrands;
        }

        public List<SupplierBrand> Get(Supplier supplier, int setBrandToLevel)
        {

            List<SupplierBrand> suppliersBrands;
            SqlDataReader sqlDataReader;
            List<SqlParameter> sqlParameters;
            bool connectionOn = !engine.SqlServer.IsConnectionOpen();

            suppliersBrands = new List<SupplierBrand>();
            sqlParameters = new List<SqlParameter>();

            sqlParameters.Add(new SqlParameter("@WhereClause", ""));

            sqlParameters[sqlParameters.Count - 1].Value = string.Format("[FornecedorCodigo]='{0}'", SQLStrings.CleanDangerousText(supplier.Code));

            sqlParameters.Add(new SqlParameter("@OrderByClause", "[MarcaDescricao] ASC"));

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
                    return suppliersBrands;  //lista vazia
                }

                while (sqlDataReader.Read())
                {
                    suppliersBrands.Add(Deserialize(ref sqlDataReader));
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

                foreach (SupplierBrand supplierBrand in suppliersBrands)
                {
                    supplierBrand.Supplier = supplier;
                    supplierBrand.Brand = engine.Brands.Get((string)(supplierBrand.MetaInfo["Brand.Code"]), DataState.Active);
                }

            }
            else
            {
                foreach (SupplierBrand supplierBrand in suppliersBrands)
                {
                    supplierBrand.Supplier = supplier;
                    supplierBrand.Brand = new Brand
                    {
                        Code = (string)(supplierBrand.MetaInfo["Brand.Code"]),
                        Description = (string)(supplierBrand.MetaInfo["Brand.Description"])
                    };
                }
            }

        


            return suppliersBrands;
        }
    }
}
