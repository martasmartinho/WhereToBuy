/*
	This SP, perform Product Mapping of incomplete Quotations.
*/


CREATE PROCEDURE [dbo].[Step2c4_MappingProducts_Map]
	@Info nvarchar(256) OUTPUT
AS
	
	-- begin matching
	BEGIN TRY

		/*
			Map entities considering ProdutosMatching rules.
		*/
		UPDATE ci
		SET ProdutoCodigo = pm.MapTo,
			Versao = GETDATE()

		/*  Using CotacoesIncompletasMapeamentoProdutoView, I am assuring that I'm only working with Cotacoes where Inactivo='false' and Completo='false'
			(although, with this mapping lack, never the quotation whould be completed - so this control is redundant)
		*/
		FROM [dbo].[CotacoesIncompletasMapeamentoProdutoView] ci
			INNER JOIN [dbo].[ProdutosMatching] pm
				ON ci.FornecedorCodigo = pm.FornecedorCodigo
				   AND ci.ComplementoCodigo = pm.ComplementoCodigo
				   AND ci._ProdutoCodigo = pm.Codigo
			INNER JOIN [dbo].[Fornecedores] f
				ON ci.FornecedorCodigo = f.Codigo

		-- filter only active matching rules with MapTo already settled, and not mapped incomplete quotations
		WHERE pm.Inativo = CAST('false' as bit)
			  AND pm.MapTo IS NOT NULL
			  AND f.ProdutosMatchingAutomatico = CAST('true' as bit)



		-- fill output @info with sucess message
		SET @Info = '[Step2c4_MappingProducts_Map-01] Number of mapped quotations: ' + CAST(@@ROWCOUNT as nvarchar);

		-- send true to output
		RETURN 1;

	END TRY

	BEGIN CATCH
		-- fill output @info with occurred error message
		SET @Info = '[Step2c4_MappingProducts_Map-02] ' + CAST(ERROR_NUMBER() as nvarchar) + ' - ' + ERROR_MESSAGE();

		-- send false to output
		RETURN 0;

	END CATCH

RETURN;
