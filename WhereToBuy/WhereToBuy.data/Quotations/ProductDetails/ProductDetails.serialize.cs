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
    public partial class ProductDetails
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sqlDataReader"></param>
        /// <returns></returns>
        ProductDetail Deserialize(ref SqlDataReader sqlDataReader)
        {

            ProductDetail productDetail = new ProductDetail();
            productDetail.ProductCode = ((string)sqlDataReader["ProdutoCodigo"]).TrimEnd();
            productDetail.Description = ((string)sqlDataReader["Descricao"]).TrimEnd();
            productDetail.DescriptionScore = (short)sqlDataReader["DescricaoPontuacao"];
            productDetail.IsDescriptionDisable = (bool)sqlDataReader["DescricaoInativa"];
            productDetail.Features = ((string)sqlDataReader["Caracteristicas"]).TrimEnd();
            productDetail.FeaturesScore = (short)sqlDataReader["CaracteristicasPontuacao"];
            productDetail.IsFeaturesDisable = (bool)sqlDataReader["CaracteristicasInativas"];
            productDetail.Link = ((string)sqlDataReader["Link"]).TrimEnd();
            productDetail.LinkScore = (short)sqlDataReader["LinkPontuacao"];
            productDetail.IsLinkDisable = (bool)sqlDataReader["LinkInativo"];
            productDetail.Image = ((string)sqlDataReader["Imagem"]).TrimEnd();
            productDetail.ImageScore = (short)sqlDataReader["ImagemPontuacao"];
            productDetail.IsImageDisable = (bool)sqlDataReader["ImagemInativa"];
            productDetail.AutomaticUpdate = (bool)sqlDataReader["AtualizacaoAutomaticaInativa"];
            productDetail.ContentConcernIndex = (int)sqlDataReader["AtualizacaoManualNecessaria"];
            productDetail.NeddManualUpdate = (bool)sqlDataReader["IndicePreocupacaoConteudo"];

            productDetail.MetaInfo = new Dictionary<string, object>();
            productDetail.MetaInfo.Add("Supplier.Code", (object)sqlDataReader["FornecedorCodigo"]);
            productDetail.MetaInfo.Add("Supplier.Name", (object)sqlDataReader["FornecedorNome"]);

            productDetail.Inactive = (bool)sqlDataReader["Inativo"];
            productDetail.Creation = (DateTime)sqlDataReader["Criacao"];
            productDetail.Version = (DateTime)sqlDataReader["Versao"];
            productDetail.EditionMode = true;
            return productDetail;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productDetail"></param>
        /// <param name="sqlOperationType"></param>
        /// <returns></returns>
        List<SqlParameter> Serialize(ProductDetail productDetail, SqlOperationType sqlOperationType)
        {

            List<SqlParameter> sqlParameters = new List<SqlParameter>();

            switch (sqlOperationType)
            {
                case SqlOperationType.Insert:
                    sqlParameters.Add(new SqlParameter("@ProdutoCodigo", productDetail.ProductCode));
                    sqlParameters.Add(new SqlParameter("@ProdutoCodigo", productDetail.Supplier.Code));
                    sqlParameters.Add(new SqlParameter("@Descricao", SQLStrings.CleanDangerousText(productDetail.Description)));
                    sqlParameters.Add(new SqlParameter("@DescricaoPontuacao", productDetail.DescriptionScore));
                    sqlParameters.Add(new SqlParameter("@DescricaoInativa", productDetail.IsDescriptionDisable));
                    sqlParameters.Add(new SqlParameter("@Caracteristicas", SQLStrings.CleanDangerousText(productDetail.Features)));
                    sqlParameters.Add(new SqlParameter("@CaracteristicasPontuacao", productDetail.FeaturesScore));
                    sqlParameters.Add(new SqlParameter("@CaracteristicasInativa", productDetail.IsFeaturesDisable));
                    sqlParameters.Add(new SqlParameter("@Link", SQLStrings.CleanDangerousText(productDetail.Link)));
                    sqlParameters.Add(new SqlParameter("@LinkPontuacao", productDetail.LinkScore));
                    sqlParameters.Add(new SqlParameter("@LinkInativa", productDetail.IsLinkDisable));
                    sqlParameters.Add(new SqlParameter("@Imagem", SQLStrings.CleanDangerousText(productDetail.Image)));
                    sqlParameters.Add(new SqlParameter("@ImagemPontuacao", productDetail.ImageScore));
                    sqlParameters.Add(new SqlParameter("@ImagemInativa", productDetail.IsImageDisable));
                    sqlParameters.Add(new SqlParameter("@AtualizacaoAutomaticaInativa", productDetail.AutomaticUpdate));
                    sqlParameters.Add(new SqlParameter("@AtualizacaoManualNecessaria", productDetail.NeddManualUpdate));
                    sqlParameters.Add(new SqlParameter("@IndicePreocupacaoConteudo", productDetail.ContentConcernIndex));
                    sqlParameters.Add(new SqlParameter("@Inativo", productDetail.Inactive));
                    break;

                case SqlOperationType.Update:
                    sqlParameters.Add(new SqlParameter("@ProdutoCodigo", productDetail.ProductCode));
                    sqlParameters.Add(new SqlParameter("@ProdutoCodigo", productDetail.Supplier.Code));
                    sqlParameters.Add(new SqlParameter("@Descricao", SQLStrings.CleanDangerousText(productDetail.Description)));
                    sqlParameters.Add(new SqlParameter("@DescricaoPontuacao", productDetail.DescriptionScore));
                    sqlParameters.Add(new SqlParameter("@DescricaoInativa", productDetail.IsDescriptionDisable));
                    sqlParameters.Add(new SqlParameter("@Caracteristicas", SQLStrings.CleanDangerousText(productDetail.Features)));
                    sqlParameters.Add(new SqlParameter("@CaracteristicasPontuacao", productDetail.FeaturesScore));
                    sqlParameters.Add(new SqlParameter("@CaracteristicasInativa", productDetail.IsFeaturesDisable));
                    sqlParameters.Add(new SqlParameter("@Link", SQLStrings.CleanDangerousText(productDetail.Link)));
                    sqlParameters.Add(new SqlParameter("@LinkPontuacao", productDetail.LinkScore));
                    sqlParameters.Add(new SqlParameter("@LinkInativa", productDetail.IsLinkDisable));
                    sqlParameters.Add(new SqlParameter("@Imagem", SQLStrings.CleanDangerousText(productDetail.Image)));
                    sqlParameters.Add(new SqlParameter("@ImagemPontuacao", productDetail.ImageScore));
                    sqlParameters.Add(new SqlParameter("@ImagemInativa", productDetail.IsImageDisable));
                    sqlParameters.Add(new SqlParameter("@AtualizacaoAutomaticaInativa", productDetail.AutomaticUpdate));
                    sqlParameters.Add(new SqlParameter("@AtualizacaoManualNecessaria", productDetail.NeddManualUpdate));
                    sqlParameters.Add(new SqlParameter("@IndicePreocupacaoConteudo", productDetail.ContentConcernIndex));
                    sqlParameters.Add(new SqlParameter("@Inativo", productDetail.Inactive));
                    sqlParameters.Add(new SqlParameter("@Versao", productDetail.Version));
                    break;

                case SqlOperationType.Delete:
                    sqlParameters.Add(new SqlParameter("@ProdutoCodigo", productDetail.ProductCode));
                    sqlParameters.Add(new SqlParameter("@ProdutoCodigo", productDetail.Supplier.Code));
                    sqlParameters.Add(new SqlParameter("@Versao", productDetail.Version));
                    break;

                default:
                    throw new MyException(_namespace, _className, "Serialize()", string.Format("{0}!", GlobalVariables.Resource.GetString("ForeseenEnumeratorString", GlobalVariables.Culture).ToLower()));
            }

            return sqlParameters;
        }
    }
}
