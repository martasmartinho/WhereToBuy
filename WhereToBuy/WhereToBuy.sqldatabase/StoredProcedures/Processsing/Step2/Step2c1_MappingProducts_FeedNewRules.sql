/*
	This SP feeds Products Matching rules (ProdutosMatching) with new rules to be filled manually by user
	or automatic (specific to products matching) later in algorithm process.
*/


CREATE PROCEDURE [dbo].[Step2c1_MappingProducts_FeedNewRules]
	@Info nvarchar(256) OUTPUT
AS
	
	-- new rules feeding
	BEGIN TRY

		/*
			Feed (..)Matching table with new incomplete rules, to simplify mapping work to the user or to be automatically mapped later.
		*/
		INSERT [dbo].[ProdutosMatching] (FornecedorCodigo, ComplementoCodigo, Codigo, Descricao, MapTo, 
			   HorasValidadeCotacao, StockCodigoSubstituto, DispensaPrevencaoPrecosDesfasados, DispensaPrevencaoFalsoStock, DataReset, Notas,
			   Inativo, Criacao, Versao)

		SELECT um.FornecedorCodigo,
			   um.ComplementoCodigo, 
			   um.Codigo,
			   (
					-- get 1st existing description in CotacoesIncompletasMapeamentoProdutoView for this product
					SELECT TOP 1 CASE WHEN ci._Descricao IS NULL THEN '(vazio)'
									  ELSE ci._Descricao
								 END
					FROM [dbo].[CotacoesIncompletasMapeamentoProdutoView] ci
					WHERE ci.FornecedorCodigo = um.FornecedorCodigo
						  AND ci.ComplementoCodigo = um.ComplementoCodigo
						  AND ci._ProdutoCodigo = um.Codigo
				    ORDER BY ci._Descricao DESC  -- prevents the existence of some _Descricao = NULL
		      ) AS [Descricao],
			  NULL,		-- MapTo will be filled later by user or automatic next in the algorithm (specific to products)
			  NULL,		-- this null means that prevails the rules defined on CotacoesRegras. (to automatic set as inative all quotations for this supplier / product, set this value to -1)
			  NULL,		-- use this field if you want to specify a stock position directly to the product. this rule prevails to CotacoesRegras rules
			  CAST('false' as bit),	-- by default all products must be tested to offset prices.
			  CAST('false' as bit),	-- by default all products must be tested on FakeStock prevention
			  NULL,		-- Date to auto reset this custom definitions
			  NULL,
			  CAST('false' as bit),
			  GETDATE(),
			  GETDATE()
		FROM
			  (
					-- un-existing but needed matching rules
					SELECT DISTINCT ci.FornecedorCodigo, ci.ComplementoCodigo, ci._ProdutoCodigo AS Codigo
					FROM [dbo].[CotacoesIncompletasMapeamentoProdutoView] ci
					EXCEPT
					SELECT pm.FornecedorCodigo, pm.ComplementoCodigo, pm.Codigo
					FROM [dbo].[ProdutosMatching] pm
			  ) um



		-- fill output @info with sucess message
		SET @Info = '[Step2c1_MappingProducts_FeedNewRules-01] Number of created matching rules: ' + CAST(@@ROWCOUNT as nvarchar);

		-- send true to output
		RETURN 1;

	END TRY

	BEGIN CATCH
		-- fill output @info with occurred error message
		SET @Info = '[Step2c1_MappingProducts_FeedNewRules-02] ' + CAST(ERROR_NUMBER() as nvarchar) + ' - ' + ERROR_MESSAGE();

		-- send false to output
		RETURN 0;

	END CATCH

RETURN;
