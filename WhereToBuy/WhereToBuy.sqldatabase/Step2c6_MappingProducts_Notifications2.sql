/*
	Notifications SP: 2 of 3   (ambiguous partnumbers)

	This SP makes sure that user is notified via CotacoesAvisos table, that something 
	wrong happened in the mapping task.
*/

CREATE PROCEDURE [dbo].[Step2c5_MappingProducts_Notifications2]
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
			   'CIPM9',	-- Partnumber belongs to PartnumbersAmbiguous SET
			   'proceda à criação e mapeamento do produto manualmente!' AS Descricao,
			   GETDATE() AS Criacao

		-- consider again only quotations missing yet (after processing) ProdutoCodigo mapping.
		FROM [dbo].[CotacoesIncompletasMapeamentoProdutoView] ci
			 INNER JOIN [dbo].[ProdutosMatching] pm
				ON ci.FornecedorCodigo = pm.FornecedorCodigo
				   AND ci.ComplementoCodigo = pm.ComplementoCodigo
				   AND ci._ProdutoCodigo = pm.Codigo

		-- consider only quotations that are in Partnumbers ambiguous list
		WHERE ci.Partnumber IN
			  (
				   SELECT Partnumber FROM [dbo].[PartnumbersAmbiguosView]
			  )



		-- fill output @info with sucess message
		SET @Info = '[Step2c5_MappingProducts_Notifications2-01] Number of inserted notifications: ' + CAST(@@ROWCOUNT as nvarchar);

		-- send true to output
		RETURN 1;

	END TRY

	BEGIN CATCH
		-- fill output @info with occurred error message
		SET @Info = '[Step2c5_MappingProducts_Notifications2-02] ' + CAST(ERROR_NUMBER() as nvarchar) + ' - ' + ERROR_MESSAGE();

		-- send false to output
		RETURN 0;

	END CATCH

RETURN;
