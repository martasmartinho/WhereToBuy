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
    public partial class States
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sqlDataReader"></param>
        /// <returns></returns>
        State Deserialize(ref SqlDataReader sqlDataReader)
        {
            State state = new State();

            state.Code = ((string)sqlDataReader["Codigo"]).TrimEnd();
            state.Description = ((string)sqlDataReader["Descricao"]).TrimEnd();
            state.Inactive = (bool)sqlDataReader["Inativo"];
            state.Creation = (DateTime)sqlDataReader["Criacao"];
            state.Version = (DateTime)sqlDataReader["Versao"];
            state.EditionMode = true;
            return state;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="state"></param>
        /// <param name="sqlOperationType"></param>
        /// <returns></returns>
        List<SqlParameter> Serialize(State state, SqlOperationType sqlOperationType)
        {

            List<SqlParameter> sqlParameters = new List<SqlParameter>();

            switch (sqlOperationType)
            {
                case SqlOperationType.Insert:
                    sqlParameters.Add(new SqlParameter("@Codigo", SQLStrings.CleanDangerousText(state.Code).ToUpper()));
                    sqlParameters.Add(new SqlParameter("@Descricao", SQLStrings.CleanDangerousText(state.Description)));
                    sqlParameters.Add(new SqlParameter("@Inativo", state.Inactive));
                    break;

                case SqlOperationType.Update:
                    sqlParameters.Add(new SqlParameter("@Codigo", SQLStrings.CleanDangerousText(state.Code)));
                    sqlParameters.Add(new SqlParameter("@Descricao", SQLStrings.CleanDangerousText(state.Description)));
                    sqlParameters.Add(new SqlParameter("@Inativo", state.Inactive));
                    sqlParameters.Add(new SqlParameter("@Versao", state.Version));
                    break;

                case SqlOperationType.Delete:
                    sqlParameters.Add(new SqlParameter("@Codigo", SQLStrings.CleanDangerousText(state.Code)));
                    sqlParameters.Add(new SqlParameter("@Versao", state.Version));
                    break;

                default:
                    throw new MyException(_namespace, _className, "Serialize()", string.Format("{0}!", GlobalVariables.Resource.GetString("ForeseenEnumeratorString", GlobalVariables.Culture).ToLower()));
            }

            return sqlParameters;
        }
    }
}
