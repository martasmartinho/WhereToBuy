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
        string _procedureCountName = "SupplierBrandCount";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataState"></param>
        /// <returns></returns>
        public int Count()
        {

            List<SqlParameter> sqlParameters = new List<SqlParameter>();

            sqlParameters.Add(new SqlParameter("@WhereClause", ""));

            sqlParameters[sqlParameters.Count - 1].Value = "";
           
            return Count(ref sqlParameters);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="brandCode"></param>
        /// <param name="dataState"></param>
        /// <returns></returns>
        public int Count(string supplierCode, string brandCode)
        {

            List<SqlParameter> sqlParameters = new List<SqlParameter>();

            sqlParameters.Add(new SqlParameter("@WhereClause", ""));
            sqlParameters[sqlParameters.Count - 1].Value = string.Format("[FornecedorCodigo]='{0}' AND [MarcaCodigo]='{1}'", SQLStrings.CleanDangerousText(supplierCode), SQLStrings.CleanDangerousText(brandCode));
           
            return Count(ref sqlParameters);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="brand"></param>
        /// <param name="dataState"></param>
        /// <returns></returns>
        public int Count(Supplier supplier, Brand brand)
        {

            List<SqlParameter> sqlParameters = new List<SqlParameter>();

            sqlParameters.Add(new SqlParameter("@WhereClause", ""));
            sqlParameters[sqlParameters.Count - 1].Value = string.Format("[FornecedorCodigo]='{0}' AND [MarcaCodigo]='{1}'", SQLStrings.CleanDangerousText(supplier.Code), SQLStrings.CleanDangerousText(brand.Code));
            
            return Count(ref sqlParameters);
        }


        int Count(ref List<SqlParameter> sqlParameters)
        {
            int affectedRecords;
            bool connectionOn = !engine.SqlServer.IsConnectionOpen();

            try
            {
                if (connectionOn)
                {
                    engine.SqlServer.OpenConnection();
                }

                affectedRecords = (int)engine.SqlServer.ExecuteScalar(System.Data.CommandType.StoredProcedure, _procedureCountName, false, sqlParameters);

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
                throw new MyException(GlobalVariables.ProjectName, MyException.OriginClassSqlError.SQLStoredProcedure, ex.Procedure, ex.Errors);
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
                throw new MyException(_namespace, _className, "Count()", ex.Message);
            }

            return affectedRecords;
        }
    }
}
