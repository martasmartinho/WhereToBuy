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
    public partial class SupplementsMatching
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sqlDataReader"></param>
        /// <returns></returns>
        SupplementMatching Deserialize(ref SqlDataReader sqlDataReader)
        {
            SupplementMatching supplementMatching = new SupplementMatching();

            supplementMatching.Code = ((string)sqlDataReader["Codigo"]).TrimEnd();
            supplementMatching.Description = ((string)sqlDataReader["Descricao"]).TrimEnd();


            supplementMatching.MetaInfo = new Dictionary<string, object>();
            supplementMatching.MetaInfo.Add("Supplier.Code", (object)sqlDataReader["FornecedorCodigo"]);
            supplementMatching.MetaInfo.Add("Supplier.Name", (object)sqlDataReader["FornecedorNome"]);
            supplementMatching.MetaInfo.Add("Supplement.Code", (object)sqlDataReader["MapTo"].ToString());

            supplementMatching.Inactive = (bool)sqlDataReader["Inativo"];
            supplementMatching.Creation = (DateTime)sqlDataReader["Criacao"];
            supplementMatching.Version = (DateTime)sqlDataReader["Versao"];
            supplementMatching.EditionMode = true;

            return supplementMatching;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="SupplementMatching"></param>
        /// <param name="sqlOperationType"></param>
        /// <returns></returns>
        List<SqlParameter> Serialize(SupplementMatching supplementMatching, SqlOperationType sqlOperationType)
        {

            List<SqlParameter> sqlParameters = new List<SqlParameter>();

            switch (sqlOperationType)
            {
                case SqlOperationType.Insert:
                    sqlParameters.Add(new SqlParameter("@FornecedorCodigo", SQLStrings.CleanDangerousText(supplementMatching.Supplier.Code).ToUpper()));
                    sqlParameters.Add(new SqlParameter("@Codigo", SQLStrings.CleanDangerousText(supplementMatching.Code).ToUpper()));
                    sqlParameters.Add(new SqlParameter("@Descricao", SQLStrings.CleanDangerousText(supplementMatching.Description)));
                    if (supplementMatching.MapTo != null)
                    {
                        sqlParameters.Add(new SqlParameter("@MapTo", SQLStrings.CleanDangerousText(supplementMatching.MapTo.Code)));
                    }
                    else
                    {
                        sqlParameters.Add(new SqlParameter("@MapTo", DBNull.Value));
                    }

                    sqlParameters.Add(new SqlParameter("@Inativo", supplementMatching.Inactive));
                    break;

                case SqlOperationType.Update:
                    sqlParameters.Add(new SqlParameter("@FornecedorCodigo", SQLStrings.CleanDangerousText(supplementMatching.Supplier.Code).ToUpper()));
                    sqlParameters.Add(new SqlParameter("@Codigo", SQLStrings.CleanDangerousText(supplementMatching.Code).ToUpper()));
                    sqlParameters.Add(new SqlParameter("@Descricao", SQLStrings.CleanDangerousText(supplementMatching.Description)));
                    if (supplementMatching.MapTo != null)
                    {
                        sqlParameters.Add(new SqlParameter("@MapTo", SQLStrings.CleanDangerousText(supplementMatching.MapTo.Code)));
                    }
                    else
                    {
                        sqlParameters.Add(new SqlParameter("@MapTo", DBNull.Value));
                    }
                    sqlParameters.Add(new SqlParameter("@Inativo", supplementMatching.Inactive));
                    sqlParameters.Add(new SqlParameter("@Versao", supplementMatching.Version));
                    break;

                case SqlOperationType.Delete:
                    sqlParameters.Add(new SqlParameter("@FornecedorCodigo", SQLStrings.CleanDangerousText(supplementMatching.Supplier.Code).ToUpper()));
                    sqlParameters.Add(new SqlParameter("@Codigo", SQLStrings.CleanDangerousText(supplementMatching.Code)));
                    sqlParameters.Add(new SqlParameter("@Versao", supplementMatching.Version));
                    break;

                default:
                    throw new MyException(_namespace, _className, "Serialize()", string.Format("{0}!", GlobalVariables.Resource.GetString("ForeseenEnumeratorString", GlobalVariables.Culture).ToLower()));
            }

            return sqlParameters;
        }
    }
}
