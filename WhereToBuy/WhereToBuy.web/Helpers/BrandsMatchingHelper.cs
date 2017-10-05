using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WhereToBuy.entities;

namespace WhereToBuy.web.Helpers
{
    public class BrandsMatchingHelper
    {
        const string brandMatchingSessionVariableName = "brandMatching";
        const string brandMatchingFilterSessionVariableName = "BrandsMatchingFilter";
        



        /// <summary>
        /// this method initiates a brandMacting from its Supplier code and external code)
        /// </summary>
        public static void BrandMatchingLoad(string codigo,  string SupplierCode)
        {
            throw new NotImplementedException();
        }



        /// <summary>
        /// Initiates a brand matching from an existing one
        /// </summary>
        /// <param name="brandMatching"></param>
        public static void BrandMatchingLoad(BrandMatching brandMatching)
        {
            // limpar variaveis de sessão
            BaseHelper.SessaoReinicia();

            // saves new brandmatching
            if (brandMatching != null)
            {
                //saves brandmatching in session variable
                HttpContext.Current.Session[brandMatchingSessionVariableName] = brandMatching;
            }
        }



        /// <summary>
        /// destroys brandmatching variable
        /// </summary>
        public static void BrandMatchingKill()
        {
            if (BrandMatchingLoaded)
            {
                HttpContext.Current.Session.Remove(brandMatchingSessionVariableName);
            }
        }



        /// <summary>
        /// Check if existe a loaded brandmatching
        /// </summary>
        public static bool BrandMatchingLoaded
        {
            get
            {
                return (HttpContext.Current.Session[brandMatchingSessionVariableName] != null);
            }
        }



        /// <summary>
        /// Cliente atualmente em session
        /// Esta propriedade é readonly. Para definir um cliente, usar o método ClienteLoad() porque, além de indicar o cliente
        /// o ambiente de navegação é preparado para uma nova navegação
        /// </summary>
        public static BrandMatching BrandMatching
        {
            get
            {
                if (HttpContext.Current.Session[brandMatchingSessionVariableName] == null)
                {
                    throw new Exception(string.Format("variavel session -{0}- não existe (ClientesHelper)!", brandMatchingSessionVariableName));
                }
                return (BrandMatching)HttpContext.Current.Session[brandMatchingSessionVariableName];
            }
        }


        ///// <summary>
        ///// Este metodo guarda a ordenação selecionada atualmente pelo utilizador para visualizar os produtos habituais
        ///// </summary>
        //public static ProdutosHabituaisOrdenacao ProdutosHabituaisOrdenacao
        //{
        //    get
        //    {
        //        // se ainda não está definida a ordenacao dos produtos habituais -> faze-lo agora
        //        if (HttpContext.Current.Session[produtosHabituaisOrdenacaoSessionVariableName] == null)
        //        {
        //            // guarda o tipo de ordenacao para futuras consultas
        //            HttpContext.Current.Session[produtosHabituaisOrdenacaoSessionVariableName] = ProdutosHabituaisOrdenacao.transacoes;

        //        }
        //        return (ProdutosHabituaisOrdenacao)HttpContext.Current.Session[produtosHabituaisOrdenacaoSessionVariableName];
        //    }
        //    set
        //    {
        //        // guardar o novo critério de consulta
        //        HttpContext.Current.Session[produtosHabituaisOrdenacaoSessionVariableName] = value;
        //    }
        //}

    }
}