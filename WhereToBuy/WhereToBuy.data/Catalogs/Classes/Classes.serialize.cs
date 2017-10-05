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
    public partial class Classes
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sqlDataReader"></param>
        /// <returns></returns>
        Classe Deserialize(ref SqlDataReader sqlDataReader)
        {
            Classe classe = new Classe();

            classe.Code = ((string)sqlDataReader["Codigo"]).TrimEnd();
            classe.Description = ((string)sqlDataReader["Descricao"]).TrimEnd();
            classe.Range = ((double)sqlDataReader["Margem"]);
            classe.RangeMinValue = ((decimal)sqlDataReader["MargemValorMinimo"]);
            classe.Notes = (sqlDataReader["Descricao"]).ToString().TrimEnd();

            classe.MetaInfo = new Dictionary<string, object>();
            classe.MetaInfo.Add("Catalog.Codigo", (object)sqlDataReader["CatalogoCodigo"]);
            classe.MetaInfo.Add("Catalog.Description", (object)sqlDataReader["CatalogoDescricao"]);

            classe.Inactive = (bool)sqlDataReader["Inativo"];
            classe.Creation = (DateTime)sqlDataReader["Criacao"];
            classe.Version = (DateTime)sqlDataReader["Versao"];
            classe.EditionMode = true;

            return classe;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="classe"></param>
        /// <param name="sqlOperationType"></param>
        /// <returns></returns>
        List<SqlParameter> Serialize(Classe classe, SqlOperationType sqlOperationType)
        {

            List<SqlParameter> sqlParameters = new List<SqlParameter>();

            switch (sqlOperationType)
            {
                case SqlOperationType.Insert:
                    sqlParameters.Add(new SqlParameter("@Codigo", SQLStrings.CleanDangerousText(classe.Code).ToUpper()));
                    sqlParameters.Add(new SqlParameter("@Descricao", SQLStrings.CleanDangerousText(classe.Description)));
                    sqlParameters.Add(new SqlParameter("@CatalogoCodigo", SQLStrings.CleanDangerousText(classe.Catalog.Code)));
                    sqlParameters.Add(new SqlParameter("@Margem", classe.Range));
                    sqlParameters.Add(new SqlParameter("@MargemValorMinimo", classe.RangeMinValue));
                    sqlParameters.Add(new SqlParameter("@Notas", SQLStrings.CleanDangerousText(string.Format("{0}", classe.Notes))));
                    sqlParameters.Add(new SqlParameter("@Inativo", classe.Inactive));
                    break;

                case SqlOperationType.Update:
                    sqlParameters.Add(new SqlParameter("@Codigo", SQLStrings.CleanDangerousText(classe.Code)));
                    sqlParameters.Add(new SqlParameter("@Descricao", SQLStrings.CleanDangerousText(classe.Description)));
                    sqlParameters.Add(new SqlParameter("@CatalogoCodigo", SQLStrings.CleanDangerousText(classe.Catalog.Code)));
                    sqlParameters.Add(new SqlParameter("@Margem", classe.Range));
                    sqlParameters.Add(new SqlParameter("@MargemValorMinimo", classe.RangeMinValue));
                    sqlParameters.Add(new SqlParameter("@Notas", SQLStrings.CleanDangerousText(string.Format("{0}", classe.Notes))));
                    sqlParameters.Add(new SqlParameter("@Inativo", classe.Inactive));
                    sqlParameters.Add(new SqlParameter("@Versao", classe.Version));
                    break;

                case SqlOperationType.Delete:
                    sqlParameters.Add(new SqlParameter("@Codigo", SQLStrings.CleanDangerousText(classe.Code)));
                    sqlParameters.Add(new SqlParameter("@Versao", classe.Version));
                    break;

                default:
                    throw new MyException(_namespace, _className, "Serialize()", string.Format("{0}!", GlobalVariables.Resource.GetString("ForeseenEnumeratorString", GlobalVariables.Culture).ToLower()));
            }

            return sqlParameters;
        }
    }
}
