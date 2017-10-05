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
    public partial class Catalogs
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sqlDataReader"></param>
        /// <returns></returns>
        Catalog Deserialize(ref SqlDataReader sqlDataReader)
        {
            Catalog catalog = new Catalog();

            catalog.Code = ((string)sqlDataReader["Codigo"]).TrimEnd();
            catalog.Description = ((string)sqlDataReader["Descricao"]).TrimEnd();
            catalog.Notes = (Convert.ToString(sqlDataReader["Notas"])).TrimEnd();
            catalog.Inactive = (bool)sqlDataReader["Inativo"];
            catalog.Creation = (DateTime)sqlDataReader["Criacao"];
            catalog.Version = (DateTime)sqlDataReader["Versao"];
            catalog.EditionMode = true;
            return catalog;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="group"></param>
        /// <param name="sqlOperationType"></param>
        /// <returns></returns>
        List<SqlParameter> Serialize(Catalog catalog, SqlOperationType sqlOperationType)
        {

            List<SqlParameter> sqlParameters = new List<SqlParameter>();

            switch (sqlOperationType)
            {
                case SqlOperationType.Insert:
                    sqlParameters.Add(new SqlParameter("@Codigo", SQLStrings.CleanDangerousText(catalog.Code).ToUpper()));
                    sqlParameters.Add(new SqlParameter("@Descricao", SQLStrings.CleanDangerousText(catalog.Description)));
                    sqlParameters.Add(new SqlParameter("@Notas", SQLStrings.CleanDangerousText(string.Format("{0}", catalog.Notes))));
                    sqlParameters.Add(new SqlParameter("@Inativo", catalog.Inactive));
                    break;

                case SqlOperationType.Update:
                    sqlParameters.Add(new SqlParameter("@Codigo", SQLStrings.CleanDangerousText(catalog.Code)));
                    sqlParameters.Add(new SqlParameter("@Descricao", SQLStrings.CleanDangerousText(catalog.Description)));
                    sqlParameters.Add(new SqlParameter("@Notas", SQLStrings.CleanDangerousText(string.Format("{0}", catalog.Notes))));
                    sqlParameters.Add(new SqlParameter("@Inativo", catalog.Inactive));
                    sqlParameters.Add(new SqlParameter("@Versao", catalog.Version));
                    break;

                case SqlOperationType.Delete:
                    sqlParameters.Add(new SqlParameter("@Codigo", SQLStrings.CleanDangerousText(catalog.Code)));
                    sqlParameters.Add(new SqlParameter("@Versao", catalog.Version));
                    break;

                default:
                    throw new MyException(_namespace, _className, "Serialize()", string.Format("{0}!", GlobalVariables.Resource.GetString("ForeseenEnumeratorString", GlobalVariables.Culture).ToLower()));
            }

            return sqlParameters;
        }
    }
}
