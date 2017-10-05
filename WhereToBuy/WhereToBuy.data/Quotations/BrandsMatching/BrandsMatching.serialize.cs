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
    public partial class BrandsMatching
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sqlDataReader"></param>
        /// <returns></returns>
        BrandMatching Deserialize(ref SqlDataReader sqlDataReader)
        {
            BrandMatching brandMatching = new BrandMatching();

            brandMatching.Code = ((string)sqlDataReader["Codigo"]).TrimEnd();
            brandMatching.Description = ((string)sqlDataReader["Descricao"]).TrimEnd();
           
           
            brandMatching.MetaInfo = new Dictionary<string, object>();
            brandMatching.MetaInfo.Add("Supplier.Code", (object)sqlDataReader["FornecedorCodigo"]);
            brandMatching.MetaInfo.Add("Supplier.Name", (object)sqlDataReader["FornecedorNome"]);
            brandMatching.MetaInfo.Add("Brand.Code", (object)sqlDataReader["MapTo"].ToString());

            brandMatching.Inactive = (bool)sqlDataReader["Inativo"];
            brandMatching.Creation = (DateTime)sqlDataReader["Criacao"];
            brandMatching.Version = (DateTime)sqlDataReader["Versao"];
            brandMatching.EditionMode = true;

            return brandMatching;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="brandMatching"></param>
        /// <param name="sqlOperationType"></param>
        /// <returns></returns>
        List<SqlParameter> Serialize(BrandMatching brandMatching, SqlOperationType sqlOperationType)
        {

            List<SqlParameter> sqlParameters = new List<SqlParameter>();

            switch (sqlOperationType)
            {
                case SqlOperationType.Insert:
                    sqlParameters.Add(new SqlParameter("@FornecedorCodigo", SQLStrings.CleanDangerousText(brandMatching.Supplier.Code).ToUpper()));
                    sqlParameters.Add(new SqlParameter("@Codigo", SQLStrings.CleanDangerousText(brandMatching.Code).ToUpper()));
                    sqlParameters.Add(new SqlParameter("@Descricao", SQLStrings.CleanDangerousText(brandMatching.Description)));
                    if (brandMatching.MapTo != null)
                    {
                        sqlParameters.Add(new SqlParameter("@MapTo", SQLStrings.CleanDangerousText(brandMatching.MapTo.Code)));
                    }
                    else
                    {
                        sqlParameters.Add(new SqlParameter("@MapTo", DBNull.Value));
                    }

                    sqlParameters.Add(new SqlParameter("@Inativo", brandMatching.Inactive));
                    break;

                case SqlOperationType.Update:
                    sqlParameters.Add(new SqlParameter("@FornecedorCodigo", SQLStrings.CleanDangerousText(brandMatching.Supplier.Code).ToUpper()));
                    sqlParameters.Add(new SqlParameter("@Codigo", SQLStrings.CleanDangerousText(brandMatching.Code).ToUpper()));
                    sqlParameters.Add(new SqlParameter("@Descricao", SQLStrings.CleanDangerousText(brandMatching.Description)));
                    if (brandMatching.MapTo != null)
                    {
                        sqlParameters.Add(new SqlParameter("@MapTo", SQLStrings.CleanDangerousText(brandMatching.MapTo.Code)));
                    }
                    else
                    {
                        sqlParameters.Add(new SqlParameter("@MapTo", DBNull.Value));
                    }
                    sqlParameters.Add(new SqlParameter("@Inativo", brandMatching.Inactive));
                    sqlParameters.Add(new SqlParameter("@Versao", brandMatching.Version));
                    break;

                case SqlOperationType.Delete:
                    sqlParameters.Add(new SqlParameter("@FornecedorCodigo", SQLStrings.CleanDangerousText(brandMatching.Supplier.Code).ToUpper()));
                    sqlParameters.Add(new SqlParameter("@Codigo", SQLStrings.CleanDangerousText(brandMatching.Code)));
                    sqlParameters.Add(new SqlParameter("@Versao", brandMatching.Version));
                    break;

                default:
                    throw new MyException(_namespace, _className, "Serialize()", string.Format("{0}!", GlobalVariables.Resource.GetString("ForeseenEnumeratorString", GlobalVariables.Culture).ToLower()));
            }

            return sqlParameters;
        }
    }
}
