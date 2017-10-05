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
    public partial class Brands
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sqlDataReader"></param>
        /// <returns></returns>
        Brand Deserialize(ref SqlDataReader sqlDataReader)
        {
            Brand brand = new Brand();

            brand.Code = ((string)sqlDataReader["Codigo"]).TrimEnd();
            brand.Description = ((string)sqlDataReader["Descricao"]).TrimEnd();
            brand.Inactive = (bool)sqlDataReader["Inativo"];
            brand.Creation = (DateTime)sqlDataReader["Criacao"];
            brand.Version = (DateTime)sqlDataReader["Versao"];
            brand.EditionMode = true;
            return brand;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="brand"></param>
        /// <param name="sqlOperationType"></param>
        /// <returns></returns>
        List<SqlParameter> Serialize(Brand brand, SqlOperationType sqlOperationType)
        {

            List<SqlParameter> sqlParameters = new List<SqlParameter>();

            switch (sqlOperationType)
            {
                case SqlOperationType.Insert:
                    sqlParameters.Add(new SqlParameter("@Codigo", SQLStrings.CleanDangerousText(brand.Code).ToUpper()));
                    sqlParameters.Add(new SqlParameter("@Descricao", SQLStrings.CleanDangerousText(brand.Description)));
                    sqlParameters.Add(new SqlParameter("@Inativo", brand.Inactive));
                    break;

                case SqlOperationType.Update:
                    sqlParameters.Add(new SqlParameter("@Codigo", SQLStrings.CleanDangerousText(brand.Code)));
                    sqlParameters.Add(new SqlParameter("@Descricao", SQLStrings.CleanDangerousText(brand.Description)));
                    sqlParameters.Add(new SqlParameter("@Inativo", brand.Inactive));
                    sqlParameters.Add(new SqlParameter("@Versao", brand.Version));
                    break;

                case SqlOperationType.Delete:
                    sqlParameters.Add(new SqlParameter("@Codigo", SQLStrings.CleanDangerousText(brand.Code)));
                    sqlParameters.Add(new SqlParameter("@Versao", brand.Version));
                    break;

                default:
                    throw new MyException(_namespace, _className, "Serialize()", string.Format("{0}!", GlobalVariables.Resource.GetString("ForeseenEnumeratorString", GlobalVariables.Culture).ToLower()));
            }

            return sqlParameters;
        }

    }
}
