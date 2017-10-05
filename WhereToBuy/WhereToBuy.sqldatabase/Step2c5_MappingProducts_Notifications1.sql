/*
	Notifications SP: 1 of 3	(not ambiguous partnumbers)

	This SP makes sure that user is notified via CotacoesAvisos table, that something 
	wrong happened in the mapping task.
*/

CREATE PROCEDURE [dbo].[Step2c5_MappingProducts_Notifications1]
	@Info nvarchar(256) OUTPUT
AS
	
	-- insert notifications on CotacoesAvisos table
	BEGIN TRY

		/*
			Insert notifications rows for all incomplete quotations that remain unmapped
		*/
		INSERT INTO [dbo].[CotacoesAvisos] 
				    (Id, FornecedorCodigo, Data, _ProdutoCodigo, _ComplementoCodigo, AvisoTipoCodigo, Descricao, Criacao)

		SELECT NEWID(), 
			   ci.FornecedorCodigo,
			   ci.Data,
			   ci._ProdutoCodigo,
			   ci._ComplementoCodigo,
			   
			   CASE WHEN pm.Inativo = CAST('true' as bit) THEN 'CIPM1'															-- rule mapping inactive
			        WHEN pm.Inativo = CAST('false' as bit) AND f.ProdutosCriacaoAutomatica = CAST('false' as bit) THEN 'CIPM2'	-- supplier can not insert products...
					WHEN pm.Inativo = CAST('false' as bit) AND f.ProdutosMatchingAutomatico = CAST('false' as bit) THEN 'CIPM3'	-- supplier can not do automatic matching
					ELSE 'CIPM0'																								-- mapping rule is incomplete (misses MapTo)
			   END AS AvisoTipoCodigo,

			   'verifique configurações de matching!' AS Descricao,
			   GETDATE() AS Criacao

		-- consider again only quotations missing yet (after processing) ProdutoCodigo mapping.
		FROM [dbo].[CotacoesIncompletasMapeamentoProdutoView] ci
			 INNER JOIN [dbo].[ProdutosMatching] pm
				ON ci.FornecedorCodigo = pm.FornecedorCodigo
				   AND ci.ComplementoCodigo = pm.ComplementoCodigo
				   AND ci._ProdutoCodigo = pm.Codigo
			 INNER JOIN [dbo].[Fornecedores] f
				ON ci.FornecedorCodigo = f.Codigo

		-- consider only quotations not in Partnumbers ambiguous list
		WHERE ci.Partnumber NOT IN
			  (
				   SELECT Partnumber FROM [dbo].[PartnumbersAmbiguosView]
			  )




		-- fill output @info with sucess message
		SET @Info = '[Step2c5_MappingProducts_Notifications1-01] Number of inserted notifications: ' + CAST(@@ROWCOUNT as nvarchar);

		-- send true to output
		RETURN 1;

	END TRY

	BEGIN CATCH
		-- fill output @info with occurred error message
		SET @Info = '[Step2c5_MappingProducts_Notifications1-02] ' + CAST(ERROR_NUMBER() as nvarchar) + ' - ' + ERROR_MESSAGE();

		-- send false to output
		RETURN 0;

	END CATCH

RETURN;
