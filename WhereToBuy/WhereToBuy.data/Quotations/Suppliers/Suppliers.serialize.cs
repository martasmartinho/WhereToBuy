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
    public partial class Suppliers
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sqlDataReader"></param>
        /// <returns></returns>
        Supplier Deserialize(ref SqlDataReader sqlDataReader)
        {
            Supplier supplier = new Supplier();

            supplier.Code = ((string)sqlDataReader["Codigo"]).TrimEnd();
            supplier.Name = ((string)sqlDataReader["Nome"]).TrimEnd();
            supplier.Address = ((string)sqlDataReader["Morada"]).TrimEnd();
            supplier.ZipCode = ((string)sqlDataReader["CodigoPostal"]).TrimEnd();
            supplier.City = ((string)sqlDataReader["LocalidadePostal"]).TrimEnd();
            supplier.IdentificationNumber = ((string)sqlDataReader["Contribuinte"]).TrimEnd();
            supplier.Salesman = ((string)sqlDataReader["Vendedor"]).TrimEnd();
            supplier.Phone = ((string)sqlDataReader["Telefone"]).TrimEnd();
            supplier.Cellphone = ((string)sqlDataReader["Telemovel"]).TrimEnd();
            supplier.SMS = ((string)sqlDataReader["SMS"]).TrimEnd();
            supplier.Email = ((string)sqlDataReader["Email"]).TrimEnd();
            supplier.ActiveOnlineAccess = (bool)sqlDataReader["AcessoOnlineAtivo"];
            supplier.Username = ((string)sqlDataReader["Username"]).TrimEnd();
            supplier.Password = ((string)sqlDataReader["Password"]).TrimEnd();
            supplier.SuggestionExpirationHours = (short)sqlDataReader["HorasValidadeSugestao"];
            supplier.AutomaticProductMatching = (bool)sqlDataReader["ProdutosMatchingAutomatico"];
            supplier.ActomaticProductCreation = (bool)sqlDataReader["ProdutosCriacaoAutomatica"];
            supplier.InfoProductDetailAvailable = (bool)sqlDataReader["DisponibilizaInfoProdutoDetalhe"];
            supplier.InicialScoreDescription = (short)sqlDataReader["DescricaoPontuacaoInicial"];
            supplier.InicialScoreFeatures = (short)sqlDataReader["CaracteristicasPontuacaoInicial"];
            supplier.InicialScoreLink = (short)sqlDataReader["LinkPontuacaoInicial"];
            supplier.InicialScoreImage = (short)sqlDataReader["ImagemPontuacaoInicial"];
            supplier.InactiveDescriptionSuggestion = (bool)sqlDataReader["DescricaoSugereInativo"];
            supplier.InactiveFeatureSuggestion = (bool)sqlDataReader["CaracteristicasSugereInativo"];
            supplier.InactiveLinkSuggestion = (bool)sqlDataReader["LinkSugereInativo"];
            supplier.InactiveImageSuggestion = (bool)sqlDataReader["ImagemSugereInativo"];
            supplier.InactiveAutomaticUpdateSuggestion = (bool)sqlDataReader["AtualizacaoAutomaticaInativaSugestao"];
            supplier.ProductPriceTrust = (double)sqlDataReader["ProdutosConfiancaPreco"];
            supplier.ProductAvailableTrust = (double)sqlDataReader["ProdutosConfiancaDisponibilidade"];
            //supplier.TrustIndex = (double)sqlDataReader["IndiceConfianca"];
            supplier.Inactive = (bool)sqlDataReader["Inativo"];
            supplier.Creation = (DateTime)sqlDataReader["Criacao"];
            supplier.Version = (DateTime)sqlDataReader["Versao"];
            supplier.EditionMode = true;
            return supplier;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="supplier"></param>
        /// <param name="sqlOperationType"></param>
        /// <returns></returns>
        List<SqlParameter> Serialize(Supplier supplier, SqlOperationType sqlOperationType)
        {

            List<SqlParameter> sqlParameters = new List<SqlParameter>();

            switch (sqlOperationType)
            {
                case SqlOperationType.Insert:
                    sqlParameters.Add(new SqlParameter("@Codigo", SQLStrings.CleanDangerousText(supplier.Code).ToUpper()));
                    sqlParameters.Add(new SqlParameter("@Nome", SQLStrings.CleanDangerousText(supplier.Name)));
                    sqlParameters.Add(new SqlParameter("@Morada", SQLStrings.CleanDangerousText(supplier.Address)));
                    sqlParameters.Add(new SqlParameter("@CodigoPostal", SQLStrings.CleanDangerousText(supplier.ZipCode)));
                    sqlParameters.Add(new SqlParameter("@LocalidadePostal", SQLStrings.CleanDangerousText(supplier.City)));
                    sqlParameters.Add(new SqlParameter("@Contribuinte", SQLStrings.CleanDangerousText(supplier.IdentificationNumber)));
                    sqlParameters.Add(new SqlParameter("@Vendedor", SQLStrings.CleanDangerousText(supplier.Salesman)));
                    sqlParameters.Add(new SqlParameter("@Telefone", SQLStrings.CleanDangerousText(supplier.Phone)));
                    sqlParameters.Add(new SqlParameter("@Telemovel", SQLStrings.CleanDangerousText(supplier.Cellphone)));
                    sqlParameters.Add(new SqlParameter("@SMS", SQLStrings.CleanDangerousText(supplier.SMS)));
                    sqlParameters.Add(new SqlParameter("@Email", SQLStrings.CleanDangerousText(supplier.Email)));
                    sqlParameters.Add(new SqlParameter("@AcessoOnlineAtivo", supplier.ActiveOnlineAccess));
                    sqlParameters.Add(new SqlParameter("@Username", SQLStrings.CleanDangerousText(supplier.Username)));
                    sqlParameters.Add(new SqlParameter("@Password", SQLStrings.CleanDangerousText(supplier.Password)));
                    sqlParameters.Add(new SqlParameter("@HorasValidadeSugestao", supplier.SuggestionExpirationHours));
                    sqlParameters.Add(new SqlParameter("@ProdutosMatchingAutomatico", supplier.AutomaticProductMatching));
                    sqlParameters.Add(new SqlParameter("@ProdutosCriacaoAutomatica", supplier.ActomaticProductCreation));
                    sqlParameters.Add(new SqlParameter("@DisponibilizaInfoProdutoDetalhe", supplier.InfoProductDetailAvailable));
                    sqlParameters.Add(new SqlParameter("@DescricaoPontuacaoInicial", supplier.InicialScoreDescription));
                    sqlParameters.Add(new SqlParameter("@CaracteristicasPontuacaoInicial", supplier.InicialScoreFeatures));
                    sqlParameters.Add(new SqlParameter("@LinkPontuacaoInicial", supplier.InicialScoreLink));
                    sqlParameters.Add(new SqlParameter("@ImagemPontuacaoInicial", supplier.InicialScoreImage));
                    sqlParameters.Add(new SqlParameter("@DescricaoSugereInativo", supplier.InactiveDescriptionSuggestion));
                    sqlParameters.Add(new SqlParameter("@CaracteristicasSugereInativo", supplier.InactiveFeatureSuggestion));
                    sqlParameters.Add(new SqlParameter("@LinkSugereInativo", supplier.InactiveLinkSuggestion));
                    sqlParameters.Add(new SqlParameter("@ImagemSugereInativo", supplier.InactiveImageSuggestion));
                    sqlParameters.Add(new SqlParameter("@AtualizacaoAutomaticaInativaSugestao", supplier.InactiveAutomaticUpdateSuggestion));
                    sqlParameters.Add(new SqlParameter("@ProdutosConfiancaPreco", supplier.ProductPriceTrust));
                    sqlParameters.Add(new SqlParameter("@ProdutosConfiancaDisponibilidade", supplier.ProductAvailableTrust));


                    sqlParameters.Add(new SqlParameter("@Inativo", supplier.Inactive));
                    break;

                case SqlOperationType.Update:
                    sqlParameters.Add(new SqlParameter("@Codigo", SQLStrings.CleanDangerousText(supplier.Code)));
                    sqlParameters.Add(new SqlParameter("@Nome", SQLStrings.CleanDangerousText(supplier.Name)));
                    sqlParameters.Add(new SqlParameter("@Morada", SQLStrings.CleanDangerousText(supplier.Address)));
                    sqlParameters.Add(new SqlParameter("@CodigoPostal", SQLStrings.CleanDangerousText(supplier.ZipCode)));
                    sqlParameters.Add(new SqlParameter("@LocalidadePostal", SQLStrings.CleanDangerousText(supplier.City)));
                    sqlParameters.Add(new SqlParameter("@Contribuinte", SQLStrings.CleanDangerousText(supplier.IdentificationNumber)));
                    sqlParameters.Add(new SqlParameter("@Vendedor", SQLStrings.CleanDangerousText(supplier.Salesman)));
                    sqlParameters.Add(new SqlParameter("@Telefone", SQLStrings.CleanDangerousText(supplier.Phone)));
                    sqlParameters.Add(new SqlParameter("@Telemovel", SQLStrings.CleanDangerousText(supplier.Cellphone)));
                    sqlParameters.Add(new SqlParameter("@SMS", SQLStrings.CleanDangerousText(supplier.SMS)));
                    sqlParameters.Add(new SqlParameter("@Email", SQLStrings.CleanDangerousText(supplier.Email)));
                    sqlParameters.Add(new SqlParameter("@AcessoOnlineAtivo", supplier.ActiveOnlineAccess));
                    sqlParameters.Add(new SqlParameter("@Username", SQLStrings.CleanDangerousText(supplier.Username)));
                    sqlParameters.Add(new SqlParameter("@Password", SQLStrings.CleanDangerousText(supplier.Password)));
                    sqlParameters.Add(new SqlParameter("@HorasValidadeSugestao", supplier.SuggestionExpirationHours));
                    sqlParameters.Add(new SqlParameter("@ProdutosMatchingAutomatico", supplier.AutomaticProductMatching));
                    sqlParameters.Add(new SqlParameter("@ProdutosCriacaoAutomatica", supplier.ActomaticProductCreation));
                    sqlParameters.Add(new SqlParameter("@DisponibilizaInfoProdutoDetalhe", supplier.InfoProductDetailAvailable));
                    sqlParameters.Add(new SqlParameter("@DescricaoPontuacaoInicial", supplier.InicialScoreDescription));
                    sqlParameters.Add(new SqlParameter("@CaracteristicasPontuacaoInicial", supplier.InicialScoreFeatures));
                    sqlParameters.Add(new SqlParameter("@LinkPontuacaoInicial", supplier.InicialScoreLink));
                    sqlParameters.Add(new SqlParameter("@ImagemPontuacaoInicial", supplier.InicialScoreImage));
                    sqlParameters.Add(new SqlParameter("@DescricaoSugereInativo", supplier.InactiveDescriptionSuggestion));
                    sqlParameters.Add(new SqlParameter("@CaracteristicasSugereInativo", supplier.InactiveFeatureSuggestion));
                    sqlParameters.Add(new SqlParameter("@LinkSugereInativo", supplier.InactiveLinkSuggestion));
                    sqlParameters.Add(new SqlParameter("@ImagemSugereInativo", supplier.InactiveImageSuggestion));
                    sqlParameters.Add(new SqlParameter("@AtualizacaoAutomaticaInativaSugestao", supplier.InactiveAutomaticUpdateSuggestion));
                    sqlParameters.Add(new SqlParameter("@ProdutosConfiancaPreco", supplier.ProductPriceTrust));
                    sqlParameters.Add(new SqlParameter("@ProdutosConfiancaDisponibilidade", supplier.ProductAvailableTrust));

                    sqlParameters.Add(new SqlParameter("@Inativo", supplier.Inactive));
                    sqlParameters.Add(new SqlParameter("@Versao", supplier.Version));
                    break;

                case SqlOperationType.Delete:
                    sqlParameters.Add(new SqlParameter("@Codigo", SQLStrings.CleanDangerousText(supplier.Code)));
                    sqlParameters.Add(new SqlParameter("@Versao", supplier.Version));
                    break;

                default:
                    throw new MyException(_namespace, _className, "Serialize()", string.Format("{0}!", GlobalVariables.Resource.GetString("ForeseenEnumeratorString", GlobalVariables.Culture).ToLower()));
            }

            return sqlParameters;
        }

    }
}
