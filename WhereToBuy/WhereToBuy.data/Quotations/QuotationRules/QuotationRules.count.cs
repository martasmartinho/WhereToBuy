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
    public partial class QuotationRules
    {
        string _procedureCountName = "QuotationRuleCount";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataState"></param>
        /// <returns></returns>
        public int Count()
        {

            List<SqlParameter> sqlParameters = new List<SqlParameter>();

            sqlParameters.Add(new SqlParameter("@WhereClause", ""));
            

            return Count(ref sqlParameters);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <param name="dataState"></param>
        /// <returns></returns>
        public int Count(string supplierCode, string brandCode, string categoryCode, string stockCode)
        {

            List<SqlParameter> sqlParameters = new List<SqlParameter>();

            sqlParameters.Add(new SqlParameter("@WhereClause", ""));
            sqlParameters[sqlParameters.Count - 1].Value = string.Format("[FornecedorCodigo]='{0}' AND [MarcaCodigo]='{1}' "+
                                                                            "AND [CategoriaCodigo]='{2}'" +
                                                                            "AND [StockCodigo]='{3}'",
                                                                            SQLStrings.CleanDangerousText(supplierCode),
                                                                            SQLStrings.CleanDangerousText(brandCode),                
                                                                            SQLStrings.CleanDangerousText(categoryCode),
                                                                            SQLStrings.CleanDangerousText(stockCode));

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
