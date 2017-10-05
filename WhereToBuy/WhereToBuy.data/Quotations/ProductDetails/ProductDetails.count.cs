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
    public partial class ProductDetails
    {
        string _procedureCountName = "ProductDetailCount";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataState"></param>
        /// <returns></returns>
        public int Count(DataState dataState)
        {

            List<SqlParameter> sqlParameters = new List<SqlParameter>();

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
                    sqlParameters[sqlParameters.Count - 1].Value = "";
                    break;
                default:
                    throw new MyException(_namespace, _className, "Count()", string.Format("{0}!", GlobalVariables.Resource.GetString("ForeseenEnumeratorString", GlobalVariables.Culture).ToLower()));
            }

            return Count(ref sqlParameters);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="productCode"></param>
        /// <param name="dataState"></param>
        /// <returns></returns>
        public int Count(string productCode, string supplierCode, DataState dataState)
        {

            List<SqlParameter> sqlParameters = new List<SqlParameter>();

            sqlParameters.Add(new SqlParameter("@WhereClause", ""));
            switch (dataState)
            {
                case DataState.Active:
                    sqlParameters[sqlParameters.Count - 1].Value = string.Format("[ProdutoCodigo]='{0}' AND [FornecedorCodigo]='{1}' AND [Inativo]='false'", SQLStrings.CleanDangerousText(productCode), SQLStrings.CleanDangerousText(supplierCode));
                    break;
                case DataState.Inactive:
                    sqlParameters[sqlParameters.Count - 1].Value = string.Format("[ProdutoCodigo]='{0}' AND [FornecedorCodigo]='{1}' AND [Inativo]='true'", SQLStrings.CleanDangerousText(productCode), SQLStrings.CleanDangerousText(supplierCode));
                    break;
                case DataState.All:
                    sqlParameters[sqlParameters.Count - 1].Value = string.Format("[ProdutoCodigo]='{0}' AND [FornecedorCodigo]='{1}'", SQLStrings.CleanDangerousText(productCode), SQLStrings.CleanDangerousText(supplierCode));
                    break;
                default:
                    throw new MyException(_namespace, _className, "Count()", string.Format("{0}!", GlobalVariables.Resource.GetString("ForeseenEnumeratorString", GlobalVariables.Culture).ToLower()));
            }

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
