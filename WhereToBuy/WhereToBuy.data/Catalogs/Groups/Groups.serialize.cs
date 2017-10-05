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
    public partial class Groups
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sqlDataReader"></param>
        /// <returns></returns>
        Group Deserialize(ref SqlDataReader sqlDataReader)
        {
            Group group = new Group();

            group.Code = ((string)sqlDataReader["Codigo"]).TrimEnd();
            group.Description = ((string)sqlDataReader["Descricao"]).TrimEnd();
            group.Inactive = (bool)sqlDataReader["Inativo"];
            group.Creation = (DateTime)sqlDataReader["Criacao"];
            group.Version = (DateTime)sqlDataReader["Versao"];
            group.EditionMode = true;
            return group;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="group"></param>
        /// <param name="sqlOperationType"></param>
        /// <returns></returns>
        List<SqlParameter> Serialize(Group group, SqlOperationType sqlOperationType)
        {

            List<SqlParameter> sqlParameters = new List<SqlParameter>();

            switch (sqlOperationType)
            {
                case SqlOperationType.Insert:
                    sqlParameters.Add(new SqlParameter("@Codigo", SQLStrings.CleanDangerousText(group.Code).ToUpper()));
                    sqlParameters.Add(new SqlParameter("@Descricao", SQLStrings.CleanDangerousText(group.Description)));
                    sqlParameters.Add(new SqlParameter("@Inativo", group.Inactive));
                    break;

                case SqlOperationType.Update:
                    sqlParameters.Add(new SqlParameter("@Codigo", SQLStrings.CleanDangerousText(group.Code)));
                    sqlParameters.Add(new SqlParameter("@Descricao", SQLStrings.CleanDangerousText(group.Description)));
                    sqlParameters.Add(new SqlParameter("@Inativo", group.Inactive));
                    sqlParameters.Add(new SqlParameter("@Versao", group.Version));
                    break;

                case SqlOperationType.Delete:
                    sqlParameters.Add(new SqlParameter("@Codigo", SQLStrings.CleanDangerousText(group.Code)));
                    sqlParameters.Add(new SqlParameter("@Versao", group.Version));
                    break;

                default:
                    throw new MyException(_namespace, _className, "Serialize()", string.Format("{0}!", GlobalVariables.Resource.GetString("ForeseenEnumeratorString", GlobalVariables.Culture).ToLower()));
            }

            return sqlParameters;
        }

    }
}
