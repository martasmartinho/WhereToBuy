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
    public partial class QuotationWarnings
    {
        string _namespace = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
        string _className = "QuotationWarnings";
        string _procedureGetName = "QuotationWarningSelect";


        DataEngine engine;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="engine"></param>
        public QuotationWarnings(DataEngine engine)
        {
            this.engine = engine;
        }

        public List<QuotationWarning> Get(Supplier supplier, WarningType warningType, string orderBy, int setTypeToLevel)
        {

            List<QuotationWarning> warnings;
            SqlDataReader sqlDataReader;
            List<SqlParameter> sqlParameters;
            bool connectionOn = !engine.SqlServer.IsConnectionOpen();
            string queryFilter = string.Empty;

            warnings = new List<QuotationWarning>();
            sqlParameters = new List<SqlParameter>();

            if (supplier != null)
            {
                queryFilter = string.Format("[FornecedorCodigo]='{0}'", SQLStrings.CleanDangerousText(supplier.Code));
            }

            if (warningType!=null)
            {
                if (queryFilter != string.Empty)
                {
                    queryFilter += " AND";
                }

                queryFilter += string.Format(" [AvisoTipoCodigo]='{0}'", SQLStrings.CleanDangerousText(warningType.Code));
            }


            sqlParameters.Add(new SqlParameter("@WhereClause", queryFilter));

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
                    return warnings;  //lista vazia
                }

                while (sqlDataReader.Read())
                {
                    warnings.Add(Deserialize(ref sqlDataReader));

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
            foreach (QuotationWarning warning in warnings)
            {
                if (warning.MetaInfo["WarningType.Code"].ToString() != DBNull.Value.ToString() && warning.MetaInfo["WarningType.Code"].ToString() != "")
                {
                    if (setTypeToLevel > 0)
                    {
                        warning.WarningType = engine.WarningTypes.Get((string)(warning.MetaInfo["WarningType.Code"]), DataState.All);
                    }
                    else
                    {

                        warning.WarningType = new WarningType
                        {
                            Code = (string)(warning.MetaInfo["WarningType.Code"]),
                            Description = (string)(warning.MetaInfo["WarningType.Description"]),
                            Severity = (short)(warning.MetaInfo["WarningType.Severity"])
                        };
                    }
                }
                
                warning.Supplier = new Supplier
                {
                    Code = (string)(warning.MetaInfo["Supplier.Code"]),
                    Name = (string)(warning.MetaInfo["Supplier.Name"])
                };
            }

            return warnings;
        }

    }
}
