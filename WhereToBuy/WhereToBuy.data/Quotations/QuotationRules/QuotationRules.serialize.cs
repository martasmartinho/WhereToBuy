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
    public partial class QuotationRules
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sqlDataReader"></param>
        /// <returns></returns>
        QuotationRule Deserialize(ref SqlDataReader sqlDataReader)
        {
            QuotationRule quotationRule = new QuotationRule();

            quotationRule.ExpitationHours = (short)sqlDataReader["HorasValidade"];
            quotationRule.DataReset = sqlDataReader["DataReset"] == DBNull.Value ? null : (DateTime?)sqlDataReader["DataReset"];
            quotationRule.Notes = sqlDataReader["Notas"] == DBNull.Value ? string.Empty : ((string)sqlDataReader["Notas"]).TrimEnd();


            quotationRule.MetaInfo = new Dictionary<string, object>();
            quotationRule.MetaInfo.Add("Supplier.Code", (object)sqlDataReader["FornecedorCodigo"]);
            quotationRule.MetaInfo.Add("Supplier.Name", (object)sqlDataReader["FornecedorNome"]);
            quotationRule.MetaInfo.Add("Brand.Code", (object)sqlDataReader["MarcaCodigo"]);
            quotationRule.MetaInfo.Add("Brand.Description", (object)sqlDataReader["MarcaDescricao"]);
            quotationRule.MetaInfo.Add("Category.Code", (object)sqlDataReader["CategoriaCodigo"]);
            quotationRule.MetaInfo.Add("Category.Description", (object)sqlDataReader["CategoriaDescricao"]);
            quotationRule.MetaInfo.Add("Stock.Code", (object)sqlDataReader["StockCodigo"]);
            quotationRule.MetaInfo.Add("Stock.Description", (object)sqlDataReader["StockDescricao"]);
            quotationRule.MetaInfo.Add("Stock.AvailabilityLevel", (object)sqlDataReader["DisponibilidadeNivel"]);
            quotationRule.MetaInfo.Add("SubstituteStock.Code", (object)sqlDataReader["StockCodigoSubstituto"]);
            quotationRule.MetaInfo.Add("SubstituteStock.Description", (object)sqlDataReader["StockDescricaoSubstituto"]);

            quotationRule.Version = (DateTime)sqlDataReader["Versao"];
            quotationRule.EditionMode = true;

            return quotationRule;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="quotationRule"></param>
        /// <param name="sqlOperationType"></param>
        /// <returns></returns>
        List<SqlParameter> Serialize(QuotationRule quotationRule, SqlOperationType sqlOperationType)
        {

            List<SqlParameter> sqlParameters = new List<SqlParameter>();

            switch (sqlOperationType)
            {
                case SqlOperationType.Insert:
                    sqlParameters.Add(new SqlParameter("@FornecedorCodigo", SQLStrings.CleanDangerousText(quotationRule.Supplier.Code).ToUpper()));
                    sqlParameters.Add(new SqlParameter("@MarcaCodigo", SQLStrings.CleanDangerousText(quotationRule.Brand.Code).ToUpper()));
                    sqlParameters.Add(new SqlParameter("@CategoriaCodigo", SQLStrings.CleanDangerousText(quotationRule.Category.Code).ToUpper()));
                    sqlParameters.Add(new SqlParameter("@StockCodigo", SQLStrings.CleanDangerousText(quotationRule.Stock.Code).ToUpper()));
                    sqlParameters.Add(new SqlParameter("@HorasValidade", quotationRule.ExpitationHours));
                    
                    if (quotationRule.SubstituteStock != null)
                    {
                        sqlParameters.Add(new SqlParameter("@StockCodigoSubstituto", SQLStrings.CleanDangerousText(quotationRule.SubstituteStock.Code)));
                    }
                    else
                    {
                        sqlParameters.Add(new SqlParameter("@StockCodigoSubstituto", DBNull.Value));
                    }

                    string teste = DateTime.MinValue.ToString();

                    if (quotationRule.DataReset != null && quotationRule.DataReset > DateTime.Parse("01/01/1900 00:00:00"))
                    {
                        sqlParameters.Add(new SqlParameter("@DataReset", quotationRule.DataReset));
                    }
                    else
                    {
                        sqlParameters.Add(new SqlParameter("@DataReset", DBNull.Value));
                    }

                    if (quotationRule.Notes != null)
                    {
                        sqlParameters.Add(new SqlParameter("@Notas", SQLStrings.CleanDangerousText(quotationRule.Notes)));
                    }
                    else
                    {
                        sqlParameters.Add(new SqlParameter("@Notas", DBNull.Value));
                    }

                    break;

                case SqlOperationType.Update:
                    sqlParameters.Add(new SqlParameter("@FornecedorCodigo", SQLStrings.CleanDangerousText(quotationRule.Supplier.Code).ToUpper()));
                    sqlParameters.Add(new SqlParameter("@MarcaCodigo", SQLStrings.CleanDangerousText(quotationRule.Brand.Code).ToUpper()));
                    sqlParameters.Add(new SqlParameter("@CategoriaCodigo", SQLStrings.CleanDangerousText(quotationRule.Category.Code).ToUpper()));
                    sqlParameters.Add(new SqlParameter("@StockCodigo", SQLStrings.CleanDangerousText(quotationRule.Stock.Code).ToUpper()));
                    sqlParameters.Add(new SqlParameter("@HorasValidade", quotationRule.ExpitationHours));
                    
                    if (quotationRule.SubstituteStock != null)
                    {
                        sqlParameters.Add(new SqlParameter("@StockCodigoSubstituto", SQLStrings.CleanDangerousText(quotationRule.SubstituteStock.Code)));
                    }
                    else
                    {
                        sqlParameters.Add(new SqlParameter("@StockCodigoSubstituto", DBNull.Value));
                    }

                    if (quotationRule.DataReset != null && quotationRule.DataReset > DateTime.Parse("01/01/1900 00:00:00"))
                    {
                        sqlParameters.Add(new SqlParameter("@DataReset", quotationRule.DataReset));
                    }
                    else
                    {
                        sqlParameters.Add(new SqlParameter("@DataReset", DBNull.Value));
                    }

                    if (quotationRule.Notes != null)
                    {
                        sqlParameters.Add(new SqlParameter("@Notas", SQLStrings.CleanDangerousText(quotationRule.Notes)));
                    }
                    else
                    {
                        sqlParameters.Add(new SqlParameter("@Notas", DBNull.Value));
                    }
                    sqlParameters.Add(new SqlParameter("@Versao", quotationRule.Version));
                    break;

                case SqlOperationType.Delete:
                    sqlParameters.Add(new SqlParameter("@FornecedorCodigo", SQLStrings.CleanDangerousText(quotationRule.Supplier.Code).ToUpper()));
                    sqlParameters.Add(new SqlParameter("@MarcaCodigo", SQLStrings.CleanDangerousText(quotationRule.Brand.Code).ToUpper()));
                    sqlParameters.Add(new SqlParameter("@CategoriaCodigo", SQLStrings.CleanDangerousText(quotationRule.Category.Code).ToUpper()));
                    sqlParameters.Add(new SqlParameter("@StockCodigo", SQLStrings.CleanDangerousText(quotationRule.Stock.Code).ToUpper()));
                    sqlParameters.Add(new SqlParameter("@Versao", quotationRule.Version));
                    break;

                default:
                    throw new MyException(_namespace, _className, "Serialize()", string.Format("{0}!", GlobalVariables.Resource.GetString("ForeseenEnumeratorString", GlobalVariables.Culture).ToLower()));
            }

            return sqlParameters;
        }
    }
}
