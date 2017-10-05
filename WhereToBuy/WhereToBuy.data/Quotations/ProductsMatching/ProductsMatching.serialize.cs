using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhereToBuy.entities;
using WhereToBuy.entities.specs;
using WhereToBuy.utils;
using WhereToBuy.utils.GlobalVariables;

namespace WhereToBuy.data
{
    public partial class ProductsMatching
    { /// <summary>
        /// 
        /// </summary>
        /// <param name="sqlDataReader"></param>
        /// <returns></returns>
        ProductMatching Deserialize(ref SqlDataReader sqlDataReader)
        {
            ProductMatching productMatching = new ProductMatching();

            productMatching.Code = ((string)sqlDataReader["Codigo"]).TrimEnd();
            productMatching.Supplement = ((string)sqlDataReader["ComplementoCodigo"]).TrimEnd();
            productMatching.Description = ((string)sqlDataReader["Descricao"]).TrimEnd();
            productMatching.NeedPreventionPricesOut = (bool)sqlDataReader["DispensaPrevencaoPrecosDesfasados"];
            productMatching.NeedPreventionFakeStock = (bool)sqlDataReader["DispensaPrevencaoFalsoStock"];
            productMatching.QuotationExpireHours = (int)(sqlDataReader["HorasValidadeCotacao"]);
            productMatching.DataReset = sqlDataReader["DataReset"] == DBNull.Value ? null : (DateTime?)sqlDataReader["DataReset"];
            productMatching.Notes = sqlDataReader["Notas"] == DBNull.Value ? string.Empty : ((string)sqlDataReader["Notas"]).TrimEnd();

            productMatching.MetaInfo = new Dictionary<string, object>();
            productMatching.MetaInfo.Add("Supplier.Code", (object)sqlDataReader["FornecedorCodigo"]);
            productMatching.MetaInfo.Add("Supplier.Name", (object)sqlDataReader["FornecedorNome"]);
            productMatching.MetaInfo.Add("Stock.Code", (object)sqlDataReader["StockCodigoSubstituto"].ToString());
            productMatching.MetaInfo.Add("Stock.Description", (object)sqlDataReader["StockDescricao"].ToString());
            productMatching.MetaInfo.Add("Product.Code", (object)sqlDataReader["MapTo"].ToString());
            productMatching.MetaInfo.Add("Product.Description", (object)sqlDataReader["ProdutoDescricao"].ToString());

            productMatching.Inactive = (bool)sqlDataReader["Inativo"];
            productMatching.Creation = (DateTime)sqlDataReader["Criacao"];
            productMatching.Version = (DateTime)sqlDataReader["Versao"];
            productMatching.EditionMode = true;

            return productMatching;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productMatching"></param>
        /// <param name="sqlOperationType"></param>
        /// <returns></returns>
        List<SqlParameter> Serialize(ProductMatching productMatching, SqlOperationType sqlOperationType)
        {

            List<SqlParameter> sqlParameters = new List<SqlParameter>();

            switch (sqlOperationType)
            {
                case SqlOperationType.Insert:
                    sqlParameters.Add(new SqlParameter("@FornecedorCodigo", SQLStrings.CleanDangerousText(productMatching.Supplier.Code).ToUpper()));
                    sqlParameters.Add(new SqlParameter("@ComplementoCodigo", SQLStrings.CleanDangerousText(productMatching.Supplement)));
                    sqlParameters.Add(new SqlParameter("@Codigo", SQLStrings.CleanDangerousText(productMatching.Code).ToUpper()));
                    sqlParameters.Add(new SqlParameter("@DispensaPrevencaoPrecosDesfasados", productMatching.NeedPreventionFakeStock));
                    sqlParameters.Add(new SqlParameter("@DispensaPrevencaoFalsoStock", productMatching.NeedPreventionPricesOut));
                    sqlParameters.Add(new SqlParameter("@HorasValidadeCotacao", productMatching.QuotationExpireHours));

                    if (productMatching.MapTo != null)
                    {
                        sqlParameters.Add(new SqlParameter("@MapTo", SQLStrings.CleanDangerousText(productMatching.MapTo.Code)));
                    }
                    else
                    {
                        sqlParameters.Add(new SqlParameter("@MapTo", DBNull.Value));
                    }

                    if (productMatching.ReplacementStock != null)
                    {
                        sqlParameters.Add(new SqlParameter("@StockCodigoSubstituto", SQLStrings.CleanDangerousText(productMatching.ReplacementStock.Code)));
                    }
                    else
                    {
                        sqlParameters.Add(new SqlParameter("@StockCodigoSubstituto", DBNull.Value));
                    }
                    
                    if (productMatching.DataReset != null && productMatching.DataReset > DateTime.Parse("01/01/1900 00:00:00"))
                    {
                        sqlParameters.Add(new SqlParameter("@DataReset", productMatching.DataReset));
                    }
                    else
                    {
                        sqlParameters.Add(new SqlParameter("@DataReset", DBNull.Value));
                    }

                    if (productMatching.Notes != null)
                    {
                        sqlParameters.Add(new SqlParameter("@Notas", SQLStrings.CleanDangerousText(productMatching.Notes)));
                    }
                    else
                    {
                        sqlParameters.Add(new SqlParameter("@Notas", DBNull.Value));
                    }

                    sqlParameters.Add(new SqlParameter("@Inativo", productMatching.Inactive));
                    break;

                case SqlOperationType.Update:
                    sqlParameters.Add(new SqlParameter("@FornecedorCodigo", SQLStrings.CleanDangerousText(productMatching.Supplier.Code).ToUpper()));
                    sqlParameters.Add(new SqlParameter("@ComplementoCodigo", SQLStrings.CleanDangerousText(productMatching.Supplement)));
                    sqlParameters.Add(new SqlParameter("@Codigo", SQLStrings.CleanDangerousText(productMatching.Code).ToUpper()));
                    sqlParameters.Add(new SqlParameter("@DispensaPrevencaoPrecosDesfasados", productMatching.NeedPreventionFakeStock));
                    sqlParameters.Add(new SqlParameter("@DispensaPrevencaoFalsoStock", productMatching.NeedPreventionPricesOut));
                    sqlParameters.Add(new SqlParameter("@HorasValidadeCotacao", productMatching.QuotationExpireHours));

                    if (productMatching.MapTo != null)
                    {
                        sqlParameters.Add(new SqlParameter("@MapTo", SQLStrings.CleanDangerousText(productMatching.MapTo.Code)));
                    }
                    else
                    {
                        sqlParameters.Add(new SqlParameter("@MapTo", DBNull.Value));
                    }

                    if (productMatching.ReplacementStock != null)
                    {
                        sqlParameters.Add(new SqlParameter("@StockCodigoSubstituto", SQLStrings.CleanDangerousText(productMatching.ReplacementStock.Code)));
                    }
                    else
                    {
                        sqlParameters.Add(new SqlParameter("@StockCodigoSubstituto", DBNull.Value));
                    }

                    
                    if (productMatching.DataReset != null && productMatching.DataReset > DateTime.Parse("01/01/1900 00:00:00"))
                    {
                        sqlParameters.Add(new SqlParameter("@DataReset", productMatching.DataReset));
                    }
                    else
                    {
                        sqlParameters.Add(new SqlParameter("@DataReset", DBNull.Value));
                    }

                    if (productMatching.Notes != null)
                    {
                        sqlParameters.Add(new SqlParameter("@Notas", SQLStrings.CleanDangerousText(productMatching.Notes)));
                    }
                    else
                    {
                        sqlParameters.Add(new SqlParameter("@Notas", DBNull.Value));
                    }

                    sqlParameters.Add(new SqlParameter("@Inativo", productMatching.Inactive));
                    sqlParameters.Add(new SqlParameter("@Versao", productMatching.Version));
                    break;

                case SqlOperationType.Delete:
                    sqlParameters.Add(new SqlParameter("@FornecedorCodigo", SQLStrings.CleanDangerousText(productMatching.Supplier.Code).ToUpper()));
                    sqlParameters.Add(new SqlParameter("@ComplementoCodigo", SQLStrings.CleanDangerousText(productMatching.Supplement)));
                    sqlParameters.Add(new SqlParameter("@Codigo", SQLStrings.CleanDangerousText(productMatching.Code).ToUpper()));
                    sqlParameters.Add(new SqlParameter("@Versao", productMatching.Version));
                    break;

                default:
                    throw new MyException(_namespace, _className, "Serialize()", string.Format("{0}!", GlobalVariables.Resource.GetString("ForeseenEnumeratorString", GlobalVariables.Culture).ToLower()));
            }

            return sqlParameters;
        }
    }
}
