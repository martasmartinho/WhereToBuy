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
    public partial class StocksMatching
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sqlDataReader"></param>
        /// <returns></returns>
        StockMatching Deserialize(ref SqlDataReader sqlDataReader)
        {
            StockMatching stockMatching = new StockMatching();

            stockMatching.Code = ((string)sqlDataReader["Codigo"]).TrimEnd();
            stockMatching.Description = ((string)sqlDataReader["Descricao"]).TrimEnd();


            stockMatching.MetaInfo = new Dictionary<string, object>();
            stockMatching.MetaInfo.Add("Supplier.Code", (object)sqlDataReader["FornecedorCodigo"]);
            stockMatching.MetaInfo.Add("Supplier.Name", (object)sqlDataReader["FornecedorNome"]);
            stockMatching.MetaInfo.Add("Stock.Code", (object)sqlDataReader["MapTo"].ToString());

            stockMatching.Inactive = (bool)sqlDataReader["Inativo"];
            stockMatching.Creation = (DateTime)sqlDataReader["Criacao"];
            stockMatching.Version = (DateTime)sqlDataReader["Versao"];
            stockMatching.EditionMode = true;

            return stockMatching;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="StockMatching"></param>
        /// <param name="sqlOperationType"></param>
        /// <returns></returns>
        List<SqlParameter> Serialize(StockMatching stockMatching, SqlOperationType sqlOperationType)
        {

            List<SqlParameter> sqlParameters = new List<SqlParameter>();

            switch (sqlOperationType)
            {
                case SqlOperationType.Insert:
                    sqlParameters.Add(new SqlParameter("@FornecedorCodigo", SQLStrings.CleanDangerousText(stockMatching.Supplier.Code).ToUpper()));
                    sqlParameters.Add(new SqlParameter("@Codigo", SQLStrings.CleanDangerousText(stockMatching.Code).ToUpper()));
                    sqlParameters.Add(new SqlParameter("@Descricao", SQLStrings.CleanDangerousText(stockMatching.Description)));
                    if (stockMatching.MapTo != null)
                    {
                        sqlParameters.Add(new SqlParameter("@MapTo", SQLStrings.CleanDangerousText(stockMatching.MapTo.Code)));
                    }
                    else
                    {
                        sqlParameters.Add(new SqlParameter("@MapTo", DBNull.Value));
                    }

                    sqlParameters.Add(new SqlParameter("@Inativo", stockMatching.Inactive));
                    break;

                case SqlOperationType.Update:
                    sqlParameters.Add(new SqlParameter("@FornecedorCodigo", SQLStrings.CleanDangerousText(stockMatching.Supplier.Code).ToUpper()));
                    sqlParameters.Add(new SqlParameter("@Codigo", SQLStrings.CleanDangerousText(stockMatching.Code).ToUpper()));
                    sqlParameters.Add(new SqlParameter("@Descricao", SQLStrings.CleanDangerousText(stockMatching.Description)));
                    if (stockMatching.MapTo != null)
                    {
                        sqlParameters.Add(new SqlParameter("@MapTo", SQLStrings.CleanDangerousText(stockMatching.MapTo.Code)));
                    }
                    else
                    {
                        sqlParameters.Add(new SqlParameter("@MapTo", DBNull.Value));
                    }
                    sqlParameters.Add(new SqlParameter("@Inativo", stockMatching.Inactive));
                    sqlParameters.Add(new SqlParameter("@Versao", stockMatching.Version));
                    break;

                case SqlOperationType.Delete:
                    sqlParameters.Add(new SqlParameter("@FornecedorCodigo", SQLStrings.CleanDangerousText(stockMatching.Supplier.Code).ToUpper()));
                    sqlParameters.Add(new SqlParameter("@Codigo", SQLStrings.CleanDangerousText(stockMatching.Code)));
                    sqlParameters.Add(new SqlParameter("@Versao", stockMatching.Version));
                    break;

                default:
                    throw new MyException(_namespace, _className, "Serialize()", string.Format("{0}!", GlobalVariables.Resource.GetString("ForeseenEnumeratorString", GlobalVariables.Culture).ToLower()));
            }

            return sqlParameters;
        }
    }
}
