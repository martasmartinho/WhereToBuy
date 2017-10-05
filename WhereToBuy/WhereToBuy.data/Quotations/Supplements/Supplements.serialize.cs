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
    public partial class Supplements
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sqlDataReader"></param>
        /// <returns></returns>
        Supplement Deserialize(ref SqlDataReader sqlDataReader)
        {
            Supplement supplement = new Supplement();

            supplement.Code = ((string)sqlDataReader["Codigo"]).TrimEnd();
            supplement.Description = ((string)sqlDataReader["Descricao"]).TrimEnd();
            supplement.TextToAdd = (sqlDataReader["TermoAcrescentar"].ToString()).TrimEnd();
            supplement.TextToRemove = (sqlDataReader["TermosRemover"].ToString()).TrimEnd();
            supplement.Inactive = (bool)sqlDataReader["Inativo"];
            supplement.Creation = (DateTime)sqlDataReader["Criacao"];
            supplement.Version = (DateTime)sqlDataReader["Versao"];
            supplement.EditionMode = true;
            return supplement;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="supplement"></param>
        /// <param name="sqlOperationType"></param>
        /// <returns></returns>
        List<SqlParameter> Serialize(Supplement supplement, SqlOperationType sqlOperationType)
        {

            List<SqlParameter> sqlParameters = new List<SqlParameter>();

            switch (sqlOperationType)
            {
                case SqlOperationType.Insert:
                    sqlParameters.Add(new SqlParameter("@Codigo", SQLStrings.CleanDangerousText(supplement.Code).ToUpper()));
                    sqlParameters.Add(new SqlParameter("@Descricao", SQLStrings.CleanDangerousText(supplement.Description)));
                    sqlParameters.Add(new SqlParameter("@TermoAcrescentar", SQLStrings.CleanDangerousText(string.Format("{0}", supplement.TextToAdd))));
                    sqlParameters.Add(new SqlParameter("@TermosRemover", SQLStrings.CleanDangerousText(string.Format("{0}", supplement.TextToRemove))));
                    sqlParameters.Add(new SqlParameter("@Inativo", supplement.Inactive));
                    break;

                case SqlOperationType.Update:
                    sqlParameters.Add(new SqlParameter("@Codigo", SQLStrings.CleanDangerousText(supplement.Code)));
                    sqlParameters.Add(new SqlParameter("@Descricao", SQLStrings.CleanDangerousText(supplement.Description)));
                    sqlParameters.Add(new SqlParameter("@TermoAcrescentar", SQLStrings.CleanDangerousText(string.Format("{0}", supplement.TextToAdd))));
                    sqlParameters.Add(new SqlParameter("@TermosRemover", SQLStrings.CleanDangerousText(string.Format("{0}", supplement.TextToRemove))));
                    sqlParameters.Add(new SqlParameter("@Inativo", supplement.Inactive));
                    sqlParameters.Add(new SqlParameter("@Versao", supplement.Version));
                    break;

                case SqlOperationType.Delete:
                    sqlParameters.Add(new SqlParameter("@Codigo", SQLStrings.CleanDangerousText(supplement.Code)));
                    sqlParameters.Add(new SqlParameter("@Versao", supplement.Version));
                    break;

                default:
                    throw new MyException(_namespace, _className, "Serialize()", string.Format("{0}!", GlobalVariables.Resource.GetString("ForeseenEnumeratorString", GlobalVariables.Culture).ToLower()));
            }

            return sqlParameters;
        }
    }
}
