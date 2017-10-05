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
    public partial class Categories
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sqlDataReader"></param>
        /// <returns></returns>
        Category Deserialize(ref SqlDataReader sqlDataReader)
        {
            Category category = new Category();

            category.Code = ((string)sqlDataReader["Codigo"]).TrimEnd();
            category.Description = ((string)sqlDataReader["Descricao"]).TrimEnd();
            category.UnityWeightAverage = (double)sqlDataReader["PesoMedioUnidade"];
            category.MinPriceAllowed = (decimal)sqlDataReader["PrecoMinimoPermitido"];
            category.MaxPriceAllowed = (decimal)sqlDataReader["PrecoMaximoPermitido"];
            category.MaxPriceAmplitude = (double)sqlDataReader["PrecoAmplitudeMax"];
            category.Trust = (double)sqlDataReader["Confianca"];
            category.Inactive = (bool)sqlDataReader["Inativo"];
            category.Creation = (DateTime)sqlDataReader["Criacao"];
            category.Version = (DateTime)sqlDataReader["Versao"];
            category.EditionMode = true;
            return category;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="category"></param>
        /// <param name="sqlOperationType"></param>
        /// <returns></returns>
        List<SqlParameter> Serialize(Category category, SqlOperationType sqlOperationType)
        {

            List<SqlParameter> sqlParameters = new List<SqlParameter>();

            switch (sqlOperationType)
            {
                case SqlOperationType.Insert:
                    sqlParameters.Add(new SqlParameter("@Codigo", SQLStrings.CleanDangerousText(category.Code).ToUpper()));
                    sqlParameters.Add(new SqlParameter("@Descricao", SQLStrings.CleanDangerousText(category.Description)));
                    sqlParameters.Add(new SqlParameter("@PesoMedioUnidade", category.UnityWeightAverage));
                    sqlParameters.Add(new SqlParameter("@PrecoMinimoPermitido", category.MinPriceAllowed));
                    sqlParameters.Add(new SqlParameter("@PrecoMaximoPermitido", category.MaxPriceAllowed));
                    sqlParameters.Add(new SqlParameter("@PrecoAmplitudeMax", category.MaxPriceAmplitude));
                    sqlParameters.Add(new SqlParameter("@Confianca", category.Trust));
                    sqlParameters.Add(new SqlParameter("@Inativo", category.Inactive));
                    break;

                case SqlOperationType.Update:
                    sqlParameters.Add(new SqlParameter("@Codigo", SQLStrings.CleanDangerousText(category.Code)));
                    sqlParameters.Add(new SqlParameter("@Descricao", SQLStrings.CleanDangerousText(category.Description)));
                    sqlParameters.Add(new SqlParameter("@PesoMedioUnidade", category.UnityWeightAverage));
                    sqlParameters.Add(new SqlParameter("@PrecoMinimoPermitido", category.MinPriceAllowed));
                    sqlParameters.Add(new SqlParameter("@PrecoMaximoPermitido", category.MaxPriceAllowed));
                    sqlParameters.Add(new SqlParameter("@PrecoAmplitudeMax", category.MaxPriceAmplitude));
                    sqlParameters.Add(new SqlParameter("@Confianca", category.Trust));
                    sqlParameters.Add(new SqlParameter("@Inativo", category.Inactive));
                    sqlParameters.Add(new SqlParameter("@Versao", category.Version));
                    break;

                case SqlOperationType.Delete:
                    sqlParameters.Add(new SqlParameter("@Codigo", SQLStrings.CleanDangerousText(category.Code)));
                    sqlParameters.Add(new SqlParameter("@Versao", category.Version));
                    break;

                default:
                    throw new MyException(_namespace, _className, "Serialize()", string.Format("{0}!", GlobalVariables.Resource.GetString("ForeseenEnumeratorString", GlobalVariables.Culture).ToLower()));
            }

            return sqlParameters;
        }
    }
}
