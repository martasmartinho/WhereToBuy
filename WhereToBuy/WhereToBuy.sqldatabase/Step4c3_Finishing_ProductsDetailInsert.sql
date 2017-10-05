/*
	This stored procedure will insert Products Detail (ProdutosDetalhe) table.
*/


CREATE PROCEDURE [dbo].[Step4c3_Finishing_ProductsDetailInsert]
	@Info nvarchar(256) OUTPUT
AS


	BEGIN TRY

		/*
			Insert Products Detail information
		*/
		INSERT [dbo].[ProdutosDetalhe]
			   (
				  -- pk
				   FornecedorCodigo,
				   ProdutoCodigo,
		   
				   -- description
				   Descricao,
				   DescricaoPontuacao,
				   DescricaoInativa,

				   -- features
				   Caracteristicas, 
				   CaracteristicasPontuacao,
				   CaracteristicasInativas,

				   -- link
				   Link,
				   LinkPontuacao,
				   LinkInativo,

				   -- imagem
				   Imagem,
				   ImagemPontuacao,
				   ImagemInativa,

				   -- others
				   AtualizacaoAutomaticaInativa,
				   AtualizacaoManualNecessaria,
				   IndicePreocupacaoConteudo,
				   Criacao,
				   Versao
				)


		SELECT 
			   -- pk
			   c.FornecedorCodigo,
			   c.ProdutoCodigo,

			   -- description
			   c.Descricao, 
			   f.DescricaoPontuacaoInicial,
			   f.DescricaoSugereInativo,

			   -- features
			   c.Caracteristicas,
			   f.CaracteristicasPontuacaoInicial,
			   f.CaracteristicasSugereInativo,

			   -- link
			   c.Link,
			   f.LinkPontuacaoInicial,
			   f.LinkSugereInativo,

			   -- image
			   c.Imagem,
			   f.ImagemPontuacaoInicial,
			   f.ImagemSugereInativo,

			   -- others
			   f.AtualizacaoAutomaticaInativaSugestao,
			   CAST('false' as bit),
			   [dbo].[GetProdutoDetalheIndicePreocupacaoFunction](c.Descricao, c.Caracteristicas),
			   GETDATE(),
			   GETDATE()

		FROM [dbo].[CotacoesAtuaisView] c
			 INNER JOIN (

							-- set of Actual Quotations not in ProdutosDetalhe
							SELECT DISTINCT FornecedorCodigo, ProdutoCodigo
							FROM [dbo].[CotacoesAtuaisView]
					
							EXCEPT
					
							SELECT FornecedorCodigo, ProdutoCodigo
							FROM [dbo].[ProdutosDetalhe]

						) nd
							ON c.FornecedorCodigo = nd.FornecedorCodigo
							   AND c.ProdutoCodigo = nd.ProdutoCodigo
			 INNER JOIN [dbo].[Fornecedores] f
				   ON c.FornecedorCodigo = f.Codigo




		-- fill output @info with sucess message
		SET @Info = '[Step4c3_Finishing_ProductsDetailInsert-01] Total of inserted Product Details: ' + CAST(@@ROWCOUNT as nvarchar);

		-- send true to output
		RETURN 1;

	END TRY

	BEGIN CATCH

		-- fill output @info with occurred error message
		SET @Info = '[Step4c3_Finishing_ProductsDetailInsert-02] ' + CAST(ERROR_NUMBER() as nvarchar) + ' - ' + ERROR_MESSAGE();

		-- send false to output
		RETURN 0;

	END CATCH

RETURN;
