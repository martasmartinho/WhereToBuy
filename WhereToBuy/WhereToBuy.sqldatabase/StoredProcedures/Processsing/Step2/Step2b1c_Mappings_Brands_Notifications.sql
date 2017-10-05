/*
	This SP makes sure that user is notified via CotacoesAvisos table, that something 
	wrong happened in the mapping task.
*/

CREATE PROCEDURE [dbo].[Step2b1c_Mappings_Brands_Notifications]
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
			   CASE WHEN mm.Inativo = CAST('true' as bit) THEN 'CIMM1' -- inactive mapping rule
					ELSE 'CIMM0'									   -- incomplete mapping rule
			   END AS AvisoTipoCodigo,
			   'verifique regra de matching!' AS Descricao,
			   GETDATE() AS Criacao

		FROM [dbo].[CotacoesIncompletasView] ci
			 INNER JOIN [dbo].[MarcasMatching] mm
				ON ci.FornecedorCodigo = mm.FornecedorCodigo
					AND ci._MarcaCodigo = mm.Codigo

		-- filter all quotations not yet mapped after the mapping occurrence
		WHERE ci.MarcaCodigo IS NULL



		-- fill output @info with sucess message
		SET @Info = '[Step2b1c_Mappings_Brands_Notifications-01] Number of inserted notifications: ' + CAST(@@ROWCOUNT as nvarchar);

		-- send true to output
		RETURN 1;

	END TRY

	BEGIN CATCH
		-- fill output @info with occurred error message
		SET @Info = '[Step2b1c_Mappings_Brands_Notifications-02] ' + CAST(ERROR_NUMBER() as nvarchar) + ' - ' + ERROR_MESSAGE();

		-- send false to output
		RETURN 0;

	END CATCH

RETURN;
