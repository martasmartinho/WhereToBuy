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
    public partial class WarningTypes
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sqlDataReader"></param>
        /// <returns></returns>
        WarningType Deserialize(ref SqlDataReader sqlDataReader)
        {
            WarningType warningType = new WarningType();

            warningType.Code = ((string)sqlDataReader["Codigo"]).TrimEnd();
            warningType.Description = ((string)sqlDataReader["Descricao"]).TrimEnd();
            warningType.Severity = ((short)sqlDataReader["Gravidade"]);
            warningType.Notes = (Convert.ToString(sqlDataReader["Notas"])).TrimEnd();
            warningType.Icon = (Convert.ToString(sqlDataReader["Icon"])).TrimEnd();
            warningType.Inactive = (bool)sqlDataReader["Inativo"];
            warningType.Creation = (DateTime)sqlDataReader["Criacao"];
            warningType.Version = (DateTime)sqlDataReader["Versao"];
            warningType.EditionMode = true;
            return warningType;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="warningType"></param>
        /// <param name="sqlOperationType"></param>
        /// <returns></returns>
        List<SqlParameter> Serialize(WarningType warningType, SqlOperationType sqlOperationType)
        {

            List<SqlParameter> sqlParameters = new List<SqlParameter>();

            switch (sqlOperationType)
            {
                case SqlOperationType.Insert:
                    sqlParameters.Add(new SqlParameter("@Codigo", SQLStrings.CleanDangerousText(warningType.Code).ToUpper()));
                    sqlParameters.Add(new SqlParameter("@Descricao", SQLStrings.CleanDangerousText(warningType.Description)));
                    sqlParameters.Add(new SqlParameter("@Gravidade", warningType.Severity));
                    sqlParameters.Add(new SqlParameter("@Notas", SQLStrings.CleanDangerousText(string.Format("{0}", warningType.Notes))));
                    sqlParameters.Add(new SqlParameter("@Icon", SQLStrings.CleanDangerousText(string.Format("{0}", warningType.Icon))));
                    sqlParameters.Add(new SqlParameter("@Inativo", warningType.Inactive));
                    break;

                case SqlOperationType.Update:
                    sqlParameters.Add(new SqlParameter("@Codigo", SQLStrings.CleanDangerousText(warningType.Code)));
                    sqlParameters.Add(new SqlParameter("@Descricao", SQLStrings.CleanDangerousText(warningType.Description)));
                    sqlParameters.Add(new SqlParameter("@Gravidade", warningType.Severity));
                    sqlParameters.Add(new SqlParameter("@Notas", SQLStrings.CleanDangerousText(string.Format("{0}", warningType.Notes))));
                    sqlParameters.Add(new SqlParameter("@Icon", SQLStrings.CleanDangerousText(string.Format("{0}", warningType.Icon))));
                    sqlParameters.Add(new SqlParameter("@Inativo", warningType.Inactive));
                    sqlParameters.Add(new SqlParameter("@Versao", warningType.Version));
                    break;

                case SqlOperationType.Delete:
                    sqlParameters.Add(new SqlParameter("@Codigo", SQLStrings.CleanDangerousText(warningType.Code)));
                    sqlParameters.Add(new SqlParameter("@Versao", warningType.Version));
                    break;

                default:
                    throw new MyException(_namespace, _className, "Serialize()", string.Format("{0}!", GlobalVariables.Resource.GetString("ForeseenEnumeratorString", GlobalVariables.Culture).ToLower()));
            }

            return sqlParameters;
        }
    }
}
