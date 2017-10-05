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
    public partial class Taxes
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sqlDataReader"></param>
        /// <returns></returns>
        Tax Deserialize(ref SqlDataReader sqlDataReader)
        {
            Tax tax = new Tax();

            tax.Code = ((string)sqlDataReader["Codigo"]).TrimEnd();
            tax.Description = ((string)sqlDataReader["Descricao"]).TrimEnd();
            tax.TaxDesignation = ((string)sqlDataReader["DesignacaoFiscal"]).TrimEnd();
            tax.TaxValue = ((double)sqlDataReader["Taxa"]);
            tax.Inactive = (bool)sqlDataReader["Inativo"];
            tax.Creation = (DateTime)sqlDataReader["Criacao"];
            tax.Version = (DateTime)sqlDataReader["Versao"];
            tax.EditionMode = true;
            return tax;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tax"></param>
        /// <param name="sqlOperationType"></param>
        /// <returns></returns>
        List<SqlParameter> Serialize(Tax tax, SqlOperationType sqlOperationType)
        {

            List<SqlParameter> sqlParameters = new List<SqlParameter>();

            switch (sqlOperationType)
            {
                case SqlOperationType.Insert:
                    sqlParameters.Add(new SqlParameter("@Codigo", SQLStrings.CleanDangerousText(tax.Code).ToUpper()));
                    sqlParameters.Add(new SqlParameter("@Descricao", SQLStrings.CleanDangerousText(tax.Description)));
                    sqlParameters.Add(new SqlParameter("@DesignacaoFiscal", SQLStrings.CleanDangerousText(tax.TaxDesignation)));
                    sqlParameters.Add(new SqlParameter("@Taxa", tax.TaxValue));
                    sqlParameters.Add(new SqlParameter("@Inativo", tax.Inactive));
                    break;

                case SqlOperationType.Update:
                    sqlParameters.Add(new SqlParameter("@Codigo", SQLStrings.CleanDangerousText(tax.Code)));
                    sqlParameters.Add(new SqlParameter("@Descricao", SQLStrings.CleanDangerousText(tax.Description)));
                     sqlParameters.Add(new SqlParameter("@DesignacaoFiscal", SQLStrings.CleanDangerousText(tax.TaxDesignation)));
                    sqlParameters.Add(new SqlParameter("@Taxa", tax.TaxValue));
                    sqlParameters.Add(new SqlParameter("@Inativo", tax.Inactive));
                    sqlParameters.Add(new SqlParameter("@Versao", tax.Version));
                    break;

                case SqlOperationType.Delete:
                    sqlParameters.Add(new SqlParameter("@Codigo", SQLStrings.CleanDangerousText(tax.Code)));
                    sqlParameters.Add(new SqlParameter("@Versao", tax.Version));
                    break;

                default:
                    throw new MyException(_namespace, _className, "Serialize()", string.Format("{0}!", GlobalVariables.Resource.GetString("ForeseenEnumeratorString", GlobalVariables.Culture).ToLower()));
            }

            return sqlParameters;
        }
    }
}
