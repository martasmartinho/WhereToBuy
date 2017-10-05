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
    /// <summary>
    /// 
    /// </summary>
    public partial class Languages
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sqlDataReader"></param>
        /// <returns></returns>
        Language Deserialize(ref SqlDataReader sqlDataReader)
        {
            Language language = new Language();

            language.Code = ((string)sqlDataReader["Codigo"]).TrimEnd();
            language.Description = ((string)sqlDataReader["Descricao"]).TrimEnd();
            language.Inactive = (bool)sqlDataReader["Inativo"];
            language.Creation = (DateTime)sqlDataReader["Criacao"];
            language.Version = (DateTime)sqlDataReader["Versao"];
            language.EditionMode = true;
            return language;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="language"></param>
        /// <param name="sqlOperationType"></param>
        /// <returns></returns>
        List<SqlParameter> Serialize(Language language, SqlOperationType sqlOperationType)
        {

            List<SqlParameter> sqlParameters = new List<SqlParameter>();

            switch (sqlOperationType)
            {
                case SqlOperationType.Insert:
                    sqlParameters.Add(new SqlParameter("@Codigo", SQLStrings.CleanDangerousText(language.Code)));
                    sqlParameters.Add(new SqlParameter("@Descricao", SQLStrings.CleanDangerousText(language.Description)));
                    sqlParameters.Add(new SqlParameter("@Inativo", language.Inactive));
                    break;

                case SqlOperationType.Update:
                    sqlParameters.Add(new SqlParameter("@Codigo", SQLStrings.CleanDangerousText(language.Code)));
                    sqlParameters.Add(new SqlParameter("@Descricao", SQLStrings.CleanDangerousText(language.Description)));
                    sqlParameters.Add(new SqlParameter("@Inativo", language.Inactive));
                    sqlParameters.Add(new SqlParameter("@Versao", language.Version));
                    break;

                case SqlOperationType.Delete:
                    sqlParameters.Add(new SqlParameter("@Codigo", SQLStrings.CleanDangerousText(language.Code)));
                    sqlParameters.Add(new SqlParameter("@Versao", language.Version));
                    break;

                default:
                    throw new MyException(_namespace, _className, "Serialize()", string.Format("{0}!", GlobalVariables.Resource.GetString("ForeseenEnumeratorString", GlobalVariables.Culture).ToLower()));
            }

            return sqlParameters;
        }
    }
}
