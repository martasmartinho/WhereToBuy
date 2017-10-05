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
    public partial class TaxesMatching
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sqlDataReader"></param>
        /// <returns></returns>
        TaxMatching Deserialize(ref SqlDataReader sqlDataReader)
        {
            TaxMatching taxMatching = new TaxMatching();

            taxMatching.Code = ((string)sqlDataReader["Codigo"]).TrimEnd();
            taxMatching.Description = ((string)sqlDataReader["Descricao"]).TrimEnd();


            taxMatching.MetaInfo = new Dictionary<string, object>();
            taxMatching.MetaInfo.Add("Supplier.Code", (object)sqlDataReader["FornecedorCodigo"]);
            taxMatching.MetaInfo.Add("Supplier.Name", (object)sqlDataReader["FornecedorNome"]);
            taxMatching.MetaInfo.Add("Tax.Code", (object)sqlDataReader["MapTo"].ToString());

            taxMatching.Inactive = (bool)sqlDataReader["Inativo"];
            taxMatching.Creation = (DateTime)sqlDataReader["Criacao"];
            taxMatching.Version = (DateTime)sqlDataReader["Versao"];
            taxMatching.EditionMode = true;

            return taxMatching;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="taxMatching"></param>
        /// <param name="sqlOperationType"></param>
        /// <returns></returns>
        List<SqlParameter> Serialize(TaxMatching taxMatching, SqlOperationType sqlOperationType)
        {

            List<SqlParameter> sqlParameters = new List<SqlParameter>();

            switch (sqlOperationType)
            {
                case SqlOperationType.Insert:
                    sqlParameters.Add(new SqlParameter("@FornecedorCodigo", SQLStrings.CleanDangerousText(taxMatching.Supplier.Code).ToUpper()));
                    sqlParameters.Add(new SqlParameter("@Codigo", SQLStrings.CleanDangerousText(taxMatching.Code).ToUpper()));
                    sqlParameters.Add(new SqlParameter("@Descricao", SQLStrings.CleanDangerousText(taxMatching.Description)));
                    if (taxMatching.MapTo != null)
                    {
                        sqlParameters.Add(new SqlParameter("@MapTo", SQLStrings.CleanDangerousText(taxMatching.MapTo.Code)));
                    }
                    else
                    {
                        sqlParameters.Add(new SqlParameter("@MapTo", DBNull.Value));
                    }

                    sqlParameters.Add(new SqlParameter("@Inativo", taxMatching.Inactive));
                    break;

                case SqlOperationType.Update:
                    sqlParameters.Add(new SqlParameter("@FornecedorCodigo", SQLStrings.CleanDangerousText(taxMatching.Supplier.Code).ToUpper()));
                    sqlParameters.Add(new SqlParameter("@Codigo", SQLStrings.CleanDangerousText(taxMatching.Code).ToUpper()));
                    sqlParameters.Add(new SqlParameter("@Descricao", SQLStrings.CleanDangerousText(taxMatching.Description)));
                    if (taxMatching.MapTo != null)
                    {
                        sqlParameters.Add(new SqlParameter("@MapTo", SQLStrings.CleanDangerousText(taxMatching.MapTo.Code)));
                    }
                    else
                    {
                        sqlParameters.Add(new SqlParameter("@MapTo", DBNull.Value));
                    }
                    sqlParameters.Add(new SqlParameter("@Inativo", taxMatching.Inactive));
                    sqlParameters.Add(new SqlParameter("@Versao", taxMatching.Version));
                    break;

                case SqlOperationType.Delete:
                    sqlParameters.Add(new SqlParameter("@FornecedorCodigo", SQLStrings.CleanDangerousText(taxMatching.Supplier.Code).ToUpper()));
                    sqlParameters.Add(new SqlParameter("@Codigo", SQLStrings.CleanDangerousText(taxMatching.Code)));
                    sqlParameters.Add(new SqlParameter("@Versao", taxMatching.Version));
                    break;

                default:
                    throw new MyException(_namespace, _className, "Serialize()", string.Format("{0}!", GlobalVariables.Resource.GetString("ForeseenEnumeratorString", GlobalVariables.Culture).ToLower()));
            }

            return sqlParameters;
        }
    }
}
