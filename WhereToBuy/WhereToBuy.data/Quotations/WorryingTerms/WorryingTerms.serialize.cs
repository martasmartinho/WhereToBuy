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
    public partial class WorryingTerms
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sqlDataReader"></param>
        /// <returns></returns>
        WorryingTerm Deserialize(ref SqlDataReader sqlDataReader)
        {
            WorryingTerm worryingTerm = new WorryingTerm();

            worryingTerm.Term = ((string)sqlDataReader["Termo"]).TrimEnd();
            worryingTerm.Index = ((byte)sqlDataReader["Indice"]);
            worryingTerm.Notes = (Convert.ToString(sqlDataReader["Notas"])).TrimEnd();
            worryingTerm.Inactive = (bool)sqlDataReader["Inativo"];
            worryingTerm.Creation = (DateTime)sqlDataReader["Criacao"];
            worryingTerm.Version = (DateTime)sqlDataReader["Versao"];
            worryingTerm.EditionMode = true;
            return worryingTerm;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="worryingTerm"></param>
        /// <param name="sqlOperationType"></param>
        /// <returns></returns>
        List<SqlParameter> Serialize(WorryingTerm worryingTerm, SqlOperationType sqlOperationType)
        {

            List<SqlParameter> sqlParameters = new List<SqlParameter>();

            switch (sqlOperationType)
            {
                case SqlOperationType.Insert:
                    sqlParameters.Add(new SqlParameter("@Termo", worryingTerm.Term));
                    sqlParameters.Add(new SqlParameter("@Indice", worryingTerm.Index));
                    sqlParameters.Add(new SqlParameter("@Notas", SQLStrings.CleanDangerousText(string.Format("{0}", worryingTerm.Notes))));
                    sqlParameters.Add(new SqlParameter("@Inativo", worryingTerm.Inactive));
                    break;

                case SqlOperationType.Update:
                   sqlParameters.Add(new SqlParameter("@Termo", SQLStrings.CleanDangerousText(worryingTerm.Term).ToUpper()));
                    sqlParameters.Add(new SqlParameter("@Indice", worryingTerm.Index));
                    sqlParameters.Add(new SqlParameter("@Notas", SQLStrings.CleanDangerousText(string.Format("{0}", worryingTerm.Notes))));
                    sqlParameters.Add(new SqlParameter("@Inativo", worryingTerm.Inactive));
                    sqlParameters.Add(new SqlParameter("@Versao", worryingTerm.Version));
                    break;

                case SqlOperationType.Delete:
                    sqlParameters.Add(new SqlParameter("@Termo", SQLStrings.CleanDangerousText(worryingTerm.Term)));
                    sqlParameters.Add(new SqlParameter("@Versao", worryingTerm.Version));
                    break;

                default:
                    throw new MyException(_namespace, _className, "Serialize()", string.Format("{0}!", GlobalVariables.Resource.GetString("ForeseenEnumeratorString", GlobalVariables.Culture).ToLower()));
            }

            return sqlParameters;
        }
    }
}
