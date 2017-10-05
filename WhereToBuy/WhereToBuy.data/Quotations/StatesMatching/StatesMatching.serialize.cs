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
    public partial class StatesMatching
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sqlDataReader"></param>
        /// <returns></returns>
        StateMatching Deserialize(ref SqlDataReader sqlDataReader)
        {
            StateMatching stateMatching = new StateMatching();

            stateMatching.Code = ((string)sqlDataReader["Codigo"]).TrimEnd();
            stateMatching.Description = ((string)sqlDataReader["Descricao"]).TrimEnd();


            stateMatching.MetaInfo = new Dictionary<string, object>();
            stateMatching.MetaInfo.Add("Supplier.Code", (object)sqlDataReader["FornecedorCodigo"]);
            stateMatching.MetaInfo.Add("Supplier.Name", (object)sqlDataReader["FornecedorNome"]);
            stateMatching.MetaInfo.Add("State.Code", (object)sqlDataReader["MapTo"].ToString());

            stateMatching.Inactive = (bool)sqlDataReader["Inativo"];
            stateMatching.Creation = (DateTime)sqlDataReader["Criacao"];
            stateMatching.Version = (DateTime)sqlDataReader["Versao"];
            stateMatching.EditionMode = true;

            return stateMatching;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stateMatching"></param>
        /// <param name="sqlOperationType"></param>
        /// <returns></returns>
        List<SqlParameter> Serialize(StateMatching stateMatching, SqlOperationType sqlOperationType)
        {

            List<SqlParameter> sqlParameters = new List<SqlParameter>();

            switch (sqlOperationType)
            {
                case SqlOperationType.Insert:
                    sqlParameters.Add(new SqlParameter("@FornecedorCodigo", SQLStrings.CleanDangerousText(stateMatching.Supplier.Code).ToUpper()));
                    sqlParameters.Add(new SqlParameter("@Codigo", SQLStrings.CleanDangerousText(stateMatching.Code).ToUpper()));
                    sqlParameters.Add(new SqlParameter("@Descricao", SQLStrings.CleanDangerousText(stateMatching.Description)));
                    if (stateMatching.MapTo != null)
                    {
                        sqlParameters.Add(new SqlParameter("@MapTo", SQLStrings.CleanDangerousText(stateMatching.MapTo.Code)));
                    }
                    else
                    {
                        sqlParameters.Add(new SqlParameter("@MapTo", DBNull.Value));
                    }

                    sqlParameters.Add(new SqlParameter("@Inativo", stateMatching.Inactive));
                    break;

                case SqlOperationType.Update:
                    sqlParameters.Add(new SqlParameter("@FornecedorCodigo", SQLStrings.CleanDangerousText(stateMatching.Supplier.Code).ToUpper()));
                    sqlParameters.Add(new SqlParameter("@Codigo", SQLStrings.CleanDangerousText(stateMatching.Code).ToUpper()));
                    sqlParameters.Add(new SqlParameter("@Descricao", SQLStrings.CleanDangerousText(stateMatching.Description)));
                    if (stateMatching.MapTo != null)
                    {
                        sqlParameters.Add(new SqlParameter("@MapTo", SQLStrings.CleanDangerousText(stateMatching.MapTo.Code)));
                    }
                    else
                    {
                        sqlParameters.Add(new SqlParameter("@MapTo", DBNull.Value));
                    }
                    sqlParameters.Add(new SqlParameter("@Inativo", stateMatching.Inactive));
                    sqlParameters.Add(new SqlParameter("@Versao", stateMatching.Version));
                    break;

                case SqlOperationType.Delete:
                    sqlParameters.Add(new SqlParameter("@FornecedorCodigo", SQLStrings.CleanDangerousText(stateMatching.Supplier.Code).ToUpper()));
                    sqlParameters.Add(new SqlParameter("@Codigo", SQLStrings.CleanDangerousText(stateMatching.Code)));
                    sqlParameters.Add(new SqlParameter("@Versao", stateMatching.Version));
                    break;

                default:
                    throw new MyException(_namespace, _className, "Serialize()", string.Format("{0}!", GlobalVariables.Resource.GetString("ForeseenEnumeratorString", GlobalVariables.Culture).ToLower()));
            }

            return sqlParameters;
        }
    }
}
